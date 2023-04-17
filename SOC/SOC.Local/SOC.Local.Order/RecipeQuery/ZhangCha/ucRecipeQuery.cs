using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Order.ZhangCha
{
    /// <summary>
    /// 处方批量查询
    /// </summary>
    public partial class ucRecipeQuery : Neusoft.SOC.Local.Pharmacy.Base.BaseReport
    {
        private Neusoft.HISFC.BizLogic.Order.Order orderMgr = new Neusoft.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 查询时，是否将Text进行查询（将Text转换为Tag）
        /// </summary>
        private bool isCustomTextToTag = false;

        [Category("Query查询设置"), Description("查询时，是否将Text进行查询（将Text转换为Tag）")]
        public bool IsCustomTextToTag
        {
            get
            {
                return isCustomTextToTag;
            }
            set
            {
                isCustomTextToTag = value;
            }
        }

        /// <summary>
        /// 是否初始化加载所有诊断
        /// </summary>
        private bool isInitDiag = false;

        /// <summary>
        /// 是否初始化加载所有诊断
        /// </summary>
        [Category("Query查询设置"), Description("是否初始化加载所有诊断")]
        public bool IsInitDiag
        {
            get
            {
                return isInitDiag;
            }
            set
            {
                isInitDiag = value;
            }
        }

        public ucRecipeQuery()
        {
            InitializeComponent();

            this.SQLIndexs = "Met.OutPatient.RecipeDetail.Query";
            this.MainTitle = "患者处方、治疗项目明细表";
            this.MidAdditionTitle = "";
            this.RightAdditionTitle = "";
            this.IsDeptAsCondition = false;
            this.QueryDataWhenInit = false;
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select p.pact_code id, p.pact_name name, '' memo,fun_get_querycode(p.pact_name,1) spell_code,fun_get_querycode(p.pact_name,0) wb_code,p.pact_code user_code from fin_com_pactunitinfo p";

            this.QueryEndHandler += new DelegateQueryEnd(ucRecipeQuery_QueryEndHandler);
            this.OperationStartHandler += new DelegateOperationStart(ucRecipeQuery_OperationStartHandler);

            this.InitData();
        }

        private int InitData()
        {
            Neusoft.FrameWork.Models.NeuObject objAll = new Neusoft.FrameWork.Models.NeuObject("All", "全部", "qb");
            Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

            try
            {
                ArrayList al = interMgr.GetDepartment();
                al.Add(objAll);
                this.cmbMyDept.AddItems(al);
                this.cmbMyDept.Tag = "All";

                al = interMgr.QueryEmployee(Neusoft.HISFC.Models.Base.EnumEmployeeType.D);
                al.Add(objAll);
                this.cmbMyDoct.AddItems(al);
                this.cmbMyDoct.Tag = "All";

                DateTime tt = this.orderMgr.GetDateTimeFromSysDateTime();
                this.dtpBegin.Value = tt.Date;
                this.dtpEnd.Value = new DateTime(tt.Year, tt.Month, tt.Day, 23, 59, 59);

                Neusoft.HISFC.BizLogic.HealthRecord.ICD icdMgr = new Neusoft.HISFC.BizLogic.HealthRecord.ICD();
                //if (this.isInitDiag)
                //{
                //    al = icdMgr.Query(Neusoft.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, Neusoft.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
                //    this.cmbMyDiag.AddItems(al);
                //}

                Neusoft.FrameWork.Models.NeuObject pAllObj = new Neusoft.FrameWork.Models.NeuObject("P','PCZ", "西药、中成药", "xy、zcy");
                Neusoft.FrameWork.Models.NeuObject pObj = new Neusoft.FrameWork.Models.NeuObject("P", "西药", "xy");
                Neusoft.FrameWork.Models.NeuObject pccObj = new Neusoft.FrameWork.Models.NeuObject("PCC", "中草药", "zcy");
                Neusoft.FrameWork.Models.NeuObject pczObj = new Neusoft.FrameWork.Models.NeuObject("PCZ", "中成药", "zcy");

                al = new ArrayList();
                al.Add(objAll);
                al.Add(pAllObj);
                al.Add(pObj);
                al.Add(pczObj);
                al.Add(pccObj);

                this.cmbMyType.AddItems(al);
                this.cmbMyType.Tag = "All";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return 1;
        }

        void ucRecipeQuery_OperationStartHandler(string type)
        {
            if (type == "query")
            {
                if (this.isCustomTextToTag)
                {
                    this.cmbCustomType.Tag = this.cmbCustomType.Text;
                }
            }
        }

        void ucRecipeQuery_QueryEndHandler()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }

            string lastRowMemo = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text;
            string recipeNo = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 1].Text;
            for (int rowIndex = this.fpSpread1_Sheet1.RowCount - 1; rowIndex > -1; rowIndex--)
            {
                string nowRowMemo = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;
                string recipeNoNew = this.fpSpread1_Sheet1.Cells[rowIndex, 1].Text;

                if (lastRowMemo != nowRowMemo)
                {
                    this.fpSpread1_Sheet1.AddRows(rowIndex + 1, 1);
                    this.fpSpread1_Sheet1.Cells[rowIndex, 0].Tag = recipeNo;
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].ColumnSpan = this.fpSpread1_Sheet1.ColumnCount - 2;
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].Text = lastRowMemo;
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].Font = new Font("宋体", this.Font.Size, FontStyle.Bold);
                    this.fpSpread1_Sheet1.Rows[rowIndex + 1].Tag = "SpanRow";
                    lastRowMemo = nowRowMemo;
                    recipeNo = recipeNoNew;
                }

                if (rowIndex == 0 && lastRowMemo == nowRowMemo)
                {
                    this.fpSpread1_Sheet1.AddRows(0, 1);
                    this.fpSpread1_Sheet1.Cells[0, 0].Tag = recipeNoNew;
                    this.fpSpread1_Sheet1.Cells[0, 1].ColumnSpan = this.fpSpread1_Sheet1.ColumnCount - 2;
                    this.fpSpread1_Sheet1.Cells[0, 1].Text = nowRowMemo;
                    this.fpSpread1_Sheet1.Cells[0, 1].Font = new Font("宋体", this.Font.Size, FontStyle.Bold);
                    this.fpSpread1_Sheet1.Rows[0].Tag = "SpanRow";
                }
            }
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <returns></returns>
        public override int QueryData()
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("query");
            }
            int parm = this.GetData();

            this.ResetTitleLocation();

            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("query");
            }
            return parm;
        }

        /// <summary>
        /// 查询数据
        /// 如果有明细查询，这个作为汇总查询
        /// 明细通过双击fp实现
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int GetData()
        {
            //add by cube 2009-10-15 这样可以消除行列的设置
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnCount = 0;
            //end add

            if (string.IsNullOrEmpty(this.SQLIndexs) && string.IsNullOrEmpty(this.SQL))
            {
                this.ShowBalloonTip(5000, "温馨提示", "查询的SQL语句索引不正确\n请将SqlIndexs属性赋值");
                return -1;
            }
            System.Data.DataSet ds = new DataSet();

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍候...");
            Application.DoEvents();

            if (string.IsNullOrEmpty(this.SQL))
            {
                string[] sqls = this.SQLIndexs.Split(' ', ',', '|');
                if (this.orderMgr.ExecQuery(sqls, ref ds, this.GetParms()) == -1)
                {
                    MessageBox.Show("执行sql发生错误>>" + this.orderMgr.Err);
                    return -1;
                }
            }
            else
            {
                string sql = string.Format(this.SQL, this.GetParms());
                if (this.orderMgr.ExecQuery(sql, ref ds) == -1)
                {
                    MessageBox.Show("执行sql发生错误>>" + this.orderMgr.Err);
                    return -1;
                }
            }
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (ds == null || ds.Tables.Count == 0)
            {
                return -1;
            }
            this.DataView = new DataView(ds.Tables[0]);
            this.fpSpread1_Sheet1.DataSource = this.DataView;

            this.ReadSetting(0);
            this.SortTot();
            this.MergeData();

            this.SetTitle();

            return 0;
        }

        private string[] GetParms()
        {
            string[] parm = { this.cmbMyType.Tag.ToString(), this.cmbMyDept.Tag.ToString(), this.cmbMyDoct.Tag.ToString(), this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString(), this.cmbMyDiag.Text };
            return parm;
        }

        public override int Print(object sender, object neuObject)
        {
            return this.OnPrint(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            string recipeNo = "";
            ArrayList alRecipeNo = new ArrayList();
            for (int rowIndex = this.fpSpread1_Sheet1.RowCount - 1; rowIndex > -1; rowIndex--)
            {
                if (this.fpSpread1_Sheet1.Cells[rowIndex, 0].Tag != null && !string.IsNullOrEmpty(this.fpSpread1_Sheet1.Cells[rowIndex, 0].Tag.ToString()))
                {
                    recipeNo = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Tag.ToString();

                    alRecipeNo.Add(recipeNo);
                }
            }

            Neusoft.HISFC.Models.Registration.Register regObj = null;
            Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();
            Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderMgr = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();
            ArrayList alOrder = null;

            if (alRecipeNo != null && alRecipeNo.Count > 0)
            {
                if (MessageBox.Show("共有" + alRecipeNo.Count.ToString() + "张处方，是否全部打印？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return 1;
                }

                //progressBar1.Visible = true;
                //Application.DoEvents();
                //progressBar1.Maximum = alRecipeNo.Count;

                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在打印处方,请稍候!");
                Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(1, alRecipeNo.Count);
                Application.DoEvents();
                foreach (string recipeno in alRecipeNo)
                {
                    alOrder = orderMgr.QueryOrderByRecipeNO(recipeno);
                    if (alOrder == null)
                    {
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(orderMgr.Err);
                        return -1;
                    }
                    else if (alOrder.Count == 0)
                    {
                        continue;
                    }

                    regObj = regIntegrate.GetByClinic(((Neusoft.HISFC.Models.Order.OutPatient.Order)alOrder[0]).Patient.ID);

                    if (regObj == null)
                    {
                        Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(regIntegrate.Err);
                        return -1;
                    }
                    this.PrintRecipe(regObj, recipeno);

                    Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(Neusoft.FrameWork.WinForms.Classes.Function.WaitForm.progressBar1.Value + 1);
                    Application.DoEvents();
                }
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            else
            {
                MessageBox.Show("没有处方信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 1;
            }

            return 1;
        }

        /// <summary>
        /// 处方打印接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.IRecipePrint o = null;

        /// <summary>
        /// 打印处方
        /// </summary>
        /// <returns></returns>
        private int PrintRecipe(Neusoft.HISFC.Models.Registration.Register regObj, string recipeNo)
        {
            if (o == null)
            {
                o = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint)) as Neusoft.HISFC.BizProcess.Interface.IRecipePrint;
            }

            if (o == null)
            {
                MessageBox.Show("处方打印接口未实现");
                return -1;
            }
            else
            {
                if (regObj == null || string.IsNullOrEmpty(regObj.ID) || string.IsNullOrEmpty(recipeNo))
                {
                    MessageBox.Show("没有选择处方！");
                    return -1;
                }

                o.SetPatientInfo(regObj);
                o.RecipeNO = recipeNo;
                o.Printer = null;
                o.PrintRecipe();
            }
            return 1;
        }
    }
}
