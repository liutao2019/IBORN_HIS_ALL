using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.FrameWork.Models;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    ///<br>JobExperience</br>
    ///<br> [��������: ��������ʵ��]</br>
    ///<br> [�� �� ��: �εº�]</br>
    ///<br>[����ʱ��: 2008-07-03]</br>
    ///    <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class JobExperience : Neusoft.FrameWork.Models.NeuObject
    {
        #region �ֶ�
        /// <summary>
        /// Ա��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee employee = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��ְʱ��
        /// </summary>
        private DateTime startDate=DateTime.Now;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime endDate=DateTime.Now;

        /// <summary>
        /// ��ְ����
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject postKind=new NeuObject();

        /// <summary>
        ///����λ���� 
        /// </summary>
        private string jobPalce=string.Empty;

        /// <summary>
        ///��Ƹ�� 
        /// </summary>
        private int useTime;

        /// <summary>
        /// �Ƿ������⾭��
        /// </summary>
        private bool is_Special = false;

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
        /// ��ְ����
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
        /// ��ֹ����
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
        /// ��ְ����
        /// </summary>
        public NeuObject PostKind
        {
            get 
            { 
                return postKind; 
            }
            set 
            { 
                postKind = value; 
            }
        }

        /// <summary>
        /// ��λ����
        /// </summary>
        public string JobPalce
        {
            get 
            { 
                return jobPalce; 
            }
            set 
            { 
                jobPalce = value; 
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

        /// <summary>
        /// Ƹ��
        /// </summary>
        public int UseTime
        {
            get 
            { 
                return useTime; 
            }
            set
            { 
                useTime = value; 
            }
        }

        /// <summary>
        /// �Ƿ������⾭��
        /// </summary>
        public bool Is_Special
        {
            get 
            { 
                return is_Special; 
            }
            set 
            { 
                is_Special = value;
            }
        }

        #endregion

        #region ��¡����

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new JobExperience Clone()
        {
            JobExperience jobExperience = base.Clone() as JobExperience;
            jobExperience.PostKind = this.PostKind.Clone();
            jobExperience.Employee = this.Employee.Clone();
            jobExperience.Oper = this.Oper.Clone();

            return jobExperience;

        }
        #endregion

    }
}
