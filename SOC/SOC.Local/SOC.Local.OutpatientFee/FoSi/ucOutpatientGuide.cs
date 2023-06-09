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
	/// ucFeeDetail 的摘要说明。
	/// </summary>
    public class ucOutpatientGuide : System.Windows.Forms.UserControl, IOutpatientGuide
	{
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ucOutpatientGuide()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

            lstSysClass = new List<string>();

            lstSysClass.AddRange(new string[] { "UL", "UC", "UZ", "UO", "P", "PCZ", "PCC" });

            lstUnDetialClass.AddRange(new string[] { "P", "PCZ", "PCC" });
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
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
            this.fpSpread1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(3, 40);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(349, 352);
            this.fpSpread1.TabIndex = 11;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 140F;
            this.fpSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 63F;
            this.fpSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 37F;
            this.fpSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 29F;
            this.fpSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 55F;
            this.fpSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(100, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "**医院";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(110, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "门诊收费指引单";
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
        /// 打印纸张设置类
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
        /// 指引单处理系统类别
        /// 
        /// 现由程序指定。
        /// 可做成配置读取
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
			this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("宋体", 9,System.Drawing.FontStyle.Bold);
			this.fpSpread1_Sheet1.Cells[count,0].Text = "姓名:" + rInfo.Name + " 挂号日期:" + rInfo.DoctorInfo.SeeDate.ToShortDateString();
			count ++;
			for(int i = 0; i < this.invoices.Count; i++)
			{
                FS.HISFC.Models.Fee.Outpatient.Balance invoice = invoices[i] as FS.HISFC.Models.Fee.Outpatient.Balance;

				if(invoice.Memo == "5")
				{
					continue;
				}
				this.fpSpread1_Sheet1.Models.Span.Add(count, 0, 1, 6);
				this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("宋体", 9,System.Drawing.FontStyle.Bold);
				this.fpSpread1_Sheet1.Cells[count,0].Text = "病历号:" + rInfo.PID.CardNO + " 发票号:" + invoice.Invoice.ID;
				
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
                this.fpSpread1_Sheet1.Cells[count, 0].Font = new Font("宋体", 11, System.Drawing.FontStyle.Bold);
                this.fpSpread1_Sheet1.Cells[count, 0].Text = "执行科室：" + excuDept;

                count++;

                this.fpSpread1_Sheet1.Cells[count, 0].Text = "项目名称";
                this.fpSpread1_Sheet1.Cells[count, 2].Text = "规格";
                this.fpSpread1_Sheet1.Cells[count, 4].Text = "数量";
                this.fpSpread1_Sheet1.Cells[count, 5].Text = "金额";
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
                    this.fpSpread1_Sheet1.Cells[count, 2].Font = new Font("宋体", 9);
                    if (item.FeePack == "1")//包装单位
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
            strTemp = "门诊收费指引单";
            strTemp = strTemp.PadLeft(18, ' ');
            lstPrint.Add(strTemp);

            // 患者信息
            lstPrint.Add(" ");
            strTemp = "姓名：" + rInfo.Name + "  病历号：" + rInfo.PID.CardNO;
            lstPrint.Add(strTemp);

            // 发票信息
            FS.HISFC.Models.Fee.Outpatient.Balance invoice = invoices[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
            strTemp ="发票号：" + invoice.Invoice.ID;
            lstPrint.Add(strTemp);

            //挂号信息

            strTemp = "挂号日期：" + rInfo.DoctorInfo.SeeDate.ToShortDateString();
            lstPrint.Add(strTemp);
            // 费用信息
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
                            deptTicp.Add(excuDept, "请到 【 注射室 】 治疗 ");
                        }
                    }
                    else if (lstUnDetialClass.Contains(item.Item.SysClass.ID.ToString()))
                    {
                        if (!deptTicp.ContainsKey(excuDept))
                        {
                            deptTicp.Add(excuDept, "请到 【" + excuDept + "】 取药 ");
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
                    strTemp = "执行科室：" + deptTicp[excuDept];
                    lstPrint.Add(strTemp);
                    continue;
                }



                strTemp = "执行科室：【" + excuDept + "】";
                lstPrint.Add(strTemp);

                

                strTemp = "      项目名称      " + "     数量    ";
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

                    // 项目名称
                    str = string.IsNullOrEmpty(item.UndrugComb.Name) ? item.Item.Name : item.UndrugComb.Name;
                    str = str.PadRight(15, ' ');
                    strTemp = str;

                    //str = item.Item.Specs;
                    //str = str.PadRight(9, ' ');
                    //strTemp += str;

                    // 数量
                    if (item.FeePack == "1")//包装单位
                    {
                        str = (item.Item.Qty / item.Item.PackQty).ToString();
                    }
                    else
                    {
                        str = item.Item.Qty.ToString();
                    }
                    str += item.Item.PriceUnit;
                    strTemp += str;

                    //// 金额
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

        #region IOutpatientGuide 成员

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
                this.Send("@"); // 初始化
                //设置打印字体
                int it1 = 27;
                int it2 = 156;
                string s1 = ((char)it1).ToString();
                string s2 = ((char)it2).ToString();
                this.Send(s1 + "!" + s2);//设置英文字体
                int it3 = 28;
                int it4 = 40;
                string s3 = ((char)it3).ToString();
                string s4 = ((char)it4).ToString();
                this.Send(s3 + "!" + s4);//设置中文字体
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

        #region 获取参数
        private string portName = string.Empty;
        private int baudRate = 0;
        private Parity parity = Parity.None;
        private StopBits stopBit = StopBits.None;
        private int dataBit = 0;
        /// <summary>
        /// 串口
        /// </summary>
        SerialPort serialPortObj = null;

        private string proFileName = Application.StartupPath + @"\Profile\佛四票据打印配置参数.xml";

        /// <summary>
        /// 获得连接医保数据库参数
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
                errMsg = "加载票据打印配置参数失败！ " + objEx.Message;
                return -1;
            }

            XmlNode paramNode = doc.SelectSingleNode("/票据打印");
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
                errMsg = "读取票据打印配置参数失败！" + objEx.Message;
                return -1;
            }
            return 1;
        }

        #endregion
        #region 串口操作

        /// <summary>
        /// 打开一个新的串行端口连接，并开始接收数据。
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
                    MessageBox.Show("打开串口资源失败！" + objEx.Message);
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
        /// 关闭端口连接，将 IsOpen 属性设置为 false，并释放内存。
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
