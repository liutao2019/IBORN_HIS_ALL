using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Report
{
    public partial class ucDayReportInpatient : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayReportInpatient()
        {
            InitializeComponent();
        }

        #region ����

        private DataTable dtDayReport;
        private DataView dvDayReport;
        //private string SettingFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\InpatientDayReport.xml";
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private DateTime dtDay;
        #endregion

        #region ����


        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private void InitFrp(DateTime date)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ��������Ժ�.....");
                GetData(date);
            }

            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        private void InitDataSet()
        {
            dtDayReport = new DataTable("DayReport");

            dtDayReport.Columns.AddRange(new DataColumn[]
			{
                new DataColumn("DateStat",typeof(DateTime)),
                new DataColumn("DeptID",typeof(string)),
                new DataColumn("DeptName",typeof(string)),
                new DataColumn("BedStandNum",typeof(int)),
                new DataColumn("RemainYesterdayNum",typeof(int)),
                new DataColumn("InNormalNum",typeof(int)),
                new DataColumn("InChangeNum",typeof(int)),
                new DataColumn("OutNormalNum",typeof(int)),
                new DataColumn("OutChangeNum",typeof(int)),
                new DataColumn("AccNum",typeof(int)),
                new DataColumn("BanpNum",typeof(int)),
                new DataColumn("HasRecord",typeof(int))
			});
        }
        /// <summary>
        /// ����Farpoint��ʾ��ʽ
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.DecimalPlaces = 0;
            numbCellType.MaximumValue = 9999;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.DateTimeCellType dtCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            dtCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dtCellType.UserDefinedFormat = "yyyy-MM-dd";
            dtCellType.ReadOnly = true;

            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtCellType.ReadOnly = true;

            #region "����ÿ�е���ɫ"
            this.neuSpread1_Sheet1.Columns[0].CellType = dtCellType;
            this.neuSpread1_Sheet1.Columns[0].BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[0].Label = "����";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[0].Width = 90f;
            this.neuSpread1_Sheet1.Columns[1].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[1].Visible = false;
            this.neuSpread1_Sheet1.Columns[2].CellType = txtCellType;
            this.neuSpread1_Sheet1.Columns[2].BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[2].Label = "��������";
            this.neuSpread1_Sheet1.Columns[3].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[3].Label = "�����ڲ�����";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[3].Width = 90f;
            this.neuSpread1_Sheet1.Columns[3].BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread1_Sheet1.Columns[4].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[4].Label = "ԭ�в�����";
            this.neuSpread1_Sheet1.Columns[4].BackColor = System.Drawing.SystemColors.Control;
            this.neuSpread1_Sheet1.Columns[5].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[5].Label = "������Ժ��";
            this.neuSpread1_Sheet1.Columns[6].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[6].Label = "ת����";
            this.neuSpread1_Sheet1.Columns[7].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[7].Label = "�����Ժ��";
            this.neuSpread1_Sheet1.Columns[8].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[8].Label = "ת����";
            this.neuSpread1_Sheet1.Columns[9].CellType = numbCellType;
            this.neuSpread1_Sheet1.ColumnHeader.Columns[9].Label = "�㻤����";
            this.neuSpread1_Sheet1.Columns[10].CellType = numbCellType;
            this.neuSpread1_Sheet1.Columns[10].ForeColor = Color.Red;
            this.neuSpread1_Sheet1.Columns[10].Font = new Font("Arial", 9, FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.Columns[10].Label = "��ĩʵ������";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[10].Width = 100f;
            this.neuSpread1_Sheet1.Columns[11].CellType = numbCellType;
            this.neuSpread1_Sheet1.Columns[11].Visible = false;
            #endregion
            
        }
        #endregion

        #region �¼�
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("�޸Ĵ�λ��", "�޸Ĵ�λ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�޸Ĵ�λ��":
                    this.EditBedNum();
                    break;                              
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            //if ( != 0)
            //{
            //    MessageBox.Show("����ʧ��");
            //}
            //else
            //{
            return 0;
            //}
        }
        private int EditBedNum()
        {
            this.neuSpread1.StopCellEditing();

            if (neuSpread1_Sheet1.RowCount > 0)
            {
                dtDayReport.Rows[neuSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }
            //����
            System.Collections.ArrayList al = new System.Collections.ArrayList();
            if (dtDayReport != null)
            {
                try
                {
                    foreach (DataRow dtRow in dtDayReport.Rows )
                    {
                        FS.HISFC.Models.HealthRecord.DayReport dr;
                        dr = new FS.HISFC.Models.HealthRecord.DayReport();
                        dr.DateStat = DateTime.Parse(dtRow["DateStat"].ToString());
                        dr.Dept.ID = dtRow["DeptID"].ToString();
                        dr.Dept.Name = dtRow["DeptName"].ToString();
                        dr.BedStandNum = int.Parse(dtRow["BedStandNum"].ToString());
                        dr.RemainYesterdayNum = int.Parse(dtRow["RemainYesterdayNum"].ToString());
                        dr.InNormalNum = int.Parse(dtRow["InNormalNum"].ToString());
                        dr.InChangeNum = int.Parse(dtRow["InChangeNum"].ToString());
                        dr.OutNormalNum = int.Parse(dtRow["OutNormalNum"].ToString());
                        dr.OutChangeNum = int.Parse(dtRow["OutChangeNum"].ToString());
                        dr.AccNum = int.Parse(dtRow["AccNum"].ToString());
                        dr.BanpNum = int.Parse(dtRow["BanpNum"].ToString());
                        dr.HasRecord = dtRow["HasRecord"].ToString();
                        al.Add(dr);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("�޸Ĵ�λ��������ʵ�弯��ʱ����!" + e.Message, "��ʾ");
                    return -1;
                }
            }

            try
            {
                FS.HISFC.BizLogic.HealthRecord.DayReport dayReport;
                dayReport = new FS.HISFC.BizLogic.HealthRecord.DayReport();
                if (dayReport.EditBedNum (al) == -1)
                {
                    MessageBox.Show("�޸Ĵ�λ��ʧ��!", "��ʾ");
                    return -1;
                }
            }
            catch (Exception e)
            {

                return -1;
            }

            //dtDayReport.AcceptChanges();
            //foreach (DataRow dr in dtDayReport.Rows)
            //{
            //    dr["HasRecord"] = "1";
            //}
            //dtDayReport.AcceptChanges();
            InitFrp(dtDay);
            return 0;
        }
        private int Save()
        {
            this.neuSpread1.StopCellEditing();

            if (neuSpread1_Sheet1.RowCount > 0)
            {
                dtDayReport.Rows[neuSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }
            //����
            System.Collections.ArrayList  al = new System.Collections.ArrayList ();
            if (dtDayReport != null)
            {
                try
                {
                    foreach (DataRow dtRow in dtDayReport.Rows )
                    {
                        FS.HISFC.Models.HealthRecord.DayReport dr;
                        dr = new FS.HISFC.Models.HealthRecord.DayReport();
					    dr.DateStat=DateTime.Parse(dtRow["DateStat"].ToString());
                        dr.Dept.ID = dtRow["DeptID"].ToString();
                        dr.Dept.Name = dtRow["DeptName"].ToString();
                        dr.BedStandNum = int.Parse(dtRow["BedStandNum"].ToString());
                        dr.RemainYesterdayNum = int.Parse(dtRow["RemainYesterdayNum"].ToString());
                        dr.InNormalNum = int.Parse(dtRow["InNormalNum"].ToString());
                        dr.InChangeNum = int.Parse(dtRow["InChangeNum"].ToString());
                        dr.OutNormalNum = int.Parse(dtRow["OutNormalNum"].ToString());
                        dr.OutChangeNum = int.Parse(dtRow["OutChangeNum"].ToString());
                        dr.AccNum = int.Parse(dtRow["AccNum"].ToString());
                        dr.BanpNum = int.Parse(dtRow["BanpNum"].ToString());
                        dr.HasRecord = dtRow["HasRecord"].ToString();
                        al.Add(dr);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("����סԺ�ձ���Ϣ������ʵ�弯��ʱ����!" + e.Message, "��ʾ");
                    return -1;                   
                }
            }

            try
            {
                FS.HISFC.BizLogic.HealthRecord.DayReport dayReport;
                dayReport = new FS.HISFC.BizLogic.HealthRecord.DayReport();
                if (dayReport.Save(al) == -1)
                {
                    MessageBox.Show("����סԺ�ձ���Ϣʧ��!" + dayReport.Err, "��ʾ");
                    return -1;
                }
            }
            catch (Exception e)
            {
               
                return -1;
            }

            //dtDayReport.AcceptChanges();
            //foreach (DataRow  dr in dtDayReport.Rows )
            //{
            //    dr["HasRecord"] = "1";
            //}
            //dtDayReport.AcceptChanges();
            InitFrp(dtDay);
            return 0;
        }
       
        private void ucDayReportInpatient_Load(object sender, EventArgs e)
        {
            
            //����������
            InitDataSet();
            dtDay = DateTime.Now.Date;
            this.InitFrp(dtDay);
        }
        /// <summary>
        /// �Ƿ��޸����ݣ�
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            this.neuSpread1.StopCellEditing();

            if (neuSpread1_Sheet1.RowCount > 0)
            {
                this.dtDayReport.Rows[neuSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }

            DataTable dt = dtDayReport.GetChanges();

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        #region ���ǰ����
        ///// <summary>
        ///// ���õ����¼�
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void linkLblSet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{

        //    #region �����пؼ���ʼ��
        //    Common.Controls.ucSetColumn usc;
        //    usc = new Common.Controls.ucSetColumn();
        //    usc.FilePath = this.SettingFileName;
        //    usc.SetColVisible(true, true, false, false);
        //    usc.SetDataTable(this.SettingFileName, this.neuSpread1_Sheet1);
        //    #endregion

        //    ///���ñ���
        //    FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
        //    ///�����¼�����
        //    usc.DisplayEvent += new EventHandler(usc_DisplayEvent);
        //    ///��ʾ���ô���
        //    FS.FrameWork.WinForms.Classes.Function.PopShowControl(usc);
        //    ///ɾ���¼�����
        //    usc.DisplayEvent -= new EventHandler(usc_DisplayEvent);
        //}

        ///// <summary>
        ///// ���ô���ر��¼��������
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void usc_DisplayEvent(object sender, EventArgs e)
        //{
        //    ///���¼�������
        //    InitFrp(this.txtStatDate.Value.Date);
        //}

        ///// <summary>
        ///// �п�ȱ�����¼��������
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        //{
        //    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.SettingFileName);
        //} 
        #endregion

        #endregion


        /// <summary>
        /// ���ڸı��¼��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStatDate_ValueChanged(object sender, EventArgs e)
        {
            if (IsChange())
            {
                if (MessageBox.Show("�����Ѿ��޸�,�Ƿ񱣴�䶯?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (Save() == -1)
                    {
                        this.txtStatDate.Value = dtDay;
                        return;
                    }
                }
            }
            //else
            //{
            dtDay = this.txtStatDate.Value.Date;
            InitFrp(dtDay);
            //}
        }
        public override int Exit(object sender, object neuObject)
        {
            if (IsChange())
            {
                if (MessageBox.Show("�����Ѿ��޸�,�Ƿ񱣴�䶯?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (Save() == -1)
                    {
                        return -1;
                    }
                }
            }
            return base.Exit(sender, neuObject);
        }
        private void GetData(DateTime dt)
        {
            //���ԭ������
            this.dtDayReport.Rows.Clear();
            FS.HISFC.BizLogic.HealthRecord.DayReport dayReport;
            dayReport = new FS.HISFC.BizLogic.HealthRecord.DayReport();
            System.Collections.ArrayList al;
            al = new System.Collections.ArrayList();
            al=dayReport.QueryByStatTime( this.txtStatDate.Value.Date );
            try
            {
                foreach (FS.HISFC.Models.HealthRecord.DayReport dr in al)
                {

                    this.dtDayReport.Rows.Add(new object[]
					{
					    dr.DateStat,
                        dr.Dept.ID,
                        dr.Dept.Name,
                        dr.BedStandNum ,
                        dr.RemainYesterdayNum ,
                        dr.InNormalNum ,
                        dr.InChangeNum ,
                        dr.OutNormalNum ,
                        dr.OutChangeNum ,
                        dr.AccNum ,
                        dr.BanpNum ,
                        dr.HasRecord
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ѯסԺ�ձ���Ϣ����DataSetʱ����!" + e.Message, "��ʾ");
                return;
            }
            this.dtDayReport.AcceptChanges();
            this.dvDayReport  = dtDayReport.DefaultView;
            this.dvDayReport.AllowDelete = true;
            this.dvDayReport.AllowEdit = true;
            this.dvDayReport.AllowNew = true;            
            this.neuSpread1_Sheet1.DataSource = dvDayReport;
            this.neuSpread1_Sheet1.DataMember = "DayReport";
            this.SetFpFormat();
        }

        private void neuSpread1_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            int rowIdx = 0;
            int colIdx = 0;
            rowIdx= neuSpread1_Sheet1.ActiveRowIndex;
            colIdx = neuSpread1_Sheet1.ActiveColumnIndex;
            if (colIdx != 10)
            {
                #region 
                //if (System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 4].Value) +
                //    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 5].Value) +
                //    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 6].Value) -
                //    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 7].Value) -
                //    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 8].Value) > 0)
                //{ 
                #endregion
                neuSpread1_Sheet1.Cells[rowIdx, 10].Value =
                            System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 4].Value) +
                            System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 5].Value) +
                            System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 6].Value) -
                            System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 7].Value) -
                            System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 8].Value);
                #region 
                //}
                //else
                //{
                //    MessageBox.Show("�������󽫻�ʹ��ĩʵ�в�����Ϊ����");
                //    neuSpread1.SetActiveCell(e.Row,e.Column);
                //    neuSpread1.EditMode = true;
                //} 
                #endregion
            }
            else
            {
                if (System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 10].Value) !=
                    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 4].Value) +
                    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 5].Value) +
                    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 6].Value) -
                    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 7].Value) -
                    System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 8].Value))
                {

                    #region 
                    //if (MessageBox.Show("��ĩʵ�в����������Ƿ��޸�ԭ�в�������", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    //{ 
                    #endregion
                    ///Ϊ��֤��ʽ�ĳ�����Ϊԭ�������ǲ�ȷ���ģ����ִ�������޸�ԭ������
                    neuSpread1_Sheet1.Cells[rowIdx, 4].Value =
                                System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 10].Value) -
                                System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 5].Value) -
                                System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 6].Value) +
                                System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 7].Value) +
                                System.Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIdx, 8].Value);
                    #region 
                    //}
                    //else
                    //{
                    //    neuSpread1.SetActiveCell(rowIdx, 10);
                    //    neuSpread1.EditMode = true;
                    //} 
                    #endregion
                }
            }
        }

       
    }
}

