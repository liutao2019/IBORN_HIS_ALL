using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY
{

    /// <summary>
    /// [功能描述: 门诊患者选择列表]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: ]<br></br>
    /// <修改记录>
    ///     <修改人></修改人>
    ///     <修改时间></修改时间>
    ///     <修改内容>
    ///     </修改内容>
    /// </修改记录>
    /// </summary>
    public partial class ucRegList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 构造函数

        public ucRegList()
        {
            InitializeComponent();
        }
        #endregion

        #region 变量

        public delegate void SetDataEventHandler(FS.HISFC.Models.Registration.Register regInfo);

        public event SetDataEventHandler SetDataEvent;

        private enum EnumColumns
        {
            /// <summary>
            /// 当前科室
            /// </summary>
            RegDept,

            /// <summary>
            /// 病人号
            /// </summary>
            CardNo,

            /// <summary>
            /// 姓名
            /// </summary>
            PatientName,

            /// <summary>
            /// 性别
            /// </summary>
            Sex,

            /// <summary>
            /// 年龄
            /// </summary>
            Age,

            /// <summary>
            /// 挂号日期
            /// </summary>
            RegDate,

            /// <summary>
            /// 挂号级别
            /// </summary>
            RegLevl,

            /// <summary>
            /// 看诊科室
            /// </summary>
            SeeDept,

            /// <summary>
            /// 看诊医生
            /// </summary>
            SeeDoct,

            /// <summary>
            /// 看诊时间
            /// </summary>
            SeeDate,

            /// <summary>
            /// 门诊流水号
            /// </summary>
            ClinicCode
        }
        #endregion

        #region 属性

        #endregion

        #region 方法

        public void ShowPatient(ArrayList patientList)
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            if (patientList != null && patientList.Count > 0)
            {
                FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
                for (int i = 0; i < patientList.Count; i++)
                {
                    FS.HISFC.Models.Registration.Register regInfo = patientList[i] as FS.HISFC.Models.Registration.Register;

                    this.neuSpread1_Sheet1.Rows.Add(i, 1);

                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.RegDept].Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regInfo.DoctorInfo.Templet.Dept.ID);
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.CardNo].Text = regInfo.PID.CardNO;
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.PatientName].Text = regInfo.Name;
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.Sex].Text = regInfo.Sex.Name;
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.Age].Text = dbMgr.GetAge(regInfo.Birthday);

                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.RegDate].Text = regInfo.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm");
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.RegLevl].Text = SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(regInfo.DoctorInfo.Templet.RegLevel.ID).Name;

                    if (regInfo.IsSee)
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.SeeDept].Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regInfo.SeeDoct.Dept.ID);
                        this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.SeeDoct].Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regInfo.SeeDoct.ID);
                        this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.SeeDate].Text = regInfo.SeeDoct.OperTime.ToString("yyyy-MM-dd HH:mm");
                    }
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColumns.ClinicCode].Text = regInfo.ID;

                    this.neuSpread1_Sheet1.Rows.Get(i).Tag = regInfo;
                }
            }
        }

        #endregion

        #region 事件

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || this.neuSpread1_Sheet1.RowCount == 0)
            {
                return;
            }

            if (this.SetDataEvent != null)
            {
                this.SetDataEvent(this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Registration.Register);
            }
        }

        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (this.SetDataEvent != null)
                {
                    this.SetDataEvent(neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Registration.Register);
                }
            }
        }

        #endregion
    }
}
