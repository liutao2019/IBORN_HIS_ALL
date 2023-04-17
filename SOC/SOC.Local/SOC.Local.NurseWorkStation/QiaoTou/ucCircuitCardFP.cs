using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.NurseWorkStation.QiaoTou
{
    /// <summary>
    /// 输液卡、巡回单打印
    /// </summary>
    public partial class ucCircuitCardFP : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintTransFusion
    {
        public ucCircuitCardFP()
        {
            InitializeComponent();
        }
        
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizProcess.Integrate.RADT inpatientManager = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.Models.RADT.PatientInfo patient = null;
        ArrayList curValues = null; //当前显示的数据

        /// <summary>
        /// 住院号
        /// </summary>
        protected string inpatientNo;

        /// <summary>
        /// 是否打印
        /// </summary>
        bool isPrint = false;

        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        DateTime dt1;
        DateTime dt2;
        string usCode;
        ArrayList al;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted)
        {
            this.myPatients = patients;
            this.usCode = usageCode;
            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.IsPrint = isPrinted;
            //this.OnRetrieve(new object[1]);

            inpatientNo = "";
            for (int i = 0; i <= this.myPatients.Count - 1; i++)
            {
                string pNo = this.myPatients[i].ID;
                inpatientNo += pNo + "','";
            }
            //inpatientNo = inpatientNo.Substring(0, inpatientNo.Length - 1);
            ArrayList al = this.orderManager.QueryOrderCircult(inpatientNo, this.dt1, this.dt2, this.usCode, this.IsPrint);

            if (al == null || al.Count == 0)
            {
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            int index = 0;

            this.lblTip.Text = "";
            if (patients[0].User01 == "1")
            {
                this.lblTip.Text = "首日";
            }
            if (patients[0].User02 == "1")
            {
                this.lblTip.Text += " 长嘱";
            }
            else if (patients[0].User02 == "1")
            {
                this.lblTip.Text += " 临嘱";
            }

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in al)
            {
                #region 判断首日量和医嘱类型

                 //user01为是否首日量 1为首日医嘱；0否
                //user02为是医嘱类型：0为全部；1为长嘱；2为临嘱

                if (patients[0].User01 == "1")
                {
                    if (execOrder.Order.MOTime.Date != execOrder.DateUse.Date)
                    {
                        continue;
                    }
                }

                if (patients[0].User02 == "1")
                {
                    if (!execOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }
                else if(patients[0].User02=="2")
                {
                    if (execOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }

                #endregion

                this.neuSpread1_Sheet1.Rows.Add(index, 1);

                //开始时间
                this.neuSpread1_Sheet1.Cells[index, 0].Text = execOrder.Order.BeginTime.ToString("dd/MM");
                patientInfo = this.inpatientManager.GetPatientInfoByPatientNO(execOrder.Order.Patient.ID);
                //床号
                this.neuSpread1_Sheet1.Cells[index, 1].Text = patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
                //姓名
                this.neuSpread1_Sheet1.Cells[index, 2].Text = patientInfo.Name;
                //组号
                this.neuSpread1_Sheet1.Cells[index, 3].Text = execOrder.Order.Combo.Memo;
                //名称
                this.neuSpread1_Sheet1.Cells[index, 4].Text = execOrder.Order.Item.Name + "  " + execOrder.Order.Item.Specs;//+ "  " + execOrder.Order.Memo;
                //数量
                this.neuSpread1_Sheet1.Cells[index, 5].Text = execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit;
                //频次
                this.neuSpread1_Sheet1.Cells[index, 6].Text = execOrder.Order.Frequency.ID;
                //用法
                this.neuSpread1_Sheet1.Cells[index, 7].Text = execOrder.Order.Usage.Name;
                //每次量
                this.neuSpread1_Sheet1.Cells[index, 8].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.DoseUnit;
                //应执行时间
                this.neuSpread1_Sheet1.Cells[index, 9].Text = execOrder.Memo + execOrder.Order.Memo;
                //签名
                this.neuSpread1_Sheet1.Cells[index, 10].Text = "";
                //执行时间
                this.neuSpread1_Sheet1.Cells[index, 11].Text = ".";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrintSet()
        {
            print.ShowPrintPageDialog();
            this.Print();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Print()
        {
            try
            {
                //FS.HISFC.Components.Common.Classes.Function.GetPageSize("Nurse3", ref print);
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.IsDataAutoExtend = true;
                // print.PrintPreview(0, 0, this);

                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("Nurse3");
                if (pSize != null)
                {
                    //dwMain.Modify("DataWindow.Print.Paper.Size=256");
                    ////此处设置mm
                    //dwMain.Modify("DataWindow.Print.CustomPage.Length=80");
                    //dwMain.Modify("DataWindow.Print.CustomPage.Width=500");
                }

                //this.dwMain.Print();


                print.SetPageSize(pSize);
                print.PrintPage(0, 0, this.panel1);

                #region 更新已经打印标记
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                inpatientNo = "";
                for (int i = 0; i <= this.myPatients.Count - 1; i++)
                {
                    string pNo = this.myPatients[i].ID;
                    inpatientNo += pNo + "','";
                }
                //inpatientNo = inpatientNo.Substring(0, inpatientNo.Length - 1);
                ArrayList al = this.orderManager.QueryOrderCircult(inpatientNo, this.dt1, this.dt2, this.usCode, this.IsPrint);

                if (al == null || al.Count == 0)
                {
                    return;
                }
                foreach (FS.HISFC.Models.Order.ExecOrder obj in al)
                {
                    if (this.orderManager.UpdateTransfusionPrinted(obj.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新打印标记失败!" + orderManager.Err);
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        //protected override int OnRetrieve(params object[] objects)
        //{
        //    inpatientNo = "";
        //    List<string> listInpatientNo = new List<string>();

        //    for (int i = 0; i <= this.myPatients.Count - 1; i++)
        //    {
        //        string pNo = this.myPatients[i].ID;
        //        inpatientNo += pNo + ",";
        //        listInpatientNo.Add(this.myPatients[i].ID);
        //    }
        //    inpatientNo = inpatientNo.Substring(0, inpatientNo.Length - 1);
        //    #region {B7DD6B22-FFE2-4920-852D-690EF10C09A8}
        //    try
        //    {
        //        FS.HISFC.BizProcess.Integrate.Manager managermgr = new FS.HISFC.BizProcess.Integrate.Manager();

        //        string hosname = managermgr.GetHospitalName();

        //        //dwMain.Modify("t_1.text = '" + hosname + "住院病人巡回单'");
        //    }
        //    catch { }
        //    #endregion
        //    string[] myPatientNO = listInpatientNo.ToArray();            //dwMain.Retrieve(inpatientNo, this.dtpBeginTime.Value, this.dtpEndTime.Value);
        //    base.OnRetrieve(myPatientNO, this.usCode, this.dt1, this.dt2, FS.FrameWork.Function.NConvert.ToInt32(this.IsPrint).ToString());

        //    #region {B5986A1A-8244-43eb-975E-940A0FB45B5F}
        //    //高人教导三层的datawindow如果使用了分组必须调用一下这个方法
        //    //this.dwMain.CalculateGroups();
        //    #region 画组合号
        //    //if (this.dwMain.RowCount < 1)//如果没有医嘱返回
        //    //{
        //    //    return 1;
        //    //}
        //    //string curComboID = "";
        //    ////取组合号，注意datawindow是从1开始，和。net不一样
        //    //string tmpComboID = this.dwMain.GetItemString(1, 11) + this.dwMain.GetItemDateTime(1, 12).ToString("yyyyMMddHHmm");
        //    //for (int i = 2; i <= this.dwMain.RowCount; i++)
        //    //{
        //    //    curComboID = this.dwMain.GetItemString(i, 11) + this.dwMain.GetItemDateTime(i, 12).ToString("yyyyMMddHHmm");
        //    //    if (tmpComboID == curComboID)
        //    //    {
        //    //        //组合号相等，如果上一个没有标志说明是组合的第一个
        //    //        if (string.IsNullOrEmpty(this.dwMain.GetItemString(i - 1, 17).Trim()))
        //    //        {
        //    //            //组合第一个赋值
        //    //            this.dwMain.SetItemString(i - 1, "comb_text", "┏");
        //    //            //如果是最后一行
        //    //            if (i == this.dwMain.RowCount)
        //    //                this.dwMain.SetItemString(i, "comb_text", "┗");
        //    //            else
        //    //                this.dwMain.SetItemString(i, "comb_text", "┃");//这里不管是否是一组最后一个，最后一个在组合号不等时才设置
        //    //        }
        //    //        else
        //    //        {
        //    //            //如果是最后一行┏┗
        //    //            if (i == this.dwMain.RowCount)
        //    //                this.dwMain.SetItemString(i, "comb_text", "┗");
        //    //            else
        //    //                this.dwMain.SetItemString(i, "comb_text", "┃");
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        //组合号不等，这时会改变在组合号相等时设置的"┃"或者"┓"，为"┛"
        //    //        if (!string.IsNullOrEmpty(this.dwMain.GetItemString(i - 1, "comb_text").Trim()))
        //    //        {
        //    //            //设置一组的最后一个符合
        //    //            if (this.dwMain.GetItemString(i - 1, "comb_text") == "┃" || this.dwMain.GetItemString(i - 1, "comb_text") == "┏")
        //    //                this.dwMain.SetItemString(i - 1, "comb_text", "┗");
        //    //        }
        //    //    }
        //    //    tmpComboID = curComboID;
        //    //}
        //    #endregion
        //    #endregion
        //    return 1;

        //}

        #region IPrintTransFusion 成员


        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtTime, bool isPrinted)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPrintTransFusion 成员


        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType)
        {
            throw new NotImplementedException();
        }

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, bool isFirst)
        {
            throw new NotImplementedException();
        }

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType, bool isFirst)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IPrintTransFusion 成员


        public void SetSpeOrderType(string speStr)
        {
            throw new NotImplementedException();
        }

        #endregion


        //{014680EC-6381-408b-98FB-A549DAA49B82}
        #region IPrintTransFusion 成员
        // 摘要:
        //     停止后是否打印
        public bool DCIsPrint { get; set; }
        //
        // 摘要:
        //     未收费知否打印
        public bool NoFeeIsPrint { get; set; }
        //
        // 摘要:
        //     退费是否打印
        public bool QuitFeeIsPrint { get; set; }
        #endregion
    }
}
