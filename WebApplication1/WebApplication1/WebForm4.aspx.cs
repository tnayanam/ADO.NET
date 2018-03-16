using System;
using System.Collections.Generic;
using System.Configuration;
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
                SqlCommand cmd = new SqlCommand("Select * from tblProdInventory", con);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader()) // creates the instance of datareader and loads it with data to be shown in grid.
                {
                    GridView1.DataSource = rdr; // binding the data loaded in datarader to the gridview 
                    GridView1.DataBind();
                }
            }
        }
    }
}