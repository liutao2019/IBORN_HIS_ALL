using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.DrugCard
{
    /// <summary>
    /// <br>[功能描述: 广医四院住院瓶贴实现]</br>
    /// <br>[修 改 者: SNIPER]</br>
    /// <br>[创建时间: 2011-12-15]</br>
    /// <br>基于朝阳医院的贴瓶单实现</br>
    /// <br>[遗留问题:瓶签已实现选择打印功能,但是更新打印状态时不能保留未选择打印的未打印状态]</br>
    /// </summary>
    public partial class ucInfusionLabel : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IControlPrintable
    {
        public ucInfusionLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        private System.Collections.Hashtable hsPatientInfo = new Hashtable();
        FS.HISFC.Models.RADT.PatientInfo patientnifo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 业务管理类
        /// 通过Patient.ID获取患者对象
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 为获取皮试信息
        /// </summary>
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        #region IControlPrintable 成员

        protected int vnum = 0;
        protected int hnum = 0;

        /// <summary>
        /// 是否显示通用名
        /// </summary>
        bool isDisplayRegularName = false;

        public int BeginHorizontalBlankWidth
        {
            get
            {
                return 1;
            }
            set { }
        }
        public int BeginVerticalBlankHeight
        {
            get
            {
                return 2;
            }
            set { } 
        }
        public System.Collections.ArrayList Components
        {
            get
            {
                return null;
            }
            set { }
        }

        // 控件大小
        public Size ControlSize
        {
            get
            {
                //return new Size(313, 230);
                return this.Size;
            }
        }

        private ArrayList alData = null;
     
        /// <summary>
        /// 赋值
        /// </summary>
        public object ControlValue
        {
            get
            {
                return null;
            }
            set
            {
                ArrayList alInfusionData = value as ArrayList;
                if (alInfusionData == null)
                {
                    return;
                }

                for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
                {
                    for (int j = 0; j < fpSpread1_Sheet1.ColumnCount; j++)
                    {
                        fpSpread1_Sheet1.Cells[i, j].Text = "";
                    }
                }

                #region 标签信息

                int rowIndex = 0;

                //int length = "淫荡邪恶猥琐的长度标咔咔咔咔咔".Length;


                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alInfusionData)
                {
                    if (rowIndex > 6)
                    {
                        break;
                    }

                    #region 设置患者信息

                    if (this.hsPatientInfo.ContainsKey(execOrder.Order.Patient.ID))
                    {
                        patientnifo = this.hsPatientInfo[execOrder.Order.Patient.ID] as FS.HISFC.Models.RADT.PatientInfo;
                    }
                    else
                    {
                        patientnifo = radtIntegrate.QueryPatientInfoByInpatientNO(execOrder.Order.Patient.ID);
                        this.hsPatientInfo.Add(patientnifo.ID, patientnifo);
                    }

                    if (patientnifo != null)
                    {
                        this.lblFrequency.Text = execOrder.Order.Frequency.ID + "  ";
                        this.lblBedNo.Text = patientnifo.PVisit.PatientLocation.Bed.ID.Substring(4) + "  ";
                        lblName.Text = patientnifo.Name + "  ";
                    }

                    #endregion 设置患者信息

                    #region 设置用药信息

                    #region 用药时间

                    DateTime dtUse = execOrder.DateUse;

                    int hour = execOrder.DateUse.Hour;// {A196A50F-36E2-49b4-B530-DCC38D9D4464}
                    if (hour == 0)
                    {
                        hour = 24;
                        dtUse = dtUse.AddDays(-1);
                    }

                    if (hour <= 12)
                    {
                        this.lbDate.Text = dtUse.ToString("yyyy.MM.dd ") + hour + "a";
                    }
                    else
                    {
                        this.lbDate.Text = dtUse.ToString("yyyy.MM.dd ") + (hour - 12) + "p";
                    }

                    //this.lbDate.Text = dtUse.ToString("yyyy年MM月dd日");
                    #endregion

                    //组号
                    this.lblOrderNum.Text = (execOrder.Order.OrderType.IsDecompose ? "长" : "临") + execOrder.Order.SubCombNO.ToString();
                    //lblOrderNum.Text = execOrder.Order.SubCombNO.ToString() + "方";
                    lblOrderType.Text = execOrder.Order.OrderType.IsDecompose ? "长嘱" : "临嘱";

                    this.lblUsage.Text = execOrder.Order.Usage.Name;

                    lblPage.Text = execOrder.Order.User03;

                    #endregion

                    #region 停用信息

                    lblValid.Text = "";
                    if (execOrder.Order.Status == 3)// || execOrder.Order.Status == 4)
                    {
                        if (execOrder.DateUse >= execOrder.Order.EndTime)
                        {
                            lblValid.Text = "停";
                        }
                    }

                    #endregion

                    #region 药品信息

                    //组号
                    this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text = execOrder.Order.SubCombNO.ToString();

                    //名称
                    string itemName = "";

                    //if (isDisplayRegularName
                    //    && execOrder.Order.Item.ID != "999")
                    //{
                    //    FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(execOrder.Order.Item.ID) as FS.HISFC.Models.Pharmacy.Item;
                    //    itemName = phaItem.NameCollection.RegularName + execOrder.Order.Memo + "[" + phaItem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaItem.DoseUnit + "]";
                    //}
                    //else
                    //{
                    //    itemName = execOrder.Order.Item.Name + (string.IsNullOrEmpty(execOrder.Order.Item.Specs) ? "" : "[" + execOrder.Order.Item.Specs + "]");
                    //}

                    //自备、嘱托标记  用于护士打印单据和医嘱单显示区分
                    string byoStr = "";

                    if (!execOrder.Order.OrderType.IsCharge || execOrder.Order.Item.ID == "999")
                    {
                        if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            byoStr = "[自备]";
                        }
                        else
                        {
                            byoStr = "[嘱托]";
                        }
                    }
                    itemName = byoStr + execOrder.Order.Item.Name;

                    this.fpSpread1_Sheet1.Cells[rowIndex, 1].Text = itemName;

                    //用量
                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        this.fpSpread1_Sheet1.Cells[rowIndex, 2].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.DoseUnit.ToString();
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[rowIndex, 2].Text = execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit.ToString();
                    }

                    fpSpread1_Sheet1.Rows[rowIndex].Tag = execOrder;

                    rowIndex++;
                    #endregion  药品信息
                }

                this.checkBox1.Checked = true;
                this.Tag = alInfusionData;

                #endregion 标签信息

                alData = alInfusionData;

                float height = pnItem.Height;

                //动态调整项目显示PN
                float rowHeight = fpSpread1_Sheet1.Rows[0].Height;
                float totHeight = (rowHeight + 1) * alData.Count;

                pnItem.Height = (Int32)totHeight;

                this.Height -= (Int32)(height - pnItem.Height);

                this.Height += (Int32)rowHeight * 2;
            }
        }

        //public void Print()
        //{
        //    float height = pnItem.Height;

        //    //动态调整项目显示PN
        //    float rowHeight = fpSpread1_Sheet1.Rows[0].Height;
        //    float totHeight = (rowHeight + 1) * alData.Count;

        //    pnItem.Height = (Int32)totHeight;

        //    this.Height -= (Int32)(height - pnItem.Height);

        //}

        /// <summary>
        /// 横排间隔大小
        /// </summary>
        public int HorizontalBlankWidth
        {
            get
            {
                return 2;
            }
            set
            {
                
            }
        }

        /// <summary>
        /// 横排数量
        /// </summary>
        public int HorizontalNum
        {
            get
            {
                return this.hnum;
            }
            set
            {
                this.hnum = value;
            }
        }

        public bool IsCanExtend
        {
            get
            {
                return false;
            }
            set  { }
        }

        public bool IsShowGrid
        {
            get
            {
                return false;
            }
            set { }
        }

        /// <summary>
        /// 竖排间隔大小
        /// </summary>
        public int VerticalBlankHeight
        {
            get
            {
                return 2;
            }
            set { }
        }

        /// <summary>
        /// 竖排数量
        /// </summary>
        public int VerticalNum
        {
            set
            {
                this.vnum = value;
            }
            get
            {
                return this.vnum;
            }
        }

        #endregion IControlPrintable 成员

        /// <summary>
        /// 是否选择
        /// </summary>
        protected bool bSelected = false;

        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.checkBox1.Checked;
                //return bSelected;
            }
            set
            {
                this.checkBox1.Checked = value;
                //bSelected = value;
                //if (value)
                //{
                //    this.BackColor = Color.FromArgb( 224, 224, 224 );
                //}
                //else
                //{
                //    this.BackColor = Color.FromArgb( 255, 255, 255 );
                //}
            }
        }

        /// <summary>
        /// 打印的时候隐藏
        /// </summary>
        public void SetPrint() 
        {
            this.checkBox1.Visible = false;
            this.BackColor = Color.FromArgb(255, 255, 255);
        }

        protected override void OnClick(EventArgs e)
        {
            return; //2011-12-16 sniper 
            this.IsSelected = !this.IsSelected;
            base.OnClick( e );
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                //this.IsSelected = true;
                this.BackColor = Color.FromArgb(224, 224, 224);
            }
            else
            {
                //this.IsSelected = false;
                this.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void ucInfusionLabel_Load(object sender, EventArgs e)
        {
            //FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //isDisplayRegularName = controlIntegrate.GetControlParam<bool>("HNZY01", false, false);
        }
    }
}
