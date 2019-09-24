﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.BLL;

namespace DataAccessLayer.DTO.CustomerServices
{


    public class A2ZTRNLOGICDTO
    {
        #region Propertise

        public decimal BalAmount { set; get; }
        public decimal InstlAmt { set; get; }
        public decimal DisbAmount { set; get; }
        public decimal SancAmount { set; get; }
        public decimal InterestRate { set; get; }
        public Int16 RoundFlag { set; get; }

        public Int16 IntWithdrDays { set; get; }
        public DateTime RenewalDate { set; get; }
        public decimal AccPrincipal { set; get; }

        public decimal AccRenwlAmt { set; get; }
        public decimal AccFixedAmt { set; get; }
        public decimal AccFixedMthInt { set; get; }
        public decimal AccProvBalance { set; get; }

        public decimal UptoPaidNo { set; get; }
        public DateTime UptoPaidDate { set; get; }
        public decimal DueDepositAmt { set; get; }
        public decimal NoMonths { set; get; }
        public decimal AccOrgAmt { set; get; }
        public DateTime CalFDate { set; get; }
        public DateTime CalProvDate { set; get; }
        public Int16 CalNofDays { set; get; }
        public decimal CalIntRate { set; get; }
        public double CalFDAmount { set; get; }

        public Int16 CalPeriod { set; get; }
        public double CalOrgInterest { set; get; }
        public double CalPaidInterest { set; get; }
        public double CalInterest { set; get; }
        public double CalPenalAmt { set; get; }
        public double BalInterest { set; get; }
        public double RestInterest { set; get; }
        public decimal CalEncashment { set; get; }
        public double LogicAmount { set; get; }
        public double LogicIntAmount { set; get; }
        public double CalProvAdjCr { set; get; }

        public double CalProvAdjDr { set; get; }
        public double AccCurrIntAmt { set; get; }
        public double AccDuePrincAmt { set; get; }
        public double AccDueIntAmt { set; get; }
        public double AccHoldIntAmt { set; get; }
        public double AccDuePenalAmt { set; get; }
        public double AccLastDuePrincAmt { set; get; }
        public double AccLastDueIntAmt { set; get; }

        public DateTime ProcDate { set; get; }
        public int UptoYear { set; get; }
        public String UptoMonth { set; get; }
        public int ProvSw { set; get; }
        public short NoMsg { set; get; }
        public short NoRecord { set; get; }   // 1= Record Found, 0=No Record Found
        public int AccPeriod { set; get; }
        public DateTime OpenDate { set; get; }
        public double MthDeposit { set; get; }
        public double TotalDepAmt { set; get; }
        public String UptoDate { set; get; }
        public decimal AccLienAmt { set; get; }
        public decimal AdjBalAmount { set; get; }
        public DateTime BenefitDate { set; get; }

        #endregion



        public static A2ZTRNLOGICDTO GetShareMinAmt(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            var p = new A2ZTRNLOGICDTO();

            var prm = new object[5];

            prm[0] = BranchNo;
            prm[1] = MemType;
            prm[2] = MemberNo;
            prm[3] = AccType;
            prm[4] = AccountNo;
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateSharePenalAmt", prm, "A2ZCSCUBS"));
            if (result == 0)
            {
                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");

                if (dt.Rows.Count > 0)
                {
                    p.CalPeriod = Converter.GetSmallInteger(dt.Rows[0]["CalPeriod"]);

                    DataTable dt1 = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + AccType + "'", "A2ZCSCUBS");

                    if (dt1.Rows.Count > 0)
                    {
                        p.LogicAmount = Converter.GetDouble(dt1.Rows[0]["PrmMinDeposit"]);
                    }

                    if (p.CalPeriod != 0)
                    {
                        p.LogicAmount = Converter.GetDouble(p.LogicAmount * p.CalPeriod);
                    }
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.NoRecord = 0;
                }

            }
            return p;

        }

