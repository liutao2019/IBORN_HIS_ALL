using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.PhysicalExamination
{
    /// <summary>
    /// GroupDetail<br></br>
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
    public class GroupDetail : FS.FrameWork.Management.Database
    {
        #region ���к���
        /// <summary>
        /// ���ݿ��ұ����ȡ������Ŀ��Ϣ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryGroupTailByDeptID(string deptCode)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Exami.ChkGroupDetail.GetComGroupTailByDeptCode", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, deptCode);
                this.ExecQuery(strSql);
                List = new ArrayList();
                FS.HISFC.Models.PhysicalExamination.GroupDetail info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.PhysicalExamination.GroupDetail();
                    info.ID = Reader[0].ToString();
                    info.sequenceNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[1]);
                    info.itemCode = Reader[2].ToString();
                    info.drugFlag = Reader[3].ToString();
                    info.deptCode = Reader[4].ToString();
                    info.deptName = Reader[5].ToString();
                    info.qty = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6]);
                    info.unitFlag = Reader[7].ToString();
                    info.combNo = Reader[8].ToString();
                    info.reMark = Reader[9].ToString();
                    info.operCode = Reader[10].ToString();
                    info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11]);
                    List.Add(info);
                    info = null;
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            return List;
        }
        /// <summary>
        /// �õ��µ�ID
        /// </summary>
        /// <returns></returns>
        public string GetGroupID()
        {
            string ID = "";
            string strSql = "";
            if (this.Sql.GetSql("Exami.ChkGroupDetail.getGroupID", ref strSql) == -1) return null;
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
        /// <summary>
        /// �������׺Ż�ȡ������ϸ
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public ArrayList QueryGroupTailByGroupID(string GroupID)
        {
            string strSql = "";
            if (this.Sql.GetSql("Exami.ChkGroupDetail.GetComGroupTail", ref strSql) == -1) return null;
            strSql = string.Format(strSql, GroupID);
            return  QueryGroupDetail(strSql);
        }
        /// <summary>
        /// ����һ����ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertGroupTail(FS.HISFC.Models.PhysicalExamination.GroupDetail info)
        {
            string strSql = "";
            try
            {
                if (this.Sql.GetSql("Exami.ChkGroupDetail.InsertDataIntoComGroupTail", ref strSql) == -1) return -1;
                return this.ExecNoQuery(strSql, GetParam(info));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            
        }
        /// <summary>
        /// �޸�һ����ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGroupTail(FS.HISFC.Models.PhysicalExamination.GroupDetail info)
        {
            string strSql = "";
            try
            {
                if (this.Sql.GetSql("Exami.ChkGroupDetail.ModefyDataIntoComGroupTail", ref strSql) == -1) return -1;
                return this.ExecNoQuery(strSql, GetParam(info));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            //return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ɾ��һ����ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteGroupTail(FS.HISFC.Models.PhysicalExamination.GroupDetail info)
        {
            string strSql = "";
            try
            {
                // delete fin_com_groupdetail where group_id ='{0}' and sequence_no ='{1}' and PARENT_CODE ='[��������]' and CURRENT_CODE  ='[��������] 
                if (this.Sql.GetSql("Exami.ChkGroupDetail.DeleteDataIntoComGroupTail", ref strSql) == -1) return -1;
                string OperCode = this.Operator.ID;
                strSql = string.Format(strSql, info.ID, info.sequenceNo);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        #endregion 

        #region ˽�к���
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.PhysicalExamination.GroupDetail info)
        {
            string[] str = new string[]
					{
						info.ID, //���ױ��� 
						info.SortNum.ToString(),// ��� 
						info.itemCode, //����  
						info.Spacs, //��� 
						info.qty.ToString(),//��������
						info.reMark,//��ע
						info.ValidState, //ͣ�ñ�־0����/1ͣ��
						info.unitFlag,//-��Ŀ��־0����/1����/2����
						info.combNo,//��Ϻ�
						info.ChkTime.ToString(),//������
						this.Operator.ID, //����Ա
						info.ExecDept.ID,
						info.RealPrice.ToString() //ʵ�ʼ۸�
					};
            return str;
        }
        /// <summary>
        /// ���ݣӣѣ̻�ȡ������ϸ��
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList QueryGroupDetail(string strSql)
        {
            ArrayList List = null;
            try
            {
                this.ExecQuery(strSql);
                List = new ArrayList();
                FS.HISFC.Models.PhysicalExamination.GroupDetail info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.PhysicalExamination.GroupDetail();
                    info.ID = Reader[0].ToString(); //���ױ��� 
                    info.SortNum = FS.FrameWork.Function.NConvert.ToInt32(Reader[1]);// ��� 
                    info.itemCode = Reader[2].ToString(); //����  
                    info.Spacs = Reader[3].ToString(); //��� 
                    info.qty = FS.FrameWork.Function.NConvert.ToDecimal(Reader[4]); //��������
                    info.reMark = Reader[5].ToString();//��ע
                    info.ValidState = Reader[6].ToString(); //ͣ�ñ�־0����/1ͣ��
                    info.unitFlag = Reader[7].ToString();//-��Ŀ��־0����/1����/2����
                    info.combNo = Reader[8].ToString();//��Ϻ�
                    info.ChkTime = FS.FrameWork.Function.NConvert.ToInt32(Reader[9]);//������
                    info.operCode = Reader[10].ToString();//����Ա
                    info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11]);//��������
                    info.ExecDept.ID = Reader[12].ToString();//ִ�п���
                    info.ExecDept.Name = Reader[13].ToString();//ִ�п���
                    info.RealPrice = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());
                    List.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            return List;
        }
        #endregion  
    }
}
