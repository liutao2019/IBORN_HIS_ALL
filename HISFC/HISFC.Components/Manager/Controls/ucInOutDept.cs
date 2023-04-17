using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: ��������ά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// 
    /// <˵��>
    ///     1 ���ұ���洢��ʵ��ID�ֶ��� �������ұ���洢Dept�ֶ���
    /// </˵��>
    /// </summary>
    public partial class ucInOutDept : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInOutDept()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// �������Ͱ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptTypeHelper = null;

        /// <summary>
        /// ��ѡ�����
        /// </summary>
        private ArrayList alChooseDept = null;

        /// <summary>
        /// ģ�鹦������
        /// </summary>
        private FS.HISFC.Models.IMA.EnumModuelType moduelType = FS.HISFC.Models.IMA.EnumModuelType.Phamacy;

        /// <summary>
        /// ����ͳ�Ʊ���
        /// </summary>
        private string statCode = "03";

        /// <summary>
        /// ��־������ 10 ��� 20 ����
        /// </summary>
        private string inOutFlag = "10";

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PrivInOutDept privInOutManager = new FS.HISFC.BizLogic.Manager.PrivInOutDept();

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        #endregion

        #region ����

        /// <summary>
        /// ģ�鹦������
        /// </summary>
        public FS.HISFC.Models.IMA.EnumModuelType ModuelType
        {
            get
            {
                return this.moduelType;
            }
            set
            {
                this.moduelType = value;

                switch (value)
                {
                    case FS.HISFC.Models.IMA.EnumModuelType.Phamacy:
                        this.statCode = "03";
                        break;
                    case FS.HISFC.Models.IMA.EnumModuelType.Material:
                        this.statCode = "05";
                        break;
                    case FS.HISFC.Models.IMA.EnumModuelType.Equipment:
                        this.statCode = "06";
                        break;
                    case FS.HISFC.Models.IMA.EnumModuelType.Blood:
                        this.statCode = "04";
                        break;
                }
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {          
            toolBarService.AddToolButton("���ӿ���", "������������", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ����������", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.DelInOutDept();
            }
            if (e.ClickedItem.Text == "���ӿ���")
            {
                this.AddInOutDept();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveInOut();

            return 1;
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        private void Init()
        {            
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            this.alChooseDept = deptStatManager.LoadChildrenUnionDept(this.statCode, "AAAA");
            if (this.alChooseDept == null)
            {
                MessageBox.Show(Language.Msg("ȡ�����б����:" + deptStatManager.Err));
                return;
            }

            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            this.deptTypeHelper = new FS.FrameWork.Public.ObjectHelper();
            this.deptTypeHelper.ArrayObject = this.alChooseDept;
            //{CFC740A1-77C6-4722-A6BF-DCDC94171838} by nxy
            this.SetColumnFormat();

        }

        /// <summary>
        /// ���
        /// </summary>
        protected void Clear()
        {
            this.fpInSheet.Rows.Count = 0;
            this.fpOutSheet.Rows.Count = 0;
        }

        /// <summary>
        /// ��Fp�ڼ���ʵ��
        /// </summary>
        /// <param name="sv">��������ݵ�SheetView</param>
        /// <param name="iRowIndex">������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToFp(FarPoint.Win.Spread.SheetView sv, int iRowIndex,FS.HISFC.Models.Base.PrivInOutDept info)
        {
            sv.Rows.Add(iRowIndex, 1);

            sv.Cells[iRowIndex, (int)ColumnSet.ColSortID].Text = info.SortID.ToString();    //����
            sv.Cells[iRowIndex, (int)ColumnSet.ColDeptID].Text = info.Dept.ID;              //���ű���
            sv.Cells[iRowIndex, (int)ColumnSet.ColDeptName].Text = info.Dept.Name;
            if (this.deptTypeHelper.GetObjectFromID(info.Dept.ID) != null)
            {
                sv.Cells[iRowIndex, (int)ColumnSet.ColeDeptType].Text = this.deptTypeHelper.GetObjectFromID(info.Dept.ID).User02;
            }
            sv.Cells[iRowIndex, (int)ColumnSet.ColMemo].Text = info.Memo;                   //��ע
            sv.Rows[iRowIndex].Tag = info;

            return 1;
        }

        /// <summary>
        /// �����¿���
        /// </summary>
        /// <param name="sv">��������ݵ�SheetView</param>
        /// <param name="iRowIndex">������</param>
        /// <param name="dept">���ұ���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToFp(FarPoint.Win.Spread.SheetView sv, int iRowIndex, FS.HISFC.Models.Base.Department dept)
        {
            FS.HISFC.Models.Base.PrivInOutDept privObj = new FS.HISFC.Models.Base.PrivInOutDept();

            privObj.ID = this.privDept.ID;
            privObj.Name = this.privDept.Name;
            privObj.Role.Grade2.ID = this.statCode + this.inOutFlag;
            privObj.Dept = dept;

            sv.Rows.Add(iRowIndex, 1);

            sv.Cells[iRowIndex, (int)ColumnSet.ColSortID].Value = 0;			        //����
            sv.Cells[iRowIndex, (int)ColumnSet.ColDeptID].Value = dept.ID;		        //���ű���
            sv.Cells[iRowIndex, (int)ColumnSet.ColDeptName].Value = dept.Name;		    //��������
            if (dept.DeptType != null)
            {
                sv.Cells[iRowIndex, (int)ColumnSet.ColeDeptType].Value = dept.DeptType.Name;	//��������
            }

            sv.Rows[iRowIndex].Tag = privObj;

            return 1;
        }     

        /// <summary>
        /// ����������ʾ
        /// </summary>
        /// <returns></returns>
        protected int ShowInOutDept()
        {
            if (this.neuTabControl1.SelectedTab == this.tpInDept)
            {
                return this.ShowInOutDept(this.fpInSheet, this.statCode + this.inOutFlag);
            }
            else
            {
                return this.ShowInOutDept(this.fpOutSheet, this.statCode + this.inOutFlag);
            }            
        }

        /// <summary>
        /// ������ʾ��������
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="privType"></param>
        /// <returns></returns>
        private int ShowInOutDept(FarPoint.Win.Spread.SheetView sv, string privType)
        {
            sv.Rows.Count = 0;
            ArrayList alPrivInOut = this.privInOutManager.GetPrivInOutDeptList(this.privDept.ID, privType);
            if (alPrivInOut == null)
            {
                MessageBox.Show(Language.Msg("��ȡ" + this.privDept.Name + "  " +privType + "Ȩ��ʧ�� " + this.privInOutManager.Err));
                return -1;
            }

            foreach (FS.HISFC.Models.Base.PrivInOutDept info in alPrivInOut)
            {
                this.AddDataToFp(sv, 0,info);
            }

            return 1;
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        protected int AddInOutDept()
        {
            //���ڵ���� �������
            FS.FrameWork.Models.NeuObject temp = this.tv.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
            if (temp == null)
            {
                return 1;
            }

            if (temp.ID.Substring(0, 1) == "S")
            {
                return 1;
            }

            if (this.privDept.ID.Substring(0, 1) == "S")
            {
                return 1;
            }

            if (this.neuTabControl1.SelectedTab == this.tpInDept)
            {
                return this.AddInOutDept(this.fpInSheet);
            }
            else
            {
                return this.AddInOutDept(this.fpOutSheet);
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="sv">����ӵ�SheetView</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int AddInOutDept(FarPoint.Win.Spread.SheetView sv)
        {
            //��������,���û�ѡ��Ҫ��ӵĿ���
            List<FS.HISFC.Models.Base.Department> alList = FS.HISFC.Components.Common.Classes.Function.ChooseMultiDept();
            if (alList == null || alList.Count == 0)
            {
                return -1;
            }

            System.Collections.Hashtable hsExits = new Hashtable();
            for (int i = 0; i < sv.Rows.Count; i++)
            {
                hsExits.Add(sv.Cells[i, (int)ColumnSet.ColDeptID].Text,null);
            }

            foreach (FS.HISFC.Models.Base.Department obj in alList)
            {
                if (hsExits.ContainsKey(obj.ID))
                {
                    continue;
                }

                this.AddDataToFp(sv, sv.Rows.Count, obj);
            }

            return 1;
        }

        /// <summary>
        /// ��������ɾ��
        /// </summary>
        /// <returns></returns>
        protected int DelInOutDept()
        {
            if (this.neuTabControl1.SelectedTab == this.tpInDept)
            {
                return this.DelInOutDept(this.fpInSheet);
            }
            else
            {
                return this.DelInOutDept(this.fpOutSheet);
            }
        }

        /// <summary>
        /// ������������ɾ����ǰ��
        /// </summary>
        /// <param name="sv"></param>
        /// <returns></returns>
        protected int DelInOutDept(FarPoint.Win.Spread.SheetView sv)
        {
            if (sv.Rows.Count <= 0)
            {
                return 1;
            }

            string deptName = sv.Cells[sv.ActiveRowIndex, (int)ColumnSet.ColDeptName].Text;
            if (MessageBox.Show(Language.Msg("ȷ��Ҫ�ѿ��ҡ�" + deptName + "��ɾ����"), "ȷ�Ͽ���ɾ��", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return 1;
            }

            string deptCode = sv.Cells[sv.ActiveRowIndex, (int)ColumnSet.ColDeptID].Text;
            //�����ݿ���ɾ���˼�¼
            if (this.privInOutManager.DeletePrivInOutDept(this.statCode + this.inOutFlag, this.privDept.ID, deptCode) == -1)
            {
                MessageBox.Show(this.privInOutManager.Err);
                return -1;
            }

            //�ڿؼ���ɾ���˼�¼
            sv.ActiveRow.Remove();

            MessageBox.Show(Language.Msg("ɾ���ɹ�"));

            return 1;
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns></returns>
        private bool IsValid(FarPoint.Win.Spread.SheetView sv)
        {
            for (int i = 0; i < sv.Rows.Count; i++)
            {
                if (!FS.FrameWork.Public.String.ValidMaxLengh(sv.Cells[i, (int)ColumnSet.ColMemo].Text, 128))
                {
                    MessageBox.Show(Language.Msg("��ע�ֶγ��� ���ʵ�����"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int SaveInOut()
        {
            if (this.neuTabControl1.SelectedTab == this.tpInDept)
            {
                return this.SaveInOut(this.fpInSheet);
            }
            else
            {
                return this.SaveInOut(this.fpOutSheet);
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sv">������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int SaveInOut(FarPoint.Win.Spread.SheetView sv)
        {
            if (!this.IsValid(sv))
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.privInOutManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.privInOutManager.DeletePrivInOutDeptAll(this.statCode + this.inOutFlag, this.privDept.ID) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(Language.Msg("����ɾ��ʧ��" + this.privInOutManager.Err));
                return -1;
            }

            for (int i = 0; i < sv.Rows.Count; i++)
            {
                FS.HISFC.Models.Base.PrivInOutDept privInOut = sv.Rows[i].Tag as FS.HISFC.Models.Base.PrivInOutDept;

                privInOut.SortID = NConvert.ToInt32(sv.Cells[i, (int)ColumnSet.ColSortID].Text);
                privInOut.Memo = sv.Cells[i, (int)ColumnSet.ColMemo].Text;

                if (this.privInOutManager.InsertPrivInOutDept(privInOut) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("��������ʧ��" + this.privInOutManager.Err));
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();;
            MessageBox.Show(Language.Msg("����ɹ�"));

            this.ShowInOutDept();

            return 1;
        }

        #endregion

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpInDept)
            {
                this.inOutFlag = "10";
            }
            else
            {
                this.inOutFlag = "20";
            }

            this.ShowInOutDept();
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Clear();

            if (e.Parent == null || e.Tag == null)
            {
                return -1;
            }

            FS.FrameWork.Models.NeuObject temp = e.Tag as FS.FrameWork.Models.NeuObject;
            if (temp == null)
            {
                return -1;
            }

            if (temp.ID.Substring(0, 1) == "S")
            {
                return -1;
            }

            this.privDept = temp;

            this.ShowInOutDept();

            return base.OnSetValue(neuObject, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCell = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
                markNumCell.DecimalPlaces = 0;
                this.fpInSheet.Columns[(int)ColumnSet.ColSortID].CellType = markNumCell;
                this.fpOutSheet.Columns[(int)ColumnSet.ColSortID].CellType = markNumCell;

                tvDepartmentLevelTree tvLevel = this.tv as tvDepartmentLevelTree;

                if (tvLevel != null)
                {
                    tvLevel.HideSelection = false;

                    tvLevel.BeforeLoad(this.statCode);

                    tvLevel.ExpandAll();
                }
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// ������ʾ��ʽ {CFC740A1-77C6-4722-A6BF-DCDC94171838} by nxy
        /// </summary>
        private void SetColumnFormat()
        {
            FarPoint.Win.Spread.CellType.TextCellType textType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.fpInSheet.Columns[(int)ColumnSet.ColDeptID].CellType = textType;

            this.fpOutSheet.Columns[(int)ColumnSet.ColDeptID].CellType = textType;
        }
        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            ColSortID,
            ColDeptName,
            ColeDeptType,
            ColMemo,
            ColDeptID
        }      
    }
}
