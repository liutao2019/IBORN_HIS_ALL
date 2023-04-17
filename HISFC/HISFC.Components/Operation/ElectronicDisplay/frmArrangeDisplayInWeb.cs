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
       //王坤　时间：2011-11-25
        //目的：完成电子屏幕的显示

        #region 域变量

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

        #region 初始化

        public frmArrangeDisplayInWeb()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件


        /// <summary>
        /// 加载html模板
        /// </summary>
        /// <param name="TempName"></param>
        public void LoadHtmlTemp(string TempName)
        {
            #region 加载HTML

            string DPath = "D:\\htsoft\\FS\\ArrangeDisplay\\" + TempName + ".html";
            string Path = Application.StartupPath;
            Path += "\\ArrangeDisplay\\" + TempName + ".html";
            if (!System.IO.File.Exists(Path))
            {
                if (!System.IO.File.Exists(DPath))
                {
                    //MessageBox.Show("载入指引单模板出错！请联系管理员！");
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
        /// 加载HTML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webArrangeBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        #endregion


    }
}