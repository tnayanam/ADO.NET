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
            da.Update(ds, "Students"); // (+++)
            lblMessage.Text = "Database Updated!";

            // all row state below is unchanged because after "Update" eerything is set to unchanged.
            foreach (DataRow dr in ds.Tables["Students"].Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    Response.Write(dr["Id", DataRowVersion.Original] + " - " + dr.RowState.ToString() + "</br>"); // to get row state of
                }
                else
                {
                    Response.Write(dr["Id"].ToString() + " - " + dr.RowState.ToString() + "</br>");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASET"];

            if (ds.HasChanges())
            {
                ds.RejectChanges();
                lblMessage.Text = "Changes Rejected";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                Cache["DATASET"] = ds;
                GetDataFromCache();
            }
            else
            {
                lblMessage.Text = "No Changes Detected";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}

/*
 * ADO.Net offers two datatables access models
 * connection oriented
 * disconnected
 * 
 */


//Create Table tblStudents
//(
//ID int identity primary key,
//Name nvarchar(50),
// Gender nvarchar(20),
// TotalMarks int
//)

//Insert into tblStudents values('Mark Hastings','Male',900)
//Insert into tblStudents values('Pam Nicholas','Female',760)
//Insert into tblStudents values('John Stenson','Male',980)
//Insert into tblStudents values('Ram Gerald','Male',990)
//Insert into tblStudents values('Ron Simpson','Male',440)
//Insert into tblStudents values('Able Wicht','Male',320)
//Insert into tblStudents values('Steve Thompson','Male',983)
//Insert into tblStudents values('James Bynes','Male',720)
//Insert into tblStudents values('Mary Ward','Female',870)
//Insert into tblStudents values('Nick Niron','Male',680)


//    1 - Deleted
//2 - Unchanged
//5 - Unchanged
//6 - Unchanged
//7 - Unchanged
//8 - Unchanged
//9 - Unchanged
//10 - Unchanged
//101 - Added

/*
 * Different Ros States:
 * Unchanged: if no change has been made to the datatable
 * Added: When a nbew row is added to the datatable.
 * Modified: Some element of the row is modified.
 * Deleted: The row has been deleted and ACCEPT CHANGES has not been called.
 * Detached: WHen row has been created but not added to the datatable.
 * 
 * 
 * Different Row Versions:
 * Current: CurrentState of Datarow. This does not exist for a RowState of Deleted which is obvious because it is deleted now. It will only have original rowstate
 * Original: The original valus of row previous to modification. It wont be available for newly added rows.
 * Proposed: Proposed values for the row. This exists during an edit version operation on the row.
 * Default: Default row version for an added, modified or unchanged is "Current". For Deleted: Original, For detached it is proposed.
 * 
 * 
 * 
 * Datarow.HasVersion(DataRowVersion.Original);
 * Hasversion can be used to check the rowversion
 * 
 * When RejectChanges is invoked then RowState Property of each row changes. Added rows are removed. Deleted and modified state are 
 * changed to "unchanged"
 * Accept Changes: WHen it is invoked the rowstate od added and modified is set to Unchaned and Deleted one is removed.
 * So this is the default behavour when dataadpatersm"update" (+++)method is called and DB is updated.
 * Also it is possible to call AcceptChanges explicitly. But if we do that then deleted rows will be remove from the dataset and also the 
 * added and modified propert will be set to "Unchanged" and at this point if we try to update the DB I mean if we cann da.update(+++) on database then
 *  nothing will HAPPEN because based on row state only queries are executed.
 *  Both AccepeChanges  and  RejectChanges  are  applied  to  dataset  and  datatable and datarow  level.
  
    


 */


