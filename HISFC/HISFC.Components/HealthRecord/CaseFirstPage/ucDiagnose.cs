using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// 诊断项目列表
    /// </summary>
    public partial class ucDiagnose : UserControl
    {
        public ucDiagnose()
        {
            InitializeComponent();
        }

        private ArrayList almc = new ArrayList();

        /// <summary>
        /// 诊断列表dataset
        /// </summary>
        private DataSet dsDiag = null;

        /// <summary>
        /// 列宽配置文件
        /// </summary>
        string xmlPath = Application.StartupPath + "\\Profile\\outdiagnose.xml";

        private FS.HISFC.BizLogic.HealthRecord.ICD icdManager = new FS.HISFC.BizLogic.HealthRecord.ICD();

        public delegate int MyDelegate(Keys key);

        /// <summary>
        /// 双击、回车项目列表时执行的事件
        /// </summary>
        public event MyDelegate SelectItem;

        public bool isDrag = false;

        /// <summary>
        /// 存放诊断列表
        /// </summary>
        public Hashtable hsDiags;

        #region 属性

        /// <summary>
        /// 传入的诊断列表
        /// </summary>
        ArrayList alDiag = new ArrayList();

        /// <summary>
        /// 传入的诊断列表
        /// </summary>
        public ArrayList AlDiag
        {
            get
            {
                return alDiag;
            }
            set
            {
                alDiag = value;
                this.Retrieve();
            }
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            //this.alDiag = this.icdManager.Query(ICDTypes.ICD10, QueryTypes.Valid);

            //Retrieve();
            return 0;
        }

        /// <summary>
        /// 显示诊断
        /// </summary>
        private void Retrieve()
        {
            //if (dsDiag == null)
            //{
            //    dsDiag = new DataSet();

            //    dsDiag.Tables.Add("items");
            //    dsDiag.Tables[0].Columns.AddRange(new DataColumn[]
            //    {
            //        new DataColumn("icd_code",Type.GetType("System.String")),
            //        new DataColumn("icd_name",Type.GetType("System.String")),
            //        new DataColumn("spell_code",Type.GetType("System.String"))					
            //    });
            //    dsDiag.CaseSensitive = false;
            //}

            //hsDiags = new Hashtable();

            //if (alDiag != null)
            //{
            //    if (dsDiag != null && dsDiag.Tables.Count > 0)
            //    {
            //        this.dsDiag.Tables[0].Clear();
            //    }
            //    //绑定到farPoint的DataSource时，赋值速度会很慢....
            //    this.fpSpread1.DataSource = null;
            //    this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
            //    foreach (FS.HISFC.Models.HealthRecord.ICD item in alDiag)
            //    {
            //        dsDiag.Tables[0].Rows.Add(new object[] { item.ID, item.Name, item.SpellCode });

            //        if (!hsDiags.Contains(item.ID))
            //        {
            //            hsDiags.Add(item.ID, item);
            //        }
            //    }

            //    if (dsDiag != null && dsDiag.Tables.Count > 0)
            //    {
            //        fpSpread1.DataSource = dsDiag.Tables[0].DefaultView;
            //    }

                //fpSpread1_Sheet1.Columns[0].Width = 66F;
                //fpSpread1_Sheet1.Columns[1].Width = 251F;
                //fpSpread1_Sheet1.Columns[2].Width = 90F;
                //fpSpread1_Sheet1.Columns[2].Visible = false;
            //}
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int Filter(string text)
        {
            //text = "(icd_code like '%" + text.Trim() + "%') or " +
            //     "(spell_code like '%" + text.Trim() + "%') or " +
            //     "(icd_name like '%" + text.Trim() + "%')";

            //try
            //{
            //    dsDiag.Tables[0].DefaultView.RowFilter = text;

            //    if (fpSpread1_Sheet1.Rows.Count == 1 && this.isDrag)
            //    {
            //        if (SelectItem != null)
            //        {
            //            this.SelectItem(Keys.Enter);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            return 0;
        }

        /// <summary>
        /// 下一行
        /// </summary>
        /// <returns></returns>
        public int NextRow()
        {
            int row = fpSpread1_Sheet1.ActiveRowIndex;
            if (row < fpSpread1_Sheet1.RowCount - 1)
            {
                fpSpread1_Sheet1.ActiveRowIndex = row + 1;
                fpSpread1_Sheet1.AddSelection(row + 1, 0, 1, 1);
                this.fpSpread1.ShowRow(0, this.fpSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
            }
            return 0;
        }

        /// <summary>
        /// 上一行
        /// </summary>
        /// <returns></returns>
        public int PriorRow()
        {
            int row = fpSpread1_Sheet1.ActiveRowIndex;
            if (row > 0)
            {
                fpSpread1_Sheet1.ActiveRowIndex = row - 1;
                fpSpread1_Sheet1.AddSelection(row - 1, 0, 1, 1);
                this.fpSpread1.ShowRow(0, this.fpSpread1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
            }
            return 0;
        }


        /// <summary>
        /// 返回选择项
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetItem(ref FS.HISFC.Models.HealthRecord.ICD item)
        {
            int row = fpSpread1_Sheet1.ActiveRowIndex;

            if (row < 0 || fpSpread1_Sheet1.RowCount == 0)
            {
                item = null;
                return -1;
            }
            string icdCode = fpSpread1_Sheet1.GetText(row, 0);//项目代码
            string icdName = fpSpread1_Sheet1.GetText(row, 1);

            foreach (FS.HISFC.Models.HealthRecord.ICD icd in alDiag)
            {
                if (icd.ID == icdCode && icd.Name == icdName)
                {
                    item = icd;
                    return 0;
                }
            }

            item = null;
            return -1;
        }

        /// <summary>
        /// 添加焦点
        /// </summary>
        /// <returns></returns>
        public void SetFocus()
        {
            this.fpSpread1.Focus();
        }

        /// <summary>
        /// 回车选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                if (SelectItem != null)
                {
                    this.SelectItem(Keys.Enter);
                }
            }
        }

        /// <summary>
        /// 双击选择项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
            {
                return;
            }

            if (SelectItem != null)
            {
                this.SelectItem(Keys.Enter);
            }
        }
        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.fpSpread1_Sheet1, xmlPath);
        }

        private void ucDiagnose_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(xmlPath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.fpSpread1_Sheet1, xmlPath);
            }
        }

        #region  处理病案获取诊断慢的情况chengym
        //static DataSet ds = new DataSet();
        DataSet ds = new DataSet();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int InitCase()
        {
            this.icdManager.QueryCase(ICDTypes.ICD10, QueryTypes.Valid, ref ds);

            //if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            //{
            //    this.icdManager.QueryCase(ICDTypes.ICD10, QueryTypes.Valid, ref ds);
            //} 
            fpSpread1.DataSource = ds.Tables[0].DefaultView;
            
            return 0;
        }

        /// <summary>
        /// 返回选择项
        /// </summary>
        /// <param name="icd"></param>
        /// <returns></returns>
        public int GetItemCase(ref FS.HISFC.Models.HealthRecord.ICD icd)
        {
            int row = fpSpread1_Sheet1.ActiveRowIndex;

            if (row < 0 || fpSpread1_Sheet1.RowCount == 0)
            {
                icd = null;
                return -1;
            }

            icd = new FS.HISFC.Models.HealthRecord.ICD();
            icd.ID = fpSpread1_Sheet1.GetText(row, 0); //ICD编码
            icd.Name = fpSpread1_Sheet1.GetText(row, 1); ; //ICD名称
            icd.SpellCode = fpSpread1_Sheet1.GetText(row, 2);     //拼音码
            icd.WBCode = fpSpread1_Sheet1.GetText(row, 3);     //五笔码
            return 0;
        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int FilterCase(string text)
        {
            if (this.cbAccurateFilter.Checked && text.Trim()!="")
            {
                text = "(icd_code like '" + text.Trim() + "') or " +
                     "(spell_code like '" + text.Trim() + "') or " +
                     "(wb_code like '" + text.Trim() + "') or " +
                     "(icd_name like '" + text.Trim() + "')";
            }
            else  if (this.cbBeforFilter.Checked)
            {
                text = "(icd_code like '" + text.Trim() + "%') or " +
                     "(spell_code like '" + text.Trim() + "%') or " +
                     "(wb_code like '" + text.Trim() + "%') or " +
                     "(icd_name like '" + text.Trim() + "%')";
            }
            else
            {
                text = "(icd_code like '%" + text.Trim() + "%') or " +
                     "(spell_code like '%" + text.Trim() + "%') or " +
                     "(wb_code like '%" + text.Trim() + "%') or " +
                     "(icd_name like '%" + text.Trim() + "%')";
            }
            
            try
            {
                ds.Tables[0].DefaultView.RowFilter = text;

                if (fpSpread1_Sheet1.Rows.Count == 1 && this.isDrag)
                {
                    if (SelectItem != null)
                    {
                        this.SelectItem(Keys.Enter);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0;
        }
        #endregion

        private void cbBeforFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbBeforFilter.Checked)
            {
                this.cbAccurateFilter.Checked = false;
            }
        }

        private void cbAccurateFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAccurateFilter.Checked)
            {
                this.cbBeforFilter.Checked = false;
            }
        }
    }
}
