using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.ZhangCha
{
    public partial class ucQueryInPatientInfo : FS.SOC.Local.InpatientFee.Base.ucDeptTimeBaseReport
    {
        public ucQueryInPatientInfo()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 合同单位业务
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        #endregion

        private void ucQueryInPatientInfo_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            System.Collections.ArrayList listPact = pactMgr.QueryPactUnitAll();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "All";
            obj.Name = "全部";

            System.Collections.ArrayList listPactAll = new System.Collections.ArrayList();
            listPactAll.Add(obj);
            listPactAll.AddRange(listPact);
            //合同单位
            this.cmbPact.AddItems(listPactAll);

            System.Collections.ArrayList listInState = new System.Collections.ArrayList();

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "All";
            obj.Name = "全部";
            listInState.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "R";
            obj.Name = "入院登记";
            listInState.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "I";
            obj.Name = "在院状态";
            listInState.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "B";
            obj.Name = "出院登记";
            listInState.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "O";
            obj.Name = "出院清帐";
            listInState.Add(obj);
            //患者住院状态
            this.cmbInState.AddItems(listInState);

            this.OperationStartHandler = new DelegateOperationStart(this.ucQueryInPatientInfo_DelegateOperationStart);
            this.SQLIndexs = "FS.SOC.Inpatient.QueryInpatientInfo";
            this.IsDeptAsCondition = true;
            this.IsNeedAdditionConditions = true;
            this.FilterAfterEnterKey = true;
            this.IsAllowAllDept = true;
            this.QueryDataWhenInit = false;
            this.DeptType = "I";
            this.FilerType = EnumFilterType.汇总过滤;
            this.RightAdditionTitle = "";
            this.LeftAdditionTitle = "";
            this.MidAdditionTitle = "";
            this.MainTitle = "佛山市禅城区张槎医院住院患者一览表";
            this.Init(); //再一次进行初始化
        }

        /// <summary>
        /// 查询之前操作
        /// </summary>
        /// <param name="type"></param>
        public void ucQueryInPatientInfo_DelegateOperationStart(string type)
        {
            if (type.Equals("query"))
            {
                string beginTime = this.dtStart.Value.ToString();
                string endTime = this.dtEnd.Value.ToString();
                string pactCode = this.cmbPact.Tag.ToString();
                string inState = this.cmbInState.Tag.ToString();
                string deptCode = this.cmbDept.Tag.ToString();

                this.QueryAdditionConditions = new string[] { beginTime, endTime, pactCode, inState, deptCode };
            }
        }
    }
}
