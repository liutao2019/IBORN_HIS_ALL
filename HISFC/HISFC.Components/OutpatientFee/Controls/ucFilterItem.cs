using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UFC.OutpatientFee.Controls
{
    public partial class ucFilterItem : UserControl, Neusoft.HISFC.Integrate.FeeInterface.IChooseItemForOutpatient
    {
        public ucFilterItem()
        {
            InitializeComponent();
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        #region ����

        /// <summary>
        /// ִ�п��Ҵ���
        /// </summary>
        protected string deptCode = string.Empty;

        /// <summary>
        /// �Ƿ��жϿ��
        /// </summary>
        protected bool isJudgeStore = false;

        /// <summary>
        /// �Ƿ�ģ����ѯ
        /// </summary>
        protected bool isQueryLike = false;

        /// <summary>
        /// �Ƿ�ѡ����Ŀ��ر�uc Ĭ��Ϊtrue
        /// </summary>
        protected bool isSelectAndClose = true;

        /// <summary>
        /// �Ƿ�ѡ������Ŀ
        /// </summary>
        protected bool isSelectItem = false;

         /// <summary>
        /// Ĭ����ʾ����
        /// </summary>
        protected int itemCount = 10;

        /// <summary>
        /// ��ʾ��Ŀ���
        /// </summary>
        protected Neusoft.HISFC.Object.Base.ItemTypes itemType = Neusoft.HISFC.Object.Base.ItemTypes.All;

         /// <summary>
        /// ��ǰѡ�����Ŀ
        /// </summary>
        protected Neusoft.HISFC.Object.Base.Item nowItem = new Neusoft.HISFC.Object.Base.Item();

         /// <summary>
        /// ��ǰ���ݵĹ��˶���
        /// </summary>
        protected object objectFilterObject = new object();

        /// <summary>
        /// ��ѯ��ʽ
        /// </summary>
        protected string queryType = string.Empty;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        DataSet dsItem = new DataSet();

       
        #endregion

        #region ˽�з���

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="inputChar">�����ַ���</param>
        private void SetFilter(string inputChar) 
        {
            string filterString = string.Empty;
            
            if (inputChar == string.Empty)
            {
                filterString = "1 = 1";
            }
            else
            {
                filterString = "SPELL_CODE like '%" + inputChar.ToUpper() + "%'";
            }

            this.dataWindowControl1.SetFilter(filterString);

            this.dataWindowControl1.Filter();

            if (this.dataWindowControl1.RowCount > 0)
            {
                this.dataWindowControl1.SelectRow(1, true);
            }
        }

        /// <summary>
        /// �������ƻ�õ�ǰ�к�
        /// </summary>
        /// <param name="columnName">����</param>
        /// <returns>�ɹ� ��ǰ�к� ʧ�� -1</returns>
        private short GetCurrentColumn(string columnName) 
        {
            Sybase.DataWindow.GraphicObjectColumn g = null;

            for (short i = 1; i < this.dataWindowControl1.ColumnCount; i++) 
            {
                g =  this.dataWindowControl1.GetColumnObjectByNumber(i);

                if (g != null) 
                {
                    if (g.Name.ToLower() == columnName.ToLower()) 
                    {
                        g = null;
                        
                        return i;
                    }
                }
            }

            g = null;

            return -1;
        }

        /// <summary>
        /// ��õ�ǰ��Ŀ
        /// </summary>
        private int GetItem() 
        {
            int row = this.dataWindowControl1.CurrentRow;

            if (row < 1 || this.dataWindowControl1.RowCount == 0)
            {
                this.isSelectItem = false;

                return -1;
            }
            
            string itemCode = this.dataWindowControl1.GetItemString(row, this.GetCurrentColumn("item_code"));
            string exeDeptCode = this.dataWindowControl1.GetItemString(row, this.GetCurrentColumn("exe_dept"));
            string drugFlag = this.dataWindowControl1.GetItemString(row, this.GetCurrentColumn("drug_flag"));

            this.isSelectItem = true;

            this.SelectedItem(itemCode, drugFlag, exeDeptCode);

            this.Visible = false;

            return 1;
        }

        #endregion

        #region IChooseItemForOutpatient ��Ա

        /// <summary>
        /// ѡ����Ŀ�ķ�ʽ
        /// </summary>
        public Neusoft.HISFC.Integrate.FeeInterface.ChooseItemTypes ChooseItemType
        {
            get
            {
                return Neusoft.HISFC.Integrate.FeeInterface.ChooseItemTypes.ItemChanging;
            }
            set
            {
                
            }
        }

        /// <summary>
        /// ִ�п��Ҵ���
        /// </summary>
        public string DeptCode
        {
            get
            {
                return this.deptCode;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// �õ���ǰ����
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetSelectedItem(ref Neusoft.HISFC.Object.Base.Item item)
        {
            return this.GetItem();
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Init()
        {
            this.Visible = false;
            
            return 1;
        }

        public string InputPrev
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// �Ƿ��жϿ��
        /// </summary>
        public bool IsJudgeStore
        {
            get
            {
                return this.isJudgeStore;
            }
            set
            {
                this.isJudgeStore = value;
            }
        }

        /// <summary>
        /// �Ƿ�ģ����ѯ
        /// </summary>
        public bool IsQueryLike
        {
            get
            {
                return this.isQueryLike;
            }
            set
            {
                this.isQueryLike = value;
            }
        }

        /// <summary>
        /// �Ƿ�ѡ����Ŀ��ر�uc
        /// </summary>
        public bool IsSelectAndClose
        {
            get
            {
                return this.isSelectAndClose;
            }
            set
            {
                this.isSelectAndClose = value;
            }
        }

        /// <summary>
        /// �Ƿ�ѡ������Ŀ
        /// </summary>
        public bool IsSelectItem
        {
            get
            {
                return this.isSelectItem;
            }
            set
            {
                this.isSelectItem = value;
            }
        }

        /// <summary>
        /// Ĭ��ѡ������
        /// </summary>
        public int ItemCount
        {
            get
            {
                return this.itemCount;
            }
            set
            {
                this.itemCount = value;
            }
        }

        /// <summary>
        /// ��ʾ��Ŀ���
        /// </summary>
        public Neusoft.HISFC.Object.Base.ItemTypes ItemType
        {
            get
            {
                return this.itemType;
            }
            set
            {
                this.itemType = value;
            }
        }

        /// <summary>
        /// ��ǰѡ�����Ŀ
        /// </summary>
        public Neusoft.HISFC.Object.Base.Item NowItem
        {
            get
            {
                return this.nowItem;
            }
            set
            {
                this.nowItem = value;
            }
        }

        /// <summary>
        /// ��ǰ���ݵĹ��˶���
        /// </summary>
        public object ObjectFilterObject
        {
            get
            {
                return this.objectFilterObject;
            }
            set
            {
                this.objectFilterObject = value;
            }
        }

        /// <summary>
        /// ��ѯ��ʽ
        /// </summary>
        public string QueryType
        {
            get
            {
                return this.queryType;
            }
            set
            {
                this.queryType = value;
            }
        }

        /// <summary>
        /// ѡ����Ŀ�¼�
        /// </summary>
        public event Neusoft.HISFC.Integrate.FeeInterface.WhenGetItem SelectedItem;

        /// <summary>
        /// ��������DataSet
        /// </summary>
        /// <param name="dsItem">��Ŀ����</param>
        public void SetDataSet(DataSet dsItem)
        {
            this.dsItem = dsItem;

            this.dataWindowControl1.SetRedrawOff();

            this.dataWindowControl1.Retrieve(this.dsItem.Tables[0]);

            this.dataWindowControl1.SetRedrawOn();
        }

        /// <summary>
        /// ��ǰ�����ַ���
        /// </summary>
        /// <param name="inputChar">��ǰ�����ַ���</param>
        /// <param name="inputType">ģ����ѯ��ʽ</param>
        public void SetInputChar(object sender, string inputChar, Neusoft.HISFC.Object.Base.InputTypes inputType)
        {
            if (!this.Visible) 
            {
                this.Show();
            }

            this.Show();

            this.BringToFront();
            
            this.SetFilter(inputChar);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p">��ǰ����</param>
        public void SetLocation(Point p) 
        {
            this.Location = p;
        }

        /// <summary>
        /// ��һ��
        /// </summary>
        public void NextRow() 
        {
            this.dataWindowControl1.Scroll(Sybase.DataWindow.ScrollAction.ScrollNextRow);

            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        /// <summary>
        /// ��һҳ
        /// </summary>
        public void NextPage()
        {
            this.dataWindowControl1.Scroll(Sybase.DataWindow.ScrollAction.ScrollNextPage);

            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        /// <summary>
        /// ��һ��
        /// </summary>
        public void PriorRow() 
        {
            this.dataWindowControl1.Scroll(Sybase.DataWindow.ScrollAction.ScrollPriorRow);

            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        /// <summary>
        /// ��һҳ
        /// </summary>
        public void PriorPage() 
        {
            this.dataWindowControl1.Scroll(Sybase.DataWindow.ScrollAction.ScrollNextPage);

            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        /// <summary>
        /// ѡ��ǰ��Ŀ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ��-1</returns>
        public int GetSelectedItem() 
        {
            return this.GetItem();
        }

        #endregion

        private void dataWindowControl1_Click(object sender, EventArgs e)
        {
            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        private void dataWindowControl1_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        private void dataWindowControl1_DoubleClick(object sender, EventArgs e)
        {
            this.GetItem();
        }

        private void dataWindowControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                this.GetItem();
            }
        }
    }
}
