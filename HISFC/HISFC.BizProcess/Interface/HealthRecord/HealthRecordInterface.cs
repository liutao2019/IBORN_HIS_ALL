using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.HealthRecord
{
    /// <summary>
    /// [��������: �����ӿ���]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-4-4 ]<br></br>
    /// </summary>
    public interface HealthRecordInterface : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// ����ʵ�� ,��Ϊ��ʵ��ۺ���FS.HISFC.Models.RADT.PatientInfo������סԺʵ�������ת���ɲ���ʵ��
        /// </summary>
        /// <param name="obj"></param>
        void ControlValue(FS.HISFC.Models.HealthRecord.Base obj);
        /// <summary>
        /// �������
        /// </summary>
        void Reset();
    }

    /// <summary>
    ///������ҳ�ڶ�ҳ  {DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
    /// </summary>
    public interface HealthRecordInterfaceBack : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// ����ʵ�� ,��Ϊ��ʵ��ۺ���FS.HISFC.Models.RADT.PatientInfo������סԺʵ�������ת���ɲ���ʵ��
        /// </summary>
        /// <param name="obj"></param>
        void ControlValue(FS.HISFC.Models.HealthRecord.Base obj);
        /// <summary>
        /// �������
        /// </summary>
        void Reset();
    }

    /// <summary>
    ///������ҳ��ҳ  {DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
    /// </summary>
    public interface HealthRecordInterfaceAdditional : FS.FrameWork.WinForms.Forms.IReportPrinter
    {
        /// <summary>
        /// ����ʵ�� ,��Ϊ��ʵ��ۺ���FS.HISFC.Models.RADT.PatientInfo������סԺʵ�������ת���ɲ���ʵ��
        /// </summary>
        /// <param name="obj"></param>
        void ControlValue(FS.HISFC.Models.HealthRecord.Base obj);
        /// <summary>
        /// �������
        /// </summary>
        void Reset();
    }
}
