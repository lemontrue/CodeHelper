namespace CodeHelper
{
    using System;
    using System.Windows.Forms;
    using System.IO;
    public partial class PasteWnd : Form
    {
        public PasteWndType Destination { get; set; }
        public string[] files { get; set; }

        public PasteWnd()
        {
            InitializeComponent();
        }

        public PasteWnd(PasteWndType destination, string[] files)
        {
            InitializeComponent();

            this.Destination = destination;
            Button.Text = destination == PasteWndType.Paste ? "Paste Code" : "Check Code";
            
            this.files = files;
        }

        private void Button_clck(object sender, EventArgs e)
        {
            if (Destination == PasteWndType.Paste)
            {
                PasteCode();
            }
            else
            {
                CheckCode();
            }

            this.Close();
        }

        private void PasteCode()
        {
            // later..
        }

        private void CheckCode()
        {
            foreach (string file in files)
            {
                string text = File.ReadAllText(file);

                string valid, resultString;
                valid = resultString = String.Empty;

                foreach (var str in FormatRichTextBox())
                {
                    if (text.Contains(str))
                    {
                        valid += $"Файл {file} содержит введенный текст" + "\n";
                    }
                    else
                    {
                        resultString += $"Данный текст в файле { file } не встречается" + "\n";
                    }
                }
                
                resultString += !string.IsNullOrEmpty(valid)
                    ? $"В файле {file} найдены следующие поля:" + "\n"
                    : $"В файле { file } не найдено совпадений";

                if (!string.IsNullOrEmpty(resultString))
                {
                    MessageBox.Show(resultString);
                }
            }
        }

        private string[] FormatRichTextBox()
        {
            string text = richTextBox1.Text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                string[] resultStrings = text.Split(new char[] { '\n' });
                return resultStrings;
            }

            return new string[] { };
        }
    }
}
