using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExeptionsSuGUI
{

    public class PesionAge: Exception
    {
        public int PensionAgeYear;
        public int YearAfterPension;

        public PesionAge(int pensionAgeYear, int Age)
        {
            PensionAgeYear = pensionAgeYear;
            YearAfterPension = pensionAgeYear - Age;
}
    }

    public partial class Form1 : Form
    {
        string path = null;
        public Form1()
        {
            InitializeComponent();

            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader("trump.txt");
                string visa_informacija = reader.ReadToEnd();
                string[] trumpiniai = visa_informacija.Split(' ').ToArray();
                foreach(string temp in trumpiniai)
                {
                    comboPeople.Items.Add(temp);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void Update(object sender, EventArgs e)
        {
            try
            {
               if(path == null)
                {
                    throw new MissingMemberException();
                }
                System.IO.StreamReader reader = new System.IO.StreamReader(path);
                string visiduomenys = reader.ReadToEnd();
                string[] eilutes = visiduomenys.Split(';').ToArray();
                List<string[]> paskaldyta = new List<string[]>();
                foreach (string eil in eilutes)
                {
                    paskaldyta.Add(eil.Split(' ').ToArray());
                }
                foreach (string[] tempo in paskaldyta)
                {
                    if (comboPeople.SelectedItem.ToString() == tempo[0])
                    {
                        textName.Text = tempo[1];
                        textSurname.Text = tempo[2];
                        textYear.Text = tempo[3];
                        textPayroll.Text = tempo[4];
                        textWorkYear.Text = tempo[5];
                    }
                }
                int metai = int.Parse(textYear.Text);
                metai /= 10000;
                if(metai < 1951)
                {
                    throw new PesionAge(1951, metai);
                }

            }
            catch (PesionAge ex)
            {
                MessageBox.Show("Zmogus dirba po pensijinio amziaus: " + ex.YearAfterPension);
            }
            catch(MissingMemberException ex)
            {

            }
        }

        private void bUpadate_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboPeople.SelectedItem == null)
                {
                    throw new EntryPointNotFoundException();
                }
                
                OpenFileDialog ofd = new OpenFileDialog();
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    path = ofd.FileName;
                }

                System.IO.StreamReader reader = new System.IO.StreamReader(path);
                string visiduomenys = reader.ReadToEnd();
                string[] eilutes = visiduomenys.Split(';').ToArray();
                List<string[]> paskaldyta = new List<string[]>();
                foreach(string eil in eilutes)
                {
                    paskaldyta.Add(eil.Split(' ').ToArray());
                }
                foreach(string[] tempo in paskaldyta)
                {
                    if(comboPeople.SelectedItem.ToString() == tempo[0])
                    {
                        textName.Text = tempo[1];
                        textSurname.Text = tempo[2];
                        textYear.Text = tempo[3];
                        textPayroll.Text = tempo[4];
                        textWorkYear.Text = tempo[5];
                    }
                }



            }
            catch(EntryPointNotFoundException ex)
            {
                MessageBox.Show("Nepasirinktas elementas");
            }
        }
    }
}
