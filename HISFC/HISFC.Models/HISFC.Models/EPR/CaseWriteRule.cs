using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.EPR
{
    /// <summary>
    /// OnlineController <br></br>
    /// [��������: ������д�淶ʵ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-10]<br></br>
    /// <�޸ļ�¼
    /// 
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>   
    [Serializable]
    public class CaseWriteRule:FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// ��д�������
        /// </summary>
        protected string ruleCode;
        /// <summary>
        /// ��д��������
        /// </summary>
        private string ruleName;
        /// <summary>
        /// ��������
        /// </summary>
        protected string deptName;
        /// <summary>
        /// ���ұ���
        /// </summary>
        protected string deptCode;
        /// <summary>
        /// ���
        /// </summary>
        protected string descript;
        /// <summary>
        /// ����
        /// </summary>
        protected string sort;
        /// <summary>
        /// ��ע
        /// </summary>
        protected string memo;
        /// <summary>
        /// ���ӵ�ַ
        /// </summary>
        protected string ruleLink;
        /// <summary>
        /// ��������
        /// </summary>
        protected string parentCode;
        /// <summary>
        /// �淶����
        /// </summary>
        protected string data;

        /// <summary>
        /// ��д�������
        /// </summary>
        public string RuleCode
        {
            get
            {
                return this.ruleCode;
            }
            set
            {
                this.ruleCode = value;
            }
        }
        /// <summary>
        /// ��д��������
        /// </summary>
        public string RuleName
        {
            get 
            {
                return this.ruleName; 
            }
            set 
            { 
                this.ruleName = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.deptName;
            }
            set 
            {
                this.deptName = value;
            }
        }
        /// <summary>
        /// ���ұ���
        /// </summary>
        public string DeptCode
        {
            get
            {
                return this.deptCode;
            }
            set
            {
                this.deptCode = value;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string Descript
        {
            get
            {
                return this.descript;
            }
            set
            {
                this.descript = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Sort
        {
            get
            {
                return this.sort;
            }
            set
            {
                this.sort = value;
            }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo
        {
            get
            {
                return this.memo;
            }
            set
            {
                this.memo = value;
            }
        }
        /// <summary>
        /// ���ӵ�ַ
        /// </summary>
        public string RuleLink
        {
            get
            {
                return this.ruleLink;
            }
            set
            {
                this.ruleLink = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string ParentCode
        {
            get
            {
                return this.parentCode;
            }
            set
            {
                this.parentCode = value;
            }
        }
        /// <summary>
        /// �淶����
        /// </summary>
        public string RuleData
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

    }
}
