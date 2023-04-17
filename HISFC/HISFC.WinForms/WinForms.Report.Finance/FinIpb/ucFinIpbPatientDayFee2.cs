using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Finance.FinIpb
{
    /// <summary>
    /// ������˵������
    /// �������ˣ���
    /// ������ʱ�䣺��
    /// ���޸ļ�¼��
    ///   2009-8-24 xuc ���ӿ���ѡ�����Ҷ໼�߲�ѯ���� {EA2A7657-6A55-4582-8052-DC7F8A5A4795}
    /// 
    /// 
    /// </summary>
    public partial class ucFinIpbPatientDayFee2 : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbPatientDayFee2()
        {
            InitializeComponent();
        }


        #region ����
        /// <summary>
        /// ҵ���
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();

        sql sqlLogic = new sql();


        /// <summary>
        /// �Ƿ���ʾȫԺ����
        /// </summary>
        bool isShowAllInDeptPatient = false;
        /// <summary>
        /// �Ƿ���ʾȫԺ����
        /// </summary>
        public bool IsShowAllInDeptPatient
        {
            get
            {
                return isShowAllInDeptPatient;
            }
            set
            {
                isShowAllInDeptPatient = value;
            }
        }

        #endregion



        /// <summary>
        ///��ʼ����
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            base.OnDrawTree();

            this.tvLeft.Nodes.Clear();

            //����ѡ
            this.tvLeft.CheckBoxes = true;

            if (isShowAllInDeptPatient == false)
            {
                //��Ժ����
                FS.HISFC.Models.RADT.InStateEnumService inState = new FS.HISFC.Models.RADT.InStateEnumService();
                inState.ID = FS.HISFC.Models.Base.EnumInState.I.ToString();

                ArrayList emplList = managerIntegrate.QueryPatientBasic(base.employee.Dept.ID, inState);

                TreeNode parentTreeNode = new TreeNode("���ƻ���");
                parentTreeNode.Checked = false;
                parentTreeNode.Tag = "ROOT";
                tvLeft.Nodes.Add(parentTreeNode);
                foreach (FS.HISFC.Models.RADT.PatientInfo empl in emplList)
                {
                    TreeNode emplNode = new TreeNode();
                    emplNode.Tag = empl;
                    emplNode.Text = empl.Name;
                    parentTreeNode.Nodes.Add(emplNode);
                }

                parentTreeNode.ExpandAll();
                parentTreeNode.Checked = false;
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���ȫԺ�����б����Ե�......");
                Application.DoEvents();


                //ȫԺ�����б�
                //��Ժ����
                ArrayList emplList = managerIntegrate.QueryPatient(FS.HISFC.Models.Base.EnumInState.I);

                //�������б�
                Dictionary<string, TreeNode> deptDic = new Dictionary<string, TreeNode>();

                TreeNode parentTreeNode = new TreeNode("ȫԺ����");
                
                parentTreeNode.Tag = "ROOT";
                tvLeft.Nodes.Add(parentTreeNode);
                int index = 0;
                foreach (FS.HISFC.Models.RADT.PatientInfo empl in emplList)
                {
                    if (deptDic.ContainsKey(empl.PVisit.PatientLocation.Dept.ID))
                    {
                        TreeNode patient = new TreeNode();
                        patient.Tag = empl;
                        patient.Text = empl.Name + "��" + empl.PID.PatientNO.ToString() + "��";

                        patient.Checked = false;
                        deptDic[empl.PVisit.PatientLocation.Dept.ID].Nodes.Add(patient);
                    }
                    else
                    {
                        TreeNode dept = new TreeNode();
                        dept.ForeColor = Color.Blue;
                        dept.Tag = empl.PVisit.PatientLocation.Dept;
                        dept.Text = empl.PVisit.PatientLocation.Dept.Name + "��" + empl.PVisit.PatientLocation.Dept.ID.ToString() + "��";
                        
                        TreeNode patient = new TreeNode();
                        patient.Tag = empl;
                        patient.Text = empl.Name + "��" + empl.PID.PatientNO.ToString()+"��";
                        patient.Checked = false;
                        dept.Nodes.Add(patient);
                        deptDic.Add(empl.PVisit.PatientLocation.Dept.ID, dept);

                        dept.Checked = false;
                        parentTreeNode.Nodes.Add(dept);
                    }
                    index++;
                }
                parentTreeNode.ExpandAll();
                parentTreeNode.Checked = false;


                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            

            

            this.tvLeft.AfterSelect -= new TreeViewEventHandler(tvLeft_AfterSelect);
            this.tvLeft.AfterSelect += new TreeViewEventHandler(tvLeft_AfterSelect);
            this.tvLeft.AfterCheck -= new TreeViewEventHandler(tvLeft_AfterCheck);
            this.tvLeft.AfterCheck += new TreeViewEventHandler(tvLeft_AfterCheck);

            return 1;
        }
        /// <summary>
        /// ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                e.Node.Checked = !e.Node.Checked;
            }
        }
        /// <summary>
        /// ��ѡ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvLeft_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                bool isCheck = e.Node.Checked;
                this.SelectPatient(e.Node, isCheck);
            }
        }



        /// <summary>
        /// ��ѡ����
        /// </summary>
        /// <param name="treeNode"></param>
        private void SelectPatient(TreeNode treeNode, bool isCheck)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = isCheck;
                SelectPatient(node, isCheck);
            }
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            this.plRightBottom.Controls.Clear();

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            List<string> inpatientLine = new List<string>();
            List<string> inpatientLineTemp=new List<string> ();
            GetPatients(this.tvLeft.Nodes[0], inpatientLineTemp);

            // ��inpatientLineTemp�б�ת
            foreach (string patient in inpatientLineTemp)
            {
                inpatientLine.Insert(0,patient);
            }
            if (inpatientLine.Count <= 0)
            {
                MessageBox.Show("��ѡ����");
                return -1;
            }

            Panel p = new Panel();
            p.Size = new Size(0,0);
            p.Dock = DockStyle.Top;
            this.plRightBottom.Controls.Add(p);

            int beginX = p.Location.X; //��ʼX��
            int beginY = p.Location.Y; //��ʼY��
            int hight = 0; //��¼�߶�
            foreach (string InpatientNo in inpatientLine)
            {

                #region ��С����FP����
                FarPoint.Win.Spread.FpSpread spreadMinFee = new FarPoint.Win.Spread.FpSpread();
                FarPoint.Win.Spread.SheetView viewMinFee = new FarPoint.Win.Spread.SheetView();
                spreadMinFee.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] { viewMinFee });
                spreadMinFee.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
                spreadMinFee.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
                spreadMinFee.BackColor = Color.White;
                spreadMinFee.Size = new Size(720, 20);
                viewMinFee.RowHeaderColumnCount = 0;
                viewMinFee.Rows.Count = 0;
                viewMinFee.Columns.Count = 8;
                viewMinFee.ColumnHeader.DefaultStyle.BackColor = Color.White;
                viewMinFee.RowHeader.DefaultStyle.BackColor = Color.White;
                viewMinFee.SheetCornerStyle.BackColor = Color.White;
                viewMinFee.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);


                for (int i = 0; i < 4; i++)
                {
                    viewMinFee.Columns[i * 2].Label = "��Ŀ";
                    viewMinFee.Columns[i * 2].Width = 90;
                    viewMinFee.Columns[i * 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    viewMinFee.Columns[i * 2 + 1].Label = "���";
                    viewMinFee.Columns[i * 2 + 1].Width = 90;
                    viewMinFee.Columns[i * 2 + 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                }

                ArrayList MinFeeList = new ArrayList();
                MinFeeList = sqlLogic.GetMinFeeList(InpatientNo, this.dtpBeginTime.Value.ToString(), this.dtpEndTime.Value.ToString());

                int m = 0;
                int n = 0;
                Decimal ThisCost = 0;
                if (MinFeeList.Count > 0)
                {
                    viewMinFee.Rows.Add(0, 1);
                    foreach (FS.FrameWork.Models.NeuObject obj in MinFeeList)
                    {
                        if (m >= 4)
                        {
                            m = 0;
                            n++;
                            viewMinFee.Rows.Add(n, 1);
                        }
                        spreadMinFee.Size = new Size(spreadMinFee.Size.Width, (n+2)*20);
                        viewMinFee.Cells[n, m * 2].Text = obj.ID;
                        viewMinFee.Cells[n, m * 2 + 1].Text = obj.Memo;
                        ThisCost += System.Convert.ToDecimal(obj.Memo);
                        m++;
                    }
                }
                //spreadMinFee.Dock = DockStyle.Top;
                #endregion

                #region ��Ŀ��ϸFP����
                FarPoint.Win.Spread.FpSpread spreadItemDetail = new FarPoint.Win.Spread.FpSpread();
                FarPoint.Win.Spread.SheetView viewItemDetail = new FarPoint.Win.Spread.SheetView();
                spreadItemDetail.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] { viewItemDetail });
                spreadItemDetail.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
                spreadItemDetail.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
                spreadItemDetail.Size = new Size(730, 20);
                viewItemDetail.RowHeaderColumnCount = 0;
                viewItemDetail.Rows.Count = 0;
                viewItemDetail.Columns.Count = 12;
                viewItemDetail.ColumnHeader.DefaultStyle.BackColor = Color.White;
                viewItemDetail.RowHeader.DefaultStyle.BackColor = Color.White;
                viewItemDetail.SheetCornerStyle.BackColor = Color.White;
                viewItemDetail.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);

                for (int i = 0; i < 2; i++)
                {
                    viewItemDetail.Columns[i * 6].Label = "��Ŀ����";
                    viewItemDetail.Columns[i * 6].Width = 80;
                    viewItemDetail.Columns[i * 6 + 1].Label = "��Ŀ����";
                    viewItemDetail.Columns[i * 6 + 1].Width = 110;
                    viewItemDetail.Columns[i * 6 + 2].Label = "���";
                    viewItemDetail.Columns[i * 6 + 2].Width = 60;
                    viewItemDetail.Columns[i * 6 + 3].Label = "��λ";
                    viewItemDetail.Columns[i * 6 + 3].Width = 15;
                    viewItemDetail.Columns[i * 6 + 4].Label = "����";
                    viewItemDetail.Columns[i * 6 + 4].Width = 40;
                    viewItemDetail.Columns[i * 6 + 5].Label = "���";
                    viewItemDetail.Columns[i * 6 + 5].Width = 60;
                }

                ArrayList ItemDetailList = new ArrayList();
                ItemDetailList = sqlLogic.GetItemList(InpatientNo, this.dtpBeginTime.Value.ToString(), this.dtpEndTime.Value.ToString());

                int m1 = 0;
                int n1 = 0;
                if (ItemDetailList.Count > 0)
                {
                    viewItemDetail.Rows.Add(0, 1);
                    foreach (FS.HISFC.Models.Fee.Item.Undrug item in ItemDetailList)
                    {
                        if (m1 >= 2)
                        {
                            m1 = 0;
                            n1++;
                            viewItemDetail.Rows.Add(n1, 1);
                        }
                        spreadItemDetail.Size = new Size(spreadItemDetail.Size.Width, (n1 + 2) * 20);
                        viewItemDetail.Cells[n1, m1 * 6].Text = item.ID; //��Ŀ����
                        viewItemDetail.Cells[n1, m1 * 6 + 1].Text = item.Name; //��Ŀ����
                        viewItemDetail.Cells[n1, m1 * 6 + 2].Text = item.Specs; //���
                        viewItemDetail.Cells[n1, m1 * 6 + 3].Text = item.PriceUnit;//��λ
                        viewItemDetail.Cells[n1, m1 * 6 + 4].Text = item.Qty.ToString();//����        
                        viewItemDetail.Cells[n1, m1 * 6 + 5].Text = item.Price.ToString(); //���

                        m1++;
                    }
                }
                #endregion

                if (MinFeeList.Count > 0)
                {
                    if (this.cbOption.SelectedIndex == 0)
                    {
                        //��ʾסԺ������Ϣ
                        ucMinFeeBegin cellBegin = new ucMinFeeBegin(InpatientNo, this.dtpBeginTime.Value, this.dtpEndTime.Value);
                        cellBegin.Location = new Point(beginX,beginY+hight);
                        this.plRightBottom.Controls.Add(cellBegin);
                        hight += cellBegin.Size.Height;

                        //��ʾסԺ���߷���
                        spreadMinFee.Location = new Point(beginX,beginY+hight);
                        this.plRightBottom.Controls.Add(spreadMinFee);
                        hight += spreadMinFee.Size.Height;
                        ucMinFeeEnd cellEnd = new ucMinFeeEnd(InpatientNo, this.dtpBeginTime.Value, this.dtpEndTime.Value, ThisCost);
                        cellEnd.Location = new Point(beginX, beginY+hight);
                        this.plRightBottom.Controls.Add(cellEnd);
                        hight += cellEnd.Size.Height;
                        
                    }
                    else if (this.cbOption.SelectedIndex == 1)
                    {
                        //��ʾסԺ������Ϣ
                        ucDetailFeeBegin cellBegin = new ucDetailFeeBegin(InpatientNo, this.dtpBeginTime.Value, this.dtpEndTime.Value);
                        cellBegin.Location = new Point(beginX, beginY + hight);
                        this.plRightBottom.Controls.Add(cellBegin);
                        hight += cellBegin.Size.Height;

                        //��ʾסԺ���߷���
                        spreadItemDetail.Location = new Point(beginX, beginY + hight);
                        this.plRightBottom.Controls.Add(spreadItemDetail);
                        hight += spreadItemDetail.Size.Height;
                        ucDetailFeeEnd cellEnd = new ucDetailFeeEnd(InpatientNo, this.dtpBeginTime.Value, this.dtpEndTime.Value, ThisCost);
                        cellEnd.Location = new Point(beginX, beginY + hight);
                        this.plRightBottom.Controls.Add(cellEnd);
                        hight += cellEnd.Size.Height;
                    }
                    else
                    {
                        MessageBox.Show("��ѡ���ѯ������");
                        this.plRightBottom.Controls.Clear();
                        return -1;
                    }                     
                }
            }

            return 1;
        }

        /// <summary>
        /// �ݹ��ȡѡ��Ļ���
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="inpatientLine"></param>
        void GetPatients(TreeNode nodes, List<string> inpatientLine)
        {
            foreach (TreeNode node in nodes.Nodes)
            {
                if (node.Checked && node.Tag is FS.HISFC.Models.RADT.PatientInfo)
                {
                    FS.HISFC.Models.RADT.PatientInfo patient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                    inpatientLine.Add(patient.ID);
                }
                GetPatients(node, inpatientLine);
            }
        }
    }
}

