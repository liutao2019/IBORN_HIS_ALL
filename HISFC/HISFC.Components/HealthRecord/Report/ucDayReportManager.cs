using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.Report
{
    public partial class ucDayReportManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayReportManager()
        {
            InitializeComponent();
        }

        //private FS.HISFC.BizProcess.RADT.InpatientDayReport dayReportManager = new FS.HISFC.BizProcess.RADT.InpatientDayReport();
        private FS.HISFC.BizProcess.Integrate.RADT integrateManager = new FS.HISFC.BizProcess.Integrate.RADT();

        protected DateTime StatDate
        {
            get
            {
                return NConvert.ToDateTime(this.neuDateTimePicker1.Text);
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryDayReport();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveDayReport();

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��ȡ��λ�ձ�
        /// </summary>
        protected void QueryDayReport()
        {
            ArrayList alStatList = integrateManager.GetInpatientDayReportList(this.StatDate);
            if (alStatList == null)
            {
                MessageBox.Show("��ȡ��λ�ձ�ͳ�ƻ�����Ϣ��������");
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;
            Hashtable hsDayReportDept = new Hashtable();

            foreach (FS.HISFC.Models.HealthRecord.InpatientDayReport info in alStatList)
            {
                hsDayReportDept.Add(info.ID, null);

                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.AddDataToFp(0, info);
            }

            ArrayList al = this.QueryDeptStat();
            if (al != null)
            {
                foreach (FS.FrameWork.Models.NeuObject tempStat in al)
                {
                    if (!hsDayReportDept.ContainsKey(tempStat.ID))
                    {
                        FS.HISFC.Models.HealthRecord.InpatientDayReport temp = new FS.HISFC.Models.HealthRecord.InpatientDayReport();
                        temp.ID = tempStat.ID;
                        temp.Name = tempStat.Name;
                        temp.DateStat = this.StatDate;

                        this.neuSpread1_Sheet1.Rows.Add(0, 1);

                        this.AddDataToFp(0, temp);
                    }
                }
            }
        }

        private int AddDataToFp(int rowIndex,FS.HISFC.Models.HealthRecord.InpatientDayReport info)
        {
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColDeptCode].Text = info.ID;
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColDeptName].Text = info.Name;

            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text = info.BedStand.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColAddNum].Text = info.BedAdd.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColFreeNum].Text = info.BedFree.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColBeginNum].Text = info.BeginningNum.ToString();
            // this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text = info.InNormal.ToString(); ��־�� {C4E9B22C-A959-40e8-BCD1-0796F0EE0F4E} ��Ժ�������ڳ�����Ժ���������ż�����Ժ����
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text = (info.InNormal+info.InEmergency).ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferIn].Text = info.InTransfer.ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferOut].Text = info.OutTransfer.ToString();
            // this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text = info.OutNormal.ToString(); ��־��  {C4E9B22C-A959-40e8-BCD1-0796F0EE0F4E} ��Ժ�������ڳ����Ժ������ȥ�ٻ�����������Ժ����
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text = (info.OutNormal-info.InReturn+info.OutWithdrawal).ToString();
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColEndNum].Text = info.EndNum.ToString();
            
            //if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text == "0")
            //    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text == "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColAddNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColAddNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColFreeNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColFreeNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColBeginNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColBeginNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferIn].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferIn].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferOut].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferOut].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text = "";
            if (this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColEndNum].Text == "0")
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColEndNum].Text = "";
          
            this.neuSpread1_Sheet1.Rows[rowIndex].Tag = info;

            return 1;
        }

        /// <summary>
        /// ��ȡ�������ҽṹ
        /// </summary>
        protected ArrayList QueryDeptStat()
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager myDeptManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            //ArrayList al = myDeptManager.LoadDepartmentStat("72");
            ArrayList al = myDeptManager.LoadDepartmentStatAndByNodeKind("72","1");
            //FS.HISFC.BizLogic.Manager.DepartmentStatManager myDepartmentStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            //ArrayList al = myDepartmentStatManager.LoadDepartmentStatAndByNodeKind("72", "0");

            if (al == null)
            {
                MessageBox.Show("��ȡ�������ҽṹʧ��");
                return null;
            }

            return al;
        }

        /// <summary>
        /// �ձ���ϸ��ѯ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int QueryDayReportDetail(DateTime dtDate,string deptCode,string nurseCell)
        {
            ArrayList alDetail = this.integrateManager.GetDayReportDetailList(this.StatDate, this.StatDate.AddDays(1), deptCode, nurseCell);
            if (alDetail != null)
            {
 
            }

            return 1;
        }

        /// <summary>
        /// ��Fp�ڻ�ȡ�ձ�����
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        protected FS.HISFC.Models.HealthRecord.InpatientDayReport GetDayReport(int rowIndex)
        {
            FS.HISFC.Models.HealthRecord.InpatientDayReport info = this.neuSpread1_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.HealthRecord.InpatientDayReport;
            if (info == null)
            {
                return null;
            }

            info.ID = this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColDeptCode].Text;
            info.Name = this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColDeptName].Text;
            info.BedStand = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColStarandNum].Text);
            info.BedAdd = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColAddNum].Text);
            info.BedFree = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColFreeNum].Text);
            info.BeginningNum = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColBeginNum].Text);
            info.InNormal = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColInNum].Text);
            //{C4E9B22C-A959-40e8-BCD1-0796F0EE0F4E} ��־��  ���ó�����Ժ����
            info.InNormal = info.InNormal - info.InEmergency;
            info.InTransfer = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferIn].Text);
            info.OutTransfer = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColTransferOut].Text);
            info.OutNormal = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColOutNu].Text);
            //{C4E9B22C-A959-40e8-BCD1-0796F0EE0F4E} ��־��  ���ó����Ժ����
            info.OutNormal = info.OutNormal + info.InReturn - info.OutWithdrawal;
            info.EndNum = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnSet.ColEndNum].Text);

            return info;
        }

        /// <summary>
        /// �ձ�����
        /// </summary>
        /// <returns></returns>
        protected int SaveDayReport()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.integrateManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.InpatientDayReport info = this.GetDayReport(i);

                int param = this.integrateManager.UpdateInpatientDayReport(info);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("��λ�ձ�����ʧ��"));
                    return -1;
                }
                else if (param == 0)
                {
                    if (this.integrateManager.InsertInpatientDayReport(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("��λ�ձ�����ʧ��"));
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ�");

            return 1;
        }

        private enum ColumnSet
        {
            ColDeptName,
            ColStarandNum,
            ColAddNum,
            ColFreeNum,
            ColBeginNum,
            ColInNum,
            ColTransferIn,
            ColTransferOut,
            ColOutNu,
            ColEndNum,
            ColDeptCode
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(30, 10, this);

        }
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }
        /// <summary>
        /// ����
        /// </summary>
        private void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
        }
        public override int Export(object sender, object neuObject)
        {
            this.Export();

            return base.Export(sender, neuObject);
        }
    }
}
