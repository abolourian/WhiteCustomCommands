namespace Tests
{
    using System;
    using System.Runtime.Serialization;
    using System.Windows.Media;
    using System.Xml;

    namespace TestWithWhite
    {
        public interface ITextBoxCommands
        {
            Background Background { get; }
            //        Validation Validation { get; }
        }

        public class TextBoxCommands : ITextBoxCommands
        {
            private readonly System.Windows.Controls.TextBox _textBox;

            public TextBoxCommands(System.Windows.Controls.TextBox textBox)
            {
                _textBox = textBox;
            }

            public Background Background
            {
                get { return new Background(_textBox.Background); }
            }

            public Validation Validation
            {
                get { return new Validation(_textBox); }
            }
        }

        [DataContract]
        public class Validation
        {
            [DataMember]
            private readonly bool _hasError;

            public Validation(System.Windows.Controls.TextBox textBox)
            {
                _hasError = System.Windows.Controls.Validation.GetHasError(textBox);
            }

            public bool HasError
            {
                get { return _hasError; }
            }
        }

        [DataContract]
        public class Background
        {
            [DataMember]
            private readonly string _color;

            public Background(Brush brush)
            {
                if (brush is SolidColorBrush)
                {
                    _color = (brush as SolidColorBrush).Color.ToString();
                }
                else
                {
                    _color = "Bad brush!";
                }
            }

            public string Color
            {
                get { return _color; }
            }
        }
    }
}