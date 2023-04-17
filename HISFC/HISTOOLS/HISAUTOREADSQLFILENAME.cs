using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HISTOOLS
{
    /// <summary>
    /// cao-lin
    /// 生成批处理脚本
    /// </summary>
    public partial class HISAUTOREADSQLFILENAME : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public HISAUTOREADSQLFILENAME()
        {
            InitializeComponent();
            this.nbtGenerateSql.Click += new EventHandler(nbtGenerateSql_Click);
            this.nbtCheckDir.Click += new EventHandler(nbtCheckDir_Click);
        }

        private string curFile = string.Empty;

        private string curDir = string.Empty;

        /// <summary>
        /// 选择路径按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void nbtCheckDir_Click(object sender, EventArgs e)
        {
            this.CheckDir();
          
        }

        /// <summary>
        /// 选择路径
        /// </summary>
        private void CheckDir()
        {
            string dir = string.Empty;
            this.openFileDialog1 = new OpenFileDialog();
            this.openFileDialog1.InitialDirectory = "E:\\中大五院\\程序更新\\";
            this.openFileDialog1.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            this.openFileDialog1.RestoreDirectory = true;
            string fName = string.Empty;
            string fName1 = string.Empty;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fName = this.openFileDialog1.FileName;
                fName1 = this.openFileDialog1.SafeFileName;
                curFile = fName;
                curDir = fName.Replace(fName1,string.Empty);
            }
            this.nlbCurDir.Text = curDir;
        }

        /// <summary>
        /// 生成按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void nbtGenerateSql_Click(object sender, EventArgs e)
        {
            this.GenerateSqlName();
        }

        /// <summary>
        /// 生成所有脚本
        /// </summary>
        private void GenerateSqlName()
        {
            //先删除原来的批处理
            FileInfo fileInfo = new FileInfo(curFile);
            fileInfo.Delete();
            string addSql = string.Empty;
            this.ntbGenerateSql.Text = string.Empty;
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(this.curDir);
            addSql = "cd %cd%" + "\r\n" + "@echo 请输入连接数据库的用户名/密码@服务名:" + "\r\n" + "set /p dbstr=" + "\r\n\r\n" + "@echo 正在处理脚本\r\n";
            //curFile = curFile.Replace(".bat",".txt");
            FileStream fs = new FileStream(curFile, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(addSql);
            System.IO.FileInfo [] fileInfos = directoryInfo.GetFiles();
            var sorted = from r in fileInfos
                         orderby r.Name  //这里是你要按照什么排序
                         select r;       //要返回什么

            foreach (System.IO.FileInfo file in sorted)
            {
                addSql = string.Empty;
                if(file.Name.Contains(".bat")||file.Name.Contains(".txt"))
                {
                    continue;
                }
                addSql += "\r\n" + "sqlplus %dbstr% @" + file.Name + "  > " + file.Name.Replace(".sql", ".log");
                sw.WriteLine(addSql);
            }
            addSql = string.Empty;
            addSql += "\r\n\r\n\r\n" + "@echo 成功" + "\r\n" + "PAUSE" + "\r\n" + "--exit";
            sw.WriteLine(addSql);
            sw.Close();
            //System.IO.File.Move(curFile, curFile.Replace(".txt", ".bat"));
            //curFile = curFile.Replace(".txt", ".bat");

            System.IO.StreamReader sr1 = new System.IO.StreamReader(curFile, Encoding.GetEncoding("GB2312"));

            this.ntbGenerateSql.Text += sr1.ReadToEnd();

            sr1.Close();

        }
    }
}
