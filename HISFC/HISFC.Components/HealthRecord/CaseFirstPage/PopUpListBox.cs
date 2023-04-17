using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
	/// <summary>
    /// ListBox ��ժҪ˵����IFpInputable
	/// </summary>
	public class PopUpListBox  :System.Windows.Forms.ListBox,FS.FrameWork.WinForms.Controls.IFpInputable
	{
        /// <summary> 
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

        public PopUpListBox()
        {
            // �õ����� Windows.Forms ���������������ġ�
            InitializeComponent();

            // TODO: �� InitializeComponent ���ú�����κγ�ʼ��
            Init();
            base.KeyDown += new KeyEventHandler(ListBox_KeyDown);
            base.Click += new EventHandler(ListBox_Click);

        }

        /// <summary> 
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region �����������ɵĴ���
        /// <summary> 
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
        /// �޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        private ArrayList alItems = new ArrayList();
        private DataSet dsItems = new DataSet();
        private bool showID = true;
        private bool selectNone = false;
        private int Spell = 0;
        /// <summary>
        /// �������뷨
        /// </summary>
        public int InputCode
        {
            get { return Spell; }
            set
            {
                Spell = value;
                //if(Spell>3||Spell<0)Spell=0;
                if (Spell > 5 || Spell < 0) Spell = 0;
            }
        }
        private bool omitFilter = true;
        /// <summary>
        /// �Ƿ�ģ����ѯ
        /// </summary>
        public bool OmitFilter
        {
            set
            {
                omitFilter = value;// omitFilter = true;
            }
            get
            {
                return omitFilter;
            }
        }
        /// <summary>
        /// ���е����ݣ�ԭ����alItems
        /// </summary>
        public ArrayList Items
        {
            get
            {
                return this.alItems;
            }
            set
            {
                this.alItems = value;
            }
        }
        bool isSendToNext = false;

        /// <summary>
        /// �Ƿ�������Զ�������һ��
        /// </summary>
        public bool IsSendToNext
        {
            get
            {
                return isSendToNext;
            }
            set
            {
                isSendToNext = value;
            }
        }

        #region  ���õ����������Ϊ""ʱ ,Ĭ�ϲ�ѡ���κ���
        public bool SelectNone
        {
            get
            {
                return selectNone;
            }
            set
            {
                selectNone = value;
            }
        }

        bool isSelctNone = true;

        public bool IsSelctNone
        {
            get
            {
                return isSelctNone;
            }
            set
            {
                isSelctNone = value;
            }
        }

        public int ActRowIndex
        {
            get
            {
                return base.SelectedIndex;
            }
        }

        #endregion
        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        public bool IsShowID
        {
            get { return showID; }
            set { showID = value; }
        }
        public delegate int MyDelegate(Keys key);
        public event MyDelegate SelectItem;
        public new event System.EventHandler SelectedItem;

        public delegate void AutoSelectItemDele();
        public event AutoSelectItemDele AutoSelectItemEvent;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            dsItems.Tables.Add("items");
            dsItems.Tables["items"].Columns.AddRange(new DataColumn[]
				{
					new DataColumn("ID",Type.GetType("System.String")),//ID
					new DataColumn("Name",Type.GetType("System.String")),//����
					new DataColumn("spell_code",Type.GetType("System.String")),//ƴ����
					new DataColumn("input_code",Type.GetType("System.String")),//������
					new DataColumn("wb_code",Type.GetType("System.String"))//�����
				});
            dsItems.CaseSensitive = false;
            return 1;
        }
        /// <summary>
        /// �����Ŀ�б�
        /// </summary>
        /// <param name="Items"></param>
        /// <returns></returns>
        public int AddItems(ArrayList Items)
        {
            base.Items.Clear();

            //alItems=Items;
            if (this.isSelctNone)
            {
                ArrayList al = (ArrayList)Items.Clone();
                FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
                al.Insert(0, neuObj);

                alItems = al;
            }
            else
            {
                alItems = Items;
            }
            dsItems.Tables["items"].Rows.Clear();
            FS.FrameWork.Models.NeuObject objItem;
            FS.HISFC.Models.Base.ISpell objspell;

            try
            {
                for (int i = 0; i < alItems.Count; i++)
                {
                    objItem = new FS.FrameWork.Models.NeuObject();
                    objItem = (FS.FrameWork.Models.NeuObject)alItems[i];
                    if (objItem.GetType().GetInterface("ISpell", true) != null)
                    {
                        objspell = (FS.HISFC.Models.Base.ISpell)objItem;
                        base.Items.Add(objItem.ID + ". " + objItem.Name);
                        dsItems.Tables["items"].Rows.Add(new object[]{
																	 objItem.ID,objItem.Name,objspell.SpellCode,
																	 objspell.UserCode,objspell.WBCode});
                    }
                    else
                    {
                        base.Items.Add(objItem.ID + ". " + objItem.Name);
                        dsItems.Tables["items"].Rows.Add(new object[]{
																	 objItem.ID,objItem.Name,objItem.ID,
																	 objItem.Name,objItem.ID});
                    }

                }
            }
            catch (Exception error)
            {
                MessageBox.Show("�����Ŀ�б����!" + error.Message, "ListBox");
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Filter(string where)
        {
            where = where.Replace("'", "''");
            DataView _dv = new DataView(dsItems.Tables["items"]);
            if (Spell == 0)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '%" + where + "%') or (Name like '%" + where + "%') or (spell_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (spell_code like '" + where + "%')";
                }
            }
            else if (Spell == 1)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '%" + where + "%') or (Name like '%" + where + "%') or (input_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (input_code like '" + where + "%')";
                }
            }
            else if (Spell == 2)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '%" + where + "%') or (Name like '%" + where + "%') or (wb_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (wb_code like '" + where + "%')";
                }
            }
            else if (Spell == 3)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(Name like '%" + where + "%') or (wb_code like '%" + where + "%') or (input_code like '%" + where + "%') or (spell_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(Name like '" + where + "%') or (wb_code like '" + where + "%') or (input_code like '" + where + "%') or (spell_code like '" + where + "%')";
                }
            }
            else if (Spell == 4)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '%" + where + "%') or (wb_code like '%" + where + "%') or (input_code like '%" + where + "%') or (spell_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (wb_code like '" + where + "%') or (input_code like '" + where + "%') or (spell_code like '" + where + "%')";
                }
            }
            else
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '%" + where + "%') or (Name like '%" + where + "%') or (wb_code like '%" + where + "%') or (input_code like '%" + where + "%') or (spell_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (wb_code like '" + where + "%') or (input_code like '" + where + "%') or (spell_code like '" + where + "%')";
                }
            }

            base.Items.Clear();
            for (int i = 0; i < _dv.Count; i++)
            {
                DataRowView _row = _dv[i];
                base.Items.Add(_row["ID"].ToString() + ". " + _row["Name"].ToString());
            }
            if (base.Items.Count > 0)
                base.SelectedIndex = 0;

            return 1;
        }

        /// <summary>
        /// ������Ŀ
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Filter1(string where)
        {
            //where = FS.NFC.Public.String.TakeOffSpecialChar(where);
            where = where.Replace("'", "''");
            DataView _dv = new DataView(dsItems.Tables["items"]);
            if (Spell == 0)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '%" + where + "%') or (Name like '%" + where + "%') or (spell_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (spell_code like '" + where + "%')";
                }
            }
            else if (Spell == 1)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '%" + where + "%') or (Name like '%" + where + "%') or (input_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (input_code like '" + where + "%')";
                }
            }
            else if (Spell == 2)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '%" + where + "%') or (Name like '%" + where + "%') or (wb_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (wb_code like '" + where + "%')";
                }
            }
            else if (Spell == 3)
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(Name like '%" + where + "%') or (wb_code like '%" + where + "%') or (input_code like '%" + where + "%') or (spell_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(Name like '" + where + "%') or (wb_code like '" + where + "%') or (input_code like '" + where + "%') or (spell_code like '" + where + "%')";
                }
            }
            else
            {
                if (omitFilter)
                {
                    _dv.RowFilter = "(ID like '%" + where + "%') or (Name like '%" + where + "%') or (wb_code like '%" + where + "%') or (input_code like '%" + where + "%') or (spell_code like '%" + where + "%')";
                }
                else
                {
                    _dv.RowFilter = "(ID like '" + where + "%') or (Name like '" + where + "%') or (wb_code like '" + where + "%') or (input_code like '" + where + "%') or (spell_code like '" + where + "%')";
                }
            }

            base.Items.Clear();
            for (int i = 0; i < _dv.Count; i++)
            {
                DataRowView _row = _dv[i];
                base.Items.Add(_row["ID"].ToString() + ". " + _row["Name"].ToString());
            }
            if (base.Items.Count > 0)
            {
                //if (where == "")
                //{
                //    if (this.selectNone)
                //    {
                //        //base.SelectedIndex = -1;

                //        return 1;
                //    }
                //}
                base.SelectedIndex = 0;
            }

            if (base.Items.Count == 1)
            {
                if (AutoSelectItemEvent != null)
                {
                    AutoSelectItemEvent();
                }
            }

            return 1;
        }


        /// <summary>
        /// �ƶ���һ��
        /// </summary>
        /// <returns></returns>
        public int NextRow()
        {
            int index = base.SelectedIndex;
            if (index >= base.Items.Count - 1) return 1;

            base.SelectedIndex = index + 1;
            return 1;
        }
        /// <summary>
        /// �ƶ���һ��
        /// </summary>
        /// <returns></returns>
        public int PriorRow()
        {
            int index = base.SelectedIndex;
            if (index <= 0) return 1;

            base.SelectedIndex = index - 1;
            return 1;
        }
        /// <summary>
        /// �л����뷨
        /// </summary>
        /// <returns></returns>
        public int SetInputMode()
        {
            Spell++;
            if (Spell > 2) Spell = 0;
            return 1;
        }
        /// <summary>
        /// ���ѡ����
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public   int   GetSelectedItem(out FS.FrameWork.Models.NeuObject item)
        {
            int index = base.SelectedIndex;
            if (index < 0 || index > base.Items.Count - 1)
            {
                item = new FS.FrameWork.Models.NeuObject();
                return -1;
            }

            //���ID
            string itemname = base.SelectedItem.ToString();
            string ID = itemname.Substring(0, itemname.IndexOf(". ", 0));
            for (int i = 0; i < alItems.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = (FS.FrameWork.Models.NeuObject)alItems[i];
                if (obj.ID == ID)
                {
                    item = obj.Clone();
                    return 1;
                }
            }
            item = new FS.FrameWork.Models.NeuObject();
            return -1;
        }

        private void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SelectItem != null)
                    SelectItem(Keys.Enter);
                if (SelectedItem != null)
                    SelectedItem(this.GetSelectedItem(), e);
            }
        }

        private void ListBox_Click(object sender, EventArgs e)
        {
            if (SelectItem != null)
                SelectItem(Keys.Enter);
        }

        #region IFpInputable ��Ա
        /// <summary>
        /// ����
        /// </summary>
        public void MoveNext()
        {
            // TODO:  ��� PopUpListBox.MoveNext ʵ��
            this.NextRow();
        }
        /// <summary>
        /// ����
        /// </summary>
        public void MovePrevious()
        {
            // TODO:  ��� PopUpListBox.MovePrevious ʵ��
            this.PriorRow();
        }
        /// <summary>
        /// ��ҳ
        /// </summary>
        public void NextPage()
        {
            // TODO:  ��� PopUpListBox.NextPage ʵ��

        }
        /// <summary>
        /// ��ҳ
        /// </summary>
        public void PreviousPage()
        {
            // TODO:  ��� PopUpListBox.PreviousPage ʵ��
        }
        /// <summary>
        /// ��õ�ǰ��
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public object GetRow(int row)
        {
            // TODO:  ��� PopUpListBox.GetRow ʵ��
            if (row >= this.alItems.Count)
                return null;
            else
                return this.alItems[row];
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="filter"></param>
        void FS.FrameWork.WinForms.Controls.IFpInputable.Filter(string filter)
        {
            // TODO:  ��� PopUpListBox.FS.NFC.Interface.Controls.IFpInputable.Filter ʵ��
            this.Filter(filter);
        }
        /// <summary>
        /// �仯���뷨
        /// </summary>
        public void ChangeInput()
        {
            // TODO:  ��� PopUpListBox.ChangeInput ʵ��
            this.SetInputMode();
        }
        /// <summary>
        /// �����Ŀ
        /// </summary>
        public object GetSelectedItem()
        {
            // TODO:  ��� PopUpListBox.ChangeInput ʵ��
            FS.FrameWork.Models.NeuObject obj = null;
            this.GetSelectedItem(out obj);
            return obj;
        }
        #endregion
    }
}
