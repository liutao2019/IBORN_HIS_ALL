using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Local.LisInterface.FOSI
{
    public partial class ucFoSiPacsReportPrint : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Order.ISaveOrder, FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint
    {
        public ucFoSiPacsReportPrint()
        {
            InitializeComponent();
        }

        #region 域变量
        FS.HISFC.BizLogic.Admin.FunSetting funMgr = new FS.HISFC.BizLogic.Admin.FunSetting();
        /// <summary>
        /// 诊断业务类
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        /// <summary>
        /// 非药品项目业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 项目序号
        /// </summary>
        int iOrder;
        /// <summary>
        /// 是否打印多页
        /// </summary>
        bool isPaging = false;
        /// <summary>
        /// 是否首次打印
        /// </summary>
        bool isFirstPrint = true;
        #endregion

        #region 属性
        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.Registration.Register regInfo;
        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegInfo
        {
            get
            {
                return regInfo;
            }
            set
            {
                regInfo = value;
                this.SetPatientInfo(regInfo);
            }
        }

        /// <summary>
        /// 检验项目
        /// </summary>
        private ArrayList alLisItems;
        /// <summary>
        /// 检验项目
        /// </summary>
        public ArrayList AlLisItems
        {
            get
            {
                return alLisItems;
            }
            set
            {
                this.Clear();
                alLisItems = value;
                if (alLisItems != null && alLisItems.Count > 0)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderItem = null;
                    string itemName = string.Empty;
                    string itemID = string.Empty;
                    for (int i = 0; i < alLisItems.Count; i++)
                    {
                        if (iOrder > 10)
                        {
                            if (MessageBox.Show("是否打印LIS申请单？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.Print();
                                this.Clear();
                                this.isFirstPrint = false;
                                this.isPaging = true;
                            }
                            else
                            {
                                this.isFirstPrint = false;
                                this.isPaging = false;
                                return;
                            }
                        }
                        orderItem = alLisItems[i] as FS.HISFC.Models.Order.OutPatient.Order;
                        itemName = orderItem.Item.Name + "[" + orderItem.Item.Qty.ToString() + orderItem.Unit + "]" + "(部位：" + orderItem.Sample.ToString() + ")";
                        itemID = orderItem.Item.ID;
                        if (this.SetInfo(i + 1, itemName, itemID) == -1)
                        {
                            alLisItems = null;
                            return;
                        }
                    }
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 把检验项目赋值到label中
        /// </summary>
        /// <param name="i"></param>
        /// <param name="itemName"></param>
        /// <param name="itemID"></param>
        /// <returns></returns>
        private int SetInfo(int i, string itemName, string itemID)
        {
            string strTemp = "";
            FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();
            item = itemMgr.GetItemByUndrugCode(itemID);
            if (item == null || item.ID == "")
            {
                this.errInfo = "查找项目" + itemName + "失败！";
                return -1;
            }
            strTemp = i + "、" + itemName;
            if (i <= 1)
            {

                if (strTemp.Length >= 21)
                {
                    this.txtItem.Text += strTemp.Substring(0, 21) + "\r\n" + strTemp.Substring(21, strTemp.Length - 21);
                }
                else
                {
                    this.txtItem.Text += strTemp;
                }
            }
            else
            {
                if (strTemp.Length >= 21)
                {
                    this.txtItem.Text += "\r\n" + strTemp.Substring(0, 21) + "\r\n" + strTemp.Substring(21, strTemp.Length - 21);
                }
                else
                {
                    this.txtItem.Text += "\r\n" + strTemp;
                }
            }
            return 1;
        }

        FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            pSize = pgMgr.GetPageSize("LisReport");

            if (pSize != null)
            {
                print.SetPageSize(pSize);
                if (!string.IsNullOrEmpty(pSize.Printer))
                {
                    print.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
                }
            }
            print.PrintDocument.DefaultPageSettings.Landscape = false;
            //print.PrintPreview(this);
            print.PrintPage(0, 0, this);

            return 1;
        }

        /// <summary>
        /// 设置患者相关信息
        /// </summary>
        /// <param name="register"></param>
        private void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            lbCilicCode.Text = "门诊号： " + register.ID;
            lblDept.Text = "科室：   " + ((FS.HISFC.Models.Base.Employee)funMgr.Operator).Dept.Name;
            ArrayList al = new ArrayList();
            lblDiagnose.Text = "诊断：";
            al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);

            if (al == null)
            {
                return;
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.Memo != null && diag.Memo != "")
                {
                    this.lblDiagnose.Text += diag.Memo + "、";
                }
                else
                {
                    this.lblDiagnose.Text += diag.DiagInfo.ICD10.Name;
                }
                this.lblDiagnose.Text = this.lblDiagnose.Text.TrimEnd(new char[] { '、' });
            }
            this.pbxCardNo.Image = this.CreateBarCode(regInfo.PID.CardNO);
            this.lblName.Text = "姓名：   " + register.Name;
            this.lblAge.Text = "年龄：   " + funMgr.GetAge(register.Birthday);
            this.lblSex.Text = "性别：   " + register.Sex.Name;
            this.lblOperCode.Text = "医生工作号：  " + ((FS.HISFC.Models.Base.Employee)funMgr.Operator).ID;
            this.lblOperName.Text = "医生签名:   " + ((FS.HISFC.Models.Base.Employee)funMgr.Operator).Name;
            this.lblPrintDate.Text = "打印时间:" + itemMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");


        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.pbxCardNo.Size.Width, this.pbxCardNo.Height);
        }

        private void Clear()
        {
            this.txtItem.Text = "";
        }

        /// <summary>
        /// 门诊LIS申请单打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="alLisOrder"></param>
        /// <param name="isShowNoNote"></param>
        /// <returns></returns>
        private int PACSReportPrint(FS.HISFC.Models.Registration.Register regObj, ArrayList alOrder, bool isShowNoNote)
        {
            try
            {
                this.RegInfo = regObj;

                ArrayList alTemp = new ArrayList();
                foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
                {
                    if (orderObj.Item.SysClass.ID.ToString() == "UC")
                    {
                        alTemp.Add(orderObj);
                    }
                }

                if (alTemp.Count > 0)
                {
                    this.AlLisItems = alTemp;
                    if (this.AlLisItems == null)
                    {
                        return -1;
                    }
                    if (this.isFirstPrint && MessageBox.Show("是否打印检查申请单？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Print();
                    }
                    if (this.isPaging)
                    {
                        this.Print();
                    }
                }
                else
                {
                    if (isShowNoNote)
                    {
                        MessageBox.Show("没有检验项目，无法打印检验申请单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            return 1;
        }

        #endregion

        #region ISaveOrder 成员

        string errInfo = "";

        public String ErrInfo
        {
            get
            {
                return ErrInfo;
            }
            set
            {
                ErrInfo = value;
            }
        }

        public int OnSaveOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return 1;
        }

        public int OnSaveOrderForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return this.PACSReportPrint(regObj, alOrder, false);
        }

        #endregion

        #region IPacsReportPrint 成员

        public int PacsReportPrintForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            return 1;
        }

        public int PacsReportPrintForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            return this.PACSReportPrint(regObj, alOrder, true);
        }

        #endregion
    }
}
