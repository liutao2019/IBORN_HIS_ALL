using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Manager;
using System.Collections;
using System.Text.RegularExpressions;
namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [功能描述: 整合的管理类]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Manager : IntegrateBase
    {
        public  Manager()
        {
            
        }

        protected FS.HISFC.BizLogic.Manager.Constant managerConstant = new FS.HISFC.BizLogic.Manager.Constant();
        protected FS.HISFC.BizLogic.Manager.Department managerDepartment = new Department();
        protected FS.HISFC.BizLogic.Manager.Person manangerPerson = new Person();
        protected FS.HISFC.BizLogic.Manager.OrderType orderType = new OrderType( );
        protected FS.HISFC.BizLogic.Manager.Frequency managerFrequency = new Frequency();
        protected FS.HISFC.BizLogic.Manager.Bed managerBed = new Bed();
        protected FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// 组套业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.ComGroupTail comGroupDetailManager = new ComGroupTail();
        /// <summary>
        /// 合同单位关系业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.PactStatRelation pactStatRelationManager = new PactStatRelation();
        /// <summary>
        /// 合同单位比例
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfoManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        /// <summary>
        /// 午别
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Noon noonManager = new FS.HISFC.BizLogic.Registration.Noon();
        /// <summary>
        /// 挂号业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 分诊业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Nurse.Assign assignManager = new FS.HISFC.BizLogic.Nurse.Assign();
        protected FS.HISFC.BizLogic.Nurse.Room roomManager = new FS.HISFC.BizLogic.Nurse.Room();
        protected FS.HISFC.BizLogic.Nurse.Seat seatManager = new FS.HISFC.BizLogic.Nurse.Seat();
        #region addby xuewj 2010-11-2 增加叫号按钮{5A8B39E0-76A8-4e68-AF14-E2E0F45617D1}
        protected FS.HISFC.BizLogic.Nurse.Queue queueManager = new FS.HISFC.BizLogic.Nurse.Queue(); 
        #endregion
        protected FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new UserPowerDetailManager();

        protected FS.HISFC.BizLogic.Manager.DepartmentStatManager departStatManager = new DepartmentStatManager();

        //protected static FS.HISFC.BizLogic.Fee.UndrugComb undrugztManager = new FS.HISFC.BizLogic.Fee.UndrugComb();

        protected FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackageManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// 住院业务
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.InPatient managerInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// 用户文本
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.UserText userTextManager = new UserText();
        protected FS.HISFC.BizLogic.Manager.Spell spellManager = new Spell();
        /// <summary>
        /// 设置Trans
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;

            managerConstant.SetTrans(trans);
            managerDepartment.SetTrans(trans);
            manangerPerson.SetTrans(trans);
            orderType.SetTrans( trans );
            managerFrequency.SetTrans(trans);
            managerBed.SetTrans(trans);
            controler.SetTrans(trans);
            pactStatRelationManager.SetTrans(trans);
            comGroupDetailManager.SetTrans(trans);
            assignManager.SetTrans(trans);
            managerInpatient.SetTrans(trans);
            userTextManager.SetTrans(trans);
            spellManager.SetTrans(trans);
            undrugPackageManager.SetTrans(trans);
            userPowerDetailManager.SetTrans(trans);
        }

        #region 合同单位关系

        /// <summary>
        /// 通过合同单位编码获得合同单位关系
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <returns>成功 : 返回合同单位关系数组 失败 null</returns>
        public ArrayList QueryRelationsByPactCode(string pactCode) 
        {
            return pactStatRelationManager.GetRelationByPactCode(pactCode);
        }
        /// <summary>
        /// 获得所有合同单位信息
        /// </summary>
        /// <returns>成功: 合同单位集合 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public ArrayList QueryPactUnitAll()
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitAll();
        }
        /// <summary>
        /// 获得门诊合同单位信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitOutPatient()
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitOutPatient();
        }
        /// <summary>
        /// 获得住院合同单位信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitInPatient()
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitInPatient();
        }
        /// <summary>
        /// 根据简明模糊查询取合同单位信息
        /// </summary>
        /// <param name="shortName">简名</param>
        /// <returns>成功: 合同单位集合 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public ArrayList QueryPactUnitByShortNameLiked(string shortName)
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitByShortNameLiked(shortName);
        }
        /// <summary>
        /// 根据结算类别取合同单位
        /// </summary>
        /// <param name="payKindCode">结算类别编码</param>
        /// <returns>成功: 合同单位集合 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        public ArrayList QueryPactUnitByPayKindCode(string payKindCode)
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.QueryPactUnitByPayKindCode(payKindCode);
        }
        /// <summary>
		/// 根据合同代码查询
		/// </summary>
        /// <param name="pactCode">合同单位代码</param>
		/// <returns>成功 合同单位实体 失败 Null</returns>
        public FS.HISFC.Models.Base.PactInfo GetPactUnitInfoByPactCode(string pactCode)
        {
            this.SetDB(pactUnitInfoManager);
            return pactUnitInfoManager.GetPactUnitInfoByPactCode(pactCode);
        }
        //修改将IsDrug(是否药品)由Bool改为枚举EnumItemType代替 Drug:药品 Undrug:非药品 MatItem物资
        /// <summary>
		/// 根据合同单位和项目代码得到项目价格
		/// </summary>
		/// <param name="patient"></param>
		/// <param name="IsDrug"></param>
		/// <param name="ItemID"></param>
		/// <param name="Price"></param>
		/// <returns></returns>
        [Obsolete("停用此方法，改为Intergrate.Fee里面", true)]
        public int GetPrice(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Base.EnumItemType IsDrug, string ItemID, ref decimal Price)
        {
            this.SetDB(pactUnitInfoManager);

            return -1;// pactUnitInfoManager.GetPrice(patient, IsDrug, ItemID, ref Price);
        }
        #endregion

        #region 常数

        

        /// <summary>
        /// 获得常数
        /// </summary>
        /// <returns></returns>
        public  ArrayList GetConstantList(FS.HISFC.Models.Base.EnumConstant constant)
        {
            this.SetDB(managerConstant);
            return managerConstant.GetList(constant);
        }

        /// <summary>
        /// 获取账户类型 
        ///{F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAccountTypeList()
        {
            return this.GetAccountTypeList("ALL");
        }

        /// <summary>
        /// 根据类型获取账户类型
        /// {ECECDF2F-BA74-4615-A240-C442BE0A0074}
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAccountTypeList(string Type)
        {
            this.SetDB(managerConstant);
            return managerConstant.GetAccountTypeList(Type);
        }


        /// <summary>
        /// 根据类别获得常数列表
        /// </summary>
        /// <param name="type">常数类别</param>
        /// <returns></returns>
        public ArrayList GetConstantList(string type) 
        {
            this.SetDB(managerConstant);
            return managerConstant.GetList(type);
        }

        /// <summary>
		/// 获得常数列的一个实体
		/// </summary>
		/// <param name="type"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetConstant(string type, string ID)
        {
            this.SetDB(managerConstant);
            return managerConstant.GetConstant(type, ID);
        }
        /// <summary>
        /// 获取常数
        /// </summary>
        /// <param name="constant"></param>
        /// <returns></returns>
        public ArrayList QueryConstantList(string constant)
        {
            this.SetDB(managerConstant);
            return managerConstant.GetList(constant);
        }

        /// <summary>
        /// 获得一个常数实体
        /// </summary>
        /// <param name="type">常数类型</param>
        /// <param name="ID">项目编码</param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetConstansObj(string type, string ID) 
        {
            this.SetDB(managerConstant);

            return managerConstant.GetConstant(type, ID);
        }

        /// <summary>
        /// 插入常数信息
        /// </summary>
        /// <param name="type">常数类别</param>
        /// <param name="constant">常数实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int InsertConstant(string type, FS.HISFC.Models.Base.Const constant) 
        {
            this.SetDB(managerConstant);

            return managerConstant.InsertItem(type, constant);
        }

        /// <summary>
        /// 更新常数信息
        /// </summary>
        /// <param name="type">常数类别</param>
        /// <param name="constant">常数实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int UpdateConstant(string type, FS.HISFC.Models.Base.Const constant)
        {
            this.SetDB(managerConstant);

            return managerConstant.UpdateItem(type, constant);
        }

        #endregion

        #region 科室
        /// <summary>
        /// 根据传入科室类型获得科室列表
        /// </summary>
        /// <param name="type">组套用</param>
        /// <returns></returns>
        public ArrayList GetDeptmentByType(string type)
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetDeptmentByType(type);
        }
        /// <summary>
        /// 获得科室列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType type)
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetDeptment(type);
        }
        public ArrayList GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType Type)
        {
            SetDB(managerDepartment);
            return managerDepartment.GetDeptmentIn(Type);
        }

        /// <summary>
        /// 获取挂号科室列表
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryRegDepartment()
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetRegDepartment();
        }
        /// <summary>
        /// 通过科室编码获得科室信息
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功: 科室信息 失败: null</returns>
        public FS.HISFC.Models.Base.Department GetDepartment(string deptCode) 
        {
            this.SetDB(managerDepartment);

            return managerDepartment.GetDeptmentById(deptCode);
        }

        /// <summary>
        /// 获得全部科室列表
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDepartment()
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetDeptmentAll();
        }

        /// <summary>
        /// 获得在院科室列表
        /// </summary>
        /// <param name="isInHos"></param>
        /// <returns></returns>
        public ArrayList QueryDeptmentsInHos(bool isInHos) 
        {
            this.SetDB(managerDepartment);

            return managerDepartment.GetInHosDepartment(isInHos);
        }

        /// <summary>
		///  
		/// 获得所有在用的科室
		/// </summary>
		/// <returns></returns>
        public ArrayList GetDeptmentAllValid()
        {
            this.SetDB(managerDepartment);

            return managerDepartment.GetDeptmentAll();
        }
        /// <summary>
        /// 查询病区包含的科室
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryDepartment(string nurseCode)
        {
            this.SetDB(managerDepartment);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = nurseCode;
            return managerDepartment.GetDeptFromNurseStation(obj);
        }

        /// <summary>
        /// 查询病区包含的分诊科室
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryDepartmentForArray(string nurseCode)
        {
            this.SetDB(managerDepartment);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = nurseCode;
            return managerDepartment.GetDeptFromNurseStationForArray(obj);
        }

        #endregion

        #region 人员

        /// <summary>
        /// 获取拥有指定科室登录权限的人员列表
        /// </summary>
        /// <param name="deptcode"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList GetEmployeeHasAccLoinByDept(string deptcode, FS.HISFC.Models.Base.EnumEmployeeType type)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetEmployeeHasAccLoinByDept(deptcode, type);
        }


        /// <summary>
        /// 获取助产人员列表 {0a849cd8-db12-48e0-97ff-0b34f287c0a0}
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDeliverEmployee()
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetDeliverEmployee();
        
        }

        /// <summary>
        /// 根据人员类型获得人员列表
        /// </summary>
        /// <param name="emplType">人员类型枚举</param>
        /// <returns>人员列表</returns>
        public ArrayList QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType emplType) 
        {
            this.SetDB(manangerPerson);

            return manangerPerson.GetEmployee(emplType);
        }


           /// <summary>
        /// 根据人员类型获得人员列表,妇产科优先{BF4583B0-B5C7-490e-8AB3-1B6708E7A162}
        /// </summary>
        /// <param name="emplType">人员类型枚举</param>
        /// <returns>人员列表</returns>
        public ArrayList QueryEmployee4(FS.HISFC.Models.Base.EnumEmployeeType emplType) 
        {
            this.SetDB(manangerPerson);

            return manangerPerson.GetEmployee4(emplType);
        }
        /// <summary>
        /// 根据科室编码取人员列表
        /// </summary>
        /// <param name="deptID">科室编码</param>
        /// <returns></returns>
        public ArrayList QueryEmployeeByDeptID(string deptID)
        {
            this.SetDB(manangerPerson);

            return manangerPerson.GetPersonsByDeptID(deptID);
             
        }
        /// <summary>
        /// 获得人员列表
        /// </summary>
        /// <param name="emplType"></param>
        /// <param name="deptcode"></param>
        /// <returns></returns>
        public ArrayList QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType emplType,string deptcode)
        {
            this.SetDB(manangerPerson);

            return manangerPerson.GetEmployee(emplType,deptcode);
        }
        /// <summary>
        /// 获得排班专家的人员列表
        /// </summary>
        /// <param name="emplType"></param>
        /// <param name="deptcode"></param>
        /// <returns></returns>
        public ArrayList QueryEmployeeForScama(FS.HISFC.Models.Base.EnumEmployeeType emplType, string deptcode)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetEmployeeForScama(emplType, deptcode);
 
        }
        /// <summary>
        /// 获得全部人员列表
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryEmployeeAll( )
        {
            this.SetDB( manangerPerson );

            return manangerPerson.GetEmployeeAll( );
        }

        /// <summary>
        /// 根据人员ID获取人员信息
        /// </summary>
        /// <param name="emplID">人员id</param>
        /// <returns>人员信息</returns>
        public FS.HISFC.Models.Base.Employee  GetEmployeeInfo(string emplID)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetPersonByID(emplID);
        }

        /// <summary>
        /// 根据身份证号获取人员信息
        /// </summary>
        /// <param name="idenNo">身份证号</param>
        /// <returns>人员信息</returns>
        public FS.HISFC.Models.Base.Employee GetEmployeeInfoByIendNo(string idenNo)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetPersonByIdenNo(idenNo);
        }

        /// <summary>
        /// 获得护士列表
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryNurse(string nurseCode)
        {
            this.SetDB(manangerPerson);
            return manangerPerson.GetNurse(nurseCode);
        }

        /// <summary>
        /// 获得非护士人员列表
        /// </summary>
        /// <param name="deptID">科室编码</param>
        /// <returns>人员列表</returns>
        public ArrayList QueryEmployeeExceptNurse(string deptID)
        {
            this.SetDB( manangerPerson );

            return manangerPerson.GetAllButNurse( deptID );
        }
        #endregion

        #region 医嘱类型
        /// <summary>
        /// 获取医嘱类型列表
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryOrderTypeList( )
        {
            this.SetDB( orderType );
            return orderType.GetList( );
        }
        #endregion

        #region 医嘱频次
        /// <summary>
        /// 查询医嘱频次
        /// </summary>
        /// <returns></returns>
        public ArrayList QuereyFrequencyList()
        {
            this.SetDB( managerFrequency );
            return managerFrequency.GetAll("Root");
        }

        /// <summary>
        /// 获得特殊频次
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Frequency QuereySpecialFrequencyList(string orderID,string comboNO)
        {
            this.SetDB(managerFrequency);
            return managerFrequency.GetDfqspecial(orderID, comboNO);
        }
        #endregion

        #region 病床
        /// <summary>
        /// 获得病床列表
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryBedList(string nurseCode)
        {
            this.SetDB(managerBed);

            return managerBed.GetBedList(nurseCode);
        }

        /// <summary>
        /// 获得病区空床信息
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryUnoccupiedBed(string nurseCode)
        {
            this.SetDB(managerBed);

            return managerBed.GetUnoccupiedBed(nurseCode);
        }

        /// <summary>
        /// 获得病床信息
        /// </summary>
        /// <param name="bedNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Bed GetBed(string bedNo)
        {
            this.SetDB(managerBed);

            return managerBed.GetBedInfo(bedNo);
        }

        /// <summary>
        /// 设置病床信息
        /// </summary>
        /// <param name="bed"></param>
        /// <returns></returns>
        public int SetBed(FS.HISFC.Models.Base.Bed bed)
        {
            this.SetDB(managerBed);

            return managerBed.SetBedInfo(bed);
        }

        /// <summary>
        /// 删除病床信息
        /// </summary>
        /// <param name="bedNo"></param>
        /// <returns></returns>
        public int DeleteBed(string bedNo)
        {
            this.SetDB(managerBed);

            return managerBed.DeleteBedInfo(bedNo);
        }


        /// <summary>
        /// 获得护理组
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryBedNurseTendGroupList(string nurseCode)
        {
            this.SetDB(managerBed);

            return managerBed.GetBedNurseTendGroupList(nurseCode);
        }

        /// <summary>
        /// 更新护理组
        /// </summary>
        /// <param name="bedNo"></param>
        /// <param name="nurseTendGroup"></param>
        /// <returns></returns>
        public int UpdateNurseTendGroup(string bedNo,string nurseTendGroup)
        {
            this.SetDB(managerBed);

            return managerBed.UpdateNurseTendGroup(bedNo, nurseTendGroup);
        }
        #endregion

        #region 控制Controler

        /// <summary>
        /// 根据控制类代码检索控制类型的值
        /// </summary>
        /// <param name="ControlerCode"></param>
        /// <returns></returns>
        public string QueryControlerInfo(string ControlerCode)
        {
            this.SetDB(controler);
            return controler.QueryControlerInfo(ControlerCode);
        }

        /// <summary>
        /// 根据控制类代码检索控制类型的值
        /// </summary>
        /// <param name="ControlerKind"></param>
        /// <returns></returns>
        public ArrayList QueryControlerInfoByKind(string ControlerKind)
        {
            this.SetDB(controler);
            return controler.QueryControlInfoByKind(ControlerKind);
        }

        /// <summary>
        /// 插入常数信息
        /// </summary>
        /// <param name="c">常数实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int InsertControlerInfo(FS.HISFC.Models.Base.ControlParam c) 
        {
            this.SetDB(controler);

            return controler.AddControlerInfo(c);
        }

        /// <summary>
        /// 更新常数信息 
        /// </summary>
        /// <param name="c">常数实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int UpdateControlerInfo(FS.HISFC.Models.Base.ControlParam c)
        {
            this.SetDB(controler);

            return controler.UpdateControlerInfo(c);
        }



        #endregion

        #region 组套

        /// <summary>
        /// 通过组套编码获得组套明细项目集合
        /// </summary>
        /// <param name="groupCode">组套编码</param>
        /// <returns>成功组套明细项目集合 失败 null </returns>
        public ArrayList QueryGroupDetailByGroupCode(string groupCode) 
        {
            this.SetDB(comGroupDetailManager);

            return comGroupDetailManager.GetComGroupTailByGroupID(groupCode);
        }

        public ArrayList GetValidGroupList(string deptID)
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new ComGroup();
            this.SetDB( groupManager );

            return groupManager.GetValidGroupList( deptID );
        }

         /// <summary>
        /// 根据科室获取所有组套{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        /// </summary>
        /// <param name="GroupKind">0 财务用，1科室用,ALL 全部</param>
        /// <returns></returns>
        public ArrayList GetValidGroupListByRoot(string deptCode)
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new ComGroup();
            this.SetDB(groupManager);

            return groupManager.GetValidGroupListByRoot(deptCode);
        }

         /// <summary>
        /// 获取所有组套{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        /// </summary>
        /// <param name="GroupKind">0 财务用，1科室用,ALL 全部</param>
        /// <returns></returns>
        public ArrayList GetGroupsByDeptParent(string GroupKind, string deptCode, string parentGroupID)
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new ComGroup();
            this.SetDB(groupManager);

            return groupManager.GetGroupsByDeptParent(GroupKind, deptCode, parentGroupID);
        }
        #endregion

        #region 分诊

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="roomID">诊台代码</param>
        /// <param name="state">状态 1.进诊患者   2.已诊患者</param>
        /// <param name="doctID">医生编码</param>
        /// <returns>分诊实体树祖</returns>
        public ArrayList QueryPatient(DateTime beginTime, DateTime endTime, string roomID, String state, string doctID)
        {
            this.SetDB(assignManager);
            return assignManager.QueryPatient(beginTime, endTime, roomID, state, doctID);
        }

          /// <summary>
        /// 根据状态查询分诊患者
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">截至时间</param>
        /// <param name="consoleID">诊台代码</param>
        /// <param name="state">状态 1.进诊患者   2.已诊患者</param>
        /// <returns>ArrayList (分诊实体数组)</returns>
        public ArrayList QueryAssignPatientByState(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID)
        {
            this.SetDB(assignManager);
            return assignManager.QueryAssignPatientByState(beginTime, endTime, consoleID, state, doctID);
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="deptID">科室代码</param>
        /// <param name="roomID">诊台代码</param>
        /// <returns>分诊实体树祖</returns>
        public ArrayList QueryPatient(string deptID, string roomID)
        {
            this.SetDB(assignManager);
            return assignManager.Query(deptID, roomID);
        }

        /// <summary>
        /// 根据诊室号码获取诊台
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public ArrayList QuerySeatByRoomNo(string roomNo)
        {
            this.SetDB(seatManager);
            return seatManager.QueryValid(roomNo);
        }

        /// <summary>
        /// 根据科室获取诊室列表
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryRoomByDeptID(string deptID)
        {
            this.SetDB(roomManager);
            return roomManager.GetRoomInfoByNurseNoValid(deptID);
        }

        /// <summary>
        /// 根据科室获取护理站
        /// </summary>
        /// <param name="objDept"></param>
        /// <returns></returns>
        public ArrayList QueryNurseStationByDept(FS.FrameWork.Models.NeuObject objDept)
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetNurseStationFromDept(objDept);
        }
       /// <summary>
        /// 根据根据科室，分类码获取护理站
       /// </summary>
       /// <param name="objDept">科室</param>
       /// <param name="MyStatCode">分类码</param>
       /// <returns></returns>
        public ArrayList QueryNurseStationByDept(FS.FrameWork.Models.NeuObject objDept,string MyStatCode)
        {
            this.SetDB(managerDepartment);
            return managerDepartment.GetNurseStationFromDept(objDept, MyStatCode);
        }

        /// <summary>
        /// 诊出(门诊医生直接点击诊出专用)
        /// </summary>
        /// <param name="consoleCode">诊台编码</param>
        /// <param name="clinicID">门诊流水号</param>
        /// <param name="outDate">诊出日期</param>
        /// <param name="doctID">医生编码</param>
        /// <returns></returns>
        public int UpdateAssign(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            this.SetDB(assignManager);
            return assignManager.Update(consoleCode, clinicID, outDate, doctID);
        }

        /// <summary>
        /// 诊出(门诊医生保存医嘱后专用)
        /// </summary>
        /// <param name="consoleCode">诊台编码</param>
        /// <param name="clinicID">门诊流水号</param>
        /// <param name="outDate">诊出日期</param>
        /// <param name="doctID">医生编码</param>
        /// <returns></returns>
        public int UpdateAssignSaved(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            this.SetDB(assignManager);
            return assignManager.UpdateSaved(consoleCode, clinicID, outDate, doctID);
        }


        public ArrayList QueryFZDept()
        {
            this.SetDB(departStatManager);
            return departStatManager.LoadDepartmentStat("14");
        }

        /// <summary>
        /// 根据门诊流水号，分诊标志获取一个唯一分诊信息
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryAssignByClinicID(string clinicCode)
        {
            this.SetDB(assignManager);
            return assignManager.QueryByClinicID(clinicCode);
        }

        /// <summary>
        /// 为挂号使用的自动分诊
        /// </summary>
        /// <param name="register">挂号实体</param>
        /// <param name="trigeWhereFlag">分诊标记0：分诊1：进诊</param>
        /// <param name="seeSequence"></param>
        /// <param name="isUseBookNum"></param>
        /// <returns></returns>
        public int TriageForRegistration(FS.HISFC.Models.Registration.Register register, string trigeWhereFlag, int seeSequence, bool isUseBookNum)
        {
            //1、准备数据
            FS.HISFC.Models.Nurse.Assign assign = new FS.HISFC.Models.Nurse.Assign();
            FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

            DateTime dtCurr = assignManager.GetDateTimeFromSysDateTime();

            string noonID = this.noonManager.getNoon(dtCurr).ID;

            string doctID = register.DoctorInfo.Templet.Doct.ID;

            int seeNO = 0;

            queue = this.queueManager.GetQueueByDoct(doctID, dtCurr.Date, noonID);

            if (queue == null || string.IsNullOrEmpty(queue.ID))
            {
                this.Err = "此医生没有对应的分诊队列！";
                return -1;
            }
            //seeNO = this.assignManager.Query(queue);

            //seeNO = seeNO + 1;

            seeNO = register.DoctorInfo.SeeNO;

            #region 赋值、生成分诊实体

            assign.Register = register;

            assign.Queue = queue;

            if (trigeWhereFlag == "0")
            {
                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
            }
            else if (trigeWhereFlag == "1")
            {
                assign.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.In;
            }

            assign.TriageDept = queue.Dept.ID;
            assign.TirageTime = dtCurr;
            assign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            assign.Oper.OperTime = dtCurr;

            assign.SeeNO = seeNO;

            if (isUseBookNum)
            {
                if (seeSequence == 0)
                {
                    assign.SeeNO = seeNO;
                }
                else
                {
                    assign.SeeNO = seeSequence;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(queue.Memo))
                {
                    string[] bookNum = queue.Memo.Split('|');

                    for (int i = 0; i < bookNum.Length; i++)
                    {
                        if (seeNO.ToString().Contains(bookNum[i]))
                        {
                            assign.SeeNO = seeNO + 1;
                        }
                    }
                }
            }
            #endregion
            //2、插入分诊数据
            if (this.assignManager.Insert(assign) == -1)
            {
                this.Err = this.assignManager.Err;
                return -1;
            }

            //3、更新挂号信息表，置分诊标志

            if (this.regManager.Update(register.ID, FS.FrameWork.Management.Connection.Operator.ID, dtCurr) == -1)
            {
                this.Err = this.regManager.Err;
                return -1;
            }
            //4.队列数量增加1
            if (this.assignManager.UpdateQueue(assign.Queue.ID, "1") == -1)
            {
                this.Err = this.assignManager.Err;
                return -1;
            }

            return 1;
        }

        #region addby xuewj 2010-11-2 增加叫号按钮{5A8B39E0-76A8-4e68-AF14-E2E0F45617D1}
        /// <summary>
        /// 查询当前时间,当前队列中的最先进诊的候诊信息
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryWait(string queueCode, DateTime begin, DateTime end)
        {
            this.SetDB(assignManager);
            return assignManager.QueryWait(queueCode, begin, end);
        }

        /// <summary>
        /// 根据诊台午别日期队列日期查询队列ID
        /// </summary>
        ///  
        /// <param name="consoleCode">诊台</param>
        /// <param name="noonID">午别</param>
        /// <param name="queueDate">队列时间</param>
        /// <returns>false：取sql出错或该诊台已被使用 true ：没有被使用</returns>
        public string QueryQueueID(string consoleCode, string noonID, string queueDate)
        {
            this.SetDB(queueManager);
            return queueManager.QueryQueueID(consoleCode, noonID, queueDate);
        }

        /// <summary>
        /// 入诊室(更新标志，入室时间)
        /// </summary>
        /// <param name="room"></param>
        /// <param name="inDate"></param>
        /// <returns></returns>
        public int Update(string clinicID, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, DateTime inDate)
        {
            this.SetDB(assignManager);
            return assignManager.Update(clinicID, room, console, inDate);
        }

        #endregion

        #endregion

        #region 复合项目

        /// <summary>
        /// 通过复合项目编码查询明细项目
        /// </summary>
        /// <param name="combCode"></param>
        /// <returns></returns>
        [Obsolete("作废,复合项目已归并到非药品", true)]
        public ArrayList QueryUndrugztDetailByCode(string combCode)
        {
            ArrayList list = new ArrayList();
            return list;
        }

        /// <summary>
        /// 通过复合项目编码查询明细项目
        /// </summary>
        /// <param name="combCode"></param>
        /// <returns></returns>
        public ArrayList QueryUndrugPackageDetailByCode(string combCode)
        {
            this.SetDB(undrugPackageManager);
            return undrugPackageManager.QueryUndrugPackagesBypackageCode(combCode);
        }

        #endregion

        #region 住院
        /// <summary>
        /// 按就诊卡号查询患者
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QueryPatientInfoByCardNO(string cardNO)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.GetPatientInfoByCardNO(cardNO);
        }

        /// <summary>
		/// 患者基本信息查询  com_patientinfo
		/// </summary>
		/// <param name="cardNO">卡号</param>
		/// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfo(string cardNO)
        {
            return managerInpatient.QueryComPatientInfo(cardNO);
        }

        /// <summary>
        /// 患者基本信息查询  {F55EE363-24DB-4a01-B540-49761A5ADBC6}
        /// </summary>
        /// <param name="cardNO">CRMID</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfoByCRMID(string crmID)
        {
            return managerInpatient.QueryComPatientInfoByCRMID(crmID);
        }

        /// <summary>
        /// 患者基本信息查询  {F55EE363-24DB-4a01-B540-49761A5ADBC6}{6ABA909B-8693-46d5-B636-8C30797BAE8E}
        /// </summary>
        /// <param name="cardNO">CRMID</param>
        /// <returns></returns>
        public ArrayList  QueryComPatientInfoByphone(string phone)
        {
            return managerInpatient.QueryPatientByPhone(phone);
        }

        /// <summary>
		/// 插入预约入院登记患者-基本信息
		/// </summary>
		/// <param name="PatientInfo"></param>
		/// <returns>大于 0 成功 小于 0 失败</returns>
        public int InsertPreInPatient(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.InsertPreInPatient(PatientInfo);
        }

        /// <summary>
        /// 更新预约入院登记患者-基本信息
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns>大于 0 成功 小于 0 失败</returns>
        public int UpdatePreInPatientByHappenNo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.UpdatePreInPatientByHappenNo(PatientInfo);
        }

        /// <summary>
        /// 患者可以预约多次，根据门诊号 发生序号更新预约状态 0 为预约 1 为作废 2转入院
        /// </summary>
        /// <param name="CardNO">门诊卡号</param>
        /// <param name="State">状态</param>
        /// <param name="HappenNO">发生序号</param>
        /// <returns></returns>
        public int UpdatePreInPatientState(string CardNO, string State, string HappenNO)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.UpdatePreInPatientState(CardNO, State, HappenNO);
        }

        /// <summary>
		/// 按发生序号获得登记实体
		/// </summary>
		/// <param name="strNo">发生序号</param>
		/// <param name="cardNO">卡号</param>
		/// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryPreInPatientInfoByCardNO(string strNo, string cardNO)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.GetPreInPatientInfoByCardNO(strNo, cardNO);
        }

        /// <summary>
		/// 获取预约登记信息通过状态和预约时间
		/// </summary>
		/// <param name="State"></param>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <returns></returns>
        public ArrayList QueryPreInPatientInfoByDateAndState(string State, string Begin, string End)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.GetPreInPatientInfoByDateAndState(State, Begin, End);
        }
        /// <summary>
        /// 获取预约登记信息通过卡号和预约时间// {6BF1F99D-7307-4d05-B747-274D24174895}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ArrayList GetPrepayInByCardNoAndDate(string cardNo)
        {
            this.SetDB(managerInpatient);
            return managerInpatient.GetPrepayInByCardNoAndDate(cardNo);
        }
        #endregion

        #region 科室结构维护

        /// <summary>
        /// 根据统计分类编码，儿子科室编码提取其父级节点科室信息。
        /// </summary>
        /// <param name="deptCode">科室编码(儿子科室)</param>
        /// <returns></returns>
        public ArrayList LoadPhaParentByChildren(string deptCode)
        {
            this.SetDB(departStatManager);

            return departStatManager.LoadByChildren("03", deptCode);
        }

        /// <summary>
        /// 根据统计分类编码，父级科室编码提取其所有下级节点科室信息。
        /// </summary>
        /// <param name="statCode">统计大类编码</param>
        /// <param name="parDeptCode">父级科室编码</param>
        /// <param name="nodeKind">科室类型: 0真实科室, 1科室分类(虚科室), 2全部科室</param>
        /// <returns></returns>
        public ArrayList LoadChildren(string statCode, string parDeptCode, int nodeKind)
        {
            this.SetDB(departStatManager);

            return departStatManager.LoadChildren(statCode, parDeptCode, nodeKind);
        }

        #endregion

        #region 用户文本
        /// <summary>
        /// 查找用户文本
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList GetList(string Code, int Type)
        {
            this.SetDB(userTextManager);
            return userTextManager.GetList(Code, Type);
        }
        /// <summary>
        /// 更新使用频次
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        public int UpdateFrequency(string id, string operId)
        {
            this.SetDB(userTextManager);
            return userTextManager.UpdateFrequency(id, operId);
        }

        #endregion

        #region  取医院名称
        public string GetHospitalName()
        {
            this.SetDB(managerConstant);
            return managerConstant.GetHospitalName();
        }
        #endregion 

        #region 拼音管理
        /// <summary>
		/// 取一个汉字的拼音码（全拼） 
		/// </summary>
		/// <param name="word">一个汉字</param>
		/// <returns>null 程序错误 </returns>
        public string GetSpellCode(string word)
        {
            this.SetDB(spellManager);
            return spellManager.GetSpellCode(word);
        }
        /// <summary>
        /// 获得字符串
        /// </summary>
        /// <param name="Words"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.ISpell Get(string Words)
        {
            this.SetDB(spellManager);
            return spellManager.Get(Words);
        }
        #endregion

        #region 入出库科室维护

        public ArrayList GetPrivInOutDeptList(string deptCode, string class2Priv)
        {
            FS.HISFC.BizLogic.Manager.PrivInOutDept privInOutManager = new FS.HISFC.BizLogic.Manager.PrivInOutDept();
            return privInOutManager.GetPrivInOutDeptList(deptCode, class2Priv);
        }

        #endregion

        //{7565C40F-3BD3-416a-B12B-BD12ABA51551}
         /// <summary>
        /// 根据人员编码，二级权限编码取人员拥有权限的部门
        /// </summary>
        /// <param name="userCode">操作员编码</param>
        /// <param name="class2Code">二级权限码</param>
        /// <returns>成功返回具有权限的科室集合 失败返回null</returns>        
        public List<FS.FrameWork.Models.NeuObject> QueryUserPriv(string userCode, string class2Code)
        {

            this.SetDB(this.userPowerDetailManager);
            return this.userPowerDetailManager.QueryUserPriv(userCode, class2Code);

        }

        #region 权限

        protected FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();

        public List<FS.FrameWork.Models.NeuObject> QueryUserPrivCollection(string userCode, string class2Code, string deptCode)
        {
            this.SetDB(powerDetailManager);

            return powerDetailManager.QueryUserPrivCollection(userCode, class2Code, deptCode);
        }

        #endregion

        //{3AEB5613-1CB0-4158-89E6-F82F0B643388}
        /// <summary>
        /// 根据人员编码获取医疗组信息
        /// </summary>
        /// <param name="emplCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> GetMedicalGroup(string deptCode, string emplCode)
        {
            FS.HISFC.BizLogic.Order.MedicalTeamForDoct medicalTeamForDoct = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();
            this.SetDB(medicalTeamForDoct);
            return medicalTeamForDoct.QueryQueryMedicalTeamForDoctInfo(deptCode,emplCode);
        }
    }
}
