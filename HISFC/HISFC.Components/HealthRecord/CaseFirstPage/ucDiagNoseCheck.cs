using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucDiagNoseCheck<br></br>
    /// [��������: ������ϳ�ͻ�����ʾ]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-04-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDiagNoseCheck : Form
    {
        public ucDiagNoseCheck()
        {
            InitializeComponent();
        }
        #region  ȫ�ֱ���
        #endregion
        #region ����
        #endregion
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int initDiangNoseCheck(ArrayList list)
        {
            try
            {
                ucDiagCheck.InitInfo();
                ucDiagCheck.LoadInfo(list);
                if (this.ucDiagCheck.RedAlarm)
                {
                    this.label1.Text = "����ʧ�� : ���ȱ�ٱ������ɫ�����벹��";
                }
                else
                {
                    this.label1.Text = "����ɹ� : ��Ͽ��ܻ�����©,��˲�";
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// ����б�����д����Ŀ ���� true; ���򷵻� false; 
        /// </summary>
        /// <returns></returns>
        public bool GetRedALarm()
        {
            return ucDiagCheck.RedAlarm;
        }

        private void FrmDiagNoseCheck_Activated(object sender, System.EventArgs e)
        {

        }

        private void tbReturn_Click(object sender, System.EventArgs e)
        {
            //�ر�
            this.Close();
        }
    }
}