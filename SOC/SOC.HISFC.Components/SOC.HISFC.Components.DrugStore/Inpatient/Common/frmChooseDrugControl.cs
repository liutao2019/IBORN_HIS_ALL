using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// [��������: סԺҩ����ҩ̨ѡ��]<br></br>
    /// [�� �� ��: liangzj]<br></br>
    /// [����ʱ��: ]<br></br>
    /// ˵����
    /// 1��ȥ���˿��������������ܲ��󲿷ֹ���
    /// </summary>
    public partial class frmChooseDrugControl : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmChooseDrugControl()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.neuSpread1.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);
            this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            if (System.IO.File.Exists(settingFileName))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, settingFileName);
            }
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, settingFileName);
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        private string settingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\ChooseDrugControlSetting.xml";

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ��ǰѡ��İ�ҩ̨
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugControl drugControl;

        /// <summary>
        /// ��ǰѡ��İ�ҩ̨
        /// </summary>
        public FS.HISFC.Models.Pharmacy.DrugControl DrugControl
        {
            get { return drugControl; }
        }

        /// <summary>
        /// ��ʾ������ȫ����ҩ̨�б�
        /// </summary>
        public virtual int ShowControlList(FS.FrameWork.Models.NeuObject dept)
        {
            //�жϿ��ұ����Ƿ����
            if (dept == null)
            {
                MessageBox.Show(Language.Msg("��Ч�İ�ҩ���ң�û�п���ѡ��İ�ҩ̨"));
                return -1;
            }

            string deptCode = dept.ID;
           
            //�жϿ��ұ����Ƿ����
            if (deptCode == "")
            {
                MessageBox.Show(Language.Msg("��Ч�İ�ҩ���ң�û�п���ѡ��İ�ҩ̨"));
                return -1;
            }

            this.nlbTitly.Text = dept.Name + "��ҩ̨��Ϣ";

            //�����ǰ��ʾ�İ�ҩ̨
            this.neuSpread1_Sheet1.Rows.Count = 0;


            //����ҩ��������
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            //ȡ������ȫ����ҩ̨�б�
            ArrayList al = drugStoreManager.QueryDrugControlList(deptCode);
            if (al == null)
            {
                MessageBox.Show(drugStoreManager.Err);
                return -1;
            }
            if (al.Count == 0)
            {
                MessageBox.Show(Language.Msg("�����ڵĿ���û�����ð�ҩ̨���������ñ����ҵİ�ҩ̨��"));
                return -1;
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
                this.DialogResult = DialogResult.OK;
            }

            return al.Count;
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0)
            {
                this.drugControl = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Pharmacy.DrugControl;
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
        }

    }
}
