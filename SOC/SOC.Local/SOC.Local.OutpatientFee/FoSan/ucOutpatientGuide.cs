using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using FS.HISFC.BizProcess.Interface.Fee;
using System.IO.Ports;
using System.Xml;
using System.IO;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.FoSan
{
	/// <summary>
	/// ucFeeDetail ��ժҪ˵����
	/// </summary>
    public class ucOutpatientGuide : System.Windows.Forms.UserControl, IOutpatientGuide
    {
        private FS.FrameWork.WinForms.Controls.NeuLabel neulblOrder;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ucOutpatientGuide()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

            lstSysClass = new List<string>();

            lstSysClass.AddRange(new string[] { "UL", "UC", "UZ", "UO", "P", "PCZ", "PCC" });

            lstUnDetialClass.AddRange(new string[] { "P", "PCZ", "PCC" });
		}

		/// <summary> 
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.neulblOrder = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.SuspendLayout();
            // 
            // neulblOrder
            // 
            this.neulblOrder.AutoSize = true;
            this.neulblOrder.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neulblOrder.Location = new System.Drawing.Point(23, 17);
            this.neulblOrder.Name = "neulblOrder";
            this.neulblOrder.Size = new System.Drawing.Size(0, 16);
            this.neulblOrder.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neulblOrder.TabIndex = 14;
            // 
            // ucOutpatientGuide
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.neulblOrder);
            this.Name = "ucOutpatientGuide";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(472, 394);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ��ӡֽ��������
        /// </summary>
        FS.HISFC.Models.Registration.Register rInfo = new FS.HISFC.Models.Registration.Register();
        FS.HISFC.Models.Fee.Outpatient.Balance invoiceInfo = new FS.HISFC.Models.Fee.Outpatient.Balance();
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
		ArrayList invoices = new ArrayList();
		ArrayList feeDetails = new ArrayList();

		string operCode = "";
		int count = 0;
		decimal totCost = 0;

        int pageNum = 1;

        /// <summary>
        /// ָ��������ϵͳ���
        /// 
        /// ���ɳ���ָ����
        /// ���������ö�ȡ
        /// </summary>
        List<string> lstSysClass = new List<string>();
        List<string> lstUnDetialClass = new List<string>();
		
		public FS.HISFC.Models.Registration.Register RInfo 
		{
			set
			{
				this.rInfo = value;	
			}
		}

        //public void SetDisplay()
        //{
        //    this.label1.Text = this.managerIntegrate.GetHospitalName();

        //    this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 6);
        //    this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("����", 9,System.Drawing.FontStyle.Bold);
        //    this.fpSpread1_Sheet1.Cells[count,0].Text = "����:" + rInfo.Name + " �Һ�����:" + rInfo.DoctorInfo.SeeDate.ToShortDateString();
        //    count ++;
        //    for(int i = 0; i < this.invoices.Count; i++)
        //    {
        //        FS.HISFC.Models.Fee.Outpatient.Balance invoice = invoices[i] as FS.HISFC.Models.Fee.Outpatient.Balance;

        //        if(invoice.Memo == "5")
        //        {
        //            continue;
        //        }
        //        this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 6);
        //        this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("����", 9,System.Drawing.FontStyle.Bold);
        //        this.fpSpread1_Sheet1.Cells[count,0].Text = "������:" + rInfo.PID.CardNO + " ��Ʊ��:" + invoice.Invoice.ID;
				
        //        count ++;
        //    }
        //    if(feeDetails == null)
        //    {
        //        return;
        //    }

        //    string strTemp = "";
        //    List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> lstItem = null;
        //    Dictionary<string, List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>> dicExcuAddr = new Dictionary<string, List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>>();

        //    foreach (string strSysClass in lstSysClass)
        //    {                
        //        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in feeDetails)
        //        {
        //            if (feeItem.Item.IsMaterial)
        //            {
        //                continue;
        //            }
        //            if (feeItem.Item.SysClass.ID.ToString() != strSysClass)
        //            {
        //                continue;
        //            }

        //            strTemp = feeItem.ExecOper.Dept.Name;
        //            if (dicExcuAddr.ContainsKey(strTemp))
        //            {
        //                lstItem = dicExcuAddr[strTemp];
        //                if (lstItem == null)
        //                {
        //                    lstItem = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
        //                    lstItem.Add(feeItem);
        //                    dicExcuAddr[strTemp] = lstItem;
        //                }
        //                else
        //                {
        //                    lstItem.Add(feeItem);
        //                    dicExcuAddr[strTemp] = lstItem;
        //                }
        //            }
        //            else
        //            {
        //                lstItem = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
        //                lstItem.Add(feeItem);
        //                dicExcuAddr.Add(strTemp, lstItem);
        //            }
        //        }
        //    }

        //    if (dicExcuAddr.Count <= 0)
        //    {
        //        return;
        //    }

        //    foreach (string excuDept in dicExcuAddr.Keys)
        //    {
        //        lstItem = dicExcuAddr[excuDept];
        //        if (lstItem == null || lstItem.Count <= 0)
        //            continue;

        //        count++;

        //        this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 6);
        //        this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("����", 11, System.Drawing.FontStyle.Bold);
        //        this.fpSpread1_Sheet1.Cells[count, 0].Text = "ִ�п��ң�" + excuDept;

        //        count++;

        //        this.fpSpread1_Sheet1.Cells[count, 0].Text = "��Ŀ����";
        //        this.fpSpread1_Sheet1.Cells[count, 2].Text = "���";
        //        this.fpSpread1_Sheet1.Cells[count, 4].Text = "����";
        //        this.fpSpread1_Sheet1.Cells[count, 5].Text = "���";
        //        count++;

        //        FS.HISFC.Models.Fee.Outpatient.FeeItemList itemTemp = null;
        //        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in lstItem)
        //        {
        //            if (itemTemp != null)
        //            {
        //                if (!string.IsNullOrEmpty(item.UndrugComb.Name))
        //                {
        //                    if (itemTemp.UndrugComb.Name == item.UndrugComb.Name)
        //                        continue;
        //                }
        //            }
        //            itemTemp = item;

        //            this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 2);
        //            this.fpSpread1_Sheet1.Cells[count, 0].Text = string.IsNullOrEmpty(item.UndrugComb.Name) ? item.Item.Name : item.UndrugComb.Name;

        //            this.fpSpread1_Sheet1.Models.Span.Add(count, 2, 1, 2);
        //            this.fpSpread1_Sheet1.Cells[count, 2].Text = item.Item.Specs;
        //            this.fpSpread1_Sheet1.Cells[count, 2].Font = new Font("����", 9);
        //            if (item.FeePack == "1")//��װ��λ
        //            {
        //                this.fpSpread1_Sheet1.Cells[count, 4].Text = (item.Item.Qty/item.Item.PackQty).ToString();
        //            }
        //            else
        //            {
        //                this.fpSpread1_Sheet1.Cells[count, 4].Text = item.Item.Qty.ToString();
        //            }

        //            this.fpSpread1_Sheet1.Cells[count, 5].Text = item.FT.TotCost.ToString();

        //            count++;
        //        }
        //    }

        //    count++;

        //    this.fpSpread1_Sheet1.RowCount = count;

        //}

        List<string> lstPrint = new List<string>();
        public void SetPrintValue()
        {
            lstPrint.Clear();

            string strTemp = this.managerIntegrate.GetHospitalName();
            strTemp = strTemp.PadLeft(20, ' ');
            lstPrint.Add(strTemp);
            strTemp = "�����շ�ָ����";
            strTemp = strTemp.PadLeft(20, ' ');
            lstPrint.Add(strTemp);

            // ������Ϣ
            lstPrint.Add(" ");
            strTemp = "������" + rInfo.Name + "    �Һ����ڣ�" + rInfo.DoctorInfo.SeeDate.ToShortDateString();
            lstPrint.Add(strTemp);

            // ��Ʊ��Ϣ
            FS.HISFC.Models.Fee.Outpatient.Balance invoice = invoices[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
            strTemp = "�����ţ�" + rInfo.PID.CardNO + "   ��Ʊ�ţ�" + invoice.Invoice.ID;
            lstPrint.Add(strTemp);

            // ������Ϣ
            List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> lstItem = null;
            Dictionary<string, List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>> dicExcuAddr = new Dictionary<string, List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>>();

            foreach (string strSysClass in lstSysClass)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in feeDetails)
                {
                    if (feeItem.Item.IsMaterial)
                    {
                        continue;
                    }
                    if (feeItem.Item.SysClass.ID.ToString() != strSysClass)
                    {
                        continue;
                    }

                    strTemp = feeItem.ExecOper.Dept.Name;
                    if (dicExcuAddr.ContainsKey(strTemp))
                    {
                        lstItem = dicExcuAddr[strTemp];
                        if (lstItem == null)
                        {
                            lstItem = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
                            lstItem.Add(feeItem);
                            dicExcuAddr[strTemp] = lstItem;
                        }
                        else
                        {
                            lstItem.Add(feeItem);
                            dicExcuAddr[strTemp] = lstItem;
                        }
                    }
                    else
                    {
                        lstItem = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
                        lstItem.Add(feeItem);
                        dicExcuAddr.Add(strTemp, lstItem);
                    }
                }
            }

            if (dicExcuAddr.Count <= 0)
            {
                return;
            }

            Dictionary<string, string> deptTicp = new Dictionary<string, string>();
            foreach (string excuDept in dicExcuAddr.Keys)
            {
                lstItem = dicExcuAddr[excuDept];
                if (lstItem == null || lstItem.Count <= 0)
                    continue;

                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in lstItem)
                {
                    if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(item.Order.Usage.ID))
                    {
                        if (!deptTicp.ContainsKey(excuDept))
                        {
                            deptTicp.Add(excuDept, "�뵽 �� ע���� �� ���� ");
                        }
                    }
                    else if (lstUnDetialClass.Contains(item.Item.SysClass.ID.ToString()))
                    {
                        if (!deptTicp.ContainsKey(excuDept))
                        {
                            deptTicp.Add(excuDept, "�뵽 ��" + excuDept + "�� ȡҩ ");
                        }
                    }
                }
            }



            foreach (string excuDept in dicExcuAddr.Keys)
            {
                lstItem = dicExcuAddr[excuDept];
                if (lstItem == null || lstItem.Count <= 0)
                    continue;

                strTemp = "------------------------------------------";
                lstPrint.Add(strTemp);

                if (deptTicp.ContainsKey(excuDept))
                {
                    strTemp = "ִ�п��ң�" + deptTicp[excuDept];
                    lstPrint.Add(strTemp);
                    continue;
                }



                strTemp = "ִ�п��ң���" + excuDept + "��";
                lstPrint.Add(strTemp);

                

                strTemp = "��Ŀ����      " + "    ����    ";
                lstPrint.Add(strTemp);

                string str = "";
                //FS.HISFC.Models.Fee.Outpatient.FeeItemList itemTemp = null;
                Hashtable hsCombName = new Hashtable();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in lstItem)
                {
                    //if (itemTemp != null)
                    //{
                    //    if (!string.IsNullOrEmpty(item.UndrugComb.Name))
                    //    {
                    //        if (itemTemp.UndrugComb.Name == item.UndrugComb.Name)
                    //            continue;
                    //    }
                    //}
                    if (!string.IsNullOrEmpty(item.UndrugComb.Name))
                    {
                        if (hsCombName.Contains(item.UndrugComb.Name))
                        {
                            continue;
                        }
                        else
                        {
                            hsCombName.Add(item.UndrugComb.Name,item);
                        }
                    }
                    //itemTemp = item;

                    // ��Ŀ����
                    str = string.IsNullOrEmpty(item.UndrugComb.Name) ? item.Item.Name : item.UndrugComb.Name;
                    str = str.PadRight(15, ' ');
                    strTemp = str;

                    // ����
                    if (item.FeePack == "1")//��װ��λ
                    {
                        str = (item.Item.Qty / item.Item.PackQty).ToString();
                    }
                    else
                    {
                        str = item.Item.Qty.ToString();
                    }
                    str += item.Item.PriceUnit;
                    strTemp += str;

                    lstPrint.Add(strTemp);
                }
            }
            strTemp = "------------------------------------------";
            lstPrint.Add(strTemp);
            lstPrint.Add(" ");
            String temp = "";
            Decimal i = 0;
            foreach (string str in lstPrint)
            {
                temp+= str  +"\r\n";
                i++;
            }
            pageNum = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(i / 17).ToString());
            this.neulblOrder.Text = temp;
        }

        #region IOutpatientGuide ��Ա

        public void Print()
        {

            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize pSize = null;
            //pSize = pageSizeMgr.GetPageSize("MZZYD");
            int height = 0;
            height = 375 * pageNum;
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("MZZYD", 600, height);
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = "MZZYD";
            }
   

            print.SetPageSize(pSize);
            print.PrintPage(0, 0, this);

        }

        public void SetValue(FS.HISFC.Models.Registration.Register rInfo, ArrayList invoices, ArrayList feeDetails)
        {
            this.rInfo = rInfo;
            this.invoices = invoices;
            this.feeDetails = feeDetails;

            //this.SetDisplay();

            SetPrintValue();
        }

        #endregion


    }
}
