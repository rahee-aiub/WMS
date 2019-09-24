﻿using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZLOANHSTDTO
    {
        #region Propertise
        public int AccType { set; get; }
        public Int64 AccNo { set; get; }
        public Int16 CuType { set; get; }
        public int CuNo { set; get; }
        public int MemberNo { set; get; }
        public DateTime SanctionDate { set; get; }
        public decimal NewSanctionAmount { set; get; }
        public decimal NewIntRate { set; get; }
        public Int16 ApproveBy { set; get; }
        public String Note { set; get; }
        #endregion


        public static int InsertInformation(A2ZLOANHSTDTO dto)
        {
            int rowEffect = 0;
            string strQuery = @"INSERT INTO A2ZLOANHST( AccType, AccNo, CuType, CuNo, MemNo, SanctionDate, NewSanctionAmt, NewIntRate, ApproveBy, Note)values('" + dto.AccType + "','" + dto.AccNo + "','" + dto.CuType + "','" + dto.CuNo + "','" + dto.MemberNo + "','" + dto.SanctionDate + "','" + dto.NewSanctionAmount + "','" + dto.NewIntRate + "','" + dto.ApproveBy + "','" + dto.Note + "')";
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
