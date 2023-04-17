using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Admin;


namespace FS.HISFC.Components.Pharmacy.MonthStore
{
    /// <summary>
    /// [��������: ҩƷ�Զ����½�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-07-30]<br></br>
    /// </summary>
    public partial class ucMSCustomManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMSCustomManager()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ��������
        /// </summary>
        private EnumDeptType deptTypeEnum = EnumDeptType.ҩ��;

        /// <summary>
        /// ��֧����
        /// </summary>
        private string[] addFlagStrCollection = new string[] { "����","֧��"};

        /// <summary>
        /// ҵ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = null;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// �����Ұ�������
        /// </summary>
        private System.Collections.Hashtable hsPrivInDept = new Hashtable();

        /// <summary>
        /// �����Ҽ���
        /// </summary>
        private string[] privInStrCollection = null;

        /// <summary>
        /// ������Ұ�������
        /// </summary>
        private System.Collections.Hashtable hsPrivOutDept = new Hashtable();

        /// <summary>
        /// ������Ҽ���
        /// </summary>
        private string[] privOutStrCollection = null;

        /// <summary>
        /// �������Ȩ�޼���
        /// </summary>
        private string[] privInC3StrCollection = null;

        /// <summary>
        /// �������Ȩ�ް�������
        /// </summary>
        private System.Collections.Hashtable hsPrivInC3 = new Hashtable();

        /// <summary>
        /// ��������Ȩ�޼���
        /// </summary>
        private string[] privOutC3StrCollection = null;

        /// <summary>
        /// ��������Ȩ�ް�������
        /// </summary>
        private System.Collections.Hashtable hsPrivOutC3 = new Hashtable();

        /// <summary>
        /// �̵�ӯ������
        /// </summary>
        private string[] strCheckCollection = new string[] { "�̵� - �̿�", "�̵� - ��ӯ" };

