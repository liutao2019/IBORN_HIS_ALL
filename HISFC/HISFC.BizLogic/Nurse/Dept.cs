using System;
using FS.HISFC.Models;
using System.Collections;
namespace FS.HISFC.BizLogic.Nurse
{
	/// <summary>
	/// ����̨��Ӧ���ҹ�����
	/// </summary>
	public class Dept:FS.FrameWork.Management.Database
	{
        #region ԭ����

    //    public Dept()
    //    {
    //        //
    //        // TODO: �ڴ˴���ӹ��캯���߼�
    //        //
    //    }
		
    //    #region ���ﻤʿվ��Ӧ�鿴�����б�

    //    public string GetCommonSqlDept() 
    //    {
    //        string strSql = "";
    //        if (this.Sql.GetCommonSql("Nurse.Dept.GetDeptInfo.Select",ref strSql)==-1) return null;
    //        return strSql;
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="NurseNo"></param>
    //    /// <returns></returns>
    //    public ArrayList GetDeptInfoByNurseNo(string NurseNo)
    //    {
    //        string strSql1="";
    //        string strSql2="";
    //        //�����Ŀ��ϸ��SQL���
    //        strSql1=this.GetCommonSqlDept();
    //        if(this.Sql.GetCommonSql("Nurse.Dept.GetDeptInfo.Where1",ref strSql2)==-1)return null;
    //        strSql1=strSql1+" "+strSql2;
    //        try
    //        {
    //            strSql1=string.Format(strSql1,NurseNo);
    //        }
    //        catch(Exception ex)
    //        {
    //            this.ErrCode = ex.Message;
    //            this.Err = ex.Message;
    //            return null;
    //        }			
    //        return this.GetDeptInfo(strSql1);
    //    }


    //    /// <summary>
    //    /// ���ݿ��Ҵ����ÿ��Ҷ�Ӧ�Ĳ�������
    //    /// </summary>
    //    /// <param name="deptID"></param>
    //    /// <returns></returns>
    //    public string GetNurseByDeptID(string deptID)
    //    {
    //        string sql = "";

    //        if(this.Sql.GetCommonSql("Nurse.Dept.GetDeptInfo.Where2",ref sql)==-1)return null;
			
    //        try
    //        {
    //            sql = string.Format(sql,deptID);
    //        }
    //        catch(Exception ex)
    //        {
    //            this.ErrCode = ex.Message;
    //            this.Err = ex.Message;
    //            return null;
    //        }			
			
    //        return this.ExecSqlReturnOne(sql,"-1");
			
    //    }
    //    /// <summary>
    //    /// ID:���ﻤʿվ����,Name:�Һſ���,user01:��ʾ˳��,02:����Ա,03:ʱ��
    //    /// </summary>
    //    /// <param name="strSql"></param>
    //    /// <returns></returns>
    //    private ArrayList GetDeptInfo(string strSql)
    //    {
    //        ArrayList al = new ArrayList();
    //        FS.FrameWork.Models.NeuObject obj;
    //        this.ExecQuery(strSql);
    //        while (this.Reader.Read()) 
    //        {
    //            #region
    ////				  NURSE_CELL_CODE,--		0 VARCHAR2(4)	N			���ﻤʿվ����
    ////             DEPT_CODE					1 VARCHAR2(4)	N			�Һſ���
    ////             SORT_ID						2 NUMBER(4)	Y			��ʾ˳��
    ////             OPER_CODE					3 VARCHAR2(6)	Y			����Ա
    ////             OPER_DATE					4 DATE	Y			����ʱ�� 
    //            #endregion
    //            obj = new FS.FrameWork.Models.NeuObject();
    //            try 
    //            {
    //                obj.ID = this.Reader[0].ToString();//���ﻤʿվ����,	--		��Ʊ��
	
    //                obj.Name = this.Reader[1].ToString();//�Һſ���

    //                obj.User01 = this.Reader[2].ToString();//��ʾ˳��

    //                obj.User02 = this.Reader[3].ToString();//����Ա
			
    //                obj.User03 = this.Reader[4].ToString();//����ʱ��				
					
    //            }

    //            catch(Exception ex) 
    //            {
    //                this.Err= "��ѯ��ϸ��ֵ����"+ex.Message;
    //                this.ErrCode=ex.Message;
    //                this.WriteErr();
    //                return null;
    //            }
				
    //            al.Add(obj);
    //        }
    //        this.Reader.Close();
    //        return al;
    //    }


    //    protected string [] myGetParmDeptInfo(FS.FrameWork.Models.NeuObject obj)
    //    {
    //        string[] strParm={	
    //                             obj.ID,
    //            obj.Name,
    //            obj.User01,
    //            obj.User02,
    //            obj.User03
												
    //                         };

    //        return strParm;

    //    }

    //    #endregion


		
    //    #region ��ʿվ��Ӧ�鿴���Ҳ���

