using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl.IPrintBar
{
    /// <summary>
    /// �����ӡLIS�������
    /// </summary>
    public partial class ucPrintLisCode : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPrintLisCode()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.Load += new EventHandler(ucPrintLisCode_Load);
            }
        }

        #region ����

        #region ҵ������

        /// <summary>
        /// ������ù�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// Ժע������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// ҩƷ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// �ҺŹ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// �ۺ�ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ҽ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        #endregion

        /// <summary>
        /// ��ǰ����
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// ��ſ�����Ϣ
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �����ļ�
        /// </summary>
        private string injectRegisterXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\printLisCode.xml";

        FS.HISFC.Models.Pharmacy.Item drug = null;

        FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();

        FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();

        Common com = new Common();

        #endregion

        #region ����

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucPrintLisCode_Load(object sender, EventArgs e)
        {
            this.dtpStart.Value = DateTime.Now.Date.AddDays(0);
            this.dtpEnd.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            this.InitData();
            this.SetFP();
            this.Clear();
        }

        /// <summary>
        /// ��ʼ��ҽ��
        /// </summary>
        private void InitData()
        {
        }

        /// <summary>
        /// ���ø�ʽ
        /// </summary>
        private void SetFP()
        {
            txtType.ReadOnly = true;
            numType.DecimalPlaces = 0;

            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.������].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.��Ŀ].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.�����].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.�Ƿ����].CellType = txtType;

            if (System.IO.File.Exists(injectRegisterXml))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        #region ������

        /// <summary>
        /// ���幤����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("��ӡLIS����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// �˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "��ӡLIS����":
                    this.PrintLisBarCode();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #endregion

        #region ����

        /// <summary>
        /// ������ɫ(��������ʾ���һ��clinicҽ��)
        /// </summary>
        /// <returns></returns>
        private int ShowColor()
        {
            //ȡ�����clinic_code
            int maxClinic = 0;
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return -1;
            }
            FS.HISFC.Models.Fee.Outpatient.FeeItemList item = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (FS.FrameWork.Function.NConvert.ToInt32(item.ID) > maxClinic)
                {
                    maxClinic = FS.FrameWork.Function.NConvert.ToInt32(item.ID);
                }
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (item.ID == maxClinic.ToString())
                {
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(i, 0, false);
                }
            }
            return 0;
        }

        /// <summary>
        /// ���ò�����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        private void SetPatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (patient == null || patient.ID == "")
            {
                return;
            }
            else
            {
                this.patientInfo = patient;
                this.txtName.Text = patient.Name;
                this.txtSex.Text = patient.Sex.Name;
                this.txtBirthday.Text = patient.Birthday.ToString("yyyy-MM-dd");
                this.txtAge.Text = this.GetAgeByBirthday(patient.Birthday);
                this.txtDiagnoses.Text = com.GetDiagnose(patient.ID);
                this.txtClinicNo.Text = patientInfo.PID.CardNO;
                this.Query();
            }
        }

        private string GetAgeByBirthday(DateTime birthday)
        {
            TimeSpan ts = DateTime.Now - birthday;
            int year = ts.Days / 365;
            return year + "��";
        }

        #endregion

        #region  ��ӡ

        /// <summary>
        /// ��ӡLis����
        /// </summary>
        private void PrintLisBarCode()
        {
            FS.HISFC.BizProcess.Interface.Registration.IPrintBar lisBarPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar)) as FS.HISFC.BizProcess.Interface.Registration.IPrintBar;
            if (lisBarPrint == null)
            {
                return;
            }
            string err = "";
            if (this.patientInfo == null)
            {
                return;
            }
            this.patientInfo.ExtendFlag1 = this.dtpStart.Value.ToString();
            this.patientInfo.ExtendFlag2 = this.dtpEnd.Value.ToString();
            lisBarPrint.printBar(this.patientInfo, ref err);
        }

        #endregion

        #region ����

        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            neuSpread1_Sheet1.RowCount = 0;

            this.txtPatientName.Text = "";
            this.txtPatientName.Focus();
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.checkPatient();
            return 1;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            //��ѯȫ��LIS��Ŀ
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return;
            }
            else
            {
                ArrayList al = com.GetULItemListByClinicNo(this.patientInfo.ID);
                this.AddDetail(al);
            }
        }

        /// <summary>
        /// �����Ŀ��ϸ
        /// </summary>
        /// <param name="details"></param>
        private void AddDetail(ArrayList details)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            int curRow = 0;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in details)
            {
                curRow = this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Rows[curRow].Locked = true;
                this.neuSpread1_Sheet1.Rows[curRow].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Rows[curRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = feeItemList.RecipeNO;
                this.neuSpread1_Sheet1.Cells[curRow, 1].Text = feeItemList.Item.Name;
                this.neuSpread1_Sheet1.Cells[curRow, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                this.neuSpread1_Sheet1.Cells[curRow, 2].Text = feeItemList.Order.Sample.ID;
                this.neuSpread1_Sheet1.Cells[curRow, 3].Text = feeItemList.ConfirmedInjectCount > 0 ? "��" : "��";
                this.neuSpread1_Sheet1.Cells[curRow, 3].Font = new Font("����", 9, FontStyle.Bold);
                if (feeItemList.ConfirmedInjectCount > 0)
                {
                    this.neuSpread1_Sheet1.Rows[curRow].BackColor = Color.LightSkyBlue;
                }
            }
        }

        private int QueryPatientWithULItemByNameAndDate(string name, DateTime start, DateTime end)
        {
            int result = 0;
            frmQueryPatientByName f = new frmQueryPatientByName();
            f.SelectedPatient += new frmQueryPatientByName.GetPatient(SetPatient);

            if (f.QueryByNameAndDate(name, start, end, "UL") > 0)
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    result = 1;
                }
                f.Dispose();//�ͷ���Դ
            }
            return result;
        }

        private int QueryPatientWithULItemByCardNoAndDate(string cardNo, DateTime start, DateTime end)
        {
            int result = 0;
            frmQueryPatientByName f = new frmQueryPatientByName();
            f.SelectedPatient += new frmQueryPatientByName.GetPatient(SetPatient);
            if (f.QueryByCardNoAndDate(cardNo, start, end, "UL") > 0)
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    result = 1;
                }
                f.Dispose();//�ͷ���Դ
            }
            return result;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �����Ų�ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.checkPatient();
            }
        }

        private void checkPatient()
        {
            string input = this.txtPatientName.Text.Trim();
            Regex regex = new Regex("^\\d+$");
            if (!regex.IsMatch(input))
            {
                if (string.IsNullOrEmpty(input))
                {
                    input = "%";
                }

                //���ݻ�������������Ҫ��ӡLIS����Ļ�����Ϣ

                int result = this.QueryPatientWithULItemByNameAndDate(input, this.dtpStart.Value, this.dtpEnd.Value);
                if (result != 1 || this.patientInfo == null)
                {
                    MessageBox.Show("�û���û�м�����Ŀ", "��ʾ");
                    this.txtPatientName.Focus();
                    return;
                }
            }
            else
            {
                int result = this.QueryPatientWithULItemByCardNoAndDate(input, this.dtpStart.Value, this.dtpEnd.Value);
                if (result != 1 || this.patientInfo == null)
                {
                    MessageBox.Show("�û���û�м�����Ŀ", "��ʾ");
                    this.txtPatientName.Focus();
                    return;
                }
            }

            this.txtPatientName.Focus();
        }

        /// <summary>
        /// ������ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint);
                return types;
            }
        }

        #endregion

        /// <summary>
        /// ������ö��
        /// </summary>
        enum EnumColSet
        {
            ������,
            ��Ŀ,
            �����,
            �Ƿ����
        }
    }

}
