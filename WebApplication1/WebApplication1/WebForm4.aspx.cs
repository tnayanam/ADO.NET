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
                SqlCommand cmd = new SqlCommand("Select * from tblProdInventory", con);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader()) // creates the instance of datareader and loads it with data to be shown in grid.
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Id");
                    dt.Columns.Add("ProductName");
                    dt.Columns.Add("UnitPrice");
                    dt.Columns.Add("DiscountedPrice");
                    while(rdr.Read())
                    {
                        DataRow dr = dt.NewRow();
                        int origPrice = Convert.ToInt32(rdr["UnitPrice"]);
                        double discountedPrice = origPrice * 0.9;
                        dr["Id"] = rdr["ProductId"];
                        dr["ProductName"] =rdr["ProductName"];
                        dr["UnitPrice"] = rdr["UnitPrice"];
                        dr["DiscountedPrice"] = discountedPrice;
                        dt.Rows.Add(dr);
                    }
                    GridView1.DataSource = dt; // binding the data loaded in datarader to the gridview 
                    GridView1.DataBind();
                }
            }
        }
    }
}