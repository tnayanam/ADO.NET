using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetStudent_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            //SqlDataAdapter da = new SqlDataAdapter("Select * from tblStudents where ID = @Id", con);
            //da.SelectCommand.Parameters.AddWithValue("@Id", txtStudentID.Text);
            string selectQuery = "Select * from tblStudents where ID = " +
           txtStudentID.Text;
            SqlDataAdapter da = new SqlDataAdapter(selectQuery, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Students"); // now this FILL is very useful as it manages opening of the connection and then executing the command to get the data and the loading it into the dataset and then closes the connection.
            ViewState["SQL_Query"] = selectQuery;
            ViewState["Data"] = ds;
            if (ds.Tables["Students"].Rows.Count > 0)
            {
                DataRow dataRow = ds.Tables["Students"].Rows[0];
                txtStudentName.Text = dataRow["Name"].ToString();
                txtTotalMarks.Text = dataRow["TotalMarks"].ToString();
                ddlGender.SelectedValue = dataRow["Gender"].ToString();
                lblStatus.Text = "";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString =
   ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand =
                new SqlCommand((string)ViewState["SQL_Query"], con);
            SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);

            DataSet ds = (DataSet)ViewState["Data"];
            DataRow dr = ds.Tables["Students"].Rows[0];
            dr["Name"] = txtStudentName.Text;
            dr["Gender"] = ddlGender.SelectedValue;
            dr["TotalMarks"] = txtTotalMarks.Text;
            dr["Id"] = txtStudentID.Text;
            builder.GetUpdateCommand();
            int rowsUpdated = dataAdapter.Update(ds, "Students");
            if (rowsUpdated == 0)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "No rows updated";
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.Green;
                lblStatus.Text = rowsUpdated.ToString() + " row(s) updated";
            }
        }
    }
}
