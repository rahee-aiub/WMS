using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBWMS.Pages
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                if (lblID.Text == string.Empty || lblID.Text == "0")
                {
                    Response.Redirect("A2ZLogin.aspx");
                }

                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());


             

                string PFlag = (string)Session["ProgFlag"];
                CtrlProgFlag.Text = PFlag;

                if (CtrlProgFlag.Text != "1")
                {

                    lblParty.Visible = false;
                    chkAllParty.Visible = false;
                    ddlParty.Visible = false;

                    
                    ddlReportsDetails1.Visible = true;
                    ddlReportsDetails2.Visible = false;
                    ddlReportsDetails3.Visible = false;
                   

                    InitializeValues();
                }
                else
                {
                    string RfDate = (string)Session["fDate"];
                    string RtDate = (string)Session["tDate"];
                    string RReportSelect = (string)Session["ReportSelect"];

                    string RSchkAllParty = (string)Session["SchkAllParty"];
                    string RSddlParty = (string)Session["SddlParty"];

                    txtFromDate.Text = RfDate;
                    txtToDate.Text = RtDate;
                    //ddlReports.SelectedValue = RReportSelect;


                    lblParty.Visible = false;
                    chkAllParty.Visible = false;
                    ddlParty.Visible = false;

                    if (RReportSelect == "1")
                    {
                        rbtReports01.Checked = true;
                        ddlReportsDetails1.Visible = true;
                        ddlReportsDetails2.Visible = false;
                        ddlReportsDetails3.Visible = false;
                    }
                    else if (RReportSelect == "2")
                    {
                        rbtReports02.Checked = true;
                        ddlReportsDetails2.Visible = true;
                        ddlReportsDetails1.Visible = false;
                        ddlReportsDetails3.Visible = false;
                    }
                    else if (RReportSelect == "3")
                    {
                        rbtReports03.Checked = true;
                        ddlReportsDetails3.Visible = true;
                        ddlReportsDetails2.Visible = false;
                        ddlReportsDetails1.Visible = false;
                    }




                    //if (ddlReports.SelectedValue == "6" ||
                    //    ddlReports.SelectedValue == "7" ||
                    //    ddlReports.SelectedValue == "8" ||
                    //    ddlReports.SelectedValue == "12" ||
                    //    ddlReports.SelectedValue == "13")
                    //{
                    //    lblfromDate.Visible = false;
                    //    txtFromDate.Visible = false;

                    //    chkAllParty.Checked = false;
                    //    lblParty.Visible = false;
                    //    chkAllParty.Visible = false;
                    //    ddlParty.Visible = false;
                    //}


                    //if (ddlReports.SelectedValue == "3" || ddlReports.SelectedValue == "4" || ddlReports.SelectedValue == "5")
                    //{
                    //    lblfromDate.Visible = true;
                    //    txtFromDate.Visible = true;

                    //    chkAllParty.Checked = false;
                    //    lblParty.Visible = true;
                    //    chkAllParty.Visible = false;
                    //    ddlParty.Visible = true;
                    //}

                    //if (ddlReports.SelectedValue == "99")
                    //{
                    //    lblfromDate.Visible = true;
                    //    txtFromDate.Visible = true;

                    //    chkAllParty.Checked = false;
                    //    lblParty.Visible = false;
                    //    chkAllParty.Visible = false;
                    //    ddlParty.Visible = false;
                    //}


                    if (RSchkAllParty == "1")
                    {
                        chkAllParty.Checked = true;
                        ddlParty.SelectedIndex = 0;
                        ddlParty.Enabled = false;
                    }
                    else
                    {
                        chkAllParty.Checked = false;
                        ddlParty.SelectedValue = RSddlParty;
                        ddlParty.Enabled = true;
                    }



                    //if (ddlReports.SelectedValue == "3")
                    //{
                    //    CodeDropdown();
                    //}
                    //else if (ddlReports.SelectedValue == "4")
                    //{
                    //    TransitPartyDropdown();
                    //}
                    //else if (ddlReports.SelectedValue == "5")
                    //{
                    //    TransitPartyDropdown();
                    //}

                }


            }

        }

        private void CodeDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 12 GROUP BY PartyCode,PartyName";
            ddlParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlParty, "A2ZACWMS");
        }

        private void PurchasePartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 14 GROUP BY PartyCode,PartyName";
            ddlParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlParty, "A2ZACWMS");
        }

        private void TransitPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 13 GROUP BY PartyCode,PartyName";
            ddlParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlParty, "A2ZACWMS");
        }

        private void SalesPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 20 GROUP BY PartyCode,PartyName";
            ddlParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlParty, "A2ZACWMS");
        }
        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 11 and GroupCode != 12 and GroupCode !=16 and GroupCode !=51 and GroupCode !=22 GROUP BY PartyCode,PartyName";
            ddlParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlParty, "A2ZACWMS");
        }

        private void AllPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode != 12 GROUP BY PartyCode,PartyName";
            ddlParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlParty, "A2ZACWMS");
        }

        private void InitializeValues()
        {
            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt = Converter.GetDateTime(dto.ProcessDate);
            string date = dt.ToString("dd/MM/yyyy");
            lblProcessDate.Text = date;

            string RfDate = (string)Session["fDate"];
            string RtDate = (string)Session["tDate"];
            string RReportSelect = (string)Session["ReportSelect"];
            if (RfDate == null || RfDate == string.Empty)
            {
                txtFromDate.Text = Converter.GetString(dto.ProcessDate.ToString("dd/MM/yyyy"));
            }
            else
            {
                txtFromDate.Text = RfDate;
            }
            if (RtDate == null || RtDate == string.Empty)
            {
                txtToDate.Text = Converter.GetString(dto.ProcessDate.ToString("dd/MM/yyyy"));
            }
            else
            {
                txtToDate.Text = RtDate;
            }

            //if (RReportSelect != null && RReportSelect != string.Empty)
            //{
            //    ddlReports.SelectedValue = RReportSelect;
            //}


        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZWSERPMODULE.aspx");
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (CtrlReportSelect.Text == "3" || CtrlReportSelect.Text == "4" || CtrlReportSelect.Text == "5")
                {
                    if (ddlParty.SelectedIndex == 0)
                    {
                        return;
                    }
                }




                SessionStoreValues();

                if (CtrlReportSelect.Text == "15")
                {
                    var prm = new object[1];

                    prm[0] = Converter.GetDateToYYYYMMDD(txtToDate.Text);
                  
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_rptWMSTotalStock", prm, "A2ZACWMS"));
                    
                }


                var p = A2ZERPSYSPRMDTO.GetParameterValue();
                string comName = p.PrmUnitName;
                string comAddress1 = p.PrmUnitAdd1;

                if (CtrlReportSelect.Text == "12")
                {
                    txtFromDate.Text = txtToDate.Text;
                }


                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtFromDate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txtToDate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACWMS");
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);


                if (chkAllParty.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, 0);
                }
                else
                {
                    Int64 code = Converter.GetLong(ddlParty.SelectedValue);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.ACCNO, code);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, code);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlParty.SelectedItem.Text);
                }

                if (CtrlReportSelect.Text == "1")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSPurchaseReport_Main");
                }
                else if (CtrlReportSelect.Text == "2")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSPartyPurchaseDetailsReport");
                }

                else if (CtrlReportSelect.Text == "3" || CtrlReportSelect.Text == "4")
                {
                    if (CtrlReportSelect.Text == "3")
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 1);
                    }
                    else
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 2);
                    }

                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 2);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSStatementCodewise");
                }
                else if (CtrlReportSelect.Text == "5")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 2);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSStatementPartywise");
                }
                else if (CtrlReportSelect.Text == "6")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSDxbStockDetailsReport");
                }
                else if (CtrlReportSelect.Text == "7")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSTransitStockDetailsReport");
                }
                else if (CtrlReportSelect.Text == "8")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSDhkStockDetailsReport");
                }
                else if (CtrlReportSelect.Text == "9")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSTransitIssueDetailsReport");
                }
                else if (CtrlReportSelect.Text == "10")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSTransitReceivedDetailsReport");
                }
                else if (CtrlReportSelect.Text == "11")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSPartySalesDetailsReport");
                }
                else if (CtrlReportSelect.Text == "12")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSDxbStockBalanceReport");
                }
                else if (CtrlReportSelect.Text == "13")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSStockBalanceDetail");
                }
                else if (CtrlReportSelect.Text == "14")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSStockBalanceDetailPartyWise");
                }
                else if (CtrlReportSelect.Text == "15")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSTotalStockDetailReport");
                }
                else if (CtrlReportSelect.Text == "16")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSPartyWiseTransitDetailReport");
                }
                else if (CtrlReportSelect.Text == "17")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptWMSPartyWiseTransitSummaryReport");
                }
                else if (CtrlReportSelect.Text == "99")
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMTransactionList");
                }



                Response.Redirect("ReportServer.aspx", false);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void SessionStoreValues()
        {
            Session["ProgFlag"] = "1";

            Session["fDate"] = txtFromDate.Text;
            Session["tDate"] = txtToDate.Text;

            if (rbtReports01.Checked == true)
            {
                Session["ReportSelect"] = "1";
            }
            else if (rbtReports02.Checked == true)
            {
                Session["ReportSelect"] = "2";
            }
            else if (rbtReports03.Checked == true)
            {
                Session["ReportSelect"] = "3";
            }
           

            if (chkAllParty.Checked == true)
            {
                Session["SchkAllParty"] = "1";
            }
            else
            {
                Session["SchkAllParty"] = "0";
            }

            Session["SddlParty"] = ddlParty.SelectedValue;




        }

        private void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;
            Session["fDate"] = string.Empty;
            Session["tDate"] = string.Empty;
            Session["ReportSelect"] = string.Empty;
        }

        protected void chkAllParty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllParty.Checked == true)
            {
                ddlParty.Enabled = false;
                ddlParty.SelectedIndex = 0;
            }
            else
            {
                ddlParty.Enabled = true;
            }
        }


        
        protected void ddlReportsDetails1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (ddlReportsDetails1.SelectedValue == "9" || ddlReportsDetails1.SelectedValue == "10")
            {
                lblfromDate.Visible = true;
                txtFromDate.Visible = true;

                lblParty.Visible = false;
                chkAllParty.Visible = false;
                ddlParty.Visible = false;
            }
            else if (ddlReportsDetails1.SelectedValue == "7")
            {

                lblfromDate.Visible = false;
                txtFromDate.Visible = false;

                lblParty.Visible = false;
                chkAllParty.Visible = false;
                ddlParty.Visible = false;
            }
            else if (ddlReportsDetails1.SelectedValue == "3")
            {
                lblfromDate.Visible = true;
                txtFromDate.Visible = true;

                chkAllParty.Checked = false;
                lblParty.Visible = true;
                chkAllParty.Visible = false;
                ddlParty.Enabled = true;
                ddlParty.Visible = true;
                CodeDropdown();
            }
            else if (ddlReportsDetails1.SelectedValue == "4")
            {
                lblfromDate.Visible = true;
                txtFromDate.Visible = true;

                chkAllParty.Checked = false;
                lblParty.Visible = true;
                chkAllParty.Visible = false;
                ddlParty.Enabled = true;
                ddlParty.Visible = true;
                TransitPartyDropdown();
            }
            else if (ddlReportsDetails1.SelectedValue == "5")
            {
                lblfromDate.Visible = true;
                txtFromDate.Visible = true;

                chkAllParty.Checked = false;
                lblParty.Visible = true;
                chkAllParty.Visible = false;
                ddlParty.Enabled = true;
                ddlParty.Visible = true;
                TransitPartyDropdown();
            }
            else if (ddlReportsDetails1.SelectedValue == "16" || ddlReportsDetails1.SelectedValue == "17")
            {
                lblfromDate.Visible = true;
                txtFromDate.Visible = true;

                chkAllParty.Checked = false;
                lblParty.Visible = true;
                chkAllParty.Visible = false;
                ddlParty.Enabled = true;
                ddlParty.Visible = true;
                SalesPartyDropdown();
            }

            CtrlReportSelect.Text = ddlReportsDetails1.SelectedValue;
        }

        protected void ddlReportsDetails2_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            if (ddlReportsDetails2.SelectedValue == "6" ||
                ddlReportsDetails2.SelectedValue == "8" ||
                ddlReportsDetails2.SelectedValue == "12" ||
                ddlReportsDetails2.SelectedValue == "13" ||
                ddlReportsDetails2.SelectedValue == "14" ||
                ddlReportsDetails2.SelectedValue == "15")
            {

                lblfromDate.Visible = false;
                txtFromDate.Visible = false;

                lblParty.Visible = false;
                chkAllParty.Visible = false;
                ddlParty.Visible = false;
            }

            CtrlReportSelect.Text = ddlReportsDetails2.SelectedValue;
        }

        protected void ddlReportsDetails3_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (ddlReportsDetails3.SelectedValue == "1" || ddlReportsDetails3.SelectedValue == "2" || ddlReportsDetails3.SelectedValue == "11")
            {
                lblfromDate.Visible = true;
                txtFromDate.Visible = true;

                lblParty.Visible = false;
                chkAllParty.Visible = false;
                ddlParty.Visible = false;
            }
            else if (ddlReportsDetails3.SelectedValue == "99")
            {
                lblfromDate.Visible = true;
                txtFromDate.Visible = true;

                chkAllParty.Checked = false;
                lblParty.Visible = false;
                chkAllParty.Visible = false;
                ddlParty.Enabled = false;
                ddlParty.Visible = false;

            }

            CtrlReportSelect.Text = ddlReportsDetails3.SelectedValue;
        }

        
        protected void rbtReports01_CheckedChanged(object sender, EventArgs e)
        {
            ddlReportsDetails1.Visible = true;
           
            ddlReportsDetails2.Visible = false;
            ddlReportsDetails3.Visible = false;
        }

        protected void rbtReports02_CheckedChanged(object sender, EventArgs e)
        {
            ddlReportsDetails2.Visible = true;
           
            ddlReportsDetails1.Visible = false;
            ddlReportsDetails3.Visible = false;
        }

        protected void rbtReports03_CheckedChanged(object sender, EventArgs e)
        {
            ddlReportsDetails3.Visible = true;
           
            ddlReportsDetails1.Visible = false;
            ddlReportsDetails2.Visible = false;
        }

    }
}