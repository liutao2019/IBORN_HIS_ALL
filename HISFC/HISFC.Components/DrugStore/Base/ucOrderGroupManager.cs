using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Base
{
    /// <summary>
    /// [��������: ҽ����������<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08-20]<br></br>
    /// </summary>
    public partial class ucOrderGroupManager : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucOrderGroupManager()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ҩƷ����������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// ϵͳʱ��
        /// </summary>
        DateTime sysTime;

        #endregion

        #region ����

        /// <summary>
        /// ��ҽ��������Ϣ����Fp
        /// </summary>
        /// <returns></returns>
        private int AddDataToFp(List<FS.HISFC.Models.Pharmacy.OrderGroup> orderGroupList)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            int iIndex = 0;
            foreach (FS.HISFC.Models.Pharmacy.OrderGroup info in orderGroupList)
            {
                this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);

                this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = info.ID;
                this.neuSpread1_Sheet1.Cells[iIndex, 1].Text = info.BeginTime.ToString("HH:mm:ss");
                this.neuSpread1_Sheet1.Cells[iIndex, 2].Text = info.EndTime.ToString("HH:mm:ss");
                this.neuSpread1_Sheet1.Cells[iIndex, 3].Text = info.Oper.ID;
                this.neuSpread1_Sheet1.Cells[iIndex, 4].Text = info.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");

                this.neuSpread1_Sheet1.Rows[iIndex].Tag = info;

                iIndex++;
            }

            return 1;
        }

        /// <summary>
        /// ��Fp�ڻ�ȡҽ������������Ϣ
        /// </summary>
        /// <param name="iRowIndex">������</param>
        /// <returns></returns>
        private FS.HISFC.Models.Pharmacy.OrderGroup GetDataFromFp(int iRowIndex)
        {
            FS.HISFC.Models.Pharmacy.OrderGroup orderGroup = new FS.HISFC.Models.Pharmacy.OrderGroup();

            orderGroup.ID = this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text;
            orderGroup.BeginTime = NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text);
            orderGroup.EndTime = NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text);

            return orderGroup;
        }

        /// <summary>
        /// ������ҽ������������Ϣ
        /// </summary>
        /// <returns></returns>
        private int AddNewOrderGroup()
        {
            int iCount = this.neuSpread1_Sheet1.Rows.Count;

            if (iCount == 0)                //��һ������
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                this.neuSpread1_Sheet1.Cells[0, 1].Text = this.sysTime.Date.ToString();
                this.neuSpread1_Sheet1.Cells[0, 1].Text = this.sysTime.Date.AddSeconds(-1).ToString();

                return 1;
            }

            this.neuSpread1_Sheet1.Rows.Add(iCount, 1);

            return 1;
        }

        /// <summary>
        /// ɾ��ҽ������������Ϣ
        /// </summary>
        /// <returns></returns>
        protected int DelOrderGroup()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ��ҽ������������Ϣ��"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No)
            {
                return 1;
            }

            string groupCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            DateTime dtBegin = NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text);
            DateTime dtEnd = NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 2].Text);
            int returnValue = this.consManager.DelOrderGroup(groupCode,dtBegin,dtEnd);

            if (returnValue == -1)
            {
                MessageBox.Show(Language.Msg("ɾ��ҽ������������Ϣ��������"));
                return -1;
            }
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);

            if (returnValue == 0)
            {
                MessageBox.Show("ɾ���ɹ���");
                return 1;
            }

            MessageBox.Show("ɾ���ɹ���\n�Ѿ��ɹ������ݿ���ɾ����Ϣ");

            


            return 1;
        }

        /// <summary>
        /// ����ҽ������������Ϣ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int QueryOrderGroup()
        {
            List<FS.HISFC.Models.Pharmacy.OrderGroup> orderGroupList = this.consManager.QueryOrderGroup();
            if (orderGroupList == null)
            {
                MessageBox.Show(Language.Msg("��ȡҽ������������Ϣ��������"));
                return -1;
            }

            return this.AddDataToFp(orderGroupList);
        }

        /// <summary>
        /// ��Ч�Ա���
        /// </summary>
        /// <returns></returns>
        protected bool Valid()
        {
            return true;
        }

        /// <summary>
        /// ҽ������������Ϣ����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int SaveOrderGroup()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.consManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.consManager.DelOrderGroup() == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("ɾ��ҽ������������Ϣ��������"));
                return -1;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.OrderGroup orderGroup = this.GetDataFromFp(i);

                orderGroup.Oper.ID = this.consManager.Operator.ID;

                if (this.consManager.InsertOrderGroup(orderGroup) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����ҽ������������Ϣ��������"));
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("����ɹ�"));

            return 1;
        }

        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F8)
            {
                this.neuDateTimePicker1.Visible = true;

                string group = this.consManager.GetOrderGroup(this.neuDateTimePicker1.Value);
                if (group == null)
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    MessageBox.Show(group);
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        #region IMaintenanceControlable ��Ա

        public int Add()
        {
            return this.AddNewOrderGroup();
        }

        public int Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Cut()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Delete()
        {
            return this.DelOrderGroup();
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Import()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Init()
        {
            this.sysTime = consManager.GetDateTimeFromSysDateTime();

            FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType markDateCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType();

            markDateCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.TimeOnly;

            this.neuSpread1_Sheet1.Columns[1].CellType = markDateCellType;
            this.neuSpread1_Sheet1.Columns[2].CellType = markDateCellType;

            return 1;
        }

        private bool isDirty = false;

        public bool IsDirty
        {
            get
            {
                return false;
            }
            set
            {
                this.isDirty = value;
            }
        }

        public int Modify()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int NextRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Paste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PreRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintConfig()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Query()
        {
            return this.QueryOrderGroup();
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public int Save()
        {
            return this.SaveOrderGroup();
        }

        #endregion
    }
}
