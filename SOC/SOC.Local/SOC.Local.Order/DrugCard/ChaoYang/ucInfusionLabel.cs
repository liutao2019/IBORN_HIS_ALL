using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Order.DrugCard.ChaoYang
{
    /// <summary>
    /// <br>[功能描述: 朝阳住院瓶贴实现]</br>
    /// <br>[修 改 者: SNIPER]</br>
    /// <br>[创建时间: 2011-12-15]</br>
    /// <br>基于张槎医院的贴瓶单实现</br>
    /// <br>[遗留问题:瓶签已实现选择打印功能,但是更新打印状态时不能保留未选择打印的未打印状态]</br>
    /// </summary>
    public partial class ucInfusionLabel : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Classes.IControlPrintable
    {
        public ucInfusionLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        private System.Collections.Hashtable hsPatientInfo = new Hashtable();
        Neusoft.HISFC.Models.RADT.PatientInfo patientnifo = new Neusoft.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 业务管理类
        /// 通过Patient.ID获取患者对象
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 为获取皮试信息
        /// </summary>
        Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();

        #region IControlPrintable 成员

        protected int vnum = 0;
        protected int hnum = 0;

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
                return new Size(262, 200);
            }
        }
     
        /// <summary>
        /// 这里是怎么调用的不清楚
        /// Value是如何进来的不清楚
        /// 原版未作表明,无法细致修改
        /// 2011-12-15  SNIPER
        /// </summary>
        public object ControlValue
        {
            get
            {
                //在无法实现请求的方法或操作时引发的异常_MSDN
                throw new NotImplementedException();
            }
            set
            {
                ArrayList alInfusionData = value as ArrayList;
                if (alInfusionData == null)
                {
                    return;
                }

                #region 标签信息

                int iIndex = 1;
                int length = "我是淫荡邪恶猥琐的长度标咔咔又加长了".Length; //14+4

                // {2F204B7C-61F4-4f0c-9696-E4183CEBBF0C}
                // FP 设定打印8行，当组合超过4项时，且项目名称长度都超过 length 时，程序报错
                // 处理方法： 当组合超过4项时，不进行换行处理；超过8项时，只处理前8项
                bool blnLineFeeds = true;
                if (alInfusionData.Count >= 4)
                {
                    blnLineFeeds = false;
                }

                foreach (Neusoft.HISFC.Models.Order.ExecOrder info in alInfusionData)
                {
                    #region 设置患者信息

                    if (this.hsPatientInfo.ContainsKey(info.Order.Patient.ID))
                    {
                        patientnifo = this.hsPatientInfo[info.Order.Patient.ID] as Neusoft.HISFC.Models.RADT.PatientInfo;
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

                        this.lblFrequency.Text= info.Order.Frequency.ID;
                        //this.lbName.Text = patientnifo.Name;//此处取消_sniper
                          this.lbBed.Text += patientnifo.Name;
                    }

                    #endregion 设置患者信息

                    #region 设置用药信息

                    this.lbDate.Text = info.DateUse.ToString("yy.MM.dd");     //用药时间
                    this.lbUsage.Text = info.Order.Usage.Name;
                    this.lbCombo.Text = "(" + info.Order.Combo.ID + ")";

                    if (iIndex == 0)
                    {
                        #region none
                        //this.lbLabelTotNum.Text = info.Operation.User01;
                        //if (info.Operation.User03 == "-1")
                        //{
                        //    this.lbiCount.Text = info.Operation.User02;
                        //}
                        //else
                        //{
                        //    this.lbiCount.Text = info.Operation.User02 + "(" + info.Operation.User03 + ")";
                        //}
                        #endregion
                    }

                    #endregion 设置用药信息



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
                    #endregion 皮试信息

                    #region 药品信息

                    // {2F204B7C-61F4-4f0c-9696-E4183CEBBF0C}
                    if (iIndex >= 8)
                    {
                        break;
                    }


                    //用量 
                    this.fpSpread1_Sheet1.Cells[iIndex, 2].Text = info.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Order.DoseUnit.ToString();
                    //this.fpSpread1_Sheet1.Cells[iIndex, 3].Text = info.Order.Frequency.ID;
                    string itemName = "";

                    if (info.Order.Memo==null|| info.Order.Memo == "")
                    {
                        itemName = info.Order.Item.Name;
                    }
                    else
                    {
                        // +" " + info.Order.Item.Specs;
                        itemName = info.Order.Item.Name + " " + "备注：" + info.Order.Memo;
                    }
                    //因为存在列的分行 iIndex+1
                    //{2F204B7C-61F4-4f0c-9696-E4183CEBBF0C}
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
                            // +" " + info.Order.Item.Specs;
                            //药品名称
                            this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = info.Order.Item.Name;
                        }
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName;
                    }

                    iIndex++;
                    #endregion  药品信息
                }

                this.checkBox1.Checked = true;
                this.Tag = alInfusionData;

                #endregion 标签信息
            }
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
                return this.bSelected;
            }
            set
            {
                this.bSelected = value;
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
            }
        }

        protected override void OnClick(EventArgs e)
        {
            return; //2011-12-16 sniper 
            this.IsSelected = !this.IsSelected;
            base.OnClick( e );
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           // return; //这是咩意思？不让选择打印？2012-12-16 sniper
            if (this.checkBox1.Checked)
            {
                this.IsSelected = true;
            }
            else
            {
                this.IsSelected = false;
            }
        }

 

    }
}
