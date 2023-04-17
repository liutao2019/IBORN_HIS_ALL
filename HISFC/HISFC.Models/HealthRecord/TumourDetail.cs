using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// CTumourDetail ��ժҪ˵����
    /// </summary>
    [Serializable]
    public class TumourDetail : FS.FrameWork.Models.NeuObject
    {
        public TumourDetail()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ˽�б���
        /// <summary>
        /// --סԺ��ˮ��
        /// </summary>
        private string inpatientNO;  // 
        /// <summary>
        /// �������
        /// </summary>
        private int happen_no;  // --
        /// <summary>
        /// ��������
        /// </summary>
        private System.DateTime cure_date;  // --
        /// <summary>
        /// ҩ����Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject drugInfo = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// ����
        /// </summary>
        private decimal qty;
        /// <summary>
        /// ��λ
        /// </summary>
        private string unit;//   --
        /// <summary>
        /// �Ƴ�
        /// </summary>
        private string period;//   --
        /// <summary>
        /// ��Ч
        /// </summary>
        private string result; //  --
        /// <summary>
        /// ����Ա����	
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion

        #region  ����
        /// <summary>
        /// סԺ��ˮ��
        /// </summary>
        public string InpatientNO
        {
            get
            {
                return inpatientNO;
            }
            set
            {
                inpatientNO = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public int HappenNO
        {
            get
            {
                return happen_no;
            }
            set
            {
                happen_no = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public System.DateTime CureDate
        {
            get
            {
                return cure_date;
            }
            set
            {
                cure_date = value;
            }
        }
        /// <summary>
        /// ҩ����Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject DrugInfo
        {
            get
            {
                return drugInfo;
            }
            set
            {
                drugInfo = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
            }
        }
        /// <summary>
        /// ��λ
        /// </summary>
        public string Unit
        {
            get
            {
                if (unit == null)
                {
                    unit = "";
                }
                return unit;
            }
            set
            {
                unit = value;
            }
        }
        /// <summary>
        /// �Ƴ�
        /// </summary>
        public string Period
        {
            get
            {
                if (period == null)
                {
                    period = "";
                }
                return period;
            }
            set
            {
                period = value;
            }
        }
        /// <summary>
        /// ��Ч
        /// </summary>
        public string Result
        {
            get
            {
                if (result == null)
                {
                    result = "";
                }
                return result;
            }
            set
            {
                result = value;
            }

        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperInfo
        {
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }

        }
        #endregion


        public new TumourDetail Clone()
        {
            TumourDetail ctd = (TumourDetail)base.Clone();
            ctd.drugInfo = this.drugInfo.Clone();
            ctd.operInfo = this.operInfo.Clone();
            return ctd;
        }
    }
}
