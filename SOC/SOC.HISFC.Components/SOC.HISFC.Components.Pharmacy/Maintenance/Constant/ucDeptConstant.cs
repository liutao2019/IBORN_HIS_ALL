using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    /// <summary>
    /// [��������: ���ҿ�泣��ά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-03]<br></br>
    /// </summary>
    public partial class ucDeptConstant : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDeptConstant()
        {
            InitializeComponent();
        }

        #region ˵��

        /*
         *  1����������(��һ����ҩ����ҩ��)��Ҫά�����ų���ʱ �����ڿ��ҽṹ�ڶ�03���������
         * 
         *  2��Sql
         *      SELECT  
				PHA_COM_DEPT.DEPT_CODE,                              --���ű���
				COM_DEPTSTAT.DEPT_NAME,				     --��������
				PHA_COM_DEPT.STORE_MAX_DAYS,                         --�ⷿ��߿����(��)
				PHA_COM_DEPT.STORE_MIN_DAYS,                         --�ⷿ��Ϳ����(��)
				PHA_COM_DEPT.REFERENCE_DAYS,                         --�ο�����
				PHA_COM_DEPT.BATCH_FLAG,                             --�Ƿ����Ź���ҩƷ
				PHA_COM_DEPT.STORE_FLAG,                             --�Ƿ����ҩƷ���
				PHA_COM_DEPT.UNIT_FLAG,                              --������ʱĬ�ϵĵ�λ��1��װ��λ��0��С��λ
				PHA_COM_DEPT.OPER_CODE,                              --����Ա����
				PHA_COM_DEPT.OPER_DATE                               --����ʱ��
			FROM 	PHA_COM_DEPT,
				COM_DEPTSTAT  
			WHERE 	PHA_COM_DEPT.PARENT_CODE  = COM_DEPTSTAT.PARENT_CODE 
			AND  	PHA_COM_DEPT.CURRENT_CODE = COM_DEPTSTAT.CURRENT_CODE 
			AND   	COM_DEPTSTAT.STAT_CODE = '03' 
			AND   	COM_DEPTSTAT.DEPT_CODE = PHA_COM_DEPT.DEPT_CODE 
			AND   	PHA_COM_DEPT.PARENT_CODE  =  fun_get_parentcode  
			AND  	PHA_COM_DEPT.CURRENT_CODE =  fun_get_currentcode  
         * 
         */

        #endregion

        #region �����

        /// <summary>
        /// ҵ�������
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// �Ƿ�������ÿ�����λ
        /// </summary>
        private bool isManagerUnitFlag = false;

        /// <summary>
        /// �Ƿ�����������Ź���
        /// </summary>
        private bool isManagerBatch = true;

        /// <summary>
        /// �Ƿ�������ÿ�����
        /// </summary>
        private bool isManagerStore = true;

        /// <summary>
        /// �Ƿ�������ÿ�澯���߲���
        /// </summary>
        private bool isManagerParam = true;

        /// <summary>
        /// �Ƿ��������ҩ������־
        /// </summary>
        private bool isManagerArk = false;

        /// <summary>
        /// ��ά���Ŀ��ҿ���б�
        /// </summary>
        private System.Collections.Hashtable hsPhaDept = new Hashtable();

        private System.Collections.Hashtable hsDeptControlDrugType = new Hashtable();

        /// <summary>
        /// �Ƿ�����������ⵥ��
        /// </summary>
        private bool isManagerInListNO = false;

        /// <summary>
        /// �Ƿ��������ó��ⵥ��
        /// </summary>
        private bool isManagerOutListNO = false;
        #endregion

        #region ����

        /// <summary>
        /// �Ƿ�������ÿ�����λ
        /// </summary>
        [Description("�Ƿ�������ÿ�����λ"),Category("����"),DefaultValue(true)]
        public bool IsManagerUnitFlag
        {
            get
            {
                return isManagerUnitFlag;
            }
            set
            {
                isManagerUnitFlag = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUnitFlag].Visible = value;                
            }
        }

        /// <summary>
        /// �Ƿ�����������Ź���
        /// </summary>
        [Description("�Ƿ�����������Ź���"), Category("����"), DefaultValue(true)]
        public bool IsManagerBatch
        {
            get
            {
                return isManagerBatch;
            }
            set
            {
                isManagerBatch = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsBatch].Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ�������ÿ�����
        /// </summary>
        [Description("�Ƿ�������ÿ�����"), Category("����"), DefaultValue(true)]
        public bool IsManagerStore
        {
            get
            {
                return isManagerStore;
            }
            set
            {
                isManagerStore = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsStore].Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ�������ÿ�澯���߲���
        /// </summary>
        [Description("�Ƿ�������ÿ�澯���߲���"), Category("����"), DefaultValue(true)]
        public bool IsManagerParam
        {
            get
            {
                return isManagerParam;
            }
            set
            {
                isManagerParam = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMaxDays].Visible = value;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMinDays].Visible = value;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColReferenceDays].Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ��������ҩ�����
        /// </summary>
        [Description("�Ƿ��������ҩ�����"), Category("����"), DefaultValue(true)]
        public bool IsManagerArk
        {
            get
            {
                return this.isManagerArk;
            }
            set
            {
                this.isManagerArk = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColIsArk].Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ�����������ⵥ��
        /// </summary>
        [Description("�Ƿ�����������ⵥ��"), Category("����"), DefaultValue(true)]
        public bool IsManagerInListNO
        {
            get
            {
                return this.isManagerInListNO;
            }
            set
            {
                this.isManagerInListNO = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColInListNO].Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ��������ó��ⵥ��
        /// </summary>
        [Description("�Ƿ��������ó��ⵥ��"), Category("����"), DefaultValue(true)]
        public bool IsManagerOutListNO
        {
            get
            {
                return this.isManagerOutListNO;
            }
            set
            {
                this.isManagerOutListNO = value;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOutListNO].Visible = value;
            }
        }

        private string privePowerString = "0300";

        [Description("ʹ�ô�����Ҫ��Ȩ��,�磺0300"), Category("����"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {          
            return toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.SaveDeptCons();
        }

        #endregion

        /// <summary>
        /// ��ʾ�����б�
        /// </summary>
        private int ShowDeptList()
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            //ȡ���ҳ�����Ϣ
            //�����ά����ҩ��Ȩ�޹�����Ŀ��ң�ҩ�����Ȩ��һ������03
            ArrayList alPharmacyStatDept = this.phaConsManager.QueryPharmacyStatList();
            if (alPharmacyStatDept == null)
            {
                MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                return -1;
            }
            if (alPharmacyStatDept.Count == 0)
            {
                Function.ShowMessage("ϵͳ��û��ά��ҩ�����Ȩ�ޣ�����ϵͳ����Ա��ϵ��", MessageBoxIcon.Information);
                return 0;
            }
            ArrayList alMaintenanced = this.phaConsManager.QueryDeptConstantList();
            if (alMaintenanced == null)
            {
                MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                return -1;
            }
            if (alMaintenanced.Count == 0)
            {
                Function.ShowMessage("ϵͳ��û��ά��ҩ�����Ȩ�ޣ�����ϵͳ����Ա��ϵ��", MessageBoxIcon.Information);
                return 0;
            }
            try
            {
                this.hsPhaDept.Clear();

                FS.HISFC.Models.Pharmacy.DeptConstant deptConstant = null;
                this.neuSpread1_Sheet1.RowCount = 0;

                int iCount = 0;

                for (int i = 0; i < alMaintenanced.Count; i++)
                {                  
                    deptConstant = alMaintenanced[i] as FS.HISFC.Models.Pharmacy.DeptConstant;

                    this.hsPhaDept.Add(deptConstant.ID,null);

                    if (deptConstant.ID.Substring(0, 1) == "S")
                    {
                        continue;
                    }
                   
                    this.neuSpread1_Sheet1.Rows.Add(iCount, 1);

                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptID].Value = deptConstant.ID;			//���ű���
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptName].Value = deptConstant.Name;			//��������
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsStore].Value = deptConstant.IsStore;		//�Ƿ������
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsBatch].Value = deptConstant.IsBatch;		//�Ƿ����Ź���
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColUnitFlag].Value = deptConstant.UnitFlag == "1" ? "��װ��λ" : "��С��λ";//������Ĭ�ϵ�λ:0��С��λ,1��װ��λ
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMaxDays].Value = deptConstant.StoreMaxDays;	//�����������
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMinDays].Value = deptConstant.StoreMinDays;	//�����������
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColReferenceDays].Value = deptConstant.ReferenceDays;//�ο�����
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsArk].Value = deptConstant.IsArk;            //ҩ������־ �Ƿ�ҩ�����
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColInListNO].Value = deptConstant.InListNO;
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColOutListNO].Value = deptConstant.OutListNO;

                    iCount++;
                }

                for (int i = 0; i < alPharmacyStatDept.Count; i++)
                {
                    deptConstant = alPharmacyStatDept[i] as FS.HISFC.Models.Pharmacy.DeptConstant;
                    if (hsPhaDept.Contains(deptConstant.ID))
                    {
                        continue;
                    }

                    if (deptConstant.ID.Substring(0, 1) == "S")
                    {
                        continue;
                    }

                    this.hsPhaDept.Add(deptConstant.ID, null);

                    this.neuSpread1_Sheet1.Rows.Add(iCount, 1);

                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptID].Value = deptConstant.ID;			//���ű���
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptName].Value = deptConstant.Name;			//��������
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsStore].Value = deptConstant.IsStore;		//�Ƿ������
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsBatch].Value = deptConstant.IsBatch;		//�Ƿ����Ź���
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColUnitFlag].Value = deptConstant.UnitFlag == "1" ? "��װ��λ" : "��С��λ";//������Ĭ�ϵ�λ:0��С��λ,1��װ��λ
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMaxDays].Value = deptConstant.StoreMaxDays;	//�����������
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColMinDays].Value = deptConstant.StoreMinDays;	//�����������
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColReferenceDays].Value = deptConstant.ReferenceDays;//�ο�����
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsArk].Value = deptConstant.IsArk;            //ҩ������־ �Ƿ�ҩ�����
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColInListNO].Value = deptConstant.InListNO;
                    this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColOutListNO].Value = deptConstant.OutListNO;

                    iCount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ȡ���ҽṹ��Ϣ
        /// </summary>
        /// <returns></returns>
        private int ShowDeptStat()
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDeptStat = deptStatManager.LoadDepartmentStat("03");
            if (alDeptStat == null)
            {
                MessageBox.Show(Language.Msg("��ȡ���ҽڵ���Ϣʧ��"));
                return -1;
            }

            foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alDeptStat)
            {
                if (this.hsPhaDept.ContainsKey(deptStat.DeptCode))
                {
                    continue;
                }

                if (deptStat.DeptCode.Substring(0, 1) == "S")
                {
                    continue;
                }

                FS.HISFC.Models.Pharmacy.DeptConstant deptConstant = new FS.HISFC.Models.Pharmacy.DeptConstant();

                deptConstant.ID = deptStat.DeptCode;
                deptConstant.Name = deptStat.DeptName;

                int iCount = this.neuSpread1_Sheet1.Rows.Count;

                this.neuSpread1_Sheet1.Rows.Add(iCount, 1);

                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptID].Value = deptConstant.ID;			//���ű���
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColDeptName].Value = deptConstant.Name;			//��������        

                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsStore].Value = false;
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsBatch].Value = false ;		//�Ƿ����Ź���
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColUnitFlag].Value = "��С��λ";//������Ĭ�ϵ�λ:0��С��λ,1��װ��λ
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColIsArk].Value = false;            //ҩ������־ �Ƿ�ҩ�����

                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColInListNO].Value = "";            //ҩ������־ �Ƿ�ҩ�����
                this.neuSpread1_Sheet1.Cells[iCount, (int)ColumnSet.ColOutListNO].Value = "";            //ҩ������־ �Ƿ�ҩ�����

            }
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        private int SaveDeptCons()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show(Language.Msg("û�п��Ա��������"));
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            phaConsManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Pharmacy.DeptConstant deptConstant = null;

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                deptConstant = new FS.HISFC.Models.Pharmacy.DeptConstant();

                deptConstant.ID = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDeptID].Text;			                                //���ű���
                deptConstant.Name = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDeptName].Text;			                            //��������
                deptConstant.IsStore = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColIsStore].Value.ToString());		//�Ƿ������
                deptConstant.IsBatch = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColIsBatch].Value.ToString());		//�Ƿ����Ź���
                deptConstant.UnitFlag = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColUnitFlag].Text.ToString() == "��װ��λ" ? "1" : "0";//������Ĭ�ϵ�λ:0��С��λ,1��װ��λ
                deptConstant.StoreMaxDays = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColMaxDays].Text);              //�����������
                deptConstant.StoreMinDays = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColMinDays].Text);              //�����������
                deptConstant.ReferenceDays = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColReferenceDays].Text);       //�ο�����
                deptConstant.IsArk = NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColIsArk].Value.ToString());         //�Ƿ�ҩ�����
                //{849BBA57-0A27-4e6b-BC8C-C92A9B9B325F}
                //deptConstant.InListNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColInListNO].Value.ToString();
                //deptConstant.OutListNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColOutListNO].Value.ToString();
                try
                {
                    deptConstant.InListNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColInListNO].Value.ToString();
                }
                catch (Exception)
                {

                    deptConstant.InListNO = "";
                }

                try
                {
                    deptConstant.OutListNO = this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColOutListNO].Value.ToString();
                }
                catch (Exception)
                {

                    deptConstant.OutListNO = "";
                }

                int parm = this.phaConsManager.UpdateDeptConstant(deptConstant);
                if (parm == 0)
                {                    
                        if (this.phaConsManager.InsertDeptConstant(deptConstant) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                            return -1;
                        }
                }
                else if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                    return -1;
                }
            }

            if (this.phaConsManager.DeleteControlDrugAttribute("D", this.sheetView1.Cells[0, 3].Text, "T") == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                return -1;
            }
            for (int i = 0; i < this.sheetView1.RowCount; i++)
            {
                FS.SOC.HISFC.Models.Pharmacy.Constant.ControlAttribute ca = new FS.SOC.HISFC.Models.Pharmacy.Constant.ControlAttribute();
                ca.ObjectCode = this.sheetView1.Cells[i, 3].Text;
                ca.ObjectType = "D";
                ca.AttributeCode = this.sheetView1.Cells[i, 1].Text;
                ca.AttributeType = "T";
                ca.ValidState = (FS.FrameWork.Function.NConvert.ToBoolean(this.sheetView1.Cells[i, 0].Value) ? "1" : "0");
                ca.OperID = this.phaConsManager.Operator.ID;
                ca.OperTime = this.phaConsManager.GetDateTimeFromSysDateTime();
                if (this.phaConsManager.InsetControlDrugAttribute(ca) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(this.phaConsManager.Err));
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����ɹ�"));

            return 1;
        }

        private int ShowControlDrugAttribute(string deptNO)
        {
            hsDeptControlDrugType.Clear();
            SOC.HISFC.BizProcess.Cache.Common.InitDrugType();
            this.sheetView1.RowCount = 0;
            this.sheetView1.RowCount = SOC.HISFC.BizProcess.Cache.Common.drugTypeHelper.ArrayObject.Count;
            int rowIndex = 0;

            ArrayList alAttribute = this.phaConsManager.QueryControlDrugAttribute("D", "T");
            if (alAttribute == null)
            {
                MessageBox.Show("��ȡ��ҩ���Է�������" + this.phaConsManager.Err);
                return -1;
            }

            foreach (FS.SOC.HISFC.Models.Pharmacy.Constant.ControlAttribute controlAttribute in alAttribute)
            {
                if (!hsDeptControlDrugType.Contains(controlAttribute.ObjectCode + controlAttribute.AttributeCode))
                {
                    FS.FrameWork.Models.NeuObject deptControlDrugType = new FS.FrameWork.Models.NeuObject();
                    deptControlDrugType.ID = controlAttribute.AttributeCode;
                    deptControlDrugType.Name = SOC.HISFC.BizProcess.Cache.Common.GetDrugTypeName(controlAttribute.AttributeCode);
                    deptControlDrugType.Memo = (controlAttribute.ValidState == "1" ? "1" : "0");
                    hsDeptControlDrugType.Add(controlAttribute.ObjectCode + controlAttribute.AttributeCode, deptControlDrugType);
                }

            }

            foreach (FS.FrameWork.Models.NeuObject drugType in SOC.HISFC.BizProcess.Cache.Common.drugTypeHelper.ArrayObject)
            {
                FS.FrameWork.Models.NeuObject deptControlDrugType = new FS.FrameWork.Models.NeuObject();
                if (hsDeptControlDrugType.Contains(deptNO + drugType.ID))
                {
                    deptControlDrugType = hsDeptControlDrugType[deptNO + drugType.ID] as FS.FrameWork.Models.NeuObject;
                }
                else
                {
                    deptControlDrugType.ID = drugType.ID;
                    deptControlDrugType.Name = drugType.Name;
                    deptControlDrugType.Memo = "0";
                    hsDeptControlDrugType.Add(deptNO + drugType.ID, deptControlDrugType);
                }

                this.sheetView1.Cells[rowIndex, 0].Value = FS.FrameWork.Function.NConvert.ToBoolean(deptControlDrugType.Memo);
                this.sheetView1.Cells[rowIndex, 1].Text = drugType.ID;
                this.sheetView1.Cells[rowIndex, 2].Text = drugType.Name;
                this.sheetView1.Cells[rowIndex, 3].Text = deptNO;
                this.sheetView1.Cells[rowIndex, 4].Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptNO);
                rowIndex++;
            }
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {


                this.ShowDeptList();

                //this.ShowDeptStat();

                this.neuSpread1.CellClick+=new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellClick);
                this.sheetView1.RowCount = 0;
                this.sheetView1.Columns.Get(0).CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.sheetView1.Columns.Get(1).CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.sheetView1.Columns.Get(1).Width = 0;
                this.sheetView1.Columns.Get(2).CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.sheetView1.Columns.Get(3).CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.sheetView1.Columns.Get(3).Width = 0;
                this.sheetView1.Columns.Get(4).CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1_Sheet1.ActiveRowIndex = -1;
            }

            base.OnLoad(e);
        }

        void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string deptNO = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDeptID].Text;			                                //���ű���
            this.ShowControlDrugAttribute(deptNO);
        }


        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// ��������
            /// </summary>
            ColDeptName,
            /// <summary>
            /// �Ƿ������
            /// </summary>
            ColIsStore,
            /// <summary>
            /// �Ƿ��������
            /// </summary>
            ColIsBatch,
            /// <summary>
            /// ������λ
            /// </summary>
            ColUnitFlag,
            /// <summary>
            /// ����������
            /// </summary>
            ColMaxDays,
            /// <summary>
            /// ����������
            /// </summary>
            ColMinDays,
            /// <summary>
            /// �ο�����
            /// </summary>
            ColReferenceDays,
            /// <summary>
            /// �Ƿ�ҩ�����
            /// </summary>
            ColIsArk,
            ColInListNO,
            ColOutListNO,
            /// <summary>
            /// ���ұ���
            /// </summary>
            ColDeptID
        }

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            if (!SOC.HISFC.Components.PharmacyCommon.Function.JugePrive(this.PrivePowerString))
            {
                Function.ShowMessage("��û��Ȩ�޲����˴��ڣ�������������ϵͳ����Ա��ϵ��", MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }

        #endregion
    }
}
