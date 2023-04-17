using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Nurse
{
    /// <summary>
    /// [��������: ���Ӳ������µ�����]<br></br>
    /// 
    /// </summary>
    /// 
    [System.Serializable]
    public class DataStoreVitalSign: FS.FrameWork.Models.NeuObject
    {

        public DataStoreVitalSign()
        {
        }
        #region

        private string id;//������
        private string sysType;//ϵͳ���,0 ���Ӳ�����1 �������뵥
        private string dataType;//��������
        private string index1;//����1������ID
        private string index2;//����2����������
        private string parentNodeName;//�����ڵ�����
        private string nodeName;//�ڵ�����
        private string nodeValue;//�ڵ�ֵ
        private string operatorCode;//������
        private DateTime operatorTime;//����ʱ��
        private string name;//�ļ���
        private DateTime recordDate;//��¼����
        private string recordIndex;//��¼ʱ��
        private string unit;//��¼��λ

        #endregion
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string SysType
        {
            get
            {
                return this.sysType;
            }
            set
            {
                this.sysType = value;
            }
        }
        public string DataType
        {
            get
            {
                return this.dataType;
            }
            set
            {
                this.dataType = value;
            }
        }
        public string Index1
        {
            get
            {
                return this.index1;
            }
            set
            {
                this.index1 = value;
            }
        }
        public string Index2
        {
            get
            {
                return this.index2;
            }
            set
            {
                this.index2 = value;
            }
        }
        public string ParentNodeName
        {
            get
            {
                return this.parentNodeName;
            }
            set
            {
                this.parentNodeName = value;
            }
        }
        public string NodeName
        {
            get
            {
                return this.nodeName;
            }
            set
            {
                this.nodeName = value;
            }
        }
        public string NodeValue
        {
            get
            {
                return this.nodeValue;
            }
            set
            {
                this.nodeValue = value;
            }
        }
        public string OperatorCode
        {
            get
            {
                return this.operatorCode;
            }
            set
            {
                this.operatorCode = value;
            }
        }
        public DateTime OperatorTime
        {
            get
            {
                return this.operatorTime;
            }
            set
            {
                this.operatorTime = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public DateTime RecordDate
        {
            get
            {
                return this.recordDate;
            }
            set
            {
                this.recordDate = value;
            }
        }
        public string RecordIndex
        {
            get
            {
                return this.recordIndex;
            }
            set
            {
                this.recordIndex = value;
            }
        }
        public string Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                this.unit = value;
            }
        }
    }

  


     

       

     

       
}
