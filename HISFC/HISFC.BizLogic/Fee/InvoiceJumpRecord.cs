using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.Fee
{
    public  class InvoiceJumpRecord:FS.FrameWork.Management.Database
    {
        #region  ����
        /// <summary>
        ///  �����
        /// </summary>
        /// <param name="invoiceRecord"></param>
        /// <returns></returns>
        public int InsertTable(FS.HISFC.Models.Fee.InvoiceJumpRecord invoiceRecord)
        {
            string[] args = this.GetArgs(invoiceRecord);

            if (args == null)
            {
                return -1;
            }

            string StrSql = string.Empty;

            int returnValue = this.Sql.GetCommonSql("Fee.JumpRecord.Insert", ref StrSql);

            if (returnValue < 0)
            {
                this.Err = "û���ҵ�����ΪFee.JumpRecord.Insert��SQL���";
                return -1;
            }

            StrSql = string.Format(StrSql, args);

            return this.ExecNoQuery(StrSql); ;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="acceptCode"></param>
        /// <param name="invoiceTypeID"></param>
        /// <returns></returns>
        public string GetMaxHappenNO(string acceptCode, string invoiceTypeID)
        {
            string StrSql = string.Empty;

            int returnValue = this.Sql.GetCommonSql("Fee.JumpRecord.GetMaxHappenNO", ref StrSql);

            if (returnValue < 0)
            {
                this.Err = "û���ҵ�����ΪFee.JumpRecord.GetMaxHappenNO��SQL���";
                return "-1";
            }

            StrSql = string.Format(StrSql, acceptCode,invoiceTypeID);

            return  this.ExecSqlReturnOne(StrSql);
             
        }
        #endregion

        #region ����
        
        #endregion

        #region ��ѯ


        
        #endregion

        #region ˽�з���

        /// <summary>
        /// ʵ�帳ֵ
        /// </summary>
        /// <param name="invoiceRecord"></param>
        /// <returns></returns>
        private string[] GetArgs(FS.HISFC.Models.Fee.InvoiceJumpRecord invoiceRecord)
        {
            string[] args;
            try
            {
                args = new string[] {
                    invoiceRecord.Invoice.AcceptOper.ID,
                    invoiceRecord.HappenNO.ToString(),
                    invoiceRecord.Invoice.Type.ID,
                    invoiceRecord.Invoice.Type.Name,
                    invoiceRecord.Invoice.BeginNO,
                    invoiceRecord.Invoice.EndNO,
                    invoiceRecord.OldUsedNO,
                    invoiceRecord.NewUsedNO,
                    invoiceRecord.Oper.ID,
                    invoiceRecord.Oper.OperTime.ToString()
                    };
            }
            catch (Exception ex)
            {
                this.Err = "ʵ�帳ֵ����" + ex.Message;
                return null;
            }
            return args;

        }
        #endregion
    }
}
