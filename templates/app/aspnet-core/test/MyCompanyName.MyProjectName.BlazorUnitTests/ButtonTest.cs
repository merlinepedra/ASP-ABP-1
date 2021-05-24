using System;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using MyCompanyName.MyProjectName.BlazorUnitTests.Mocks;
using Xunit;

namespace MyCompanyName.MyProjectName.BlazorUnitTests
{
    public class ButtonTest
    {
        private readonly EventCallbackFactory callbackFactory = new EventCallbackFactory();

        [Fact]
        public void SetFocus()
        {
            // setup
            var button = new MockButton();
            var expectedId = button.ElementId;

            // test
            button.Focus();

            // validate
            Assert.Equal(expectedId, button.FocusedId);
        }

        [Fact]
        public void SetParentDropdown()
        {
            // setup
            var drop = new Dropdown();
            var button = new MockButton(drop);

            // test
            button.Dispose();

            // validate
        }

        [Fact]
        public void SetParentAddons()
        {
            // setup
            var a = new Addons();
            var button = new MockButton(parentAddons: a);

            // test
            button.Dispose();

            // validate
            Assert.False(button.IsAddons);
        }

        [Fact]
        public void SetParentButtons()
        {
            // setup
            var b = new Buttons();
            var button = new MockButton(parentButtons: b);

            // test
            button.Dispose();

            // validate
            Assert.True(button.IsAddons);
        }

        [Fact]
        public async Task ClickWithEventCallback()
        {
            // setup
            var button = new MockButton();
            bool clicked = false;

            // test
            button.Clicked = callbackFactory.Create(this, () => { clicked = true; });
            await button.Click();

            // validate
            Assert.True(clicked);
        }

        [Fact]
        public async Task ClickWithCommand()
        {
            // setup
            var button = new MockButton();
            string result = null;
            button.Command = new TestCommand(p => result = p);
            button.CommandParameter = new TestCommandParameter { Message = "foo" };

            // test
            await button.Click();

            // validate
            Assert.NotNull(result);
            Assert.Equal("foo", result);
        }

        class TestCommand : System.Windows.Input.ICommand
        {
            private readonly Action<string> callback;

            public TestCommand(Action<string> callback)
            {
                this.callback = callback;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
                => parameter is TestCommandParameter param && !string.IsNullOrWhiteSpace(param.Message);

            public void Execute(object parameter)
            {
                var result = parameter is TestCommandParameter param ? param.Message : "NoParam";
                this.callback.Invoke(result);
            }

            public void FireCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        class TestCommandParameter
        {
            public string Message { get; set; }
        }
    }
}