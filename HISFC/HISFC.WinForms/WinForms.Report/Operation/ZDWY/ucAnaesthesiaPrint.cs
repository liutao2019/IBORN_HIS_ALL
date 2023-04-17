using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using FS.HISFC.Models.Operation;
using System.Collections;

namespace FS.WinForms.Report.Operation.ZDWY
{
    /// <summary>
    /// [功能描述: 手术安排，麻醉安排打印单]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2007-01-05]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucAnaesthesiaPrint : UserControl, FS.HISFC.BizProcess.Interface.Operation.IArrangePrint
    {
        public ucAnaesthesiaPrint()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;

        }


        #region 字段

        ArrayList allAppliction = new ArrayList();
        ArrayList alRooms = new ArrayList();
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType arrangeType = FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType.Anaesthesia;
        FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        int maxRowCount = 28;
        #endregion

        #region 属性

        public string Title
        {
            set
            {
                this.nlbTitle.Text = value;
            }
        }
        public DateTime Date
        {
            set
            {
                this.neuLabel2.Text = string.Concat("截止", value.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }

        #endregion

        #region IReportPrinter 成员

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public int Export()
        {
            return 0;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            
            pageSize = pageSizeMgr.GetPageSize("");

            if (pageSize == null)
            {
                pageSize = new FS.HISFC.Models.Base.PageSize("A4", 850, 1150);
            }
            print.SetPageSize(pageSize);
            print.IsLandScape = true;
            return this.print.PrintPreview(15, 0, this);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        public int PrintPreview()
        {
            FS.HISFC.Models.Operation.OperationAppllication applicationInfo = null;
            if (allAppliction != null && allAppliction.Count > 0)
            {
                applicationInfo = allAppliction[0] as FS.HISFC.Models.Operation.OperationAppllication;
            }
            alRooms = Environment.TableManager.GetRoomsByDept(applicationInfo.ExeDept.ID);
            return this.AddApplicationsToFarPoint(allAppliction);
        }


        #endregion


        private int AddApplicationsToFarPoint(ArrayList allApplication)
        {
            if (allAppliction == null || allAppliction.Count == 0)
            {
                return -1;
            }
            allApplication.Sort(new CompareByRoomIdAndName());
            decimal maxPage = Math.Ceiling((decimal)(allAppliction.Count/this.maxRowCount));
            if(allApplication.Count % this.maxRowCount != 0)
            {
                maxPage++;
            }
            for(int i = 0;i < maxPage;i++)
            {
                this.Reset();
                this.nlbPageNO.Text = "页码：第" + (i + 1) + "页/共" + maxPage + "页"; 
                ArrayList allData = new ArrayList();
                if (i == maxPage -1)
                {
                    allData = allApplication.GetRange(i * this.maxRowCount, allApplication.Count - i * this.maxRowCount);
                }
                else
                {
                    allData = allApplication.GetRange(i * this.maxRowCount, this.maxRowCount);
                }
                foreach (FS.HISFC.Models.Operation.OperationAppllication application in allData)
                {
                    this.AddApplicationToFarpoint(application);
                }
                this.Print();
               
            }
            return 1;
        }

        private void AddApplicationToFarpoint(OperationAppllication appliction)
        {
            if (appliction == null)
                return;
            this.neuSpread1_Sheet1.RowCount += 1;
            int i = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[i].Height = 23;
            this.neuSpread1_Sheet1.Rows[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #region 数据赋值
            //手术间
            if (appliction.RoomID != null && appliction.RoomID != "")
            {

                FS.FrameWork.Models.NeuObject obj = this.GetRoom(appliction.RoomID);
                try
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = obj.Name.Substring(0, obj.Name.Length - 1);
                }
                catch { }
            }

            //科室
            this.neuSpread1_Sheet1.Cells[i, 1].Text = appliction.PatientInfo.PVisit.PatientLocation.Dept.Name;

            //床号
            this.neuSpread1_Sheet1.Cells[i, 2].Text = appliction.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);

            //姓名
            this.neuSpread1_Sheet1.Cells[i, 3].Text = appliction.PatientInfo.Name;

            //年龄
            this.neuSpread1_Sheet1.Cells[i, 4].Text = appliction.PatientInfo.Age;

            //性别
            this.neuSpread1_Sheet1.Cells[i, 5].Text = appliction.PatientInfo.Sex.Name;
          
            //住院号
            this.neuSpread1_Sheet1.Cells[i, 6].Text = appliction.PatientInfo.PID.PatientNO.TrimStart('0');
   
            //术前诊断
            if (appliction.DiagnoseAl.Count > 0)
                this.neuSpread1_Sheet1.Cells[i, 7].Text = (appliction.DiagnoseAl[0] as NeuObject).Name;
            //手术名称
            this.neuSpread1_Sheet1.Cells[i, 8].Text = appliction.MainOperationName;
           
            //麻醉方式
            if (appliction.AnesType.ID != null && appliction.AnesType.ID.Length != 0)
            {
                FS.FrameWork.Models.NeuObject obj = new NeuObject();
                int le = appliction.AnesType.ID.IndexOf("|");
                if (le > 0)
                {
                    obj = Environment.GetAnes(appliction.AnesType.ID.Substring(0, le));
                    if (obj != null)
                    {
                        appliction.AnesType.Name = obj.Name + "、";
                    }
                    obj = Environment.GetAnes(appliction.AnesType.ID.Substring(le + 1));
                    if (obj != null)
                    {
                        appliction.AnesType.Name += obj.Name;
                    }
                }
                else
                {
                    obj = Environment.GetAnes(appliction.AnesType.ID);
                    if (obj != null)
                    {
                        appliction.AnesType.Name = obj.Name;
                    }
                }
                this.neuSpread1_Sheet1.Cells[i, 9].Text = appliction.AnesType.Name;
            }
            if (appliction.RoleAl != null && appliction.RoleAl.Count > 0)
            {
                foreach (FS.HISFC.Models.Operation.ArrangeRole role in appliction.RoleAl)
                {

                    //麻醉医生
                    if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Anaesthetist.ToString())
                    {
                        this.neuSpread1_Sheet1.Cells[i, 10].Text = role.Name;
                    }
                    //麻醉助手
                    if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.AnaesthesiaHelper1.ToString())
                    {
                        this.neuSpread1_Sheet1.Cells[i, 11].Text = role.Name;
                    }
                }
            }

            this.neuSpread1_Sheet1.Cells[i, 12].Text = " ";
            this.neuSpread1_Sheet1.Cells[i, 13].Text = " ";
            #endregion
        }

        #region IArrangePrint 成员

        /// <summary>
        /// 获取手术房间
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        private NeuObject GetRoom(string roomID)
        {
            NeuObject obj = new NeuObject();
            foreach (OpsRoom room in alRooms)
            {
                if (roomID == room.ID)
                {
                    obj.ID = room.ID;
                    obj.Name = room.Name;
                    return obj;
                }
            }
            obj.ID = roomID;
            obj.Name = "无";
            return obj;
        }

        /// <summary>
        /// 添加申请单
        /// </summary>
        /// <param name="appliction"></param>
        public void AddAppliction(FS.HISFC.Models.Operation.OperationAppllication appliction)
        {
            allAppliction.Add(appliction);
            
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Reset()
        {
            this.nlbTitle.Location = new Point((this.Width - this.nlbTitle.Width)/2,this.nlbTitle.Location.Y);
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 安排类型
        /// </summary>
        public FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType ArrangeType
        {
            get
            {
                return this.arrangeType;
            }
            set
            {
                this.arrangeType = value;
                this.allAppliction = new ArrayList();
            }
        }

        #endregion
    }
}
