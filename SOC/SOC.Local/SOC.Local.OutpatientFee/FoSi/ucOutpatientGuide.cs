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

namespace FS.SOC.Local.OutpatientFee.FoSi
{
	/// <summary>
	/// ucFeeDetail µÄÕªÒªËµÃ÷¡£
	/// </summary>
    public class ucOutpatientGuide : System.Windows.Forms.UserControl, IOutpatientGuide
	{
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary> 
		/// ±ØÐèµÄÉè¼ÆÆ÷±äÁ¿¡£
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ucOutpatientGuide()
		{
			// ¸Ãµ÷ÓÃÊÇ Windows.Forms ´°ÌåÉè¼ÆÆ÷Ëù±ØÐèµÄ¡£
			InitializeComponent();

			// TODO: ÔÚ InitializeComponent µ÷ÓÃºóÌí¼ÓÈÎºÎ³õÊ¼»¯

            lstSysClass = new List<string>();

            lstSysClass.AddRange(new string[] { "UL", "UC", "UZ", "UO", "P", "PCZ", "PCC" });

            lstUnDetialClass.AddRange(new string[] { "P", "PCZ", "PCC" });
		}

		/// <summary> 
		/// ÇåÀíËùÓÐÕýÔÚÊ¹ÓÃµÄ×ÊÔ´¡£
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

