using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinIpr
{
    public partial class ucFinIprOutFreecost : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIprOutFreecost ()
        {
            InitializeComponent();
        }

        
        //private bool isAllDept = true;

        /// <summary>
        /// סԺ������  
        /// </summary>
        //public bool IsAllDept
        //{
        //    get
        //    {
        //        return this.isAllDept;
        //    }
        //    set
        //    {
        //        this.isAllDept = value;
        //    }
        //}

        
               

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

        //protected override int OnQuery(object sender, object neuObject)
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            //string  deptCode;

            //if (this.IsAllDept)
            //{
            //   deptCode = "all" ;
                
            //}
            //else
            //{
            //    deptCode = this.employee.Dept.ID;
            //}
            
             this.dwMain.Modify("time.text='��Ժʱ�䣺" + this.beginTime.ToString() + "��" + this.endTime.ToString() + "'");
             this.dwMain.RowFocusChanged -= this.dwMain_RowFocusChanged;
             int num = this.dwMain.Retrieve(this.beginTime, this.endTime);
             this.dwMain.RowFocusChanged += this.dwMain_RowFocusChanged;
             if (dwMain.RowCount > 0)
             {
                 RetrieveDetail(1);

             }
             else
             {
                 dwDetail.Reset();
             }
             return num;
            
        }

        private void dwMain_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            if (e.RowNumber == 0)
            {
                this.dwDetail.Reset();
                return;
            }
             RetrieveDetail(e.RowNumber);
        }

        private void RetrieveDetail(int currRow)
        {
           

                //���ұ���
                string deptNo = string.Empty;
                //��ʼʱ��
                DateTime beginDate;
                //����ʱ��
                DateTime endDate;

                try
                {
                    deptNo = this.dwMain.GetItemString(currRow, "aaa"); //���ұ���
                    //beginDate = this.dwMain.GetItemDateTime(e.RowNumber, "begin_date");
                    //endDate = this.dwMain.GetItemDateTime(e.RowNumber, "end_date");
                    this.dwDetail.Modify("time.text='��Ժʱ�䣺" + this.beginTime.ToString() + "��" + this.endTime.ToString() + "'");
                    this.dwDetail.Retrieve(deptNo, beginTime, endTime);
                   
                }
                finally
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

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

