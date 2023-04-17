using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
namespace FS.HISFC.BizProcess.Factory
{
    /// <summary>
    /// 系统管理
    /// </summary>
    public  abstract class ManagerBase:FactoryBase
    {
        #region 科室管理

        public virtual System.Collections.ArrayList QueryDeptment()
        {
            FS.HISFC.BizLogic.Manager.Department departManager = new FS.HISFC.BizLogic.Manager.Department();

            this.SetDB(departManager);

            return departManager.GetDeptmentAll();
        }

        public virtual System.Collections.ArrayList QueryDeptment(FS.HISFC.Models.Base.EnumDepartmentType deptType)
        {
            FS.HISFC.BizLogic.Manager.Department departManager = new FS.HISFC.BizLogic.Manager.Department();

            this.SetDB(departManager);

            return departManager.GetDeptment(deptType);

        }

        ///// <summary>
        ///// 插入科室信息
        ///// </summary>
        ///// <param name="info">科室基本信息</param>
        ///// <returns>成功 1 失败 -1</returns>
        //public virtual int InsertDept(FS.HISFC.Models.Base.Department info)
        //{
        //    FS.HISFC.BizLogic.Manager.Department departManager = new FS.HISFC.BizLogic.Manager.Department();

        //    this.SetDB(departManager);

        //    return departManager.Insert(info);
        //}

        ///// <summary>
        ///// 更新科室信息
        ///// </summary>
        ///// <param name="info">科室基本信息</param>
        ///// <returns>成功 1 失败 -1</returns>
        //public virtual int UpdateDept(FS.HISFC.Models.Base.Department info)
        //{
        //    FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        //    this.SetDB(deptManager);

        //    return deptManager.Update(info);
        //}

        ///// <summary>
        ///// 获得除了护士站的所有科室
        ///// </summary>
        ///// <returns>成功 所有科室  失败 null</returns>
        //public virtual ArrayList GetDeptNoNurse()
        //{
        //    FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        //    this.SetDB(deptManager);

        //    return deptManager.GetDeptNoNurse();
        //}

        /// <summary>
        /// 根据科室类型获得科室列表
        /// </summary>
        /// <param name="Type">科室类型</param>
        /// <returns>成功 根据科室类型获得科室列表 失败 null</returns>
        public virtual ArrayList GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType Type)
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

            this.SetDB(deptManager);

