using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Drawing;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using System.Text;
using A2ZWEBWMS.WebSessionStore;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.CustomerServices;


namespace A2ZWEBWMS.Pages
{
    public partial class A2ZHKERPMODULE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                TextBox username = new TextBox();

                DivUserMaint.Visible = false;
                DivHolidayMaint.Visible = false;
                DivDatabaseMaint.Visible = false;

                lblCompanyName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.COMPANY_NAME));


                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));


                lblIDName.Text = "Login as : " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());



            }
        }

        


        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("a2zLogin.aspx", false);
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx", false);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERP.aspx", false);
        }

        protected void btnParameter_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParameterMaintenance.aspx", false);

        }
        protected void btnUserMaint_Click(object sender, EventArgs e)
        {
            DivUserMaint.Visible = true;

            btnParameter.Visible = false;
            btnUserMaint.Visible = false;
            btnHolidayMaint.Visible = false;
            btnDatabase.Visible = false;
            btnProcessEnd.Visible = false;
            btnBack.Visible = false;
            btnLogOut.Visible = false;


            DivUserMaint.Attributes.CssStyle.Add("opacity", "0.7");
            DivUserMaint.Attributes.CssStyle.Add("opacity", "0.7");



            DivUserMaint.Style.Add("Top", "150px");
            DivUserMaint.Style.Add("left", "36px");
            DivUserMaint.Style.Add("position", "fixed");
            DivUserMaint.Attributes.CssStyle.Add("opacity", "200");
            DivUserMaint.Attributes.CssStyle.Add("z-index", "200");




        }

        protected void btnHolidayMaint_Click(object sender, EventArgs e)
        {
            DivHolidayMaint.Visible = true;

            btnParameter.Visible = false;
            btnUserMaint.Visible = false;
            btnHolidayMaint.Visible = false;
            btnDatabase.Visible = false;
            btnProcessEnd.Visible = false;
            btnBack.Visible = false;
            btnLogOut.Visible = false;


            DivHolidayMaint.Attributes.CssStyle.Add("opacity", "0.7");
            DivHolidayMaint.Attributes.CssStyle.Add("opacity", "0.7");



            DivHolidayMaint.Style.Add("Top", "150px");
            DivHolidayMaint.Style.Add("left", "36px");
            DivHolidayMaint.Style.Add("position", "fixed");
            DivHolidayMaint.Attributes.CssStyle.Add("opacity", "200");
            DivHolidayMaint.Attributes.CssStyle.Add("z-index", "200");
        }

        

        protected void btnDatabase_Click(object sender, EventArgs e)
        {
            DivDatabaseMaint.Visible = true;

            btnParameter.Visible = false;
            btnUserMaint.Visible = false;
            btnHolidayMaint.Visible = false;
            btnDatabase.Visible = false;
            btnProcessEnd.Visible = false;
            btnBack.Visible = false;
            btnLogOut.Visible = false;


            DivDatabaseMaint.Attributes.CssStyle.Add("opacity", "0.7");
            DivDatabaseMaint.Attributes.CssStyle.Add("opacity", "0.7");



            DivDatabaseMaint.Style.Add("Top", "150px");
            DivDatabaseMaint.Style.Add("left", "36px");
            DivDatabaseMaint.Style.Add("position", "fixed");
            DivDatabaseMaint.Attributes.CssStyle.Add("opacity", "200");
            DivDatabaseMaint.Attributes.CssStyle.Add("z-index", "200");
        }

        protected void btnProcessEnd_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZPeriodEnd.aspx", false);
            
        }

        protected void btnUserBack_Click(object sender, EventArgs e)
        {

            DivUserMaint.Visible = false;
           

            btnParameter.Visible = true;
            btnUserMaint.Visible = true;
            btnHolidayMaint.Visible = true;
            btnDatabase.Visible = true;
            btnProcessEnd.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }

        protected void btnUserAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPUserIdMaint.aspx", false);
        }

        protected void btnUserEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPUserIdUpdate.aspx", false);
        }

        
        protected void btnHolType_Click(object sender, EventArgs e)
        {
            Response.Redirect("HolidayTypeMaintenance.aspx", false);

        }

        protected void btnHolWeekly_Click(object sender, EventArgs e)
        {
            Response.Redirect("WeekHolidayMaintenance.aspx", false);
            
        }

        protected void btnHolNational_Click(object sender, EventArgs e)
        {
            Response.Redirect("NationalHolidayMaintenance.aspx", false);
            
        }

        protected void btnHolBack_Click(object sender, EventArgs e)
        {

            DivHolidayMaint.Visible = false;


            btnParameter.Visible = true;
            btnUserMaint.Visible = true;
            btnHolidayMaint.Visible = true;
            btnDatabase.Visible = true;
            btnProcessEnd.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZBackupProcess.aspx", false);
            
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZRestoreProcess.aspx", false);
        }

        protected void btnDatabaseBack_Click(object sender, EventArgs e)
        {
            DivDatabaseMaint.Visible = false;


            btnParameter.Visible = true;
            btnUserMaint.Visible = true;
            btnHolidayMaint.Visible = true;
            btnDatabase.Visible = true;
            btnProcessEnd.Visible = true;
            btnBack.Visible = true;
            btnLogOut.Visible = true;
        }
    }
}