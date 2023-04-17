using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GZSI.Controls
{
    /// <summary>
    /// 广州医保待遇算法维护 by huangchw 2012-12-06
    /// </summary>
    public partial class ucInsuranceManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInsuranceManager()
        {
            InitializeComponent();

            if (htPactDescript == null || htPactDescript.Count == 0)
            {
                FS.HISFC.Models.Base.PactInfo pactInfo = null;
                htPactDescript = new Hashtable();
                ArrayList alPactDescript = new FS.HISFC.BizLogic.Fee.PactUnitInfo().QueryPactUnitDLL();
                for (int i = 0; i < alPactDescript.Count; i++)
                {
                    pactInfo = alPactDescript[i] as FS.HISFC.Models.Base.PactInfo;
                    //可能同个DLL会有不同的描述，只取其中一个
                    if (!htPactDescript.ContainsKey(pactInfo.PactDllName))
                    {
                        htPactDescript.Add(pactInfo.PactDllName, pactInfo.PactDllDescription);
                    }
                }
            }

            this.SetValues();
        }

        private FS.HISFC.BizLogic.Fee.InPatient bizInpatient = new FS.HISFC.BizLogic.Fee.InPatient();

        private static Hashtable htPactDescript;

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("添加", "添加", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("修改", "修改", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("删除", "删除", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("刷新", "刷新", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                if (MessageBox.Show("确认删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
                this.Delete();
            }
            else if (e.ClickedItem.Text == "添加")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "刷新")
            {
                this.Refresh(); ;
            }
            else if (e.ClickedItem.Text == "修改")
            {
                this.Modify();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 赋值
        /// </summary>
        private void SetValues()
        {
            ArrayList al = bizInpatient.QueryInsuranceTreatment();

            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
               //this.neuSpread1_Sheet1.Rows.Add(0, 1);
            }

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.SIInterface.Insurance obj = al[i] as FS.HISFC.Models.SIInterface.Insurance;
                this.neuSpread1_Sheet1.Rows.Add(i, 1);
                this.neuSpread1_Sheet1.Rows[i].Tag = obj;
                
                if (htPactDescript.ContainsKey(obj.PactInfo.ID))
                {
                    this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.PactCode, htPactDescript[obj.PactInfo.ID]);
                }

                this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.Kind, (InsuranceTreatmentKind)Int32.Parse(obj.Kind.ID));
                this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.PartId, obj.PartId);
                this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.Rate, obj.Rate.ToString());
                this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.BeginCost, obj.BeginCost.ToString());
                this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.EndCost, obj.EndCost.ToString());
                this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.Memo, obj.Memo);
                this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.OperCode, obj.OperCode.ID);
                this.neuSpread1_Sheet1.SetValue(i, (int)InsuranceTreatmentCols.OperDate, obj.OperDate.ToString());
                
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        private int Add()
        {
            frmInsuranceManager frmIM = new frmInsuranceManager(htPactDescript);
            frmIM.Init("add");
            frmIM.ShowDialog();
            this.Refresh();
            return 1;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int Modify()
        {
            FS.HISFC.Models.SIInterface.Insurance oldObj = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.SIInterface.Insurance;

            frmInsuranceManager frmIM = new frmInsuranceManager(htPactDescript);
            frmIM.Init("modify", oldObj);
            frmIM.ShowDialog();

            this.Refresh();
            return 1;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private int Delete()
        {
            int index = this.neuSpread1_Sheet1.ActiveRow.Index;
            FS.HISFC.Models.SIInterface.Insurance obj = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.SIInterface.Insurance;
            if (bizInpatient.DeleteInsuranceTreatment(obj) == -1)
            {
                MessageBox.Show("删除出错，请联系管理员。");
                return -1;
            }
            this.neuSpread1_Sheet1.Rows.Remove(index, 1);//不刷新，只移除
            return 1;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <returns></returns>
        public override void Refresh()
        {
            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            this.SetValues();
            base.Refresh();
            
        }


        /// <summary>
        /// 医保待遇类别
        /// </summary>
        internal enum InsuranceTreatmentKind
        {
            在职 = 1,
            退休 = 2,
            离休 = 3,
            一_四级工残 = 4,
            无业 = 5,
            已趸缴 = 6,
            退职 = 7,
            学龄前儿童 = 70,
            中小学生 = 71,
            大中专学生 = 72,
            其他未成年人 = 73,
            非从业人员 = 74,
            老年人 = 75

            //(InsuranceTreatmentKind)72;
            //(int)InsuranceTreatmentKind.离休;
            //InsuranceTreatmentKind.离休.ToString();
        }

        /// <summary>
        /// FarPoint显示列
        /// </summary>
        private enum InsuranceTreatmentCols
        {
            /// <summary>
            /// 合同单位
            /// </summary>
            PactCode = 1,

            /// <summary>
            /// 类别
            /// </summary>
            Kind,

            /// <summary>
            /// 分段序号
            /// </summary>
            PartId,

            /// <summary>
            /// 比例
            /// </summary>
            Rate,

            /// <summary>
            /// 区间开始
            /// </summary>
            BeginCost,

            /// <summary>
            /// 区间结束
            /// </summary>
            EndCost,

            /// <summary>
            /// 备注
            /// </summary>
            Memo,

            /// <summary>
            /// 操作员
            /// </summary>
            OperCode,

            /// <summary>
            /// 操作日期
            /// </summary>
            OperDate
        }

       

    }
}
