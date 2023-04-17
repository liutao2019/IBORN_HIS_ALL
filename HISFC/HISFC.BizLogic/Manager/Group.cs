using System;
using System.Collections;
//using FS.FrameWork.Models;
//using FS.HISFC.Models;
//using FS.HISFC.Models.Order;
namespace FS.HISFC.BizLogic.Manager
{

    /// <summary>
    /// ���׹���
    /// </summary>
    public class Group : DataBase
    {
        /// <summary>
        ///ҽ�� 
        /// </summary>
        public Group()
        {
        }

        #region "���"
        /// <summary>
        /// ���ҽ���������
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllOrderGroup(FS.HISFC.Models.Base.ServiceTypes t)
        {
            string sql = "", sqlWhere = "";

            #region �ӿ�˵��
            //0 : ����ID                                1 : ��������                                2 : ����ƴ����                               
            //3 : ����������                               4 : 1����/2סԺ                             5 : ��������,1.ҽʦ���ף�2.��������                  
            //6 : ���Ҵ���                                7 : ����ҽʦ                                8 : �Ƿ���1�ǣ�0��                          
            //9 : ���ױ�ע    
            #endregion

            if (this.GetSQL("Manager.Group.Select", ref sql) == -1)
                return null;

            #region �ӿ�˵��
            //ҽ����� ����/סԺ
            #endregion

            if (this.GetSQL("Manager.Group.Where.1", ref sqlWhere) == -1)
                return null;
            sql = sql + " " + sqlWhere;
            try
            {
                sql = string.Format(sql, t.GetHashCode().ToString());
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "��ֵ����!" + ex.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetGroup(sql);
        }


        /// <summary>
        /// ���ҽ���������
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllPCCOrderGroup(FS.HISFC.Models.Base.ServiceTypes t)
        {
            string sql = "", sqlWhere = "";

            if (this.GetSQL("Manager.Group.Select", ref sql) == -1)
                return null;

            #region �ӿ�˵��
            //ҽ����� ����/סԺ
            #endregion

            if (this.GetSQL("Manager.Group.Where.1", ref sqlWhere) == -1)
                return null;
            sql = sql + " " + sqlWhere + @"and exists (select * from met_com_groupdetail f
                                            where f.groupid=met_com_group.GROUPID
                                            and f.class_code='PCC')
                                            order by spell_code";
            try
            {
                sql = string.Format(sql, t.GetHashCode().ToString());
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "��ֵ����!" + ex.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetGroup(sql);
        }

        protected ArrayList myGetGroup(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            //			if(this.Reader.ra == false) return al;
            #region �ӿ�˵��
            //0 : ����ID                                1 : ��������                                2 : ����ƴ����                               
            //3 : ����������                               4 : 1����/2סԺ                             5 : ��������,1.ҽʦ���ף�2.��������                  
            //6 : ���Ҵ���                                7 : ����ҽʦ                                8 : �Ƿ���1�ǣ�0��                          
            //9 : ���ױ�ע    
            #endregion
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Group info = new FS.HISFC.Models.Base.Group();
                try
                {
                    info.ID = this.Reader[0].ToString();
                    info.Name = this.Reader[1].ToString();
                    try
                    {
                        info.SpellCode = this.Reader[2].ToString();
                        info.WBCode = this.Reader[3].ToString();
                    }
                    catch { }
                    try
                    {
                        info.UserType = (FS.HISFC.Models.Base.ServiceTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());
                    }
                    catch { }
                    try
                    {
                        info.Kind = (FS.HISFC.Models.Base.GroupKinds)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());
                    }
                    catch { }
                    info.Dept.ID = this.Reader[6].ToString();
                    info.Doctor.ID = this.Reader[7].ToString();
                    info.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                    info.Memo = this.Reader[9].ToString();
                    if (this.Reader.FieldCount > 12)
                    {
                        info.ParentID = this.Reader[12].ToString();
                    }
                }
                catch { }
                al.Add(info);
            }
            this.Reader.Close();
            return al;
        }


