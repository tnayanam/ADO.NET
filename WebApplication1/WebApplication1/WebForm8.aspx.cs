using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            if (Cache["Data"] == null)
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select * from tblProductInventory", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds); // now this FILL is very useful as it manages opening of the connection and then executing the command to get the data and the loading it into the dataset and then closes the connection.
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    Cache["Data"] = ds;
                    lblMessage.Text = "Fresh Data Loaded";
                }
            }
            else
            {
                GridView1.DataSource = Cache["Data"];
                GridView1.DataBind();
                lblMessage.Text = "Data Loaded from cache";
            }

        }

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            if (Cache["Data"] != null)
            {
                Cache.Remove("Data");
                lblMessage.Text = "Cache cleared";
            }
        }
    }
}