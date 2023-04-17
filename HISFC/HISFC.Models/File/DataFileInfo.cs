using System;

namespace FS.HISFC.Models.File
{
    /// <summary>
    /// DataFileInfo ��ժҪ˵����
    /// ID �ļ����
    /// Name ˵������
    /// </summary>
    [System.Serializable]
    public class DataFileInfo : FS.FrameWork.Models.NeuObject
    {

        public DataFileInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        public new string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
                this.Param.FileName = value + ".xml";
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public FS.HISFC.Models.File.DataFileParam Param = new DataFileParam();
        /// <summary>
        /// �ļ��ṹ-סԺ����,���̼�¼��
        /// </summary>
        public string DataType;
        /// <summary>
        /// �ļ�����
        /// </summary>
        public string Type;
        /// <summary>
        /// �����ļ���
        /// </summary>
        public string FullFileName
        {
            get
            {
                string s;
                s = this.Param.Http + this.Param.Folders + "/" + this.Param.FileName;
                return s;
            }
        }
        /// <summary>
        /// ����1
        /// </summary>
        public string Index1;
        /// <summary>
        /// ����2
        /// </summary>
        public string Index2;
        /// <summary>
        /// ��ʶ
        /// �Ƿ���� 0 ���� 1 ������
        /// </summary>
        public int valid = 0;
        /// <summary>
        /// ����
        /// </summary>
        public string Data;

        private int useType = 0;

        /// <summary>
        /// ʹ����Ա 0 ҽ��ʹ�� 1 ��ʿʹ��
        /// </summary>
        public int UseType
        {
            get { return useType; }
            set { useType = value; }
        }

        private int count = 0;

        /// <summary>
        /// ʹ�ü���
        /// </summary>
        public int Count
        {
            get { return count; }
            set { count = value; }
        }


        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new DataFileInfo Clone()
        {
            DataFileInfo obj = new DataFileInfo();
            obj = base.Clone() as DataFileInfo;
            obj.Param = this.Param.Clone();
            return obj;
        }
    }
}
