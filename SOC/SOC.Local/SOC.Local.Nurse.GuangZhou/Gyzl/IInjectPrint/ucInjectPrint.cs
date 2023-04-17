using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectPrint
{
    public partial class ucInjectPrint : UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint
    {
        public ucInjectPrint()
        {
            InitializeComponent();
            ArrayList al = this.inteManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null)
            {
                return;
            }
            this.doctHelper.ArrayObject = al;

            al = this.inteManager.GetDepartment();
            if (al == null)
            {
                return;
            }
            this.deptHelper.ArrayObject = al;

            al = this.inteManager.QuereyFrequencyList();
            if (al == null)
            {
                return;
            }
            this.freqHelper.ArrayObject = al;
        }

        private bool isRePrint = false;

        private FS.HISFC.Models.Registration.Register register;

        /// <summary>
        /// 院注管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject injectManager = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 医生帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freqHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 公用方法类
        /// </summary>
        private FS.SOC.Local.Nurse.GuangZhou.Gyzl.Common comManager = new FS.SOC.Local.Nurse.GuangZhou.Gyzl.Common();

        #region IInjectPrint 成员

        //public void Init(System.Collections.ArrayList alPrintData)
        //{
        //    string data = alPrintData[0] as string;
        //    if (data.Contains("(*)"))
        //    {
        //        DialogResult dr = MessageBox.Show("是否补打?");
        //        if (dr == DialogResult.OK)
        //        {
        //            this.lblReprint.Visible = true;
        //            data = data.Replace("(*)", "");
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    this.lblOrder.Text = data;
        //    this.print();
        //}

        /// <summary>
        /// 初始化注射数据
        /// alPrintData[0] bool 是否补打 true为补打,默认非补打
        /// alPrintData[1] FS.HISFC.Models.Registration.Register 患者信息
        /// alPrintData[2] ArrayList 选择的打印注射项目
        ///                FS.HISFC.Models.Fee.Outpatient.FeeItemList itemInfo
        /// </summary>
        /// <param name="alPrintData"></param>
        public void Init(System.Collections.ArrayList alPrintData)
        {
            int result;
            if (alPrintData.Count < 3)
            {
                //参数个数不足
                return;
            }
            if (alPrintData[0] != null && alPrintData[0] is bool)
            {
                isRePrint = (bool)alPrintData[0];
                if (isRePrint)
                {
                    this.lblReprint.Visible = true;
                }
            }

            this.register = alPrintData[1] as FS.HISFC.Models.Registration.Register;
            if (this.register != null)
            {
                result = this.setPatient();
                if (result != 1)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            if (alPrintData[2] != null && alPrintData[2] is ArrayList)
            {
                int count = 0;
                ArrayList printData = alPrintData[2] as ArrayList;
                do
                {
                    count = this.countInjectItem(printData);
                    if (count == 0)
                    {
                        result = this.setInjectItem(printData);
                    }
                    else
                    {
                        result = this.setInjectItem(printData.GetRange(0, count));
                    }
                    printData = printData.GetRange(count, printData.Count - count);
                    if (result != 1)
                    {
                        return;
                    }
                    else
                    {
                        this.print();
                    }
                } while (count > 0);
                result = this.setInjectItem(alPrintData[2] as ArrayList);
                if (result != 1)
                {
                    return;
                }
            }
            else
            {
                return;
            }

            //this.print();
        }

        #endregion

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int setPatient()
        {
            if (this.register == null)
            {
                return -1;
            }

            this.lblName.Text = register.Name;
            if ("男".Equals(register.Sex.Name))
            {
                this.chkMale.Checked = true;
                this.chkFemale.Checked = false;
            }
            else
            {
                this.chkMale.Checked = false;
                this.chkFemale.Checked = true;
            }
            this.lblAge.Text = this.injectManager.GetAge(register.Birthday);
            this.lblSeeDept.Text = this.deptHelper.GetName(register.SeeDoct.Dept.ID);
            this.lblSeeDoct.Text = this.doctHelper.GetName(register.SeeDoct.ID);
            this.lblCardNo.Text = register.PID.CardNO;
            this.npbBarCode.Image = this.CreateBarCode(register.PID.CardNO);
            if (string.IsNullOrEmpty(register.AddressHome))
            {
                if (string.IsNullOrEmpty(register.PhoneHome))
                {
                    this.lblTel.Visible = false;
                }
                else
                {
                    this.lblTel.Text = register.PhoneHome;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(register.PhoneHome))
                {
                    this.lblTel.Text = register.AddressHome;
                }
                else
                {
                    this.lblTel.Text = register.AddressHome + " / " + register.PhoneHome;
                }
            }
            if (string.IsNullOrEmpty(register.IDCard))
            {
                this.lblIdenNO.Visible = false;
            }
            else
            {
                this.lblIdenNO.Text = register.IDCard;
            }

            if (this.register.Pact.PayKind.ID == "01")
            {
                this.lblFeeType.Text = "自费";
            }
            else if (this.register.Pact.PayKind.ID == "02")
            {
                this.lblFeeType.Text = "医保";
            }
            else
            {
                this.lblFeeType.Text = "公费";
            }

            return 1;
        }

        /// <summary>
        /// 计算当前页显示项目数
        /// </summary>
        /// <param name="alInjectItem">需要计算的项目</param>
        /// <returns>项目数</returns>
        private int countInjectItem(ArrayList alInjectItem)
        {
            if (alInjectItem == null || alInjectItem.Count == 0)
            {
                return -1;
            }
            string lastComboID = string.Empty;
            int rowCount = 0;
            int comboItemCount = 0;//组内项目数
            int lastComboItemCount = 0;//前一个组内项目数
            for (int i = 0; i < alInjectItem.Count; i++)
            {
                rowCount += 3;//一个项目占3行
                comboItemCount++;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = alInjectItem[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (!lastComboID.Equals(detail.Order.Combo.ID))
                {
                    lastComboItemCount = comboItemCount;
                    comboItemCount = 0;
                    rowCount++;//每组最后显示用户占1行
                }
                lastComboID = detail.Order.Combo.ID;
                if (i == alInjectItem.Count - 1)
                {
                    if (rowCount > 32)
                    {
                        lastComboItemCount = comboItemCount;
                        return i - lastComboItemCount;
                    }
                    else
                    {
                        if (rowCount < 32)
                        {
                            return 0;
                        }
                        else
                        {
                            return i;
                        }
                    }
                }
                else
                {
                    if (rowCount > 32)//显示行数
                    {
                        lastComboItemCount = comboItemCount;
                        return i - lastComboItemCount;//已经超出一页显示范围,减去最后一组的项目数
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 设置注射项目信息
        /// </summary>
        /// <param name="alInjectItem"></param>
        /// <returns></returns>
        private int setInjectItem(ArrayList alInjectItem)
        {
            if (alInjectItem == null || alInjectItem.Count == 0)
            {
                return -1;
            }
            else
            {
                string result = string.Empty;
                string lastComboID = string.Empty;
                bool isShowUsage = false;
                for (int i = 0; i < alInjectItem.Count; i++)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = alInjectItem[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    if (detail == null)
                    {
                        return -1;
                    }

                    isShowUsage = false;

                    #region 是否显示用法,即组号是否与下一个项目相同
                    //同组只显示一个用法
                    if (i + 1 < alInjectItem.Count)
                    {
                        FS.HISFC.Models.Fee.Outpatient.FeeItemList nextDetail = alInjectItem[i + 1] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                        if (!detail.Order.Combo.ID.Equals(nextDetail.Order.Combo.ID))
                        {
                            //当前与下个项目组号不同,即组内最后一个项目
                            isShowUsage = true;
                        }
                    }
                    else
                    {
                        //最后一个项目
                        isShowUsage = true;
                    }
                    #endregion

                    #region 显示组合
                    //组号
                    if (!lastComboID.Equals(detail.Order.Combo.ID))
                    {
                        if (isShowUsage)
                        {
                            result += "  ";
                        }
                        else
                        {
                            result += "┏";
                        }
                    }
                    else if (isShowUsage)
                    {
                        result += "┗";
                    }
                    else
                    {
                        result += "┃";
                    }
                    #endregion

                    result += detail.Item.Name;                     //名称
                    result += "[" + detail.Item.Specs + "]";        //规格
                    result += "\n";
                    result += "  每次量:" + detail.Order.DoseOnce.ToString() + detail.Order.DoseUnit;//每次量
                    result += "\n";
                    if (isShowUsage)
                    {
                        result += "  用法:" + string.Format("{0,-" + (12 - detail.Order.Usage.Name.Length) + "}", detail.Order.Usage.Name);//用法
                        result += string.Format("{0,-8}", this.freqHelper.GetName(detail.Order.Frequency.ID));//频次
                        result += string.Format("{0,-12}", this.comManager.GetInjectDay(detail, isRePrint));//院注天数
                        result += string.Format("{0,10}", " 执行者:        时间:");
                    }
                    result += "\n";
                    if (isShowUsage)
                    {
                        result += "\n";
                    }
                    //记录最后一个组号
                    lastComboID = detail.Order.Combo.ID;
                }
                this.lblOrder.Text = result;
                return 1;
            }
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
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        private void print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 790));
            this.lblPrintTime.Text = this.injectManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            print.PrintPage(5, 5, this);
        }
    }
}
