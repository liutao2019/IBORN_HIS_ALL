using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
    /// <summary>
    /// Bed ��ժҪ˵����
    /// </summary>
    public class Bed : DataBase
    {
        public Bed()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        /// <summary>
        /// ȡ������λ��Ϣ��sql���
        /// </summary>
        /// <returns></returns>
        private string myGetQueryString()
        {
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetBedList", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            return strSql;
        }


        /// <summary>
        /// ��ʽ������:���ºͲ���ʱ����
        /// </summary>
        /// <param name="objBed"></param>
        /// <returns></returns>
        private string[] myGetParm(FS.HISFC.Models.Base.Bed objBed)
        {
            string[] strParm ={   objBed.ID,						//0 ����
								 objBed.NurseStation.ID,		//1 ����վ����
								 objBed.BedGrade.ID,			//2 ��λ�ȼ�����
								 objBed.BedRankEnumService.ID.ToString(),	//3 ��λ���Ʊ���
								 objBed.Status.ID.ToString(),//4 ��λ״̬����
								 FS.FrameWork.Function.NConvert.ToInt32(objBed.IsValid).ToString(),	//5 �Ƿ���Ч
								 objBed.SickRoom.ID,				//6 ����
								 objBed.Doctor.ID,				//7 ҽ������
								 objBed.Phone,					//8 �绰
								 objBed.OwnerPc,				//9 ����
								 objBed.SortID.ToString(),		//10���
								 this.Operator.ID,				//11����Ա
								 objBed.InpatientNO,			//12����סԺ��
								 FS.FrameWork.Function.NConvert.ToInt32(objBed.IsPrepay).ToString(),	//13�Ƿ�ԤԼ
								 objBed.PrepayOutdate.ToString(),//14ԤԼ����
								 objBed.AdmittingDoctor.ID,		//סԺҽ��
								 objBed.AttendingDoctor.ID,		//����ҽ��
								 objBed.ConsultingDoctor.ID,	//����ҽ��
								 objBed.AdmittingNurse.ID,		//���λ�ʿ
								 objBed.TendGroup,				//������
                                 //{4A0E8D9F-2FF5-4fc5-A050-8AA719E4D302}
                                 objBed.Status.User03 ==string.Empty?"ALL":objBed.Status.User03
							 };
            return strParm;
        }


        /// <summary>
        /// ���Ӵ�λ��Ϣ
        /// </summary>
        /// <param name="objBed"></param>
        /// <returns></returns>
        public int CreatBedInfo(FS.HISFC.Models.Base.Bed objBed)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Bed.CreatBedInfo.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                //�������С����λ,����(������λ��ʱ��)ǰ��λ�����ڻ���վ����ʱ,���ɴ���10λ:����վ����+����
                if (objBed.ID.Length <= 4 || objBed.ID.Substring(0, 4) != objBed.NurseStation.ID)
                {
                    //���ɴ���10λ:����վ����+����(����λ)
                    objBed.ID = objBed.NurseStation.ID + (objBed.ID.Length > 6 ? objBed.ID.Substring(0, 6) : objBed.ID);
                }

                try
                {
                    string[] strParm = myGetParm(objBed);  //ȡ�����б�
                    strSql = string.Format(strSql, strParm);

                }
                catch (Exception ex)
                {
                    this.ErrCode = ex.Message;
                    this.Err = ex.Message;
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }

            //ִ��SQL���
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ���Ĵ�λ��Ϣ
        /// </summary>
        /// <param name="objBed"></param>
        /// <returns></returns>
        public int UpdateBedInfo(FS.HISFC.Models.Base.Bed objBed)
        {
            string strSql = "";

            if (this.GetSQL("Manager.Bed.UpdateBedInfo.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                string[] strParm = myGetParm(objBed);  //ȡ�����б�

                strSql = string.Format(strSql, strParm);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            //ִ��SQL���
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ���洲λ��Ϣ������ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
        /// </summary>
        /// <param name="objBed">��λ��Ϣʵ��</param>
        /// <returns>0δ���£�����1�ɹ���-1ʧ��</returns>
        public int SetBedInfo(FS.HISFC.Models.Base.Bed objBed)
        {
            int parm;
            //ִ�и��²���
            parm = this.UpdateBedInfo(objBed);

            //���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (parm == 0)
            {
                parm = this.CreatBedInfo(objBed);
            }

            return parm;
        }


        /// <summary>
        /// ɾ����λ��Ϣ
        /// </summary>
        /// <param name="BedNo"></param>
        /// <returns></returns>
        public int DeleteBedInfo(string BedNo)
        {
            if (QuerySpecialBed(BedNo) > 0)
            {
                this.Err = "�ô�λ���ڰ�����Ҵ����ã��������⿪�������ɾ����";
                return -1;
            }
            string strSql = "";

            if (this.GetSQL("Manager.Bed.DeleteBedInfo.1", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, BedNo);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) <= 0)
            {
                return -1;
            }
            return 0;
        }


        /// <summary>
        /// �ж��Ƿ���ڰ������Ҵ�����
        /// </summary>
        /// <param name="BedNo"></param>
        /// <returns></returns>
        private int QuerySpecialBed(string BedNo)
        {
            string strSql = "";

            if (this.GetSQL("Manager.Bed.QuerySpecialBed.1", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, BedNo);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecQuery(strSql);
        }

        /// <summary>
        /// ���벡��ID�õ��������д�λ��Ϣ
        /// </summary>
        /// <param name="NurseStationId">ALL ��ѯ����</param>
        /// <returns></returns>
        public ArrayList GetBedList(string NurseStationId)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetBedList.1", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, NurseStationId);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(myGetQueryString() + " " + strSql) < 0)
                {
                    return null;
                }
                return myGetBedList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��û��ߵ���ٴ��Ͱ�����Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList GetOtherBedList(string inpatientNo)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetOtherBedList.Where", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, inpatientNo);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(myGetQueryString() + " " + strSql) < 0) return null;
                return myGetBedList();

            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// �մ���ѯ
        /// </summary>
        /// <param name="NurseStationId"></param>
        /// <returns></returns>
        public ArrayList GetFeeBedList(string NurseStationId)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetFeeBedList.1", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, NurseStationId);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                this.ExecQuery(strSql);
                FS.HISFC.Models.Base.Bed obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Base.Bed();
                    try
                    {
                        obj.ID = this.Reader[0].ToString();					//��λ����
                        obj.Name = obj.ID.Substring(4);						//ȥ������λ��Ϊ��λ����
                        obj.Name = this.Reader[0].ToString().Substring(4);	//�����λ�������4λ,��ӵ�5λ��ʼȡʣ���,����ȡȫ������
                        obj.BedGrade.ID = this.Reader[2].ToString();			//��λ�Ǽ�
                        obj.BedRankEnumService.ID = this.Reader[3].ToString();			//��λ����
                        obj.Status.ID = this.Reader[4].ToString();		//��λ״̬
                        obj.SickRoom.ID = this.Reader[5].ToString();				//����
                        obj.Doctor.ID = this.Reader[6].ToString();			//��λҽ��
                        obj.Phone = this.Reader[7].ToString();				//��λ�绰
                        obj.OwnerPc = this.Reader[8].ToString();				//����
                        obj.InpatientNO = this.Reader[9].ToString();			//����סԺ��ˮ��
                        obj.PrepayOutdate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());		//ԤԼ��Ժ����
                        //�Ƿ���Ч:0��Ч,1��Ч
                        if (this.Reader[11].ToString() == "0")
                        {
                            obj.IsValid = true;
                        }
                        else
                        {
                            obj.IsValid = false;
                        }
                        obj.IsPrepay = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[12].ToString());			//�Ƿ�ԤԼ
                        obj.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13].ToString());	//����
                        obj.NurseStation.ID = this.Reader[1].ToString();		//����վ
                        obj.User01 = this.Reader[14].ToString();			//�����˱���
                        obj.User02 = this.Reader[15].ToString();				//��������
                        obj.BedGrade.Name = this.Reader[16].ToString();		//��λ�ȼ�����
                        obj.User03 = this.Reader["sumfee"].ToString();		//��λ��
                        obj.AdmittingDoctor.ID = this.Reader[18].ToString();//סԺҽ��
                        obj.AttendingDoctor.ID = this.Reader[19].ToString();//����ҽ��
                        obj.ConsultingDoctor.ID = this.Reader[20].ToString();//����ҽ��
                        obj.AdmittingNurse.ID = this.Reader[21].ToString();	//���λ�ʿ
                        obj.User01 = this.Reader[22].ToString();
                        al.Add(obj);
                    }
                    catch (Exception ex)
                    {
                        this.Err = ex.Message;
                        this.WriteErr();
                        this.Reader.Close();
                        return null;
                    }
                }
                this.Reader.Close();
                return al;

            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// ���벡��ID�õ��������д�λ��Ϣ����λ״̬
        /// </summary>
        /// <param name="NurseStationId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public ArrayList GetBedList(string NurseStationId, string Status)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetBedList.2", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, NurseStationId, Status);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(myGetQueryString() + " " + strSql) < 0) return null;

                return myGetBedList();
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// ���벡��ID�õ��������пմ�λ��Ϣ
        /// </summary>
        /// <param name="NurseStationId"></param>
        /// <returns></returns>
        public ArrayList GetUnoccupiedBed(string NurseStationId)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetBedList.3", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, NurseStationId);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(myGetQueryString() + " " + strSql) < 0) return null;
                return myGetBedList();

            }
            else
            {
                return null;
            }
        }


        //�����Ų�ѯ��λ��Ϣ(���ſ���¼�뱾���������к�)
        public FS.HISFC.Models.Base.Bed GetBedInfo(string BedNo)
        {
            FS.HISFC.Models.Base.Bed obj = new FS.HISFC.Models.Base.Bed();

            //���ſ���¼�뱾���������к�
            if (BedNo.Length < 10)
            {
            }

            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetBedInfo.1", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, BedNo);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(myGetQueryString() + " " + strSql) < 0)
                    return null;

                ArrayList al = this.myGetBedList();

                if (al == null)
                    return null;
                else
                    return al[0] as FS.HISFC.Models.Base.Bed;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ��ò��������
        /// </summary>
        /// <param name="NurseStationId"></param>
        /// <returns></returns>
        public ArrayList GetBedRoom(string NurseStationId)
        {
            ArrayList al = new ArrayList();
            string strSql = "";

            if (this.GetSQL("Manager.Bed.GetBedRoom.1", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, NurseStationId);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(strSql) == -1) return null;
                while (this.Reader.Read())
                {
                    al.Add(this.Reader[0].ToString());
                }
                this.Reader.Close();
            }
            else
            {
                return null;
            }
            return al;
        }


        /// <summary>
        /// ���벡��ID�õ��������д�λ��Ϣ
        /// </summary>
        /// <param name="RoomId">������</param>
        /// <param name="NurseCellID">����վ����</param>
        /// <returns></returns>
        public ArrayList GetBedListByRoom(string RoomId, string NurseCellID)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetBedByRoom", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, RoomId, NurseCellID);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(myGetQueryString() + " " + strSql) < 0) return null;
                return myGetBedList();

            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// ͨ�����������û�����
        /// </summary>
        /// <param name="NurseCode"></param>
        /// <returns></returns>
        public ArrayList GetNurseTendGroupList(string NurseCode)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetNurseTendGroupList", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, NurseCode);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(strSql) < 0) return null;
                while (this.Reader.Read())
                {
                    al.Add(this.Reader[0].ToString());
                }

            }
            else
            {
                return null;
            }
            return al;
        }
        /// <summary>
        /// ��û�����
        /// </summary>
        /// <param name="BedID"></param>
        /// <returns></returns>
        public string GetNurseTendGroupFromBed(string BedID)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetNurseTendGroupFromBed", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, BedID);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return "-1";
                }
                if (this.ExecQuery(strSql) < 0) return null;
                if (this.Reader.Read() == false)
                {
                    return "";
                }
                while (this.Reader.Read())
                {
                    return this.Reader[0].ToString();
                }

            }
            else
            {
                return "-1";
            }
            return "";
        }

        /// <summary>
        /// ��ò�����������
        /// </summary>
        /// <param name="NurseCode"></param>
        /// <returns></returns>
        public ArrayList GetBedNurseTendGroupList(string NurseCode)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetNurseTendGroupList.1", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, NurseCode);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(strSql) < 0) return null;
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = (this.Reader[0].ToString());
                    obj.Name = this.Reader[1].ToString();
                    al.Add(obj);
                }

            }
            else
            {
                return null;
            }
            return al;
        }


        /// <summary>
        /// ��ò����б�ͨ��������
        /// </summary>
        /// <param name="NurseTendGroup"></param>
        /// <returns></returns>
        public ArrayList GetBedListFromNurseTendGroup(string NurseTendGroup)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetBedListFromNurseTendGroup", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, NurseTendGroup);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(strSql) < 0)
                    return null;
                while (this.Reader.Read())
                {
                    al.Add(this.Reader[0].ToString());
                }

            }
            else
            {
                return null;
            }
            return al;
        }


        /// <summary>
        /// ���²���������
        /// </summary>
        /// <param name="BedNo"></param>
        /// <returns></returns>
        public int UpdateNurseTendGroup(string BedNo, string NurseTendGroup)
        {
            string strSql = "";


            if (this.GetSQL("Manager.Bed.UpdateNurseTendGroup", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, BedNo, NurseTendGroup);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) <= 0)
            {
                return -1;
            }
            return 0;
        }


        /// <summary>
        /// �Ƿ��д��ڲ�����
        /// </summary>
        /// <param name="bedNo"></param>
        /// <returns></returns>
        public int IsExistBedNo(string bedNo)
        {
            int IsExist = 0;
            string strSql = "";
            if (this.GetSQL("Manager.Bed.GetBedListByRoom", ref strSql) == -1) return -1;
            try
            {
                if (bedNo != "")
                {
                    strSql = string.Format(strSql, bedNo);
                    this.ExecQuery(strSql);
                    while (this.Reader.Read())
                    {
                        IsExist = 1;
                    }
                    this.Reader.Close();
                }
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                IsExist = -1;
            }
            return IsExist;
        }


        /// <summary>
        /// ȡ��λ��Ϣ--˽�з���
        /// </summary>
        /// <returns>��λ����,���󷵻�null</returns>
        private ArrayList myGetBedList()
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.Bed obj = null;
            while (this.Reader.Read())
            {
                obj = new FS.HISFC.Models.Base.Bed();
                try
                {
                    obj.ID = this.Reader[0].ToString();					//��λ����
                    obj.Name = obj.ID.Substring(4);						//ȥ������λ��Ϊ��λ����
                    obj.NurseStation.ID = this.Reader[1].ToString();		//����վ
                    obj.BedGrade.ID = this.Reader[2].ToString();			//��λ�Ǽ�
                    obj.BedRankEnumService.ID = this.Reader[3].ToString();			//��λ����
                    obj.Status.ID = this.Reader[4].ToString();		//��λ״̬
                    obj.SickRoom.ID = this.Reader[5].ToString();				//����
                    obj.Doctor.ID = this.Reader[6].ToString();			//��λҽ��
                    obj.Phone = this.Reader[7].ToString();				//��λ�绰
                    obj.OwnerPc = this.Reader[8].ToString();				//����
                    obj.InpatientNO = this.Reader[9].ToString();			//����סԺ��ˮ��
                    obj.PrepayOutdate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());		//ԤԼ��Ժ����			
                    obj.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[11].ToString());			//�Ƿ���Ч:0��Ч,1��Ч
                    obj.IsPrepay = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[12].ToString());			//�Ƿ�ԤԼ
                    obj.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13].ToString());	//����
                    obj.User01 = this.Reader[14].ToString();			//�����˱���
                    obj.User02 = this.Reader[15].ToString();				//��������
                    obj.BedGrade.Name = this.Reader[16].ToString();		//��λ�ȼ�����
                    obj.User03 = this.Reader["sumfee"].ToString();		//��λ��
                    obj.Memo = this.Reader["specialFee"].ToString();	//�����
                    obj.AdmittingDoctor.ID = this.Reader[18].ToString();//סԺҽ��
                    obj.AttendingDoctor.ID = this.Reader[19].ToString();//����ҽ��
                    obj.ConsultingDoctor.ID = this.Reader[20].ToString();//����ҽ��
                    obj.AdmittingNurse.ID = this.Reader[21].ToString();	//���λ�ʿ
                    obj.TendGroup = this.Reader[22].ToString();			//������
                    al.Add(obj);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                    this.Reader.Close();
                    return null;
                }
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// ��ȡ��ʿվ�б�[2007/01/04 xuweizhe]
        /// </summary>
        /// <returns>nullʧ��</returns>
        public ArrayList QueryNurseStationInfo()
        {
            string strsql = "";
            if (this.GetSQL("Manager.Bed.GetNurseStationInfo", ref strsql) == -1)
            {
                return null;
            }
            if (this.ExecQuery(strsql) == -1)
            {
                return null;
            }
            ArrayList alBeds = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
                bed.ID = this.Reader.GetString(0);
                bed.Name = this.Reader.GetString(1);
                alBeds.Add(bed);
            }
            this.Reader.Close();
            return alBeds;
        }

        /// <summary>
        /// ��ȡ��λ��Ϣ,���ݻ�ʿվID[2007/01/04 XUWEIZHE]
        /// </summary>
        /// <param name="id">��ʿվID</param>
        /// <param name="dv">���ص�������ͼ</param>
        /// <returns>-1,ʧ��; 1,�ɹ�</returns>
        public int QueryBedInfoByNurseStationID(string id, ref System.Data.DataView dv)
        {
            string strsql = "";
            if (this.GetSQL("Manager.Bed.QueryBedInfoByNurseStationID", ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = String.Format(strsql, id);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            dv.Table = ds.Tables[0];
            return 1;

            #region 1111
            //ArrayList alBeds = new ArrayList();
            //while (this.Reader.Read())
            //{
            //    FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
            //    bed.ID = this.Reader.GetString(0);
            //    bed.Name = this.Reader.GetString(1);
            //    bed.BedRankEnumService.Name = this.Reader.GetString(2);
            //    bed.Status.Name = this.Reader.GetString(3);
            //    bed.Phone = this.Reader.GetString(4);
            //    bed.OwnerPc = this.Reader.GetString(5);
            //    bed.InpatientNO = this.Reader.GetString(6);//ҽ����ˮ��
            //    bed.PrepayOutdate = Convert.ToDateTime(this.Reader.GetString(7));//��Ժ����
            //    bed.IsValid = this.Reader.GetString(8) == "0" ? true : false;
            //    bed.IsPrepay = this.Reader.GetString(9) == "1" ? true : false;
            //    bed.SortID = this.Reader.GetInt32(10);
            //    bed.TendGroup = this.Reader.GetString(11);//������

            //    alBeds.Add(bed);
            //}
            //this.Reader.Close();
            //return alBeds;
            #endregion


        }

        /// <summary>
        /// ��ȡ���д�λ��Ϣ
        /// </summary>
        /// <param name="dv">���ص�������ͼ</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryBedInfo(ref System.Data.DataView dv)
        {
            string strsql = "";
            if (this.GetSQL("Manager.Bed.QueryBedInfo", ref strsql) == -1)
            {
                return -1;
            }
            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            dv.Table = ds.Tables[0];
            return 1;
        }
    }
}