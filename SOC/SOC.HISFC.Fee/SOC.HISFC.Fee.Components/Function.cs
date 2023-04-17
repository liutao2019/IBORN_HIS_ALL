using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.Fee.Components
{
    public class Function
    {
        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public static bool JugePrive(string class2Code, string class3Code)
        {
            if (string.IsNullOrEmpty(class2Code) || string.IsNullOrEmpty(class3Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPriv(powerDetailManager.Operator.ID, class2Code, class3Code);
            if (listPrive == null)
            {
                return false;
            }

            return listPrive.Count > 0;
        }

        /// <summary>
        /// 根据药品名称的默认过滤字段 返回过滤字符串
        /// </summary>
        /// <param name="dv">需过滤的DataView</param>
        /// <param name="queryCode">过滤数据字符串</param>
        /// <returns>成功返回过滤字符串 失败返回null</returns>
        public static string GetFilterStr(DataView dv, string queryCode)
        {
            string filterStr = "";
            if (dv.Table.Columns.Contains("自定义码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("自定义码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("别名自定义码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("别名自定义码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("拼音码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("拼音码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("五笔码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("五笔码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("项目名称"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("项目名称 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("项目编号"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("项目编号 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("国家编码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("国家编码 like '{0}'", queryCode), "or");

            if (dv.Table.Columns.Contains("单位编码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("单位编码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("单位名称"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("单位名称 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("中心代码"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("中心代码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("中心名称"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("中心名称 like '{0}'", queryCode), "or");


            return filterStr;
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
            if (ISpell == null)
            {
                return new FS.HISFC.Models.Base.Spell();
            }
            return (FS.HISFC.Models.Base.Spell)ISpell;
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
        /// 打印单据
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3code">三级用户自定义权限</param>
        /// <param name="alPrintData">所有需要打印的数据</param>
        /// <returns>-1 打印错误</returns>
        public static int PrintBill(string class2Code, string class3code, ArrayList alPrintData)
        {
            return 1;
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

            //测试一下
            //FS.SOC.HISFC.BizLogic.MessagePattern.MessageSenderInterfaceImplement MessageSenderInterfaceImplement = new FS.SOC.HISFC.BizLogic.MessagePattern.MessageSenderInterfaceImplement();
            //return MessageSenderInterfaceImplement.Send(alInfo, operType, infoType, ref errInfo);

            errInfo = "接口实现不是指定类型：FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender";
            return -1;
        }

        /// <summary>
        /// 更新复合项目信息，并更新非药品信息价格
        /// </summary>
        /// <param name="alInfo"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int UpdateForFinUndrugztinfo(FS.SOC.HISFC.Fee.Models.Undrug undrug,FS.SOC.HISFC.Fee.Models.Undrug item, ref string errInfo)
        {
            if (undrug != null)
            {
                string oper = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;
                int Return = (new SOC.HISFC.Fee.BizLogic.Undrug()).ExecForUndrugztInfo(undrug.ID, undrug.ValidState, oper, item.ValidState, NConvert.ToInt32(undrug.Price), NConvert.ToInt32(item.Price));
                if (Return == -1)
                {
                    errInfo = "存储过程未实现";
                    return Return;
                }
                else
                {
                    return Return;
                }
            }
            errInfo = "存储过程未实现";
            return -1;
        }
    }
}
