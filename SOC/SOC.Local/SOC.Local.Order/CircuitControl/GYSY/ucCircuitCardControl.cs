using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.CircuitControl.GYSY
{
    /// <summary>
    /// [功能描述: 贴瓶单控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///  />
    /// </summary>
    public partial class ucCircuitCardControl : FS.WinForms.Report.Common.ucQueryBaseForDataWindow, FS.HISFC.BizProcess.Interface.IPrintTransFusion
    {
        /// <summary>
        /// 
        /// </summary>
        public ucCircuitCardControl()
        {
            InitializeComponent();
        }
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        FS.HISFC.BizProcess.Integrate.RADT inpatientManager = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 
        /// </summary>
        protected string inpatientNo;
        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        DateTime dt1;
        DateTime dt2;
        string usCode;
        string execID = "ALL";

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
                ArrayList al = new ArrayList();// this.orderManager.QueryOrderCircult(inpatientNo, this.dt1, this.dt2, this.usCode, this.IsPrint);
                this.execID = "";

                for (int i = 1; i <= this.dwMain.RowCount; i++)
                {
                    string check = this.dwMain.GetItemString(i, "flag");

                    if (check == "1")
                    {
                        string execSqn = this.dwMain.GetItemString(i, "exec_sqn");

                        al.Add(execSqn);

                        this.execID += "'"+execSqn + "',";
                    }
                }

                if (this.execID.Length > 0)
                {
                    this.execID = this.execID.Substring(0, this.execID.Length - 2);
                    this.execID = this.execID.Substring(1);

                }

                this.OnRetrieve(new object[1]);

                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("Nurse3");
                if (pSize == null)
                {
                    //255是像素、256是厘米
                    dwMain.Modify("DataWindow.Print.Paper.Size=256");
                    //此处设置letter纸为：216*279 像素为850*1100
                    //dwMain.Modify("DataWindow.Print.CustomPage.Length=1100");
                    //dwMain.Modify("DataWindow.Print.CustomPage.Width=425");
                    //此处设置letter纸为：216*279
                    dwMain.Modify("DataWindow.Print.CustomPage.Length=140");
                    dwMain.Modify("DataWindow.Print.CustomPage.Width=216");
                }
                else
                {
                    dwMain.Modify("DataWindow.Print.Paper.Size=255");
                    dwMain.Modify("DataWindow.Print.CustomPage.Length=" + pSize.Height);
                    dwMain.Modify("DataWindow.Print.CustomPage.Width=" + pSize.Width);
                }


                dwMain.Modify("flag.visible = false");

                //数据窗口打印
                //dwMain.Print(true, true);
                dwMain.Print();

                dwMain.Modify("flag.visible = true");

                #region 更新已经打印标记
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                inpatientNo = "";
                for (int i = 0; i <= this.myPatients.Count - 1; i++)
                {
                    string pNo = this.myPatients[i].ID;
                    inpatientNo += pNo + "','";
                }

                if (al == null || al.Count == 0)
                {
                    return;
                }
                foreach (string execSqn in al)
                {
                    if (this.orderManager.UpdateCircultPrinted(execSqn) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新打印标记失败!" + orderManager.Err);
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                this.execID = "ALL";

                this.OnRetrieve(new object[1]);

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
        /// <param name="isPrinted"></param>
        /// <param name="orderType"></param>
        /// <param name="isFirst"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            inpatientNo = "";
            List<string> listInpatientNo = new List<string>();

            for (int i = 0; i <= this.myPatients.Count - 1; i++)
            {
                string pNo = this.myPatients[i].ID;
                inpatientNo += pNo + ",";
                listInpatientNo.Add(this.myPatients[i].ID);
            }
            inpatientNo = inpatientNo.Substring(0, inpatientNo.Length - 1);
            try
            {
                FS.HISFC.BizProcess.Integrate.Manager managermgr = new FS.HISFC.BizProcess.Integrate.Manager();

                string hosname = managermgr.GetHospitalName();

                dwMain.Modify("t_1.text = '" + hosname + "住院病人输液卡'");
            }
            catch { }

            string[] myPatientNO = listInpatientNo.ToArray(); 

            //user01为是否首日量 1为首日医嘱；0否
            //user02为是医嘱类型：0为全部；1为长嘱；2为临嘱
            base.OnRetrieve(myPatientNO, this.usCode, this.dt1, this.dt2, 
                FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString(),
                FS.FrameWork.Function.NConvert.ToInt32(isPrinted).ToString(), orderType, this.execID);

            //高人教导三层的datawindow如果使用了分组必须调用一下这个方法
            this.dwMain.CalculateGroups();

            #region 画组合号
            if (this.dwMain.RowCount < 1)//如果没有医嘱返回
            {
                return 1;
            }
            string curComboID = "";
            //取组合号，注意datawindow是从1开始，和。net不一样
            string tmpComboID = this.dwMain.GetItemString(1, 11) + this.dwMain.GetItemDateTime(1, 12).ToString("yyyyMMddHHmm");

            Hashtable hsMoOrder = new Hashtable();
            string moOrder = "";

            for (int i = 2; i <= this.dwMain.RowCount; i++)
            {
                #region 组合号处理

                curComboID = this.dwMain.GetItemString(i, 11) + this.dwMain.GetItemDateTime(i, 12).ToString("yyyyMMddHHmm");
                if (tmpComboID == curComboID)
                {
                    ///输液卡 输液卡用法同一组的不用显示多个用法
                    if (this.dwMain.GetItemString(i, "met_ipm_execdrug_use_name").Equals(this.dwMain.GetItemString(i - 1, "met_ipm_execdrug_use_name")))
                    {
                        this.dwMain.SetItemString(i, "met_ipm_execdrug_use_name", "");
                    }
                    //组合号相等，如果上一个没有标志说明是组合的第一个
                    if (string.IsNullOrEmpty(this.dwMain.GetItemString(i - 1, 17).Trim()))
                    {
                        //组合第一个赋值
                        this.dwMain.SetItemString(i - 1, "comb_text", "┏");
                        //如果是最后一行
                        if (i == this.dwMain.RowCount)
                            this.dwMain.SetItemString(i, "comb_text", "┗");
                        else
                            this.dwMain.SetItemString(i, "comb_text", "┃");//这里不管是否是一组最后一个，最后一个在组合号不等时才设置
                    }
                    else
                    {
                        //如果是最后一行┏┗
                        if (i == this.dwMain.RowCount)
                            this.dwMain.SetItemString(i, "comb_text", "┗");
                        else
                            this.dwMain.SetItemString(i, "comb_text", "┃");
                    }
                }
                else
                {
                    //组合号不等，这时会改变在组合号相等时设置的"┃"或者"┓"，为"┛"
                    if (!string.IsNullOrEmpty(this.dwMain.GetItemString(i - 1, "comb_text").Trim()))
                    {
                        //设置一组的最后一个符合
                        if (this.dwMain.GetItemString(i - 1, "comb_text") == "┃" || this.dwMain.GetItemString(i - 1, "comb_text") == "┏")
                            this.dwMain.SetItemString(i - 1, "comb_text", "┗");
                    }
                }
                tmpComboID = curComboID;

                #endregion
            }
            #endregion


            for (int i = 1; i <= this.dwMain.RowCount; i++)
            {
                //增加年龄显示
                try
                {
                    string birthday = this.dwMain.GetItemString(i, "txt_birthday");
                    this.dwMain.SetItemString(i, "txt_age", this.orderManager.GetAge(FS.FrameWork.Function.NConvert.ToDateTime(birthday)));
                    //this.dwMain.Modify("txt_age.text='" + this.orderManager.GetAge(FS.FrameWork.Function.NConvert.ToDateTime(birthday)) + "'");
                }
                catch
                {
                }

                #region 删除项目名称等

                moOrder = dwMain.GetItemString(i, "mo_order");

                if (hsMoOrder.Contains(moOrder))
                {
                    //组合号
                    dwMain.SetItemString(i, "comb_text", "");
                    //项目名称
                    dwMain.SetItemString(i, "itemName", "");

                    //每次量
                    dwMain.SetItemString(i, "dose_once", "");
                    dwMain.SetItemString(i, "met_ipm_execdrug_dose_unit", "");

                    //频次
                    dwMain.SetItemString(i, "met_ipm_execdrug_frequency_code", "");

                    //数量
                    dwMain.SetItemString(i, "qty", "");
                    //用法
                    dwMain.SetItemString(i, "met_ipm_execdrug_use_name", "");
                }
                else
                {
                    hsMoOrder.Add(moOrder, null);
                }

                #endregion
            }
            return 1;
        }

        #region IPrintTransFusion 成员

        bool isPrinted;
        string orderType;

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType, bool isFirst)
        {
            this.myPatients = patients;
            this.usCode = usageCode;
            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.isPrinted = isPrinted;
            this.orderType = orderType;

            this.OnRetrieve(isPrinted,orderType,isFirst, new object[1]);
        }

        public void SetSpeOrderType(string speStr)
        {
            return;
        }

        #endregion

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            string flag = "";

            if (this.chkAll.Checked)
            {
                flag = "1";
            }
            else
            {
                flag = "0";
            }

            for (int i = 1; i <= this.dwMain.RowCount; i++)
            {
                this.dwMain.SetItemString(i,"flag",flag);
                this.dwMain.Refresh();
            }
        }

        private void dwMain_ItemChanged(object sender, Sybase.DataWindow.ItemChangedEventArgs e)
        {
            if (e.ColumnName == "flag")
            {
                string curComboID = "";
                //取组合号，注意datawindow是从1开始，和。net不一样
                string tmpComboID = this.dwMain.GetItemString(e.RowNumber, 11) + this.dwMain.GetItemDateTime(e.RowNumber, 12).ToString("yyyyMMddHHmm");

                for (int i = 1; i <= this.dwMain.RowCount; i++)
                {
                    if (i == e.RowNumber)
                        continue;

                    curComboID = this.dwMain.GetItemString(i, 11) + this.dwMain.GetItemDateTime(i, 12).ToString("yyyyMMddHHmm");

                    if (tmpComboID == curComboID)
                    {
                        this.dwMain.SetItemString(i, "flag", e.Data);
                    }
                }
            }
        }


        #region IPrintTransFusion 成员

        public bool DCIsPrint
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool NoFeeIsPrint
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool QuitFeeIsPrint
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

