using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Material
{
    public partial class ucMatListSelect : FS.HISFC.Components.Common.Controls.ucIMAListSelecct
    {
        public ucMatListSelect()
        {
            InitializeComponent();
        }

        #region ���Ա����

        /// <summary>
        /// ������Ͱ�����
        /// </summary>		
        private FS.FrameWork.Public.ObjectHelper inTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �������Ͱ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper outTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������ĿID
        /// </summary>
        private string itemID = "all";

        /// <summary>
        /// �������
        /// </summary>
        private string deptCode = "all";        

        #endregion

        #region ����

        /// <summary>
        /// ������Ͱ�����
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper InTypeHelper
        {
            get
            {
                return inTypeHelper;
            }
            set
            {
                inTypeHelper = value;
            }
        }

        /// <summary>
        /// �������Ͱ�����
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper OutTypeHelper
        {
            get
            {
                return outTypeHelper;
            }
            set
            {
                outTypeHelper = value;
            }
        }

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper DeptHelper
        {
            get
            {
                return deptHelper;
            }
            set
            {
                deptHelper = value;
            }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public string ItemID
        {
            get 
            { 
                return itemID; 
            }
            set 
            { 
                itemID = value; 
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string DeptCode
        {
            get
            {
                return deptCode;
            }
            set
            {
                deptCode = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public override void Init()
        {
            base.Init();

            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            base.neuSpread1_Sheet1.Columns[0, neuSpread1_Sheet1.ColumnCount - 1].CellType = txtType;
            #region ��ȡ���Ȩ��

            FS.HISFC.BizLogic.Manager.PowerLevelManager myManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
            ArrayList inPriv = myManager.LoadLevel3ByLevel2("0510");

            ArrayList alPriv = new ArrayList();
            FS.FrameWork.Models.NeuObject tempInfo = new FS.FrameWork.Models.NeuObject();
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 info in inPriv)
            {
                tempInfo = new FS.FrameWork.Models.NeuObject();
                tempInfo.ID = info.Class3Code;
                tempInfo.Name = info.Class3Name;

                alPriv.Add(tempInfo);
            }
            this.inTypeHelper = new FS.FrameWork.Public.ObjectHelper(alPriv);

            #endregion

            #region ��ȡ����Ȩ��

            ArrayList outPriv = myManager.LoadLevel3ByLevel2("0520");

            ArrayList alOutPriv = new ArrayList();
            FS.FrameWork.Models.NeuObject tempOutfo = new FS.FrameWork.Models.NeuObject();
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 info in outPriv)
            {
                tempOutfo = new FS.FrameWork.Models.NeuObject();
                tempOutfo.ID = info.Class3Code;
                tempOutfo.Name = info.Class3Name;

                alOutPriv.Add(tempOutfo);
            }
            this.outTypeHelper = new FS.FrameWork.Public.ObjectHelper(alOutPriv);

            #endregion

            #region ��ȡ����

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();
            if (alDept != null)
                this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            #endregion

            #region ��ȡ�����б�

            this.GetItemList();

            #endregion
        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        private void GetItemList()
        {
            this.ucMaterialItemList1.ShowFpRowHeader = false;

            this.ucMaterialItemList1.ShowMaterialList("all");            
        }

        /// <summary>
        /// ��ⵥ�ݲ�ѯ
        /// </summary>
        protected override void QueryIn()
        {
            FS.HISFC.BizLogic.Material.Store itemManager = new FS.HISFC.BizLogic.Material.Store();

            //����д�˲��ҵ��ݵķ���  ������ͨ��������Ŀ��ѯ
            //ArrayList alList = itemManager.QueryInputList(this.DeptInfo.ID, "AA", this.State, this.BeginDate, this.EndDate);
            ArrayList alList = itemManager.QueryInputList(this.DeptInfo.ID, "AA", this.State, this.BeginDate, this.EndDate, this.itemID);
            if (alList == null)
            {
                MessageBox.Show("��ѯ�����б�������" + itemManager.Err);
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Material.Input info in alList)
            {
                if (this.MarkPrivType != null)
                {
                    if (this.MarkPrivType.ContainsKey(info.StoreBase.PrivType))       //���ڹ��˵�Ȩ�޲���ʾ
                    {
                        continue;
                    }
                }

                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.InListNO;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = this.inTypeHelper.GetName(info.StoreBase.PrivType);

                if (this.DeptInfo.Memo == "L")			//�ֿ� ��ȡ������˾
                {
                    #region ��ȡ������˾

                    FS.HISFC.Models.Material.MaterialCompany company = new FS.HISFC.Models.Material.MaterialCompany();

                    if (info.StoreBase.Company.ID != "None")
                    {
                        FS.HISFC.BizLogic.Material.ComCompany constant = new FS.HISFC.BizLogic.Material.ComCompany();
                        company = constant.QueryCompanyByCompanyID(info.StoreBase.Company.ID, "A", "1");
                        if (company == null)
                        {
                            MessageBox.Show(constant.Err);
                            return;
                        }
                    }
                    else
                    {
                        company.ID = "None";
                        company.Name = "�޹�����˾";
                    }

                    this.neuSpread1_Sheet1.Cells[0, 3].Text = company.Name;
                    this.neuSpread1_Sheet1.Cells[0, 4].Text = company.ID;

                    #endregion
                }
            }
        }

        /// <summary>
        /// ���ⵥ�ݲ�ѯ
        /// </summary>
        protected override void QueryOut()
        {
            this.ShowSelectData();
            FS.HISFC.BizLogic.Material.Store itemManager = new FS.HISFC.BizLogic.Material.Store();
            //����д�˲��ҵ��ݵķ���  ������ͨ��������Ŀ��ѯ
            //List<FS.HISFC.Models.Material.Output> alList = itemManager.QueryOutputList(this.DeptInfo.ID, "AA", this.State, this.BeginDate, this.EndDate);
            List<FS.HISFC.Models.Material.Output> alList = itemManager.QueryOutputList(this.DeptInfo.ID, "AA", this.State, this.BeginDate, this.EndDate, itemID, this.deptCode);
            if (alList == null)
            {
                MessageBox.Show("��ѯ�����б�������" + itemManager.Err);
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Material.Output info in alList)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColList].Text = info.OutListNO;

                this.neuSpread1_Sheet1.Cells[0, 1].Text = info.StoreBase.StockDept.Name;

                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.StoreBase.StockDept.ID;

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetName].Text = info.StoreBase.TargetDept.Name;

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = info.StoreBase.TargetDept.ID;
            }
        }

        /// <summary>
        /// �ɹ����ݲ�ѯ
        /// </summary>
        protected override void QueryStock()
        {
            this.ShowStockData();

            FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

            ArrayList al = planManager.QueryStockPLanCompanayList(this.DeptInfo.ID, "2");
            if (al == null)
            {
                MessageBox.Show("��ȡ�ɹ����б�������!" + planManager.Err);
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColList].Text = info.ID;						//�ɹ�����
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetName].Text = info.User01;			//������˾����
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = info.Name;				//������˾����
            }
        }

        private int ShowStockData()
        {
            string[] filterStr = new string[1] { "�ɹ�����", };
            string[] label = new string[] { "�ɹ�����", "�ͻ�����", "����", "������λ", "������λ����" };
            float[] width = new float[] { 120, 100, 100, 100, 100 };
            bool[] visible = new bool[] { true, false, false, true, true };

            this.InitFp(label, visible, width);

            return 1;
        }

        private int ShowSelectData()
        {
            string[] filterStr = new string[1] { "���ⵥ��", };
            string[] label = new string[] { "���ⵥ��", "�������", "������Ҵ���", "Ŀ�����", "Ŀ����Ҵ���" };
            float[] width = new float[] { 120, 100, 60, 100, 60 };
            bool[] visible = new bool[] { true, true, false, true, false };

            this.InitFp(label, visible, width);

            return 1;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ��ȡ�б��е�ǰ��е�������ĿID
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="activeRow"></param>
        private void ucMaterialItemList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (chkItem.Checked)
            {
                if (sv != null && activeRow >= 0)
                {
                    this.itemID = sv.Cells[activeRow, 10].Text;
                    this.chkItem.Text = sv.Cells[activeRow, 1].Text;
                } 
            }
        }        
        
        /// <summary>
        /// �ж��Ƿ�ͨ�����ʹ���  ��  ��ǰѡ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkItem.Checked)
            {
                this.itemID = "all";
                this.chkItem.Text = "δѡ����Ŀ";
            }
        }

        #endregion
    }
}
