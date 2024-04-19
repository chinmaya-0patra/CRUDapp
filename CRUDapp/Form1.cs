using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Net.Http.Headers;

namespace CRUDapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=CHINMAYA-PATRA\\SQLEXPRESS;Initial Catalog=CRUDdatabase;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");


        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string insertQ = "INSERT INTO itemData VALUES(@id, @itemname, @design, @color, getdate())";
            SqlCommand cmd = new SqlCommand(insertQ, con);

            cmd.Parameters.AddWithValue("@id", txtID.Text);
            cmd.Parameters.AddWithValue("@itemname", txtItemname.Text);
            cmd.Parameters.AddWithValue("@design", txtDesign.Text);
            cmd.Parameters.AddWithValue("@color", comboBox1.Text);

            int rowsaffected = cmd.ExecuteNonQuery();
            if(rowsaffected > 0)
            {
                MessageBox.Show("Successfully Inserted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Registration Failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            con.Close();
            bindData();
        }

        private void bindData()
        {
            SqlCommand cmdSelect = new SqlCommand("SELECT * FROM itemData", con);
            SqlDataAdapter da = new SqlDataAdapter(cmdSelect);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdUpdate = new SqlCommand("UPDATE itemData SET itemname= '"+txtItemname.Text+"',design = '"+txtDesign.Text+"', color = '"+comboBox1.Text+"', insertdate = getdate() WHERE id = '"+int.Parse(txtID.Text)+"' ", con);
            cmdUpdate.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            bindData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(txtID.Text != "")
            {
                DialogResult dr = MessageBox.Show("Are you Sure? you want to Delete!", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dr == DialogResult.Yes)
                {
                    con.Open();
                    SqlCommand cmdDel = new SqlCommand("DELETE itemData where id ='" + int.Parse(txtID.Text) + "' ", con);
                    cmdDel.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindData();
                }
                
            }
            else
            {
                MessageBox.Show("Id can not be empty! Enter ID", "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
