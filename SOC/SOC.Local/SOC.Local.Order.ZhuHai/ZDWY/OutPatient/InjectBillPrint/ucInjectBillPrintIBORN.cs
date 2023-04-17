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
    public partial class ucInjectBillPrintIBORN : UserControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucInjectBillPrintIBORN()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// ��ӡ������Ŀ�����Ƿ���ͨ������1 ͨ������0 ��Ʒ��
        /// </summary>
        private int printItemNameType = -1;

        private void Clear()
        {
            lblName.Text = "";
            lblSex.Text = "";
            lblAge.Text = "";
            lblCardNo.Text = "";
            lblSeeDept.Text = "";
            labelPhoneAddr.Text = "";
            labelSeeDate.Text = "";
        }

        /// <summary>
        /// ���ô�ӡ
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            //this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name ;
            // {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB} {92F1E785-D681-4a0d-8806-8A330BED5228}
            FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
            currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            if (!string.IsNullOrEmpty(currDept.HospitalName))
            {
                this.labelTitle.Text = currDept.HospitalName;
            }
            else
            {
                this.labelTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            }
             
            if (null == orderList || orderList.Count <= 0)
            {
                return;
            }

            int ig = 0;
            int iRow = -1;
            string show = "";
            string strDoseOnce = "";
            
            bool isCombo = false;
            orderList.GroupBy(o => o.Combo.ID).ToList().ForEach(g =>
                {


                    g.OrderBy(s => s.SubCombNO)
                        .ToList().ForEach(order =>
                        {
                            iRow++;
                            //���

                            ig++;
                            string showName = GetName(order, false);
                            isCombo = false;
                            if (GetCombCount(orderList, order) > 1)
                            {
                                showName = GetName(order, true);
                                isCombo = true;
                            }
                            this.fpSpreadItemsSheet.Cells[iRow, 0].Text = ig + ".";
                            
                            this.fpSpreadItemsSheet.Cells[iRow, 2].Text = order.Combo.ID;
                            //����

                            this.fpSpreadItemsSheet.Cells[iRow, 4].Text = showName;
                            //����
                            //this.fpSpreadItemsSheet.Cells[iRow, 4].Text = string.Format("{0}��{1}", order.Frequency.ID, order.HerbalQty);
                            if (isCombo)
                            {
                                string freName = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(order.Frequency.ID);

                                strDoseOnce = "ÿ��" + order.DoseOnce.ToString() + order.DoseUnit + " ";
                                show = "�÷���" + order.Usage.Name + "," + freName + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "��");

                            }
                            else
                            {
                                string freName = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(order.Frequency.ID);

                                strDoseOnce = "ÿ��" + order.DoseOnce.ToString() + order.DoseUnit + " ";
                                show = "�÷���" + order.Usage.Name + "," + freName + "," + strDoseOnce + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "��");

                            }
                        }
                        );
                        iRow++;
                        this.fpSpreadItemsSheet.Cells[iRow, 4].Text = show;
                        this.fpSpreadItemsSheet.Cells[iRow, 4].Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular);

                });
            if (iRow < fpSpreadItemsSheet.RowCount - 1)
            {
                fpSpreadItemsSheet.Cells[iRow + 1, 4].Text = "(����Ϊ��)";
            }
            ZhuHai.Classes.Function.DrawComboLeft(fpSpreadItemsSheet, 2, 1);
            
            this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(orderList[0].ReciptNO, this.npbRecipeNo.Width, this.npbRecipeNo.Height);

            this.lblSeeDept.Text = orderList[0].ReciptDept.Name;
            this.labelSeeDate.Text = orderList[0].MOTime.ToString();
            this.lblPhaDoc.Text = orderList[0].ReciptDoctor.ID.Substring(2, orderList[0].ReciptDoctor.ID.Length - 2);
            this.labelCost.Text = FS.FrameWork.Public.String.ToSimpleString(orderList.Sum(x => x.FT.OwnCost + x.FT.PubCost + x.FT.PayCost)) + "Ԫ";

            if (!isPreview)
            {
                this.PrintPage();
            }
        }

        public int LengthString(string str)
        {
            if (str == null || str.Length == 0) { return 0; }

            int l = str.Length;
            int realLen = l;

            #region ���㳤��
            int clen = 0;//��ǰ����
            while (clen < l)
            {
                //ÿ����һ�����ģ���ʵ�ʳ��ȼ�һ��
                if ((int)str[clen] > 128) { realLen++; }
                clen++;
            }
            #endregion

            return realLen;
        }
        private void AddUsageShow(FS.HISFC.Models.Order.OutPatient.Order order, int row, bool isComb)
        {
            //ÿ����+�÷�+Ƶ��+��ע
            string show = "";

            string strDoseOnce = "";

            //����ҩ����ʾÿ������
            if (//(order.Item.ID != "999"
                //&& SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).SpecialFlag2 == "1")
                //|| 
                isComb)
            {
            }
            else
            {
                strDoseOnce = "ÿ��" + order.DoseOnce.ToString() + order.DoseUnit + " ";
            }

            string freName = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(order.Frequency.ID);

            show = "�÷���" + order.Usage.Name + "," + freName + "," + strDoseOnce + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "��");

            //if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
            //{
            //    show += " ��Һ��ȥ";
            //}
            fpSpreadItemsSheet.Cells[row, 4].Text = show;
        }
        private int GetCombCount(IList<FS.HISFC.Models.Order.OutPatient.Order> alOrder, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            int count = 0;
            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alOrder)
            {
                if (order.Combo.ID == ord.Combo.ID)
                {
                    count += 1;
                }
            }
            return count;
        }
        private string GetName(FS.HISFC.Models.Order.OutPatient.Order order, bool isShowDoseOnce)
        {
            string itemName = "";
            if (printItemNameType == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
            }

            string strDoseOnce = isShowDoseOnce ? ("  /ÿ��" + order.DoseOnce.ToString() + order.DoseUnit) : "";

            if (order.Item.ID == "999" && !itemName.Contains("�Ա�"))
            {
                itemName = order.Item.Name + "[�Ա�]"+(order.IsEmergency ? "������" : "");
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

                if (printItemNameType == 0)
                {
                    itemName = order.Item.Name + (order.IsEmergency ? "������" : "");
                }
                else
                {
                    if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                    {
                        itemName = phaItem.Name + (order.IsEmergency ? "������" : "");
                    }
                    else
                    {
                        itemName = phaItem.NameCollection.RegularName + (order.IsEmergency ? "������" : "");
                    }
                }

                //if (order.Unit == phaItem.MinUnit)
                //{
                //    itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "/" + phaItem.MinUnit + "��" + order.Qty.ToString() + order.Unit;
                //}
                //else
                {
                    //itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "*" + phaItem.PackQty.ToString() + phaItem.MinUnit + "/" + phaItem.PackUnit + "     ��" + order.Qty.ToString() + order.Unit;
                    itemName += "    " + phaItem.Specs + "     ��" + order.Qty.ToString() + order.Unit;
                }
            }

            //��ע
            //itemName += (string.IsNullOrEmpty(outOrder.Memo) ? "" : " ��" + outOrder.Memo + "��");

            return itemName + strDoseOnce;
        }
        /// <summary>
        /// ���û��߻�����Ϣ
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
                return;

            this.lblName.Text = register.Name;
            this.lblName1.Text = register.Name;

            int strLength = this.LengthString(register.Name);
            if (strLength > 16)
            {
                this.lblName.Visible = true;
                this.lblName1.Visible = false;
            }
            else
            {

                this.lblName.Visible = false;
                this.lblName1.Visible = true;
            }
            this.lblAge.Text = this.outOrderMgr.GetAge(register.Birthday, false);
            this.lblSex.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO;
            lblPrintDate.Text = outOrderMgr.GetDateTimeFromSysDateTime().ToString();

            this.labelPhoneAddr.Text = register.AddressHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.PhoneHome;
            FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            accountCard = accountManager.GetAccountCardForOne(register.PID.CardNO);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            /*
            if (accountCard != null)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = managerIntegrate.GetConstansObj("MemCardType", accountCard.AccountLevel.ID);

                this.lblAccountType.Text = obj.Name;
            }
            else
            {
                this.lblAccountType.Text = "";
            }
             * */
            //{3A2E38C1-3A99-45a3-82BD-0A1055298E69}
            this.lblAccountType.Text = register.Pact.Name;


            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                return;
            }

            string strDiag = "";
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "��";
                }
            }
            this.lblDiag.Text = strDiag.TrimEnd('��');

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
            fpSpreadFootSheet.PrintInfo.ShowBorder = false;

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
