using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    ///<br> NurseAgeParameters</br>
    ///<br> [��������: ���乤��ʵ��]</br>
    ///<br> [�� �� ��: �εº�]</br>
    ///<br> [����ʱ��: 2008-07-03]</br>
    ///     <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class NurseAgeParameters : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// ������������
        /// </summary>
        private decimal workYearLow = Decimal.MinValue;

        /// <summary>
        /// ������������
        /// </summary>
        private decimal workYearHigh = Decimal.MinValue;

        /// <summary>
        /// ���乤��
        /// </summary>
        private decimal nurseAgePay = Decimal.MinValue;

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new OperEnvironment();
        #endregion

        #region ����
        /// <summary>
        /// ������������
        /// </summary>
        public decimal WorkYearLow
        {
            get 
            { 
                return workYearLow; 
            }
            set 
            { 
                workYearLow = value; 
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public decimal WorkYearHigh
        {
            get 
            { 
                return workYearHigh;
            }
            set 
            { 
                workYearHigh = value; 
            }
        }

        /// <summary>
        /// ���乤��
        /// </summary>
        public decimal NurseAgePay
        {
            get 
            { 
                return nurseAgePay; 
            }
            set 
            { 
                nurseAgePay = value; 
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
        {
            get 
            { 
                return oper; 
            }
            set 
            { 
                oper = value; 
            }
        }
        #endregion

        #region ��¡����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new NurseAgeParameters Clone()
        {
            NurseAgeParameters nurseAgeParameters = base.Clone() as NurseAgeParameters;      
            nurseAgeParameters.Oper = this.Oper.Clone();

            return nurseAgeParameters;

        }
        #endregion

    }
}
