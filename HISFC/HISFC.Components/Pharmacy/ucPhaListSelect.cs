using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ����ѡ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucPhaListSelect : FS.HISFC.Components.Common.Controls.ucIMAListSelecct
    {
        public ucPhaListSelect()
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

        #endregion

        public override void Init()
        {
            base.Init();

            #region ��ȡ���Ȩ��

            FS.HISFC.BizLogic.Manager.PowerLevelManager myManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
            ArrayList inPriv = myManager.LoadLevel3ByLevel2("0310");

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

            ArrayList outPriv = myManager.LoadLevel3ByLevel2("0320");

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
        }

        /// <summary>
        /// ��ⵥ�ݲ�ѯ
        /// </summary>
        protected override void QueryIn()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alList = itemManager.QueryInputList(this.DeptInfo.ID, "AAAA", this.State, this.BeginDate, this.EndDate);
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("��ѯ�����б�������" + itemManager.Err));
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.Input info in alList)
            {
                if (this.MarkPrivType != null)
                {
                    if (this.MarkPrivType.ContainsKey(info.PrivType))       //���ڹ��˵�Ȩ�޲���ʾ
                    {
                        continue;
                    }
                }

                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.InListNO;
                this.neuSpread1_Sheet1.Cells[0, 1].Text = info.DeliveryNO;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = this.inTypeHelper.GetName(info.PrivType);

                FS.HISFC.Models.Pharmacy.Company company = new FS.HISFC.Models.Pharmacy.Company();

                if (info.Company.ID != "None")
                {
                    FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();
                    company = constant.QueryCompanyByCompanyID(info.Company.ID);
                    if (company == null)
                    {
                        MessageBox.Show(Language.Msg(constant.Err));
                        return;
                    }
                }
                else
                {
                    company.ID = "None";
                    company.Name = "�޹�����˾";
                }

                this.neuSpread1_Sheet1.Cells[0, 3].Text = company.Name;
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = company.ID;
            }
        }

        /// <summary>
        /// ���ⵥ�ݲ�ѯ
        /// </summary>
        protected override void QueryOut()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alList = new ArrayList();
            if (this.PrivType != null && this.PrivType.ID != "")
            {
                #region ���ݲ�ͬȨ�޽��в�ͬ����

                switch (this.PrivType.Memo)
                {
                    case "16":              //��׼���
                        alList = itemManager.QueryOutputListForApproveInput(this.DeptInfo.ID, this.BeginDate, this.EndDate);
                        break;
                }

                #endregion
            }
            else
            {
                alList = itemManager.QueryOutputList(this.DeptInfo.ID, "A", this.State, this.BeginDate, this.EndDate);
            }

            if (alList == null)
            {
                MessageBox.Show(Language.Msg("��ѯ�����б�������" + itemManager.Err));
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.FrameWork.Models.NeuObject info in alList)
            {
                if (this.MarkPrivType != null)
                {
                    if (this.MarkPrivType.ContainsKey(info.User01))       //���ڹ��˵�Ȩ�޲���ʾ
                    {
                        continue;
                    }
                }

                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.ID;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = this.outTypeHelper.GetName(info.User01);

                FS.HISFC.Models.Pharmacy.Company company = new FS.HISFC.Models.Pharmacy.Company();

                if (this.deptHelper.GetObjectFromID(info.Memo) == null)
                {
                    FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();
                    company = constant.QueryCompanyByCompanyID(info.Memo);
                    if (company == null)
                    {
                        MessageBox.Show(constant.Err);
                        return;
                    }
                }
                else
                {
                    company.ID = info.Memo;
                    company.Name = this.deptHelper.GetName(info.Memo);
                }

                this.neuSpread1_Sheet1.Cells[0, 3].Text = company.Name;
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = company.ID;
            }
        }

        /// <summary>
        /// �ɹ����ݲ�ѯ
        /// </summary>
        protected override void QueryStock()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            //ArrayList al = itemManager.QueryStockPLanCompanayList(this.DeptInfo.ID, "2");//{9530E80F-BE03-487f-B6E7-E6DE4FD3745B}
            ArrayList al = itemManager.QueryStockPLanCompanayList(this.DeptInfo.ID, "2", this.BeginDate.ToString(), this.EndDate.ToString());
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ�ɹ����б�������!" + itemManager.Err));
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColList].Text = info.ID;             //�ɹ�����
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetName].Text = info.Name;     //������˾����
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = info.User01;     //������˾����
            }
        }
    }
}
