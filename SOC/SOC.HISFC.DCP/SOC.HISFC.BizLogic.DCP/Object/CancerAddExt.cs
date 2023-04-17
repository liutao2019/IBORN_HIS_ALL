using System;

namespace FS.HISFC.DCP.Object
{
	/// <summary>
	/// CancerAddExt ��ժҪ˵����
	/// ����������չ
	/// </summary>
    public class CancerAddExt : FS.FrameWork.Models.NeuObject
    {


        #region ˽�б���
        /// <summary>
        /// ������
        /// </summary>
        private string report_no;
        /// <summary>
        /// ��չ�������
        /// </summary>
        private string class_code;
        /// <summary>
        /// ��չ��������
        /// </summary>
        private string class_name;
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string item_code;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private string item_name;

        /// <summary>
        /// ��Ŀ��ע
        /// </summary>
        private string item_demo;

        #endregion

        #region ��������
        /// <summary>
        /// ������
        /// </summary>
        public string Report_No
        {
            set { this.report_no = value; }
            get { return this.report_no; }
        }

        /// <summary>
        /// ��չ�������
        /// </summary>
        public string Class_Code
        {
            set { this.class_code = value; }
            get { return this.class_code; }
        }
        /// <summary>
        /// ��չ��������
        /// </summary>

        public string Class_Name
        {
            set { this.class_name = value; }
            get { return this.class_name; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string Item_Code
        {
            set { this.item_code = value; }
            get { return this.item_code; }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string Item_Name
        {
            set { this.item_name = value; }
            get { return this.item_name; }
        }

        /// <summary>
        /// ��Ŀ��ע
        /// </summary>
        public string Item_Demo
        {
            set { this.item_demo = value; }
            get { return this.item_demo; }
        }

        #endregion
        public CancerAddExt()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        public new CancerAddExt Clone()
        {
            CancerAddExt cancerAddExt = base.Clone() as CancerAddExt;
            return cancerAddExt;

        }
    }
}
