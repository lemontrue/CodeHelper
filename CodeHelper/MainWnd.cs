namespace CodeHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    public partial class MainWnd : Form
    {
        private List<string> files { get; set; }
        public MainWnd()
        {
            InitializeComponent();
        }

        private void ChooseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSharp files(*.cs)|*.cs";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            files = openFileDialog.FileNames?.ToList();
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

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < listView.SelectedItems.Count; i++)
            {
                var element = files.FirstOrDefault(x => x == listView.SelectedItems[i].Text);
                files.Remove(element);

                listView.SelectedItems[i].Remove();
            }
        }
    }
}
