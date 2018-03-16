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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", txtEmployeeName.Text); // these are parameters that we are adding which are requied by Sproc
                cmd.Parameters.AddWithValue("@Gender", ddlGender.Text);
                cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);
                SqlParameter outputParameter = new SqlParameter(); // there is one more parameter that out stored procedure is expecting but we need to tell Srpoc that it is an output paramter
                outputParameter.ParameterName = "@EmployeeId"; // so we are building the parameter from scratch.
                outputParameter.SqlDbType = System.Data.SqlDbType.Int; //. we need to tel the type
                outputParameter.Direction = ParameterDirection.Output; // also we need to tel that it is going to be an output parameter
                cmd.Parameters.Add(outputParameter); // now add the parameter.
                con.Open();
                cmd.ExecuteNonQuery(); // since we are inserting better to run executenonquery
                lblMessage.Text = "EmployeeId is: " + outputParameter.Value.ToString(); // get the output parameter value
            }
        }
    }
}

//Sproc
//Create proc spAddEmployee
//@Name nvarchar(50),
//@Gender nvarchar(50),
//@Salary int,
//@EmployeeId int Out
//AS
//BEGIN
//Insert into tblEmployees values(@Name, @Gender, @Salary)
//Select @EmployeeId  = SCOPE_IDENTITY()
//END

//ExecuteScalar() only returns the value from the first column of the first row of your query. ExecuteReader() returns an object that can
//iterate over the entire result set. ExecuteNonQuery() does not return data at all: only the number of rows affected by an insert, update, or delete.