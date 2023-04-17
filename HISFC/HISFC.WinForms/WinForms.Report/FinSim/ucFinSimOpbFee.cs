using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.FinSim
{
    public partial class ucFinSimOpbFee : Common.ucQueryBaseForDataWindow
    {
        public ucFinSimOpbFee()
        {
            InitializeComponent();
        }
        private string metCode = string.Empty;
        private string metName = string.Empty;
        protected override void OnLoad()
        {
            this.Init();

            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList consList = manager.GetConstantList("PACTUNIT");
            foreach (FS.HISFC.Models.Base.Const con in consList)
            {
                metComboBox1.Items.Add(con);
            }
            if (metComboBox1.Items.Count >= 0)
            {

                metComboBox1.SelectedIndex = 0;
                metCode = ((FS.HISFC.Models.Base.Const)metComboBox1.Items[0]).ID;
                metName = ((FS.HISFC.Models.Base.Const)metComboBox1.Items[0]).Name;

            }

            this.InitCombox();

            base.OnLoad();
        }

        /// <summary>
        /// 用于判断报表中的市保类型（城镇职工和城镇居民）
        /// </summary>
        private void InitCombox()
        {
            ArrayList al = new ArrayList();

            ///加关于是否包含中途结算数据的判断
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "城镇职工";
            obj.Name = "城镇职工";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "城镇居民";
            obj.Name = "城镇居民";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "农民工人员";
            obj.Name = "农民工人员";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "所有患者";
            obj.Name = "所有患者";
            al.Add(obj);

            this.neuComboBox1.AddItems(al);

        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            string idsim = this.neuComboBox1.Tag.ToString();

            dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, metCode, idsim);
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, metCode, idsim);
        }

        private void metComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metComboBox1.SelectedIndex >= 0)
            {
                metCode = ((FS.HISFC.Models.Base.Const)metComboBox1.Items[this.metComboBox1.SelectedIndex]).ID;
                metName = ((FS.HISFC.Models.Base.Const)metComboBox1.Items[this.metComboBox1.SelectedIndex]).Name;
            }
        }

        //这个是下拉列表选择改变事件(判断报表中的市保类型(城镇职工和城镇居民))
        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnRetrieve();
        }

    }
}
