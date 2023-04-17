using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.WinForms.Report.Order
{
    /// <summary>
    /// [��������: ��Һ��С�ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDrugTransfusionControl : UserControl,FS.FrameWork.WinForms.Classes.IControlPrintable
    {
        public ucDrugTransfusionControl()
        {
            InitializeComponent();
        }



        #region IControlPrintable ��Ա

        public bool IsCanExtend
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.IsCanExtend getter ʵ��
                return false;
            }
            set
            {
                // TODO:  ��� ucDrugCardInfo.IsCanExtend setter ʵ��
            }
        }

        public int BeginVerticalBlankHeight
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.BeginVerticalBlankHeight getter ʵ��
                return 1;
            }
            set
            {
                // TODO:  ��� ucDrugCardInfo.BeginVerticalBlankHeight setter ʵ��
            }
        }

        public int VerticalBlankHeight
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.VerticalBlankHeight getter ʵ��
                return 1;
            }
            set
            {
                // TODO:  ��� ucDrugCardInfo.VerticalBlankHeight setter ʵ��
            }
        }

        public int HorizontalBlankWidth
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.HorizontalBlankWidth getter ʵ��
                return 0;
            }
            set
            {
                // TODO:  ��� ucDrugCardInfo.HorizontalBlankWidth setter ʵ��
            }
        }

     

        protected int vnum = 0;
        protected int hnum = 0;
        public int HorizontalNum
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.HorizontalNum getter ʵ��
                return this.hnum;
            }
            set
            {
                this.hnum = value;
            }
        }

        public Size ControlSize
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.ControlSize getter ʵ��
                return new Size(331, 220);
            }
        }

        public bool IsShowGrid
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.IsShowGrid getter ʵ��
                return false;
            }
            set
            {
                // TODO:  ��� ucDrugCardInfo.IsShowGrid setter ʵ��
            }
        }

        /// <summary>
        /// �ؼ���ֵ
        /// </summary>
        public object ControlValue
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.ControlValue getter ʵ��
                return null;
            }
            set
            {
                // TODO:  ��� ucDrugCardInfo.ControlValue setter ʵ��
                ArrayList al = value as ArrayList;
                if (al == null) return;
                int intWidth = 0;
                intWidth = 130 / al.Count;
                if (intWidth > 20) intWidth = 20;

                #region {47D5BD74-2263-4275-9CF8-18DD973E31E7}
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManagemnt = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                bool isHavePccExecBill = controlManagemnt.GetControlParam<bool>("200211", false, false);
                //FS.HISFC.PC.MNS.Implement.OrderExcBill ppcExecBillMgr = null;
                if (isHavePccExecBill)
                {
                    //ppcExecBillMgr = new FS.HISFC.PC.MNS.Implement.OrderExcBill();
                }

                #endregion

                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Order.ExecOrder order = al[i] as FS.HISFC.Models.Order.ExecOrder;
                    if (order == null) 
                        return;
                    string hypoTest = "";
                    if (order.Order.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.Negative)
                        hypoTest = "[+]";
                    if (order.Order.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.Positive)
                        hypoTest = "[��]";

                    if (i == 0)
                    {
                        this.lblName.Text = order.Order.Patient.Name;
                        this.lblBed.Text = FS.HISFC.Components.Order.Classes.Function.BedDisplay(order.Order.Patient.PVisit.PatientLocation.Bed.ID);
                        this.lblFrequency.Text = order.Order.Frequency.ID;
                        this.lblUsage.Text = order.Order.Usage.Name;
                        this.lblUseTime.Text = order.DateUse.Month + "." + order.DateUse.Day;// +" " + order.DateUse.ToShortTimeString();
          
                    }
                    if (i == 0)
                    {
                        this.lblItem.Text = order.Order.DoseOnce.ToString() + order.Order.DoseUnit + order.Order.Item.Name + hypoTest + "[" + order.Order.Item.Specs + "]";

                        this.lblPage.Text = order.User01 + "/" + order.User02;//����ҳ��
                        if (FS.FrameWork.Function.NConvert.ToInt32(order.User01) > 1)//�ǵ�һҳû����ҩ
                            this.lblItem.Font = new Font(this.lblItem.Font.FontFamily, this.lblItem.Font.Size, System.Drawing.FontStyle.Regular);
                    }
                    else
                    {
                        Label lb = new Label();
                        lb.Location = new Point(0, this.lblItem.Top + this.lblItem.Height + (i - 1) * intWidth);
                        lb.Size = new Size(224, 16);
                        lb.Font = new Font(this.lblItem.Font.FontFamily, this.lblItem.Font.Size, System.Drawing.FontStyle.Regular);
                        lb.Text = order.Order.DoseOnce.ToString() + order.Order.DoseUnit + order.Order.Item.Name + hypoTest + "[" + order.Order.Item.Specs + "]";
                        lb.Visible = true;
                        this.Controls.Add(lb);
                    }

                    #region {47D5BD74-2263-4275-9CF8-18DD973E31E7}
                    if (isHavePccExecBill)
                    {
                        //string barcode = ppcExecBillMgr.GetBarCodeByExecSqn(order.ID);
                        //if (string.IsNullOrEmpty(barcode))
                        //{
                            //barcode = order.Order.Combo.ID + order.DateUse.ToString("yyMMddHHmm");
                        //}

                        //Common.Code39 code39 = new FS.WinForms.Report.Common.Code39();
                        //code39.ShowCodeString = true;
                        //Bitmap bitmap = code39.GenerateBarcode(barcode);
                        //PicBoxBarCode.Image = bitmap as Image;
                    }
                    #endregion
                }

            }
        }

        public int VerticalNum
        {
            set
            {
                this.vnum = value;
            }
            get
            {
                // TODO:  ��� ucDrugCardInfo.VerticalNum getter ʵ��
                return this.vnum;
            }
        }

        public int BeginHorizontalBlankWidth
        {
            get
            {
                // TODO:  ��� ucDrugCardInfo.BeginHorizontalBlankWidth getter ʵ��
                return 1;
            }
            set
            {
                // TODO:  ��� ucDrugCardInfo.BeginHorizontalBlankWidth setter ʵ��
            }
        }


        public ArrayList Components
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        #endregion
    }
}
