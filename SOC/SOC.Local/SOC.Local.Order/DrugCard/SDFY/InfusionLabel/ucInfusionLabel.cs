using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel
{
    /// <summary>
    /// [功能描述: 住院住院瓶贴默认实现]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2010-01]<br></br>
    /// </summary>
    public partial class ucInfusionLabel : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IControlPrintable
    {
        public ucInfusionLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 患者信息显示
        /// </summary>
        private System.Collections.Hashtable hsPatientInfo = new Hashtable();

        /// <summary>
        /// 业务管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();        

        /// <summary>
        /// 
        /// </summary>
        protected int vnum = 0;

        /// <summary>
        /// 
        /// </summary>
        protected int hnum = 0;

        #region IControlPrintable 成员

        public int BeginHorizontalBlankWidth
        {
            get
            {
                return 0;
            }
            set
            {
          
            }
        }

        public int BeginVerticalBlankHeight
        {
            get
            {
                return 20;
            }
            set
            {
            
            }
        }

        public System.Collections.ArrayList Components
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        /// <summary>
        /// 控件大小
        /// </summary>
        public Size ControlSize
        {
            get
            {
                return new Size(262, 200);
            }
        }
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 当前用户
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientnifo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 项目列表
        /// </summary>
        ArrayList alInfusionData = null;

        /// <summary>
        /// 是否自动换行
        /// </summary>
        bool blnLineFeeds = true;

        #region 不同的瓶签格式

        InfusionLabel.ucInfusionLabel3 inFuLabel3 = null;
        InfusionLabel.ucInfusionLabel4 inFuLabel4 = null;
        InfusionLabel.ucInfusionLabel5 inFuLabel5 = null;
        InfusionLabel.ucInfusionLabel6 inFuLabel6 = null;
        InfusionLabel.ucInfusionLabel7 inFuLabel7 = null;
        InfusionLabel.ucInfusionLabel8 inFuLabel8 = null;
        InfusionLabel.ucInfusionLabel99 inFuLabel99 = null;

        #endregion

        public object ControlValue
        {
            get
            {
                return null;
            }
            set
            {
                alInfusionData = value as ArrayList;
                if (alInfusionData == null)
                {
                    return;
                }

                #region 判断打印哪种瓶签
                //if (this.alInfusionData.Count <= 3)
                //{
                //    if (inFuLabel3 == null)
                //    {
                //        inFuLabel3 = new ucInfusionLabel3();
                //    }
                //    this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
                //    this.fpSpread1.Sheets.Add(inFuLabel3.SheetView);
                //    this.fpSpread1.Font = inFuLabel3.Font;
                //}
                if (this.alInfusionData.Count <= 4)
                {
                    if (inFuLabel4 == null)
                    {
                        inFuLabel4 = new ucInfusionLabel4();
                    }
                    this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
                    this.fpSpread1.Sheets.Add(inFuLabel4.SheetView);
                    this.fpSpread1.Font = inFuLabel4.Font;
                }
                else if (this.alInfusionData.Count == 5)
                {
                    if (inFuLabel5 == null)
                    {
                        inFuLabel5 = new ucInfusionLabel5();
                    }
                    this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
                    this.fpSpread1.Sheets.Add(inFuLabel5.SheetView);
                    this.fpSpread1.Font = inFuLabel5.Font;
                }
                else if (this.alInfusionData.Count == 5)
                {
                    if (inFuLabel6 == null)
                    {
                        inFuLabel6 = new ucInfusionLabel6();
                    }
                    this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
                    this.fpSpread1.Sheets.Add(inFuLabel6.SheetView);
                    this.fpSpread1.Font = inFuLabel6.Font;
                }
                else if (this.alInfusionData.Count == 5)
                {
                    if (inFuLabel7 == null)
                    {
                        inFuLabel7 = new ucInfusionLabel7();
                    }
                    this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
                    this.fpSpread1.Sheets.Add(inFuLabel7.SheetView);
                    this.fpSpread1.Font = inFuLabel7.Font;
                }
                else if (this.alInfusionData.Count == 5)
                {
                    if (inFuLabel8 == null)
                    {
                        inFuLabel8 = new ucInfusionLabel8();
                    }
                    this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
                    this.fpSpread1.Sheets.Add(inFuLabel8.SheetView);
                    this.fpSpread1.Font = inFuLabel8.Font;
                }
                else if (this.alInfusionData.Count  > 5)
                {
                    if (inFuLabel99 == null)
                    {
                        inFuLabel99 = new ucInfusionLabel99();
                    }
                    this.lblLack.Visible = true;
                    this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
                    this.fpSpread1.Sheets.Add(inFuLabel99.SheetView);
                    this.fpSpread1.Font = inFuLabel99.Font;
                }
                #endregion

                #region 标签信息

                int iIndex = 1;

                foreach (FS.HISFC.Models.Order.ExecOrder info in alInfusionData)
                {
                    #region 设置患者信息

                    if (this.hsPatientInfo.ContainsKey(info.Order.Patient.ID))
                    {
                        patientnifo = this.hsPatientInfo[info.Order.Patient.ID] as FS.HISFC.Models.RADT.PatientInfo;
                    }
                    else
                    {
                        patientnifo = radtIntegrate.QueryPatientInfoByInpatientNO(info.Order.Patient.ID);
                        info.Order.Patient = patientnifo;
                    }

                    if (patientnifo != null)
                    {
                        this.lbDept.Text = patientnifo.PVisit.PatientLocation.Dept.Name;

                        if (patientnifo.PVisit.PatientLocation.Bed.ID.Length <= 4)
                        {
                            this.lbBed.Text = patientnifo.PVisit.PatientLocation.Bed.ID + "床";//床号
                        }
                        else
                        {
                            this.lbBed.Text = patientnifo.PVisit.PatientLocation.Bed.ID.Substring(4, patientnifo.PVisit.PatientLocation.Bed.ID.Length - 4) + "床";//床号
                        }

                        this.lbName.Text = patientnifo.Name;
                        this.lbBed.Text += " " + patientnifo.Name;
                    }

                    #endregion

                    #region 设置用药信息

                    this.lbDate.Text = info.DateUse.ToString("yy.MM.dd");     //用药时间

                    if (info.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order orderObj = this.orderManager.QueryOneOrder(info.Order.ID);
                        if (orderObj == null)
                        {
                            MessageBox.Show(orderManager.Err);
                        }
                        else
                        {
                            info.Order.Usage = orderObj.Usage;
                        }
                    }
                    this.lbUsage.Text = info.Order.Usage.Name;

                    this.lbCombo.Text = "(" + info.Order.Combo.ID + ")";

                    #endregion

                    #region 药品信息

                    #region 皮试信息

                    try
                    {
                        int hypotest = this.orderManager.QueryOrderHypotest(info.Order.ID);

                        if (FS.SOC.Local.Order.Classes.Function.GetPhaItem(info.Order.Item.ID) != null && FS.SOC.Local.Order.Classes.Function.GetPhaItem(info.Order.Item.ID).IsAllergy)
                        {
                            info.Order.Memo += orderManager.TransHypotest((FS.HISFC.Models.Order.EnumHypoTest)hypotest);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("获得皮试信息出错！", "Note");
                    }
                    #endregion

                    this.fpSpread1.Sheets[0].Cells[iIndex, 3].Text = info.Order.Frequency.ID;

                    string itemName = "";

                    if (info.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //用量 因为存在合并列 列索引+1
                        this.fpSpread1.Sheets[0].Cells[iIndex, 2].Text = info.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Order.DoseUnit.ToString();

                        if (!string.IsNullOrEmpty(info.Order.Memo))
                        {
                            itemName = info.Order.Item.Name + "(" + info.Order.Memo + ")" + "[" + ((FS.HISFC.Models.Pharmacy.Item)info.Order.Item).BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + ((FS.HISFC.Models.Pharmacy.Item)info.Order.Item).DoseUnit + "]";
                        }
                        else
                        {
                            itemName = info.Order.Item.Name + "[" + ((FS.HISFC.Models.Pharmacy.Item)info.Order.Item).BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + ((FS.HISFC.Models.Pharmacy.Item)info.Order.Item).DoseUnit + "]";
                        }
                    }
                    else
                    {
                        //用量 因为存在合并列 列索引+1
                        this.fpSpread1.Sheets[0].Cells[iIndex, 2].Text = info.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Order.Unit.ToString();

                        if (!string.IsNullOrEmpty(info.Order.Memo))
                        {
                            itemName = info.Order.Item.Name + "(" + info.Order.Memo + ")";
                        }
                        else
                        {
                            itemName = info.Order.Item.Name;
                        }
                    }

                    this.fpSpread1.Sheets[0].Cells[iIndex, 0].Text = itemName;

                    #endregion

                    iIndex++;
                }

                this.cbxIsPrint.Checked = true;
                this.Tag = alInfusionData;
                #endregion
            }
        }

        /// <summary>
        /// 设置为打印状态
        /// </summary>
        /// <returns></returns>
        public void SetPrint()
        {
            this.cbxIsPrint.Visible = false;
        }

        /// <summary>
        /// 横排间隔大小
        /// </summary>
        public int HorizontalBlankWidth
        {
            get
            {
                return 5;
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
            set
            {
                
            }
        }

        public bool IsShowGrid
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        /// <summary>
        /// 竖排间隔大小
        /// </summary>
        public int VerticalBlankHeight
        {
            get
            {
                return 80;
            }
            set
            {
                
            }
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

        #endregion

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
                return this.bSelected;
            }
            set
            {
                this.bSelected = value;
                if (value)
                {
                    this.BackColor = Color.FromArgb( 224, 224, 224 );

                    foreach (Control c in this.Controls)
                    {
                        c.BackColor = Color.FromArgb(224, 224, 224);
                    }

                    for (int i = 0; i < this.fpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        this.fpSpread1.Sheets[0].Rows[i].BackColor = Color.FromArgb(224, 224, 224);
                        this.fpSpread1.Sheets[0].GrayAreaBackColor = Color.FromArgb(224, 224, 224);
                    }
                }
                else
                {
                    this.BackColor = Color.FromArgb( 255, 255, 255 );

                    foreach (Control c in this.Controls)
                    {
                        c.BackColor = Color.FromArgb(255, 255, 255);
                    }

                    for (int i = 0; i < this.fpSpread1.Sheets[0].Rows.Count; i++)
                    {
                        this.fpSpread1.Sheets[0].Rows[i].BackColor = Color.FromArgb(255, 255, 255);
                        this.fpSpread1.Sheets[0].GrayAreaBackColor = Color.FromArgb(255, 255, 255);
                    }
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            this.IsSelected = !this.IsSelected;

            base.OnClick( e );
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxIsPrint.Checked)
            {
                this.IsSelected = false;
            }
            else
            {
                this.IsSelected = true;
            }
        }

    }

    public class ExecOrderCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            //此处不在排序，用SQL排序
            return 0;
            try
            {
                FS.HISFC.Models.Order.ExecOrder exec1 = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder exec2 = y as FS.HISFC.Models.Order.ExecOrder;

                if (exec1.Order.SortID > exec2.Order.SortID)
                {
                    return 1;
                }
                else if (exec1.Order.SortID < exec2.Order.SortID)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }
}
