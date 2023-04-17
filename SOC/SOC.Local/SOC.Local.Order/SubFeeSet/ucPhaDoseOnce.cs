using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.SOC.Local.Order.SubFeeSet
{
    /// <summary>
    /// 药品第二基本计量维护（用于附材算法)
    /// </summary>
    public partial class ucPhaDoseOnce : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucPhaDoseOnce()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 交叉管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 药品编码与第二基本计量对照
        /// </summary>
        private string constPhaDoseOnce = "PharmacyDoseOnce";

        /// <summary>
        /// 药品列表
        /// </summary>
        private ArrayList alPhaItem = null;

        /// <summary>
        /// 药品基本信息帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper phaHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 配置文件
        /// </summary>
        private string xmlProfile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profiles\\PhaDoseOnceSet.xml";

        #endregion

        #region 方法

        /// <summary>
        /// 初始化列表
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);

            #region 初始化列表

            //医生职级
            alPhaItem = new ArrayList(phaIntegrate.QueryItemList(false).ToArray());
            if (alPhaItem == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("查询药品列表发生错误！" + phaIntegrate.Err));
                return -1;
            }
            this.phaHelper = new FS.FrameWork.Public.ObjectHelper(alPhaItem);

            //FS.HISFC.Models.Base.Spell obj = null;
            //foreach (FS.HISFC.Models.Pharmacy.Item itemObj in al)
            //{
            //    obj = new FS.HISFC.Models.Base.Spell();
            //    obj.ID = itemObj.UserCode;
            //    obj.Name = itemObj.Name;
            //    obj.Memo = itemObj.Specs;
            //    obj.SpellCode = itemObj.BaseDose.ToString();
            //    obj.WBCode = itemObj.DoseUnit;

            //    alPhaItem.Add(obj);
            //}

            #endregion

            this.neuSpread1_Sheet1.Columns[0].Visible = false;

            this.neuSpread1_Sheet1.Columns[3].Locked = true;
            this.neuSpread1_Sheet1.Columns[4].Locked = true;
            this.neuSpread1_Sheet1.Columns[6].Locked = true;
            this.neuSpread1_Sheet1.Columns[8].Locked = true;
            this.neuSpread1_Sheet1.Columns[9].Locked = true;

            if (System.IO.File.Exists(this.xmlProfile))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(neuSpread1_Sheet1, this.xmlProfile);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, xmlProfile);
            }

            return this.Query();
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, xmlProfile);
        }

        /// <summary>
        /// 双击弹出选择项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            if (e.Column == 1||e.Column==2)
            {
                this.PopItem(this.alPhaItem);
            }
        }

        /// <summary>
        /// 弹出常数选择
        /// </summary>
        private void PopItem(ArrayList al)
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item itemObj = phaHelper.GetObjectFromID(info.ID) as FS.HISFC.Models.Pharmacy.Item;
                if (itemObj != null)
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Value = itemObj.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Value = itemObj.UserCode;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 2].Value = itemObj.Name;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 3].Value = itemObj.Specs;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 4].Value = itemObj.BaseDose.ToString("F4").TrimEnd('0').Trim('.') + itemObj.DoseUnit; ;
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    if (this.neuSpread1_Sheet1.ActiveColumnIndex == 1 || this.neuSpread1_Sheet1.ActiveColumnIndex == 2)
                    {
                        this.PopItem(this.alPhaItem);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public int Query()
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            ArrayList alPhaDoseOnce = this.consManager.GetAllList(this.constPhaDoseOnce);

            FS.HISFC.Models.Pharmacy.Item phaItem = null;

            foreach (FS.HISFC.Models.Base.Const conObj in alPhaDoseOnce)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                phaItem = phaHelper.GetObjectFromID(conObj.ID) as FS.HISFC.Models.Pharmacy.Item;

                //药品编码
                this.neuSpread1_Sheet1.Cells[0, 0].Text = phaItem.ID;
                //药品编码
                this.neuSpread1_Sheet1.Cells[0, 1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.neuSpread1_Sheet1.Cells[0, 1].Text = phaItem.UserCode;
                //药品名称
                this.neuSpread1_Sheet1.Cells[0, 2].Text = phaItem.Name;
                //规格
                this.neuSpread1_Sheet1.Cells[0, 3].Text = phaItem.Specs;

                try
                {
                    //基本记录

                    this.neuSpread1_Sheet1.Cells[0, 4].Text = phaItem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaItem.DoseUnit;

                }
                catch(Exception e) 
                {
                    MessageBox.Show(phaItem.Name+e.Message);
                }
                //第二基本计量
                this.neuSpread1_Sheet1.Cells[0, 5].Text = conObj.Name;
                //第二基本计量单位
                this.neuSpread1_Sheet1.Cells[0, 6].Text = conObj.Memo;

                //是否有效
                this.neuSpread1_Sheet1.Cells[0, 7].Value = conObj.IsValid;
                //操作员
                this.neuSpread1_Sheet1.Cells[0, 8].Text = conObj.OperEnvironment.ID;
                //操作日期
                this.neuSpread1_Sheet1.Cells[0, 9].Text = conObj.OperEnvironment.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (!this.Valid())
            {
                return -1;
            }
            this.txtFileter.Text = "";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            consManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (consManager.DelConstant(this.constPhaDoseOnce) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除常数失败：" + consManager.Err));
                return -1;
            }

            FS.HISFC.Models.Base.Const phaDoseOnce = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                phaDoseOnce = new FS.HISFC.Models.Base.Const();

                phaDoseOnce.ID = this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim(); //药品编码
                phaDoseOnce.Name = this.neuSpread1_Sheet1.Cells[i, 5].Text.Trim();//第二基本计量
                phaDoseOnce.Memo = this.neuSpread1_Sheet1.Cells[i, 6].Text.Trim();//第二基本计量单位


                if (this.neuSpread1_Sheet1.Cells[i, 2].Text.Length >= 15)
                {
                    phaDoseOnce.UserCode = this.neuSpread1_Sheet1.Cells[i, 2].Text.Trim().Substring(0, 15);//药品名称
                }


                phaDoseOnce.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 7].Value);
                phaDoseOnce.OperEnvironment.ID = this.neuSpread1_Sheet1.Cells[i, 8].Text.Trim();
                phaDoseOnce.OperEnvironment.OperTime = consManager.GetDateTimeFromSysDateTime();

                if (consManager.SetConstant(this.constPhaDoseOnce, phaDoseOnce) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存失败：" + consManager.Err));
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功"));

            return 0;
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            Hashtable hsCode = new Hashtable();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (hsCode.Contains(this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim()))
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "行编码重复，已存在相同编码项目！");
                    this.neuSpread1_Sheet1.ActiveRowIndex = i;
                    this.neuSpread1.ShowRow(0, this.neuSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                    return false;
                }
                hsCode.Add(this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim(), null);

                for (int index = 0; index < this.neuSpread1_Sheet1.Columns.Count; index++)
                {
                    if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 0].Text))
                    {
                        MessageBox.Show("第" + (i + 1).ToString() + "行编码不能为空！");
                        this.neuSpread1_Sheet1.ActiveRowIndex = i;
                        this.neuSpread1.ShowRow(0, this.neuSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                        return false;
                    }
                    else if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 4].Text))
                    {
                        MessageBox.Show("第" + (i + 1).ToString() + "行名称不能为空！");
                        this.neuSpread1_Sheet1.ActiveRowIndex = i;
                        this.neuSpread1.ShowRow(0, this.neuSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region IMaintenanceControlable 成员 无用

        public int Add()
        {
            this.neuSpread1_Sheet1.AddRows(0, 1);
            this.neuSpread1_Sheet1.ActiveRowIndex = 0;
            this.neuSpread1_Sheet1.ActiveColumnIndex = 0;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 6].Text = "ml";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 7].Value = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 8].Value = consManager.Operator.ID;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 9].Value = this.consManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            this.neuSpread1.ShowRow(0, this.neuSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);

            return 0;
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
            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);

            return 1;
        }

        public int Export()
        {
            //>>导出{72DA7F3E-3446-4025-B21D-1C2465C69D84}
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show("导出成功");
            }
            //<<

            return 1;
        }

        public int Import()
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
        private FS.FrameWork.WinForms.Forms.IMaintenanceForm queryForm;
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

        #endregion

        private void txtFileter_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (string.IsNullOrEmpty(txtFileter.Text))
                {
                    this.neuSpread1_Sheet1.Rows[i].Visible = true;
                }
                else
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 1].Text.Contains(txtFileter.Text.Trim())
                        || this.neuSpread1_Sheet1.Cells[i, 2].Text.Contains(txtFileter.Text.Trim()))
                    {
                        this.neuSpread1_Sheet1.Rows[i].Visible = true;
                        //this.neuSpread1_Sheet1.RowFilter.AllString=
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[i].Visible = false;
                    }
                }
            }
        }
    }
}
