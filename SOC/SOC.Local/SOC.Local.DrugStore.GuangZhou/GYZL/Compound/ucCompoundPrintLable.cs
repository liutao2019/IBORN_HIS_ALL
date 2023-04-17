using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound
{
    public partial class ucCompoundPrintLable : UserControl
    {
        public ucCompoundPrintLable()
        {
            InitializeComponent();
        }

        #region 变量


        /// <summary>
        /// 药库管理类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 住院患者管理类
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 药品管理类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();



        /// <summary>
        /// 常数管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        #endregion


        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.SetPageSize(new FS.HISFC.Models.Base.PageSize("InPatientDrugLabel",400, 400));
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsDataAutoExtend = false;
                try
                {
                    //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
                    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
                }
                catch { }
                if(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)                    
                {
                    print.PrintPreview(5, 5, this);
                }
                else
                {
                    print.PrintPage(5, 5, this);
                }
                this.Clear();
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }
        
        /// <summary>
        /// 清除控件文字
        /// </summary>
        private void Clear()
        {
            this.nlbShowID.Text = string.Empty;

            this.nlbPrintTime.Text = string.Empty;

            this.nlbBedNO.Text = string.Empty;

            this.nlbDeptName.Text = string.Empty;

            this.nlbCardNO.Text = string.Empty;

            this.nlbName.Text = string.Empty;

            this.nlbSex.Text = string.Empty;

            this.nlbAge.Text = string.Empty;

            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 2)
                {
                    for (int i = 1; i < this.neuSpread1_Sheet1.RowCount - 1; i++)
                    {
                        this.neuSpread1_Sheet1.RemoveRows(1, 1);
                    }
                }
            }
            catch 
            {
                this.neuSpread1_Sheet1.RowCount = 1;
            }
        }


        /// <summary>
        /// 设置标签的值并打印
        /// </summary>
        /// <param name="allData"></param>
        /// <param name="printIndex"></param>
        public void SetValue(ArrayList allData, int printIndex)
        {
            this.Clear();
            this.SetLableValue(allData,printIndex);
            this.SetFarPoint(allData);
            this.Print();
        
        }

        /// <summary>
        /// 设置Lable的值
        /// </summary>
        /// <param name="info"></param>
        private void SetLableValue(ArrayList allData,int printIndex)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut applyInfo = allData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;

            FS.HISFC.Models.RADT.PatientInfo patient = this.inpatientMgr.QueryPatientInfoByInpatientNO(applyInfo.PatientNO);

            //编号
            this.nlbShowID.Text = applyInfo.CompoundGroup;

            //打印时间
            this.nlbPrintTime.Text = this.itemManager.GetDateTimeFromSysDateTime().ToString();

            //科室
            this.nlbDeptName.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyInfo.ApplyDept.ID);

            //床号
            this.nlbBedNO.Text = applyInfo.User01;

            //住院号
            this.nlbCardNO.Text = applyInfo.PatientNO;

            //姓名
            this.nlbName.Text = patient.Name;

            //性别
            this.nlbSex.Text = patient.Sex.Name;

            //年龄
            this.nlbAge.Text = patient.Age;

        }

        /// <summary>
        /// 设置farpoint显示格式
        /// </summary>
        /// <param name="allData"></param>
        private void SetFarPoint(ArrayList allData)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut info = allData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;

            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, allData.Count + 1);

            int index = 1;

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in allData)
            {
                FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyInfo.Item.ID);

                this.neuSpread1_Sheet1.SetText(index, 0, item.Name);

                this.neuSpread1_Sheet1.SetText(index, 1, item.Specs);

                this.neuSpread1_Sheet1.SetText(index, 2, applyInfo.Item.DoseUnit);

                this.neuSpread1_Sheet1.SetText(index, 3, applyInfo.Operation.ApplyQty.ToString());

                this.neuSpread1_Sheet1.SetText(index, 4, applyInfo.DoseOnce.ToString());

                index++;
            }
            this.AddLastRow(allData);
        
        }

        /// <summary>
        /// 增加最后一行
        /// </summary>
        private void AddLastRow(ArrayList allData)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut info = allData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, this.neuSpread1_Sheet1.ColumnCount);
            try
            {
                string txtInfo = "该组内共计{0}种药品  用法：{1}  频次：{2}";
                txtInfo = string.Format(txtInfo,allData.Count ,FS.SOC.HISFC.BizProcess.Cache.Order.GetFrequency(info.Frequency.ID), FS.SOC.HISFC.BizProcess.Cache.Common.GetUsage(info.Usage.ID));
                this.neuSpread1_Sheet1.SetText(this.neuSpread1_Sheet1.RowCount - 1, 0, txtInfo);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            }
            catch { }
            FarPoint.Win.LineBorder lineBorder11 = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, true);
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Border = lineBorder11;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            //设置补打标记
            if (info.PrintState == "0")
            {
                this.lbReprint.Visible = false;
            }
            else
            {
                this.lbReprint.Visible = true;
            }
        }
    }
}
