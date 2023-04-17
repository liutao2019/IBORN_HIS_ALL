using System;
using System.Collections.Generic;
using System.Linq;   //������ӵ�
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.InjectBillPrint
{
    /// <summary>
    /// ���Ƶ�
    /// </summary>
    public partial class ucInjectBillPrint2 : UserControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucInjectBillPrint2()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        private void Clear()
        {
            lblName.Text = "";
            lblSex.Text = "";
            //lblAge.Text = "";
            //lblCardNo.Text = "";
            lblSeeDept.Text = "";
            //labelPhoneAddr.Text = "";
            labelSeeDate.Text = "";
        }

        /// <summary>
        /// ���ô�ӡ
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            if (null == orderList || orderList.Count <= 0)
            {
                return;
            }

            int ig = 0;
            int iRow = 1;
            string str_hypotest = "";
            int ios = 1;
            this.fpSpreadItemsSheet.RowCount = 13;
            this.fpSpreadItemsSheet.ColumnCount = 7;
            this.fpSpreadItemsSheet.Columns[0].Width = 90;//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}��150����Ϊ100
            this.fpSpreadItemsSheet.Columns[1].Width = 20;
            this.fpSpreadItemsSheet.Columns[2].Width = 120;//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}��170����Ϊ150
            this.fpSpreadItemsSheet.Columns[3].Width = 140;//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}��170����Ϊ150
            this.fpSpreadItemsSheet.Columns[4].Width = 85;//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}��120����Ϊ90
            this.fpSpreadItemsSheet.Columns[5].Width = 85;//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}��120����Ϊ90
            this.fpSpreadItemsSheet.Columns[6].Width = 0;
            //this.fpSpreadItemsSheet.Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.fpSpreadItemsSheet.Columns[1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.fpSpreadItemsSheet.Columns[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.fpSpreadItemsSheet.Columns[3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.fpSpreadItemsSheet.Columns[4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpreadItemsSheet.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            string eName = "";
            orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>
                {
                    ig++;

                    g.OrderBy(s => s.SubCombNO)
                        .ToList().ForEach(order =>
                        {
                            iRow++;

                            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                eName = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).NameCollection.EnglishName;
                            }
                            else
                            {
                                eName = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).NameCollection.EnglishName;
                            }
                            if (string.IsNullOrEmpty(eName))
                            {
                                eName = order.Item.Name;
                            }
                            this.fpSpreadItemsSheet.Cells[iRow, 2].ColumnSpan = 2;//+ outOrderMgr.TransHypotest(order.HypoTest) 
                            this.fpSpreadItemsSheet.Cells[iRow, 2].Text = eName + (order.IsEmergency ? "������" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "����ע:" + order.Memo + "��") + "  " + order.DoseOnce.ToString() + order.DoseUnit + "  " + order.Usage.Name;
                            //this.fpSpreadItemsSheet.Rows[iRow].Height = 40f;
                            this.fpSpreadItemsSheet.Rows[iRow].Height = this.fpSpreadItemsSheet.GetPreferredRowHeight(iRow,false)*0.7f;
                            //��̬�����е�����������{C9A02C2D-E0D2-4777-8775-25C615A18BB8}
                            if (order.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                //if (GJLocal.HISFC.Components.OpGuide.OutPatient.Function.GetItemIsAllergy(order.Item.ID))
                                if(!(order.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest
                                || order.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest))
                                {
                                    str_hypotest += eName +"\n";
                                    ios += 1;
                                }
                            }

                            //���
                            //this.fpSpreadItemsSheet.Cells[iRow, 0].Text = order.SubCombNO.ToString();
                            this.fpSpreadItemsSheet.Cells[iRow, 6].Text = order.SubCombNO.ToString();
                            //����
                            //this.fpSpreadItemsSheet.Cells[iRow, 1].Text = order.Item.Name + outOrderMgr.TransHypotest(order.HypoTest) + (order.IsEmergency ? "������" : "") + (string.IsNullOrEmpty(order.Memo) ? "" : "����ע:" + order.Memo + "��");

                            //����
                            //this.fpSpreadItemsSheet.Cells[iRow, 2].Text = order.DoseOnce.ToString() + order.DoseUnit;

                            //�÷�
                            //this.fpSpreadItemsSheet.Cells[iRow, 3].Text = order.Usage.Name;

                            //����
                            //this.fpSpreadItemsSheet.Cells[iRow, 4].Text = string.Format("{0}��{1}", order.Frequency.ID, order.HerbalQty);
                        });
                });
            //if (iRow < fpSpreadItemsSheet.RowCount - 1)
            //{
            //    fpSpreadItemsSheet.Cells[iRow + 1, 1].Text = "����Ϊ��";
            //}
            GJLocal.HISFC.Components.OpGuide.OutPatient.Classes.Function.DrawComboLeft(fpSpreadItemsSheet, 6, 1);
            this.lblSeeDept.Text = orderList[0].ReciptDept.Name;//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}ȡ��ע��
            this.labelSeeDate.Text = orderList[0].MOTime.Date.ToString("yyyy.MM.dd");//{CF366E13-90F2-42cb-80AF-8BA41E93D3D8}ȡ��ע��
            //this.lblPhaDoc.Text = orderList[0].ReciptDoctor.ID + "(" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(orderList[0].ReciptDoctor.ID) + ")";
            //this.labelCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "Ԫ";
            //{BE53AD4A-F4BE-4d6a-8E82-F7F6718D6691}

            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            textCellType2.WordWrap = true;
            textCellType2.Multiline = true;
            this.fpSpreadItemsSheet.Cells[0, 0].ColumnSpan = 4;
            this.fpSpreadItemsSheet.Columns[0].CellType = textCellType2;
            this.fpSpreadItemsSheet.Cells[0, 0].Text = "��ҪƤ�Ե�ҩƷ/Sensitivity Test Required:" + "\n" + str_hypotest;
            this.fpSpreadItems.ActiveSheet.Rows[0].Height = 20f*ios;
            this.fpSpreadItemsSheet.Cells[1, 0].ColumnSpan = 4;
            this.fpSpreadItemsSheet.Cells[1, 0].Text = "Ƥ�Խ��/Sensitivity Test Required(      )  ";
            this.fpSpreadItemsSheet.Cells[0, 4].Text = "ִ��ʱ��";
            this.fpSpreadItemsSheet.Cells[0, 5].Text = "ǩ��";
            if (iRow < 11) { iRow = 11; }
            int num = iRow - 2;
            this.fpSpreadItemsSheet.Cells[2, 0].RowSpan = num;
            this.fpSpreadItemsSheet.Cells[2, 0].Text = "ҽ������(ҩ�����������÷�)Prescripts";
            this.fpSpreadItemsSheet.Cells[11, 0].ColumnSpan = 3;
            this.fpSpreadItemsSheet.Cells[11, 0].Text = "ҽʦ/M.D:" + orderList[0].ReciptDoctor.Name;
            this.fpSpreadItemsSheet.Cells.Get(11, 0).Border = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame);

            this.fpSpreadItemsSheet.Cells[12, 0].ColumnSpan = 3;
            this.fpSpreadItemsSheet.Cells[12, 0].Text = "ҩʦ/Pharmacist:       ";
            this.fpSpreadItemsSheet.Cells.Get(12, 0).Border = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame);

            //this.fpSpreadItemsSheet.AddSpanCell(11, 2, 12, 4);
            this.fpSpreadItemsSheet.Cells[11, 3].ColumnSpan = 3;
            this.fpSpreadItemsSheet.Cells[11, 3].RowSpan = 2;
            this.fpSpreadItemsSheet.Cells[11, 3].Text = "ҩ��/Medicine Fee��\n" + "USD               " + "  RMB " + FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "Ԫ";
            this.fpSpreadItemsSheet.Cells.Get(11, 3).Border = new FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame);

            

            if (!isPreview)
            {
                this.PrintPage();
            }
        }

        /// <summary>
        /// ���û��߻�����Ϣ
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
                return;
            FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo patientInfo = radtManager.QueryComPatientInfo(register.PID.CardNO);
            this.lblnation.Text = patientInfo.Nationality.ID;
            this.lblName.Text = register.Name;
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList al1 = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            foreach (FS.HISFC.Models.Base.Const con in al1)
            {
                if (con.ID == patientInfo.Country.ID)
                {
                    this.lblnation.Text = con.Name;
                }
            }
            //this.lblAge.Text = this.outOrderMgr.GetAge(register.Birthday, false);
            this.lblBirthday.Text = register.Birthday.ToShortDateString();
            this.lblSex.Text = register.Sex.Name;
            //if (register.Sex.Name.Equals("��"))
            //{
            //    this.lblSex.Text = "��/M";
            //}
            //else {
            //    this.lblSex.Text = "Ů/F";
            //}
            //this.lblSex.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            //lblPrintDate.Text = outOrderMgr.GetDateTimeFromSysDateTime().ToString();

            //this.labelPhoneAddr.Text = register.AddressHome;

            //his.npbBarCode.Image = FS.SOC.Public.Function.CreateBarCode(register.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="judPrint">����OR����</param>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize pSize = FS.SOC.Local.Order.ZhuHai.ZDWY.Function.GetPrintPage(false);
            print.SetPageSize(pSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            fpSpreadItemsSheet.PrintInfo.ShowBorder = false;
            //fpSpreadFootSheet.PrintInfo.ShowBorder = false;

            if (FS.SOC.Local.Order.ZhuHai.ZDWY.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        #region IOutPatientOrderPrint ��Ա

        /// <summary>
        /// ҽ��������ĺ�������
        /// </summary>
        /// <param name="regObj">�Һ�ʵ��</param>
        /// <param name="reciptDept">��������</param>
        /// <param name="reciptDoct">����ҽ��</param>
        /// <param name="IList">ҽ���б�</param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            this.Clear();

            this.SetPatientInfo(regObj);
            this.SetPrintValue(orderList, isPreview);

            return 1;
        }

        /// <summary>
        /// ���ﴦ��������ĺ�������
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            List<FS.HISFC.Models.Order.OutPatient.Order> orderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                orderList.Add(order);
            }

            this.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);
            return 1;
        }

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);
        }


        public void SetPage(string pageStr)
        {
        }

        #endregion
    }
}
