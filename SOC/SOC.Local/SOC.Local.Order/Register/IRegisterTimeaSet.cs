using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.Register
{
    /// <summary>
    /// 补收挂号费的非急诊时间设置
    /// </summary>
    public partial class IRegisterTimeaSet : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public IRegisterTimeaSet()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 设置类型
        /// </summary>
        private string SetRegTimeType = "EmergencyTimeSet";

        protected override void OnLoad(EventArgs e)
        {
            this.QueryData();
        }

        private void QueryData()
        {
            ArrayList al = this.constMgr.GetList(SetRegTimeType);
            this.fpRegTimeSet_Sheet1.RowCount = al.Count;

            FS.HISFC.Models.Base.Const constObj = null;
            for (int i = 0; i < al.Count; i++)
            {
                constObj = al[i] as FS.HISFC.Models.Base.Const;
                this.fpRegTimeSet_Sheet1.Cells[i, 0].Text = constObj.ID;
                this.fpRegTimeSet_Sheet1.Cells[i, 1].Text = constObj.Name;
                this.fpRegTimeSet_Sheet1.Cells[i, 2].Text = constObj.Memo;
                this.fpRegTimeSet_Sheet1.Cells[i, 3].Text = constObj.UserCode;
                this.fpRegTimeSet_Sheet1.Cells[i, 4].Text = constObj.SpellCode;
                this.fpRegTimeSet_Sheet1.Cells[i, 5].Text = constObj.WBCode;
                this.fpRegTimeSet_Sheet1.Cells[i, 6].Value = constObj.SortID;
                this.fpRegTimeSet_Sheet1.Cells[i, 7].Text = constObj.IsValid ? "有效" : "无效";
                this.fpRegTimeSet_Sheet1.Cells[i, 8].Text = constObj.OperEnvironment.ID;
                this.fpRegTimeSet_Sheet1.Cells[i, 9].Value = constObj.OperEnvironment.OperTime;
            }
        }

        /// <summary>
        /// 获取维护数据
        /// </summary>
        /// <returns></returns>
        private int SaveSetTimes()
        {
            ArrayList alSetTimes = new ArrayList();
            FS.HISFC.Models.Base.Const setTimeConst = null;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.constMgr.DelConstant(this.SetRegTimeType) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(constMgr.Err);
                return -1;
            }

            try
            {
                for (int i = 0; i < this.fpRegTimeSet_Sheet1.RowCount; i++)
                {
                    setTimeConst = new FS.HISFC.Models.Base.Const();
                    setTimeConst.ID = this.fpRegTimeSet_Sheet1.Cells[i, 0].Text;
                    //星期
                    setTimeConst.Name = this.fpRegTimeSet_Sheet1.Cells[i, 1].Text;
                    //开始时间
                    setTimeConst.Memo = this.fpRegTimeSet_Sheet1.Cells[i, 2].Text;
                    //截止时间
                    setTimeConst.UserCode = this.fpRegTimeSet_Sheet1.Cells[i, 3].Text;

                    setTimeConst.SpellCode = FS.FrameWork.Public.String.GetSpell(setTimeConst.Name);
                    setTimeConst.WBCode = FS.FrameWork.Public.String.GetSpell(setTimeConst.Name); //先这样吧
                    setTimeConst.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.fpRegTimeSet_Sheet1.Cells[i, 6].Text);
                    setTimeConst.IsValid = this.fpRegTimeSet_Sheet1.Cells[i, 7].Text.Trim() == "有效" ? true : false;
                    setTimeConst.OperEnvironment.ID = constMgr.Operator.ID;
                    setTimeConst.OperEnvironment.OperTime = constMgr.GetDateTimeFromSysDateTime();

                    alSetTimes.Add(setTimeConst);
                    if (this.constMgr.InsertItem(this.SetRegTimeType, setTimeConst) == -1)
                    {
                        if (this.constMgr.UpdateItem(this.SetRegTimeType, setTimeConst) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新失败：" + this.constMgr.Err);
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("保存成功！");

            return 1;
        }

        /// <summary>
        /// 有效性校验
        /// </summary>
        /// <returns></returns>
        private int CheckValid()
        {
            Hashtable hsTimeSet = new Hashtable();

            for (int i = 0; i < this.fpRegTimeSet_Sheet1.RowCount; i++)
            {
                if (string.IsNullOrEmpty(this.fpRegTimeSet_Sheet1.Cells[i, 0].Text))
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "行编码不能为空！");
                    return -1;
                }
                if(string.IsNullOrEmpty(this.fpRegTimeSet_Sheet1.Cells[i, 1].Text))
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "行星期没有选择！");
                    return -1;
                }

                if (hsTimeSet.Contains(this.fpRegTimeSet_Sheet1.Cells[i, 0].Text))
                {
                    hsTimeSet.Add(this.fpRegTimeSet_Sheet1.Cells[i, 0].Text, this.fpRegTimeSet_Sheet1.Cells[i, 1].Text);
                }
                else
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "行编码已存在！");
                    return -1;
                }
            }

            return 1;
        }

        #region IMaintenanceControlable 成员

        public int Add()
        {
            this.fpRegTimeSet_Sheet1.AddRows(this.fpRegTimeSet_Sheet1.Rows.Count, 1);
            this.fpRegTimeSet_Sheet1.ActiveRowIndex = this.fpRegTimeSet_Sheet1.Rows.Count - 1;
            this.fpRegTimeSet_Sheet1.ActiveColumnIndex = 0;
            this.fpRegTimeSet_Sheet1.Cells[this.fpRegTimeSet_Sheet1.ActiveRowIndex, 6].Value = this.fpRegTimeSet_Sheet1.ActiveRowIndex + 1;
            this.fpRegTimeSet_Sheet1.Cells[this.fpRegTimeSet_Sheet1.ActiveRowIndex, 7].Value = "有效";
            this.fpRegTimeSet_Sheet1.Cells[this.fpRegTimeSet_Sheet1.ActiveRowIndex, 8].Value = this.constMgr.Operator.ID;
            this.fpRegTimeSet_Sheet1.Cells[this.fpRegTimeSet_Sheet1.ActiveRowIndex, 9].Value = this.constMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            //this.neuSpread1.ShowRow(0, this.fpRegTimeSet_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
            return 1;
        }

        public int Copy()
        {
            return 1;
        }

        public int Cut()
        {
            return 1;
        }

        public int Delete()
        {
            this.fpRegTimeSet_Sheet1.Rows.Remove(this.fpRegTimeSet_Sheet1.ActiveRowIndex, 1);

            return 1;
        }

        public int Export()
        {
            return 1;
        }

        public int Import()
        {
            return 1;
        }

        public int Init()
        {
            return 1;
        }

        public bool IsDirty
        {
            get
            {
                return true;
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Modify()
        {
            return 1;
        }

        public int NextRow()
        {
            return 1;
        }

        public int Paste()
        {
            return 1;
        }

        public int PreRow()
        {
            return 1;
        }

        public int Print()
        {
            return 1;
        }

        public int PrintConfig()
        {
            return 1;
        }

        public int PrintPreview()
        {
            return 1;
        }

        public new int Query()
        {
            this.QueryData();
            return 1;
        }

        public FS.FrameWork.WinForms.Forms.IMaintenanceForm QueryForm
        {
            get
            {
                return null;
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Save()
        {
            return SaveSetTimes();
        }

        #endregion
    }
}
