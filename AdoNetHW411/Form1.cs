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

namespace AdoNetHW411
{
    public partial class Form1 : Form
    {
        SqlConnection cnn = null;
        public Form1()
        {
            InitializeComponent();
            
            try
            {
                cnn = new SqlConnection("Data Source =.; Initial Catalog = Northwind; Integrated Security = True");
                cnn.Open();
                SqlCommand com = new SqlCommand("Select CategoryName from Categories", cnn);
                SqlDataReader dataReader = com.ExecuteReader();

                while (dataReader.Read())
                {
                    string s = dataReader["CategoryName"].ToString();
                    //listBox1.Items.Add(dataReader["ProductName"]+" "+ dataReader["UnitPrice"]);
                    comboBox1.Items.Add(s);
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sli = comboBox1.SelectedItem.ToString();
            listBox1.Items.Clear();
            try
            {
                cnn = new SqlConnection("Data Source =.; Initial Catalog = Northwind; Integrated Security = True");
                cnn.Open();
                SqlCommand com = new SqlCommand(("Select * from Products  p join Categories c on p.CategoryID = c.CategoryID where CategoryName=" + "'"+sli+"'"), cnn);
                SqlDataReader dataReader = com.ExecuteReader();

                while (dataReader.Read())
                {
                    string s = $"{dataReader["ProductID"].ToString()}    {dataReader["ProductName"].ToString()}   {dataReader["CategoryName"].ToString()}";
                    //listBox1.Items.Add(dataReader["ProductName"]+" "+ dataReader["UnitPrice"]);
                    listBox1.Items.Add(s);
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }

        }
    }
}
