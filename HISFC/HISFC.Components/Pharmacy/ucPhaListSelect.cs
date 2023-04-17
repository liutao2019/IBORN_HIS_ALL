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
    /// [功能描述: 药品单据选择]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2006-12]<br></br>
    /// </summary>
    public partial class ucPhaListSelect : FS.HISFC.Components.Common.Controls.ucIMAListSelecct
    {
        public ucPhaListSelect()
        {
            InitializeComponent();
        }


        #region 域成员变量

        /// <summary>
        /// 入库类型帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper inTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 出库类型帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper outTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region 属性

        /// <summary>
        /// 入库类型帮助类
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
        /// 出库类型帮助类
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
        /// 科室帮助类
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

            #region 获取入库权限

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

            #region 获取出库权限

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

            #region 获取科室

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();
            if (alDept != null)
                this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            #endregion
        }

        /// <summary>
        /// 入库单据查询
        /// </summary>
        protected override void QueryIn()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alList = itemManager.QueryInputList(this.DeptInfo.ID, "AAAA", this.State, this.BeginDate, this.EndDate);
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("查询单据列表发生错误" + itemManager.Err));
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.Input info in alList)
            {
                if (this.MarkPrivType != null)
                {
                    if (this.MarkPrivType.ContainsKey(info.PrivType))       //对于过滤的权限不显示
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
                    company.Name = "无供货公司";
                }

                this.neuSpread1_Sheet1.Cells[0, 3].Text = company.Name;
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = company.ID;
            }
        }

        /// <summary>
        /// 出库单据查询
        /// </summary>
        protected override void QueryOut()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            ArrayList alList = new ArrayList();
            if (this.PrivType != null && this.PrivType.ID != "")
            {
                #region 根据不同权限进行不同处理

                switch (this.PrivType.Memo)
                {
                    case "16":              //核准入库
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
                MessageBox.Show(Language.Msg("查询单据列表发生错误" + itemManager.Err));
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.FrameWork.Models.NeuObject info in alList)
            {
                if (this.MarkPrivType != null)
                {
                    if (this.MarkPrivType.ContainsKey(info.User01))       //对于过滤的权限不显示
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
        /// 采购单据查询
        /// </summary>
        protected override void QueryStock()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            //ArrayList al = itemManager.QueryStockPLanCompanayList(this.DeptInfo.ID, "2");//{9530E80F-BE03-487f-B6E7-E6DE4FD3745B}
            ArrayList al = itemManager.QueryStockPLanCompanayList(this.DeptInfo.ID, "2", this.BeginDate.ToString(), this.EndDate.ToString());
            if (al == null)
            {
                MessageBox.Show(Language.Msg("获取采购单列表发生错误!" + itemManager.Err));
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColList].Text = info.ID;             //采购单号
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetName].Text = info.Name;     //供货公司名称
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = info.User01;     //供货公司编码
            }
        }
    }
}
