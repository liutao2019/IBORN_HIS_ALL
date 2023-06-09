using System;
using System.Collections;
//using FS.FrameWork.Models;
//using FS.HISFC.Models;
//using FS.HISFC.Models.Order;
namespace FS.HISFC.BizLogic.Manager
{

    /// <summary>
    /// 组套管理
    /// </summary>
    public class Group : DataBase
    {
        /// <summary>
        ///医嘱 
        /// </summary>
        public Group()
        {
        }

        #region "获得"
        /// <summary>
        /// 获得医嘱组套类别
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllOrderGroup(FS.HISFC.Models.Base.ServiceTypes t)
        {
            string sql = "", sqlWhere = "";

            #region 接口说明
            //0 : 组套ID                                1 : 组套名称                                2 : 组套拼音码                               
            //3 : 组套助记码                               4 : 1门诊/2住院                             5 : 组套类型,1.医师组套；2.科室组套                  
            //6 : 科室代码                                7 : 组套医师                                8 : 是否共享，1是，0否                          
            //9 : 组套备注    
            #endregion

            if (this.GetSQL("Manager.Group.Select", ref sql) == -1)
                return null;

            #region 接口说明
            //医嘱类别 门诊/住院
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
                this.Err = "付值出错!" + ex.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetGroup(sql);
        }


        /// <summary>
        /// 获得医嘱组套类别
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllPCCOrderGroup(FS.HISFC.Models.Base.ServiceTypes t)
        {
            string sql = "", sqlWhere = "";

            if (this.GetSQL("Manager.Group.Select", ref sql) == -1)
                return null;

            #region 接口说明
            //医嘱类别 门诊/住院
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
                this.Err = "付值出错!" + ex.Message;
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
            #region 接口说明
            //0 : 组套ID                                1 : 组套名称                                2 : 组套拼音码                               
            //3 : 组套助记码                               4 : 1门诊/2住院                             5 : 组套类型,1.医师组套；2.科室组套                  
            //6 : 科室代码                                7 : 组套医师                                8 : 是否共享，1是，0否                          
            //9 : 组套备注    
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
        /// 查询科室组套与个人组套
        /// Add By liangjz 2005-10
        /// </summary>
        /// <param name="t">组套类别 门诊/住院</param>
        /// <param name="deptCode">科室代码</param>
        /// <param name="doctCode">医生编码</param>
        /// <returns>成功返回组套列表 失败返回null</returns>
        public ArrayList GetDeptOrderGroup(FS.HISFC.Models.Base.ServiceTypes t, string deptCode, string doctCode)
        {
            string sql = "", sqlWhere = "";

            #region  接口说明
            //0 : 组套ID                                1 : 组套名称                                2 : 组套拼音码                               
            //3 : 组套助记码                               4 : 1门诊/2住院                             5 : 组套类型,1.医师组套；2.科室组套                  
            //6 : 科室代码                                7 : 组套医师                                8 : 是否共享，1是，0否                          
            //9 : 组套备注    
            #endregion
            if (this.GetSQL("Manager.Group.Select", ref sql) == -1)
                return null;
            #region  接口说明
            //医嘱类别 门诊/住院 科室代码 医生编码
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
                this.Err = "付值出错!" + ex.Message;
                this.WriteErr();
                return null;
            }
            return this.myGetGroup(sql);
        }

        /// <summary>
        /// 获得项目
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllItem(FS.HISFC.Models.Base.Group group)
        {
            string sql = "";
            #region 接口说明
            //0 组套内单项流水号,
            //1 项目代码, 2 医嘱类型,3 服药频次,    4 服药方法       5 每次服用剂量
            //6 剂量单位，自备药使用,7 开立数量,8 开立单位，自备项目使用,9 草药付数(周期)
            //10 组合流水号,11 主药标记,12 检查部位检体,13 执行科室,14 医嘱开始时间,15 医嘱结束时间
            //16 医嘱备注,17 药品组合医嘱备注
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
                this.Err = "付值出错!" + ex.Message;
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
        /// 获得项目
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllPCCItem(string groupID)
        {
            string sql = "";
            #region 接口说明
            //0 组套内单项流水号,
            //1 项目代码, 2 医嘱类型,3 服药频次,    4 服药方法       5 每次服用剂量
            //6 剂量单位，自备药使用,7 开立数量,8 开立单位，自备项目使用,9 草药付数(周期)
            //10 组合流水号,11 主药标记,12 检查部位检体,13 执行科室,14 医嘱开始时间,15 医嘱结束时间
            //16 医嘱备注,17 药品组合医嘱备注
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
                this.Err = "付值出错!" + ex.Message;
                this.WriteErr();
                return null;
            }
            return this.MyGetOutGroupItem(sql);
        }

