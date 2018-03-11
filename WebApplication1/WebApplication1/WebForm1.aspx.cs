﻿using System;
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
            using (SqlConnection con = new SqlConnection(@"data source=.\MSSQLSERVER01; database=Sample; integrated security=SSPI"))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblProduct", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                GridView1.DataSource = rdr;
                GridView1.DataBind();
                con.Close();
            }
        }
    }
}