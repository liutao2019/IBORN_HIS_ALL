using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    public partial class ucChooseDrugControl : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucChooseDrugControl()
        {
            InitializeComponent();

            this.neuSpread1.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
        }

        public delegate void SelectControlDelegate(FS.HISFC.Models.Pharmacy.DrugControl drugControl);

        public event SelectControlDelegate SelectControlEvent;

        /// <summary>
        /// ��ǰѡ��İ�ҩ̨
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugControl drugControl = new FS.HISFC.Models.Pharmacy.DrugControl();

        /// <summary>
        /// ��ǰѡ��Ŀ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject selectOperDept = null;

        /// <summary>
        /// �Ƿ���ʾ�����б�
        /// </summary>
        [Description("�Ƿ���ʾ�����б�"),Category("����"),DefaultValue(false)]
        public bool IsShowDept
        {
            get
            {
                return this.panelTree.Visible;
            }
            set
            {
                this.panelTree.Visible = value;
            }
        }

        /// <summary>
        /// ��ǰѡ��Ŀ���
        /// </summary>
        public FS.FrameWork.Models.NeuObject SelectOperDept
        {
            get
            {
                return this.selectOperDept;
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void InitDeptList()
        {
            try
            {
                FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
                this.ShowControlList(((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept.ID);

                this.tvDeptTree1.IsShowPI = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ʼ����ҩ̨�б�������" + ex.Message);
            }
        }

        /// <summary>
        /// ��ʾ������ȫ����ҩ̨�б�
        /// </summary>
        public virtual void ShowControlList(string deptCode)
        {
            //�����ǰ��ʾ�İ�ҩ̨
            this.neuSpread1_Sheet1.Rows.Count = 0;

            //�жϿ��ұ����Ƿ����
            if (deptCode == "")
            {
                MessageBox.Show(Language.Msg("��Ч�İ�ҩ���ң�û�п���ѡ��İ�ҩ̨"));
                return;
            }

            //����ҩ��������
            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

            //ȡ������ȫ����ҩ̨�б�
            ArrayList al = drugStoreManager.QueryDrugControlList(deptCode);
            if (al == null)
            {
                MessageBox.Show(drugStoreManager.Err);
                return;
            }
            if (al.Count == 0)
            {
                MessageBox.Show(Language.Msg("�����ڵĿ���û�����ð�ҩ̨���������ñ����ҵİ�ҩ̨��"));
                return;
            }
           
            this.neuSpread1_Sheet1.Rows.Add(0, al.Count);
            FS.HISFC.Models.Pharmacy.DrugControl drugControl;
            for (int i = 0; i < al.Count; i++)
            {
                drugControl = al[i] as FS.HISFC.Models.Pharmacy.DrugControl;

                FarPoint.Win.Spread.CellType.ButtonCellType btnType = new FarPoint.Win.Spread.CellType.ButtonCellType();
                btnType.ButtonColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(225)), ((System.Byte)(243)));
                btnType.Text = drugControl.Name;
                btnType.TextDown = drugControl.Name;
                this.neuSpread1_Sheet1.Cells[i, 0].CellType = btnType;

                this.neuSpread1_Sheet1.Cells[i, 1].Text = drugControl.SendType == 0 ? "ȫ��" : (drugControl.SendType == 1 ? "����" : "��ʱ");
                this.neuSpread1_Sheet1.Cells[i, 2].Text = drugControl.ShowLevel == 0 ? "��ʾ���һ���" : (drugControl.ShowLevel == 1 ? "��ʾ������ϸ" : "��ʾ������ϸ");
                this.neuSpread1_Sheet1.Rows[i].Tag = drugControl;
            }

            if (al.Count == 1)
            {
                this.drugControl = al[0] as FS.HISFC.Models.Pharmacy.DrugControl;

                if (this.SelectControlEvent != null)
                {
                    this.SelectControlEvent(this.drugControl);
                }
                return;
            }
        }

         private void tvDeptTree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.ShowControlList((e.Node.Tag as FS.HISFC.Models.Base.Department).ID);

                this.selectOperDept = e.Node.Tag as FS.HISFC.Models.Base.Department;
            }
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0)
            {
                this.drugControl = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Pharmacy.DrugControl;

                if (this.SelectControlEvent != null)
                {
                    this.SelectControlEvent(this.drugControl);
                }
            }
        }

    }
}
