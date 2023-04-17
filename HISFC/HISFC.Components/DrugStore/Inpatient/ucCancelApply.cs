using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// <br></br>
    /// [��������: ֱ���˷ѣ������ն˿۷�����ʱʹ�ã�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>
    ///     1���ô���ʹ���ڻ�ʿվ
    /// </˵��>
    /// </summary>
    public partial class ucCancelApply : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCancelApply()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucCancelApply_Load);
        }
     
        #region �����

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper personHelper = null;
       
        /// <summary>
        /// ��ҩ������Ϣ
        /// </summary>
        private System.Collections.Hashtable hsApply = null;

        /// <summary>
        /// ������ҩ����
        /// </summary>
        private DataTable dtNormalApply = null;

        /// <summary>
        /// ������ҩ����
        /// </summary>
        private DataTable dtCancelApply = null;

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        private string strPatientInfo = "������{0}  �Ա�{1}  ������{2}  ���ţ�{3}";

        /// <summary>
        /// ���뻼����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo applyPatient = null;

        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = null;

        #endregion

        #region ����

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        protected DateTime BeginDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtBegin.Text);
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        protected DateTime EndDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtEnd.Text);
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            #region ��������Ϣ��ʼ��

            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

            ArrayList alDept = managerIntegrate.GetDepartment();
            if (alDept == null)
            {
                MessageBox.Show(Language.Msg("��ȡȫԺ�����б�ʧ��") + managerIntegrate.Err);
                return -1;
            }
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            ArrayList alPerson = managerIntegrate.QueryEmployeeAll();
            if (alPerson == null)
            {
                MessageBox.Show(Language.Msg("��ȡȫԺ��Ա�б�ʧ��") + managerIntegrate.Err);
                return -1;
            }
            this.personHelper = new FS.FrameWork.Public.ObjectHelper(alPerson);

            #endregion

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            DateTime sysTime = dataManager.GetDateTimeFromSysDateTime();

            this.dtBegin.Value = sysTime.Date.AddDays(-1);

            this.operDept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;

            this.InitDataTable();

            return 1;
        }

        /// <summary>
        /// ���ݼ���ʼ��
        /// </summary>
        /// <returns></returns>
        private int InitDataTable()
        {
            System.Type dtBol = System.Type.GetType("System.Boolean");
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDate = System.Type.GetType("System.DateTime");

            #region ������ҩ����DataTable��ʼ��

            this.dtNormalApply = new DataTable();

            this.dtNormalApply.Columns.AddRange(
                                    new System.Data.DataColumn[] {
                                                                    new DataColumn("��Ʒ����",  dtStr),
                                                                    new DataColumn("���",      dtStr),
                                                                    new DataColumn("���ۼ�",    dtDec),
                                                                    new DataColumn("��������",  dtDec),
                                                                    new DataColumn("���",      dtDec),
                                                                    new DataColumn("�������",  dtStr),
                                                                    new DataColumn("��ҩ����",  dtStr),
                                                                    new DataColumn("������",    dtStr),
                                                                    new DataColumn("��������",  dtStr),
                                                                    new DataColumn("��ע",      dtStr),
                                                                    new DataColumn("��ˮ��",    dtStr),
                                                                    new DataColumn("ƴ����",    dtStr),
                                                                    new DataColumn("�����",    dtStr),
                                                                    new DataColumn("�Զ�����",  dtStr)
                                                                   }
                                  );

            DataColumn[] normalKeys = new DataColumn[1];

            normalKeys[0] = this.dtNormalApply.Columns["��ˮ��"];

            this.dtNormalApply.PrimaryKey = normalKeys;

            this.fpNormalApply_Sheet1.DataSource = this.dtNormalApply.DefaultView;

            #endregion

            #region ������ҩ����DataTable��ʼ��

            this.dtCancelApply = new DataTable();

            this.dtCancelApply.Columns.AddRange(
                                    new System.Data.DataColumn[] {
                                                                    new DataColumn("��Ʒ����",  dtStr),
                                                                    new DataColumn("���",      dtStr),
                                                                    new DataColumn("���ۼ�",    dtDec),
                                                                    new DataColumn("��������",  dtDec),
                                                                    new DataColumn("���",      dtDec),
                                                                    new DataColumn("�������",  dtStr),
                                                                    new DataColumn("��ҩ����",  dtStr),
                                                                    new DataColumn("������",    dtStr),
                                                                    new DataColumn("��������",  dtStr),
                                                                    new DataColumn("��ע",      dtStr),
                                                                    new DataColumn("��ˮ��",    dtStr),
                                                                    new DataColumn("ƴ����",    dtStr),
                                                                    new DataColumn("�����",    dtStr),
                                                                    new DataColumn("�Զ�����",  dtStr)
                                                                   }
                                  );

            DataColumn[] cancelKeys = new DataColumn[1];

            cancelKeys[0] = this.dtCancelApply.Columns["��ˮ��"];

            this.dtCancelApply.PrimaryKey = cancelKeys;

            this.fpCancelApply_Sheet1.DataSource = this.dtCancelApply.DefaultView;

            #endregion            

            this.SetFormat();

            return 1;
        }

        /// <summary>
        /// ��ʽ��
        /// </summary>
        private void SetFormat()
        {
            this.fpNormalApply_Sheet1.DataAutoSizeColumns = false;
            this.fpCancelApply_Sheet1.DataAutoSizeColumns = false;

            this.fpCancelApply_Sheet1.Columns[(int)ColumnSet.ColOper].Width = 80F;
            this.fpCancelApply_Sheet1.Columns[(int)ColumnSet.ColID].Visible = false;
            this.fpCancelApply_Sheet1.Columns[(int)ColumnSet.ColSpellCode].Visible = false;
            this.fpCancelApply_Sheet1.Columns[(int)ColumnSet.ColWBCode].Visible = false;
            this.fpCancelApply_Sheet1.Columns[(int)ColumnSet.ColCustomeCode].Visible = false;

            this.fpNormalApply_Sheet1.Columns[(int)ColumnSet.ColOper].Width = 80F;
            this.fpNormalApply_Sheet1.Columns[(int)ColumnSet.ColID].Visible = false;
            this.fpNormalApply_Sheet1.Columns[(int)ColumnSet.ColSpellCode].Visible = false;
            this.fpNormalApply_Sheet1.Columns[(int)ColumnSet.ColWBCode].Visible = false;
            this.fpNormalApply_Sheet1.Columns[(int)ColumnSet.ColCustomeCode].Visible = false;
        }

        /// <summary>
        /// ������Ϣ��ʾ����
        /// </summary>
        private void SetPatientInfo()
        {
            this.lbPatientInfo.Text = string.Format(this.strPatientInfo,this.applyPatient.Name,this.applyPatient.Sex.Name,this.applyPatient.PVisit.PatientLocation.Dept.ID,this.applyPatient.PVisit.PatientLocation.Bed.ID);
        }

        /// <summary>
        /// �޸�����������Ϣ
        /// </summary>
        /// <param name="isAdd">�Ƿ�����������Ϣ</param>
        private int AddRemoveCancelApply(bool isAdd)
        {
            if (isAdd)
            {
                string applyID = this.fpNormalApply_Sheet1.Cells[this.fpNormalApply_Sheet1.ActiveRowIndex, (int)ColumnSet.ColID].Text;

                DataRow drNormal = this.dtNormalApply.Rows.Find(applyID);

                try
                {
                    if (drNormal != null)
                    {
                        DataRow dr = this.dtCancelApply.NewRow();
                        dr.ItemArray = drNormal.ItemArray;

                        this.dtNormalApply.Rows.Remove(drNormal);

                        this.dtNormalApply.AcceptChanges();

                        this.dtCancelApply.Rows.Add(dr);

                        this.dtCancelApply.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }
            else
            {
                string applyID = this.fpCancelApply_Sheet1.Cells[this.fpCancelApply_Sheet1.ActiveRowIndex, (int)ColumnSet.ColID].Text;

                DataRow drNormal = this.dtCancelApply.Rows.Find(applyID);

                try
                {
                    if (drNormal != null)
                    {
                        DataRow dr = this.dtNormalApply.NewRow();
                        dr.ItemArray = drNormal.ItemArray;

                        this.dtCancelApply.Rows.Remove(drNormal);

                        this.dtCancelApply.AcceptChanges();

                        this.dtNormalApply.Rows.Add(dr);

                        this.dtNormalApply.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ������ҩ������Ϣ����
        /// </summary>
        /// <returns></returns>
        private int SetNormalApply(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            this.dtNormalApply.Rows.Add(new object[] { 
                                                        applyOut.Item.Name,
                                                        applyOut.Item.Specs,
                                                        applyOut.Item.PriceCollection.RetailPrice,
                                                        applyOut.Operation.ApplyQty,
                                                        System.Math.Round((applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty) * applyOut.Item.PriceCollection.RetailPrice,2),
                                                        this.deptHelper.GetName(applyOut.ApplyDept.ID),
                                                        this.deptHelper.GetName(applyOut.StockDept.ID),
                                                        this.personHelper.GetName(applyOut.Operation.ApplyOper.ID),
                                                        applyOut.Operation.ApplyOper.OperTime,
                                                        applyOut.Memo,
                                                        applyOut.ID,
                                                        applyOut.Item.NameCollection.SpellCode,
                                                        applyOut.Item.NameCollection.WBCode,
                                                        applyOut.Item.NameCollection.UserCode                                                        
                                                        });
            return 1;
        }

        /// <summary>
        /// ������ҩ������Ϣ����
        /// </summary>
        /// <returns></returns>
        private int SetCancelApply(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            this.dtCancelApply.Rows.Add(new object[] { 
                                                        applyOut.Item.Name,
                                                        applyOut.Item.Specs,
                                                        applyOut.Item.PriceCollection.RetailPrice,
                                                        applyOut.Operation.ApplyQty,
                                                        System.Math.Round((applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty) * applyOut.Item.PriceCollection.RetailPrice,2),
                                                        this.deptHelper.GetName(applyOut.ApplyDept.ID),
                                                        this.deptHelper.GetName(applyOut.StockDept.ID),
                                                        this.personHelper.GetName(applyOut.Operation.ApplyOper.ID),
                                                        applyOut.Operation.ApplyOper.OperTime,
                                                        applyOut.Memo,
                                                        applyOut.ID,
                                                        applyOut.Item.NameCollection.SpellCode,
                                                        applyOut.Item.NameCollection.WBCode,
                                                        applyOut.Item.NameCollection.UserCode                                                        
                                                        });

            this.fpCancelApply_Sheet1.Rows[this.fpCancelApply_Sheet1.Rows.Count - 1].ForeColor = System.Drawing.Color.Red;

            return 1;
        }       

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <returns></returns>
        internal int QueryApply()
        {
            if (this.applyPatient == null)
            {
                MessageBox.Show(Language.Msg("������סԺ�Żس�ѡ���˷ѻ���"));
                return 0;
            }

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alApply = itemManager.GetPatientApply(this.applyPatient.ID,"AAAA",this.operDept.ID,this.BeginDate,this.EndDate,"0");
            if (alApply == null)
            {
                MessageBox.Show(Language.Msg("���߻�ȡ������Ϣʧ��") + itemManager.Err);
                return -1;
            }

            this.dtNormalApply.Rows.Clear();

            this.dtCancelApply.Rows.Clear();

            this.hsApply = new Hashtable();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApply)
            {
                if (applyOut.State != "0")
                {
                    continue;
                }
                //����ʾ��ҩ����
                if (applyOut.SystemType == FS.HISFC.Models.Base.EnumIMAOutTypeService.GetNameFromEnum(FS.HISFC.Models.Base.EnumIMAOutType.InpatientBackOutput))
                {
                    continue;
                }
                //1��Ч 0 ��Ч
                if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    this.SetNormalApply(applyOut);
                }
                else
                {
                    this.SetCancelApply(applyOut);
                }

                this.hsApply.Add(applyOut.ID, applyOut);
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        internal void Clear()
        {
            this.dtCancelApply.Rows.Clear();

            this.dtNormalApply.Rows.Clear();

            this.hsApply.Clear();
        }

        /// <summary>
        /// ������ҩ����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        internal int CancelApply()
        {
            if (this.fpCancelApply_Sheet1.Rows.Count <= 0)
            {
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            //itemManager.SetTrans(t.Trans);
            //{E8849BB0-3C69-4d60-8771-C201E445BD5D}  Ԥ�ۿ����жϴ���
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            bool isPreOut = ctrlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Pre_Out, false, true);

            for (int i = 0; i < this.fpCancelApply_Sheet1.Rows.Count; i++)            
            {
                string applyID = this.fpCancelApply_Sheet1.Cells[i, (int)ColumnSet.ColID].Text;

                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsApply[applyID] as FS.HISFC.Models.Pharmacy.ApplyOut;

                //���Ѿ���Ч������ ���ظ�����
                if (applyOut.ValidState ==  FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    continue;
                }

                //���ϰ�ҩ����
                //{E8849BB0-3C69-4d60-8771-C201E445BD5D}  �����������
                if (itemManager.CancelApplyOutByID(applyOut.ID, isPreOut) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���ϰ�ҩ����ʧ��"));
                    return -1;
                }

                //����ҽ��ִ�е�
                
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("��������ɹ�"));

            this.Clear();

            return 1;
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        protected int UnCancelApply()
        {
            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            //itemManager.SetTrans(t.Trans);

            //{E8849BB0-3C69-4d60-8771-C201E445BD5D}  Ԥ�ۿ����жϴ���
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            bool isPreOut = ctrlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Pre_Out, false, true);

            string applyID = this.fpCancelApply_Sheet1.Cells[this.fpCancelApply_Sheet1.ActiveRowIndex, (int)ColumnSet.ColID].Text;

            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.hsApply[applyID] as FS.HISFC.Models.Pharmacy.ApplyOut;

            //ȡ�����ϰ�ҩ����
            //{E8849BB0-3C69-4d60-8771-C201E445BD5D}  �����������
            if (itemManager.UndoCancelApplyOutByID(applyOut.ID, isPreOut) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("ȡ�����ϰ�ҩ����ʧ��"));
                return -1;
            }

            //ȡ������ҽ��ִ�е�

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("ȡ����������ɹ�"));

            this.AddRemoveCancelApply(false);

            return 1;
        }

        #region ��������Ϣ

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryApply();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.CancelApply();

            return base.OnSave(sender, neuObject);
        }

        #endregion

        private void ucCancelApply_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            string patientNO = this.ucQueryInpatientNo1.InpatientNo;
            if (patientNO == null || patientNO == "")
            {
                MessageBox.Show(Language.Msg("סԺ�Ų�����"));
                return;
            }

            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

            this.applyPatient = radtIntegrate.QueryPatientInfoByInpatientNO(patientNO);
            if (this.applyPatient == null)
            {
                MessageBox.Show(Language.Msg("����סԺ��ˮ�Ų���סԺ������Ϣʧ��") + radtIntegrate.Err);
                return;
            }

            this.SetPatientInfo();

            this.QueryApply();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (this.dtNormalApply == null)
                return;

            //��ù�������
            string queryCode = "%" + this.txtFilter.Text + "%";

            string filter = string.Format("(ƴ���� LIKE '{0}') OR (����� LIKE '{0}') OR (�Զ����� LIKE '{0}') OR (��Ʒ���� LIKE '{0}')", queryCode);

            this.dtNormalApply.DefaultView.RowFilter = filter;
        }

        private void fpNormalApply_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }

            this.AddRemoveCancelApply(true);
        }

        private void fpCancelApply_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader || e.ColumnHeader)
            {
                return;
            }

            this.fpCancelApply_Sheet1.ActiveRowIndex = e.Row;

            this.UnCancelApply();
        }

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ���ۼ�
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// ��������
            /// </summary>
            ColApplyQty,
            /// <summary>
            /// ���
            /// </summary>
            ColCost,
            /// <summary>
            /// �������
            /// </summary>
            ColApplyDept,
            /// <summary>
            /// ��ҩ����
            /// </summary>
            ColDrugDept,
            /// <summary>
            /// ����/���� ��
            /// </summary>
            ColOper,
            /// <summary>
            /// ����/���� ����
            /// </summary>
            ColDate,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ��ˮ��
            /// </summary>
            ColID,
            /// <summary>
            /// ƴ����
            /// </summary>
            ColSpellCode,
            /// <summary>
            /// �����
            /// </summary>
            ColWBCode,
            /// <summary>
            /// �Զ�����
            /// </summary>
            ColCustomeCode
        }
    }
}
