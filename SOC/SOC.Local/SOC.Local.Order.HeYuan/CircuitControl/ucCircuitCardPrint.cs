using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Order.HeYuan.Classes;

namespace FS.SOC.Local.Order.HeYuan.CircuitControl
{
    public partial class ucCircuitCardPrint : UserControl
    {

        public ucCircuitCardPrint()
        {
            InitializeComponent();
        }

        private ArrayList alOrders = new ArrayList();

        public ArrayList AlOrders
        {
            set
            {
                this.alOrders = value;
                this.SetItems();
            }
        }
        /// <summary>
        /// 获取医院名称和医院LOGO
        /// </summary>
        private void GetHospLogo()
        {
            string erro = "出错";
            string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.SOC.Local.Order.HeYuan.Classes.Function.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }


        private void SetItems()//构造预览页面内容
        {
            //获取LOGO
            #region 河源不获取logo
            //GetHospLogo();
            #endregion

            string AuthorName = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Name;
            //string AuthorNO = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).ID;
            this.lblAuthor.Text =  AuthorName;

            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            foreach (string s in alOrders)
            {
                string[] items = s.Split('|');

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);//在页尾往后增加1行

                for (int i = 0; i < items.Length - 1; i++)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, i].Text = items[i];
                }
                if (this.neuSpread1_Sheet1.Rows.Count == 1)//只有一行则继续
                {
                    continue;
                }
            }

            #region 内容

            //只显示下面的边框  bevelBorder1---普通线   bevelBorder2---粗黑线   bevelBorder3---空白线
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White, 1, false, false, false, false);

            if (this.neuSpread1_Sheet1.Rows.Count > 0)//有数据
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    //“医嘱内容”容纳14个汉字28个字节长度====只显示到2行，3行以上不显示
                    if (FS.SOC.Local.Order.HeYuan.Classes.Function.SubItemNameFP(this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ItemName].Text, 28).Count > 1)
                    {
                        this.neuSpread1_Sheet1.Rows[i].Height = 2 * this.neuSpread1_Sheet1.Rows[i].Height;
                    }
                    
                    if (i != this.neuSpread1_Sheet1.RowCount - 1)//未完
                    {
                        for (int j = (int)ExecBillCols.Memo; j < (int)ExecBillCols.PrintFlag; j++)//时间点之后---普通线
                        {
                            this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder1;
                        }

                        if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┏" || this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text == "┃")//组内用空白线
                        {
                            for (int j = (int)ExecBillCols.OrderState; j <= (int)ExecBillCols.DoseOnce; j++)//空白线
                            {
                                this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder3;
                            }
                        }
                        else if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┗")//不同组---普通线
                        {
                            for (int j = (int)ExecBillCols.BedID; j <= (int)ExecBillCols.PatientName; j++)//普通线-----不起作用？？？
                            {
                                this.neuSpread1_Sheet1.Cells[i, j].Text = " ";
                                this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder1;
                            }

                            this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
                        }


                        if (this.neuSpread1_Sheet1.RowCount > 1)
                        {
                            this.neuSpread1_Sheet1.Cells[0, (int)ExecBillCols.BedID].Tag = this.neuSpread1_Sheet1.Cells[0, (int)ExecBillCols.BedID].Text;
                            if (this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Text == "")
                            {
                                this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Tag = this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.BedID].Tag;
                            }
                            else if (this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Text == this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.BedID].Tag.ToString())//床号相同
                            {
                                this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Tag = this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.BedID].Tag;
                                this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
                            }
                            else //床号不同---粗黑线
                            {
                                this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Tag = this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Text;
                                this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                                for (int j = (int)ExecBillCols.Memo; j < (int)ExecBillCols.PrintFlag; j++)
                                {
                                    this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder2;
                                }
                            }
                        }
                    }
                    else //表尾
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// 设置人员基本信息
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.lblDept.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.lblBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
            this.lblPatientNo.Text = patientInfo.PID.PatientNO;
            this.lblName.Text = patientInfo.Name;
            this.lblSex.Text = patientInfo.Sex.Name;
            this.lblAge.Text = patientInfo.Age;

        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="titleDate"></param>
        /// <param name="printTime"></param>
        /// <param name="page"></param>
        public void SetHeader(string title, string titleDate, string printTime, string page) 
        {
            this.lblTitle.Text = title;
            this.neuLblExecTime.Text = titleDate;
            this.neuLblPrintTime.Text = printTime;
            this.lblPage.Text = "第" + page + "页";
        }
        
    }
    
}
