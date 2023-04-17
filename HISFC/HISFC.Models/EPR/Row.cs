using System;
using System.Collections.Generic;
using System.Text;
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
    public class Row : FS.FrameWork.Models.NeuObject, System.IComparable
    {
        private string text;

        [Description("�ı�"), Category("���")]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private string rtf;

        [Description("�г��ı�"), Category("���")]
        public string Rtf
        {
            get { return rtf; }
            set { rtf = value; }
        }

        private System.Drawing.Bitmap bmp;

        [Description("��ͼ��"), Category("���")]
        public System.Drawing.Bitmap BmpLine
        {
            get { return bmp; }
            set { bmp = value; }
        }

        private int rowCount;

        /// <summary>
        /// ������
        /// ���ǵ�ͼƬ���Զ���ͼ��ռ�ö��е����⣬�����������
        /// </summary>
        [Description("������"), Category("���")]
        public int RowCount
        {
            get { return rowCount; }
            set { rowCount = value; }
        }

        private DateTime dateInput;

        /// <summary>
        /// ��������
        /// ����
        /// </summary>
        [Description("������"), Category("���")]
        public DateTime DateInput
        {
            get { return dateInput; }
            set { dateInput = value; }
        }

        private int index;

        /// <summary>
        /// ���������ڵ�����
        /// ����
        /// </summary>
        [Description("���������ڵ�����"), Category("���")]
        public int Index
        {
            get { return index; }
            set { index = value; }
        }


        #region IComparable ��Ա

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            FS.HISFC.Models.EPR.Row ctr = (FS.HISFC.Models.EPR.Row)obj;
            if (this == null)
            {
                return -1;
            }
            else if (this.DateInput < ctr.DateInput)
            {
                return -1;
            }
            else if (this.DateInput < ctr.DateInput)
            {
                return 1;
            }
            else
            {
                if (this.index < ctr.index)
                {
                    return -1;
                }
                else if (this.index > ctr.index)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion
    }
}
