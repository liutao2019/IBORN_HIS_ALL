using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class ucShowCaseInfo : UserControl
    {
        public ucShowCaseInfo()
        {
            InitializeComponent();
        }
        #region  ȫ�ֱ���
        private System.Data.DataSet ds = null;
        private FS.HISFC.BizLogic.HealthRecord.SearchManager SearMan = new FS.HISFC.BizLogic.HealthRecord.SearchManager();
        #endregion
        public void LockFp()
        {
            this.fpSpread1_Sheet1.Columns[0].Width = 60;//����
            this.fpSpread1_Sheet1.Columns[1].Width = 65;//סԺ��
            this.fpSpread1_Sheet1.Columns[2].Width = 50;//�Ա� 
            this.fpSpread1_Sheet1.Columns[3].Width = 50;//����
            this.fpSpread1_Sheet1.Columns[4].Width = 65;//��������
            this.fpSpread1_Sheet1.Columns[5].Width = 120;//����
            this.fpSpread1_Sheet1.Columns[6].Width = 120;//������
            this.fpSpread1_Sheet1.Columns[7].Width = 120;//������ַ
            this.fpSpread1_Sheet1.Columns[8].Width = 50;//����
            this.fpSpread1_Sheet1.Columns[9].Width = 65;//��Ժ����
            this.fpSpread1_Sheet1.Columns[10].Width = 65;//��Ժ�Ʊ�
            this.fpSpread1_Sheet1.Columns[11].Width = 65;//��Ժ����
            this.fpSpread1_Sheet1.Columns[12].Width = 65;//��Ժ�Ʊ�
        }
        /// <summary>
        /// ����sql��ѯ����
        /// </summary>
        /// <param name="xmlIndex">��ѯ������ </param>
        /// <param name="strWhere">ɸѡ����</param>
        /// <returns></returns>
        public int SearchInfo(string xmlIndex, string strWhere)
        {
            try
            {
                if (ds != null)
                {
                    ds.Clear();//���
                }
                else
                {
                    ds = new System.Data.DataSet();
                }
                SearMan.GetSearchInfo(xmlIndex, ds, strWhere);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        this.fpSpread1_Sheet1.DataSource = ds.Tables[0];
                    }
                }
                LockFp();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// ����sql��ѯ����
        /// </summary>
        /// <param name="xmlIndex"></param>
        /// <param name="likeName"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int SearchInfo(string xmlIndex, string likeName, string strWhere)
        {
            try
            {
                if (ds != null)
                {
                    ds.Clear();//���
                }
                else
                {
                    ds = new System.Data.DataSet();
                }
                SearMan.GetSearchInfo(xmlIndex, ds, strWhere);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        this.fpSpread1_Sheet1.DataSource = ds.Tables[0];
                    }
                }
                LockFp();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public void ExportInfo()
        {
            bool ret = false;
            //��������
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Excel|.xls";
                saveFileDialog1.FileName = "";

                saveFileDialog1.Title = "��������";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //��Excel ����ʽ��������
                    ret = fpSpread1.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    if (ret)
                    {
                        MessageBox.Show("�����ɹ���");
                    }
                }
            }
            catch (Exception ex)
            {
                //������
                MessageBox.Show(ex.Message);
            }
        }
        public void PrintInfo()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.PrintPreview(this.panel1);
        }

        private void ucShowCaseInfo_Load(object sender, System.EventArgs e)
        {
            LockFp();
        }
    }
}
