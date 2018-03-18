using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void GetDataFromDB()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter("Select * from tblStudents", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Students");

            ds.Tables["Students"].PrimaryKey = new DataColumn[] { ds.Tables["Students"].Columns["ID"] };
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
            gdStudents.DataSource = ds;
            gdStudents.DataBind();

        }

        protected void btnGetDataFromDB_Click(object sender, EventArgs e)
        {
            GetDataFromDB();
            lblMessage.Text = "Data Loaded from DB";
        }

        private void GetDataFromCache()
        {
            if (Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                gdStudents.DataSource = ds;
                gdStudents.DataBind();
            }
        }

        protected void gdStudents_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gdStudents.EditIndex = e.NewEditIndex;
            GetDataFromCache();
        }

        protected void gdStudents_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            if (Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["ID"]);
                dr["Name"] = e.NewValues["Name"];
                dr["Gender"] = e.NewValues["Gender"];
                dr["TotalMarks"] = e.NewValues["TotalMarks"];
                Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                gdStudents.EditIndex = -1;
                GetDataFromCache();
            }
        }

        protected void gdStudents_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gdStudents.EditIndex = -1;
            GetDataFromCache();
        }

        protected void gdStudents_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {

            if (Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                DataRow dr = ds.Tables["Students"].Rows.Find(e.Keys["ID"]);
                dr.Delete();
                Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                GetDataFromCache();
            }
        }

        protected void btlUpdateDB_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter("Select * from tblStudents", con);
            DataSet ds = (DataSet)Cache["DATASET"];
            string strUpdateCommand = "Update tblStudents Set Name = @Name, Gender = @Gender, TotalMarks = @TotalMarks Where ID = @ID";
            SqlCommand updateCmd = new SqlCommand(strUpdateCommand, con);
            updateCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
            updateCmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 20, "Gender");
            updateCmd.Parameters.Add("@TotalMarks", SqlDbType.Int, 0, "TotalMarks");
            updateCmd.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            da.UpdateCommand = updateCmd;

            string strDeleteCommand = "Delete from tblStudents Where ID = @ID";
            SqlCommand deleteCmd = new SqlCommand(strDeleteCommand, con);
            deleteCmd.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            da.DeleteCommand = deleteCmd;
            // here one important point to be noted is we have not explicitly told which are the rows in the table which we have deleted
            // also we have not mentioned which are the rows which we have updated, or inserted.
            // all this information is stored in the DataTable. Once the below command is run then based on the RowState of the datatable
            // correspondiong attached update insert and deleter command is run.
            da.Update(ds, "Students");
            lblMessage.Text = "Database Updated!";
        }
    }
}

/*
 * ADO.Net offers two datatables access models
 * connection oriented
 * disconnected
 * 
 */
