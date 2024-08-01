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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ParseAcordForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"c:\",
                Title = "Browse pdf Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "pdf",
                Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            string fileName = "";
            List<string> strCollect = new List<string>();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                string item = "";
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while ((item = sr.ReadLine()) != null)
                    {
                        if (item.Contains(" /T ("))
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                strCollect.Add(item);
                            }
                        }
                    }
                }
                List<string> fields = new List<string>();
                for(int a = 0; a < strCollect.Count;a++)
                {
                    string[] t = strCollect[a].Replace(" /T (", "*").Split('*');
                    string[] te = t[1].Replace(") /TU (", "*").Replace(") /Type ", "*").Split('*');
                    string fieldName = te[0].Trim();
                    string tooltip = te[1].Trim();
                    fields.Add(fieldName + "*" + tooltip);
                }

                using (StreamWriter sw = new StreamWriter(@"C:\Users\Digit\Desktop\commercialPropertyFields.txt"))
                {
                    foreach(string str in fields)
                    {
                        sw.WriteLine(str);
                    }
                }
            }
        }
    }
}
