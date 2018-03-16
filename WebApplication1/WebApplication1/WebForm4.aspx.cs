using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblProdInventory; Select * from tblProdCategory", con); // here we have two result set.
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader()) // creates the instance of datareader and loads it with data to be shown in grid.
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                    while (rdr.NextResult())
                    {
                        GridView2.DataSource = rdr;
                        GridView2.DataBind();
                    }
                }
            }
        }
    }
}

/*
 * Important: if we want to loop through the rows of single result set then we need to use the rdr.read(). It will loop through single result set.
 * But incase if we have multiple result set within the rdr then we  need to loop through the result set itself. and then only we can access the rows int hem so for that we need to add
 * rdr.NextResult()
 * To Loop through rows of resutl set: READ
 * To loop through results sets itself use: NextResult.
 * 
 * 
 * 
 * SQLDataReader requires an active  connection for data to be read. Where as SQLdataAdapter and Dataset provides us with disconnected data access.
 */