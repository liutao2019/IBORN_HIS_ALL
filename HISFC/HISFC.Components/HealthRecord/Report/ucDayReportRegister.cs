using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Report
{
    /// <summary>
    /// [��������: �����ձ�ά��]<br></br>
    /// [�� �� ��: ��ȫ]<br></br>
    /// [����ʱ��: 2007-09-17]<br></br>
    /// 
    /// <�޸ļ�¼
    ///		�޸��� =
    ///		�޸�ʱ�� =
    ///		�޸�Ŀ�� =
    ///		�޸����� =
    ///  />
    /// </summary>
    public partial class ucDayReportRegister : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayReportRegister()
        {
            InitializeComponent();
        }

        #region ����

        private DataTable dtDayReport = null;
        private DataView dvDayReport = null;
        private DateTime dtTime = DateTime.Now.Date;
        
        //��־�ձ��Ƿ�洢�����ݿ��� true - �Ѵ洢, false - δ�洢
        private bool hasRecord;

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��DateTable�ֶ�
        /// </summary>
        private void InitDataSet()
        {
            this.dtDayReport = new DataTable("DayReportRegister");

            this.dtDayReport.Columns.AddRange(new DataColumn[]
			{
                new DataColumn("DateStat",typeof(DateTime)),
                new DataColumn("DeptCode",typeof(string)),
                new DataColumn("DeptName",typeof(string)),
                new DataColumn("ClinicNum",typeof(int)),
                new DataColumn("EmcNum",typeof(int)),
                new DataColumn("EmcDeadNum",typeof(int)),
                new DataColumn("ObserveNum",typeof(int)),
                new DataColumn("ObserveDeadNum",typeof(int)),
                new DataColumn("ReDiagnoseNum",typeof(int)),
                new DataColumn("ClcDiagnoseNum",typeof(int)),
                new DataColumn("SpecialNum",typeof(int)),
                new DataColumn("HosInsuranceNum",typeof(int)),
                new DataColumn("BdCheckNum",typeof(int))
			});
        }

        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private void InitFrp(DateTime dateTime)
        {

            FS.HISFC.BizLogic.HealthRecord.DayReportRegister dayReport = new FS.HISFC.BizLogic.HealthRecord.DayReportRegister();
            ArrayList al = new ArrayList();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ��������Ժ�...");
            Application.DoEvents();

            al = dayReport.QueryByStatTime(dateTime);
            this.hasRecord = true;

           /*��������ΪĬ�ϳ�ʼ��farpoint
            if (al.Count == 0)
            {
                this.hasRecord = false;
                al = dayReport.QueryAllDept(dateTime);
            }
            else
            {
                this.hasRecord = true;
            }
            */
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //���ԭ������
            this.dtDayReport.Rows.Clear();

            FS.HISFC.Models.HealthRecord.DayReportRegister regReport = new FS.HISFC.Models.HealthRecord.DayReportRegister();

            foreach (object obj in al)
            {
                regReport = obj as FS.HISFC.Models.HealthRecord.DayReportRegister;

                this.dtDayReport.Rows.Add(new object[]
                {
                    regReport.DateStat,
                    regReport.Dept.ID,
                    regReport.Dept.Name,
                    regReport.ClinicNum,
                    regReport.EmcNum,
                    regReport.EmcDeadNum,
                    regReport.ObserveNum,
                    regReport.ObserveDeadNum,
                    regReport.ReDiagnoseNum,
                    regReport.ClcDiagnoseNum,
                    regReport.SpecialNum,
                    regReport.HosInsuranceNum,
                    regReport.BdCheckNum
                });
            }

            this.dtDayReport.AcceptChanges();
            this.dvDayReport = this.dtDayReport.DefaultView;
            this.dvDayReport.AllowDelete = true;
            this.dvDayReport.AllowEdit = true;
            this.dvDayReport.AllowNew = true;
            this.neuSpread1_Sheet1.DataSource = this.dvDayReport;
            this.neuSpread1_Sheet1.DataMember = "DayReportRegister";

            this.SetFpFormat();
        }

        /// <summary>
        /// ����Farpoint��ʾ��ʽ
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.DecimalPlaces = 0;
            numbCellType.MaximumValue = 9999;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.DateTimeCellType dtCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            dtCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dtCellType.UserDefinedFormat = "yyyy-MM-dd";
            dtCellType.ReadOnly = true;

            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;

            #region ����ÿ�е�����

            this.neuSpread1_Sheet1.Columns[0].CellType = dtCellType;
            this.neuSpread1_Sheet1.Columns[0].BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[0].Label = "����";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[0].Width = 90f;
            this.neuSpread1_Sheet1.Columns[1].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[1].Visible = false;
            this.neuSpread1_Sheet1.Columns[2].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[2].BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[2].Label = "��������";
            this.neuSpread1_Sheet1.Columns[3].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[3].Label = "��������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[3].Width = 90f;
            this.neuSpread1_Sheet1.Columns[4].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[4].Label = "��������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[4].Width = 90f;
            this.neuSpread1_Sheet1.Columns[5].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[5].Label = "������������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[5].Width = 90f;
            this.neuSpread1_Sheet1.Columns[6].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[6].Label = "�۲�����";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[6].Width = 90f;
            this.neuSpread1_Sheet1.Columns[7].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[7].Label = "�۲���������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[7].Width = 90f;
            this.neuSpread1_Sheet1.Columns[8].CellType = numbCellType;
            this.neuSpread1_Sheet1.Columns[8].Visible = false;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[8].Label = "��������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[8].Width = 90f;
            this.neuSpread1_Sheet1.Columns[9].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[9].Label = "�������������˴���";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[9].Width = 110f;
            this.neuSpread1_Sheet1.Columns[10].CellType = numbCellType;
            this.neuSpread1_Sheet1.Columns[10].Visible = false;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[10].Label = "ר����������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[10].Width = 90f;
            this.neuSpread1_Sheet1.Columns[11].CellType = numbCellType;
            this.neuSpread1_Sheet1.Columns[11].Visible = false;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[11].Label = "ҽ����������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[11].Width = 90f;
            this.neuSpread1_Sheet1.Columns[12].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[12].Label = "��콡���������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[12].Width = 110f;

            #endregion

        }

        /// <summary>
        /// �ж�farpoint�����Ƿ��޸� true - �޸�, false - δ�޸�
        /// </summary>
        /// <returns></returns>
        private bool IsChange()
        {
            this.neuSpread1.StopCellEditing();

            if (neuSpread1_Sheet1.RowCount > 0)
            {
                this.dtDayReport.Rows[neuSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }

            DataTable dt = this.dtDayReport.GetChanges();
            
            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// �������� 1 ����ɹ�, -1 ����ʧ��
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (!this.IsChange()) return 1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.HealthRecord.DayReportRegister regReportMrg = new FS.HISFC.BizLogic.HealthRecord.DayReportRegister();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            ArrayList al = new ArrayList();

            //t.BeginTransaction();
            //regReportMrg.SetTrans(t.Trans);

            if (!this.hasRecord)
            {
                //��������
                al = this.GetList(this.dtDayReport);

                if (al == null) return -1;

                if (regReportMrg.InsertOpdDayReport(al) < 0)
                {
                    MessageBox.Show("�������ݳ���", "��ʾ");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            else
            {
                //��������
                DataTable dtChange = this.dtDayReport.GetChanges();

                al = this.GetList(dtChange);

                if (al == null) return -1;

                if (regReportMrg.UpdateOpdDayReport(al) < 0)
                {
                    MessageBox.Show("�������ݳ���", "��ʾ");
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.dtDayReport.AcceptChanges();
            this.SetFpFormat();

            MessageBox.Show("����ɹ���", "��ʾ");

            return 1;
        }

        /// <summary>
        /// �õ����ݱ��еĶ���
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns>����</returns>
        private ArrayList GetList(DataTable dataTable)
        {
            ArrayList arrayList = new ArrayList();

            try
            {
                foreach (DataRow dtRow in dataTable.Rows)
                {
                    FS.HISFC.Models.HealthRecord.DayReportRegister regReport = new FS.HISFC.Models.HealthRecord.DayReportRegister();

                    regReport.DateStat = DateTime.Parse(dtRow["DateStat"].ToString());
                    regReport.Dept.ID = dtRow["DeptCode"].ToString();
                    regReport.Dept.Name = dtRow["DeptName"].ToString();
                    regReport.ClinicNum = int.Parse(dtRow["ClinicNum"].ToString());
                    regReport.EmcNum = int.Parse(dtRow["EmcNum"].ToString());
                    regReport.EmcDeadNum = int.Parse(dtRow["EmcDeadNum"].ToString());
                    regReport.ObserveNum = int.Parse(dtRow["ObserveNum"].ToString());
                    regReport.ObserveDeadNum = int.Parse(dtRow["ObserveDeadNum"].ToString());
                    regReport.ReDiagnoseNum = int.Parse(dtRow["ReDiagnoseNum"].ToString());
                    regReport.ClcDiagnoseNum = int.Parse(dtRow["ClcDiagnoseNum"].ToString());
                    regReport.SpecialNum = int.Parse(dtRow["SpecialNum"].ToString());
                    regReport.HosInsuranceNum = int.Parse(dtRow["HosInsuranceNum"].ToString());
                    regReport.BdCheckNum = int.Parse(dtRow["BdCheckNum"].ToString());

                    regReport.Oper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;

                    arrayList.Add(regReport);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("����ʵ�弯�ϳ���!" + e.Message, "��ʾ");
                return null;
            }

            return arrayList;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDayReportRegister_Load(object sender, EventArgs e)
        {
            //����������
            this.InitDataSet();
            this.InitFrp(this.dtTime);
        }

        /// <summary>
        /// ���ڿؼ�value�ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStatDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.IsChange())
            {
                DialogResult dr = MessageBox.Show("�����Ѿ��޸�,�Ƿ񱣴�?", "��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (dr == DialogResult.Yes)
                {
                    if (this.Save() < 0)
                    {
                        this.txtStatDate.Value = this.dtTime;
                        return;
                    }
                }
                else if (dr == DialogResult.Cancel) return;
            }

            this.dtTime = this.txtStatDate.Value.Date;
            this.InitFrp(this.dtTime);
        }

        /// <summary>
        /// �˳��ж������Ƿ�ı� ����ı���ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Exit(object sender, object neuObject)
        {
            if (IsChange())
            {
                DialogResult dr = MessageBox.Show("�����Ѿ��޸�,�Ƿ񱣴�?", "��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (dr == DialogResult.Yes)
                {
                    if (this.Save() < 0) return -1;
                }
                else if (dr == DialogResult.Cancel)
                {
                    return -1;
                }
            }

            return base.Exit(sender, neuObject);
        }

        #endregion
    }
}
