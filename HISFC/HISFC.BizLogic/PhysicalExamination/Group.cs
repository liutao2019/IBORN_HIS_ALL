using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.PhysicalExamination
{
    /// <summary>
    /// Group<br></br>
    /// [��������: ������׹�����]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-03-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public  class Group : FS.FrameWork.Management.Database
    {
        #region ˽�к���
        /// <summary>
        /// ��ȡ�� SQL��� 
        /// </summary>
        /// <returns></returns>
        private string GetGroupSql()
        {
            string strSql = "";
            if (this.Sql.GetSql("Exami.ChkGroup.GetAllGroups", ref strSql) == -1) return null;
            return strSql;
        }
        /// <summary>
        /// ���ײ���
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.PhysicalExamination.Group obj)
        {
            string[] str = new string[]
					{
						obj.ID, //���״��� 
						obj.Name ,//��������1
						obj.deptCode,//���Ҵ���2
						obj.spellCode,//ƴ����3 
						obj.WBCode,//�����4
                        obj.inputCode,//�Զ�����5
						obj.reMark, //��ע6
						obj.IsShare ,// --�Ƿ���,0�ǣ�1��7
						obj.OwnRate.ToString(),//�Էѱ��� 
						obj.PayRate.ToString(),//�Ը�����9
						obj.PubRate.ToString(),//���ѱ���10
						obj.EcoRate.ToString(),//�Żݱ���11
						((int)obj.ValidState).ToString(),//ͣ�ñ�־12
						this.Operator.ID//����Ա13
					};
            return  str;
        }
        /// <summary>
        /// ���ݣӣѣ̲�ѯ����
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList QueryGroup(string strSql)
        {
            ArrayList List = new ArrayList();
            try
            {
                if (this.ExecQuery(strSql) == -1) return null;
                
                FS.HISFC.Models.PhysicalExamination.Group info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.PhysicalExamination.Group();
                    info.ID = Reader[0].ToString(); //���״��� 
                    info.Name = Reader[1].ToString();//��������1
                    info.deptCode = Reader[2].ToString();//���Ҵ���2
                    info.spellCode = Reader[3].ToString();//ƴ����3 
                    info.inputCode = Reader[5].ToString();//�Զ�����5
                    info.WBCode = Reader[4].ToString();//�����4
                    info.reMark = Reader[6].ToString(); //��ע6
                    info.IsShare = Reader[7].ToString();// --�Ƿ���,0�ǣ�1��7
                    info.OwnRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8]);//�Էѱ��� 
                    info.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9]);//�Ը�����9
                    info.PubRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[10].ToString());//���ѱ���10
                    info.EcoRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11]);//�Żݱ���11
                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(FS.FrameWork.Function.NConvert.ToInt32(Reader[12].ToString()));//ͣ�ñ�־12
                    info.operCode = Reader[13].ToString();//����Ա13

                    List.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = "Exami.ChkGroup.GetAllGroups" + ee.Message;
                this.ErrCode = ee.Message;
                WriteErr();
                return null;
            }
            return List;
        }
        #endregion 

        #region ���к���
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllGroups()
        {
            string strSql = "";
            if (this.Sql.GetSql("Exami.ChkGroup.GetAllGroups", ref strSql) == -1) return null;
            return QueryGroup(strSql);
            //return List;
        }

        /// <summary>
        /// ��������ID��ȡ������Ϣ
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.PhysicalExamination.Group GetGroupByGroupID(string GroupID)
        {
            string strSql = "";
            FS.HISFC.Models.PhysicalExamination.Group info = new FS.HISFC.Models.PhysicalExamination.Group();
            string TempStr = GetGroupSql();
            if (TempStr == null)
            {
                return null;
            }
            if (this.Sql.GetSql("Exami.ChkGroup.GetAllGroups.where.2", ref strSql) == -1) return null;
            strSql = string.Format(strSql, GroupID);
            //��ȡSQL 
            strSql = TempStr + strSql;

            ArrayList list = this.QueryGroup(strSql);
            if (list == null)
            {
                return null;
            }
            if (list.Count == 0)
            {
                return info;
            }
            return (FS.HISFC.Models.PhysicalExamination.Group)list[0];
            
        }

        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertGroup(FS.HISFC.Models.PhysicalExamination.Group info)
        {
            string strSql = "";
            try
            {
                if (this.Sql.GetSql("Exami.ChkGroup.InsertInToComGroup", ref strSql) == -1) return -1;
                string OperCode = this.Operator.ID;
                return this.ExecNoQuery(strSql, GetParam(info));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            
        }

        /// <summary>
        /// �޸�һ����¼
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGroup(FS.HISFC.Models.PhysicalExamination.Group info)
        {
            string strSql = "";
            try
            {
                //update fin_com_group set GROUP_NAME ='{0}',SPELL_COD ='{1}',INPUT_CODE='{2}',GROUP_KIND='{3}',DEPT_CODE=(select dept_code from com_department where dept_name ='{4}' and PARENT_CODE ='[��������]' and CURRENT_CODE  ='[��������]') ,SORT_ID ={5},VALID_FLAG ='{6}',REMARK ='{7}',OPER_CODE ='{8}' where GROUP_ID ='{9}' and PARENT_CODE ='[��������]' and CURRENT_CODE  ='[��������]'
                if (this.Sql.GetSql("Exami.ChkGroup.ModefyComGroup", ref strSql) == -1) return -1;
                string OperCode = this.Operator.ID;
              
                return this.ExecNoQuery(strSql, GetParam(info));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            
        }

        /// <summary>
        /// ɾ��һ����¼
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public int DeleteGroup(FS.HISFC.Models.PhysicalExamination.Group com)
        {
            string strSql = "";
            try
            {
                //delete fin_com_group where group_id = '{0}'
                if (this.Sql.GetSql("Exami.ChkGroup.DeleteComGroup", ref strSql) == -1) return -1;
                strSql = string.Format(strSql, com.ID);
                if (this.ExecNoQuery(strSql) == -1) return -1;
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                WriteErr();
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ɾ������������ϸ
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public int DelGroupDetails(string groupID)
        {
            #region sql
            //DELETE FROM fin_com_groupdetail 
            //		WHERE parent_code='[��������]' and current_code='[��������]' and group_id='{0}'
            #endregion
            string strSql = "";

            if (Sql.GetSql("Exami.ChkGroupDetail.DeleteDataInfo", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, groupID);
                if (ExecNoQuery(strSql) == -1) return -1;
            }
            catch (Exception e)
            {
                this.Err = "Exami.ChkGroup.DeleteDetails!" + e.Message;
                WriteErr();
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// �����һ�ȡ������Ч����
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryValidGroupList(string deptID)
        {
            #region sql
            //			 SELECT group_id,   --����ID
            //					group_name,   --��������
            //					spell_code,   --����ƴ����
            //					input_code,   --����������
            //					group_kind,   --��������  0 .������  1 .������
            //					dept_code,   --���׿���
            //					sort_id,   --��ʾ˳��
            //					valid_flag,   --��Ч��־��1��Ч/2��Ч
            //					remark,   --���ױ�ע
            //					oper_code,   --����Ա
            //					oper_date    --����ʱ��
            //			   FROM fin_com_group   --������Ϣ��,���������շѡ�סԺ�շѡ���ʿվ�շѡ�����շѡ��������ס��ն�����
            //			  WHERE parent_code='[��������]' and current_code='[��������]' and dept_code='{0}' and valid_flag='1'
            #endregion
            string strSql = "";
            string TempStr = GetGroupSql();
            if (TempStr == null)
            {
                return null;
            }
            if (this.Sql.GetSql("Exami.ChkGroup.GetAllGroups.where.1", ref strSql) == -1) return null;
            strSql = TempStr + strSql;
            strSql = string.Format(strSql, deptID);
            return QueryGroup(strSql);
        }

        /// <summary>
        /// �����һ�ȡ��������
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllGroupListByDeptID(string deptID)
        {
            #region sql
            //			 SELECT group_id,   --����ID
            //					group_name,   --��������
            //					spell_code,   --����ƴ����
            //					input_code,   --����������
            //					group_kind,   --��������  0 .������  1 .������
            //					dept_code,   --���׿���
            //					sort_id,   --��ʾ˳��
            //					valid_flag,   --��Ч��־��1��Ч/2��Ч
            //					remark,   --���ױ�ע
            //					oper_code,   --����Ա
            //					oper_date    --����ʱ��
            //			   FROM fin_com_group   --������Ϣ��,���������շѡ�סԺ�շѡ���ʿվ�շѡ�����շѡ��������ס��ն�����
            //			  WHERE parent_code='[��������]' and current_code='[��������]' and dept_code='{0}' and valid_flag='1'
            #endregion
            string strSql = "";
            ArrayList group = new ArrayList();

            string TempStr = GetGroupSql();
            if (TempStr == null)
            {
                return null;
            }
            if (this.Sql.GetSql("Exami.ChkGroup.GetAllGroups.where.5", ref strSql) == -1) return null;
            strSql = string.Format(strSql, deptID);
            strSql = TempStr + strSql;
            return QueryGroup(strSql);
            
        }
        /// <summary>
        /// �õ��µ�ID
        /// </summary>
        /// <returns></returns>
        public string GetGroupID()
        {
            string ID = "";
            string strSql = "";  //select seq_comgroupid.nextval from dual
            if (this.Sql.GetSql("Manager.ComGroup.getGroupID", ref strSql) == -1) return null;
            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    ID = Reader[0].ToString();
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return "";
            }
            return ID;
        }
        #endregion  
    }
}
