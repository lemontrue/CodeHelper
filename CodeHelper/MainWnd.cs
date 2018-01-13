namespace CodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    public partial class MainWnd : Form
    {
        private string[] files { get; set; }
        public MainWnd()
        {
            InitializeComponent();
        }

        private void ChooseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            files = openFileDialog.FileNames;
            foreach (string name in files)
            {
                listView.Items.Add(name);
            }

            PasteBtn.Visible = CheckBtn.Visible = true;
        }

        private void PasteBtn_Click(object sender, EventArgs e)
        {
            PasteWnd wnd = new PasteWnd(PasteWndType.Paste, files);
            wnd.Show();
        }

        private void CheckBtn_Click(object sender, EventArgs e)
        {
            PasteWnd wnd = new PasteWnd(PasteWndType.Check, files);
            wnd.Show();
        }
    }
}
