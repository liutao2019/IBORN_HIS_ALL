using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Print
{
    #region 打印接口的实现
    public class BillPrintInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBill
    {
        #region 变量
        /// <summary>
        /// 单据接口
        /// </summary>
        private FS.SOC.Local.Pharmacy.ShenZhen.Base.IPharmacyBillPrint iPrint;
        #endregion

        #region 获取用户自定义的打印设置

        /// <summary>
        /// 获取单据设置
        /// </summary>
        /// <param name="class2Code">二级权限编码</param>
        /// <param name="class3Code">操作类别的编码[自定义的三级权限]</param>
        /// <returns></returns>
        private Base.PrintBill GetPrintBill(string class2Code, string class3Code)
        {
            Base.PrintBill pringBill = new Base.PrintBill();
            
            #region 读取本地设置文件
            if (System.IO.File.Exists(this.SettingFile))
            {
                System.Data.DataSet dsTmp = new System.Data.DataSet();
                dsTmp.ReadXml(this.SettingFile);
                if (dsTmp == null)
                {
                    System.Windows.Forms.MessageBox.Show("本地设置文件可能已经损坏!");
                    return null;
                }
                foreach (System.Data.DataRow r in dsTmp.Tables[0].Rows)
                {
                    if (r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.二级权限代码.ToString()].ToString() == class2Code)
                    {
                        if (r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.三级权限代码.ToString()].ToString() == class3Code)
                        {
                            try
                            {
                                pringBill.IsNeedPrint = FS.FrameWork.Function.NConvert.ToBoolean(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.打印.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsNeedQuest = FS.FrameWork.Function.NConvert.ToBoolean(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.提示.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsNeedPreview = FS.FrameWork.Function.NConvert.ToBoolean(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.预览.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsQuested = FS.FrameWork.Function.NConvert.ToBoolean(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.已经询问.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.Title = r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.单据名称.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.DLLName = r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.程序集.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.ControlName = r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.控件.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.RowCount = FS.FrameWork.Function.NConvert.ToInt32(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.每页行数.ToString()]);
                                ///{A513864B-B069-4604-B554-EBAD57F91A49}
                                if (pringBill.RowCount <= 0)
                                {
                                    pringBill.RowCount = 1;
                                }
                            }
                            catch { }

                            try
                            {
                                string sortTemp = r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.排序方式.ToString()].ToString();
                                if (sortTemp == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.货位号.ToString())
                                {
                                    pringBill.Sort = FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.货位号;
                                }
                                else if (sortTemp == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.类别货位号.ToString())
                                {
                                    pringBill.Sort = FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.类别货位号;
                                }
                                else if (sortTemp == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.物理顺序.ToString())
                                {
                                    pringBill.Sort = FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.物理顺序;
                                }
                                
                                //pringBill.Sort = (FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType)FS.FrameWork.Function.NConvert.ToInt32(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.排序方式.ToString()]);
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            #endregion

            #region 从数据库读取系统设置

            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            pringBill.PageSize = pageSizeMgr.GetPageSize(class2Code + class3Code);
            if (pringBill.PageSize != null && pringBill.PageSize.Printer.ToLower() == "default")
            {
                pringBill.PageSize.Printer = "";
            }
            
            #endregion

            return pringBill;
        }

        /// <summary>
        /// 获取纸张设置的本地文件
        /// </summary>
        private string settingFile = "";

        /// <summary>
        /// 获取纸张设置的本地文件
        /// </summary>
        public string SettingFile
        {
            get
            {
                if (string.IsNullOrEmpty(this.settingFile))
                {
                    return FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyBillPrintDefine.xml";
                }
                return this.settingFile;
            }
        }

        #endregion            


        #region IPharmacyBill 成员

        public int PrintBill(string class2Code, string class3code, System.Collections.ArrayList alPrintData)
        {
            Base.PrintBill printBill = this.GetPrintBill(class2Code, class3code);
            if (printBill == null || !printBill.IsNeedPrint)
            {
                return -1;
            }
            if (printBill.IsNeedQuest)
            {
                System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("是否打印单据", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.No)
                {
                    return -1;
                }
            }
            if (string.IsNullOrEmpty(printBill.DLLName) || string.IsNullOrEmpty(printBill.ControlName))
            {
                System.Windows.Forms.MessageBox.Show("没有指定控件！");
                return -1;
            }
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\" + printBill.DLLName);
                object myObject = assembly.CreateInstance(printBill.ControlName, true);
                this.iPrint = myObject as FS.SOC.Local.Pharmacy.ShenZhen.Base.IPharmacyBillPrint;
                this.iPrint.SetPrintData(alPrintData, printBill);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("错误：" + ex.Message);
                return -1;
            }
            return 1;
        }

        #endregion

    }
    #endregion         

}
