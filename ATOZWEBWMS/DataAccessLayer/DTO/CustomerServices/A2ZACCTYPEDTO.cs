using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZACCTYPEDTO
    {

       #region Propertise

       public int AccTypeCode { set; get; }     
       public int AccTypeGuaranty { set; get; }
       public int AccCorrType { set; get; }
       public String AccTypeDescription { set; get; }
       public Int16 AccTypeClass { set; get; }
       public String AccTypeClassDesc { set; get; }
       public Int16 AccFlag { set; get; }
       public Int16 AccTypeMode { set; get; }
       public Int16 AccCertNo { set; get; }
       public Int16 AccDivident { set; get; }
       public Int16 AccessT1 { set; get; }
       public Int16 AccessT2 { set; get; }
       public Int16 AccessT3 { set; get; }
       public Int16 AccProvisionMode { set; get; }
       public Int16 AccDepositMode { set; get; }
       public String AccDepositModeDesc { set; get; }
       public Int16 AccWeeklyDay { set; get; }
       public decimal AccDepRoundingBy { set; get; }
       public int AccProductGLCode { set; get; }
       public int AccProvisionGLCode { set; get; }
       public int AccInterestGLCode { set; get; }
       public int AccExcessIntGLCode { set; get; }
       public int AccExpenseGLCode { set; get; }
       public int AccFineGLCode { set; get; }  
       public int AccClosingFeeGLCode { set; get; }
       public int AccProcessFeeGLCode { set; get; }
       public int AccDepositFeeGLCode { set; get; }
       public int AccBonusGLCode { set; get; }
       #endregion


        public static int InsertInformation(A2ZACCTYPEDTO dto)
        {
            dto.AccTypeDescription = (dto.AccTypeDescription != null) ? dto.AccTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZACCTYPE(AccTypeCode,AccTypeDescription,AccTypeClass,AccTypeClassDesc,AccFlag,AccTypeMode,AccCertNo,AccDivident,AcessType1,AcessType2,AcessType3,AccDepositMode,AccDepositModeDesc,AccWeeklyDay,AccDepRoundingBy,AccTypeGuaranty,AccCorrType,AccProductGLCode,AccProvisionGLCode,AccInterestGLCode,AccExcessIntGLCode,AccExpenseGLCode,AccFineGLCode,AccClosingFeeGLCode,AccProcessFeeGLCode,AccDepositFeeGLCode,AccBonusGLCode,AccProvisionMode)values('" + dto.AccTypeCode + "','" +
                dto.AccTypeDescription + "','" + dto.AccTypeClass + "','" + dto.AccTypeClassDesc + "','" + dto.AccFlag + "','" + dto.AccTypeMode + "','" + dto.AccCertNo + "','" + dto.AccDivident + "','" + dto.AccessT1 + "','" + dto.AccessT2 + "','" + dto.AccessT3 + "','" + dto.AccDepositMode + "','" + dto.AccDepositModeDesc + "','" + dto.AccWeeklyDay + "','" + dto.AccDepRoundingBy + "','" + dto.AccTypeGuaranty + "','" + dto.AccCorrType + "','" + dto.AccProductGLCode + "','" + dto.AccProvisionGLCode + "','" + dto.AccInterestGLCode + "','" + dto.AccExcessIntGLCode + "','" + dto.AccExpenseGLCode + "','" + dto.AccFineGLCode + "','" + dto.AccClosingFeeGLCode + "','" + dto.AccProcessFeeGLCode + "','" + dto.AccDepositFeeGLCode + "','" + dto.AccBonusGLCode + "','" + dto.AccProvisionMode + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static A2ZACCTYPEDTO GetInformation(int Typecode)
        {
            var prm = new object[1];

            prm[0] = Typecode;
            

            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoAccType", prm, "A2ZCSCUBS");
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCTYPE WHERE AccTypeCode = " + Typecode, "A2ZCSMCUS");


            var p = new A2ZACCTYPEDTO();
            if (dt.Rows.Count > 0)
            {

                p.AccTypeCode = Converter.GetInteger(dt.Rows[0]["AccTypeCode"]);
                p.AccTypeDescription = Converter.GetString(dt.Rows[0]["AccTypeDescription"]);
                p.AccFlag = Converter.GetSmallInteger(dt.Rows[0]["AccFlag"]);
                p.AccTypeClass = Converter.GetSmallInteger(dt.Rows[0]["AccTypeClass"]);
                p.AccTypeClassDesc = Converter.GetString(dt.Rows[0]["AccTypeClassDesc"]);
                p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);
                
                p.AccCertNo = Converter.GetSmallInteger(dt.Rows[0]["AccCertNo"]);
                p.AccDivident = Converter.GetSmallInteger(dt.Rows[0]["AccDivident"]);
                p.AccessT1 = Converter.GetSmallInteger(dt.Rows[0]["AcessType1"]);
                p.AccessT2 = Converter.GetSmallInteger(dt.Rows[0]["AcessType2"]);
                p.AccessT3 = Converter.GetSmallInteger(dt.Rows[0]["AcessType3"]);
               
                p.AccDepositMode = Converter.GetSmallInteger(dt.Rows[0]["AccDepositMode"]);
                p.AccDepositModeDesc = Converter.GetString(dt.Rows[0]["AccDepositModeDesc"]);
                p.AccWeeklyDay = Converter.GetSmallInteger(dt.Rows[0]["AccWeeklyDay"]);

                p.AccDepRoundingBy = Converter.GetDecimal(dt.Rows[0]["AccDepRoundingBy"]);
                p.AccTypeGuaranty = Converter.GetInteger(dt.Rows[0]["AccTypeGuaranty"]);
                p.AccCorrType = Converter.GetInteger(dt.Rows[0]["AccCorrType"]);

                p.AccProductGLCode = Converter.GetInteger(dt.Rows[0]["AccProductGLCode"]);
                p.AccProvisionGLCode = Converter.GetInteger(dt.Rows[0]["AccProvisionGLCode"]);
                p.AccInterestGLCode = Converter.GetInteger(dt.Rows[0]["AccInterestGLCode"]);
                p.AccExcessIntGLCode = Converter.GetInteger(dt.Rows[0]["AccExcessIntGLCode"]);
                p.AccExpenseGLCode = Converter.GetInteger(dt.Rows[0]["AccExpenseGLCode"]);
                p.AccFineGLCode = Converter.GetInteger(dt.Rows[0]["AccFineGLCode"]);      
                p.AccClosingFeeGLCode = Converter.GetInteger(dt.Rows[0]["AccClosingFeeGLCode"]);
                p.AccProcessFeeGLCode = Converter.GetInteger(dt.Rows[0]["AccProcessFeeGLCode"]);
                p.AccDepositFeeGLCode = Converter.GetInteger(dt.Rows[0]["AccDepositFeeGLCode"]);
                p.AccBonusGLCode = Converter.GetInteger(dt.Rows[0]["AccBonusGLCode"]);
                p.AccProvisionMode = Converter.GetSmallInteger(dt.Rows[0]["AccProvisionMode"]);
                return p;
            }
            p.AccTypeCode = 0;

            return p;
        }


        public static A2ZACCTYPEDTO GetOldInformation(int Typecode)
        {
            var prm = new object[1];

            prm[0] = Typecode;


            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCTYPE WHERE OldAccType = " + Typecode, "A2ZCSCUBS");


            var p = new A2ZACCTYPEDTO();
            if (dt.Rows.Count > 0)
            {

                p.AccTypeCode = Converter.GetInteger(dt.Rows[0]["AccTypeCode"]);
                p.AccTypeDescription = Converter.GetString(dt.Rows[0]["AccTypeDescription"]);
                p.AccFlag = Converter.GetSmallInteger(dt.Rows[0]["AccFlag"]);
                p.AccTypeClass = Converter.GetSmallInteger(dt.Rows[0]["AccTypeClass"]);
                p.AccTypeClassDesc = Converter.GetString(dt.Rows[0]["AccTypeClassDesc"]);
                p.AccTypeMode = Converter.GetSmallInteger(dt.Rows[0]["AccTypeMode"]);
                p.AccCertNo = Converter.GetSmallInteger(dt.Rows[0]["AccCertNo"]);
                p.AccDivident = Converter.GetSmallInteger(dt.Rows[0]["AccDivident"]);
                p.AccessT1 = Converter.GetSmallInteger(dt.Rows[0]["AcessType1"]);
                p.AccessT2 = Converter.GetSmallInteger(dt.Rows[0]["AcessType2"]);
                p.AccessT3 = Converter.GetSmallInteger(dt.Rows[0]["AcessType3"]);
               
                p.AccDepositMode = Converter.GetSmallInteger(dt.Rows[0]["AccDepositMode"]);
                p.AccDepositModeDesc = Converter.GetString(dt.Rows[0]["AccDepositModeDesc"]);
                p.AccWeeklyDay = Converter.GetSmallInteger(dt.Rows[0]["AccWeeklyDay"]);

                p.AccDepRoundingBy = Converter.GetDecimal(dt.Rows[0]["AccDepRoundingBy"]);
                p.AccTypeGuaranty = Converter.GetInteger(dt.Rows[0]["AccTypeGuaranty"]);
                p.AccCorrType = Converter.GetInteger(dt.Rows[0]["AccCorrType"]);

                p.AccProductGLCode = Converter.GetInteger(dt.Rows[0]["AccProductGLCode"]);
                p.AccProvisionGLCode = Converter.GetInteger(dt.Rows[0]["AccProvisionGLCode"]);
                p.AccInterestGLCode = Converter.GetInteger(dt.Rows[0]["AccInterestGLCode"]);
                p.AccExcessIntGLCode = Converter.GetInteger(dt.Rows[0]["AccExcessIntGLCode"]);
                p.AccExpenseGLCode = Converter.GetInteger(dt.Rows[0]["AccExpenseGLCode"]);
                p.AccFineGLCode = Converter.GetInteger(dt.Rows[0]["AccFineGLCode"]);
                p.AccClosingFeeGLCode = Converter.GetInteger(dt.Rows[0]["AccClosingFeeGLCode"]);
                p.AccProcessFeeGLCode = Converter.GetInteger(dt.Rows[0]["AccProcessFeeGLCode"]);
                p.AccDepositFeeGLCode = Converter.GetInteger(dt.Rows[0]["AccDepositFeeGLCode"]);
                p.AccBonusGLCode = Converter.GetInteger(dt.Rows[0]["AccBonusGLCode"]);
                p.AccProvisionMode = Converter.GetSmallInteger(dt.Rows[0]["AccProvisionMode"]);
                return p;
            }
            p.AccTypeCode = 0;

            return p;
        }


        public static int UpdateInformation(A2ZACCTYPEDTO dto)
        {
            dto.AccTypeDescription = (dto.AccTypeDescription != null) ? dto.AccTypeDescription.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZACCTYPE set AccTypeCode='" + dto.AccTypeCode + "',AccTypeDescription='" + dto.AccTypeDescription + "',AccTypeClass='" +
                dto.AccTypeClass + "',AccTypeClassDesc='" + dto.AccTypeClassDesc + "',AccFlag='" + dto.AccFlag + "',AccTypeMode='" + dto.AccTypeMode + "',AccCertNo='" + dto.AccCertNo + "',AccDivident='" + dto.AccDivident + "',AcessType1='" + 
                dto.AccessT1 + "',AcessType2='" +
                dto.AccessT2 + "',AcessType3='" + dto.AccessT3 + "',AccDepositMode='" + dto.AccDepositMode + "',AccDepositModeDesc='" + 
                dto.AccDepositModeDesc + "',AccWeeklyDay='" + dto.AccWeeklyDay + "',AccDepRoundingBy='" + dto.AccDepRoundingBy + "',AccTypeGuaranty='" + 
                dto.AccTypeGuaranty + "',AccCorrType='" + dto.AccCorrType + "',AccProductGLCode='" + dto.AccProductGLCode + "',AccProvisionGLCode='" + 
                dto.AccProvisionGLCode + "',AccInterestGLCode='" + dto.AccInterestGLCode + "',AccExcessIntGLCode='" + dto.AccExcessIntGLCode + "',AccExpenseGLCode='" + dto.AccExpenseGLCode + "',AccFineGLCode='" + 
                dto.AccFineGLCode + "',AccClosingFeeGLCode='" + dto.AccClosingFeeGLCode + "',AccProcessFeeGLCode='" + dto.AccProcessFeeGLCode + "',AccDepositFeeGLCode='" + dto.AccDepositFeeGLCode + "',AccBonusGLCode='" + dto.AccBonusGLCode + "',AccProvisionMode='" + 
                dto.AccProvisionMode + "' where AccTypeCode='" + dto.AccTypeCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));
            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }



    }
}