        /// <summary>
        /// 获取住院组套项目
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList MyGetInGroupItem(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            #region 接口说明
            //0 组套内单项流水号,
            //1 项目代码, 2 医嘱类型 ,3 服药频次,    4 服药方法       5 每次服用剂量
            //6 剂量单位，自备药使用,7 开立数量,8 开立单位，自备项目使用,9 草药付数(周期)
            //10 组合流水号,11 主药标记,12 检查部位检体,13 执行科室,14 医嘱开始时间,15 医嘱结束时间
            //16 医嘱备注,17 药品组合医嘱备注
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

                    //开始、停止时间重取
                    //info.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());
                    //info.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());
                    info.Memo = this.Reader[16].ToString();
                    info.Combo.Memo = this.Reader[17].ToString();
                    info.Usage.Name = this.Reader[18].ToString();
                    info.ExeDept.Name = this.Reader[19].ToString();
                    info.OrderType.Name = this.Reader[20].ToString();
                    info.Name = this.Reader[21].ToString();
                    try//时间间隔
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

        #region 组套返回门诊order add by sunm

        /// <summary>
        /// 获得住院组套
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList MyGetOutGroupItem(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            #region 接口说明
            //0 组套内单项流水号,
            //1 项目代码, 2 医嘱类型 ,3 服药频次,    4 服药方法       5 每次服用剂量
            //6 剂量单位，自备药使用,7 开立数量,8 开立单位，自备项目使用,9 草药付数(周期)
            //10 组合流水号,11 主药标记,12 检查部位检体,13 执行科室,14 医嘱开始时间,15 医嘱结束时间
            //16 医嘱备注,17 药品组合医嘱备注
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

                    //开始时间、停止时间重取
                    //info.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());
                    //info.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());
                    info.Memo = this.Reader[16].ToString();
                    info.Combo.Memo = this.Reader[17].ToString();
                    info.Usage.Name = this.Reader[18].ToString();
                    info.ExeDept.Name = this.Reader[19].ToString();
                    //info.OrderType.Name = this.Reader[20].ToString();
                    info.Name = this.Reader[21].ToString();
                    try//时间间隔
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

        #region 组文件夹
        /// <summary>
        /// 获得新的组套文件夹ID
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
        /// 插入或更新组套文件夹
        /// </summary>
        /// <param name="groupFolder"></param>
        /// <returns></returns>
        public int SetNewFolder(FS.HISFC.Models.Base.Group groupFolder)
        {
            //如果为空--获得
            if (groupFolder.ID == "")
            {
                groupFolder.ID = this.GetNewFolderID();
                if (groupFolder.ID == "")
                {
                    return -1;
                }
            }
            //先更新
            int iRet = this.updateFolder(groupFolder);
            if (iRet < 0)//出错
            {
                return -1;
            }
            else if (iRet == 0)//没有更新到
            {
                //插入
                int iReturn = this.insertFolder(groupFolder);
                if (iReturn < 0)//出错
                {
                    return -1;
                }
                return iReturn;
            }
            //返回
            return iRet;
        }

        /// <summary>
        /// 更新组套文件夹
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
            /* 编码，名称，拼音码，五笔码,
             * 组套类别，组套类型，
             * 组套科室，组套医生，组套共享
             * 组套备注，操作员
             * */
            strSql = System.String.Format(strSql, groupFolder.ID, groupFolder.Name, groupFolder.SpellCode, groupFolder.WBCode,
                groupFolder.UserType.GetHashCode().ToString(), groupFolder.Kind.GetHashCode().ToString(),
                groupFolder.Dept.ID, groupFolder.Doctor.ID, FS.FrameWork.Function.NConvert.ToInt32(groupFolder.IsShared),
                groupFolder.Memo, this.Operator.ID,
                groupFolder.ParentID //上级目录ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
                );
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入一个目录
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
            /* 编码，名称，拼音码，五笔码,
             * 组套类别，组套类型，
             * 组套科室，组套医生，组套共享
             * 组套备注，操作员
             * */
            strSql = System.String.Format(strSql, groupFolder.ID, groupFolder.Name, groupFolder.SpellCode, groupFolder.WBCode,
                groupFolder.UserType.GetHashCode().ToString(), groupFolder.Kind.GetHashCode().ToString(),
                groupFolder.Dept.ID, groupFolder.Doctor.ID, FS.FrameWork.Function.NConvert.ToInt32(groupFolder.IsShared),
                groupFolder.Memo, this.Operator.ID,
                groupFolder.ParentID //上级目录ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
                );
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除文件夹，同时更新其所属组套为没有文件夹
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
        /// 获得所有目录
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
            //出错
            if (this.ExecQuery(strSql) < 0)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Group group = new FS.HISFC.Models.Base.Group();
                group.ID = this.Reader[0].ToString();//编码
                group.Name = this.Reader[1].ToString();//名称
                group.UserType = (FS.HISFC.Models.Base.ServiceTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                group.Dept.ID = this.Reader[3].ToString();//科室代码
                group.Doctor.ID = this.Reader[4].ToString();//医生代码
                group.Kind = (FS.HISFC.Models.Base.GroupKinds)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//类型
                group.SpellCode = this.Reader[6].ToString();
                group.WBCode = this.Reader[7].ToString();
                group.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                group.Memo = this.Reader[9].ToString();//备注
                group.UserCode = "F";//代表是文件夹
                group.ParentID = this.Reader[10].ToString();  //上级目录ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
                al.Add(group);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 更新组套序号
        /// </summary>
        /// <param name="groupID">组套ID</param>
        /// <param name="sortID">排序号</param>
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
        /// 更新组套序号
        /// </summary>
        /// <param name="groupID">组套ID</param>
        /// <param name="sortID">排序号</param>
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
        /// 获得最大的组套序号
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
        /// 按照目录ID取下面的所有目录
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
            //出错
            if (this.ExecQuery(strSql) < 0)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Group group = new FS.HISFC.Models.Base.Group();
                group.ID = this.Reader[0].ToString();//编码
                group.Name = this.Reader[1].ToString();//名称
                group.UserType = (FS.HISFC.Models.Base.ServiceTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                group.Dept.ID = this.Reader[3].ToString();//科室代码
                group.Doctor.ID = this.Reader[4].ToString();//医生代码
                group.Kind = (FS.HISFC.Models.Base.GroupKinds)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//类型
                group.SpellCode = this.Reader[6].ToString();
                group.WBCode = this.Reader[7].ToString();
                group.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                group.Memo = this.Reader[9].ToString();//备注
                group.UserCode = "F";//代表是文件夹
                group.ParentID = this.Reader[10].ToString();//上级文件夹编码

                al.Add(group);
            }
            this.Reader.Close();
            return al;
        }

        //{C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
        /// <summary>
        /// 获得所有一级目录
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
                //出错
                if (this.ExecQuery(strSql) < 0)
                {
                    return null;
                }
                ArrayList al = new ArrayList();

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Base.Group group = new FS.HISFC.Models.Base.Group();
                    group.ID = this.Reader[0].ToString();//编码
                    group.Name = this.Reader[1].ToString();//名称
                    group.UserType = (FS.HISFC.Models.Base.ServiceTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                    group.Dept.ID = this.Reader[3].ToString();//科室代码
                    group.Doctor.ID = this.Reader[4].ToString();//医生代码
                    group.Kind = (FS.HISFC.Models.Base.GroupKinds)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());//类型
                    group.SpellCode = this.Reader[6].ToString();
                    group.WBCode = this.Reader[7].ToString();
                    group.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
                    group.Memo = this.Reader[9].ToString();//备注
                    group.UserCode = "F";//代表是文件夹
                    group.ParentID = this.Reader[10].ToString();//上级文件夹编码

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
        /// 根据组套文件夹ＩＤ获得组套
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ArrayList GetGroupByFolderID(string folderID)
        {
            string strSql = "";
            string strWhere = "";
            if (this.GetSQL("Manager.Group.Select", ref strSql) == -1)
            {
                this.Err = "没有找到sql";
                return null;
            }
            if (this.GetSQL("Manager.Group.GetGroup.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到sql";
                return null;
            }
            strSql = strSql + strWhere;
            strSql = System.String.Format(strSql, folderID);
            return this.myGetGroup(strSql);
        }

        /// <summary>
        /// 更新组套目录ID
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

        #region 组
        /// <summary>
        /// 获得新的组ID
        /// </summary>
        /// <returns></returns>
        public string GetNewGroupID()
        {
            string sql = "";
            if (this.GetSQL("Manager.Group.GetNewGroupID", ref sql) == -1) return "-1";
            return this.ExecSqlReturnOne(sql);
        }

        /// <summary>
        /// 更新组套的名称
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
        /// 更新一条组
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
                #region 接口说明
                //<!--0 : 组套ID          1 : 是否医嘱组套 0 否 1 是             2 : 组套名称                                
                //3: 组套拼音码                            4 : 组套助记码           5 : 1门诊/2住院                             
                //6 : 组套类型,1.医师组套；2.科室组套      7 : 科室代码             8 : 组套医师                                
                //9 : 是否共享，1是，0否                   10 : 组套备注            11 : 操作员                                                              
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
            //先更新
            if (this.ExecNoQuery(strUpdate, GetParam(info)) <= 0)
            {
                //插入
                if (this.ExecNoQuery(strInsert, GetParam(info)) <= 0)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取参数列表
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
        /// 更新一条组
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
                #region 接口说明
                //<!--0 : 组套ID          1 : 是否医嘱组套 0 否 1 是             2 : 组套名称                                
                //3: 组套拼音码                            4 : 组套助记码           5 : 1门诊/2住院                             
                //6 : 组套类型,1.医师组套；2.科室组套      7 : 科室代码             8 : 组套医师                                
                //9 : 是否共享，1是，0否                   10 : 组套备注            11 : 操作员                                                              
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
            //先更新
            if (this.ExecNoQuery(strUpdate) <= 0)
            {
                //插入
                if (this.ExecNoQuery(strInsert) <= 0)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 删除一条组
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteGroup(FS.HISFC.Models.Base.Group info)
        {
            string strSql = "";
            //接口说明
            //返回
            //0 成功,-1失败
            //
            if (this.GetSQL("Manager.Group.Delete", ref strSql) == -1)
            {
                return -1;
            }
            //更新信息不成功再插入新信息
            //传入参数 
            //0 ID
            try
            {
                strSql = string.Format(strSql, info.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除组套明细
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int DeleteGroupOrder(FS.HISFC.Models.Base.Group info)
        {
            string strSql = "";
            //接口说明
            //返回
            //0 成功,-1失败
            //
            if (this.GetSQL("Manager.Group.DeleteOrder", ref strSql) == -1)
            {
                return -1;
            }
            //更新信息不成功再插入新信息
            //传入参数 
            //0 ID
            try
            {
                strSql = string.Format(strSql, info.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除组套里面的一条医嘱信息
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
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据组套编码查询组套明细是否默认全选
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
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }

        /// <summary>
        /// 更新是否默认全选标志
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
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }

        #endregion

        #region 组项目
        /// <summary>
        /// 更新一条组项目
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
                #region 接口说明
                //<!--
                //0 : 组套流水号             1 : 组套内单项流水号            2 : 每次服用剂量                              
                //3 : 剂量单位，自备药使用        4 : 开立数量              5 : 开立单位，自备项目使用                         
                //6 : 草药付数(周期)         7 : 组合流水号                 8 : 主药标记                                
                //9 : 检查部位检体          10 : 执行科室                  11 : 医嘱开始时间                             
                //12 : 医嘱结束时间         13 : 医嘱备注                  14 : 药品组合医嘱备注                           
                //15 : 操作员               16 : 操作时间                  17 : 项目代码                               
                //18 : 医嘱类型             19 : 服药频次                  20 : 服药方法 
                //21: 时间间隔              22 classcode 24 extcode
                //-->
                #endregion
                //====扩展编码用于存放药品的扣库科室========
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
            //先更新
            if (this.ExecNoQuery(strUpdate) <= 0)
            {
                //插入
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
                #region 接口说明
                //<!--
                //0 : 组套流水号             1 : 组套内单项流水号            2 : 每次服用剂量                              
                //3 : 剂量单位，自备药使用        4 : 开立数量              5 : 开立单位，自备项目使用                         
                //6 : 草药付数(周期)         7 : 组合流水号                 8 : 主药标记                                
                //9 : 检查部位检体          10 : 执行科室                  11 : 医嘱开始时间                             
                //12 : 医嘱结束时间         13 : 医嘱备注                  14 : 药品组合医嘱备注                           
                //15 : 操作员               16 : 操作时间                  17 : 项目代码                               
                //18 : 医嘱类型             19 : 服药频次                  20 : 服药方法 
                //21: 时间间隔              22 classcode 24 extcode
                //-->
                #endregion

                //====扩展编码用于存放药品的扣库科室========
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

            //先更新
            if (this.ExecNoQuery(strUpdate) <= 0)
            {
                //插入
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