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
    /// [��������: ���������Ϣ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-26]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class DiagnoseManage : FS.FrameWork.Management.Database
    {
        #region ���ò�������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="diagNose">�걾�����ʵ��</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SpecDiagnose diagNose)
        {
            string[] str = new string[]
						{
							diagNose.BaseID.ToString(),
                            diagNose.InBaseTime.ToString(),
                            diagNose.SpecSource.SpecId.ToString(),
                            diagNose.HCV_AB,
                            diagNose.HbSAG,
                            diagNose.Hiv_AB,
                            diagNose.RHBlood,
                            diagNose.X_Times,
                            diagNose.MR_Times,
                            diagNose.DSA_Times,
                            diagNose.PET_Times,
                            diagNose.ECT_Times,                           
                            diagNose.Main_DiagState,
                            diagNose.Diagnose_Oper_Flag,
                            diagNose.Is30Disease,
                            diagNose.Diag.Icd,
                            diagNose.Diag.IcdName,
                            diagNose.Diag.Mod,
                            diagNose.Diag.ModName,
                            diagNose.Diag.P_Code,
                            diagNose.Diag.T_Code,
                            diagNose.Diag.N_Code,
                            diagNose.Diag.M_Code,
                            diagNose.Diag1.Icd,
                            diagNose.Diag1.IcdName,
                            diagNose.Diag1.Mod,
                            diagNose.Diag1.ModName,
                            diagNose.Diag1.P_Code,
                            diagNose.Diag1.T_Code,
                            diagNose.Diag1.N_Code,
                            diagNose.Diag1.M_Code,
                            diagNose.Diag2.Icd,
                            diagNose.Diag2.IcdName,
                            diagNose.Diag2.Mod,
                            diagNose.Diag2.ModName,
                            diagNose.Diag2.P_Code,
                            diagNose.Diag2.T_Code,
                            diagNose.Diag2.N_Code,
                            diagNose.Diag2.M_Code,    
                            diagNose.DiagRemark,
                            diagNose.Comment,
                            diagNose.OperId,
                            diagNose.OperName,
                            diagNose.Ext1,
                            diagNose.Ext2,
                            diagNose.Ext3
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
            sequence = this.GetSequence("Speciment.BizLogic.PatientManage.GetNextSequence");
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

        #region �������
        /// <summary>
        /// ���²���
        /// </summary>
        /// <param name="sqlIndex">sql����</param>
        /// <param name="args"></param>
        /// <returns></returns>
        private int UpdateDiagnose(string sqlIndex, params string[] args)
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

        private SpecDiagnose SetDiagnoseInfo()
        {
            try
            {
                SpecDiagnose dianose = new SpecDiagnose();
                if (!Reader.IsDBNull(0)) dianose.BaseID = Convert.ToInt32(this.Reader["BASEID"].ToString());
                if (!Reader.IsDBNull(1)) dianose.InBaseTime = Convert.ToDateTime(this.Reader["INBASETIME"].ToString());
                if (!Reader.IsDBNull(2)) dianose.SpecSource.SpecId = Convert.ToInt32(this.Reader["SPECID"].ToString());
                if (!Reader.IsDBNull(3)) dianose.HCV_AB = this.Reader["HCV_AB"].ToString();
                if (!Reader.IsDBNull(4)) dianose.HbSAG = this.Reader["HBSAG"].ToString();
                if (!Reader.IsDBNull(5)) dianose.Hiv_AB = this.Reader["HIV_AB"].ToString();
                if (!Reader.IsDBNull(6)) dianose.RHBlood = this.Reader["RH_BLOOD"].ToString();
                if (!Reader.IsDBNull(7)) dianose.X_Times = this.Reader["X_TIMES"].ToString();
                if (!Reader.IsDBNull(8)) dianose.MR_Times = this.Reader["MR_TIMES"].ToString();
                if (!Reader.IsDBNull(9)) dianose.DSA_Times = this.Reader["DSA_TIMES"].ToString();
                if (!Reader.IsDBNull(10)) dianose.PET_Times = this.Reader["PET_TIMES"].ToString();
                if (!Reader.IsDBNull(11)) dianose.ECT_Times = this.Reader["ECT_TIMES"].ToString();               
                if (!Reader.IsDBNull(12)) dianose.Main_DiagState = this.Reader["MAIN_DIAGSTATE"].ToString();
                if (!Reader.IsDBNull(13)) dianose.Diagnose_Oper_Flag = this.Reader["DIAG_OPER_FLAG"].ToString();
                if (!Reader.IsDBNull(14)) dianose.Is30Disease = this.Reader["IS30DISEASE"].ToString();

                if (!Reader.IsDBNull(15)) dianose.Diag.Icd = this.Reader["MAIN_DIACODE"].ToString();
                if (!Reader.IsDBNull(16)) dianose.Diag.IcdName = this.Reader["MAIN_DIANAME"].ToString();
                if (!Reader.IsDBNull(17)) dianose.Diag.Mod = this.Reader["MOD_ICD"].ToString();
                if (!Reader.IsDBNull(18)) dianose.Diag.ModName = this.Reader["MOD_NAME"].ToString();
                if (!Reader.IsDBNull(19)) dianose.Diag.P_Code = this.Reader["P_CODE"].ToString();
                if (!Reader.IsDBNull(20)) dianose.Diag.T_Code = this.Reader["T_CODE"].ToString();
                if (!Reader.IsDBNull(21)) dianose.Diag.N_Code = this.Reader["N_CODE"].ToString();
                if (!Reader.IsDBNull(22)) dianose.Diag.M_Code = this.Reader["M_CODE"].ToString();

                if (!Reader.IsDBNull(23)) dianose.Diag1.Icd = this.Reader["MAIN_DIACODE1"].ToString();
                if (!Reader.IsDBNull(24)) dianose.Diag1.IcdName = this.Reader["MAIN_DIANAME1"].ToString();
                if (!Reader.IsDBNull(25)) dianose.Diag1.Mod = this.Reader["MOD_ICD1"].ToString();
                if (!Reader.IsDBNull(26)) dianose.Diag1.ModName = this.Reader["MOD_NAME1"].ToString();
                if (!Reader.IsDBNull(27)) dianose.Diag1.P_Code = this.Reader["P_CODE1"].ToString();
                if (!Reader.IsDBNull(28)) dianose.Diag1.T_Code = this.Reader["T_CODE1"].ToString();
                if (!Reader.IsDBNull(29)) dianose.Diag1.N_Code = this.Reader["N_CODE1"].ToString();
                if (!Reader.IsDBNull(30)) dianose.Diag1.M_Code = this.Reader["M_CODE1"].ToString();

                if (!Reader.IsDBNull(31)) dianose.Diag2.Icd = this.Reader["MAIN_DIACODE2"].ToString();
                if (!Reader.IsDBNull(32)) dianose.Diag2.IcdName = this.Reader["MAIN_DIANAME2"].ToString();
                if (!Reader.IsDBNull(33)) dianose.Diag2.Mod = this.Reader["MOD_ICD2"].ToString();
                if (!Reader.IsDBNull(34)) dianose.Diag2.ModName = this.Reader["MOD_NAME2"].ToString();
                if (!Reader.IsDBNull(35)) dianose.Diag2.P_Code = this.Reader["P_CODE2"].ToString();
                if (!Reader.IsDBNull(36)) dianose.Diag2.T_Code = this.Reader["T_CODE2"].ToString();
                if (!Reader.IsDBNull(37)) dianose.Diag2.N_Code = this.Reader["N_CODE2"].ToString();
                if (!Reader.IsDBNull(38)) dianose.Diag2.M_Code = this.Reader["M_CODE2"].ToString();

                if (!Reader.IsDBNull(39)) dianose.Diag.M_Code = this.Reader["DIAGNOSEREMARK"].ToString();
                if (!Reader.IsDBNull(40)) dianose.Comment = this.Reader["MARK"].ToString();

                if (!Reader.IsDBNull(41)) dianose.Comment = this.Reader["OPERID"].ToString();
                if (!Reader.IsDBNull(42)) dianose.Comment = this.Reader["OPERNAME"].ToString();
                if (!Reader.IsDBNull(43)) dianose.Comment = this.Reader["EXT1"].ToString();
                if (!Reader.IsDBNull(44)) dianose.Comment = this.Reader["EXT2"].ToString();
                if (!Reader.IsDBNull(45)) dianose.Comment = this.Reader["EXT3"].ToString();

                return dianose;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return null;
            }

        }
        #endregion

        #region �걾��ϲ���
        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="specBase"></param>
        /// <returns></returns>
        public int InsertDiagnose(SpecDiagnose diagnose)
        {
            try
            {
                return UpdateDiagnose("Speciment.BizLogic.DiagnoseManage.Insert", GetParam(diagnose));

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }
        #endregion

        #region ���������Ϣ
        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="specBase"></param>
        /// <returns></returns>
        public int UpdateDiagnose(SpecDiagnose diagnose)
        {
            try
            {
                return UpdateDiagnose("Speciment.BizLogic.DiagnoseManage.Update", GetParam(diagnose));

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
            ArrayList arrDiagnoseInfo = new ArrayList();
            while (this.Reader.Read())
            {
                SpecDiagnose info = SetDiagnoseInfo();
                arrDiagnoseInfo.Add(info);
            }
            Reader.Close();
            return arrDiagnoseInfo;
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
        public void GetNotInBaseInfo(string inBase, string strat, string end, ref DataSet ds)
        {
            this.ExecQuery("Speciment.BizLogic.DiagnoseManage.GetNoDiagnoseFromDiagnose", ref ds, new string[] { inBase, strat, end });
        }

        /// <summary>
        /// ���ݲ����Ż�ȡ�����Ϣ
        /// </summary>
        /// <param name="patientNo">סԺ��</param>
        /// <param name="ds"></param>
        public DataTable GetDiagnoseFromDiagnose(string patientNo, string icd)
        {
            DataSet ds = new DataSet();
            this.ExecQuery("Speciment.BizLogic.DiagnoseManage.GetDiagnose", ref ds, new string[] { patientNo, icd });
            if (ds == null || ds.Tables.Count <= 0)
            {
                return new DataTable();
            }
            else
            {
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// ��ѯ�����˵��������
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <returns></returns>
        public DataTable GetDiagnoseByInPatientNo(string inPatientNo)
        {
            string sql = "select distinct * from MET_CAS_DIAGNOSE where substr(INPATIENT_NO,5,10) = '" + inPatientNo.Substring(4) + "' and OPER_TYPE = '2' and (DIAG_KIND='1'  or DIAG_KIND='2')";
            DataSet ds = new DataSet();
            ExecQuery(sql, ref ds);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return new DataTable();
            }
            return ds.Tables[0];
        }

        public DataTable GetOperInfoByInPatNo(string inPatientNo)
        {
            string sql = "select distinct OPERATION_CODE, OPERATION_CNNAME from MET_CAS_OPERATIONDETAIL where INPATIENT_NO = '" + inPatientNo + "'";
            DataSet ds = new DataSet();
            ExecQuery(sql, ref ds);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return new DataTable();
            }
            return ds.Tables[0];
        }
    }
}
