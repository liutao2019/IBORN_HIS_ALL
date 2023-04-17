using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.GYZL.PubReport.Components
{
    public partial class frmPublicBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public frmPublicBill()
        {
            InitializeComponent();
        }

        #region 菜单栏
        
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();       
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("在院患者", "在院患者", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L列表, true, false, null);
            toolBarService.AddToolButton("结算患者", "结算患者", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L历史信息, true, false, null);
            toolBarService.AddToolButton("单独患者", "单独患者", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L临时号, true, false, null);
            toolBarService.AddToolButton("统计(&S)", "统计(&S)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.W完成的, true, false, null);
            toolBarService.AddToolButton("生成托收", "生成托收", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S收费项目, true, false, null);
            toolBarService.AddToolButton("修改(&M)", "修改(&M)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("删除(&D)", "删除(&D)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("打印(&P)", "打印(&P)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("全选", "全选", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            toolBarService.AddToolButton("全选已统计", "全选已统计", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            toolBarService.AddToolButton("取消全选", "取消全选", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选取消, true, false, null);
            toolBarService.AddToolButton("取消选择已统计", "取消选择已统计", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选取消, true, false, null);
            /*
            //ToolStripDropDownButton tsddb = new ToolStripDropDownButton();
            ToolStripDropDownMenu tsddm = new ToolStripDropDownMenu();
            ToolStripMenuItem tsm1 = new ToolStripMenuItem();
            tsm1.Text="全选所有";
            tsm1.Click += new System.EventHandler(this.menuItem1_Click);
            //tsddb.DropDownItems.Add(tsm1);
            tsddm.Items.Add(tsm1);
            ToolStripMenuItem tsm2 = new ToolStripMenuItem();
            tsm2.Text = "全选已经统计";
            tsm2.Click += new System.EventHandler(this.menuItem2_Click);
           // tsddb.DropDownItems.Add(tsm2);
            tsddm.Items.Add(tsm2);
            ToolStripMenuItem tsm3 = new ToolStripMenuItem();
            tsm3.Text = "取消全选所有";
            tsm3.Click += new System.EventHandler(this.menuItem3_Click);
            //tsddb.DropDownItems.Add(tsm3);
            tsddm.Items.Add(tsm3);
            ToolStripMenuItem tsm4 = new ToolStripMenuItem();
            tsm4.Text = "取消选择已经统计";
            tsm4.Click += new System.EventHandler(this.menuItem4_Click);
           // tsddb.DropDownItems.Add(tsm4);
            tsddm.Items.Add(tsm4);                        
            //toolBarService.AddToolButton();
            */
            return this.toolBarService;           
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "在院患者":
                    this.ucPublicBill1.SetStaticPatients("0", "I", "");
                    break;
                case "结算患者":
                    this.ucPublicBill1.SetStaticPatients("0", "O", "");
                    break;
                case "单独患者":                   
                        this.ucPublicBill1.ucQueryInpatientNo1.Focus();
                        break;
                case "统计":
                        this.ucPublicBill1.SaveBill("0");
                        break;
                case "生成托收":
                        this.ucPublicBill1.MakeBill();
                        break;
                //case "选择(&C)":
                //        this.ucPublicBill1.MakeBill();
                //        break;
                case "修改":
                        this.ucPublicBill1.Modify();
                        break;
                case "删除":
                        this.ucPublicBill1.Delete();
                        break;
                case "打印":
                        this.ucPublicBill1.Print();
                        break;
                case "全选":
                        this.ucPublicBill1.SelectAll("1", true);
                        break;
                case "全选已统计":
                        this.ucPublicBill1.SelectAll("2", true);
                        break;
                case "取消全选":
                        this.ucPublicBill1.SelectAll("1", false);
                        break;
                case "取消选择已统计":
                        this.ucPublicBill1.SelectAll("2", false);
                        break; 
                default:
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }      
        #endregion




        private void frmPublicBill_Load(object sender, System.EventArgs e)
        {
            this.ucPublicBill1.Init();
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == this.tbInHos)
            {
                this.ucPublicBill1.SetStaticPatients("0", "I", "");//
            }
            if (e.Button == this.tbBal)
            {
                this.ucPublicBill1.SetStaticPatients("0", "O", "");//
            }
            if (e.Button == this.tbStatic)
            {
                this.ucPublicBill1.SaveBill("0");//
            }
            if (e.Button == this.tbExit)
            {
                //this.Close();
            }
            if (e.Button == this.tbPerson)
            {
                this.ucPublicBill1.ucQueryInpatientNo1.Focus();//
            }
            if (e.Button == this.tbMake)
            {
                this.ucPublicBill1.MakeBill();//
            }
            if (e.Button == this.tbPrint)
            {
                this.ucPublicBill1.Print();//
            }
            if (e.Button == this.tbModify)
            {
                this.ucPublicBill1.Modify();//
            }
            if (e.Button == this.tbDelete)
            {
                this.ucPublicBill1.Delete();//
            }

        }

        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            this.ucPublicBill1.SelectAll("1", true);
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            this.ucPublicBill1.SelectAll("2", true);
        }

        private void menuItem3_Click(object sender, System.EventArgs e)
        {
            this.ucPublicBill1.SelectAll("1", false);
        }

        private void menuItem4_Click(object sender, System.EventArgs e)
        {
            this.ucPublicBill1.SelectAll("2", false);
        }
    }
}
