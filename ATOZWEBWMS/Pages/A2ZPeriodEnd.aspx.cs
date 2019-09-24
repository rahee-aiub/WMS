using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO;
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
    public partial class A2ZPeriodEnd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                lblIDName.Text = "Login AS: " + DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();                
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());

                var dt = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime processDate = dt.ProcessDate;
                string date1 = processDate.ToString("dd/MM/yyyy");
                lblProcessDate.Text = date1;

                lblNewYear.Text = Converter.GetString(dt.CurrentYear);

                txtDayEnd.Focus();


                txtToDaysDate.Text = Converter.GetString(String.Format("{0:D}", processDate));


               
            }
        }

        
        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZHKERPMODULE.aspx");
        }



        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtDayEnd.Text == string.Empty)
                {
                    txtDayEnd.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input END OF DAY');", true);
                    return;

                }

                if (txtDayEnd.Text == "END OF DAY")
                {
                    ProcessEOD();
                }
                else
                {
                    txtDayEnd.Text = string.Empty;
                    txtDayEnd.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input END OF DAY');", true);
                    return;
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnProcess_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void ProcessEOD()
        {
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();

            CtrlBackUpStat.Text = Converter.GetString(dto.PrmBackUpStat);
            
            if (CtrlBackUpStat.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Database Backup Not Done');", true);
                return;
            }
            else
            {
                int periodFlag = 1;

                if (lblEndOfMonth.Visible)
                {
                    periodFlag = 2;
                }
                if (lblYearEnd.Visible)
                {
                    periodFlag = 3;
                }

                
                var prm = new object[2];
                prm[0] = lblUserID.Text;
                prm[1] = periodFlag;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_PeriodEnd", prm, "A2ZACWMS"));

                if (result == 0)
                {

                    if (periodFlag == 3)
                    {
                        //CreateNewYearDatabase();
                    }
                    //btnProcess.BackColor = Color.Black;
                    btnProcess.Enabled = false;
                    UpdateBackUpStat();
                   
                    UpdateEODStat();

                    //if (lblYearClose.Text == "1")
                    //{
                    //    UpdateYearEndStat();
                    //}



                    //hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                    string sql = "UPDATE A2ZERPSYSPRM SET PrmNoOfUser = 0";
                    int rowEff = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sql, "A2ZHKWMS"));

                    string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = 0 WHERE IdsNo = " + lblUserID.Text;
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACWMS"));

                    if (rowEffect > 0)
                    {
                        //CtrlProcFlag.Text = "1";
                        txtDayEnd.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Day End Process Sucessfully Done');", true);
                        return;
                   
                    }


                }

            }
        }

        protected void UpdateBackUpStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateBackUpStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateBackUpStat Problem');</script>");
                //throw ex;
            }

        }
        protected void UpdateEODStat()
        {
            try
            {
                Int16 BStat = 1;

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
       
    }
}