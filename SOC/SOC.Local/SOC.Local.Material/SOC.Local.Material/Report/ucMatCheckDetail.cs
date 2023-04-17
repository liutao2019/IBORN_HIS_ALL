using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Material.Report
{
    public partial class ucMatCheckDetail : Base.ucPrivePowerReport, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucMatCheckDetail()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Material.BizLogic.Store.CheckLogic checkLogic = new FS.HISFC.BizLogic.Material.BizLogic.Store.CheckLogic();
        private FS.HISFC.Interface.Material.Print.IBillPrint checkBillPrint = null;

        protected override void OnLoad(EventArgs e)
        {
            this.DeptType = "PriveDept";
            this.PriveClassTwos = "5510|5520";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.其他;
            this.MainTitle = "盘点明细表";

            this.SQLIndexs = "SOC.Local.Material.Report.CheckDetail";

            base.OnLoad(e);
        }

        //public override int Print(object sender, object neuObject)
        //{
        //    string checkCode = this.fpSpread1_Sheet1.Cells[0, 0].Text.Trim();

        //    List<FS.HISFC.BizLogic.Material.Object.CheckDetail> alCheckDetail = checkLogic.QueryCheckDetailByCheckCode(checkCode);

        //    this.Print<FS.HISFC.BizLogic.Material.Object.CheckDetail>(alCheckDetail);
        //    return 1;
        //}

        public void Print<T>(List<T> printList)
        {
            if (this.checkBillPrint == null)
            {
                this.checkBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.Interface.Material.Print.IBillPrint)) as FS.HISFC.Interface.Material.Print.IBillPrint;
            }
            if (this.checkBillPrint != null)
            {
                List<FS.FrameWork.Models.NeuObject> objList = new List<FS.FrameWork.Models.NeuObject>();
                foreach (T obj in printList)
                {
                    objList.Add(obj as FS.FrameWork.Models.NeuObject);
                }
                
                this.checkBillPrint.SetPrintDataTotal(objList);

                int printRowCount = this.checkBillPrint.PrintRowCount;

                int totPageCount = (int)Math.Ceiling((decimal)objList.Count / (decimal)printRowCount);
                for (int i = 1; i <= totPageCount; i++)
                {
                    List<FS.FrameWork.Models.NeuObject> tmpList;
                    if (i == totPageCount)
                    {
                        tmpList = objList;
                    }
                    else
                    {
                        tmpList = objList.GetRange(0, printRowCount);
                        objList.RemoveRange(0, printRowCount);
                    }
                    this.checkBillPrint.SetPrintData(tmpList, i, totPageCount);
                    this.checkBillPrint.Print();
                }
            }
        }

        #region IInterfaceContainer 成员

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.Interface.Material.Print.IBillPrint);
                return type;
            }
        }

        #endregion
    }
}
