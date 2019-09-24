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
    public partial class A2ZERP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                TextBox username = new TextBox();

                DivDetails.Visible = false;
                DivUpdateMSG.Visible = false;

                lblCompanyName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.COMPANY_NAME));


                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));


                lblIDName.Text = "Login as : " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());


                string sqlQuery = @"SELECT * FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO='" + lblID.Text + "'";

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHKWMS");

                if (dt.Rows.Count > 0)
                {
                    lblNoRec.Text = Converter.GetString(dt.Rows.Count);

                    DataView view = new DataView(dt);

                    foreach (DataRowView row in view)
                    {
                        int moduleNo = Converter.GetInteger(row["ModuleNo"].ToString());

                        if (dt.Rows.Count == 1)
                        {
                            string LogOut = (string)Session["LogOutFlag"];
                            if (LogOut == "1")
                            {
                                Session["LogOutFlag"] = "";
                                Response.Redirect("A2ZLogin.aspx", false);
                                return;
                            }
                            else
                            {
                                //hdnModule.Value = Converter.GetString(moduleNo);
                                SODProcessFunction();
                                if (lblMsgFlag.Text == "1")
                                {
                                    ShowMessage();
                                }
                                else if (lblMsgFlag.Text == "2")
                                {
                                    UpdatedMSG();
                                    return;
                                }

                                if (moduleNo == 1)
                                {
                                    //hdnModule.Value = "1";
                                    SODProcessFunction();
                                    if (lblMsgFlag.Text == "1")
                                    {
                                        ShowMessage();
                                    }
                                    else if (lblMsgFlag.Text == "2")
                                    {
                                        UpdatedMSG();
                                        return;
                                    }
                                    else
                                    {
                                        Response.Redirect("A2ZWSERPMODULE.aspx", false);
                                        return;
                                    }
                                }
                                else
                                    if (moduleNo == 2)
                                    {
                                        Response.Redirect("A2ZHKERPMODULE.aspx", false);
                                        return;
                                    }

                            }
                        }

                    }



                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This User Have No Permission');", true);
                    return;
                }


            }
        }



        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("a2zLogin.aspx", false);
        }



        protected void btnWSmodule_Click(object sender, EventArgs e)
        {

            //hdnModule.Value = "1";
            SODProcessFunction();
            if (lblMsgFlag.Text == "1")
            {
                ShowMessage();
            }
            else if (lblMsgFlag.Text == "2")
            {
                UpdatedMSG();
                return;
            }
            else
            {
                Response.Redirect("A2ZWSERPMODULE.aspx", false);
            }



            
        }

        protected void btnHKmodule_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZHKERPMODULE.aspx", false);
        }


        protected void SODProcessFunction()
        {
            lblMsgFlag.Text = "0";

            A2ZERPSYSPRMDTO prmobj = A2ZERPSYSPRMDTO.GetParameterValue();

            if (prmobj.PrmEODStat == 0)
            {
                return;
            }
            else
            {
                A2ZSYSIDSDTO sysobj = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(lblID.Text), "A2ZHKWMS");

                if (sysobj.SODflag == false)
                {
                    lblMsgFlag.Text = "2";
                    //String csname1 = "PopupScript";
                    //Type cstype = GetType();
                    //ClientScriptManager cs = Page.ClientScript;

                    //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    //{
                    //    String cstext1 = "alert('Access Denied START OF DAY NOT DONE');";
                    //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    //}
                    return;
                }
                else
                {
                    lblMsgFlag.Text = "1";
                }
            }
        }

        protected void ShowMessage()
        {

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_DUMMYProcessDt", "A2ZACWMS"));

            if (result == 0)
            {
                var dt = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime processDate = dt.ProcessDate;
                DateTime NewprocessDate = dt.DummyProcessDate;


                string date = NewprocessDate.ToString("dd/MM/yyyy");
                lblNewProcDate.Text = date;

                txtLastProcDt.Text = Converter.GetString(String.Format("{0:D}", processDate));
                txtNewProcDt.Text = Converter.GetString(String.Format("{0:D}", NewprocessDate));

                txtStDay.Focus();
            }


            DivDetails.Visible = true;

            DivDetails.Style.Add("Top", "220px");
            DivDetails.Style.Add("Right", "640px");
            DivDetails.Style.Add("position", "fixed");
            DivDetails.Attributes.CssStyle.Add("opacity", "200");
            DivDetails.Attributes.CssStyle.Add("z-index", "200");



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //DivMain.Attributes.CssStyle.Add("opacity", "100");

            DivDetails.Visible = false;
            //DivMain.Visible = true;

            //unlockbutton();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStDay.Text == string.Empty)
                {
                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Input START OF DAY');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }

                    //DivMain.Attributes.CssStyle.Add("opacity", "100");

                    DivDetails.Visible = false;
                    //DivMain.Visible = true;

                    //unlockbutton();
                    txtStDay.Text = string.Empty;

                    return;

                }

                if (txtStDay.Text == "START OF DAY")
                {

                    lblProessing.Text = "Please Wait... Auto SOD Processing";

                    ProcessSOD();

                    return;

                }
                else
                {

                    //DivMain.Attributes.CssStyle.Add("opacity", "100");

                    DivDetails.Visible = false;
                    //DivMain.Visible = true;

                    //unlockbutton();
                    txtStDay.Text = string.Empty;
                    txtStDay.Focus();

                    String csname1 = "PopupScript";
                    Type cstype = GetType();
                    ClientScriptManager cs = Page.ClientScript;

                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Please Input START OF DAY');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ProcessSOD()
        {
            var prm = new object[2];
            prm[0] = lblID.Text;

            DateTime fdate = DateTime.ParseExact(lblNewProcDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            prm[1] = fdate;


            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_SODProcess", prm, "A2ZACWMS"));

            if (result == 0)
            {
                UpdateEODStat();
                DivDetails.Visible = false;

                Response.Redirect("A2ZWSERPMODULE.aspx", false);


                //DivMain.Visible = true;

                // SelectFromCashCodeSw();

                //ModuleFunction();
                return;
            }

        }

        protected void UpdateEODStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateEODStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }

        protected void UpdatedMSG()
        {
            DivUpdateMSG.Visible = true;
            LockAllButton();

            DivUpdateMSG.Style.Add("Top", "150px");
            DivUpdateMSG.Style.Add("left", "20px");
            DivUpdateMSG.Style.Add("position", "fixed");
            DivUpdateMSG.Attributes.CssStyle.Add("opacity", "200");
            DivUpdateMSG.Attributes.CssStyle.Add("z-index", "200");

            btnUpdMsg.Focus();
            btnUpdMsg.ForeColor = Color.Red;

            lblMsg1.Text = "Access Denied START OF DAY NOT DONE";     
        }

        protected void btnUpdMsg_Click(object sender, EventArgs e)
        {
            UnlockALlButton();
            DivUpdateMSG.Visible = false;
            Response.Redirect("A2ZLogin.aspx", false);  
        }


        protected void LockAllButton()
        {

            btnWSmodule.Enabled = false;
            btnHKmodule.Enabled = false;
            btnLogOut.Enabled = false;
            
        }

        protected void UnlockALlButton()
        {
            btnWSmodule.Enabled = true;
            btnHKmodule.Enabled = true;
            btnLogOut.Enabled = true;
        }

    }
}