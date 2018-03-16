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
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("spTwoTable", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                da.Fill(ds); // now this FILL is very useful as it manages opening of the connection and then executing the command to get the data and the loading it into the dataset and then closes the connection.
                GridView1.DataSource = ds.Tables["0"];
                GridView1.DataBind();
                GridView2.DataSource = ds.Tables["1"];
                GridView2.DataBind();
            }
        }
    }
}