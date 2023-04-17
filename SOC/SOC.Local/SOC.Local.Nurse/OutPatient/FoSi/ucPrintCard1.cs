using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.FoSi
{
    public partial class ucPrintCard1 : System.Windows.Forms.UserControl
    {
        #region 域
        private ArrayList alPrint = new ArrayList();
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize =new FS.HISFC.Models.Base.PageSize();
        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList=new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

        FS.HISFC.BizLogic.Fee.Outpatient feeManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        #endregion

        #region 打印处理

        #region 方法
        public ucPrintCard1()
        {
            InitializeComponent();
        }

        public void Init(ArrayList al)
        {
            if (al.Count == 0)
            {
                return;
            }
            this.clear();
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < al.Count; i++)
            {
                info = (FS.HISFC.Models.Nurse.Inject)al[i];
                string name ="";
                if (info.Item.Item.Name != null && info.Item.Item.Name != "") //药品名
                {
                    name = info.Item.Item.Name;
                }
                else
                {
                    name = info.Item.Name;
                }
                if (i == 0)
                {
                    this.lbName1.Text = name;//药品名称
                    this.lbUserName1.Text = info.Item.Order.Usage.Name;//用法
                    this.lbToLower1.Text = info.Item.Order.Frequency.ID.ToLower();//频次
                    this.lbSpec1.Text = "(" + info.Item.Item.Specs + ")";//规格
                }
                if (i == 1)
                {
                    this.lbName2.Text = name;//药品名称
                    this.lbUserName2.Text = info.Item.Order.Usage.Name;//用法
                    this.lbToLower2.Text = info.Item.Order.Frequency.ID.ToLower();//频次
                    this.lbSpec2.Text = "("+info.Item.Item.Specs +")";//规格
                }
                if (i == 2)
                {
                    this.lbName3.Text = name;//药品名称
                    this.lbUserName3.Text = info.Item.Order.Usage.Name;//用法
                    this.lbToLower3.Text = info.Item.Order.Frequency.ID.ToLower();//频次
                    this.lbSpec3.Text = "(" + info.Item.Item.Specs + ")";//规格
                }
                if (i == 3)
                {
                    this.lbName4.Text = name;//药品名称
                    this.lbUserName4.Text = info.Item.Order.Usage.Name;//用法
                    this.lbToLower4.Text = info.Item.Order.Frequency.ID.ToLower();//频次
                    this.lbSpec4.Text = "(" + info.Item.Item.Specs + ")";//规格
                }
                //不知道为什么老是打印上一张的数据，暂时打印空格算了！
                string nullEmpty = "                         ";
                if(string.IsNullOrEmpty(this.lbName2.Text))
                {
                    this.lbName2.Text = nullEmpty;
                    this.lbUserName2.Text = nullEmpty;
                    this.lbToLower2.Text = nullEmpty;
                    this.lbSpec2.Text = nullEmpty;
                }
                if (string.IsNullOrEmpty(this.lbName3.Text))
                {
                    this.lbName3.Text = nullEmpty;
                    this.lbUserName3.Text = nullEmpty;
                    this.lbToLower3.Text = nullEmpty;
                    this.lbSpec3.Text = nullEmpty;
                }
                if (string.IsNullOrEmpty(this.lbName4.Text))
                {
                    this.lbName4.Text = nullEmpty;
                    this.lbUserName4.Text = nullEmpty;
                    this.lbToLower4.Text = nullEmpty;
                    this.lbSpec4.Text = nullEmpty;

                }
            }
            this.lbname.Text = info.Patient.Name; //病人姓名
            if (string.IsNullOrEmpty(info.Patient.PID.CardNO))
            {
                this.npbBarCode.Image = this.CreateBarCode(info.Patient.Card.ID); //病历号条码
                this.neuLblCardNo.Text = info.Patient.Card.ID.TrimStart('0');//病历号
            }
            this.Print();

        }
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = psManager.GetPageSize("MZZSBQ");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("MZZSBQ", 350, 350);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
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
            b.IncludeLabel = false;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }
        /// <summary>
        ///完清空数据
        /// </summary>
        private void clear()
        {
            this.lbName2.Text = "";
            this.lbUserName2.Text = "";
            this.lbToLower2.Text = "";
            this.lbSpec2.Text = "";
            this.lbName3.Text = "";
            this.lbUserName3.Text = "";
            this.lbToLower3.Text = "";
            this.lbSpec3.Text = "";
            this.lbName4.Text = "";
            this.lbUserName4.Text = "";
            this.lbToLower4.Text = "";
            this.lbSpec4.Text = "";
        }
        #endregion
    }
}
        #endregion