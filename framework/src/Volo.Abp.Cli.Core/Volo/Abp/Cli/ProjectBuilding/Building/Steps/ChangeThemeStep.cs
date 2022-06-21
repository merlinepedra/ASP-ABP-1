using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Volo.Abp.Cli.ProjectBuilding.Files;
using Volo.Abp.Cli.Utils;

namespace Volo.Abp.Cli.ProjectBuilding.Building.Steps;

public class ChangeThemeStep : ProjectBuildPipelineStep
{
    public override void Execute(ProjectBuildContext context)
    {
        switch (context.BuildArgs.Theme)
        {
            case Theme.Lepton:
            {
                ToLepton(context);
                break;
            }
        }
    }
    
    private static void ToLepton(ProjectBuildContext context)
    {
        var removedLeptonXReferenceProjectFiles = RemoveLeptonXReference(context);
        AddMvcUiThemeLeptonReference(removedLeptonXReferenceProjectFiles);
        var addedLeptonThemeManagementReferenceProjectFiles = AddLeptonThemeManagementReference(context);

        var changedModuleFiles = GetModuleFilesByProjectFiles(context, addedLeptonThemeManagementReferenceProjectFiles).ToList();
        AddLeptonThemeManagementToModule(changedModuleFiles);
        ChangedMvcUiThemeLeptonXToMvcUiThemeLepton(context);

        RenameLeptonXFoldersToLepton(context);
    }

    private static List<FileEntry> RemoveLeptonXReference(ProjectBuildContext context)
    {
        var changedFileEntries = new List<FileEntry>();
        
        var relatedProjects = context.Files.Where(f => f.Content.Contains("Lepton")).ToList();
        var projectFiles = relatedProjects.Where(f => f.Name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase));
        foreach (var file in projectFiles)
        {
            var projectFileLength = file.Bytes.Length;
            file.RemoveLineByContains("Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX");
            if (projectFileLength != file.Bytes.Length)
            {
                changedFileEntries.Add(file);
            }
        }

