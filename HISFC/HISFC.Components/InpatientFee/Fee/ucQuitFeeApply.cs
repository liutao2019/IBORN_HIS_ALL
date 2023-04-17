using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Management;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;
using FS.SOC.HISFC.BizProcess.CommonInterface;
namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucNurseQuitFee<br></br>
    /// [��������: סԺ�˷�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucQuitFeeApply : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQuitFeeApply()
        {
            InitializeComponent();

        }

        #region ����

        /// <summary>
        /// ���תҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ���ù���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// סԺ�շ�ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ��ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
        public bool isCanQuitOtherFee = true;
        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy phamarcyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// �˷�����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.ReturnApply returnApplyManager = new FS.HISFC.BizLogic.Fee.ReturnApply();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();


        FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// �˷ѵ���ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint IBackFeePrint = null;
        /// <summary>
        /// סԺ���߻�����Ϣ
        /// </summary>
        protected PatientInfo patientInfo = null;

        /// <summary>
        /// ҩƷδ���б�
        /// </summary>
        protected DataTable dtDrug = new DataTable();

        /// <summary>
        /// ҩƷDV
        /// </summary>
        protected DataView dvDrug = new DataView();

        /// <summary>
        /// ҩƷ�����б�
        /// </summary>
        protected DataTable dtDrugQuit = new DataTable();

        /// <summary>
        /// ��ҩƷδ���б�
        /// </summary>
        protected DataTable dtUndrug = new DataTable();

        /// <summary>
        /// ��ҩƷδ��dv
        /// </summary>
        protected DataView dvUndrug = new DataView();

        /// <summary>
        /// ��ҩƷ�����б�
        /// </summary>
        protected DataTable dtUndrugQuit = new DataTable();

        /// <summary>
        /// δ��ҩƷ��������·��
        /// </summary>
        protected string filePathUnQuitDrug = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QuitFeeUnQuitDrug.xml";

        /// <summary>
        /// δ�˷�ҩƷ��������·��
        /// </summary>
        protected string filePathUnQuitUndrug = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QuitFeeUnQuitUndrug.xml";

        /// <summary>
        /// �˷Ѳ�������
        /// </summary>
        protected Operations operation;

        /// <summary>
        /// �ɲ�������Ŀ���
        /// </summary>
        protected ItemTypes itemType;

        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �Ƿ�����ֹ�����סԺ��
        /// </summary>
        protected bool isCanInputInpatientNO = true;

        /// <summary>
        /// ת����С����ID,Name��
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper objectHelperMinFee = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �Ƿ���Ը����˷�����
        /// </summary>
        protected bool isChangeItemQty = true;

        /// <summary>
        /// ����Ա����
        /// </summary>
        protected FS.FrameWork.Models.NeuObject operDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ�ֱ�ӽ����˷Ѳ���
        /// </summary>
        protected bool isDirBackFee = false;
        /// <summary>
        /// �����շ�
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Material.Material mateInteger = new FS.HISFC.BizProcess.Integrate.Material.Material();
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private DataTable dtMate = new DataTable();

        /// <summary>
        /// �������Ƿ�ֱ���˷�
        /// //{0FAD327F-9954-442a-B3A2-EA79629EB7B2} ModifyBy �Ծ�
        /// </summary>
        private bool isMyDeptDirQuitFee = false;

        /// <summary>
        /// �Ƿ���������������Ա����
        /// {097418EF-9E96-48d4-9E6C-46CCC111C78C} ModifyBy �Ծ�
        /// </summary>
        private bool isCanQuitOtherOperator = true;

        /// <summary>
        /// ������Ŀ�˷��Ƿ����ȫ��{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        protected bool isCombItemAllQuit = false;
        //{F4912030-EF65-4099-880A-8A1792A3B449}����

        private Hashtable hsQuitFeeByPackage = new Hashtable();

        private Hashtable depts = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// ������Ŀ�˷��Ƿ����ȫ��{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        public bool IsCombItemAllQuit
        {
            get
            {
                return this.isCombItemAllQuit;
            }
            set
            {
                this.isCombItemAllQuit = value;
            }
        }//{F4912030-EF65-4099-880A-8A1792A3B449}����

        /// <summary>
        /// �ɲ�������Ŀ���
        /// </summary>
        [Category("�ؼ�����"), Description("���û��߻�ÿɲ�������Ŀ���")]
        public ItemTypes ItemType
        {
            set
            {
                this.itemType = value;
            }
            get
            {
                return this.itemType;
            }
        }

        /// <summary>
        /// סԺ���߻�����Ϣ
        /// </summary>
        public PatientInfo PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;

                this.SetPatientInfomation();
            }
        }
        [Category("�ؼ�����"), Description("�Ƿ��������������ҷ��� True:���� False:������")]
        public bool IsCanQuitOtherDeptFee
        {
            get
            {
                return isCanQuitOtherFee;
            }
            set
            {
                isCanQuitOtherFee = value;
            }
        }

        private bool isPrintQuitTogether = false;
        [Category("�ؼ�����"), Description("ҩƷ�ͷ�ҩƷ�Ƿ��ӡͬһ���˷����뵥 True:�� False:����")]
        public bool IsPrintQuitTogether
        {
            get
            {
                return isPrintQuitTogether;
            }
            set
            {
                isPrintQuitTogether = value;
            }
        }

        bool isPrintQuitFee = true;
        [Category("����"), Description("�Ƿ��ӡ�˷����뵥")]
        public bool IsPrintQuitFee
        {
            set
            {
                this.isPrintQuitFee = value;
            }
            get
            {
                return this.isPrintQuitFee;
            }
        }

        bool isPrintQuitFeeNoApply = true;
        [Category("����"), Description("ֱ����ҩ�������룩�Ƿ��ӡ�˷ѵ�")]
        public bool IsPrintQuitFeeNoApply
        {
            set
            {
                this.isPrintQuitFeeNoApply = value;
            }
            get
            {
                return this.isPrintQuitFeeNoApply;
            }
        }

        bool isPrintUndrugQuitFee = true;
        [Category("����"), Description("�Ƿ��ӡ��ҩƷ�˷����뵥")]
        public bool IsPrintUndrugQuitFee
        {
            set
            {
                this.isPrintUndrugQuitFee = value;
            }
            get
            {
                return this.isPrintUndrugQuitFee;
            }
        }

        /// <summary>
        /// Ĭ�ϼ��صļ������ڷ�Χ���죩
        /// </summary>
        int queryFeeDays = 1;

        [Category("����"), Description("Ĭ�ϼ��صļ������ڷ�Χ���죩")]
        public int QueryFeeDays
        {
            set
            {
                this.queryFeeDays = value;
            }
            get
            {
                return this.queryFeeDays;
            }
        }

        /// <summary>
        /// �Ƿ�����ֹ�����סԺ��
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�����ֹ�����סԺ��")]
        public bool IsCanInputInpatientNO
        {
            get
            {
                return this.isCanInputInpatientNO;
            }
            set
            {
                this.isCanInputInpatientNO = value;

                this.ucQueryPatientInfo.Enabled = this.isCanInputInpatientNO;
            }
        }

        [Category("�ؼ�����"), Description("�Ƿ���Ը����˷����� True:���� False:������")]
        public bool IsChangeItemQty
        {
            get
            {
                return isChangeItemQty;
            }
            set
            {
                isChangeItemQty = value;
                if (this.DesignMode)
                    return;
                if (!value)
                {
                    this.ckbAllQuit.Checked = true;
                    this.ckbAllQuit.Enabled = false;
                }
            }
        }

        /// <summary>
        /// �Ƿ�ֱ�ӽ����˷Ѳ���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ֱ�ӽ����˷Ѳ���")]
        public bool IsDirQuitFee
        {
            get
            {
                return this.isDirBackFee;
            }
            set
            {
                this.isDirBackFee = value;
            }
        }

        /// <summary>
        /// �Ƿ���������������Ա����
        /// {097418EF-9E96-48d4-9E6C-46CCC111C78C} ModifyBy �Ծ�
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���������������Ա���ã�True:���� False:������")]
        public bool IsCanQuitOtherOperator
        {
            get
            {
                return this.isCanQuitOtherOperator;
            }
            set
            {
                this.isCanQuitOtherOperator = value;
            }
        }


        /// <summary>
        /// �Ƿ������������Ŀ�˷�
        /// </summary>
        private bool isQuitFeeByPackage = false;
        [Category("��������"), Description("�Ƿ������������Ŀ�˷ѣ�True:���� False:������")]
        public bool IsQuitFeeByPackage
        {
            get
            {
                return isQuitFeeByPackage;
            }
            set
            {
                isQuitFeeByPackage = value;
            }
        }

        /// <summary>
        /// �����ҷ����Ƿ���Ҫ���룬True:��Ҫ False:����Ҫ
        /// </summary>
        private bool isCurrentDeptNeedAppy = true;
        [Category("��������"), Description("�����ҷ����Ƿ���Ҫ���룬True:��Ҫ False:����Ҫ")]
        public bool IsCurrentDeptNeedAppy
        {
            get
            {
                return isCurrentDeptNeedAppy;
            }
            set
            {
                isCurrentDeptNeedAppy = value;
            }
        }
        [Category("�ؼ�����"), Description("סԺ�������Ĭ������0סԺ�ţ�5����")]
        public int DefaultInput
        {
            get
            {
                return this.ucQueryPatientInfo.DefaultInputType;
            }
            set
            {
                this.ucQueryPatientInfo.DefaultInputType = value;
            }
        }

        [Category("�ؼ�����"), Description("�����Ų�ѯ������Ϣʱ�������ߵ�״̬��ѯ�����ȫ����'ALL'")]
        public string PatientInState
        {
            get
            {
                return this.ucQueryPatientInfo.PatientInState;
            }
            set
            {
                this.ucQueryPatientInfo.PatientInState = value;
            }
        }


        /// <summary>
        /// ����Ȩ��
        /// </summary>
        private string operationPriv = string.Empty;

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        [Category("�ؼ�����"), Description("�������Ȩ�ޣ����磺0830+21��0830 �������Ȩ�ޣ�21 ��������Ȩ�ޣ�Ϊ���������ҪȨ��Ҳ����ʹ��")]
        public string OperationPriv
        {
            get
            {
                return operationPriv;
            }
            set
            {
                operationPriv = value;
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 0, 0, 0).AddDays(0 - queryFeeDays + 1);

            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 23, 59, 59);

            ArrayList minFeeList = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (minFeeList == null)
            {
                MessageBox.Show("�����С���ó���!" + this.managerIntegrate.Err);

                return -1;
            }

            this.objectHelperMinFee.ArrayObject = minFeeList;

            this.InitFp();

            this.SetItemType();

            this.InitQuitDrugReason();

            this.operDept = ((FS.HISFC.Models.Base.Employee)this.undrugManager.Operator).Dept;

            //���Ҳ��������Ŀ���
            depts.Clear();
            ArrayList alDept = managerIntegrate.QueryDepartment((this.returnApplyManager.Operator as FS.HISFC.Models.Base.Employee).Nurse.ID);
            if (alDept == null || alDept.Count == 0)
            {
                alDept = new ArrayList();
                alDept.Add((this.returnApplyManager.Operator as FS.HISFC.Models.Base.Employee).Dept);
            }
            if (alDept != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in alDept)
                {
                    if (depts.ContainsKey(obj.ID) == false)
                    {
                        depts.Add(obj.ID, obj.Name);
                    }
                }
            }

            if (depts.ContainsKey(this.operDept.ID) == false)
            {
                depts.Add(this.operDept.ID, this.operDept.Name);
            }
            //this.ckShow.Checked = true;
            this.ckShow.CheckedChanged += new EventHandler(ckShow_CheckedChanged);// {58D72110-55F9-4efb-BCB0-FCD84C68E905}
            return 1;
        }
        // {58D72110-55F9-4efb-BCB0-FCD84C68E905}
        private void ckShow_CheckedChanged(object sender, EventArgs e)
        {

            DateTime beginTime = this.dtpBeginTime.Value;
            DateTime endTime = this.dtpEndTime.Value;
            //this.fpUnQuit_SheetDrug.RowCount = 0;
            //this.fpUnQuit_SheetUndrug.RowCount = 0;
            this.ClearItemList();
            this.RetriveDrug(beginTime, endTime);
            this.RetriveUnrug(beginTime, endTime);
        }
        /// <summary>
        /// ��ʼ����ҩԭ��
        /// </summary>
        private void InitQuitDrugReason()
        {
            this.cmbQuitFeeReason.Items.Clear();
            FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList allQuitFeeReason = consMgr.GetAllList("QUITFEEREASON");
            if (allQuitFeeReason != null && allQuitFeeReason.Count > 0)
            {
                this.cmbQuitFeeReason.AddItems(allQuitFeeReason);
                this.cmbQuitFeeReason.SelectedIndex = 0;
                this.cmbQuitFeeReason.DropDownStyle = ComboBoxStyle.Simple;
                this.cmbQuitFeeReason.Enter += new EventHandler(cmbQuitFeeReason_Enter);
            }
        }

        void cmbQuitFeeReason_Enter(object sender, EventArgs e)
        {
            this.SetQuitReason();
        }

        private void SetQuitReason()
        {
            for (int index = 0; index < this.fpQuit_SheetDrug.Rows.Count; index++)
            {
                if (this.fpQuit_SheetDrug.Rows[index].ForeColor == System.Drawing.Color.Red)
                {
                    continue;
                }
                this.fpQuit_SheetDrug.Cells[index, (int)DrugColumns.QuitDrugReason].Text = this.cmbQuitFeeReason.Text;
                (this.fpQuit_SheetDrug.Rows[index].Tag as FeeItemList).Item.Memo = this.cmbQuitFeeReason.Text;
            }
        }

        /// <summary>
        /// ��ʼ��Fp�к�Sheet��ʾ˳�����Ϣ,Sheet�����ݰ�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitFp()
        {
            this.fpQuit.ActiveSheet = this.fpQuit_SheetDrug;
            this.fpQuit_SheetDrug.Columns[(int)DrugColumns.FeeDate].Visible = false;

            this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetDrug;

            #region ��ʼ��ҩƷ��Ϣ

            this.dtDrug.Reset();

            //������ڱ���ҩƷ���������ļ�,ֱ�Ӷ�ȡ�����ļ�����DataTable,���Ѵ����źʹ�����ˮ����Ϊ����
            //������������Ҫ��Ϊ���Ժ�Ĳ��Ҷ�Ӧ��Ŀ��
            if (System.IO.File.Exists(this.filePathUnQuitDrug))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePathUnQuitDrug, dtDrug, ref dvDrug, this.fpUnQuit_SheetDrug);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);

                dtDrug.PrimaryKey = new DataColumn[] { dtDrug.Columns["������"], dtDrug.Columns["������ˮ��"] };
            }
            else//���û���ҵ������ļ�,��Ĭ�ϵ���˳�����������DataTable,����ͬ��Ϊ�����źʹ�����ˮ��
            {
                this.dtDrug.Columns.AddRange(new DataColumn[]
                {
                    //new DataColumn("��ҩԭ��",typeof(string)),
                    new DataColumn("ҩƷ����", typeof(string)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("�۸�", typeof(decimal)),
                    new DataColumn("��������", typeof(decimal)),
                    new DataColumn("��λ", typeof(string)),
                    new DataColumn("���", typeof(decimal)),
                    new DataColumn("ִ�п���", typeof(string)),
                    new DataColumn("����ҽʦ", typeof(string)),
                    new DataColumn("Ӧִ��ʱ��", typeof(DateTime)),
                    new DataColumn("��������", typeof(DateTime)),
                    new DataColumn("�Ƿ��ҩ", typeof(string)),
                    new DataColumn("����", typeof(string)),
                    new DataColumn("ҽ����", typeof(string)),
                    new DataColumn("ҽ��ִ�к�", typeof(string)),
                    new DataColumn("������", typeof(string)),
                    new DataColumn("������ˮ��", typeof(string)),
                    new DataColumn("��װ��", typeof(decimal)),
                    new DataColumn("�Ƿ��װ��λ", typeof(string)),
                    new DataColumn("ƴ����", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("�����", typeof(string)),
                    new DataColumn("��Ϻ�", typeof(string)),
                 });

                dtDrug.PrimaryKey = new DataColumn[] { dtDrug.Columns["������"], dtDrug.Columns["������ˮ��"] };

                dvDrug = new DataView(dtDrug);

                //�󶨵���Ӧ��Farpoint
                this.fpUnQuit_SheetDrug.DataSource = dvDrug;

                //FarPoint.Win.Spread.CellType.NumberCellType n=new FarPoint.Win.Spread.CellType.NumberCellType();
                //n.DecimalPlaces=4;
                //this.fpUnQuit_SheetDrug.Columns[4].CellType = n;

                //���浱ǰ����˳��,���Ƶ���Ϣ��XML
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);

            }

            #endregion

            #region ��ʼ��δ�˷�ҩƷ��Ϣ

            dtUndrug.Reset();

            //������ڱ��ط�ҩƷ���������ļ�,ֱ�Ӷ�ȡ�����ļ�����DataTable,���Ѵ����źʹ�����ˮ����Ϊ����
            //������������Ҫ��Ϊ���Ժ�Ĳ��Ҷ�Ӧ��Ŀ��
            if (System.IO.File.Exists(this.filePathUnQuitUndrug))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePathUnQuitUndrug, dtUndrug, ref dvUndrug, this.fpUnQuit_SheetUndrug);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);

                dtUndrug.PrimaryKey = new DataColumn[] { dtUndrug.Columns["������"], dtUndrug.Columns["������ˮ��"] };
                this.fpUnQuit_SheetUndrug.DataSource = null;
            }
            else
            {
                this.dtUndrug.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("��Ŀ����", typeof(string)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("�۸�", typeof(decimal)),
                    new DataColumn("��������", typeof(decimal)),
                    new DataColumn("��λ", typeof(string)),
                    new DataColumn("���", typeof(decimal)),
                    new DataColumn("ִ�п���", typeof(string)),
                    new DataColumn("����ҽʦ", typeof(string)),
                    new DataColumn("Ӧִ��ʱ��", typeof(DateTime)),
                    new DataColumn("��������", typeof(DateTime)),
                    new DataColumn("�Ƿ�ִ��", typeof(string)),
                    new DataColumn("����", typeof(string)),
                    new DataColumn("ҽ����", typeof(string)),
                    new DataColumn("ҽ��ִ�к�", typeof(string)),
                    new DataColumn("������", typeof(string)),
                    new DataColumn("������ˮ��", typeof(string)),
                    new DataColumn("ƴ����", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("������ˮ��",typeof(string)),
                    new DataColumn("������",typeof(string)),
                    //��0ҩƷ 2����
                    new DataColumn("��ʶ",typeof(string)),
                    new DataColumn("�����", typeof(string)),
                    new DataColumn("��Ϻ�", typeof(string)),
                    
                 });

                dtUndrug.PrimaryKey = new DataColumn[] { dtUndrug.Columns["������"], dtUndrug.Columns["������ˮ��"] };

                dvUndrug = new DataView(dtUndrug);

                //�󶨵���Ӧ��Farpoint
                this.fpUnQuit_SheetUndrug.DataSource = dvUndrug;

                // ���浱ǰ����˳��,���Ƶ���Ϣ��XML
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
            }

            this.dtMate.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("��Ŀ����", typeof(string)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("��������", typeof(string)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("�۸�", typeof(decimal)),
                    new DataColumn("��������", typeof(decimal)),
                    new DataColumn("��λ", typeof(string)),
                    new DataColumn("���", typeof(decimal)),
                    new DataColumn("����", typeof(string)),
                    new DataColumn("������", typeof(string)),
                    new DataColumn("������ˮ��", typeof(string)),
                    new DataColumn("������ˮ��",typeof(string)),
                    new DataColumn("������",typeof(string)),
                    //��0ҩƷ 2����
                    new DataColumn("��ʶ",typeof(string))
                });
            dtMate.PrimaryKey = new DataColumn[] { dtMate.Columns["������"], dtMate.Columns["������ˮ��"] };

            #endregion

            return 1;
        }

        /// <summary>
        /// ���ÿɲ�������Ŀ���
        /// </summary>
        protected virtual void SetItemType()
        {
            switch (this.itemType)
            {
                case ItemTypes.Pharmarcy:
                    this.fpUnQuit_SheetDrug.Visible = true;
                    this.fpUnQuit_SheetUndrug.Visible = false;
                    this.fpQuit_SheetDrug.Visible = true;
                    this.fpQuit_SheetUndrug.Visible = false;
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetDrug;
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetDrug;

                    break;

                case ItemTypes.Undrug:
                    this.fpUnQuit_SheetDrug.Visible = false;
                    this.fpUnQuit_SheetUndrug.Visible = true;
                    this.fpQuit_SheetDrug.Visible = false;
                    this.fpQuit_SheetUndrug.Visible = true;
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetUndrug;
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetUndrug;

                    break;

                case ItemTypes.All:
                    this.fpUnQuit_SheetDrug.Visible = true;
                    this.fpUnQuit_SheetUndrug.Visible = true;
                    this.fpQuit_SheetDrug.Visible = true;
                    this.fpQuit_SheetUndrug.Visible = true;
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetDrug;
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetDrug;

                    break;
            }
        }

        #endregion

        #region ���ݼ�Fp����

        /// <summary>
        /// ����ҩƷ�б�
        /// {C0E249B0-DE20-4d56-9C1E-9952858E32F1}
        /// </summary>
        /// <param name="drugList"></param>
        protected virtual void SetDrugList(List<FeeItemList> lstDrugList)
        {
            int iIndex = 0;
            foreach (FeeItemList feeItemList in lstDrugList)
            {
                DataRow row = this.dtDrug.NewRow();

                ////��ȡҩƷ������Ϣ,������ʱΪ�˻��ƴ����
                //FS.HISFC.Models.Base.Item phamarcyItem = this.phamarcyIntegrate.GetItem(feeItemList.Item.ID);
                //if (phamarcyItem == null)
                //{
                //    phamarcyItem = new FS.HISFC.Models.Base.Item();

                //}
                #region add by liuww
                //this.fpUnQuit.ActiveSheet.AddRows(this.fpUnQuit.ActiveSheet.RowCount, 1);
                #endregion

                if (feeItemList.Item.PackQty == 0)
                {
                    feeItemList.Item.PackQty = 1;
                }

                //feeItemList.Item.IsPharmacy = true;
                feeItemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                //row["��ҩԭ��"] = this.cmbQuitFeeReason.Text;
                row["ҩƷ����"] = feeItemList.Item.Name;
                row["���"] = feeItemList.Item.Specs;
                row["��������"] = this.objectHelperMinFee.GetName(feeItemList.Item.MinFee.ID);
                row["�۸�"] = feeItemList.Item.Price;
                row["��������"] = feeItemList.NoBackQty;
                row["��λ"] = feeItemList.Item.PriceUnit;
                row["���"] = feeItemList.FT.TotCost; // FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2);

                //FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                //deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
                //if (deptInfo == null)
                //{
                //    deptInfo = new FS.HISFC.Models.Base.Department();
                //    deptInfo.Name = feeItemList.ExecOper.Dept.ID;
                //}

                if (string.IsNullOrEmpty(feeItemList.ExecOper.Dept.Name))
                {
                    feeItemList.ExecOper.Dept.Name = feeItemList.ExecOper.Dept.ID;
                }

                row["ִ�п���"] = feeItemList.ExecOper.Dept.Name;

                //FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                //empl = this.managerIntegrate.GetEmployeeInfo(feeItemList.RecipeOper.ID);

                if (string.IsNullOrEmpty(feeItemList.RecipeOper.Name))
                {
                    feeItemList.RecipeOper.Name = feeItemList.RecipeOper.ID;
                }

                row["����ҽʦ"] = feeItemList.RecipeOper.Name;

                //row["����ҽʦ"] = feeItemList.RecipeOper.ID;
                row["Ӧִ��ʱ��"] = feeItemList.ExecOper.OperTime;
                row["��������"] = feeItemList.FeeOper.OperTime;
                //row["�Ƿ��ҩ"] = feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? '��' : '��';

                row["����"] = feeItemList.Item.ID;
                row["ҽ����"] = feeItemList.Order.ID;
                row["ҽ��ִ�к�"] = feeItemList.ExecOrder.ID;
                row["������"] = feeItemList.RecipeNO;
                row["������ˮ��"] = feeItemList.SequenceNO;

                row["ƴ����"] = feeItemList.Item.SpellCode;
                row["��������"] = feeItemList.FeeOper.Dept.ID;
                row["�����"] = feeItemList.Item.WBCode;
                row["��Ϻ�"] = feeItemList.Order.Combo.ID;
                this.dtDrug.Rows.Add(row);

                #region add by liuww
                //this.fpUnQuit.ActiveSheet.Rows[iIndex].Tag = feeItemList;
                iIndex = iIndex + 1;
                #endregion
            }
        }

        /// <summary>
        /// ���÷�ҩƷ�б�
        /// </summary>
        /// <param name="undrugList"></param>
        protected virtual void SetUndrugList(List<FeeItemList> undrugList)
        {
            dtUndrug.Rows.Clear();
            dtMate.Rows.Clear();

            //Hashtable �����ж��Ƿ�������Ŀ
            Hashtable hsPackageItem = new Hashtable();
            //�۸񼯺�
            Dictionary<string, decimal> priceCollection = new Dictionary<string, decimal>();
            Dictionary<string, string> priceItemCollection = new Dictionary<string, string>();

            foreach (FeeItemList feeItemList in undrugList)
            {


                //��÷�ҩƷ��Ϣ,������Ҫ��Ϊ�˻�ü�����
                //FS.HISFC.Models.Fee.Item.Undrug undrugItem = this.undrugManager.GetValidItemByUndrugCode(feeItemList.Item.ID);
                //if (undrugItem == null)
                //{
                //    undrugItem = new FS.HISFC.Models.Fee.Item.Undrug();
                //}
                if (isQuitFeeByPackage)
                {
                    if (string.IsNullOrEmpty(feeItemList.UndrugComb.ID) == false)
                    {
                        string key = feeItemList.UndrugComb.ID + "|" + feeItemList.TransType.ToString() + (string.IsNullOrEmpty(feeItemList.ExecOrder.ID) ? feeItemList.FeeOper.OperTime.ToString() : feeItemList.ExecOrder.ID);
                        if (priceCollection.ContainsKey(key))
                        {
                            if (priceItemCollection.ContainsKey(key + feeItemList.Item.ID) == false)
                            {
                                if (feeItemList.UndrugComb.Qty != 0)
                                {
                                    priceCollection[key] += Math.Abs(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.UndrugComb.Qty);
                                }
                                else
                                {
                                    priceCollection[key] += Math.Abs(feeItemList.Item.Price);
                                }

                                priceItemCollection[key + feeItemList.Item.ID] = feeItemList.Item.ID;
                            }
                        }
                        else
                        {
                            if (feeItemList.UndrugComb.Qty != 0)
                            {
                                priceCollection[key] = Math.Abs(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.UndrugComb.Qty);
                            }
                            else
                            {
                                priceCollection[key] = Math.Abs(feeItemList.Item.Price);
                            }
                            priceItemCollection[key + feeItemList.Item.ID] = feeItemList.Item.ID;
                        }

                        if (hsPackageItem.ContainsKey(key))
                        {
                            ((ArrayList)hsPackageItem[key]).Add(feeItemList);
                        }
                        else
                        {
                            //�½�
                            ArrayList al = new ArrayList();
                            al.Add(feeItemList);
                            hsPackageItem[key] = al;
                        }

                        continue;
                    }

                }

                DataRow row = this.dtUndrug.NewRow();
                if (feeItemList.Item.PackQty == 0)
                {
                    feeItemList.Item.PackQty = 1;
                }
                //feeItemList.Item.IsPharmacy = false;
                //feeItemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                row["��Ŀ����"] = feeItemList.Item.Name;
                row["��������"] = feeItemList.UndrugComb.Name;
                row["��������"] = this.objectHelperMinFee.GetName(feeItemList.Item.MinFee.ID);
                row["�۸�"] = feeItemList.Item.Price;
                row["��������"] = feeItemList.NoBackQty;
                row["��λ"] = feeItemList.Item.PriceUnit;
                row["���"] = feeItemList.FT.TotCost;//FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2);

                //FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                //deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
                //if (deptInfo == null)
                //{
                //    deptInfo = new FS.HISFC.Models.Base.Department();
                //    deptInfo.Name = feeItemList.ExecOper.Dept.ID;
                //}

                if (string.IsNullOrEmpty(feeItemList.ExecOper.Dept.Name))
                {
                    feeItemList.ExecOper.Dept.Name = feeItemList.ExecOper.Dept.ID;
                }

                row["ִ�п���"] = feeItemList.ExecOper.Dept.Name;

                //FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                //empl = this.managerIntegrate.GetEmployeeInfo(feeItemList.RecipeOper.ID);

                if (string.IsNullOrEmpty(feeItemList.RecipeOper.Name))
                {
                    feeItemList.RecipeOper.Name = feeItemList.RecipeOper.ID;
                }

                row["����ҽʦ"] = feeItemList.RecipeOper.Name;

                //row["����ҽʦ"] = feeItemList.RecipeOper.ID;
                row["Ӧִ��ʱ��"] = feeItemList.ExecOrder.DateUse;
                row["��������"] = feeItemList.FeeOper.OperTime;
                row["�Ƿ�ִ��"] = feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? '��' : '��';
                row["����"] = feeItemList.Item.ID;
                row["ҽ����"] = feeItemList.Order.ID;
                row["ҽ��ִ�к�"] = feeItemList.ExecOrder.ID;
                row["������"] = feeItemList.RecipeNO;
                row["������ˮ��"] = feeItemList.SequenceNO;

                row["ƴ����"] = feeItemList.Item.SpellCode; // undrugItem == null ? string.Empty : undrugItem.SpellCode;
                row["��������"] = feeItemList.FeeOper.Dept.ID;
                row["������ˮ��"] = feeItemList.UpdateSequence.ToString();
                row["��ʶ"] = ((int)feeItemList.Item.ItemType).ToString();
                row["�����"] = feeItemList.Item.WBCode;
                row["��Ϻ�"] = feeItemList.Order.Combo.ID;
                this.dtUndrug.Rows.Add(row);
                //���ݷ�ҩƷ��ȡ���յ�������Ϣ
                GetMateData(feeItemList);
            }

            if (isQuitFeeByPackage)
            {
                if (hsPackageItem.Count > 0)
                {
                    foreach (DictionaryEntry de in hsPackageItem)
                    {
                        string packagecode = de.Key.ToString().Split('|')[0];
                        ArrayList al = de.Value as ArrayList;
                        if (al != null && al.Count > 0)
                        {
                            FeeItemList feeItemListFirst = (al[0] as FeeItemList).Clone();

                            decimal qtyMutilpe = decimal.Divide(feeItemListFirst.NoBackQty, feeItemListFirst.Item.Qty);
                            bool isAddByPackage = true;
                            decimal sumCost = 0.00M;
                            foreach (FeeItemList feeItemList in al)
                            {
                                if (feeItemList.Item.PackQty == 0)
                                {
                                    feeItemList.Item.PackQty = 1;
                                }

                               
                                sumCost += FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2);
                            }

                            if (isAddByPackage)
                            {
                                //���Ҹ�����Ŀ
                                FS.HISFC.Models.Fee.Item.Undrug undrug = this.feeIntegrate.GetUndrugByCode(packagecode);

                                DataRow row = this.dtUndrug.NewRow();

                                feeItemListFirst.Item = undrug;
                                if (feeItemListFirst.Item.PackQty == 0)
                                {
                                    feeItemListFirst.Item.PackQty = 1;
                                }

                                if (priceCollection.ContainsKey(de.Key.ToString()))
                                {
                                    feeItemListFirst.Item.Price = priceCollection[de.Key.ToString()];
                                }
                                else
                                {
                                    feeItemListFirst.Item.Price = sumCost;
                                }


                                feeItemListFirst.NoBackQty = sumCost / feeItemListFirst.Item.Price;

                                row["��Ŀ����"] = feeItemListFirst.Item.Name;
                                row["��������"] = feeItemListFirst.UndrugComb.Name;
                                row["��������"] = this.objectHelperMinFee.GetName(feeItemListFirst.Item.MinFee.ID);
                                row["�۸�"] = feeItemListFirst.Item.Price;
                                row["��������"] = feeItemListFirst.NoBackQty;
                                row["��λ"] = feeItemListFirst.Item.PriceUnit;
                                row["���"] = sumCost;

                                //FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                                //deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
                                //if (deptInfo == null)
                                //{
                                //    deptInfo = new FS.HISFC.Models.Base.Department();
                                //    deptInfo.Name = feeItemList.ExecOper.Dept.ID;
                                //}

                                if (string.IsNullOrEmpty(feeItemListFirst.ExecOper.Dept.Name))
                                {
                                    feeItemListFirst.ExecOper.Dept.Name = feeItemListFirst.ExecOper.Dept.ID;
                                }

                                row["ִ�п���"] = feeItemListFirst.ExecOper.Dept.Name;

                                //FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                                //empl = this.managerIntegrate.GetEmployeeInfo(feeItemList.RecipeOper.ID);

                                if (string.IsNullOrEmpty(feeItemListFirst.RecipeOper.Name))
                                {
                                    feeItemListFirst.RecipeOper.Name = feeItemListFirst.RecipeOper.ID;
                                }

                                row["����ҽʦ"] = feeItemListFirst.RecipeOper.Name;

                                //row["����ҽʦ"] = feeItemList.RecipeOper.ID;
                                row["Ӧִ��ʱ��"] = feeItemListFirst.ExecOper.OperTime;
                                row["��������"] = feeItemListFirst.FeeOper.OperTime;
                                row["�Ƿ�ִ��"] = feeItemListFirst.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? '��' : '��';
                                row["����"] = feeItemListFirst.Item.ID;
                                row["ҽ����"] = feeItemListFirst.Order.ID;
                                row["ҽ��ִ�к�"] = feeItemListFirst.ExecOrder.ID;
                                row["������"] = feeItemListFirst.RecipeNO;
                                row["������ˮ��"] = feeItemListFirst.SequenceNO;

                                row["ƴ����"] = feeItemListFirst.Item.SpellCode; // undrugItem == null ? string.Empty : undrugItem.SpellCode;
                                row["��������"] = feeItemListFirst.FeeOper.Dept.ID;
                                row["������ˮ��"] = feeItemListFirst.UpdateSequence.ToString();
                                row["��ʶ"] = ((int)feeItemListFirst.Item.ItemType).ToString();
                                row["�����"] = feeItemListFirst.Item.WBCode;
                                row["��Ϻ�"] = feeItemListFirst.Order.Combo.ID;

                                string key = feeItemListFirst.UndrugComb.ID + "|" + feeItemListFirst.TransType.ToString() + "|" + feeItemListFirst.RecipeNO + feeItemListFirst.ExecOrder.ID;

                                hsQuitFeeByPackage[key] = feeItemListFirst;
                                hsQuitFeeByPackage["Apply" + key] = al;


                                this.dtUndrug.Rows.Add(row);
                                //���ݷ�ҩƷ��ȡ���յ�������Ϣ
                                GetMateData(feeItemListFirst);
                            }
                            else
                            {
                                foreach (FeeItemList feeItemList in al)
                                {
                                    DataRow row = this.dtUndrug.NewRow();

                                    if (feeItemList.Item.PackQty == 0)
                                    {
                                        feeItemList.Item.PackQty = 1;
                                    }
                                    //feeItemList.Item.IsPharmacy = false;
                                    //feeItemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                                    row["��Ŀ����"] = feeItemList.Item.Name;
                                    row["��������"] = feeItemList.UndrugComb.Name;
                                    row["��������"] = this.objectHelperMinFee.GetName(feeItemList.Item.MinFee.ID);
                                    row["�۸�"] = feeItemList.Item.Price;
                                    row["��������"] = feeItemList.NoBackQty;
                                    row["��λ"] = feeItemList.Item.PriceUnit;
                                    row["���"] = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2);

                                    //FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                                    //deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
                                    //if (deptInfo == null)
                                    //{
                                    //    deptInfo = new FS.HISFC.Models.Base.Department();
                                    //    deptInfo.Name = feeItemList.ExecOper.Dept.ID;
                                    //}

                                    if (string.IsNullOrEmpty(feeItemList.ExecOper.Dept.Name))
                                    {
                                        feeItemList.ExecOper.Dept.Name = feeItemList.ExecOper.Dept.ID;
                                    }

                                    row["ִ�п���"] = feeItemList.ExecOper.Dept.Name;

                                    //FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                                    //empl = this.managerIntegrate.GetEmployeeInfo(feeItemList.RecipeOper.ID);

                                    if (string.IsNullOrEmpty(feeItemList.RecipeOper.Name))
                                    {
                                        feeItemList.RecipeOper.Name = feeItemList.RecipeOper.ID;
                                    }

                                    row["����ҽʦ"] = feeItemList.RecipeOper.Name;

                                    //row["����ҽʦ"] = feeItemList.RecipeOper.ID;
                                    row["Ӧִ��ʱ��"] = feeItemList.ExecOrder.DateUse;
                                    row["��������"] = feeItemList.FeeOper.OperTime;
                                    row["�Ƿ�ִ��"] = feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? '��' : '��';
                                    row["����"] = feeItemList.Item.ID;
                                    row["ҽ����"] = feeItemList.Order.ID;
                                    row["ҽ��ִ�к�"] = feeItemList.ExecOrder.ID;
                                    row["������"] = feeItemList.RecipeNO;
                                    row["������ˮ��"] = feeItemList.SequenceNO;

                                    row["ƴ����"] = feeItemList.Item.SpellCode; // undrugItem == null ? string.Empty : undrugItem.SpellCode;
                                    row["��������"] = feeItemList.FeeOper.Dept.ID;
                                    row["������ˮ��"] = feeItemList.UpdateSequence.ToString();
                                    row["��ʶ"] = ((int)feeItemList.Item.ItemType).ToString();
                                    row["�����"] = feeItemList.Item.WBCode;
                                    row["��Ϻ�"] = feeItemList.Order.Combo.ID;
                                    this.dtUndrug.Rows.Add(row);
                                    //���ݷ�ҩƷ��ȡ���յ�������Ϣ
                                    GetMateData(feeItemList);
                                }
                            }
                        }
                    }
                }
            }
            SetUndragFp(string.Empty);
        }
        /// <summary>
        /// ���ݷ�ҩƷ��ȡ���յ�������Ϣ
        /// </summary>
        /// <param name="feeItem">��ҩƷ�շ���Ϣ</param>
        private void GetMateData(FeeItemList feeItem)
        {
            string outNo = feeItem.UpdateSequence.ToString();
            List<HISFC.Models.FeeStuff.Output> list = mateInteger.QueryOutput(outNo);
            if (list != null)
            {
                DataRow row = null;
                foreach (HISFC.Models.FeeStuff.Output item in list)
                {
                    row = dtMate.NewRow();
                    row["��Ŀ����"] = item.StoreBase.Item.Name;
                    row["��������"] = this.objectHelperMinFee.GetName(item.StoreBase.Item.MinFee.ID);
                    row["���"] = item.StoreBase.Item.Specs;
                    row["�۸�"] = item.StoreBase.Item.Price;
                    row["��������"] = item.StoreBase.Quantity - item.ReturnApplyNum - item.StoreBase.Returns;
                    row["��λ"] = item.StoreBase.Item.PriceUnit;
                    row["���"] = NConvert.ToDecimal(row["�۸�"]) * NConvert.ToInt32(row["��������"]);
                    row["������ˮ��"] = item.ID;
                    row["����"] = item.StoreBase.Item.ID;
                    row["������"] = item.StoreBase.StockNO;

                    dtMate.Rows.Add(row);
                }
            }
        }

        /// <summary>
        /// ���˷�ҩƷ��Ϣ
        /// </summary>
        /// <param name="strVal"></param>
        private void SetUndragFp(string strVal)
        {
            string filter = "��Ŀ���� like '" + "%" + strVal + "%'" + " OR " + "ƴ���� like '" + "%" + strVal + "%'" + " OR " + "����� like '" + "%" + strVal + "%'" + " OR " + "�������� like '" + "%" + strVal + "%'" + " OR " + "�������� like '" + "%" + strVal + "%'";
            DataRow[] vdr = dtUndrug.Select(filter);
            if (vdr == null)
            {
                return;
            }

            this.fpUnQuit_SheetUndrug.Rows.Count = 0;
            foreach (DataRow dr in vdr)
            {
                SetUndrugRow(dr);
            }

        }

        /// <summary>
        /// ��ʾ��ҩƷ����
        /// </summary>
        /// <param name="dr"></param>
        private void SetUndrugRow(DataRow dr)
        {
            this.fpUnQuit_SheetUndrug.Rows.Add(this.fpUnQuit_SheetUndrug.Rows.Count, 1);
            int rowIndex = this.fpUnQuit_SheetUndrug.Rows.Count - 1;
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 0].Text = dr["��Ŀ����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 1].Text = dr["���"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 2].Text = dr["��������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 3].Text = dr["��������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 4].Text = dr["�۸�"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 5].Text = dr["��������"].ToString();
            if (decimal.Parse(dr["��������"].ToString()) == 0)// {58D72110-55F9-4efb-BCB0-FCD84C68E905}
            {
                this.fpUnQuit_SheetUndrug.Rows[rowIndex].ForeColor = Color.Red;
            }
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 6].Text = dr["��λ"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 7].Text = dr["���"].ToString();
            if (decimal.Parse(dr["���"].ToString()) < 0)// {9B75C463-0167-41d4-B42E-DB869D5EFC11}
            {
                this.fpUnQuit_SheetUndrug.Rows[rowIndex].ForeColor = Color.Red;
            }
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 8].Text = dr["ִ�п���"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 9].Text = dr["����ҽʦ"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 10].Text = dr["Ӧִ��ʱ��"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 11].Text = dr["��������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 12].Text = dr["�Ƿ�ִ��"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 13].Text = dr["����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 14].Text = dr["ҽ����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 15].Text = dr["ҽ��ִ�к�"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 16].Text = dr["������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 17].Text = dr["������ˮ��"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 18].Text = dr["ƴ����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 19].Text = dr["��������"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 20].Text = dr["������ˮ��"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 22].Text = dr["��ʶ"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 23].Text = dr["�����"].ToString();
            this.fpUnQuit_SheetUndrug.Cells[rowIndex, 24].Text = dr["��Ϻ�"].ToString();

            //��ʾ��������
            SetMateRow(dr, rowIndex);

        }

        /// <summary>
        /// ��ʾ��������
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="rowIndex"></param>
        private void SetMateRow(DataRow dr, int rowIndex)
        {

            int index = 0;
            //string itemCode = dr["����"].ToString();
            string outNo = dr["������ˮ��"].ToString();
            //string strfilter = "���� ='" + itemCode + "' And " + "������ˮ�� ='" + outNo + "'";
            string strfilter = "������ˮ�� ='" + outNo + "'";
            DataRow[] vdr = dtMate.Select(strfilter);
            if (vdr == null || vdr.Length == 0)
                return;
            if (vdr.Length == 1)
                return;
            fpUnQuit_SheetUndrug.RowHeader.Cells[rowIndex, 0].Text = "+";
            fpUnQuit_SheetUndrug.RowHeader.Cells[rowIndex, 0].BackColor = Color.YellowGreen;
            foreach (DataRow row in vdr)
            {
                fpUnQuit_SheetUndrug.Rows.Add(fpUnQuit_SheetUndrug.Rows.Count, 1);
                index = fpUnQuit_SheetUndrug.Rows.Count - 1;
                this.fpUnQuit_SheetUndrug.Cells[index, 0].Text = row["��Ŀ����"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                this.fpUnQuit_SheetUndrug.Cells[index, 1].Text = row["��������"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 2].Text = row["�۸�"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 3].Text = row["��������"].ToString();
                if (decimal.Parse(dr["��������"].ToString()) == 0)// {58D72110-55F9-4efb-BCB0-FCD84C68E905}
                {
                    this.fpUnQuit_SheetUndrug.Rows[rowIndex].ForeColor = Color.Red;
                }
                this.fpUnQuit_SheetUndrug.Cells[index, 4].Text = row["��λ"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 5].Text = row["���"].ToString();

                if (decimal.Parse(dr["���"].ToString()) < 0)// {9B75C463-0167-41d4-B42E-DB869D5EFC11}
                {
                    this.fpUnQuit_SheetUndrug.Rows[rowIndex].ForeColor = Color.Red;
                }
                this.fpUnQuit_SheetUndrug.Cells[index, 10].Text = row["����"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 17].Text = row["������ˮ��"].ToString();
                this.fpUnQuit_SheetUndrug.Cells[index, 18].Text = row["������"].ToString();
                this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].Text = ".";
                this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].BackColor = System.Drawing.Color.SkyBlue;
                this.fpUnQuit_SheetUndrug.Rows[index].Visible = false;
            }

        }

        /// <summary>
        /// ���ҩƷ�˷������б�
        /// </summary>
        /// <param name="drugList">ҩƷ</param>
        protected virtual void SetQuitDrug(FS.HISFC.Models.Fee.ReturnApply retrunApply)
        {
            int rowCount = this.fpQuit_SheetDrug.Rows.Count;

            this.fpQuit_SheetDrug.Rows.Add(rowCount, 1);

            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.ItemName, retrunApply.Item.Name);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Specs, retrunApply.Item.Specs);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Price, retrunApply.Item.Price);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Qty, retrunApply.Item.Qty);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Unit, retrunApply.Item.PriceUnit);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2));
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.FeeDate, retrunApply.CancelOper.OperTime);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.IsConfirm, retrunApply.IsConfirmed);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.IsApply, true);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.RecipeNO, retrunApply.RecipeNO);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.SequuenceNO, retrunApply.SequenceNO);
            //�������ϴ�����
            retrunApply.CancelRecipeNO = retrunApply.RecipeNO;
            //�������ϴ����ڲ���ˮ��
            retrunApply.CancelSequenceNO = retrunApply.SequenceNO;
            if (string.IsNullOrEmpty(retrunApply.ExecOrder.ID))
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemlistTemp = inpatientManager.GetItemListByRecipeNO(retrunApply.RecipeNO, retrunApply.SequenceNO, retrunApply.Item.ItemType);
                if (itemlistTemp != null)
                {
                    retrunApply.ExecOrder.ID = itemlistTemp.ExecOrder.ID;
                }
            }
            FS.HISFC.Models.Order.ExecOrder execOrder = orderMgr.QueryExecOrderByExecOrderID(retrunApply.ExecOrder.ID, ((int)FS.HISFC.Models.Base.EnumItemType.Drug).ToString());

            if (execOrder != null)
            {
                this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.UseTime, execOrder.DateUse);
            }
            this.fpQuit_SheetDrug.Rows[rowCount].ForeColor = System.Drawing.Color.Red;

            this.fpQuit_SheetDrug.Rows[rowCount].Tag = retrunApply;
        }

        /// <summary>
        /// ���ҩƷ�˷������б�
        /// </summary>
        /// <param name="drugList">ҩƷ</param>
        protected virtual void SetQuitDrug(FS.HISFC.Models.Pharmacy.ApplyOut returnApplyOut)
        {
            int rowCount = this.fpQuit_SheetDrug.Rows.Count;

            this.fpQuit_SheetDrug.Rows.Add(rowCount, 1);

            //�˷�ԭ��
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.QuitDrugReason, returnApplyOut.Memo);

            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.ItemName, returnApplyOut.Item.Name);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Specs, returnApplyOut.Item.Specs);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Price, returnApplyOut.Item.PriceCollection.RetailPrice);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Qty, returnApplyOut.Operation.ApplyQty);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Unit, returnApplyOut.Item.MinUnit);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(returnApplyOut.Item.PriceCollection.RetailPrice * returnApplyOut.Operation.ApplyQty / returnApplyOut.Item.PackQty, 2));
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.FeeDate, returnApplyOut.Operation.ApplyOper.OperTime);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.IsConfirm, true);     // ��־�Ƿ��ҩ
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.IsApply, true);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.RecipeNO, returnApplyOut.RecipeNO);
            this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.SequuenceNO, returnApplyOut.SequenceNO);
            ////�������ϴ�����
            //returnApplyOut.CancelRecipeNO = returnApplyOut.RecipeNO;
            ////�������ϴ����ڲ���ˮ��
            //returnApplyOut.CancelSequenceNO = returnApplyOut.SequenceNO;
            FS.HISFC.Models.Order.ExecOrder execOrder = orderMgr.QueryExecOrderByExecOrderID(returnApplyOut.ExecNO, ((int)FS.HISFC.Models.Base.EnumItemType.Drug).ToString());

            if (execOrder != null)
            {
                this.fpQuit_SheetDrug.SetValue(rowCount, (int)DrugColumns.UseTime, execOrder.DateUse);
            }
            this.fpQuit_SheetDrug.Rows[rowCount].ForeColor = System.Drawing.Color.Red;

            this.fpQuit_SheetDrug.Rows[rowCount].Tag = returnApplyOut;
        }

        /// <summary>
        /// ��ӷ�ҩƷ�˷������б�
        /// </summary>
        /// <param name="undrugApplyList">��ҩƷ</param>
        protected virtual void SetQuitUnDrug(FS.HISFC.Models.Fee.ReturnApply retrunApply)
        {
            int rowCount = this.fpQuit_SheetUndrug.Rows.Count;

            this.fpQuit_SheetUndrug.Rows.Add(rowCount, 1);

            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.ItemName, retrunApply.Item.Name);

            FS.HISFC.Models.Fee.Item.Undrug undrugInfo = new FS.HISFC.Models.Fee.Item.Undrug();
            undrugInfo = this.undrugManager.GetUndrugByCode(retrunApply.Item.ID);
            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.FeeName, this.inpatientManager.GetMinFeeNameByCode(undrugInfo.MinFee.ID));

            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.Price, retrunApply.Item.Price);
            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.Qty, retrunApply.Item.Qty);
            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.Unit, retrunApply.Item.PriceUnit);
            
            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2));
            //this.fpQuit_SheetUndrug.SetValue(rowCount, (int)DrugColumns.FeeDate, retrunApply.ExecOper.OperTime);
            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.IsConfirm, retrunApply.IsConfirmed);
            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.IsApply, true);
            FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

            deptInfo = this.managerIntegrate.GetDepartment(retrunApply.ExecOper.Dept.ID);
            if (deptInfo == null)
            {
                deptInfo = new FS.HISFC.Models.Base.Department();
                deptInfo.Name = retrunApply.ExecOper.Dept.ID;
            }

            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.ExecDept, deptInfo.Name);
            //�������ϴ�����
            retrunApply.CancelRecipeNO = retrunApply.RecipeNO;
            //�������ϴ����ڲ���ˮ��
            retrunApply.CancelSequenceNO = retrunApply.SequenceNO;

            if (string.IsNullOrEmpty(retrunApply.ExecOrder.ID))
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemlistTemp = inpatientManager.GetItemListByRecipeNO(retrunApply.RecipeNO, retrunApply.SequenceNO, retrunApply.Item.ItemType);
                if (itemlistTemp != null)
                {
                    retrunApply.ExecOrder.ID = itemlistTemp.ExecOrder.ID;
                }
            }
            FS.HISFC.Models.Order.ExecOrder execOrder = orderMgr.QueryExecOrderByExecOrderID(retrunApply.ExecOrder.ID, ((int)FS.HISFC.Models.Base.EnumItemType.UnDrug).ToString());

            if (execOrder != null)
            {
                this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.UseTime, execOrder.DateUse);
            }
            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.RecipeNO, retrunApply.RecipeNO);
            this.fpQuit_SheetUndrug.SetValue(rowCount, (int)UndrugColumns.SequuenceNO, retrunApply.SequenceNO);

            this.fpQuit_SheetUndrug.Rows[rowCount].ForeColor = System.Drawing.Color.Red;
            this.fpQuit_SheetUndrug.Rows[rowCount].Tag = retrunApply;
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        protected virtual void SetPatientInfomation()
        {
            this.txtName.Text = this.patientInfo.Name;
            this.txtPact.Text = this.patientInfo.Pact.Name;
            this.txtDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtBed.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID;
            this.neuTxtInDate.Text = this.patientInfo.PVisit.InTime.ToString();
            this.dtpBeginTime.Focus();

            //ֱ�Ӷ�ȡ����
            this.ClearItemList();

            this.Retrive(true);

            this.txtFilter.Focus();
        }

        /// <summary>
        /// ���δ�˷ѵ�ҩƷ��Ϣ
        /// {C0E249B0-DE20-4d56-9C1E-9952858E32F1}
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int RetriveDrug(DateTime beginTime, DateTime endTime)
        {
            string flag = "1','2";

            // {C0E249B0-DE20-4d56-9C1E-9952858E32F1}
            List<FeeItemList> lstDrugList = new List<FeeItemList>();

            if (this.ckShow.Checked)// {58D72110-55F9-4efb-BCB0-FCD84C68E905}
            {
                lstDrugList = this.inpatientManager.QueryMedItemListsCanQuitEx(this.patientInfo.ID, beginTime, endTime, flag, false);

            }
            else
            {
                lstDrugList = this.inpatientManager.QueryAllMedItemListsCanQuitEx(this.patientInfo.ID, beginTime, endTime, flag, false);

            }
            if (lstDrugList == null)
            {
                MessageBox.Show(Language.Msg("���ҩƷ�б����!") + this.inpatientManager.Err);

                return -1;
            }

            this.SetDrugList(lstDrugList);

            Function.DrawCombFlag(this.fpUnQuit_SheetDrug, 1, 23, 10);

            DataRow row;// {58D72110-55F9-4efb-BCB0-FCD84C68E905}
            int count = this.fpUnQuit_SheetDrug.RowCount;
            for (int i = 0; i < count; i++)
            {
                row = this.dtDrug.Rows[i];

                if (decimal.Parse(row["���"].ToString()) < 0)// {9B75C463-0167-41d4-B42E-DB869D5EFC11}
                {
                    this.fpUnQuit_SheetDrug.Rows[i].ForeColor = Color.Red;
                }

                if (decimal.Parse(row["��������"].ToString()) == 0)
                {
                    this.fpUnQuit_SheetDrug.Rows[i].ForeColor = Color.Red;
                }
            }
            return 1;
        }

        /// <summary>
        /// ���δ�˷ѵķ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int RetriveUnrug(DateTime beginTime, DateTime endTime)
        {
            
            List<FeeItemList> feeItemLists = new List<FeeItemList>();
            if (this.ckShow.Checked)// {58D72110-55F9-4efb-BCB0-FCD84C68E905}
            {
                feeItemLists = this.inpatientManager.QueryFeeItemListsCanQuitEx(this.patientInfo.ID, beginTime, endTime, false);
            }
            else
            {
                feeItemLists = this.inpatientManager.QueryAllFeeItemListsCanQuitEx(this.patientInfo.ID, beginTime, endTime, false);
          
            }


            
            if (feeItemLists == null)
            {
                MessageBox.Show(Language.Msg("��÷�ҩƷ�б����!") + this.inpatientManager.Err);

                return -1;
            }

            this.SetUndrugList(feeItemLists);
            Function.DrawCombFlag(this.fpUnQuit_SheetUndrug, 1, 24, 10);

            return 1;
        }

        /// <summary>
        /// ��������˷�������Ϣ
        /// </summary>
        /// <param name="isPharmarcy"></param>
        private void RetrieveReturnApply(bool isPharmarcy)
        {
            if (isPharmarcy)
            {
                this.fpQuit_SheetDrug.Rows.Count = 0;
            }
            else
            {
                this.fpQuit_SheetUndrug.Rows.Count = 0;
            }

            //��ȡ�˷�������Ϣ
            ArrayList returnApplys = this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, isPharmarcy);
            if (returnApplys == null)
            {
                MessageBox.Show(Language.Msg("����˷�������Ϣ����"));

                return;
            }


            //��ȡҩƷ��ҩ������Ϣ
            if (isPharmarcy)
            {
                ArrayList alQuitDrug = this.phamarcyIntegrate.QueryDrugReturn("AAAA"/*this.operDept.ID*/, "AAAA", this.patientInfo.ID);
                if (alQuitDrug == null)
                {
                    MessageBox.Show(Language.Msg("��ȡ��ҩ������Ϣ��������"));
                    return;
                }
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alQuitDrug)
                {
                    FS.HISFC.Models.Fee.ReturnApply temp = new FS.HISFC.Models.Fee.ReturnApply();

                    temp.Item = info.Item;
                    temp.CancelOper.OperTime = info.Operation.ApplyOper.OperTime;
                    temp.IsConfirmed = true;            //��־�Ƿ��ҩ
                    temp.Item.Qty = info.Operation.ApplyQty;
                    temp.Item.Price = info.Item.PriceCollection.RetailPrice;
                    temp.Item.PriceUnit = info.Item.MinUnit;

                    temp.User01 = "QuitDrugApply";
                    info.User01 = "QuitDrugApply";

                    //  returnApplys.Add(temp);

                    this.SetQuitDrug(info);
                }
            }

            //Hashtable �����ж��Ƿ�������Ŀ
            Hashtable hsPackageItem = new Hashtable();
            //�۸񼯺�
            Dictionary<string, decimal> priceCollection = new Dictionary<string, decimal>();
            Dictionary<string, string> priceItemCollection = new Dictionary<string, string>();

            foreach (FS.HISFC.Models.Fee.ReturnApply retrunApply in returnApplys)
            {
                //�Ѿ�ִ�й��ն��˷�ȷ�ϵģ���������ȡ��
                if (retrunApply.IsConfirmed)
                {
                    continue;
                }

                if (retrunApply.User01 != "QuitDrugApply")
                {
                    retrunApply.User01 = "QuitFeeApply";
                }

                //ҩƷ
                //if (retrunApply.Item.IsPharmacy)
                if (retrunApply.Item.ItemType == EnumItemType.Drug)
                {
                    this.SetQuitDrug(retrunApply);
                }
                else
                {


                    //List<HISFC.Models.Material.Output> outlist = returnApplyManager.QueryOutPutByApplyNo(retrunApply.ID, FS.HISFC.Models.Base.CancelTypes.Canceled);
                    List<HISFC.Models.Fee.ReturnApplyMet> returnlist = returnApplyManager.QueryReturnApplyMetByApplyNo(retrunApply.ID, FS.HISFC.Models.Base.CancelTypes.Canceled);
                    //���������¼
                    retrunApply.ApplyMateList = returnlist;
                    //ͨ�����������¼�γ����ʳ����¼
                    retrunApply.MateList = this.GetOutPutByApplyItem(retrunApply);


                    if (isQuitFeeByPackage)
                    {
                        if (string.IsNullOrEmpty(retrunApply.UndrugComb.ID) == false)
                        {
                            string key = retrunApply.UndrugComb.ID + "|" + retrunApply.TransType.ToString() + (string.IsNullOrEmpty(retrunApply.ExecOrder.ID) ? retrunApply.FeeOper.OperTime.ToString() : retrunApply.ExecOrder.ID);

                            if (priceCollection.ContainsKey(key))
                            {
                                if (priceItemCollection.ContainsKey(key + retrunApply.Item.ID) == false)
                                {
                                    if (retrunApply.UndrugComb.Qty != 0)
                                    {
                                        priceCollection[key] += Math.Abs(FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / (retrunApply.Item.PackQty * retrunApply.UndrugComb.Qty), 2));
                                    }
                                    else
                                    {
                                        priceCollection[key] += Math.Abs(FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2));
                                    }
                                    priceItemCollection[key + retrunApply.Item.ID] = retrunApply.Item.ID;
                                }
                            }
                            else
                            {
                                if (retrunApply.UndrugComb.Qty != 0)
                                {
                                    priceCollection[key] = Math.Abs(FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / (retrunApply.Item.PackQty * retrunApply.UndrugComb.Qty), 2));
                                }
                                else
                                {
                                    priceCollection[key] = Math.Abs(FS.FrameWork.Public.String.FormatNumber(retrunApply.Item.Price * retrunApply.Item.Qty / retrunApply.Item.PackQty, 2));
                                }
                                priceItemCollection[key + retrunApply.Item.ID] = retrunApply.Item.ID;
                            }

                            if (hsPackageItem.ContainsKey(key))
                            {
                                ((ArrayList)hsPackageItem[key]).Add(retrunApply);
                            }
                            else
                            {
                                //�½�
                                ArrayList al = new ArrayList();
                                al.Add(retrunApply);
                                hsPackageItem[key] = al;
                            }
                            continue;
                        }
                    }
                    this.SetQuitUnDrug(retrunApply);
                }
            }

            if (isQuitFeeByPackage)
            {

                if (hsPackageItem.Count > 0)
                {
                    foreach (DictionaryEntry de in hsPackageItem)
                    {
                        string packagecode = de.Key.ToString().Split('|')[0];
                        ArrayList al = de.Value as ArrayList;
                        if (al != null && al.Count > 0)
                        {
                            FS.HISFC.Models.Fee.ReturnApply retrunApply = (al[0] as FS.HISFC.Models.Fee.ReturnApply).Clone();

                            decimal sumCost = 0.00M;
                            foreach (FS.HISFC.Models.Fee.ReturnApply feeItemList in al)
                            {
                                if (feeItemList.Item.PackQty == 0)
                                {
                                    feeItemList.Item.PackQty = 1;
                                }
                                sumCost += FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                            }

                            //���Ҹ�����Ŀ
                            FS.HISFC.Models.Fee.Item.Undrug undrug = this.feeIntegrate.GetUndrugByCode(packagecode);
                            retrunApply.Item = undrug;
                            //���Ҹ�����Ŀ
                            if (priceCollection.ContainsKey(de.Key.ToString()))
                            {
                                retrunApply.Item.Price = priceCollection[de.Key.ToString()];
                            }
                            else
                            {
                                retrunApply.Item.Price = sumCost;
                            }

                            retrunApply.Item.Qty = sumCost / retrunApply.Item.Price;

                            if (retrunApply.Item.PackQty == 0)
                            {
                                retrunApply.Item.PackQty = 1;
                            }
                            this.SetQuitUnDrug(retrunApply);

                            hsQuitFeeByPackage.Add("Quit" + retrunApply.ID + retrunApply.UndrugComb.ID, al);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ͨ���������������γ����ʳ���ʵ��
        /// </summary>
        /// <param name="feeitemList"></param>
        /// <returns></returns>
        private List<HISFC.Models.FeeStuff.Output> GetOutPutByApplyItem(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            List<HISFC.Models.FeeStuff.Output> list = new List<FS.HISFC.Models.FeeStuff.Output>();
            HISFC.Models.FeeStuff.Output outItem = null;
            if (returnApply.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                foreach (HISFC.Models.Fee.ReturnApplyMet applyMet in returnApply.ApplyMateList)
                {
                    outItem = new FS.HISFC.Models.FeeStuff.Output();
                    outItem.StoreBase.Item = applyMet.Item;
                    outItem.ReturnApplyNum = applyMet.Item.Qty;
                    outItem.RecipeNO = applyMet.RecipeNo;
                    outItem.SequenceNO = FrameWork.Function.NConvert.ToInt32(applyMet.SequenceNo);
                    outItem.StoreBase.StockNO = applyMet.StockNo;
                    outItem.ID = applyMet.OutNo;
                    list.Add(outItem);
                }
            }
            return list;
        }

        /// <summary>
        /// ��ȡδ�˷���Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Retrive(bool isRetrieveRetrunApply)
        {
            if (this.patientInfo == null)
            {
                MessageBox.Show(Language.Msg("�����뻼�߻�����Ϣ!"));

                return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ã����Ժ�...");
            Application.DoEvents();
            this.ClearItemList();
            DateTime beginTime = this.dtpBeginTime.Value;
            DateTime endTime = this.dtpEndTime.Value;

            //���ݴ��ڿɲ�������Ŀ���,��ȡδ�˷ѵ���Ŀ��Ϣ
            switch (this.itemType)
            {
                case ItemTypes.Pharmarcy:
                    this.RetriveDrug(beginTime, endTime);
                    if (isRetrieveRetrunApply)
                    {
                        this.RetrieveReturnApply(true);
                    }

                    break;

                case ItemTypes.Undrug:
                    this.RetriveUnrug(beginTime, endTime);
                    if (isRetrieveRetrunApply)
                    {
                        this.RetrieveReturnApply(false);
                    }

                    break;

                case ItemTypes.All:
                    this.RetriveDrug(beginTime, endTime);
                    this.RetriveUnrug(beginTime, endTime);
                    if (isRetrieveRetrunApply)
                    {
                        this.RetrieveReturnApply(true);
                        this.RetrieveReturnApply(false);
                    }

                    break;
            }

            this.fpUnQuit_SheetDrug.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            this.fpUnQuit_SheetUndrug.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// ���˲�����Ŀ
        /// </summary>
        protected virtual void SetFilter()
        {
            string filterString = string.Empty;

            filterString = this.txtFilter.Text.Trim();

            //ȥ�����ܵ��¹��˱����������ַ�
            filterString = FS.FrameWork.Public.String.TakeOffSpecialChar(filterString, new string[] { "[", "'" });

            //�����ǰ�ҳ����δ��ҩƷʱ�Ĺ���
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                dvDrug.RowFilter = "ҩƷ���� like '" + "%" + filterString + "%'" + " OR " + "ƴ���� like '" + "%" + filterString + "%'" + " OR " + "����� like '" + "%" + filterString + "%'";

                //���¶�ȡ�е�˳��Ϳ�ȵ���Ϣ
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);
                DataRow row;// {58D72110-55F9-4efb-BCB0-FCD84C68E905}
                int count = this.fpUnQuit_SheetDrug.RowCount;
                for (int i = 0; i < count; i++)
                {
                    row = this.dtDrug.Rows[i];

                    if (decimal.Parse(this.fpUnQuit_SheetDrug.Cells[i, 7].Text) < 0)
                    {
                        this.fpUnQuit_SheetDrug.Rows[i].ForeColor = Color.Red;
                    }
                    if (decimal.Parse(this.fpUnQuit_SheetDrug.Cells[i, 5].Text) == 0)
                    {
                        this.fpUnQuit_SheetDrug.Rows[i].ForeColor = Color.Red;
                    }
                }
            }
            //�����ǰ�ҳ����δ�˷�ҩƷʱ�Ĺ���
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetUndrug)
            {
                //dvUndrug.RowFilter = "��Ŀ���� like '" + filterString + "%'" + " OR " + "ƴ���� like '" + filterString + "%'";

                //���¶�ȡ�е�˳��Ϳ�ȵ���Ϣ
                //FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
                this.SetUndragFp(filterString);
                //DataRow row;
                //int count = this.fpUnQuit_SheetUndrug.RowCount;
                //for (int i = 0; i < count; i++)
                //{
                //    row = this.dtDrug.Rows[i];

                //    if (decimal.Parse(row["��������"].ToString()) == 0)
                //    {
                //        this.fpUnQuit_SheetUndrug.Rows[i].ForeColor = Color.Red;
                //    }
                //}
            }
        }

        /// <summary>
        /// ������������������λ��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="sv">Ҫ���ҵ�SheetView</param>
        /// <returns>�ɹ� ������λ�� ʧ�� -1</returns>
        private int FindColumn(string name, FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.Columns.Count; i++)
            {
                if (sv.Columns[i].Label == name)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// ��ʾѡ����˷���Ϣ
        /// </summary>
        /// <param name="feeItemList">������Ϣʵ��</param>
        protected virtual void SetItemToDeal(FeeItemList feeItemList)
        {
            HISFC.Models.FeeStuff.Output outItem = null;
            bool isFind = false;
            if (feeItemList.MateList.Count == 0)
            {
                isFind = false;

            }
            else
            {
                DataRow[] vdr = dtMate.Select("������ˮ�� ='" + feeItemList.MateList[0].ID + "'");
                if (vdr.Length <= 1)
                {
                    isFind = false;
                }
                else
                {
                    isFind = true;
                }

            }
            if (!isFind)
            {
                this.txtItemName.Text = feeItemList.Item.Name;
                this.txtItemName.Tag = feeItemList;
                this.txtPrice.Text = feeItemList.Item.Price.ToString();
                this.mtbQty.Text = feeItemList.NoBackQty.ToString();
                this.txtUnit.Text = feeItemList.Item.PriceUnit;
            }
            else
            {
                outItem = feeItemList.MateList[0];
                this.txtItemName.Text = outItem.StoreBase.Item.Name;
                this.txtItemName.Tag = feeItemList;
                this.txtPrice.Text = outItem.StoreBase.Item.Price.ToString();
                this.mtbQty.Text = outItem.StoreBase.Quantity.ToString();
                this.txtUnit.Text = feeItemList.Item.PriceUnit;
            }
            neuPanel1.Focus();
            neuPanel3.Focus();
            neuPanel4.Focus();
            gbQuitItem.Focus();
            this.mtbQty.Focus();
        }


        /// <summary>
        ///  liuww
        /// </summary>
        public void SelectGroupItem()
        {

            if (this.fpUnQuit.ActiveSheet.Rows.Count <= 0)
            {
                return;
            }
            if (this.fpUnQuit.ActiveSheet.ActiveRow == null)
            {
                MessageBox.Show("����ѡ��Ҫ�˵���Ŀ");
                return;
            }
            //if (this.fpUnQuit.ActiveSheet.ActiveRow.Tag.ToString() == null || this.fpUnQuit.ActiveSheet.ActiveRow.Tag.ToString() == "") return;

            //FeeItemList ItemList, SampleItemList;

            //ItemList = (FeeItemList)this.fpUnQuit.ActiveSheet.ActiveRow.Tag;

            DataRow row;
            if (this.fpUnQuit.ActiveSheetIndex == 0)
            {

                row = this.dtDrug.Rows[this.fpUnQuit.ActiveSheet.ActiveRowIndex];

            }
            else
            {

                row = this.dtUndrug.Rows[this.fpUnQuit.ActiveSheet.ActiveRowIndex];
            }
            if (decimal.Parse(row["��������"].ToString()) == 0)// {58D72110-55F9-4efb-BCB0-FCD84C68E905}
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڴ����˷����� ���Ժ�");

            if (row["ҽ����"].ToString() == "" || row["��Ϻ�"].ToString() == "")
            {
                this.ChooseUnquitItem();

            }
            else
            {
                for (int i = 0; i < this.fpUnQuit.ActiveSheet.Rows.Count; i++)
                {
                    DataRow dataRow;


                    if (this.fpUnQuit.ActiveSheetIndex == 0)
                    {
                        dataRow = this.dtDrug.Rows[i];
                    }
                    else
                    {
                        dataRow = this.dtUndrug.Rows[i];
                    }


                    if (dataRow != null && dataRow["��Ϻ�"].ToString() != "" && row["��Ϻ�"].ToString() == dataRow["��Ϻ�"].ToString() && row["Ӧִ��ʱ��"].ToString() == dataRow["Ӧִ��ʱ��"].ToString())
                    {
                        this.fpUnQuit.ActiveSheet.ActiveRowIndex = i;
                        this.ChooseUnquitItem();


                    }


                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }





        /// <summary>
        /// ˫����Ŀ�����¼�
        /// </summary>
        protected virtual void ChooseUnquitItem()
        {
            string recipeNO = string.Empty;//������
            int recipeSequence = 0;//��������Ŀ��ˮ��
            decimal noBackQty = 0;//��������
            decimal totCost = 0;//���
            //bool isPharmarcy = false;//�Ƿ�ҩƷ
            EnumItemType isPharmarcy = EnumItemType.Drug;

            //�жϵ�ǰ�������Ϣ�Ƿ�ΪҩƷ,����ҳ��ҩƷҳ,��ô˵���������ҩƷ,����Ϊ��ҩƷ
            //isPharmarcy = this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug;
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                isPharmarcy = EnumItemType.Drug;

            }
            else
            {

                isPharmarcy = EnumItemType.UnDrug;
            }

            if (this.fpUnQuit.ActiveSheet.RowCount == 0)
            {
                return;
            }

            int index = this.fpUnQuit.ActiveSheet.ActiveRowIndex;
            #region �Ƿ��������������ҷ���
            if (!isCanQuitOtherFee)
            {
                string FeeOperDept = fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("ִ�п���", this.fpUnQuit.ActiveSheet));
                //if (FeeOperDept!= ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name)
                if (this.depts.ContainsValue(FeeOperDept) == false)
                {
                    MessageBox.Show("���������������ҷ���");
                    return;
                }
            }
            #endregion

            #region �Ƿ���������������Ա����
            // {097418EF-9E96-48d4-9E6C-46CCC111C78C} ModifyBy �Ծ�
            if (this.IsCanQuitOtherOperator == false)
            {
                string recipeTempNO = this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("������", this.fpUnQuit.ActiveSheet));
                int recipeID = 0;
                try
                {
                    recipeID = Int32.Parse(this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("������ˮ��", this.fpUnQuit.ActiveSheet)));
                }
                catch (Exception e)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ˮ�Ÿ�ʽ������" + e.Message));
                    return;
                }
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeTempItemList = this.inpatientManager.GetItemListByRecipeNO(recipeTempNO, recipeID, isPharmarcy);
                if (feeTempItemList.FeeOper.ID != FS.FrameWork.Management.Connection.Operator.ID)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������������շ�Ա�ķ�����Ϣ��"));
                    return;
                }
            }

            #endregion

            #region ����(��ҩƷ��Ӧ��������)
            //�Ƿ��Ƕ�������
            //bool isMate = false;
            List<HISFC.Models.FeeStuff.Output> outitemList = new List<FS.HISFC.Models.FeeStuff.Output>();
            string headerText = this.fpUnQuit.ActiveSheet.RowHeader.Cells[index, 0].Text;
            //��������
            if (isPharmarcy == EnumItemType.UnDrug)
            {
                if (headerText == "+" || headerText == "-")
                {
                    if (!this.ckbAllQuit.Checked)
                    {

                        if (!this.ckbAllQuit.Checked && headerText != ".")
                        {
                            MessageBox.Show("��ѡ��Ҫ�˷ѵ�������Ϣ��");
                            if (this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].Text == "+")
                            {
                                this.ExpandOrCloseRow(false, index + 1);
                            }
                            return;
                        }
                    }
                }
                outitemList = QuiteAllMate(index);
                index = this.FinItemRowIndex(index);
            }

            #endregion

            recipeNO = this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("������", this.fpUnQuit.ActiveSheet));
            recipeSequence = NConvert.ToInt32(this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("������ˮ��", this.fpUnQuit.ActiveSheet)));
            noBackQty = NConvert.ToDecimal(this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("��������", this.fpUnQuit.ActiveSheet)));
            totCost = NConvert.ToDecimal(this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("���", this.fpUnQuit.ActiveSheet)));
            if (totCost < 0)// {9B75C463-0167-41d4-B42E-DB869D5EFC11}
            {
                return;
            }
            //��÷��õ�������Ϣ,��ΪDataTable�в��ܰ�Tag,��Ϣ��ȫ
            FeeItemList feeItemList = this.inpatientManager.GetItemListByRecipeNO(recipeNO, recipeSequence, isPharmarcy);
            if (feeItemList == null)
            {
                MessageBox.Show(Language.Msg("�����Ŀ������Ϣ����!") + this.inpatientManager.Err);

                return;
            }
            //��ֹ��Ժ����ʱ��ʿδ�˳��˷��������������˷�
            if (feeItemList.BalanceState == "1")
            {
                if (DialogResult.No == MessageBox.Show(Language.Msg("����Ŀ�Ѿ����㣬�����²�ѯ���ڽ����˷ѣ�лл!") + this.inpatientManager.Err))
                {
                    return;
                }
            }
            //��NoBackQty�����б��浱ǰ�Ŀ�������,��Ϊ������ϵĿ��ܱ������˶��.
            feeItemList.NoBackQty = noBackQty;

            //if (isMate)
            //{
            //feeItemList.MateList.Add(outitemList.);
            if (feeItemList.Item.ItemType == EnumItemType.UnDrug && outitemList.Count > 0)
            {
                feeItemList.MateList = outitemList;
            }

            if (isQuitFeeByPackage && isPharmarcy == EnumItemType.UnDrug)
            {
                string packagecode = this.fpUnQuit.ActiveSheet.GetText(index, this.FindColumn("����", this.fpUnQuit.ActiveSheet));
                string key = feeItemList.UndrugComb.ID + "|" + feeItemList.TransType.ToString() + "|" + feeItemList.RecipeNO + feeItemList.ExecOrder.ID;

                if (!string.IsNullOrEmpty(feeItemList.UndrugComb.ID))
                {
                    //˵������
                    feeItemList = hsQuitFeeByPackage[key] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    feeItemList.NoBackQty = noBackQty;
                    this.QuitOperation(feeItemList.Clone());
                    return;
                }

            }

            //{F4912030-EF65-4099-880A-8A1792A3B449}
            //�Ǹ�����Ŀ�����Ҹ�����Ŀ����ȫ�ˣ����ﴦ��
            //ѭ����������moorder��packcodeһ������Ŀ��һ������.
            //����Ĭ�ϱ���ȫ�ˣ���ʹ������Ŀ��������1����Ȼ��������̫�鷳��
            //
            // ֱ���շѵ� moorder Ϊ�գ���ѯ�����⣻
            // ���Ե� moorder Ϊ��ʱ������ recipeNO
            //
            if (this.isCombItemAllQuit && isPharmarcy == EnumItemType.UnDrug && !string.IsNullOrEmpty(feeItemList.UndrugComb.ID))
            {
                MessageBox.Show("��ѡ����˷���Ŀ���븴����Ŀ������С���ǰ���ã�������Ŀ����ȫ�ˣ���ע��!");

                ArrayList recipeNOs = null;
                if (string.IsNullOrEmpty(feeItemList.Order.ID))
                {
                    recipeNOs = this.inpatientManager.QueryRecipesByRepriceNO(feeItemList.RecipeNO, feeItemList.ExecOrder.ID, feeItemList.UndrugComb.ID);
                }
                else
                {
                    recipeNOs = this.inpatientManager.QueryRecipesByMoOrder(feeItemList.Order.ID, feeItemList.UndrugComb.ID);
                }
                if (recipeNOs == null)
                {
                    MessageBox.Show("û���ҵ���ͬ�ĸ�����Ŀ!" + this.inpatientManager.Err);

                    return;
                }
                foreach (FS.FrameWork.Models.NeuObject o in recipeNOs)
                {
                    FeeItemList f = this.inpatientManager.GetItemListByRecipeNO(o.ID, NConvert.ToInt32(o.Name), isPharmarcy);
                    if (f == null)
                    {
                        MessageBox.Show(Language.Msg("�����Ŀ������Ϣ����!") + this.inpatientManager.Err);

                        return;
                    }

                    this.QuitOperation(f.Clone());
                }

                return;
            }//{F4912030-EF65-4099-880A-8A1792A3B449}����
            //}
            //else
            //{
            //    DataRow[] vdr = dtMate.Select("������ˮ�� ='" + feeItemList.UpdateSequence + "'");
            //    if (vdr.Length > 0)
            //    {
            //        feeItemList.MateList.Add(this.GetOutPutByDataRow(vdr[0]));
            //    }
            //}

            //���ȫ�˰�ťΪѡ��,��ô��ȫ�˵�ǰ�Ŀ�������.
            //{7A07D8BE-FEEA-42b4-B515-4699951333E8} ������Ϊ1�Ĳ���������
            //if (this.ckbAllQuit.Checked || (this.ckbAllQuit.Checked == false && feeItemList.Days != 1 && feeItemList.Item.ItemType == EnumItemType.Drug))

            if (this.ckbAllQuit.Checked)
            {
                this.QuitOperation(feeItemList.Clone());
            }
            else//����,������������������ĶԻ���,��������Ҫ�˵�����
            {
                this.SetItemToDeal(feeItemList.Clone());
            }
        }

        /// <summary>
        /// ����dtmate�е�DataRow�γ�OutPutʵ��
        /// </summary>
        /// <param name="dr">dtmate�е�DataRow</param>
        /// <returns></returns>
        private HISFC.Models.FeeStuff.Output GetOutPutByDataRow(DataRow dr)
        {
            HISFC.Models.FeeStuff.Output outItem = new FS.HISFC.Models.FeeStuff.Output();
            outItem.StoreBase.Item.Name = dr["��Ŀ����"].ToString();
            outItem.StoreBase.Item.MinFee.ID = this.objectHelperMinFee.GetID(dr["��������"].ToString());
            outItem.StoreBase.Item.Specs = dr["���"].ToString();
            outItem.StoreBase.Item.Price = NConvert.ToDecimal(dr["�۸�"]);
            outItem.StoreBase.Quantity = NConvert.ToDecimal(dr["��������"]);
            if (this.ckbAllQuit.Checked)
            {
                outItem.ReturnApplyNum = NConvert.ToDecimal(dr["��������"]);
            }
            outItem.StoreBase.Item.PriceUnit = dr["��λ"].ToString();
            outItem.ID = dr["������ˮ��"].ToString();
            outItem.StoreBase.Item.ID = dr["����"].ToString();
            //������
            outItem.StoreBase.StockNO = dr["������"].ToString();
            return outItem;
        }

        /// <summary>
        /// ȫ�˷�ҩƷ������
        /// </summary>
        /// <returns></returns>
        private List<HISFC.Models.FeeStuff.Output> QuiteAllMate(int index)
        {
            string headerText = this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].Text;
            string stockNo = string.Empty; //���ʳ������
            string outNo = string.Empty; //���ʳ������
            List<HISFC.Models.FeeStuff.Output> list = new List<FS.HISFC.Models.FeeStuff.Output>();
            FS.HISFC.Models.FeeStuff.Output outItem = null;
            //ȫ�˷�ҩƷ
            if (headerText != ".")
            {
                outNo = fpUnQuit_SheetUndrug.GetText(index, this.FindColumn("������ˮ��", fpUnQuit_SheetUndrug));
                DataRow[] vdr = dtMate.Select("������ˮ�� ='" + outNo + "'");
                if (vdr.Length == 0)
                    return list;

                foreach (DataRow dr in vdr)
                {
                    outItem = this.GetOutPutByDataRow(dr);
                    if (outItem == null)
                    {
                        MessageBox.Show("����������Ŀ��Ϣʧ��");
                        return null;
                    }
                    list.Add(outItem);
                }
            }
            //ȫ������
            if (headerText == ".")
            {
                outItem = QuiteMate(index);
                if (outItem == null)
                {
                    MessageBox.Show("����������Ŀ��Ϣʧ��");
                    return null;
                }
                list.Add(outItem);
            }
            return list;
        }

        /// <summary>
        /// ��ǰ�˷ѵ�����(��ҩƷ���յ�����)
        /// </summary>
        /// <param name="index">��ǰѡ�е���</param>
        /// <param name="IsAllQuite">�Ƿ�ȫ��</param>
        /// <returns></returns>
        private HISFC.Models.FeeStuff.Output QuiteMate(int index)
        {
            string headerText = this.fpUnQuit_SheetUndrug.RowHeader.Cells[index, 0].Text;
            string stockNo = string.Empty; //���ʳ������
            string outNo = string.Empty; //���ʳ������
            HISFC.Models.FeeStuff.Output outItem = null;
            if (headerText == ".")
            {
                stockNo = fpUnQuit_SheetUndrug.GetText(index, this.FindColumn("������", fpUnQuit_SheetUndrug));
                outNo = fpUnQuit_SheetUndrug.GetText(index, this.FindColumn("������ˮ��", fpUnQuit_SheetUndrug));
                DataRow dr = this.FindMateRow(stockNo, outNo);
                if (dr == null)
                {
                    MessageBox.Show(Language.Msg("������ʻ�����Ϣ����!") + this.mateInteger.Err);
                    return null;
                }
                outItem = this.GetOutPutByDataRow(dr);
                if (outItem == null)
                {
                    MessageBox.Show(Language.Msg("������ʻ�����Ϣ����!") + this.mateInteger.Err);
                    return null;
                }
            }
            return outItem;
        }

        /// <summary>
        /// �������������յķ�ҩƷ����Ӧ����
        /// </summary>
        /// <param name="rowIndex">�������ڵ���</param>
        /// <returns></returns>
        private int FinItemRowIndex(int rowIndex)
        {
            for (int i = rowIndex; i >= 0; i--)
            {
                if (this.fpUnQuit_SheetUndrug.RowHeader.Cells[i, 0].Text != ".")
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// �����Ƿ����������Ŀ
        /// </summary>
        /// <param name="feeItemList">���û�����Ϣʵ��</param>
        /// <returns>�ɹ� �Ѿ����ڷ��õ�index, û���ҵ� -1</returns>
        protected virtual int FindQuitItem(FeeItemList feeItemList)
        {
            //�����ҩƷ,������ҩƷҳ���ұ����Ѿ��˹�����Ŀ
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                for (int i = 0; i < this.fpQuit_SheetDrug.RowCount; i++)
                {
                    if (this.fpQuit_SheetDrug.Rows[i].Tag == null)
                    {
                        continue;
                    }
                    //�����ѱ�����˷����� �����д���
                    if (this.fpQuit_SheetDrug.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        FS.HISFC.Models.Fee.ReturnApply myReturnApply = this.fpQuit_SheetDrug.Rows[i].Tag as FS.HISFC.Models.Fee.ReturnApply;
                        //��ͬ�Ĵ����źʹ�������ˮ�Ų�������������
                        if (feeItemList.RecipeNO == myReturnApply.RecipeNO && feeItemList.SequenceNO == myReturnApply.SequenceNO)
                        {
                            MessageBox.Show(feeItemList.Item.Name + "�����ţ�" + feeItemList.RecipeNO + " ��������ˮ�ţ�" + feeItemList.SequenceNO + " �Ѿ������˷�����" + System.Environment.NewLine
                                + "����ִ���˷�ȷ�ϲ�������ȡ���ϴ��˷����룬����ִ���˷�����", "��ʾ");
                            return -2;
                        }
                        // continue;
                    }
                    if (this.fpQuit_SheetDrug.Rows[i].Tag is FS.HISFC.Models.Pharmacy.ApplyOut)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut myApplyOut = this.fpQuit_SheetDrug.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                        //��ͬ�Ĵ����źʹ�������ˮ�Ų�������������
                        if (feeItemList.RecipeNO == myApplyOut.RecipeNO && feeItemList.SequenceNO == myApplyOut.SequenceNO)
                        {
                            MessageBox.Show(feeItemList.Item.Name + "�����ţ�" + feeItemList.RecipeNO + " ��������ˮ�ţ�" + feeItemList.SequenceNO + " �Ѿ�������ҩ����" + System.Environment.NewLine
                                + "����ִ����ҩ��������ȡ���ϴ���ҩ���룬����ִ����ҩ����", "��ʾ");
                            return -2;
                        }
                        // continue;
                    }

                    if (this.fpQuit_SheetDrug.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList temp = this.fpQuit_SheetDrug.Rows[i].Tag as FeeItemList;

                        if (temp.RecipeNO == feeItemList.RecipeNO && temp.SequenceNO == feeItemList.SequenceNO)
                        {
                            return i;
                        }
                    }
                }
            }
            else //����Ƿ�ҩƷ,�����˷�ҩƷҳ���ұ����Ѿ��˹�����Ŀ
            {
                for (int i = 0; i < this.fpQuit_SheetUndrug.RowCount; i++)
                {
                    if (this.fpQuit_SheetUndrug.Rows[i].Tag == null)
                    {
                        continue;
                    }
                    //�����ѱ�����˷����� �����д���
                    if (this.fpQuit_SheetUndrug.Rows[i].Tag is FS.HISFC.Models.Fee.ReturnApply)
                    {
                        continue;
                    }

                    if (this.fpQuit_SheetUndrug.Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList temp = this.fpQuit_SheetUndrug.Rows[i].Tag as FeeItemList;

                        if (temp.RecipeNO == feeItemList.RecipeNO && temp.SequenceNO == feeItemList.SequenceNO)
                        {
                            return i;
                        }

                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// ����δ����Ŀ
        /// </summary>
        /// <param name="feeItemList">��Ŀ��Ϣʵ��</param>
        /// <returns>�ɹ� ��ǰ�� ʧ�� null</returns>
        protected virtual DataRow FindUnquitItem(FeeItemList feeItemList)
        {
            DataRow rowFind = null;

            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                rowFind = dtDrug.Rows.Find(new object[] { feeItemList.RecipeNO, feeItemList.SequenceNO });
            }
            if (feeItemList.Item.ItemType == EnumItemType.UnDrug || feeItemList.Item.ItemType == EnumItemType.MatItem)
            {
                rowFind = dtUndrug.Rows.Find(new object[] { feeItemList.RecipeNO, feeItemList.SequenceNO });
            }
            return rowFind;
        }

        /// <summary>
        /// ���һ��������Ŀ
        /// </summary>
        /// <param name="feeItemList">��Ŀ��Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SetNewQuitItem(FeeItemList feeItemList)
        {
            //ҩƷ
            //if (feeItemList.Item.IsPharmacy)
            bool isApply = true;
            if (this.isDirBackFee)//ֱ���˷ѵĲ���Ҫ����
            {
                isApply = false;
            }
            else
            {
                if (this.isCanQuitOtherFee == false && this.depts.ContainsKey(feeItemList.ExecOper.Dept.ID) == false && this.isCurrentDeptNeedAppy == false)//���������������ҵģ�����ִ�п��Ҳ����ڵ�ǰ�����ģ�����Ҫ�˷�����
                {
                    isApply = false;
                }
                else if (this.depts.ContainsKey(feeItemList.ExecOper.Dept.ID) && this.isCurrentDeptNeedAppy == false)//���ִ�п����ǵ�ǰ���������Ҳ���Ҫ����ģ�ֱ���˷�
                {
                    isApply = false;
                }
                else
                {
                    isApply = true;
                }
            }

            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                this.fpQuit_SheetDrug.Rows.Add(this.fpQuit_SheetDrug.RowCount, 1);

                int index = this.fpQuit_SheetDrug.RowCount - 1;
                //��ҩԭ��
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.QuitDrugReason, this.cmbQuitFeeReason.Text);
                feeItemList.Item.Memo = cmbQuitFeeReason.Text;
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.ItemName, feeItemList.Item.Name);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Specs, feeItemList.Item.Specs);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Price, feeItemList.Item.Price);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Qty, feeItemList.NoBackQty);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Unit, feeItemList.Item.PriceUnit);
               
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2));
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.FeeDate, feeItemList.FeeOper.OperTime);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.IsConfirm, feeItemList.PayType == FS.HISFC.Models.Base.PayTypes.SendDruged ? true : false);



                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.IsApply, isApply);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.RecipeNO, feeItemList.RecipeNO);
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.SequuenceNO, feeItemList.SequenceNO.ToString());
                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.UseTime, feeItemList.ExecOrder.DateUse.ToString());
                //�������ϴ�����
                feeItemList.CancelRecipeNO = feeItemList.RecipeNO;
                //���������ڲ�������ˮ��
                feeItemList.CancelSequenceNO = feeItemList.SequenceNO;
                this.fpQuit_SheetDrug.Rows[index].Tag = feeItemList;

            }
            else
            {
                this.fpQuit_SheetUndrug.Rows.Add(this.fpQuit_SheetUndrug.RowCount, 1);

                int index = this.fpQuit_SheetUndrug.RowCount - 1;

                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ItemName, feeItemList.Item.Name);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.FeeName, this.inpatientManager.GetMinFeeNameByCode(feeItemList.Item.MinFee.ID));
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Price, feeItemList.Item.Price);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Qty, feeItemList.NoBackQty);

                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Unit, feeItemList.Item.PriceUnit);
               
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2));
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.IsConfirm, feeItemList.IsConfirmed);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.IsApply, isApply);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.RecipeNO, feeItemList.RecipeNO);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.SequuenceNO, feeItemList.SequenceNO);
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.UseTime, feeItemList.ExecOrder.DateUse.ToString());
                FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();

                deptInfo = this.managerIntegrate.GetDepartment(feeItemList.ExecOper.Dept.ID);
                if (deptInfo == null)
                {
                    deptInfo = new FS.HISFC.Models.Base.Department();
                    deptInfo.Name = feeItemList.ExecOper.Dept.ID;
                }
                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.ExecDept, deptInfo.Name);
                //�������ϴ�����
                feeItemList.CancelRecipeNO = feeItemList.RecipeNO;
                //���������ڲ�������ˮ��
                feeItemList.CancelSequenceNO = feeItemList.SequenceNO;
                this.fpQuit_SheetUndrug.Rows[index].Tag = feeItemList;
            }

            return 1;
        }

        /// <summary>
        /// ���һ���Ѿ����ڵ��˷���Ϣ
        /// </summary>
        /// <param name="feeItemList">������Ϣʵ��</param>
        /// <param name="index">�ҵ����Ѿ����ڵ��˷Ѽ�¼����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SetExistQuitItem(FeeItemList feeItemList, int index)
        {
            //ҩƷ
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                FeeItemList temp = this.fpQuit_SheetDrug.Rows[index].Tag as FeeItemList;

                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Qty, feeItemList.NoBackQty + temp.NoBackQty);

                temp.NoBackQty += feeItemList.NoBackQty;

                this.fpQuit_SheetDrug.SetValue(index, (int)DrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * temp.NoBackQty / feeItemList.Item.PackQty, 2));
            }
            else
            {
                FeeItemList temp = this.fpQuit_SheetUndrug.Rows[index].Tag as FeeItemList;

                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Qty, feeItemList.NoBackQty + temp.NoBackQty);

                temp.NoBackQty += feeItemList.NoBackQty;

                this.fpQuit_SheetUndrug.SetValue(index, (int)UndrugColumns.Cost, FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * temp.NoBackQty, 2));
                
                SetFeeItemList(temp, feeItemList);
            }

            return 1;
        }

        private void SetFeeItemList(FeeItemList temp, FeeItemList feeItemList)
        {
            if (feeItemList.MateList.Count == 0)
                return;
            if (temp.MateList.Count == 0)
            {
                temp.MateList.Add(feeItemList.MateList[0]);
            }
            else
            {

                foreach (HISFC.Models.FeeStuff.Output outItem in feeItemList.MateList)
                {
                    bool isFind = false;
                    foreach (HISFC.Models.FeeStuff.Output tempItem in temp.MateList)
                    {
                        if (tempItem.ID == outItem.ID && tempItem.StoreBase.StockNO == outItem.StoreBase.StockNO)
                        {
                            isFind = true;
                            tempItem.ReturnApplyNum += outItem.ReturnApplyNum;
                        }
                    }
                    if (!isFind)
                    {
                        temp.MateList.Add(outItem);
                    }
                }

            }
        }

        /// <summary>
        /// �������б�ֵ
        /// </summary>
        /// <param name="feeItemList">������Ŀ��Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SetQuitItem(FeeItemList feeItemList)
        {
            int findIndex = -1;

            findIndex = this.FindQuitItem(feeItemList);

            //û���ҵ�,˵��û���˷Ѳ���
            if (findIndex == -1)
            {
                this.SetNewQuitItem(feeItemList.Clone());
            }
            else if (findIndex == -2)
            {
                //������
            }
            else//�Ѿ��������˷���Ϣ 
            {
                this.SetExistQuitItem(feeItemList.Clone(), findIndex);
            }

            return 1;
        }


        /// <summary>
        /// �˷Ѳ���
        /// </summary>
        /// <param name="feeItemList">���û�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QuitOperation(FeeItemList feeItemList)
        {
            DataRow findRow = this.FindUnquitItem(feeItemList.Clone());

            if (findRow == null)
            {
                MessageBox.Show("������Ŀ����!");

                return -1;
            }
            //songrabit��ʱ�ı����
            decimal orgNoBackQty = NConvert.ToDecimal(findRow["��������"]);
            orgNoBackQty = decimal.Round(orgNoBackQty, 2);
            if (orgNoBackQty < feeItemList.NoBackQty)
            {
                MessageBox.Show(Language.Msg("�����������ܴ���") + orgNoBackQty.ToString());

                return -1;
            }
            int index = this.fpUnQuit.ActiveSheet.ActiveRowIndex;
            //ȫ�����ʷ�ҩƷ�Ŀ�������Ϊ���ʵ�����
            if (this.fpUnQuit.ActiveSheet.RowHeader.Cells[index, 0].Text == "." && this.ckbAllQuit.Checked)
            {
                feeItemList.NoBackQty = feeItemList.MateList[0].ReturnApplyNum;
            }
            findRow["��������"] = NConvert.ToDecimal(findRow["��������"]) - feeItemList.NoBackQty;

            findRow["���"] = feeItemList.Item.Price * NConvert.ToDecimal(findRow["��������"]) / feeItemList.Item.PackQty;

            UpdateFpData(findRow, false);

            #region ͬ��fpUnQuit_SheetUndrug��DataTable�е�����
            //��Ϊ��ҩƷ�����ݲ��ǰ��ϵ�����Ҫͬ��fpUnQuit_SheetUndrug��DataTable�е�����
            string rowHeader = string.Empty;
            DataRow mateRow = null;
            if (feeItemList.Item.ItemType != EnumItemType.Drug && feeItemList.MateList.Count > 0)
            {

                foreach (HISFC.Models.FeeStuff.Output outItem in feeItemList.MateList)
                {
                    mateRow = FindMateRow(outItem);
                    if (mateRow == null)
                    {
                        MessageBox.Show("������Ŀ����!");
                        return -1;
                    }
                    mateRow["��������"] = NConvert.ToDecimal(mateRow["��������"]) - outItem.ReturnApplyNum;
                    mateRow["���"] = feeItemList.Item.Price * NConvert.ToDecimal(mateRow["��������"]) / feeItemList.Item.PackQty;
                    UpdateFpData(mateRow, true);
                }
            }
            #endregion
            this.SetQuitItem(feeItemList.Clone());

            return 1;
        }
        /// <summary>
        /// �������ʷ�ҩƷ
        /// </summary>
        /// <param name="outItem">���ʳ����¼</param>
        /// <returns></returns>
        private DataRow FindMateRow(HISFC.Models.FeeStuff.Output outItem)
        {
            string outNo = string.Empty;
            string stockNo = string.Empty;

            outNo = outItem.ID;
            stockNo = outItem.StoreBase.StockNO;
            return FindMateRow(stockNo, outNo);

        }
        /// <summary>
        /// �������ʷ�ҩƷ
        /// </summary>
        /// <param name="stockNo">������</param>
        /// <param name="outNo">������ˮ��</param>
        /// <returns></returns>
        private DataRow FindMateRow(string stockNo, string outNo)
        {
            DataRow dr = dtMate.Rows.Find(new object[] { stockNo, outNo });
            return dr;
        }

        /// <summary>
        /// ���·�ҩƷFarpoint����
        /// </summary>
        /// <param name="dr">��ҩƷ����</param>
        private void UpdateFpData(DataRow dr, bool isMate)
        {
            //��Ϊ��ҩƷ�����ݲ��ǰ��ϵ�����Ҫͬ��fpUnQuit_SheetUndrug��DataTable�е�����
            string stockNo = string.Empty;
            string outNo = string.Empty;
            string recipeNO = string.Empty;
            string recipeSequence = string.Empty;
            bool isFind = false;
            for (int i = 0; i < fpUnQuit_SheetUndrug.Rows.Count; i++)
            {
                if (isMate)
                {
                    stockNo = this.fpUnQuit_SheetUndrug.GetText(i, this.FindColumn("������", this.fpUnQuit_SheetUndrug));
                    outNo = this.fpUnQuit_SheetUndrug.GetText(i, this.FindColumn("������ˮ��", this.fpUnQuit_SheetUndrug));
                    if (stockNo == dr["������"].ToString() && outNo == dr["������ˮ��"].ToString())
                    {
                        isFind = true;
                    }
                }
                else
                {
                    recipeNO = fpUnQuit_SheetUndrug.GetText(i, this.FindColumn("������", this.fpUnQuit_SheetUndrug));
                    recipeSequence = this.fpUnQuit_SheetUndrug.GetText(i, this.FindColumn("������ˮ��", this.fpUnQuit_SheetUndrug));
                    if (recipeNO == dr["������"].ToString() && recipeSequence == dr["������ˮ��"].ToString())
                    {
                        isFind = true;
                    }
                }
                if (isFind)
                {
                    this.fpUnQuit_SheetUndrug.Cells[i, this.FindColumn("��������", this.fpUnQuit_SheetUndrug)].Text = dr["��������"].ToString();
                    this.fpUnQuit_SheetUndrug.Cells[i, this.FindColumn("���", this.fpUnQuit_SheetUndrug)].Text = dr["���"].ToString();
                    return;
                }
            }

        }


        /// <summary>
        /// ȡ���˷Ѳ���
        /// </summary>
        /// <ҵ����˵��>
        ///     1��������ҩ����(��ҩ���˷ѡ���δ��ҩȷ�ϵ����)��ȡ��ʱֱ��������ҩ���롣���¿�������
        ///     2�������˷�����ķ�ҩƷ��ֱ�Ӹ��¿�������
        ///        �����˷������ҩƷ,���ȸ��·��ñ���������������ȫ��ȡ����ֱ�ӻָ�ȡҩ����
        ///        ����ǰ���ȡ��������������ԭȡҩ���룬���������γ���ȡҩ����
        /// </ҵ����˵��>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int CancelQuitOperation()
        {
            if (this.fpQuit.ActiveSheet.RowCount == 0)
            {
                return -1;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ȡ���˷�������?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
            {
                return -1;
            }

            int index = this.fpQuit.ActiveSheet.ActiveRowIndex;

            object quitItem = this.fpQuit.ActiveSheet.Rows[index].Tag;
            if (quitItem == null)
            {
                return -1;
            }
            FS.HISFC.Models.Fee.Inpatient.FeeItemList tempFeeItem = null;

            #region û�����˷ѡ���ҩ���������
            //û�����˷ѡ���ҩ���������
            if (quitItem.GetType() == typeof(FS.HISFC.Models.Fee.Inpatient.FeeItemList))
            {
                tempFeeItem = quitItem as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                DataRow rowFind = this.FindUnquitItem(tempFeeItem);
                if (rowFind == null)
                {
                    MessageBox.Show("����δ����Ŀ����");

                    return -1;
                }

                rowFind["��������"] = NConvert.ToDecimal(rowFind["��������"]) + tempFeeItem.NoBackQty;
                
                rowFind["���"] = tempFeeItem.Item.Price * NConvert.ToDecimal(rowFind["��������"]) / tempFeeItem.Item.PackQty;

                UpdateFpData(rowFind, false);

                #region ͬ��fpUnQuit_SheetUndrug��DataTable�е�����
                //��Ϊ��ҩƷ�����ݲ��ǰ��ϵ�����Ҫͬ��fpUnQuit_SheetUndrug��DataTable�е�����
                string rowHeader = string.Empty;
                DataRow mateRow = null;
                if (tempFeeItem.Item.ItemType != EnumItemType.Drug && tempFeeItem.MateList.Count > 0)
                {

                    foreach (HISFC.Models.FeeStuff.Output outItem in tempFeeItem.MateList)
                    {
                        mateRow = FindMateRow(outItem);
                        if (mateRow == null)
                        {
                            MessageBox.Show("������Ŀ����!");
                            return -1;
                        }
                        mateRow["��������"] = NConvert.ToDecimal(mateRow["��������"]) + outItem.ReturnApplyNum;
                        mateRow["���"] = tempFeeItem.Item.Price * NConvert.ToDecimal(mateRow["��������"]) / tempFeeItem.Item.PackQty;
                        UpdateFpData(mateRow, true);
                    }
                }
                #endregion
                this.fpQuit.ActiveSheet.Rows.Remove(index, 1);
                return 1;
            }
            #endregion

            //��ʼ����
            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();


            if (quitItem is FS.HISFC.Models.Pharmacy.ApplyOut)     //��ҩ����
            {
                #region ��ҩ����ȡ�� ��ʱ��û���˷�������Ϣ

                FS.HISFC.Models.Pharmacy.ApplyOut tempApplyOut = quitItem as FS.HISFC.Models.Pharmacy.ApplyOut;
                //���ݴ����š�������ˮ�Ż�ȡ����״̬
                //tempFeeItem = this.inpatientManager.GetItemListByRecipeNO(tempApplyOut.RecipeNO, tempApplyOut.SequenceNO, true);
                tempFeeItem = this.inpatientManager.GetItemListByRecipeNO(tempApplyOut.RecipeNO, tempApplyOut.SequenceNO, EnumItemType.Drug);
                if (tempFeeItem == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���ݴ����š���������Ŀ��ˮ�Ż�ȡ������ϸ��Ϣʧ��") + this.inpatientManager.Err);
                    return -1;
                }
                //����ҩƷ��ϸ���еĿ�����������ֹ����
                int parm = this.inpatientManager.UpdateNoBackQtyForDrug(tempApplyOut.RecipeNO, tempApplyOut.SequenceNO, -tempApplyOut.Days * tempApplyOut.Operation.ApplyQty, tempFeeItem.BalanceState);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����ҩƷ��������ʧ��" + this.inpatientManager.Err));
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));
                    return -1;
                }
                //������ҩ����
                parm = this.phamarcyIntegrate.CancelApplyOut(tempApplyOut.RecipeNO, tempApplyOut.SequenceNO);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.phamarcyIntegrate.Err);
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�������ѱ�ȡ�����޷��ٴγ���");
                    return -1;
                }

                #endregion

                #region ��ʱ������ϢtempFeeItem��ֵ

                tempFeeItem.Item.Qty = tempApplyOut.Days * tempApplyOut.Operation.ApplyQty;
                tempFeeItem.Item.Price = tempApplyOut.Item.PriceCollection.RetailPrice;

                #endregion
            }
            if (quitItem is FS.HISFC.Models.Fee.ReturnApply)       //�˷�����
            {

                FS.HISFC.Models.Fee.ReturnApply temp = quitItem as FS.HISFC.Models.Fee.ReturnApply;
                ArrayList listReturnApply = new ArrayList();

                if (isQuitFeeByPackage && hsQuitFeeByPackage.ContainsKey("Quit" + temp.ID + temp.UndrugComb.ID))
                {
                    //�������ݿ�
                    ArrayList altemp = hsQuitFeeByPackage["Quit" + temp.ID + temp.UndrugComb.ID] as ArrayList;// this.returnApplyManager.QueryReturnApplys(this.patientInfo.ID, false, false);
                    //foreach (FS.HISFC.Models.Fee.ReturnApply tempReturnApply in altemp)
                    //{
                    //    if (tempReturnApply.ApplyBillNO == temp.ApplyBillNO && tempReturnApply.UndrugComb.ID == temp.UndrugComb.ID)
                    //    {
                    //        listReturnApply.Add(tempReturnApply);
                    //    }
                    //}
                    listReturnApply.AddRange(altemp);
                }
                else
                {
                    listReturnApply.Add(temp);
                }

                foreach (FS.HISFC.Models.Fee.ReturnApply tempReturnApply in listReturnApply)
                {
                    #region ���ݴ����š�������ˮ�Ż�ȡ������Ϣ

                    //if (tempReturnApply.Item.IsPharmacy)
                    if (tempReturnApply.Item.ItemType == EnumItemType.Drug)
                    {
                        //���ݴ����š�������ˮ�Ż�ȡ����״̬
                        //tempFeeItem = this.inpatientManager.GetItemListByRecipeNO(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, true);
                        tempFeeItem = this.inpatientManager.GetItemListByRecipeNO(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, EnumItemType.Drug);
                    }
                    else
                    {
                        //tempFeeItem = this.inpatientManager.GetItemListByRecipeNO(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, false);
                        tempFeeItem = this.inpatientManager.GetItemListByRecipeNO(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, EnumItemType.UnDrug);
                    }
                    if (tempFeeItem == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���ݴ����š���������Ŀ��ˮ�Ż�ȡ������ϸ��Ϣʧ��") + this.inpatientManager.Err);
                        return -1;
                    }

                    #endregion

                    //if (tempReturnApply.Item.IsPharmacy)                    //ҩƷ�˷�����
                    if (tempReturnApply.Item.ItemType == EnumItemType.Drug)     //ҩƷ�˷�����
                    {
                        #region ҩƷ�˷���������

                        #region ������ϸ���еĿ�����������ֹ����

                        int parm = this.inpatientManager.UpdateNoBackQtyForDrug(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, -tempReturnApply.Item.Qty, tempFeeItem.BalanceState);
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("����ҩƷ��������ʧ��" + this.inpatientManager.Err));
                            return -1;
                        }
                        else if (parm == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));
                            return -1;
                        }

                        #endregion

                        //�����˵��������ʱҩƷ�Ѿ����ڲ����˺����Ч���롣���������룬����ȡ���������������ɰ�ҩ����
                        if (tempFeeItem.NoBackQty != 0 || tempFeeItem.Item.Qty != tempFeeItem.NoBackQty + tempReturnApply.Item.Qty)
                        {
                            #region ������ȡ�����
                            //�������ϰ�ҩ����
                            int returnValue = this.phamarcyIntegrate.CancelApplyOut(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO);
                            if (returnValue == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("��Ŀ��") + tempFeeItem.Item.Name + Language.Msg("���Ѱ�ҩ�������¼�������"));

                                return -1;
                            }

                            //ȡ�շѶ�Ӧ�İ�ҩ�����¼
                            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.phamarcyIntegrate.GetApplyOut(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO);
                            if (applyOutTemp == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("���������Ϣ����!") + this.phamarcyIntegrate.Err);
                                return -1;
                            }
                            //��ʣ���������Ͱ�ҩ����
                            applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                            applyOutTemp.Operation.ApplyQty = tempFeeItem.NoBackQty + tempReturnApply.Item.Qty;//��������ʣ������
                            applyOutTemp.Operation.ApproveQty = tempFeeItem.NoBackQty + tempReturnApply.Item.Qty;//��������ʣ������
                            applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬                        
                            //��ʣ���������Ͱ�ҩ����  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                            if (this.phamarcyIntegrate.ApplyOut(applyOutTemp) != 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("���²��뷢ҩ�������!") + this.phamarcyIntegrate.Err);

                                return -1;
                            }

                            #endregion
                        }
                        else
                        {
                            #region ȫ�����

                            //�ָ����������¼Ϊ��Ч   ����� 
                            parm = this.phamarcyIntegrate.UndoCancelApplyOut(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO);
                            if (parm == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�ָ�����������Ч�Է�������" + this.phamarcyIntegrate.Err);
                                return -1;
                            }
                            else if (parm == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�������ѱ�ȡ�����޷���������");
                                return -1;
                            }

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region ��ҩƷ�˷���������

                        //������ϸ���еĿ�����������ֹ����
                        int parm = this.inpatientManager.UpdateNoBackQtyForUndrug(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, -tempReturnApply.Item.Qty, tempFeeItem.BalanceState);
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("����ҩƷ��������ʧ��" + this.inpatientManager.Err));
                            return -1;
                        }
                        else if (parm == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));
                            return -1;
                        }

                        #endregion

                        #region ���������˷�����
                        //�������ʳ�����е���������
                        parm = mateInteger.ApplyMaterialFeeBack(tempReturnApply.MateList, true);
                        if (parm < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("����������������ʧ��" + this.inpatientManager.Err));
                            return -1;
                        }
                        if (parm == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));
                            return -1;
                        }
                        parm = returnApplyManager.UpdateReturnApplyState(tempReturnApply.ApplyMateList, CancelTypes.Reprint);
                        if (parm < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("��������������Ϣʧ�ܣ�" + this.inpatientManager.Err));
                            return -1;
                        }
                        #endregion
                    }

                    #region ��ʱ������ϢtempFeeItem��ֵ

                    tempFeeItem.Item.Qty = tempReturnApply.Item.Qty;
                    tempFeeItem.Item.Price = tempReturnApply.Item.Price;

                    #endregion

                    //�����˷�����
                    if (this.returnApplyManager.CancelReturnApply(tempReturnApply.ID, this.returnApplyManager.Operator.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("�����˷����뷢������") + this.returnApplyManager.Err);
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            //������˷���Ŀ(��������)
            if (tempFeeItem != null)
            {
                if (tempFeeItem.Item.ItemType == EnumItemType.Drug)
                {
                    DataRow rowFind = this.FindUnquitItem(tempFeeItem);
                    if (rowFind != null)
                    {

                        rowFind["��������"] = NConvert.ToDecimal(rowFind["��������"]) + tempFeeItem.Item.Qty;
                        rowFind["���"] = tempFeeItem.Item.Price * NConvert.ToDecimal(rowFind["��������"]) / tempFeeItem.Item.PackQty;
                        this.fpQuit.ActiveSheet.Rows.Remove(index, 1);
                    }
                    else
                    {
                        this.Retrive(true);
                    }
                }
                else
                {
                    this.Retrive(true);
                }
            }

            return 1;
        }

        /// <summary>
        /// ��֤�Ϸ���
        /// </summary>
        /// <returns>�ɹ� True ʧ�� false</returns>
        protected virtual bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// �����˷�������Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SaveApply()
        {
            //��֤�Ϸ���
            if (!this.IsValid())
            {
                return -1;
            }

            List<FeeItemList> feeItemLists = this.GetConfirmItem();

            if (feeItemLists.Count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з��ÿ���!"));

                return -1;
            }

            ArrayList alBackFeeListDrug = new ArrayList();
            ArrayList alBackFeeListUnDrug = new ArrayList();




            //��ʼ����
            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string errMsg = string.Empty;//������Ϣ
            int returnValue = 0;//����ֵ
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            //����˷������
            string applyBillCode = this.GetApplyBillCode(ref errMsg);
            if (applyBillCode == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errMsg);

                return -1;
            }
            string msg = string.Empty;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            //ѭ�������˷�����
            foreach (FeeItemList feeItemList in feeItemLists)
            {
                feeItemList.User03 = nowTime.ToString("yyyy-MM-dd");
                if (this.isPrintQuitTogether)
                {
                    alBackFeeListDrug.Add(feeItemList);
                }
                else
                {
                    if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                    {
                        if (feeItemList.ConfirmedQty > 0)
                        {
                            string key = (string.IsNullOrEmpty(feeItemList.UndrugComb.ID) ? feeItemList.Item.ID : feeItemList.UndrugComb.ID) + feeItemList.ExecOrder.ID + feeItemList.TransType.ToString();
                            if (dictionary.ContainsKey(key) == false)
                            {
                                msg += string.Format(Environment.NewLine + @"{1}��{0}  {2}{3}��", string.IsNullOrEmpty(feeItemList.UndrugComb.ID) ? feeItemList.Item.Name : feeItemList.UndrugComb.Name, CommonController.Instance.GetDepartmentName(feeItemList.ExecOper.Dept.ID), feeItemList.ConfirmedQty, feeItemList.Item.PriceUnit);
                                dictionary[key] = msg;
                            }
                        }
                        alBackFeeListUnDrug.Add(feeItemList);
                    }
                    else
                    {
                        alBackFeeListDrug.Add(feeItemList);
                    }
                }
                //��Ӧ�������˷�����ģ���֪��Ҫ�������ã���ʲô�ã��Ȼ���
                //if (this.feeIntegrate.QuitFeeApply(this.patientInfo, feeItemList, NConvert.ToBoolean(feeItemList.ExecOper.Dept.User01), applyBillCode, nowTime, ref errMsg) == -1)
                if (this.feeIntegrate.QuitFeeApply(this.patientInfo, feeItemList, true, applyBillCode, nowTime, ref errMsg) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this, "�˷�ʧ��!" + this.feeIntegrate.Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(this, Language.Msg("����ɹ�!" + (string.IsNullOrEmpty(msg) ? "" : @"��֪ͨ���¿��ҽ����˷�ȷ�ϣ�" + Environment.NewLine + Environment.NewLine + msg)));


            if (alBackFeeListDrug.Count > 0)
            {
                //����ӡ��ֱ�ӷ���
                if (!this.IsPrintQuitFee) return 1;

                if (this.IBackFeePrint == null)
                {
                    this.IBackFeePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint;
                }
                if (this.IBackFeePrint == null)
                {
                    this.IBackFeePrint = new ucQuitDrugBill();
                }
                if (this.IBackFeePrint != null)
                {
                    ArrayList alBackFeeListDrugPrint = new ArrayList();
                    foreach (FeeItemList feeItemListTmp in alBackFeeListDrug)
                    {
                        if (feeItemListTmp.User01 == "��ҩ��")
                        {
                            alBackFeeListDrugPrint.Add(feeItemListTmp);
                        }
                    }
                    if (alBackFeeListDrugPrint.Count > 0)
                    {
                        this.IBackFeePrint.Patient = this.patientInfo;
                        this.IBackFeePrint.SetData(alBackFeeListDrug);
                        this.IBackFeePrint.Print();
                    }
                }
            }


            if (alBackFeeListUnDrug.Count > 0)
            {
                //����ӡ��ֱ�ӷ���
                if (!this.IsPrintQuitFee || !this.isPrintUndrugQuitFee) return 1;

                if (this.IBackFeePrint == null)
                {
                    this.IBackFeePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint;
                }
                if (this.IBackFeePrint == null)
                {
                    this.IBackFeePrint = new ucQuitDrugBill();
                }
                if (this.IBackFeePrint != null)
                {
                    this.IBackFeePrint.Patient = this.patientInfo;

                    this.IBackFeePrint.SetData(alBackFeeListUnDrug);

                    this.IBackFeePrint.Print();
                }
            }


            return 1;
        }

        /// <summary>
        /// ֱ���˷�
        /// </summary>
        /// <returns></returns>
        protected virtual int SaveQuitFee()
        {
            #region ��֤�Ϸ���

            if (!this.IsValid())
            {
                return -1;
            }

            List<FeeItemList> feeItemLists = this.GetConfirmItem();
            if (feeItemLists.Count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з��ÿ���!"));

                return -1;
            }

            ArrayList alBackFeeListDrug = new ArrayList();
            #endregion

            #region ��ʼ����

            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #endregion

            string errMsg = string.Empty;//������Ϣ
            int returnValue = 0;//����ֵ
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            #region ����˷������

            string applyBillCode = this.GetApplyBillCode(ref errMsg);
            if (applyBillCode == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errMsg);

                return -1;
            }

            #endregion

            string msg = "";
            bool isApply = false;

            //ѭ�������˷�����
            foreach (FeeItemList feeItemList in feeItemLists)
            {
                if (this.feeIntegrate.QuitFeeApply(this.patientInfo, feeItemList, NConvert.ToBoolean(feeItemList.ExecOper.Dept.User01), applyBillCode, nowTime, ref msg) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this, "�˷�ʧ��!" + this.feeIntegrate.Err, "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    alBackFeeListDrug.Add(feeItemList);
                }
            }

            //FS.FrameWork.Management.PublicTrans.Commit();
            //{5C3C59A9-7E36-4c31-995C-8396DCDCBF9E}
            this.feeIntegrate.Commit();
            MessageBox.Show(this, "�˷ѳɹ�!" + "\n" + (string.IsNullOrEmpty(msg) ? "" : "����ҩƷ " + msg + " ��ȥҩ��ȷ����ҩ"), "��ʾ>>", MessageBoxButtons.OK);


            if (this.isPrintQuitFeeNoApply)// {AD405AA0-3101-46c0-A0B6-846DC3AAB10A}
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ��ӡ��ҩ����"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (alBackFeeListDrug.Count > 0)
                    {
                        if (this.IBackFeePrint == null)
                        {
                            this.IBackFeePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint;
                        }
                        if (this.IBackFeePrint == null)
                        {
                            this.IBackFeePrint = new ucQuitDrugBill();
                        }
                        if (this.IBackFeePrint != null)
                        {
                            ArrayList alBackFeeListDrugPrint = new ArrayList();
                            foreach (FeeItemList feeItemListTmp in alBackFeeListDrug)
                            {
                                if (feeItemListTmp.User01 == "��ҩ��")
                                {
                                    alBackFeeListDrugPrint.Add(feeItemListTmp);
                                }
                            }
                            if (alBackFeeListDrugPrint.Count > 0)
                            {
                                this.IBackFeePrint.Patient = this.patientInfo;
                                this.IBackFeePrint.SetData(alBackFeeListDrug);
                                this.IBackFeePrint.Print();
                            }
                        }
                    }

                }
            }


            return 1;
        }

        /// <summary>
        /// ����˷������
        /// </summary>
        /// <param name="errMsg">������Ϣ</param>
        /// <returns>�ɹ�  ����˷������ ʧ�� null</returns>
        private string GetApplyBillCode(ref string errMsg)
        {
            string applyBillCode = string.Empty;

            applyBillCode = this.returnApplyManager.GetReturnApplyBillCode(); //this.inpatientManager.GetSequence("Fee.ApplyReturn.GetBillCode");
            if (applyBillCode == null || applyBillCode == string.Empty)
            {
                errMsg = Language.Msg("��ȡ�˷����뷽�ų���!");

                return null;
            }

            return applyBillCode;
        }

        /// <summary>
        /// ����˷ѵ���Ŀ
        /// </summary>
        /// <returns>�ɹ� ������Ŀ���� ʧ�� null</returns>
        private List<FeeItemList> GetConfirmItem()
        {
            List<FeeItemList> feeItemLists = new List<FeeItemList>();


            for (int j = 0; j < this.fpQuit.Sheets.Count; j++)
            {
                for (int i = 0; i < this.fpQuit.Sheets[j].RowCount; i++)
                {
                    if (this.fpQuit.Sheets[j].Rows[i].Tag != null && this.fpQuit.Sheets[j].Rows[i].Tag is FeeItemList)
                    {
                        FeeItemList feeItemList = this.fpQuit.Sheets[j].Rows[i].Tag as FeeItemList;
                        if (feeItemList.NoBackQty > 0)
                        {
                            feeItemList.Item.Qty = feeItemList.NoBackQty;
                            feeItemList.NoBackQty = 0;
                            feeItemList.FT.TotCost = feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty;
                            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                            feeItemList.IsNeedUpdateNoBackQty = true;

                            string key = feeItemList.UndrugComb.ID + "|" + feeItemList.TransType.ToString() + "|" + feeItemList.RecipeNO + feeItemList.ExecOrder.ID;

                            if (feeItemList.Item.ItemType == EnumItemType.UnDrug && isQuitFeeByPackage && hsQuitFeeByPackage.ContainsKey(key))
                            {
                                ArrayList al = hsQuitFeeByPackage["Apply" + key] as ArrayList;
                                //ArrayList recipeNOs = this.inpatientManager.QueryRecipesByRepriceNO(feeItemList.RecipeNO, feeItemList.ExecOrder.ID, feeItemList.UndrugComb.ID);

                                //if (recipeNOs == null)
                                //{
                                //    MessageBox.Show("û���ҵ���ͬ�ĸ�����Ŀ!" + this.inpatientManager.Err);
                                //    return null;
                                //}
                                //foreach (FS.FrameWork.Models.NeuObject o in recipeNOs)
                                foreach (FeeItemList f in al)
                                {
                                    //FeeItemList f = this.inpatientManager.GetItemListByRecipeNO(o.ID, NConvert.ToInt32(o.Name), EnumItemType.UnDrug);
                                    //if (f == null)
                                    //{
                                    //    MessageBox.Show(Language.Msg("�����Ŀ������Ϣ����!") + this.inpatientManager.Err);
                                    //    return null;
                                    //}
                                    if (f.NoBackQty > 0)
                                    {
                                        f.Item.Qty = f.NoBackQty;
                                        f.NoBackQty = 0;
                                        f.FT.TotCost = f.Item.Price * f.Item.Qty / f.Item.PackQty;
                                        f.FT.OwnCost = f.FT.TotCost;
                                        f.IsNeedUpdateNoBackQty = true;
                                        //
                                        if (j == 1)
                                        {
                                            f.ExecOper.Dept.User01 = this.fpQuit.Sheets[j].Cells[i, (int)UndrugColumns.IsApply].Value.ToString();//�Ƿ���Ҫ����
                                        }
                                        else
                                        {
                                            f.ExecOper.Dept.User01 = this.fpQuit.Sheets[j].Cells[i, (int)DrugColumns.IsApply].Value.ToString();
                                        }

                                        feeItemLists.Add(f);

                                    }
                                }


                            }
                            else
                            {
                                if (j == 1)
                                {
                                    feeItemList.ExecOper.Dept.User01 = this.fpQuit.Sheets[j].Cells[i, (int)UndrugColumns.IsApply].Value.ToString();//�Ƿ���Ҫ����
                                }
                                else
                                {
                                    feeItemList.ExecOper.Dept.User01 = this.fpQuit.Sheets[j].Cells[i, (int)DrugColumns.IsApply].Value.ToString();
                                }

                                feeItemLists.Add(feeItemList);


                            }
                        }
                    }
                }
            }

            return feeItemLists;
        }

        /// <summary>
        /// �����п�
        /// </summary>
        protected virtual void SetColumns()
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn ucSetCol = new FS.HISFC.Components.Common.Controls.ucSetColumn();

            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                ucSetCol.SetDataTable(this.filePathUnQuitDrug, this.fpUnQuit_SheetDrug);
            }
            else
            {
                ucSetCol.SetDataTable(this.filePathUnQuitUndrug, this.fpUnQuit_SheetUndrug);
            }

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucSetCol);
        }

        /// <summary>
        /// �۵���ʾ��������
        /// </summary>
        /// <param name="isExpand"></param>
        /// <param name="index"></param>
        private void ExpandOrCloseRow(bool isExpand, int index)
        {

            for (int i = index; i < fpUnQuit_SheetUndrug.Rows.Count; i++)
            {
                if (this.fpUnQuit_SheetUndrug.RowHeader.Cells[i, 0].Text == "." && this.fpUnQuit_SheetUndrug.Rows[i].Visible == isExpand)
                {
                    this.fpUnQuit_SheetUndrug.Rows[i].Visible = !isExpand;
                }
                else
                {
                    break;
                }
            }
            if (isExpand)
            {
                fpUnQuit_SheetUndrug.RowHeader.Cells[index - 1, 0].Text = "+";
            }
            else
            {
                fpUnQuit_SheetUndrug.RowHeader.Cells[index - 1, 0].Text = "-";
            }
        }
        #endregion

        #region ���з���

        /// <summary>
        /// �������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public virtual int Save()
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뻼��!"));

                return -1;
            }

            if (this.isDirBackFee)
            {
                this.SaveQuitFee();
            }
            else
            {
                if (this.SaveApply() == -1)
                {
                    return -1;
                }

                this.Clear();

                //this.ClearItemList();

                //this.Retrive(true);

                this.txtFilter.Focus();
                return 1;
            }

            this.Clear();

            return 1;
        }

        /// <summary>
        /// ��պ���
        /// </summary>
        public virtual void Clear()
        {
            this.ClearItemList();

            this.txtItemName.Text = string.Empty;
            this.txtItemName.Tag = null;
            this.txtPrice.Text = "0";
            this.mtbQty.Text = "0";
            this.txtUnit.Text = "0";
            this.txtName.Text = string.Empty;
            this.txtPact.Text = string.Empty;
            this.txtDept.Text = string.Empty;
            this.txtFilter.Text = string.Empty;
            this.txtBed.Text = string.Empty;
            this.ucQueryPatientInfo.Text = string.Empty;
            this.ucQueryPatientInfo.txtInputCode.SelectAll();
            this.ucQueryPatientInfo.txtInputCode.Focus();
            this.patientInfo = null;
            hsQuitFeeByPackage.Clear();

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
        }

        /// <summary>
        /// �����ʾ�б�
        /// </summary>
        public virtual void ClearItemList()
        {
            this.dtDrug.Clear();
            this.dtUndrug.Clear();
            this.fpQuit_SheetDrug.RowCount = 0;
            this.fpQuit_SheetUndrug.RowCount = 0;
            this.fpUnQuit_SheetDrug.RowCount = 0;
            this.fpUnQuit_SheetUndrug.RowCount = 0;

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);

            hsQuitFeeByPackage.Clear();
            //FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
        }

        #endregion

        #region �ؼ�����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("ȡ��", "ȡ������������ϸ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("������", "������ʾ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            toolBarService.AddToolButton("�˷����뵥����", "�˷����뵥����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Clear();
                    break;
                case "ȡ��":
                    this.CancelQuitOperation();
                    break;
                case "������":
                    this.SetColumns();
                    break;
                case "�˷����뵥����":
                    if (this.PatientInfo == null)
                    {
                        MessageBox.Show("��ѡ����");
                        return;
                    }
                    using (frmQuitItemBillRePrint fqbrp = new frmQuitItemBillRePrint())
                    {
                        this.PatientInfo.PID.PatientNO = ucQueryPatientInfo.Text;
                        fqbrp.Patient = this.PatientInfo;
                        fqbrp.ShowDialog();
                    }
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            Retrive(true);
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ��ȡ���߻�����Ϣ
        /// </summary>
        private void ucQueryInpatientNO_myEvent()
        {
            if (this.ucQueryPatientInfo.InpatientNo == null || this.ucQueryPatientInfo.InpatientNo == string.Empty)
            {
                MessageBox.Show(Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(this.ucQueryPatientInfo.InpatientNo);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            this.patientInfo = patientTemp;

            this.SetPatientInfomation();
        }

        /// <summary>
        /// Uc��Loade�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ucQuitFee_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                try
                {
                    this.Init();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// �˷��л�ҩƷ,��ҩƷ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpUnQuit_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                if (this.fpQuit.ActiveSheet != null)
                {
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetDrug;
                }
            }
            else
            {
                if (this.fpQuit.ActiveSheet != null)
                {
                    this.fpQuit.ActiveSheet = this.fpQuit_SheetUndrug;
                }
            }
        }

        /// <summary>
        /// �˷��л�ҩƷ,��ҩƷ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpQuit_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.fpQuit.ActiveSheet == this.fpQuit_SheetDrug)
            {
                if (this.fpUnQuit.ActiveSheet != null)
                {
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetDrug;
                }
            }
            else
            {
                if (this.fpUnQuit.ActiveSheet != null)
                {
                    this.fpUnQuit.ActiveSheet = this.fpUnQuit_SheetUndrug;
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.SetFilter();
        }

        private void fpUnQuit_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetDrug)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUnQuit_SheetDrug, this.filePathUnQuitDrug);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUnQuit_SheetUndrug, this.filePathUnQuitUndrug);
            }
        }

        private void fpUnQuit_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //ChooseUnquitItem();
            if (this.ckbAllQuit.Checked)
            {
                SelectGroupItem();
            }
            else
            {
                ChooseUnquitItem();
            }
        }

        private void mtbQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal qty = 0;

                try
                {
                    qty = NConvert.ToDecimal(this.mtbQty.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Msg("������Ϸ������֣�") + ex.Message);
                    this.mtbQty.SelectAll();
                    this.mtbQty.Focus();

                    return;
                }

                if (qty <= 0)
                {
                    MessageBox.Show(Language.Msg("�˷���������С�ڻ��ߵ���0"));
                    this.mtbQty.SelectAll();
                    this.mtbQty.Focus();

                    return;
                }

                if (this.txtItemName.Tag == null)
                {
                    return;
                }

                FeeItemList feeItemList = this.txtItemName.Tag as FeeItemList;

                if (qty > feeItemList.NoBackQty)
                {
                    MessageBox.Show(Language.Msg("�˷��������ܴ��ڿ������������������룡"));
                    this.mtbQty.SelectAll();
                    this.mtbQty.Focus();
                    return;
                }

                //int iQtyTemp = FS.FrameWork.Function.NConvert.ToInt32(qty);
                //if (qty != FS.FrameWork.Function.NConvert.ToInt32(qty))
                //{
                //    decimal decTemp = feeItemList.NoBackQty - FS.FrameWork.Function.NConvert.ToInt32(feeItemList.NoBackQty);
                //    if ((qty - iQtyTemp) != decTemp)
                //    {
                //        MessageBox.Show(Language.Msg("�˷�����С�����ݱ����� " + decTemp.ToString() + " �����������룡"));
                //        this.mtbQty.SelectAll();
                //        this.mtbQty.Focus();
                //        return;

                //    }
                //}


                feeItemList.NoBackQty = qty;

                //�˵�������
                if (feeItemList.MateList.Count > 0)
                {
                    feeItemList.MateList[0].ReturnApplyNum = qty;
                }

                this.QuitOperation(feeItemList);

                this.txtFilter.Focus();
            }
        }

        private void fpQuit_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CancelQuitOperation();
        }

        private void dtpBeginTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtpEndTime.Focus();
            }
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.fpUnQuit.ActiveSheet.ActiveRowIndex--;
                this.fpUnQuit.ActiveSheet.AddSelection(this.fpUnQuit.ActiveSheet.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.fpUnQuit.ActiveSheet.ActiveRowIndex++;
                this.fpUnQuit.ActiveSheet.AddSelection(this.fpUnQuit.ActiveSheet.ActiveRowIndex, 0, 1, 0);
            }
        }

        /// <summary>
        /// ��ʿվ��ѡ���¼�
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e == null || e.Tag == null)
                return -1;
            if (e.Tag as PatientInfo == null)
            {
                if (patientInfo == null)
                {
                    MessageBox.Show("��ѡ����");
                    return -1;
                }
            }
            else
            {
                patientInfo = e.Tag as PatientInfo;
            }
            this.ClearItemList();
            this.ucQueryPatientInfo.Text = patientInfo.PID.ID;
            this.SetPatientInfomation();

            return base.OnSetValue(neuObject, e);
        }

        #endregion

        #region ö��

        /// <summary>
        /// ҩƷ�˷�����Ϣ
        /// </summary>
        protected enum DrugColumns
        {
            /// <summary>
            /// ��ҩԭ��
            /// </summary>
            QuitDrugReason = 0,
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ItemName,

            /// <summary>
            /// ���
            /// </summary>
            Specs,

            /// <summary>
            /// ����
            /// </summary>
            Price,

            /// <summary>
            /// �˷�����
            /// </summary>
            Qty,

            /// <summary>
            /// ��λ
            /// </summary>
            Unit,

            /// <summary>
            /// ���
            /// </summary>
            Cost,

            /// <summary>
            /// �Ʒ�����
            /// </summary>
            FeeDate,

            /// <summary>
            /// �Ƿ��ҩ
            /// </summary>
            IsConfirm,

            /// <summary>
            /// �Ƿ��˷�����
            /// </summary>
            IsApply,
            /// <summary>
            /// ������ˮ��
            /// </summary>
            RecipeNO,
            /// <summary>
            /// �����ڲ���ˮ��
            /// </summary>
            SequuenceNO,
            /// <summary>
            /// Ӧִ��ʱ��
            /// </summary>
            UseTime
        }

        /// <summary>
        /// ��ҩƷ�˷�����Ϣ
        /// </summary>
        protected enum UndrugColumns
        {
            /// <summary>
            /// ҩƷ����
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// ��������
            /// </summary>
            FeeName,

            /// <summary>
            /// ����
            /// </summary>
            Price,

            /// <summary>
            /// �˷�����
            /// </summary>
            Qty,

            /// <summary>
            /// ��λ
            /// </summary>
            Unit,

            /// <summary>
            /// ���
            /// </summary>
            Cost,

            /// <summary>
            /// ִ�п���
            /// </summary>
            ExecDept,

            /// <summary>
            /// �Ƿ��ҩ
            /// </summary>
            IsConfirm,

            /// <summary>
            /// �Ƿ��˷�����
            /// </summary>
            IsApply,
            /// <summary>
            /// ������ˮ��
            /// </summary>
            RecipeNO,
            /// <summary>
            /// �����ڲ���ˮ��
            /// </summary>
            SequuenceNO,
            /// <summary>
            /// Ӧִ��ʱ��
            /// </summary>
            UseTime

        }

        /// <summary>
        /// �˷ѹ���
        /// </summary>
        public enum Operations
        {
            /// <summary>
            /// ֱ���˷�
            /// </summary>
            QuitFee = 0,

            /// <summary>
            /// �˷�����
            /// </summary>
            Apply,

            /// <summary>
            /// �˷�ȷ��
            /// </summary>
            Confirm,
        }

        /// <summary>
        /// �ɲ�����Ŀ����
        /// </summary>
        public enum ItemTypes
        {
            /// <summary>
            /// ����
            /// </summary>
            All = 0,

            /// <summary>
            /// ҩƷ
            /// </summary>
            Pharmarcy,

            /// <summary>
            /// ��ҩƷ
            /// </summary>
            Undrug
        }

        #endregion

        private void fpUnQuit_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (e.RowHeader && this.fpUnQuit_SheetUndrug.RowHeader.Cells[e.Row, 0].Text == "+" &&
                this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetUndrug)
            {
                ExpandOrCloseRow(false, e.Row + 1);
                return;
            }
            if (e.RowHeader && fpUnQuit_SheetUndrug.RowHeader.Cells[e.Row, 0].Text == "-" &&
                this.fpUnQuit.ActiveSheet == this.fpUnQuit_SheetUndrug)
            {
                ExpandOrCloseRow(true, e.Row + 1);
                return;
            }

        }

        private void fpUnQuit_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {

        }

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            if (string.IsNullOrEmpty(this.operationPriv))
            {
                return 1;
            }
            else
            {
                //����ǹ���Ա�������ֱ�ӽ���
                if (((FS.HISFC.Models.Base.Employee)inpatientManager.Operator).IsManager)
                {
                    return 1;
                }

                string[] privs = this.operationPriv.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                if (privs.Length == 0)
                {
                    return 1;
                }
                else if (privs.Length == 1)//ֻ�ж���û�ж���Ȩ��
                {
                    if (CommonController.Instance.JugePrive(privs[0]) == false)
                    {
                        CommonController.Instance.MessageBox(this, "��û�в����˷ѵ�Ȩ�ޣ�������ȡ����", MessageBoxIcon.Stop);
                        return -1;
                    }
                }
                else
                {
                    string class2Code = privs[0];
                    string class3Code = privs[1];
                    if (CommonController.Instance.JugePrive(privs[0], privs[1]) == false)
                    {
                        CommonController.Instance.MessageBox(this, "��û�в����˷ѵ�Ȩ�ޣ�������ȡ����", MessageBoxIcon.Stop);
                        return -1;
                    }
                }
            }

            return 1;
        }

        #endregion
    }
}

