using System;
using System.Collections;
using System.ComponentModel;
namespace FS.HISFC.Models.EPR
{
    /// <summary>
    /// ucNurseSheetSettingColumn<br></br>
    /// [��������: �����¼������������]<br></br>
    /// [�� �� ��: ��־��]<br></br>
    /// [����ʱ��: 2007-11-05]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
	public class NurseSheetSettingColumn
	{
        //private string id;

        //[Description("�б��룬��󳤶�10λ"), Category("���"), Obsolete("û��ʲô����")]
        //public string ID
        //{
        //    get { return id; }
        //    set { id = value; }
        //}

        private string caption;

        [Description("�б��⣬��󳤶�10λ"), Category("���")]
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        private int wordCount;

        [Obsolete("����������Ҫ�����ַ�����", true), Description("�ַ�������������ռ��λ"), Category("���")]
        public int WordCount
        {
            get { return wordCount; }
            set { wordCount = value; }
        }

        private int width;

        [Description("���"), Category("���")]
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int left;

        [Description("��߾�"), Category("���")]
        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        private ColumnStyle style;

        [Description("�����ͣ��������ı���ʱ��"), Category("���")]
        public ColumnStyle Style
        {
            get { return style; }
            set { style = value; }
        }

        private string[] items;

        [Description("����ѡ������"), Category("���")]
        public string[] Items
        {
            get { return items; }
            set { items = value; }
        }

        private bool isDescription = false;

        [Description("�Ƿ�������"), Category("���"), DefaultValue(false)]
        public bool IsDescription
        {
            get { return isDescription; }
            set { isDescription = value; }
        }

        private bool isUseHelp = false;

        [Description("�Ƿ���ʾ����"), DefaultValue(false), Category("���")]
        public bool IsUseHelp
        {
            get { return isUseHelp; }
            set { isUseHelp = value; }
        }

        private string[] help;

        [Description("������Ϣ"), Category("���")]
        public string[] Help
        {
            get { return help; }
            set { help = value; }
        }

    }
    public enum ColumnStyle
    {
        �ı���,
        ������,
        �����ı���,
        ����
    }
}
