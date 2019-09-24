using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBWMS.Pages
{
    public partial class OpenNewParty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAddress.Visible = false;
                DivAddress.Visible = false;
                lblPhone.Visible = false;
                DivPhone.Visible = false;
                lblEmail.Visible = false;
                DivEmail.Visible = false;
                     

                lblPurchaseParty.Visible = false;
                DivPurchaseParty.Visible = false;

                lblSalesParty.Visible = false;
                DivSalesParty.Visible = false;

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                if (lblID.Text == string.Empty || lblID.Text == "0")
                {
                    Response.Redirect("A2ZLogin.aspx");
                }

                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();                
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());
                PurchasePartyDropdown();
                SalesPartyDropdown();

            }
        }


        private void PurchasePartyDropdown()
        {
            string sqlquery = "SELECT DISTINCT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 14 GROUP BY PartyCode,PartyName ORDER BY PartyName ASC ";
            ddlPurchasePartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPurchasePartyName, "A2ZACWMS");
        }

        private void SalesPartyDropdown()
        {
            string sqlquery = "SELECT DISTINCT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 20 GROUP BY PartyCode,PartyName ORDER BY PartyName ASC ";
            ddlSalesPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlSalesPartyName, "A2ZACWMS");
        }

        //private void PartyGroupDropdown()
        //{


        //    string sqlquery = "SELECT CtrlRecType,CtrlNote FROM A2ZRECCTRL";
        //    ddlGroup = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGroup, "A2ZACWMS");
        //}

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZWSERPMODULE.aspx");
        }

      

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGroup.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Party Group');", true);
                    return;
                }

                if (ddlGroup.SelectedValue == "12")
                {
                    if(txtPartyName.Text.Length > 5)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Tracking Code Should be < 6 Character');", true);
                        return;
                    }

                    string qry = "SELECT Id,PartyCode FROM A2ZPARTYCODE  WHERE PartyPurchaseCode = '" + ddlPurchasePartyName.SelectedValue + "' AND PartySalesCode = '" + ddlSalesPartyName.SelectedValue + "'";
                    DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACWMS");
                    if (dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Tracking Code Already Exist.');", true);
                        return;
                    }
                }

                int code = Converter.GetInteger(ddlGroup.SelectedValue);
                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(code));
                lblLastLPartyNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);
               
                GenerateNewLPartyNo();
                UpdateRecords();


            }
            catch (Exception ex)
            {

                //throw ex;
            }
        }


        protected void GenerateNewLPartyNo()
        {
            string input1 = Converter.GetString(lblLastLPartyNo.Text).Length.ToString();

            string result1 = "";

            if (input1 == "1")
            {
                result1 = "000";
            }
            if (input1 == "2")
            {
                result1 = "00";
            }
            if (input1 == "3")
            {
                result1 = "0";
            }

            if (input1 == "4")
            {
                lblNewLPartyNo.Text = ddlGroup.SelectedValue + lblLastLPartyNo.Text;
            }
            else
            {
                lblNewLPartyNo.Text = ddlGroup.SelectedValue + result1 + lblLastLPartyNo.Text;
            }


        }


        private void UpdateRecords()
        {

            A2ZPARTYDTO objDTO = new A2ZPARTYDTO();

            objDTO.GroupCode = Converter.GetInteger(ddlGroup.SelectedValue);
            objDTO.GroupName = Converter.GetString(ddlGroup.SelectedItem.Text);
            objDTO.PartyCode = Converter.GetInteger(lblNewLPartyNo.Text);
            objDTO.PartyName = Converter.GetString(txtPartyName.Text);
            objDTO.PartyAddresss = Converter.GetString(txtPartyAddress.Text);
            objDTO.PartyMobileNo = Converter.GetString(txtPartyPhone.Text);
            objDTO.PartyEmail = Converter.GetString(txtPartyEmail.Text);

            if (ddlGroup.SelectedValue == "12")
            {
                objDTO.PartyPurchaseCode = Converter.GetInteger(ddlPurchasePartyName.SelectedValue);
                objDTO.PartySalesCode = Converter.GetInteger(ddlSalesPartyName.SelectedValue);
            }
            else
            {
                objDTO.PartyPurchaseCode = Converter.GetInteger(0);
                objDTO.PartySalesCode = Converter.GetInteger(0);
            }


            int roweffect = A2ZPARTYDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {

                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedValue == "12")
            {
                lblPurchaseParty.Visible = true;
                DivPurchaseParty.Visible = true;

                lblSalesParty.Visible = true;
                DivSalesParty.Visible = true;
            }
            else 
            {
                lblPurchaseParty.Visible = false;
                DivPurchaseParty.Visible = false;

                lblSalesParty.Visible = false;
                DivSalesParty.Visible = false; 
            }
            
        }
    }
}