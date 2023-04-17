using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Manager.Items
{
    public partial class ucFinanceGroup : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
     
        public ucFinanceGroup()
        {
            InitializeComponent();
        }

        #region ����
                  
        //���һ���
        private Hashtable htDept = new Hashtable();        
        //������Ա        
        private DataTable dtDeptEmp;
        //��Ա������
        private DataTable dtFinanceEmployee;
        //��Ա���������ݼ�
        private DataSet dsFinanceEmployee;

        //fpGroupEmployee�м���       
        private int countEmplFinance; 

        //��Ա��������
        FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup feeEmplFinanceGroup = new FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup();
        //��������
        FS.HISFC.Models.Fee.FinanceGroup feeFinanceGroup = new FS.HISFC.Models.Fee.FinanceGroup();
        //��Ա��
        FS.HISFC.BizLogic.Manager.Person managerPerson = new FS.HISFC.BizLogic.Manager.Person();
        //ToolBar
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion
        
        #region ����

        /// <summary>
        /// ����ToolBar��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("�½�", "�½�������",(int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�,true,false,null);
            toolBarService.AddToolButton("ɾ��","ɾ����Ա",(int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��,true,false,null);
            toolBarService.AddToolButton("����", "������Ա", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);   

            return this.toolBarService;

        }

        /// <summary>
        /// ToolBar�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.Text)
            {
                case "�½�":
                    this.NewGroup();
                    break;
                case "ɾ��":
                    this.DeleteGroup();
                    break;
                case "����":
                    this.SaveAll();
                    break;
            }

            base.ToolStrip_ItemClicked(sender,e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveAll();
            return 1;
            //return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        private void GetDept()
        {
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();

            ArrayList alDept = dept.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.F);
            //ArrayList alDept = dept.GetDeptmentAll();

            if (alDept == null)
            {
                return;
            }

            for (int i = 0, j = alDept.Count; i < j; i++)
            {
                this.htDept.Add(((FS.FrameWork.Models.NeuObject)alDept[i]).ID, ((FS.FrameWork.Models.NeuObject)alDept[i]).Name);
            }

            this.cbDepartment.AddItems(alDept);
        }

        /// <summary>
        /// ��ȡ��������Ա��Ϣ
        /// </summary>
        /// <param name="strDeptId">���ұ��</param>
        private void GetDeptEmployee(string deptId)
        {

            ArrayList alEmployee = managerPerson.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F, deptId);

            DataSet dsEmployee = new DataSet();

            DataTable dtEmployee = new DataTable("dtEmployee");

            DataColumn[] colEmployee = {new DataColumn("��Ա����"),
						               new DataColumn("��Ա����"),
						               new DataColumn("���Ҵ���")};

            dtEmployee.Columns.AddRange(colEmployee);

            dtEmployee.Rows.Clear();

            foreach (FS.HISFC.Models.Base.Employee pInfo in alEmployee)
            {
                DataRow row = dtEmployee.NewRow();

                row["��Ա����"] = pInfo.ID;
                row["��Ա����"] = pInfo.Name;
                row["���Ҵ���"] = pInfo.Dept;

                dtEmployee.Rows.Add(row);
            }

            this.fpEmployee_Sheet1.DataSource = dtEmployee;

        }

        /// <summary>
        /// ��ȡ�������б�
        /// </summary>
        private void GetFinanceGroup()
        {

            ArrayList alFinance = feeEmplFinanceGroup.QueryFinaceGroupIDAndNameAll();

            DataSet dsFinance = new DataSet();

            DataTable dtFinance = new DataTable();

            DataColumn[] colFinance = {new DataColumn("���������"),
                                    new DataColumn("����������")};

            dtFinance.Columns.AddRange(colFinance);

            dtFinance.Rows.Clear();

            foreach (FS.HISFC.Models.Fee.FinanceGroup fg in alFinance)
            {
                DataRow row = dtFinance.NewRow();

                row["���������"] = fg.ID;
                row["����������"] = fg.Name;

                dtFinance.Rows.Add(row);
            }

            this.fpGroup_Sheet1.DataSource = dtFinance;

        }

        /// <summary>
        /// ��ȡ����������Ա�б�
        /// </summary>
        /// <param name="strFinance">���������</param>
        private void GetFinanceEmployee(string strFinance)
        {
            ArrayList alFinanceEmployee = feeEmplFinanceGroup.GetFinaceGroupInfo(strFinance);

            dsFinanceEmployee = new DataSet();

            dtFinanceEmployee = new DataTable();

            DataColumn[] colFinanceEmployee = {new DataColumn("���������"),
                                               new DataColumn("����������"),
                                               new DataColumn("��Ա����"),
                                               new DataColumn("��Ա����"),
                                               new DataColumn("��Ч��״̬"),
                                               new DataColumn("���"),
                                               new DataColumn("Ψһ��ʶ")};

            dtFinanceEmployee.Columns.AddRange(colFinanceEmployee);

            dtFinanceEmployee.Rows.Clear();

            foreach (FS.HISFC.Models.Fee.FinanceGroup fe in alFinanceEmployee)
            {
                DataRow row = dtFinanceEmployee.NewRow();

                row["���������"] = fe.ID;
                row["����������"] = fe.Name;
                row["��Ա����"] = fe.Employee.ID;
                row["��Ա����"] = fe.Employee.Name;
                row["��Ч��״̬"] = ((int)fe.ValidState).ToString();
                row["���"] = fe.SortID;
                row["Ψһ��ʶ"] = fe.PkID;

                dtFinanceEmployee.Rows.Add(row);
            }

            this.fpGroupEmployee_Sheet1.DataSource = dtFinanceEmployee;

        }

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        protected virtual void ChangeFinanceEmployee(int flag)
        {
            string strFinanceId;

            strFinanceId = "";

            try
            {
                if (flag == 0)
                {
                    strFinanceId = this.fpGroup_Sheet1.Cells[0, 0].Text;
                }
                else
                {
                    strFinanceId = this.fpGroup_Sheet1.Cells[this.fpGroup_Sheet1.ActiveRowIndex, 0].Text;
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            if (this.fpGroupEmployee_Sheet1.Rows.Count > 0)
            {
                this.fpGroupEmployee_Sheet1.Rows.Remove(0, this.fpGroupEmployee_Sheet1.Rows.Count);
            }

            this.GetFinanceEmployee(strFinanceId);
            countEmplFinance = fpGroupEmployee_Sheet1.Rows.Count;

            if (fpGroupEmployee_Sheet1.Rows.Count > 0)
            {
                string ValidState;

                FarPoint.Win.Spread.CellType.ComboBoxCellType comboState = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

                comboState.Items = new String[] { "��Ч", "ͣ��", "����" };

                comboState.ItemData = new String[] { "0", "1", "2" };


                for (int i = 0; i < fpGroupEmployee_Sheet1.Rows.Count; i++)
                {
                    ValidState = fpGroupEmployee_Sheet1.Cells[i, 4].Text;
                    //MessageBox.Show("ValidState" + ValidState);
                    switch (ValidState)
                    {
                        case "1":
                            ValidState = "��Ч";
                            break;
                        case "0":
                            ValidState = "ͣ��";
                            break;
                        case "2":
                            ValidState = "����";
                            break;
                    }
                    fpGroupEmployee_Sheet1.Cells[i, 4].CellType = comboState;

                    fpGroupEmployee_Sheet1.SetText(i, 4, ValidState);
                }

                //this.fpGroupEmployee_Sheet1.Columns[4].CellType = comboState;
            }
            else
            {
                return;
            }

        }

        /// <summary>
        /// ���Ӳ�����
        /// </summary>
        protected virtual void NewGroup()
        {
            try
            {
                int intGroupCode;

                intGroupCode = feeEmplFinanceGroup.GetMaxPkID();

                if (intGroupCode != -1 && intGroupCode != 0)
                {
                    fpGroup.AddRow();                            
                    fpGroup_Sheet1.Cells[fpGroup_Sheet1.ActiveRowIndex, 0].Text = intGroupCode.ToString();
                    fpGroup_Sheet1.Cells[fpGroup_Sheet1.ActiveRowIndex, 1].Text = "������" + intGroupCode.ToString();
                    fpGroup_Sheet1.SetActiveCell(fpGroup_Sheet1.ActiveRowIndex, 1);
                    
                }
                else if (intGroupCode == 0)
                {
                    int index = fpGroup_Sheet1.Rows.Count;
                    fpGroup_Sheet1.AddRows(index, 1);
                    fpGroup_Sheet1.Cells[0, 0].Text = intGroupCode.ToString();
                    fpGroup_Sheet1.Cells[0, 1].Text = "������" + intGroupCode.ToString();
                    fpGroup_Sheet1.SetActiveCell(0, 1);

                }
                fpGroup.ScrollBarMaxAlign = true;
                
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            this.fpGroup_Sheet1.Columns[0].Locked = true;


        }


        protected virtual void DeleteGroup()
        {
            if (fpGroupEmployee_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show("�ò�������û����Ա,��ɾ�����������飡");
                return;
            }
            string GroupName=this.fpGroup_Sheet1.Cells[this.fpGroup_Sheet1.ActiveRowIndex,1].Text;
            if (MessageBox.Show("��ȷ��ɾ��" + GroupName + "��", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction Tran = new FS.FrameWork.Management.Transaction(feeEmplFinanceGroup.Connection);
            //Tran.BeginTransaction();

            feeEmplFinanceGroup.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                string delText;
                for (int i = 0; i < this.fpGroupEmployee_Sheet1.Rows.Count; i++)
                {
                    delText = fpGroupEmployee_Sheet1.GetText(i, 6);
                    if (feeEmplFinanceGroup.Delete(delText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();;
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ǰ����������Աɾ��ʧ�ܣ�") + this.feeEmplFinanceGroup.Err);
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ǰ����������Աɾ���ɹ���") /*+ this.feeEmplFinanceGroup.Err*/);
                ChangeFinanceEmployee(1);
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ǰ����������Աɾ��ʧ�ܣ�") );
            }
        }

        #region ��������

        /// <summary>
        /// ɾ��������
        /// </summary>
        //protected virtual void DeleteGroup()
        //{
        //    int index;
        //    string delText;

        //    try
        //    {

        //        index = fpGroupEmployee_Sheet1.ActiveRow.Index;
        //        delText = fpGroupEmployee_Sheet1.GetText(index, 6);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("��ѡ�������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    try
        //    {

        //        //ɾ��ǰ̨����
        //        //fpGroup.DelRow(fpGroup_Sheet1.ActiveRowIndex);

        //        for (int i = 0; i < this.fpGroupEmployee_Sheet1.Rows.Count; i++)
        //        { 

        //        }

        //        //ɾ����̨����
        //        if (feeEmplFinanceGroup.Delete(delText) != -1)
        //        {
        //            MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ǰ����������Աɾ���ɹ���") /*+ this.feeEmplFinanceGroup.Err*/);
        //            ChangeFinanceEmployee(1);
        //        }
        //        else
        //        {
        //            MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ǰ����������Աɾ��ʧ�ܣ�") + this.feeEmplFinanceGroup.Err);
        //            return;
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show(ee.Message);
        //    }
        //}
        #endregion
        /// <summary>
        /// ����
        /// </summary>
        protected virtual void SaveAll()
        {
            this.fpGroup.EditMode = false;
            if (fpGroupEmployee_Sheet1.Rows.Count < 1)
            {
                return;
            }

            //FS.FrameWork.Management.Transaction Tran = new FS.FrameWork.Management.Transaction(feeEmplFinanceGroup.Connection);
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //Tran.BeginTransaction();

                feeEmplFinanceGroup.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                string tempStr = string.Empty;
                //1���������ӵļ�¼
                if ((fpGroupEmployee_Sheet1.Rows.Count - countEmplFinance) > 0)
                {
                    //�������ӵ��п�ʼѭ������
                    for (int i = countEmplFinance, j = fpGroupEmployee_Sheet1.Rows.Count; i < j; i++)
                    {
                        feeFinanceGroup.ID = fpGroupEmployee_Sheet1.Cells[i, 0].Text;
                        feeFinanceGroup.Name = fpGroupEmployee_Sheet1.Cells[i, 1].Text;
                        feeFinanceGroup.Employee.ID = fpGroupEmployee_Sheet1.Cells[i, 2].Text;
                        feeFinanceGroup.Employee.Name = fpGroupEmployee_Sheet1.Cells[i, 3].Text;
                        tempStr = fpGroupEmployee_Sheet1.Cells[i, 4].Text;
                                                
                        //״ֵ̬ת��
                        switch (tempStr)
                        {
                            case "��Ч":
                                feeFinanceGroup.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                                break;

                            case "ͣ��":
                                feeFinanceGroup.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
                                break;

                            case "����":
                                feeFinanceGroup.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;
                                break;
                        }

                        feeFinanceGroup.SortID = 0;

                        feeFinanceGroup.PkID = fpGroupEmployee_Sheet1.GetText(i, 6);

                        //���ñ��淽��
                        if (feeEmplFinanceGroup.Insert(feeFinanceGroup) > 0)
                        {
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ӵ�ǰ����������Աʧ�ܣ�") + this.feeEmplFinanceGroup.Err);
                            return;
                        }

                    }
                }//1

                //2�����޸ĵļ�¼
                if ((fpGroupEmployee_Sheet1.Rows.Count - countEmplFinance) == 0)
                {
                    //��������ֵѭ�������޸ļ�¼
                    for (int i = 0, j = fpGroupEmployee_Sheet1.Rows.Count; i < j; i++)
                    {
                        feeFinanceGroup.ID = fpGroupEmployee_Sheet1.Cells[i, 0].Text;
                        feeFinanceGroup.Name = fpGroupEmployee_Sheet1.Cells[i, 1].Text;
                        feeFinanceGroup.Employee.ID = fpGroupEmployee_Sheet1.Cells[i, 2].Text;
                        feeFinanceGroup.Employee.Name = fpGroupEmployee_Sheet1.Cells[i, 3].Text;
                        tempStr = fpGroupEmployee_Sheet1.Cells[i, 4].Text;

                        //״ֵ̬ת��
                        switch (tempStr)
                        {
                            case "��Ч":
                                feeFinanceGroup.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                                break;

                            case "ͣ��":
                                feeFinanceGroup.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
                                break;

                            case "����":
                                feeFinanceGroup.ValidState = FS.HISFC.Models.Base.EnumValidState.Ignore;
                                break;
                        }


                        feeFinanceGroup.SortID = Convert.ToInt32(fpGroupEmployee_Sheet1.GetText(i, 5));

                        feeFinanceGroup.PkID = fpGroupEmployee_Sheet1.GetText(i, 6);

                        //���ø��·���
                        if (feeEmplFinanceGroup.Update(feeFinanceGroup.PkID, feeFinanceGroup) > 0)
                        {
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���µ�ǰ����������Ա��Ϣʧ�ܣ�") + this.feeEmplFinanceGroup.Err);
                            return;
                        }

                    }
                }//2
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ա��������Ϣ����ɹ���") + this.feeEmplFinanceGroup.Err);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + FS.FrameWork.Management.Language.Msg("��Ա��������Ϣ����ʧ�ܣ�") + this.feeEmplFinanceGroup.Err);
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            
        }

        #endregion
                
        #region �¼�

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFinanceGroup_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void Init()
        {
            //ȡ�ÿ����б�
            this.GetDept();
            //ȡ�ò������б�
            this.GetFinanceGroup();
            //ȡ�ò���������Ա�б�
            if (fpGroup_Sheet1.Rows.Count > 0)
            {
                ChangeFinanceEmployee(0);
            }
            else
            {
                //{8C1F1BED-7D98-46ef-B36F-5D37DC04A6B1} ����ʼû������ʱ��ʹ��Rows.Clear() �ᷢ������
                //fpGroupEmployee_Sheet1.Rows.Clear();
                this.fpGroupEmployee_Sheet1.Rows.Count = 0;
            }
        }

        /// <summary>
        /// ����ѡ��
        /// </summary>
        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cbDepartment.Tag != null)
                {
                    this.GetDeptEmployee(this.cbDepartment.Tag.ToString());

                    fpEmployee_Sheet1.Columns[0].Width = 80;
                    fpEmployee_Sheet1.Columns[1].Width = 80;
                    fpEmployee_Sheet1.Columns[2].Visible = false;

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// ������ѡ��
        /// </summary>
        private void fpGroup_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            ChangeFinanceEmployee(1);
        }

        /// <summary>
        /// ˫��������ӵ�ǰ����������Ա��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEmployee_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                int ActiveFinance;//��ǰ��������
                int ActiveEmployee;//��ǰ��Ա��

                ActiveFinance = 0;
                ActiveEmployee = 0;

                //��ȡ��Ա�б�������
                if (fpEmployee_Sheet1.RowCount > 0)
                {
                    ActiveEmployee = fpEmployee_Sheet1.ActiveRow.Index;
                }
                else
                {
                    MessageBox.Show("��ǰ��Ա�б���û�м�¼�����飡");
                }

                //��ȡ�������б�������
                if (fpGroup_Sheet1.RowCount > 0)
                {
                    ActiveFinance = fpGroup_Sheet1.ActiveRow.Index;

                    if (ActiveFinance == null)
                    {
                        ActiveFinance = 1;
                    }
                }
                else
                {
                    MessageBox.Show("��ǰ�������б���û�м�¼�����飡");
                }


                feeFinanceGroup.Employee.ID = fpEmployee_Sheet1.Cells[ActiveEmployee, 0].Text.Trim().ToString();
                feeFinanceGroup.Employee.Name = fpEmployee_Sheet1.Cells[ActiveEmployee, 1].Text.Trim().ToString();

                feeFinanceGroup.ID = fpGroup_Sheet1.Cells[ActiveFinance, 0].Text.Trim().ToString();
                feeFinanceGroup.Name = fpGroup_Sheet1.Cells[ActiveFinance, 1].Text.Trim().ToString();

                string pkId = Convert.ToString(feeEmplFinanceGroup.GetMaxPkID());

                dtFinanceEmployee.Rows.Add(new object[] { feeFinanceGroup.ID, feeFinanceGroup.Name, feeFinanceGroup.Employee.ID, feeFinanceGroup.Employee.Name, "��Ч", "0", pkId });

                FarPoint.Win.Spread.CellType.ComboBoxCellType comboValidState = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

                comboValidState.Items = new String[] { "��Ч", "ͣ��", "����" };

                fpGroupEmployee_Sheet1.Cells[dtFinanceEmployee.Rows.Count - 1, 4].CellType = comboValidState;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        #endregion

        private void fpGroup_EditModeOff(object sender, EventArgs e)
        {
            int rowIndex = this.fpGroup_Sheet1.ActiveRowIndex;
            if (this.fpGroup_Sheet1.ActiveColumnIndex == 1)
            {
                int count = this.fpGroupEmployee_Sheet1.Rows.Count;
                string groupName=this.fpGroup_Sheet1.Cells[rowIndex,1].Text;
                if (count> 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        this.fpGroupEmployee_Sheet1.Cells[i, 1].Text = groupName;
                    }
                }
            }
            
        }
               

        

       
    }
}
