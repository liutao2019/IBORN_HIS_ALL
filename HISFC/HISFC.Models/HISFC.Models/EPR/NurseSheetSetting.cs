using System;
using System.Collections;
using System.ComponentModel;
namespace FS.HISFC.Models.EPR
{
    /// <summary>
    /// ucNurseSheetSetting<br></br>
    /// [��������: �����¼������]<br></br>
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
	public class NurseSheetSetting:System.IComparable
    {

        //private int printType = 0;
        //[Obsolete("��������ģ�������������ô�ӡ��ʽ", true), Description("��ӡ���ͣ�0 �����¼��һ�����ʽ��1 �����¼���ϱ����������ʽ��2 �����¼���������������и�ʽ��3 �����¼�Ĳ��̸�ʽ"), DefaultValue(0), Category("��ӡ")]
        //public int PrintType
        //{
        //    get { return printType; }
        //    set { printType = value; }
        //}

        //private string page = "A4";

        //[Obsolete("��������ģ�������������ô�ӡֽ��", true), Description("ֽ�Ŵ�С��A4��B5"), DefaultValue("A4"), Category("��ӡ")]
        //public string Page
        //{
        //    get { return page; }
        //    set { page = value; }
        //}

        //private bool landSacpe = false;

        //[Obsolete("��������ģ�������������ô�ӡֽ��", true), Description("�Ƿ�����ӡ"), DefaultValue(false), Category("��ӡ")]
        //public bool LandSacpe
        //{
        //    get { return landSacpe; }
        //    set { landSacpe = value; }
        //}

        //private int startX = 65;

        //[Obsolete("��������ģ�������������ô�ӡ��ʽ",true), Description("��ӡ��ʼX����"), DefaultValue(65), Category("��ӡ")]
        //public int StartX
        //{
        //    get { return startX; }
        //    set { startX = value; }
        //}

        //private int startY = 40;

        //[Obsolete("��������ģ�������������ô�ӡ��ʽ",true), Description("��ӡ��ʼY����"), DefaultValue(40), Category("��ӡ")]
        //public int StartY
        //{
        //    get { return startY; }
        //    set { startY = value; }
        //}

        //private int rowHeight = 30;

        //[Obsolete("��������ģ�������������ô�ӡ��ʽ",true), Description("��ӡ�и�"), DefaultValue(30), Category("��ӡ")]
        //public int RowHeight
        //{
        //    get { return rowHeight; }
        //    set { rowHeight = value; }
        //}

        //private int rowCount1 = 16;

        //[Obsolete("��������ģ�������������ô�ӡ��ʽ",true), Description("��ӡ��������������"), DefaultValue(16), Category("��ӡ")]
        //public int RowCount1
        //{
        //    get { return rowCount1; }
        //    set { rowCount1 = value; }
        //}

        //private int rowCount2 = 16;

        //[Obsolete("��������ģ�������������ô�ӡ��ʽ", true), Description("��ӡ��������������"), DefaultValue(16), Category("��ӡ")]
        //public int RowCount2
        //{
        //    get { return rowCount2; }
        //    set { rowCount2 = value; }
        //}

        //private int captionRowCount = 4;

        //[Obsolete("��������ģ�������������ô�ӡ��ʽ", true), Description("��ӡ����������"), DefaultValue(16), Category("��ӡ")]
        //public int CaptionRowCount
        //{
        //    get { return captionRowCount; }
        //    set { captionRowCount = value; }
        //}

        private FS.HISFC.Models.EPR.NurseSheetSettingColumn[] columns;

        [Description("�����¼��"), DefaultValue(16), Category("����")]
        public FS.HISFC.Models.EPR.NurseSheetSettingColumn[] Columns
        {
            get { return columns; }
            set { columns = value; }
        }
        //private System.Drawing.Bitmap backImage;

        //[Obsolete("��������ģ�������������ô�ӡ����ͼ", true), Description("����ͼ"), Category("��ӡ")]
        //public System.Drawing.Bitmap BackImage
        //{
        //    get { return backImage; }
        //    set { backImage = value; }
        //}

        #region IComparable ��Ա

        int IComparable.CompareTo(object obj)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