        /// <summary>
        /// ��ѯ�����������������
        /// Add By liangjz 2005-10
        /// </summary>
        /// <param name="t">������� ����/סԺ</param>
        /// <param name="deptCode">���Ҵ���</param>
        /// <param name="doctCode">ҽ������</param>
        /// <returns>�ɹ����������б� ʧ�ܷ���null</returns>
        public ArrayList GetDeptOrderGroup(FS.HISFC.Models.Base.ServiceTypes t, string deptCode, string doctCode)
        {
            string sql = "", sqlWhere = "";

            #region  �ӿ�˵��
            //0 : ����ID                                1 : ��������                                2 : ����ƴ����                               
            //3 : ����������                               4 : 1����/2סԺ                             5 : ��������,1.ҽʦ���ף�2.��������                  
            //6 : ���Ҵ���                                7 : ����ҽʦ                                8 : �Ƿ���1�ǣ�0��                          
            //9 : ���ױ�ע    
            #endregion
            if (this.GetSQL("Manager.Group.Select", ref sql) == -1)
                return null;
            #region  �ӿ�˵��
            //ҽ����� ����/סԺ ���Ҵ��� ҽ������
            #endregion
            if (this.GetSQL("Manager.Group.Where.2", ref sqlWhere) == -1)
                return null;
            sql = sql + " " + sqlWhere;
            try
            {
                sql = string.Format(sql, t.GetHashCode().ToString(), deptCode, doctCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "��ֵ����!" + ex.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetGroup(sql);
        }

        /// <summary>
        /// �����Ŀ
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllItem(FS.HISFC.Models.Base.Group group)
        {
            string sql = "";
            #region �ӿ�˵��
            //0 �����ڵ�����ˮ��,
            //1 ��Ŀ����, 2 ҽ������,3 ��ҩƵ��,    4 ��ҩ����       5 ÿ�η��ü���
            //6 ������λ���Ա�ҩʹ��,7 ��������,8 ������λ���Ա���Ŀʹ��,9 ��ҩ����(����)
            //10 �����ˮ��,11 ��ҩ���,12 ��鲿λ����,13 ִ�п���,14 ҽ����ʼʱ��,15 ҽ������ʱ��
            //16 ҽ����ע,17 ҩƷ���ҽ����ע
            #endregion
            if (this.GetSQL("Manager.Group.Order.Item", ref sql) == -1)
                return null;
            try
            {
                sql = string.Format(sql, group.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "��ֵ����!" + ex.Message;
                this.WriteErr();
                return null;
            }
            if (group.UserType == FS.HISFC.Models.Base.ServiceTypes.I)
            {
                return this.MyGetInGroupItem(sql);
            }
            else
            {
                return this.MyGetOutGroupItem(sql);
            }
        }



        /// <summary>
        /// �����Ŀ
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllPCCItem(string groupID)
        {
            string sql = "";
            #region �ӿ�˵��
            //0 �����ڵ�����ˮ��,
            //1 ��Ŀ����, 2 ҽ������,3 ��ҩƵ��,    4 ��ҩ����       5 ÿ�η��ü���
            //6 ������λ���Ա�ҩʹ��,7 ��������,8 ������λ���Ա���Ŀʹ��,9 ��ҩ����(����)
            //10 �����ˮ��,11 ��ҩ���,12 ��鲿λ����,13 ִ�п���,14 ҽ����ʼʱ��,15 ҽ������ʱ��
            //16 ҽ����ע,17 ҩƷ���ҽ����ע
            #endregion
            if (this.GetSQL("Manager.Group.Order.Item", ref sql) == -1)
                return null;

            sql = sql + @"and r.class_code='PCC'  ";
            try
            {
                sql = string.Format(sql, groupID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "��ֵ����!" + ex.Message;
                this.WriteErr();
                return null;
            }
            return this.MyGetOutGroupItem(sql);
        }

        /// <summary>
        /// ��ȡסԺ������Ŀ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList MyGetInGroupItem(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            #region �ӿ�˵��
            //0 �����ڵ�����ˮ��,
            //1 ��Ŀ����, 2 ҽ������ ,3 ��ҩƵ��,    4 ��ҩ����       5 ÿ�η��ü���
            //6 ������λ���Ա�ҩʹ��,7 ��������,8 ������λ���Ա���Ŀʹ��,9 ��ҩ����(����)
            //10 �����ˮ��,11 ��ҩ���,12 ��鲿λ����,13 ִ�п���,14 ҽ����ʼʱ��,15 ҽ������ʱ��
            //16 ҽ����ע,17 ҩƷ���ҽ����ע
            #endregion

            while (this.Reader.Read())
            {
                FS.HISFC.Models.Order.Inpatient.Order info = new FS.HISFC.Models.Order.Inpatient.Order();
                try
                {
                    info.ID = this.Reader[1].ToString();
                    info.User01 = this.Reader[0].ToString();
                    info.OrderType.ID = this.Reader[2].ToString();
                    try
                    {
                        info.Frequency.ID = this.Reader[3].ToString();
                        info.Usage.ID = this.Reader[4].ToString();
                    }
                    catch { }
                    try
                    {
                        info.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                    }
                    catch { }
                    try
                    {
                        info.DoseUnit = this.Reader[6].ToString();
                    }
                    catch { }
                    try
                    {
                        info.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
                    }
                    catch { }
                    info.Unit = this.Reader[8].ToString();
                    try
                    {
                        info.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
                    }
                    catch { }
                    info.Combo.ID = this.Reader[10].ToString();
                    info.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[11].ToString());
                    info.CheckPartRecord = this.Reader[12].ToString();
                    info.ExeDept.ID = this.Reader[13].ToString();

                    //��ʼ��ֹͣʱ����ȡ
                    //info.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());
                    //info.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());
                    info.Memo = this.Reader[16].ToString();
                    info.Combo.Memo = this.Reader[17].ToString();
                    info.Usage.Name = this.Reader[18].ToString();
                    info.ExeDept.Name = this.Reader[19].ToString();
                    info.OrderType.Name = this.Reader[20].ToString();
                    info.Name = this.Reader[21].ToString();
                    try//ʱ����
                    {
                        info.User03 = this.Reader[22].ToString();
                        info.Item.SysClass.ID = this.Reader[23].ToString();
                        info.Item.User01 = this.Reader[24].ToString();

                        info.Item.ID = this.Reader[1].ToString();
                        info.Item.Name = this.Reader[21].ToString();
                        //{C9782537-9253-4bcd-BD8F-8BDF21A28D24}
                        if (info.Item.ID.Contains("Y"))
                        {
                            info.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                        }
                        else
                        {
                            info.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                        }
                    }
                    catch { }

                    if (this.Reader.FieldCount > 26)
                    {
                        info.DoseOnceDisplay = this.Reader[26].ToString();
                    }

                    if (info.DoseOnceDisplay.Length <= 0)
                        info.DoseOnceDisplay = info.DoseOnce.ToString();

                    if (this.Reader.FieldCount > 27)
                    {
                        info.DoseUnitDisplay = this.Reader[27].ToString();
                    }

                    if (info.DoseUnitDisplay.Length <= 0)
                        info.DoseUnitDisplay = info.DoseUnit.ToString();
                }
                catch { }
                al.Add(info);
            }
            this.Reader.Close();
            return al;
        }

        #region ���׷�������order add by sunm

        /// <summary>
        /// ���סԺ����
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList MyGetOutGroupItem(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            #region �ӿ�˵��
            //0 �����ڵ�����ˮ��,
            //1 ��Ŀ����, 2 ҽ������ ,3 ��ҩƵ��,    4 ��ҩ����       5 ÿ�η��ü���
            //6 ������λ���Ա�ҩʹ��,7 ��������,8 ������λ���Ա���Ŀʹ��,9 ��ҩ����(����)
            //10 �����ˮ��,11 ��ҩ���,12 ��鲿λ����,13 ִ�п���,14 ҽ����ʼʱ��,15 ҽ������ʱ��
            //16 ҽ����ע,17 ҩƷ���ҽ����ע
            #endregion

            while (this.Reader.Read())
            {
                FS.HISFC.Models.Order.OutPatient.Order info = new FS.HISFC.Models.Order.OutPatient.Order();
                try
                {
                    info.ID = this.Reader[1].ToString();
                    info.User01 = this.Reader[0].ToString();
                    //info.OrderType.ID = this.Reader[2].ToString();
                    try
                    {
                        info.Frequency.ID = this.Reader[3].ToString();
                        info.Usage.ID = this.Reader[4].ToString();
                    }
                    catch { }
                    try
                    {
                        info.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                    }
                    catch { }
                    try
                    {
                        info.DoseUnit = this.Reader[6].ToString();
                    }
                    catch { }
                    try
                    {
                        info.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
                    }
                    catch { }
                    info.Unit = this.Reader[8].ToString();
                    try
                    {
                        info.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
                    }
                    catch { }
                    info.Combo.ID = this.Reader[10].ToString();
                    info.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[11].ToString());
                    info.CheckPartRecord = this.Reader[12].ToString();
                    info.ExeDept.ID = this.Reader[13].ToString();

                    //��ʼʱ�䡢ֹͣʱ����ȡ
                    //info.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());
                    //info.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());
                    info.Memo = this.Reader[16].ToString();
                    info.Combo.Memo = this.Reader[17].ToString();
                    info.Usage.Name = this.Reader[18].ToString();
                    info.ExeDept.Name = this.Reader[19].ToString();
                    //info.OrderType.Name = this.Reader[20].ToString();
                    info.Name = this.Reader[21].ToString();
                    try//ʱ����
                    {
                        info.User03 = this.Reader[22].ToString();
                        info.Item.SysClass.ID = this.Reader[23].ToString();
                        info.Item.User01 = this.Reader[24].ToString();

                        info.Item.ID = this.Reader[1].ToString();
                        info.Item.Name = this.Reader[21].ToString();
                        //if ("P,PCC,PCZ".Contains(info.Item.SysClass.ID.ToString()))
                        if (info.Item.ID.StartsWith("Y"))
                        {
                            info.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                        }
                        else
                        {
                            info.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                        }
                    }
                    catch { }
                    info.MinunitFlag = this.Reader[25].ToString();
                    if (this.Reader.FieldCount > 26)
                    {
                        info.DoseOnceDisplay = this.Reader[26].ToString();
                    }

                    if (info.DoseOnceDisplay.Length <= 0)
                        info.DoseOnceDisplay = info.DoseOnce.ToString();

                    if (this.Reader.FieldCount > 27)
                    {
                        info.DoseUnitDisplay = this.Reader[27].ToString();
                    }

                    if (info.DoseUnitDisplay.Length <= 0)
                        info.DoseUnitDisplay = info.DoseUnit.ToString();
                }
                catch { }
                al.Add(info);
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #region ���ļ���
        /// <summary>
        /// ����µ������ļ���ID
        /// </summary>
        /// <returns></returns>
        public string GetNewFolderID()
        {
            string strSql = "";
            if (this.GetSQL("Manager.Group.GetNewFolderID", ref strSql) == -1)
            {
                return "";
            }
            return this.ExecSqlReturnOne(strSql);
        }

        /// <summary>
        /// �������������ļ���
        /// </summary>
        /// <param name="groupFolder"></param>
        /// <returns></returns>
        public int SetNewFolder(FS.HISFC.Models.Base.Group groupFolder)
        {
            //���Ϊ��--���
            if (groupFolder.ID == "")
            {
                groupFolder.ID = this.GetNewFolderID();
                if (groupFolder.ID == "")
                {
                    return -1;
                }
            }
            //�ȸ���
            int iRet = this.updateFolder(groupFolder);
            if (iRet < 0)//����
            {
                return -1;
            }
            else if (iRet == 0)//û�и��µ�
            {
                //����
                int iReturn = this.insertFolder(groupFolder);
                if (iReturn < 0)//����
                {
                    return -1;
                }
                return iReturn;
            }
            //����
            return iRet;
        }

        /// <summary>
        /// ���������ļ���
        /// </summary>
        /// <param name="groupFolder"></param>
        /// <returns></returns>
        public int updateFolder(FS.HISFC.Models.Base.Group groupFolder)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Group.UpdateFolder", ref strSql) == -1)
            {
                return -1;
            }
            /* ���룬���ƣ�ƴ���룬�����,
             * ��������������ͣ�
             * ���׿��ң�����ҽ�������׹���
             * ���ױ�ע������Ա
             * */
            strSql = System.String.Format(strSql, groupFolder.ID, groupFolder.Name, groupFolder.SpellCode, groupFolder.WBCode,
                groupFolder.UserType.GetHashCode().ToString(), groupFolder.Kind.GetHashCode().ToString(),
                groupFolder.Dept.ID, groupFolder.Doctor.ID, FS.FrameWork.Function.NConvert.ToInt32(groupFolder.IsShared),
                groupFolder.Memo, this.Operator.ID,
                groupFolder.ParentID //�ϼ�Ŀ¼ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
                );
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����һ��Ŀ¼
        /// </summary>
        /// <param name="groupFolder"></param>
        /// <returns></returns>
        private int insertFolder(FS.HISFC.Models.Base.Group groupFolder)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Group.InsertFolder", ref strSql) == -1)
            {
                return -1;
            }
            /* ���룬���ƣ�ƴ���룬�����,
             * ��������������ͣ�
             * ���׿��ң�����ҽ�������׹���
             * ���ױ�ע������Ա
             * */
            strSql = System.String.Format(strSql, groupFolder.ID, groupFolder.Name, groupFolder.SpellCode, groupFolder.WBCode,
                groupFolder.UserType.GetHashCode().ToString(), groupFolder.Kind.GetHashCode().ToString(),
                groupFolder.Dept.ID, groupFolder.Doctor.ID, FS.FrameWork.Function.NConvert.ToInt32(groupFolder.IsShared),
                groupFolder.Memo, this.Operator.ID,
                groupFolder.ParentID //�ϼ�Ŀ¼ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
                );
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ���ļ��У�ͬʱ��������������Ϊû���ļ���
        /// </summary>
        /// <param name="groupFolder"></param>
        /// <returns></returns>
        public int deleteFolder(FS.HISFC.Models.Base.Group groupFolder)
        {
            string strSql = "";
            string strSql1 = "";
            if (this.GetSQL("Manager.Group.deleteFolder", ref strSql) == -1)
            {
                return -1;
            }
            if (this.GetSQL("Manager.Group.updateFolderIDNull", ref strSql1) == -1)
            {
                return -1;
            }
            strSql = System.String.Format(strSql, groupFolder.ID);

            if (this.ExecNoQuery(strSql) < 0)
            {
                return -1;
            }
            strSql1 = System.String.Format(strSql1, groupFolder.ID);

            if (this.ExecNoQuery(strSql1) < 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �������Ŀ¼
        /// </summary>
        /// <param name="type"></param>
        /// <param name="deptCode"></param>
        /// <param name="docCode"></param>
        /// <returns></returns>
        public ArrayList GetAllFolder(FS.HISFC.Models.Base.ServiceTypes type, string deptCode, string docCode)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Group.GetAllFolder", ref strSql) == -1)
            {
                return null;
            }
            strSql = System.String.Format(strSql, type.GetHashCode().ToString(), deptCode, docCode);
            //����
            if (this.ExecQuery(strSql) < 0)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Group group = new FS.HISFC.Models.Base.Group();
                group.ID = this.Reader[0].ToString();//����
                group.Name = this.Reader[1].ToString();//����
                group.UserType = (FS.HISFC.Models.Base.ServiceTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                group.Dept.ID = this.Reader[3].ToString();//���Ҵ���
                group.Doctor.ID = this.Reader[4].ToString();//ҽ������
                group.Kind = (FS.HISFC.Models.Base.GroupKinds)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//����
                group.SpellCode = this.Reader[6].ToString();
                group.WBCode = this.Reader[7].ToString();
                group.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                group.Memo = this.Reader[9].ToString();//��ע
                group.UserCode = "F";//�������ļ���
                group.ParentID = this.Reader[10].ToString();  //�ϼ�Ŀ¼ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
                al.Add(group);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="groupID">����ID</param>
        /// <param name="sortID">�����</param>
        /// <returns></returns>
        public int UpdateGroupSortID(string groupID, string sortID)
        {
            try
            {
                string strSql = "";
                if (this.GetSQL("Manager.Group.UpdateGroupSortID", ref strSql) == -1)
                {
                    return -1;
                }

                strSql = string.Format(strSql, groupID, sortID);

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }
        }


        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="groupID">����ID</param>
        /// <param name="sortID">�����</param>
        /// <returns></returns>
        public int UpdateGroupDetailSortID(string groupID,string seqID, int sortID)
        {
            try
            {
                string strSql = "";
                if (this.GetSQL("Manager.Group.UpdateGroupDetailSortID", ref strSql) == -1)
                {
                    return -1;
                }

                strSql = string.Format(strSql, groupID,seqID, sortID);

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }
        }



        /// <summary>
        /// ��������������
        /// </summary>
        /// <returns></returns>
        public string GetMaxGroupSortID()
        {
            try
            {
                string strSql = "";
                if (this.GetSQL("Manager.Group.GetMaxGroupSortID", ref strSql) == -1)
                {
                    return null;
                }

                string maxSort = this.ExecSqlReturnOne(strSql);
                if (string.IsNullOrEmpty(maxSort))
                {
                    return "1".PadLeft(10, '0');
                }
                else
                {
                    return maxSort.PadLeft(10, '0');
                }
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return "00001";
            }
        }

        //{C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
        /// <summary>
        /// ����Ŀ¼IDȡ���������Ŀ¼
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ArrayList GetAllFolderByFolderID(string folderID)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Group.GetAllFolderByFolderID", ref strSql) == -1)
            {
                return null;
            }
            strSql = System.String.Format(strSql, folderID);
            //����
            if (this.ExecQuery(strSql) < 0)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Group group = new FS.HISFC.Models.Base.Group();
                group.ID = this.Reader[0].ToString();//����
                group.Name = this.Reader[1].ToString();//����
                group.UserType = (FS.HISFC.Models.Base.ServiceTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                group.Dept.ID = this.Reader[3].ToString();//���Ҵ���
                group.Doctor.ID = this.Reader[4].ToString();//ҽ������
                group.Kind = (FS.HISFC.Models.Base.GroupKinds)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//����
                group.SpellCode = this.Reader[6].ToString();
                group.WBCode = this.Reader[7].ToString();
                group.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                group.Memo = this.Reader[9].ToString();//��ע
                group.UserCode = "F";//�������ļ���
                group.ParentID = this.Reader[10].ToString();//�ϼ��ļ��б���

                al.Add(group);
            }
            this.Reader.Close();
            return al;
        }

        //{C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
        /// <summary>
        /// �������һ��Ŀ¼
        /// </summary>
        /// <param name="type"></param>
        /// <param name="deptCode"></param>
        /// <param name="docCode"></param>
        /// <returns></returns>
        public ArrayList GetAllFirstLVFolder(FS.HISFC.Models.Base.ServiceTypes type, string deptCode, string docCode)
        {
            try
            {
                string strSql = "";
                if (this.GetSQL("Manager.Group.GetAllFirstLVFolder", ref strSql) == -1)
                {
                    return null;
                }
                strSql = System.String.Format(strSql, type.GetHashCode().ToString(), deptCode, docCode);
                //����
                if (this.ExecQuery(strSql) < 0)
                {
                    return null;
                }
                ArrayList al = new ArrayList();

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Base.Group group = new FS.HISFC.Models.Base.Group();
                    group.ID = this.Reader[0].ToString();//����
                    group.Name = this.Reader[1].ToString();//����
                    group.UserType = (FS.HISFC.Models.Base.ServiceTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                    group.Dept.ID = this.Reader[3].ToString();//���Ҵ���
                    group.Doctor.ID = this.Reader[4].ToString();//ҽ������
                    group.Kind = (FS.HISFC.Models.Base.GroupKinds)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//����
                    group.SpellCode = this.Reader[6].ToString();
                    group.WBCode = this.Reader[7].ToString();
                    group.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                    group.Memo = this.Reader[9].ToString();//��ע
                    group.UserCode = "F";//�������ļ���
                    group.ParentID = this.Reader[10].ToString();//�ϼ��ļ��б���

                    al.Add(group);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// ���������ļ��УɣĻ������
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ArrayList GetGroupByFolderID(string folderID)
        {
            string strSql = "";
            string strWhere = "";
            if (this.GetSQL("Manager.Group.Select", ref strSql) == -1)
            {
                this.Err = "û���ҵ�sql";
                return null;
            }
            if (this.GetSQL("Manager.Group.GetGroup.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�sql";
                return null;
            }
            strSql = strSql + strWhere;
            strSql = System.String.Format(strSql, folderID);
            return this.myGetGroup(strSql);
        }

        /// <summary>
        /// ��������Ŀ¼ID
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="FolderID"></param>
        /// <returns></returns>
        public int UpdateGroupFolderID(string GroupID, string FolderID)
        {
            string strSql = "";
            if (GroupID == "") return -1;
            if (this.GetSQL("Manager.Group.UpdateGroupFolderID", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return -1;
            }
            strSql = System.String.Format(strSql, GroupID, FolderID);
            return this.ExecNoQuery(strSql);
        }
        #endregion

        #region ��
        /// <summary>
        /// ����µ���ID
        /// </summary>
        /// <returns></returns>
        public string GetNewGroupID()
        {
            string sql = "";
            if (this.GetSQL("Manager.Group.GetNewGroupID", ref sql) == -1) return "-1";
            return this.ExecSqlReturnOne(sql);
        }

        /// <summary>
        /// �������׵�����
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public int UpdateGroupName(string GroupID, string groupName)
        {
            string strSql = "";
            if (GroupID == "") return -1;
            if (this.GetSQL("Manager.Group.UpdateGroupName", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return -1;
            }
            string[] str = new string[]{  GroupID,
                                        groupName
                                      };
            //strSql = System.String.Format(strSql,GroupID,groupName);
            return this.ExecNoQuery(strSql, str);
        }

        /// <summary>
        /// ����һ����
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGroup(FS.HISFC.Models.Base.Group info)
        {
            string strUpdate = "", strInsert = "";
            if (this.GetSQL("Manager.Group.Update", ref strUpdate) == -1) return -1;
            if (this.GetSQL("Manager.Group.Insert", ref strInsert) == -1) return -1;
            try
            {
                #region �ӿ�˵��
                //<!--0 : ����ID          1 : �Ƿ�ҽ������ 0 �� 1 ��             2 : ��������                                
                //3: ����ƴ����                            4 : ����������           5 : 1����/2סԺ                             
                //6 : ��������,1.ҽʦ���ף�2.��������      7 : ���Ҵ���             8 : ����ҽʦ                                
                //9 : �Ƿ���1�ǣ�0��                   10 : ���ױ�ע            11 : ����Ա                                                              
                //-->
                #endregion
                //strUpdate = string.Format(strUpdate,info.ID,
                //                                    '1',
                //                                    info.Name,
                //                                    info.SpellCode,
                //                                    info.UserCode,
                //                                    info.UserType.GetHashCode().ToString(),
                //                                    info.Kind.GetHashCode().ToString(),
                //                                    info.Dept.ID,
                //                                    info.Doctor.ID,
                //                                    FS.FrameWork.Function.NConvert.ToInt32(info.IsShared).ToString(),
                //                                    info.Memo,this.Operator.ID);
                //strInsert = string.Format(strInsert,info.ID,'1',
                //    info.Name,info.SpellCode,info.UserCode,info.UserType.GetHashCode().ToString(),
                //    info.Kind.GetHashCode().ToString(),info.Dept.ID,info.Doctor.ID,FS.FrameWork.Function.NConvert.ToInt32(info.IsShared).ToString(),
                //    info.Memo,this.Operator.ID);

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            //�ȸ���
            if (this.ExecNoQuery(strUpdate, GetParam(info)) <= 0)
            {
                //����
                if (this.ExecNoQuery(strInsert, GetParam(info)) <= 0)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Base.Group info)
        {
            string[] str = new string[]{
                                                    info.ID,
                                                    "1",
					                                info.Name,
                                                    info.SpellCode,
                                                    info.UserCode,
                                                    info.UserType.GetHashCode().ToString(),
					                                info.Kind.GetHashCode().ToString(),
                                                    info.Dept.ID,
                                                    info.Doctor.ID,
                                                    FS.FrameWork.Function.NConvert.ToInt32(info.IsShared).ToString(),
					                                info.Memo,this.Operator.ID,
                                                    info.ParentID
                                        };
            return str;
        }

        /// <summary>
        /// ����һ����
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateOrderGroup(FS.HISFC.Models.Base.Group info)
        {
            string strUpdate = "", strInsert = "";
            if (this.GetSQL("Manager.Group.Update", ref strUpdate) == -1) return -1;
            if (this.GetSQL("Manager.Group.Insert", ref strInsert) == -1) return -1;
            try
            {
                #region �ӿ�˵��
                //<!--0 : ����ID          1 : �Ƿ�ҽ������ 0 �� 1 ��             2 : ��������                                
                //3: ����ƴ����                            4 : ����������           5 : 1����/2סԺ                             
                //6 : ��������,1.ҽʦ���ף�2.��������      7 : ���Ҵ���             8 : ����ҽʦ                                
                //9 : �Ƿ���1�ǣ�0��                   10 : ���ױ�ע            11 : ����Ա                                                              
                //-->
                #endregion
                strUpdate = string.Format(strUpdate, info.ID, '0',
                    info.Name, info.SpellCode, info.UserCode, info.UserType.GetHashCode().ToString(),
                    info.Kind.GetHashCode().ToString(), info.Dept.ID, info.Doctor.ID, FS.FrameWork.Function.NConvert.ToInt32(info.IsShared).ToString(),
                    info.Memo, this.Operator.ID);
                strInsert = string.Format(strInsert, info.ID, '0',
                    info.Name, info.SpellCode, info.UserCode, info.UserType.GetHashCode().ToString(),
                    info.Kind.GetHashCode().ToString(), info.Dept.ID, info.Doctor.ID, FS.FrameWork.Function.NConvert.ToInt32(info.IsShared).ToString(),
                    info.Memo, this.Operator.ID);

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            //�ȸ���
            if (this.ExecNoQuery(strUpdate) <= 0)
            {
                //����
                if (this.ExecNoQuery(strInsert) <= 0)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// ɾ��һ����
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteGroup(FS.HISFC.Models.Base.Group info)
        {
            string strSql = "";
            //�ӿ�˵��
            //����
            //0 �ɹ�,-1ʧ��
            //
            if (this.GetSQL("Manager.Group.Delete", ref strSql) == -1)
            {
                return -1;
            }
            //������Ϣ���ɹ��ٲ�������Ϣ
            //������� 
            //0 ID
            try
            {
                strSql = string.Format(strSql, info.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�ӿڴ���" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��������ϸ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteGroupOrder(FS.HISFC.Models.Base.Group info)
        {
            string strSql = "";
            //�ӿ�˵��
            //����
            //0 �ɹ�,-1ʧ��
            //
            if (this.GetSQL("Manager.Group.DeleteOrder", ref strSql) == -1)
            {
                return -1;
            }
            //������Ϣ���ɹ��ٲ�������Ϣ
            //������� 
            //0 ID
            try
            {
                strSql = string.Format(strSql, info.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�ӿڴ���" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ�����������һ��ҽ����Ϣ
        /// </summary>
        /// <param name="SeqId"></param>
        /// <returns></returns>
        public int DeleteSingleOrder(string SeqId)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Group.DeleteSingleOrder", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, SeqId);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�ӿڴ���" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �������ױ����ѯ������ϸ�Ƿ�Ĭ��ȫѡ
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public int QueryIsSelectAll(string groupID)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Group.QueryIsSelectAll", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, groupID);
                return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�ӿڴ���" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }

        /// <summary>
        /// �����Ƿ�Ĭ��ȫѡ��־
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="isSelectAll"></param>
        /// <returns></returns>
        public int UpdateIsSelectAll(string groupID, bool isSelectAll)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Group.UpdateIsSelectAll", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                return this.ExecNoQuery(strSql, groupID, FS.FrameWork.Function.NConvert.ToInt32(isSelectAll).ToString());
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�ӿڴ���" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }

        #endregion

        #region ����Ŀ
        /// <summary>
        /// ����һ������Ŀ
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGroupItem(FS.HISFC.Models.Base.Group info, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string strUpdate = "", strInsert = "";
            if (this.GetSQL("Manager.GroupItem.Update", ref strUpdate) == -1) return -1;
            if (this.GetSQL("Manager.GroupItem.Insert", ref strInsert) == -1) return -1;
            try
            {
                #region �ӿ�˵��
                //<!--
                //0 : ������ˮ��             1 : �����ڵ�����ˮ��            2 : ÿ�η��ü���                              
                //3 : ������λ���Ա�ҩʹ��        4 : ��������              5 : ������λ���Ա���Ŀʹ��                         
                //6 : ��ҩ����(����)         7 : �����ˮ��                 8 : ��ҩ���                                
                //9 : ��鲿λ����          10 : ִ�п���                  11 : ҽ����ʼʱ��                             
                //12 : ҽ������ʱ��         13 : ҽ����ע                  14 : ҩƷ���ҽ����ע                           
                //15 : ����Ա               16 : ����ʱ��                  17 : ��Ŀ����                               
                //18 : ҽ������             19 : ��ҩƵ��                  20 : ��ҩ���� 
                //21: ʱ����              22 classcode 24 extcode
                //-->
                #endregion
                //====��չ�������ڴ��ҩƷ�Ŀۿ����========
                //{B9661764-2E06-462a-A9D9-05A3009D1F23}
                string stockDept = string.Empty;
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    stockDept = order.StockDept.ID;
                }
                strUpdate = string.Format(strUpdate, info.ID, order.ID, order.DoseOnce.ToString(),
                    order.DoseUnit, order.Qty.ToString(), order.Unit,
                    order.HerbalQty.ToString(), order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug).ToString(),
                    order.CheckPartRecord, order.ExeDept.ID, order.BeginTime.ToString(),
                    order.EndTime.ToString(), order.Memo, order.Combo.Memo, this.Operator.ID, this.GetSysDateTime(), order.Item.ID,
                    order.OrderType.ID, order.Frequency.ID, order.Usage.ID, order.Item.Name, order.User03, order.Item.SysClass.ID, stockDept, "", order.DoseOnceDisplay, order.DoseUnitDisplay);
                strInsert = string.Format(strInsert, info.ID, order.ID, order.DoseOnce.ToString(),
                    order.DoseUnit, order.Qty.ToString(), order.Unit,
                    order.HerbalQty.ToString(), order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug).ToString(),
                    order.CheckPartRecord, order.ExeDept.ID, order.BeginTime.ToString(),
                    order.EndTime.ToString(), order.Memo, order.Combo.Memo, this.Operator.ID, this.GetSysDateTime(), order.Item.ID,
                    order.OrderType.ID, order.Frequency.ID, order.Usage.ID, order.Item.Name, order.User03, order.Item.SysClass.ID, stockDept, "", order.DoseOnceDisplay, order.DoseUnitDisplay);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            //�ȸ���
            if (this.ExecNoQuery(strUpdate) <= 0)
            {
                //����
                if (this.ExecNoQuery(strInsert) <= 0)
                {
                    return -1;
                }
            }
            return 0;
        }

        public int UpdateGroupItem(FS.HISFC.Models.Base.Group info, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string strUpdate = "", strInsert = "";
            if (this.GetSQL("Manager.GroupItem.Update", ref strUpdate) == -1) return -1;
            if (this.GetSQL("Manager.GroupItem.Insert", ref strInsert) == -1) return -1;
            try
            {
                #region �ӿ�˵��
                //<!--
                //0 : ������ˮ��             1 : �����ڵ�����ˮ��            2 : ÿ�η��ü���                              
                //3 : ������λ���Ա�ҩʹ��        4 : ��������              5 : ������λ���Ա���Ŀʹ��                         
                //6 : ��ҩ����(����)         7 : �����ˮ��                 8 : ��ҩ���                                
                //9 : ��鲿λ����          10 : ִ�п���                  11 : ҽ����ʼʱ��                             
                //12 : ҽ������ʱ��         13 : ҽ����ע                  14 : ҩƷ���ҽ����ע                           
                //15 : ����Ա               16 : ����ʱ��                  17 : ��Ŀ����                               
                //18 : ҽ������             19 : ��ҩƵ��                  20 : ��ҩ���� 
                //21: ʱ����              22 classcode 24 extcode
                //-->
                #endregion

                //====��չ�������ڴ��ҩƷ�Ŀۿ����========
                //{B9661764-2E06-462a-A9D9-05A3009D1F23}
                string stockDept = string.Empty;
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    stockDept = order.StockDept.ID;
                }

                strUpdate = string.Format(
                    strUpdate,
                    info.ID,
                    order.ID,
                    order.DoseOnce.ToString(),
                    order.DoseUnit,
                    order.Qty.ToString(),
                    order.Unit,
                    order.HerbalQty.ToString(),
                    order.Combo.ID,
                    FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug).ToString(),
                    order.CheckPartRecord,
                    order.ExeDept.ID,
                    order.BeginTime.ToString(),
                    order.EndTime.ToString(),
                    order.Memo,
                    order.Combo.Memo,
                    this.Operator.ID,
                    this.GetSysDateTime(),
                    order.Item.ID,
                    "MZ",
                    order.Frequency.ID,
                    order.Usage.ID,
                    order.Item.Name,
                    order.User03,
                    order.Item.SysClass.ID,
                    stockDept,
                    order.MinunitFlag,
                    order.DoseOnceDisplay,
                    order.DoseUnitDisplay
                    );

                strInsert = string.Format(
                    strInsert,
                    info.ID,
                    order.ID,
                    order.DoseOnce.ToString(),
                    order.DoseUnit,
                    order.Qty.ToString(),
                    order.Unit,
                    order.HerbalQty.ToString(),
                    order.Combo.ID,
                    FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug).ToString(),
                    order.CheckPartRecord,
                    order.ExeDept.ID,
                    order.BeginTime.ToString(),
                    order.EndTime.ToString(),
                    order.Memo,
                    order.Combo.Memo,
                    this.Operator.ID,
                    this.GetSysDateTime(),
                    order.Item.ID,
                    "MZ",
                    order.Frequency.ID,
                    order.Usage.ID,
                    order.Item.Name,
                    order.User03,
                    order.Item.SysClass.ID,
                    stockDept,
                    order.MinunitFlag,
                    order.DoseOnceDisplay,
                    order.DoseUnitDisplay
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            //�ȸ���
            if (this.ExecNoQuery(strUpdate) <= 0)
            {
                //����
                if (this.ExecNoQuery(strInsert) <= 0)
                {
                    return -1;
                }
            }
            return 0;
        }

        #endregion

        #endregion
    }
}