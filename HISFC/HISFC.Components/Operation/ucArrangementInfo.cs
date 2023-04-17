using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [功能描述: 手术安排中的患者手术信息]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-12-04]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucArrangementInfo : UserControl
    {
        public ucArrangementInfo()
        {
            InitializeComponent();
        }

        #region 属性

        public OperationAppllication OperationApplication
        {
            set
            {
                FS.HISFC.Models.Operation.OperationAppllication apply = value;

                if (apply == null)
                {
                    this.ClearSpread();
                    return;
                }
                //添加到详细显示
                //病区/床号
                this.PatientLocation = string.Concat(apply.PatientInfo.PVisit.PatientLocation.Name, "[",
                    apply.PatientInfo.PVisit.PatientLocation.Bed.ID, "]");
                
                //患者姓名、性别、年龄                
                
                int age = Environment.OperationManager.GetDateTimeFromSysDateTime().Year - apply.PatientInfo.Birthday.Year;
                if (age == 0) 
                    age = 1;
                fpSpread1_Sheet1.SetValue(0, 3, apply.PatientInfo.Name + " [" + apply.PatientInfo.Sex.Name +
                                            ", " + age.ToString() + "岁]", false);
                //住院号
                fpSpread1_Sheet1.SetValue(1, 1, apply.PatientInfo.PID.PatientNO, false);
                //手术类型
                switch (apply.OperateKind)
                {
                    case "1":
                        fpSpread1_Sheet1.SetValue(1, 3, "择期", false);
                        break;
                    case "2":
                        fpSpread1_Sheet1.SetValue(1, 3, "急诊", false);
                        break;
                    case "3":
                        fpSpread1_Sheet1.SetValue(1, 3, "感染", false);
                        break;
                }
                //手术台类型
                switch (apply.TableType)
                {
                    case "1":
                        fpSpread1_Sheet1.SetValue(2, 1, "正台", false);
                        break;
                    case "2":
                        fpSpread1_Sheet1.SetValue(2, 1, "加台", false);
                        break;
                    case "3":
                        fpSpread1_Sheet1.SetValue(2, 1, "点台", false);
                        break;
                }
                //
                ////TODO: 术前诊断
                if (apply.DiagnoseAl != null && apply.DiagnoseAl.Count > 0)
                    fpSpread1_Sheet1.SetValue(2, 3, (apply.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10.Name, false);
                else
                    fpSpread1_Sheet1.SetValue(2, 3, "", false);
                ////手术名称
                fpSpread1_Sheet1.SetValue(3, 1, apply.MainOperationName);
                //手术时间
                fpSpread1_Sheet1.SetValue(3, 3, apply.PreDate.ToString(), false);
                //麻醉方式
                //fpSpread1_Sheet1.SetValue(4, 1, apply.AnesType.Name, false);
                if (apply.AnesType.ID != null && apply.AnesType.ID != "")
                {
                    int le = apply.AnesType.ID.IndexOf("|");
                    if (le > 0)
                    {
                        FS.FrameWork.Models.NeuObject obj = Environment.GetAnes(apply.AnesType.ID.Substring(0,le).ToString());
                        string anesName = string.Empty;
                        if (obj != null)
                        {
                            anesName = obj.Name;
                        }
                        obj = Environment.GetAnes(apply.AnesType.ID.Substring(le+1).ToString());
                        if (obj != null)
                        {
                            anesName += "|"+obj.Name;
                        }
                        fpSpread1_Sheet1.SetValue(4, 1, anesName, false);
                    }
                    else
                    {
                        FS.FrameWork.Models.NeuObject obj = Environment.GetAnes(apply.AnesType.ID.ToString());

                        if (obj != null)
                        {
                            fpSpread1_Sheet1.SetValue(4, 1, obj.Name, false);

                        }
                    }
                }
                //申请医生
                fpSpread1_Sheet1.SetValue(4, 3, apply.ApplyDoctor.Name, false);
                //手术医生
                if (apply.OperationDoctor != null)
                    fpSpread1_Sheet1.SetValue(5, 1, apply.OperationDoctor.Name, false);
                else
                    fpSpread1_Sheet1.SetValue(5, 1, "", false);
                //是否特殊手术

                string txt = "";
                if (apply.BloodNum == 0)
                { 
                    txt = "否"; 
                }
                else if (apply.BloodNum == 1)
                { 
                    txt = "HAV"; 
                }
                else if (apply.BloodNum == 2)
                { 
                    txt = "HBV"; 
                }
                else if (apply.BloodNum == 3)
                { 
                    txt = "HCV"; 
                }
                else if (apply.BloodNum == 4)
                { 
                    txt = "HDV"; 
                }
                else if (apply.BloodNum == 5)
                { 
                    txt = "HIV"; 
                }
                else if (apply.BloodNum == 6)
                { 
                    txt = "其他"; 
                }
                fpSpread1_Sheet1.SetValue(5, 3, txt, false);

                //助手
                string Helper1 = string.Empty;
                string Helper2 = string.Empty;
                string Helper3 = string.Empty;
                foreach (FS.HISFC.Models.Operation.ArrangeRole roleInfo in apply.HelperAl)
                {
                    if (roleInfo.RoleType.ID.ToString() == "Helper1")
                    {
                        Helper1 = roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == "Helper2")
                    {
                        Helper2 = roleInfo.Name;
                    }
                    else if (roleInfo.RoleType.ID.ToString() == "Helper3")
                    {
                        Helper3 = roleInfo.Name;
                    }
                }
                if (Helper1 != string.Empty && Helper2 != string.Empty && Helper3 != string.Empty)
                {
                    fpSpread1_Sheet1.SetValue(6, 1, Helper1 + "|" + Helper2 + "|" + Helper3, false);
                }
                else if (Helper1 != string.Empty && Helper2 != string.Empty && Helper3 == string.Empty)
                {
                    fpSpread1_Sheet1.SetValue(6, 1, Helper1 + "|" + Helper2 , false);
                }
                else if (Helper1 != string.Empty && Helper2 == string.Empty && Helper3 == string.Empty)
                {
                    fpSpread1_Sheet1.SetValue(6, 1, Helper1 , false);
                }
                else if (Helper1 == string.Empty)
                {
                    fpSpread1_Sheet1.SetValue(6, 1, "", false);
                }

                //if (apply.HelperAl != null && apply.HelperAl.Count != 0)
                //    fpSpread1_Sheet1.SetValue(6, 1, (apply.HelperAl[0] as FS.HISFC.Models.Base.Employee).Name, false);
                //else
                //    fpSpread1_Sheet1.SetValue(6, 1, "", false);
                //备注
                fpSpread1_Sheet1.SetValue(6, 3, apply.ApplyNote, false);

                #region sunm0917
                this.PatientIDCard = apply.PatientInfo.IDCard;
                this.PatientAddress = apply.PatientInfo.AddressHome;
                #endregion
                if (apply.IsOlation == "1")
                {
                    this.fpSpread1_Sheet1.Cells[9, 1].Text = "是";
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[9, 1].Text = "否";
                }
                this.fpSpread1_Sheet1.Cells[8, 3].Text = apply.LastTime.Replace("|","");
                this.fpSpread1_Sheet1.Cells[8, 1].Text = apply.Position;

                fpSpread1_Sheet1.Tag = apply;

            }
        }

        //路志鹏　时间：２００６-４-１６
        //目的：当手术申请单实体OperationAppllication为null时清空fpSpread1_Sheet1的内容
        private void ClearSpread()
        {
            try
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, 1].Text = "";
                    this.fpSpread1_Sheet1.Cells[i, 3].Text = "";
                }
            }
            catch { }
        }

        /// <summary>
        /// 病区/床号
        /// </summary>
        public string PatientLocation
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[0, 1].Text = value;
            }
        }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[0, 3].Text = value;
            }
        }

        /// <summary>
        /// 患者住院号
        /// </summary>
        public string PatientID
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[1, 1].Text = value;
            }
        }

        /// <summary>
        /// 手术类型
        /// </summary>
        public string OperationType
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[1, 3].Text = value;
            }
        }


        /// <summary>
        /// 手术台类型
        /// </summary>
        public string TableType
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[2, 1].Text = value;
            }
        }
        
        /// <summary>
        /// 术前诊断
        /// </summary>
        public string Diagnosis
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[2, 3].Text = value;
            }
        }

        /// <summary>
        /// 手术名称
        /// </summary>
        public string OperationName
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[3, 1].Text = value;
            }
        }

        /// <summary>
        /// 手术时间
        /// </summary>
        public string OperationTime
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[3, 3].Text = value;
            }
        }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string AnseType
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[4, 1].Text = value;
            }
        }

        /// <summary>
        /// 申请医生
        /// </summary>
        public string ApplyDoctorName
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[4, 3].Text = value;
            }
        }

        /// <summary>
        /// 手术医生
        /// </summary>
        public string OperatorName
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[5, 1].Text = value;
            }
        }

        /// <summary>
        /// 是否特殊手术
        /// </summary>
        public bool IsSpecialOperation
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[5, 3].Value = value;
            }
        }

        /// <summary>
        /// 助手医生
        /// </summary>
        public string HelperDoctorName
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[6, 1].Text = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[6, 3].Text = value;
            }
        }

        #region sunm0917

        /// <summary>
        /// 身份证号
        /// </summary>
        public string PatientIDCard
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[7, 1].Text = value;
            }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public string PatientAddress
        {
            set
            {
                this.fpSpread1_Sheet1.Cells[7, 3].Text = value;
            }
        }

        #endregion

        #endregion
    }
}
