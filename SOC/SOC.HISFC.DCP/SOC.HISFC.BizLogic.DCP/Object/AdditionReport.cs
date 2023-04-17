using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.DCP.Object
{
    /// <summary>
    /// AdditionReport<br></br>
    /// [��������: ������Ϣʵ��]<br></br>
    /// [�� �� ��: zj]<br></br>
    /// [����ʱ��: 2008-8-25]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />    /// </summary>
    [System.Serializable]
    public class AdditionReport: FS.FrameWork.Models.NeuObject
    {
        #region �����

        /// <summary>
        /// ����ʵ��
        /// </summary>
        private FS.HISFC.DCP.Object.CommonReport report;

        /// <summary>
        /// ���߱��
        /// </summary>
        private string patientNO;

        /// <summary>
        /// ��������
        /// </summary>
        private string patientName;

        /// <summary>
        /// ����XML
        /// </summary>
        private string reportXML;

        #endregion

        #region ����

        /// <summary>
        /// ����ʵ��
        /// </summary>
        public FS.HISFC.DCP.Object.CommonReport Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
            }
        }

        /// <summary>
        /// ���߱��
        /// </summary>
        public string PatientNO
        {
            get
            {
                return patientNO;
            }
            set
            {
                patientNO = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string PatientName
        {
            get
            {
                return patientName;
            }
            set
            {
                patientName = value;
            }
        }

        /// <summary>
        /// ����XML
        /// </summary>
        public string ReportXML
        {
            get
            {
                return reportXML;
            }
            set
            {
                reportXML = value;
            }
        }

        #endregion

        #region ����

        public AdditionReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
        }

        public void Clone()
        {
            AdditionReport additionReport = base.Clone() as AdditionReport;

            additionReport.Report = this.Report.Clone();
        }

        #endregion
    }
}
