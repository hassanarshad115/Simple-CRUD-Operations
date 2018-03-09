using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_CRUD_Operations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetData();

        }


        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from itbl", conn);
            adapter.Fill(dt);
            return dt;
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //add new record pr click kro tw ak method call hoy
            methodInFirstForm(0, false);
        }

        //jo method uper declare kia ha uski coding
        private void methodInFirstForm(int idV, bool UpdateV)
        {
            //insert wala form open hoy r usmy jo 2 properties bnae ti unhy asy access krna ha
            Form2 obj = new Form2();
            obj.idP = idV;
            obj.isUpdateP = UpdateV;
            obj.Show();
        }

        //datagridview pr double click kr k jo event hoga..
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int updateObj = (int)dataGridView1.Rows[index].Cells[0].Value;
            //jo method upser add new record ko jb click krty ha usmy bnaya ta usko yha agian call ki but obj r true pas krdia
            methodInFirstForm(updateObj, true);
            //bs datagridview ma jst ye 3 lines he likhni  ha..
        }
    }
}
