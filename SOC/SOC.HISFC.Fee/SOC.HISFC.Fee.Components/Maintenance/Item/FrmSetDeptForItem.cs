using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Item
{
    partial class FrmSetDeptForItem : Form
    {
        public FrmSetDeptForItem()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable dtTable = new DataTable();
        /// <summary>
        /// 非药品数据集
        /// </summary>
        ArrayList itemList = null;
        /// <summary>
        /// 科室数据集
        /// </summary>
        ArrayList deptList = null;
        /// <summary>
        /// 用户选择的科室
        /// </summary>
        string chooseDeptList = string.Empty;
        /// <summary>
        /// 返回结果
        /// </summary>
        DialogResult rs = new DialogResult();
        /// <summary>
        /// 科室业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 传入科室信息
        /// </summary>
        private string deptStr = "";

        /// <summary>
        /// 科室名称
        /// </summary>
        private string chooseDeptListName;

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ChooseDeptListName
        {
            get
            {
                return chooseDeptListName;
            }
            set
            {
                chooseDeptListName = value;
            }
        }

        #endregion

        #region  属性
        /// <summary>
        /// 返回结果
        /// </summary>
        public DialogResult Rs
        {
            get
            {
                return rs;
            }
            set
            {
                rs = value;
            }
        }
        /// <summary>
        /// 返回科室
        /// </summary>
        public string ChooseDeptList
        {
            get
            {
                return chooseDeptList;
            }
            set
            {
                chooseDeptList = value;
            }
        }
        /// <summary>
        /// 传入科室信息
        /// </summary>
        public string DeptStr
        {
            get
            {
                return deptStr;
            }
            set
            {
                deptStr = value;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 初始化FP
        /// </summary>
        /// <returns></returns>
        private int InitDatatable()
        {
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            dtTable.Columns.AddRange(new DataColumn[]{new DataColumn("选择", dtBool),
															new DataColumn("科室编码", dtStr),   
															new DataColumn("科室名称", dtStr),
                                                            new DataColumn("科室类型", dtStr),
                                                            new DataColumn("拼音码",dtStr),
                                                            new DataColumn("五笔码",dtStr),
                                                            new DataColumn("自定义码")
            });

            this.neuSpread1_Sheet1.DataSource = dtTable.DefaultView;
            return 1;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        private int InitData()
        {
            ArrayList list = new ArrayList();
            this.deptList = new ArrayList();
            list = this.deptManager.GetDeptmentAll();
            foreach (FS.HISFC.Models.Base.Department dpt in list)
            {
                //只取门诊住院终端手术的科室
                if (dpt.DeptType.ID.ToString() == "C" 
                    || dpt.DeptType.ID.ToString() == "I" 
                    || dpt.DeptType.ID.ToString() == "T" 
                    || dpt.DeptType.ID.ToString() == "OP"
                    || dpt.DeptType.ID.ToString() == "N"
                    )
                {
                    deptList.Add(dpt);
                }
            }
            if (deptList.Count > 0)
            {
                FS.HISFC.Models.Base.Department allObject = new FS.HISFC.Models.Base.Department();
                allObject.ID = "ALL";
                allObject.Name = "全部";
                allObject.Memo = "全部";
                deptList.Insert(0, allObject);
                int i = 0;
                foreach (FS.HISFC.Models.Base.Department dpt in deptList)
                {
                    //this.SetDatatable( dpt.ID, dpt.Name, dpt.DeptType.ToString(), i, dpt.SpellCode, dpt.WBCode, dpt.UserCode );

                    this.SetDataToTable(dpt);

                    i++;
                }
            }
            if (this.deptStr.ToString().Length > 0) //存在已维护科室
            {
                if (deptStr.ToString().Contains("全部") || deptStr.ToString().Contains("ALL"))
                {
                    this.neuSpread1_Sheet1.Cells[0, (int)ItemColumns.value].Value = true;
                }
                else
                {
                    string[] str = this.deptStr.ToString().Split('|');
                    for (int i = 0; i < str.Length; i++)
                    {
                        for (int j = 1; j < this.neuSpread1_Sheet1.Rows.Count; j++)
                        {
                            if (str[i].ToString() == this.neuSpread1_Sheet1.Cells[j, (int)ItemColumns.deptCode].Text)
                            {
                                this.neuSpread1_Sheet1.Cells[j, (int)ItemColumns.value].Value = true;
                            }
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 向数据表内添加数据
        /// </summary>
        /// <param name="info"></param>
        private void SetDataToTable(FS.HISFC.Models.Base.Department info)
        {
            DataRow dr = this.dtTable.NewRow();

            dr["选择"] = false;
            dr["科室编码"] = info.ID;
            dr["科室名称"] = info.Name;
            dr["科室类型"] = info.DeptType.ToString();
            dr["拼音码"] = info.SpellCode;
            dr["五笔码"] = info.WBCode;
            dr["自定义码"] = info.UserCode;

            this.dtTable.Rows.Add(dr);
        }

        /// <summary>
        /// 设置DT格式
        /// </summary>
        /// <returns></returns>
        private int SetDatatable(string deptNo, string deptName, string deptType, int i, string spellCode, string wbCode, string userCode)
        {
            FS.FrameWork.WinForms.Controls.NeuCheckBox ckb = new FS.FrameWork.WinForms.Controls.NeuCheckBox();

            this.neuSpread1_Sheet1.Rows.Add(i, 1);

            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.deptCode].Text = deptNo;
            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.deptCode].Locked = true;
            this.neuSpread1_Sheet1.Columns[(int)ItemColumns.deptCode].AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns[(int)ItemColumns.deptCode].AllowAutoSort = true;

            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.deptName].Text = deptName;
            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.deptName].Locked = true;
            this.neuSpread1_Sheet1.Columns[(int)ItemColumns.deptName].AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns[(int)ItemColumns.deptName].AllowAutoSort = true;

            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.deptType].Text = deptType;
            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.deptType].Locked = true;
            this.neuSpread1_Sheet1.Columns[(int)ItemColumns.deptType].AllowAutoFilter = true;
            this.neuSpread1_Sheet1.Columns[(int)ItemColumns.deptType].AllowAutoSort = true;

            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.spellCode].Text = spellCode;
            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.spellCode].Locked = true;

            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.wbCode].Text = wbCode;
            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.wbCode].Locked = true;

            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.userCode].Text = userCode;
            this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.userCode].Locked = true;

            return 1;
        }
        #endregion

        #region 枚举

        /// <summary>
        /// 列信息
        /// </summary>
        protected enum ItemColumns
        {
            /// <summary>
            /// 选择
            /// </summary>
            value,
            /// <summary>
            /// 开立科室代码
            /// </summary>
            deptCode,
            /// <summary>
            /// 开立科室名称
            /// </summary>
            deptName,
            /// <summary>
            /// 科室类型
            /// </summary>
            deptType,
            spellCode,
            wbCode,
            userCode

        }
        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.InitDatatable();
            this.InitData();
            int i = 0;
            for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[j, (int)ItemColumns.value].Value) == true)
                {
                    i++;
                }
            }
            this.lblTxt.Text = "当前已经选中科室" + i + "条!";
            base.OnLoad(e);
        }
        /// <summary>
        /// 重选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.value].Value = false;
                }
            }
            this.rs = DialogResult.Cancel;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.value].Value))
                    {
                        string dpt = this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.deptCode].Text.ToString();
                        this.chooseDeptList += (dpt + "|");

                        string strDeptName = this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.deptName].Text.ToString();
                        this.chooseDeptListName += (strDeptName + "|");
                    }
                }
            }

            this.Close();

            this.Rs = DialogResult.OK;
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.dtTable.DefaultView.RowFilter = "1=1";

            if (this.chkFilter.Checked)
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.value].Value) == false)
                        {
                            this.neuSpread1_Sheet1.Rows[i].Visible = false;
                        }
                    }
                }
            }
            else
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        this.neuSpread1_Sheet1.Rows[i].Visible = true;

                    }
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = string.Format("拼音码 like '%{0}%' or 五笔码 like '%{0}%' or 自定义码 like '%{0}%'", this.txtFilter.Text.Trim());

            this.dtTable.DefaultView.RowFilter = filter;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void ckChooseAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, (int)ItemColumns.value].Value = this.ckChooseAll.Checked;
            }
        }
        #endregion


    }
}
