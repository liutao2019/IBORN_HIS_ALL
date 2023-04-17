using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen
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

  

        #endregion

        #region 需要接口的函数

        public static object bizExtendInterfaceImplement = null;

 


 

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



    }
}
