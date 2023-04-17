using System;
using System.Collections;

namespace FS.SOC.HISFC.Components.DCP.Notice
{
	/// <summary>
	/// Notice ��ժҪ˵����
	/// </summary>
	public class Notice
	{
		private string deptCode = "";
		private string employerCode = "";
		private System.Windows.Forms.Timer timer;
		private FS.FrameWork.Public.ObjectHelper employerHelper;
		private Hashtable hsReport;
		private DCP.Notice.frmNotice frmNotice;

		public Notice()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public Notice(string deptCode, string employerCode, int interval)
		{
			this.deptCode = deptCode;
			this.employerCode = employerCode;

			this.init();

			if(timer == null)
			{
				timer = new System.Windows.Forms.Timer();
			}
			if(interval < 1000)
			{
				interval = 5000;
			}

			timer.Enabled = true;
			timer.Interval = interval;
			timer.Tick += new EventHandler(timer_Tick);
		}

		private void init()
		{
			if(this.employerHelper == null)
			{
				this.employerHelper = new FS.FrameWork.Public.ObjectHelper();
				FS.HISFC.BizLogic.Manager.Person pMgr = new FS.HISFC.BizLogic.Manager.Person();
				this.employerHelper.ArrayObject = pMgr.GetEmployeeAll();
			}
		}

		public void ShowNotice(string notice)
		{
			this.ShowNotice(notice, true);
		}

		public void ShowNotice(string notice, bool controlBox)
		{
			if(frmNotice == null)
			{
				frmNotice = new frmNotice(true);
			}
			frmNotice.ControlBox = controlBox;
			frmNotice.Show(notice);
		}
		public void Hide()
		{
			if(frmNotice != null)
			{
				frmNotice.Hide();
			}
		}

        public void TerminateNotice()
        {
            if (frmNotice != null && !frmNotice.IsDisposed)
            {
                frmNotice.Close();
            }
        }

		private void timer_Tick(object sender, EventArgs e)
		{
			ArrayList al = new ArrayList();
			FS.SOC.HISFC.BizLogic.DCP.DiseaseReport reportMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();
			al = reportMgr.GetCommonReportListByStateAndDept("2",this.deptCode);
			if(al == null)
			{
				return;
			}
			if(this.hsReport == null)
			{
				this.hsReport = new Hashtable();
			}
            foreach (FS.HISFC.DCP.Object.CommonReport report in al)
			{
				string notice = "\n    ����" 
					+ this.employerHelper.GetName(report.ReportDoctor.ID)
					+ report.ReportTime.ToString() 
					+ "�������Ϊ"+report.ReportNO
					+ "�ı��濨���ϸ�"
					+ "\nԭ��"
					+ report.OperCase
					+ "\n\n    ���޸ģ�";
				if(!this.hsReport.Contains(report.ID))
				{
					frmNotice = new frmNotice(true);
					frmNotice.Show(notice);
					this.hsReport.Add(report.ID,null);
				}
			}
		}

	}
}
