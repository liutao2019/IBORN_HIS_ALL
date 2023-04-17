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
                return 1;
            }
            set
            {
          
            }
        }

        public int BeginVerticalBlankHeight
        {
            get
            {
                return 2;
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
                return new Size(340, 240);//(262, 200);
            }
             
        }
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        public object ControlValue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                ArrayList alInfusionData = value as ArrayList;
                if (alInfusionData == null)
                {
                    return;
                }

                FS.HISFC.Models.RADT.PatientInfo patientnifo = new FS.HISFC.Models.RADT.PatientInfo();

                #region 标签信息

                int iIndex = 1;
                //按字符计算{3537A677-3DEF-47f6-B352-5BAEABA9327E}
                int length = 28;// "哈哈哈哈哈哈哈哈哈哈哈哈哈哈".Length;

                // {2F204B7C-61F4-4f0c-9696-E4183CEBBF0C}
                // FP 设定打印8行，当组合超过4项时，且项目名称长度都超过 length 时，程序报错
                // 处理方法： 当组合超过4项时，不进行换行处理；超过8项时，只处理前8项

                bool blnLineFeeds = true;
                if (alInfusionData.Count >= 4)
                {
                    blnLineFeeds = false;
                }

                bool bDealItemName = true;

                FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                bDealItemName = con.GetControlParam<bool>("HNHS01", false, false);

                foreach (FS.HISFC.Models.Order.ExecOrder info in alInfusionData)
                {
                    if (!string.IsNullOrEmpty(info.User01) && info.User01 != "1")
                    {
                        //this.lblContinue.Visible = true;
                        this.lblContinue.Text = "第一页续";
                    }
                    else
                    {
                        //this.lblContinue.Visible = false;
                        this.lblContinue.Text = "第一页";
                    }
                    #region 设置患者信息

                    if (this.hsPatientInfo.ContainsKey(info.Order.Patient.ID))
                    {
                        patientnifo = this.hsPatientInfo[info.Order.Patient.ID] as FS.HISFC.Models.RADT.PatientInfo;
                    }
                    else
                    {
                        patientnifo = radtIntegrate.QueryPatientInfoByInpatientNO(info.Order.Patient.ID);
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
                        this.lbBed.Text += patientnifo.Name;
                    }

                    #endregion

                    #region 设置用药信息

                    this.lbDate.Text = info.DateUse.ToString("yy.MM.dd");     //用药时间

                    this.lbUsage.Text = info.Order.Usage.Name;

                    this.lbCombo.Text = "(" + info.Order.Combo.ID + ")";

                    if (iIndex == 0)
                    {
                        //this.lbLabelTotNum.Text = info.Operation.User01;
                        //if (info.Operation.User03 == "-1")
                        //{
                        //    this.lbiCount.Text = info.Operation.User02;
                        //}
                        //else
                        //{
                        //    this.lbiCount.Text = info.Operation.User02 + "(" + info.Operation.User03 + ")";
                        //}
                    }

                    #endregion

                    #region 药品信息

                    #region 皮试信息

                    if (info.Order.Memo != "" && info.Order.Memo.IndexOf("需皮试") != -1)
                    {
                        try
                        {
                            int hypotest = this.orderManager.QueryOrderHypotest(info.Order.ID);
                            switch (hypotest)
                            {
                                case 2:
                                    info.Order.Memo += "需皮试";
                                    break;
                                case 3:
                                    info.Order.Memo += "皮试(+)";
                                    break;
                                case 4:
                                    info.Order.Memo += "皮试(-)";
                                    break;
                                default:
                                    info.Order.Memo += "需皮试";
                                    break;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("获得皮试信息出错！", "Note");
                        }
                    }
                    #endregion

                    // {2F204B7C-61F4-4f0c-9696-E4183CEBBF0C}
                    if (iIndex >= 7)
                    {
                        break;
                    }


                    //用量 因为存在合并列 列索引+1
                    //this.fpSpread1_Sheet1.Cells[iIndex, 2].Text = info.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Order.DoseUnit.ToString();
                    this.fpSpread1_Sheet1.Cells[iIndex, 3].Text = info.Order.Frequency.ID;
                    //string itemName = "";

                    //if (info.Order.Memo==null|| info.Order.Memo == "")
                    //{
                    //    itemName = info.Order.Item.Name;
                    //}
                    //else
                    //{
                    //    itemName = info.Order.Item.Name + " " + "备注：" + info.Order.Memo;// +" " + info.Order.Item.Specs;
                    //}

                    string itemName = "";

                    if (info.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {

                        //用量 因为存在合并列 列索引+1
                        this.fpSpread1_Sheet1.Cells[iIndex, 2].Text = info.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Order.DoseUnit.ToString();

                        FS.HISFC.Models.Pharmacy.Item phaitem = (info.Order.Item as FS.HISFC.Models.Pharmacy.Item);

                        if (bDealItemName && info.Order.Item.Name.IndexOf("(") > 0 && info.Order.Patient.Pact.UserCode != "NONE")
                        {
                            int index = 0;

                            for (int j = 0; j < info.Order.Item.Name.Length; j++)
                            {
                                if (info.Order.Item.Name.Substring(j, 1) == "(")
                                {
                                    index = j;
                                }
                            }

                            info.Order.Item.Name = info.Order.Item.Name.Substring(0, index);
                        }
                        //itemName = info.Order.Item.Name + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]" + " " + info.Order.Memo;

                        if (!string.IsNullOrEmpty(info.Order.Memo))
                        {
                            itemName = info.Order.Item.Name + "(" + info.Order.Memo + ")" + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";
                        }
                        else
                        {
                            //itemName = info.Order.Item.Name + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";
                            ///一行最多可写28个字符{3537A677-3DEF-47f6-B352-5BAEABA9327E}
                            string tempItemName = info.Order.Item.Name + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";
                                                     
                            if (System.Text.Encoding.Default.GetByteCount(tempItemName) >= 28)
                            {
                                itemName = info.Order.Item.Name.ToString().PadRight(28, ' ') + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";
                            }
                            else
                            {
                                itemName = info.Order.Item.Name + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";
                            }
                        }
                    }
                    else
                    {
                        //用量 因为存在合并列 列索引+1
                        this.fpSpread1_Sheet1.Cells[iIndex, 2].Text = info.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Order.Unit.ToString();

                        if (bDealItemName && info.Order.Patient.Pact.UserCode != "NONE")
                        {
                            int index = 0;

                            for (int j = 0; j < info.Order.Item.Name.Length; j++)
                            {
                                if (info.Order.Item.Name.Substring(j, 1) == "(")
                                {
                                    index = j;
                                }
                            }

                            if (info.Order.Item.Name.IndexOf("(") > 0)
                            {
                                info.Order.Item.Name = info.Order.Item.Name.Substring(0, index);
                            }
                        }

                        if (!string.IsNullOrEmpty(info.Order.Memo))
                        {
                            itemName = info.Order.Item.Name + "(" + info.Order.Memo + ")";
                        }
                        else
                        {
                            itemName = info.Order.Item.Name;
                        }
                    }





                    // {2F204B7C-61F4-4f0c-9696-E4183CEBBF0C}
                    if (blnLineFeeds)
                    {
                        if (itemName.Length > length)
                        {
                            this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName.Substring(0, length);
                            iIndex++;
                            this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName.Substring(length);
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName;//info.Order.Item.Name;// +" " + info.Order.Item.Specs;//药品名称
                        }
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName;
                    }

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
                return 2;
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
                /*
                if (value)
                {
                    this.BackColor = Color.FromArgb( 224, 224, 224 );
                    //this.fpSpread1.BackColor = Color.FromArgb(224, 224, 224);
                    //for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                    //{
                    //    this.fpSpread1_Sheet1.Rows[i].BackColor = Color.FromArgb(224, 224, 224);
                    //}
                }
                else
                {
                    this.BackColor = Color.FromArgb( 255, 255, 255 );
                    //for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                    //{
                    //    this.fpSpread1_Sheet1.Rows[i].BackColor = Color.FromArgb(255, 255, 255);
                    //}
                    //this.fpSpread1.BackColor = Color.FromArgb(255, 255, 255);
                }
                 * */
                //if (value)
                //{
                //    this.BackColor = Color.FromArgb(224, 224, 224);

                //    foreach (Control c in this.Controls)
                //    {
                //        c.BackColor = Color.FromArgb(224, 224, 224);
                //    }

                //    for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                //    {
                //        this.fpSpread1_Sheet1.Rows[i].BackColor = Color.FromArgb(224, 224, 224);
                //        this.fpSpread1_Sheet1.GrayAreaBackColor = Color.FromArgb(224, 224, 224);
                //    }
                //}
                //else
                //{
                //    this.BackColor = Color.FromArgb(255, 255, 255);

                //    foreach (Control c in this.Controls)
                //    {
                //        c.BackColor = Color.FromArgb(255, 255, 255);
                //    }

                //    for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                //    {
                //        this.fpSpread1_Sheet1.Rows[i].BackColor = Color.FromArgb(255, 255, 255);
                //        this.fpSpread1_Sheet1.GrayAreaBackColor = Color.FromArgb(255, 255, 255);
                //    }
                //}
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
                this.IsSelected = true;
            }
            else
            {
                this.IsSelected = false;
            }
            return;
        }

    }
}
