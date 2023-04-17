using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ���뵥���б���ʾ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// 
    /// {C77D4120-D4BF-4770-8B8F-973F52EB5056}
    /// </summary>
    public partial class ucPhaApplyList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPhaApplyList()
        {
            InitializeComponent();
        }

        #region ί��

        /// <summary>
        /// ˫��ί��
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public delegate int DoubleClickDelegate(object doubleClickRowTag);

        /// <summary>
        /// ί�з���ʵ��
        /// </summary>
        private DoubleClickDelegate doubleClickInstanceMethod = null;

        /// <summary>
        /// ί�з���ʵ��
        /// </summary>
        public DoubleClickDelegate DoubleClickInstanceMethod
        {
            set
            {
                this.doubleClickInstanceMethod = value;
            }
        }

        #endregion

        #region ���Ա����

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������Ϣ����
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> applyListCollection = new List<FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// �������
        /// </summary>
        private DialogResult rs = DialogResult.Cancel;
        #endregion

        #region ����

        /// <summary>
        /// ��ѡ������뵥����Ϣ����
        /// </summary>
        public List<FS.FrameWork.Models.NeuObject> ApplyListCollection
        {
            get
            {
                return this.applyListCollection;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.rs;
            }
            set
            {
                this.rs = value;
            }
        }

        /// <summary>
        /// ����List Sheet ҳ��ʾ����
        /// </summary>
        public string DisplaySheetName
        {
            set
            {
                this.fsApplyData_List.SheetName = value;
            }
        }

        /// <summary>
        /// ���ý�����ʾ��Ϣ
        /// </summary>
        public string DisplayNotice
        {
            set
            {
                this.lbNotice.Text = value;
            }
        }

        /// <summary>
        /// ��ϸ��Ϣ��ʾ Sheet ҳ
        /// </summary>
        public FarPoint.Win.Spread.SheetView DetailSheet
        {
            get
            {
                return this.fsApplyData_Detail;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ���ϰ�ť
        /// </summary>
        public bool IsShowCancelButton
        {
            set
            {
                this.btnCancel.Visible = value;
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public int Init()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();
            if (alDept != null)
            {
                this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
            }

            return 1;
        }

        /// <summary>
        /// �������
        /// </summary>
        protected void Clear()
        {
            this.fsApplyData_List.Rows.Count = 0;
            this.fsApplyData_Detail.Rows.Count = 0;
        }

        /// <summary>
        /// ������Ϣ���ݼ���
        /// </summary>
        /// <param name="alApplyList">����������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal int ShowData(ArrayList alApplyList)
        {
            this.Clear();

            int iRowIndex = 0;
            foreach (FS.FrameWork.Models.NeuObject info in alApplyList)
            {
                this.fsApplyData_List.Rows.Add(iRowIndex, 1);
                this.fsApplyData_List.Cells[iRowIndex, 0].Value = false;
                this.fsApplyData_List.Cells[iRowIndex, 1].Text = info.ID;       //���ݺ�
                this.fsApplyData_List.Cells[iRowIndex, 2].Text = info.Name;     //Ŀ�굥λ����                

                this.fsApplyData_List.Rows[iRowIndex].Tag = info;
            }
            return 1;
        }

        /// <summary>
        /// �����б���Ϣ����
        /// </summary>
        /// <returns></returns>
        public int QueryApplyListByTarget(FS.FrameWork.Models.NeuObject stockDept,string class3MeaningCode,string applyState)
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alList = itemManager.QueryApplyOutListByTargetDept(stockDept.ID, class3MeaningCode, applyState);
            if (alList == null)
            {
                MessageBox.Show("����ҩƷ������Ϣ�б�������" + itemManager.Err);
                return -1;
            }

            foreach (FS.FrameWork.Models.NeuObject info in alList)
            {
                info.User03 = stockDept.ID;
            }

            this.ShowData(alList);

            return 1;
        }

        /// <summary>
        /// �����б���Ϣ����
        /// </summary>
        /// <returns></returns>
        public int QueryApplyListByApply(FS.FrameWork.Models.NeuObject applyDept, string class3MeaningCode, string applyState)
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alList = itemManager.QueryApplyOutList(applyDept.ID, class3MeaningCode, applyState);
            if (alList == null)
            {
                MessageBox.Show("����ҩƷ������Ϣ�б�������" + itemManager.Err);
                return -1;
            }

            foreach (FS.FrameWork.Models.NeuObject info in alList)
            {
                info.User03 = info.Memo;        //��浥λ����
            }

            this.ShowData(alList);

            return 1;
        }

        /// <summary>
        /// ������ϸ��Ϣ����
        /// </summary>
        /// <returns></returns>
        protected int QueryApplyDetail(int rowIndex)
        {
            FS.FrameWork.Models.NeuObject info = this.fsApplyData_List.Rows[rowIndex].Tag as FS.FrameWork.Models.NeuObject;
            string listNO = this.fsApplyData_List.Cells[rowIndex, 1].Text;

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            ArrayList alDetail = itemManager.QueryApplyOutInfoByListCode(info.Memo, listNO, "0");
            if (alDetail == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("δ��ȷ��ȡ�ڲ����������Ϣ" + itemManager.Err));
                return -1;
            }

            this.fsApplyData_Detail.Rows.Count = 0;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alDetail)
            {
                this.fsApplyData_Detail.Rows.Add(0, 1);
                this.fsApplyData_Detail.Cells[0, 0].Text = applyOut.Item.Name;
                this.fsApplyData_Detail.Cells[0, 1].Text = applyOut.Item.Specs;
                this.fsApplyData_Detail.Cells[0, 2].Text = applyOut.Operation.ApplyQty.ToString();
                this.fsApplyData_Detail.Cells[0, 3].Text = applyOut.Operation.ApproveQty.ToString();
                this.fsApplyData_Detail.Cells[0,4].Text = applyOut.Item.MinUnit;
            }

            return 1;
        }

        /// <summary>
        /// �ر�
        /// </summary>
        protected void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.applyListCollection.Clear();

            for (int i = 0; i < this.fsApplyData_List.Rows.Count; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fsApplyData_List.Cells[i, 0].Value))
                {
                    this.applyListCollection.Add(this.fsApplyData_List.Rows[i].Tag as FS.FrameWork.Models.NeuObject);
                }
            }

            this.rs = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.rs = DialogResult.Cancel;

            this.Close();
        }

        private void fsApplyData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.doubleClickInstanceMethod != null)
            {
                this.doubleClickInstanceMethod( this.fsApplyData_List.Rows[this.fsApplyData_List.ActiveRowIndex].Tag );
            }
            else
            {
                this.QueryApplyDetail( this.fsApplyData_List.ActiveRowIndex );
            }

            this.fsApplyData.ActiveSheet = this.fsApplyData_Detail;
        }
    }
}
