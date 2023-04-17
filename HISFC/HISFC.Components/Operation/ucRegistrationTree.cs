using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FS.HISFC.Models.Operation;
using FS.HISFC.BizLogic.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: �����Ǽǵ��б����οؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucRegistrationTree : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public ucRegistrationTree()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
                this.Init();
        }

        public ucRegistrationTree(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            if (!Environment.DesignMode)
                this.Init();
        }



        #region �ֶ�
        private EnumListType listType = EnumListType.Operation;
        private bool showCanceled = true;

        //{80D89813-7B64-4acf-A2CD-55BFD9F1E7C6}
        private FS.HISFC.BizProcess.Integrate.Manager deptStatMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region ����
        /// <summary>
        /// �б�����
        /// </summary>
        public EnumListType ListType
        {
            get
            {
                return this.listType;
            }
            set
            {
                this.listType = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�Ѿ�ȡ��������
        /// </summary>
        public bool ShowCanceled
        {
            get
            {
                return this.showCanceled;
            }
            set
            {
                this.showCanceled = value;
                if (value == false)
                {
                    if (this.Nodes.Count > 3)
                    {
                        this.Nodes[3].Remove();
                    }
                }
            }
        }
        #endregion

        #region ����
        //{8DA8B1D6-DDD6-4329-B661-F4BDAE45DB66}
        public void Init()
        {
            this.Nodes.Clear();
            //��ʾδ�Ǽ��������ѵǼǡ���ȡ������
            ////{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
            string listName = "����";
            if (listType == EnumListType.Anaesthesia)
            {
                listName = "����";
            }
            ////{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
            //TreeNode root = new TreeNode("δ�Ǽ�����");
            TreeNode root = new TreeNode("�Ѱ���" + listName);
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            root.Tag = "NO_Register";
            this.Nodes.Add(root);


            //TreeNode root = new TreeNode("δ�Ǽ�����");
            root = new TreeNode("�����δ�շ�" + listName);
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            root.Tag = "YES_NO_Register";
            this.Nodes.Add(root);

            //root = new TreeNode("�ѵǼ�����");
            root = new TreeNode("���շ�" + listName);
            root.SelectedImageIndex = 22;
            root.ImageIndex = 22;
            root.Tag = "Register";
            this.Nodes.Add(root);
            

            //{D31ECFF1-2BC5-4770-A1BF-1093B5B9B315}
            if (this.listType == EnumListType.Operation)
            {
                root = new TreeNode("����������");
                root.SelectedImageIndex = 22;
                root.ImageIndex = 22;
                root.Tag = "Cancel";
                this.Nodes.Add(root);
            }
        }

        /// <summary>
        /// ˢ�������Ǽ��б�
        /// </summary>
        public void RefreshList(DateTime begin, DateTime end)
        {

            this.RefreshListNotReg(begin, end);

           //this.RefreshListReged(begin, end);

            RefreshListYesNotReg(begin, end); //add by zhy

            //RefreshListRegedbyapply(begin, end);
            RefreshListReged(begin, end);
            
            //{8DA8B1D6-DDD6-4329-B661-F4BDAE45DB66}
            //if(this.showCanceled)
            if (this.Nodes.Count > 3)
            {
                this.RefreshListCanceled(begin, end);
            }

        }

        /// <summary>
        /// ˢ��δ�Ǽ������б�
        /// </summary>
        private void RefreshListNotReg(DateTime begin, DateTime end)
        {
            this.Nodes[0].Nodes.Clear();

            #region δ�Ǽ�
            ArrayList al;

            if (this.listType == EnumListType.Operation)
                al = Environment.OperationManager.GetOpsAppList(Environment.OperatorDeptID, begin, end, true);
            else 
            {

                //{80D89813-7B64-4acf-A2CD-55BFD9F1E7C6}

                ArrayList alTemp = Environment.OperationManager.GetOpsAppList(begin, end, "1");
                al = new ArrayList();

                foreach (OperationAppllication apply in alTemp)
                {

                    if (apply.ExecStatus != "3"&&apply.ExecStatus != "4") //û�а��ŵ�������û�еǼǵĲ���ʾ
                    {
                        continue;
                    }
                    // ���������������ҹ�ϵ�����й��ˣ�ֻ�ܹ��˳������������Ӧ�������ҵ�����
                    if (!apply.IsAnesth)
                    {
                        continue;
                    }

                    FS.HISFC.Models.Operation.AnaeRecord ananRecord = Environment.AnaeManager.GetAnaeRecord(apply.ID);
                    if (ananRecord != null && ananRecord.AnaeDate != DateTime.MinValue)
                    {
                        continue;
                    }

                    ArrayList alAnesDepts = this.deptStatMgr.LoadChildren("10", apply.ExeDept.ID, 1);
                    if (alAnesDepts == null)
                    {
                        MessageBox.Show("���ҿ��Ҷ�Ӧ��ϵʱ����" + this.deptStatMgr.Err);
                        return;
                    }
                    if (alAnesDepts.Count == 0)
                    {
                        //FS.HISFC.BizProcess.Integrate.Manager depMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                        //apply.ExeDept.Name = depMgr.GetDepartment(apply.ExeDept.ID).Name;
                        //MessageBox.Show("�������ң���" + apply.ExeDept.Name + "���Ҳ����������ҵĶ�Ӧ��ϵ�����ڿ��ҽṹ����ά����");
                        //return ;
                        continue;
                    }
                    foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alAnesDepts)
                    {
                        #region {2F58330D-0BEC-4a68-AE06-6C2868CFE545}
                        //{E4C275E8-6E12-4a42-A60A-0EB9A8CB52BD}
                        if (deptStat.DeptCode == (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                        {
                            al.Add(apply);
                        }
                        //if (deptStat.PardepCode == (this.deptStatMgr.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                        //{
                        //    this.ucAnaesthesiaSpread1.AddOperationApplication(apply);
                        //    break;
                        //}
                        #endregion
                    }
                }



            }

            if (al != null)
            {
                foreach (OperationAppllication apply in al)
                {

                    if (apply.ExecStatus != "3" && apply.ExecStatus != "4") //û�а��ŵ�������û�еǼǵĲ���ʾ
                    {
                        continue;
                    }
                    if (apply.ExecStatus == "6") //��������δ�շѵ�������ʾ
                    {
                        continue;
                    }

                    /// [�����޸�: �����Ǽǵ��б�����ʾ��ʿ���ڻ�ʿվ]<br></br>
                    /// [�� �� ��: �Ż�Դ]<br></br>
                    /// [����ʱ��: 2012-11-29]<br></br>
                    
                    TreeNode node = new TreeNode();
                    node.Text = apply.PatientInfo.Name + " " + apply.PatientInfo.PVisit.PatientLocation.Bed.Name + "��";
                    //node.Text = string.Concat("[", Environment.GetDept(apply.PatientInfo.PVisit.PatientLocation.Dept.ID),
                    //    "] ", apply.PatientInfo.Name);
                    node.Text = string.Concat("[", Environment.GetDept(apply.PatientInfo.PVisit.PatientLocation.NurseCell.ID),
                        "]", apply.PatientInfo.Name);
                   
                    if (apply.OperationInfos.Count > 0)
                        node.Text = node.Text + "   " + (apply.OperationInfos[0] as OperationInfo).OperationItem.Name;
                    node.Tag = apply;
                    #region {ED78E665-913F-44c1-A807-6023F32E3AC1}
                    if (!apply.IsChange)
                    {
                        node.ForeColor = Color.Red;
                    }
                    #endregion
                    node.SelectedImageIndex = 21;
                    node.ImageIndex = 20;
                    this.Nodes[0].Nodes.Add(node);
                }
            }
            #endregion

            this.Nodes[0].Expand();
        }



        /// <summary>
        /// ˢ���ѵǼ�������¼
        /// </summary>
        private void RefreshListRegedbyapply(DateTime begin, DateTime end)
        {
            this.Nodes[2].Nodes.Clear();

            #region �ѵǼ�

            ArrayList al;
            if (this.listType == EnumListType.Operation)
            {
               // al = Environment.RecordManager.GetOperatorRecords(Environment.OperatorDeptID, begin, end);
                al = Environment.OperationManager.GetALLOpsApp( begin.ToString(), end.ToString());
                if (al != null)
                {
                    //foreach (OperationRecord record in al)
                    //{
                    //    TreeNode node = new TreeNode();
                    //    node.Text = "[" + Environment.GetDept(record.OperationAppllication.PatientInfo.PVisit.PatientLocation.Dept.ID) +
                    //        "] " + record.OperationAppllication.PatientInfo.Name;
                    //    #region {ED78E665-913F-44c1-A807-6023F32E3AC1}
                    //    if (!record.IsCharged)
                    //    {
                    //        node.ForeColor = Color.Red;
                    //    }
                    //    #endregion
                    //    if (record.OperationAppllication.OperationInfos.Count > 0)
                    //        node.Text = node.Text + "   " + record.OperationAppllication.OperationInfos[0].OperationItem.Name;
                    //    node.Tag = record;
                    //    node.SelectedImageIndex = 21;
                    //    node.ImageIndex = 20;
                    //    this.Nodes[1].Nodes.Add(node);
                    //}

                    if (al != null)
                    {
                        foreach (OperationAppllication apply in al)
                        {
                            if (apply.ExecStatus != "4")
                            {
                                continue;
                            }
                            TreeNode node = new TreeNode();
                            node.Text = string.Concat("[", Environment.GetDept(apply.PatientInfo.PVisit.PatientLocation.Dept.ID),
                                "] ", apply.PatientInfo.Name);
                            if (apply.OperationInfos.Count > 0)
                                node.Text = node.Text + "   " + (apply.OperationInfos[0] as OperationInfo).OperationItem.Name;
                            node.Tag = apply;
                            #region {ED78E665-913F-44c1-A807-6023F32E3AC1}
                            //if (!apply.IsChange)
                            //{
                            //    node.ForeColor = Color.Red;
                            //}
                            #endregion
                            node.SelectedImageIndex = 21;
                            node.ImageIndex = 20;
                            this.Nodes[2].Nodes.Add(node);
                        }
                    }
                }
            }
            

            #endregion

            this.Nodes[2].Expand();
        }


        /// <summary>
        /// ˢ���ѵǼ�������¼
        /// </summary>
        private void RefreshListReged(DateTime begin, DateTime end)
        {
            this.Nodes[1].Nodes.Clear();
            this.Nodes[2].Nodes.Clear();

            #region �ѵǼ�

            ArrayList al;
            if (this.listType == EnumListType.Operation)
            {
                al = Environment.RecordManager.GetOperatorRecords(Environment.OperatorDeptID, begin, end);
                if (al != null && al.Count != 0)
                {
                    foreach (OperationRecord record in al)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = "[" + Environment.GetDept(record.OperationAppllication.PatientInfo.PVisit.PatientLocation.Dept.ID) +
                            "] " + record.OperationAppllication.PatientInfo.Name;
                        #region {ED78E665-913F-44c1-A807-6023F32E3AC1}
                        if (!record.OperationAppllication.IsChange)
                        {
                            node.ForeColor = Color.Red;
                            if (record.OperationAppllication.OperationInfos.Count > 0)
                                node.Text = node.Text + "   " + record.OperationAppllication.OperationInfos[0].OperationItem.Name;
                            node.Tag = record;
                            node.SelectedImageIndex = 21;
                            node.ImageIndex = 20;
                            this.Nodes[1].Nodes.Add(node);
                        }
                        else
                        {
                            if (record.OperationAppllication.OperationInfos.Count > 0)
                                node.Text = node.Text + "   " + record.OperationAppllication.OperationInfos[0].OperationItem.Name;
                            node.Tag = record;
                            node.SelectedImageIndex = 21;
                            node.ImageIndex = 20;
                            this.Nodes[2].Nodes.Add(node);
                        }
                        #endregion
                      
                    }
                }
                else
                {
                    //���ߵǼ����̵Ĳ�ѯ�������״̬�Լ���Ӧ���շ�״̬
                    al = Environment.OperationManager.GetOpsAppList(begin, end, "1");
                    {
                        this.Nodes[2].Nodes.Clear();
                        if (al != null && al.Count > 0)
                        {
                            foreach (OperationAppllication operationInfo in al)
                            {
                                if (operationInfo.ExecStatus != "4" || !operationInfo.IsChange)
                                {
                                    continue;
                                }

                                TreeNode node = new TreeNode();
                                node.Text = "[" + Environment.GetDept(operationInfo.PatientInfo.PVisit.PatientLocation.Dept.ID) +
                                    "] " + operationInfo.PatientInfo.Name;
                                if (operationInfo.OperationInfos.Count > 0)
                                    node.Text = node.Text + "   " + operationInfo.OperationInfos[0].OperationItem.Name;
                                node.Tag = operationInfo;
                                node.SelectedImageIndex = 21;
                                node.ImageIndex = 20;
                                this.Nodes[2].Nodes.Add(node);
                                this.Nodes[2].ExpandAll();
                            }
                        }
                    }
                }
            }
            else
            {
                al = Environment.AnaeManager.GetAnaeRecords(Environment.OperatorDeptID, begin, end);

                this.Nodes[1].Nodes.Clear();
                this.Nodes[2].Nodes.Clear();


                if (al != null)
                {



                    foreach (FS.HISFC.Models.Operation.AnaeRecord record in al)
                    {

                        // //{80D89813-7B64-4acf-A2CD-55BFD9F1E7C6}���������������ҹ�ϵ�����й��ˣ�ֻ�ܹ��˳������������Ӧ�������ҵ�����

                        ArrayList alAnesDepts = this.deptStatMgr.LoadChildren("10", record.OperationApplication.ExeDept.ID, 1);
                        if (alAnesDepts == null)
                        {
                            MessageBox.Show("���ҿ��Ҷ�Ӧ��ϵʱ����" + this.deptStatMgr.Err);
                            return;
                        }
                        if (alAnesDepts.Count == 0)
                        {
                            continue;
                        }
                        foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alAnesDepts)
                        {

                            //{E4C275E8-6E12-4a42-A60A-0EB9A8CB52BD}
                            if (deptStat.DeptCode == (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                            {
                                if (record.IsCharged)
                                {
                                    TreeNode node = new TreeNode();
                                    node.Text = "[" + Environment.GetDept(record.OperationApplication.PatientInfo.PVisit.PatientLocation.Dept.ID) +
                                        "] " + record.OperationApplication.PatientInfo.Name;

                                    if (record.OperationApplication.OperationInfos.Count > 0)
                                        node.Text = node.Text + "   " + record.OperationApplication.OperationInfos[0].OperationItem.Name;
                                    node.Tag = record;
                                    node.SelectedImageIndex = 21;
                                    node.ImageIndex = 20;
                                    this.Nodes[2].Nodes.Add(node);
                                }
                                else
                                {
                                    TreeNode node = new TreeNode();
                                    node.Text = "[" + Environment.GetDept(record.OperationApplication.PatientInfo.PVisit.PatientLocation.Dept.ID) +
                                        "] " + record.OperationApplication.PatientInfo.Name;

                                    if (record.OperationApplication.OperationInfos.Count > 0)
                                        node.Text = node.Text + "   " + record.OperationApplication.OperationInfos[0].OperationItem.Name;
                                    node.Tag = record;
                                    node.SelectedImageIndex = 21;
                                    node.ImageIndex = 20;
                                    this.Nodes[1].Nodes.Add(node);
                                }
                            }
                        }


                    }
                }
            }

            #endregion

            this.Nodes[1].Expand();
            this.Nodes[2].Expand();
        }


        /// <summary>
        /// ˢ����������δ�շѻ��� add by zhy
        /// </summary>
        private void RefreshListYesNotReg(DateTime begin, DateTime end)
        {
            this.Nodes[1].Nodes.Clear();

            #region ��������δ�շѣ�ExecStatus״̬Ϊ6
            ArrayList al;

            if (this.listType == EnumListType.Operation)
                al = Environment.OperationManager.GetOpsAppList(Environment.OperatorDeptID, begin, end, true);
            else
            {

                //{80D89813-7B64-4acf-A2CD-55BFD9F1E7C6}

                ArrayList alTemp = Environment.OperationManager.GetOpsAppList(begin, end, "1");
                al = new ArrayList();

                foreach (OperationAppllication apply in alTemp)
                {

                    if (apply.ExecStatus != "6" ) //û�а��ŵ�������û�еǼǵĲ���ʾ
                    {
                        continue;
                    }
                    // ���������������ҹ�ϵ�����й��ˣ�ֻ�ܹ��˳������������Ӧ�������ҵ�����

                    ArrayList alAnesDepts = this.deptStatMgr.LoadChildren("10", apply.ExeDept.ID, 1);
                    if (alAnesDepts == null)
                    {
                        MessageBox.Show("���ҿ��Ҷ�Ӧ��ϵʱ����" + this.deptStatMgr.Err);
                        return;
                    }
                    if (alAnesDepts.Count == 0)
                    {
                        //FS.HISFC.BizProcess.Integrate.Manager depMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                        //apply.ExeDept.Name = depMgr.GetDepartment(apply.ExeDept.ID).Name;
                        //MessageBox.Show("�������ң���" + apply.ExeDept.Name + "���Ҳ����������ҵĶ�Ӧ��ϵ�����ڿ��ҽṹ����ά����");
                        //return ;
                        continue;
                    }
                    foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alAnesDepts)
                    {
                        #region {2F58330D-0BEC-4a68-AE06-6C2868CFE545}
                        //{E4C275E8-6E12-4a42-A60A-0EB9A8CB52BD}
                        if (deptStat.DeptCode == (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                        {
                            al.Add(apply);
                        }
                        //if (deptStat.PardepCode == (this.deptStatMgr.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                        //{
                        //    this.ucAnaesthesiaSpread1.AddOperationApplication(apply);
                        //    break;
                        //}
                        #endregion
                    }
                }



            }

            if (al != null)
            {
                foreach (OperationAppllication apply in al)
                {
                    if (apply.ExecStatus != "6" ) // ������������δ�շѵľͲ���ʾ
                    {
                        continue;
                    }

                    /// [�����޸�: �����Ǽǵ��б�����ʾ��ʿ���ڻ�ʿվ]<br></br>
                    /// [�� �� ��: �Ż�Դ]<br></br>
                    /// [����ʱ��: 2012-11-29]<br></br>

                    TreeNode node = new TreeNode();
                    node.Text = apply.PatientInfo.Name + " " + apply.PatientInfo.PVisit.PatientLocation.Bed.Name + "��";
                    //node.Text = string.Concat("[", Environment.GetDept(apply.PatientInfo.PVisit.PatientLocation.Dept.ID),
                    //    "] ", apply.PatientInfo.Name);
                    node.Text = string.Concat("[", Environment.GetDept(apply.PatientInfo.PVisit.PatientLocation.NurseCell.ID),
                        "]", apply.PatientInfo.Name);

                    if (apply.OperationInfos.Count > 0)
                        node.Text = node.Text + "   " + (apply.OperationInfos[0] as OperationInfo).OperationItem.Name;
                    node.Tag = apply;
                    #region {ED78E665-913F-44c1-A807-6023F32E3AC1}
                    if (!apply.IsChange)
                    {
                        node.ForeColor = Color.Red;
                    }
                    #endregion
                    node.SelectedImageIndex = 21;
                    node.ImageIndex = 20;
                    this.Nodes[1].Nodes.Add(node);
                }
            }
            #endregion

            this.Nodes[1].Expand();
        }



        /// <summary>
        /// ˢ�������������뵥
        /// </summary>
        private void RefreshListCanceled(DateTime begin, DateTime end)
        {
            this.Nodes[3].Nodes.Clear();

            ArrayList al;

            if (this.listType == EnumListType.Operation)
                //al = Environment.OperationManager.GetOpsAppList(Environment.OperatorDeptID, begin, end, "0");
                al = Environment.OperationManager.GetOpsCancelRecord(Environment.OperatorDeptID, begin, end);
            else
                al = Environment.OperationManager.GetOpsAppList(begin, end, "0");

            if (al != null)
            {
                foreach (OperationAppllication apply in al)
                {
                    /// [�����޸�: �����Ǽǵ��б�����ʾ��ʿ���ڻ�ʿվ]<br></br>
                    /// [�� �� ��: �Ż�Դ]<br></br>
                    /// [����ʱ��: 2012-11-29]<br></br>

                    TreeNode node = new TreeNode();
                    node.Text = apply.PatientInfo.Name + " " + apply.PatientInfo.PVisit.PatientLocation.Bed.Name + "��";
                    node.Text = string.Concat("[", Environment.GetDept(apply.PatientInfo.PVisit.PatientLocation.NurseCell.ID),
                        "]", apply.PatientInfo.Name);
                 //   node.Text = "[" + Environment.GetDept(apply.PatientInfo.PVisit.PatientLocation.Dept.ID) +
                 //       "] " + apply.PatientInfo.Name;
                    //{E526A9B6-48BC-4ffc-A1F8-069276E7E738}
                    if (apply.OperationInfos.Count > 0)
                        node.Text = node.Text + "   " + apply.OperationInfos[0].OperationItem.Name + ",ȡ����["+apply.User.ID+"]";
                     
                    node.Tag = apply;
                    node.ForeColor = Color.Red;
                    node.SelectedImageIndex = 21;
                    node.ImageIndex = 20;
                    this.Nodes[3].Nodes.Add(node);
                }
            }
        }
        #endregion


        /// <summary>
        /// �б�����
        /// </summary>
        public enum EnumListType
        {
            /// <summary>
            /// ����
            /// </summary>
            Operation,
            /// <summary>
            /// ����
            /// </summary>
            Anaesthesia
        }
    }
}
