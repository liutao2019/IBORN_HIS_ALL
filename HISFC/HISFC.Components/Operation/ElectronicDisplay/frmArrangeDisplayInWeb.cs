using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Operation.ElectronicDisplay
{
    public partial class frmArrangeDisplayInWeb :FS.FrameWork.WinForms.Forms.BaseForm
    {
       //������ʱ�䣺2011-11-25
        //Ŀ�ģ���ɵ�����Ļ����ʾ

        #region �����

        private string bodyHtml = "";

        public string BodyHtml
        {
            get
            {
                return bodyHtml;
            }
            set
            {
                bodyHtml = value;
            }
        }

        #endregion

        #region ��ʼ��

        public frmArrangeDisplayInWeb()
        {
            InitializeComponent();
        }

        #endregion

        #region �¼�


        /// <summary>
        /// ����htmlģ��
        /// </summary>
        /// <param name="TempName"></param>
        public void LoadHtmlTemp(string TempName)
        {
            #region ����HTML

            string DPath = "D:\\htsoft\\FS\\ArrangeDisplay\\" + TempName + ".html";
            string Path = Application.StartupPath;
            Path += "\\ArrangeDisplay\\" + TempName + ".html";
            if (!System.IO.File.Exists(Path))
            {
                if (!System.IO.File.Exists(DPath))
                {
                    //MessageBox.Show("����ָ����ģ���������ϵ����Ա��");
                    return;
                }
                else
                {
                    webArrangeBrowser.Navigate(DPath);
                }
            }
            else
            {
                webArrangeBrowser.Navigate(Path);
            }

            #endregion

        }
        /// <summary>
        /// ����HTML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webArrangeBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        #endregion


    }
}