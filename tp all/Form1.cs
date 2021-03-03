using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tp_all
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ADO d = new ADO();
        public static int cpt = 0;
        public void DisplayData()
        {
            Clear();
            d.cmd.CommandText = "select * from stagaire";
            d.cmd.Connection = d.cnx;
            d.dap = new SqlDataAdapter(d.cmd);
            d.dap.Fill(d.ds, "stagaire");
            dataGridView1.DataSource = d.ds.Tables["stagaire"];
        }
        public void DisplayByRadio(string term,string field)
        {
            Clear();
            d.cmd.CommandText = "select * from stagaire where "+field+"="+term;
            d.cmd.Connection = d.cnx;
            d.dap = new SqlDataAdapter(d.cmd);
            d.dap.Fill(d.ds, "stagaire");
            dataGridView1.DataSource = d.ds.Tables["stagaire"];
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            d.Connecter();
            DisplayData();
            Naviger(cpt);
        }
        public void Clear()
        {
            d.ds.Clear();
            /*
             
             MessageBox.Show(dataGridView1.Rows.Count.ToString());
            //dataGridView1.Rows.Clear();
            
             
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                d.ds.Tables["stagaire"].Rows[i].Delete();
            }
            M
            */


        }
        public void AddStagaire()
        {
            Clear();
            d.dr = d.ds.Tables["stagaire"].NewRow();
            d.dr[1] = txtNom.Text;
            d.dr[2] = txtPrenom.Text;
            d.dr[3] = txtMoyenne.Text;
            d.dr[4] = txtAge.Text;
            d.ds.Tables["stagaire"].Rows.Add(d.dr);
            d.cb = new SqlCommandBuilder(d.dap);
            d.dap.Update(d.ds, "stagaire");
            //Clear();
            DisplayData();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            AddStagaire();
        }
        public void Naviger(int index)
        {
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Selected = false;
            }
            dataGridView1.Rows[index].Selected = true;
            AffectValue(index);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            cpt = 0;

            Naviger(cpt);
                
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            cpt = dataGridView1.Rows.Count - 2;
            MessageBox.Show(cpt.ToString());
            Naviger(cpt);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            cpt = cpt + 1;
            Naviger(cpt);
            dataGridView1.Rows[cpt - 1].Selected = false;

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            cpt = cpt - 1;
            Naviger(cpt);
            dataGridView1.Rows[cpt + 1].Selected = false;
        }
        public void delete(int index)
        {
            //MessageBox.Show(index.ToString());

            d.ds.Tables["stagaire"].Rows[index].Delete();
            d.cb = new SqlCommandBuilder(d.dap);
            d.dap.Update(d.ds, "stagaire");
            Clear();
            DisplayData();
            /*
             
              d.cb = new SqlCommandBuilder(d.dap);
            d.dap.Update(d.ds, "stagaire");
             */
            //dataGridView1.DataSource = d.ds.Tables["stagaire"];

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete(cpt);
            DisplayData();
        }
        public void AffectValue(int index)
        {
            //MessageBox.Show(index.ToString());
            txtMatricule.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            txtNom.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            txtPrenom.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            txtMoyenne.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
            txtAge.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
        }
        public void Modifier(int index)
        {
            d.dr = d.ds.Tables["stagaire"].Rows[index];
            d.dr["matricule"] = txtMatricule.Text;
            d.dr["nom"] = txtNom.Text;
            d.dr["prenom"] = txtPrenom.Text;
            d.dr["moyenne"] = txtMoyenne.Text;
            d.dr["age"] = txtAge.Text;
            d.cb = new SqlCommandBuilder(d.dap);
            d.dap.Update(d.ds, "stagaire");
            dataGridView1.DataSource = d.ds.Tables["stagaire"];
        }
        private void updatebtn_Click(object sender, EventArgs e)
        {
            Modifier(cpt);
        }
       
        private void clearbtn_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void afficherbtn_Click(object sender, EventArgs e)
        {
            //if(dataGridView1.Rows.Count == 0)
            //{
                DisplayData();
            //}
            
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if(matradio.Checked == true)
            {
                //MessageBox.Show("mat");
                DisplayByRadio("matricule", txtSearch.Text);
            }
            else if(nomradio.Checked == true)
            {
                //MessageBox.Show("nom");
                DisplayByRadio("nom", txtSearch.Text);
            }
            else
            {
                MessageBox.Show("no field selected");
            }
        }
    }
}
