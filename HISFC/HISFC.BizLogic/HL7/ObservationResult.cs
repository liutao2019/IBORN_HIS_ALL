using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.HL7
{
    /// <summary>
    /// [��������: LIS���]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-05-09]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class ObservationResult : FS.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        private string value;
        private AbnormalFlag abnormalFlag;
        private string unit;
        private string referencesRange;
        private ObservationResultStatus observationResultStatus;
        private DateTime observationDateTime;
        private ValueType valueType;

        private string loincId;
        private string loincName;
        #endregion

        #region ����
        
        /// <summary>
        /// LOINC ID
        /// </summary>
        public string LoincId
        {
            get { return this.loincId; }
            set { this.loincId = value; }
        }

        /// <summary>
        /// LOINC ����
        /// </summary>
        public string LoincName
        {
            get { return this.loincName; }
            set { this.loincName = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public ValueType ValueType
        {
            get { return this.valueType; }
            set { this.valueType = value; }
        }
        /// <summary>
        /// ���ֵ
        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public AbnormalFlag AbnormalFlag
        {
            get { return this.abnormalFlag; }
            set { this.abnormalFlag = value; }
        }

        /// <summary>
        /// ��λ
        /// </summary>
        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        /// <summary>
        /// �ο�ֵ
        /// </summary>
        public string ReferencesRange
        {
            get { return this.referencesRange; }
            set { this.referencesRange = value; }
        }

        /// <summary>
        /// ״̬
        /// </summary>
        public ObservationResultStatus ObservationResultStatus
        {
            get { return this.observationResultStatus; }
            set { this.observationResultStatus = value; }
        }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime ObservationDateTime
        {
            get { return this.observationDateTime; }
            set { this.observationDateTime = value; }
        }
        #endregion

    }

    public enum AbnormalFlag
    {
        /// <summary>
        /// Below low normal
        /// </summary>
        L,
        /// <summary>
        /// Above high normal
        /// </summary>
        H,
        /// <summary>
        /// Below lower panic limits
        /// </summary>
        LL,
        /// <summary>
        /// Above upper panic limits
        /// </summary>
        HH,
        /// <summary>
        /// Normal (applies to non-numeric results)
        /// </summary>
        N,
        /// <summary>
        /// Abnormal (applies to non-numeric results)
        /// </summary>
        A,
        /// <summary>
        /// Very abnormal (applies to non-numeric units, analogous to panic limits for numeric units)
        /// </summary>
        AA,
        /// <summary>
        /// Significant change up
        /// </summary>
        U,
        /// <summary>
        /// Significant change down
        /// </summary>
        D,
        /// <summary>
        /// Better--use when direction not relevant
        /// </summary>
        B,
        /// <summary>
        /// Worse--use when direction not relevant
        /// </summary>
        W

    }

    public enum ObservationResultStatus
    {
        /// <summary>
        /// Record coming over is a correction and thus replaces a final result
        /// </summary>
        C,
        /// <summary>
        /// Deletes the OBX record
        /// </summary>
        D,
        /// <summary>
        /// Final results;  Can only be changed with a corrected result.
        /// </summary>
        F,
        /// <summary>
        /// Specimen in lab; results pending
        /// </summary>
        I,
        /// <summary>
        /// Not asked; used to affirmatively document that the observation identified in the OBX was not sought when the universal service ID in OBR-4 implies that it would be sought.
        /// </summary>
        N,
        /// <summary>
        /// Order detail description only (no result)
        /// </summary>
        O,
        /// <summary>
        /// Preliminary results
        /// </summary>
        P,
        /// <summary>
        /// Results entered -- not verified
        /// </summary>
        R,
        /// <summary>
        /// Partial results
        /// </summary>
        S,
        /// <summary>
        /// Results cannot be obtained for this observation
        /// </summary>
        X,
        /// <summary>
        /// Results status change to Final.  without retransmitting results already sent as ��preliminary.��  E.g., radiology changes status from preliminary to final
        /// </summary>
        U,
        /// <summary>
        /// Post original as wrong, e.g., transmitted for wrong patient
        /// </summary>
        W

    }

    public enum ValueType
    {
        /// <summary>
        /// Numeric
        /// </summary>
        NM,
        /// <summary>
        /// String Data.
        /// </summary>
        ST


    }
}
