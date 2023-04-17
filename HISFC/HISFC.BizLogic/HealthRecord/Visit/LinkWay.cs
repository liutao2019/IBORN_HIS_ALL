using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Function;
using System.Data;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord.Visit
{

    /// <summary>
    /// LinkWay<br></br>
    /// [��������: �����ϵ��ʽ����ҵ���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-08-23]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class LinkWay :FS.FrameWork.Management.Database
    {
        #region  ���ݿ��������

        /// <summary>
        /// ����һ����ϵ��ʽ��¼
        /// </summary>
        /// <param name="linkway">��ϵ��ʽ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int Insert(FS.HISFC.Models.HealthRecord.Visit.LinkWay linkway)
        {
            string strSQL = "";
            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.Insert�ֶΣ�";
                return -1;
            }
            try
            {
                //ȡ��ϵ��ʽ��ˮ��
                linkway.ID = this.GetSequence("HealthReacord.Visit.LinkWay.GetLinkWayID");
                if (linkway.ID == null)
                {
                    return -1;
                }
                //��ȡ���ݲ���
                string[] strParm = this.GetLinkWayParmItem(linkway);
                strSQL = string.Format(strSQL, strParm);

            }
            catch(Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }

            //ִ��SQL��䷵��
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ��һ����ϵ��ʽ��¼
        /// </summary>
        /// <param name="linkway">��ϵ��ʽ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int DeleteByLinkWayID(string linkWayID)
        {
            string strSQL = "";

            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.DeleteByLinkID", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.DeleteByLinkID�ֶΣ�";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, linkWayID);
            }
            catch(Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ɾ��ĳ�����ߵ�������ϵ��ʽ
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int DeleteByCardNo(string cardNo)
        {
            string strSQL = "";

            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.DeleteByCardNo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.DeleteByCardNo�ֶΣ�";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ���ݿ��Ų�ѯ��ϵ��ʽ
        /// </summary>
        /// <param name="CardNo">������</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList SelectByCardNo(string cardNo)
        {
            string strSQL = "";
            string strSQL1 = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.Select�ֶΣ�";
                return null;
            }
            //��ȡwhere���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.SelectWhereByCardNo", ref strSQL1) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.SelectWhereByCardNo�ֶΣ�";
                return��null;
            }
            strSQL = strSQL + "\n" + strSQL1;
            try
            {
                //���벡���Ų���
                strSQL = string.Format(strSQL, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return null;
            }

            //��������
            return this.ReadLinkWayInfo(strSQL);           
        }

        /// <summary>
        /// ���ݵ绰�����ѯ
        /// </summary>
        /// <param name="phone">�绰����</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList SelectByPhone(string phone)
        {
            string strSQL = "";
            string strSQL1 = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.Select�ֶΣ�";
                return null;
            }
            //��ȡwhere���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.SelectWhereByPhone", ref strSQL1) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.SelectWhereByPhone�ֶΣ�";
                return null;
            }
            strSQL = strSQL + "\n" + strSQL1;
            try
            {
                //���벡���Ų���
                strSQL = string.Format(strSQL, phone);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return null;
            }

            //��������
            return this.ReadLinkWayInfo(strSQL);         
        }

        /// <summary>
        /// ����������ѯ��ϵ��ʽ
        /// </summary>
        /// <param name="name">����</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList SelectByName(string name)
        {
            string strSQL = "";
            string strSQL1 = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.Select�ֶΣ�";
                return null;
            }
            //��ȡwhere���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.SelectWhereByName", ref strSQL1) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.SelectWhereByName�ֶΣ�";
                return null;
            }
            strSQL = strSQL + "\n" + strSQL1;
            try
            {
                //���벡���Ų���
                strSQL = string.Format(strSQL, name);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return null;
            }

            //��������
            return this.ReadLinkWayInfo(strSQL);
        }

        /// <summary>
        /// ����������ѯ��ϵ��ʽ
        /// </summary>
        /// <param name="name">����</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList SelectByAddress(string address)
        {
            string strSQL = "";
            string strSQL1 = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.Select�ֶΣ�";
                return null;
            }
            //��ȡwhere���
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.SelectWhereByAddress", ref strSQL1) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.LinkWay.SelectWhereByAddress�ֶΣ�";
                return null;
            }
            strSQL = strSQL + "\n" + strSQL1;
            try
            {
                //���벡���Ų���
                strSQL = string.Format(strSQL, address);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return null;
            }

            //��������
            return this.ReadLinkWayInfo(strSQL);
        }

        /// <summary>
        /// ��ȡInsert�Ĳ���
        /// </summary>
        /// <param name="linkway">��ϵ��ʽʵ����</param>
        /// <returns>���ز�������</returns>
        private string[] GetLinkWayParmItem(FS.HISFC.Models.HealthRecord.Visit.LinkWay linkway)
        {
            string[] strParm =new string[17];
            strParm[0] = linkway.ID;
            if(linkway.IsLinkMan)
            {
                strParm[1] = "1";
            }
            else
            {
                strParm[1] = "0";
            }          
            strParm[2] = linkway.LinkMan.Name;
            strParm[3] = linkway.LinkWayType.ID;
            strParm[4] = linkway.Relation.ID;
            strParm[5] = linkway.Patient.PID.CardNO;
            strParm[6] = linkway.ZIP;
            strParm[7] = linkway.OperEnvi.ID;
            strParm[8] = linkway.OperEnvi.OperTime.ToString();
            strParm[9] = linkway.Address;
            strParm[10] = linkway.Mail;
            strParm[11] = linkway.Phone;
            strParm[12] = linkway.OtherLinkway;
            strParm[13] = linkway.Memo;
            strParm[14] = linkway.User01;
            strParm[15] = linkway.User02;
            strParm[16] = linkway.User03;
            //��������
            return strParm;

        }

        /// <summary>
        /// ִ��SQL��䣬��ȡlinkWayʵ����Ϣ��ArrayList��
        /// </summary>
        /// <param name="strSQL">��Ҫִ�е�SQL���</param>
        /// <returns>�������顢���󷵻�null</returns>
        private ArrayList ReadLinkWayInfo(string strSQL)
        {
            this.ExecQuery(strSQL);

            ArrayList list = new ArrayList();
            FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWay = null;

            try
            {
                while (this.Reader.Read())
                {
                    linkWay = new FS.HISFC.Models.HealthRecord.Visit.LinkWay();

                    linkWay.ID = this.Reader[0].ToString();
                    if (this.Reader[1].ToString().Equals("1"))
                    {
                        linkWay.IsLinkMan = true;
                    }
                    else
                    {
                        linkWay.IsLinkMan = false;
                    }
                    linkWay.LinkMan.Name = this.Reader[2].ToString();
                    linkWay.LinkWayType.ID = this.Reader[3].ToString();
                    linkWay.Relation.ID = this.Reader[4].ToString();
                    linkWay.Patient.PID.CardNO = this.Reader[5].ToString();
                    linkWay.ZIP = this.Reader[6].ToString();
                    linkWay.OperEnvi.ID = this.Reader[7].ToString();
                    linkWay.OperEnvi.OperTime = NConvert.ToDateTime(this.Reader[8].ToString());
                    linkWay.Address = this.Reader[9].ToString();
                    linkWay.Mail = this.Reader[10].ToString();
                    linkWay.Phone = this.Reader[11].ToString();
                    linkWay.OtherLinkway = this.Reader[12].ToString();
                    linkWay.Memo = this.Reader[13].ToString();
                    linkWay.User01 = this.Reader[14].ToString();
                    linkWay.User02 = this.Reader[15].ToString();
                    linkWay.User03 = this.Reader[16].ToString();
                    linkWay.LinkWayType.Name = this.Reader[17].ToString();
                    linkWay.Relation.Name = this.Reader[18].ToString();
                    linkWay.OperEnvi.Name = this.Reader[19].ToString();

                    list.Add(linkWay);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ��ϵ��ʽ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return list;
        }

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}
        
        /// <summary>
        /// ����סԺ�Ż��߲����Ų�ѯ������ϵ����Ϣ
        /// </summary>
        /// <param name="patientNo">סԺ��</param>
        /// <param name="cardNo">������</param>
        /// <returns></returns>
        public ArrayList QueryLinkWay(string patientNo, string cardNo)
        {
            //��ȡ��sql���
            string strSQL = string.Empty;
            string str = "";
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.QueryLinkWay", ref strSQL) == -1)
            {
                this.Err = "��ȡSQL���:HealthReacord.Visit.VisitRecord.QueryLinkWay ʧ��";
                return null;
            }

            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.WhereLinkWay", ref str) == -1)
            {
                this.Err = "��ȡSQL���:HealthReacord.Visit.VisitRecord.WhereLinkWay ʧ��";
                return null;
            }
            strSQL += str;
            strSQL = string.Format(strSQL, patientNo, cardNo);

            return QueryLinkWayBySql(strSQL);
        }


        /// <summary>
        /// ����SQL��䣬��ѯ������ϵ���б�
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList QueryLinkWayBySql(string strSql)
        {
            this.ExecQuery(strSql);

            ArrayList list = new ArrayList();
            FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWay = null;

            try
            {
                while (this.Reader.Read())
                {
                    linkWay = new FS.HISFC.Models.HealthRecord.Visit.LinkWay();

                    linkWay.ID = this.Reader[0].ToString();//Ψһ���
                    linkWay.Name = this.Reader[1].ToString();//��ϵ��
                    linkWay.Memo = this.Reader[2].ToString();//�뻼�߹�ϵ
                    linkWay.Phone = this.Reader[3].ToString();//��ϵ�绰
                    linkWay.User01 = this.Reader[4].ToString();//�绰״̬
                    linkWay.Address = this.Reader[5].ToString();//��ϵ��ַ
                    linkWay.Mail = this.Reader[6].ToString();//Email

                    list.Add(linkWay);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ��ϵ��ʽ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return list;
        }

        /// <summary>
        /// ������ϵ��
        /// </summary>
        /// <param name="linkWayObj"></param>
        /// <returns></returns>
        public int InsertLinkWay(FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj)
        {
            //��ȡ��sql���
            string strSQL = string.Empty;
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.InsertLinkWay", ref strSQL) == -1)
            {
                this.Err = "��ȡSQL���:HealthReacord.Visit.LinkWay.InsertLinkWay ʧ��";
                return -1;
            }

            linkWayObj.ID = GetLinkWaySeqNo();//��ȡΨһID

            //��ȡ���ݲ���
            string[] strParm = GetLinkWayParm(linkWayObj);

            strSQL = string.Format(strSQL, strParm);

            return this.ExecNoQuery(strSQL);
      
        }

        /// <summary>
        /// ������ϵ��
        /// </summary>
        /// <param name="linkWayObj">��ϵ��ʽʵ����</param>
        /// <returns>ʧ�ܷ���-1</returns>
        public int UpdateInsertLinkWay(FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj)
        {
            //��ȡ��sql���
            string strSQL = string.Empty;
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.UpdateLinkWay", ref strSQL) == -1)
            {
                this.Err = "��ȡSQL���:HealthReacord.Visit.LinkWay.UpdateLinkWay ʧ��";
                return -1;
            }

            //��ȡ���ݲ���
            string[] strParm = GetLinkWayParm(linkWayObj);

            strSQL = string.Format(strSQL, strParm);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ɾ����ϵ��
        /// </summary>
        /// <param name="linkWayObj">��ϵ��ʽʵ����</param>
        /// <returns>ʧ�ܷ���-1</returns>
        public int DelLinkWay(FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj)
        {
            //��ȡ��sql���
            string strSQL = string.Empty;
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.DeleteLinkWay", ref strSQL) == -1)
            {
                this.Err = "��ȡSQL���:HealthReacord.Visit.LinkWay.DeleteLinkWay ʧ��";
                return -1;
            }

            strSQL = string.Format(strSQL, linkWayObj.ID);

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ȡ��ϵ��Ψһ��־
        /// </summary>
        /// <returns></returns>
        private string GetLinkWaySeqNo()
        {
            //��ȡ��sql���
            string strSQL = string.Empty;
            if (this.Sql.GetSql("HealthReacord.Visit.LinkWay.NewLinkWayNo", ref strSQL) == -1)
            {
                this.Err = "��ȡSQL���:HealthReacord.Visit.LinkWay.NewLinkWayNo ʧ��";
                return null;
            }

            return this.ExecSqlReturnOne(strSQL);
        }


        /// <summary>
        /// ��ȡSQL������
        /// </summary>
        /// <param name="linkWayObj">��ϵ��ʽʵ����</param>
        /// <returns>���ز�������</returns>
        private string[] GetLinkWayParm(FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj)
        {
            return new string[] { linkWayObj.ID, //Ψһ���
                linkWayObj.Patient.PID.PatientNO, //סԺ��
                linkWayObj.Patient.PID.CardNO, //������
                linkWayObj.Name ,//��ϵ��
                linkWayObj.Memo,//�뻼�߹�ϵ
                linkWayObj.Address,//��ַ
                linkWayObj.Mail,//��������
                linkWayObj.Phone,//��ϵ�绰
                linkWayObj.User01,//�绰״̬
                this.Operator.ID,//����Ա
                this.GetSysDateTime()//����ʱ��
            };
        }

        #endregion

        #endregion
    }
}
