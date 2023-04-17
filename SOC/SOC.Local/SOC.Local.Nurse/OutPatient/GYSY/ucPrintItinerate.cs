using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.GYSY
{
    /// <summary>
    /// {637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
    /// </summary>
    public partial class ucPrintItinerate : UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint
    {
        #region 变量
        
        private ArrayList alPrint = new ArrayList();
        
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();


        private FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        private FS.HISFC.BizLogic.Order.Order OrderBizLogic = new FS.HISFC.BizLogic.Order.Order(); 

        private int iSet = 8;

        /// <summary>
        /// 纸张
        /// </summary>
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        #endregion

        /// <summary>
        /// 是否补打
        /// </summary>
        private bool isReprint = false;

        /// <summary>
        /// 是否补打
        /// </summary>
        public bool IsReprint
        {
            get
            {
                return isReprint;
            }
            set
            {
                isReprint = value;
                this.lblReprint.Visible = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ucPrintItinerate()
        {
            InitializeComponent();
        }

        private void ucPrintItinerate_Load(object sender, EventArgs e)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="al">打印数组</param>
        /// <param name="current">当前页面数</param>
        /// <param name="total">总页数</param>
        private void Print(ArrayList al, int current, int total)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
            FS.HISFC.Models.Nurse.Inject info = null;
            int intCount = al.Count;
            //合并组号 xiaohf 2012年6月27日15:47:53
            Hashtable objHash = new Hashtable();
            for (int i = 0; i < intCount; i++)
            {
                 info = (FS.HISFC.Models.Nurse.Inject)al[i];
                 if (objHash.ContainsKey(info.Item.Order.Combo.ID+info.Item.User03))
                 {
                     objHash[info.Item.Order.Combo.ID + info.Item.User03] = int.Parse(objHash[info.Item.Order.Combo.ID + info.Item.User03].ToString()) + 1;
                 }
                 else
                 {
                     objHash.Add(info.Item.Order.Combo.ID + info.Item.User03, 1);
                 }
            }
            
            //接瓶次数
           // int jpNum = 1;
            //赋值并打印
            for (int i = 0; i < intCount; i++)
            {
                info = (FS.HISFC.Models.Nurse.Inject)al[i];
                if (objHash.ContainsKey(info.Item.Order.Combo.ID + info.Item.User03))
                {
                    int Count = int.Parse(objHash[info.Item.Order.Combo.ID + info.Item.User03].ToString());
                    this.neuSpread1_Sheet1.Rows.Add(i, Count);
                    this.neuSpread1_Sheet1.Cells[i, 0].RowSpan = Count;
                    this.neuSpread1_Sheet1.Cells[i, 4].RowSpan = Count;
                    this.neuSpread1_Sheet1.Cells[i, 5].RowSpan = Count;
                    this.neuSpread1_Sheet1.Cells[i, 6].RowSpan = Count;
                    this.neuSpread1_Sheet1.Cells[i, 7].RowSpan = Count;
                    this.neuSpread1_Sheet1.Cells[i, 8].RowSpan = Count;
                    this.neuSpread1_Sheet1.Cells[i, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    objHash.Remove(info.Item.Order.Combo.ID+info.Item.User03);
                }
                //this.neuSpread1_Sheet1.Rows.Add(0, 1);
                if (info.Item.Order.Combo.ID.Length <= 2)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text =
                        info.Item.Order.Combo.ID;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text =
                        info.Item.Order.Combo.ID.Substring(info.Item.Order.Combo.ID.Length - 2, 2);
                }
                //this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Order.Combo.ID;
                if (info.Item.Item.Name != null && info.Item.Item.Name != "")
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = info.Item.Item.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = info.Item.Name;
                }


                this.neuSpread1_Sheet1.Cells[i, 2].Text = info.Item.Order.DoseOnce.ToString() + info.Item.Order.DoseUnit.ToString();
                //string strHypo = string.Empty;
                //switch (info.Hypotest)
                //{
                //    case "2":
                //        strHypo = "需皮试";
                //        break;
                //    case "3":
                //        strHypo = "阳性";
                //        break;
                //    case "4":
                //        strHypo = "阴性";
                //        break;
                //    default:
                //        strHypo = " ";
                //        break;
                //}

                //strHypo = this.OrderBizLogic.TransHypotest(info.Hypotest);
                this.neuSpread1_Sheet1.Cells[i, 3].Text = this.OrderBizLogic.TransHypotest(info.Hypotest);
                this.neuSpread1_Sheet1.Cells[i, 4].Text = info.Memo;
                this.neuSpread1_Sheet1.Cells[i, 5].Text = " ";
                this.neuSpread1_Sheet1.Cells[i, 6].Text = " ";
                this.neuSpread1_Sheet1.Cells[i, 7].Text = " ";
                this.neuSpread1_Sheet1.Cells[i, 8].Text = " ";
            }
            info = (FS.HISFC.Models.Nurse.Inject)al[0];
            this.lbCard.Text = "病历号:"+info.Patient.PID.CardNO;
            this.lbName.Text ="姓名:"+ info.Patient.Name;
            this.lbTime.Text = "打印时间:"+System.DateTime.Now.ToString();
            this.lbAge.Text ="年龄"+ this.injectMgr.GetAge(info.Patient.Birthday, System.DateTime.Now);
            if (info.Patient.Sex.ID.ToString() == "M")
            {
                this.lbSex.Text = "性别:男";
            }
            else if (info.Patient.Sex.ID.ToString() == "F")
            {
                this.lbSex.Text = "性别:女";
            }
            else
            {
                this.lbSex.Text = "";
            }

            this.lbPage.Text = "第" + current.ToString()
                + "页" + "/" + "共" + total.ToString() + "页";
            this.lblDoct.Text = "医生："+info.Item.Order.Doctor.Name;
            this.lblDept.Text = "科室：" + info.Item.Order.DoctorDept.Name;
            GetHospLogo();
            //this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = "拔针";
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }
            this.Print();

        }

        /// <summary>
        /// 
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = null;
            if (p == null)
            {
                p = new FS.FrameWork.WinForms.Classes.Print();
                //FS.Common.Class.Function.GetPageSize("Inject2", ref p);
            }
            //System.Windows.Forms.Control c = this;
            //c.Width = this.Width;
            //c.Height = this.Height;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientItinerate");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientItinerate", 900, 750);
                }
            }
            p.SetPageSize(pageSize);
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsDataAutoExtend = false;


            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    p.PrintPreview(12, 0, this.pnlPrint);
            //}
            //else
            //{
                p.PrintPage(12, 1, this.pnlPrint);
            //}
        }
        //获取医院LOGO和名称
        private void GetHospLogo()
        {
            FS.SOC.Local.Nurse.OutPatient.GYSY.ComFun cf = new FS.SOC.Local.Nurse.OutPatient.GYSY.ComFun();
            string erro = "出错";
            string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }
    }
}
