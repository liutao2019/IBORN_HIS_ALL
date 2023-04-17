using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [功能描述: 药库管理用函数集合]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public class Function
    {
        #region 综合函数

        /// <summary>
        /// 根据药品名称的默认过滤字段 返回过滤字符串
        /// </summary>
        /// <param name="dv">需过滤的DataView</param>
        /// <param name="queryCode">过滤数据字符串</param>
        /// <returns>成功返回过滤字符串 失败返回null</returns>
        public static string GetFilterStr(DataView dv, string queryCode)
        {
            string filterStr = "";
            if (dv.Table.Columns.Contains("拼音码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("拼音码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("五笔码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("五笔码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("自定义码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("自定义码 like '{0}'", queryCode), "or");
            
            if (dv.Table.Columns.Contains("商品名称"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("商品名称 like '{0}'", queryCode), "or");

            if (dv.Table.Columns.Contains("名称"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("名称 like '{0}'", queryCode), "or");
            
            if (dv.Table.Columns.Contains("药品名称"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("药品名称 like '{0}'", queryCode), "or");
            
            if (dv.Table.Columns.Contains("商品名"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("商品名 like '{0}'", queryCode), "or");
            
            if (dv.Table.Columns.Contains("通用名"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("通用名 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("通用名拼音码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("通用名拼音码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("通用名五笔码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("通用名五笔码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("通用名自定义码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("通用名自定义码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("英文商品名"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("英文商品名 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("英文通用名"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("英文通用名 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("英文别名"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("英文别名 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("部门名称"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("部门名称 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("学名"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("学名 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("学名拼音码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("学名拼音码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("别名"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("别名 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("别名拼音码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("别名拼音码 like '{0}'", queryCode), "or");
            return filterStr;
        }

        /// <summary>
        /// 连接过滤字符串
        /// </summary>
        /// <param name="filterStr">原始过滤字符串</param>
        /// <param name="newFilterStr">新增加的过滤条件</param>
        /// <param name="logicExpression">逻辑运算符</param>
        /// <returns>成功返回连接后的过滤字符串</returns>
        public static string ConnectFilterStr(string filterStr, string newFilterStr, string logicExpression)
        {
            string connectStr = "";
            if (filterStr == "")
            {
                connectStr = newFilterStr;
            }
            else
            {
                connectStr = filterStr + " " + logicExpression + " " + newFilterStr;
            }
            return connectStr;
        }

        /// <summary>
        /// 显示消息，MessageBox的统一风格
        /// </summary>
        /// <param name="text">提示内容</param>
        /// <param name="messageBoxIcon">图标</param>
        public static void ShowMessage(string text, System.Windows.Forms.MessageBoxIcon messageBoxIcon)
        {

            string caption = "";
            switch (messageBoxIcon)
            {
                case System.Windows.Forms.MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case System.Windows.Forms.MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, messageBoxIcon);
        }

        /// <summary>
        /// 获取管理库存的科室
        /// 一般出库的目标科室
        /// 和入出库科室维护有关
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="privCode">二级权限</param>
        /// <returns></returns>
        public static ArrayList QueryManagerStockDept(string deptNO, string privCode)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string strSQL = "";
            //取SELECT语句
            if (dbMgr.Sql.GetCommonSql("SOC.Manager.PrivInOutDept.GetPrivInOutDept.1", ref strSQL) == -1)
            {
                strSQL = @"	SELECT  t3.DEPT_CODE,
				t3.DEPT_NAME,
				t3.USER_CODE,
				t3.SPELL_CODE,
				t3.WB_CODE,
				t3.DEPT_TYPE
		FROM 	COM_PRIV_INOUT_DEPT t1,PHA_COM_DEPT t2,COM_DEPARTMENT t3
		WHERE   t1.OBJECT_DEPT_CODE = t2.DEPT_CODE
		AND     t1.OBJECT_DEPT_CODE = t3.DEPT_CODE
		AND     t3.VALID_STATE = '1'
		AND  	t1.CLASS2_CODE  = '{1}'  
		AND 	t1.DEPT_CODE    = '{0}'
		AND     t2.STORE_FLAG = '{2}' 
		ORDER BY t1.SORT_ID, t1.OBJECT_DEPT_NAME";
            }
            string managerStock = "1";
            try
            {
                strSQL = string.Format(strSQL, deptNO, privCode, managerStock);	//替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                dbMgr.Err = "格式化SQL语句时出错SOC.Manager.PrivInOutDept.GetPrivInOutDept.1" + ex.Message;
                dbMgr.WriteErr();
                return null;
            }

            ArrayList al = new ArrayList();

            //执行查询语句
            if (dbMgr.ExecQuery(strSQL) == -1)
            {
                dbMgr.Err = "获得出入库科室时，执行SQL语句出错！" + dbMgr.Err;
                dbMgr.ErrCode = "-1";
                return null;
            }
            try
            {
                while (dbMgr.Reader.Read())
                {
                    FS.HISFC.Models.Base.Spell dept = new FS.HISFC.Models.Base.Spell();

                    dept.ID = dbMgr.Reader[0].ToString();
                    dept.Name = dbMgr.Reader[1].ToString();
                    dept.UserCode = dbMgr.Reader[2].ToString();
                    dept.SpellCode = dbMgr.Reader[3].ToString();
                    dept.WBCode = dbMgr.Reader[4].ToString();
                    dept.Memo = dbMgr.Reader[5].ToString();//科室类型

                    al.Add(dept);
                }
            }//抛出错误
            catch (Exception ex)
            {
                dbMgr.Err = "获得出入库科室信息时出错！" + ex.Message;
                dbMgr.ErrCode = "-1";
                return null;
            }
            dbMgr.Reader.Close();

            return al;
        }

        /// <summary>
        /// 获取管理库存的科室
        /// 一般出库的目标科室
        /// 和入出库科室维护有关
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <param name="privCode">二级权限</param>
        /// <returns></returns>
        public static ArrayList QueryManagerStockDept()
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string strSQL = "";
            //取SELECT语句
            if (dbMgr.Sql.GetCommonSql("SOC.Manager.PrivInOutDept.GetPrivInOutDept.3", ref strSQL) == -1)
            {
                strSQL = @"SELECT DEPT_CODE,
				                DEPT_NAME,
				                USER_CODE,
				                SPELL_CODE,
				                WB_CODE,
				                DEPT_TYPE 
                FROM COM_DEPARTMENT d
                WHERE d.DEPT_CODE IN
                (
                SELECT dept_code FROM PHA_COM_DEPT WHERE STORE_FLAG = '1'
                )";
            }
            try
            {
                strSQL = string.Format(strSQL);	//替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                dbMgr.Err = "格式化SQL语句时出错SOC.Manager.PrivInOutDept.GetPrivInOutDept.3" + ex.Message;
                dbMgr.WriteErr();
                return null;
            }

            ArrayList al = new ArrayList();

            //执行查询语句
            if (dbMgr.ExecQuery(strSQL) == -1)
            {
                dbMgr.Err = "获得出入库科室时，执行SQL语句出错！" + dbMgr.Err;
                dbMgr.ErrCode = "-1";
                return null;
            }
            try
            {
                while (dbMgr.Reader.Read())
                {
                    FS.HISFC.Models.Base.Spell dept = new FS.HISFC.Models.Base.Spell();

                    dept.ID = dbMgr.Reader[0].ToString();
                    dept.Name = dbMgr.Reader[1].ToString();
                    dept.UserCode = dbMgr.Reader[2].ToString();
                    dept.SpellCode = dbMgr.Reader[3].ToString();
                    dept.WBCode = dbMgr.Reader[4].ToString();
                    dept.Memo = dbMgr.Reader[5].ToString();//科室类型

                    al.Add(dept);
                }
            }//抛出错误
            catch (Exception ex)
            {
                dbMgr.Err = "获得出入库科室信息时出错！" + ex.Message;
                dbMgr.ErrCode = "-1";
                return null;
            }
            dbMgr.Reader.Close();

            return al;
        }

        /// <summary>
        /// 获取不管理库存的科室
        /// 科室出库获取目标科室
        /// 和入出库科室维护无关
        /// </summary>
        /// <returns></returns>
        public static ArrayList QueryCommonDept()
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string strSQL = "";
            //取SELECT语句
            if (dbMgr.Sql.GetCommonSql("SOC.Manager.PrivInOutDept.GetPrivInOutDept.2", ref strSQL) == -1)
            {
                strSQL = @"SELECT DEPT_CODE,
				DEPT_NAME,
				USER_CODE,
				SPELL_CODE,
				WB_CODE,
				DEPT_TYPE 
FROM COM_DEPARTMENT d
WHERE d.DEPT_CODE NOT IN
(
SELECT dept_code FROM PHA_COM_DEPT WHERE STORE_FLAG = '1'
)";
            }

            ArrayList al = new ArrayList();

            //执行查询语句
            if (dbMgr.ExecQuery(strSQL) == -1)
            {
                dbMgr.Err = "获得出入库科室时，执行SQL语句出错！" + dbMgr.Err;
                dbMgr.ErrCode = "-1";
                return null;
            }
            try
            {
                while (dbMgr.Reader.Read())
                {
                    FS.HISFC.Models.Base.Spell dept = new FS.HISFC.Models.Base.Spell();

                    dept.ID = dbMgr.Reader[0].ToString();
                    dept.Name = dbMgr.Reader[1].ToString();
                    dept.UserCode = dbMgr.Reader[2].ToString();
                    dept.SpellCode = dbMgr.Reader[3].ToString();
                    dept.WBCode = dbMgr.Reader[4].ToString();
                    dept.Memo = dbMgr.Reader[5].ToString();//科室类型

                    al.Add(dept);
                }
            }//抛出错误
            catch (Exception ex)
            {
                dbMgr.Err = "获得出入库科室信息时出错！" + ex.Message;
                dbMgr.ErrCode = "-1";
                return null;
            }
            dbMgr.Reader.Close();

            return al;
        }

        /// <summary>
        /// 获取出入库科室
        /// </summary>
        /// <param name="deptNO">库存科室</param>
        /// <param name="class2Code">二级权限</param>
        /// <returns></returns>
        public static ArrayList QueryInOutDept(string deptNO, string class2Code)
        {
            FS.HISFC.BizLogic.Manager.PrivInOutDept priveMgr = new FS.HISFC.BizLogic.Manager.PrivInOutDept();
            ArrayList alPriveDept = priveMgr.GetPrivInOutDeptList(deptNO, class2Code);
            if (alPriveDept == null)
            {
                ShowMessage("获取入出库科室发生错误：" + priveMgr.Err, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            else
            {
                ArrayList alDept = new ArrayList();
                foreach (FS.HISFC.Models.Base.PrivInOutDept priveDept in alPriveDept)
                {
                    FS.FrameWork.Models.NeuObject dept = SOC.HISFC.BizProcess.Cache.Common.GetDept(priveDept.Dept.ID);
                    if (dept != null)
                    {
                        alDept.Add(dept);
                    }
                }

                return alDept;
            }
        }

        /// <summary>
        /// 获取所有的科室
        /// </summary>
        /// <returns></returns>
        public static ArrayList QueryAllDept()
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string strSQL = "";
            //取SELECT语句
            if (dbMgr.Sql.GetCommonSql("SOC.Manager.PrivInOutDept.GetAllDept", ref strSQL) == -1)
            {
                strSQL = @"SELECT DEPT_CODE,
        DEPT_NAME,
        USER_CODE,
        SPELL_CODE,
        WB_CODE,
        DEPT_TYPE 
FROM COM_DEPARTMENT d
WHERE D.VALID_STATE = '1'
ORDER BY D.DEPT_NAME
";
            }

            ArrayList al = new ArrayList();

            //执行查询语句
            if (dbMgr.ExecQuery(strSQL) == -1)
            {
                dbMgr.Err = "获得科室时，执行SQL语句出错！" + dbMgr.Err;
                dbMgr.ErrCode = "-1";
                return null;
            }
            try
            {
                while (dbMgr.Reader.Read())
                {
                    FS.HISFC.Models.Base.Spell dept = new FS.HISFC.Models.Base.Spell();

                    dept.ID = dbMgr.Reader[0].ToString();
                    dept.Name = dbMgr.Reader[1].ToString();
                    dept.UserCode = dbMgr.Reader[2].ToString();
                    dept.SpellCode = dbMgr.Reader[3].ToString();
                    dept.WBCode = dbMgr.Reader[4].ToString();
                    dept.Memo = dbMgr.Reader[5].ToString();//科室类型

                    al.Add(dept);
                }
            }//抛出错误
            catch (Exception ex)
            {
                dbMgr.Err = "获得科室信息时出错！" + ex.Message;
                dbMgr.ErrCode = "-1";
                return null;
            }
            dbMgr.Reader.Close();

            return al;
        }

        public static ArrayList QueryDeptBySql(string sqlID)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string strSQL = "";
            //取SELECT语句
            if (dbMgr.Sql.GetCommonSql(sqlID, ref strSQL) == -1)
            {
                dbMgr.Err = "获取SQL语句时出错" + sqlID;
                dbMgr.WriteErr();
                return null;
            }          

            ArrayList al = new ArrayList();

            //执行查询语句
            if (dbMgr.ExecQuery(strSQL) == -1)
            {
                dbMgr.Err = "获得出入库科室时，执行SQL语句出错！" + dbMgr.Err;
                dbMgr.ErrCode = "-1";
                return null;
            }
            try
            {
                while (dbMgr.Reader.Read())
                {
                    FS.HISFC.Models.Base.Spell dept = new FS.HISFC.Models.Base.Spell();

                    dept.ID = dbMgr.Reader[0].ToString();
                    dept.Name = dbMgr.Reader[1].ToString();
                    dept.UserCode = dbMgr.Reader[2].ToString();
                    dept.SpellCode = dbMgr.Reader[3].ToString();
                    dept.WBCode = dbMgr.Reader[4].ToString();
                    dept.Memo = dbMgr.Reader[5].ToString();//科室类型

                    al.Add(dept);
                }
            }//抛出错误
            catch (Exception ex)
            {
                dbMgr.Err = "获得出入库科室信息时出错！" + ex.Message;
                dbMgr.ErrCode = "-1";
                return null;
            }
            dbMgr.Reader.Close();

            return al;
        }

        /// <summary>
        /// 将药品总数量转换为大包装数量+小包装数量
        /// </summary>
        /// <param name="totQty">总数量，对应最小单位</param>
        /// /// <param name="totQty">包装数量</param>
        /// <param name="packQty">大包装数量</param>
        /// <param name="minQty">小包装数量</param>
        /// <returns></returns>
        public static void GetDrugQtys(decimal totQty, decimal itemPackQty, ref decimal packQty, ref decimal minQty)
        {
                if (itemPackQty == 0)
                {
                    itemPackQty = 1;
                }
                packQty = Math.Floor(totQty / itemPackQty);
                minQty = totQty - packQty * itemPackQty;
        }

        /// <summary>
        /// 获取拼音码五笔码
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <returns>FS.HISFC.Models.Base.Spell</returns>
        public static FS.HISFC.Models.Base.Spell GetSpellCode(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new FS.HISFC.Models.Base.Spell();
            }
            FS.HISFC.BizLogic.Manager.Spell spellMgr = new FS.HISFC.BizLogic.Manager.Spell();
            FS.HISFC.Models.Base.ISpell ISpell = spellMgr.Get(text);
            if (ISpell==null)
            {
                return new FS.HISFC.Models.Base.Spell();
            }
            return (FS.HISFC.Models.Base.Spell)ISpell;
        }


         #region 读取配置文件获得filter
        public static string GetChooseDataFilter(string inoutType)
        {            
            string settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyInoutDataChooseFilter.xml";
            string filter = SOC.Public.XML.SettingFile.ReadSetting(settingFile, "InoutDataChooseFilter", inoutType, "default");
            return filter;
        }
        #endregion

        #endregion

        #region 需要接口的函数

        public static object bizExtendInterfaceImplement = null;
        public static object pharmacyInputBizManagerImplement = null;
        public static object pharmacyOutputBizManagerImplement = null;

        /// <summary>
        /// 获取二级权限、三级系统权限所代表的业务的金额小数位数
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3MeaningCode">三级系统权限</param>
        /// <returns>小数位 默认2位</returns>
        public static uint GetCostDecimals(string class2Code, string class3MeaningCode)
        {
            uint decimals = 2;
            if (bizExtendInterfaceImplement == null)
            {
                bizExtendInterfaceImplement = InterfaceManager.GetExtendBizImplement();
            }

            if (bizExtendInterfaceImplement == null)
            {
                return decimals;
            }

            if (bizExtendInterfaceImplement is FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)
            {
                decimals = ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)bizExtendInterfaceImplement).GetCostDecimals(class2Code, class3MeaningCode, "0");
            }

            return decimals;
        }

        /// <summary>
        /// 获取本地化规则的单单号
        /// </summary>
        /// <param name="stockDeptNO">库存科室</param>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3code">三级用户自定义权限</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>"-1"表示获取单据失败</returns>
        public static string GetBillNO(string stockDeptNO, string class2Code, string class3code, ref string errInfo)
        {
            string billNO = "-1";
            if (bizExtendInterfaceImplement == null)
            {
                bizExtendInterfaceImplement = InterfaceManager.GetExtendBizImplement();
            }

            if (bizExtendInterfaceImplement != null && bizExtendInterfaceImplement is FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)
            {
                billNO = ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)bizExtendInterfaceImplement).GetBillNO(stockDeptNO, class2Code, class3code, ref errInfo);
            }

            //默认值处理
            if (bizExtendInterfaceImplement == null || (!string.IsNullOrEmpty(billNO) && billNO.ToLower() == "default"))
            {
                if (class2Code == "0314")//付款单号
                {
                    FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

                    decimal iSequence = 0;

                    //获取当前科室的单据最大流水号
                    FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "PhaPayNO", stockDeptNO);
                    if (deptExt != null)
                    {
                        if (deptExt.Item.ID == "")          //当前科室尚无记录 流水号置为1
                        {
                            iSequence = 1;
                        }
                        else                                //当前科室存在记录 根据日期是否为当天 确定流水号是否归1
                        {
                            if (deptExt.DateProperty.Date < DateTime.Now.Date)
                            {
                                iSequence = 1;
                            }
                            else
                            {
                                iSequence = deptExt.NumberProperty + 1;
                            }
                        }
                        //生成单据号
                        billNO = DateTime.Now.ToString("yyyyMMdd") + iSequence.ToString().PadLeft(4, '0');

                        //保存当前最大流水号
                        deptExt.Item.ID = stockDeptNO;
                        deptExt.DateProperty = DateTime.Now;
                        deptExt.NumberProperty = iSequence;
                        deptExt.PropertyCode = "PhaPayNO";
                        deptExt.PropertyName = "药品财务付款号";

                        if (extentManager.SetComExtInfo(deptExt) == -1)
                        {
                            errInfo = extentManager.Err;
                            return null;
                        }

                    }
                }

               
                billNO = GetInOutListNO(stockDeptNO, (class2Code=="0310"));
                if (billNO == null)
                {
                    errInfo = "获取最新入库单号出错";
                    return "-1";
                }
                return billNO;
            }

            return billNO;
        }

        /// <summary>
        /// 处理扩展业务
        /// 入库后采购对比，根据录入的货位号更新数据等都在此处理
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3code">三级用户自定义权限</param>
        /// <param name="alPrintData">所有需要打印的数据</param>
        /// <returns>-1 打印错误</returns>
        public static int DealExtendBiz(string class2Code, string class3code, ArrayList alData, ref string errInfo)
        {
            if (bizExtendInterfaceImplement == null)
            {
                bizExtendInterfaceImplement = InterfaceManager.GetExtendBizImplement();
            }
            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (bizExtendInterfaceImplement == null)
            {
                return 0;
            }

            return ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)bizExtendInterfaceImplement).AfterSave(class2Code, class3code, alData, ref errInfo);
        }

        /// <summary>
        /// 创建本地化要求的内部入库申请表
        /// </summary>
        /// <param name="applyDeptNO">申请科室</param>
        /// <param name="stockDeptNO">库存科室</param>
        /// <param name="alData">原数据列表</param>
        /// <returns></returns>
        public static ArrayList SetInnerInputApply(string applyDeptNO, string stockDeptNO, ArrayList alData)
        {
            if (bizExtendInterfaceImplement == null)
            {
                bizExtendInterfaceImplement = InterfaceManager.GetExtendBizImplement();
            }
            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (bizExtendInterfaceImplement == null)
            {            
                return null;
            }

            return ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)bizExtendInterfaceImplement).SetInnerInputApply(applyDeptNO, stockDeptNO, alData);
        }

        /// <summary>
        /// 创建本地化要求的入库申请表
        /// </summary>
        /// <param name="stockDeptNO">库存科室</param>
        /// <param name="alData">原数据列表</param>
        /// <returns></returns>
        public static ArrayList SetInputPlan(string stockDeptNO, ArrayList alData)
        {
            if (bizExtendInterfaceImplement == null)
            {
                bizExtendInterfaceImplement = InterfaceManager.GetExtendBizImplement();
            }
            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (bizExtendInterfaceImplement == null)
            {    
                return null;
            }

            return ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)bizExtendInterfaceImplement).SetInputPlan(stockDeptNO, alData);
        }


        /// <summary>
        /// 获取入库业务管理
        /// </summary>
        public static Base.IPharmacyBizManager GetInputBizManager()
        {
            if (pharmacyInputBizManagerImplement == null)
            {
                pharmacyInputBizManagerImplement = InterfaceManager.GetPharmacyInputControl();
            }
            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (pharmacyInputBizManagerImplement == null)
            {
                pharmacyInputBizManagerImplement = new Common.Input.InputBizManager();
            }

            return (Base.IPharmacyBizManager)pharmacyInputBizManagerImplement;

        }

        /// <summary>
        /// 获取出库业务管理
        /// </summary>
        public static Base.IPharmacyBizManager GetOutputBizManager()
        {
            if (pharmacyOutputBizManagerImplement == null)
            {
                pharmacyOutputBizManagerImplement = InterfaceManager.GetPharmacyOutputControl();
            }
            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (pharmacyOutputBizManagerImplement == null)
            {
                pharmacyOutputBizManagerImplement = new Common.Output.OutputBizManager();
            }

            return (Base.IPharmacyBizManager)pharmacyOutputBizManagerImplement;

        }

        /// <summary>
        /// 获取和业务相关的数据选择列表属性集合
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3MeaningCode">三级系统权限</param>
        /// <param name="class3Code">三级用户自定义权限</param>
        /// <param name="listType">单据类型 0药品清单 1入库单 2出库单 3申请单 4采购单</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>null 发生错误</returns>
        public static FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting GetBizChooseDataSetting(string class2Code, string class3MeaningCode, string calss3Code, string listType, ref string errInfo)
        {
            if (bizExtendInterfaceImplement == null)
            {
                bizExtendInterfaceImplement = InterfaceManager.GetExtendBizImplement();
            }
            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (bizExtendInterfaceImplement == null)
            {    
                FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
                chooseDataSetting.IsDefault = true;
                return chooseDataSetting;
            }

            return ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)bizExtendInterfaceImplement).GetChooseDataSetting(class2Code, class3MeaningCode, calss3Code, listType, ref errInfo);

        }

        /// <summary>
        /// 获取入库信息录入控件
        /// </summary>
        /// <param name="class3Code">三级用户自定义权限区别入库类别</param>
        /// <param name="isSpecial"></param>
        /// <returns></returns>
        public static SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl GetInputInfoControl(string class3Code, bool isSpecial)
        {
            if (bizExtendInterfaceImplement == null)
            {
                bizExtendInterfaceImplement = InterfaceManager.GetExtendBizImplement();
            }
            SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl defaultInputInfoControl;
            if (isSpecial)
            {
                defaultInputInfoControl= new Common.Input.ucSpecialInput();
            }
            else
            {
                defaultInputInfoControl = new Common.Input.ucCommonInput();
            }

            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (bizExtendInterfaceImplement == null)
            {
                return defaultInputInfoControl;
            }

            return ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)bizExtendInterfaceImplement).GetInputInfoControl(class3Code, isSpecial, defaultInputInfoControl);

        }

        /// <summary>
        /// 获取药品基本信息扩展信息控件
        /// </summary>
        /// <param name="class3Code">三级用户自定义权限区别入库类别</param>
        /// <param name="isSpecial"></param>
        /// <returns></returns>
        public static SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl GetItemExtendControl()
        {
            if (bizExtendInterfaceImplement == null)
            {
                bizExtendInterfaceImplement = InterfaceManager.GetExtendBizImplement();
            }
            //没有接口实现（当然也可能是业务扩展本地化有问题），则不处理扩展业务
            if (bizExtendInterfaceImplement == null)
            {
                return new Maintenance.ucItemExtend();
            }

            return ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)bizExtendInterfaceImplement).GetItemExtendControl(new Maintenance.ucItemExtend());

        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3code">三级用户自定义权限</param>
        /// <param name="alPrintData">所有需要打印的数据</param>
        /// <returns>-1 打印错误</returns>
        public static int PrintBill(string class2Code, string class3code, ArrayList alPrintData)
        {
            object interfaceImplement = InterfaceManager.GetBillPrintImplement();
            if (interfaceImplement == null)
            {
                return -1;
            }

            return ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBill)interfaceImplement).PrintBill(class2Code, class3code, alPrintData);
        }

        /// <summary>
        /// 信息发送
        /// </summary>
        /// <param name="alInfo">所有信息</param>
        /// <param name="operType">操作类别</param>
        /// <param name="infoType">数据类别</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>-1发送失败</returns>
        public static int SendBizMessage(ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType infoType, ref string errInfo)
        {
            object MessageSender = InterfaceManager.GetBizInfoSenderImplement();
            if (MessageSender is FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender)
            {
                return ((FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender)MessageSender).Send(alInfo, operType, infoType, ref errInfo);
            }
            else if (MessageSender == null)
            {
                errInfo = "没维护接口：FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender的实现";
                return 0;
            }

            errInfo = "接口实现不是指定类型：FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender";
            return -1;
        }
        #endregion

        #region 获取通用格式单据号

        /// <summary>
        /// 入出库单据号获取
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="isInListNO">是否入库单号</param>
        /// <returns>成功返回入库单号  失败返回null</returns>
        public static string GetInOutListNO(string deptCode, bool isInListNO)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.DeptConstant deptCons = phaConsManager.QueryDeptConstant(deptCode);

            string listCode = "";
            if (isInListNO)
            {
                listCode = deptCons.InListNO;
            }
            else
            {
                listCode = deptCons.OutListNO;
            }

            if (string.IsNullOrEmpty(listCode))
            {
                return GetCommonListNO(deptCode);
            }
            else
            {
                string nextListCode = GetNextListSequence(listCode, true);
                if (isInListNO)
                {
                    deptCons.InListNO = nextListCode;
                }
                else
                {
                    deptCons.OutListNO = nextListCode;
                }
                if (phaConsManager.UpdateDeptConstant(deptCons) == -1)
                {
                    //Err = "生成下一单据号序列发生错误" + phaConsManager.Err;
                    return null;
                }

                return listCode;
            }
        }

        /// <summary>
        /// 获取入库、出库申请单据号，避免和入出库单据重复
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static string GetApplyListNO(string deptCode)
        {
            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

            string ListNO = "";
            decimal iSequence = 0;
            DateTime sysDate = extentManager.GetDateTimeFromSysDateTime().Date;

            FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT,"ApplyListNO",deptCode);
            if(deptExt == null)
            {
                return null;
            }
            else
            {
                if (deptExt.Item.ID == "")
                {
                    iSequence = 1;
                }
                else
                {
                    if (deptExt.DateProperty.Date != sysDate)
                    {
                        iSequence = 1;
                    }
                    else
                    {
                        iSequence = deptExt.NumberProperty + 1;
                    }
                }
                    //生成单据号
                ListNO = deptCode + sysDate.Year.ToString().Substring(2, 2) + sysDate.Month.ToString().PadLeft(2, '0') + sysDate.Day.ToString().PadLeft(2, '0')
                    + iSequence.ToString().PadLeft(3, '0');

                //保存当前最大流水号
                deptExt.Item.ID = deptCode;
                deptExt.DateProperty = sysDate;
                deptExt.NumberProperty = iSequence;
                deptExt.PropertyCode = "ApplyListNO";
                deptExt.PropertyName = "科室单据号最大流水号";

                if (extentManager.SetComExtInfo(deptExt) == -1)
                {
                    return null;
                }
                return ListNO;
            }
                        
                }
        

        /// <summary>
        /// 获取通用单据号 科室编码+YYMMDD+三位流水号
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回新获取的单据号 失败返回null</returns>
        public static string GetCommonListNO(string deptCode)
        {
            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();
          
            string ListNO = "";
            decimal iSequence = 0;
            DateTime sysDate = extentManager.GetDateTimeFromSysDateTime().Date;

            //获取当前科室的单据最大流水号
            FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "ListCode", deptCode);
            if (deptExt == null)
            {
                return null;
            }
            else
            {
                if (deptExt.Item.ID == "")          //当前科室尚无记录 流水号置为1
                {
                    iSequence = 1;
                }
                else                                //当前科室存在记录 根据日期是否为当天 确定流水号是否归1
                {
                    if (deptExt.DateProperty.Date != sysDate)
                    {
                        iSequence = 1;
                    }
                    else
                    {
                        iSequence = deptExt.NumberProperty + 1;
                    }
                }
                //生成单据号
                ListNO = deptCode + sysDate.Year.ToString().Substring(2, 2) + sysDate.Month.ToString().PadLeft(2, '0') + sysDate.Day.ToString().PadLeft(2, '0')
                    + iSequence.ToString().PadLeft(3, '0');

                //保存当前最大流水号
                deptExt.Item.ID = deptCode;
                deptExt.DateProperty = sysDate;
                deptExt.NumberProperty = iSequence;
                deptExt.PropertyCode = "ListCode";
                deptExt.PropertyName = "科室单据号最大流水号";

                if (extentManager.SetComExtInfo(deptExt) == -1)
                {
                    return null;
                }
            }
            return ListNO;
        }

        /// <summary>
        /// 根据字符串获取下一个单据号的数值部分
        /// </summary>
        /// <param name="listCode"></param>
        /// <returns></returns>
        public static string GetNextListSequence(string listCode, bool isAddSequence)
        {
            string listNum = "";
            string listStr = "";
            //修改生成返回入出库单号中非数字字符处理的BUG by Sunjh 2010-8-17 {FA29FD4A-7379-49ae-847E-ED4BAB67E815}
            int numIndex = 0;//listCode.Length;
            for (int i = listCode.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(listCode[i]))
                {
                    listNum = listCode[i] + listNum;
                }
                else
                {
                    numIndex = i + 1;       //序列部分截至位置
                    break;
                }
            }

            listStr = listCode.Substring(0, numIndex);

            if (string.IsNullOrEmpty(listNum))
            {
                //Err = "单据号格式不规范 无法继续获取下一序列";
                return null;
            }
            else
            {
                int listNumLength = listNum.Length;
                string nextListNum = "";
                if (isAddSequence)
                {
                    nextListNum = ((FS.FrameWork.Function.NConvert.ToDecimal(listNum) + 1).ToString()).PadLeft(listNumLength, '0');
                }
                else
                {
                    nextListNum = ((FS.FrameWork.Function.NConvert.ToDecimal(listNum) - 1).ToString()).PadLeft(listNumLength, '0');
                }

                return listStr + nextListNum;
            }
        }

        #endregion

        #region 权限控制

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="deptNO">权限科室</param>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public static bool JugePrive(string deptNO, string class2Code, string class3Code)
        {
            return FS.SOC.HISFC.Components.PharmacyCommon.Function.JugePrive(deptNO, class2Code, class3Code);
        }

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public static bool JugePrive(string class2Code, string class3Code)
        {
            return FS.SOC.HISFC.Components.PharmacyCommon.Function.JugePrive(class2Code, class3Code);
        }

        /// <summary>
        /// 取当前操作员是否有某一权限。
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <returns>True 有权限, False 无权限</returns>
        public static bool JugePrive(string class2Code)
        {
            return FS.SOC.HISFC.Components.PharmacyCommon.Function.JugePrive(class2Code);
            
        }       

        /// <summary>
        /// 弹出窗口，选择权限科室，如果用户只有一个科室的权限，则可以直接登陆
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <param name="privDept">权限科室</param>
        /// <returns>1 返回正常权限科室 0 用户选择取消 －1 用户无权限</returns>
        public static int ChoosePriveDept(string class2Code, ref FS.FrameWork.Models.NeuObject privDept)
        {
            return ChoosePrivDept(class2Code, null, ref privDept);
        }

        /// <summary>
        /// 弹出窗口，选择权限科室，如果用户只有一个科室的权限，则可以直接登陆
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <param name="class3Code">三级权限编码</param>
        /// <param name="privDept">权限科室</param>
        /// <returns>1 返回正常权限科室 0 用户选择取消 －1 用户无权限</returns>
        public static int ChoosePrivDept(string class2Code, string class3Code, ref FS.FrameWork.Models.NeuObject privDept)
        {
            return FS.SOC.HISFC.Components.PharmacyCommon.Function.ChoosePrivDept(class2Code, class3Code, ref privDept);
        }

        #endregion

    }
}
