using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.WinForms.Report.DrugStore
{
    public partial class ucPhaGetSalesTotBill : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucPhaGetSalesTotBill()
        {
            InitializeComponent();
         
        }
        /// <summary>
        /// 常数管理类－取常数列表


        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }



            return base.OnRetrieve(base.beginTime, base.endTime, cmbQuality.Tag.ToString(), base.employee.Dept.ID, cmbType.Tag.ToString());
        }


        private void ucPhaGetTotBill_Load(object sender, EventArgs e)
        {
 

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "all";
            obj.Name ="全部";

            System.Collections.ArrayList alDrugQuality = new System.Collections.ArrayList();

            alDrugQuality = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            alDrugQuality.Insert(0, obj);
            this.cmbQuality.AddItems(alDrugQuality);

            this.cmbQuality.SelectedIndex =0;
        //}
        //private void ucPhaGetTotBill_OnLoad(object sender, EventArgs e)
        //{

            //加药品类别查找 add jlj

            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "all";
            obj1.Name = "全部";

            System.Collections.ArrayList alDrugType = new System.Collections.ArrayList();

            alDrugType = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            alDrugType.Insert(0, obj1);
            this.cmbType.AddItems(alDrugType);

            this.cmbType.SelectedIndex = 0;
        }

        private void cmbQuality_SelectedIndexChanged ( object sender , EventArgs e )
        {

            this.OnRetrieve ( base.beginTime , base.endTime , cmbQuality.Tag.ToString ( ) , base.employee.Dept.ID , cmbType.Tag.ToString ( ) );
        }

        private void cmbType_SelectedIndexChanged ( object sender , EventArgs e )
        {
            this.OnRetrieve ( base.beginTime , base.endTime , cmbQuality.Tag.ToString ( ) , base.employee.Dept.ID , cmbType.Tag.ToString ( ) );
        }


   }         
}  

