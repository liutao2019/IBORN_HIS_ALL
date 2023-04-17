using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucCurePhase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCurePhase()
        {
            InitializeComponent();

        }

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ���ƽ׶���Ϣ
        /// </summary>
        private FS.HISFC.Models.Order.CurePhase curePhase = new FS.HISFC.Models.Order.CurePhase();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        /// <summary>
        /// ·��
        /// </summary>
        private string filePathCurePhase = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\PatientCurePhase.xml";


        private DataTable dtCurePhase = new DataTable();

        private DataView dvCurePhase = new DataView();

        /// <summary>
        /// ���ƽ׶�ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Order.CurePhase curePhaseManagement = new FS.HISFC.BizLogic.Order.CurePhase();

        /// <summary>
        /// ToolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.InitFrp();
            this.InitDoct();
            this.InitCurePhase();
        }

        /// <summary>
        /// ��ʼ��Frp
        /// </summary>
        private void InitFrp()
        {
            this.dtCurePhase.Reset();

            if (System.IO.File.Exists(this.filePathCurePhase))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePathCurePhase, dtCurePhase, ref dvCurePhase, this.fpCurePhase_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpCurePhase_Sheet1, this.filePathCurePhase);
            }
            else
            {
                this.dtCurePhase.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("סԺ��ˮ��",typeof(string)),
                    new DataColumn("���к�",typeof(string)),
                    //new DataColumn("���ұ���",typeof(string)),
                    new DataColumn("��������",typeof(string)),
                    //new DataColumn("���ƽ׶α���",typeof(string)),
                    new DataColumn("���ƽ׶�����",typeof(string)),
                    new DataColumn("�׶ο�ʼʱ��",typeof(DateTime)),
                    new DataColumn("�׶ν���ʱ��",typeof(DateTime)),
                    //new DataColumn("ҽ������",typeof(string)),
                    new DataColumn("ҽ������",typeof(string)),
                    new DataColumn("��Ч���",typeof(bool)),
                    new DataColumn("��ע",typeof(string)),
                    new DataColumn("����Ա",typeof(string)),
                    new DataColumn("����ʱ��",typeof(DateTime))
                });

                this.dvCurePhase = new DataView(this.dtCurePhase);

                this.fpCurePhase_Sheet1.DataSource = this.dvCurePhase;

                //this.fpCurePhase_Sheet1.Columns[2].Visible = false;
                //this.fpCurePhase_Sheet1.Columns[4].Visible = false;
                //this.fpCurePhase_Sheet1.Columns[8].Visible = false;
                this.fpCurePhase_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpCurePhase_Sheet1, this.filePathCurePhase);
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private void InitDoct()
        {
            ArrayList doctors = CacheManager.InterMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctors == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ����Ϣ����!") + CacheManager.InterMgr.Err);

                return ;
            }
            
            this.cmbDoct.AddItems(doctors);
        }

        /// <summary>
        /// �������ƽ׶�
        /// </summary>
        private void InitCurePhase()
        {
            ArrayList alCurePhase = CacheManager.InterMgr.QueryConstantList("CurePhase");
            if (alCurePhase == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ƽ׶���Ϣ����") + CacheManager.InterMgr.Err);

                return;
            }

            this.cmbCurePhase.AddItems(alCurePhase);
        }

        /// <summary>
        /// ���ݽ���ȡ��CurePhaseʵ��
        /// </summary>
        private void GetCurePhase()
        {
            this.curePhase.PatientID = this.patientInfo.ID;
            this.curePhase.Dept.ID = this.oper.Dept.ID;
            this.curePhase.Dept.Name = this.oper.Dept.Name;
            this.curePhase.StartTime = this.dtStart.Value;
            this.curePhase.EndTime = this.dtEnd.Value;
            if (this.cmbDoct.Tag != null)
            {
                this.curePhase.Doctor.ID = this.cmbDoct.Tag.ToString();
                this.curePhase.Doctor.Name = this.cmbDoct.Text;
            }
            if (this.cmbCurePhase.Tag != null)
            {
                this.curePhase.CurePhaseInfo.ID = this.cmbCurePhase.Tag.ToString();
                this.curePhase.CurePhaseInfo.Name = this.cmbCurePhase.Text;
            }
            this.curePhase.IsVaild = this.ckbVaild.Checked;
            this.curePhase.Remark = this.txtRemark.Text;
            this.curePhase.Oper.ID = this.oper.ID;
            this.curePhase.Oper.OperTime = this.curePhaseManagement.GetDateTimeFromSysDateTime();
        }

        /// <summary>
        /// ����ѡ���е����ݵ�����
        /// </summary>
        /// <param name="row">����</param>
        private void SetCurePhaseToControl(int row)
        {
            this.curePhase = this.curePhaseManagement.QuerCurePhaseBySeq(this.fpCurePhase_Sheet1.Cells[row, 1].Text);

            this.cmbCurePhase.Tag = this.curePhase.CurePhaseInfo.ID;
            this.cmbDoct.Tag = this.curePhase.Doctor.ID;
            this.dtStart.Value = this.curePhase.StartTime;
            this.dtEnd.Value = this.curePhase.EndTime;
            this.ckbVaild.Checked = this.curePhase.IsVaild;
            this.txtRemark.Text = this.curePhase.Remark;
            
        }

        /// <summary>
        /// ���������Ч��
        /// </summary>
        /// <returns></returns>
        private int CheckData()
        {
            if (this.curePhase.PatientID == null || this.curePhase.PatientID.Length == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��û��ѡ����"));
                return -1;
            }
            if (this.curePhase.CurePhaseInfo.ID == null || this.curePhase.CurePhaseInfo.ID.Length == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��û��ѡ�����ƽ׶���Ϣ"));
                return -1;
            }
            if (this.curePhase.Doctor.ID == null || this.curePhase.Doctor.ID.Length == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��û��ѡ����ҽ��"));
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        private void QueryCurePhase(string inpatientNO)
        {
            this.fpCurePhase_Sheet1.RowCount = 0;
            this.dtCurePhase.Clear();
            ArrayList alCurePhase = new ArrayList();
            alCurePhase = this.curePhaseManagement.QueryCurePhaseByInPatientNO(inpatientNO);
            if (alCurePhase != null && alCurePhase.Count > 0)
            {
                this.AddCurePhaseToFrp(alCurePhase);
            }
        }

        /// <summary>
        /// ������ݵ����
        /// </summary>
        /// <param name="al"></param>
        private void AddCurePhaseToFrp(ArrayList al)
        {
            
            foreach (FS.HISFC.Models.Order.CurePhase obj in al)
            {
                DataRow row = dtCurePhase.NewRow();

                row["סԺ��ˮ��"] = obj.PatientID;
                row["���к�"] = obj.ID;
                //row["���ұ���"] = obj.Dept.ID;
                row["��������"] = obj.Dept.Name;
                //row["���ƽ׶α���"] = obj.CurePhaseInfo.ID;
                row["���ƽ׶�����"] = obj.CurePhaseInfo.Name;
                row["�׶ο�ʼʱ��"] = obj.StartTime;
                row["�׶ν���ʱ��"] = obj.EndTime;
                //row["ҽ������"] = obj.Doctor.ID;
                row["ҽ������"] = obj.Doctor.Name;
                row["��Ч���"] = obj.IsVaild;
                row["��ע"] = obj.Remark;
                row["����Ա"] = obj.Oper.ID;
                row["����ʱ��"] = obj.Oper.OperTime;
                
                this.dtCurePhase.Rows.Add(row);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.curePhaseManagement.Connection);
            //t.BeginTransaction();
            this.curePhaseManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            this.GetCurePhase();

            if (this.CheckData() < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                return -1;
            }

            if (this.curePhase.ID == null || this.curePhase.ID.Length == 0)
            {
                this.curePhase.ID = this.curePhaseManagement.GetNewCurePhaseID();

                if (this.curePhaseManagement.InsertCurePhase(this.curePhase) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ƽ׶���Ϣ����!") + this.curePhaseManagement.Err);
                    return -1;
                }
            }
            else
            {
                if (this.curePhaseManagement.UpdateCurePhase(this.curePhase) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ƽ׶���Ϣ����!") + this.curePhaseManagement.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"));
            return 0;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {

            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            if (this.patientInfo != null && this.patientInfo.ID.Length > 0)
            {
                this.lblName.Text = this.patientInfo.Name;
                this.QueryCurePhase(this.patientInfo.ID);
            }
            return base.OnSetValue(neuObject, e);
        }
        

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            this.QueryCurePhase(this.patientInfo.ID);
            return base.OnSave(sender, neuObject);
        }

        private void ColumnSet()
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn uc = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            uc.FilePath = this.filePathCurePhase;
            uc.SetColVisible(true, true, false, false);
            uc.SetDataTable(this.filePathCurePhase, this.fpCurePhase.Sheets[0]);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            uc.DisplayEvent += new EventHandler(ucSetColumn_DisplayEvent);
            this.ucSetColumn_DisplayEvent(null, null);
        }

        private void ucSetColumn_DisplayEvent(object sender, EventArgs e)
        {

        }

        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("������", "���������", FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            this.toolBarService.AddToolButton( "�½�", "�½����ƹ���", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null );

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "������":
                    this.ColumnSet();
                    break;
                case "�½�":
                    this.NewPhase();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void NewPhase()
        {
            this.curePhase = new FS.HISFC.Models.Order.CurePhase();
            this.cmbCurePhase.Text = "";
            this.cmbDoct.Text = "";
            this.ckbVaild.Checked = true;
            this.txtRemark.Text = "";

            this.cmbCurePhase.Focus();
        }

        #endregion

        #region �¼�

        private void ucCurePhase_Load(object sender, EventArgs e)
        {
            
            this.Init();
            
        }

        private void cmbDoct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDoct.Tag != null)
            {
                //this
            }
        }

        private void fpCurePhase_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpCurePhase_Sheet1.RowCount > 0)
            {
                this.SetCurePhaseToControl(e.Row);
            }
        }

        #endregion

    }
}

