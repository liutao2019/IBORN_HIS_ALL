using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Operation
{
    public partial class frmApplyMZList : Form
    {
        //{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
        public frmApplyMZList()
        {
            InitializeComponent();
        }

        FS.HISFC.Models.RADT.PatientInfo p;
        private List<FS.HISFC.Models.Operation.OperationAppllication> applylist;
        FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
        public FS.HISFC.Models.Operation.OperationAppllication operation;
        public int  applynum=0;

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        public frmApplyMZList(FS.HISFC.Models.RADT.PatientInfo p2)
        {
            InitializeComponent();
            this.p = p2;
            Init();
        }

        private void Init()
        {
            LoadData();
        }

        /// <summary>
        /// 载入数据
        /// </summary>
        private void LoadData()
        {
            ArrayList applylist = Environment.OperationManager.GetOpsAppListByCardNo(this.p.PID.CardNO.ToString()); ;//
          applynum=   applylist.Count;
            if (applylist == null)
            {
                MessageBox.Show("查询手术失败");
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            foreach (FS.HISFC.Models.Operation.OperationAppllication apply in applylist)
            {
                this.SetApplyToDataTable(apply);
            }

        }

        /// <summary>
        /// 将手术信息放入数据表
        /// </summary>
        /// <param name="apply"></param>
        private void SetApplyToDataTable(FS.HISFC.Models.Operation.OperationAppllication apply)
        {
            neuSpread1_Sheet1.Rows.Add(neuSpread1_Sheet1.RowCount, 1);
            int row = neuSpread1_Sheet1.RowCount - 1;
            neuSpread1_Sheet1.Rows[row].Tag = apply;
            //床号 add by zhy
            neuSpread1_Sheet1.SetValue(row, (int)Cols.bedID, apply.PatientInfo.PVisit.PatientLocation.Bed.Name, false);
            //住院号
            neuSpread1_Sheet1.SetValue(row, (int)Cols.patientNO, apply.PatientInfo.PID.PatientNO, false);
            //患者姓名
            neuSpread1_Sheet1.SetValue(row, (int)Cols.Name, apply.PatientInfo.Name, false);
            //性别
            neuSpread1_Sheet1.SetValue(row, (int)Cols.Sex, apply.PatientInfo.Sex.Name, false);
            //年龄
            int age = this.dataManager.GetDateTimeFromSysDateTime().Year - apply.PatientInfo.Birthday.Year;
            if (age == 0) age = 1;
            neuSpread1_Sheet1.SetValue(row, (int)Cols.Age, age.ToString(), false);

            //主手术名称            
            neuSpread1_Sheet1.SetValue(row, (int)Cols.opItemName, apply.MainOperationName, false);
            //手术时间
            neuSpread1_Sheet1.SetValue(row, (int)Cols.opDate, apply.PreDate, false);

            //手术医生
            neuSpread1_Sheet1.SetValue(row, (int)Cols.opDoctID, apply.OperationDoctor.Name, false);
            //手术备注
            neuSpread1_Sheet1.SetValue(row, (int)Cols.Memo, apply.ApplyNote, false);
        }

        #region columns 数据列
        private enum Cols
        {
            //
            /// <summary>
            /// 是否选择
            /// </summary>
            isSelect,//0

            /// <summary>
            /// 住院号
            /// </summary>
            patientNO,

            /// <summary>
            /// 姓名
            /// </summary>
            Name,

            /// <summary>
            /// 性别
            /// </summary>
            Sex,

            /// <summary>
            /// 床号
            /// </summary>
            bedID,

            /// <summary>
            /// 年龄
            /// </summary>
            Age,

            /// <summary>
            /// 手术名称
            /// </summary>
            opItemName,

            /// <summary>
            /// 手术时间
            /// </summary>
            opDate,

            /// <summary>
            /// 手术医生
            /// </summary>
            opDoctID,

            /// <summary>
            /// 备注
            /// </summary>
            Memo
        }
        #endregion


        private void neuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {

            this.operation = neuSpread1_Sheet1.Rows[neuSpread1_Sheet1.ActiveRow.Index].Tag as FS.HISFC.Models.Operation.OperationAppllication;
            this.Close();
        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            for (int i = 0; i <neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (i == neuSpread1_Sheet1.ActiveRow.Index)
                {
                    neuSpread1_Sheet1.SetValue(neuSpread1_Sheet1.ActiveRow.Index, (int)Cols.isSelect, true, false);
                }
                else
                {
                    neuSpread1_Sheet1.SetValue(i, (int)Cols.isSelect, false, false);
                }

            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.operation = neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Operation.OperationAppllication;
            this.Close();
        }

    }
}
