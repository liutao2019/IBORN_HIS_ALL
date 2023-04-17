using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Terminal.Confirm.IBORN
{
    public partial class frmComfirm : Form
    {
        /// <summary>
        /// 当前患者信息
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.register;
            }
            set
            {
                this.register = value;
            }
        }

        /// <summary>
        /// 当前患者信息
        /// </summary>
        FS.HISFC.Models.Registration.Register register = null;


        /// <summary>
        /// 当前执行实体
        /// </summary>
        public FS.HISFC.Models.Terminal.TerminalApply Apply
        {
            get
            {
                return this.apply;
            }
            set
            {
                this.apply = value;
            }
        }

        /// <summary>
        /// 当前执行实体
        /// </summary>
        FS.HISFC.Models.Terminal.TerminalApply apply = null;



        HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        HISFC.BizProcess.Integrate.Order orderManager = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 执行确认窗口
        /// </summary>
        public frmComfirm()
        {
            InitializeComponent();
            init();
        }
        
        private void init()
        {
            
            // 执行人列表
            System.Collections.ArrayList tempList = manager.QueryEmployeeAll();//  {D95D9641-54AF-4a47-9879-942E26618258} 

            this.cmbExecOper.AddItems(tempList);
        }

        public int SetDataValue()
        {
            if (this.apply == null || this.register == null)
            {
                return -1;
            }

            this.lblItemName.Text = this.apply.Item.Item.Name + "    *" + (this.Apply.Item.Item.Qty - this.Apply.AlreadyConfirmCount).ToString() + this.Apply.Item.Item.PriceUnit;
            this.lblCardNO.Text = "卡    号：" + apply.Patient.PID.CardNO.ToString();
            this.lblName.Text = "姓    名：" + apply.Patient.Name.ToString();
            if(!string.IsNullOrEmpty(apply.Item.Order.ReciptDoctor.ID))
            {
                HISFC.Models.Base.Employee emp = this.manager.GetEmployeeInfo(apply.Item.Order.ReciptDoctor.ID);
                if (emp != null && !string.IsNullOrEmpty(emp.Name))
                {
                    this.lblDoctor.Text = "开单医生：" + emp.Name.ToString();
                }
            }

            HISFC.Models.Order.OutPatient.Order order = this.orderManager.GetOneOrder(this.apply.Patient.PID.ID, this.apply.Order.ID);

            if (order != null)
            {
                this.lblRecipeDate.Text = "申请时间：" + order.MOTime.ToString();
            }

            return 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.nudComfirmQTY.Value > (this.Apply.Item.Item.Qty - this.Apply.AlreadyConfirmCount))
            {
                MessageBox.Show("执行次数不能大于可执行次数！请确认！");
                return;
            }

            if (this.nudComfirmQTY.Value <= 0)
            {
                MessageBox.Show("执行次数请填写一个大于0的数字！");
                return;
            }

            this.apply.Item.ConfirmedQty = this.nudComfirmQTY.Value;

            if (this.cmbExecOper.SelectedItem != null)
            {
                this.apply.InsertOperEnvironment.ID = this.cmbExecOper.SelectedItem.ID;
                this.apply.InsertOperEnvironment.Name = this.cmbExecOper.SelectedItem.Name;
            }

            
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
