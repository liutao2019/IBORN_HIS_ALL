using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;
using System.Data;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: ���˲�����Ϣ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-26]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class BaseManage : FS.FrameWork.Management.Database
    {
        #region ���ò�������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="patient">�걾�ⲡ��ʵ��</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SpecBase specBase)
        {            
            string[] str = new string[]
						{
							specBase.BaseID.ToString(),
                            specBase.InBaseTime.ToString(),
                            specBase.SpecSource.SpecId.ToString(),
                            specBase.HCV_AB,
                            specBase.HbSAG,
                            specBase.Hiv_AB,
                            specBase.RHBlood,
                            specBase.X_Times,
                            specBase.MR_Times,
                            specBase.DSA_Times,
                            specBase.PET_Times,
                            specBase.ECT_Times,
                            specBase.OutDiaICD,
                            specBase.OutDiaName,
                            specBase.Main_DiagState,
                            specBase.Diagnose_Oper_Flag,
                            specBase.Is30Disease,
                            specBase.InDiaICD,
                            specBase.InDiaName,
                            specBase.CliDiagICD,
                            specBase.CliDiagName,
                            specBase.MainDiaICD,
                            specBase.MainDiaName,
                            specBase.MainDiaICD1,
                            specBase.MainDiaName1,
                            specBase.MainDiaICD2,
                            specBase.MainDiagName2,
                            specBase.ModICD,
                            specBase.ModName,
                            specBase.Comment
                        };
            return str;
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("Speciment.BizLogic.BaseManage.GetNextSequence");
            //
            // �������NULL�����ȡʧ��
            //
            if (sequence == null)
            {
                this.SetError("", "��ȡSequenceʧ��");
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion

        #region ���ô�����Ϣ
        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="errorCode">������뷢������</param>
        /// <param name="errorText">������Ϣ</param>
        private void SetError(string errorCode, string errorText)
        {
            this.ErrCode = errorCode;
            this.Err = errorText + "[" + this.Err + "]"; // + "��ShelfSpecManage.cs�ĵ�" + argErrorCode + "�д���";
            this.WriteErr();
        }
        #endregion

        #region ���²���
        /// <summary>
        /// ���²���
        /// </summary>
        /// <param name="sqlIndex">sql����</param>
        /// <param name="args"></param>
        /// <returns></returns>
        private int UpdateBase(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }

        private SpecBase SetBaseInfo()
        {
            try
            {
                SpecBase specBase = new SpecBase();
                if (!Reader.IsDBNull(0)) specBase.BaseID = Convert.ToInt32(this.Reader["BASEID"].ToString());
                if (!Reader.IsDBNull(1)) specBase.InBaseTime = Convert.ToDateTime(this.Reader["INBASETIME"].ToString());
                if (!Reader.IsDBNull(2)) specBase.SpecSource.SpecId = Convert.ToInt32(this.Reader["SPECID"].ToString());
                if (!Reader.IsDBNull(3)) specBase.HCV_AB = this.Reader["HCV_AB"].ToString();
                if (!Reader.IsDBNull(4)) specBase.HbSAG = this.Reader["HBSAG"].ToString();
                if (!Reader.IsDBNull(5)) specBase.Hiv_AB = this.Reader["HIV_AB"].ToString();
                if (!Reader.IsDBNull(6)) specBase.RHBlood = this.Reader["RH_BLOOD"].ToString();
                if (!Reader.IsDBNull(7)) specBase.X_Times = this.Reader["X_TIMES"].ToString();
                if (!Reader.IsDBNull(8)) specBase.MR_Times = this.Reader["MR_TIMES"].ToString();
                if (!Reader.IsDBNull(9)) specBase.DSA_Times = this.Reader["DSA_TIMES"].ToString();
                if (!Reader.IsDBNull(10)) specBase.PET_Times = this.Reader["PET_TIMES"].ToString();
                if (!Reader.IsDBNull(11)) specBase.ECT_Times = this.Reader["ECT_TIMES"].ToString();
                if (!Reader.IsDBNull(12)) specBase.OutDiaICD = this.Reader["M_DIAGICD"].ToString();
                if (!Reader.IsDBNull(13)) specBase.OutDiaName = this.Reader["M_DIAGICDNAME"].ToString();
                if (!Reader.IsDBNull(14)) specBase.Main_DiagState = this.Reader["MAIN_DIAGSTATE"].ToString();
                if (!Reader.IsDBNull(15)) specBase.Diagnose_Oper_Flag = this.Reader["DIAG_OPER_FLAG"].ToString();
                if (!Reader.IsDBNull(16)) specBase.Is30Disease = this.Reader["IS30DISEASE"].ToString();
                if (!Reader.IsDBNull(17)) specBase.InDiaICD = this.Reader["INHOS_DIACODE"].ToString();
                if (!Reader.IsDBNull(18)) specBase.InDiaName = this.Reader["INHOS_DIANAME"].ToString();
                if (!Reader.IsDBNull(19)) specBase.CliDiagICD = this.Reader["CILINIC_DIACODE"].ToString();
                if (!Reader.IsDBNull(20)) specBase.CliDiagName = this.Reader["CLINIC_DIANAME"].ToString();
                if (!Reader.IsDBNull(21)) specBase.MainDiaICD = this.Reader["MAIN_DIACODE"].ToString();
                if (!Reader.IsDBNull(22)) specBase.MainDiaName = this.Reader["MAIN_DIANAME"].ToString();
                if (!Reader.IsDBNull(23)) specBase.MainDiaICD1 = this.Reader["MAIN_DIACODE1"].ToString();
                if (!Reader.IsDBNull(24)) specBase.MainDiaName1 = this.Reader["MAIN_DIANAME1"].ToString();
                if (!Reader.IsDBNull(25)) specBase.MainDiaICD2 = this.Reader["MAIN_DIACODE2"].ToString();
                if (!Reader.IsDBNull(26)) specBase.MainDiagName2 = this.Reader["MAIN_DIANAME2"].ToString();
                if (!Reader.IsDBNull(27)) specBase.ModICD = this.Reader["MOD_ICD"].ToString();
                if (!Reader.IsDBNull(28)) specBase.ModName = this.Reader["MOD_NAME"].ToString();
                if (!Reader.IsDBNull(29)) specBase.Comment = this.Reader["MARK"].ToString();
                return specBase;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return null;
            }
 
        }
        #endregion

        #region �걾��������
        /// <summary>
        /// ���没����Ϣ
        /// </summary>
        /// <param name="specBase"></param>
        /// <returns></returns>
        public int InsertBase(SpecBase specBase)
        {
            try
            {
                return UpdateBase("Speciment.BizLogic.BaseManage.Insert", GetParam(specBase));

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }
        #endregion

        #region ���²��������Ϣ
        /// <summary>
        /// ���²�����Ϣ
        /// </summary>
        /// <param name="specBase"></param>
        /// <returns></returns>
        public int UpdateBase(SpecBase specBase)
        {
            try
            {
                return UpdateBase("Speciment.BizLogic.BaseManage.Update", GetParam(specBase));

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// ����sql����ѯ���
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList GetBaseInfo(string sql)
        {
            if (this.ExecQuery(sql) == -1)
                return null;
            ArrayList arrBaseInfo = new ArrayList();
            while (this.Reader.Read())
            {
                SpecBase baseInfo = SetBaseInfo();
                arrBaseInfo.Add(baseInfo);
            }
            Reader.Close();
            return arrBaseInfo;
        }

        /// <summary>
        /// ��ȡ�걾����û��¼����ϵı걾
        /// </summary>
        /// <returns></returns>
        public DataSet GetNotInBaseInfo()
        {
            DataSet ds = new DataSet();
            this.ExecQuery("Speciment.BizLogic.BaseManage.GetDiagnoseFromBase", ref ds, new string[] { });
            return ds;
        }

        /// <summary>
        /// ����ʱ���ȡ�걾����û��¼����ϵı걾
        /// </summary>
        /// <param name="start">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <returns></returns>
        public DataSet GetNotInBaseByTime(string start, string end)
        {
            DataSet ds = new DataSet();
            this.ExecQuery("Speciment.BizLogic.BaseManage.GetDiagnoseFromBase.ByTimeStamp", ref ds, new string[] { start, end });
            return ds;
        }

        /// <summary>
        /// ����ʱ���ȡ�걾����û��¼����ϵı걾
        /// </summary>
        /// <param name="inBase">�Ƿ�¼�������Ϣ</param>
        /// <param name="strat"></param>
        /// <param name="end"></param>
        /// <param name="ds"></param>
        public void GetNotInBaseInfo(string inBase, string strat ,string end, ref DataSet ds)
        {
            this.ExecQuery("Speciment.BizLogic.BaseManage.GetNoDiagnose", ref ds, new string[] { inBase, strat, end }); 
        }

        /// <summary>
        /// ���ݲ����Ż�ȡ�����Ϣ
        /// </summary>
        /// <param name="patientNo">סԺ��</param>
        /// <param name="ds"></param>
        public DataTable GetDiagnoseFromDiagnose(string patientNo)
        {
            DataSet ds = new DataSet();
            this.ExecQuery("Speciment.BizLogic.BaseManage.GetDiagnoseFromDiagnose", ref ds, new string[] { patientNo });
            if (ds == null || ds.Tables.Count <= 0)
            {
                return new DataTable();
            }
            else
            {
                return ds.Tables[0];
            }
        }
    }
}