        return changedFileEntries;
    }

    private static void AddMvcUiThemeLeptonReference(List<FileEntry> projectFiles)
    {
        const string reference = "..\\..\\..\\..\\..\\lepton-theme\\src\\Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton\\Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton.csproj";
        foreach (var projectFile in projectFiles)
        {
            AddProjectReference(projectFile, reference);
        }
    }
    
    private static List<FileEntry> AddLeptonThemeManagementReference(ProjectBuildContext context)
    {
        var changedFiles = new List<FileEntry>();
        
        var projectFiles = context.Files.Where(f => !f.Name.Contains("Test") && f.Name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase));
        
        foreach (var file in projectFiles)
        {
            var fileLength = file.Bytes.Length;
            var projectType = GetValidProjectTypeForLeptonThemeManagementReference(file.Name);
            if (projectType.IsNullOrEmpty())
            {
                continue;
            }
            var reference = $"..\\..\\..\\..\\..\\lepton-theme\\src\\Volo.Abp.LeptonTheme.Management.{projectType}\\Volo.Abp.LeptonTheme.Management.{projectType}.csproj";
            AddProjectReference(file, reference, projectType);
            if (fileLength != file.Bytes.Length)
            {
                changedFiles.Add(file);
            }
        }

        return changedFiles;
    }

    private static void AddProjectReference(FileEntry file, string reference, string projectType = null)
    {
        var doc = new XmlDocument() { PreserveWhitespace = true };
        using (var stream = StreamHelper.GenerateStreamFromString(file.Content))
        {
            doc.Load(stream);

            var itemGroupNodes = doc.SelectNodes("/Project/ItemGroup");
            XmlNode itemGroupNode = null;

            if (itemGroupNodes == null || itemGroupNodes.Count < 1)
            {
                var projectNodes = doc.SelectNodes("/Project");
                var projectNode = projectNodes![0];

                itemGroupNode = doc.CreateElement("ItemGroup");
                projectNode.AppendChild(itemGroupNode);
            }
            else
            {
                for (var i = 0; i < itemGroupNodes.Count; i++)
                {
                    for (var j = 0; j < itemGroupNodes[i].ChildNodes.Count; j++)
                    {
                        if (itemGroupNodes[i].ChildNodes[j].Name != "ProjectReference" || itemGroupNodes[i].ChildNodes[j].NodeType != XmlNodeType.Element)
                        {
                            continue;
                        }

                        itemGroupNode = itemGroupNodes[i];
                        break;
                    }
                }
            }
            
            itemGroupNode ??= itemGroupNodes[0];
            
            var packageReferenceNode = doc.CreateElement("ProjectReference");

            var includeAttr = doc.CreateAttribute("Include");
            includeAttr.Value = reference;
            packageReferenceNode.Attributes.Append(includeAttr);
            
            itemGroupNode.AppendChild(doc.CreateTextNode("\t"));
            itemGroupNode.AppendChild(packageReferenceNode);
            itemGroupNode.AppendChild(doc.CreateTextNode(Environment.NewLine + "\t"));
            file.SetContent(doc.OuterXml);
        }
    }

    // TODO: refactor this method
    private static string GetValidProjectTypeForLeptonThemeManagementReference(string name)
    {
        if (name.Contains("EntityFramework") || name.Contains("MongoDB") || name.Contains("DbMigrator"))
        {
            return null;
        }

        name = name.Replace(".csproj", "");
        
        var splittedName = name.Split('.');

        var projectName = splittedName.Last();
        if (projectName == "Public")
        {
            return "Web";
        }

        if (name.Contains("HttpApi.") || name.Contains("Domain.") || name.Contains("Application."))
        {
            name = name.Replace(projectName, "");

            name = name.RemovePostFix(".");

            projectName = projectName.Insert(0,  name.Split('.').Last() + ".");
        }

        // HttpApi.Host or HttpApi
        return projectName;
    }

    private static void ChangedMvcUiThemeLeptonXToMvcUiThemeLepton(ProjectBuildContext context)
    {
        var moduleFiles = context.Files.Where(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var file in moduleFiles)
        {
            file.SetContent(file.Content.Replace("LeptonX", "Lepton"));
        }
    }

    // TODO: use the symbol as empty placeholder for LeptonThemeManagement
    private static void AddLeptonThemeManagementToModule(List<FileEntry> moduleFiles)
    {
        foreach (var file in moduleFiles)
        {
            file.SetContent(file.Content
                .Replace("using Volo.Abp.LanguageManagement;", "using Volo.Abp.LeptonTheme.Management;" + Environment.NewLine + "using Volo.Abp.LanguageManagement;"));
            
           var projectType = GetValidProjectTypeForLeptonThemeManagementReference(file.Name);
           file.SetContent(file.Content
                .Replace($"typeof(LanguageManagement{projectType}Module),", $"typeof(LeptonThemeManagement{projectType}Module)," + Environment.NewLine + $"typeof(LanguageManagement{projectType}Module),"));
        }
    }

    private static IEnumerable<FileEntry> GetModuleFilesByProjectFiles(ProjectBuildContext context, List<FileEntry> projectFiles)
    {
        var moduleFileNames = new List<string>();
        foreach (var file in projectFiles)
        {
            var splittedNames = file.Name.RemovePostFix("/").Split("/");
            var fileName = splittedNames.Last();
            fileName = fileName
                .Replace("MyCompanyName.", "")
                .Replace(".csproj", "Module")
                .Replace(".", "");
            
            moduleFileNames.Add(fileName);
        }

        foreach (var fileName in moduleFileNames)
        {
            yield return context.Files.First(f => f.Name.Contains(fileName));
        }
    }

    private static void RenameLeptonXFoldersToLepton(ProjectBuildContext context)
    {
        var leptonXFiles = context.Files.Where(x => x.Name.Contains("LeptonX") && x.IsDirectory);
        foreach (var file in leptonXFiles)
        {
            new MoveFolderStep(file.Name, file.Name.Replace("LeptonX", "Lepton")).Execute(context);
        }
    }
}