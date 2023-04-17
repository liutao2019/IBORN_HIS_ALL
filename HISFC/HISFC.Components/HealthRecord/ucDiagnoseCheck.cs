using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord
{
    public partial class ucDiagnoseCheck : UserControl
    {
        /// <summary>
        /// ucDiagnoseCheck<br></br>
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
        public ucDiagnoseCheck()
        {
            InitializeComponent();
        }

        #region  ȫ�ֱ���
        private System.Drawing.Color alarmColr = System.Drawing.Color.Red;
        private bool redAlarm = false;
        #endregion

        #region  ����
        /// <summary>
        /// ����б����������Ŀ, redAlarm Ϊ true;
        /// </summary>
        public bool RedAlarm
        {
            get
            {
                return redAlarm;
            }
        }
        /// <summary>
        /// ���ñ����������ı���ɫ
        /// </summary>
        private System.Drawing.Color AlarmColr
        {
            get
            {
                return alarmColr;
            }
            set
            {
                alarmColr = value;
            }
        }
        #endregion
        /// <summary>
        /// ������Ϣ�б�
        /// </summary>
        /// <param name="list"></param>
        /// <returns>������ -1 </returns>
        public int LoadInfo(ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                ListViewItem item = new ListViewItem(obj.DiagInfo.ICD10.Name);
                item.SubItems.Add(obj.User02);
                if (obj.User01 == "2") //��������� 
                {
                    //���ñ���Ϊ��ɫ
                    item.BackColor = System.Drawing.Color.Red;
                    redAlarm = true;
                }
                listView1.Items.Add(item);
            }
            return 1;
        }
        /// <summary>
        /// ��ʼ���ؼ� ������ͷ 
        /// </summary>
        /// <returns></returns>
        public int InitInfo()
        {
            //Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = true;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Create columns for the items and subitems.
            listView1.Columns.Add("�������", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("��ʾ��Ϣ", -2, HorizontalAlignment.Left);
            return 1;
        }
    }
}
