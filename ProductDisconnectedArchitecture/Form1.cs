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
using System.Configuration;

namespace ProductDisconnectedArchitecture
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public DataSet GetAllProduct()
        {
            da = new SqlDataAdapter("select * from product", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Product");// product is a table name given to DataTable
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds=GetAllProduct();
                DataRow row = ds.Tables["Product"].NewRow();
                row["pname"]=txtProductName.Text;
                row["price"]=txtPrice.Text;
                row["company_name"] = txtCompanyName.Text;
                ds.Tables["Product"].Rows.Add(row);
                int result = da.Update(ds.Tables["Product"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                DataRow row = ds.Tables["Product"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row["pname"] = txtProductName.Text;
                    row["price"] = txtPrice.Text;
                    row["company_name"] = txtCompanyName.Text;
                    int result = da.Update(ds.Tables["Product"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record Update");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                DataRow row = ds.Tables["Product"].Rows.Find(txtId.Text);
                if(row!=null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["Product"]);
                    if(result == 1)
                    {
                        MessageBox.Show("Record Deleted");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                DataRow row = ds.Tables["Product"].Rows.Find(txtId.Text);
                if(row!=null)
                {
                        txtProductName.Text = row["pname"].ToString();
                        txtPrice.Text = row["price"].ToString();
                        txtCompanyName.Text = row["company_name"].ToString();
                }
               else
               {

                        MessageBox.Show("record not found");
               }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds = GetAllProduct();
            dataGridView1.DataSource = ds.Tables["Product"];
        }
    }
}
