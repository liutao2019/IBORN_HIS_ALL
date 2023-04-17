using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.MetOrd
{
    public partial class ucMetOrdListDoct : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucMetOrdListDoct()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                this.dwMain.Print();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡ����", "��ʾ");
                return -1;
            }

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override int OnExport()
        {
            //������ڶ��DataWindowʱ����������Ҫ��д��������Ҫ��д�÷��������ݽ����жϵ��������ĸ�DataWindow
            try
            {
                //����Excel��ʽ�ļ�
                SaveFileDialog saveDial = new SaveFileDialog();
                saveDial.Filter = "Excel�ļ���*.xls��|*.xls";
                //�ļ���
                string fileName = string.Empty;
                if (saveDial.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveDial.FileName;
                }
                this.dwMain.SaveAs(fileName, Sybase.DataWindow.FileSaveAsType.Excel);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������", "��ʾ");
                return -1;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            dwMain.Modify("t_date.text = '" + this.dtpBeginTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + " -- " + this.dtpEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            return base.OnRetrieve(beginTime, endTime);
        }
    }
}
