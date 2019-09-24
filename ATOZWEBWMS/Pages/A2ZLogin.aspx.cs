using A2ZWEBWMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace A2ZWEBWMS.Pages
{
    public partial class A2ZLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                RemoveSession();

                txtUserId.Visible = false;
                txtPassword.Visible = false;

                DivLogin.Visible = false;

                DivUpdateMSG.Visible = false;

                lblAuthentic.Text = "Your Mobile is not valid  for this apps please Contact Your system administrator Thank You .";


                btnLogin.Visible = false;

                Label lberror = new Label();

                lberror.Text = "Please Infrom Your Vendore";
                lberror.ForeColor = Color.Red;
                lberror.Visible = true;

             
                divMain.Visible = true;
                divChangePass.Visible = false;

                DivLogin.Visible = true;
                txtUserId.Visible = true;
                txtPassword.Visible = true;
                btnLogin.Visible = true;

                lblAuthentic.Visible = false;

                txtUserId.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUserId.Focus();

                     

            }

        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        protected void RemoveSession()
        {
            SessionStore.SaveToCustomStore(Params.SYS_USER_ID, 0);
        }

        protected int CheckUserId()
        {
            // For Return Value of CheckUserId()
            //---------------------------------
            // 0 = Id is Available 
            // 1 = ID not in Table
            // 2 = Please Change Password - New Id was created
            // 3 = Id was not Initialize - Abnormal Logout
            //---------------------------------
            // End of For Return Value of CheckUserId()

            try
            {

                A2ZSYSIDSDTO idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtUserId.Text), "A2ZACWMS");

                lblhdnfldVariable.Text = idsDto.IdsName;

                if (idsDto.IdsNo == 0)
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('ID Not Found');", true);

                    txtUserId.Text = string.Empty;
                    txtUserId.Focus();

                    return 1;
                }

                //string sqlQuery = @"SELECT * FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO='" + txtUserId.Text + "' AND ModuleNo = 3";

                //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHKCUBS");

                //if (dt.Rows.Count > 0)
                //{

                    lblOrgPassH.Text = idsDto.IdsPass;

                    lblCashCode.Text = Converter.GetString(idsDto.GLCashCode);

                    lblUserBranchNo.Text = Converter.GetString(idsDto.UserBranchNo);

                    //lblMobileFlag.Text = Converter.GetString(idsDto.IdsMobile);

                    

                    A2ZERPBRANCHDTO getDTO = new A2ZERPBRANCHDTO();
                    int userbranch = Converter.GetInteger(lblUserBranchNo.Text);
                    getDTO = (A2ZERPBRANCHDTO.GetInformation(userbranch));

                    if (getDTO.BranchNo > 0)
                    {
                        lblCompanyName.Text = Converter.GetString(getDTO.BranchName);
                        UnitAddress1.Text = Converter.GetString(getDTO.BranchAdd1);
                        UnitNameB.Text = Converter.GetString(getDTO.BranchNameB);
                        UnitAddress1B.Text = Converter.GetString(getDTO.BranchAdd1B);
                    }

                    txtPassword.Focus();

                    if (idsDto.IdsPass == "XXXXXXXX")
                    {
                        return 2;
                    }

                      return 0;
                //}
                //else
                //{
                //    txtUserId.Text = string.Empty;
                //    txtUserId.Focus();

                //    return 1;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int checkUser = CheckUserId();

                if (checkUser == 0)
                {                   
                    btnLogin.Enabled = true;
                }
                else
                {
                    string msg = string.Empty;
                    switch (checkUser)
                    {
                        case 1:
                            msg = "ID Not Found";
                            txtUserId.Text = string.Empty;
                            txtUserId.Focus();
                            break;
                        case 2:
                            msg = "Change Password - New Id was Created";

                            break;
                        case 3:
                            msg = "User Id is using by other client or Abnormal Logout";

                            txtUserId.Text = string.Empty;
                            txtUserId.Focus();

                            break;
                        default:
                            msg = "Check User Id Information";

                            break;
                    }


                    DisplayMsg.Text = msg;
                    DisplayMassage();
                    return;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected int CheckBoothUserId()
        {
            // For Return Value of CheckUserId()
            //---------------------------------
            // 0 = Id is Available 
            // 1 = ID not in Table
            // 2 = Please Change Password - New Id was created
            // 3 = Id was not Initialize - Abnormal Logout
            //---------------------------------
            // End of For Return Value of CheckUserId()

            try
            {

                A2ZSYSIDSDTO idsDto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtBoothUserId.Text), "A2ZACWMS");

                lblhdnfldVariable.Text = idsDto.IdsName;
                lblOrgPassH.Text = idsDto.IdsPass;

                //lblMobileFlag.Text = Converter.GetString(idsDto.IdsMobile);

                if (idsDto.IdsNo == 0)
                {

                    DisplayMsg.Text = "ID Not Found";
                    DisplayMassage();
                
                    txtBoothUserId.Text = string.Empty;
                    txtBoothUserId.Focus();

                    return 0;
                }

                return 0;


                //string sqlQuery = @"SELECT * FROM dbo.A2ZSYSMODULECTRL  WHERE IDSNO='" + txtBoothUserId.Text + "' AND ModuleNo = 2";

                //DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlQuery, "A2ZHKWMS");

                //if (dt.Rows.Count > 0)
                //{
                //    lblOrgPassH.Text = idsDto.IdsPass;
                //    return 0;
                //}
                //else
                //{
                //    DisplayMsg.Text = "Invalid Id. No.";
                //    DisplayMassage();
                //    txtBoothUserId.Text = string.Empty;
                //    txtBoothUserId.Focus();

                //    return 0;
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void txtBoothUserId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int checkUser = CheckBoothUserId();

                if (checkUser == 0)
                {                 
                    btnLogin.Enabled = true;
                }
                else
                {
                    string msg = string.Empty;
                    switch (checkUser)
                    {
                        case 1:
                            msg = "User Id Not Available";
                            txtBoothUserId.Text = string.Empty;
                            txtBoothUserId.Focus();
                            break;
                        case 2:
                            msg = "Change Password - New Id was Created";

                            break;
                        case 3:
                            msg = "User Id is using by other client or Abnormal Logout";

                            txtBoothUserId.Text = string.Empty;
                            txtBoothUserId.Focus();

                            break;
                        default:
                            msg = "Check User Id Information";

                            break;
                    }


                    DisplayMsg.Text = msg;
                    DisplayMassage();
                    return;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                int checkUser = CheckUserId();

                if (checkUser == 0)
                {           
                    btnLogin.Enabled = true;
                }
                else
                {
                    string msg = string.Empty;
                    switch (checkUser)
                    {
                        case 1:

                            DisplayMsg.Text = "ID Not Found";
                            DisplayMassage();

                            txtUserId.Text = string.Empty;
                            txtUserId.Focus();
                            return;
                        case 2:
                            DisplayMsg.Text = "Change Password - New Id was Created";
                            DisplayMassage();
                            txtUserId.Text = string.Empty;
                            txtUserId.Focus();
                            return;
                        case 3:
                            DisplayMsg.Text = "User Id is using by other client or Abnormal Logout";
                            DisplayMassage();
                            txtUserId.Text = string.Empty;
                            txtUserId.Focus();
                            return;
                        default:
                            DisplayMsg.Text = "Check User Id Information";
                            DisplayMassage();
                            txtUserId.Text = string.Empty;
                            txtUserId.Focus();
                            return;
                    }
                }


                //---------------------------------------------------------------------------------------------------

                if (txtPassword.Text == string.Empty)
                {
                    txtPassword.Focus();
                    DisplayMsg.Text = "Password shoud be filled up.";
                    DisplayMassage();
                    return;
                }

                AtoZUtility a2zUtility = new AtoZUtility();
                string orgPass = a2zUtility.GeneratePassword(lblOrgPassH.Text.Replace(" ", ""), 1);
                string orgPass2 = a2zUtility.GeneratePassword(hidTripDetails.Value, 1);

                if (orgPass != txtPassword.Text)
                {
                    DisplayMsg.Text = "Wrong Password";
                    DisplayMassage();
                    txtPassword.Focus();
                    return;
                }


                //if (lblMobileFlag.Text == "False")
                //{
                //    DisplayMsg.Text = "Un Authorized User Id.";
                //    DisplayMassage();
                //    txtUserId.Text = string.Empty;
                //    txtPassword.Text = string.Empty;
                //    txtUserId.Focus();
                //    return;
                //}


                //------------------------------------------------------------------


                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());


                SessionStore.SaveToCustomStore(Params.BRNO, lblUserBranchNo.Text);
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME, lblCompanyName.Text);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, UnitAddress1.Text);
                SessionStore.SaveToCustomStore(Params.COMPANY_NAME_B, UnitNameB.Text);
                SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS_B, UnitAddress1B.Text);

                SessionStore.SaveToCustomStore(Params.SYS_USER_GLCASHCODE, lblCashCode.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_BRNO, lblUserBranchNo.Text);

                SessionStore.SaveToCustomStore(Params.SYS_USER_ID, txtUserId.Text);
                SessionStore.SaveToCustomStore(Params.SYS_USER_NAME, lblhdnfldVariable.Text);

                //--------------------------------------------------------------------

                DataAccessLayer.DTO.A2ZSYSIDSDTO.UpdateUserCSLoginFlag(Converter.GetInteger(txtUserId.Text), 1);
                Response.Redirect("A2ZERP.aspx", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnChangePassOpenPanel_Click(object sender, EventArgs e)
        {

            divMain.Visible = false;
            divChangePass.Visible = true;

            txtBoothUserId.Focus();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divMain.Visible = true;
            divChangePass.Visible = false;
        }

        protected void btnPasswordChange_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtNewPassword.Text == string.Empty)
                {
                    txtNewPassword.Focus();
                    MSG4();
                    return;
                }


                //AtoZUtility a2zUtility = new AtoZUtility();
                //string orgPass = a2zUtility.GeneratePassword(OrgPass.Value, 1);


                AtoZUtility a2zUtility = new AtoZUtility();
                string orgPass = a2zUtility.GeneratePassword(lblOrgPassH.Text.Replace(" ", ""), 1);

                if (orgPass != txtOldPassword.Text)
                {
                    txtOldPassword.Focus();
                    MSG1();

                }
                else
                {
                    if (txtNewPassword.Text != this.txtConfirmPassword.Text)
                    {
                        MSG2();
                    }
                    else
                    {

                        string newPass = a2zUtility.GeneratePassword(txtNewPassword.Text, 0);

                        A2ZSYSIDSDTO idsDto = new A2ZSYSIDSDTO();

                        idsDto.IdsNo = Converter.GetSmallInteger(txtBoothUserId.Text);
                        idsDto.IdsPass = newPass;

                        int rowEffiect = A2ZSYSIDSDTO.UpdateNewPassword(idsDto);

                        if (rowEffiect > 0)
                        {
                            MSG3();
                            txtBoothUserId.Text = string.Empty;
                            txtUserId.Text = string.Empty;
                            txtUserId.Focus();
                            divMain.Visible = true;
                            divChangePass.Visible = false;

                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        protected void MSG1()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                DisplayMsg.Text = "Old Password did not match.";
                DisplayMassage();

            }

            return;

        }

        protected void MSG2()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                DisplayMsg.Text = "New Password did not match.";
                DisplayMassage();

            }

            return;

        }

        protected void MSG3()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;



            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {

                DisplayMsg.Text = "Password Changed sucessfully";
                DisplayMassage();

            }

            return;

        }

        protected void MSG4()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                DisplayMsg.Text = "Empty New Password Inputted";
                DisplayMassage();

            }

            return;

        }

        protected void DisplayMassage()
        {
            DivUpdateMSG.Visible = true;


            DivLogin.Attributes.CssStyle.Add("opacity", "0.7");

            DivUpdateMSG.Style.Add("Top", "150px");
            DivUpdateMSG.Style.Add("left", "20px");
            DivUpdateMSG.Style.Add("position", "fixed");
            DivUpdateMSG.Attributes.CssStyle.Add("opacity", "200");
            DivUpdateMSG.Attributes.CssStyle.Add("z-index", "200");

            btnUpdMsg.Focus();
            btnUpdMsg.ForeColor = Color.Red;

            LockAllButton();

            lblMsg1.Text = string.Empty;
            lblMsg2.Text = string.Empty;

            lblMsg2.Text = DisplayMsg.Text;

        }

        protected void LockAllButton()
        {

            //ddlAccType.Enabled = false;
            //txtOldAccNo1.Enabled = false;
            //txtMemNo.Enabled = false;
            //BtnMemSearch.Enabled = false;
            //BtnMemCancel.Enabled = false;
            //BtnMemExit.Enabled = false;
            //BtnVchTransation.Enabled = false;
            //txtGrpAmount.Enabled = false;
            //BtnGrpSumbit.Enabled = false;

            //txtTotalAmount.Enabled = false;
            //txtVchNo.Enabled = false;
            //BtnUpdate.Enabled = false;
            //BtnCancel.Enabled = false;
            //BtnExit.Enabled = false;
        }

        protected void UnlockALlButton()
        {
            //ddlAccType.Enabled = true;
            //txtOldAccNo1.Enabled = true;
            //txtMemNo.Enabled = true;
            //BtnMemSearch.Enabled = true;
            //BtnMemCancel.Enabled = true;
            //BtnMemExit.Enabled = true;
            //BtnVchTransation.Enabled = true;
            //txtGrpAmount.Enabled = true;
            //BtnGrpSumbit.Enabled = true;

            //txtTotalAmount.Enabled = true;
            //txtVchNo.Enabled = true;
            //BtnUpdate.Enabled = true;
            //BtnCancel.Enabled = true;
            //BtnExit.Enabled = true;
        }

        protected void btnUpdMsg_Click(object sender, EventArgs e)
        {
            DivUpdateMSG.Visible = false;

            DivLogin.Attributes.CssStyle.Add("opacity", "1");

            UnlockALlButton();
        }
    }
}