            return deptManager.GetDeptment(Type);
        }
        
        #endregion

        #region Manager 成员

        #region 拼音管理 --Leiyj
        /// <summary>
        /// 根据传入字符串获得拼音码
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public virtual FS.HISFC.Models.Base.ISpell GetSpell(string words)
        {
            FS.HISFC.BizLogic.Manager.Spell spMgr = new FS.HISFC.BizLogic.Manager.Spell();
            this.SetDB(spMgr);
            return spMgr.Get(words);
        }
        #endregion
        public virtual DateTime GetDateTimeFromSysDateTime()
        {
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
            this.SetDB(dept);
            return dept.GetDateTimeFromSysDateTime();
        }

        #endregion

        #region 系统功能管理

        public virtual System.Collections.ArrayList LoadModelFunction()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// 查询系统功能模块
        /// </summary>
        /// <returns></returns>
        public virtual System.Collections.ArrayList QuerySysModelFunction()
        {
            FS.HISFC.BizLogic.Manager.SysModelFunctionManager functionManager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            this.SetDB(functionManager);

            return functionManager.QuerySysModelFunction();
        }

        public virtual System.Collections.ArrayList QuerySysModelFunction(string sysCode)
        {

            FS.HISFC.BizLogic.Manager.SysModelFunctionManager functionManager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            this.SetDB(functionManager);

            return functionManager.QuerySysModelFunction(sysCode);
        }

        public virtual System.Collections.ArrayList QuerySysModelFunctionByType(string FormType)
        {
            FS.HISFC.BizLogic.Manager.SysModelFunctionManager functionManager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            this.SetDB(functionManager);

            return functionManager.QuerySysModelFunctionByType(FormType);
        }

        public virtual FS.HISFC.Models.Admin.SysModelFunction QuerySysModelFunctionByID(string id)
        {
            FS.HISFC.BizLogic.Manager.SysModelFunctionManager functionManager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            this.SetDB(functionManager);

            return functionManager.QuerySysModelFunctionByID(id);
        }
        /// <summary>
        ///  增加系统功能模块信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual int InsertSysModelFunction(FS.HISFC.Models.Admin.SysModelFunction info)
        {
            FS.HISFC.BizLogic.Manager.SysModelFunctionManager functionManager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            this.SetDB(functionManager);

            return functionManager.InsertSysModelFunction(info);
        }
        /// <summary>
        /// 更新系统功能模块信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual int UpdateSysModelFunction(FS.HISFC.Models.Admin.SysModelFunction info)
        {
            FS.HISFC.BizLogic.Manager.SysModelFunctionManager functionManager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            this.SetDB(functionManager);

            return functionManager.UpdateSysModelFunction(info);
        }
        /// <summary>
        /// 删除系统功能模块信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public virtual int DeleteSysModelFunction(FS.HISFC.Models.Admin.SysModelFunction info)
        {
            FS.HISFC.BizLogic.Manager.SysModelFunctionManager functionManager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            this.SetDB(functionManager);

            return functionManager.DeleteSysModelFunction(info);
        }
        /// <summary>
        /// 得到插入记录的新id
        /// </summary>
        /// <returns></returns>
        public virtual string GetNewID()
        {
            FS.HISFC.BizLogic.Manager.SysModelFunctionManager functionManager = new FS.HISFC.BizLogic.Manager.SysModelFunctionManager();

            this.SetDB(functionManager);

            return functionManager.GetNewID();
        }

        #endregion


        #region 菜单管理

        public virtual System.Collections.ArrayList LoadAllMenu()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public virtual System.Collections.ArrayList LoadAllMenu(string groupID)
        {
            FS.HISFC.BizLogic.Manager.SysMenuManager manager = new FS.HISFC.BizLogic.Manager.SysMenuManager();
            this.SetDB(manager);
            return manager.LoadAll(groupID);
        }

        public virtual System.Collections.ArrayList LoadAllParentMenu(string groupID)
        {
            FS.HISFC.BizLogic.Manager.SysMenuManager manager = new FS.HISFC.BizLogic.Manager.SysMenuManager();
            this.SetDB(manager);
            return manager.LoadAllParentMenu(groupID);
        }
        public virtual System.Collections.ArrayList QueryMenu(FS.HISFC.Models.Base.Hospital hospital)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual int AddMenu(FS.HISFC.Models.Base.Hospital hospital, FS.HISFC.Models.Admin.SysMenu menu)
        {
            FS.HISFC.BizLogic.Manager.SysMenuManager manager = new FS.HISFC.BizLogic.Manager.SysMenuManager();
            this.SetDB(manager);
            return manager.InsertSysMenu(menu);
        }

        public virtual int AddMenu(FS.HISFC.Models.Admin.SysMenu parantMenu)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual int DeleteMenu(FS.HISFC.Models.Admin.SysMenu menu)
        {
            return 0;
        }
        public virtual int DeleteMenu(FS.HISFC.Models.Admin.SysGroup group)
        {
            FS.HISFC.BizLogic.Manager.SysMenuManager manager = new FS.HISFC.BizLogic.Manager.SysMenuManager();
            this.SetDB(manager);
            return manager.Delete(group.ID);
        }

        public virtual int DeleteMenu(FS.HISFC.Models.Admin.SysGroup group, int x)
        {
            FS.HISFC.BizLogic.Manager.SysMenuManager manager = new FS.HISFC.BizLogic.Manager.SysMenuManager();
            this.SetDB(manager);
            return manager.Delete(group.ID, x);
        }
        public virtual int ModifyMenu(FS.HISFC.Models.Admin.SysMenu menu)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual int MoveMenu(FS.HISFC.Models.Admin.SysMenu parantMenu, FS.HISFC.Models.Admin.SysMenu menu)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public virtual System.Collections.ArrayList LoadAllModel()
        {
            FS.HISFC.BizLogic.Manager.SysModelManager sysModelManager = new FS.HISFC.BizLogic.Manager.SysModelManager();
            this.SetDB(sysModelManager);
            return sysModelManager.LoadAll();
        }
        #endregion

        #region 组管理

        public virtual System.Collections.ArrayList LoadAllGroup()
        {
            FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();
            this.SetDB(sysGroupManager);
            return sysGroupManager.GetList();
        }

        public virtual System.Collections.ArrayList QueryGroup(FS.HISFC.Models.Base.Hospital hospital)
        {
            FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();
            this.SetDB(sysGroupManager);
            return sysGroupManager.GetList();
        }

        public virtual int AddGroup(FS.HISFC.Models.Base.Hospital hospital, FS.HISFC.Models.Admin.SysGroup sysGroup)
        {
            FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();
            this.SetDB(sysGroupManager);
            return sysGroupManager.Insert(sysGroup);
        }

        public virtual int ModifyGroup(FS.HISFC.Models.Admin.SysGroup sysGroup)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual int DeleteGroup(FS.HISFC.Models.Admin.SysGroup sysGroup)
        {
            FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();
            this.SetDB(sysGroupManager);
            return sysGroupManager.Del(sysGroup);
        }

        //public virtual FS.FrameWork.Object.NeuObject GetSysGroup(FS.HISFC.Models.Admin.SysGroup sysGroup)
        //{
        //    FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();
        //    this.SetDB(sysGroupManager);
        //    return sysGroupManager.Get(null);
        //}
        public virtual FS.FrameWork.Models.NeuObject GetSingleGroup(FS.HISFC.Models.Admin.SysGroup sysGroup)
        {
            FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();
            this.SetDB(sysGroupManager);
            return sysGroupManager.Get(sysGroup);
        }
        #endregion


        #region 人员管理

        /// <summary>
        /// 根据科室获得当前科室的所有人员
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功 所有人员集合  失败 null</returns>
        public virtual ArrayList GetPersonsByDeptIDAll(string deptCode)
        {
            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

            this.SetDB(personManager);

            return personManager.GetPersonsByDeptIDAll(deptCode);
        }



      
        #endregion


        #region Manager 成员


        public virtual int ChangePassword(string operatorID, string oldPassword, string newPassword)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IManager 成员

        public virtual FS.HISFC.Models.Base.Department GetDeptmentById(string id)
        {
            FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();
            this.SetDB(manager);
            return manager.GetDeptmentById(id);
        }

       

        public virtual System.Collections.ArrayList GetDeptmentAll()
        {
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
            this.SetDB(dept);
            return dept.GetDeptmentAll();
        }

        //public virtual int Insert(FS.HISFC.Models.Base.Department info)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int Update(FS.HISFC.Models.Base.Department info)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual string GetMaxDeptID()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int SelectDepartMentIsExist(string DepartmentId)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public virtual FS.HISFC.Models.Base.Employee GetPersonByID(string personID)
        {
            FS.HISFC.BizLogic.Manager.UserManager manager = new FS.HISFC.BizLogic.Manager.UserManager();
            this.SetDB(manager);
            return manager.GetPerson(personID);
        }

        #endregion

        #region IManager 成员


        public virtual string CheckPwd(string userID, string PWD)
        {
            FS.HISFC.BizLogic.Manager.UserManager manager = new FS.HISFC.BizLogic.Manager.UserManager();
            this.SetDB(manager);
            return manager.CheckPwd(userID, PWD);

        }

       

        public virtual System.Collections.ArrayList GetMultiDept(string userID)
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            this.SetDB(manager);
            return manager.GetMultiDept(userID);
        }

        #endregion


        #region 科室组套与USERTEXT组套成员  --ZGX

        /// <summary>
        /// 获得在院科室列表
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public virtual System.Collections.ArrayList QueryDeptmentsInHos(bool tr)
        {
            FS.HISFC.BizLogic.Manager.Department manger = new FS.HISFC.BizLogic.Manager.Department();
            this.SetDB(manger);
            return manger.GetInHosDepartment(tr);
        }

        /// <summary>
        /// 获得全部人员列表
        /// </summary>
        /// <returns></returns>
        public virtual System.Collections.ArrayList QueryEmployeeAll()
        {
            FS.HISFC.BizLogic.Manager.Person pMgr = new FS.HISFC.BizLogic.Manager.Person();
            this.SetDB(pMgr);
            return pMgr.GetEmployeeAll();
        }

       

        #region UserText   --Zgx

        /// <summary>
        /// 查找组别
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public virtual System.Collections.ArrayList GetGroupList(string code, string Type)
        {
            FS.HISFC.BizLogic.Manager.UserText usrTexMgr = new FS.HISFC.BizLogic.Manager.UserText();
            this.SetDB(usrTexMgr);
            return usrTexMgr.GetGroupList(code, Type);
        }

        public virtual System.Collections.ArrayList GetUserTextList(string code, int type)
        {
            FS.HISFC.BizLogic.Manager.UserText userText = new FS.HISFC.BizLogic.Manager.UserText();
            this.SetDB(userText);
            return userText.GetList(code, type);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int InsertUserText(FS.HISFC.Models.Base.UserText obj)
        {
            FS.HISFC.BizLogic.Manager.UserText usrTextMgr = new FS.HISFC.BizLogic.Manager.UserText();
            this.SetDB(usrTextMgr);
            return usrTextMgr.Insert(obj);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual int UpdateUserText(FS.HISFC.Models.Base.UserText obj)
        {
            FS.HISFC.BizLogic.Manager.UserText usrTextMgr = new FS.HISFC.BizLogic.Manager.UserText();
            this.SetDB(usrTextMgr);
            return usrTextMgr.Update(obj);
        }

        //public virtual int ReplaceSql(FS.HISFC.Models.Base.UserText obj, ref string sql)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}


        public virtual int UpdateFrequency(string id, string operId)
        {
            FS.HISFC.BizLogic.Manager.UserText userText = new FS.HISFC.BizLogic.Manager.UserText();
            this.SetDB(userText);
            return userText.UpdateFrequency(id, operId);
        }

        public virtual int DeleteUserText(string ID)
        {
            FS.HISFC.BizLogic.Manager.UserText userText = new FS.HISFC.BizLogic.Manager.UserText();
            this.SetDB(userText);
            return userText.Delete(ID);
        }

        public virtual ArrayList GetUserTextList(string GroupId, string Code, int Type)
        {
            FS.HISFC.BizLogic.Manager.UserText userText = new FS.HISFC.BizLogic.Manager.UserText();
            this.SetDB(userText);
            return userText.GetList(GroupId, Code, Type);
        }

        #endregion


        #endregion

        #region 查询条件管理 --Leiyj
        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <returns></returns>
        public virtual string GetQueryCondtion(string formName)
        {
            FS.HISFC.BizLogic.Manager.QueryCondition qc = new FS.HISFC.BizLogic.Manager.QueryCondition();
            this.SetDB(qc);
            return qc.GetQueryCondtion(formName);
        }

        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <returns></returns>
        public virtual string GetQueryCondtion(string formName, bool isDefault)
        {
            FS.HISFC.BizLogic.Manager.QueryCondition qc = new FS.HISFC.BizLogic.Manager.QueryCondition();
            this.SetDB(qc);
            return qc.GetQueryCondtion(formName, isDefault);
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public virtual int SetQueryCondition(string formName, string xml)
        {
            FS.HISFC.BizLogic.Manager.QueryCondition qc = new FS.HISFC.BizLogic.Manager.QueryCondition();
            this.SetDB(qc);
            return qc.SetQueryCondition(formName, xml);
        }

        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="xml"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public virtual int SetQueryCondition(string formName, string xml, bool isDefault)
        {
            FS.HISFC.BizLogic.Manager.QueryCondition qc = new FS.HISFC.BizLogic.Manager.QueryCondition();
            this.SetDB(qc);
            return qc.SetQueryCondition(formName, xml, isDefault);
        }
        #endregion

        #region 纸张管理 --Leiyj
        /// <summary>
        /// 获得纸张大小
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual FS.HISFC.Models.Base.PageSize GetPageSize(string pageName, string deptName)
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSize = new FS.HISFC.BizLogic.Manager.PageSize();
            this.SetDB(pageSize);
            return pageSize.GetPageSize(pageName, deptName);
        }

        /// <summary>
        /// 获得纸张大小
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual FS.HISFC.Models.Base.PageSize GetPageSize(string ID)
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSize = new FS.HISFC.BizLogic.Manager.PageSize();
            this.SetDB(pageSize);
            return pageSize.GetPageSize(ID);
        }

        #endregion

        #region 人员组管理

        /// <summary>
        /// 通过人员编码获得已经维护的登陆组信息
        /// </summary>
        /// <param name="personID">人员ID</param>
        /// <returns>成功 已经维护的登陆组信息 失败 null</returns>
        public virtual ArrayList GetPersonGroupList(string personID)
        {
            FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

            this.SetDB(userManager);

            return userManager.GetPersonGroupList(personID);
        }

        /// <summary>
        /// 获得所有登陆组信息
        /// </summary>
        /// <returns></returns>
        public virtual ArrayList QueryLogOnGroupList()
        {
            FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();

            this.SetDB(sysGroupManager);

            return sysGroupManager.GetList();
        }

        /// <summary>
        /// 设置人员组
        /// </summary>
        /// <param name="Person">人员基本信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public virtual int UpdatePersonGroup(FS.HISFC.Models.Base.Employee person)
        {
            FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

            this.SetDB(userManager);

            return userManager.UpdatePersonGroup(person);
        }

        /// <summary>
        /// 是否存在登陆名字
        /// </summary>
        /// <param name="loginName">登陆名</param>
        /// <param name="operCode">人员编码</param>
        /// <returns>1存在 其他不存在</returns>
        public virtual int IsExistLoginName(string loginName, string operCode)
        {
            FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

            this.SetDB(userManager);

            return userManager.IsExistLoginName(loginName, operCode);
        }

        /// <summary>
        /// 根据员工号获得登陆信息
        /// </summary>
        /// <param name="emplCode">员工编码</param>
        /// <returns>登陆信息集合</returns>
        public virtual ArrayList GetLoginInfoByEmplCode(string emplCode)
        {
            FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

            this.SetDB(userManager);

            return userManager.GetLoginInfoByEmplCode(emplCode);
        }

        /// <summary>
        /// 插入人员组
        /// </summary>
        /// <param name="person">人员信息</param>
        /// <param name="group">组信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public virtual int InsertPersonGroup(FS.HISFC.Models.Base.Employee person, FS.FrameWork.Models.NeuObject group)
        {
            FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

            this.SetDB(userManager);

            return userManager.InsertPersonGroup(person, group);
        }

        /// <summary>
        /// 删除人员组
        /// </summary>
        /// <param name="person">人员信息</param>
        /// <param name="group">组信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public virtual int DeletePersonGroup(FS.HISFC.Models.Base.Employee person, FS.FrameWork.Models.NeuObject group)
        {
            FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();


            this.SetDB(userManager);

            return userManager.DeletePersonGroup(person, group);
        }
        /// <summary>
        /// 根据出生日期获取年龄
        /// </summary>
        /// <param name="birthday">出生日期</param>
        /// <returns>年龄</returns>
        public virtual string GetAge(DateTime birthday)
        {
            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            this.SetDB(personManager);
            return personManager.GetAge(birthday);
        }
        /// <summary>
        /// 获取所有人员组列表
        /// </summary>
        /// <returns></returns>
        public virtual ArrayList GetPeronList()
        {
            FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

            this.SetDB(userManager);

            return userManager.GetPeronList();
        }
        #endregion

        #region 获得全部在用科室列表

        /// <summary>
        ///  获得全部在用科室列表
        /// </summary>
        /// <returns></returns>
        public virtual System.Collections.ArrayList GetDepartment()
        {

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            this.SetDB(deptMgr);
            return deptMgr.GetDeptmentAll();

        }

        #endregion

        #region 无用
        //#region 科室组套
        ///// <summary>
        /////  查询科室组套与个人组套
        ///// </summary>
        ///// <param name="t">组套类别 门诊/住院</param>
        ///// <param name="deptCode">  科室代码</param>
        ///// <param name="docCode"> 医生编码</param>
        ///// <returns>成功返回组套列表 失败返回null</returns>
        //public virtual System.Collections.ArrayList GetDeptOrderGroup(FS.HISFC.Models.Base.ServiceTypes t, string deptCode, string docCode)
        //{
        //    FS.HISFC.BizLogic.Manager.Group gMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(gMgr);
        //    return gMgr.GetDeptOrderGroup(t, deptCode, docCode);
        //}

        ///// <summary>
        /////  获得医嘱组套类别
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public virtual System.Collections.ArrayList GetAllOrderGroup(FS.HISFC.Models.Base.ServiceTypes t)
        //{
        //    FS.HISFC.BizLogic.Manager.Group gMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(gMgr);
        //    return gMgr.GetAllOrderGroup(t);
        //}

        ///// <summary>
        ///// 更新组套名称
        ///// </summary>
        ///// <param name="GroupId">组套ID</param>
        ///// <param name="GroupName">组套名称</param>
        ///// <returns></returns>
        //public virtual int UpdateGroupName(string GroupId, string GroupName)
        //{
        //    FS.HISFC.BizLogic.Manager.Group gMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(gMgr);
        //    return gMgr.UpdateGroupName(GroupId, GroupName);
        //}

        ///// <summary>
        ///// 删除一条组套
        ///// </summary>
        ///// <param name="gr"></param>
        ///// <returns></returns>
        //public virtual int DeleteGroup(FS.HISFC.Models.Base.Group gr)
        //{
        //    FS.HISFC.BizLogic.Manager.Group gMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(gMgr);
        //    return gMgr.DeleteGroup(gr);
        //}

        ///// <summary>
        ///// 删除组套明细
        ///// </summary>
        ///// <param name="group"></param>
        ///// <returns></returns>
        //public virtual int DeleteGroupOrder(FS.HISFC.Models.Base.Group group)
        //{
        //    FS.HISFC.BizLogic.Manager.Group grMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(grMgr);
        //    return grMgr.DeleteGroupOrder(group);
        //}

        ///// <summary>
        /////  获得项目
        ///// </summary>
        ///// <param name="group"></param>
        ///// <returns></returns>
        //public virtual System.Collections.ArrayList GetAllItem(FS.HISFC.Models.Base.Group group)
        //{
        //    FS.HISFC.BizLogic.Manager.Group grMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(grMgr);
        //    return grMgr.GetAllItem(group);
        //}

        ///// <summary>
        ///// 获得新的组ID
        ///// </summary>
        ///// <returns></returns>
        //public virtual String GetNewGroupID()
        //{
        //    FS.HISFC.BizLogic.Manager.Group grMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(grMgr);
        //    return grMgr.GetNewGroupID();
        //}

        ///// <summary>
        ///// 更新一条组
        ///// </summary>
        ///// <param name="ID"></param>
        ///// <returns></returns>
        //public virtual int UpdateGroup(FS.HISFC.Models.Base.Group group)
        //{
        //    FS.HISFC.BizLogic.Manager.Group grMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(grMgr);
        //    return grMgr.UpdateGroup(group);
        //}

        ///// <summary>
        ///// 更新一条组项目 门诊
        ///// </summary>
        ///// <param name="group"></param>
        ///// <param name="order"></param>
        ///// <returns></returns>
        //public virtual int UpdateGroupItem(FS.HISFC.Models.Base.Group group, FS.HISFC.Models.Order.OutPatient.Order order)
        //{
        //    FS.HISFC.BizLogic.Manager.Group grMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(grMgr);
        //    return grMgr.UpdateGroupItem(group, order);
        //}

        ///// <summary>
        ///// 更新一条组项目 住院
        ///// </summary>
        ///// <param name="group"></param>
        ///// <param name="order"></param>
        ///// <returns></returns>
        //public virtual int UpdateGroupItem(FS.HISFC.Models.Base.Group group, FS.HISFC.Models.Order.Inpatient.Order order)
        //{
        //    FS.HISFC.BizLogic.Manager.Group grMgr = new FS.HISFC.BizLogic.Manager.Group();
        //    this.SetDB(grMgr);
        //    return grMgr.UpdateGroupItem(group, order);
        //}
        //#endregion
        //#region 药品管理
        ///// <summary>
        ///// 获取药理作用列表
        ///// </summary>
        ///// <returns></returns>
        //public virtual ArrayList QueryPhaFunction()
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Constant manager = new FS.HISFC.BizLogic.Pharmacy.Constant();
        //    this.SetDB(manager);
        //    return manager.QueryPhaFunction();
        //}
        //public virtual System.Collections.ArrayList LoadAllDrug()
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Item manager = new FS.HISFC.BizLogic.Pharmacy.Item();
        //    this.SetDB(manager);
        //    return new ArrayList(manager.QueryItemList(false).ToArray());
        //}

        //public virtual System.Collections.ArrayList QueryDrug(FS.HISFC.Models.Base.Hospital hospital)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual System.Collections.ArrayList QueryDrug(FS.HISFC.Models.Base.Hospital hospital, string drugType)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int AddDrug(FS.HISFC.Models.Base.Hospital hospital, FS.HISFC.Models.Pharmacy.Item item)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int ModifyDrug(FS.HISFC.Models.Pharmacy.Item item)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int DeleteDrug()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual System.Collections.ArrayList GetOrderTypeList()
        //{
        //    FS.HISFC.BizLogic.Manager.OrderType orderType = new FS.HISFC.BizLogic.Manager.OrderType();
        //    this.SetDB(orderType);
        //    return orderType.GetList();
        //}
        ///// <summary>
        ///// 获取药品信息
        ///// </summary>
        ///// <returns></returns>
        //public virtual ArrayList QueryItemList()
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        //    this.SetDB(itemManager);
        //    return new ArrayList(itemManager.QueryItemList());
        //}
        ///// <summary>
        ///// 根据编码获取药品
        ///// </summary>
        ///// <param name="code"></param>
        ///// <returns></returns>
        //public virtual FS.HISFC.Models.Pharmacy.Item GetItem(string code)
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        //    this.SetDB(itemManager);
        //    return itemManager.GetItem(code);
        //}

        ///// <summary>
        ///// 根据编号获取药品存储的行号
        ///// </summary>
        ///// <param name="drugNo"></param>
        ///// <returns></returns>
        //public virtual int GetDrugStorageRowNum(string drugNo)
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        //    this.SetDB(itemManager);
        //    return itemManager.GetDrugStorageRowNum(drugNo);
        //}

        ///// <summary>
        ///// 根据编号删除药品信息
        ///// </summary>
        ///// <param name="drugNo"></param>
        ///// <returns></returns>
        //public virtual int DeleteItem(string drugNo)
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        //    this.SetDB(itemManager);
        //    return itemManager.DeleteItem(drugNo);
        //}

        ///// <summary>
        ///// 添加药品信息
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //public virtual string SetItem(FS.HISFC.Models.Pharmacy.Item item)
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        //    this.SetDB(itemManager);
        //    int i = itemManager.SetItem(item);

        //    if (i == -1)
        //        return i.ToString();
        //    else
        //        return item.ID;

        //}
        ///// <summary>
        ///// 根据用户代码获取有效的药品信息
        ///// </summary>
        ///// <param name="userCode"></param>
        ///// <returns></returns>
        //public virtual ArrayList QueryValidDrugByCustomCode(string userCode)
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        //    this.SetDB(itemManager);
        //    return new ArrayList(itemManager.QueryValidDrugByCustomCode(userCode));
        //}
        ///// <summary>
        ///// 获取药理叶子节点数据
        ///// </summary>
        ///// <returns></returns>
        //public virtual ArrayList QueryPhaFunctionLeafage()
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Constant itemConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
        //    this.SetDB(itemConsManager);
        //    return itemConsManager.QueryPhaFunctionLeafage();
        //}
        ///// <summary>
        ///// 获取药品供应商信息
        ///// </summary>
        ///// <param name="s"></param>
        ///// <returns></returns>
        //public virtual ArrayList QueryCompany(string type)
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Constant company = new FS.HISFC.BizLogic.Pharmacy.Constant();
        //    this.SetDB(company);
        //    return company.QueryCompany(type);
        //}
        //#endregion

        //#region 非药品管理
        //public virtual System.Collections.ArrayList LoadAllUnDrug()
        //{
        //    FS.HISFC.BizLogic.Fee.Item manager = new FS.HISFC.BizLogic.Fee.Item();
        //    this.SetDB(manager);
        //    return new ArrayList(manager.QueryAllItemList());
        //}
        //public virtual bool UnDrugIsUsed(string code)
        //{
        //    FS.HISFC.BizLogic.Fee.Item manager = new FS.HISFC.BizLogic.Fee.Item();
        //    this.SetDB(manager);
        //    return manager.IsUsed(code);
        //}
        //public virtual int DeleteUndrugItemByCode(string code)
        //{
        //    FS.HISFC.BizLogic.Fee.Item manager = new FS.HISFC.BizLogic.Fee.Item();
        //    this.SetDB(manager);
        //    return manager.DeleteUndrugItemByCode(code);
        //}
        //public virtual System.Collections.ArrayList QueryUnDrug(FS.HISFC.Models.Base.Hospital hospital)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int AddUnDrug(FS.HISFC.Models.Base.Hospital hospital, FS.HISFC.Models.Fee.Item.Undrug unDrug)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int ModifyUnDrug(FS.HISFC.Models.Fee.Item.Undrug unDrug)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int DeleteUnDrug(FS.HISFC.Models.Fee.Item.Undrug unDrug)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}
        ///// <summary>
        ///// 添加非药品
        ///// </summary>
        ///// <param name="unDrug"></param>
        ///// <returns></returns>
        //public virtual int InsertUndrugItem(FS.HISFC.Models.Fee.Item.Undrug unDrug)
        //{
        //    FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
        //    this.SetDB(item);
        //    return item.InsertUndrugItem(unDrug);
        //}
        ///// <summary>
        ///// 修改非药品
        ///// </summary>
        ///// <param name="unDrug"></param>
        ///// <returns></returns>
        //public virtual int UpdateUndrugItem(FS.HISFC.Models.Fee.Item.Undrug unDrug)
        //{
        //    FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
        //    this.SetDB(item);
        //    return item.UpdateUndrugItem(unDrug);
        //}
        ///// <summary>
        ///// 得到一个新流水号
        ///// </summary>
        ///// <returns></returns>
        //public virtual string GetUndrugCode()
        //{
        //    FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
        //    this.SetDB(item);
        //    return item.GetUndrugCode();
        //}
        //#endregion

        //#region 字典管理
        //public virtual System.Collections.ArrayList GetConstByGroup(string groupID)
        //{
        //    FS.HISFC.BizLogic.Manager.SysGroup sysGroup = new FS.HISFC.BizLogic.Manager.SysGroup();
        //    this.SetDB(sysGroup);
        //    return this.GetConstByGroup(groupID);
        //}
        //public virtual System.Collections.ArrayList LoadAllDictionary()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual System.Collections.ArrayList QueryDictinary(FS.HISFC.Models.Base.Hospital hospital)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual System.Collections.ArrayList QueryDictionary(FS.HISFC.Models.Base.Hospital hospital, string dictionaryType)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int AddDictionary(FS.HISFC.Models.Base.Hospital hospital)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int ModifyDictionary()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual int DeleteDictionary()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}
        //public virtual ArrayList GetConstantList(string constant)
        //{
        //    FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //    this.SetDB(con);
        //    return con.GetList(constant);
        //}
        //#endregion

        //#region 频次管理
        //public virtual int AddFrequency(FS.HISFC.Models.Order.Frequency frequency)
        //{
        //    FS.HISFC.BizLogic.Manager.Frequency frequencyManager = new FS.HISFC.BizLogic.Manager.Frequency();
        //    this.SetDB(frequencyManager);
        //    return frequencyManager.Set(frequency);
        //}

        //public virtual int DelFrequenty(FS.HISFC.Models.Order.Frequency frequency)
        //{
        //    FS.HISFC.BizLogic.Manager.Frequency frequencyManager = new FS.HISFC.BizLogic.Manager.Frequency();
        //    this.SetDB(frequencyManager);
        //    return frequencyManager.Del(frequency);
        //}
        //public virtual ArrayList GetFrequencyList(string depCode)
        //{
        //    FS.HISFC.BizLogic.Manager.Frequency frequencyManager = new FS.HISFC.BizLogic.Manager.Frequency();
        //    this.SetDB(frequencyManager);
        //    return frequencyManager.GetList(depCode);
        //}

        //public virtual int ExistFrequencyCounts(FS.HISFC.Models.Order.Frequency frequency)
        //{
        //    FS.HISFC.BizLogic.Manager.Frequency frequencyManager = new FS.HISFC.BizLogic.Manager.Frequency();
        //    this.SetDB(frequencyManager);
        //    return frequencyManager.ExistFrequencyCounts(frequency);
        //}
        //public virtual int DelFrequency(FS.HISFC.Models.Order.Frequency frequency)
        //{
        //    FS.HISFC.BizLogic.Manager.Frequency frequencyManager = new FS.HISFC.BizLogic.Manager.Frequency();
        //    this.SetDB(frequencyManager);
        //    return frequencyManager.Del(frequency);
        //}
        //#endregion

      

     

        //#region 常数管理 --Leiyj
        //public virtual System.Collections.ArrayList GetConstantList(FS.HISFC.Models.Base.EnumConstant constType)
        //{
        //    FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //    this.SetDB(con);
        //    string type = constType.ToString();
        //    return con.GetList(type);
        //}
        ///// <summary>
        ///// 根据传入TYPE获得所有常数列表
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public virtual ArrayList GetConstantListFromType(string type)
        //{
        //    FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //    this.SetDB(con);
        //    return con.GetAllList(type);

        //}
        ///// <summary>
        ///// 更新常数表中的一条数据
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="cost"></param>
        ///// <returns></returns>
        //public virtual int UpdateConst(string type, FS.HISFC.Models.Base.Const cost)
        //{
        //    FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //    this.SetDB(con);
        //    return con.UpdateItem(type, cost);
        //}
        ///// <summary>
        ///// 插入常数表中的一条数据
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="cost"></param>
        ///// <returns></returns>
        //public virtual int InsertConst(string type, FS.HISFC.Models.Base.Const cost)
        //{
        //    FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //    this.SetDB(con);
        //    return con.InsertItem(type, cost);
        //}

        //#endregion

        //#region 床位管理 --Leiyj
        ///// <summary>
        ///// 获取所有床位信息
        ///// </summary>
        ///// <param name="dv">返回的数据视图</param>
        ///// <returns>1 成功 ;-1 失败</returns>
        //public virtual int QueryBedInfo(ref System.Data.DataView dv)
        //{
        //    FS.HISFC.BizLogic.Manager.Bed bedMgr = new FS.HISFC.BizLogic.Manager.Bed();
        //    this.SetDB(bedMgr);
        //    return bedMgr.QueryBedInfo(ref dv);
        //}

        ///// <summary>
        ///// 获取床位信息,根据护士站ID
        ///// </summary>
        ///// <param name="id">护士站ID</param>
        ///// <param name="dv">返回的数据视图</param>
        ///// <returns>-1,失败; 1,成功</returns>
        //public virtual int QueryBedInfoByNurseStationID(string id, ref System.Data.DataView dv)
        //{
        //    FS.HISFC.BizLogic.Manager.Bed bed = new FS.HISFC.BizLogic.Manager.Bed();
        //    this.SetDB(bed);
        //    return bed.QueryBedInfoByNurseStationID(id, ref dv);
        //}
        //#endregion

       

       

        //#region 权限管理 --Leiyj
        ///// <summary>
        ///// 根据人员编码，二级权限编码取人员拥有权限的部门
        ///// </summary>
        ///// <param name="userCode">操作员编码</param>
        ///// <param name="class2Code">二级权限码</param>
        ///// <returns>成功返回具有权限的科室集合 失败返回null</returns>   
        //public virtual System.Collections.Generic.List<FS.FrameWork.Object.NeuObject> QueryUserPriv(string userCode, string class2Code)
        //{
        //    FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPorwerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        //    this.SetDB(userPorwerMgr);
        //    return userPorwerMgr.QueryUserPriv(userCode, class2Code);
        //}

        ///// <summary>
        ///// 取操作员所拥有的权限操作科室
        ///// </summary>
        ///// <param name="userCode">操作员编码</param>
        ///// <param name="class2Code">二级权限码</param>
        ///// <param name="class3Code">三级权限码</param>
        ///// <returns>成功返回具有权限的科室集合 失败返回null</returns>
        //public virtual System.Collections.Generic.List<FS.FrameWork.Object.NeuObject> QueryUserPriv(string userCode, string class2Code, string class3Code)
        //{
        //    FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        //    this.SetDB(userPowerMgr);
        //    return userPowerMgr.QueryUserPriv(userCode, class2Code, class3Code);
        //}
        //#endregion

       
        #region IManager 成员

        ///// <summary>
        ///// 根据药品编码获得某一药品信息
        ///// </summary>
        ///// <param name="ID">药品编码</param>
        ///// <returns>成功返回药品实体 失败返回null</returns>
        //public virtual FS.HISFC.Models.Pharmacy.Item QueryDrug(string ItemCode)
        //{
        //    FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
        //    this.SetDB(itemMgr);
        //    return itemMgr.GetItem(ItemCode);
        //}

        //#region 据非药品编码获得该项目信息(该项目必须有效)
        ////
        //// 摘要:
        ////     根据非药品编码获得该项目信息(该项目必须有效)
        ////
        //// 参数:
        ////   undrugCode:
        ////     非药品编码
        ////
        //// 返回结果:
        ////     成功:返回非药品实体 失败:返回null
        //public virtual FS.HISFC.Models.Fee.Item.Undrug GetValidItemByUndrugCode(string undrugCode)
        //{
        //    FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
        //    this.SetDB(itemMgr);
        //    return itemMgr.GetValidItemByUndrugCode(undrugCode);
        //}

        //#endregion
        //public virtual FS.HISFC.Models.Fee.Item.Undrug QueryUnDrug(string itemCode)
        //{
        //    FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
        //    this.SetDB(itemMgr);
        //    return itemMgr.GetValidItemByUndrugCode(itemCode);
        //}

        //public virtual string QueryControlerInfo(string controlID)
        //{
        //    return null;
        //}

        #endregion

       

       

      

        //#region IManager 成员


        //public virtual string GetNewOrderComboID()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //#endregion

      



      

       
       

        #endregion
        #region 消息管理
        /// <summary>
        /// 查询消息
        /// </summary>
        /// <returns></returns>
        private ArrayList QueryMessage(string oper)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.QueryMessage(oper);
           
        }
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Message QueryMessageById(string id)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.QueryMessageById(id);

        }
        /// <summary>
        /// 插入一条消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int InsertMessage(FS.HISFC.Models.Base.Message message)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.InsertMessage(message);

        }
        private int UpdateMessage(FS.HISFC.Models.Base.Message message)
        {
            FS.HISFC.BizLogic.EPR.Message messageManager = new FS.HISFC.BizLogic.EPR.Message();

            this.SetDB(messageManager);

            return messageManager.UpdateMessage(message);

        }
        #endregion 
        #region 日程管理
        /// <summary>
        /// 查询全部日程
        /// </summary>
        /// <returns></returns>
        private ArrayList QueryCalendar()
        {
            FS.HISFC.BizLogic.EPR.Calendar calendarManager = new FS.HISFC.BizLogic.EPR.Calendar();

            this.SetDB(calendarManager);

            return calendarManager.QueryCalendar();
            
        }
        /// <summary>
        /// 增加日程
        /// </summary>
        /// <param name="calendar"></param>
        /// <returns></returns>
        private int AddCalender(FS.HISFC.Models.Base.Calendar calendar)
        {
            FS.HISFC.BizLogic.EPR.Calendar calendarManager = new FS.HISFC.BizLogic.EPR.Calendar();

            this.SetDB(calendarManager);

            return calendarManager.InsertCalendar(calendar);

        }
        /// <summary>
        /// 按时间段查询
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private ArrayList QueryCalendar(DateTime dtBegin, DateTime dtEnd)
        {
            FS.HISFC.BizLogic.EPR.Calendar calendarManager = new FS.HISFC.BizLogic.EPR.Calendar();

            this.SetDB(calendarManager);

            return calendarManager.QueryCalendar(dtBegin,dtEnd);

        }
        #endregion 



    }
}
