using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.Examination
{
    /// <summary>
    /// IBaby<br></br>
    /// [��������: ��쵥λ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12-08]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class CHKCompanyGroup : Neusoft.HISFC.Object.Base.Item
    {
        #region ����
        /// <summary>
        /// ����ʵ����
        /// </summary>
        private Neusoft.HISFC.Object.Base.Department department = new Neusoft.HISFC.Object.Base.Department();
        /// <summary>
        /// �����ѯ��
        /// </summary>
        private string groupSpell = string.Empty;
        
        /// <summary>
        /// �Ա�
        /// </summary>
        private string sexCode = string.Empty;
        
        /// <summary>
        /// ����״��
        /// </summary>
        private string wedState = string.Empty;
        
        /// <summary>
        /// ��������
        /// </summary>
        private int ageTop = 0;
        
        /// <summary>
        /// ��������
        /// </summary>
        private int ageBottom = 0;
        
        /// <summary>
        /// ְ��
        /// </summary>
        private string postCode = string.Empty;
        
        /// <summary>
        /// ְ��
        /// </summary>
        private string postTitleCode = string.Empty;
       
        /// <summary>
        /// �Ƿ���Ч1��Ч0��Ч
        /// </summary>
        private string validFlag;
        #endregion

        #region ����
        ///// <summary>
        ///// ��������
        ///// </summary>
        //public string GroupName
        //{
        //    get
        //    {
        //        return groupName;
        //    }
        //    set
        //    {
        //        groupName = value;
        //    }
        //}

        /// <summary>
        /// �����ѯ��
        /// </summary>
        public string GroupSpell
        {
            get
            {
                return groupSpell;
            }
            set
            {
                groupSpell = value;
            }
        }

        /// <summary>
        /// �Ա�
        /// </summary>
        public string SexCode
        {
            get
            {
                return sexCode;
            }
            set
            {
                sexCode = value;
            }
        }

        /// <summary>
        /// ����״��
        /// </summary>
        public string WedState
        {
            get
            {
                return wedState;
            }
            set
            {
                wedState = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int AgeTop
        {
            get
            {
                return ageTop;
            }
            set
            {
                ageTop = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int AgeBottom
        {
            get
            {
                return ageBottom;
            }
            set
            {
                ageBottom = value;
            }
        }

        /// <summary>
        /// ְ��
        /// </summary>
        public string PostCode
        {
            get
            {
                return postCode;
            }
            set
            {
                postCode = value;
            }
        }

        /// <summary>
        /// ְ��
        /// </summary>
        public string PostTitleCode
        {
            get
            {
                return postTitleCode;
            }
            set
            {
                postTitleCode = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ч1��Ч0��Ч
        /// </summary>
        public string ValidFlag
        {
            get
            {
                return validFlag;
            }
            set
            {
                validFlag = value;
            }
        }

        /// <summary>
        /// ����ʵ����
        /// </summary>
        public Neusoft.HISFC.Object.Base.Department Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
            }
        }
        #endregion

        #region ��¡����
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new CHKCompanyGroup Clone ()
        {
            CHKCompanyGroup obj = base.Clone() as CHKCompanyGroup;
            obj.Department = obj.Department.Clone();
            return obj;
        }
        #endregion
    }
}
