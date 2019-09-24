using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBWMS.Pages
{
    public partial class HolidayTypeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();                
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());
                HolidayTypeDropdown();

                txtDescription.Focus();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;

            }
        }

        private void HolidayTypeDropdown()
        {


            string sqlquery = "SELECT HolType,HolTypeDescription from A2ZHOLIDAYTYPE";
            ddlHolType = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlHolType, "A2ZHKWMS");
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZHKERPMODULE.aspx");
        }

        private void clearinfo()
        {
            txtHolType.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZHOLIDAYTYPEDTO objDTO = new A2ZHOLIDAYTYPEDTO();

                objDTO.HolType = Converter.GetInteger(txtHolType.Text);
                objDTO.HolTypeDescription = Converter.GetString(txtDescription.Text);

                int roweffect = A2ZHOLIDAYTYPEDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    txtHolType.Focus();
                    clearinfo();
                    HolidayTypeDropdown();
                    //gvDetail();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            A2ZHOLIDAYTYPEDTO UpDTO = new A2ZHOLIDAYTYPEDTO();
            UpDTO.HolType = Converter.GetInteger(txtHolType.Text);
            UpDTO.HolTypeDescription = Converter.GetString(txtDescription.Text);

            int roweffect = A2ZHOLIDAYTYPEDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                HolidayTypeDropdown();
                clearinfo();
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                txtHolType.Focus();
                //gvDetail();

            }
        }

        protected void txtHolType_TextChanged(object sender, EventArgs e)
        {
            if (ddlHolType.SelectedValue == "-Select-")
            {
                txtDescription.Focus();

            }
            try
            {

                if (txtHolType.Text != string.Empty)
                {
                    int MainCode = Converter.GetInteger(txtHolType.Text);
                    A2ZHOLIDAYTYPEDTO getDTO = (A2ZHOLIDAYTYPEDTO.GetInformation(MainCode));

                    if (getDTO.HolType > 0)
                    {
                        txtHolType.Text = Converter.GetString(getDTO.HolType);
                        txtDescription.Text = Converter.GetString(getDTO.HolTypeDescription);
                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        ddlHolType.SelectedValue = Converter.GetString(getDTO.HolType);
                        txtDescription.Focus();
                    }
                    else
                    {
                        txtDescription.Text = string.Empty;
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                        txtDescription.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlHolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHolType.SelectedValue == "-Select-")
            {
                txtHolType.Focus();
                txtHolType.Text = string.Empty;
                txtDescription.Text = string.Empty;
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
            }

            try
            {


                if (ddlHolType.SelectedValue != "-Select-")
                {

                    int MainCode = Converter.GetInteger(ddlHolType.SelectedValue);
                    A2ZHOLIDAYTYPEDTO getDTO = (A2ZHOLIDAYTYPEDTO.GetInformation(MainCode));
                    if (getDTO.HolType > 0)
                    {
                        txtHolType.Text = Converter.GetString(getDTO.HolType);
                        txtDescription.Text = Converter.GetString(getDTO.HolTypeDescription);
                        btnSubmit.Visible = false;
                        btnUpdate.Visible = true;
                        txtDescription.Focus();


                    }
                    else
                    {
                        txtHolType.Focus();
                        txtHolType.Text = string.Empty;
                        txtDescription.Text = string.Empty;
                        btnSubmit.Visible = true;
                        btnUpdate.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}