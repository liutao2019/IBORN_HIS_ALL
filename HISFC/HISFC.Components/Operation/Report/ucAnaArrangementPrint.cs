using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation.Report
{
    /// <summary>
    /// 手术一览表打印
    /// </summary>
    public partial class ucAnaArrangementPrint : UserControl,FS.HISFC.BizProcess.Interface.Operation.IArrangePrint
    {
        /// <summary>
        /// 手术一览表打印
        /// </summary>
        public ucAnaArrangementPrint()
        {
            InitializeComponent();
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].Height = 28;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;
        }

        #region 字段

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType arrangeType = FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType.Anaesthesia;


        #endregion

        #region 属性

        public string Title
        {
            set
            {
                this.lblTitle.Text = value;
            }
        }
        public DateTime Date
        {
            set
            {
                this.lblDate.Text = string.Concat("截止", value.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }
        #endregion

        #region IReportPrinter 成员

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public int Export()
        {
            return 0;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            return 0;
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        public int PrintPreview()
        {
            this.print.IsLandScape = true;
            return this.print.PrintPreview(10, 0, this);
        }

        #endregion


        #region IArrangePrint 成员

        /// <summary>
        /// 添加申请单
        /// </summary>
        /// <param name="appliction"></param>
        public void AddAppliction(FS.HISFC.Models.Operation.OperationAppllication appliction)
        {
            if (appliction == null)
                return;

            this.fpSpread1_Sheet1.RowCount += 1;
            int row = this.fpSpread1_Sheet1.RowCount - 1;
            this.fpSpread1_Sheet1.Rows[row].Height = 50;
            this.fpSpread1_Sheet1.Rows[row].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Rows[row].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.fpSpread1_Sheet1.Cells[row, 0].Text = appliction.OpePos.Memo;//appliction.RoomID;//手术间
            //this.fpSpread1_Sheet1.Cells[row, 0].Text = appliction.RoomID;//手术间
            this.fpSpread1_Sheet1.Cells[row, 1].Text = appliction.PreDate.ToShortTimeString();//手术时间
            this.fpSpread1_Sheet1.Cells[row, 2].Text = appliction.OpsTable.Name;//台序
            this.fpSpread1_Sheet1.Cells[row, 3].Text = appliction.PatientInfo.PVisit.PatientLocation.NurseCell.Name;//患者所在病区
            this.fpSpread1_Sheet1.Cells[row, 4].Text = appliction.PatientInfo.PVisit.PatientLocation.Dept.Name;//患者所在科室
            this.fpSpread1_Sheet1.Cells[row, 5].Text = appliction.PatientInfo.Name;//姓名
            this.fpSpread1_Sheet1.Cells[row, 6].Text = appliction.PatientInfo.Sex.Name;//性别
            this.fpSpread1_Sheet1.Cells[row, 7].Text = appliction.PatientInfo.Age.ToString();//年龄
            this.fpSpread1_Sheet1.Cells[row, 8].Text = appliction.PatientInfo.PVisit.PatientLocation.Bed.Name;//床号
            this.fpSpread1_Sheet1.Cells[row, 9].Text = appliction.PatientInfo.PID.PatientNO;//住院号
            //术前诊断
            //if (appliction.DiagnoseAl.Count > 0)
            //    this.fpSpread1_Sheet1.Cells[row, 7].Text = (appliction.DiagnoseAl[0] as FS.FrameWork.Models.NeuObject).Name;

            string forediagnose = string.Empty;

            for (int t = 0; t < appliction.DiagnoseAl.Count; ++t)
            {
                forediagnose += "诊断" + (t + 1).ToString() + ":" + appliction.DiagnoseAl[t].ToString() + "  ";
            }
            //forediagnose = apply.DiagnoseAl[0] + "\r\n" + apply.DiagnoseAl[1] + "\r\n" + apply.DiagnoseAl[2];
            fpSpread1_Sheet1.Cells[row, 10].Text = forediagnose;

            //手术名称
            this.fpSpread1_Sheet1.Cells[row, 11].Text = appliction.MainOperationName;

            //是否特殊手术
            if (appliction.IsSpecial)
            {
                //this.fpSpread1_Sheet1.Cells[row, 12].Text = "是";
                if (appliction.BloodNum.ToString() == "1")
                {
                    this.fpSpread1_Sheet1.Cells[row, 12].Text = "HAV";
                }
                if (appliction.BloodNum.ToString() == "2")
                {
                    this.fpSpread1_Sheet1.Cells[row, 12].Text = "HBV";
                }
                if (appliction.BloodNum.ToString() == "3")
                {
                    this.fpSpread1_Sheet1.Cells[row, 12].Text = "HCV";
                }
                if (appliction.BloodNum.ToString() == "4")
                {
                    this.fpSpread1_Sheet1.Cells[row, 12].Text = "HDV";
                }
                if (appliction.BloodNum.ToString() == "5")
                {
                    this.fpSpread1_Sheet1.Cells[row, 12].Text = "HIV";
                }
                if (appliction.BloodNum.ToString() == "6")
                {
                    this.fpSpread1_Sheet1.Cells[row, 12].Text = "其他";
                }
            }
            else
            {
                this.fpSpread1_Sheet1.Cells[row, 12].Text = "否";
            }

            //特殊说明
            this.fpSpread1_Sheet1.Cells[row, 13].Text = appliction.ApplyNote;

            //手术麻醉
            
            if (appliction.AnesType.ID != null && appliction.AnesType.ID != "")
            {
                //FS.HISFC.Models. NeuObject obj = new NeuObject();
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                int le = appliction.AnesType.ID.IndexOf("|");
                if (le > 0)
                {
                    obj = Environment.GetAnes(appliction.AnesType.ID.Substring(0, le));
                    if (obj != null)
                    {
                        //fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType, obj.Name, false);
                        //fpSpread1_Sheet1.Columns[(int)Cols.anaeType].Locked = false;
                     //   this.fpSpread1_Sheet1.Cells[row, 11].Text = obj.name;
                        fpSpread1_Sheet1.SetValue(row, 16, obj.Name, false);
                         
                    }
                    obj = Environment.GetAnes(appliction.AnesType.ID.Substring(le + 1));
                    if (obj != null)
                    {
                        //fpSpread1_Sheet1.SetValue(row, (int)Cols.anaeType2, obj.Name, false);
                        //fpSpread1_Sheet1.Columns[(int)Cols.anaeType2].Locked = false;
                        fpSpread1_Sheet1.SetValue(row, 17, obj.Name, false);
                         
                    }
                }
                else
                {
                    obj = Environment.GetAnes(appliction.AnesType.ID.ToString());
                    if (obj != null)
                    {
                        fpSpread1_Sheet1.SetValue(row, 11, obj.Name, false);
                        fpSpread1_Sheet1.SetTag(row, 16, obj);                       
                    }
                }                
            }


            //洗手护士
            string WashingNurse1 = string.Empty;
            string WashingNurse2 = string.Empty;
            string WashingNurse3 = string.Empty;
            int washSum = 0;
            string itinerantNurse1 = string.Empty;
            string itinerantNurse2 = string.Empty;
            string itinerantNurse3 = string.Empty;
            int itinerantSum = 0;
            string operatorDoc = string.Empty;
            string operatorDoc1 = string.Empty;
            string operatorDoc2 = string.Empty;
            string operatorDoc3 = string.Empty;
            int operDoc = 0;
            string AnaesthetistDoc1 = string.Empty;
            string AnaesthetistDoc2 = string.Empty;
            string AnaesthetistDoc3 = string.Empty;
            string AnaesthetistDoc4 = string.Empty;

            //接班人员
            string ShiftDoctor = string.Empty;

            //一助手
            string helper1 = string.Empty;

            foreach (FS.HISFC.Models.Operation.ArrangeRole role in appliction.RoleAl)
            {
                if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Operator.ToString())
                {
                    operatorDoc = role.Name + "\n";
                    operDoc++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString())
                {
                    operatorDoc1 = role.Name + "\n";
                    operDoc++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString())
                {
                    operatorDoc2 = role.Name + "\n";
                    operDoc++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper3.ToString())
                {
                    operatorDoc3 = role.Name + "\n";
                    operDoc++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.WashingHandNurse1.ToString())
                {
                    WashingNurse1 = role.Name + "\n";
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.WashingHandNurse2.ToString())
                {
                    WashingNurse2 = role.Name + "\n";
                    washSum++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.WashingHandNurse3.ToString())
                {
                    WashingNurse3 = role.Name;
                    washSum++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.ItinerantNurse1.ToString())
                {
                    itinerantNurse1 = role.Name + "\n";
                    washSum++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.ItinerantNurse2.ToString())
                {
                    itinerantNurse2 = role.Name + "\n";
                    itinerantSum++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.ItinerantNurse3.ToString())
                {
                    itinerantNurse3 = role.Name;
                    itinerantSum++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Anaesthetist.ToString())
                {
                    AnaesthetistDoc1 = role.Name + "\n";
                    itinerantSum++;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.AnaesthesiaHelper.ToString())
                {
                    AnaesthetistDoc2 = role.Name;
                }
                else if(role.RoleType.ID.ToString()==FS.HISFC.Models.Operation.EnumOperationRole.AnaesthesiaHelper1.ToString())
                {
                    AnaesthetistDoc3=role.Name;
                }
                else if(role.RoleType.ID.ToString()==FS.HISFC.Models.Operation.EnumOperationRole.AnaesthesiaHelper2.ToString())
                {
                    AnaesthetistDoc4=role.Name;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.OpsShiftDoctor.ToString())
                {
                    ShiftDoctor = role.Name;
                }
                else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString())
                {
                    helper1 = role.Name;
                }
            }
            if (operDoc == 4)
            {
                this.fpSpread1_Sheet1.Rows[row].Height = 60;
            }
            if (operDoc == 3 || washSum == 3 || itinerantSum == 3)
            {
                this.fpSpread1_Sheet1.Rows[row].Height = 45;
            }
            this.fpSpread1_Sheet1.Cells[row, 14].Text = operatorDoc;//手术医生
            this.fpSpread1_Sheet1.Cells[row, 15].Text = operatorDoc1; //+ operatorDoc2 + operatorDoc3;//助手
            //this.fpSpread1_Sheet1.Cells[row, 11].Text = WashingNurse1 + WashingNurse2 + WashingNurse3;//洗手护士
            //this.fpSpread1_Sheet1.Cells[row, 12].Text = itinerantNurse1 + itinerantNurse2 + itinerantNurse3;//巡回护士
            this.fpSpread1_Sheet1.Cells[row, 18].Text = AnaesthetistDoc1;//麻醉医生--主麻
            this.fpSpread1_Sheet1.Cells[row, 19].Text = AnaesthetistDoc2;
            this.fpSpread1_Sheet1.Cells[row, 20].Text = AnaesthetistDoc3;
            this.fpSpread1_Sheet1.Cells[row, 21].Text = AnaesthetistDoc4;
            this.fpSpread1_Sheet1.Cells[row, 23].Text = ShiftDoctor;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Reset()
        {
            this.fpSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 安排类型
        /// </summary>
        public FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType ArrangeType
        {
            get
            {
                return this.arrangeType;
            }
            set
            {
                this.arrangeType = value;
            }
        }

        #endregion
    }
}
