using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br> PayList</br>
    /// <br>[��������: ���ʵ�ʵ��]</br>
    /// <br> [�� �� ��: �εº�]</br>
    /// <br>[����ʱ��: 2008-07-16]</br>
    ///     <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class PayList : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// Ա��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Employee();

        /// <summary>
        /// ��Ⱥ��·�
        /// </summary>
        private string yearAndMonth = string.Empty;

        /// <summary>
        /// ����ϸ�����
        /// </summary>
        private string payItemCode = string.Empty;
       
        /// <summary>
        /// ����ϸ��ֵ
        /// </summary>
        private decimal payItemValue = Decimal.MinValue;
    
        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new OperEnvironment();
       #endregion

        #region ����
        /// <summary>
        /// Ա��
        /// </summary>
        public Neusoft.HISFC.Models.Base.Employee Employee
        {
            get 
            { 
                return employee; 
            }
            set 
            { 
                employee = value; 
            }
        }

        /// <summary>
        /// ��Ⱥ��·�
        /// </summary>
        public string YearAndMonth
        {
            get 
            { 
                return yearAndMonth; 
            }
            set 
            { 
                yearAndMonth = value; 
            }
        }

        /// <summary>
        /// ����ϸ�����
        /// </summary>
        public string PayItemCode
        {
            get 
            { 
                return payItemCode; 
            }
            set 
            { 
                payItemCode = value; 
            }
        }

        /// <summary>
        /// ����ϸ��ֵ
        /// </summary>
        public decimal PayItemValue
        {
            get 
            { 
                return payItemValue; 
            }
            set 
            { 
                payItemValue = value; 
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
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new PayList Clone()
        {
            PayList payList = base.Clone() as PayList;
            payList.Oper = this.Oper.Clone();
            payList.Employee = this.Employee.Clone();

            return payList;
        }
        #endregion

    }
}