        /// <summary>
        /// ����ӯ������
        /// </summary>
        private string[] strAdjustCollection = new string[] { "���� - ����", "���� - ��ӯ" };
    
        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        [Description("�Զ����½���Ŀ��������"),Category("����")]
        public EnumDeptType DeptTypeEnum
        {
            get
            {
                return this.deptTypeEnum;
            }
            set
            {
                this.deptTypeEnum = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        protected FS.HISFC.Models.Base.EnumDepartmentType DeptType
        {
            get
            {
                if (this.deptTypeEnum == EnumDeptType.ҩ��)
                {
                    return FS.HISFC.Models.Base.EnumDepartmentType.PI;
                }
                else
                {
                    return FS.HISFC.Models.Base.EnumDepartmentType.P;
                }
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "�½��ƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
           
            toolBarService.AddToolButton("ɾ��", "ɾ����ǰѡ��ļƻ�ҩƷ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.DelData();
            }
            if (e.ClickedItem.Text == "����")
            {
                this.AddNewData();
            }
           
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.SaveMSCustom() == 1)
            {
                this.ShowMSCustomManager();
            }

            return 1;
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void Init()
        {
            FarPoint.Win.Spread.CellType.ComboBoxCellType addFlagCmbType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            addFlagCmbType.Items = this.addFlagStrCollection;

            this.neuSpread1_Sheet1.Columns[6].CellType = addFlagCmbType;

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            this.privDept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;

            FS.HISFC.BizProcess.Integrate.Manager integrateManager = new FS.HISFC.BizProcess.Integrate.Manager();
            
            ArrayList aldept = integrateManager.GetDepartment();
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(aldept);

            int iIndex = 0;

            #region ��ȡ���������б�

            FS.HISFC.BizLogic.Manager.PrivInOutDept privInOutManager = new FS.HISFC.BizLogic.Manager.PrivInOutDept();

            ArrayList alPrivInDept = privInOutManager.GetPrivInOutDeptList(this.privDept.ID, "0310");
            if (alPrivInDept == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ǰ�����������б�������") + privInOutManager.Err);
                return;
            }

            this.privInStrCollection = new string[alPrivInDept.Count];
            iIndex = 0;
            foreach (FS.HISFC.Models.Base.PrivInOutDept privInInfo in alPrivInDept)
            {
                this.privInStrCollection[iIndex] = "���� �� " + privInInfo.Dept.Name;

                this.hsPrivInDept.Add(this.privInStrCollection[iIndex], privInInfo.Dept.ID);

                iIndex++;
            }
            
            ArrayList alPrivOutDept = privInOutManager.GetPrivInOutDeptList(this.privDept.ID, "0320");
            if (alPrivOutDept == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ǰ���ҳ�������б�������") + privInOutManager.Err);
                return;
            }
            this.privOutStrCollection = new string[alPrivOutDept.Count];
            iIndex = 0;
            foreach (FS.HISFC.Models.Base.PrivInOutDept privOutInfo in alPrivOutDept)
            {
                this.privOutStrCollection[iIndex] = "���� �� " + privOutInfo.Dept.Name;

                this.hsPrivOutDept.Add(this.privOutStrCollection[iIndex], privOutInfo.Dept.ID);

                iIndex++;
            }

            #endregion

            #region �����Ȩ�޼���

            FS.HISFC.BizLogic.Manager.PowerLevelManager powerManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
            
            ArrayList alPrivInC3 = powerManager.LoadLevel3ByLevel2("0310");
            if (alPrivInC3 == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ǰ�������Ȩ�����ͷ�������") + powerManager.Err);
                return;
            }
            this.privInC3StrCollection = new string[alPrivInC3.Count];
            iIndex = 0;
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 privInC3 in alPrivInC3)
            {
                this.privInC3StrCollection[iIndex] = "��� �� " + privInC3.Name;

                this.hsPrivInC3.Add(this.privInC3StrCollection[iIndex], privInC3.ID);

                iIndex++;
            }

            ArrayList alPrivOutC3 = powerManager.LoadLevel3ByLevel2("0320");
            if (alPrivOutC3 == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ǰ��������Ȩ�����ͷ�������") + powerManager.Err);
                return;
            }
            this.privOutC3StrCollection = new string[alPrivOutC3.Count];
            iIndex = 0;
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 privOutC3 in alPrivOutC3)
            {
                this.privOutC3StrCollection[iIndex] = "���� �� " + privOutC3.Name;

                this.hsPrivOutC3.Add(this.privOutC3StrCollection[iIndex], privOutC3.ID);

                iIndex++;
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// ��Fp�ڼ�������
        /// </summary>
        /// <param name="msCustomLis">ҩƷ������Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int AddDataToFp(List<FS.HISFC.Models.Pharmacy.MSCustom> msCustomLis)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.MSCustom info in msCustomLis)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.CustomItem.ID;
                this.neuSpread1_Sheet1.Cells[0, 1].Text = info.CustomItem.Name;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = FS.HISFC.Models.Base.EnumMSCustomTypeService.GetNameFromEnum(info.CustomType);
                this.neuSpread1_Sheet1.Cells[0, 3].Text = info.CustomType.ToString();
                this.neuSpread1_Sheet1.Cells[0, 6].Text = info.Trans == FS.HISFC.Models.Base.TransTypes.Positive ? "����" : "֧��";

                this.neuSpread1_Sheet1.Cells[0, 4].Text = info.TypeItem;

                FarPoint.Win.Spread.CellType.ComboBoxCellType cmbCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                cmbCellType.Items = this.GetDescriptionFromType(info.CustomType, info.Trans);

                this.neuSpread1_Sheet1.Cells[0, 5].CellType = cmbCellType;
                this.neuSpread1_Sheet1.Cells[0, 5].Text = this.GetDescriptionFromTypeItem(info.CustomType, info.TypeItem,info.Trans);
                
                this.neuSpread1_Sheet1.Cells[0, 7].Text = info.ID;

                this.neuSpread1_Sheet1.Rows[0].Tag = info;
            }

            return 1;
        }

        /// <summary>
        /// ��Fp�ڻ�ȡ����
        /// </summary>
        /// <param name="iRowIndex">������</param>
        /// <returns></returns>
        protected FS.HISFC.Models.Pharmacy.MSCustom GetDataFormFp(int iRowIndex)
        {
            FS.HISFC.Models.Pharmacy.MSCustom msCustom = this.neuSpread1_Sheet1.Rows[iRowIndex].Tag as FS.HISFC.Models.Pharmacy.MSCustom;

            msCustom.DeptType = this.DeptType;

            msCustom.CustomItem.ID = this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text;
            msCustom.CustomItem.Name = this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text;

            msCustom.ItemOrder = FS.FrameWork.Function.NConvert.ToInt32(msCustom.CustomItem.ID);

            msCustom.CustomType = FS.HISFC.Models.Base.EnumMSCustomTypeService.GetEnumFromName(this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text);
            msCustom.TypeItem = this.neuSpread1_Sheet1.Cells[iRowIndex, 4].Text;
            msCustom.Trans = this.neuSpread1_Sheet1.Cells[iRowIndex, 6].Text == "֧��" ? FS.HISFC.Models.Base.TransTypes.Negative : FS.HISFC.Models.Base.TransTypes.Positive;

            return msCustom;
        }

        /// <summary>
        /// ������Ŀ��𡢷������ݻ�ȡ������Ϣ
        /// </summary>
        /// <param name="customType">����</param>
        /// <param name="typeItem">��������</param>
        /// <returns></returns>
        protected string GetDescriptionFromTypeItem(EnumMSCustomType customType,string typeItem,TransTypes trans)
        {
            switch (customType)
            {
                case FS.HISFC.Models.Base.EnumMSCustomType.���:       //����������ͼ���                         
                case FS.HISFC.Models.Base.EnumMSCustomType.����:       //���ݳ������ͼ���
                    FS.HISFC.BizLogic.Manager.PowerLevelManager powerManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();

                    PowerLevelClass3 pIn3 = powerManager.LoadLevel3ByPrimaryKey(FS.HISFC.Models.Base.EnumMSCustomTypeService.GetNameFromEnum(customType), typeItem);

                    return customType.ToString() + " �� " + pIn3.Name;
                case FS.HISFC.Models.Base.EnumMSCustomType.����:       //���� ������������ѡ��
                    FS.HISFC.BizLogic.Manager.PrivInOutDept privInOutManager = new FS.HISFC.BizLogic.Manager.PrivInOutDept();

                    return "���� �� " + this.deptHelper.GetName(typeItem);   
                case FS.HISFC.Models.Base.EnumMSCustomType.����:
                    if (typeItem == "00")
                    {
                        return this.strAdjustCollection[0];
                    }
                    else
                    {
                        return this.strAdjustCollection[1];
                    }
                case FS.HISFC.Models.Base.EnumMSCustomType.�̵�:       //�����̵�ӯ��
                    if (typeItem == "00")
                    {
                        return this.strCheckCollection[0];
                    }
                    else
                    {
                        return this.strCheckCollection[1];
                    }              
                default:
                    return customType.ToString();
            }
        }

        /// <summary>
        /// ������Ŀ������ò�ͬ�ķ�������
        /// </summary>
        /// <param name="customType">��Ŀ����</param>
        /// <returns></returns>
        protected string[] GetDescriptionFromType(EnumMSCustomType customType,TransTypes trnas)
        {
            switch (customType)
            {
                case FS.HISFC.Models.Base.EnumMSCustomType.���:       //����������ͼ���  

                    return this.privInC3StrCollection;
                case FS.HISFC.Models.Base.EnumMSCustomType.����:       //���ݳ������ͼ���

                    return this.privOutC3StrCollection;
                case FS.HISFC.Models.Base.EnumMSCustomType.����:       //���� ������������ѡ��
                    if (trnas == TransTypes.Positive)                       //������ ȡ������
                    {
                        return this.privInStrCollection;
                    }
                    else                                                    //������ ȡ�������
                    {
                        return this.privOutStrCollection;
                    }

                case FS.HISFC.Models.Base.EnumMSCustomType.����:

                    return this.strAdjustCollection;
                case FS.HISFC.Models.Base.EnumMSCustomType.�̵�:       //�����̵�ӯ��

                    return this.strCheckCollection;
                default:
                    return new string[] { customType.ToString() };
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        protected int AddNewData()
        {
            int rowCount = this.neuSpread1_Sheet1.Rows.Count;

            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);

            FS.HISFC.Models.Pharmacy.MSCustom msCustom = new FS.HISFC.Models.Pharmacy.MSCustom();
            msCustom.Trans = TransTypes.Positive;

            if (rowCount > 0)
            {
                this.neuSpread1_Sheet1.Cells[rowCount, 6].Text = this.neuSpread1_Sheet1.Cells[rowCount - 1, 6].Text;
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[rowCount, 6].Text = this.addFlagStrCollection[0];
            }

            this.neuSpread1_Sheet1.Rows[rowCount].Tag = msCustom;
                        
            return 1;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <returns></returns>
        public int DelData()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return -1;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("�Ƿ�ȷ��ɾ����ǰѡ���������Ϣ"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
            {
                return -1;
            }

            int iIndex = this.neuSpread1_Sheet1.ActiveRowIndex;

            if (this.neuSpread1_Sheet1.Cells[iIndex, 7].Text != "")
            {
                if (this.consManager.DelMSCustom(this.neuSpread1_Sheet1.Cells[iIndex, 7].Text) == -1)
                {
                    MessageBox.Show(Language.Msg("����ɾ��ʧ��") + this.consManager.Err);
                    return -1;
                }
            }

            this.neuSpread1_Sheet1.Rows.Remove(iIndex, 1);

            MessageBox.Show(Language.Msg("ɾ���ɹ�"));
            return 1;
        }

        /// <summary>
        /// ������ά���Զ����½�������Ϣ
        /// </summary>
        /// <returns></returns>
        public int ShowMSCustomManager()
        {
            List<FS.HISFC.Models.Pharmacy.MSCustom> msCustomLis = this.consManager.QueryMSCustom(this.DeptType);

            if (msCustomLis == null)
            {
                MessageBox.Show(Language.Msg("������ά���Զ����½�������Ϣ��������"));
                return -1;
            }

            this.AddDataToFp(msCustomLis);

            return 1;
        }

        /// <summary>
        /// ����ѡ��
        /// </summary>
        /// <param name="iRow">������</param>
        protected int PopMSCustomType(int iRow)
        {
            ArrayList al = FS.HISFC.Models.Base.EnumMSCustomTypeService.List();

            FS.FrameWork.Models.NeuObject typeObj = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref typeObj) == 0)
            {
                return -1;
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[iRow,2].Text = typeObj.Name;
                this.neuSpread1_Sheet1.Cells[iRow,3].Text = typeObj.ID;

                this.SetTypeItem(iRow);
            }

            return 1;
        }

        /// <summary>
        /// ����Fp��������
        /// </summary>
        /// <param name="iRow"></param>
        private void SetTypeItem(int iRow)
        {
            EnumMSCustomType customType = EnumMSCustomTypeService.GetEnumFromName(this.neuSpread1_Sheet1.Cells[iRow, 2].Text);

            TransTypes trans = this.neuSpread1_Sheet1.Cells[iRow, 6].Text.Trim() == "����" ? TransTypes.Positive : TransTypes.Negative;

            FarPoint.Win.Spread.CellType.ComboBoxCellType typeItemCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

            typeItemCellType.Items = this.GetDescriptionFromType(customType, trans);
            this.neuSpread1_Sheet1.Cells[iRow, 5].CellType = typeItemCellType;
        }

        /// <summary>
        /// �ж��Ƿ�������,�Ƿ�����ȫ��Ҫ����Ϣ
        /// </summary>
        /// <returns>�ɹ�����True ʧ�ܷ���False</returns>
        protected bool IsValid()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text == "")
                {
                    MessageBox.Show(Language.Msg("�����õ�" + (i+1).ToString() + "����Ŀ����,�ñ���ͬʱ��־��ϲ������Ŀ˳��"));
                    return false;
                }
                if (this.neuSpread1_Sheet1.Cells[i, 1].Text == "")
                {
                    MessageBox.Show(Language.Msg("�����õ�" + (i + 1).ToString() + "����Ŀ����,������ͬʱ��־������Ӧ������"));
                    return false;
                }
                if (this.neuSpread1_Sheet1.Cells[i, 3].Text == "")
                {
                    MessageBox.Show(Language.Msg("�����õ�" + (i + 1).ToString() + "����Ŀ����"));
                    return false;
                }
                if (this.neuSpread1_Sheet1.Cells[i, 5].Text == "")
                {
                    MessageBox.Show(Language.Msg("�����õ�" + (i + 1).ToString() + "�з�������"));
                    return false;
                }
                if (this.neuSpread1_Sheet1.Cells[i,6].Text == "")
                {
                    MessageBox.Show(Language.Msg("�����õ�" + (i + 1).ToString() + "����֧���"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ���ݱ���
        /// </summary>
        /// <returns></returns>
        public int SaveMSCustom()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return 0;
            }

            if (!this.IsValid())
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.consManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.consManager.DelMSCustom(this.DeptType) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("����ǰɾ��ԭ�������ݷ�������") + this.consManager.Err);
                return -1;
            }

