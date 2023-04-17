using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse
{
    public partial class ucQueryReg : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ����

        /// <summary>
        /// ���ﻼ��ˢ��Ƶ�ʣ�Ĭ�ϲ�ˢ��
        /// </summary>
        private string frequence = "None";
        /// <summary>
        /// �Һź���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// ���ﺯ��
        /// </summary>
        FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        /// <summary>
        /// ���Һ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
     

        ArrayList alDept = new ArrayList();
        ArrayList alDoct = new ArrayList();

        #endregion

        public ucQueryReg()
        {
            InitializeComponent();
            this.Init();
        }

        #region ��ʼ

        /// <summary>
        /// ��ȫ��ʼ��
        /// </summary>
        private void Init()
        {
            if (this.InitTreeView1() == -1)
            {
                this.FindForm().Close();
            }
        }
        /// <summary>
        /// ��ʼ����(������,�ѷ���)
        /// </summary>
        private int InitTreeView1()
        {
            this.neuTreeView1.Nodes.Clear();

            TreeNode root = new TreeNode("�������");
            root.Tag = null;
            this.neuTreeView1.Nodes.Add(root);

            TreeNode node1 = new TreeNode("δ���ﻼ��");
            node1.Tag = "1";
            root.Nodes.Add(node1);

            TreeNode node2 = new TreeNode("�ѿ��ﻼ��");
            node2.Tag = "2";
            root.Nodes.Add(node2);
            this.neuTreeView1.ExpandAll();

            //���ҽ���б�
            FS.HISFC.BizProcess.Integrate.Manager personMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.BizProcess.Integrate.Manager depMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Base.Employee ps = new FS.HISFC.Models.Base.Employee();
            FS.HISFC.Models.Base.Department deptinfo = new FS.HISFC.Models.Base.Department();

            //��ȡ����Ŀ����б�
            //alDept = depMgr.QueryDepartment(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            alDept = depMgr.QueryDepartment(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            if (alDept == null || alDept.Count <= 0)
            {
                MessageBox.Show("����Աû��ά������վ�����߻���վû��ά����Ӧ�ķ������!", "��ʼ������ȡ�����б����!");
                return -1;
            }

            #region ��ȡ������ҵ�ҽ���б�
            //foreach (FS.HISFC.Models.Base.Department dept in alDept)
            //{
            //    //����
            //    ArrayList altmp = personMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, dept.ID);
            //    if (altmp != null && altmp.Count > 0)
            //    {
            //        foreach (FS.HISFC.Models.Base.Employee psinfo in altmp)
            //        {
            //            alDoct.Add(psinfo);
            //        }
            //    }
            //}
            ////����
            //if (alDoct == null || alDoct.Count < 0)
            //{
            //    return -1;
            //}

            //foreach (FS.HISFC.Models.Base.Employee psinfo in alDoct)
            //{
            //    TreeNode node = new TreeNode();
            //    node.Text = psinfo.Name;
            //    node.Tag = psinfo;
            //    //node.ImageIndex = 26;
            //    //node.SelectedImageIndex = 35;

            //    node2.Nodes.Add(node);

            //}
            #endregion

            return 0;
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ˢ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            return this.toolBarService;
        }        

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "ˢ��":
                    this.RefreshPatient();
                    break;
                default: break;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// �������ﻼ��
        /// </summary>
        /// <param name="state"></param>
        /// <param name="doct"></param>
        private void QueryAlready(string doct)
        {
            //����ҽ��
            this.lvPatient.Items.Clear();
            this.neuLabel3.Text = "�ѿ��ﻼ��";
            //
            ArrayList al = assignMgr.QueryOrder(assignMgr.GetDateTimeFromSysDateTime().Date,
                                assignMgr.GetDateTimeFromSysDateTime().Date.AddDays(1), doct);
            if (al == null || al.Count <= 0) return;

            //
            foreach (FS.HISFC.Models.Registration.Register reginfo in al)
            {
                //----------�Ѳ����ڱ��ƵĹ��˵�---------------------------------------------------
                FS.FrameWork.Public.ObjectHelper helpMgr = new FS.FrameWork.Public.ObjectHelper();
                helpMgr.ArrayObject = alDept;
                if (helpMgr.GetObjectFromID(reginfo.SeeDoct.Dept.ID) == null)
                {
                    continue;
                }
                //------------------end------------------------------------------------------------


                if (doct == "ALL")
                {
                    this.AddWaitDetail(reginfo);
                }
                else if (doct == reginfo.SeeDoct.ID)
                {
                    this.AddWaitDetail(reginfo);
                }
            }
        }

        /// <summary>
        /// ����δ�ﻼ��
        /// </summary>
        private void QueryWait(string doct)
        {
            this.lvPatient.Items.Clear();
            this.neuLabel3.Text = "δ���ﻼ��";

            //ArrayList pstemp = regMgr.QueryNoTriagebyDept(assignMgr.GetDateTimeFromSysDateTime().Date/* regMgr.GetDateTimeFromSysDateTime().Date*/, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            ArrayList pstemp = this.regMgr.QueryNoTriagebyDeptUnSee(assignMgr.GetDateTimeFromSysDateTime().Date/* regMgr.GetDateTimeFromSysDateTime().Date*/, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            if (pstemp != null && pstemp.Count > 0)
            {
                foreach (FS.HISFC.Models.Registration.Register reginfo in pstemp)
                {
                    if (doct == "ALL")
                    {
                        this.AddWaitDetail(reginfo);
                    }
                    else if (doct == reginfo.DoctorInfo.Templet.Doct.ID)
                    {
                        this.AddWaitDetail(reginfo);
                    }
                }
            }
        }

        /// <summary>
        /// �Һ�ʵ�帳ֵ����
        /// </summary>
        /// <param name="reginfo"></param>
        private void AddWaitDetail(FS.HISFC.Models.Registration.Register reginfo)
        {
            if (reginfo == null || reginfo.PID.CardNO == null)
            {
                return;
            }

            ListViewItem item = new ListViewItem();
            item.SubItems[0].Text = reginfo.PID.CardNO;

            item.SubItems.Add(reginfo.Name);
            item.SubItems.Add(reginfo.Sex.Name);
            item.SubItems.Add(reginfo.OrderNO.ToString());
            item.SubItems.Add(reginfo.DoctorInfo.Templet.Dept.Name);
            item.SubItems.Add(reginfo.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss"));
            //����ҽ��
            //if (reginfo.DoctorInfo.Templet.Doct.ID != null && reginfo.DoctorInfo.Templet.Doct.ID != "")
            //{
                //item.SubItems.Add(reginfo.DoctorInfo.Templet.Doct.Name);
            if (reginfo.SeeDoct.ID == null || reginfo.SeeDoct.ID == "")
            {
                item.SubItems.Add("");
            }
            else
            {
                item.SubItems.Add(this.deptMgr.GetEmployeeInfo(reginfo.SeeDoct.ID).Name);
            }


            if (reginfo.SeeDoct.ID == null || reginfo.SeeDoct.ID == "")
            {
                item.SubItems.Add("");
            }
            else
            {
                string deptname = this.deptMgr.GetDepartment(reginfo.SeeDoct.Dept.ID).Name;
                if (deptname != null)
                {
                    item.SubItems.Add(deptname);
                }
            }
            
            item.Tag = reginfo;
            this.lvPatient.Items.Add(item);
        }

        /// <summary>
        /// ҽ����Ϣ����
        /// </summary>
        /// <param name="orderinfo"></param>
        private void AddAlreadyDetail(FS.HISFC.Models.Order.OutPatient.Order orderinfo)
        {
            //�ܷ����ҽ��,ʱ���ѯclinic_code
        }

        /// <summary>
        /// ˢ�»����б�
        /// </summary>
        private void RefreshPatient()
        {
            try
            {
                TreeNode select = this.neuTreeView1.SelectedNode;
                if (select == null)
                {
                    return;
                }
                //û��ѡ��
                if (select.Tag == null)
                {
                    return;
                }
                //δ���ﻼ��
                if (select.Tag.ToString() == "1")
                {
                    this.QueryWait("ALL");
                }
                //�ѿ��ﻼ��
                else if (select.Tag.ToString() == "2")
                {
                    this.QueryAlready("ALL");
                }
                else if (select.Tag.GetType() == typeof(FS.HISFC.Models.Base.Employee))
                {
                    FS.HISFC.Models.Base.Employee ps = (FS.HISFC.Models.Base.Employee)select.Tag;
                    this.QueryAlready(ps.ID);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// ����RadioButton��
        /// </summary>
        private void SetRadioFalse()
        {
            this.rbNo.Checked = false;
            this.rb10.Checked = false;
            this.rb30.Checked = false;
            this.rb60.Checked = false;
        }

        #endregion

        #region ����

        private string AvoidNull(string str)
        {
            if (str == null)
            {
                str = "";
            }
            return str;
        }

        #endregion

        private void txtCard_KeyDown(object sender, KeyEventArgs e)
        {
            this.lvPatient.Items.Clear();
            if (e.KeyData == Keys.Enter)
            {
                //�Ȳ��ҹҺű�
                string cardNo = this.txtCard.Text.PadLeft(10, '0');
                ArrayList alReg = this.regMgr.Query(cardNo, this.assignMgr.GetDateTimeFromSysDateTime().Date);
                if (alReg == null || alReg.Count == 0)
                {
                    MessageBox.Show("û�иû��ߵĹҺ���Ϣ");
                    return;
                }
                if (alReg != null || alReg.Count > 0)
                {
                    foreach (FS.HISFC.Models.Registration.Register reginfo in alReg)
                    {
                        if (reginfo.IsSee)
                        {
                            continue;
                        }
                        else
                        {
                            this.AddWaitDetail(reginfo);
                        }
                    }
                }
               
                //Ȼ�����������
                alReg = this.assignMgr.QueryOrder(cardNo, this.assignMgr.GetDateTimeFromSysDateTime().Date,
                                            this.assignMgr.GetDateTimeFromSysDateTime().Date.AddDays(1));
                if (alReg != null || alReg.Count > 0)
                {
                    foreach (FS.HISFC.Models.Registration.Register reginfo in alReg)
                    {
                        this.AddWaitDetail(reginfo);
                    }
                }
            }
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbNo.Checked)
            {
                RefreshFrequence rf = new RefreshFrequence();
                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/profile/patientQuery.xml"), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "no";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Enabled = false;
            }
        }

        private void rb10_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb10.Checked)
            {
                RefreshFrequence rf = new RefreshFrequence();
                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/profile/patientQuery.xml"), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "10";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 10000;
                this.timer1.Enabled = true;
            }
        }

        private void rb30_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb30.Checked)
            {
                RefreshFrequence rf = new RefreshFrequence();
                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/profile/patientQuery.xml"), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "30";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 30000;
                this.timer1.Enabled = true;
            }
        }

        private void rb60_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rb60.Checked)
            {
                RefreshFrequence rf = new RefreshFrequence();
                System.IO.FileStream fs = new System.IO.FileStream((Application.StartupPath + "/profile/patientQuery.xml"), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                rf.RefreshTime = "60";
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                x.Serialize(fs, rf);
                fs.Close();

                this.timer1.Interval = 60000;
                this.timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.RefreshPatient();
        }

        private void ucQueryReg_Load(object sender, EventArgs e)
        {
            // [2007/03/13] �������ļ�,�������(���߾�����Ϣ��ѯ)
            if (System.IO.File.Exists(Application.StartupPath + "/profile/patientQuery.xml"))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(RefreshFrequence));
                System.IO.FileStream fs = new System.IO.FileStream(Application.StartupPath + "/profile/patientQuery.xml", System.IO.FileMode.Open);
                
                RefreshFrequence refres = (RefreshFrequence)xs.Deserialize(fs);
                switch (refres.RefreshTime.Trim())
                {
                    case "no":
                        this.SetRadioFalse();
                        fs.Close();
                        this.rbNo.Checked = true;
                        break;
                    case "10":
                        this.SetRadioFalse();
                        fs.Close();
                        this.rb10.Checked = true;
                        break;
                    case "30":
                        this.SetRadioFalse();
                        fs.Close();
                        this.rb30.Checked = true;
                        break;
                    case "60":
                        this.SetRadioFalse();
                        fs.Close();
                        this.rb60.Checked = true;
                        break;
                    default:
                        this.SetRadioFalse();
                        fs.Close();
                        this.rbNo.Checked = true;
                        break;
                }
            }
            else
            {
                this.SetRadioFalse();
                this.rbNo.Checked = true;
            }
            // [2007/03/13] ����
        }

        private void neuTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            //ˢ�²�ѯ������Ϣ
            this.RefreshPatient();
        }
    }
}