        public static A2ZTRNLOGICDTO GetPensionDepositAmt(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {




            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {

                p.LogicAmount = Converter.GetDouble(dt.Rows[0]["AccMonthlyDeposit"]);
                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }


            return p;

        }

        public static A2ZTRNLOGICDTO GetFixedDepositAmt(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.AccOrgAmt = Converter.GetDecimal(dt.Rows[0]["AccOrgAmt"]);

                if (p.AccOrgAmt != 0)
                {
                    p.NoMsg = Converter.GetSmallInteger(1);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.NoRecord = Converter.GetSmallInteger(1);
                    p.LogicAmount = Converter.GetDouble(dt.Rows[0]["AccFixedAmt"]);
                    p.NoMsg = 0;
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
                p.NoMsg = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetLoanDisbursementAmt(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.DisbAmount = Converter.GetDecimal(dt.Rows[0]["AccDisbAmt"]);
                p.SancAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);

                p.LogicAmount = Converter.GetDouble(p.SancAmount - p.DisbAmount);

                if (p.LogicAmount == 0)
                {
                    p.NoMsg = Converter.GetSmallInteger(1);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.NoRecord = Converter.GetSmallInteger(1);
                    p.NoMsg = 0;
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
                p.NoMsg = 0;
            }

            return p;

        }
        public static A2ZTRNLOGICDTO GetODLoanWithdrawal(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo, string TrnDate)
        {
            var prm = new object[6];

            prm[0] = BranchNo;
            prm[1] = MemType;
            prm[2] = MemberNo;
            prm[3] = AccType;
            prm[4] = AccountNo;
            prm[5] = TrnDate;
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateODInterest", prm, "A2ZCSCUBS"));
            if (result == 0)
            {


                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                    AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


                var p = new A2ZTRNLOGICDTO();
                if (dt.Rows.Count > 0)
                {
                    p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);
                    p.SancAmount = Converter.GetDecimal(dt.Rows[0]["AccLoanSancAmt"]);
                    p.BalAmount = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);

                    p.AccCurrIntAmt = Converter.GetDouble(Math.Abs(p.CalInterest));
                    p.LogicAmount = Converter.GetDouble(p.SancAmount + p.BalAmount);

                    if (p.LogicAmount > 0)
                    {
                        p.NoRecord = Converter.GetSmallInteger(1);
                        p.NoMsg = 0;
                        return p;
                    }
                    else
                    {
                        p.NoMsg = Converter.GetSmallInteger(1);
                        p.NoRecord = Converter.GetSmallInteger(1);
                        return p;
                    }
                }
                else
                {
                    p.NoRecord = 0;
                    p.NoMsg = 0;
                }

                return p;
            }
            else
            {
                return null;
            }
        }