            DateTime sysTime = this.consManager.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.MSCustom msCustom = this.GetDataFormFp(i);

                msCustom.Oper.OperTime = sysTime;
                msCustom.Oper.ID = this.consManager.Operator.ID;

                if (this.consManager.InsertMSCustom(msCustom) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����������ݷ�������") + this.consManager.Err);
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("����ɹ�"));

            return 1;
        }

        /// <summary>
        /// �Զ����½����ѡ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.ShowMSCustomManager();
            }

            base.OnLoad(e);
        }        

        #region ö��

        /// <summary>
        /// �������
        /// </summary>
        public enum EnumDeptType
        {
            ҩ��,
            ҩ��
        }

        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 3)
            {
                this.PopMSCustomType(e.Row);
            }
        }

        private void neuSpread1_ComboCloseUp(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 5 )
            {
                #region ����ѡ�����Ŀ��ȡ����

                EnumMSCustomType customType = EnumMSCustomTypeService.GetEnumFromName(this.neuSpread1_Sheet1.Cells[e.Row, 2].Text);

                TransTypes trans = this.neuSpread1_Sheet1.Cells[e.Row, 6].Text.Trim() == "����" ? TransTypes.Positive : TransTypes.Negative;

                string keys = this.neuSpread1_Sheet1.Cells[e.Row,5].Text;
                string typeItemID = "";
                switch (customType)
                {
                    case FS.HISFC.Models.Base.EnumMSCustomType.���:       //����������ͼ���  
                        if (this.hsPrivInC3.ContainsKey(keys))
                        {
                            typeItemID = this.hsPrivInC3[keys].ToString();
                        }
                        break;
                    case FS.HISFC.Models.Base.EnumMSCustomType.����:       //���ݳ������ͼ���
                        if (this.hsPrivOutC3.ContainsKey(keys))
                        {
                            typeItemID = this.hsPrivOutC3[keys].ToString();
                        }
                        break;
                    case FS.HISFC.Models.Base.EnumMSCustomType.����:       //���� ������������ѡ��
                        if (trans == TransTypes.Positive)                       //������ ȡ������
                        {
                            if (this.hsPrivInDept.ContainsKey(keys))
                            {
                                typeItemID = this.hsPrivInDept[keys].ToString();
                            }
                        }
                        else                                                    //������ ȡ�������
                        {
                            if (this.hsPrivOutDept.ContainsKey(keys))
                            {
                                typeItemID = this.hsPrivOutDept[keys].ToString();
                            }
                        }
                        break;
                    case FS.HISFC.Models.Base.EnumMSCustomType.����:
                        typeItemID = keys == this.strAdjustCollection[0] ? "00" : "01";
                        break;
                    case FS.HISFC.Models.Base.EnumMSCustomType.�̵�:       //�����̵�ӯ��
                        typeItemID = keys == this.strCheckCollection[0] ? "00" : "01";
                        break;
                    case EnumMSCustomType.���ﻼ����ҩ:
                        typeItemID = "M1";
                        break;
                    case EnumMSCustomType.���ﻼ����ҩ:
                        typeItemID = "M2";
                        break;
                    case EnumMSCustomType.סԺ������ҩ:
                        typeItemID = "Z1";
                        break;
                    case EnumMSCustomType.סԺ������ҩ:
                        typeItemID = "Z2";
                        break;
                    case EnumMSCustomType.С��:
                        typeItemID = "SUB";
                        break;
                }

                this.neuSpread1_Sheet1.Cells[e.Row, 4].Text = typeItemID;

                #endregion
            }
            if (e.Column == 6)      //��֧���
            {
                this.SetTypeItem(e.Row);
            }   
        }
    }
}
