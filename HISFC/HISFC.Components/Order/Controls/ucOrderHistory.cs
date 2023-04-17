using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ��ʷҽ����ѯ]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='Dorian'
    ///		�޸�ʱ��='2008-01'
    ///		�޸�Ŀ��='��������ҽԺ���޸��������'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucOrderHistory : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderHistory()
        {
            InitializeComponent();
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            //{7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}
            this.fpSpread1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpSpread1_MouseUp);
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Tag == null) return;
                FS.FrameWork.Models.NeuObject obj = e.Node.Tag as FS.FrameWork.Models.NeuObject;
                ArrayList al = CacheManager.InOrderMgr.QueryOrder(obj.ID);
                //��ҽ���ֳ�������ʱ�ֱ��
                this.AddObjectsToList(al);
                if (alAllLong != null)
                {
                    FS.HISFC.Components.Common.Classes.Function.ShowOrder(this.fpSpread1_Sheet1, alAllLong, FS.HISFC.Models.Base.ServiceTypes.I);
                }
                if (alAllShort != null)
                {
                    FS.HISFC.Components.Common.Classes.Function.ShowOrder(this.fpSpread1_Sheet2, alAllShort, FS.HISFC.Models.Base.ServiceTypes.I);
                }
                #region {EE6864E5-796D-4b21-B9C4-ABD40F5CF9A5}
                for (int i = 0; i < this.fpSpread1_Sheet1.Columns.Count; i++)
                {
                    this.fpSpread1_Sheet1.Columns[i].Locked = true;
                }
                for (int i = 0; i < this.fpSpread1_Sheet2.Columns.Count; i++)
                {
                    this.fpSpread1_Sheet2.Columns[i].Locked = true;
                }
                #endregion

            }
            catch { }
        }
        /// <summary>
        /// ����ҽ��ArrayList
        /// </summary>
        protected ArrayList alAllLong = new ArrayList(); 
        /// <summary>
        /// ��ʱҽ��ArrayList
        /// </summary>
        protected ArrayList alAllShort = new ArrayList();
        /// <summary>
        /// ���ʵ��toArrayList
        /// </summary>
        /// <param name="list"></param>
        private void AddObjectsToList(ArrayList list)
        {
            if (alAllLong != null)
                alAllLong.Clear();
            if (alAllShort != null)
                alAllShort.Clear();
            foreach (object obj in list)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    //����ҽ��

                    alAllLong.Add(order);
                }
                else
                {
                    //��ʱҽ��
                    alAllShort.Add(order);
                }
            }
        }

        //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
        //�Ƿ���ʾסԺ�������
        private bool isShowPatientControl = false;

        /// <summary>
        /// �Ƿ���ʾסԺ�������
        /// </summary>
        [Category("�ؼ�����"),Description("�Ƿ���ʾסԺ�������"),DefaultValue(false)]
        public bool IsShowPatientControl
        {
            get { return isShowPatientControl; }
            set { this.isShowPatientControl = value; }
        }


        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            return 0;
        }

        protected FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                if (value == null) return;
                //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
                this.txtPatientNO.Text = value.PID.PatientNO;
                ArrayList al = CacheManager.RadtIntegrate.QueryInpatientNoByPatientNo(value.PID.PatientNO);
                if (al == null)
                {
                    MessageBox.Show("��ȡ������Ϣ����", CacheManager.RadtIntegrate.Err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.treeView1.Nodes[0].Nodes.Clear();
                    if (this.fpSpread1_Sheet1.Rows.Count > 0)
                    {
                        this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.RowCount);
                    }
                    if (this.fpSpread1_Sheet2.Rows.Count > 0)
                    {
                        this.fpSpread1_Sheet2.RemoveRows(0, this.fpSpread1_Sheet2.RowCount);
                    }

                    return;
                }
                if (al.Count == 0)
                {
                    this.treeView1.Nodes[0].Nodes.Clear();
                    if (this.fpSpread1_Sheet1.Rows.Count > 0)
                    {
                        this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.RowCount);
                    }
                    if (this.fpSpread1_Sheet2.Rows.Count > 0)
                    {
                        this.fpSpread1_Sheet2.RemoveRows(0, this.fpSpread1_Sheet2.RowCount);
                    }
                    return;
                }

                this.treeView1.Nodes[0].Nodes.Clear();
                #region {1705F464-D530-47e8-A675-761A10CA8E52}
                if (this.fpSpread1_Sheet1.Rows.Count > 0)
                {
                    this.fpSpread1_Sheet1.RemoveRows(0, this.fpSpread1_Sheet1.RowCount);
                }
                if (this.fpSpread1_Sheet2.Rows.Count > 0)
                {
                    this.fpSpread1_Sheet2.RemoveRows(0, this.fpSpread1_Sheet2.RowCount);
                }
                #endregion
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
                    string no = obj.ID;
                    FS.HISFC.Models.RADT.PatientInfo p = CacheManager.RadtIntegrate.GetPatientInfomation(obj.ID);
                    if (p == null)
                    {
                        MessageBox.Show("��ѯ������Ϣ����" + CacheManager.RadtIntegrate.Err);
                        return;
                    }
                    if (p.PVisit.InState.ID.ToString() == "I")
                    {
                        continue;
                    }
                    if (obj.ID != value.ID)
                    {
                        #region {7DFF015D-7EE9-447f-8222-B29A39DBD409}
                        //try
                        //{
                        //    no = obj.ID.Substring(2, 2);
                        //}
                        //catch { }
                        //TreeNode node = new TreeNode("[" + no + "]" + obj.Name);
                        //node.ImageIndex = 1;
                        //node.SelectedImageIndex = 2;
                        //node.Tag = obj;
                        //this.treeView1.Nodes[0].Nodes.Add(node); 
                        this.AddPatientToTree(p);
                        #endregion
                    }
                }
            }
        }

        //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
        private void txtPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtPatientNO.Text.Trim()))
            {
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

            

            this.txtPatientNO.Text = this.txtPatientNO.Text.Trim().PadLeft(10, '0');
            p.PID.PatientNO = this.txtPatientNO.Text;

            this.Patient = p;
        }
        //{3EC00FCD-64D6-49ea-8A23-CB2B0CAB9A53}
        protected override void OnLoad(EventArgs e)
        {
            this.txtPatientNO.Visible = this.isShowPatientControl;

            this.neuLabel1.Visible = this.isShowPatientControl;
            this.neuPanel2.Visible = this.isShowPatientControl;

            this.ucQueryInpatientNo1.IsCanChangeInputType = true;
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);

            base.OnLoad(e);
        }

        #region {7DFF015D-7EE9-447f-8222-B29A39DBD409}


        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("סԺ�Ŵ���û���ҵ��û���", 111);
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            string no = this.ucQueryInpatientNo1.InpatientNo;
            FS.HISFC.Models.RADT.PatientInfo p = CacheManager.RadtIntegrate.GetPatientInfomation(no);
            if (p == null)
            {
                MessageBox.Show("��ѯ������Ϣ����" + CacheManager.RadtIntegrate.Err);
                return;
            }
            #region {F170B20E-6C35-476c-AD94-F6819B5E7CFD}
            bool isFound = false;
            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
            {
                if (p.ID == ((FS.FrameWork.Models.NeuObject)this.treeView1.Nodes[0].Nodes[i].Tag).ID)
                {
                    isFound = true;
                }
            }
            if (isFound)
            {
                MessageBox.Show("�û��������б���");
                return;
            }
            else
            {
                this.AddPatientToTree(p);
            }
            //this.AddPatientToTree(p); 
            #endregion
        }

        private void AddPatientToTree(FS.HISFC.Models.RADT.PatientInfo myPatient)
        {
            string inTimes = string.Empty;//סԺ����
            string inTime = string.Empty;//��Ժ����
            try
            {
                //update by zhaorong at 2013-9-2 ������סԺ��סԺ���޸�Ϊ��Ժʱ��
                //no = myPatient.ID.Substring(2, 2);
                inTimes = myPatient.InTimes.ToString();
                inTime = myPatient.PVisit.InTime.ToString("yyyy-MM-dd");
            }
            catch { }
            TreeNode node = new TreeNode("[" + inTime + "]["+ inTimes+"]"+ myPatient.Name);
            node.ImageIndex = 1;
            node.SelectedImageIndex = 2;
            node.Tag = myPatient;
            this.treeView1.Nodes[0].Nodes.Add(node);
        } 
        #endregion

        #region ����ճ��{7E9CE45E-3F00-4540-8C5C-7FF6AE1FF992}

        /// <summary>
        /// �Ҽ��˵�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();


        /// <summary>
        /// ����
        /// </summary>
        private void CopyOrder()
        {
            #region �޸�ԭ����--�÷���ֻ�����fpSpread1_Sheet1 //update by zhaorong at 2013-9-2
            //if (this.fpSpread1_Sheet1.Rows.Count <= 0) return;

            //this.fpSpread1.Focus();

            //ArrayList list = new ArrayList();

            ////��ȡѡ���е�ҽ��ID
            //for (int row = 0; row < this.fpSpread1_Sheet1.Rows.Count; row++)
            //{
            //    for (int col = 0; col < this.fpSpread1_Sheet1.Columns.Count; col++)
            //    {
            //        if (this.fpSpread1_Sheet1.IsSelected(row, col))
            //        {
            //            list.Add(fpSpread1_Sheet1.Cells[row, 0].Value.ToString());
            //            break;
            //        }
            //    }
            //}

            //if (list.Count <= 0) return;
            ////����ӵ�COPY�б�
            //for (int count = 0; count < list.Count; count++)
            //{
            //    HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(list[count]);
            //}
            //string type = "2";
            //HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(type);
            ////Ȼ��copy�б�ŵ���������
            //HISFC.Components.Order.Classes.HistoryOrderClipboard.Copy();
            #endregion

            if (this.fpSpread1.ActiveSheet.RowCount <= 0) return;

            this.fpSpread1.Focus();

            ArrayList list = new ArrayList();

            //��ȡѡ���е�ҽ��ID
            for (int row = 0; row < this.fpSpread1.ActiveSheet.RowCount; row++)
            {
                for (int col = 0; col < this.fpSpread1.ActiveSheet.ColumnCount; col++)
                {
                    if (this.fpSpread1.ActiveSheet.IsSelected(row, col))
                    {
                        list.Add(this.fpSpread1.ActiveSheet.Cells[row, 0].Value.ToString());
                        break;
                    }
                }
            }

            if (list.Count <= 0) return;
            //����ӵ�COPY�б�
            for (int count = 0; count < list.Count; count++)
            {
                HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(list[count]);
            }
            string type = "2";
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Add(type);
            //Ȼ��copy�б�ŵ���������
            HISFC.Components.Order.Classes.HistoryOrderClipboard.Copy();
        }

        /// <summary>
        /// �Ҽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            int ActiveRowIndex = -1;

            if (e.Button == MouseButtons.Right)
            {
                this.contextMenu1.Items.Clear();
                FarPoint.Win.Spread.Model.CellRange c = fpSpread1.GetCellFromPixel(0, 0, e.X, e.Y);
                if (c.Row >= 0)
                {
                    //this.fpSpread1.ActiveSheet.ActiveRowIndex = c.Row;
                    this.fpSpread1.ActiveSheet.AddSelection(c.Row, 0, 1, 1);
                    ActiveRowIndex = c.Row;
                }
                else
                {
                    ActiveRowIndex = -1;
                }
                if (ActiveRowIndex < 0) 
                    return;

                ToolStripMenuItem mnuCopyOrder = new ToolStripMenuItem();
                mnuCopyOrder.Text = "����";
                mnuCopyOrder.Click += new EventHandler(mnuCopyOrder_Click);
                this.contextMenu1.Items.Add(mnuCopyOrder);

                this.contextMenu1.Show(this.fpSpread1, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// �˵����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyOrder_Click(object sender, EventArgs e)
        {
            this.CopyOrder();
        }

        #endregion
    }
}
