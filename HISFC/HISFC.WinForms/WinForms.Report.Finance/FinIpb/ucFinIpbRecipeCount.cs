using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbRecipeCount :FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbRecipeCount()
        {
            InitializeComponent();
            InitTypeCode();
            InitItemType();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            FS.HISFC.BizLogic.Manager.Department de = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Employee employee = de.Operator as FS.HISFC.Models.Base.Employee;

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(base.beginTime, base.endTime, employee.Nurse.ID,this.cbItemName.SelectedItem.ID,
                this.cbTypeName.SelectedItem.ID,this.cbItemName.SelectedItem.Name,this.cbTypeName.SelectedItem.Name);
        }

        private bool InitTypeCode()
        {
            FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();
            ArrayList alTypeCode = new ArrayList();
            this.cbTypeName.alItems.Clear();
            this.cbTypeName.Items.Clear();
            FS.HISFC.Models.Base.Spell sp = new FS.HISFC.Models.Base.Spell();
            sp.Name = "全部";
            sp.ID = "ALL";
            alTypeCode.Add(sp);
            string strSql = @" SELECT m.type_code,m.type_name FROM met_ipm_kind m";

            strSql = string.Format(strSql);
            DataSet ds = new DataSet();
            if (Manager.ExecQuery(strSql, ref ds) == -1)
            {
                return false;
            }
            if (ds == null || ds.Tables[0] == null)
            {
                MessageBox.Show("查询错误", "警告,药品加载错误！");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                    obj.ID = ds.Tables[0].Rows[i][0].ToString();
                    obj.Name = ds.Tables[0].Rows[i][1].ToString();
                    alTypeCode.Add(obj);
                }
                int c = this.cbTypeName.AddItems(alTypeCode);

            }
            else
            {
                return false;
            }
            this.cbTypeName.SelectedIndex = 0;
            return true;
        }

        private bool InitItemType()
        {
            ArrayList alItemType = new ArrayList();
            FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
            obj.ID = "ALL";
            obj.Name = "全部";
            alItemType.Add(obj);

            FS.HISFC.Models.Base.Spell obj1 = new FS.HISFC.Models.Base.Spell();
            obj1.ID = "1";
            obj1.Name = "药品";
            alItemType.Add(obj1);
            FS.HISFC.Models.Base.Spell obj2 = new FS.HISFC.Models.Base.Spell();
            obj2.ID = "2";
            obj2.Name = "非药品";
            alItemType.Add(obj2);
            this.cbItemName.AddItems(alItemType);
            this.cbItemName.SelectedIndex = 0;
            return true;
        }
    }
}
