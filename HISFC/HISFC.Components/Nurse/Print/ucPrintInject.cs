using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse.Print
{
    /// <summary>
    /// 打印注射单
    /// </summary>
    public partial class ucPrintInject : UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint
    {
        #region 变量

        private ArrayList alPrint = new ArrayList();
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        private int iSet = 8;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ucPrintInject()
        {
            InitializeComponent();
        }

        private void ucPrintInject_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="al"></param>
        public void Init(ArrayList al)
        {
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(al.Count) / iSet));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = al.GetRange(iSet * (i - 1), iSet);
                        this.Print(alPrint, i, icount);
                    }
                    else
                    {
                        int num = al.Count % iSet;
                        if (al.Count % iSet == 0)
                        {
                            num = iSet;
                        }
                        alPrint = al.GetRange(iSet * (i - 1), num);
                        this.Print(alPrint, i, icount);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("打印出错!" + e.Message);
                return;
            }
        }

        private void Print(ArrayList al, int current, int total)
        {
            try
            {
                //有备注的集合
                ArrayList alMemo = new ArrayList();
                FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();
                //FS.HISFC.BizLogic.Order.OutPatient.Order orderMgr
                //    = new FS.HISFC.BizLogic.Order.OutPatient.Order();

                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }
                FS.HISFC.Models.Nurse.Inject info = null;

                //接瓶次数
                int jpNum = 1;
                //赋值并打印
                for (int i = 0; i < al.Count; i++)
                {
                    info = (FS.HISFC.Models.Nurse.Inject)al[i];
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                        new FS.HISFC.Models.Order.OutPatient.Order();
                    orderinfo = orderMgr.GetOneOrder(info.Patient.ID, info.Item.Order.ID);
                    if (orderinfo != null && orderinfo.Memo != null)
                    {
                        info.Item.Memo = orderinfo.Memo;
                        alMemo.Add(info);
                        
                    }
                    this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    if (info.Item.Order.Combo.ID.Length <= 2)
                    {
                        this.neuSpread1_Sheet1.Cells[0, 0].Text =
                            info.Item.Order.Combo.ID;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[0, 0].Text =
                            info.Item.Order.Combo.ID.Substring(info.Item.Order.Combo.ID.Length - 2, 2);
                    }
                    if (info.Item.Item.Name != null && info.Item.Item.Name != "")
                    {
                        this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Item.Name;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[0, 1].Text = info.Item.Name;
                    }
                    //jpNum = FS.FrameWork.Function.NConvert.ToInt32(info.Memo);
                                            
                    //if (jpNum == 0)
                    //{
                    //    jpNum = 1;
                    //}
                    
                    this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Item.Order.DoseOnce.ToString() + info.Item.Order.DoseUnit.ToString();
                    //Math.Round(info.Item.DoseOnce/jpNum,3).ToString() + info.Item.DoseUnit.ToString();
                    this.neuSpread1_Sheet1.Cells[0, 3].Text = info.Item.Order.Frequency.ID;
                    this.neuSpread1_Sheet1.Cells[0, 4].Text = info.Item.InjectCount.ToString();
                    this.neuSpread1_Sheet1.Cells[0, 5].Text = info.Item.Order.Usage.Name;
                    this.neuSpread1_Sheet1.Cells[0, 6].Text = info.Item.Memo.ToString();
                }

                #region 医嘱备注添加打印
                //if (alMemo.Count > 0)
                //{
                //    for (int i = 0; i < alMemo.Count; i++)
                //    {
                //        FS.HISFC.Models.Nurse.Inject temp = (FS.HISFC.Models.Nurse.Inject)alMemo[i];
                //        //					string strtemp = temp.Item.Name + ":" + temp.Item.Memo;
                //        string strtemp = "";
                //        if (temp.Item.Order.Combo.ID.Length <= 2)
                //        {
                //            strtemp = temp.Item.Order.Combo.ID.TrimStart('0') + "组:"
                //                + temp.Item.Memo;
                //        }
                //        else
                //        {
                //            strtemp = temp.Item.Order.Combo.ID.Substring(temp.Item.Order.Combo.ID.Length - 2, 2).TrimStart('0') + "组:"
                //                + temp.Item.Memo;
                //        }

                //        this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 5;
                //        this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = strtemp;
                //    }
                //}
                #endregion

                info = (FS.HISFC.Models.Nurse.Inject)al[0];
                this.lbCard.Text = info.Patient.PID.CardNO;
                this.lbName.Text = info.Patient.Name;
                this.lbTime.Text = "打印时间:" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.lbAge.Text = this.injectMgr.GetAge(info.Patient.Birthday, System.DateTime.Now);
                if (info.Patient.Sex.ID.ToString() == "M")
                {
                    this.lbSex.Text = "男";
                }
                else if (info.Patient.Sex.ID.ToString() == "F")
                {
                    this.lbSex.Text = "女";
                }
                else
                {
                    this.lbSex.Text = "";
                }
                this.lbPage.Text = "第" + current.ToString()
                                    + "页" + "/" + "共" + total.ToString() + "页";

                this.Print();

            }
            catch { }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = null;
            if (p == null)
            {
                p = new FS.FrameWork.WinForms.Classes.Print();
                //FS.Common.Class.Function.GetPageSize("Inject3", ref p);
            }
            //System.Windows.Forms.Control c = this;
            //c.Width = this.Width;
            //c.Height = this.Height;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.PrintPage(12, 1, pnlPrint);
        }
    }
}
