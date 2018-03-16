using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            // text enterd by user causing sqlinjection in the textbox:      ip'; Truncate table tblEmp;Select * from tblProductInventory where ProductName like 'w
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertInto", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productName", TextBox1.Text);
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
        }
    }
}

//Sproc
//Create proc sp_InsertInto 
//@productName nvarchar(100)
//As
//Begin
//Select * from tblInventory where  ProductName like @productName + '%'
//End

//Now there will  be no sql injection
