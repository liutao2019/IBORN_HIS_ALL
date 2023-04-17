using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    public partial class ucUndrugDictionary : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucUndrugDictionary()
        {
            InitializeComponent();
        }

        #region 变量

        private List<FS.HISFC.Models.Fee.Item.Undrug> undrugList = new List<FS.HISFC.Models.Fee.Item.Undrug>();

        private DataSet dsUndrug = new DataSet();
        private DataTable dtUndrug = new DataTable();
        private DataView dvUndrug = new DataView();
        private string filterInput = "";
        private string mainSettingFilePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "UndrugDictionary.xml";
        #endregion

        #region 私有方法

        /// <summary>
        /// 设置ｆｒｐ
        /// </summary>
        private void InitFrp()
        {
            dsUndrug = new DataSet();
            dtUndrug = new DataTable();
            dvUndrug = new DataView();
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在初始化表格，请稍候.....");
            if (File.Exists(this.mainSettingFilePath))
            {
                
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.mainSettingFilePath, this.dtUndrug, ref this.dvUndrug, this.fpSpread1_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
            }
            else
            {
                this.dtUndrug.Columns.AddRange(new DataColumn[] 
                {
                    new DataColumn("非药品代码", typeof(string)),
                    new DataColumn("非药品名称", typeof(string)),
                    new DataColumn("国标码", typeof(string)),
                    new DataColumn("国际编码", typeof(string)),
                    new DataColumn("系统类别", typeof(string)),
                    new DataColumn("最小费用码", typeof(string)),
                    new DataColumn("拼音码", typeof(string)),
                    new DataColumn("五笔码", typeof(string)),
                    new DataColumn("输入码", typeof(string)),
                    new DataColumn("计价单位", typeof(string)),
                    new DataColumn("有效性标志", typeof(string)),
                    new DataColumn("规格", typeof(string)),
                    new DataColumn("执行科室", typeof(string)),
                    new DataColumn("默认检查部位", typeof(string)),
                    new DataColumn("价格", typeof(decimal)),
                    new DataColumn("特诊价", typeof(decimal)),
                    new DataColumn("儿童价", typeof(decimal)),
                    new DataColumn("确认标志", typeof(bool)),
                    
                    new DataColumn("通用名", typeof(string)),
                    new DataColumn("通用名拼音码", typeof(string)),
                    new DataColumn("通用名五笔码", typeof(string)),
                    new DataColumn("通用名自定义码", typeof(string))
                    
                });

                this.dvUndrug = new DataView(this.dtUndrug);

                this.fpSpread1.DataSource = this.dvUndrug;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 加载非药品信息
        /// </summary>
        private void QueryUndrug()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载非药品信息，请稍候.....");
            Application.DoEvents();
            undrugList = CacheManager.FeeIntegrate.QueryAllItemsList();
            
            this.dtUndrug.Clear();
            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in undrugList)
            {
                DataRow row = this.dtUndrug.NewRow();

                row["非药品代码"] = undrug.ID;
                row["非药品名称"] = undrug.Name;
                row["国标码"] = undrug.GBCode;
                row["国际编码"] = undrug.NationCode;
                row["系统类别"] = undrug.SysClass.Name;
                row["最小费用码"] = undrug.MinFee.Name;
                row["拼音码"] = undrug.SpellCode;
                row["五笔码"] = undrug.WBCode;
                row["输入码"] = undrug.UserCode;
                row["计价单位"] = undrug.PriceUnit;

                if (undrug.ValidState == "1")
                {
                    row["有效性标志"] = "有效";
                }
                else
                {
                    row["有效性标志"] = "无效";
                }

                row["规格"] = undrug.Specs;
                if (string.IsNullOrEmpty(undrug.ExecDept) || undrug.ExecDept == "ALL")
                {
                    row["执行科室"] = "全部";
                }
                else
                {
                    string allExecDept = string.Empty;
                    string[] allDept = undrug.ExecDept.Split('|');
                    for (int i = 0; i < allDept.Length; i++)
                    {
                        allExecDept += FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(allDept[i]) + "|";
                    }
                    row["执行科室"] = allExecDept.TrimEnd('|');
                }
                row["默认检查部位"] = undrug.CheckBody;
                row["价格"] = undrug.Price;
                row["特诊价"] = undrug.SpecialPrice;
                row["儿童价"] = undrug.ChildPrice;
                row["确认标志"] = undrug.IsNeedConfirm;


                row["通用名"] = undrug.NameCollection.OtherName;
                row["通用名拼音码"] = undrug.NameCollection.OtherSpell.SpellCode;
                row["通用名五笔码"] = undrug.NameCollection.OtherSpell.WBCode;
                row["通用名自定义码"] = undrug.NameCollection.OtherSpell.UserCode;

                this.dtUndrug.Rows.Add(row);
            }
            this.dtUndrug.AcceptChanges();

            for (int row = 0; row < fpSpread1_Sheet1.RowCount; row++)
            {
                fpSpread1_Sheet1.Rows[row].BackColor = Color.White;
                if (fpSpread1_Sheet1.Cells[row, 10].Text == "无效")
                {
                    fpSpread1_Sheet1.Rows[row].BackColor = Color.Red;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
                
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            if (!this.DesignMode)
            {
                this.InitFrp();
                this.QueryUndrug();

                try
                {
                    if (System.IO.File.Exists(this.mainSettingFilePath))
                    {
                        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
                    }
                    else
                    {
                        FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
                    }


                }
                catch (Exception ex)
                {
                    //MessageBox.Show("获取数据显示配置文件时出错! 请退出重试" + ex.Message);
                    //GC.Collect();
                    //return;
                }
            }
            return base.OnInit(sender, neuObject, param);
        }

        #endregion
                
        #region 事件
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtFilter.Text);

            queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(queryCode, "'", "%");
            
            queryCode = queryCode + "%";
            this.filterInput = "((拼音码 LIKE '" + queryCode + "') OR " +
                "(五笔码 LIKE '" + queryCode + "') OR " +
                "(输入码 LIKE '" + queryCode + "') OR " +
                "(国标码 LIKE '" + queryCode + "') OR " +

                "(通用名 LIKE '" + queryCode + "') OR " +
                "(通用名拼音码 LIKE '" + queryCode + "') OR " +
                "(通用名五笔码 LIKE '" + queryCode + "') OR " +
                "(通用名自定义码 LIKE '" + queryCode + "') OR " +

                "(非药品名称 LIKE '" + queryCode + "') OR " +
                "(非药品代码 LIKE '" + queryCode + "') )";

            this.dvUndrug.RowFilter = filterInput;
            if (System.IO.File.Exists(this.mainSettingFilePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
            }

            for (int row = 0; row < fpSpread1_Sheet1.RowCount; row++)
            {
                fpSpread1_Sheet1.Rows[row].BackColor = Color.White;
                if (fpSpread1_Sheet1.Cells[row, 10].Text == "无效")
                {
                    fpSpread1_Sheet1.Rows[row].BackColor = Color.Red;
                }
            }
        }

        private void linkLblSet_Click(object sender, EventArgs e)
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn uc = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            uc.FilePath = this.mainSettingFilePath;
            uc.SetDataTable(this.mainSettingFilePath, this.fpSpread1_Sheet1);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "显示设置";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            uc.DisplayEvent += new EventHandler(ucSetColumn_DisplayEvent);
            this.ucSetColumn_DisplayEvent(null, null);
        }

        private void ucSetColumn_DisplayEvent(object sender, EventArgs e)
        {
            this.InitFrp();
            this.QueryUndrug();
        }
        
        #endregion

        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.mainSettingFilePath);
        }
    }
}

