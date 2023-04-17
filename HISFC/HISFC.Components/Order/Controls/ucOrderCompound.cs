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

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ��Һ���Ĺ����˵]<br></br>
    /// [�� �� ��: dorian]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// 
    /// </summary>
    public partial class ucOrderCompound : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderCompound()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ҽ�����ܹ�����
        /// </summary>
        private HISFC.Components.Order.Classes.Function orderFun = new HISFC.Components.Order.Classes.Function();

        /// <summary>
        /// �б��Ƿ���ʾ���߲���
        /// </summary>
        private bool isShowNurseCell = true;

        /// <summary>
        /// ��Һ��Ϣ����׷������
        /// </summary>
        private int beforeDays = 1;

        /// <summary>
        /// ��Һ��Ϣ�����Ӻ�����
        /// </summary>
        private int afterDays = 2;

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ��ӡͬʱ���б���
        /// </summary>
        private bool isPrintWithSave = false;

        /// <summary>
        /// ��Һ��Ա��Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment compoundOper = null;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime dtBegin;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime dtEnd;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private System.Collections.Hashtable hsPatientInfo = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// �б��Ƿ���ʾ���߲��� True ��ʾ�����б� False ��ʾ�����б�
        /// </summary>
        [Description("�б��Ƿ���ʾ���߲��� True ��ʾ�����б� False ��ʾ�����б�"),Category("����"),DefaultValue(true)]
        public bool IsShowNurseCell
        {
            get
            {
                return this.isShowNurseCell;
            }
            set
            {
                this.isShowNurseCell = value;
            }
        }

        /// <summary>
        /// ��Һ��Ϣ����׷������
        /// </summary>
        [Description("��Һ��Ϣ����׷������"), Category("����"), DefaultValue(1)]
        public int BeforeDays
        {
            get
            {
                return this.beforeDays;
            }
            set
            {
                this.beforeDays = value;
            }
        }

        /// <summary>
        /// ��Һ��Ϣ�����Ӻ�����
        /// </summary>
        [Description("��Һ��Ϣ�����Ӻ�����"),Category("����"),DefaultValue(2)]
        public int AfterDays
        {
            get
            {
                return this.afterDays;
            }
            set
            {
                this.afterDays = value;
            }
        }

        /// <summary>
        /// �Ƿ��ӡͬʱ���б���
        /// </summary>
        [Description("�Ƿ��ӡͬʱ���б������ ������Һִ����Ϣ"),Category("����"),DefaultValue(false)]
        public bool IsPrintWithSave
        {
            get
            {
                return this.isPrintWithSave;
            }
            set
            {
                this.isPrintWithSave = value;
            }
        }            

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "ʱ��������趨", FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            toolBarService.AddToolButton("ȫѡ", "ȫѡ", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            toolBarService.AddToolButton("ȫ��ѡ", "ȫ��ѡ", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ȫѡ")
            {
                this.Check(true);
            }
            if (e.ClickedItem.Text == "ȫ��ѡ")
            {
                this.Check(false);
            }
            if (e.ClickedItem.Text == "����")
            {
                this.ChooseTime();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ShowCompounding();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.SaveCompounding() == 1)
            {
                this.ShowCompounding();
            }

            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.IPrint != null)
            {
                this.IPrint.SetTitle(this.compoundOper,this.operDept);

                this.IPrint.SetExecOrder(this.GetCheckExecOrder(), this.hsPatientInfo);

                this.IPrint.Print();
            }
            // ��ӡ����
            if (this.isPrintWithSave)
            {
                this.SaveCompounding();
            }
            return 1;
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        protected int Init()
        {
            this.compoundOper = new FS.HISFC.Models.Base.OperEnvironment();

            this.compoundOper.ID = CacheManager.OutOrderMgr.Operator.ID;
            this.compoundOper.Name = CacheManager.OutOrderMgr.Operator.Name;
            this.compoundOper.Dept = ((FS.HISFC.Models.Base.Employee)CacheManager.OutOrderMgr.Operator).Dept;

            DateTime sysTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

            dtBegin = sysTime.AddDays(-this.beforeDays);
            dtEnd = sysTime.AddDays(this.afterDays);

            this.SetQueryTimeShow();

            this.InitDeptTree();

            this.InitPrintInterface();

            return 1;
        }

        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        /// <returns></returns>
        protected int InitDeptTree()
        {
            if (this.isShowNurseCell)
            {
                ArrayList alNurse = CacheManager.InterMgr.GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType.N);
                if (alNurse == null)
                {
                    MessageBox.Show(Language.Msg("��ʾ�����б�������"));
                    return -1;
                }

                return this.AddDataToTree(alNurse);
            }
            else
            {
                ArrayList alDept = CacheManager.InterMgr.GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType.I);
                if (alDept == null)
                {
                    MessageBox.Show(Language.Msg("��ʾ�����б�������"));
                    return -1;
                }

                return this.AddDataToTree(alDept);
            }            
        }

        /// <summary>
        /// �������ݵ�Tree��ʾ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToTree(ArrayList alData)
        {
            this.tvDeptTree.Nodes.Clear();

            TreeNode parentNode = new TreeNode("��Һ�����б�");
            parentNode.Tag = null;

            foreach (FS.HISFC.Models.Base.Department deptInfo in alData)
            {
                TreeNode node = new TreeNode(deptInfo.Name);
                node.Tag = deptInfo;

                parentNode.Nodes.Add(node);
            }

            this.tvDeptTree.Nodes.Add(parentNode);

            this.tvDeptTree.ExpandAll();

            return 1;
        }

        #endregion

        #region ��ӡ����

        private FS.HISFC.BizProcess.Interface.IPrintExecDrug IPrint = null;

        /// <summary>
        /// ��ӡ�ӿڳ�ʼ��
        /// </summary>
        protected void InitPrintInterface()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ��ش�ӡ�ӿ���Ϣ.."));
            Application.DoEvents();

            try
            {
                object[] o = new object[] { };
                ////�Ժ���ά�������ȡ������
                //System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", "Report.Order.ucDrugCompound", false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
                //if (objHandel != null)
                //{
                //    object oLabel = objHandel.Unwrap();

                //    this.IPrint = oLabel as FS.HISFC.BizProcess.Integrate.IPrintExecDrug;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �ж������Ƿ������ͬ����Ϻ�
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool IsSameCombo(int i, int j)
        {
            try
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColComboNO].Text == this.neuSpread1_Sheet1.Cells[j, (int)ColumnSet.ColComboNO].Text)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// �������ѡ�С���ѡ�еĴ���
        /// </summary>
        /// <param name="iOriginalRow">��ǰѡ����</param>
        private void SetComboCheck(int activeRow)
        {
            //ѡ��/ȡ������ĳ����Ŀ�� ������������Ŀͬ��ѡ��/ȡ��
            bool isCheck = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[activeRow, (int)ColumnSet.ColCheck].Value);

            int privIndex = activeRow - 1;
            while (privIndex >= 0 && this.IsSameCombo(privIndex, activeRow))
            {
                this.neuSpread1_Sheet1.Cells[privIndex, (int)ColumnSet.ColCheck].Value = isCheck;

                privIndex = privIndex - 1;
            }

            int nextIndex = activeRow + 1;
            while (nextIndex < this.neuSpread1_Sheet1.Rows.Count && this.IsSameCombo(nextIndex, activeRow))
            {
                this.neuSpread1_Sheet1.Cells[nextIndex, (int)ColumnSet.ColCheck].Value = isCheck;

                nextIndex = nextIndex + 1;
            }
        }

        /// <summary>
        /// ��ȡ��ǰѡ�е�ҽ��ִ����Ϣ
        /// </summary>
        /// <returns></returns>
        private ArrayList GetCheckExecOrder()
        {
            ArrayList alExecOrder = new ArrayList();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCheck].Value))
                {
                    alExecOrder.Add(this.neuSpread1_Sheet1.Rows[i].Tag);
                }
            }

            return alExecOrder;
        }

        /// <summary>
        /// ��ѯ������ʾ
        /// </summary>
        private void SetQueryTimeShow()
        {
            this.lbTimeInfo.Text = string.Format("��ѯ����:{0} �� {1}", dtBegin.ToString(), dtEnd.ToString());
        }

        /// <summary>
        /// ��������ѡ��
        /// </summary>
        private void ChooseTime()
        {
            if (FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref this.dtBegin, ref this.dtEnd) == 1)
            {
                this.SetQueryTimeShow();

                this.ShowCompounding();
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        protected void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ȫѡ/ȫ��ѡ
        /// </summary>
        /// <param name="isCheck">�Ƿ�ѡ��</param>
        protected void Check(bool isCheck)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCheck].Value = isCheck;
            }
        }

        /// <summary>
        /// ��Ч�Լ��
        /// </summary>
        /// <returns></returns>
        protected bool IsValid()
        {
            bool isHaveCheck = false;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCheck].Value))
                {
                    isHaveCheck = true;
                    break;
                }
            }

            if (!isHaveCheck)
            {
                MessageBox.Show(Language.Msg("��ѡ��ȷ��ִ�е���Һ��Ϣ"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// ��Fp�ڼ�����Ϣ
        /// </summary>
        /// <param name="execOrder">ҽ��ִ�е���Ϣ</param>
        /// <param name="iRowIndex">������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int AddDataToFp(FS.HISFC.Models.Order.ExecOrder execOrder,int iRowIndex)
        {
            this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);

            string patientName = "";
            if (this.hsPatientInfo.ContainsKey(execOrder.Order.Patient.ID))
            {
                FS.HISFC.Models.RADT.PatientInfo patient = this.hsPatientInfo[execOrder.Order.Patient.ID] as FS.HISFC.Models.RADT.PatientInfo;

                patientName = "[" + patient.PVisit.PatientLocation.Bed.ID + "]" + patient.Name;
            }
            else
            {
                FS.HISFC.Models.RADT.PatientInfo patient = CacheManager.RadtIntegrate.GetPatientInfoByPatientNO(execOrder.Order.Patient.ID);

                if (patient == null)
                {
                    MessageBox.Show(Language.Msg("���ݻ�����ˮ�Ż�ȡ������Ϣ��������") + CacheManager.RadtIntegrate.Err);
                    return -1;
                }

                patientName = "[" + patient.PVisit.PatientLocation.Bed.ID + "]" + patient.Name;

                this.hsPatientInfo.Add(patient.ID, patient);
            }


            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColName].Text = patientName;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColCheck].Value = true;

            FS.HISFC.Models.Pharmacy.Item item = ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item);
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColTradeNameSpecs].Text = item.Name + "[" + item.Specs + "]";
            
            //����  ...                      
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColDoseonce].Text = execOrder.Order.DoseOnce.ToString();
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColFrequency].Text = execOrder.Order.Frequency.Name;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColUsage].Text = execOrder.Order.Usage.Name;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColQty].Text = execOrder.Order.Qty.ToString() + execOrder.Order.Unit;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColEmergency].Value = execOrder.Order.IsEmergency;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColRecipeDoc].Text = execOrder.Order.ReciptDoctor.Name;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColMemo].Text = execOrder.Order.Memo;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColSortID].Text = execOrder.Order.SortID.ToString();
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColExecID].Text = execOrder.ID;
            this.neuSpread1_Sheet1.Cells[iRowIndex, (int)ColumnSet.ColComboNO].Text = execOrder.Order.Combo.ID + execOrder.DateUse.ToString();

            this.neuSpread1_Sheet1.Rows[iRowIndex].Tag = execOrder;
            return 1;
        }

        /// <summary>
        /// ˢ����ʾ���Ҵ���Һ��Ϣ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 </returns>
        protected int ShowCompounding()
        {
            this.Clear();

            ArrayList al = CacheManager.InOrderMgr.QueryExecOrderForCompound(this.isShowNurseCell, this.operDept.ID, this.dtBegin, this.dtEnd, this.ckExec.Checked);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("����Һ��Ϣ��ѯʧ��") + CacheManager.OutOrderMgr.Err);
                return -1;
            }

            int iRowIndex = 0;
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in al)
            {
                this.AddDataToFp(execOrder, iRowIndex);

                iRowIndex++;
            }

            HISFC.Components.Order.Classes.Function.DrawCombo(this.neuSpread1_Sheet1, (int)ColumnSet.ColComboNO, (int)ColumnSet.ColComboFlag);            
            return 1;   
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected int SaveCompounding()
        {
            if (!this.IsValid())
            {
                return -1;
            }

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.OutOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Order.Compound compoundInfo = new FS.HISFC.Models.Order.Compound();
            compoundInfo.IsExec = true;
            compoundInfo.CompoundOper = this.compoundOper;
            compoundInfo.CompoundOper.OperTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (!NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCheck].Value))
                {
                    continue;
                }

                string execID = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColExecID].Text;

                if (CacheManager.InOrderMgr.UpdateOrderCompound(execID, compoundInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("����ҽ����Һ��Ϣ�������� ҽ��ִ����ˮ��:" + execID + "\n" + CacheManager.OutOrderMgr.Err));
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����ɹ�"));

            return 1;
        }

        #endregion

        private void tvDeptTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node.Tag != null)
            {
                this.operDept = e.Node.Tag as FS.FrameWork.Models.NeuObject;

                this.ShowCompounding();
            }
        }

        private void ckUnExec_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowCompounding();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
            base.OnLoad(e);
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColCheck].Value))
            {
                this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColCheck].Value = false;
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColCheck].Value = true;
            }

            this.SetComboCheck(e.Row);
        }

        #region ������

        private enum ColumnSet
        {
            /// <summary>
            /// [����]����
            /// </summary>
            ColName,
            /// <summary>
            /// �Ƿ�ѡ��
            /// </summary>
            ColCheck,
            /// <summary>
            /// ��Ʒ����[���]
            /// </summary>
            ColTradeNameSpecs,
            /// <summary>
            /// ����
            /// </summary>
            ColComboFlag,
            /// <summary>
            /// ÿ����
            /// </summary>
            ColDoseonce,
            /// <summary>
            /// Ƶ��
            /// </summary>
            ColFrequency,
            /// <summary>
            /// �÷�
            /// </summary>
            ColUsage,
            /// <summary>
            /// ����
            /// </summary>
            ColQty,
            /// <summary>
            /// �Ӽ�
            /// </summary>
            ColEmergency,
            /// <summary>
            /// ����ҽ��
            /// </summary>
            ColRecipeDoc,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ˳���
            /// </summary>
            ColSortID,
            /// <summary>
            /// ��ˮ��
            /// </summary>
            ColExecID,
            /// <summary>
            /// ��Ϻ�
            /// </summary>
            ColComboNO
        }

        #endregion            
       
    }
}
