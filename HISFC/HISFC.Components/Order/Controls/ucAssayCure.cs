using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������������ҽ������]
    /// [�� �� �ߣ�Ѧ�Ľ�]
    /// [����ʱ�䣺2009-8-25]
    /// {1F2B9330-7A32-4da4-8D60-3A4568A2D1D8}
    /// </summary>
    public partial class ucAssayCure : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public ucAssayCure()
        {
            InitializeComponent();
        }

        #endregion

        #region ����

        /// <summary>
        /// �������ɴ���
        /// </summary>
        /// <param name="alOrders"></param>
        public delegate void MakeSuccessedHandler(System.Collections.ArrayList alOrders);

        /// <summary>
        /// ���ɻ���ҽ��
        /// </summary>
        public event MakeSuccessedHandler MakeSuccessed;

        /// <summary>
        /// ����һЩҽ����Ϣ
        /// </summary>
        private System.Collections.ArrayList orders = new System.Collections.ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// ����һЩҽ����Ϣ
        /// </summary>
        public System.Collections.ArrayList Orders
        {
            get
            {
                return this.orders;
            }
            set
            {
                this.orders = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ȡ����ҽ��ʱ��
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="injectDate"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Base.OperEnvironment> GetOrderDate(string strDate, DateTime injectDate)
        {
            //{7E8FD8F7-AB9D-47ce-A245-37F8BE2D023D} �쳣���� ���ӽ�����ʾ��Ϣ
            try
            {
                strDate = strDate.ToLower();//������ĸ��תСд

                List<FS.HISFC.Models.Base.OperEnvironment> listDate = new List<FS.HISFC.Models.Base.OperEnvironment>();
                if (strDate.IndexOf('-') > 0)
                {
                    //����: d1-d8,��ô��-�ָ������һ��������Ԫ�ص�����,0Ϊ��ʼ����,1Ϊ��������
                    //Ҫ������ַ�����������,��������d1,d2,d3-d5,���ָ�ʽ������
                    string[] temp = strDate.Split(new char[] { '-' });
                    int begin = int.Parse(temp[0].Replace("d", "").Trim()) - 1; //�ӵ�ǰ���ڿ�ʼ��,���Լ�һ
                    int end = int.Parse(temp[1].Replace("d", "").Trim()) - 1;   //��Ȼ��ʼ���ڼ�һ��,�ǽ�������Ҳ���밡

                    FS.HISFC.Models.Base.OperEnvironment obj = null;
                    for (int i = begin; i <= end; i++)
                    {
                        obj = new FS.HISFC.Models.Base.OperEnvironment();
                        obj.OperTime = injectDate.AddDays(i);
                        obj.ID = "d" + Convert.ToString(i + 1) + "(" + obj.OperTime.ToShortDateString() + ")";
                        listDate.Add(obj);
                    }
                }
                else if (strDate.IndexOf(',') > 0)
                {
                    //����: d1,d2,d4,d8,��ô��,�ָ������һ������
                    string[] temp = strDate.Split(new char[] { ',' });
                    int[] days = new int[temp.Length];

                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i] != "")
                        {
                            days[i] = int.Parse(temp[i].Replace("d", "").Trim()) - 1; //�ӵ�ǰ���ڿ�ʼ��,�������е�ֵ����Ҫ��һ
                        }
                    }

                    FS.HISFC.Models.Base.OperEnvironment obj = null;
                    for (int i = 0; i < days.Length; i++)
                    {
                        obj = new FS.HISFC.Models.Base.OperEnvironment();
                        obj.OperTime = injectDate.AddDays(days[i]);
                        obj.ID = "d" + Convert.ToString(days[i] + 1) + "(" + obj.OperTime.ToShortDateString() + ")";
                        listDate.Add(obj);
                    }
                }
                else
                {
                    MessageBox.Show("������Ϣ��ʽ����ȷ���������ɫ��ʾ��Ϣ������ȷ��Ϣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return null;
                }

                return listDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("������Ϣ��ʽ����ȷ���������ɫ��ʾ��Ϣ������ȷ��Ϣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return null;
            }
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (this.dtpBeginDate.Value.Date < CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show("��ʼ���ڲ���С�ڵ�ǰ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (this.txtDays.Text.Trim().Equals(""))
            {
                MessageBox.Show("�����뻯������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// ���ɻ���ҽ��
        /// </summary>
        /// <returns></returns>
        private int SaveAssayCure()
        {
            DateTime currentTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();//��ǰϵͳʱ��

            DateTime tempOrder = this.dtpBeginDate.Value;

            //ȡҽ��ʱ��
            List<FS.HISFC.Models.Base.OperEnvironment> listDays = this.GetOrderDate(this.txtDays.Text, tempOrder);
            if (listDays == null)
            {
                return -1;
            }

            System.Collections.ArrayList al = new System.Collections.ArrayList();
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            string comboID = string.Empty;
            string preComboID = string.Empty;
            string preNewComboID = string.Empty;

            foreach (FS.HISFC.Models.Base.OperEnvironment dt in listDays)
            {
                if (dt.OperTime.Date < currentTime.Date)
                {
                    MessageBox.Show("����ʱ�䲻��С�ڵ�ǰʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }


                int rowIndex = 0;//����ҽԺ�������,��һ��ҩ����ֻ�е�һ��ҽ�����ϱ�ע

                foreach (FS.HISFC.Models.Order.Inpatient.Order obj in this.orders)
                {

                    order = obj.Clone();
                    order.SortID = 0;
                    if (obj.Combo.ID == preComboID)
                    {
                        order.Combo.ID = preNewComboID;
                    }
                    else
                    {
                        comboID = CacheManager.InOrderMgr.GetNewOrderComboID();//����
                        preComboID = obj.Combo.ID;
                        preNewComboID = comboID;
                        order.Combo.ID = comboID;
                    }
                    order.BeginTime = dt.OperTime;
                    order.Oper.OperTime = dt.OperTime;
                    order.ID = string.Empty;
                    if (rowIndex == 0)
                    {
                        order.Memo += dt.ID;
                        rowIndex++;
                    }
                    else
                    {
                        if (order.Memo == "")
                        {
                            order.Memo = string.Empty;
                        }
                    }

                    al.Add(order);//һ��һ���ĸ㰡,�ǲ�
                }
            }

            if (this.MakeSuccessed != null)
            {
                this.MakeSuccessed(al);
            }

            return 1;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ���水ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.IsValid())
            {
                return;
            }

            if (this.orders == null)
            {
                MessageBox.Show("������ʱҽ����ѡ����Ŀ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (SaveAssayCure() == -1)
            {
                return;
            }

            this.FindForm().DialogResult = DialogResult.OK;//����Ҫ�õ���

            this.FindForm().Close();
        }

        /// <summary>
        /// ȡ����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion
    }
}
