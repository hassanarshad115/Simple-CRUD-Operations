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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //insert waly ma properties bnaygy na k gridview waly ma
        public int idP { get; set; }
        public bool isUpdateP { get; set; }

        //isUpdateP k ander update ka method he call krana ha else ma save krna ha yad rkhna
        //insert ma jo save button ha ye code uska ha
        private void button1_Click(object sender, EventArgs e)
        {
            if (isUpdateP)
            {
                UpdateRecordMethod();
            }
            else
            {
                SaveRecordMethod();
            }

        }
        //whe porana treeka save r update wala
        private void SaveRecordMethod()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("i", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@n", textBox1.Text);
            cmd.Parameters.AddWithValue("@fn", textBox2.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Test successfully");
        }

        private void UpdateRecordMethod()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("up", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@n", textBox1.Text);
            cmd.Parameters.AddWithValue("@fn", textBox2.Text);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Test successfully");
        }


        //ye nichy waly 2 kam insert form k lod event ma hongy
        private void Form2_Load(object sender, EventArgs e)
        {
            if (isUpdateP)
            {
                DataTable dt = GetDataByIDMethod(idP);//ak method bnaygy ismy id property pass krygy
                DataRow dr = dt.Rows[0]; //datarow ka obj bnaygy jsko datatabke ka obj with Rows[0] k sath jor dyngy

                //jtny b textbox ka combobox ya images honge unko nichy asy likhna ha
                textBox1.Text = dr["name"].ToString();
                textBox2.Text = dr["fatherName"].ToString();
            }
        }

        private DataTable GetDataByIDMethod(int idPMv) 
        {
            //sbsy phly datatale ka obj bnana ha jo k last m return b hoga
            DataTable dt = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("IdAccess", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", idPMv);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);

            conn.Close();

            return dt;
        }
    }
}
