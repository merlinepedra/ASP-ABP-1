using MyCompanyName.MyProjectName.BlazorE2ETests.Infrastructure;
using MyCompanyName.MyProjectName.BlazorE2ETests.Infrastructure.ServerFixtures;
using MyCompanyName.MyProjectName.BlazorTestApp.Client;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using DevHostServerProgram = MyCompanyName.MyProjectName.BlazorTestApp.Server.Program;

namespace MyCompanyName.MyProjectName.BlazorE2ETests
{
    public class ButtonTest : BasicTestAppTestBase
    {
        public ButtonTest(BrowserFixture browserFixture,
            ToggleExecutionModeServerFixture<DevHostServerProgram> serverFixture,
            ITestOutputHelper output)
            : base(browserFixture, serverFixture, output)
        {
            Navigate(ServerPathBase, noReload: !serverFixture.UsingAspNetHost);
            MountTestComponent<ButtonComponent>();
        }

        [Fact]
        public void CanRaiseCallback()
        {
            var paragraph = Browser.FindElement(By.Id("basic-button-event"));
            var button = paragraph.FindElement(By.TagName("button"));
            var result = paragraph.FindElement(By.Id("basic-button-event-result"));

            WaitAssert.Equal("0", () => result.Text);

            button.Click();
            WaitAssert.Equal("1", () => result.Text);

            button.Click();
            WaitAssert.Equal("2", () => result.Text);
        }
    }
}
