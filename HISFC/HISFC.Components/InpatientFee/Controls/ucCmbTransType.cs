using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace FS.HISFC.Components.InpatientFee.Controls
{
    /// <summary>
    /// cmbTransType<br></br>
    /// [功能描述: 支付方式组件]<br></br>
    /// [创 建 者: 王儒超]<br></br>
    /// [创建时间: 2006-11-29]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class cmbTransType : FS.FrameWork.WinForms.Controls.NeuComboBox
    {

        //public cmbTransType(System.ComponentModel.IContainer container)
        //{
        //    //
        //    // Windows.Forms 类撰写设计器支持所必需的


        //    //
        //    container.Add(this);
        //    InitializeComponent();
        //    this.initControl();
        //    //
        //    // TODO: 在 InitializeComponent 调用后添加任何构造函数代码


        //    //
        //}

        public cmbTransType()
        {
            //
            // Windows.Forms 类撰写设计器支持所必需的

            //
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return;
            }
            this.initControl();
            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码

            //
        }


       

        #region 组件设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改






        /// 此方法的内容。






        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // cmbTransType
            // 
            this.SelectedIndexChanged += new System.EventHandler(this.txtPayKind_SelectedIndexChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTransType_KeyDown);
            this.ResumeLayout(false);

        }
        #endregion

        #region "变量"


        private IContainer components;
        /// <summary>
        /// 是否弹出
        /// </summary>
        private bool bPop = true;

        /// <summary>
        /// 工作单位
        /// </summary>
        private string workUnit = "";
        #endregion

        #region "实体变量"

        /// <summary>
        /// 银行实体
        /// </summary>
        public FS.HISFC.Models.Base.Bank bank = new FS.HISFC.Models.Base.Bank();

        #endregion

        #region"属性"
        /// <summary>
        /// 弹出属性

        /// </summary>
        public bool Pop
        {
            get
            {
                return this.bPop;
            }
            set
            {
                this.bPop = value;
            }
        }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string WorkUnit
        {
            get
            {
                return this.workUnit;
            }
            set
            {
                this.workUnit = value;
            }
        }
        #endregion 

        #region "事件"

        /// <summary>
        /// 选择变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPayKind_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                
                if (this.bPop == false) return;

                //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                //FS.HISFC.Models.Fee.EnumPayType payType = 
                //    (FS.HISFC.Models.Fee.EnumPayType)Enum.Parse(typeof(FS.HISFC.Models.Fee.EnumPayType), this.Tag.ToString());
                //switch (payType)
                //{
                //    //借记卡


                //    case FS.HISFC.Models.Fee.EnumPayType.DB:

                //        break;
                //    //支票
                //    case FS.HISFC.Models.Fee.EnumPayType.CH:
                //        this.ShowBank();
                //        break;
                //    //信用卡

                //    case FS.HISFC.Models.Fee.EnumPayType.CD:
                        
                //        break;
                //    //汇票
                //    case FS.HISFC.Models.Fee.EnumPayType.PO:
                //        this.ShowBank();
                //        break;

                //    default:
                //        break;
                //}
                FS.FrameWork.Models.NeuObject payType = this.SelectedItem as FS.FrameWork.Models.NeuObject;
                switch (payType.ID)
                {
                    //借记卡


                    case "DB":

                        break;
                    //支票
                    case "CH":
                        this.ShowBank();
                        break;
                    //信用卡

                    case "CD":

                        break;
                    //汇票
                    case "PO":
                        this.ShowBank();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        /// <summary>
        /// 控件回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTransType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //base.ComboBox_KeyDown(sender, e);


        }
        #endregion

        #region "方法"

        /// <summary>
        /// 初始化控件

        /// </summary>
        private void initControl()
        {
            this.Items.Clear();
            

            //住院显示部分支付方式。


            try
            {
                //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                //this.AddItems(FS.HISFC.Models.Fee.EnumPayTypeService.List());
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager ();
                this.AddItems(managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 显示bank控件
        /// </summary>
        private void ShowBank()
        {

            FS.FrameWork.WinForms.Forms.BaseForm f   ;
            f = new FS.FrameWork.WinForms.Forms.BaseForm();

            ucBank Bank = new ucBank();

            Bank.Dock = System.Windows.Forms.DockStyle.Fill;
            f.Controls.Add(Bank);

            Bank.Bank = this.bank;
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            f.Size = new Size(295, 240);
            //			ft.Location = this.PointToScreen(new Point(this.Width/2+this.Left ,this.Height+this.Top));
            f.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            f.BackColor = this.Parent.BackColor;
            f.Text = "选择银行";
            f.ShowDialog();
        }
        #endregion 

        
     
        

    
        
      
        
       
     


   

       
    }
}
