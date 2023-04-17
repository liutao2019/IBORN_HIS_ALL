using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Account.Forms
{
    public partial class frmQueryPatientByIdenno : Form
    {
        public frmQueryPatientByIdenno()
        {
            InitializeComponent();
        }
        #region 变量
        /// <summary>
        /// 如初转业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 当选择单条患者信息
        /// </summary>
        public delegate void GetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// 当选择单条患者信息后触发
        /// </summary>
        public event GetPatient SelectedPatient;

        /// <summary>
        /// 患者信息实体
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 患者身份证号
        /// </summary>
        string idenNO = string.Empty;

        /// <summary>
        /// 符合条件的患者信息条目
        /// </summary>
        private int personCount;
        #endregion

        #region 属性
        /// <summary>
        /// 患者身份证号
        /// </summary>
        public string IdenNO
        {
            get
            {
                return this.idenNO;
            }
            set
            {
                try
                {
                    this.idenNO = value;
                    if (this.idenNO == string.Empty || this.idenNO == null)
                    {
                        return;
                    }
                    //根据idenNO 获得符合条件的患者信息
                    FillPatientInfoByIdenNO();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + this.Name.ToString());
                }
            }
        }

        /// <summary>
        /// 符合条件的患者信息条目
        /// </summary>
        public int PersonCount
        {
            get
            {
                return this.personCount;
            }
        }

        /// <summary>
        /// 选定的患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 将符合条件的患者信息显示在列表中，如果只有一条，则不显示该控件，直接赋值患者信息实体
        /// </summary>
        private void FillPatientInfoByIdenNO()
        {
            ArrayList patients = this.radtIntegrate.QueryComPatientInfoListByIDNO(this.idenNO);

            DisplayPatients(patients);
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// <param name="patients">查询出来的患者列表</param>
        private void DisplayPatients(ArrayList patients)
        {
            //获得患者基本信息出错
            if (patients == null)
            {
                this.personCount = 0;
                this.patientInfo = null;
                this.SelectedPatient(null);

                return;
            }
            //没有找到符合条件的患者信息
            if (patients.Count == 0)
            {
                this.personCount = 0;
                this.patientInfo = null;
                this.SelectedPatient(null);

                return;
            }
            //如果只找到一个符合条件的患者信息，那么不显示控件，直接返回患者的挂号实体
            if (patients.Count == 1)
            {
                this.personCount = 1;
                this.patientInfo = patients[0] as FS.HISFC.Models.RADT.PatientInfo;
                this.SelectedPatient(this.patientInfo);

                return;
            }
            //如果有多个符合条件的患者信息，在控件的列表中显示基本信息，患者信息实体邦定在改行的tag属性中
            this.fpSpread1_Sheet1.RowCount = 1; //默认只有一行
            this.personCount = patients.Count;
            this.fpSpread1_Sheet1.RowCount = personCount;
            FS.HISFC.Models.RADT.PatientInfo patient = null;

            int index = 0;
            for (int i = personCount - 1; i >= 0; i--)
            {
                patient = patients[i] as FS.HISFC.Models.RADT.PatientInfo;

                this.fpSpread1_Sheet1.Cells[index, 0].Text = patient.PID.CardNO;
                this.fpSpread1_Sheet1.Cells[index, 1].Text = patient.Name;
                this.fpSpread1_Sheet1.Cells[index, 2].Text = patient.Sex.Name;
                this.fpSpread1_Sheet1.Cells[index, 3].Text = patient.PhoneHome;
                this.fpSpread1_Sheet1.Cells[index, 4].Text = patient.Kin.RelationPhone;
                this.fpSpread1_Sheet1.Cells[index, 5].Text = patient.AddressHome;

                this.fpSpread1_Sheet1.Rows[index].Tag = patient;
                this.fpSpread1_Sheet1.Rows[index].Height = 30F;
                this.fpSpread1_Sheet1.Rows[index].Font = new System.Drawing.Font("宋体", 14F);

                index++;
            }
            this.ShowDialog();
        }
        /// <summary>
        /// 按患者身份证查询患者信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public void FillPatientInfoByIdenNO(string patientIdenNO)
        {
            ArrayList patients = this.radtIntegrate.QueryComPatientInfoListByIDNO(patientIdenNO);
            DisplayPatients(patients);
        }
        /// <summary>
        /// 按患者姓名查询患者挂号信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int QueryByName(ArrayList al, string idenNO)
        {
            //FS.HISFC.BizProcess.Integrate.RADT Mgr = new FS.HISFC.BizProcess.Integrate.RADT();
            //ArrayList al = Mgr.QueryPatientByName(Name);

            if (al == null)
            {
                MessageBox.Show("查询患者信息时出错!");
                return -1;
            }
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            foreach (FS.HISFC.Models.RADT.PatientInfo obj in al)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.SetValue(row, 0, obj.PID.CardNO, false);
                this.fpSpread1_Sheet1.SetValue(row, 1, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, 2, obj.Sex.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, 3, obj.PhoneHome, false);
                this.fpSpread1_Sheet1.SetValue(row, 4, obj.Kin.RelationPhone, false);
                this.fpSpread1_Sheet1.SetValue(row, 5, obj.AddressHome, false);
            }
            return 0;
        }

        /// <summary>
        /// 获取选择的患者病历号
        /// </summary>
        public string SelectedCardNo
        {
            get
            {
                if (this.fpSpread1_Sheet1.RowCount == 0) return "";

                int Row = this.fpSpread1_Sheet1.ActiveRowIndex;

                return this.fpSpread1_Sheet1.GetText(Row, 0);
            }
        }
        #endregion

        #region 事件
        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.Rows.Count > 0)
                {
                    if (this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag != null)
                    {
                        this.SelectPatient(this.fpSpread1_Sheet1.ActiveRowIndex);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
        }

        /// <summary>
        /// 双击FP事件,选择当前患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || this.fpSpread1_Sheet1.RowCount == 0) return;
            int row = e.Row;
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                if (this.fpSpread1_Sheet1.Rows[row].Tag != null)
                {
                    this.SelectPatient(row);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        /// <summary>
        /// 双击，回车等选择患者
        /// </summary>
        /// <param name="row">当前行</param>
        private void SelectPatient(int row)
        {
            if (this.SelectedPatient != null)
            {
                this.SelectedPatient((FS.HISFC.Models.RADT.PatientInfo)this.fpSpread1_Sheet1.Rows[row].Tag);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                if (this.SelectedPatient != null)
                {
                    this.SelectedPatient(null);
                }
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 当控件获得焦点的时候,让FP获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmQueryPatientByName_Enter(object sender, EventArgs e)
        {
            this.fpSpread1.Focus();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0
                || this.fpSpread1_Sheet1.SelectionCount == 0)
            {
                return;
            }
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                if (this.fpSpread1_Sheet1.Rows[row].Tag != null)
                {
                    this.SelectPatient(row);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0
                || this.fpSpread1_Sheet1.SelectionCount == 0)
            {
                return;
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