		#region ×é¼þÉè¼ÆÆ÷Éú³ÉµÄ´úÂë
		/// <summary> 
		/// Éè¼ÆÆ÷Ö§³ÖËùÐèµÄ·½·¨ - ²»ÒªÊ¹ÓÃ´úÂë±à¼­Æ÷ 
		/// ÐÞ¸Ä´Ë·½·¨µÄÄÚÈÝ¡£
		/// </summary>
		private void InitializeComponent()
		{
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpSpread1.Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(3, 40);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(349, 352);
            this.fpSpread1.TabIndex = 11;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance2;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 6;
            this.fpSpread1_Sheet1.ColumnHeader.RowCount = 0;
            this.fpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("ËÎÌå", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 140F;
            this.fpSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 63F;
            this.fpSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 37F;
            this.fpSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 29F;
            this.fpSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 55F;
            this.fpSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("ËÎÌå", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 65F;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.SheetCornerHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpSpread1_Sheet1.SheetCornerVerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("ËÎÌå", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(100, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "**Ò½Ôº";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("ËÎÌå", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(110, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "ÃÅÕïÊÕ·ÑÖ¸Òýµ¥";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucFeeDetail
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fpSpread1);
            this.Name = "ucFeeDetail";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(360, 399);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ´òÓ¡Ö½ÕÅÉèÖÃÀà
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Registration.Register rInfo = new FS.HISFC.Models.Registration.Register();
        FS.HISFC.Models.Fee.Outpatient.Balance invoiceInfo = new FS.HISFC.Models.Fee.Outpatient.Balance();
		ArrayList invoices = new ArrayList();
		ArrayList feeDetails = new ArrayList();

		string operCode = "";
		int count = 0;
		decimal totCost = 0;
        /// <summary>
        /// Ö¸Òýµ¥´¦ÀíÏµÍ³Àà±ð
        /// 
        /// ÏÖÓÉ³ÌÐòÖ¸¶¨¡£
        /// ¿É×ö³ÉÅäÖÃ¶ÁÈ¡
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

		public void SetDisplay()
		{
            this.label1.Text = this.managerIntegrate.GetHospitalName();

			this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 6);
			this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("ËÎÌå", 9,System.Drawing.FontStyle.Bold);
			this.fpSpread1_Sheet1.Cells[count,0].Text = "ÐÕÃû:" + rInfo.Name + " ¹ÒºÅÈÕÆÚ:" + rInfo.DoctorInfo.SeeDate.ToShortDateString();
			count ++;
			for(int i = 0; i < this.invoices.Count; i++)
			{
                FS.HISFC.Models.Fee.Outpatient.Balance invoice = invoices[i] as FS.HISFC.Models.Fee.Outpatient.Balance;

				if(invoice.Memo == "5")
				{
					continue;
				}
				this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 6);
				this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("ËÎÌå", 9,System.Drawing.FontStyle.Bold);
				this.fpSpread1_Sheet1.Cells[count,0].Text = "²¡ÀúºÅ:" + rInfo.PID.CardNO + " ·¢Æ±ºÅ:" + invoice.Invoice.ID;
				
				count ++;
			}
			if(feeDetails == null)
			{
				return;
			}

            string strTemp = "";
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

            foreach (string excuDept in dicExcuAddr.Keys)
            {
                lstItem = dicExcuAddr[excuDept];
                if (lstItem == null || lstItem.Count <= 0)
                    continue;

                count++;

                this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 6);
                this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("ËÎÌå", 11, System.Drawing.FontStyle.Bold);
                this.fpSpread1_Sheet1.Cells[count, 0].Text = "Ö´ÐÐ¿ÆÊÒ£º" + excuDept;

                count++;

                this.fpSpread1_Sheet1.Cells[count, 0].Text = "ÏîÄ¿Ãû³Æ";
                this.fpSpread1_Sheet1.Cells[count, 2].Text = "¹æ¸ñ";
                this.fpSpread1_Sheet1.Cells[count, 4].Text = "ÊýÁ¿";
                this.fpSpread1_Sheet1.Cells[count, 5].Text = "½ð¶î";
                count++;

                FS.HISFC.Models.Fee.Outpatient.FeeItemList itemTemp = null;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in lstItem)
                {
                    if (itemTemp != null)
                    {
                        if (!string.IsNullOrEmpty(item.UndrugComb.Name))
                        {
                            if (itemTemp.UndrugComb.Name == item.UndrugComb.Name)
                                continue;
                        }
                    }
                    itemTemp = item;

                    this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 2);
                    this.fpSpread1_Sheet1.Cells[count, 0].Text = string.IsNullOrEmpty(item.UndrugComb.Name) ? item.Item.Name : item.UndrugComb.Name;

                    this.fpSpread1_Sheet1.Models.Span.Add(count, 2, 1, 2);
                    this.fpSpread1_Sheet1.Cells[count, 2].Text = item.Item.Specs;
                    this.fpSpread1_Sheet1.Cells[count, 2].Font = new Font("ËÎÌå", 9);
                    if (item.FeePack == "1")//°ü×°µ¥Î»
                    {
                        this.fpSpread1_Sheet1.Cells[count, 4].Text = (item.Item.Qty/item.Item.PackQty).ToString();
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[count, 4].Text = item.Item.Qty.ToString();
                    }

                    this.fpSpread1_Sheet1.Cells[count, 5].Text = item.FT.TotCost.ToString();

                    count++;
                }
            }

            count++;

			this.fpSpread1_Sheet1.RowCount = count;

		}

        List<string> lstPrint = new List<string>();
        public void SetPrintValue()
        {
            lstPrint.Clear();

            string strTemp = this.managerIntegrate.GetHospitalName();
            strTemp = strTemp.PadLeft(18, ' ');
            lstPrint.Add(strTemp);
            strTemp = "ÃÅÕïÊÕ·ÑÖ¸Òýµ¥";
            strTemp = strTemp.PadLeft(18, ' ');
            lstPrint.Add(strTemp);

            // »¼ÕßÐÅÏ¢
            lstPrint.Add(" ");
            strTemp = "ÐÕÃû£º" + rInfo.Name + "  ²¡ÀúºÅ£º" + rInfo.PID.CardNO;
            lstPrint.Add(strTemp);

            // ·¢Æ±ÐÅÏ¢
            FS.HISFC.Models.Fee.Outpatient.Balance invoice = invoices[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
            strTemp ="·¢Æ±ºÅ£º" + invoice.Invoice.ID;
            lstPrint.Add(strTemp);

            //¹ÒºÅÐÅÏ¢

            strTemp = "¹ÒºÅÈÕÆÚ£º" + rInfo.DoctorInfo.SeeDate.ToShortDateString();
            lstPrint.Add(strTemp);
            // ·ÑÓÃÐÅÏ¢
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
                            deptTicp.Add(excuDept, "Çëµ½ ¡¾ ×¢ÉäÊÒ ¡¿ ÖÎÁÆ ");
                        }
                    }
                    else if (lstUnDetialClass.Contains(item.Item.SysClass.ID.ToString()))
                    {
                        if (!deptTicp.ContainsKey(excuDept))
                        {
                            deptTicp.Add(excuDept, "Çëµ½ ¡¾" + excuDept + "¡¿ È¡Ò© ");
                        }
                    }
                }
            }



