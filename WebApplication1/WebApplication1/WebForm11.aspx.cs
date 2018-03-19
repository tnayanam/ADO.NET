using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand("Select * from Accounts", con);
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(str))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("Update Accounts set Balance = Balance - 10 Where AccountNumber = 'A1' ", con, tran);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Update Accounts set Balance = Balancee + 10 Where AccountNumber = 'A2' ", con, tran);
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    lblMessage.Text = "Both table updated";
                }
                catch (Exception)
                {
                    tran.Rollback();
                    lblMessage.Text = "Transaction RolledBack";
                }
                finally
                {


                }

            }
        }
    }
}