        public static A2ZTRNLOGICDTO GetODInterestAmt(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo, string TrnDate)
        {

            var prm = new object[6];

            prm[0] = BranchNo;
            prm[1] = MemType;
            prm[2] = MemberNo;
            prm[3] = AccType;
            prm[4] = AccountNo;
            prm[5] = TrnDate;
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateODInterest", prm, "A2ZCSCUBS"));
            if (result == 0)
            {

                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");

                var p = new A2ZTRNLOGICDTO();
                if (dt.Rows.Count > 0)
                {
                    p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);
                    p.AccDueIntAmt = Converter.GetDouble(dt.Rows[0]["AccDueIntAmt"]);
                    p.AccHoldIntAmt = Converter.GetDouble(dt.Rows[0]["AccHoldIntAmt"]);

                    if (p.AccDueIntAmt < 0)
                    {
                        p.AccDueIntAmt = 0;
                    }

                    //if (p.CalInterest < 0)
                    //{
                    p.AccCurrIntAmt = Converter.GetDouble(Math.Abs(p.CalInterest + p.AccHoldIntAmt));
                    p.LogicAmount = Converter.GetDouble(Math.Abs(p.CalInterest) + p.AccDueIntAmt + p.AccHoldIntAmt);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                    //}
                    //else
                    //{
                    //    p.LogicAmount = 0;
                    //    p.NoRecord = Converter.GetSmallInteger(1);
                    //    return p;
                    //}
                }
                else
                {
                    p.NoRecord = 0;
                }

                return p;

            }
            else
            {
                return null;
            }


        }
        public static A2ZTRNLOGICDTO GetLoanReturnAmt(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {

                p.BalAmount = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.InstlAmt = Converter.GetDecimal(dt.Rows[0]["AccLoanInstlAmt"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);
                p.AccDuePrincAmt = Converter.GetDouble(dt.Rows[0]["AccDuePrincAmt"]);
                p.AccDueIntAmt = Converter.GetDouble(dt.Rows[0]["AccDueIntAmt"]);
                p.AccDuePenalAmt = Converter.GetDouble(dt.Rows[0]["AccDuePenalAmt"]);
                p.AccLastDuePrincAmt = Converter.GetDouble(dt.Rows[0]["AccLastDuePrincAmt"]);

                p.LogicAmount = p.AccDuePrincAmt;

                //p.LogicAmount = Converter.GetDouble(p.BalAmount + p.InstlAmt);

                //if (p.LogicAmount > 0)
                //{
                //    p.LogicAmount = Converter.GetDouble(Math.Abs(p.BalAmount));
                //}
                //else
                //{
                //    p.LogicAmount = Converter.GetDouble(p.InstlAmt);
                //}

                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }


            return p;

        }


        public static A2ZTRNLOGICDTO GetIntWithdrawal(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.AccPrincipal = Converter.GetDecimal(dt.Rows[0]["AccPrincipal"]);
                p.AccRenwlAmt = Converter.GetDecimal(dt.Rows[0]["AccRenwlAmt"]);
                p.BalAmount = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.AccLienAmt = Converter.GetDecimal(dt.Rows[0]["AccLienAmt"]);

                var rr = Convert.ToString(dt.Rows[0]["AccRenwlDate"]);

                if (rr != "")
                {
                    DataTable dt1 = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + AccType + "'", "A2ZCSCUBS");
                    if (dt.Rows.Count > 0)
                    {
                        p.IntWithdrDays = Converter.GetSmallInteger(dt1.Rows[0]["PrmIntWithdrDays"]);

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime tDate = Converter.GetDateTime(dto.ProcessDate);
                        DateTime fDate = Converter.GetDateTime(rr);
                        int Days = Convert.ToInt32(tDate.Subtract(fDate).TotalDays);

                        if (Days <= p.IntWithdrDays)
                        {
                            if (p.AccLienAmt > p.AccPrincipal)
                            {
                                p.LogicAmount = Converter.GetDouble(p.BalAmount - p.AccLienAmt);
                            }
                            else
                            {
                                p.LogicAmount = Converter.GetDouble(p.BalAmount - p.AccPrincipal);
                            }
                        }
                        else
                        {
                            p.NoMsg = 1;
                        }
                    }
                }

                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetBenefitWithdrawalA(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {


            var prm = new object[1];

            prm[0] = AccountNo;

            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSGetMonthlyBenefit", prm, "A2ZCSCUBS"));
            if (result == 0)
            {


                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                    AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


                var p = new A2ZTRNLOGICDTO();
                if (dt.Rows.Count > 0)
                {
                    p.AccFixedAmt = Converter.GetDecimal(dt.Rows[0]["AccFixedAmt"]);
                    p.AccFixedMthInt = Converter.GetDecimal(dt.Rows[0]["AccFixedMthInt"]);
                    p.BalAmount = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);
                    p.CalProvDate = Converter.GetDateTime(dt.Rows[0]["AccProvCalDate"]);
                    p.AdjBalAmount = Converter.GetDecimal(dt.Rows[0]["AccAdjProvBalance"]);
                    p.BenefitDate = Converter.GetDateTime(dt.Rows[0]["AccBenefitDate"]);

                    p.AccProvBalance = Converter.GetDecimal(dt.Rows[0]["AccProvBalance"]);


                    p.LogicAmount = Converter.GetDouble(p.BalAmount + p.AdjBalAmount);


                    p.NoMonths = Converter.GetDecimal((p.BalAmount + p.AdjBalAmount) / p.AccFixedMthInt);

                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.NoRecord = 0;
                }

                return p;
            }
            else
            {
                return null;
            }



        }

        public static A2ZTRNLOGICDTO GetInterestAmtFDR(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {

            var prm = new object[5];

            prm[0] = BranchNo;
            prm[1] = MemType;
            prm[2] = MemberNo;
            prm[3] = AccType;
            prm[4] = AccountNo;
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateEncashmentFDR", prm, "A2ZCSCUBS"));
            if (result == 0)
            {

                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");

                var p = new A2ZTRNLOGICDTO();
                if (dt.Rows.Count > 0)
                {
                    p.CalNofDays = Converter.GetSmallInteger(dt.Rows[0]["CalNofDays"]);
                    p.CalIntRate = Converter.GetDecimal(dt.Rows[0]["CalIntRate"]);
                    p.CalFDAmount = Converter.GetDouble(dt.Rows[0]["CalFDAmount"]);
                    p.CalOrgInterest = Converter.GetDouble(dt.Rows[0]["CalOrgInterest"]);
                    p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);
                    p.CalProvAdjCr = Converter.GetDouble(dt.Rows[0]["CalProvAdjCr"]);
                    p.CalProvAdjDr = Converter.GetDouble(dt.Rows[0]["CalProvAdjDr"]);
                    p.CalFDate = Converter.GetDateTime(dt.Rows[0]["CalFDate"]);
                    p.CalPeriod = Converter.GetSmallInteger(dt.Rows[0]["CalPeriod"]);


                    if (p.CalInterest > 0)
                    {
                        p.LogicAmount = Converter.GetDouble(p.CalInterest);
                        p.NoRecord = Converter.GetSmallInteger(1);
                        return p;
                    }
                    else
                    {
                        p.LogicAmount = 0;
                        p.NoRecord = Converter.GetSmallInteger(1);
                        return p;
                    }
                }
                else
                {
                    p.NoRecord = 0;
                }

                return p;

            }
            else
            {
                return null;
            }


        }

        public static A2ZTRNLOGICDTO GetInterestAdjFDR(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);

                if (p.CalInterest < 0)
                {
                    p.LogicAmount = Converter.GetDouble(p.CalInterest);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.LogicAmount = 0;
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetEncashmentIntFDR(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);

                if (p.CalInterest > 0)
                {
                    p.LogicAmount = Converter.GetDouble(p.CalInterest);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.LogicAmount = 0;
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }
        public static A2ZTRNLOGICDTO GetNetEncashmentPrincFDR(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalEncashment = Converter.GetDecimal(dt.Rows[0]["CalEncashment"]);


                p.LogicAmount = Converter.GetDouble(p.CalEncashment);

                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetInterestAmt6YR(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {

            var prm = new object[5];

            prm[0] = BranchNo;
            prm[1] = MemType;
            prm[2] = MemberNo;
            prm[3] = AccType;
            prm[4] = AccountNo;
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateEncashment6YR", prm, "A2ZCSCUBS"));
            if (result == 0)
            {

                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");

                var p = new A2ZTRNLOGICDTO();
                if (dt.Rows.Count > 0)
                {

                    p.CalNofDays = Converter.GetSmallInteger(dt.Rows[0]["CalNofDays"]);
                    p.CalFDate = Converter.GetDateTime(dt.Rows[0]["CalFDate"]);
                    p.CalFDAmount = Converter.GetDouble(dt.Rows[0]["CalFDAmount"]);
                    p.CalIntRate = Converter.GetDecimal(dt.Rows[0]["CalIntRate"]);
                    p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);
                    p.CalProvAdjCr = Converter.GetDouble(dt.Rows[0]["CalProvAdjCr"]);
                    p.CalProvAdjDr = Converter.GetDouble(dt.Rows[0]["CalProvAdjDr"]);

                    if (p.CalInterest > 0)
                    {
                        p.LogicAmount = Converter.GetDouble(p.CalInterest);
                        p.NoRecord = Converter.GetSmallInteger(1);
                        return p;
                    }
                    else
                    {
                        p.LogicAmount = 0;
                        p.NoRecord = Converter.GetSmallInteger(1);
                        return p;
                    }
                }
                else
                {
                    p.NoRecord = 0;
                }

                return p;

            }
            else
            {
                return null;
            }


        }

        public static A2ZTRNLOGICDTO GetInterestAdj6YR(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);

                if (p.CalInterest < 0)
                {
                    p.LogicAmount = Converter.GetDouble(p.CalInterest);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.LogicAmount = 0;
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetNetEncashmentInt6YR(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);

                if (p.CalInterest > 0)
                {
                    p.LogicAmount = Converter.GetDouble(p.CalInterest);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.LogicAmount = 0;
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetNetEncashmentPrinc6YR(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalEncashment = Converter.GetDecimal(dt.Rows[0]["CalEncashment"]);


                p.LogicAmount = Converter.GetDouble(p.CalEncashment);

                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetBenefitAmtMSplus(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            var p = new A2ZTRNLOGICDTO();


            var prm = new object[5];

            prm[0] = BranchNo;
            prm[1] = MemType;
            prm[2] = MemberNo;
            prm[3] = AccType;
            prm[4] = AccountNo;
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateEncashmentMSplus", prm, "A2ZCSCUBS"));
            if (result == 0)
            {

                DataTable dt0 = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCTYPE WHERE AccTypeCode = '" + AccType + "'", "A2ZCSCUBS");
                if (dt0.Rows.Count > 0)
                {
                    p.ProvSw = Converter.GetInteger(dt0.Rows[0]["AccProvisionMode"]);

                    DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                    AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


                    if (dt.Rows.Count > 0)
                    {
                        p.BalInterest = Converter.GetDouble(dt.Rows[0]["AccProvBalance"]);
                        p.CalOrgInterest = Converter.GetDouble(dt.Rows[0]["CalOrgInterest"]);
                        p.CalPaidInterest = Converter.GetDouble(dt.Rows[0]["AccTotIntWdrawn"]);
                        p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);

                        p.CalEncashment = Converter.GetDecimal(dt.Rows[0]["CalEncashment"]);

                        if (p.ProvSw == 1)
                        {
                            p.RestInterest = Converter.GetDouble(p.BalInterest - p.CalInterest);
                        }
                        else
                        {
                            p.RestInterest = p.CalInterest;
                        }


                        if (p.CalInterest > 0 && p.RestInterest < 0)
                        {
                            p.LogicAmount = Converter.GetDouble(p.RestInterest);
                            p.NoRecord = Converter.GetSmallInteger(1);
                            return p;
                        }
                        else
                        {
                            p.LogicAmount = 0;
                            p.NoRecord = Converter.GetSmallInteger(1);
                            return p;
                        }
                    }
                    else
                    {
                        p.NoRecord = 0;
                    }
                }

                    return p;
               
            
            }
            else
            {
                return null;
            }


        }

        public static A2ZTRNLOGICDTO GetBenefitAdjMSplus(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);

                if (p.CalInterest < 0)
                {
                    p.LogicAmount = Converter.GetDouble(p.CalInterest);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.LogicAmount = 0;
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }


        public static A2ZTRNLOGICDTO GetNetEncashmentBenefitMSplus(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
            AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");

            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.BalInterest = Converter.GetDouble(dt.Rows[0]["AccProvBalance"]);
                p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);


                if (p.CalInterest > 0)
                {
                    p.LogicAmount = Converter.GetDouble(p.CalInterest);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.LogicAmount = 0;
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetNetEncashmentPrincMSplus(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalEncashment = Converter.GetDecimal(dt.Rows[0]["CalEncashment"]);


                p.LogicAmount = Converter.GetDouble(p.CalEncashment);

                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetNetInterestReceived(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {


            
                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                     AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


                var p = new A2ZTRNLOGICDTO();
                if (dt.Rows.Count > 0)
                {


                    p.BalAmount = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                    p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);

                    p.LogicAmount = Converter.GetDouble(p.BalAmount * p.InterestRate) / 1200;

                    DataTable dt1 = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + AccType + "'", "A2ZCSCUBS");
                    if (dt1.Rows.Count > 0)
                    {
                        p.RoundFlag = Converter.GetSmallInteger(dt1.Rows[0]["PrmRoundFlag"]);

                        if (p.RoundFlag == 1)
                        {
                            p.LogicAmount = Math.Round(p.LogicAmount);
                        }

                        if (p.RoundFlag == 2)
                        {
                            p.LogicAmount = Math.Ceiling(p.LogicAmount);
                        }
                        if (p.RoundFlag == 3)
                        {
                            p.LogicAmount = (p.LogicAmount);
                        }

                        double Balance = Converter.GetDouble(p.BalAmount);
                        double Interest = Converter.GetDouble(p.LogicAmount);

                        // p.LogicAmount = Converter.GetDouble(Balance + Interest);
                        p.LogicAmount = Converter.GetDouble(Interest);
                    }

                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;

                }
                else
                {
                    p.NoRecord = 0;
                }

                return p;
            

        }


        public static A2ZTRNLOGICDTO GetNetLoanAmtReceived(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                 AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.BalAmount = Converter.GetDecimal(dt.Rows[0]["AccBalance"]);
                p.InterestRate = Converter.GetDecimal(dt.Rows[0]["AccIntRate"]);

                p.LogicAmount = Converter.GetDouble(p.BalAmount * p.InterestRate) / 1200;

                DataTable dt1 = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + AccType + "'", "A2ZCSCUBS");
                if (dt1.Rows.Count > 0)
                {
                    p.RoundFlag = Converter.GetSmallInteger(dt1.Rows[0]["PrmRoundFlag"]);

                    if (p.RoundFlag == 1)
                    {
                        p.LogicAmount = Math.Round(p.LogicAmount);
                    }

                    if (p.RoundFlag == 2)
                    {
                        p.LogicAmount = Math.Ceiling(p.LogicAmount);
                    }
                    if (p.RoundFlag == 3)
                    {
                        p.LogicAmount = (p.LogicAmount);
                    }

                    double Balance = Converter.GetDouble(p.BalAmount);
                    double Interest = Converter.GetDouble(p.LogicAmount);

                    // p.LogicAmount = Converter.GetDouble(Balance + Interest);
                    p.LogicAmount = Converter.GetDouble(Balance);
                }

                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetInterestAdjCrPension(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {
            var prm = new object[5];

            prm[0] = BranchNo;
            prm[1] = MemType;
            prm[2] = MemberNo;
            prm[3] = AccType;
            prm[4] = AccountNo;
            int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateEncashmentCPS", prm, "A2ZCSCUBS"));
            if (result == 0)
            {


                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                    AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


                var p = new A2ZTRNLOGICDTO();
                if (dt.Rows.Count > 0)
                {
                    p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);
                    p.CalPeriod = Converter.GetSmallInteger(dt.Rows[0]["CalPeriod"]);

                    if (p.CalInterest > 0)
                    {
                        p.LogicAmount = Converter.GetDouble(p.CalInterest);
                        p.NoRecord = Converter.GetSmallInteger(1);
                        return p;
                    }
                    else
                    {
                        p.LogicAmount = 0;
                        p.NoRecord = Converter.GetSmallInteger(1);
                        return p;
                    }
                }
                else
                {
                    p.NoRecord = 0;
                }

                return p;
            }
            else
            {
                return null;
            }

        }

        public static A2ZTRNLOGICDTO GetInterestAdjDrPension(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {

            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {
                p.CalInterest = Converter.GetDouble(dt.Rows[0]["CalInterest"]);

                if (p.CalInterest < 0)
                {
                    p.LogicAmount = Converter.GetDouble(p.CalInterest);
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
                else
                {
                    p.LogicAmount = 0;
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;
                }
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;


        }

        public static A2ZTRNLOGICDTO GetNetEncashmentPension(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo, DateTime TrnDate)
        {
            var p = new A2ZTRNLOGICDTO();

            A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
            DateTime dt1 = Converter.GetDateTime(dto.ProcessDate);
            string date = dt1.ToString("dd/MM/yyyy");

            A2ZPENSIONDEFAULTERDTO getDTO = (A2ZPENSIONDEFAULTERDTO.GetPensionDepInformation(BranchNo, TrnDate, MemType, MemberNo, AccType, AccountNo));
            if (getDTO.MemType > 0)
            {
                p.UptoMonth = Converter.GetString(getDTO.UptoMonth);
                p.UptoYear = Converter.GetInteger(getDTO.UptoYear);

                p.UptoPaidDate = Converter.GetDateTime(getDTO.UptoPaidDate);
                p.UptoPaidNo = Converter.GetDecimal(getDTO.UptoPaidNo);
                p.DueDepositAmt = Converter.GetDecimal(getDTO.DueDepositAmt);

               
            }


            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");


            if (dt.Rows.Count > 0)
            {
                p.CalEncashment = Converter.GetDecimal(dt.Rows[0]["CalEncashment"]);
                p.LogicAmount = Converter.GetDouble(p.CalEncashment);

                p.AccPeriod = Converter.GetInteger(dt.Rows[0]["AccPeriod"]);
                p.MthDeposit = Converter.GetDouble(dt.Rows[0]["AccMonthlyDeposit"]);
                p.TotalDepAmt = Converter.GetDouble(dt.Rows[0]["AccTotalDep"]);
                p.OpenDate = Converter.GetDateTime(dt.Rows[0]["AccOpenDate"]);

                //p.NoMonths = Converter.GetDecimal(p.TotalDepAmt / p.MthDeposit);

                //if (p.NoMonths == 0)
                //{
                //    p.UptoDate = "";
                //}
                //else
                //{
                //    p.NoMonths = p.NoMonths - 1;
                //    if (p.NoMonths < 1)
                //    {
                //        p.NoMonths = 0;
                //    }
                //    DateTime Depdate = new DateTime();
                //    Depdate = p.OpenDate.AddMonths(Converter.GetSmallInteger(p.NoMonths));
                //    p.UptoDate = String.Format("{0:Y}", Depdate);
                //}

                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }

            return p;

        }

        public static A2ZTRNLOGICDTO GetClosingFees(int AccType)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + AccType + "'", "A2ZCSCUBS");


            var p = new A2ZTRNLOGICDTO();
            if (dt.Rows.Count > 0)
            {

                p.LogicAmount = Converter.GetDouble(dt.Rows[0]["PrmAccClosingFees"]);
                p.NoRecord = Converter.GetSmallInteger(1);
                return p;
            }
            else
            {
                p.NoRecord = 0;
            }


            return p;

        }


        public static A2ZTRNLOGICDTO GetSharePenalAmt(int BranchNo, Int16 MemType, int MemberNo, int AccType, Int64 AccountNo)
        {

            //var prm = new object[5];

            //prm[0] = BranchNo;
            //prm[1] = MemType;
            //prm[2] = MemberNo;
            //prm[3] = AccType;
            //prm[4] = AccountNo;
            //int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_CSCalculateSharePenalAmt", prm, "A2ZCSCUBS"));
            //if (result == 0)
            //{

                DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZACCOUNT WHERE BranchNo = '" + BranchNo + "' and AccType = '" + AccType + "' and AccNo = '" +
                AccountNo + "' and MemType='" + MemType + "' and MemNo='" + MemberNo + "'", "A2ZCSCUBS");

                var p = new A2ZTRNLOGICDTO();
                if (dt.Rows.Count > 0)
                {
                    p.CalPenalAmt = Converter.GetDouble(dt.Rows[0]["CalPenalAmt"]);

                    p.LogicAmount = Converter.GetDouble(Math.Abs(p.CalPenalAmt));
                    p.NoRecord = Converter.GetSmallInteger(1);
                    return p;

                }
                else
                {
                    p.NoRecord = 0;
                }

                return p;

            //}
            ////else
            ////{
            ////    return null;
            ////}


        }

    }
}

