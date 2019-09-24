using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBWMS.Pages
{
    public partial class EditParty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
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


                lblPurchaseParty.Visible = false;
                DivPurchaseParty.Visible = false;
                lblSalesParty.Visible = false;
                DivSalesParty.Visible = false;

                ddlPurchasePartyName.Enabled = false;
                ddlSalesPartyName.Enabled = false;


            }

        }

        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode='" + ddlGroup.SelectedValue + "' GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACWMS");
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

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZWSERPMODULE.aspx");
        }


        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(Converter.GetInteger(ddlPartyName.SelectedValue)));
            if (getDTO.PartyName != null)
            {
                txtPartyName.Text = Converter.GetString(getDTO.PartyName);
                txtPartyAddress.Text = Converter.GetString(getDTO.PartyAddresss);
                txtPartyPhone.Text = Converter.GetString(getDTO.PartyMobileNo);
                txtPartyEmail.Text = Converter.GetString(getDTO.PartyEmail);

                if (ddlGroup.SelectedValue == "12")
                {
                    ddlPurchasePartyName.SelectedValue = Converter.GetString(getDTO.PartyPurchaseCode);
                    ddlSalesPartyName.SelectedValue = Converter.GetString(getDTO.PartySalesCode);
                }
            }
            else
            {
                txtPartyName.Text = string.Empty;
                txtPartyAddress.Text = string.Empty;
                txtPartyPhone.Text = string.Empty;
                txtPartyEmail.Text = string.Empty;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            A2ZPARTYDTO objDTO = new A2ZPARTYDTO();

           
        
            objDTO.PartyCode = Converter.GetInteger(ddlPartyName.SelectedValue);
            objDTO.PartyName = Converter.GetString(txtPartyName.Text);
            objDTO.PartyAddresss = Converter.GetString(txtPartyAddress.Text);           
            objDTO.PartyMobileNo = Converter.GetString(txtPartyPhone.Text);
            objDTO.PartyEmail = Converter.GetString(txtPartyEmail.Text);
        

            int roweffect = A2ZPARTYDTO.UpdateParty(objDTO);
            if (roweffect > 0)
            {
                Response.Redirect(Request.RawUrl);
               
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            PartyDropdown();

            if (ddlGroup.SelectedValue == "12")
            {
                lblPurchaseParty.Visible = true;
                DivPurchaseParty.Visible = true;
                lblSalesParty.Visible = true;
                DivSalesParty.Visible = true;

                ddlPurchasePartyName.Enabled = false;
                ddlSalesPartyName.Enabled = false;

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