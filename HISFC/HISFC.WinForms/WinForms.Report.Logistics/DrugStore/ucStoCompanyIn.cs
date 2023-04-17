using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.Report.Logistics.DrugStore
{
    public partial class ucStoCompanyIn : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucStoCompanyIn()
        {
            InitializeComponent();
            this.myInit();
            this.GetItemzFunctionInfo();
        }

        private void myInit()
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alItemType = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            if (alItemType == null)
            {
                MessageBox.Show(Language.Msg("���ݳ�������ȡҩƷ�������Ʒ�������!") + consManager.Err);
                return;
            }
            this.cmbType.AddItems(alItemType);
            //FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            //string sqlSentence = @"select t.code,t.name from com_dictionary t where t.type = 'ITEMTYPE' order by sort_id";
            //if (dataManager.ExecQuery(sqlSentence, ref ds) == -1)
            //{
            //   MessageBox.Show(Language.Msg("��ȡҩƷ���ͷ�������"));
            //    return;
            //}
            //else if (ds != null && ds.Tables.Count > 0)
            //{
            //    this.cmbStockDept.Items.Clear();

            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        this.cmbStockDept.Items.Add(dr[1]);
                    
            //    }
            //}
         }
        private bool GetItemzFunctionInfo()
        {
            FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();
            System.Collections.ArrayList alUsecodeList = new ArrayList();
            this.neuComboBox1.alItems.Clear();
            this.neuComboBox1.Items.Clear();
            string strSql = @"select fac_code,fac_name,spell_code,wb_code from pha_com_company";

            strSql = string.Format(strSql);
            DataSet ds = new DataSet();
            if (Manager.ExecQuery(strSql, ref ds) == -1)
            {
                return false;
            }
            if (ds == null || ds.Tables[0] == null)
            {
                MessageBox.Show("��ѯ����", "����,�÷����ش���");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                    obj.ID = ds.Tables[0].Rows[i][0].ToString();
                    obj.Name = ds.Tables[0].Rows[i][1].ToString();
                    obj.SpellCode = ds.Tables[0].Rows[i][2].ToString();
                    obj.WBCode = ds.Tables[0].Rows[i][3].ToString();
                    alUsecodeList.Add(obj);
                }
                int c = this.neuComboBox1.AddItems(alUsecodeList);

            }
            else
            {
                return false;
            }
            return true;
        }
        
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1 )
            {
                return -1;
            }
            return base.OnRetrieve(base.beginTime,base.endTime,neuComboBox1.Tag, cmbType.Tag);
        }
       
    }
}

