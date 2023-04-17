using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen.Maintenance
{
    public partial class ucDrugCompare : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDrugCompare()
        {
            InitializeComponent();
        }

        FS.SOC.HISFC.BizLogic.Pharmacy.Item drugItemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
        BizLogic.DrugCompare drugCompareMgr = new FS.SOC.HISFC.SocialSecurity.ShenZhen.BizLogic.DrugCompare();
        System.Data.DataSet dtDetail = new DataSet();
        ArrayList alDrug = new ArrayList();
        Hashtable hsCompareItem = new Hashtable();

        int Sequence = 0;

        #region 变量属性

        /// <summary>
        /// 配置文件
        /// </summary>
        private string settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPSocialSecrityDrugCompareSetting.xml";

        /// <summary>
        /// FarPoint配置文件
        /// </summary>
        [Description("汇总信息FarPoint配置文件"), Category("设置"), Browsable(true)]
        public string SettingFile
        {
            get { return settingFile; }
        }


        #endregion

        #region 工具栏
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("新增", "新增对照信息", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("复制", "复制药品", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("删除", "删除对照信息", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "新增")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "复制")
            {
                this.Copy();
            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.Delete();
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        #endregion

        private string GetKey()
        {
            this.Sequence++;
            return this.Sequence.ToString().PadLeft(5,'0') + "";
        }

        private int Query()
        {
            this.hsCompareItem.Clear();
            if (this.dtDetail.Tables.Count > 0)
            {
                this.dtDetail.Tables[0].BeginLoadData();
            }
            if (drugCompareMgr.QueryDetail(ref dtDetail) == -1)
            {
                MessageBox.Show(this, "请系统管理员联系并报告错误：获取对信息发送错误，" + drugCompareMgr.Err, "错误>>");
            }
            if (dtDetail.Tables.Count > 0)
            {
                foreach (DataRow row in this.dtDetail.Tables[0].Rows)
                {
                    row["KEY"] = this.GetKey();
                    FS.FrameWork.Models.NeuObject neuObject = new FS.FrameWork.Models.NeuObject();
                    neuObject.ID = row["KEY"].ToString();
                    neuObject.Name = row["药品编码"].ToString();
                    neuObject.Memo = row["对照编码"].ToString();

                    hsCompareItem.Add(neuObject.ID, neuObject);
                }
                this.fpSpread1_Sheet1.DataSource = this.dtDetail.Tables[0].DefaultView;

                if (this.dtDetail.Tables[0].PrimaryKey == null || this.dtDetail.Tables[0].PrimaryKey.Length == 0)
                {
                    DataColumn[] keys = new DataColumn[1];

                    keys[0] = this.dtDetail.Tables[0].Columns["KEY"];
                    this.dtDetail.Tables[0].Columns["KEY"].ReadOnly = true;
                    this.dtDetail.Tables[0].PrimaryKey = keys;

                }
            }
            this.dtDetail.Tables[0].EndLoadData();
            
            dtDetail.AcceptChanges();

            this.fpSpread1.ReadSchema(this.settingFile);

            return 1;
        }

        private int Add()
        {
            if (this.dtDetail.Tables.Count == 0)
            {
                return 0;
            }

            //初始化窗口
            FS.FrameWork.WinForms.Forms.frmEasyChoose frmPop = new FS.FrameWork.WinForms.Forms.frmEasyChoose(alDrug);
            frmPop.Text = "请选择项目";
            frmPop.StartPosition = FormStartPosition.CenterScreen;
            if (frmPop.ShowDialog(this) != DialogResult.OK)
            {
                return 0;
            }

            FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(frmPop.Object.ID);
            if (item != null)
            {
                DataRow row = this.dtDetail.Tables[0].NewRow();
                //注意过滤的相当于sql语句，避免新加的行有null值
                row["药品编码"] = item.ID;
                row["自定义码"] = item.UserCode;
                row["药品名称"] = item.Name;
                row["规格"] = item.Specs;
                row["药品停用"] = ((item.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid) ? "否" : "是");
                row["对照编码"] = this.ntxtSocialSecrityInfo.Text;
                row["拼音码"] = item.SpellCode;
                row["五笔码"] = item.WBCode;
                row["KEY"] = this.GetKey(); 

                this.dtDetail.Tables[0].Rows.Add(row);
                this.fpSpread1_Sheet1.ActiveRowIndex = this.fpSpread1_Sheet1.RowCount - 1;

            }

            return 1;
        }

        private int Copy()
        {
            int rowIndex = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (rowIndex < 0 && this.fpSpread1_Sheet1.Rows.Count == 1)
            {
                rowIndex = 1;
            }
            if (rowIndex > -1 || this.fpSpread1_Sheet1.Rows.Count > 0)
            {
                string key = this.fpSpread1.GetCellText(0, rowIndex, "KEY");
                string drugNO = this.fpSpread1.GetCellText(0, rowIndex, "药品编码");
                if (string.IsNullOrEmpty(drugNO))
                {
                    return 0;
                }
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(drugNO);
                if (item != null && !string.IsNullOrEmpty(item.ID))
                {
                    DataRow row = this.dtDetail.Tables[0].NewRow();
                    //注意过滤的相当于sql语句，避免新加的行有null值
                    row["药品编码"] = item.ID;
                    row["自定义码"] = item.UserCode;
                    row["药品名称"] = item.Name;
                    row["规格"] = item.Specs;
                    row["药品停用"] = ((item.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid) ? "否" : "是");
                    row["对照编码"] = "";
                    row["拼音码"] = item.SpellCode;
                    row["五笔码"] = item.WBCode;
                    row["KEY"] = this.GetKey();
                    int index = -1;
                    DataRow findedRow = this.dtDetail.Tables[0].Rows.Find(key);
                    if (findedRow != null)
                    {
                        index = this.dtDetail.Tables[0].Rows.IndexOf(findedRow);
                    }
                    this.dtDetail.Tables[0].Rows.InsertAt(row, index + 1);
                    this.fpSpread1_Sheet1.ActiveRowIndex = index + 1;
                }
            }

            return 1;
        }

        private int Delete()
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                return 0;
            }
            int rowIndex = this.fpSpread1_Sheet1.ActiveRowIndex;
            DialogResult dr = MessageBox.Show("删除数据无法恢复，确定删除第" + (rowIndex + 1).ToString() + "行的数据吗？", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return 0;
            }


            //string drugNO = this.fpSpread1.GetCellText(0, rowIndex, "药品编码");
            //string compareItemNO = this.fpSpread1.GetCellText(0, rowIndex, "对照编码");
            //删除的数据必须考虑是否已经在数据库保存过，因为界面允许全字段改动
            //有可能在删除前把原来的药品编码甚至对照编码改掉，如此删除本行数据外还删除原有对照关系
            string key = this.fpSpread1.GetCellText(0, rowIndex, "KEY");

            if (hsCompareItem.Contains(key))
            {
                FS.FrameWork.Models.NeuObject neuObject = hsCompareItem[key] as FS.FrameWork.Models.NeuObject;
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (this.drugCompareMgr.DeleteDetail(neuObject.Name, neuObject.Memo) == 1)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                }
            }
            this.fpSpread1_Sheet1.Rows.Remove(rowIndex, 1);

            return 1;
        }

        private int Save()
        {
            fpSpread1.StopCellEditing();
            if (this.dtDetail.Tables.Count == 0)
            {
                return 0;
            }
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                return 0;
            }
            if (fpSpread1_Sheet1.RowCount > 0)   
            {
                dtDetail.Tables[0].Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }

            string errInfo = "";
            DataSet modifyDataSet = this.dtDetail.GetChanges(DataRowState.Added | DataRowState.Modified);
            if (modifyDataSet == null || modifyDataSet.Tables.Count == 0 || modifyDataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("数据没有改变！");
                return 0;
            }
            foreach (DataRow row in modifyDataSet.Tables[0].Rows)
            {
                FS.SOC.HISFC.SocialSecurity.ShenZhen.Models.DrugCompare Drupcompare = new FS.SOC.HISFC.SocialSecurity.ShenZhen.Models.DrugCompare();
                ArrayList aldrupcompare = new ArrayList();
                string key = row["KEY"].ToString();
                string drugNO = row["药品编码"].ToString();
                string compareItemNO = row["对照编码"].ToString();
                string compareCommonts = row["对照说明"].ToString();
                string extend = row["扩展"].ToString();
                Drupcompare.ID = drugNO;
                Drupcompare.CompareCode = compareItemNO;
                Drupcompare.CompareMemo = compareCommonts;
                Drupcompare.Oper.ID = drugCompareMgr.Operator.ID;
             //   Drupcompare.Oper.OperTime =  drugCompareMgr.GetSysDateTime();
                aldrupcompare.Add(Drupcompare);


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (hsCompareItem.Contains(key))
                {
                    FS.FrameWork.Models.NeuObject neuObject = hsCompareItem[key] as FS.FrameWork.Models.NeuObject;
                    //有可能是改动原有数据的药品编码和对照编码，这个就需要删除原有数据
                    if (drugNO != neuObject.Name || compareItemNO != neuObject.Memo)
                    {
                        if (this.drugCompareMgr.DeleteDetail(neuObject.Name, neuObject.Memo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this, "请系统管理员联系并报告错误：保存对照信息发生错误，" + drugCompareMgr.Err, "错误>>");
                            return -1;
                        }

                        Function.SendBizMessage(aldrupcompare, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugCompare, ref errInfo);
                    }
                }
                if (!string.IsNullOrEmpty(drugNO) && !string.IsNullOrEmpty(compareItemNO))
                {
                    if (this.drugCompareMgr.InsertDetail(drugNO, compareItemNO, compareCommonts, extend) != 1)
                    {
                        if (this.drugCompareMgr.UpdateDetail(drugNO, compareItemNO, compareCommonts, extend) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this, "请系统管理员联系并报告错误：保存对照信息发生错误，" + drugCompareMgr.Err, "错误>>");
                            return -1;
                        }
                        Function.SendBizMessage(aldrupcompare, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugCompare, ref errInfo);
                    }
                    Function.SendBizMessage(aldrupcompare, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugCompare, ref errInfo);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            this.dtDetail.AcceptChanges();
            this.fpSpread1.ReadSchema(this.settingFile);
            MessageBox.Show("保存成功！");
            return this.fpSpread1_Sheet1.Rows.Count;
        }

        protected override void OnLoad(EventArgs e)
        {
          
            this.Query();

            string value= SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, "OperSet", "RefreshAfterSave", this.ncbRefreshAfterSave.Checked.ToString());
            this.ncbRefreshAfterSave.Checked = FS.FrameWork.Function.NConvert.ToBoolean(value);

            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.ntxtDrugInfo.TextChanged += new EventHandler(ntxtDrugInfo_TextChanged);
            this.ntxtSocialSecrityInfo.TextChanged += new EventHandler(ntxtSocialSecrityInfo_TextChanged);
            this.ncbRefreshAfterSave.CheckedChanged += new EventHandler(ncbRefreshAfterSave_CheckedChanged);


            base.OnLoad(e);
        }

        void ncbRefreshAfterSave_CheckedChanged(object sender, EventArgs e)
        {
            SOC.Public.XML.SettingFile.SaveSetting(this.settingFile, "OperSet", "RefreshAfterSave", this.ncbRefreshAfterSave.Checked.ToString());
        }

        void ntxtSocialSecrityInfo_TextChanged(object sender, EventArgs e)
        {
            if (this.dtDetail.Tables.Count == 0)
            {
                return;
            }

            //注意过滤的相当于sql语句，避免新加的行有null值
            this.dtDetail.Tables[0].DefaultView.RowFilter
                = string.Format("对照编码 like '%{0}%' and (药品名称 like  '%{1}%' or 自定义码 like '%{1}%' or 拼音码 like '%{1}%' or 五笔码 like '%{1}%') ", this.ntxtSocialSecrityInfo.Text, this.ntxtDrugInfo.Text);

            this.fpSpread1.ReadSchema(this.settingFile);
        }

        void ntxtDrugInfo_TextChanged(object sender, EventArgs e)
        {
            if (this.dtDetail.Tables.Count == 0)
            {
                return;
            }
            //注意过滤的相当于sql语句，避免新加的行有null值
            this.dtDetail.Tables[0].DefaultView.RowFilter
                = string.Format("对照编码 like '%{0}%' and (药品名称 like  '%{1}%' or 自定义码 like '%{1}%' or 拼音码 like '%{1}%' or 五笔码 like '%{1}%') ", this.ntxtSocialSecrityInfo.Text, this.ntxtDrugInfo.Text);

            this.fpSpread1.ReadSchema(this.settingFile);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() != -1)
            {
                if (this.ncbRefreshAfterSave.Checked)
                {
                    this.Query();
                }
            }
            return base.OnSave(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.fpSpread1.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return base.Export(sender, neuObject);
        }

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.fpSpread1.SaveSchema(this.settingFile);
        }


        #region IPreArrange 成员

        public int PreArrange()
        {
            List<FS.HISFC.Models.Pharmacy.Item> listItem = this.drugItemMgr.QueryItemList();
            if (listItem == null)
            {
                MessageBox.Show(this, "请系统管理员联系并报告错误：获取药品基本信息失败，" + drugItemMgr.Err, "错误>>");
                return -1;
            }
            foreach (FS.HISFC.Models.Pharmacy.Item item in listItem)
            {
                if (item.SpecialFlag2.ToString() == "0")//过滤不是限制用药
                    continue;
                FS.HISFC.Models.Base.Spell spell = (FS.HISFC.Models.Base.Spell)item;
                spell.Memo = item.Specs;
                alDrug.Add(spell);
            }

            return 1;
        }

        #endregion
    }
}