using MyCompanyName.MyProjectName.BlazorE2ETests.Infrastructure;
using MyCompanyName.MyProjectName.BlazorE2ETests.Infrastructure.ServerFixtures;
using MyCompanyName.MyProjectName.BlazorTestApp.Client;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using DevHostServerProgram = MyCompanyName.MyProjectName.BlazorTestApp.Server.Program;

namespace MyCompanyName.MyProjectName.BlazorE2ETests
{
    public class UiMessageAlertTest : BasicTestAppTestBase
    {
        public UiMessageAlertTest(BrowserFixture browserFixture,
            ToggleExecutionModeServerFixture<DevHostServerProgram> serverFixture,
            ITestOutputHelper output)
            : base(browserFixture, serverFixture, output)
        {
            Navigate(ServerPathBase, noReload: !serverFixture.UsingAspNetHost);
            MountTestComponent<UiMessageAlertComponent>();
        }

        [Fact]
        public void CanFindModal()
        {
            var modal = Browser.FindElement(By.ClassName("modal"));

            WaitAssert.NotNull(() => modal);
        }

        [Fact]
        public void CanShowModal()
        {
            var button = Browser.FindElement(By.Id("btn_show_modal"));

            button.Click();

            var modal = Browser.FindElement(By.CssSelector(".modal.fade.show"));

            WaitAssert.NotNull(() => modal);
        }
    }
}
