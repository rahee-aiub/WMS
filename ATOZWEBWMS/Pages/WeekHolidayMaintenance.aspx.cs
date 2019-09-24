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
    public partial class WeekHolidayMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();                
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());
                
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;

                GetWeekHolidayInfo();

            }
        }

        public void GetWeekHolidayInfo()
        {
            A2ZWEEKHOLIDAYDTO getDTO = (A2ZWEEKHOLIDAYDTO.GetInformation());

            if (getDTO.Record > 0)
            {
                ddlHolWeekDay1.SelectedValue = Converter.GetString(getDTO.HolWeekDay1);
                ddlHolWeekDay2.SelectedValue = Converter.GetString(getDTO.HolWeekDay2);
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;

            }
            else
            {
                btnSubmit.Visible = true;
                btnUpdate.Visible = false;

            }

        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZHKERPMODULE.aspx");
        }

        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZWEEKHOLIDAYDTO objDTO = new A2ZWEEKHOLIDAYDTO();

                objDTO.HolWeekDay1 = Converter.GetSmallInteger(ddlHolWeekDay1.SelectedValue);
                objDTO.HolWeekDay2 = Converter.GetSmallInteger(ddlHolWeekDay2.SelectedValue);
                objDTO.HolWeekDayName1 = Converter.GetString(ddlHolWeekDay1.SelectedItem);
                objDTO.HolWeekDayName2 = Converter.GetString(ddlHolWeekDay2.SelectedItem);

                int roweffect = A2ZWEEKHOLIDAYDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {
                    Response.Redirect("A2ZERPModule.aspx");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            A2ZWEEKHOLIDAYDTO UpDTO = new A2ZWEEKHOLIDAYDTO();

            UpDTO.HolWeekDay1 = Converter.GetSmallInteger(ddlHolWeekDay1.SelectedValue);
            UpDTO.HolWeekDay2 = Converter.GetSmallInteger(ddlHolWeekDay2.SelectedValue);
            UpDTO.HolWeekDayName1 = Converter.GetString(ddlHolWeekDay1.SelectedItem);
            UpDTO.HolWeekDayName2 = Converter.GetString(ddlHolWeekDay2.SelectedItem);

            int roweffect = A2ZWEEKHOLIDAYDTO.UpdateInformation(UpDTO);
            if (roweffect > 0)
            {
                Response.Redirect("A2ZERPModule.aspx");

            }
        }

        
        
    }
}