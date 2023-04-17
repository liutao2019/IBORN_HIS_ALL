using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.Common
{
    /// <summary>
    /// [功能描述: 门诊药房公用函数及变量]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
   public class Function
    {

        #region 门诊终端选择
        /// <summary>
        /// 门诊终端选择业务
        /// </summary>
        /// <param name="stockDept">操作库房科室</param>
        /// <param name="terminalType">门诊终端类别 0 发药窗口 1 配药台</param>
        /// <param name="isShowMessageBox">对相应的提示信息是否采用MessageBox弹出显示</param>
        /// <returns>成功返回 门诊终端实体 失败返回null</returns>
        public static FS.HISFC.Models.Pharmacy.DrugTerminal TerminalSelect(string stockDept, FS.HISFC.Models.Pharmacy.EnumTerminalType terminalType, bool isShowMessageBox)
        {
            FS.HISFC.Models.Pharmacy.DrugTerminal terminal = new FS.HISFC.Models.Pharmacy.DrugTerminal();
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            bool isShowTerminalList = true;
            string terminalCode1 = "";
            terminalCode1 = FS.SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStore.xml", "TerminalCode", "Dept" + stockDept + terminalType.ToString(), terminalCode1);
            if (!string.IsNullOrEmpty(terminalCode1))
            {
                //根据配置文件内编码确定终端
                terminal = drugStoreManager.GetDrugTerminalById(terminalCode1);
                if (terminal != null)
                {
                    if (System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("确认是" + terminal.Name + "吗？"), "提示>>", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (terminal.IsClose && isShowMessageBox)
                            System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("终端" + terminal.Name + "已关闭"));

                        if (terminal.TerminalType == terminalType)
                            isShowTerminalList = false;
                    }
                }
            }

            if (isShowTerminalList)
            {
                #region 配置文件内编码无效 弹出列表供人员选择

                ArrayList al = drugStoreManager.QueryDrugTerminalByDeptCode(stockDept, terminalType.GetHashCode().ToString());
                if (al == null && isShowMessageBox)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("未获取终端列表") + drugStoreManager.Err);
                    return null;
                }
                FS.FrameWork.Models.NeuObject tempTerminal = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, null, new bool[] { true, true, false, false, false, false, false, false }, null, ref tempTerminal) == 0)
                {
                    return null;
                }
                else
                {
                    terminal = tempTerminal as FS.HISFC.Models.Pharmacy.DrugTerminal;
                }

                #endregion
            }

            //配药的发药窗口只有一个，选择了配药台则确定发药窗口
            if (terminal != null && terminal.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.配药台)
            {
                FS.HISFC.Models.Pharmacy.DrugTerminal sendTerminal = drugStoreManager.GetDrugTerminalById(terminal.SendWindow.ID);
                if (sendTerminal != null)
                {
                    terminal.SendWindow.Name = sendTerminal.Name;
                }
            }
            FS.SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStore.xml", "TerminalCode", "Dept" + stockDept + terminalType.ToString(), terminal.ID);
            return terminal;
        }

        /// <summary>
        /// 根据发药窗口选择配药台
        /// </summary>
        /// <param name="stockDept">操作库房科室</param>
        /// <param name="sendTerminal">发药窗口</param>
        /// <param name="isShowMessageBox">对相应的提示信息是否采用MessageBox弹出显示</param>
        /// <returns>成功返回 门诊配药台终端实体 失败返回null</returns>
        public static FS.HISFC.Models.Pharmacy.DrugTerminal TerminalSelect(string stockDept, string sendTerminal, bool isShowMessageBox)
        {
            FS.HISFC.Models.Pharmacy.EnumTerminalType terminalType = FS.HISFC.Models.Pharmacy.EnumTerminalType.配药台;
            FS.HISFC.Models.Pharmacy.DrugTerminal terminal = new FS.HISFC.Models.Pharmacy.DrugTerminal();
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            #region  弹出列表供人员选择
            bool isShowTerminalList = true;
            string terminalCode2 = "";
            terminalCode2 = FS.SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStore.xml", "TerminalCode", "Dept" + stockDept + terminalType.ToString(), terminalCode2);
            if (!string.IsNullOrEmpty(terminalCode2))
            {
                //根据配置文件内编码确定终端
                terminal = drugStoreManager.GetDrugTerminalById(terminalCode2);
            }


            ArrayList al = drugStoreManager.QueryDrugTerminalByDeptCode(stockDept, terminalType.GetHashCode().ToString());
            if (al == null && isShowMessageBox)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("未获取终端列表") + drugStoreManager.Err);
                return null;
            }
            for (int index = al.Count - 1; index > -1; index--)
            {
                FS.HISFC.Models.Pharmacy.DrugTerminal t = al[index] as FS.HISFC.Models.Pharmacy.DrugTerminal;
                if (t.SendWindow.ID != sendTerminal)
                {
                    al.RemoveAt(index);
                }
                else
                {
                    //配置文件中的中端可能不是这个发药窗口的
                    if (terminal != null && t.ID == terminal.ID)
                    {
                        if (System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("确认是" + terminal.Name + "吗？"), "提示>>", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (terminal.IsClose && isShowMessageBox)
                                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("终端" + terminal.Name + "已关闭"));

                            if (terminal.TerminalType == terminalType)
                                isShowTerminalList = false;
                        }

                    }
                }
            }

            if (!isShowTerminalList)
            {
                FS.SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStore.xml", "TerminalCode", "Dept" + stockDept + terminalType.ToString(), terminal.ID);
                return terminal;
            }
            if (al.Count == 1 && (terminal == null || string.IsNullOrEmpty(terminal.ID)))
            {
                FS.SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStore.xml", "TerminalCode", "Dept" + stockDept + terminalType.ToString(), terminal.ID);
                return al[0] as FS.HISFC.Models.Pharmacy.DrugTerminal;
            }
            FS.FrameWork.Models.NeuObject tempTerminal = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, null, new bool[] { true, true, false, false, false, false, false, false }, null, ref tempTerminal) == 0)
            {
                return null;
            }
            else
            {
                terminal = tempTerminal as FS.HISFC.Models.Pharmacy.DrugTerminal;
            }

            #endregion
            FS.SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugStore.xml", "TerminalCode", "Dept" + stockDept + terminalType.ToString(), terminal.ID);
            return terminal;
        }

        #endregion

        #region 门诊终端开关
        /// <summary>
        /// 门诊终端开关
        /// </summary>
        /// <param name="stockDept">科室</param>
        /// <param name="termial">终端</param>
        /// <returns>-1失败</returns>
        public static int TerminalSwith(string stockDept, FS.HISFC.Models.Pharmacy.DrugTerminal termial)
        {
            if (termial == null || string.IsNullOrEmpty(stockDept))
            {
                return 0;
            }

            bool isCurState = termial.IsClose;
            string curState = (isCurState ? "打开" : "关闭");

            FS.HISFC.Models.Pharmacy.DrugTerminal termialTmp = termial;
            if (termial.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.发药窗口)
            {
                termialTmp = TerminalSelect(stockDept, termial.ID, true);
            }
            if (termialTmp == null)
            {
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在关闭...");
            Application.DoEvents();

            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
            termialTmp = drugStoreMgr.GetDrugTerminalById(termialTmp.ID);
            if (termialTmp == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("获取终端信息出错：" + drugStoreMgr.Err);
                return -1;
            }

            //状态已经变化
            if (isCurState != termialTmp.IsClose)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("终端已经" + curState + "！");
                return 1;
            }

            //关了打开，开了关闭
            termialTmp.IsClose = !termialTmp.IsClose;
            if (drugStoreMgr.UpdateDrugTerminal(termialTmp) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("更新终端信息出错：" + drugStoreMgr.Err);
                return -1;
            }

            //不允许关闭所有的普通台，保证门诊收费畅通
            if (termialTmp.TerminalProperty == FS.HISFC.Models.Pharmacy.EnumTerminalProperty.普通 && termialTmp.IsClose)
            {
                bool isHaveComTermial = false;
                ArrayList al = drugStoreMgr.QueryDrugTerminalByDeptCode(stockDept, FS.HISFC.Models.Pharmacy.EnumTerminalType.配药台.GetHashCode().ToString());
                if (al == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("获取终端信息出错：" + drugStoreMgr.Err);
                    return -1;
                }
                foreach (FS.HISFC.Models.Pharmacy.DrugTerminal t in al)
                {
                    if (t.TerminalProperty == FS.HISFC.Models.Pharmacy.EnumTerminalProperty.普通 && !t.IsClose)
                    {
                        isHaveComTermial = true;
                        break;
                    }
                }
                if (!isHaveComTermial)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("不能关闭所有的普通配药台！");
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("终端已经" + curState + "！");
            return 1;

        }
        #endregion

        #region 门诊药房终端自动打印控制
        /// <summary>
        /// 门诊药房终端自动打印控制
        /// </summary>
        /// <param name="drugTeminalMemo">终端的备注</param>
        /// <returns>true 可以启用自动打印</returns>
        public static bool CheckAutoPrintPrive(FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            try
            {
                System.Net.IPAddress[] IPS = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                foreach (System.Net.IPAddress IP in IPS)
                {
                    if (IP.ToString() == drugTerminal.Memo)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 注册终端自动打印
        /// </summary>
        /// <param name="drugTerminal">终端信息</param>
        public static bool RegisterDrugTermial(FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            if (drugTerminal == null)
            {
                return false;
            }
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            string myIP = "";
            try
            {
                System.Net.IPAddress[] IPS = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                myIP = IPS.GetValue(0).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册自动发生错误：" + ex.Message, "错误>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(myIP))
            {
                MessageBox.Show("注册自动发生错误：没有获取到本机IP地址", "错误>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            drugTerminal = drugStoreMgr.GetDrugTerminalById(drugTerminal.ID);
            if (drugTerminal == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("注册自动打印发生错误：" + drugStoreMgr.Err, "错误>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrEmpty(drugTerminal.Memo))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("终端已经注册IP：" + drugTerminal.Memo + "不可以重复注册自动打印！", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (drugTerminal.IsClose)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("终端关闭，不可以注册自动打印！", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!drugTerminal.IsAutoPrint)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("终端没有设置自动打印功能，不可以注册自动打印！", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            drugTerminal.Memo = myIP;
            if (drugStoreMgr.UpdateDrugTerminal(drugTerminal) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("注册自动打印发生错误：" + drugStoreMgr.Err, "错误>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("注册自动打印成功，程序可以自动打印！", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        #endregion

        #region 门诊发药保存
        /// <summary>
        /// 门诊发药保存
        /// </summary>
        /// <param name="applyOutCollection">摆药申请信息</param>
        /// <param name="terminal">发药终端</param>
        /// <param name="sendDept">发药科室信息(扣库科室)</param>
        /// <param name="sendOper">发药人员信息</param>
        /// <param name="isDirectSave">是否直接保存 (无配药流程)</param>
        /// <param name="isUpdateAdjustParam">是否更新处方调剂参数</param>
        /// <returns>发药确认成功返回1 失败返回-1</returns>
        internal static int OutpatientSend(List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutCollection, FS.FrameWork.Models.NeuObject terminal, FS.FrameWork.Models.NeuObject sendDept, FS.FrameWork.Models.NeuObject sendOper, bool isDirectSave, bool isUpdateAdjustParam)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.OutpatientSend(applyOutCollection, terminal, sendDept, sendOper, isDirectSave, isUpdateAdjustParam) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }
        #endregion

        #region 门诊配药保存
        /// <summary>
        /// 门诊配药保存
        /// </summary>
        /// <param name="applyOutCollection">摆药申请信息</param>
        /// <param name="terminal">配药终端</param>
        /// <param name="drugedDept">配药科室信息</param>
        /// <param name="drugedOper">配药人员信息</param>
        /// <param name="isUpdateAdjustParam">是否更新处方调剂参数</param>
        /// <returns>配药确认成功返回1 失败返回-1</returns>
        internal static int OutpatientDrug(List<FS.HISFC.Models.Pharmacy.ApplyOut> applyOutCollection, FS.FrameWork.Models.NeuObject terminal, FS.FrameWork.Models.NeuObject drugedDept, FS.FrameWork.Models.NeuObject drugedOper, bool isUpdateAdjustParam)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.OutpatientDrug(applyOutCollection, terminal, drugedDept, drugedOper, isUpdateAdjustParam) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }
        #endregion



        #region 门诊药房数量显示方式
        /// <summary>
        /// 药房数量显示方式
        /// </summary>
        public enum EnumQtyShowType
        {
            最小单位,
            包装单位,
            门诊拆分
        }
        #endregion

        #region 门诊药房配发药操作状态
        /// <summary>
        /// 门诊药房配发药操作状态
        /// 避免同步操作引起不可预料的问题
        /// </summary>
        public enum EnumOutpatintDrugOperType
        {
            手工刷新,
            自动刷新,
            打印,
            查询,
            大屏显示处理,
            保存,
            空闲
        }
        #endregion

        #region 住院摆药出库金额位数

        /// <summary>
        /// 获取二级权限、三级系统权限所代表的业务的金额小数位数
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3MeaningCode">三级系统权限</param>
        /// <returns>小数位 默认2位</returns>
        public static uint GetCostDecimals()
        {
            uint decimals = 2;
            object interfaceImplement = InterfaceManager.GetExtendBizImplement();
            if (interfaceImplement == null)
            {
                return decimals;
            }

            if (interfaceImplement is FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)
            {
                decimals = ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)interfaceImplement).GetCostDecimals("0320", "Z1", "0");
            }

            return decimals;
        }

        #endregion
    }
}
