using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
            cmd.Parameters.AddWithValue("@image", SaveImageInDatabase());

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Save successfully");
        }
        //image ko database ma save krna phla treeka h ye
        private byte[] SaveImageInDatabase()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void UpdateRecordMethod()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("up", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@n", textBox1.Text);
            cmd.Parameters.AddWithValue("@fn", textBox2.Text);
            cmd.Parameters.AddWithValue("@image", SaveImageInDatabase());

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Update successfully");
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
                pictureBox1.Image = getImageFromDataBaseSy((byte[])dr["image"]);
            }
        }

        private Image getImageFromDataBaseSy(byte[] veriable)
        {
            MemoryStream ms = new MemoryStream(veriable);
            return Image.FromStream(ms);
        }

        //jb b dataTable type ka method bnyga tb method k ander dataTable type ka obj lazmi bnyga jo k last ma return b hoga
        private DataTable GetDataByIDMethod(int idPMv)
        {
            //sbsy phly datatale ka obj bnana ha jo k last m return b hoga
            DataTable dt = new DataTable();

            string connectionstring = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("IdAccess", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //ak parameter lia ta usko yha pas kia ha
            cmd.Parameters.AddWithValue("@id", idPMv);


            //sirf ak parameter pas kia h islye sqldatareader lia ha
            //conn.Open();
            //SqlDataReader reader = cmd.ExecuteReader();
            //dt.Load(reader);
            //conn.Close();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);



            return dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose Image";
            ofd.Filter = "Choose Image(*.jpg;*.png|*.JPG;*.PNG)";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }
    }
}
