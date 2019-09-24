﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZLOANSCHEDULEDTO
    {
        #region Propertise

        public int ID { set; get; }

        public Decimal SchduleLoan { set; get; }
        public int NoInstl { set; get; }
        public Decimal IntRate { set; get; }
        public int PaymentMode { set; get; }
        public string PaymentModeDesc { set; get; }
        public int WeekDays { set; get; }
        public Decimal instlAmt { set; get; }
        public Decimal LastInstlAmt { set; get; }
        public Decimal TotalIntAmount { set; get; }
        public Decimal NetPayable { set; get; }
        public int LoanMth { set; get; }
        public DateTime LoanMthDate { set; get; }
        public Decimal InstlAmt { set; get; }
        public Decimal LoanAmt { set; get; }
        public Decimal IntAmt { set; get; }
        public Decimal LoanPayable { set; get; }
        #endregion

        public static int InsertInformation(A2ZLOANSCHEDULEDTO dto)
        {
            
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZWFLOANSCHEDULE(ScheduleLoanAmt,NoofInstl,IntRate,PaymentMode,PaymentModeDesc,WeekDays,InstlAmount,LastInstlAmount,TotalIntAmount,NetPayable,LoanMth,LoanMthDate,InstlAmt,LoanAmt,IntAmt,LoanPayable)values('" + dto.SchduleLoan + "','" + dto.NoInstl + "','" + dto.IntRate + "','" + dto.PaymentMode + "','" + dto.PaymentModeDesc + "','" + dto.WeekDays + "','" + dto.instlAmt + "','" + dto.LastInstlAmt + "','" + dto.TotalIntAmount + "','" + dto.NetPayable + "','" + dto.LoanMth + "','" + dto.LoanMthDate + "','" + dto.InstlAmt + "','" + dto.LoanAmt + "','" + dto.IntAmt + "','" + dto.LoanPayable + "')";
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
