using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.FrameWork.Models ;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>EmployeeEducation</br>
    /// <br>[��������: ������������ʵ��]</br>
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
    public class EmployeeEducation : Neusoft.FrameWork.Models.NeuObject
    {
    
        #region �ֶ�    
        /// <summary>
        /// Ա��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee =new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��ѵ��Ŀ
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject item=new NeuObject();
        
        /// <summary>
        /// ��ѵ�ص�   
        /// </summary>
        private string place=string.Empty;
        
        /// <summary>
        /// ������
        /// </summary>
        private string manager=string.Empty ;
        
        /// <summary>
        /// ��ѵ���� 
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject level=new NeuObject();

        /// <summary>
        ///��ѵ��ʼʱ�� 
        /// </summary>
        private DateTime startTime=DateTime.Now;
        
        /// <summary>
        /// ��ѵ����ʱ�� 
        /// </summary>
        private DateTime endTime=DateTime.Now;
        
        /// <summary>
        ///ѧʱ
        /// </summary>
        private decimal period=Decimal.MinValue;
        
        /// <summary>
        /// ѧ��
        /// </summary>
        private decimal credit = Decimal.MinValue;
        
        /// <summary>
        /// ��ѵ�������
        /// </summary>
        private string evaluation=string.Empty;

        /// <summary>
        /// ��һ�β�������ʱ��
        /// </summary>
        private DateTime insertTime = DateTime.Now;

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
        /// ��ѵ��Ŀ
        /// </summary>
        public NeuObject Item
        {
            get 
            { 
                return item; 
            }
            set 
            { 
                item = value; 
            }
        }
 
        /// <summary>
        /// ��ѵ�ص�
        /// </summary>
        public string Place
        {
            get 
            { 
                return place; 
            }
            set 
            { 
                place = value; 
            }
        }
     
        /// <summary>
        /// ������
        /// </summary>
        public string  Manager
        {
            get 
            { 
                return manager; 
            }
            set 
            { 
                manager = value; 
               
            }
        }
   
        /// <summary>
        /// ��ѵ����
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject  Level
        {
            get 
            { 
                return level; 
            }
            set 
            { 
                level = value; 
            }
        }
    
        /// <summary>
        /// ��ѵ��ʼʱ��
        /// </summary>
        public DateTime StartTime
        {
            get 
            { 
                return startTime; 
            }
            set 
            { 
                startTime = value; 
            }
        }
    
        /// <summary>
        /// ��ѵ����ʱ��
        /// </summary>
        public DateTime EndTime
        {
            get 
            { 
                return endTime; 
            }
            set 
            { 
                endTime = value; 
            }
        }

        /// <summary>
        /// ѧʱ
        /// </summary>
        public decimal Period
        {
            get 
            { 
                return period; 
            }
            set 
            { 
                period = value; 
            }
        }
  
        /// <summary>
        /// ѧ��
        /// </summary>
        public decimal Credit
        {
            get 
            { 
                return credit; 
            }
            set 
            { 
                credit = value; 
            }
        }

        /// <summary>
        /// ��ѵ�������
        /// </summary>
        public string Evaluation
        {
            get 
            { 
                return evaluation; 
            }
            set 
            { 
                evaluation = value; 
            }
        }

        /// <summary>
        /// ��һ�β���ʱ��
        /// </summary>
        public DateTime InsertTime
        {
            get 
            { 
                return insertTime; 
            }
            set 
            { 
                insertTime = value; 
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
        /// <returns>��ѵ��¼ʵ��</returns>
        public new EmployeeEducation Clone()
        {
            EmployeeEducation employeeEducation = base.Clone() as EmployeeEducation;
            employeeEducation.Employee = this.Employee.Clone();
            employeeEducation.Level = this.Level.Clone();
            employeeEducation.Oper = this.Oper.Clone();
           
            return employeeEducation;
        }
        #endregion



    }
}
