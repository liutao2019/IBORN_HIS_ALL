using System;
using FS.HISFC.Models;
using System.Collections;
namespace FS.HISFC.BizLogic.Nurse
{
	/// <summary>
	/// ���ҹ�����
	/// </summary>
	public class Room:FS.FrameWork.Management.Database
    {
        #region ԭ����

//        public Room()
//        {
//            //
//            // TODO: �ڴ˴���ӹ��캯���߼�
//            //
//        }

//        #region �������������Ϣ

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public string GetCommonSqlRoomInfo() 
//        {
//            string strSql = "";
//            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Select",ref strSql)==-1) return null;
//            return strSql;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="NurseNo"></param>
//        /// <returns></returns>
//        public ArrayList GetRoomInfoByNurseNo(string NurseNo)
//        {
//            string strSql1="";
//            string strSql2="";
//            //�����Ŀ��ϸ��SQL���
//            strSql1=this.GetCommonSqlRoomInfo();
//            if(this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Where1",ref strSql2)==-1)return null;
//            strSql1=strSql1+" "+strSql2;
//            try
//            {
//                strSql1=string.Format(strSql1,NurseNo);
//            }
//            catch(Exception ex)
//            {
//                this.ErrCode = ex.Message;
//                this.Err = ex.Message;
//                return null;
//            }			
//            return this.GetRoomInfo(strSql1);
//        }


//        /// <summary>
//        /// ���ݿ��һ�ȡ�����б�
//        /// </summary>
//        /// <param name="deptID"></param>
//        /// <returns></returns>
//        public ArrayList GetRoomsByDeptID(string deptID)
//        {
//            string strSql1="";
//            string strSql2="";
//            //�����Ŀ��ϸ��SQL���
//            strSql1=this.GetCommonSqlRoomInfo();
//            if(this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Where2",ref strSql2)==-1)return null;
//            strSql1=strSql1+" "+strSql2;
//            try
//            {
//                strSql1=string.Format(strSql1,deptID);
//            }
//            catch(Exception ex)
//            {
//                this.ErrCode = ex.Message;
//                this.Err = ex.Message;
//                return null;
//            }			
//            return this.GetRoomInfo(strSql1);
//        }

//        private ArrayList GetRoomInfo(string strSql)
//        {
//            ArrayList al = new ArrayList();
//            FS.HISFC.Models.Nurse.Room objRoom ;
//            this.ExecQuery(strSql);
//            while (this.Reader.Read()) 
//            {
//                #region
////								DEPT_CODE	--VARCHAR2(4)	N			�������
////					ROOM_ID	--VARCHAR2(4)	N			���Ҵ���
////					ROOM_NAME	--VARCHAR2(20)	Y			��������
////					INPUT_CODE	--VARCHAR2(8)	Y			������
////					VALID_FLAG	--VARCHAR2(1)	Y			1��Ч/0��Ч
////					SORT_ID	--NUMBER(4)	Y			��ʾ˳��
////					OPER_CODE	--VARCHAR2(6)	Y			����Ա
////					OPER_DATE	--DATE	Y			����ʱ��
//                #endregion
//                objRoom = new FS.HISFC.Models.Nurse.Room();
//                try 
//                {
//                    objRoom.Nurse.ID = this.Reader[0].ToString();//RECIPE_NO,	--		��Ʊ��
	
//                    objRoom.ID = this.Reader[1].ToString();//���Ҵ���

//                    objRoom.Name = this.Reader[2].ToString();//��������

//                    objRoom.InputCode = this.Reader[3].ToString();//������
			
//                    objRoom.IsValid = this.Reader[4].ToString();//�Ƿ���Ч

//                    objRoom.Sort = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//���

//                    objRoom.User01 = this.Reader[6].ToString();//����Ա

//                    objRoom.User02 = this.Reader[7].ToString();//����ʱ��
					
//                }

//                catch(Exception ex) 
//                {
//                    this.Err= "��ѯ������ϸ��ֵ����"+ex.Message;
//                    this.ErrCode=ex.Message;
//                    this.WriteErr();
//                    return null;
//                }
				
//                al.Add(objRoom);
//            }
//            this.Reader.Close();
//            return al;
//        }


//        protected string [] myGetParmRoomInfo(FS.HISFC.Models.Nurse.Room obj)
//        {
//            string[] strParm={	
//                                 obj.Nurse.ID,
//                                    obj.ID,
//                obj.Name,
//                obj.InputCode,
//                obj.IsValid,
//                obj.Sort.ToString(),
//                obj.User01,
//                obj.User02
												
//                             };

//            return strParm;

//        }

//        #endregion


//        #region ����

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public int InsertRoomInfo(FS.HISFC.Models.Nurse.Room obj)
//        {
//                string strSQL = "";
//                //ȡ���������SQL���
//                string[] strParam ;
//                if(this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Insert",ref strSQL) == -1) 
//                {
//                    this.Err = "û���ҵ��ֶ�!";
//                    return -1;
//                }
//                try
//                {
//                    if (obj.ID == null) return -1;
//                    strParam = this.myGetParmRoomInfo(obj); 
				
//                }
//                catch(Exception ex)
//                {
//                    this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
//                    this.WriteErr();
//                    return -1;
//                }
//                return this.ExecNoQuery(strSQL,strParam);
			
//        }

//        /// <summary>
//        /// ɾ��������Ϣ
//        /// </summary>
//        /// <param name="nurseNo"></param>
//        /// <returns></returns>
//        public int DelRoomInfo(string nurseNo)
//        {
//            string strSql = "";
//            if (this.Sql.GetCommonSql("Nurse.DelRoomInfo.1",ref strSql)==-1) return -1;
//            try
//            {
//                strSql = string.Format(strSql,nurseNo);
//            }
//            catch(Exception ex)
//            {
//                this.Err=ex.Message;
//                this.ErrCode=ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(strSql);
//        }

//        #endregion
        #endregion

