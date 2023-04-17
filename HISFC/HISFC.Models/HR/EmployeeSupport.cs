using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.FrameWork.Models;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>EmployeeSupport</br>
    /// <br>[��������: ְ��֧Ԯ�߽����������������֧Ԯ����ҽ��ʵ��]</br>
    /// <br>[�� �� ��: �εº�]</br>
    /// <br>[����ʱ��: 2008-07-03]</br>
    ///     <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class EmployeeSupport : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�

        /// <summary>
        /// Ա��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject itemName=new NeuObject();

        /// <summary>
        /// ֧�ߵص�
        /// </summary>
        private NeuObject supplyPlace=new NeuObject();

        /// <summary>
        /// ��Ŀ��Դ
        /// </summary>
        private string itemSource = string.Empty;

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime startDate=DateTime.Now;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime endDate=DateTime.Now;
   
        /// <summary>
        /// ����Ա
        /// </summary>
        Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();
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
        /// ��Ŀ����
        /// </summary>
        public NeuObject ItemName
        {
            get 
            { 
                return itemName; 
            }
            set 
            { 
                itemName = value; 
            }
        }

        /// <summary>
        /// ��Ŀ��Դ
        /// </summary>
        public string ItemSource
        {
            get 
            { 
                return itemSource; 
            }
            set 
            { 
                itemSource = value; 
            }
        }

        /// <summary>
        /// ֧�ߵص�
        /// </summary>
        public NeuObject SupplyPlace
        {
            get 
            { 
                return supplyPlace; 
            }
            set 
            { 
                supplyPlace = value; 
            }
        }

        /// <summary>
        ///����ʼʱ��
        /// </summary>
        public DateTime StartDate
        {
            get 
            { 
                return startDate; 
            }
            set 
            { 
                startDate = value; 
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        public DateTime EndDate
        {
            get 
            { 
                return endDate; 
            }
            set 
            { 
                endDate = value; 
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
        public new EmployeeSupport Clone()
        {
            EmployeeSupport employeeSupport = base.Clone() as EmployeeSupport;
            employeeSupport.Employee = this.Employee.Clone();
            employeeSupport.Oper = this.Oper.Clone();
        
            return employeeSupport;

        }
        #endregion



    }
}
