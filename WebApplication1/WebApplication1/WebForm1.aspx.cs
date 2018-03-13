using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // one type of opening connection and closing it
            //SqlConnection con = new SqlConnection(@"data source=.\MSSQLSERVER01; database=Sample; integrated security=SSPI");
            //try
            //{
            //    SqlCommand cmd = new SqlCommand("Select * from tblProduct", con);
            //    con.Open();
            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    GridView1.DataSource = rdr;
            //    GridView1.DataBind();

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    con.Close();
            //}

            // Other type
            var CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert into tblProduct values ('Mixture', 2340)", con);
                con.Open();
                int TotalRowsAffected = cmd.ExecuteNonQuery(); // we get the number of affecteed rows in output
                Response.Write(" Total Rows" + TotalRowsAffected);
                cmd.CommandText = "Update tblProduct set Name = 'Biscuit' where Name = 'Patanjali'";
                TotalRowsAffected = cmd.ExecuteNonQuery(); // we get the number of affecteed rows in output
                Response.Write(" Total Rows" + TotalRowsAffected);
                cmd.CommandText = "Delete from  tblProduct where Name = 'Patanjali'";
                TotalRowsAffected = cmd.ExecuteNonQuery(); // we get the number of affecteed rows in output
                Response.Write(" Total Rows" + TotalRowsAffected);
            }
        }
    }
}

/*     
 SQLCommand class is used to prepare SQL Command or StoredProcedure
 that we want to execute on a SQL Server Database.

    The most commonly used methods of the SQL COmmand class

    1. ExecuteReader: Use when the T-SQL statement returns more than a single value. For example if the query returns rows of data.

    2. ExecuteNonQuery - Use when you want to perform an Insert, Update Or Deete Operation
    3. ExecuteScalar -  Use when the query returns a single scalar value. for examle, queries that return the totdal number of rowas in a table.

     */
