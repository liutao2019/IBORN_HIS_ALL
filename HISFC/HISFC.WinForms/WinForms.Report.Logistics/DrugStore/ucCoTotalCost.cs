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
    public partial class ucCoTotalCost : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucCoTotalCost()
        {
            InitializeComponent();
            this.myInit();
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
        
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1 || this.cmbType.SelectedIndex == -1)
            {
                return -1;
            }
            return base.OnRetrieve(base.beginTime,base.endTime,cmbType.Tag);
        }
       
    }
}

