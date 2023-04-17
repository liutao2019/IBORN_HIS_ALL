using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    /// <summary>
    /// [功能描述: 住院药房公用函数及变量]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public partial class Function
    {
        #region 摆药台选择
        /// <summary>
        /// 选择摆药台
        /// </summary>
        /// <param name="dept">科室</param>
        /// <returns>null 没有选择</returns>
        public static FS.HISFC.Models.Pharmacy.DrugControl ControlSelect(FS.FrameWork.Models.NeuObject dept)
        {
            frmChooseDrugControl frmChooseDrugControl = new frmChooseDrugControl();
            int param = frmChooseDrugControl.ShowControlList(dept);
            if (param == -1)
            {
                return null;
            }
            else if (param > 5)
            {
                //5个配药台的高度是非常完美的，多余5个的增加高度
                frmChooseDrugControl.Height = 414 + (455 - 414)* (param - 5) + param - 5;
            }

            //如果只有一个摆药台，摆药台选择窗口不显示
            if (frmChooseDrugControl.DialogResult == DialogResult.OK || frmChooseDrugControl.ShowDialog() == DialogResult.OK)
            {
                FS.HISFC.Models.Pharmacy.DrugControl drugControl = frmChooseDrugControl.DrugControl;
                frmChooseDrugControl.Close();
                return drugControl;
            }

            return null;
        }
        #endregion

        #region 住院摆药保存

        internal static int DrugConfirm(ArrayList arrayApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept, ref string info)
        {
            string noPrivPatient = "";
            if (JudgeInStatePatient(arrayApplyOut, null, ref noPrivPatient) == -1)
            {
                info = "判断患者状态信息发生错误";
                return -1;
            }
            if (noPrivPatient != "")
            {
                info = noPrivPatient;
                return -1;
            }
            //对申请项目按照项目编码排序 减少资源死锁的发生几率 {1B35A424-0127-42ff-96A4-6835D5DB0151}
            FS.HISFC.BizProcess.Integrate.PharmacyMethod.SortApplyOutByItemCode(ref arrayApplyOut);

            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.InpatientDrugConfirm(arrayApplyOut, drugMessage, arkDept, approveDept) != 1)
            {
                info = pharmacyIntegrate.Err;
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 对用户确认的出库申请数组进行发药处理（打印摆药单）
        /// writed by cuipeng
        /// 2005-1
        /// 操作如下:
        /// 1、如果该记录未计费,则
        ///		确定摆药是否对药品数量取整（只对每次量取整），确定数量。
        ///		取最新的药品基本信息
        ///	2、更新医嘱执行档（摆药确认信息）
        ///	3、更新药嘱主档的最新的执行信息
        ///	4、如果摆药的同时需要出库则处理出库数据，否则，只确认不处理出库数据
        ///	5、更新出库申请表中的摆药信息
        ///	6、如果全部核准，则更新摆药通知信息。否则不更新摆药通知信息
        ///	摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="drugMessage">摆药通知，用来更新摆药通知(摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中)</param>
        /// <returns>1成功，-1失败</returns>
        internal static int DrugConfirm(ArrayList arrayApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept, System.Data.IDbTransaction trans)
        {
            //对申请项目按照项目编码排序 减少资源死锁的发生几率 {1B35A424-0127-42ff-96A4-6835D5DB0151}
            FS.HISFC.BizProcess.Integrate.PharmacyMethod.SortApplyOutByItemCode(ref arrayApplyOut);

            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.InpatientDrugConfirm(arrayApplyOut, drugMessage, arkDept, approveDept, trans) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 对已打印的摆药单进行核准处理（摆药核准）
        /// writed by cuipeng
        /// 2005-1
        /// 操作如下：
        /// 1、如果需要在核准时出库，则进行出库处理。并取得applyOut.OutBillCode
        /// 2、如果该记录未收费，则处理费用信息，否则更新费用表中的发药状态和出库流水号
        /// 3、核准摆药单
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="approveOperCode">核准人（摆药人）</param>
        /// <param name="deptCode">核准科室</param>
        /// <returns>1成功，-1失败</returns>
        internal static int DrugApprove(ArrayList arrayApplyOut, string approveOperCode, string deptCode)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.InpatientDrugApprove(arrayApplyOut, approveOperCode, deptCode) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 对已打印的摆药单进行核准处理（摆药核准）
        /// writed by cuipeng
        /// 2005-1
        /// 操作如下：
        /// 1、如果需要在核准时出库，则进行出库处理。并取得applyOut.OutBillCode
        /// 2、如果该记录未收费，则处理费用信息，否则更新费用表中的发药状态和出库流水号
        /// 3、核准摆药单
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="approveOperCode">核准人（摆药人）</param>
        /// <param name="deptCode">核准科室</param>
        /// <returns>1成功，-1失败</returns>
        internal static int DrugApprove(ArrayList arrayApplyOut, string approveOperCode, string deptCode, System.Data.IDbTransaction trans)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.InpatientDrugApprove(arrayApplyOut, approveOperCode, deptCode, trans) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 对退药申请进行核准处理（退药核准）
        /// writed by cuipeng
        /// 2005-3
        /// 操作如下：
        /// 1、出库处理，返回出库流水号。
        /// 2、如果退药的同时退费,则处理费用信息
        /// 3、核准出库申请，将摆药状态由“0”改成ApplyState。
        /// 4、取费用信息
        /// 5、进行退费申请
        /// 6、如果全部核准，则更新摆药通知信息。否则不更新摆药通知信息
        /// 摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中
        /// </summary>
        /// <param name="arrayApplyOut">出库申请信息</param>
        /// <param name="drugMessage">摆药通知，用来更新摆药通知(摆药后产生的摆药单保存在drugMessage.DrugBillClass.Memo中)</param>
        /// <returns>1成功，-1失败</returns>
        internal static int DrugReturnConfirm(ArrayList arrayApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept)
        {
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            if (pharmacyIntegrate.InpatientDrugReturnConfirm(arrayApplyOut, drugMessage, arkDept, approveDept) != 1)
            {
                System.Windows.Forms.MessageBox.Show(pharmacyIntegrate.Err);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 根据药品申请，判断患者是否已出院，返回不允许继续进行摆药收费的患者信息
        /// </summary>
        /// <param name="arrayApplyOut">药品申请</param>
        /// <param name="noPrivPatient">不在院的患者信息</param>
        /// <returns>成功返回1 失败返回－1</returns>
        internal static int JudgeInStatePatient(ArrayList arrayApplyOut, System.Data.IDbTransaction trans, ref string noPrivPatient)
        {
            System.Collections.Hashtable hsPatient = new Hashtable();
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
            if (trans != null)
            {
                radtIntegrate.SetTrans(trans);
            }

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in arrayApplyOut)
            {
                //顺便处理金额小数位,免得多次循环
                info.CostDecimals = Function.GetCostDecimals();

                if (info.IsCharge)      //对已经收费的记录不进行判断处理
                {
                    continue;
                }
                if (hsPatient.ContainsKey(info.PatientNO))
                {
                    continue;
                }
                else
                {
                    FS.HISFC.Models.RADT.PatientInfo p = radtIntegrate.GetPatientInfomation(info.PatientNO);
                    if (p != null)
                    {
                        if (p.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                        {
                            if (noPrivPatient == "")
                            {
                                noPrivPatient = p.Name;
                            }
                            else
                            {
                                noPrivPatient += "，" + p.Name;
                            }
                        }
                    }

                    hsPatient.Add(info.PatientNO, null);
                }
            }

            if (noPrivPatient != "")
            {
                noPrivPatient += "已不在院，不能进行摆药扣费操作。";
            }

            return 1;
        }
        #endregion

        #region 住院药房数量显示方式
        /// <summary>
        /// 药房数量显示方式
        /// </summary>
        public enum EnumQtyShowType
        {
            最小单位,
            包装单位,
            中成药包装单位
        }
        #endregion

        #region 库存不足时摆药方式,即系统选择数据方式
        /// <summary>
        ///  库存不足时摆药方式,即系统选择数据方式
        /// </summary>
        public enum EnumSendDrugTypeOnLackStock
        {
            手工处理,
            系统处理
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
                decimals = ((FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend)interfaceImplement).GetCostDecimals("0320", "Z1", "Z1");
            }

            return decimals;
        }

        #endregion

        #region 住院药房发药操作状态
        /// <summary>
        /// 住院药房发药操作状态
        /// 避免同步操作引起不可预料的问题
        /// </summary>
        public enum EnumInpatintDrugOperType
        {
            手工刷新,
            自动刷新,
            打印,
            查询,
            保存,
            空闲
        }
        #endregion

        #region 住院发药申请方式
        /// <summary>
        /// 住院发药申请方式
        /// </summary>
        public enum EnumInpatintDrugApplyType
        {
            临时发送,
            全区发送,
            按单发送
        }
        #endregion

        #region 住院药房终端自动打印控制
        /// <summary>
        /// 住院药房终端自动打印控制
        /// </summary>
        /// <param name="drugControl">摆药台</param>
        /// <returns>true 可以启用自动打印</returns>
        public static bool CheckAutoPrintPrive(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            try
            {
                System.Net.IPAddress[] IPS = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                foreach (System.Net.IPAddress IP in IPS)
                {
                    if (IP.ToString() == drugControl.Memo)
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
        /// <param name="drugControl">摆药台</param>
        public static bool RegisterControlAutoPrint(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            if (drugControl == null)
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
            ArrayList al = drugStoreMgr.QueryDrugControlList(drugControl.Dept.ID);
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("注册自动发生错误：没有获取到摆药台", "错误>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            foreach (FS.HISFC.Models.Pharmacy.DrugControl d in al)
            {
                if (d.ID == drugControl.ID)
                {
                    drugControl = d;
                    break;
                }
            }

            if (drugControl == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("注册自动打印发生错误：" + drugStoreMgr.Err, "错误>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrEmpty(drugControl.Memo))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("摆药台已经注册IP：" + drugControl.Memo + "不可以重复注册自动打印！", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
           
            if (!drugControl.IsAutoPrint)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("摆药台没有设置自动打印功能，不可以注册自动打印！", "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            drugControl.Memo = myIP;
            if (drugStoreMgr.ExecNoQuery("update pha_sto_control c set c.mark = '{2}' where c.dept_code = '{0}' and c.control_code = '{1}' and (c.mark is null or c.mark = '')", drugControl.Dept.ID, drugControl.ID, drugControl.Memo) <= 0)
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
            return ChoosePriveDept(class2Code, null, ref privDept);
        }

        /// <summary>
        /// 弹出窗口，选择权限科室，如果用户只有一个科室的权限，则可以直接登陆
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <param name="class3Code">三级权限编码</param>
        /// <param name="privDept">权限科室</param>
        /// <returns>1 返回正常权限科室 0 用户选择取消 －1 用户无权限</returns>
        public static int ChoosePriveDept(string class2Code, string class3Code, ref FS.FrameWork.Models.NeuObject privDept)
        {
            return FS.SOC.HISFC.Components.PharmacyCommon.Function.ChoosePrivDept(class2Code, class3Code, ref privDept);
        }

        #endregion

        /// <summary>
        /// 绘制组合号
        /// </summary>
        /// <param name="sender">Farpoint</param>
        /// <param name="column">组合号列</param>
        /// <param name="drawColumn">标记绘制的列</param>
        public static void DrawCombo(object sender, int column, int drawColumn)
        {
            FS.SOC.HISFC.Components.Common.Function.DrawCombo(sender, column, drawColumn);
        }

        /// <summary>
        /// 判断字符串parentValue是否包含字符串childValue
        /// 逗号或者单竖线分隔，如
        /// parentValue=R,O childValue=R返回true
        /// parentValue=R,O childValue=R,返回true
        /// </summary>
        /// <param name="parentValue"></param>
        /// <param name="childValue"></param>
        /// <returns>true 包含</returns>
        public static bool Contains(string parentValue, string childValue)
        {
             //   if (this.SplitPatientBillClassNO.Contains(applyOut.BillClassNO + ",") || this.SplitPatientBillClassNO == applyOut.PatientNO || this.SplitPatientBillClassNO.Contains("," + applyOut.BillClassNO))
            if (parentValue.Contains(childValue + ",") || parentValue.Contains("," + childValue) || parentValue == childValue)
            {
                return true;
            }

            if (parentValue.Contains(childValue + "|") || parentValue.Contains("|" + childValue) || parentValue == childValue)
            {
                return true;
            }

            return false;
        }

        public static string GetDrugBillClassName(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            if (string.IsNullOrEmpty(drugMessage.DrugBillClass.Name))
            {
                string name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugBillClassName(drugMessage.DrugBillClass.ID);
                return name;
            }

            return drugMessage.DrugBillClass.Name;
        }

    }
}
