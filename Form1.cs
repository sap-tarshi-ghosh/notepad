using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace notepad
{
    public partial class Form1 : Form
    {
        
        private int tabCount = 0; //to keep track of number of new tabs
        public Form1()
        {
            InitializeComponent();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //richTextBox1.Clear();
            tabCount++;

            TabPage newPage = new TabPage("Untitled" + tabCount);

            RichTextBox rtb = new RichTextBox();
            rtb.Dock = DockStyle.Fill;

            newPage.Controls.Add(rtb);  //RichTextBox lives inside the tab.

            tabControl1.Controls.Add(newPage); //opens the new tab abd puts the tab into TabControl (the one you dragged from Toolbox)

            tabControl1.SelectedTab = newPage;  //the new tab is getting active here
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TabPage currentTab = tabControl1.SelectedTab; // selecting current tab

                if (currentTab != null && currentTab.Controls.Count > 0)
                {
                    RichTextBox rtb = currentTab.Controls[0] as RichTextBox;
                    if (rtb != null)
                    {
                        rtb.Text = File.ReadAllText(openFileDialog1.FileName);
                        currentTab.Text = Path.GetFileName(openFileDialog1.FileName); //this wil rename the tab title
                    }
                }
                
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage currentTab = tabControl1.SelectedTab; //getting current tab

            if (currentTab != null && currentTab.Controls.Count > 0)
            {
                RichTextBox rtb = currentTab.Controls[0] as RichTextBox;

                if (rtb != null)
                {

                    if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog1.FileName, rtb.Text);

                        currentTab.Text = Path.GetFileName(saveFileDialog1.FileName); //showing the file name in the tab
                    }
                }
            }


        }

        private void fontToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = fontDialog1.Font;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionColor = colorDialog1.Color;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //in this section we were only cosing the tab without saving the current contents


            //if (tabControl1.TabPages.Count > 0) //tab control feature
            //{
            //    TabPage currentTab = tabControl1.SelectedTab; //seleting the currently open tab
            //    if (currentTab != null)
            //    {
            //        tabControl1.TabPages.Remove(currentTab); //closing that tab
            //    }
            //}


            //in this section we are saving the contents of the tab before closing the tab

            if (tabControl1.TabPages.Count > 0) //tab control feature
            {
                TabPage currentTab = tabControl1.SelectedTab; //seleting the currently open tab
                if (currentTab != null)
                {
                    DialogResult result = MessageBox.Show("Sure about closing the current tab after saving the data ?",
                        "Confirm",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        saveToolStripMenuItem_Click(sender, e);  //savint the content before saving
                        tabControl1.TabPages.Remove(currentTab);
                    }

                    else if (result == DialogResult.No)
                    {
                        tabControl1.TabPages.Remove(currentTab); //closing that tab
                    }
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
