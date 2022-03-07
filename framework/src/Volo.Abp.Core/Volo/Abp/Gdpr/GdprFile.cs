using System.Collections.Generic;

namespace Volo.Abp.Gdpr;

public class GdprFile
{
    public string Name { get; protected set; }

    public string Type { get; protected set; }

    public Dictionary<string, string> Content { get; protected set; }

    public GdprFile(string name, string type, Dictionary<string, string> content)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        Type = Check.NotNullOrWhiteSpace(type, nameof(type));
        Content = content ?? new Dictionary<string, string>();
    }
}