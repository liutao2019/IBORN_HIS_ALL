using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Manager.Controls
{
    public partial class ucPrivManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrivManager()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ϵͳȨ���б�
        /// </summary>
        private ArrayList alClass3Meaning = new ArrayList();

        /// <summary>
        /// ����Ȩ�޹�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PowerLevelManager class3Manager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
      
        /// <summary>
        /// ����Ȩ��ʵ����
        /// </summary>
        private FS.HISFC.Models.Admin.PowerLevelClass2 operClass2Priv = new FS.HISFC.Models.Admin.PowerLevelClass2();
        
        /// <summary>
        /// ����Ȩ�޹�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PowerLevel2Manager class2Manager = new FS.HISFC.BizLogic.Manager.PowerLevel2Manager();

        /// <summary>
        /// ����Ȩ�޹���
        /// </summary>
        private System.Collections.Hashtable hsClass3JoinCode = new Hashtable();

        /// <summary>
        /// �Ƿ�����޸�ϵͳȨ�������Ȩ��
        /// </summary>
        private bool isEditSysData = false;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ����ά��ϵͳȨ������
        /// </summary>
        public bool IsManagerEdit
        {
            get
            {
                return this.isEditSysData;
            }
            set
            {
                this.isEditSysData = value;
                this.gbClass2.Enabled = value;
                this.gbClass3Meaning.Enabled = value;
            }
        }

        #endregion

        /// <summary>
        /// ���ݴ���Ĳ�������ʾ��ά����һ��Ȩ��
        /// </summary>
        /// <param name="parm"></param>
        public void ShowClass1(string parm)
        {
            //��սڵ�
            this.tvClass1.Nodes.Clear();

            //ȡ��ά����һ��Ȩ�ޣ���ʾ�����Ϳؼ��ĸ��ڵ㡣
            FS.HISFC.BizLogic.Manager.PowerLevel1Manager class1Manager = new FS.HISFC.BizLogic.Manager.PowerLevel1Manager();
            ArrayList al = class1Manager.LoadLevel1Available(parm);
            if (al == null)
            {
                MessageBox.Show(Language.Msg(class1Manager.Err));
                return;
            }

            this.tvClass1.ImageList = this.tvClass1.groupImageList;

            //���һ��Ȩ�޽ڵ�
            FS.HISFC.Models.Admin.PowerLevelClass2 class2 = null;

            foreach (FS.HISFC.Models.Admin.PowerLevelClass1 info in al)
            {
                TreeNode node = this.tvClass1.Nodes.Add(info.Name);

                node.Text = info.Class1Name;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 4;
                //��һ��Ȩ�޵���Ϣת��Ϊ����Ȩ��ʵ��,�������ڽڵ��Tag��
                class2 = new FS.HISFC.Models.Admin.PowerLevelClass2();
                class2.Class1Code = info.Class1Code;

                node.Tag = class2;
            }
        }

        /// <summary>
        /// ��ʾ����Ȩ��
        /// </summary>
        public void ShowClass2()
        {
            //����
            this.Clear();

            //ȡ����Ȩ��
            ArrayList al = this.class2Manager.LoadLevel2All(this.operClass2Priv.Class1Code);
            if (al == null)
            {
                MessageBox.Show(Language.Msg(class2Manager.Err));
                return;
            }
            //������Ȩ����ʾ���б���
            this.fpClass2_Sheet1.RowCount = al.Count;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Admin.PowerLevelClass2 info = al[i] as FS.HISFC.Models.Admin.PowerLevelClass2;

                this.fpClass2_Sheet1.Cells[i, 0].Text = info.Class2Code;		    //����Ȩ�ޱ���	
                this.fpClass2_Sheet1.Cells[i, 1].Text = info.Class2Name;		    //����Ȩ������ 
                this.fpClass2_Sheet1.Cells[i, 2].Text = info.Flag;			        //������ 
                this.fpClass2_Sheet1.Cells[i, 3].Text = info.Memo;			        //��ע 
                this.fpClass2_Sheet1.Cells[i, 4].Text = info.Class1Code;		    //һ��Ȩ�ޱ��� 
                this.fpClass2_Sheet1.Rows[i].Tag = info;
            }

            if (al.Count > 0)
            {
                this.fpClass2_Sheet1.ActiveColumnIndex = 0;

                //ȡ��ǰ����Ȩ��
                this.GetClass2();
            }
        }

        /// <summary>
        /// ��ʾ����Ȩ��
        /// </summary>
        public void ShowClass3()
        {
            //{42AB2AC3-EAC6-4b7d-9102-4997B2E9AAAA} ���Ӷ���Ȩ�ޱ���
            if (this.operClass2Priv == null || string.IsNullOrEmpty( this.operClass2Priv.Class2Code))
            {
                this.fpClass3_Sheet1.RowCount = 0;

                return;
            }

            //ȡ����Ȩ��
            ArrayList al = class3Manager.LoadLevel3ByLevel2(this.operClass2Priv.Class2Code);
            if (al == null)
            {
                MessageBox.Show(Language.Msg(class2Manager.Err));
                return;
            }

            string[] class3JoinCollection = this.ShowClass3JoinPriv();

            if (class3JoinCollection != null)
            {
                //����ComboBoxCellType�����ϵͳ������
                FarPoint.Win.Spread.CellType.ComboBoxCellType combo = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                //��ComboBoxCellType��������Ȩ�޵Ķ�Ӧ��
                combo.Items = class3JoinCollection;
                this.fpClass3_Sheet1.Columns[5].CellType = combo;
            }
            else
            {
                //����ComboBoxCellType�����ϵͳ������
                FarPoint.Win.Spread.CellType.ComboBoxCellType combo = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                //��ComboBoxCellType��������Ȩ�޵Ķ�Ӧ��
                combo.Items = new string[1];
                this.fpClass3_Sheet1.Columns[5].CellType = combo;
            }

            //��ʾ����Ȩ��
            this.fpClass3_Sheet1.RowCount = al.Count;

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Admin.PowerLevelClass3 info = al[i] as FS.HISFC.Models.Admin.PowerLevelClass3;
                this.fpClass3_Sheet1.Cells[i, 0].Text = info.ID;
                this.fpClass3_Sheet1.Cells[i, 1].Text = info.Name;
                this.fpClass3_Sheet1.Cells[i, 2].Text = info.Class3MeaningCode;
                this.fpClass3_Sheet1.Cells[i, 3].Text = info.Class3MeaningName;

                this.fpClass3_Sheet1.Cells[i, 4].Text = info.Class3JoinCode;
                if (this.hsClass3JoinCode.ContainsKey(info.Class3JoinCode))
                {
                    this.fpClass3_Sheet1.Cells[i, 5].Text = this.hsClass3JoinCode[info.Class3JoinCode] as string;
                }

                this.fpClass3_Sheet1.Cells[i, 6].Text = info.Memo;
                this.fpClass3_Sheet1.Rows[i].Tag = info;
            }
        }

        /// <summary>
        /// ��ȡ�������������Ȩ�޼���
        /// </summary>
        protected string[] ShowClass3JoinPriv()
        {
            string class2JoinPrivCode = null;
            if (this.operClass2Priv.Class2Code.Substring(2) == "10" )
            {
                class2JoinPrivCode = "20";
            }
            else if (this.operClass2Priv.Class2Code.Substring(2) == "20")
            {
                class2JoinPrivCode = "10";
            }
            else
            {
                return null;
            }

            ArrayList alClass3Join = class3Manager.LoadLevel3ByLevel2(this.operClass2Priv.Class1Code + class2JoinPrivCode);
            if (alClass3Join == null)
            {
                return null;
            }

            string[] class3JoinName = new string[alClass3Join.Count];
            int i = 0;
            this.hsClass3JoinCode = new Hashtable();

            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 class3Join in alClass3Join)
            {
                class3JoinName[i] = class3Join.Class3Name;

                this.hsClass3JoinCode.Add(class3Join.Class3Code, class3Join.Class3Name);

                i++;
            }

            return class3JoinName;
        }

        /// <summary>
        /// ��ʾϵͳȨ��
        /// </summary>
        public void ShowClass3Meaning()
        {
            //{42AB2AC3-EAC6-4b7d-9102-4997B2E9AAAA} ���Ӷ���Ȩ�ޱ���
            //if (this.operClass2Priv == null)
            if (this.operClass2Priv == null || string.IsNullOrEmpty(this.operClass2Priv.Class2Code))
            {
                this.fpClass3Meaning_Sheet1.RowCount = 0;

                return;
            }

            //ȡϵͳȨ��
            alClass3Meaning = class3Manager.LoadLevel3Meaning(this.operClass2Priv.Class2Code);
            if (alClass3Meaning == null)
            {
                MessageBox.Show(Language.Msg(class3Manager.Err));
                return;
            }

            //ȡϵͳȨ������
            string[] items = new string[this.alClass3Meaning.Count];
            int index = 0;

            //��ʾϵͳȨ��
            this.fpClass3Meaning_Sheet1.RowCount = alClass3Meaning.Count;
            FS.FrameWork.Models.NeuObject info;
            for (int i = 0; i < alClass3Meaning.Count; i++)
            {
                info = alClass3Meaning[i] as FS.FrameWork.Models.NeuObject;
                this.fpClass3Meaning_Sheet1.Cells[i, 0].Text = info.ID;
                this.fpClass3Meaning_Sheet1.Cells[i, 1].Text = info.Name;
                this.fpClass3Meaning_Sheet1.Cells[i, 2].Text = info.Memo;
                this.fpClass3Meaning_Sheet1.Rows[i].Tag = info;

                items[index] = info.Name;
                index++;
            }

            //����˶���Ȩ����û��ϵͳȨ�ޣ���Ĭ�϶���Ȩ�����ƣ�����Ϊ01
            if (alClass3Meaning.Count == 0)
            {
                items = new string[1];
                items[0] = this.operClass2Priv.Class2Name;
            }

            //����ComboBoxCellType�����ϵͳ������
            FarPoint.Win.Spread.CellType.ComboBoxCellType combo = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            //��ComboBoxCellType��������Ȩ�޵Ķ�Ӧ��
            combo.Items = items;
            this.fpClass3_Sheet1.Columns[3].CellType = combo;
        }

        /// <summary>
        /// �������Ȩ��
        /// </summary>
        public void SaveClass2()
        {
            int parm;
            bool isUpdate = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            try
            {
                //����
                this.class2Manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //��ɾ������
                parm = this.class2Manager.DeleteLevel2(this.operClass2Priv.Class1Code);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("�������Ȩ��ʱɾ�������������� " + class2Manager.Err), "������ʾ");
                    return;
                }

                //���������Ч����������±�־
                if (parm > 0)
                {
                    isUpdate = true;
                }

                FS.HISFC.Models.Admin.PowerLevelClass2 info;
                //��������
                for (int i = 0; i < this.fpClass2_Sheet1.Rows.Count; i++)
                {
                    info = new FS.HISFC.Models.Admin.PowerLevelClass2();

                    info.Class2Code = this.fpClass2_Sheet1.Cells[i, 0].Text; //����Ȩ�ޱ���
                    info.Class2Name = this.fpClass2_Sheet1.Cells[i, 1].Text; //����Ȩ������
                    if (info.Class2Code == "" || info.Class2Name == "")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();;
                        MessageBox.Show(Language.Msg("����Ȩ�ޱ�������Ʋ���Ϊ��"), "��ʾ");
                        return;
                    }
                    
                    info.Class1Code = this.operClass2Priv.Class1Code;		    //һ��Ȩ�ޱ���
                    info.Flag = this.fpClass2_Sheet1.Cells[i, 2].Text;	    //������
                    info.Memo = this.fpClass2_Sheet1.Cells[i, 3].Text;        //��ע
                    this.fpClass2_Sheet1.Rows[i].Tag = info;	                //�ڵ�ǰ���б������Ȩ����Ϣ
                    parm = class2Manager.InsertLevel2(info);
                    if (parm != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();;
                        if (this.class2Manager.DBErrCode == 1)
                            MessageBox.Show(Language.Msg("����Ϊ��" + info.Class2Code + "���Ķ���Ȩ���Ѿ�����,�����ظ����."));
                        else
                            MessageBox.Show(Language.Msg(this.class3Manager.Err), "������ʾ");

                        return;
                    }
                }

                //���������Ч����������±�־
                if (this.fpClass2_Sheet1.Rows.Count > 0)
                {
                    isUpdate = true;
                }

                if (isUpdate)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();;
                    MessageBox.Show(Language.Msg("����ɹ�"), "��ʾ");
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(ex.Message, "������ʾ");
            }
        }

        /// <summary>
        /// ��������Ȩ��
        /// </summary>
        public void SaveClass3()
        {
            int parm;
            bool isUpdate = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            class3Manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //��ɾ������
            parm = class3Manager.Delete(this.operClass2Priv.Class2Code);
            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(Language.Msg(class3Manager.Err), "������ʾ");
                return;
            }

            //���������Ч����������±�־
            if (parm > 0)
            {
                isUpdate = true;
            }

            FS.HISFC.Models.Admin.PowerLevelClass3 info;
            //��������
            for (int i = 0; i < this.fpClass3_Sheet1.Rows.Count; i++)
            {
                info = new FS.HISFC.Models.Admin.PowerLevelClass3();

                info.Class3Code = this.fpClass3_Sheet1.Cells[i, 0].Text;          //����Ȩ�ޱ���
                info.Class3Name = this.fpClass3_Sheet1.Cells[i, 1].Text;          //����Ȩ������
                info.Class3MeaningCode = this.fpClass3_Sheet1.Cells[i, 2].Text;   //ϵͳȨ�ޱ���
                info.Class3MeaningName = this.fpClass3_Sheet1.Cells[i, 3].Text;   //ϵͳȨ������

                if (info.Class3Code == "" || info.Class3Name == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("����Ȩ�ޱ�������Ʋ���Ϊ��"), "��ʾ");
                    return;
                }

                if (info.Class3MeaningCode == "" || info.Class3MeaningName == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("��ѡ��ϵͳȨ��"), "��ʾ");
                    return;
                }

                info.Class3JoinCode = this.fpClass3_Sheet1.Cells[i, 4].Text;           //����Ȩ������               

                info.Memo = this.fpClass3_Sheet1.Cells[i, 6].Text; //��ע
                info.Class2Code = this.operClass2Priv.Class2Code;					   //����Ȩ�ޱ���
                parm = class3Manager.InsertPowerLevelClass3(info);
                if (parm != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    if (this.class3Manager.DBErrCode == 1)
                        MessageBox.Show(Language.Msg("����Ϊ��" + info.Class3Code + "��������Ȩ���Ѿ�����,�����ظ����."));
                    else
                        MessageBox.Show(Language.Msg(this.class3Manager.Err), "������ʾ");

                    return;
                }
            }

            //���������Ч����������±�־
            if (this.fpClass3_Sheet1.Rows.Count > 0)
            {
                isUpdate = true;
            }

            if (isUpdate)
            {
                FS.FrameWork.Management.PublicTrans.Commit();;
                MessageBox.Show(Language.Msg("����ɹ�"), "��ʾ");
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
            }
        }

        /// <summary>
        /// ����ϵͳȨ��
        /// </summary>
        public void SaveClass3Meaning()
        {
            int parm;
            bool isUpdate = false;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            class3Manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //��ɾ������
            parm = this.class3Manager.DeleteLevel3Meaning(this.operClass2Priv.Class2Code);
            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(Language.Msg(class3Manager.Err), "������ʾ");
                return;
            }

            //���������Ч����������±�־
            if (parm > 0) isUpdate = true;

            FS.FrameWork.Models.NeuObject info;
            //��������
            for (int i = 0; i < this.fpClass3Meaning_Sheet1.Rows.Count; i++)
            {
                info = new FS.FrameWork.Models.NeuObject();
                info.ID = this.fpClass3Meaning_Sheet1.Cells[i, 0].Text; //ϵͳȨ�ޱ���
                info.Name = this.fpClass3Meaning_Sheet1.Cells[i, 1].Text; //ϵͳȨ������
                if (info.ID == "" || info.Name == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(Language.Msg("ϵͳȨ�ޱ�������Ʋ���Ϊ��"), "��ʾ");
                    return;
                }
                info.Memo = this.fpClass3Meaning_Sheet1.Cells[i, 2].Text; //��ע
                info.User01 = this.operClass2Priv.Class2Code;					 //����Ȩ�ޱ���
                info.User02 = this.operClass2Priv.Class2Name;					 //����Ȩ������
                parm = class3Manager.InsertLevel3Meaning(info);
                if (parm != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    if (this.class3Manager.DBErrCode == 1)
                        MessageBox.Show(Language.Msg("����Ϊ��" + info.ID + "����ϵͳȨ���Ѿ�����,�����ظ����."));
                    else
                        MessageBox.Show(Language.Msg(this.class3Manager.Err), "������ʾ");

                    return;
                }
            }

            //���������Ч����������±�־
            if (this.fpClass3Meaning_Sheet1.Rows.Count > 0) isUpdate = true;

            if (isUpdate)
            {
                FS.FrameWork.Management.PublicTrans.Commit();;
                MessageBox.Show(Language.Msg("����ɹ�"), "��ʾ");
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
            }
        }

        /// <summary>
        /// ȡ��ǰ�Ķ���Ȩ��
        /// </summary>
        public void GetClass2()
        {
            //ȡ��ǰ���������е�����
            FS.HISFC.Models.Admin.PowerLevelClass2 temp = this.fpClass2_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Admin.PowerLevelClass2;
            if (temp == null)
            {
                this.operClass2Priv.Class2Code = "";
            }
            else
            {
                this.operClass2Priv = temp;
            }

            //��ʾϵͳȨ��
            this.ShowClass3Meaning();

            //��ʾ����Ȩ��
            this.ShowClass3();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            //��ն���Ȩ�޼�¼
            this.fpClass2_Sheet1.RowCount = 0;
            //�������Ȩ�޼�¼
            this.fpClass3_Sheet1.RowCount = 0;
            //���ϵͳȨ�޼�¼
            this.fpClass3Meaning_Sheet1.RowCount = 0;
        }


        private void ucPrivManager_Load(object sender, System.EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.ShowClass1("@@");

               // this.IsManagerEdit = false;
                this.gbClass2.Enabled = this.isEditSysData;
                this.gbClass3Meaning.Enabled = this.isEditSysData;
                //������ɾ��
                this.btSysDel.Enabled = false;
                this.btnDeleteClass2.Enabled = false;
            }
        }

        private void tvClass1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            this.operClass2Priv = e.Node.Tag as FS.HISFC.Models.Admin.PowerLevelClass2;

            this.ShowClass2();
        }

        private void fpClass2_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpClass2_Sheet1.RowCount == 0) return;

            //ȡ��ǰ���������е�����
            this.GetClass2();
        }

        private void fpClass3_ComboSelChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 3)
            {
                try
                {
                    //ȡ��ǰѡ�е�ϵͳȨ������
                    string text = ((FarPoint.Win.FpCombo)e.EditingControl).SelectedItem.ToString();
                    //�ҵ��ı�����Ӧ��ϵͳȨ�ޱ��룬������Ӧ����
                    foreach (FS.FrameWork.Models.NeuObject info in this.alClass3Meaning)
                    {
                        if (text == info.Name)
                        {
                            this.fpClass3_Sheet1.Cells[e.Row, 2].Value = info.ID;
                            break;
                        }
                    }

                    //����˶���Ȩ����û��ϵͳȨ�ޣ���Ĭ�϶���Ȩ�����ƣ�����Ϊ01
                    if (this.alClass3Meaning.Count == 0)
                    {
                        this.fpClass3_Sheet1.Cells[e.Row, 0].Text = "01"; //����Ȩ�ޱ���
                        this.fpClass3_Sheet1.Cells[e.Row, 2].Text = "01"; //ϵͳȨ�ޱ���
                    }

                    //����Ȩ��Ĭ������ΪϵͳȨ������
                    if (this.fpClass3_Sheet1.Cells[e.Row, 1].Text == "") this.fpClass3_Sheet1.Cells[e.Row, 1].Text = text;

                }
                catch { }
            }
            if (e.Column == 5)
            {
                try
                {
                    if (e.EditingControl == null)
                    {
                        return;
                    }

                    //ȡ��ǰѡ�е�ϵͳȨ������
                    string text = ((FarPoint.Win.FpCombo)e.EditingControl).SelectedItem.ToString();
                    //�ҵ��ı�����Ӧ��ϵͳȨ�ޱ��룬������Ӧ����
                    foreach (string strKey in this.hsClass3JoinCode.Keys)
                    {
                        if (this.hsClass3JoinCode[strKey] as string == text)
                        {
                            this.fpClass3_Sheet1.Cells[e.Row, 4].Value = strKey;
                            break;
                        }
                    }
                }
                catch { }
            }

        }


        private void btnAddClass2_Click(object sender, System.EventArgs e)
        {
            if (this.operClass2Priv.Class1Code == "") return;

            this.fpClass2_Sheet1.Rows.Add(this.fpClass2_Sheet1.RowCount, 1);
            //��ʾ��һ������
            this.fpClass2_Sheet1.ActiveRowIndex = this.fpClass2_Sheet1.RowCount - 1;
            this.fpClass2_Sheet1.Cells[this.fpClass2_Sheet1.ActiveRowIndex, 2].Value = 0;	//Ĭ�ϵ�������Ϊ0,�����ǵĺ�����:���ж�Ȩ��ʱ,�����ǿ�����Ϣ
            //ȡ��ǰ����Ȩ����Ϣ
            this.GetClass2();
            this.fpClass2.Focus();
        }

        private void btnDeleteClass2_Click(object sender, System.EventArgs e)
        {
            if (this.fpClass2_Sheet1.RowCount == 0)
            {
                MessageBox.Show(Language.Msg("��ѡ��Ҫɾ����ϵͳȨ��"), "��ʾ");
                return;
            }
            if (MessageBox.Show(Language.Msg("ȷ��Ҫɾ����������Ȩ����"), "����Ȩ��ɾ��", MessageBoxButtons.YesNo) == DialogResult.No) return;
            //ɾ��һ������Ȩ��
            this.fpClass2_Sheet1.ActiveRow.Remove();
        }

        private void btnSaveClass2_Click(object sender, System.EventArgs e)
        {
            //�������Ȩ��
            this.SaveClass2();
        }


        private void btnAddClass3_Click(object sender, System.EventArgs e)
        {
            if (this.operClass2Priv.Class2Code == "")
            {
                MessageBox.Show(Language.Msg("��ѡ��һ�����Ȩ��"), "��ʾ");
                return;
            }

            if (this.neuTabControl1.SelectedIndex == 0)
            {
                //����˶���Ȩ����û��ϵͳȨ�ޣ���ֻ������һ������Ȩ��
                if (this.alClass3Meaning.Count == 0 && this.fpClass3_Sheet1.RowCount == 1)
                {
                    MessageBox.Show(Language.Msg("�˶���Ȩ����ֻ�ܴ���һ������Ȩ�ޣ�"), "��ʾ");
                    return;
                }
                //����һ������Ȩ��
                this.fpClass3_Sheet1.Rows.Add(this.fpClass3_Sheet1.RowCount, 1);
                //��ʾ��һ������
                this.fpClass3_Sheet1.ActiveRowIndex = this.fpClass3_Sheet1.RowCount - 1;
                this.fpClass3.Focus();
            }
            else
            {
                //����һ��ϵͳȨ��
                this.fpClass3Meaning_Sheet1.Rows.Add(this.fpClass3Meaning_Sheet1.RowCount, 1);
                //��ʾ��һ������
                this.fpClass3Meaning_Sheet1.ActiveRowIndex = this.fpClass3Meaning_Sheet1.RowCount - 1;
                this.fpClass3Meaning.Focus();
            }
        }

        private void btnDeleteClass3_Click(object sender, System.EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                if (this.fpClass3_Sheet1.RowCount == 0)
                {
                    MessageBox.Show(Language.Msg("û�п���ɾ��������Ȩ��"), "��ʾ");
                    return;
                }
                if (MessageBox.Show(Language.Msg("ȷ��Ҫɾ����������Ȩ����"), "����Ȩ��ɾ��", MessageBoxButtons.YesNo) == DialogResult.No) return;
                //ɾ��һ������Ȩ��
                this.fpClass3_Sheet1.ActiveRow.Remove();
                //��������Ȩ��
                this.SaveClass3();
            }
            else
            {
                if (this.fpClass3Meaning_Sheet1.RowCount == 0)
                {
                    MessageBox.Show(Language.Msg("û�п���ɾ����ϵͳȨ��"), "��ʾ");
                    return;
                }
                if (MessageBox.Show(Language.Msg("ȷ��Ҫɾ������ϵͳȨ����"), "ϵͳȨ��ɾ��", MessageBoxButtons.YesNo) == DialogResult.No) return;
                //ɾ��һ��ϵͳȨ��
                this.fpClass3Meaning_Sheet1.ActiveRow.Remove();
                //����ϵͳȨ��
                this.SaveClass3Meaning();
            }
        }

        private void btnSaveClass3_Click(object sender, System.EventArgs e)
        {
            if (this.operClass2Priv.Class2Code == "") return;
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                //��������Ȩ��
                this.SaveClass3();
            }
            else
            {
                //����ϵͳȨ��
                this.SaveClass3Meaning();
            }
        }

        private void tabControl1_SelectionChanged(object sender, EventArgs e)
        {
           
        }
    }
}
