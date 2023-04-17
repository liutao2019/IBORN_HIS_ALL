using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ������Ա���ҿؼ�]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFindDeptAndEmployee : UserControl
    {
        
        private ucDepartmentManager ucDeptMgr;
        int searchPerson = 0;//������Աλ�ó�ʼֵ
        int searchPersonMax = 0;//������Աλ�����ֵ
        int searchDept = 0;//���ҿ���λ�ó�ʼֵ
        int searchDeptMax = 0;//���ҿ���λ�����ֵ
        string tempSearch = "XXXXXXhhdhfhruuuurrr^^&&&&&((**&&%%###$";
        ArrayList alPerson = new ArrayList();
        //��Ա������
        FS.HISFC.BizLogic.Manager.Person perMgr = new FS.HISFC.BizLogic.Manager.Person();
        public ucFindDeptAndEmployee()
        {   
            InitializeComponent();
        }
        /// <summary>
        /// ������Ա����������
        /// </summary>
        public ucDepartmentManager UcDeptMgr
        {
            get 
            {
                return this.ucDeptMgr;
            }
            set
            {
                this.ucDeptMgr = value;
            }
        }

        /// <summary>
        /// ���ݿ��ұ����ѯ���ҷ���
        /// </summary>
        /// <param name="deptCode"></param>
        private void SearchDept(string deptCode)
        {
            foreach (TreeNode obj in this.ucDeptMgr.tvDeptList1.Nodes[0].Nodes)
            {
                foreach (TreeNode objT in obj.Nodes)
                {
                    if (objT.Text.Substring(1, 4) == deptCode)
                    {
                        this.ucDeptMgr.tvDeptList1.SelectedNode = objT;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// ���ҷ���
        /// </summary>
        private void Search()
        {
            try
            {
                string search = this.tbFind.Text;
                //�������Ϊ����
                if (rbDept.Checked)
                {   //�����������Ϊ����
                    if (rbCode.Checked)
                    {
                        search = search.PadLeft(4, '0');
                        foreach (TreeNode typeNode in this.ucDeptMgr.tvDeptList1.Nodes[0].Nodes)
                        {
                            foreach (TreeNode obj in typeNode.Nodes)
                            {
                                string text = obj.Text.Substring(0, 4);

                                if (obj.Text.Substring(1, 4) == search)
                                {
                                    this.ucDeptMgr.tvDeptList1.SelectedNode = obj;
                                    break;
                                }
                            }
                        }
                    }
                    //�����ѯ����Ϊ����
                    if (rbName.Checked)
                    {
                        if (search != tempSearch)
                        {
                            searchDept = 0;
                            searchDeptMax = this.ucDeptMgr.tvDeptList1.Nodes[0].Nodes.Count;
                        }

                        tempSearch = search;

                        for (int i = searchDept; i < this.ucDeptMgr.tvDeptList1.Nodes[0].Nodes.Count; i++)
                        {
                            TreeNode typeNode = this.ucDeptMgr.tvDeptList1.Nodes[0].Nodes[i];
                            foreach (TreeNode obj in typeNode.Nodes)
                            {
                                if (obj.Text.IndexOf(search) >= 0)
                                {
                                    this.ucDeptMgr.tvDeptList1.SelectedNode = obj;
                                    break;
                                }
                            }
                            searchDept++;
                        }

                        searchDept++;
                        if (searchDept == searchDeptMax)
                        {
                            searchDept = 0;
                        }

                    }
                }
                //�������Ϊ��Ա
                if (rbEmpl.Checked)
                {   //�����ѯ����Ϊ����
                    if (rbCode.Checked)
                    {
                        search = search.PadLeft(6, '0');
                       FS.HISFC.Models.Base.Employee obj = perMgr.GetPersonByID(search);
                        if (obj == null)
                        {
                            MessageBox.Show("û�д�Ա����ŵ���Ա!");
                            return;
                        }
                        SearchDept(obj.Dept.ID);
                        for (int i = 0; i < this.ucDeptMgr.neuSpread1_Sheet1.RowCount; i++)
                        {
                            if (this.ucDeptMgr.neuSpread1_Sheet1.Cells[i, 0].Text == search)
                            {
                                //{4C8F4B67-330F-4e5e-B537-8AB753F272A7}
                                this.ucDeptMgr.neuSpread1_Sheet1.AddSelection(i, 1, 1,1);
                                break;
                            }
                        }
                    }
                    //�����ѯ����Ϊ����
                    if (rbName.Checked)
                    {
                        if (search != tempSearch)
                        {
                            alPerson = perMgr.GetPersonByName("%" + search + "%");
                            /*
                             *  [2007/02/05] ����ط�Ӧ���Ǵ����,alPerson��ȫ�ֵı�ı���
                             *               �Ѿ��ڵ�31�г�ʼ����,����ҵ���ķ���ֵ��̫������null.
                             *               �Ҿ��ñ���Ӧ���� alPerson.Count==0;
                             *     
                             *   if (alPerson == null)
                             *   {
                             *       MessageBox.Show("û�����Ʒ���" + "[" + search + "]" + "��Ա��!");
                             *       return;
                             *   }
                             * 
                             * */
                            if (alPerson.Count == 0)
                            {
                                MessageBox.Show("û�����Ʒ���" + "[" + search + "]" + "��Ա��!");
                                return;
                            }
                            searchPersonMax = alPerson.Count;
                            searchPerson = 0;
                        }

                        tempSearch = search;

                        if (alPerson.Count == 0)
                        {
                            MessageBox.Show("û�д�Ա����ŵ���Ա!");
                            return;
                        }

                        FS.HISFC.Models.Base.Employee obj = (FS.HISFC.Models.Base.Employee)alPerson[searchPerson];
                        if (obj == null)
                        {
                            MessageBox.Show("û�д�Ա����ŵ���Ա!");
                            return;
                        }
                        SearchDept(obj.Dept.ID);
                        for (int i = 0; i < this.ucDeptMgr.neuSpread1_Sheet1.RowCount; i++)
                        {
                            if (this.ucDeptMgr.neuSpread1_Sheet1.Cells[i, 0].Text == obj.ID)
                            {
                                //{4C8F4B67-330F-4e5e-B537-8AB753F272A7}
                                this.ucDeptMgr.neuSpread1_Sheet1.AddSelection(i, 1, 1, 1);
                                break;
                            }
                        }
                        searchPerson++;
                        if (searchPerson == searchPersonMax)
                            searchPerson = 0;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void bttClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void bttNext_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void rbCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCode.Checked)
            {
                if (rbDept.Checked)
                    tbFind.MaxLength = 4;
                if (rbEmpl.Checked)
                    tbFind.MaxLength = 6;
            }
            else
            {
                tbFind.MaxLength = 100;
            }
        }

        private void rbDept_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDept.Checked)
            {
                if (rbCode.Checked)
                {
                    tbFind.MaxLength = 4;
                }
                if (rbName.Checked)
                {
                    tbFind.MaxLength = 100;
                }
            }
            else
            {
                if (rbCode.Checked)
                {
                    tbFind.MaxLength = 6;
                }
                if (rbName.Checked)
                {
                    tbFind.MaxLength = 100;
                }
            }
        }

        private void tbFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Search();
        }
    }
}
