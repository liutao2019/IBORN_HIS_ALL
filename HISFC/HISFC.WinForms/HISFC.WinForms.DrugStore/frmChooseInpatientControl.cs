using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.WinForms.DrugStore
{
    public partial class frmChooseInpatientControl : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmChooseInpatientControl()
        {
            InitializeComponent();

            this.ProgressRun(true);
        }

        /// <summary>
        /// ҩ����
        /// </summary>
        private void SetArkDept(ref FS.FrameWork.Models.NeuObject operDept)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.DeptConstant deptCons = phaConsManager.QueryDeptConstant(operDept.ID);
            if (deptCons == null)
            {
                MessageBox.Show(Language.Msg("���ݿ��ұ����ȡ���ҳ�����Ϣ��������") + phaConsManager.Err);
                return;
            }

            if (deptCons.IsArk)         //��ҩ�������ҽ������´���
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                ArrayList al = managerIntegrate.LoadPhaParentByChildren(operDept.ID);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show(Language.Msg("��ȡ���ҽṹ��Ϣ��������") + managerIntegrate.Err);
                    return;
                }

                FS.HISFC.Models.Base.DepartmentStat deptStat = al[0] as FS.HISFC.Models.Base.DepartmentStat;
                if (deptStat.PardepCode.Substring(0, 1) == "S")     //�ϼ��ڵ�Ϊ������� �����д���
                {
                    return;
                }
                else
                {
                    operDept.ID = deptStat.PardepCode;
                    operDept.Name = deptStat.PardepName;
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //�˳�
            if (keyData == Keys.F10)
            {
                this.Close();
            }
            //��ʾ��ҩ��
            if (keyData == Keys.F12)
            {
                this.ShowDrugApprove();
            }
            return base.ProcessDialogKey(keyData);
        }

        private DrugStore.frmInpatientDrug frmDrugManager = null;

        private frmDrugBillApprove frmApprove = null;

        /// <summary>
        /// ��ҩ����
        /// </summary>
        private void ShowDrugManager(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            frmDrugManager  = new frmInpatientDrug();

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            if (drugControl.Dept == null || drugControl.Dept.ID == "")
            {
                drugControl.Dept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept.Clone();
            }

            frmDrugManager.SetDrugControl(drugControl);

            frmDrugManager.Show();
        }

        /// <summary>
        /// ��ҩ������ ��ҩ��׼��ʾ
        /// </summary>
        private void ShowDrugApprove()
        {
            frmApprove = new frmDrugBillApprove();

            if (this.ucChooseDrugControl1.SelectOperDept != null)
            {
                frmApprove.OperDept = this.ucChooseDrugControl1.SelectOperDept;
            }

            frmApprove.ShowDialog();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

            FS.FrameWork.Models.NeuObject operDept = ((FS.HISFC.Models.Base.Employee)drugStoreManager.Operator).Dept.Clone();

            this.SetArkDept(ref operDept);

            if (this.Tag.ToString() != "1")
            {
                //����ʾ�����б�
                this.ucChooseDrugControl1.IsShowDept = false;
                this.ucChooseDrugControl1.ShowControlList(operDept.ID);
            }
            else
            {
                this.ucChooseDrugControl1.IsShowDept = true;
                this.ucChooseDrugControl1.InitDeptList();
            }
        }

        private void ucChooseDrugControl1_SelectControlEvent(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            this.ShowDrugManager(drugControl);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tsbExit)
                this.Close();
            if (e.ClickedItem == this.tsbBill)
                this.ShowDrugApprove();
        }

        protected override void OnClosed(EventArgs e)
        {
            //�����ڹر�ʱ �ر����д򿪵ĸ�������
            if (this.frmApprove != null)
            {
                this.frmApprove.Close();
            }
            if (this.frmDrugManager != null)
            {
                this.frmDrugManager.Close();
            }
            base.OnClosed(e);
        }

    }
}