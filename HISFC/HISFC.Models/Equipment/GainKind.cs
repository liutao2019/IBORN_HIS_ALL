using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Equipment
{
    [System.Serializable]
    public class GainKind:Spell,IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public GainKind() 
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// �ϼ�����(�����ϼ�Ϊ0)
        /// </summary>
        private string preCode;

        /// <summary>
        /// �Ƿ�ĩ��1��0��
        /// </summary>
        private string leafFlag;

        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject deptInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��֧���0֧��1����
        /// </summary>
        private string inoutFlag;

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        private string validFlag;

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private OperEnvironment operInfo = new OperEnvironment();

        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        private string remark;

        /// <summary>
        /// ������
        /// </summary>
        private string nationCode;

        /// <summary>
        /// ���Ĳ��
        /// </summary>
        private int treeLevel;

        #endregion

        #region ����
        
        /// <summary>
        /// �ϼ�����(�����ϼ�Ϊ0)
        /// </summary>
        public string PreCode
        {
            get { return preCode; }
            set { preCode = value; }
        }

        /// <summary>
        /// �Ƿ�ĩ��1��0��
        /// </summary>
        public string LeafFlag
        {
            get { return leafFlag; }
            set { leafFlag = value; }
        }

        /// <summary>
        /// �豸������Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject DeptInfo
        {
            get { return deptInfo; }
            set { deptInfo = value; }
        }

        /// <summary>
        /// ��֧���0֧��1����
        /// </summary>
        public string InoutFlag
        {
            get { return inoutFlag; }
            set { inoutFlag = value; }
        }

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public OperEnvironment OperInfo
        {
            get { return operInfo; }
            set { operInfo = value; }
        }

        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string NationCode
        {
            get { return nationCode; }
            set { nationCode = value; }
        }

        /// <summary>
        /// ���Ĳ��
        /// </summary>
        public int TreeLevel
        {
            get { return treeLevel; }
            set { treeLevel = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new GainKind Clone() 
        {
            GainKind gainKind = base.Clone() as GainKind;

            gainKind.deptInfo = this.deptInfo.Clone();
            gainKind.operInfo = this.operInfo.Clone();

            return gainKind;
        }

        #endregion
    
        #region IValid ��Ա

        public bool  IsValid
        {
	          get 
	        {
                if (this.validFlag == "1")
                    return true;
                else
                    return false;
	        }
	          set 
	        {
                if (value == true)
                    this.validFlag = "1";
                else
                    this.validFlag = "0";
	        }
        }

        #endregion
}
}
