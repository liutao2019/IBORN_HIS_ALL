using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation.Report
{
    /// <summary>
    /// [��������: �����Ⱦ����ͳ��]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-16]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucReportEmergency : ucReportBase
    {
        public ucReportEmergency()
        {
            InitializeComponent();
            this.Title = "�����Ⱦ����ͳ��";
            this.fpSpread1_Sheet1.GrayAreaBackColor = Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;
            this.fpSpread1_Sheet1.Columns[-1].Width = 150;
            this.fpSpread1_Sheet1.Columns[-1].Locked = true;


        }

#region �¼�



        protected override int OnQuery()
        {
            this.fpSpread1_Sheet1.RowCount = 0;

            System.Data.DataSet ds = null;


            ds = Environment.ReportManager.GetEmergencyOperation(this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
            

            //ds = m_OpsReportManager.GetSpecalOperationReport(m_DeptID,this.BeginTime,this.EndTime);

            if (ds == null || ds.Tables.Count == 0) return -1;
            this.fpSpread1_Sheet1.DataSource = ds.Tables[0];

            //������Ҫ����ʾ�ܷ��ã�������ʾ���� modified by cuipeng 2006-08-11
            //this.fpSpread1_Sheet1.Columns[5].Visible = false;//����ʾ����

            return 0;
        }
#endregion
    }
}