        public Room()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        #region ����

        /// <summary>
        /// ���ݻ���վ�����ȡ�����б�
        /// </summary>
        /// <param name="NurseNo"></param>
        /// <returns></returns>
        public ArrayList GetRoomInfoByNurseNo(string NurseNo)
        {
            string strSql1 = "";
            string strSql2 = "";
            //�����Ŀ��ϸ��SQL���
            strSql1 = this.GetCommonSqlRoomInfo();
            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Where1", ref strSql2) == -1) return null;
            strSql1 = strSql1 + " " + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, NurseNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            return this.GetRoomInfo(strSql1);

        }
        /// <summary>
        /// ���ݻ���վ�����ȡ��Ч�����б�
        /// </summary>
        /// <param name="NurseNo"></param>
        /// <returns></returns>
        public ArrayList GetRoomInfoByNurseNoValid(string NurseNo)
        {
            string strSql1 = "";
            string strSql2 = "";
            //�����Ŀ��ϸ��SQL���
            strSql1 = this.GetCommonSqlRoomInfo();
            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Where3", ref strSql2) == -1) return null;
            strSql1 = strSql1 + " " + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, NurseNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            return this.GetRoomInfo(strSql1);
        }
        /// <summary>
        /// ���ݿ��һ�ȡ�����б�
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList GetRoomsByDeptID(string deptID)
        {
            string strSql1 = "";
            string strSql2 = "";
            //�����Ŀ��ϸ��SQL���
            strSql1 = this.GetCommonSqlRoomInfo();
            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Where1", ref strSql2) == -1) return null;
            strSql1 = strSql1 + " " + strSql2;
            try
            {
                strSql1 = string.Format(strSql1, deptID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            return this.GetRoomInfo(strSql1);
        }
        /// <summary>
        /// ����һ��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertRoomInfo(FS.HISFC.Models.Nurse.Room obj)
        {
            string strSQL = "";
            //ȡ���������SQL���
            string[] strParam;
            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ��ֶ�!";
                return -1;
            }
            try
            {
                if (obj.ID == null) return -1;
                strParam = this.myGetParmRoomInfo(obj);

            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL, strParam);

        }
        /// <summary>
        /// ���µǼ���Ϣ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.Nurse.Room info)
        {
            string sql = "";
            string[] strParam;
            if (this.Sql.GetCommonSql("Nurse.Room.Update.1", ref sql) == -1) return -1;

            try
            {
                if (info.ID == null) return -1;
                strParam = this.myGetParmRoomInfo(info);

            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql, strParam);
        }

        /// <summary>
        /// ɾ��������Ϣ(�������Ҵ���)
        /// </summary>
        /// <param name="nurseNo"></param>
        /// <returns></returns>
        public int DelRoomInfo(string RoomNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Nurse.DelRoomInfo.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, RoomNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���������µ���̨״̬
        /// </summary>
        /// <param name="roomID">���ұ��</param>
        /// <param name="isVlid">�Ƿ���Ч(1����Ч�� 0����Ч)</param>
        /// <returns>-1,ʧ�ܣ�</returns>
        public int UpdateSeatByRoom(string roomID, string isvalid)
        {
            string strsql = "";
            if (this.Sql.GetCommonSql("UpdateSeatStateByRoomID", ref strsql) == -1)
            {
                this.Err = "�õ�UpdateSeatStateByRoomIDʧ��";
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, isvalid, roomID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strsql);
        }
        /// <summary>
        /// ��ѯҪɾ���������Ƿ񱻶���ά��
        /// </summary>
        /// <param name="roomID">����</param>
        /// <param name="strDate">ϵͳʱ��</param>
        /// <returns></returns>
        public int QueryRoom(string roomID,string strDate)
        {
            string strsql = "";
            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomUsed", ref strsql) == -1)
            {
                this.Err = "�õ�Nurse.Room.GetRoomUsedʧ��";
                return -1;
            }
            try
            {
                strsql = string.Format(strsql,roomID,strDate);

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return  FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
        }

        /// <summary>
        ///���ݿ��ң����������ж������Ƿ����
        /// </summary>
        /// <param name="deptID">���Ҵ���</param>
        /// <param name="roomName">��������</param>
        /// <returns></returns>
        public int QueryRoomByNameAndDept(string roomID, string deptID,string roomName)
        {
            string strsql = string.Empty;
            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomByNameAndDept", ref strsql) == -1)
            {
                this.Err = "�õ�Nurse.Room.GetRoomByNameAndDeptʧ��";
                return -1;
            }

            try
            {
                strsql = string.Format(strsql,roomID,deptID,roomName);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
 
        }
        /// <summary>
        /// ��ѯmet_nuo_assignrecord���Ƿ��з��������������Ƿ�����
        /// </summary>
        /// <param name="roomID">���Ҵ���</param>
        /// <returns></returns>
        public int QueryRoomByRoomID(string roomID,string currentDT)
        {
            string strsql = string.Empty;
            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomByRoomID", ref strsql) == -1)
            {
                this.Err = "�õ�Nurse.Room.GetRoomByRoomIDʧ��";
                return -1;
            }

            try
            {
                strsql = string.Format(strsql, roomID,currentDT);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
        }
        

           
        
        #endregion

        #region ����

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCommonSqlRoomInfo()
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Nurse.Room.GetRoomInfo.Select", ref strSql) == -1) return null;
            return strSql;
        }
        /// <summary>
        /// ת��Ϊʵ��
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList GetRoomInfo(string strSql)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Nurse.Room objRoom;
            this.ExecQuery(strSql);
            while (this.Reader.Read())
            {
                #region
                //								DEPT_CODE	--VARCHAR2(4)	N			�������
                //					ROOM_ID	--VARCHAR2(4)	N			���Ҵ���
                //					ROOM_NAME	--VARCHAR2(20)	Y			��������
                //					INPUT_CODE	--VARCHAR2(8)	Y			������
                //					VALID_FLAG	--VARCHAR2(1)	Y			1��Ч/0��Ч
                //					SORT_ID	--NUMBER(4)	Y			��ʾ˳��
                //					OPER_CODE	--VARCHAR2(6)	Y			����Ա
                //					OPER_DATE	--DATE	Y			����ʱ��
                #endregion
                objRoom = new FS.HISFC.Models.Nurse.Room();
                try
                {
                    objRoom.Nurse.ID = this.Reader[0].ToString();//RECIPE_NO,	--		�������

                    objRoom.ID = this.Reader[1].ToString();//���Ҵ���

                    objRoom.Name = this.Reader[2].ToString();//��������

                    objRoom.InputCode = this.Reader[3].ToString();//������

                    objRoom.IsValid = this.Reader[4].ToString();//�Ƿ���Ч

                    objRoom.Sort = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//���

                    objRoom.User01 = this.Reader[6].ToString();//����Ա

                    objRoom.User02 = this.Reader[7].ToString();//����ʱ��

                }

                catch (Exception ex)
                {
                    this.Err = "��ѯ������ϸ��ֵ����" + ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    return null;
                }

                al.Add(objRoom);
            }
            this.Reader.Close();
            return al;
        }


        protected string[] myGetParmRoomInfo(FS.HISFC.Models.Nurse.Room obj)
        {
            string[] strParm ={	
								 obj.Nurse.ID,
								 obj.ID,
								 obj.Name,
								 obj.InputCode,
								 obj.IsValid,
								 obj.Sort.ToString(),
								 obj.User01,
								 obj.User02
												
							 };

            return strParm;

        }

        #endregion
    }
}