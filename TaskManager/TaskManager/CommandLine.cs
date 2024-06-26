using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TaskManager
{
	public partial class CommandLine : Form
	{
		public ComboBox ComboBoxFileName
		{
			get
			{
				return comboBoxFilename;
			}
		}
		public CommandLine()
		{
			InitializeComponent();
			Load();
			this.AcceptButton = this.buttonOK;
		}
		public void Load()
		{
			StreamReader sr = new StreamReader("ProgramList.txt");

			while (!sr.EndOfStream)
			{
				string item = sr.ReadLine();
				comboBoxFilename.Items.Add(item);
			}
			comboBoxFilename.Text = comboBoxFilename.Items[0].ToString();

			sr.Close();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{

			try
			{
				string text = comboBoxFilename.Text;
				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(text);
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				process.StartInfo = startInfo;
				process.Start();
				//if(!comboBoxFilename.Items.Contains(comboBoxFilename.Text))
				//	comboBoxFilename.Items.Insert(0, comboBoxFilename.Text);
				comboBoxFilename.Items.Remove(text);
				comboBoxFilename.Text = (text);
				comboBoxFilename.Items.Insert(0, text);
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				
			}
		}

        private void comboBoxFilename_KeyDown(object sender, KeyEventArgs e)
        {
			if(e.KeyValue == (char)Keys.Enter) buttonOK_Click(sender, e);
            if (e.KeyValue == (char)Keys.Escape) Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
			DialogResult result = openFile.ShowDialog();
			if(result == DialogResult.OK)
			{
				comboBoxFilename.Text=openFile.FileName;
			}
			
        }
    }
}
