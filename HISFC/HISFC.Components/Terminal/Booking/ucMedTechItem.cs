using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Terminal.Booking
{
    public partial class ucMedTechItem : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMedTechItem()
        {
            InitializeComponent();
        }

        #region ����ȫ�ֱ���
        //��������洢��ҩƷ��Ϣ
        private DataTable UndrugTable = null;
        private DataView UndrugView = null;��
        //������� �洢����ԤԼ��Ŀ����Ϣ
        private DataTable DeptItemTable = null;
        private DataView DeptItemView = null;
        //����ҵ��������
        FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizProcess.Integrate.Terminal.Booking bookMgr = new FS.HISFC.BizProcess.Integrate.Terminal.Booking();
        //
        FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private bool IsTextFocus = false;
        //�����������ݴ�������
        //����

        private ArrayList Deptlist = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper OperListHelper = new FS.FrameWork.Public.ObjectHelper();
        //ϵͳ���
        private ArrayList Classlist = new ArrayList();
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.PrintDialog printDialog1;
        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        //private FS.HISFC.BizLogic.Manager.Person controlMgr = new FS.HISFC.BizLogic.Manager.Person();
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label16;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbQuery;
        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        //private FS.HISFC.Models.RADT.Person p = new FS.HISFC.Models.RADT.Person();
        private ArrayList alALL = new ArrayList();
        FS.HISFC.Models.Base.Employee var = null;
        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ɾ��", "ɾ��", 0, true, false, null);
            toolBarService.AddToolButton("����", "����", 1, true, false, null);
            toolBarService.AddToolButton("ȡ�����", "ȡ�����", 4, true, false, null);
            return toolBarService;
        }
        #endregion

        #region ���������Ӱ�ť�����¼�
        /// <summary>
        /// ���������Ӱ�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ɾ��":
                    this.DeleteInfo();
                    break;
                default:
                    break;
            }
        }
        #endregion


        #endregion

        #region ���ӣ��޸ģ�ɾ��������  �Ĳ���
        public override int Save(object sender, object neuObject)
        {
             SaveInfo();
             return 1;
        }
        /// <summary>
        /// �������Ӻ��޸ĵ�����
        /// </summary>
        protected  void SaveInfo()
        {
            
            //ԭ��û�п���,ֻ��ά���Լ����ҵ���Ŀ,��ʱ����һ��...
            string strDept = var.Dept.ID;
            if (this.cmbDept.Tag != null)
            {
                strDept = this.cmbDept.Tag.ToString();
            }
            //FS.HISFC.BizLogic.MedTech.MedTech item = new FS.HISFC.BizLogic.MedTech.MedTech();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction Addtrans = new FS.FrameWork.Management.Transaction(dbMgr.Connection);
            //Addtrans.BeginTransaction();
            feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            bookMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                this.fpSpread2.StopCellEditing();
                //��ȡ���ӵĿ�����Ŀ����Ϣ

                ArrayList list = GetInfo();
                if (list == null || list.Count <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("û�л�ÿ���ԤԼ��Ŀ��Ϣ");
                    return;
                }

                //�������ӵĿ�����Ŀ����Ϣ
                bool Result = true;
                foreach (FS.HISFC.Models.Terminal.MedTechItem info in list)
                {
                    info.ItemExtend.Dept.ID = strDept;
                    //�������Ŀ���в����µļ�¼ MET_TEC_DEPTITEM
                    if (bookMgr.UpdateMedTechItem(info) <= 0)
                    {
                        //�������Ŀ���и����µļ�¼ MET_TEC_DEPTITEM
                        if (bookMgr.InsertMedTechItem(info) <= 0)
                        {
                            Result = false;
                            break;
                        }
                    }
                }
                if (Result)
                {
                    //�ύ����
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("����ɹ�");
                }
                else
                {
                    //������Ϣ
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������Ŀ��Ϣʧ��" + bookMgr.Err);
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
            }
            this.LockFp();
        }

        /// <summary>
        /// ����һ�����ݵ�������ĿԤԼ����
        /// </summary>
        private void AddDataInfo()
        {
            //�жϸ���Ŀ�Ƿ��Ѿ���ӡ�
            ArrayList list = GetInfo();
            if (list != null && list.Count > 0)
            {
                foreach (FS.HISFC.Models.Terminal.MedTechItem oninfo in list)
                {
                    if (oninfo.Item.ID == this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)Cols.DeptID].Text)
                    {
                        MessageBox.Show(oninfo.Item.Name + "�Ѿ��ڸÿ���ԤԼ��Ŀ������,��ֱ��ά������");
                        return;
                    }
                }
            }
            string zt = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, GetColumnKey("��ʶ")].Text;
            if (zt == "1")
            {
                try
                {
                    if (fpSpread1_Sheet1.RowCount < 1)
                    {
                        return; //���û�����ݷ��ؿ� 
                    }
                    FS.HISFC.Models.Fee.Item.Undrug feeinfo = new FS.HISFC.Models.Fee.Item.Undrug();
                    FS.HISFC.Models.Terminal.MedTechItem deptinfo = new FS.HISFC.Models.Terminal.MedTechItem();
                    //					//�����ݿ��ȡҪ�޸ĵ���Ϣ
                    //					feeinfo = item.GetItemAll(fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex,GetColumnKey("��Ŀ����")].Text);
                    //					if(feeinfo ==null)
                    //					{
                    //						MessageBox.Show("��ȡ��ҩƷ��Ϣ����");
                    //						return ;
                    //					}
                    feeinfo.ID = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, GetColumnKey("��Ŀ����")].Text;
                    feeinfo.Name = fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, GetColumnKey("��Ŀ����")].Text;
                    FS.HISFC.Models.Terminal.MedTechItem tail = new FS.HISFC.Models.Terminal.MedTechItem();
                    //p = controlMgr.GetEmployeeInfo(controlMgr.Operator.ID);
                    tail.Item.ID = feeinfo.ID; //��ҩƷ����
                    tail.Item.Name = feeinfo.Name;//��ҩƷ����
                    tail.ItemExtend.UnitFlag = "��ϸ"; //���׻�����ϸ
                    tail.ItemExtend.Dept.ID = var.Dept.ID;  //���Ҵ���
                    tail.Item.SysClass.ID = feeinfo.SysClass.ID;//feeinfo.SysClass.ID;  //ϵͳ���
                    tail.ItemExtend.ReasonableFlag = FS.FrameWork.Function.NConvert.ToInt32(feeinfo.IsConsent).ToString();//֪��ͬ����
                    tail.Item.Notice = feeinfo.Notice;//ע������
                    tail.Item.Oper.ID = var.ID;//����Ա
                    //����
                    DataRow row = DeptItemTable.NewRow();
                    //�������
                    SetNewRow(tail, row);
                    //���ӵ�����
                    DeptItemTable.Rows.Add(row);
                    //�������
                    DeptItemTable.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (zt == "2")
            {
                try
                {
                    if (fpSpread1_Sheet1.RowCount < 1)
                    {
                        return; //���û�����ݷ��ؿ� 
                    }
                    FS.HISFC.Models.Fee.Item.Undrug ztinfo = new FS.HISFC.Models.Fee.Item.Undrug();
                    //FS.HISFC.BizLogic.Manager.ComGroup feeMgr = new FS.HISFC.BizLogic.Manager.ComGroup();
                    FS.HISFC.Models.Terminal.MedTechItem deptinfo = new FS.HISFC.Models.Terminal.MedTechItem();
                    //�����ݿ��ȡҪ�޸ĵ���Ϣ
                    ztinfo = feeMgr.GetItem(fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, GetColumnKey("��Ŀ����")].Text);
                    if (ztinfo == null)
                    {
                        MessageBox.Show("��ȡ��ҩƷ��Ϣ����");
                        return;
                    }
                    FS.HISFC.Models.Terminal.MedTechItem tail = new FS.HISFC.Models.Terminal.MedTechItem();
                    //p = this.controlMgr.GetEmployeeInfo(controlMgr.Operator.ID);
                    tail.Item.ID = ztinfo.ID; //��ҩƷ����
                    tail.Item.Name = ztinfo.Name;//��ҩƷ����
                    tail.ItemExtend.UnitFlag = "����"; //���׻�����ϸ
                    tail.ItemExtend.Dept.ID = var.Dept.ID;  //���Ҵ���
                    tail.Item.SysClass.ID = ztinfo.SysClass.ID;  //ϵͳ���
                    //					tail.ItemExtend.ReasonableFlag = this.getStringValue(ztinfo.);//֪��ͬ����
                    tail.Item.Notice = ztinfo.Notice;//ע������
                    tail.Item.Oper.ID = var.ID;//����Ա
                    //����
                    DataRow row = DeptItemTable.NewRow();
                    //�������
                    SetNewRow(tail, row);
                    //���ӵ�����
                    DeptItemTable.Rows.Add(row);
                    //�������
                    DeptItemTable.AcceptChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.LockFp();
        }

        /// <summary>
        /// ɾ����ѡ�����
        /// </summary>
        private void DeleteInfo()
        {
            try
            {
                ArrayList list = this.GetDelInfo();
                if (list == null)  //���ݿ���û�е���
                {
                    fpSpread2_Sheet1.Rows.Remove(this.fpSpread2_Sheet1.ActiveRowIndex, 1);
                }
                else  //���ݿ��д��ڵ�
                {
                    //��ʾ�û�������ɾ��
                    string message = "ɾ���󽫲��ָܻ�����ȷ��Ҫɾ����";
                    string caption = "��ʾ��";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult re;

                    re = MessageBox.Show(this, message, caption, buttons,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (re == DialogResult.Yes)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        //FS.FrameWork.Management.Transaction Addtrans = new FS.FrameWork.Management.Transaction(dbMgr.Connection);
                        //Addtrans.BeginTransaction();
                        bookMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        try
                        {
                            bool result = true;
                            foreach (FS.HISFC.Models.Terminal.MedTechItem info in list)
                            {
                                if (bookMgr.DeleteMedTechItem(info.ItemExtend.Dept.ID, info.Item.ID) <= 0)
                                {
                                    result = false;
                                    break;
                                }
                            }
                            if (result)
                            {
                                FS.FrameWork.Management.PublicTrans.Commit();
                                //{0E428A94-08F0-4b88-B78C-41C3490718C7}
                                fpSpread2.EditMode = false;
                                fpSpread2_Sheet1.Rows.Remove(this.fpSpread2_Sheet1.ActiveRowIndex, 1);
                            }
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���ݿ�ɾ������ʧ��");
                            }
                        }
                        catch (Exception ex)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DeptItemTable.AcceptChanges();
            this.LockFp();
        }


        /// <summary>
        /// �ҵ�ԤԼ��Ŀ������Ҫɾ�����ݵ���Ϣ
        /// </summary>
        /// <param name="iteminfo"></param>
        /// <returns></returns>
        private ArrayList GetDelInfo()
        {
            ArrayList ItemList = new ArrayList();
            if (DeptItemTable == null)
            {
                return null;
            }
            FS.HISFC.Models.Terminal.MedTechItem item = new FS.HISFC.Models.Terminal.MedTechItem();

            item.ItemExtend.Dept.ID = this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.DeptID].Text;
            item.Item.ID = this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.ItemID].Text;
            FS.HISFC.Models.Terminal.MedTechItem info = bookMgr.GetMedTechItem(var.Dept.ID, item.Item.ID);
            if (info.ItemExtend.Dept.ID == null)
            {
                return null;
            }
            else
            {
                ItemList.Add(item);
            }
            return ItemList;
        }


        /// <summary>
        /// �õ���Ҫ����ԤԼ��Ŀ����Ϣ �����б� ׼���������
        /// </summary>
        /// <returns></returns>
        private ArrayList GetInfo()
        {
            ArrayList ItemList = new ArrayList();
            if (DeptItemTable == null)
            {
                return null;
            }
            FS.HISFC.Models.Terminal.MedTechItem item = null;
            //ѭ��ȡ���� 
            if (this.fpSpread2_Sheet1.RowCount <= 0) return null;
            for (int k = 0; k < this.fpSpread2_Sheet1.RowCount; k++)
            {
                item = new FS.HISFC.Models.Terminal.MedTechItem();
                item.ItemExtend.Dept.ID = var.Dept.ID;
                item.Item.ID = this.fpSpread2_Sheet1.Cells[k, (int)Cols.ItemID].Text;//��Ŀ����	
                item.Item.Name = this.fpSpread2_Sheet1.Cells[k, (int)Cols.ItemName].Text;//��Ŀ����
                item.Item.SysClass.ID = "";//this.GetSysClassFromName(this.fpSpread2_Sheet1.Cells[k,(int)Cols.SysClass].Text);//ϵͳ���
                item.ItemExtend.UnitFlag = this.getUnitIDByName(this.fpSpread2_Sheet1.Cells[k, (int)Cols.UnitFlag].Text);//��λ��ʶ
                item.ItemExtend.BookLocate = this.fpSpread2_Sheet1.Cells[k, (int)Cols.BookLocate].Text;//ԤԼ��
                item.ItemExtend.BookTime = this.fpSpread2_Sheet1.Cells[k, (int)Cols.BookTime].Text;//ԤԼ�̶�ʱ��
                item.ItemExtend.ExecuteLocate = this.fpSpread2_Sheet1.Cells[k, (int)Cols.ExecuteLocate].Text;//ִ�еص�
                item.ItemExtend.ReportTime = this.fpSpread2_Sheet1.Cells[k, (int)Cols.ReportTime].Text;//ȡ����ʱ��
                item.ItemExtend.HurtFlag = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread2_Sheet1.Cells[k, (int)Cols.HurtFlag].Text.ToUpper()).ToString();//�д�/�޴�
                item.ItemExtend.SelfBookFlag = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread2_Sheet1.Cells[k, (int)Cols.SelfBookFlag].Text.ToUpper()).ToString();//�Ƿ����ԤԼ
                item.ItemExtend.ReasonableFlag = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread2_Sheet1.Cells[k, (int)Cols.ReasonableFlag].Text.ToUpper()).ToString();//֪��ͬ����
                item.ItemExtend.Speciality = this.fpSpread2_Sheet1.Cells[k, (int)Cols.Speciality].Text;//����רҵ
                item.ItemExtend.ClinicMeaning = this.fpSpread2_Sheet1.Cells[k, (int)Cols.ClinicMeaning].Text;//�ٴ�����
                item.ItemExtend.SimpleKind = this.fpSpread2_Sheet1.Cells[k, (int)Cols.SimpleKind].Text;//�걾
                string strway = "";
                if (this.fpSpread2_Sheet1.Cells[k, (int)Cols.SimpleWay].Tag == null)
                {
                    strway = "";
                }
                else
                {
                    strway = this.fpSpread2_Sheet1.Cells[k, (int)Cols.SimpleWay].Tag.ToString();
                }
                item.ItemExtend.SimpleWay = strway;//��������
                item.ItemExtend.SimpleUnit = this.fpSpread2_Sheet1.Cells[k, (int)Cols.SimpleUnit].Text;//�걾��λ
                item.ItemExtend.SimpleQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread2_Sheet1.Cells[k, (int)Cols.SimpleQty].Text);//�걾��
                item.ItemExtend.Container = this.fpSpread2_Sheet1.Cells[k, (int)Cols.Container].Text;//����
                item.ItemExtend.Scope = this.fpSpread2_Sheet1.Cells[k, (int)Cols.Scope].Text;//����ֵ��Χ
                item.ItemExtend.MachineType = this.fpSpread2_Sheet1.Cells[k, (int)Cols.MachineType].Text;//�豸����
                item.ItemExtend.BloodWay = this.fpSpread2_Sheet1.Cells[k, (int)Cols.BloodWay].Text;//��Ѫ����
                item.Item.Notice = this.fpSpread2_Sheet1.Cells[k, (int)Cols.Notice].Text;//ע������
                item.Item.Oper.ID = this.fpSpread2_Sheet1.Cells[k, (int)Cols.OperID].Text;//����Ա
                item.Item.Oper.OperTime = this.dateTimePicker1.Value;//��������

                ItemList.Add(item);
            }
            return ItemList;
        }
        #endregion
        #region ö��
        private enum Cols
        {
            DeptID, //����0
            ItemID, //��Ŀ����1
            ItemName,//��Ŀ����2
            SysClass, //ϵͳ���3
            UnitFlag,//ҩƷ/��ҩƷ/������Ŀ4
            BookLocate,//ԤԼ�ص�5
            BookTime,//ԤԼʱ��6
            ExecuteLocate,//ִ�еص�7
            ReportTime,//ȡ����ʱ��8
            HurtFlag,//�д�/�޴� 9
            SelfBookFlag,//�Ƿ����ԤԼ10
            ReasonableFlag,//֪��ͬ����11
            Speciality,//����רҵ 12
            ClinicMeaning,//�ٴ����� 13
            SimpleKind,//�걾 14
            SimpleWay,//�������� 15
            SimpleUnit,//�걾��λ 16
            SimpleQty,//�걾�� 17
            Container,//���� 18
            Scope,//����ֵ��Χ 19
            MachineType,//�豸���� 20
            BloodWay,//��Ѫ���� 21
            Notice,//ע������ 22
            OperID,//����Ա 23
            OperTime//�������� 24
        }
         #endregion 
        #region  �����б�����ɼ����¼�

        //�����б�
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitInfo()
        {
            try
            {
                //���������б�
                this.initList();
                fpSpread2_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// �����������б�
        /// </summary>
        private void initList()
        {
            try
            {
                //FS.HISFC.BizProcess.Integrate.Manager dept = new FS.HISFC.BizLogic.Manager.Department();
                this.fpSpread2.SelectNone = true;
                //��ȡ����
                ArrayList al = this.managerMgr.GetDepartment();
                //				this.fpSpread2.SetColumnList(this.fpSpread2_Sheet1,0,al);   //���������������
                //				this.fpSpread2.SetColumnList(this.fpSpread2_Sheet1,1,al);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ������Ӧ����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int fpSpread2_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                //�س�
                if (this.fpSpread2.ContainsFocus)
                {
                    int i = this.fpSpread2_Sheet1.ActiveColumnIndex;
                    if (i == 0)
                    {
                        ProcessDept();
                    }
                    else if (i == 2)
                    {
                        if (fpSpread2_Sheet1.ActiveRowIndex < fpSpread2_Sheet1.Rows.Count - 1)
                        {
                            fpSpread2_Sheet1.SetActiveCell(fpSpread2_Sheet1.ActiveRowIndex + 1, 0);
                        }
                        else
                        {
                            //����һ��
                            //							this.AddRow();
                        }
                    }
                }
            }
            else if (key == Keys.Up)
            {

            }
            else if (key == Keys.Down)
            {

            }
            else if (key == Keys.Escape)
            {

            }
            return 0;
        }
        private int fpSpread2_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }

        private int ProcessDept()
        {
            int CurrentRow = fpSpread2_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            if (fpSpread2_Sheet1.ActiveColumnIndex == 0)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpSpread2.getCurrentList(this.fpSpread2_Sheet1, 0);
                //��ȡѡ�е���Ϣ
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                //���ұ���
                fpSpread2_Sheet1.ActiveCell.Text = item.ID;
                fpSpread2_Sheet1.SetActiveCell(fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.SysClass);
                return 0;
            }

            else if (fpSpread2_Sheet1.ActiveColumnIndex == 3)
            {
                return 0;
            }
            return 0;
        }
        #endregion

        #region �򿪴��ڵ��¼�
        private void frmTecDeptItem_Load(object sender, System.EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
            Application.DoEvents();
            this.fpSpread2_Sheet1.DataAutoSizeColumns = false;
            var = (FS.HISFC.Models.Base.Employee)dbMgr.Operator;
            //������Ӧ�����¼�
            fpSpread2.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpSpread2_KeyEnter);
            fpSpread2.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpSpread2_SetItem);
            fpSpread2.ShowListWhenOfFocus = true;
            InitInfo();
            //��ʼ����ҩƷ�б�
            loadUndrug();
            //��ʼ�����ұ�
            loadDeptItem();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            retrieveDeptItemAll();
            this.InitComb();

            this.LockFp();
            this.initList();
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private void loadUndrug()
        {
            try
            {
                UndrugTable = new DataTable();
                System.Type dtStr = System.Type.GetType("System.String");
                System.Type dtDec = System.Type.GetType("System.Decimal");
                System.Type dtDTime = System.Type.GetType("System.DateTime");
                System.Type dtBool = System.Type.GetType("System.Boolean");
                UndrugTable = new DataTable();
                UndrugTable.Columns.AddRange(new DataColumn[] {
																   new DataColumn( "��Ŀ����",  dtStr ),		//0
																   new DataColumn("��Ŀ����",    dtStr),		//1
																   new DataColumn("ƴ����",  dtStr),		//2
																   new DataColumn("���",	 dtStr),		//3
																   new DataColumn("������",	 dtStr)	,	//4
																   new DataColumn("��ʶ",dtStr)
				});

                //��������Ϊ����
                //				CreateKeys(UndrugTable);
                UndrugView = new DataView(UndrugTable);
                this.fpSpread1_Sheet1.DataSource = UndrugView;
                this.LockFp();

                ArrayList alReturn = new ArrayList();//���صķ�ҩƷ��Ϣ;

                alReturn = this.bookMgr.GetAllList("MEDTECHITEM");
                //ѭ��������Ϣ
                foreach (FS.HISFC.Models.Base.Const obj in alReturn)
                {
                    if (obj.IsValid)
                    {
                        DataRow row = UndrugTable.NewRow();
                        SetRow(obj, row);
                        UndrugTable.Rows.Add(row);
                    }
                }
                alALL.AddRange(alReturn);

                UndrugTable.AcceptChanges(); //�������
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            this.fpSpread1_Sheet1.Columns[(int)Cols.DeptID].Width = 80F;
            this.fpSpread1_Sheet1.Columns[(int)Cols.ItemName].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)Cols.SysClass].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)Cols.UnitFlag].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)Cols.BookLocate].Visible = false;
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private void loadDeptItem()
        {
            try
            {
                DeptItemTable = new DataTable();
                System.Type dtStr = System.Type.GetType("System.String");
                System.Type dtDec = System.Type.GetType("System.Decimal");
                System.Type dtDTime = System.Type.GetType("System.DateTime");
                System.Type dtBool = System.Type.GetType("System.Boolean");
                DeptItemTable = new DataTable();
                DeptItemTable.Columns.AddRange(new DataColumn[] {
																	 new DataColumn("���Ҵ���",  dtStr ),		//0
																	 new DataColumn("��Ŀ����",    dtStr),		//1
																	 new DataColumn("��Ŀ����",  dtStr),		//2
																	 new DataColumn("ϵͳ���",	 dtStr),		//3
																	 new DataColumn("��λ��ʶ",	 dtStr),		//4
																	 new DataColumn("ԤԼ��",  dtStr),		//5
																	 new DataColumn("ԤԼ�̶�ʱ��",  dtStr),		//6
																	 new DataColumn("ִ�еص�",  dtStr) ,		//7
																	 new DataColumn("ȡ����ʱ��",  dtStr) ,		//8
																	 new DataColumn("�д�/�޴�",  dtStr), 		//9
																	 new DataColumn("�Ƿ����ԤԼ",  dtStr) ,		//10
																	 new DataColumn("֪��ͬ����",  dtStr) ,		//11
																	 new DataColumn("����רҵ",  dtStr) ,		//12
																	 new DataColumn("�ٴ�����",  dtStr) ,		//13
																	 new DataColumn("�걾",  dtStr) ,		//14
																	 new DataColumn("��������",  dtStr) ,		//15
																	 new DataColumn("�걾��λ",  dtStr) ,		//16
																	 new DataColumn("�걾��",  dtStr) ,		//17
																	 new DataColumn("����",  dtStr), //18
																	 new DataColumn("����ֵ��Χ",  dtStr),//19
																	 new DataColumn("�豸����",  dtStr),//20
																	 new DataColumn("��Ѫ����",  dtStr),//21
																	 new DataColumn("ע������",  dtStr) ,		//22
																	 new DataColumn("����Ա",  dtStr) ,		//23
																	 new DataColumn("��������",  dtStr)		//24
																   
																 });

                //��������Ϊ����
                //				CreateKeys(DeptItemTable);
                DeptItemView = new DataView(DeptItemTable);
                this.fpSpread2_Sheet1.DataSource = DeptItemView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        /// <summary>
        /// �����Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>

        /// <summary>
        /// ��ԤԼ��Ŀ�� ���ݿ��� ˢ�µ�������
        /// </summary>
        private void retrieveDeptItemAll()
        {
            if (this.fpSpread2_Sheet1.RowCount > 0)
            {
                this.fpSpread2_Sheet1.RemoveRows(0, this.fpSpread2_Sheet1.RowCount);
            }
            //FS.HISFC.BizLogic.Manager.Person controlMgr = new FS.HISFC.BizLogic.Manager.Person();
            //FS.HISFC.Models.RADT.Person p = controlMgr.GetEmployeeInfo(controlMgr.Operator.ID);
            //FS.HISFC.BizLogic.MedTech.MedTech dp = new FS.HISFC.BizLogic.MedTech.MedTech();
            //FS.HISFC.BizLogic.Manager.Constant controlMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList allInfo = new ArrayList();
            string strdept = var.Dept.ID;
            if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
            {
                strdept = this.cmbDept.Tag.ToString();
            }
            allInfo = this.bookMgr.QueryMedTechItem(strdept);

            foreach (FS.HISFC.Models.Terminal.MedTechItem info in allInfo)
            {
                if (info.ItemExtend.Dept.ID == null)
                {
                    return;
                }
                else
                {
                    this.fpSpread2_Sheet1.Rows.Add(0, 1);
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.DeptID].Text = info.ItemExtend.Dept.ID;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.ItemID].Text = info.Item.ID;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.ItemName].Text = info.Item.Name;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.SysClass].Text = info.Item.SysClass.Name;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.UnitFlag].Text = this.getUnitNameById(info.ItemExtend.UnitFlag);
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.BookLocate].Text = info.ItemExtend.BookLocate;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.BookTime].Text = info.ItemExtend.BookTime;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.ExecuteLocate].Text = info.ItemExtend.ExecuteLocate;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.ReportTime].Text = info.ItemExtend.ReportTime;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.HurtFlag].Value = FS.FrameWork.Function.NConvert.ToBoolean(info.ItemExtend.HurtFlag);
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.SelfBookFlag].Value = FS.FrameWork.Function.NConvert.ToBoolean(info.ItemExtend.SelfBookFlag);
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.ReasonableFlag].Value = FS.FrameWork.Function.NConvert.ToBoolean(info.ItemExtend.ReasonableFlag);
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.Speciality].Text = info.ItemExtend.Speciality;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.ClinicMeaning].Text = info.ItemExtend.ClinicMeaning;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.SimpleKind].Text = info.ItemExtend.SimpleKind;
                    string strWay = "";
                    if (info.ItemExtend.SimpleWay == null || info.ItemExtend.SimpleWay == "")
                    {
                        strWay = "";
                    }
                    else
                    {
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        obj = bookMgr.GetConstant("USAGE", info.ItemExtend.SimpleWay);
                        if (obj == null || obj.Name == null || obj.Name == "")
                        {
                            strWay = "";
                        }
                        else
                        {
                            strWay = obj.Name;
                        }
                    }
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.SimpleWay].Text = strWay;//��������
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.SimpleUnit].Text = info.ItemExtend.SimpleUnit;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.SimpleQty].Text = info.ItemExtend.SimpleQty.ToString();
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.Container].Text = info.ItemExtend.Container;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.Scope].Text = info.ItemExtend.Scope;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.MachineType].Text = info.ItemExtend.MachineType;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.BloodWay].Text = info.ItemExtend.BloodWay;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.Notice].Text = info.Item.Notice;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.OperID].Text = info.Item.Oper.ID;
                    this.fpSpread2_Sheet1.Cells[this.fpSpread2_Sheet1.ActiveRowIndex, (int)Cols.OperTime].Text = dbMgr.GetDateTimeFromSysDateTime().ToString();
                }
            }
        }
        #region ���������Ϣ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        private void SetRow(FS.HISFC.Models.Fee.Item.Undrug obj, DataRow row)
        {
            row["��Ŀ����"] = obj.ID;					//0                                             
            row["��Ŀ����"] = obj.Name;					//1
            row["ƴ����"] = obj.SpellCode;			//2											
            row["���"] = obj.WBCode;				//3											
            row["������"] = obj.UserCode;			//4
            row["��ʶ"] = "1";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        private void SetRow(FS.FrameWork.Models.NeuObject obj, DataRow row)
        {
            row["��Ŀ����"] = obj.ID;					//0                                             
            row["��Ŀ����"] = obj.Name;					//1
            row["ƴ����"] = obj.User01;			//2											
            row["���"] = obj.User02;				//3											
            row["������"] = obj.User03;			//4
            row["��ʶ"] = "1";
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        private void SetRowZt(FS.HISFC.Models.Base.Group obj, DataRow row)
        {
            row["��Ŀ����"] = obj.ID;					//0                                             
            row["��Ŀ����"] = obj.Name;					//1
            row["ƴ����"] = obj.SpellCode;			//2											
            row["���"] = obj.WBCode;				//3											
            row["������"] = obj.UserCode;			//4		
            row["��ʶ"] = "2";
        }
        #endregion

        #region ������洰����Ϣ
        /// <summary>
        /// ��� ������Ŀ��Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        private void SetNewRow(FS.HISFC.Models.Terminal.MedTechItem obj, DataRow row)
        {
            row["���Ҵ���"] = obj.ItemExtend.Dept.ID;
            row["��Ŀ����"] = obj.Item.ID;			//0                                             
            row["��Ŀ����"] = obj.Item.Name;				//1																	
            row["��λ��ʶ"] = obj.ItemExtend.UnitFlag;			//2
            row["ϵͳ���"] = obj.Item.SysClass.Name;			//3
            row["֪��ͬ����"] = FS.FrameWork.Function.NConvert.ToBoolean(obj.ItemExtend.ReasonableFlag);			//4
            row["ע������"] = obj.Item.Notice;			//5
            row["����Ա"] = obj.Item.Oper.ID;			//6
            row["��������"] = System.DateTime.Now;			//7
        }
        #endregion

        /// <summary>
        /// ��ʼ�������ؼ�
        /// </summary>
        private void InitComb()
        {
            //����
            //FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

            ArrayList al = managerMgr.GetDepartment();
            if (al == null) al = new ArrayList();

            this.cmbDept.AddItems(al);
            //this.cmbDept.isItemOnly = true;

            this.cmbDept.Tag = var.Dept.ID;
            this.cmbDept.Text = var.Dept.Name;

            if (this.Tag == null || this.Tag.ToString() != "ALL")
            {
                this.cmbDept.Enabled = false;
            }

            if (this.alALL.Count > 0)
            {
                this.cmbQuery.AddItems(alALL);
            }
        }
        #endregion

        #region �Դ��ڵĲ����¼��Ĵ���
        /// <summary>
        /// ���洰�ڵĵ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //			fpSpread1_Sheet1.SetActiveCell(1,0);
            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex,
                this.fpSpread1_Sheet1.ActiveColumnIndex);
        }
        /// <summary>
        /// ����˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex,
                this.fpSpread1_Sheet1.ActiveColumnIndex);
            try
            {
                //����һ�����ݵ�����ԤԼ��Ŀ����
                AddDataInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        /// <summary>
        /// ���ÿ�ݼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            int AltKey = Keys.Alt.GetHashCode();
            if (keyData.GetHashCode() == AltKey + Keys.S.GetHashCode())
            {
                //����
                SaveInfo();
                //				retrieveDeptItemAll();
            }

            if (keyData.GetHashCode() == AltKey + Keys.A.GetHashCode())
            {
                //ɾ��
                DeleteInfo();
            }

            if (keyData.GetHashCode() == AltKey + Keys.X.GetHashCode())
            {
                //�˳�
                //this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!IsTextFocus) //��ѯ��û�л�ý��� 
            {
                if (keyData.GetHashCode() == Keys.Enter.GetHashCode())
                {
                    //�س����� 
                    if (fpSpread2_Sheet1.Rows.Count > 0)
                    {
                        //��ǰ���
                        int i = fpSpread2_Sheet1.ActiveRowIndex;
                        int j = fpSpread2_Sheet1.ActiveColumnIndex;
                        while ((j + 1 <= fpSpread2_Sheet1.ColumnCount - 1) && !this.fpSpread2_Sheet1.Columns[j + 1].Visible)
                        {
                            j++;
                        }
                        if (j + 1 <= fpSpread2_Sheet1.ColumnCount - 1)
                        {
                            //�������һ�� ������ƶ�һ��
                            fpSpread2_Sheet1.SetActiveCell(i, j + 1);
                        }
                        else if (i < fpSpread2_Sheet1.Rows.Count)
                        {
                            //�Ѿ������һ��  ����������һ�� ��������һ��
                            fpSpread2_Sheet1.SetActiveCell(i + 1, 5);
                        }
                        else
                        {
                            this.fpSpread2_Sheet1.SetActiveCell(i, j);
                        }
                    }
                }
                else if (keyData.GetHashCode() == Keys.Space.GetHashCode())
                {
                    if (fpSpread2_Sheet1.ActiveColumnIndex == 20)
                    {
                        int m = this.fpSpread2_Sheet1.ActiveRowIndex;
                        FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
                        if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(bookMgr.GetAllList("MACHINETYPE"), ref neuObj) == 1)
                        {
                            this.fpSpread2_Sheet1.SetValue(m, 20, neuObj.Name);
                        }
                    }
                    else if (fpSpread2_Sheet1.ActiveColumnIndex == 15)
                    {
                        int m = this.fpSpread2_Sheet1.ActiveRowIndex;
                        FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
                        if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(bookMgr.GetAllList("USAGE"), ref neuObj) == 1)
                        {
                            this.fpSpread2_Sheet1.SetValue(m, 15, neuObj.Name);
                            this.fpSpread2_Sheet1.Cells[m, (int)Cols.SimpleWay].Tag = neuObj.ID;
                        }
                    }
                    else if (fpSpread2_Sheet1.ActiveColumnIndex == 22)
                    {
                        int m = this.fpSpread2_Sheet1.ActiveRowIndex;
                        FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
                        if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(bookMgr.GetAllList("UNDRUGEXTENDMARK"), ref neuObj) == 1)
                        {
                            FarPoint.Win.Spread.CellType.TextCellType text3 = new FarPoint.Win.Spread.CellType.TextCellType();
                            text3.Multiline = true;
                            this.fpSpread2_Sheet1.Columns[(int)Cols.Notice].CellType = text3;
                            this.fpSpread2_Sheet1.SetValue(m, 22, this.bookMgr.GetConstant("UNDRUGEXTENDMARK", neuObj.ID).Memo);
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void fpSpread2_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
        }
        private void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {

        }

        /// <summary>
        /// ���ҵ�ת��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.retrieveDeptItemAll();
        }
        #endregion

        #region ������������
        /// <summary>
        /// ��ѯ������λ��
        /// </summary>
        /// <returns></returns>
        private int GetColumnKey(string str)
        {
            foreach (FarPoint.Win.Spread.Column col in this.fpSpread1_Sheet1.Columns)
            {
                if (col.Label == str)
                {
                    return col.Index;
                }
            }
            return 0;
        }

        /// <summary>
        /// ��������Ϊ����
        /// </summary>
        private void CreateKeys(DataTable table)
        {
            DataColumn[] keys = new DataColumn[] { table.Columns["��Ŀ����"] };
            table.PrimaryKey = keys;
        }

        /// <summary>
        /// �����п�
        /// </summary>
        private void LockFp()
        {
            FarPoint.Win.Spread.CellType.TextCellType txttype = new FarPoint.Win.Spread.CellType.TextCellType();
            txttype.Multiline = true;
            FarPoint.Win.Spread.CellType.CheckBoxCellType cbxtype = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType numtype = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            txttype.ReadOnly = true;
            this.fpSpread2_Sheet1.Columns[(int)Cols.DeptID].CellType = txttype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.ItemID].CellType = txttype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.ItemName].CellType = txttype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.SysClass].CellType = txttype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.UnitFlag].CellType = txttype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.SysClass].Visible = false;
            this.fpSpread2_Sheet1.Columns[(int)Cols.UnitFlag].Visible = false;
            this.fpSpread2_Sheet1.Columns[(int)Cols.OperID].CellType = txttype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.OperTime].CellType = txttype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.HurtFlag].CellType = cbxtype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.SelfBookFlag].CellType = cbxtype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.ReasonableFlag].CellType = cbxtype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.SimpleWay].CellType = txttype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.SimpleQty].CellType = numtype;
            this.fpSpread2_Sheet1.Columns[(int)Cols.DeptID].Visible = false;	//���Ҵ���
            this.fpSpread2_Sheet1.Columns[(int)Cols.ItemID].Visible = false; //��Ŀ����
            //this.fpSpread2_Sheet1.Columns[(int)Cols.ItemName].Visible = false;//��Ŀ����
            this.fpSpread2_Sheet1.Columns[(int)Cols.UnitFlag].Visible = false;  //��λ��ʶ
            this.fpSpread2_Sheet1.Columns[(int)Cols.OperID].Visible = false; //����Ա
            this.fpSpread2_Sheet1.Columns[(int)Cols.OperTime].Visible = false; //����ʱ��

        }
        /// <summary>
        /// �����ݿ����Ѿ������ݵĽ��м���
        /// </summary>
        private FS.HISFC.Models.Terminal.MedTechItem find(string deptcode, string itemcode)
        {
            try
            {
                FS.HISFC.Models.Terminal.MedTechItem info = bookMgr.GetMedTechItem(deptcode, itemcode);
                if (info.ItemExtend.Dept.ID == null)
                {
                    return null;
                }
                else
                {
                    return info;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// ���������������ÿ���ԤԼ��Ŀ��¼��Ϣ
        /// </summary>
        private ArrayList deptItemInfo = new ArrayList();
        public ArrayList DeptItemInfo
        {
            get
            {
                return this.deptItemInfo;
            }
            set
            {
                this.deptItemInfo = value;
            }
        }

        ///// <summary>
        ///// CheckBox�ж���ֵ��ת����
        ///// </summary>
        //private string getStringValue(bool bl)
        //{
        //    string str = "1";
        //    if (bl)
        //    {
        //        str = "0";
        //    }
        //    return str;
        //}
        //private bool getBoolValue(string str)
        //{
        //    bool bl = true;
        //    if (str == "1" || str == "" || str == "FALSE")
        //    {
        //        bl = false;
        //    }
        //    return bl;
        //}

        /// <summary>
        /// ��ȡ��λ��ʶ����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string getUnitIDByName(string name)
        {
            string unitId;
            if (name == "����")
            {
                unitId = "2";
                return unitId;
            }
            else if (name == "��ϸ")
            {
                unitId = "1";
                return unitId;
            }
            return "0";
        }

        private string getUnitNameById(string id)
        {
            string unitName;
            if (id == "2")
            {
                unitName = "����";
                return unitName;
            }
            else if (id == "1")
            {
                unitName = "��ϸ";
                return unitName;
            }
            return "δ֪";
        }

        #endregion

        #region ɸѡ
        private void cmbQuery_Leave(object sender, System.EventArgs e)
        {
            IsTextFocus = false;
        }

        private void cmbQuery_TextChanged(object sender, System.EventArgs e)
        {
            string temp = " like  '%" + this.cmbQuery.Text + "%' ";
            UndrugView.RowFilter = "ƴ����" + temp + " or " + "���" + temp + " or " + "������" + temp + " or " + "���" + temp + " or " + "��Ŀ����" + temp;
            IsTextFocus = true;
        }

        private void chbAll_CheckedChanged(object sender, System.EventArgs e)
        {
            //			this.loadUndrug();
            //			this.InitComb();
        }
        #endregion 

    }
}