    //    public int InsertDeptInfo(FS.FrameWork.Models.NeuObject obj)
    //    {
    //        string strSQL = "";
    //        //ȡ���������SQL���
    //        string[] strParam ;
    //        if(this.Sql.GetCommonSql("Nurse.Room.GetDeptInfo.Insert",ref strSQL) == -1) 
    //        {
    //            this.Err = "û���ҵ��ֶ�!";
    //            return -1;
    //        }
    //        try
    //        {
    //            if (obj.ID == null) return -1;
    //            strParam = this.myGetParmDeptInfo(obj); 
				
    //        }
    //        catch(Exception ex)
    //        {
    //            this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
    //            this.WriteErr();
    //            return -1;
    //        }
    //        return this.ExecNoQuery(strSQL,strParam);
			
    //    }

    //    /// <summary>
    //    /// ɾ��������Ϣ
    //    /// </summary>
    //    /// <param name="nurseNo"></param>
    //    /// <returns></returns>
    //    public int DelDeptInfo(string nurseNo)
    //    {
    //        string strSql = "";
    //        if (this.Sql.GetCommonSql("Nurse.DelDeptInfo.1",ref strSql)==-1) return -1;
    //        try
    //        {
    //            strSql = string.Format(strSql,nurseNo);
    //        }
    //        catch(Exception ex)
    //        {
    //            this.Err=ex.Message;
    //            this.ErrCode=ex.Message;
    //            return -1;
    //        }
    //        return this.ExecNoQuery(strSql);
    //    }


    //    #endregion

        #endregion

        public Dept()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ���ﻤʿվ��Ӧ�鿴�����б�

        public string GetCommonSqlDept()
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Nurse.Dept.GetDeptInfo.Select", ref strSql) == -1) return null;
            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NurseNo"></param>
        /// <returns></returns>
        public ArrayList GetDeptInfoByNurseNo(string NurseNo)
        {
            string strSql1 = "";
            string strSql2 = "";
            //�����Ŀ��ϸ��SQL���
            strSql1 = this.GetCommonSqlDept();
            if (this.Sql.GetCommonSql("Nurse.Dept.GetDeptInfo.Where1", ref strSql2) == -1) return null;
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
            return this.GetDeptInfo(strSql1);
        }


        /// <summary>
        /// ���ݿ��Ҵ����ÿ��Ҷ�Ӧ�Ĳ�������
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public string GetNurseByDeptID(string deptID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Dept.GetDeptInfo.Where2", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, deptID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            return this.ExecSqlReturnOne(sql, "-1");

        }
        /// <summary>
        /// ID:���ﻤʿվ����,Name:�Һſ���,user01:��ʾ˳��,02:����Ա,03:ʱ��
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList GetDeptInfo(string strSql)
        {
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj;
            this.ExecQuery(strSql);
            while (this.Reader.Read())
            {
                #region
                //				  NURSE_CELL_CODE,--		0 VARCHAR2(4)	N			���ﻤʿվ����
                //             DEPT_CODE					1 VARCHAR2(4)	N			�Һſ���
                //             SORT_ID						2 NUMBER(4)	Y			��ʾ˳��
                //             OPER_CODE					3 VARCHAR2(6)	Y			����Ա
                //             OPER_DATE					4 DATE	Y			����ʱ�� 
                #endregion
                obj = new FS.FrameWork.Models.NeuObject();
                try
                {
                    obj.ID = this.Reader[0].ToString();//���ﻤʿվ����,	--		��Ʊ��

                    obj.Name = this.Reader[1].ToString();//�Һſ���

                    obj.User01 = this.Reader[2].ToString();//��ʾ˳��

                    obj.User02 = this.Reader[3].ToString();//����Ա

                    obj.User03 = this.Reader[4].ToString();//����ʱ��				

                }

                catch (Exception ex)
                {
                    this.Err = "��ѯ��ϸ��ֵ����" + ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    return null;
                }

                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }


        protected string[] myGetParmDeptInfo(FS.FrameWork.Models.NeuObject obj)
        {
            string[] strParm ={	
								 obj.ID,
				obj.Name,
				obj.User01,
				obj.User02,
				obj.User03
												
							 };

            return strParm;

        }

        #endregion



        #region ��ʿվ��Ӧ�鿴���Ҳ���

        public int InsertDeptInfo(FS.FrameWork.Models.NeuObject obj)
        {
            string strSQL = "";
            //ȡ���������SQL���
            string[] strParam;
            if (this.Sql.GetCommonSql("Nurse.Room.GetDeptInfo.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ��ֶ�!";
                return -1;
            }
            try
            {
                if (obj.ID == null) return -1;
                strParam = this.myGetParmDeptInfo(obj);

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
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="nurseNo"></param>
        /// <returns></returns>
        public int DelDeptInfo(string nurseNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Nurse.DelDeptInfo.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, nurseNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }


        #endregion



    }
}
