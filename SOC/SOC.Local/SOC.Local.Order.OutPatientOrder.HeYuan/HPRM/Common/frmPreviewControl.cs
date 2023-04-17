using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Order.OutPatient;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common
{
    public partial class frmPreviewControl : Form
    {
        public frmPreviewControl()
        {
            InitializeComponent();
        }

        public frmPreviewControl(Dictionary<Int32, List<Control>> previewDictionary)
        {

            InitializeComponent();

            int iCount = 0;
            checkedListBox1.Items.Clear();
            foreach (Int32 keysType in previewDictionary.Keys)
            {
                ControlList.AddRange(previewDictionary[keysType]);

                for (int i = 0; i < previewDictionary[keysType].Count; i++)
                {
                    iCount++;
                    Neusoft.FrameWork.Models.NeuObject neuObject = new Neusoft.FrameWork.Models.NeuObject();
                    neuObject.ID = keysType.ToString();
                    neuObject.Name = Neusoft.FrameWork.Public.EnumHelper.Current.GetEnumName((EnumOutPatientBill)keysType) + iCount;
                    neuObject.Memo = iCount.ToString();
                    checkedListBox1.Items.Add(neuObject, true);
                }
            }
            controlDictionary = previewDictionary;
      
        }

        private Dictionary<Int32, List<Control>> controlDictionary = new Dictionary<int, List<Control>>();


        /// <summary>
        /// 当前页码
        /// </summary>
        private int currenPage;

        /// <summary>
        /// 共计页码
        /// </summary>
        private int allPage;

        /// <summary>
        /// 控件列表
        /// </summary>
        List<Control> ControlList = new List<Control>();

        /// <summary>
        /// 打印实体类
        /// </summary>
        Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();

        Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        private void frmPreviewControl_Load(object sender, EventArgs e)
        {
            this.tbAllPage.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbCurrenPage.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D打印清单);
            this.tbFrontPage.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S上一个);
            this.tbNextPage.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.X下一个);
            this.tbExit.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退出);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //打印全部
            if (e.ClickedItem == this.tbAllPage)
            {
                PrintAllPage();

            }//打印当前页
            else if (e.ClickedItem == this.tbCurrenPage)
            {

                PrintPage(ControlList[currenPage]);

            }//上一页
            else if (e.ClickedItem == this.tbFrontPage)
            {
                this.tbFrontPage.Enabled = false;

                if (currenPage > 0)
                {
                    currenPage--;
                    this.ShowPrintDialog(ControlList[currenPage]);
                }
                this.tbFrontPage.Enabled = true;
            }//下一页
            else if (e.ClickedItem == this.tbNextPage)
            {
                this.tbNextPage.Enabled = false;
                if (currenPage < allPage - 1)
                {
                    currenPage++;
                   this.ShowPrintDialog(ControlList[currenPage]);;
            
                }
                this.tbNextPage.Enabled = true;
            }//退出
            else if (e.ClickedItem == this.tbExit)
            {
                this.Close();
            }
        }

        public void ShowPreviewControlDialog() 
        {
            if (ControlList.Count>0)
            {
                currenPage = 0;
                allPage = ControlList.Count;
                ShowPrintDialog(ControlList[0]);
            }
            this.InitControl();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            ArrayList previewList = managerIntegrate.GetConstantList("OUTPATPREVIEW");
            this.clbPreview.Items.Clear();
            foreach (Neusoft.FrameWork.Models.NeuObject neuObject in previewList)
            {
                if (controlDictionary.Keys.Contains(Int32.Parse(neuObject.ID)))
                {
                    this.clbPreview.Items.Add(neuObject,true);
                }
            }
            this.clbPreview.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbPreview_ItemCheck);
        }



        /// <summary>
        /// 设置预览界面
        /// </summary>
        /// <param name="uc"></param>
        private void ShowPrintDialog(Control uc)
        {
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            this.neuLabel1.Text = "第" + (currenPage+1) + "页 " + " 共" + allPage + "页";
            Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint IOutPatientOrderPrint = uc as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            if(!object.Equals(IOutPatientOrderPrint,null))
            {
                IOutPatientOrderPrint.SetPage(this.neuLabel1.Text);
            }
            print.IsDataAutoExtend = false;
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("liuww",uc.Width, uc.Height));
            print.SetPrintDocument(0, 0, uc);
            this.printPreviewControl1.Document = print.PrintDocument;
            this.printPreviewControl1.InvalidatePreview();            
            this.printPreviewControl1.Zoom = 1.0;
            
        }

        /// <summary>
        /// 打印全部预览
        /// </summary>
        private void PrintAllPage() 
        {
            for (int i = 0; i < this.ControlList.Count;i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    PrintPage(ControlList[i]);
                }
            }
        }
        
        /// <summary>
        /// 打印控件
        /// </summary>
        /// <param name="printControl"></param>
        private void PrintPage(Control printControl) 
        {
            if(printControl.Width>printControl.Height)
            {
                print.IsLandScape = true;
                print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", printControl.Height+5, printControl.Width+5));
            }else
            {
                print.IsLandScape = false;
                print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", printControl.Width+5, printControl.Height+5));
            }
            print.IsDataAutoExtend = false;
            print.PrintPage(0, 0, printControl);
        }


        /// <summary>
        /// 控制联动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clbPreview_ItemCheck(object sender, ItemCheckEventArgs e)
        {

          string  controlType = (this.clbPreview.SelectedItem  as Neusoft.FrameWork.Models.NeuObject).ID;
          for (int i = 0; i < this.checkedListBox1.Items.Count;i++ )
          {

              if ((this.checkedListBox1.Items[i] as Neusoft.FrameWork.Models.NeuObject).ID.Equals(controlType))
              {

                  this.checkedListBox1.SetItemCheckState(i, e.NewValue);
              }
          }

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currenPage = Convert.ToInt32(((Neusoft.FrameWork.Models.NeuObject)(((System.Windows.Forms.ListBox)(sender)).SelectedItem)).Memo) - 1;
            this.ShowPrintDialog(ControlList[currenPage]);
        }
    }
}