            foreach (string excuDept in dicExcuAddr.Keys)
            {
                lstItem = dicExcuAddr[excuDept];
                if (lstItem == null || lstItem.Count <= 0)
                    continue;
                lstPrint.Add(" ");
                lstPrint.Add(" ");
                strTemp = "----------------------------";
                lstPrint.Add(strTemp);

                if (deptTicp.ContainsKey(excuDept))
                {
                    strTemp = "Ö´ÐÐ¿ÆÊÒ£º" + deptTicp[excuDept];
                    lstPrint.Add(strTemp);
                    continue;
                }



                strTemp = "Ö´ÐÐ¿ÆÊÒ£º¡¾" + excuDept + "¡¿";
                lstPrint.Add(strTemp);

                

                strTemp = "      ÏîÄ¿Ãû³Æ      " + "     ÊýÁ¿    ";
                lstPrint.Add(strTemp);

                string str = "";
                FS.HISFC.Models.Fee.Outpatient.FeeItemList itemTemp = null;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in lstItem)
                {
                    if (itemTemp != null)
                    {
                        if (!string.IsNullOrEmpty(item.UndrugComb.Name))
                        {
                            if (itemTemp.UndrugComb.Name == item.UndrugComb.Name)
                                continue;
                        }
                    }
                    itemTemp = item;

                    // ÏîÄ¿Ãû³Æ
                    str = string.IsNullOrEmpty(item.UndrugComb.Name) ? item.Item.Name : item.UndrugComb.Name;
                    str = str.PadRight(15, ' ');
                    strTemp = str;

                    //str = item.Item.Specs;
                    //str = str.PadRight(9, ' ');
                    //strTemp += str;

                    // ÊýÁ¿
                    if (item.FeePack == "1")//°ü×°µ¥Î»
                    {
                        str = (item.Item.Qty / item.Item.PackQty).ToString();
                    }
                    else
                    {
                        str = item.Item.Qty.ToString();
                    }
                    str += item.Item.PriceUnit;
                    strTemp += str;

                    //// ½ð¶î
                    //str = (item.FT.OwnCost + item.FT.PubCost + item.FT.PayCost).ToString();
                    //str = "  " + str;
                    //strTemp += str;

                    lstPrint.Add(strTemp);
                }
            }
            strTemp = "----------------------------";
            lstPrint.Add(strTemp);
            lstPrint.Add(" ");
            lstPrint.Add(" ");
            lstPrint.Add(" ");
        }

        #region IOutpatientGuide ³ÉÔ±

        public void Print()
        {
            if (lstPrint == null || lstPrint.Count <= 0)
            {
                return ;
            }

            try
            {
                string strMsg = "";
                int iRes = this.GetCOMParams(out portName, out baudRate, out parity, out stopBit, out dataBit, out strMsg);
                if (iRes <= 0)
                {
                    return ;
                }
                this.OpenCOM();
                this.Send("@"); // ³õÊ¼»¯
                //ÉèÖÃ´òÓ¡×ÖÌå
                int it1 = 27;
                int it2 = 156;
                string s1 = ((char)it1).ToString();
                string s2 = ((char)it2).ToString();
                this.Send(s1 + "!" + s2);//ÉèÖÃÓ¢ÎÄ×ÖÌå
                int it3 = 28;
                int it4 = 40;
                string s3 = ((char)it3).ToString();
                string s4 = ((char)it4).ToString();
                this.Send(s3 + "!" + s4);//ÉèÖÃÖÐÎÄ×ÖÌå
                foreach (string str in lstPrint)
                {
                    this.Send(str + "\r\n");
                }

                this.Send("\r\n");
                this.Send("\r\n");
                this.Send("i");

                this.CloseCOM();

            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
                return ;
            }
            finally
            {
                this.CloseCOM();
            }
            return ;

            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            //FS.HISFC.Models.Base.PageSize pSize = psManager.GetPageSize("MZZYD");
            //if (pSize == null)
            //{
            //    pSize = new FS.HISFC.Models.Base.PageSize("MZZYD", 400, 400);
            //    pSize.Top = 0;
            //    pSize.Left = 0;

            //    pSize.Printer = "MZZYD";
            //}

            //print.SetPageSize(pSize);

            //print.PrintPage(0, 0, this);
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

        #region »ñÈ¡²ÎÊý
        private string portName = string.Empty;
        private int baudRate = 0;
        private Parity parity = Parity.None;
        private StopBits stopBit = StopBits.None;
        private int dataBit = 0;
        /// <summary>
        /// ´®¿Ú
        /// </summary>
        SerialPort serialPortObj = null;

        private string proFileName = Application.StartupPath + @"\Profile\·ðËÄÆ±¾Ý´òÓ¡ÅäÖÃ²ÎÊý.xml";

        /// <summary>
        /// »ñµÃÁ¬½ÓÒ½±£Êý¾Ý¿â²ÎÊý
        /// </summary>
        /// <param name="strDbName"></param>
        /// <param name="strDbUser"></param>
        /// <param name="strDbPwd"></param>
        /// <returns></returns>
        private int GetCOMParams(out string strPortName, out int iBaudRate, out Parity parity, out StopBits stopBit, out int dataBit, out string errMsg)
        {
            strPortName = "";
            iBaudRate = 0;
            parity = Parity.None;
            stopBit = StopBits.None;
            errMsg = "";
            dataBit = 8;

            XmlDocument doc = new XmlDocument();

            try
            {
                StreamReader sr = new StreamReader(proFileName, System.Text.Encoding.Default);
                string cleanDown = sr.ReadToEnd();
                doc.LoadXml(cleanDown);
                sr.Close();
            }
            catch (Exception objEx)
            {
                errMsg = "¼ÓÔØÆ±¾Ý´òÓ¡ÅäÖÃ²ÎÊýÊ§°Ü£¡ " + objEx.Message;
                return -1;
            }

            XmlNode paramNode = doc.SelectSingleNode("/Æ±¾Ý´òÓ¡");
            try
            {
                strPortName = paramNode.ChildNodes[0].Attributes["PortName"].Value.Trim();
                int.TryParse(paramNode.ChildNodes[0].Attributes["BaudRate"].Value, out iBaudRate);
                string strParity = paramNode.ChildNodes[0].Attributes["Parity"].Value;
                string strstopBit = paramNode.ChildNodes[0].Attributes["StopBit"].Value;
                int.TryParse(paramNode.ChildNodes[0].Attributes["DataBit"].Value, out dataBit);

                strParity = strParity.ToUpper();
                switch (strParity)
                {
                    case "NONE":
                        parity = Parity.None;
                        break;
                    case "ODD":
                        parity = Parity.Odd;
                        break;
                    case "EVEN":
                        parity = Parity.Even;
                        break;
                    case "MARK":
                        parity = Parity.Mark;
                        break;
                    case "SPACE":
                        parity = Parity.Space;
                        break;
                }

                strstopBit = strstopBit.ToUpper();
                switch (strstopBit)
                {
                    case "NONE":
                        stopBit = StopBits.None;
                        break;
                    case "ONE":
                        stopBit = StopBits.One;
                        break;
                    case "TWO":
                        stopBit = StopBits.Two;
                        break;
                    case "ONEPOINTFIVE":
                        stopBit = StopBits.OnePointFive;
                        break;
                }
            }
            catch (Exception objEx)
            {
                errMsg = "¶ÁÈ¡Æ±¾Ý´òÓ¡ÅäÖÃ²ÎÊýÊ§°Ü£¡" + objEx.Message;
                return -1;
            }
            return 1;
        }

        #endregion
        #region ´®¿Ú²Ù×÷

        /// <summary>
        /// ´ò¿ªÒ»¸öÐÂµÄ´®ÐÐ¶Ë¿ÚÁ¬½Ó£¬²¢¿ªÊ¼½ÓÊÕÊý¾Ý¡£
        /// </summary>
        private int OpenCOM()
        {
            if (serialPortObj == null)
                serialPortObj = new SerialPort();

            if (!serialPortObj.IsOpen)
            {
                try
                {
                    serialPortObj.PortName = portName;
                    serialPortObj.BaudRate = baudRate;
                    serialPortObj.DataBits = dataBit;
                    serialPortObj.StopBits = stopBit;
                    serialPortObj.Parity = parity;
                    serialPortObj.WriteBufferSize = 2048;
                    serialPortObj.ReadBufferSize = 2048;

                    serialPortObj.Open();
                }
                catch (Exception objEx)
                {
                    MessageBox.Show("´ò¿ª´®¿Ú×ÊÔ´Ê§°Ü£¡" + objEx.Message);
                    return -1;
                }
            }
            return 1;
        }

        private int Send(string sendMsg)
        {
            if (string.IsNullOrEmpty(sendMsg))
            {
                return 1;
            }

            byte[] buff = Encoding.Default.GetBytes(sendMsg);

            return this.Send(buff);
        }

        private int Send(byte[] buffer)
        {
            if (buffer == null || buffer.Length <= 0)
                return -1;

            if (serialPortObj == null)
                return -1;
            if (!serialPortObj.IsOpen)
            {
                return -1;
            }

            serialPortObj.Write(buffer, 0, buffer.Length);
            return 1;
        }

        /// <summary>
        /// ¹Ø±Õ¶Ë¿ÚÁ¬½Ó£¬½« IsOpen ÊôÐÔÉèÖÃÎª false£¬²¢ÊÍ·ÅÄÚ´æ¡£
        /// </summary>
        public void CloseCOM()
        {
            if (serialPortObj == null)
            {
                return;
            }
            if (serialPortObj.IsOpen)
            {
                serialPortObj.Close();
            }
            serialPortObj.Dispose();
        }
        #endregion
    }
}
