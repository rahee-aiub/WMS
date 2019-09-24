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
    public partial class NationalHolidayMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());

                A2ZCSPARAMETERDTO dto2 = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt2 = Converter.GetDateTime(dto2.ProcessDate);
                string date1 = dt2.ToString("dd/MM/yyyy");
                txtHolDate.Text = date1;
                lblProcessDate.Text = date1;

                HolidayTypeDropdown();

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
            txtHolDate.Text = string.Empty;
            txtHolDate.Text = lblProcessDate.Text;
            ddlHolType.SelectedIndex = 0;
            txtHolNote.Text = string.Empty;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlHolType.SelectedValue != "-Select-")
                {
                    A2ZHOLIDAYDTO objDTO = new A2ZHOLIDAYDTO();
                    //DateTime opdate = Converter.GetDateTime(txtHolDate.Text);
                    DateTime opdate = DateTime.ParseExact(txtHolDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    objDTO.HolDate = opdate;
                    objDTO.HolType = Converter.GetInteger(ddlHolType.SelectedValue);
                    objDTO.HolTypeDesc = Converter.GetString(ddlHolType.SelectedItem.Text);
                    objDTO.HolNote = Converter.GetString(txtHolNote.Text);
                    objDTO.HolDayName = Converter.GetString(lblDayName.Text);

                    int roweffect = A2ZHOLIDAYDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                        clearinfo();
                        txtHolDate.Focus();
                        //BtnDelete.Visible = false;
                        //dropdown();
                        //gvDetail();
                        lblDayName.Text = string.Empty;
                        ddlHolType.SelectedIndex = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            A2ZHOLIDAYDTO UpDTO = new A2ZHOLIDAYDTO();

            DateTime opdate = DateTime.ParseExact(txtHolDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            UpDTO.HolDate = opdate;
            UpDTO.HolType = Converter.GetInteger(ddlHolType.SelectedValue);
            UpDTO.HolTypeDesc = Converter.GetString(ddlHolType.SelectedItem.Text);
            UpDTO.HolNote = Converter.GetString(txtHolNote.Text);
            UpDTO.HolDayName = Converter.GetString(lblDayName.Text);

            int roweffect = A2ZHOLIDAYDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {

                clearinfo();
                //dropdown();

                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                //BtnDelete.Visible = false;
                txtHolDate.Focus();
                //gvDetail();

            }

        }



        protected void txtHolDate_TextChanged(object sender, EventArgs e)
        {
            var dat = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime processDate = dat.ProcessDate;

            DateTime HolDate = DateTime.ParseExact(txtHolDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            A2ZHOLIDAYDTO getDTO = (A2ZHOLIDAYDTO.GetInformation(HolDate));

            lblDayName.Text = HolDate.DayOfWeek.ToString();

            if (HolDate < processDate)
            {
                txtHolDate.Text = string.Empty;

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Previous Date Not Accepted');", true);
                return;
            }

            if (getDTO.HolType > 0)
            {
                //DateTime dt = Converter.GetDateTime(getDTO.HolDate);
                //string date = dt.ToString("dd/MM/yyyy");
                //txtHolDate.Text = date;
                ddlHolType.SelectedValue = Converter.GetString(getDTO.HolType);
                lblHolTypeDesc.Text = Converter.GetString(getDTO.HolTypeDesc);
                txtHolNote.Text = Converter.GetString(getDTO.HolNote);
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
                //BtnDelete.Visible = true;
            }
            else
            {
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;
                //BtnDelete.Visible = false;
                ddlHolType.SelectedIndex = 0;
                txtHolNote.Text = string.Empty;


            }
        }


    }
}