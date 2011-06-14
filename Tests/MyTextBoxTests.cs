using System.Windows.Media;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tests.TestWithWhite;
using White.Core;
using White.Core.CustomCommands;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems;

namespace Tests
{
    [TestFixture]
    public class MyTextBoxTests
    {
        private Application _application;
        private Window _horizonWindow;

        [SetUp]
        public void Setup()
        {
            _application = LaunchApplication();
            _horizonWindow = FindMainWindow();

            CustomCommandSerializer.AddKnownTypes(typeof(Background));
        }

        [TearDown]
        public void TearDown()
        {
            _application.Kill();
        }

        [Test]
        public void TestCustomCommands()
        {
            var myTextBox = _horizonWindow.Get<TextBox>("myTextBox");
            var button = _horizonWindow.Get<Button>("changeColorButton");
            var myCommands = new CustomCommandFactory().Create<ITextBoxCommands>(myTextBox);

            myTextBox.Text = "Hello";

            Assert.That(myCommands.Background.Color, Is.EqualTo(Colors.White.ToString()));
            Assert.That(myTextBox.Text, Is.EqualTo("Hello"));

            button.Click();

            // These two asserts fail
//            Assert.That(myCommands.Background.Color, Is.EqualTo(Colors.LightBlue.ToString()));
//            Assert.That(myTextBox.Text, Is.EqualTo("Hello"));

            // These two asserts pass
            Assert.That(myTextBox.Text, Is.EqualTo("Hello"));
            Assert.That(myCommands.Background.Color, Is.EqualTo(Colors.LightBlue.ToString()));
        }

        private Window FindMainWindow()
        {
            return _application.GetWindow("MainWindow");
        }

        private Application LaunchApplication()
        {
            return Application.Launch(@"C:\Projects\Sandbox\WhiteCustomCommands\TestApp\bin\Debug\TestApp.exe");
        }

    }
}
