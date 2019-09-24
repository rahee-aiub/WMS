using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
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
    public partial class ParameterMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();                
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());

                GetParameter();

            }
        }

        protected void GetParameter()
        {
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
            txtName.Text = Converter.GetString(dto.PrmUnitName);
            
            txtDataPath.Text = Converter.GetString(dto.PrmDataPath);
            txtBackPath.Text = Converter.GetString(dto.PrmBackUpPath);
            
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZHKERPMODULE.aspx");
        }

               

       
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            A2ZERPSYSPRMDTO objDTO = new A2ZERPSYSPRMDTO();
            
           
            objDTO.PrmDataPath = Converter.GetString(txtDataPath.Text);
            objDTO.PrmBackUpPath = Converter.GetString(txtBackPath.Text);
            

            int roweffect = A2ZERPSYSPRMDTO.UpdateInformation(objDTO);
            if (roweffect > 0)
            {
                

            }
        }

        

        
    }
}