using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.PubReport.Components
{
    public partial class ucSIGYRep : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSIGYRep()
        {
            InitializeComponent();
        }

        public bool IsRetire
        {
            get
            {
                return this.retired;
            }
            set
            {
                this.retired = value;
            }
        }
        private bool retired = false;
        SOC.Local.PubReport.BizLogic.PubReport pubRep = new SOC.Local.PubReport.BizLogic.PubReport();

        public void SetValue(string begin, string end)
        {
            DataSet dsMZ = this.pubRep.GetSumForSheng(begin, end, this.IsRetire, "2");
            if (dsMZ == null)
            {
                MessageBox.Show("获取门诊费用出错！" + this.pubRep.Err);
                return;
            }
            DataSet dsZY = this.pubRep.GetSumForSheng(begin, end, this.IsRetire, "1");
            if (dsZY == null)
            {
                MessageBox.Show("获取住院费用出错！" + this.pubRep.Err);
                return;
            }
            //令人恶心的报表，令人恶心的程序，
            //下面赋值代码写死，先不出更好的方法了。
            decimal[] fees = new decimal[9];//82,83
            decimal[] feeJK = new decimal[9];//缴款单位
            this.fpSpread7_Sheet1.Cells[0, 1, 6, 20].Value = 0;
            #region 门诊
            foreach (DataRow row in dsMZ.Tables[0].Rows)
            {
                switch (row["bh"].ToString())
                {
                    case "80":
                        this.fpSpread7_Sheet1.SetValue(1, 1, row["num"], false);
                        this.fpSpread7_Sheet1.SetValue(1, 2, row["yaofei"], false);
                        this.fpSpread7_Sheet1.SetValue(1, 3, row["jiancha"], false);
                        this.fpSpread7_Sheet1.SetValue(1, 4, row["zhiliao"], false);
                        this.fpSpread7_Sheet1.SetValue(1, 5, row["bedfee"], false);
                        this.fpSpread7_Sheet1.SetValue(1, 6, row["zhenjin"], false);
                        this.fpSpread7_Sheet1.SetValue(1, 7, row["jzzje"], false);
                        this.fpSpread7_Sheet1.SetValue(1, 8, row["zifu"], false);
                        this.fpSpread7_Sheet1.SetValue(1, 9, row["sjje"], false);
                        break;
                    case "81":
                        this.fpSpread7_Sheet1.SetValue(2, 1, row["num"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 2, row["yaofei"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 3, row["jiancha"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 4, row["zhiliao"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 5, row["bedfee"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 6, row["zhenjin"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 7, row["jzzje"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 8, row["zifu"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 9, row["sjje"], false);
                        break;
                    case "82":
                        fees[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        fees[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        fees[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        fees[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        fees[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        fees[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        fees[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        fees[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        fees[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "83":
                        fees[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        fees[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        fees[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        fees[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        fees[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        fees[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        fees[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        fees[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        fees[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "J80":
                        feeJK[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        feeJK[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        feeJK[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        feeJK[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        feeJK[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        feeJK[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        feeJK[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        feeJK[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        feeJK[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "J81":
                        feeJK[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        feeJK[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        feeJK[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        feeJK[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        feeJK[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        feeJK[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        feeJK[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        feeJK[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        feeJK[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "J82":
                        feeJK[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        feeJK[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        feeJK[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        feeJK[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        feeJK[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        feeJK[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        feeJK[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        feeJK[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        feeJK[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "J83":
                        feeJK[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        feeJK[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        feeJK[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        feeJK[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        feeJK[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        feeJK[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        feeJK[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        feeJK[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        feeJK[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "90":
                        this.fpSpread7_Sheet1.SetValue(5, 1, row["num"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 2, row["yaofei"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 3, row["jiancha"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 4, row["zhiliao"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 5, row["bedfee"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 6, row["zhenjin"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 7, row["jzzje"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 8, row["zifu"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 9, row["sjje"], false);
                        break;
                    default:
                        MessageBox.Show("出错" + row["bh"].ToString());
                        break;
                }
                this.fpSpread7_Sheet1.SetValue(3, 1, fees[0], false);
                this.fpSpread7_Sheet1.SetValue(3, 2, fees[1], false);
                this.fpSpread7_Sheet1.SetValue(3, 3, fees[2], false);
                this.fpSpread7_Sheet1.SetValue(3, 4, fees[3], false);
                this.fpSpread7_Sheet1.SetValue(3, 5, fees[4], false);
                this.fpSpread7_Sheet1.SetValue(3, 6, fees[5], false);
                this.fpSpread7_Sheet1.SetValue(3, 7, fees[6], false);
                this.fpSpread7_Sheet1.SetValue(3, 8, fees[7], false);
                this.fpSpread7_Sheet1.SetValue(3, 9, fees[8], false);

                this.fpSpread7_Sheet1.SetValue(4, 1, feeJK[0], false);
                this.fpSpread7_Sheet1.SetValue(4, 2, feeJK[1], false);
                this.fpSpread7_Sheet1.SetValue(4, 3, feeJK[2], false);
                this.fpSpread7_Sheet1.SetValue(4, 4, feeJK[3], false);
                this.fpSpread7_Sheet1.SetValue(4, 5, feeJK[4], false);
                this.fpSpread7_Sheet1.SetValue(4, 6, feeJK[5], false);
                this.fpSpread7_Sheet1.SetValue(4, 7, feeJK[6], false);
                this.fpSpread7_Sheet1.SetValue(4, 8, feeJK[7], false);
                this.fpSpread7_Sheet1.SetValue(4, 9, feeJK[8], false);
            }
            #endregion
            #region 住院
            fees = new decimal[10];
            feeJK = new decimal[10];
            foreach (DataRow row in dsZY.Tables[0].Rows)
            {
                switch (row["bh"].ToString())
                {
                    case "80":
                        this.fpSpread7_Sheet1.SetValue(1, 10, row["num"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 11, row["days"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 12, row["yaofei"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 13, row["jiancha"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 14, row["zhiliao"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 15, row["bedfee"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 16, row["zhenjin"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 17, row["jzzje"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 18, row["zifu"], true);
                        this.fpSpread7_Sheet1.SetValue(1, 19, row["sjje"], true);
                        break;
                    case "81":
                        this.fpSpread7_Sheet1.SetValue(2, 10, row["num"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 11, row["days"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 12, row["yaofei"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 13, row["jiancha"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 14, row["zhiliao"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 15, row["bedfee"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 16, row["zhenjin"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 17, row["jzzje"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 18, row["zifu"], false);
                        this.fpSpread7_Sheet1.SetValue(2, 19, row["sjje"], false);
                        break;
                    case "82":
                        fees[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        fees[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["days"].ToString());
                        fees[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        fees[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        fees[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        fees[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        fees[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        fees[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        fees[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        fees[9] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "83":
                        fees[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        fees[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["days"].ToString());
                        fees[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        fees[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        fees[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        fees[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        fees[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        fees[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        fees[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        fees[9] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "J80":
                        feeJK[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        feeJK[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["days"].ToString());
                        feeJK[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        feeJK[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        feeJK[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        feeJK[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        feeJK[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        feeJK[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        feeJK[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        feeJK[9] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "J81":
                        feeJK[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        feeJK[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["days"].ToString());
                        feeJK[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        feeJK[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        feeJK[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        feeJK[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        feeJK[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        feeJK[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        feeJK[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        feeJK[9] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "J82":
                        feeJK[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        feeJK[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["days"].ToString());
                        feeJK[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        feeJK[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        feeJK[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        feeJK[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        feeJK[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        feeJK[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        feeJK[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        feeJK[9] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "J83":
                        feeJK[0] += FS.FrameWork.Function.NConvert.ToDecimal(row["num"].ToString());
                        feeJK[1] += FS.FrameWork.Function.NConvert.ToDecimal(row["days"].ToString());
                        feeJK[2] += FS.FrameWork.Function.NConvert.ToDecimal(row["yaofei"].ToString());
                        feeJK[3] += FS.FrameWork.Function.NConvert.ToDecimal(row["jiancha"].ToString());
                        feeJK[4] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhiliao"].ToString());
                        feeJK[5] += FS.FrameWork.Function.NConvert.ToDecimal(row["bedfee"].ToString());
                        feeJK[6] += FS.FrameWork.Function.NConvert.ToDecimal(row["zhenjin"].ToString());
                        feeJK[7] += FS.FrameWork.Function.NConvert.ToDecimal(row["jzzje"].ToString());
                        feeJK[8] += FS.FrameWork.Function.NConvert.ToDecimal(row["zifu"].ToString());
                        feeJK[9] += FS.FrameWork.Function.NConvert.ToDecimal(row["sjje"].ToString());
                        break;
                    case "90":
                        this.fpSpread7_Sheet1.SetValue(5, 10, row["num"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 11, row["days"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 12, row["yaofei"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 13, row["jiancha"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 14, row["zhiliao"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 15, row["bedfee"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 16, row["zhenjin"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 17, row["jzzje"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 18, row["zifu"], false);
                        this.fpSpread7_Sheet1.SetValue(5, 19, row["sjje"], false);
                        break;
                    default:
                        MessageBox.Show("出错" + row["bh"].ToString());
                        break;
                }
                this.fpSpread7_Sheet1.SetValue(3, 10, fees[0], false);
                this.fpSpread7_Sheet1.SetValue(3, 11, fees[1], false);
                this.fpSpread7_Sheet1.SetValue(3, 12, fees[2], false);
                this.fpSpread7_Sheet1.SetValue(3, 13, fees[3], false);
                this.fpSpread7_Sheet1.SetValue(3, 14, fees[4], false);
                this.fpSpread7_Sheet1.SetValue(3, 15, fees[5], false);
                this.fpSpread7_Sheet1.SetValue(3, 16, fees[6], false);
                this.fpSpread7_Sheet1.SetValue(3, 17, fees[7], false);
                this.fpSpread7_Sheet1.SetValue(3, 18, fees[8], false);
                this.fpSpread7_Sheet1.SetValue(3, 19, fees[9], false);

                this.fpSpread7_Sheet1.SetValue(4, 10, feeJK[0], false);
                this.fpSpread7_Sheet1.SetValue(4, 11, feeJK[1], false);
                this.fpSpread7_Sheet1.SetValue(4, 12, feeJK[2], false);
                this.fpSpread7_Sheet1.SetValue(4, 13, feeJK[3], false);
                this.fpSpread7_Sheet1.SetValue(4, 14, feeJK[4], false);
                this.fpSpread7_Sheet1.SetValue(4, 15, feeJK[5], false);
                this.fpSpread7_Sheet1.SetValue(4, 16, feeJK[6], false);
                this.fpSpread7_Sheet1.SetValue(4, 17, feeJK[7], false);
                this.fpSpread7_Sheet1.SetValue(4, 18, feeJK[8], false);
                this.fpSpread7_Sheet1.SetValue(4, 19, feeJK[9], false);
            }
            #endregion
            #region 汇总

            for (int i = 1; i <= 20; i++)
            {

                this.fpSpread7_Sheet1.Cells[0, i].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[1, i].Text)
                    + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[2, i].Text);

                this.fpSpread7_Sheet1.Cells[6, i].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[0, i].Text)
                    + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[3, i].Text);
                this.fpSpread7_Sheet1.Cells[6, i].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[0, i].Text)
                    + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[4, i].Text);
                this.fpSpread7_Sheet1.Cells[6, i].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[0, i].Text)
                    + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[5, i].Text);
            }
            //			this.fpSpread7_Sheet1.Cells[0,1].Formula="SUM(B1:B3)";
            //			this.fpSpread7_Sheet1.Cells[0,2].Formula="SUM(C1:C3)";
            //			this.fpSpread7_Sheet1.Cells[0,3].Formula="SUM(D1:D3)";
            //			this.fpSpread7_Sheet1.Cells[0,4].Formula="SUM(E1:E3)";
            //			this.fpSpread7_Sheet1.Cells[0,5].Formula="SUM(F1:F3)";
            //			this.fpSpread7_Sheet1.Cells[0,6].Formula="SUM(G1:G3)";
            //			this.fpSpread7_Sheet1.Cells[0,7].Formula="SUM(H1:H3)";
            //			this.fpSpread7_Sheet1.Cells[0,8].Formula="SUM(I1:I3)";
            //			this.fpSpread7_Sheet1.Cells[0,9].Formula="SUM(J1:J3)";
            //			this.fpSpread7_Sheet1.Cells[0,10].Formula="SUM(K1:K3)";
            //			this.fpSpread7_Sheet1.Cells[0,11].Formula="SUM(L1:L3)";
            //			this.fpSpread7_Sheet1.Cells[0,12].Formula="SUM(M1:M3)";
            //			this.fpSpread7_Sheet1.Cells[0,13].Formula="SUM(N1:N3)";
            //			this.fpSpread7_Sheet1.Cells[0,14].Formula="SUM(O1:O3)";
            //			this.fpSpread7_Sheet1.Cells[0,15].Formula="SUM(P1:P3)";
            //			this.fpSpread7_Sheet1.Cells[0,16].Formula="SUM(Q1:Q3)";
            //			this.fpSpread7_Sheet1.Cells[0,17].Formula="SUM(R1:R3)";
            //			this.fpSpread7_Sheet1.Cells[0,18].Formula="SUM(S1:S3)";
            //			this.fpSpread7_Sheet1.Cells[0,19].Formula="SUM(T1:T3)";

            this.fpSpread7_Sheet1.Cells[0, 20].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[0, 9].Text)
                + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[0, 19].Text);

            this.fpSpread7_Sheet1.Cells[1, 20].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[1, 9].Text)
                + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[1, 19].Text);


            this.fpSpread7_Sheet1.Cells[2, 20].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[2, 9].Text)
                + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[2, 19].Text);


            this.fpSpread7_Sheet1.Cells[3, 20].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[3, 9].Text)
                + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[3, 19].Text);


            this.fpSpread7_Sheet1.Cells[4, 20].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[4, 9].Text)
                + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[4, 19].Text);


            this.fpSpread7_Sheet1.Cells[5, 20].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[5, 9].Text)
                + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[5, 19].Text);

            this.fpSpread7_Sheet1.Cells[6, 20].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[6, 9].Text)
                + FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread7_Sheet1.Cells[6, 19].Text);



            #endregion



        }
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.IsPrintInputBox = false;
            p.IsPrintBackImage = false;
            //p.TextBorder=FS.NFC.Interface.Classes.enuTextBorder.None;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //FS.Common.Class.Function.GetPageSize("birth");
            p.PrintPage(0, 0, this.panel1);
        }
        public void Export()
        {
            Exportinfo(this.fpSpread7);
        }

        /// <summary>
        /// 到处excel fp控件 update by ligz
        /// </summary>
        /// <param name="fpSpread"></param>
        public void Exportinfo(FarPoint.Win.Spread.FpSpread fpSpread)
        {
            string Result = "";
            try
            {
                bool ret = false;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel |.xls";
                saveFileDialog1.Title = "导出数据";

                saveFileDialog1.FileName = "导出";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        //以Excel 的形式导出数据
                        ret = fpSpread.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    }
                    if (ret)
                    {
                        MessageBox.Show("成功导出数据");
                    }
                }
                else
                {
                    MessageBox.Show("操作被取消");
                }
            }
            catch (Exception ee)
            {
                Result = ee.Message;
                MessageBox.Show(Result);
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}