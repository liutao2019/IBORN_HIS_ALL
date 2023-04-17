using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY
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

        #endregion

        #region 需要接口的函数

        public static object bizExtendInterfaceImplement = null;

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3code">三级用户自定义权限</param>
        /// <param name="alPrintData">所有需要打印的数据</param>
        /// <returns>-1 打印错误</returns>
        public static int PrintBill(string class2Code, string class3code, ArrayList alPrintData)
        {
            FS.SOC.Local.Pharmacy.ZhuHai.Print.BillPrintInterfaceImplement pringbill = new FS.SOC.Local.Pharmacy.ZhuHai.Print.BillPrintInterfaceImplement();
            return pringbill.PrintBill(class2Code, class3code, alPrintData);
        }

        #endregion


    }
}
