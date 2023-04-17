using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.BlackList;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// ����������ҵ���
    /// </summary>
    public class PBlackList :FS.FrameWork.Management.Database
    {

        #region ˽��
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="SqlIndex">SQL�������</param>
        /// <param name="args">��ʽ���ַ�������</param>
        /// <returns></returns>
        private int UpdateSigleTable(string SqlIndex,params string[] args)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql(SqlIndex, ref sqlStr) == -1)
            {
                this.Err = "����SQL���" + SqlIndex + "ʧ�ܣ�";
                return -1;
            }
            int resultValue = this.ExecNoQuery(sqlStr, args);
            return resultValue;
        }
        #endregion

        #region ����
        /// <summary>
        /// ���ݲ����Ų��Һ���������
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="pbl"></param>
        /// <returns></returns>
        public int GetBlackList(string cardNO, ref PatientBlackList pbl)
        {
            int resultValue = this.GetBlackListMain(cardNO, ref pbl);
            if (resultValue == -1)
            {
                return -1;
            }
            List<PatientBlackListDetail> list = new List<PatientBlackListDetail>();
            resultValue = this.GetBlackListDetail(cardNO, ref list);
            if (resultValue == -1)
            {
                return -1;
            }
            pbl.BlackListDetail = list;
            return 1;
        }

        /// <summary>
        /// ���ݲ����Ų��Һ�������������
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="pbl">������ʵ��</param>
        /// <returns></returns>
        public int GetBlackListMain(string cardNO, ref PatientBlackList pbl)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.BlackList.SelectBlackList", ref sqlStr) == -1)
            {
                this.Err = "����SQL���Fee.BlackList.SelectBlackListʧ�ܣ�";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, cardNO);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "���һ��ߺ���������ʧ�ܣ�";
                    return -1;
                }
               
                pbl = new PatientBlackList();
                while (this.Reader.Read())
                {
                    
                    pbl.CardNO = this.Reader[0].ToString();
                    pbl.BlackListValid = NConvert.ToBoolean(this.Reader[1].ToString());
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ���ݲ����Ų��Һ�������ϸ����
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="list">��������ϸ����</param>
        /// <returns></returns>
        public int GetBlackListDetail(string cardNO,ref List<PatientBlackListDetail> list)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.BlackList.SelectBlackListDetail", ref sqlStr) == -1)
            {
                this.Err = "����SQL���Fee.BlackList.SelectBlackListʧ�ܣ�";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, cardNO);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "���һ��ߺ�������ϸ����ʧ�ܣ�";
                    return -1;
                }

                list = new List<PatientBlackListDetail>();
                PatientBlackListDetail obj = null;
                while (this.Reader.Read())
                {
                    obj = new PatientBlackListDetail();
                    obj.SeqNO = this.Reader[0].ToString();
                    obj.BlackListValid = NConvert.ToBoolean(this.Reader[1]);
                    obj.Memo = this.Reader[2].ToString();
                    obj.Oper.Name = this.Reader[3].ToString();
                    obj.Oper.OperTime = NConvert.ToDateTime(this.Reader[4]);
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ���»����Ƿ��ں�������
        /// </summary>
        ///<param name="cardNO">������</param>
        /// <param name="blackListValid">�Ƿ��ں������� "0"���� "1"��</param>
        /// <returns></returns>
        public int UpdateBlackList(PatientBlackList pbl)
        {
            //{E8D9B53F-9C12-4f6e-8F7C-A94FB6B9D173}
            string[] args = new string[] { pbl.CardNO, NConvert.ToInt32(pbl.BlackListValid).ToString(), NConvert.ToInt32(!pbl.BlackListValid).ToString() };
            return this.UpdateSigleTable("Fee.BlackList.UpdateBlackList", args);
        }

        /// <summary>
        /// �����߲����������
        /// </summary>
        ///<param name="cardNO">������</param>
        /// <param name="blackListValid">�Ƿ��ں������� "0"���� "1"��</param>
        /// <returns></returns>
        public int InsertBlackList(PatientBlackList pbl)
        {
            string[] args = new string[] { pbl.CardNO, NConvert.ToInt32(pbl.BlackListValid).ToString() };
            return this.UpdateSigleTable("Fee.BlackList.InsertBlackList", args);
        }

        /// <summary>
        /// �����������������
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <param name="blackListValid">�Ƿ��ں������� "0"���� "1"��</param>
        /// <returns></returns>
        public int SaveBlackList(PatientBlackList pbl)
        {
            int resultValue = InsertBlackList(pbl);
            if (resultValue == -1)
            {
                resultValue = UpdateBlackList(pbl);
            }
            return resultValue;
        }

        /// <summary>
        /// �����������ϸ
        /// </summary>
        /// <param name="pbl">������ʵ��</param>
        /// <returns></returns>
        public int AddBlackListDetail(PatientBlackList pbl)
        {
            if (pbl.BlackListDetail == null || pbl.BlackListDetail.Count == 0)
            {
                this.Err = "�����������ϸ����ʧ�ܣ�";
                return -1;
            }
            PatientBlackListDetail pbld = pbl.BlackListDetail[0];
            
            string[] args = new string[] 
                            {
                                pbld.SeqNO,
                                pbl.CardNO,
                                NConvert.ToInt32(pbld.BlackListValid).ToString(),
                                pbld.Memo,
                                pbld.Oper.ID,
                                pbld.Oper.OperTime.ToString()
                            };
            return UpdateSigleTable("Fee.BlackList.InsertBlackListDetail", args);
        }

        /// <summary>
        /// ��ú���������
        /// </summary>
        /// <returns></returns>
        public string GetBlackListSeqNo()
        {
            return this.GetSequence("Fee.BlackList.SelectBlackListSeqNO");
        }
        #endregion
    }
}
