using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.HumanResource
{
  public class A2ZEMPLEAVEDTO
    {
        #region Propertise

        public int EmpCode { set; get; }
        public int EmpLeaveCode { set; get; }
        public DateTime LStartDate { set; get; }
        public DateTime LEndDate { set; get; }
        public decimal EmpApplyDays { set; get; }
        public decimal EmpLBalance { set; get; }
        public string NullLStartDate { set; get; }
        public string NullLEndDate { set; get; }
        #endregion


        public static int InsertInformation(A2ZEMPLEAVEDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@LStartDate", DBNull.Value);
            SqlParameter param2 = new SqlParameter("@LEndDate", DBNull.Value);

            if (dto.NullLStartDate == "")
            {

                param1 = new SqlParameter("@LStartDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@LStartDate", dto.LStartDate);
            }

            if (dto.NullLEndDate == "")
            {
                param2 = new SqlParameter("@LEndDate", DBNull.Value);
            }

            else
            {
                param2 = new SqlParameter("@LEndDate", dto.LEndDate);

            }

            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZHRMCUS"), "Sp_EmpLeaveMaintenance", new object[] { dto.EmpCode, dto.EmpLeaveCode, param1, param2, dto.EmpApplyDays,dto.EmpLBalance });

            if (result == 0)
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
