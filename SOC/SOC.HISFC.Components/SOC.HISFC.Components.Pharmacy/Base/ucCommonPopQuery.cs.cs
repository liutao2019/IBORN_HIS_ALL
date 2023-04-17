using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.HISFC.Components.Pharmacy.Base
{
       public partial class ucCommonPopQuery : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
       {
     #region 构造方法

        public ucCommonPopQuery()
        {
            InitializeComponent();

            this.dtpBegin.Value = itemManager.GetDateTimeFromSysDateTime().Date.AddMonths(-1).AddDays(1);
            this.dtpEnd.Value = itemManager.GetDateTimeFromSysDateTime().Date.AddDays(1);
        }

        public ucCommonPopQuery(string sqlStr, string[] argColumnWith)
        {
            InitializeComponent();

            this.sqlStr = sqlStr;
            this.argColumnWith = argColumnWith;
            this.ShowData();
        }

        #endregion

        #region 变量

        Neusoft.HISFC.BizLogic.Pharmacy.Item itemManager = new Neusoft.HISFC.BizLogic.Pharmacy.Item();

        private string sqlStr = "";

        private string[] argColumnWith;        

        #endregion

        #region 属性

        public string TopInfo
        {
            set
            {
                this.lblTopInfo.Text = value;
            }
        }

        public string BottomInfo
        {
            set
            {
                this.lblBottomInfo.Text = value;
            }
        }

        public int RecordCount
        {
            get
            {
                return this.fpMainList.RowCount;
            }
        }

        public bool IsShowConditionPanel
        {
            set
            {
                this.pnlTopCondition.Visible = value;
            }
        }

        public string SqlStr
        {
            get 
            { 
                return sqlStr; 
            }
            set 
            { 
                sqlStr = value; 
            }
        }

        public string[] ArgColumnWith
        {
            get
            {
                return argColumnWith;
            }
            set
            {
                argColumnWith = value;
            }
        }

        #endregion

        #region 方法

        public void ShowData()
        {
            DataSet ds = new DataSet();
            if (this.itemManager.ExecQuery(this.sqlStr, ref ds) == -1)
            {
                MessageBox.Show("查询失败!");
                return;
            }
            if (ds != null)
            {
                try
                {
                    this.fpMainList.DataSource = ds.Tables[0].DefaultView;
                    for (int i = 0; i < this.argColumnWith.Length; i++)
                    {
                        this.fpMainList.Columns[i].Width = Convert.ToInt32(this.argColumnWith[i]);
                    }
                }
                catch
                { 
                }
            }
        }

        #endregion

        #region 事件

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            if (this.dtpBegin.Value > this.dtpEnd.Value)
            {
                MessageBox.Show( "开始时间不能大于结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }

            this.sqlStr = string.Format(sqlStr, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
            this.ShowData();
        }

        #endregion        
    }
}
