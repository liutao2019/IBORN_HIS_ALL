using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [功能描述: 药品等级、医生职级对照维护]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-07]<br></br>
    /// </summary>
    public partial class ucDrugDocGradeCompare : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucDrugDocGradeCompare()
        {
            InitializeComponent();
        }


        #region 域变量

        /// <summary>
        /// 职级帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper employeeLevelHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 药品等级帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugGradeHelper = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        private ArrayList alEmployeeLevel = null;

        private ArrayList alDrugGrade = null;

        #endregion

        /// <summary>
        /// 药品等级、医生职级对照显示
        /// </summary>
        /// <returns></returns>
        internal int ShowDrugDocCompare()
        {
            ArrayList al = consManager.GetList(((System.Windows.Forms.Control)this).Text);

            return this.ShowDrugDocCompare(al);
        }

        /// <summary>
        /// 药品等级、医生职级对照显示
        /// </summary>
        /// <returns></returns>
        internal int ShowDrugDocCompare(ArrayList alCompare)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            if (alCompare == null)
            {
                return 1;
            }

            foreach (FS.FrameWork.Models.NeuObject info in alCompare)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                //{3972BA6D-5CE4-4995-90AA-30DD281D1660}
                if (info.ID.IndexOf("|") != -1)
                {
                    info.ID = info.ID.Substring(0, info.ID.IndexOf("|"));       //拆分字符 获取医生职级
                }

                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.ID;
                this.neuSpread1_Sheet1.Cells[0, 1].Text = this.employeeLevelHelper.GetName(info.ID);

                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Name;
                this.neuSpread1_Sheet1.Cells[0, 3].Text = this.drugGradeHelper.GetName(info.Name);                
            }

            return 1;
        }

        #region IMaintenanceControlable 成员
       
        private bool isDirty = false;

        private FS.FrameWork.WinForms.Forms.IMaintenanceForm queryForm = null;

        public int Add()
        {
            this.neuSpread1_Sheet1.Rows.Add(0, 1);

            return 1;
        }

        public int Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Cut()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Delete()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return -1;
            }

            DialogResult rs = MessageBox.Show(FS.FrameWork.Management.Language.Msg("确认删除当前选择信息吗？"),"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
            {
                return -1;
            }
            string id = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            //{B7BFFC6E-D820-44ca-B74C-2B211EA9FA1F}
            string name = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 2].Text;

            if (consManager.DelConstant(((System.Windows.Forms.Control)this).Text, id + "|" + name) == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除药品等级职级对照列表发生错误"));
                return -1;
            }

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除药品等级职级对照列表成功"));

            //{B7BFFC6E-D820-44ca-B74C-2B211EA9FA1F}刷新显示
            ShowDrugDocCompare();

            return 1;
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Import()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Init()
        {
            string dictionary = ((System.Windows.Forms.Control)this).Text;

            //职称
            if (dictionary == "SpeDrugPosition")
            {
                this.alEmployeeLevel = consManager.GetList("POSITION");
            }
            //职级
            else if (dictionary == "SpeDrugGrade")
            {
                this.alEmployeeLevel = consManager.GetList("LEVEL");
            }
            else
            {
                this.alEmployeeLevel = new ArrayList();
            }

            //this.alEmployeeLevel = consManager.GetList("LEVEL");
            if (alEmployeeLevel == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("加载人员职级列表发生错误"));
                return -1;
            }
            this.employeeLevelHelper = new FS.FrameWork.Public.ObjectHelper(this.alEmployeeLevel);

            this.alDrugGrade = consManager.GetList("DRUGGRADE");
            if (alDrugGrade == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("加载药品等级职级对照列表发生错误"));
                return -1;
            }
            this.drugGradeHelper = new FS.FrameWork.Public.ObjectHelper(this.alDrugGrade);

            this.ShowDrugDocCompare();

            return 1;
        }

        public bool IsDirty
        {
            get
            {
                return false;
            }
            set
            {
                this.isDirty = value;
            }
        }

        public int Modify()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int NextRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Paste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PreRow()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintConfig()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Query()
        {
            return this.ShowDrugDocCompare();
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return this.queryForm;
            }
            set
            {
                this.queryForm = value;
            }
        }

        public int Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction(); 
            //consManager.SetTrans(t.Trans);            
            //{3F1D29EA-0A9D-4703-938E-AB3E51257672}
            //int returnValue =  consManager.DelConstant("SpeDrugGrade");
            //if (returnValue < 0)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show("删除常数信息出错.\n 错误信息 :" + consManager.Err  );
            //    return -1;
            //}

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 0].Text) || string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 2].Text))
                {
                    continue;
                }

                FS.HISFC.Models.Base.Const cons = new FS.HISFC.Models.Base.Const();

                cons.ID = this.neuSpread1_Sheet1.Cells[i, 0].Text;
                cons.Name = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                cons.IsValid = true;
                //{3972BA6D-5CE4-4995-90AA-30DD281D1660}
                //为了避免主键重复 对于医生职级与药品等级分开
                cons.ID = cons.ID + "|" + cons.Name;

                if (consManager.SetConstant(((System.Windows.Forms.Control)this).Text, cons) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("药品等级、医生职级对照更新失败"));
                    return 1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功"));

            this.Query();

            return 1;
        }

        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
            {
                //操作员对窗口选择返回的信息
                FS.FrameWork.Models.NeuObject docLevel = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alEmployeeLevel, ref docLevel) == 0)
                {
                    return;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[e.Row, 0].Text = docLevel.ID;
                    this.neuSpread1_Sheet1.Cells[e.Row, 1].Text = docLevel.Name;
                }
            }

            if (e.Column == 2)
            {
                //操作员对窗口选择返回的信息
                FS.FrameWork.Models.NeuObject drugGrade = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alDrugGrade, ref drugGrade) == 0)
                {
                    return;
                }
                else
                {
                    string type = ((System.Windows.Forms.Control)this).Text;
                    FS.FrameWork.Models.NeuObject obj = this.consManager.GetConstant(type, this.neuSpread1_Sheet1.Cells[e.Row, 0].Text + "|" + drugGrade.ID);
                    if (obj.ID != string.Empty)
                    {
                        MessageBox.Show("医师级别：" + this.neuSpread1_Sheet1.Cells[e.Row, 1].Text + ",药品等级:" + drugGrade.Name + "已经存在！");
                        return;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[e.Row, 2].Text = drugGrade.ID;
                        this.neuSpread1_Sheet1.Cells[e.Row, 3].Text = drugGrade.Name;
                    }
                }
            }
        }
    }
}
