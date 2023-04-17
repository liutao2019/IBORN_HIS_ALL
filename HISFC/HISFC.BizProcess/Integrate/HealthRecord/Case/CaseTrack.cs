using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.HealthRecord.Case
{
    /// <summary>
    /// [��������: ��������]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007-9-13]<br></br>
    /// </summary>
    public class CaseTrack : IntegrateBase
    {
        private FS.HISFC.BizLogic.HealthRecord.Case.CaseTrackManager trackManager = new FS.HISFC.BizLogic.HealthRecord.Case.CaseTrackManager();

        public CaseTrack()
        {
        }

        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;

            trackManager.SetTrans(trans);
        }

        /// <summary>
        /// ���벡�����ټ�¼
        /// </summary>
        /// <param name="track">��������ʵ��</param>
        /// <returns>-1ʧ�ܣ�1�ɹ�</returns>
        public int InsertTrackRecord(FS.HISFC.Models.HealthRecord.Case.CaseTrack track, FS.HISFC.BizLogic.HealthRecord.Case.EnumTrackType type)
        {
            this.SetDB(trackManager);

            return trackManager.InsertTrackRecord(track,type);
        }

    }
}
