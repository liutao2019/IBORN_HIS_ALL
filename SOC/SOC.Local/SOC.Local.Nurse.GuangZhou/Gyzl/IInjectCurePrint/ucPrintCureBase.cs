using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectCurePrint
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucPrintCureBase : UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint
    {
        #region 变量

        private ArrayList alPrint = new ArrayList();
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ucPrintCureBase()
        {
            InitializeComponent();
        }

        private void ucPrintCure_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="al"></param>
        public void Init(ArrayList tmp)
        {
            try
            {
                //ArrayList sortList = new ArrayList();
                //while (al.Count > 0)
                //{
                //    FS.HISFC.Models.Nurse.Inject temp = al[0] as FS.HISFC.Models.Nurse.Inject;
                //    ArrayList sameList = new ArrayList();
                //    foreach (FS.HISFC.Models.Nurse.Inject obj in al)
                //    {
                //        if (obj.InjectOrder == null || obj.InjectOrder == "")//.Item.CombNo
                //        {
                //            sameList.Add(obj);
                //            break;
                //        }
                //        if (obj.InjectOrder == temp.InjectOrder)
                //        {
                //            sameList.Add(obj);
                //        }
                //    }
                //    sortList.Add(sameList);
                //    foreach (FS.HISFC.Models.Nurse.Inject obj in sameList)
                //    {
                //        al.Remove(obj);
                //    }
                //}
                //foreach (ArrayList tmp in sortList)
                //{
                    if (this.neuSpread1_Sheet1.RowCount > 0)
                    {
                        this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                    }
                    FS.HISFC.Models.Nurse.Inject info = null;

                    //接瓶次数
                    int jpNum = 1;
                    //赋值并打印
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        info = (FS.HISFC.Models.Nurse.Inject)tmp[i];
                        this.neuSpread1_Sheet1.Rows.Add(0, 1);

                        jpNum = FS.FrameWork.Function.NConvert.ToInt32(info.Memo);
                        if (jpNum == 0)
                        {
                            jpNum = 1;
                        }
                        if (info.Item.Item.Name != null && info.Item.Item.Name != "")
                        {
                            this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Item.Name;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Name;
                        }
                         this.neuSpread1_Sheet1.Cells[0, 1].Text=  "[" +
                            Math.Round(info.Item.Order.DoseOnce / jpNum, 3).ToString() + info.Item.Order.DoseUnit + "]";
                    }
                    info = (FS.HISFC.Models.Nurse.Inject)tmp[0];
                    this.lbName.Text = info.Patient.Name;
                    this.lbNumber.Text = "注射顺序" + info.InjectOrder;
                    this.lbCard.Text = info.Patient.PID.CardNO;
                    this.lbTime.Text = System.DateTime.Now.ToString();
                    this.lbOrder.Text = info.OrderNO;
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
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 3);

                    this.Print();

                }
            //}
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        /// <summary>
        /// 打印函数
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = null;
            if (p == null)
            {
                p = new FS.FrameWork.WinForms.Classes.Print();

                //FS.Common.Class.Function.GetPageSize("Inject", ref p);
            }
            //System.Windows.Forms.Control c = this;
            //c.Width = this.Width;
            //c.Height = this.Height;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.PrintPage(12, 1, pnlPrint);
        }


        #region IInjectCurePrint 成员

        void FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint.Init(ArrayList alPrintData)
        {
            this.Init(alPrintData);
        }

        #endregion
    }
}
