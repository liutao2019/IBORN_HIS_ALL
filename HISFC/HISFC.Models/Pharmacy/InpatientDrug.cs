using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Pharmacy
{
    /// <summary>
    /// [��������: סԺ��ҩ��ѯʵ����]
    /// [�� �� ��: ��ú�]
    /// [����ʱ��: 2009-7-3]
    /// </summary>
    public class InpatientDrug : Neusoft.FrameWork.Models.NeuObject
    {
        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject patient = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.Pharmacy.Item drugInfo = new Neusoft.HISFC.Models.Pharmacy.Item();

        /// <summary>
        /// ���ݺ�
        /// </summary>
        private string billNO = "";        

        /// <summary>
        /// ��������
        /// </summary>
        private decimal happenQty;

        /// <summary>
        /// ҽ���������Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment doctInfo = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Patient
        {
            get 
            { 
                return patient; 
            }
            set 
            {
                patient = value; 
            }
        }

        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.Pharmacy.Item DrugInfo
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
        /// ���ݺ�
        /// </summary>
        public string BillNO
        {
            get
            {
                return billNO;
            }
            set
            {
                billNO = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal HappenQty
        {
            get 
            { 
                return happenQty; 
            }
            set 
            {
                happenQty = value;
            }
        }

        /// <summary>
        /// ҽ���������Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment DoctInfo
        {
            get 
            {
                return doctInfo; 
            }
            set 
            { 
                doctInfo = value; 
            }
        }


        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new InpatientDrug Clone()
        {
            InpatientDrug iptDrug = new InpatientDrug();
            iptDrug.DoctInfo = this.DoctInfo.Clone();
            iptDrug.Patient = this.Patient.Clone();
            iptDrug.DrugInfo = this.DrugInfo.Clone();

            return iptDrug;
        }

        #endregion
    }
}
