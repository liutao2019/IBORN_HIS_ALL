using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Case
{
    /// <summary>
    /// ucCabinet<br></br>
    /// [��������: ����ʹ�ø���]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007-09-13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCaseTrack : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.HealthRecord.Case.CaseTrackManager ctManager = new FS.HISFC.BizLogic.HealthRecord.Case.CaseTrackManager();


        public ucCaseTrack()
        {
            InitializeComponent();
        }


        private void QueryCaseTrackRecord(string caseID)
        {
            if (caseID == null || caseID.Trim() == string.Empty)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����Ų���Ϊ��"));
                return ;
            }

            List<FS.HISFC.Models.HealthRecord.Case.CaseTrack> listTrack = this.ctManager.QueryTrackRecordByCaseID(caseID);

            if (listTrack == null || listTrack.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û���ҵ�����ʹ�ü�¼"));
                return ;
            }

            foreach (FS.HISFC.Models.HealthRecord.Case.CaseTrack track in listTrack)
            {
                this.FillTrackRecord(track);
            }


        }

        private void FillTrackRecord(FS.HISFC.Models.HealthRecord.Case.CaseTrack track)
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
            int rowIndex = this.neuSpread1_Sheet1.Rows.Count;
       

            this.neuSpread1_Sheet1.SetText(rowIndex, 0, track.ID);
            this.neuSpread1_Sheet1.SetText(rowIndex, 1, track.PatientCase.ID);
            this.neuSpread1_Sheet1.SetText(rowIndex, 2, track.UseCaseEnv.ID);
            this.neuSpread1_Sheet1.SetText(rowIndex, 3, track.UseCaseEnv.Name);
            this.neuSpread1_Sheet1.SetText(rowIndex, 4, track.UseCaseEnv.Dept.ID);
            this.neuSpread1_Sheet1.SetText(rowIndex, 5, track.UseCaseEnv.Dept.Name);
            this.neuSpread1_Sheet1.SetText(rowIndex, 6, track.UseCaseEnv.OperTime.ToString());
            this.neuSpread1_Sheet1.SetText(rowIndex, 7, track.User01);
            this.neuSpread1_Sheet1.SetText(rowIndex, 8, track.User02);

            this.neuSpread1_Sheet1.Rows[rowIndex].Tag = track;
        }

        private void Setfp()
        {
            this.neuSpread1_Sheet1.Columns[0].Visible = false;
            this.neuSpread1_Sheet1.Columns[2].Visible = false;
            this.neuSpread1_Sheet1.Columns[4].Visible = false;
            this.neuSpread1_Sheet1.Columns[7].Visible = false;
        }

        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Clear();
            this.QueryCaseTrackRecord(this.tbCardID.Text.Trim());
        }

        private void ucCaseTrack_Load(object sender, EventArgs e)
        {
            this.Setfp();
        }
    }
}