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
    /// [��������: �������ţ������Ŵ�ӡ��]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-05]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucArrangePrint : UserControl, FS.HISFC.BizProcess.Interface.Operation.IArrangePrint
    {
        public ucArrangePrint()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;

        }


#region �ֶ�

        ArrayList allAppliction = new ArrayList();
        ArrayList alRooms = new ArrayList();
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType arrangeType = FS.HISFC.BizProcess.Interface.Operation.EnumArrangeType.Anaesthesia;
        int maxRowCount = 30;
#endregion

#region ����

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
                this.neuLabel2.Text = string.Concat("", value.ToString("yyyy-MM-dd"));
            }
        }


    
#endregion

        #region IReportPrinter ��Ա

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Export()
        {
            return 0;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            
            pageSize = pageSizeMgr.GetPageSize("");

            if (pageSize == null)
            {
                pageSize = new FS.HISFC.Models.Base.PageSize("A3", 1169, 1654);
            }
            print.SetPageSize(pageSize);
            print.IsLandScape = true;
            return this.print.PrintPreview(0, 0, this);
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <returns></returns>
        public int PrintPreview()
        {
            alRooms = Environment.TableManager.GetRoomsByDept(Environment.OperatorDeptID);
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
                this.nlbPageNO.Text = "ҳ�룺��" + (i + 1) + "ҳ/��" + maxPage + "ҳ"; 
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
            this.neuSpread1_Sheet1.Rows[i].Height = 30;
            this.neuSpread1_Sheet1.Rows[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpread1_Sheet1.Rows[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #region ���ݸ�ֵ
            //������
            if (appliction.RoomID != null && appliction.RoomID != "")
            {

                FS.FrameWork.Models.NeuObject obj = this.GetRoom(appliction.RoomID);
                this.neuSpread1_Sheet1.Cells[i, 0].Text = obj.Name;
            }
            //����̨��
            this.neuSpread1_Sheet1.Cells[i, 1].Text = appliction.OpsTable.Name;
            //����
            this.neuSpread1_Sheet1.Cells[i, 2].Text = appliction.PatientInfo.PVisit.PatientLocation.Dept.Name;
            //����
            ////{2D90BEDC-96B2-4a4f-8197-21DF10F5EE17}
            this.neuSpread1_Sheet1.Cells[i, 3].Text = appliction.PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? appliction.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : appliction.PatientInfo.PVisit.PatientLocation.Bed.ID;
            //����
            this.neuSpread1_Sheet1.Cells[i, 4].Text = appliction.PatientInfo.Name;
            //סԺ��
            this.neuSpread1_Sheet1.Cells[i, 5].Text = appliction.PatientInfo.PID.PatientNO;
            //�Ա�
            this.neuSpread1_Sheet1.Cells[i, 6].Text = appliction.PatientInfo.Sex.Name;
            //����
            this.neuSpread1_Sheet1.Cells[i, 7].Text = appliction.PatientInfo.Age;
            //��ǰ���
            if (appliction.DiagnoseAl.Count > 0)
                this.neuSpread1_Sheet1.Cells[i, 8].Text = (appliction.DiagnoseAl[0] as NeuObject).Name;
            //��������
            this.neuSpread1_Sheet1.Cells[i, 9].Text = appliction.MainOperationName;
            //����ҽ��
            this.neuSpread1_Sheet1.Cells[i, 10].Text = appliction.OperationDoctor.Name;
            //����ʽ
            if (appliction.AnesType.ID != null && appliction.AnesType.ID.Length != 0)
            {
                FS.FrameWork.Models.NeuObject obj = new NeuObject();
                int le = appliction.AnesType.ID.IndexOf("|");
                if (le > 0)
                {
                    obj = Environment.GetAnes(appliction.AnesType.ID.Substring(0, le));
                    if (obj != null)
                    {
                        appliction.AnesType.Name = obj.Name + "��";
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
                this.neuSpread1_Sheet1.Cells[i, 13].Text = appliction.AnesType.Name;
            }
            if (appliction.RoleAl != null && appliction.RoleAl.Count > 0)
            {
                foreach (FS.HISFC.Models.Operation.ArrangeRole role in appliction.RoleAl)
                //һ����
                    if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString())
                 {
                     this.neuSpread1_Sheet1.Cells[i,11].Text = role.Name;
                 }
                 //������
                 else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString())
                 {
                     this.neuSpread1_Sheet1.Cells[i,12].Text = role.Name;
                 }
                 //����ҽ��
                 else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Anaesthetist.ToString())
                 {
                     this.neuSpread1_Sheet1.Cells[i, 14].Text = role.Name;
                 }
                 //ϴ�ֻ�ʿ
                 else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.WashingHandNurse1.ToString())
                 {
                     this.neuSpread1_Sheet1.Cells[i, 15].Text = role.Name;       
                 }
                 //Ѳ�ػ�ʿ
                 else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.ItinerantNurse1.ToString())
                 {
                     this.neuSpread1_Sheet1.Cells[i, 16].Text = role.Name;
                 }
            }
            //����˵��
            this.neuSpread1_Sheet1.Cells[i, 17].Text = string.IsNullOrEmpty(appliction.ApplyNote)?" ":appliction.ApplyNote;
            if (this.neuSpread1_Sheet1.Cells[i, 17].Text.Length * 16.5 / this.neuSpread1_Sheet1.Columns[17].Width > 2)
            {
                this.neuSpread1_Sheet1.Rows[i].Height = this.neuSpread1_Sheet1.Rows[i].Height +  (float)(16 * Math.Ceiling(this.neuSpread1_Sheet1.Cells[i, 17].Text.Length * 16.5 / this.neuSpread1_Sheet1.Columns[17].Width -2));
            }
          
            #endregion
        }

        #region IArrangePrint ��Ա

        /// <summary>
        /// ��ȡ��������
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
            obj.Name = "��";
            return obj;
        }

        /// <summary>
        /// ������뵥
        /// </summary>
        /// <param name="appliction"></param>
        public void AddAppliction(FS.HISFC.Models.Operation.OperationAppllication appliction)
        {
            allAppliction.Add(appliction);
            
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Reset()
        {
            this.nlbTitle.Location = new Point((this.Width - this.nlbTitle.Width)/2,this.nlbTitle.Location.Y);
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// ��������
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

    public class CompareByRoomIdAndName : IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            if ((x is FS.HISFC.Models.Operation.OperationAppllication) && (y is FS.HISFC.Models.Operation.OperationAppllication))
            {
                return ((x as FS.HISFC.Models.Operation.OperationAppllication).RoomID + (x as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.ID).CompareTo((y as FS.HISFC.Models.Operation.OperationAppllication).RoomID + (y as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.ID);
            }
            return 1;
        }

        #endregion
    }
}
