using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucQuery : UserControl
    {
        public ucQuery()
        {
            InitializeComponent();
        }

        #region  ȫ�ֱ���
        #region �ַ�������ĺ���
        //		INT 
        //		BOOL  
        //		DATETIME ����
        //		DEPARTMENT ����
        //		MARI   ����
        //		OPERATOR  ����Ա
        //           SEX �Ա�
        #endregion
        private int iRowBlank = 5;
        /// <summary>
        /// �հ׿��
        /// </summary>
        protected int iBlankWidth = 10;
        /// <summary>
        /// ��ǰ��
        /// </summary>
        protected int iCurrentRow = 0;
        protected System.Windows.Forms.Button btnDefault;
        protected enuDirection myDirection = enuDirection.H;
        /// <summary>
        /// �����ѯ����
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.QueryCondition managerCondition = new FS.HISFC.BizLogic.Manager.QueryCondition();
        protected FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        #endregion

        #region ��ʼ��
        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void ucQuery_Load(object sender, System.EventArgs e)
        {
            try
            {
                //��ʼ�� ������
                this.initOperations();
                //��ʼ����������
                this.initRelations();
                // ˢ���б�
                this.RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOK;
        private FS.FrameWork.WinForms.Controls.NeuPanel panel1;
        protected int iTab = 10;
        protected ArrayList alOperations = null;
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        protected void initOperations()
        {
            this.alOperations = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "=";
            obj.Name = "����";
            obj.Memo = "dy";

            this.alOperations.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = ">";
            obj.Name = "����";
            obj.Memo = "dy";
            this.alOperations.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "<";
            obj.Name = "С��";
            obj.Memo = "xy";
            this.alOperations.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "like";
            obj.Name = "����";
            obj.Memo = "dy";
            this.alOperations.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "<=";
            obj.Name = "С�ڵ���";
            obj.Memo = "xy";
            this.alOperations.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = ">=";
            obj.Name = "���ڵ���";
            obj.Memo = "xy";
            this.alOperations.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "<>";
            obj.Name = "������";
            obj.Memo = "bdy";
            this.alOperations.Add(obj);
        }
        protected ArrayList alRelations = null;
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        protected void initRelations()
        {
            this.alRelations = new ArrayList();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "";
            obj.Name = "��";
            obj.Memo = "w";
            this.alRelations.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "AND";
            obj.Name = "��";
            obj.Memo = "h";
            this.alRelations.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "OR";
            obj.Name = "��";
            obj.Memo = "h";
            this.alRelations.Add(obj);
        }
        #endregion

        #region IQuery ��Ա
        /// <summary>
        /// ��ǰ�ؼ�����
        /// </summary>
        public enuDirection Direction
        {
            get
            {
                return this.myDirection;
            }
            set
            {
                this.myDirection = value;
            }
        }
        private int iConditionWidth = 150;
        /// <summary>
        /// �������� 
        /// </summary>
        public int ConditionWidth
        {
            get
            {
                // TODO:  ��� ucQuery.ConditionWidth getter ʵ��
                return this.iConditionWidth;
            }
            set
            {
                // TODO:  ��� ucQuery.ConditionWidth setter ʵ��
                this.iConditionWidth = value;
                this.RefreshList();
            }
        }
        private int iValueWidth = 150;
        /// <summary>
        /// ��ֵ����
        /// </summary>
        public int ValueWidth
        {
            get
            {
                // TODO:  ��� ucQuery.ValueWidth getter ʵ��
                return this.iValueWidth;
            }
            set
            {
                // TODO:  ��� ucQuery.ValueWidth setter ʵ��
                this.iValueWidth = value;
                this.RefreshList();
            }
        }

        /// <summary>
        /// �п��
        /// </summary>
        public int RowBlank
        {
            get
            {
                // TODO:  ��� ucQuery.RowBlank getter ʵ��
                return this.iRowBlank;
            }
            set
            {
                // TODO:  ��� ucQuery.RowBlank setter ʵ��
                this.iRowBlank = value;
                this.RefreshList();
            }
        }
        /// <summary>
        /// ȷ����ť
        /// </summary>
        public System.Windows.Forms.Button ButtonOK
        {
            get
            {
                return this.btnOK;
            }
            set
            {
                this.btnOK = value;
            }
        }
        /// <summary>
        /// �˳���ť
        /// </summary>
        public System.Windows.Forms.Button ButtonExit
        {
            get
            {
                return this.btnExit;
            }
            set
            {
                this.btnExit = value;
            }
        }
        /// <summary>
        /// �ָ���ť
        /// </summary>
        public System.Windows.Forms.Button ButtonReset
        {
            get
            {
                return this.btnReset;
            }
            set
            {
                this.btnReset = value;
            }
        }
        /// <summary>
        /// Ĭ�ϰ�ť
        /// </summary>
        public System.Windows.Forms.Button ButtonDefault
        {
            get
            {
                return this.btnDefault;
            }
            set
            {
                this.btnDefault = value;
            }
        }

        protected ArrayList alConditions = null;
        /// <summary>
        /// ��ʼ������--����һ
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int InitCondition(ArrayList conditions)
        {
            // TODO:  ��� ucQuery.InitCondition ʵ��
            if (conditions.Count <= 0)
            {
                FS.FrameWork.Models.NeuObject objTemp = new FS.FrameWork.Models.NeuObject();
                conditions.Add(objTemp);
            }

            FS.FrameWork.Models.NeuObject o = conditions[0] as FS.FrameWork.Models.NeuObject;
            if (o == null)
            {
                MessageBox.Show("�������������̳���neuObject.");
                return -1;
            }
            alConditions = conditions;
            return this.RefreshList();
        }
        /// <summary>
        /// ��ʼ������-dataset
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int InitCondition(DataSet ds)
        {
            return 0;
        }

        /// <summary>
        /// ��ʼ������ - sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int InitCondition(string sql)
        {
            return 0;
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <returns></returns>
        public int RefreshList()
        {
            if (this.alConditions == null || this.alConditions.Count <= 0)
            {
                this.alConditions = new ArrayList();
                FS.FrameWork.Models.NeuObject objTemp = new FS.FrameWork.Models.NeuObject();
                objTemp.ID = "";
                objTemp.Name = "����";
                alConditions.Add(objTemp);
            }
            iCurrentRow = -1;
            this.ClearAll();
            AddNewRow();

            //this.ReadCondition();
            return 0;
        }
        /// <summary>
        /// ɾ��ȫ��
        /// </summary>
        public void ClearAll()
        {
            for (int i = this.panel1.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.panel1.Controls[i];
                try
                {
                    if (c.GetType() != typeof(Button))
                    {
                        this.panel1.Controls.RemoveAt(i);
                        c.Dispose();
                    }
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// ���һ��
        /// </summary>
        /// <param name="iRow"></param>
        public void ClearRow(int iRow)
        {
            if (iRow < 0) return;
            for (int i = this.panel1.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.panel1.Controls[i];
                try
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(c.Name.Substring(c.Name.Length - 2)) == iRow
                        && c.GetType() != typeof(Button))
                    {
                        this.panel1.Controls.RemoveAt(i);
                        c.Dispose();
                    }
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iExceptType"></param>
        public void ClearRow(int iRow, int iExceptType)
        {
            if (iRow < 0) return;
            for (int i = this.panel1.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.panel1.Controls[i];
                try
                {
                    if (iExceptType == 0)
                    {
                        if (FS.FrameWork.Function.NConvert.ToInt32(c.Name.Substring(c.Name.Length - 2)) == iRow
                            && (c.Name.Substring(0, 5) == "opera" || c.Name.Substring(0, 5) == "value") && c.GetType() != typeof(Button))
                        {
                            this.panel1.Controls.RemoveAt(i);
                            c.Dispose();
                        }
                    }
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// ����һ����
        /// </summary>
        public void AddNewRow()
        {
            iCurrentRow++;
            this.insertConditionBox();
            this.insertRelationBox();
        }
        public void AddNewRow(int iRow)
        {
            iCurrentRow = iRow;
            this.insertConditionBox();
            this.insertRelationBox();
        }
        /// <summary>
        /// �����ֶΣ� �� סԺ�ţ�����
        /// </summary>
        protected void insertConditionBox()
        {
            foreach (Control c in this.panel1.Controls)
            {
                if (c.Name == "condition" + iCurrentRow.ToString().PadLeft(2, '0'))
                {
                    return;
                }
            }
            FS.FrameWork.WinForms.Controls.NeuComboBox combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
            combox.Width = this.iConditionWidth;
            if (this.myDirection == enuDirection.H)
            {
                combox.Location = new Point(iBlankWidth, iCurrentRow * 21 + (iCurrentRow + 1) * iRowBlank);
            }
            else
            {
                combox.Location = new Point(iBlankWidth, (iCurrentRow) * 21 + (iCurrentRow + 1) * (iRowBlank * 3));
                combox.ForeColor = Color.Blue;
            }
            //combox.IsShowCustomerList = true;
            combox.Visible = true;
            combox.TabIndex = iCurrentRow * 10 + 1;
            combox.Name = "condition" + iCurrentRow.ToString().PadLeft(2, '0');
            combox.AddItems(this.alConditions);
            combox.SelectedIndexChanged += new EventHandler(combox_SelectedIndexChanged);
            combox.KeyPress += new KeyPressEventHandler(combox_KeyPress);
            combox.SelectedIndex = 0;

            //{BFE65293-F4D0-47a6-8760-3CC8B229F208}  ���Ʋ������û��ֹ������ѯ���� ֻ�����б�ѡ��
            combox.DropDownStyle = ComboBoxStyle.DropDownList;

            this.panel1.Controls.Add(combox);
        }
        protected void insertOperationBox(string type)
        {
            if (this.alOperations == null) this.initOperations();
            if (this.alOperations == null)
            {
                MessageBox.Show("��ȡ������ʧ��");
                return;
            }
            FS.FrameWork.WinForms.Controls.NeuComboBox combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
            combox.Width = 50;
            if (this.myDirection == enuDirection.H)
            {
                combox.Location = new Point(iBlankWidth * 2 + this.iConditionWidth, iCurrentRow * 21 + (iCurrentRow + 1) * iRowBlank);
            }
            else
            {
                combox.Location = new Point(iBlankWidth * 2 + this.iConditionWidth, (iCurrentRow) * 21 + (iCurrentRow + 1) * (iRowBlank * 3));
                combox.ForeColor = Color.Blue;
            }
            //combox.IsShowCustomerList = true;
            combox.Visible = true;
            combox.TabIndex = iCurrentRow * 10 + 2;
            combox.Name = "operation" + iCurrentRow.ToString().PadLeft(2, '0');
            combox.KeyPress += new KeyPressEventHandler(combox_KeyPress);
            combox.IsListOnly = true;
            try
            {
                ArrayList al = this.alOperations.Clone() as ArrayList;
                if (type == "INT")
                {
                    al.RemoveAt(3);//ȥ������
                }
                else if (type == "BOOL")
                {
                    al = new ArrayList();
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = "=";
                    obj.Name = "����";
                    obj.Memo = "dy";
                    al.Add(obj);
                }
                else if (type == "DATETIME")
                {
                    al.RemoveAt(3);//ȥ������
                }

                else//�ַ���
                {
                    //					al = new ArrayList();
                    //					FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    //					obj.ID = "=";
                    //					obj.Name = "����";
                    //					obj.Memo = "dy";
                    //					al.Add(obj);
                    //
                    //					obj = new FS.FrameWork.Models.NeuObject();
                    //					obj.ID = "like";
                    //					obj.Name = "����";
                    //					obj.Memo = "bh";
                    //					al.Add(obj);
                    //
                    //					obj = new FS.FrameWork.Models.NeuObject();
                    //					obj.ID = "<>";
                    //					obj.Name = "������";
                    //					obj.Memo = "bdy";
                    //					al.Add(obj);
                }
                combox.AddItems(al);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //�ж������б����ѡ��
            if (combox.Items.Count > 0)
            {
                combox.SelectedIndex = 0;
            }
            this.panel1.Controls.Add(combox);
        }
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// ������������ͣ���ʾ��ͬ������ؼ�
        /// </summary>
        /// <param name="type"></param>
        /// <param name="defaultValue"></param>
        protected void insertValueBox(string type, string defaultValue, string originalType)
        {
            //FS.FrameWork.WinForms.Controls.NeuComboBox combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
            Control combox = null;
            if (type == "INT")
            {
                combox = new FS.FrameWork.WinForms.Controls.ValidatedTextBox1();
                ((FS.FrameWork.WinForms.Controls.ValidatedTextBox1)combox).TextFormatType = (FS.FrameWork.WinForms.Controls.ValidatedTextBox1.TextFormatTypes)1;
            }
            else if (type == "BOOL")
            {
                combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
                ArrayList al = new ArrayList();
                FS.FrameWork.Models.NeuObject objtrue = new FS.FrameWork.Models.NeuObject();
                objtrue.ID = "1";
                objtrue.Name = "��";
                objtrue.Memo = "true";
                al.Add(objtrue);

                FS.FrameWork.Models.NeuObject objfalse = new FS.FrameWork.Models.NeuObject();
                objfalse.ID = "0";
                objfalse.Name = "��";
                objfalse.Memo = "false";
                al.Add(objfalse);
                ((FS.FrameWork.WinForms.Controls.NeuComboBox)combox).AddItems(al);
            }
            else if (type == "DATETIME")
            {
                combox = new DateTimePicker();
                ((DateTimePicker)combox).CustomFormat = "yyyy-MM-dd HH:mm";
                ((DateTimePicker)combox).Format = DateTimePickerFormat.Custom;
            }
            else if (type == "DEPARTMENT")//����
            {
                combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
                ((FS.FrameWork.WinForms.Controls.NeuComboBox)combox).AddItems(deptManager.GetDeptmentAll());
            }
            else if (type == "SEX")//�Ա�
            {
                combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
                ((FS.FrameWork.WinForms.Controls.NeuComboBox)combox).AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            }
            else if (type == "MARI")//����״��
            {
                combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
                ((FS.FrameWork.WinForms.Controls.NeuComboBox)combox).AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());
            }
            else if (type == "OPERATOR")//��Ա
            {
                combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
                ((FS.FrameWork.WinForms.Controls.NeuComboBox)combox).AddItems(personManager.GetEmployeeAll());
            }
            else if (type == "STRING")//�ַ���
            {
                combox = new TextBox();
            }
            else if (!type.Contains(","))
            {
                combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
                ((FS.FrameWork.WinForms.Controls.NeuComboBox)combox).AddItems(con.GetList(originalType));
            }
            else//�����û�����
            {
                combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
                ArrayList alList = new ArrayList();
                string[] s = type.Split(',');
                try
                {
                    for (int iList = 0; iList < s.Length; iList++)
                    {
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        obj.ID = s[iList].Split(' ')[0];
                        obj.Name = s[iList].Split(' ')[1];
                        alList.Add(obj);
                    }
                }
                catch { }
                ((FS.FrameWork.WinForms.Controls.NeuComboBox)combox).AddItems(alList);
            }

            if (this.myDirection == enuDirection.H)
            {
                combox.Location = new Point(iBlankWidth * 3 + 50 + iConditionWidth, iCurrentRow * 21 + (iCurrentRow + 1) * iRowBlank);
            }
            else
            {
                combox.Location = new Point(iBlankWidth + 10, iRowBlank * 2 + iCurrentRow * 21 + (iCurrentRow + 1) * (iRowBlank * 3));
                combox.ForeColor = Color.Red;
            }
            try
            {
                //((FS.FrameWork.WinForms.Controls.NeuComboBox)combox).IsShowCustomerList = true;
            }
            catch { }
            combox.TabIndex = iCurrentRow * 10 + 3;
            combox.Width = this.ValueWidth;
            //combox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right |System.Windows.Forms.AnchorStyles.Left )));
            combox.KeyPress += new KeyPressEventHandler(combox_KeyPress);

            combox.Visible = true;
            combox.Name = "value" + iCurrentRow.ToString().PadLeft(2, '0');

            this.panel1.Controls.Add(combox);
        }
        /// <summary>
        /// ��ϵ  ���� ���� ������
        /// </summary>
        protected void insertRelationBox()
        {
            if (this.alRelations == null) this.initRelations();
            FS.FrameWork.WinForms.Controls.NeuComboBox combox = new FS.FrameWork.WinForms.Controls.NeuComboBox();
            combox.Width = 50;
            //combox.Location = new Point(this.ButtonOK.Left - iBlankWidth -50,iCurrentRow * 21+(iCurrentRow+1)*iRowBlank +10);
            //combox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            if (this.myDirection == enuDirection.H)
            {
                combox.Location = new Point(iBlankWidth * 4 + 50 + this.iConditionWidth + this.iValueWidth, iCurrentRow * 21 + (iCurrentRow + 1) * iRowBlank);
            }
            else
            {
                combox.Location = new Point(iBlankWidth * 2 + 10 + this.iValueWidth, iRowBlank * 2 + iCurrentRow * 21 + (iCurrentRow + 1) * (iRowBlank * 3));
            }
            //combox.IsShowCustomerList = true;
            combox.Visible = true;
            combox.TabIndex = iCurrentRow * 10 + 4;
            combox.Name = "relation" + iCurrentRow.ToString().PadLeft(2, '0');
            combox.AddItems(this.alRelations);
            combox.SelectedIndex = 0;
            combox.IsListOnly = true;
            combox.SelectedIndexChanged += new EventHandler(comboxRelation_SelectedIndexChanged);
            combox.KeyPress += new KeyPressEventHandler(combox_KeyPress);

            this.panel1.Controls.Add(combox);
        }

        #endregion

        #region �仯
        /// <summary>
        /// ��ѯ���� סԺ�� �����Ա�ȱ仯ʱ�������¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = ((FS.FrameWork.WinForms.Controls.NeuComboBox)sender).SelectedItem.Memo;
            //�������Ϊ�� �� Ĭ��Ϊ �ַ���  
            if (type == "") type = "STRING";
            string originalType = type;
            type = type.ToUpper();
            //��ȡ��ǰ���� 
            this.iCurrentRow = this.getControlRow(sender);
            //ɾ����ǰ��
            this.ClearRow(iCurrentRow, 0);
            //�����µĲ����� ѡ��� 
            this.insertOperationBox(type);
            //�����µ�ֵѡ��� 
            this.insertValueBox(type, "", originalType);

        }
        /// <summary>
        /// ����ϵ����    �ޣ��ͣ��� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void comboxRelation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((FS.FrameWork.WinForms.Controls.NeuComboBox)sender).Tag.ToString().Trim() == "")
            {
                //�������Ϊ ���ޡ� ��ʱ�� ɾ����ǰ�����µ�������
                for (int i = this.getControlRow(sender) + 1; i < 20; i++)
                {
                    this.ClearRow(i);
                }
            }
            else
            {
                this.AddNewRow(this.getControlRow(sender) + 1);
                //System.Windows.Forms.SendKeys.Send("{tab}");
            }
        }
        /// <summary>
        /// ��ÿؼ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        protected int getControlRow(object sender)
        {
            return FS.FrameWork.Function.NConvert.ToInt32(((Control)sender).Name.Substring(((Control)sender).Name.Length - 2));
        }

        protected void btnExit_Click(object sender, System.EventArgs e)
        {
            try
            {
                CancelEvent(sender, null);
            }
            catch
            {
                //���û���ҵ������¼����رյ�ǰ����
                this.FindForm().Close();
            }
        }

        /// <summary>
        /// ��ò�ѯ����
        /// </summary>
        /// <returns></returns>
        public string GetWhereString()
        {
            string strWhere = " ";
            for (int i = 0; i < 20; i++)
            {
                string sCondition = "", sOperation = "", sValue = "", sRelation = "";
                string sType = "";
                for (int j = 0; j < 4; j++)
                {
                    Control c = this.getControl(j, i) as Control;
                    if (c == null)
                    {
                        //this.SaveCondtion();
                        return strWhere;
                    }

                    switch (j)
                    {
                        case 0:
                            sType = ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).SelectedItem.Memo;
                            sCondition = ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).SelectedItem.ID;
                            break;
                        case 1:
                            sOperation = ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).SelectedItem.ID;
                            break;
                        case 2:
                            try
                            {
                                if (c.GetType() == typeof(DateTimePicker))
                                {
                                    sValue = ((DateTimePicker)c).Value.ToString();
                                }
                                else if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox)) //&&(sType =="MARI" ||sType =="SEX"))
                                {
                                    sValue = ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).SelectedItem.ID;
                                }
                                else
                                {
                                    sValue = c.Text;
                                }
                            }
                            catch { }
                            break;
                        case 3:
                            try
                            {
                                sRelation = ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).SelectedItem.ID;
                            }
                            catch { }
                            break;
                        default:
                            break;
                    }
                }
                string stemp = "";
                //�������
                if (sType == "INT")
                {
                    if (sValue.Trim() == "") sValue = "0";
                    stemp = " {0} {1} {2} {3}";
                }
                else if (sType == "BOOL")
                {
                    stemp = " {0} {1} '{2}' {3}";
                }
                else if (sType == "DATETIME")
                {
                    stemp = " {0} {1} to_date('{2}','yyyy-mm-dd HH24:mi:ss') {3}";
                }
                else//�ַ���
                {
                    stemp = " {0} {1} '{2}' {3}";
                }
                if (sOperation.ToUpper() == "LIKE")
                {
                    if (sValue.Trim() == "")
                        sValue = "%";
                    else
                        sValue = "%" + sValue + "%";
                }

                FS.FrameWork.Public.String.FormatString(stemp, out stemp, sCondition, sOperation, sValue, sRelation);
                strWhere += stemp;
            }
            //this.SaveCondtion();
            return strWhere;
        }
        /// <summary>
        /// ��ÿؼ�
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iRow"></param>
        /// <returns></returns>
        protected object getControl(int iType, int iRow)
        {
            for (int i = this.panel1.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.panel1.Controls[i];
                try
                {
                    string s = "condi";
                    if (iType == 1)
                    {
                        s = "opera";
                    }
                    else if (iType == 2)
                    {
                        s = "value";
                    }
                    else if (iType == 3)
                    {
                        s = "relat";
                    }
                    else
                    {
                        s = "condi";
                    }
                    if (c.Name.Substring(0, 5) == s && FS.FrameWork.Function.NConvert.ToInt32(c.Name.Substring(c.Name.Length - 2)) == iRow)
                    {
                        return c;
                    }

                }
                catch
                {
                }
            }
            return null;
        }

        protected void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.OKEvent(this.GetWhereString(), null);
            }
            catch { }
        }

        public void btnReset_Click(object sender, System.EventArgs e)
        {
            this.RefreshList();
        }
        #endregion

        private void combox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                System.Windows.Forms.SendKeys.Send("{tab}");
                e.Handled = true;
            }
        }

        #region ������������
        /// <summary>
        /// ��ȡ����
        /// </summary>
        public void ReadCondition()
        {

            string s = "";
            if (this.FindForm() == null) return;
            try
            {
                s = this.managerCondition.GetQueryCondtion(this.FindForm().Name);
            }
            catch { return; }
            if (s == "-1")
            {
                MessageBox.Show(this.managerCondition.Err);
                return;
            }
            if (s == "")
                s = this.managerCondition.GetQueryCondtion(this.FindForm().Name, true);

            if (s == "") return;

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            XmlNodeList nodes = doc.SelectNodes("setting/row");

            this.ResetText();
            int iRow = 0;
            foreach (XmlNode node in nodes)
            {
                try
                {
                    ((Control)this.getControl(0, iRow)).Text = node.ChildNodes[0].InnerText;
                    ((Control)this.getControl(1, iRow)).Text = node.ChildNodes[1].InnerText;
                    ((Control)this.getControl(2, iRow)).Text = node.ChildNodes[2].InnerText;
                    ((Control)this.getControl(3, iRow)).Text = node.ChildNodes[3].InnerText;
                }
                catch { }
                iRow++;
            }

        }
        /// <summary>
        /// ��������
        /// </summary>
        public void SaveCondtion()
        {
            string s = this._SaveCondition();
            if (this.managerCondition.SetQueryCondition(this.FindForm().Name, s) == -1)
            {
                MessageBox.Show(managerCondition.Err);
            }
        }

        protected string _SaveCondition()
        {
            FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
            XmlDocument doc = new XmlDocument();
            XmlElement eRoot = myXml.CreateRootElement(doc, "setting");
            for (int i = 0; i < 20; i++)
            {
                XmlElement eRow = null;
                eRow = myXml.AddXmlNode(doc, eRoot, "row", "");
                for (int j = 0; j < 4; j++)
                {
                    Control c = this.getControl(j, i) as Control;
                    if (c == null) break;
                    myXml.AddXmlNode(doc, eRow, "column", c.Text);
                }
            }
            return doc.OuterXml;
        }
        /// <summary>
        /// ����Ĭ������
        /// </summary>
        public void SaveDefaultCondition()
        {
            string s = this._SaveCondition();
            if (this.managerCondition.SetQueryCondition(this.FindForm().Name, s, true) == -1)
            {
                MessageBox.Show(managerCondition.Err);
            }
            else
            {
                MessageBox.Show("����ģ�屣��ɹ���");
            }
        }

        private void btnDefault_Click(object sender, System.EventArgs e)
        {
            string s = this.managerCondition.GetQueryCondtion(this.FindForm().Name, true);

            if (s == "") return;

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            XmlNodeList nodes = doc.SelectNodes("setting/row");

            this.ResetText();
            int iRow = 0;
            foreach (XmlNode node in nodes)
            {
                try
                {
                    ((Control)this.getControl(0, iRow)).Text = node.ChildNodes[0].InnerText;
                    ((Control)this.getControl(1, iRow)).Text = node.ChildNodes[1].InnerText;
                    ((Control)this.getControl(2, iRow)).Text = node.ChildNodes[2].InnerText;
                    ((Control)this.getControl(3, iRow)).Text = node.ChildNodes[3].InnerText;
                }
                catch { }
                iRow++;
            }

        }
        #endregion

        #region �ʵ�
        //private int iLoop = 0;
        private void panel1_DoubleClick(object sender, System.EventArgs e)
        {
            //if (iLoop > 10)
            //{
            this.SaveDefaultCondition();
            //}
            //iLoop++;
        }
        #endregion
    }
    /// <summary>
    /// ����
    /// </summary>
    public enum enuDirection
    {
        /// <summary>
        /// ��
        /// </summary>
        H = 0,
        /// <summary>
        /// ��
        /// </summary>
        V = 1
    }
}
