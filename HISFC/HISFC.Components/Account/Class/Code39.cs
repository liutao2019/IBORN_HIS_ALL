using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace FS.HISFC.Components.Account.Class
{
    /// <summary>
    /// ������ luzhp 2007-7-11
    /// </summary>
    public class Code39
    {
        private const int _itemSepHeight = 3;
        //����size
        SizeF _titleSize = SizeF.Empty;
        //����size
        SizeF _barCodeSize = SizeF.Empty;
        //�����ַ���size
        SizeF _codeStringSize = SizeF.Empty;

        #region ����

        private string _titleString = null;
        private Font _titleFont = null;
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get { return _titleString; }
            set { _titleString = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public Font TitleFont
        {
            get { return _titleFont; }
            set { _titleFont = value; }
        }
        #endregion

        #region �����ַ���

        private bool _showCodeString = false;
        private Font _codeStringFont = null;
        /// <summary>
        /// �Ƿ���ʾ�����ַ���
        /// </summary>
        public bool ShowCodeString
        {
            get { return _showCodeString; }
            set { _showCodeString = value; }
        }

        /// <summary>
        /// �������ַ�������
        /// </summary>
        public Font CodeStringFont
        {
            get { return _codeStringFont; }
            set { _codeStringFont = value; }
        }
        #endregion

        #region ��������

        private Font _c39Font = null;
        private float _c39FontSize = 24;
        /// <summary>
        /// ����������size
        /// </summary>
        public float FontSize
        {
            get { return _c39FontSize; }
            set { _c39FontSize = value; }
        }

        /// <summary>
        /// �õ�����������
        /// </summary>
        private Font Code39Font
        {
            get
            {
                if (_c39Font == null)
                {
                    PrivateFontCollection pfc = new PrivateFontCollection();

                    byte[] fontdata = global ::FS.HISFC.Components.Account.Properties.Resources.FREE3OF9;
                    unsafe
                    {

                        fixed (byte* pFontData = fontdata)
                        {

                            pfc.AddMemoryFont((System.IntPtr)pFontData, fontdata.Length);

                        }

                    }
                    foreach (FontFamily ff in pfc.Families)
                    {
                        if (ff.IsStyleAvailable(FontStyle.Regular))
                        {
                            _c39Font = new Font(ff, _c39FontSize);
                            break;
                        }
                    }
                }
                return _c39Font;
            }
        }

        #endregion

        public Code39()
        {
            _titleFont = new Font("Arial", 8);
            _codeStringFont = new Font("Arial", 8);
        }

        #region ��������

        public Bitmap GenerateBarcode(string barCode)
        {

            int bcodeWidth = 0;
            int bcodeHeight = 0;

            // �õ��հ�ͼƬ
            Bitmap bcodeBitmap = CreateImageContainer(barCode, ref bcodeWidth, ref bcodeHeight);
            Graphics objGraphics = Graphics.FromImage(bcodeBitmap);

            // ��䱳����ɫ
            objGraphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, bcodeWidth, bcodeHeight));

            int vpos = 0;

            // ������
            if (_titleString != null)
            {
                objGraphics.DrawString(_titleString, _titleFont, new SolidBrush(Color.Black), XCentered((int)_titleSize.Width, bcodeWidth), vpos);
                vpos += (((int)_titleSize.Height) + _itemSepHeight);
            }
            // ������
            objGraphics.DrawString(barCode, Code39Font, new SolidBrush(Color.Black), XCentered((int)_barCodeSize.Width, bcodeWidth), vpos);

            // �������ַ���
            if (_showCodeString)
            {
                vpos += (((int)_barCodeSize.Height));
                objGraphics.DrawString(barCode, _codeStringFont, new SolidBrush(Color.Black), XCentered((int)_codeStringSize.Width, bcodeWidth), vpos);
            }

            //��������ͼƬ									
            return bcodeBitmap;
        }

        /// <summary>
        /// ����ָ����С�Ŀհ�ͼƬ
        /// </summary>
        /// <param name="barCode">�����ַ���</param>
        /// <param name="bcodeWidth">���</param>
        /// <param name="bcodeHeight">�߶�</param>
        /// <returns></returns>
        private Bitmap CreateImageContainer(string barCode, ref int bcodeWidth, ref int bcodeHeight)
        {

            Graphics objGraphics;
            //������ʱͼƬ
            Bitmap tmpBitmap = new Bitmap(1, 1, PixelFormat.Format64bppPArgb);
            objGraphics = Graphics.FromImage(tmpBitmap);

            // �������size
            if (_titleString != null)
            {
                _titleSize = objGraphics.MeasureString(_titleString, _titleFont);
                bcodeWidth = (int)_titleSize.Width;
                bcodeHeight = (int)_titleSize.Height + _itemSepHeight;
            }
            //��������size
            _barCodeSize = objGraphics.MeasureString(barCode, Code39Font);
            bcodeWidth = Max(bcodeWidth, (int)_barCodeSize.Width);
            bcodeHeight += (int)_barCodeSize.Height;
            //���������ַ���size
            if (_showCodeString)
            {
                _codeStringSize = objGraphics.MeasureString(barCode, _codeStringFont);
                bcodeWidth = Max(bcodeWidth, (int)_codeStringSize.Width);
                bcodeHeight += (_itemSepHeight + (int)_codeStringSize.Height);
            }

            //�ͷ���Դ
            objGraphics.Dispose();
            tmpBitmap.Dispose();

            return (new Bitmap(bcodeWidth, bcodeHeight, PixelFormat.Format32bppArgb));
        }

        #endregion


        #region ���㷽��

        /// <summary>
        /// �Ƚϴ�С
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        private int Max(int v1, int v2)
        {
            return (v1 > v2 ? v1 : v2);
        }

        /// <summary>
        /// �������ĵ�
        /// </summary>
        /// <param name="localWidth"></param>
        /// <param name="globalWidth"></param>
        /// <returns></returns>
        private int XCentered(int localWidth, int globalWidth)
        {
            return ((globalWidth - localWidth) / 2);
        }

        #endregion

    }
}
