using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.HealthRecord
{
    public class Operation : FS.FrameWork.Management.Database
    {
        public Operation()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// ����ĳ��סԺ��ˮ���µ� ������Ϣ  operType ����� "DOC" ��ѯ����ҽ��վ¼���������Ϣ ���������ǡ�CAS�������ѯ����ʦ¼���������Ϣ
        /// Creator: zhangjunyi@FS.com
        /// </summary>
        /// <param name="operType">����</param>
        /// <returns>�ɹ����ط������������飬ʧ�ܻ�����쳣���� ����</returns>
        public ArrayList QueryOperation(FS.HISFC.Models.HealthRecord.EnumServer.frmTypes operType, string InpatientNo)
        {
            string OperType = "";
            if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                OperType = "1";
            }
            else if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                OperType = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return null;
            }
            ArrayList List = null;
            string MainSql = QueryOperationSql();
            if (MainSql == null)
            {
                return null;
            }
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.Select", ref strSql) == -1) return null;
            strSql = MainSql + strSql;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo, OperType);
                return myQuery(strSql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                List = null;
            }
            return List;
        }

        /// <summary>
        /// ��ѯ������Ϣ������ϵͳ��
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryOperation(string InpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.Select.From.OperationSystem", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                return myQuery(strSql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                List = null;
            }
            return List;
        }

        /// <summary>
        /// ˽�б���
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList myQuery(string strSql)
        {
            ArrayList List = null;
            try
            {
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.OperationDetail info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.OperationDetail();

                    info.OperationDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[0] == DBNull.Value ? "0001-01-01" : this.Reader[0]);//�������� 
                    info.OperationInfo.ID = this.Reader[1] == DBNull.Value ? string.Empty : this.Reader[1].ToString();	//��������
                    info.OperationInfo.Name = this.Reader[2] == DBNull.Value ? string.Empty : this.Reader[2].ToString(); //��������
                    info.OperationKind = this.Reader[3] == DBNull.Value ? string.Empty : this.Reader[3].ToString();      //��������
                    info.MarcKind = this.Reader[4] == DBNull.Value ? string.Empty : this.Reader[4].ToString();			//����ʽ
                    info.NickKind = this.Reader[5] == DBNull.Value ? string.Empty : this.Reader[5].ToString();			//�п�����  
                    info.CicaKind = this.Reader[6] == DBNull.Value ? string.Empty : this.Reader[6].ToString();			//��������
                    info.FirDoctInfo.ID = this.Reader[7] == DBNull.Value ? string.Empty : this.Reader[7].ToString();		//����ҽʦ����
                    info.FirDoctInfo.Name = this.Reader[8] == DBNull.Value ? string.Empty : this.Reader[8].ToString();	//����ҽʦ����
                    info.SecDoctInfo.ID = this.Reader[9] == DBNull.Value ? string.Empty : this.Reader[9].ToString();	//I������
                    info.SecDoctInfo.Name = this.Reader[10] == DBNull.Value ? string.Empty : this.Reader[10].ToString();	//I������
                    info.ThrDoctInfo.ID = this.Reader[11] == DBNull.Value ? string.Empty : this.Reader[11].ToString();	//II������
                    info.ThrDoctInfo.Name = this.Reader[12] == DBNull.Value ? string.Empty : this.Reader[12].ToString();	//II������
                    info.NarcDoctInfo.ID = this.Reader[13] == DBNull.Value ? string.Empty : this.Reader[13].ToString();	//����ҽʦ����
                    info.NarcDoctInfo.Name = this.Reader[14] == DBNull.Value ? string.Empty : this.Reader[14].ToString();	//����ҽʦ����
                    //					info.OperationInfo.Name = Reader[15].ToString();//��������
                    info.OpbOpa = this.Reader[15] == DBNull.Value ? string.Empty : this.Reader[15].ToString();			//��ǰ_�������ǰ_�����
                    info.BeforeOperDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[16] == DBNull.Value ? 0 : this.Reader[16]);//��ǰסԺ����
                    info.StatFlag = this.Reader[17] == DBNull.Value ? string.Empty : this.Reader[17].ToString();//ͳ�Ʊ�־ 
                    info.InDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18] == DBNull.Value ? "0001-01-01" : this.Reader[18].ToString());	//������� 
                    info.OutDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[19] == DBNull.Value ? "0001-01-01" : this.Reader[19].ToString());//��Ժ����
                    info.DeatDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20] == DBNull.Value ? "0001-01-01" : this.Reader[20].ToString());//��������
                    info.OperationDeptInfo.ID = this.Reader[21] == DBNull.Value ? string.Empty : this.Reader[21].ToString();//��������
                    info.OutDeptInfo.ID = this.Reader[22] == DBNull.Value ? string.Empty : this.Reader[22].ToString();	//��Ժ���� 
                    info.OutICDInfo.ID = this.Reader[23] == DBNull.Value ? string.Empty : this.Reader[23].ToString();		//��Ժ�����ICD
                    info.SYNDFlag = this.Reader[24] == DBNull.Value ? string.Empty : this.Reader[24].ToString();			//�Ƿ�ϲ�֢
                    //					info.OperDate = Reader[25].ToString();		//����Ա
                    //					info.		  Reader[26].ToString();  ����ʱ��			
                    info.OperType = this.Reader[27] == DBNull.Value ? string.Empty : this.Reader[27].ToString();			//���  1 ҽ��վ������ϸ   2 ������������ϸ 
                    info.FourDoctInfo.ID = this.Reader[28] == DBNull.Value ? string.Empty : this.Reader[28].ToString();//����ҽʦ����2
                    info.FourDoctInfo.Name = this.Reader[29] == DBNull.Value ? string.Empty : this.Reader[29].ToString();//����ҽʦ����2
                    info.HappenNO = this.Reader[30] == DBNull.Value ? string.Empty : this.Reader[30].ToString();//�������
                    info.InpatientNO = this.Reader[31] == DBNull.Value ? string.Empty : this.Reader[31].ToString(); //סԺ��ˮ��
                    info.OperationNO = this.Reader[32] == DBNull.Value ? string.Empty : this.Reader[32].ToString(); //סԺ��ˮ��
                    List.Add(info);
                    info = null;
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return List;
        }
        /// <summary>
        /// ��SQL���
        /// </summary>
        /// <returns></returns>
        private string QueryOperationSql()
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.QueryOperationSql", ref strSql) == -1) return null;
            return strSql;
        }
        /// <summary>
        /// ��SQL����ѯmet_ops_apply����Ϣ
        /// </summary>
        /// <returns></returns>
        private string QueryOperationSql1()
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.QueryOperationSql1", ref strSql) == -1) return null;
            return strSql;
        }
        /// <summary>
        /// ����ĳ��סԺ��ˮ���µ� ������¼���������Ϣ
        /// Creator: zhangjunyi@FS.com
        /// </summary>
        /// <returns>�ɹ����ط������������飬ʧ�ܻ�����쳣���� ����</returns>
        public ArrayList QueryOperationByInpatientNo(string InpatientNo)
        {
            ArrayList List = null;
            string MainSql = QueryOperationSql();
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.SelectOperation", ref strSql) == -1) return null;
            strSql = MainSql + strSql;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                return myQuery(strSql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                List = null;
            }
            return List;
        }
        /// <summary>
        /// ����ĳ��סԺ��ˮ���µ� ������¼���������Ϣ
        /// Creator: ��ѯmet_ops_apply����Ϣ
        /// </summary>
        /// <returns>�ɹ����ط������������飬ʧ�ܻ�����쳣���� ����</returns>
        public ArrayList QueryOperationByInpatientNo1(string InpatientNo)
        {
            ArrayList List = null;
            string MainSql = QueryOperationSql1();
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.SelectOperation1", ref strSql) == -1) return null;
            strSql = MainSql + strSql;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                return myQuery(strSql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                List = null;
            }
            return List;
        }
        /// <summary>
        /// ����ĳ��������Ϣ  operType ����� "DOC" ���µ���ҽ��վ¼���������Ϣ ���������ǡ�CAS��������²���ʦ¼���������Ϣ
        /// Creator:zhangjunyi@FS.com
        /// </summary>
        /// <param name="OperType">���� ������ʶ����ҽ��վ���ǲ��� </param>
        /// <param name="info"></param>
        /// <returns>�ɹ�����1 ʧ�ܷ��� ��1</returns>
        public int Update(FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, FS.HISFC.Models.HealthRecord.OperationDetail info)
        {
            //������� �ж���ҽ�����뻹�ǲ���������

            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                info.OperType = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                info.OperType = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return -1;
            }
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.Update", ref strSql) == -1) return -1;
            try
            {
                //��������
                string[] str = Getinfo(info);
                strSql = string.Format(strSql, str);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����ĳ��������Ϣ  operType ����� "DOC" �������ҽ��վ¼���������Ϣ ���������ǡ�CAS������ ���벡��ʦ¼���������Ϣ
        /// Creator: zhangjunyi@FS.com
        /// </summary>
        /// <param name="OperType">��ʶ</param>
        /// <param name="info"></param>
        /// <returns>�ɹ����� 1 ʧ�ܷ��� ��1 </returns>
        public int Insert(FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, FS.HISFC.Models.HealthRecord.OperationDetail info)
        {
            string strSql = "";
            //������� �ж���ҽ�����뻹�ǲ���������

            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                info.OperType = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                info.OperType = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return -1;
            }
            int intHappenNo = GetNewOperationNo(info.InpatientNO, info.OperType);
            //�������
            info.HappenNO = intHappenNo.ToString();
            if (this.Sql.GetSql("Case.Operationdetail.Insert", ref strSql) == -1) return -1;
            try
            {
                //��������
                string[] str = Getinfo(info);
                strSql = string.Format(strSql, str);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// �����������������
        /// </summary>
        /// <returns> ���������� ����ʱ����-1</returns>
        public int GetNewOperationNo(string InpatientNo, string type)
        {
            int lNewNo = -1;
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.GetNewOperationNo.1", ref strSql) == -1) return -1;
            if (strSql == null) return -1;
            strSql = string.Format(strSql, InpatientNo, type);
            this.ExecQuery(strSql);
            try
            {
                while (this.Reader.Read())
                {
                    lNewNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr(); ;
                return -1;
            }
            this.Reader.Close();
            return lNewNo;
        }
        /// <summary>
        /// ɾ��ĳ��������Ϣ  operType ����� "DOC" ɾ������ҽ��վ¼���������Ϣ ���������ǡ�CAS������ ɾ������ʦ¼���������Ϣ
        /// Creator :zhangjunyi@FS.com
        /// </summary>
        /// <param name="OperType"></param>
        /// <param name="info"></param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int delete(FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, FS.HISFC.Models.HealthRecord.OperationDetail info)
        {
            //������� �ж���ҽ�����뻹�ǲ���������

            if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                info.OperType = "1";
            }
            else if (OperType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                info.OperType = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return -1;
            }
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.delete", ref strSql) == -1) return -1;
            try
            {
                //��������
                string[] str = Getinfo(info);
                strSql = string.Format(strSql, str);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        public int DeleteByCodeAndTime(System.DateTime dt, string InpatientNO)
        {
            try
            {
                string strSql = "";
                if (this.Sql.GetSql("Case.Operationdetail.DeleteByCodeAndTime", ref strSql) == -1) return -1;
                strSql = string.Format(strSql, InpatientNO, dt.ToString());
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ��ȡʵ�������Ϣ �����ز������� 
        /// </summary>
        /// <param name="info">ʵ����</param>
        /// <returns>ʧ�ܷ���null �ɹ����ز�������</returns>
        private string[] Getinfo(FS.HISFC.Models.HealthRecord.OperationDetail info)
        {
            string[] s = new string[33];
            try
            {
                s[0] = info.InpatientNO; //סԺ��ˮ��
                s[1] = info.HappenNO;//�������
                s[2] = info.OperationDate.ToString();//�������� 
                s[3] = info.OperationInfo.ID;	//��������
                s[4] = info.OperationInfo.Name; //��������
                s[5] = info.OperationKind;      //��������
                s[6] = info.MarcKind;			//����ʽ
                s[7] = info.NickKind;			//�п�����  
                s[8] = info.CicaKind;			//��������
                s[9] = info.FirDoctInfo.ID;		//����ҽʦ����
                s[10] = info.FirDoctInfo.Name;	//����ҽʦ����
                s[11] = info.SecDoctInfo.ID;	//I������
                s[12] = info.SecDoctInfo.Name;	//I������
                s[13] = info.ThrDoctInfo.ID;	//II������
                s[14] = info.ThrDoctInfo.Name;	//II������
                s[15] = info.NarcDoctInfo.ID;	//����ҽʦ����
                s[16] = info.NarcDoctInfo.Name;	//����ҽʦ����
                s[17] = info.OpbOpa;			//��ǰ_�������ǰ_�����
                s[18] = info.BeforeOperDays.ToString();//��ǰסԺ����
                s[19] = info.StatFlag;//ͳ�Ʊ�־ 
                s[20] = info.InDate.ToString();	//������� 
                s[21] = info.OutDate.ToString();//��Ժ����
                s[22] = info.DeatDate.ToString();//��������
                s[23] = info.OperationDeptInfo.ID;//��������
                s[24] = info.OutDeptInfo.ID;	//��Ժ���� 
                s[25] = info.OutICDInfo.ID;		//��Ժ�����ICD
                s[26] = info.SYNDFlag;			//�Ƿ�ϲ�֢
                s[27] = this.Operator.ID;		//����Ա
                s[28] = info.OperType;			//���  1 ҽ��վ������ϸ   2 ������������ϸ 
                s[29] = info.FourDoctInfo.ID;//����ҽʦ����2
                s[30] = info.FourDoctInfo.Name;//����ҽʦ����2
                s[31] = info.OperDate.ToString(); //����ʱ��
                s[32] = info.OperationNO.ToString(); //�������к�
                return s;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ��ȡ��һ���� 
        /// </summary>
        /// <param name="InpatientNo"></param>
        /// <param name="frmType"></param>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.OperationDetail GetFirstOperation(string InpatientNo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType)
        {
            FS.HISFC.Models.HealthRecord.OperationDetail info = new FS.HISFC.Models.HealthRecord.OperationDetail();
            ArrayList list = QueryOperation(frmType, InpatientNo);
            if (list == null)
            {
                return null;
            }
            if (list.Count > 0)
            {
                info = (FS.HISFC.Models.HealthRecord.OperationDetail)list[0];
            }
            return info;
        }

        /// <summary>
        /// ɾ��������Ϣby סԺ��ˮ��
        /// Creator :chengym 2011-9-27
        /// </summary>
        /// <param name="InpatienNo"></param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int deleteAll(string  InpatienNo)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.DeleteAll", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, InpatienNo);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #region �µ��Ӳ���ʹ��ģ��ʵ����ҳ���
        /// <summary>
        /// ����ĳ��סԺ��ˮ���µ�������Ϣ
        /// </summary>
        /// <returns>�ɹ����ط������������飬ʧ�ܻ�����쳣���� ����</returns>
        public ArrayList QueryOperationFromEmrViewByInpatientNo(string InpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            strSql = @" select 
OPERATION_DATE, --��������
OPERATION_CODE, --��������
OPERATION_CNNAME,-- ��������  
OPERATION_KIND,--��������
NARC_KIND,--����ʽ
NICK_KIND,--�п�����
CICA_KIND,--��������  
FIR_DOCD ,--����ҽʦ���� 
FIR_DONM ,--����ҽʦ���� 
SEC_DOCD , --I������
SEC_DONM ,--I������
THR_DOCD,--II������
THR_DONM,--II������
NARC_DOCD  ,--����ҽʦ����
NARC_DONM,--����ҽ������
OPB_OPA,-- ��ǰ_�����
BEFOREOPER_DAYS,--��ǰסԺ����
STAT_FLAG ,--ͳ�Ʊ�־
IN_DATE ,--�������
OUT_DATE,-- ��Ժ����
DEAD_DATE,--�������� 
OPERATION_DEPT, --��������  
OUT_DEPT ,--��Ժ���� 
OUT_ICD ,-- ��Ժ�����ICD
SYND_FLAG,-- �Ƿ�ϲ�֢ 
OPER_CODE, -- ����Ա 
OPER_DATE, --����ʱ��
OPER_TYPE, -- ���  1 ҽ��վ������ϸ   2 ������������ϸ
FIR_DCODE2, -- ����ҽʦ����2 
FIR_DNAME2,  --����ҽʦ����2  
HAPPEN_NO  ,
inpatient_no,
operationno
from view_met_cas_operationdetail
where inpatient_no = '{0}' ";
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                return myQuery(strSql);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                List = null;
            }
            return List;
        }
        #endregion
        #region ����
        /// <summary>
        /// ����ĳ��סԺ��ˮ���µ� ������Ϣ  operType ����� "DOC" ��ѯ����ҽ��վ¼���������Ϣ ���������ǡ�CAS�������ѯ����ʦ¼���������Ϣ
        /// Creator: zhangjunyi@FS.com
        /// </summary>
        /// <param name="operType">����</param>
        /// <returns>�ɹ����ط������������飬ʧ�ܻ�����쳣���� ����</returns>
        [Obsolete("���� �� QueryOperation ", true)]
        public ArrayList Select(FS.HISFC.Models.HealthRecord.EnumServer.frmTypes operType, string InpatientNo)
        {
            string OperType = "";
            if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                OperType = "1";
            }
            else if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                OperType = "2";
            }
            else
            {
                this.Err = "û��ָ����������� DOC �� CAS";
                return null;
            }
            ArrayList List = null;
            string MainSql = QueryOperationSql();
            if (MainSql == null)
            {
                return null;
            }
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.Select", ref strSql) == -1) return null;
            strSql = MainSql + strSql;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo, OperType);
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.OperationDetail info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.OperationDetail();

                    info.OperationDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[0]);//�������� 
                    info.OperationInfo.ID = Reader[1].ToString();	//��������
                    info.OperationInfo.Name = Reader[2].ToString(); //��������
                    info.OperationKind = Reader[3].ToString();      //��������
                    info.MarcKind = Reader[4].ToString();			//����ʽ
                    info.NickKind = Reader[5].ToString();			//�п�����  
                    info.CicaKind = Reader[6].ToString();			//��������
                    info.FirDoctInfo.ID = Reader[7].ToString();		//����ҽʦ����
                    info.FirDoctInfo.Name = Reader[8].ToString();	//����ҽʦ����
                    info.SecDoctInfo.ID = Reader[9].ToString();	//I������
                    info.SecDoctInfo.Name = Reader[10].ToString();	//I������
                    info.ThrDoctInfo.ID = Reader[11].ToString();	//II������
                    info.ThrDoctInfo.Name = Reader[12].ToString();	//II������
                    info.NarcDoctInfo.ID = Reader[13].ToString();	//����ҽʦ����
                    info.NarcDoctInfo.Name = Reader[14].ToString();	//����ҽʦ����
                    //					info.OperationInfo.Name = Reader[15].ToString();//��������
                    info.OpbOpa = Reader[15].ToString();			//��ǰ_�������ǰ_�����
                    info.BeforeOperDays = FS.FrameWork.Function.NConvert.ToInt32(Reader[16]);//��ǰסԺ����
                    info.StatFlag = Reader[17].ToString();//ͳ�Ʊ�־ 
                    info.InDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[18]);	//������� 
                    info.OutDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[19]);//��Ժ����
                    info.DeatDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[20]);//��������
                    info.OperationDeptInfo.ID = Reader[21].ToString();//��������
                    info.OutDeptInfo.ID = Reader[22].ToString();	//��Ժ���� 
                    info.OutICDInfo.ID = Reader[23].ToString();		//��Ժ�����ICD
                    info.SYNDFlag = Reader[24].ToString();			//�Ƿ�ϲ�֢
                    //					info.OperDate = Reader[25].ToString();		//����Ա
                    //					info.		  Reader[26].ToString();  ����ʱ��			
                    info.OperType = Reader[27].ToString();			//���  1 ҽ��վ������ϸ   2 ������������ϸ 
                    info.FourDoctInfo.ID = Reader[28].ToString();//����ҽʦ����2
                    info.FourDoctInfo.Name = Reader[29].ToString();//����ҽʦ����2
                    info.InpatientNO = InpatientNo; //סԺ��ˮ��
                    info.HappenNO = Reader[30].ToString();//�������
                    List.Add(info);
                    info = null;
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            return List;
        }

        /// <summary>
        /// ����ĳ��סԺ��ˮ���µ� ������¼���������Ϣ
        /// Creator: zhangjunyi@FS.com
        /// </summary>
        /// <returns>�ɹ����ط������������飬ʧ�ܻ�����쳣���� ����</returns>
        [Obsolete("���� �� QueryOperationByInpatientNo ���� ",true)]
        public ArrayList SelectOperation(string InpatientNo)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.Operationdetail.SelectOperation", ref strSql) == -1) return null;
            try
            {
                //��ѯ
                strSql = string.Format(strSql, InpatientNo);
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.OperationDetail info = null;
                List = new ArrayList();
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.HealthRecord.OperationDetail();

                    info.OperationDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[0]);//�������� 
                    info.OperationInfo.ID = Reader[1].ToString();	//��������
                    info.OperationInfo.Name = Reader[2].ToString(); //��������
                    info.OperationKind = Reader[3].ToString();      //��������
                    info.MarcKind = Reader[4].ToString();			//����ʽ
                    info.NickKind = Reader[5].ToString();			//�п�����  
                    info.CicaKind = Reader[6].ToString();			//��������
                    info.FirDoctInfo.ID = Reader[7].ToString();		//����ҽʦ����
                    info.FirDoctInfo.Name = Reader[8].ToString();	//����ҽʦ����
                    info.SecDoctInfo.ID = Reader[9].ToString();	//I������
                    info.SecDoctInfo.Name = Reader[10].ToString();	//I������
                    info.ThrDoctInfo.ID = Reader[11].ToString();	//II������
                    info.ThrDoctInfo.Name = Reader[12].ToString();	//II������
                    info.NarcDoctInfo.ID = Reader[13].ToString();	//����ҽʦ����
                    info.NarcDoctInfo.Name = Reader[14].ToString();	//����ҽʦ����
                    //					info.OperationInfo.Name = Reader[15].ToString();//��������
                    info.OpbOpa = Reader[15].ToString();			//��ǰ_�������ǰ_�����
                    info.BeforeOperDays = FS.FrameWork.Function.NConvert.ToInt32(Reader[16]);//��ǰסԺ����
                    info.StatFlag = Reader[17].ToString();//ͳ�Ʊ�־ 
                    info.InDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[18]);	//������� 
                    info.OutDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[19]);//��Ժ����
                    info.DeatDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[20]);//��������
                    info.OperationDeptInfo.ID = Reader[21].ToString();//��������
                    info.OutDeptInfo.ID = Reader[22].ToString();	//��Ժ���� 
                    info.OutICDInfo.ID = Reader[23].ToString();		//��Ժ�����ICD
                    info.SYNDFlag = Reader[24].ToString();			//�Ƿ�ϲ�֢
                    //					info.OperDate = Reader[25].ToString();		//����Ա
                    //					info.		  Reader[26].ToString();  ����ʱ��			
                    info.OperType = Reader[27].ToString();			//���  1 ҽ��վ������ϸ   2 ������������ϸ 
                    info.FourDoctInfo.ID = Reader[28].ToString();//����ҽʦ����2
                    info.FourDoctInfo.Name = Reader[29].ToString();//����ҽʦ����2
                    info.InpatientNO = InpatientNo; //סԺ��ˮ��
                    info.HappenNO = Reader[30].ToString();//�������
                    List.Add(info);
                    info = null;
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            return List;
        }
        #endregion
    }
}
