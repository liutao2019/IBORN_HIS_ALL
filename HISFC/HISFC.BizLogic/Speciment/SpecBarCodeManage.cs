using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Speciment;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: �걾������ϸ����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-10-10]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-18' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class SpecBarCodeManage : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">�걾��֯���Ͷ���</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.SpecBarCode specBarCode)
        {
            string[] str = new string[]
                           {
                               specBarCode.DisType,
                               specBarCode.DisAbrre,
                               specBarCode.SpecType,
                               specBarCode.SpecTypeAbrre,
                               specBarCode.Sequence,
                               specBarCode.Other,
                               specBarCode.OrgOrBld
                           };
            return str;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Speciment.SpecBarCode SetBarCode()
        {
            FS.HISFC.Models.Speciment.SpecBarCode barCode = new FS.HISFC.Models.Speciment.SpecBarCode();
            try
            {
                barCode.DisType = this.Reader["DISEASETYPE"].ToString();
                barCode.DisAbrre = this.Reader["DISABRRE"].ToString();
                barCode.SpecType = this.Reader["SPECTYPE"].ToString();
                barCode.SpecTypeAbrre = this.Reader["SPECTYPEABRRE"].ToString();
                barCode.Sequence = this.Reader["SEQ"].ToString();
                barCode.Other = this.Reader["OTHER"].ToString();
                barCode.OrgOrBld = this.Reader["ORGORBLD"].ToString();
                //if (null == this.Reader["MARK"]) spec.Comment = "";
                //else
                //    spec.Comment = this.Reader["MARK"].ToString();
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return barCode;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sqlIndex">sql����</param>
        /// <param name="args">����</param>
        /// <returns>Ӱ��ļ�¼����</returns>
        private int UpdateBarCode(string sqlIndex, params string[] args)
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

        /// <summary>
        /// ����������ȡ�����������������к�
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList GetSubBarCode(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return null;
            }
            if (this.ExecQuery(sql, args) == -1)
                return null;
            ArrayList arrSubBarCode = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Speciment.SpecBarCode tmp = SetBarCode();
                arrSubBarCode.Add(tmp);
            }
            this.Reader.Close();
            return arrSubBarCode;
        }

        /// <summary>
        /// ���¶�Ӧ�Ĳ���,��Ӧ������ ���������
        /// </summary>
        /// <param name="disType">����</param>
        /// <param name="specType">�걾����</param>
        /// <param name="sequence">�������</param>
        /// <returns></returns>
        public int UpdateBarCode(string disType, string specType, string sequence)
        {
            return this.UpdateBarCode("Speciment.BizLogic.SpecBarCodeManage.Update", new string[] { disType, specType, sequence });
        }

        public FS.HISFC.Models.Speciment.SpecBarCode GetSpecBarCode(string disType, string specType)
        {
            ArrayList arr = this.GetSubBarCode("Speciment.BizLogic.SpecBarCodeManage.GetBarCode", new string[] { disType, specType });
            if (arr == null || arr.Count <= 0)
            {
                return null;
            }
            return arr[0] as FS.HISFC.Models.Speciment.SpecBarCode;
        }

        /// <summary>
        /// ���ݲ��ֺͱ걾���ʹ���ȡ������к�
        /// </summary>
        /// <param name="disTye">��������</param>
        /// <param name="orgType">�걾���ʹ��࣬Ѫ��or��֯</param>
        /// <returns></returns>
        public string GetMaxSeqByDisAndType(string disType, string orgType)
        {
            string sql = "select max(seq) from SPEC_SUBBARCODE where ORGORBLD = '" + orgType + "' and DISEASETYPE = '" + disType + "'";
            string sequence = this.ExecSqlReturnOne(sql);
            try
            {
                return sequence;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// ����seq
        /// </summary>
        /// <param name="disType"></param>
        /// <param name="orgType"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public int UpdateMaxSeqByDisAndType(string disType, string orgType ,string seq)
        {
            string sql = "UPDATE SPEC_SUBBARCODE SET seq = '" + seq + "' where ORGORBLD = '" + orgType + "' and DISEASETYPE = '" + disType + "'";
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ��ȡһ�ֲ��ֵ����б걾���͵���Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllSpecTypeByDisType()
        {
            ArrayList arr = this.GetSubBarCode("Speciment.BizLogic.SpecBarCodeManage.GetAllSpecTypeByDistype", new string[] { });
            if (arr == null || arr.Count <= 0)
            {
                return null;
            }
            return arr;
        }

        public ArrayList GetAllDisTypeBySpecType()
        {
            ArrayList arr = this.GetSubBarCode("Speciment.BizLogic.SpecBarCodeManage.GetAllDisTypeBySpectype", new string[] { });
            if (arr == null || arr.Count <= 0)
            {
                return null;
            }
            return arr;
        }

        /// <summary>
        ///�����¼�¼
        /// </summary>
        /// <param name="bar"></param>
        /// <returns></returns>
        public int InsertBarCode(SpecBarCode bar)
        {
            return this.UpdateBarCode("Speciment.BizLogic.SpecBarCodeManage.Insert", GetParam(bar));
        }
    }
}
