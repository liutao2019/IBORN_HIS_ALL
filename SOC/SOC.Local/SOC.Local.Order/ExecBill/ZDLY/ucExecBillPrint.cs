using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SOC.Local.Order.Classes;

namespace SOC.Local.Order.ExecBill.ZDLY
{
    public partial class ucExecBillPrint : UserControl
    {

        public ucExecBillPrint()
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

        //private string title = "";

        //public string Title
        //{
        //    set
        //    {
        //        this.title = value;
        //        this.lblTitle.Text = value;
        //    }
        //}

        //private string titleDate = "";

        //public string TitleDate
        //{
        //    set
        //    {
        //        this.titleDate = value;
        //        this.neuLblExecTime.Text = value;
        //    }
        //}

        //private string page = "";

        //public string Page
        //{
        //    set
        //    {
        //        this.page = value;
        //        this.lblPage.Text = "第" + value + "页";
        //    }
        //}

        /// <summary>
        /// 获取医院名称和医院LOGO
        /// </summary>
        private void GetHospLogo()
        {
            string erro = "广州医学院第四附属医院";
            string imgpath = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + SOC.Local.Order.Classes.Function.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }


        private void SetItems()//构造预览页面内容
        {
            //获取LOGO
            //GetHospLogo();

            string AuthorName = ((Neusoft.HISFC.Models.Base.Employee)(Neusoft.FrameWork.Management.Connection.Operator)).Name;
            //string AuthorNO = ((Neusoft.HISFC.Models.Base.Employee)(Neusoft.FrameWork.Management.Connection.Operator)).ID;
            this.lblAuthor.Text = AuthorName;

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
                else if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 1].Text
                     != this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 2, 1].Text)//床号不同
                {
                    //this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count - 1, 1);
                }
            }

            //#region 内容

            ////只显示下面的边框  bevelBorder1---普通线   bevelBorder2---粗黑线   bevelBorder3---空白线
            //FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            //FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            //FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White, 1, false, false, false, false);

            //if (this.neuSpread1_Sheet1.Rows.Count > 0)//有数据
            //{
            //    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            //    {
            //        if (i != this.neuSpread1_Sheet1.RowCount - 1)//未完
            //        {
            //            for (int j = (int)ExecBillCols.Memo; j < (int)ExecBillCols.PrintFlag; j++)//时间点之后---普通线
            //            {
            //                this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder1;
            //            }

            //            if (this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim() == "┏" || this.neuSpread1_Sheet1.Cells[i, 3].Text == "┃")//组内用空白线
            //            {
            //                for (int j = 0; j < (int)ExecBillCols.PrintFlag; j++)//空白线
            //                {
            //                    this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder3;
            //                }
            //            }
            //            else if (this.neuSpread1_Sheet1.Cells[i, 3].Text.Trim() == "┗")//不同组---普通线
            //            {
            //                for (int j = 1; j < (int)ExecBillCols.PatientName; j++)//普通线----不起作用？
            //                {
            //                    this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder1;
            //                }
            //                this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
            //            }

            //            //“医嘱内容”行宽度180像素，容纳11个汉字，超过转为2行
            //            if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ItemName].Text.Length > 11)
            //            {
            //                this.neuSpread1_Sheet1.Rows[i].Height = 2 * this.neuSpread1_Sheet1.Rows[i].Height;
            //            }

            //            //隐藏无效行----无时间点
            //            if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.Memo].Text.Length == 0)
            //            {
            //                //this.neuSpread1_Sheet1.Rows[i].Visible = false;
            //            }

            //            if (this.neuSpread1_Sheet1.RowCount > 1)
            //            {
            //                this.neuSpread1_Sheet1.Cells[0, 1].Tag = this.neuSpread1_Sheet1.Cells[0, 1].Text;
            //                if (this.neuSpread1_Sheet1.Cells[i + 1, 1].Text == "")
            //                {
            //                    this.neuSpread1_Sheet1.Cells[i + 1, 1].Tag = this.neuSpread1_Sheet1.Cells[i, 1].Tag;
            //                }
            //                else if (this.neuSpread1_Sheet1.Cells[i + 1, 1].Text == this.neuSpread1_Sheet1.Cells[i, 1].Tag.ToString())//床号相同
            //                {
            //                    this.neuSpread1_Sheet1.Cells[i + 1, 1].Tag = this.neuSpread1_Sheet1.Cells[i, 1].Tag;
            //                    this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
            //                }
            //                else //床号不同---粗黑线
            //                {
            //                    this.neuSpread1_Sheet1.Cells[i + 1, 1].Tag = this.neuSpread1_Sheet1.Cells[i + 1, 1].Text;
            //                    this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
            //                    for (int j = 9; j < 12; j++)
            //                    {
            //                        this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder2;
            //                    }
            //                }
            //            }
            //        }
            //        else //表尾
            //        {
            //            this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
            //        }
            //    }
            //}
            //#endregion



            #region 内容

            //只显示下面的边框  bevelBorder1---普通线   bevelBorder2---粗黑线   bevelBorder3---空白线
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White, 1, false, false, false, false);

            if (this.neuSpread1_Sheet1.Rows.Count > 0)//有数据
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {

                    //“医嘱内容”行宽度180像素，容纳11个汉字，超过转为2行
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ItemName].Text.Length > 18)
                    {
                        this.neuSpread1_Sheet1.Rows[i].Height = 2 * this.neuSpread1_Sheet1.Rows[i].Height;
                        
                        /////////处理三行///////
                        //    this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName.Substring(0, length);
                        //    itemName = itemName.Substring(length);
                        //    iIndex++;
                        //    if (itemName.Length <= length)//只有两行
                        //    {
                        //        this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName;
                        //    }
                        //    else   //只能处理到三行，四行非常少见
                        //    {
                        //        this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName.Substring(0, length);
                        //        itemName = itemName.Substring(length);
                        //        iIndex++;
                        //        this.fpSpread1_Sheet1.Cells[iIndex, 0].Text = itemName;
                        //    }
                    }

                    /////初始化时全选/////
                    //this.neuSpread1_Sheet1.SetValue(i, (int)ExecBillCols.PrintFlag, this.chkAll.Checked);

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
        public void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
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
