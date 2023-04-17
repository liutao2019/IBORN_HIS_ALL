using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.FinIpr
{
    public partial class ucFinIprFreecost : Report.Common.ucQueryBaseForDataWindow
    {
        /// <summary>
        /// סԺ������ԺǷ��ͳ��
        /// </summary>
        /// <returns></returns>
        public ucFinIprFreecost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            return this.dwMain.Retrieve(this.beginTime, this.endTime);

        }
        
        private void dwMain_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            if (e.RowNumber == 0)
            {
                return;
            }
            string dept;
            DateTime b_d;
            DateTime e_d;
            try
            {
                dept = dwMain.GetItemString(e.RowNumber, "aaa");
                //b_d = dwMain.GetItemDateTime(e.RowNumber, "begin_date");
                //e_d = dwMain.GetItemDateTime(e.RowNumber, "end_date");

                this.dwDetail.Retrieve(dept, this.beginTime, this.endTime);
            }
            catch
            {
                return;
            }
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
                //�������������DataWindow�ؼ�ʱ��Ҫ��ӡ����ʱ�����ݽ����жϴ�ӡ�ĸ�DataWindow�ؼ�
                
                if (this.dwMain.Focused)
                {
                    this.dwMain.Print();

                }
                else if(this.dwDetail.Focused) //����DataWindow�ؼ���ӡ���������
                {
                    this.dwDetail.Print();
                   
                }
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
                else
                {
                    return 1;
                }
                
                //���ݽ����жϵ����ĸ�DataWindow
                if (this.dwMain.Focused)
                {
                    this.dwMain.SaveAs(fileName, Sybase.DataWindow.FileSaveAsType.Excel);
                }
                else if(this.dwDetail.Focused) //����Դ�����
                {
                    this.dwDetail.SaveAs(fileName, Sybase.DataWindow.FileSaveAsType.Excel);
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������","��ʾ");
                return -1;
            }
        }

    }
}

