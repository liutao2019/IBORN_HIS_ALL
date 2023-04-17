using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.HealthRecord.Case
{
    /// <summary>
    /// [��������: ������������վά��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007/09/13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class CaseSubarea : FS.FrameWork.Models.NeuObject
    {
        public CaseSubarea()
        {
        }

        private FS.FrameWork.Models.NeuObject subArea = new FS.FrameWork.Models.NeuObject();

        private FS.FrameWork.Models.NeuObject nurseStation = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject SubArea
        {
            get
            {
                return this.subArea;
            }
            set
            {
                this.subArea = value;
            }
        }


        /// <summary>
        /// ����վ
        /// </summary>
        public FS.FrameWork.Models.NeuObject NurseStation
        {
            get
            {
                return this.nurseStation;
            }
            set
            {
                this.nurseStation = value;
            }
        }

        /// <summary>
        /// �����¶���
        /// </summary>
        /// <returns></returns>
        public new CaseSubarea Clone()
        {
            CaseSubarea cb = base.Clone() as CaseSubarea;

            cb.subArea = this.subArea.Clone();
            cb.nurseStation = this.nurseStation.Clone();

            return cb;
        }
    }
}
