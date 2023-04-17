using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.HealthRecord.Visit
{
    /// <summary>
    /// VisitRecord<br></br>
    /// [��������: �����ϸ����ҵ���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-08-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='���'
    ///		�޸�ʱ��='2009-09-15'
    ///		�޸�Ŀ��='�����Ӳ�ѯ����û����б�'
    ///		�޸�����='{E9F858A6-BDBC-4052-BA57-68755055FB80}'
    ///  />
    /// </summary>
    public class VisitRecord :FS.FrameWork.Management.Database
    {
        #region ���ݿ�Ļ�������

        /// <summary>
        /// �������ϸ���в���һ���¼�¼
        /// </summary>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int Insert(FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord, string sequ) 
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.Insert�ֶ�!";
                return -1;
            }
            try
            {
                //��ȡ�����ϸ��ˮ��
                visitRecord.ID = sequ;

                //��ȡ���ݲ�������
                string[] strParm = this.GetVisitRecordParmItem(visitRecord);
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����!" + ex.Message;
                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// ��ȡ�����ϸ��ˮ��
        /// </summary>
        /// <returns>��ˮ��</returns>
        public string GetVisitRecordSequ()
        {
            string sequ = this.GetSequence("HealthReacord.Visit.VisitRecord.GetVisitRecordID");

            if (sequ == null)
            {
                this.Err = "��ȡ��ˮ�ų���";

                return null;
            }

            return sequ;
        }

        /// <summary>
        /// �����Ϣ¼��
        /// </summary>
        /// <param name="visitRecord"></param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int Update(FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.Update", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.Update�ֶΣ�";
                return -1;
            }
            try
            {
                string[] strParm = this.GetVisitRecordParmItem(visitRecord);
                strSQL = string.Format(strSQL, strParm);
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
        /// ��ȡinsert��update�Ĳ���
        /// </summary>
        /// <param name="linkway">�����ϸ��¼��</param>
        /// <returns>���ز�������</returns>
        private string[] GetVisitRecordParmItem(FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord)
        {
            string[] strParm =new string[36];

            strParm[0] = visitRecord.ID;
            strParm[1] = visitRecord.Patient.PID.CardNO;
            strParm[2] = visitRecord.Circs.ID;
            strParm[3] = visitRecord.DeadReason.ID;
            strParm[4] = visitRecord.DeadTime.ToString();
            if (visitRecord.IsDead)
            {
                strParm[5] = "1";
            }
            else
            {
                strParm[5] = "0";
            }
            if (visitRecord.IsRecrudesce)
            {
                strParm[6] = "1";
            }
            else
            {
                strParm[6] = "0";
            }
            if (visitRecord.IsSequela)
            {
                strParm[7] = "1";
            }
            else
            {
                strParm[7] = "0";
            }
            if (visitRecord.IsSuccess)
            {
                strParm[8] = "1";
            }
            else
            {
                strParm[8] = "0";
            }
            if (visitRecord.IsTransfer)
            {
                strParm[9] = "1";
            }
            else
            {
                strParm[9] = "0";
            }
            strParm[10] = visitRecord.RecrudesceTime.ToString();
            strParm[11] = visitRecord.ResultOper.ID;
            strParm[12] = visitRecord.ResultOper.OperTime.ToString();
            strParm[13] = visitRecord.Sequela.ID;
            strParm[14] = visitRecord.Symptom.ID;
            strParm[15] = visitRecord.TransferPosition.ID;
            strParm[16] = visitRecord.VisitOper.ID;
            strParm[17] = visitRecord.VisitOper.OperTime.ToString();
            strParm[18] = visitRecord.VisitType.ID;
            strParm[19] = visitRecord.WriteBackPerson;
            if (visitRecord.IsPassive)
            {
                strParm[20] = "1";
            }
            else
            {
                strParm[20] = "0";
            }
            if (visitRecord.LinkWay.IsLinkMan)
            {
                strParm[21] = "1";
            }
            else
            {
                strParm[21] = "0";
            }
            strParm[22] = visitRecord.LinkWay.LinkMan.Name;
            strParm[23] = visitRecord.LinkWay.LinkWayType.ID;
            strParm[24] = visitRecord.LinkWay.Relation.ID;
            if (visitRecord.IsResult)
            {
                strParm[25] = "1";
            }
            else
            {
                strParm[25] = "0";
            }
            strParm[26] = visitRecord.LinkWay.ZIP;
            if (visitRecord.IsWorkLoad)
            {
                strParm[27] = "1";
            }
            else
            {
                strParm[27] = "0";
            }
            strParm[28] = visitRecord.LinkWay.Address;
            strParm[29] = visitRecord.LinkWay.Mail;
            strParm[30] = visitRecord.LinkWay.Phone;
            strParm[31] = visitRecord.LinkWay.OtherLinkway;
            strParm[32] = visitRecord.User01;
            strParm[33] = visitRecord.User02;
            strParm[34] = visitRecord.User03;

            #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

            strParm[35] = visitRecord.VisitResult.ID;

            #endregion

            //��������
            return strParm;

        }

        /// <summary>
        /// �������ϸ֢״���ֱ��в���һ���¼�¼,����ü�¼��ϸ��ID���ڳ���ʵ���SortID��
        /// </summary>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int InsertSymptom(FS.HISFC.Models.Base.Const symptomInfo)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.InsertSymptom", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.InsertSymptom�ֶ�!";
                return -1;
            }
            try
            {
                //���ݲ���
                strSQL = string.Format(strSQL, FS.FrameWork.Function.NConvert.ToInt32(symptomInfo.SortID), symptomInfo.ID, symptomInfo.Name);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����!" + ex.Message;
                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ������ü�¼��ϸIDɾ�������е�֢״���ּ�¼
        /// </summary>
        /// <param name="recordID">��ü�¼��ϸID</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int DeleteSymptom(int recordID)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.DeleteSymptom", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.DeleteSymptom�ֶ�!";
                return -1;
            }
            try
            {
                //���ݲ���
                strSQL = string.Format(strSQL, recordID);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����!" + ex.Message;
                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region  ��ѯ

        /// <summary>
        /// ��ѯ�����ϸ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="type">A ȫ����1 �н����0 �޽��</param>
        /// <param name="cardNo">������</param>
        /// <returns>���ز�ѯ�����顢ʧ�ܷ���null</returns>
        public ArrayList Select(DateTime beginTime, DateTime endTime, char type, string cardNo)
        {
            string strSQL = "";
            string strSQL1 = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.Select�ֶΣ�";
                return null;
            }

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.Where", ref strSQL1) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.Where�ֶΣ�";
                return null;
            }

            try
            {
                //������� {D943E66E-9E08-4e06-9052-B79388DAB7B4}
                strSQL1 = string.Format(strSQL1, beginTime, endTime, type, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return null;
            }

            strSQL = strSQL + "\n" + strSQL1;

            //ִ��SQL���
            return ExecQueryBySQL(strSQL);
        }

        /// <summary>
        /// ����SQl����ȡ�����ϸ��Ϣ
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        private ArrayList ExecQueryBySQL(string strSQL)
        {
            this.ExecQuery(strSQL);

            ArrayList list = new ArrayList();
            FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord = null;

            try
            {
                while (this.Reader.Read())
                {
                    visitRecord = new FS.HISFC.Models.HealthRecord.Visit.VisitRecord();

                    visitRecord.ID = this.Reader[0].ToString();
                    visitRecord.Patient.PID.CardNO = this.Reader[1].ToString();
                    visitRecord.Circs.ID = this.Reader[2].ToString();

                    
                    visitRecord.DeadReason.ID = this.Reader[3].ToString();
                    
                    visitRecord.DeadTime = NConvert.ToDateTime(this.Reader[4].ToString());
                    if (this.Reader[5].ToString().Equals("1"))
                    {
                        visitRecord.IsDead = true;
                    }
                    else
                    {
                        visitRecord.IsDead = false;
                    }
                    if (this.Reader[6].ToString().Equals("1"))
                    {
                        visitRecord.IsRecrudesce = true;
                    }
                    else
                    {
                        visitRecord.IsRecrudesce = false;
                    }
                    if (this.Reader[7].ToString().Equals("1"))
                    {
                        visitRecord.IsSequela = true;
                    }
                    else
                    {
                        visitRecord.IsSequela = false;
                    }
                    if (this.Reader[8].ToString().Equals("1"))
                    {
                        visitRecord.IsSuccess = true;
                    }
                    else
                    {
                        visitRecord.IsSuccess = false;
                    }
                    if (this.Reader[9].ToString().Equals("1"))
                    {
                        visitRecord.IsTransfer = true;
                    }
                    else
                    {
                        visitRecord.IsTransfer = false;
                    }
                    visitRecord.RecrudesceTime = NConvert.ToDateTime(this.Reader[10].ToString());
                    visitRecord.ResultOper.ID = this.Reader[11].ToString();
                    visitRecord.ResultOper.OperTime = NConvert.ToDateTime(this.Reader[12].ToString());
                    visitRecord.Sequela.ID = this.Reader[13].ToString();
                    
                    visitRecord.Symptom.ID = this.Reader[14].ToString();
                    visitRecord.TransferPosition.ID = this.Reader[15].ToString();
                    
                    visitRecord.VisitOper.ID = this.Reader[16].ToString();
                    visitRecord.VisitOper.OperTime = NConvert.ToDateTime(this.Reader[17].ToString());
                    visitRecord.VisitType.ID = this.Reader[18].ToString();

                    #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

                    //��÷�ʽ
                    visitRecord.VisitType.Name = this.Reader[44].ToString();
                    visitRecord.Symptom.Name = this.Reader[45].ToString();
                    visitRecord.VisitResult.Name = this.Reader[46].ToString();


                    #endregion

                    visitRecord.WriteBackPerson = this.Reader[19].ToString();
                    if (this.Reader[20].ToString().Equals("1"))
                    {
                        visitRecord.IsPassive = true;
                    }
                    else
                    {
                        visitRecord.IsPassive = false;
                    }
                    if (this.Reader[21].ToString().Equals("1"))
                    {
                        visitRecord.LinkWay.IsLinkMan = true;
                    }
                    else
                    {
                        visitRecord.LinkWay.IsLinkMan = false;
                    }
                    visitRecord.LinkWay.LinkMan.Name = this.Reader[22].ToString();
                    visitRecord.LinkWay.LinkWayType.ID = this.Reader[23].ToString();
                    visitRecord.LinkWay.Relation.ID = this.Reader[24].ToString();
                    if (this.Reader[25].ToString().Equals("1"))
                    {
                        visitRecord.IsResult = true;
                    }
                    else
                    {
                        visitRecord.IsResult = false;
                    }
                    visitRecord.LinkWay.ZIP = this.Reader[26].ToString();
                    //visitRecord.ResultOper.Name = this.Reader[37].ToString();
                    if (this.Reader[27].ToString().Equals("1"))
                    {
                        visitRecord.IsWorkLoad = true;
                    }
                    else
                    {
                        visitRecord.IsWorkLoad = false;
                    }
                    visitRecord.LinkWay.Address = this.Reader[28].ToString();
                    visitRecord.LinkWay.Mail = this.Reader[29].ToString();
                    visitRecord.LinkWay.Phone = this.Reader[30].ToString();
                    visitRecord.LinkWay.OtherLinkway = this.Reader[31].ToString();
                    visitRecord.User01 = this.Reader[32].ToString();
                    visitRecord.User02 = this.Reader[33].ToString();
                    visitRecord.User03 = this.Reader[34].ToString();
                    visitRecord.Circs.Name = this.Reader[35].ToString();
                    visitRecord.DeadReason.Name = this.Reader[36].ToString();
                    visitRecord.Sequela.Name = this.Reader[37].ToString();
                    visitRecord.TransferPosition.Name = this.Reader[38].ToString();
                    visitRecord.LinkWay.LinkWayType.Name = this.Reader[39].ToString();
                    visitRecord.LinkWay.Relation.Name = this.Reader[40].ToString();
                    visitRecord.ResultOper.Name = this.Reader[41].ToString();
                    visitRecord.VisitOper.Name = this.Reader[42].ToString();



                    list.Add(visitRecord);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ�����ϸ����" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return list;
        }

        /// <summary>
        /// ������ü�¼��ϸID��ȡ�����е�֢״����
        /// </summary>
        /// <param name="recordID">��ü�¼��ϸID</param>
        /// <returns>���ز�ѯ�����顢ʧ�ܷ���null</returns>
        public ArrayList SelectSymptom(int recordID)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.SelectSymptom", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.SelectSymptom�ֶΣ�";
                return null;
            }
            try
            {
                //�������
                strSQL = string.Format(strSQL, recordID);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return null;
            }

            //ִ��SQL���
            this.ExecQuery(strSQL);

            ArrayList list = new ArrayList();
            FS.HISFC.Models.Base.Const symptom = null;

            try
            {
                while (this.Reader.Read())
                {
                    symptom = new FS.HISFC.Models.Base.Const();
                    symptom.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());
                    symptom.ID = this.Reader[1].ToString();
                    symptom.Name = this.Reader[2].ToString();
                    list.Add(symptom);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ֢״���ֳ���" + ex.Message;

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
        /// ��ѯ����û����б�
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int PatQuery(ref System.Data.DataSet ds)
        {
            string strSQL = string.Empty;
            string strWhere = string.Empty;

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.Visit.PatQuery", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.Query�ֶ�!";
                return -1;
            }

            if (this.Sql.GetSql("HealthReacord.Visit.Visit.PatQueryWait", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.Visit.PatQueryWait�ֶ�!";
                return -1;
            }

            try
            {
                strWhere = string.Format(strWhere, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }


            //ִ��SQL��䲢����
            return this.ExecQuery(strSQL + strWhere, ref ds);
        }

        /// <summary>
        /// ��ѯ����û����б�
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int PatQueryAlready(ref System.Data.DataSet ds)
        {
            string strSQL = string.Empty;
            string strWhere = string.Empty;

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.Visit.PatQuery", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.Query�ֶ�!";
                return -1;
            }

            if (this.Sql.GetSql("HealthReacord.Visit.Visit.PatQueryAlready", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.Visit.PatQueryAlready�ֶ�!";
                return -1;
            }

            try
            {
                strWhere = string.Format(strWhere, this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }


            //ִ��SQL��䲢����
            return this.ExecQuery(strSQL + strWhere, ref ds);
        }

        /// <summary>
        /// ��ѯ����û����б�
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int PatQueryOff(ref System.Data.DataSet ds)
        {
            string strSQL = string.Empty;

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.Visit.PatQueryOff", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.PatQueryOff�ֶ�!";
                return -1;
            }



            //ִ��SQL��䲢����
            return this.ExecQuery(strSQL, ref ds);
        }

        /// <summary>
        /// ��ѯ�ѳ�Ժ���ߵ�������
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int OutPatientQuery(ref System.Data.DataSet ds)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.OutPatientSelect", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.OutPatientSelect�ֶ�!";
                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecQuery(strSQL, ref ds);
        }

        /// <summary>
        /// ��ICD��Χ��ѯ�ѳ�Ժ���ߵ�������
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int OutPatientQueryByICD(ref System.Data.DataSet ds,string icdRange)
        {
            string strSQL = "";
            string strWhere = string.Empty;

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.OutPatientSelect", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.OutPatientSelect�ֶ�!";
                return -1;
            }

            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.OutPatientWhereByICD", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.OutPatientWhereByICD�ֶ�!";
                return -1;
            }


            try
            {
                strWhere = string.Format(strWhere, icdRange);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }


            strSQL = strSQL + strWhere;

            //ִ��SQL��䲢����
            return this.ExecQuery(strSQL, ref ds);
        }


        /// <summary>
        /// �������Ų�ѯ�����ϸ
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>ʧ�ܷ��� -1</returns>
        public ArrayList QueryByCardNo(string cardNo)
        {
            string strSQL = "";
            string strSQL1 = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.Select�ֶΣ�";
                return null;
            }

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.WhereByCardNo", ref strSQL1) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.WhereByCardNo�ֶΣ�";
                return null;
            }

            try
            {
                strSQL1 = string.Format(strSQL1,cardNo);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return null;
            }

            strSQL = strSQL + "\n" + strSQL1;

            //ִ��SQL���
            return ExecQueryBySQL(strSQL);
        }

        /// <summary>
        /// ɾ�������ϸ
        /// </summary>
        /// <param name="recordID">�����ϸ��¼ID</param>
        /// <returns></returns>
        public int DelVisitRecord(string recordID)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.DeleteBySeqNo", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.DeleteBySeqNo�ֶ�!";
                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, recordID);
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
        /// ���´���û����б�
        /// </summary>
        /// <returns>�ɹ����� 0;ʧ�ܷ��� -1</returns>
        public int RefreshVisitList()
        {
            //������û���Ҫ��Ϣ
            System.Data.DataSet ds = new System.Data.DataSet(); 

            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.VisitSelect", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.VisitSelect�ֶ�!";
                return -1;
            }

            if (this.ExecQuery(strSQL, ref ds) ==-1)
            {
                this.Err = "��ȡ������û���Ϣʧ��";
                return -1;
            }

            if (ds.Tables.Count == 0)
            {
                this.Err = "��ȡ������û���Ϣʧ��";
                return -1;
            }

            foreach(System.Data.DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    InsertVisit(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }
                catch
                {
                    continue;
                }
            }

            return 0;
            

        }

        /// <summary>
        /// ��������û��б�
        /// </summary>
        /// <param name="patientNo">סԺ��</param>
        /// <param name="cardNo">������</param>
        /// <param name="lastTime">ĩ�����ʱ��</param>
        /// <returns>�ɹ����� 0 ; ʧ�ܷ��� -1</returns>
        public int InsertVisit(string patientNo,string cardNo,string lastTime)
        {
           
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitRecord.VisitInsert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitRecord.VisitInsert�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, patientNo, cardNo, lastTime);
            }
            catch(Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);

        }


        #endregion

        #endregion
    }
}
