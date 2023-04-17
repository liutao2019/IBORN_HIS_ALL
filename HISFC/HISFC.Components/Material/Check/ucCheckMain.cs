using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.UFC.Material.Check
{
    public partial class ucCheckMain : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucCheckMain()
        {
            InitializeComponent();
        }
        /*
        #region �����
        //Ȩ�޿���
        private Neusoft.NFC.Object.NeuObject myPrivDept = new Neusoft.NFC.Object.NeuObject();
        //��ǰ����Ա
        private string myOperCode;
        //��ǰ�ⷿ�Ƿ����Ź���,Ĭ�ϰ����Ź���
        private bool isBatch = true;
        //��ǰ����̵㵥��
        private string nowCheckCode = "";
        /// <summary>
        /// �Ƿ��̵���Ȩ��
        /// </summary>
        private bool isCheckCStore = false;
        Neusoft.HISFC.Management.Material.MetItem myMetItem = new Neusoft.HISFC.Management.Material.MetItem();
        private DateTime dateBegin;
        private DateTime dateEnd;
        #endregion

        #region ����
        /// <summary>
        ///��ʾ�����̵㵥�б�
        /// </summary>
        private void ShowCheckList()
        {
            //����б�
            this.ucChooseList.tvList.Nodes.Clear();
            //��ǰ���ԶԷ����˵��жϣ�������ʾȫ�������̵㵥
            ArrayList checkAl;
            try
            {
                checkAl = this.myItem.GetCheckList(this.myPrivDept.ID, "0", "ALL");
                if (checkAl == null)
                {
                    MessageBox.Show(this.myItem.Err);
                    return;
                }
                if (checkAl.Count == 0)
                {
                    this.ucChooseList.tvList.Nodes.Add(new TreeNode("û�з����̵㵥", 0, 0));
                }
                else
                {
                    this.ucChooseList.tvList.Nodes.Add(new TreeNode("�����̵㵥�б�", 0, 0));
                    //��ʾ�̵㵥�б�
                    TreeNode newNode;
                    foreach (Neusoft.NFC.Object.NeuObject info in checkAl)
                    {
                        newNode = new TreeNode();
                        //��÷�����Ա����
                        Neusoft.HISFC.Management.Manager.Person person = new Neusoft.HISFC.Management.Manager.Person();
                        Neusoft.HISFC.Object.RADT.Person personName = person.GetPersonByID(info.Name);
                        if (personName == null)
                        {
                            MessageBox.Show("��÷�����Ա��Ϣʱ������Ա����Ϊinfo.Name����Ա������");
                            return;
                        }
                        newNode.Text = info.ID + "-" + personName.Name;		//�̵㵥��+������
                        newNode.Tag = info.ID;
                        newNode.SelectedImageIndex = newNode.ImageIndex;
                        this.ucChooseList.tvList.Nodes[0].Nodes.Add(newNode);
                    }
                    this.ucChooseList.tvList.Nodes[0].ExpandAll();
                    this.ucChooseList.tvList.SelectedNode = this.ucChooseList.tvList.Nodes[0];
                }
                //��ʾ�����б�
                this.ucChooseList.IsShowTreeView = true;
                this.ucChooseList.Caption = "�̵㵥�б�";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// ��ʼ��ucChooseList��DataSet
        /// </summary>
        public  void InitColumns()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");

            //��myDataTable�������
            this.ucChooseList.DataTable.Columns.AddRange(new DataColumn[] {
																			   new DataColumn("���ʱ���",    dtStr),
																			   new DataColumn("��������",    dtStr),
																			   new DataColumn("���",        dtStr),
																			   new DataColumn("����",        dtStr),
																			   new DataColumn("��λ��",      dtStr),
																			   new DataColumn("���",		 dtStr),
																			   new DataColumn("ƴ����",      dtStr),
																			   new DataColumn("�����",      dtStr),
																			   new DataColumn("ͨ����ƴ����",dtStr),
																			   new DataColumn("ͨ���������",dtStr)
																		   });
        }


        /// <summary>
        /// �ɿ��ҩƷ�б���ѡ��һ��ҩƷ������ʼ�¼
        /// </summary>
        /// <param name="row">ѡ�е�������</param>
        public  void ChooseData(int row)
        {
            this.dateBegin = this.ucCheckManager1.dtBegin.Value;
            this.dateEnd = this.ucCheckManager1.dtEnd.Value;
            if (row < 0) return;
            string itemCode = this.ucChooseList.fpChooseList_Sheet1.Cells[row, 0].Text;
            string batchNo = this.ucChooseList.fpChooseList_Sheet1.Cells[row, 3].Text;
            string placeCode = this.ucChooseList.fpChooseList_Sheet1.Cells[row, 4].Text;
            string checkCode = this.myMetItem.GetMaxCheckStoreCode(this.myPrivDept.ID);
            //�����̵���ϸ��¼���ʴ���
            this.ucCheckManager1.AddData(this.myPrivDept.ID, itemCode, checkCode, this.dateBegin, this.dateEnd, batchNo, placeCode, this.isBatch);
        }


        /// <summary>
        /// �жϲ���Ա�ڵ�ǰ�����Ƿ����Ȩ��
        /// </summary>
        /// <param name="privClass2Code">����Ȩ����</param>
        /// <returns>����Ȩ�޷���True ���򷵻�False</returns>
        private bool JudgePriv(string privClass2Code)
        {
            ArrayList al = Neusoft.Common.Class.Function.ChoosePivDept(privClass2Code);
            if (al == null || al.Count <= 0)
                return false;
            foreach (Neusoft.NFC.Object.NeuObject info in al)
            {
                if (info.ID == this.myPrivDept.ID)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// ���ù���������ʾ
        /// </summary>
        /// <param name="buttonName">����İ�ť</param>
        private void SetToolBatButton(string buttonName)
        {
            //			this.tbbAddSave.Visible = true;
            //			this.tbbSave.Visible = false;
            //			this.tbbCheckClose.Visible = false;
            //			this.tbbCheckAdd.Visible = false;
            //			//�̵���Ȩ��
            //			if(this.JudgePriv("0306"))	
            //			{
            //				this.tbbSave.Visible = true;
            //				this.tbbAddSave.Visible = false;
            //				this.tbbCheckClose.Visible = true;
            //				this.tbbCheckAdd.Visible = true;
            //			}

            switch (buttonName)
            {
                case "tbbCheckClose":		//���ʰ�ť
                    this.tbbCheckClose.Visible = false;			//����
                    this.tbbGroup.Visible = false;				//��������
                    //this.tbbDrug.Visible = true;				//ҩƷ
                    this.tbbList.Visible = true;				//�̵��б�
                    this.tbbCheckAdd.Visible = false;			//�̵�ģ��
                    this.ucCheckManager1.AllowDel = true;		//�����FarPoint������ɾ��
                    this.tbbDel.Visible = true;					//ɾ����ť
                    this.tbPrint.Visible = false;				//��ӡ
                    //this.tbbAddSave.Visible = false;			//��������
                    this.tbShow.Visible = true;
                    break;
                case "tbbList":				//�̵��б�ť
                    this.tbbCheckClose.Visible = true;			//����
                    this.tbbGroup.Visible = false;				//��������
                    //this.tbbDrug.Visible = false;				//ҩƷ
                    this.tbbList.Visible = false;				//�̵��б�
                    this.tbbCheckAdd.Visible = false;			//�̵�ģ��
                    this.ucCheckManager1.AllowDel = false;		//�������FarPoint������ɾ��
                    this.tbbDel.Visible = true;				//ɾ����ť
                    this.tbPrint.Visible = true;				//��ӡ
                    //this.tbbAddSave.Visible = true;				//��������
                    this.tbShow.Visible = true;
                    break;
                case "Init":				//��ʼ��
                    this.tbbCheckClose.Visible = true;
                    this.tbbCheck.Visible = false;
                    this.tbbDrug.Visible = false;
                    this.tbbList.Visible = false;
                    this.tbbCheckAdd.Visible = false;
                    this.tbbGroup.Visible = false;
                    this.tbbDel.Visible = true;
                    //this.tbbAddSave.Visible = true;				//��������
                    this.tbShow.Visible = true;
                    break;
            }

            //���ͷ��ఴť����ʾ������������ʾ��ͬ
            //this.tbbDosageClass.Visible = this.tbbGroup.Visible;
            //this.tbbChecHistoryList.Visible = this.tbbGroup.Visible;
            this.tbbOpen.Visible = this.tbbGroup.Visible;
        }


        /// <summary>
        /// ���ݼ��ʹ��� ���������̵����
        /// </summary>
        public void SetDosageClass()
        {
            /////---liuxq---/////
            //			DialogResult rs = MessageBox.Show("�������ཫ�����ǰ������ �Ƿ����?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
            //			if (rs == DialogResult.No)
            //				return;
            //
            //			try
            //			{
            //				Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڼ��ط�����漰���� ���Ժ�...");
            //				Application.DoEvents();
            //
            //				ucDosageClass uc = new ucDosageClass();
            //			
            //				Neusoft.HISFC.Management.Pharmacy.Item itemManager = new Neusoft.HISFC.Management.Pharmacy.Item();
            //				ArrayList alItem = itemManager.GetStorageList(this.myPrivDept.ID,this.isBatch);
            //				ArrayList alItemDetail = new ArrayList();
            //				int i = 1;
            //				foreach(Neusoft.HISFC.Object.Pharmacy.Item item in alItem)
            //				{
            //					i++;
            //					Neusoft.NFC.Interface.Classes.Function.ShowWaitForm(i,alItem.Count);
            //					Application.DoEvents();
            //
            //					Neusoft.HISFC.Object.Pharmacy.Item itemDetail = itemManager.GetItem(item.ID);
            //					if (itemDetail == null)
            //					{
            //						MessageBox.Show("ҩƷ�ֵ���������" + item.Name + "-" + item.ID);
            //						continue;
            //					}
            //					alItemDetail.Add(itemDetail);
            //				}
            //				uc.OrignData = alItemDetail;
            //
            //				Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            //
            //				Neusoft.NFC.Interface.Classes.Function.PopShowControl(uc);
            //				if (uc.ConvertData != null)
            //				{
            //					Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڰ�ѡ��ķ������ҩƷ���ʴ���..���Ժ�");
            //					Application.DoEvents();
            //
            //					this.ucCheckManager1.ClearData();
            //					foreach(Neusoft.HISFC.Object.Pharmacy.Item item in uc.ConvertData)
            //					{
            //						this.ucCheckManager1.AddData(this.myPrivDept.ID,item.ID,item.User01,item.User02,this.isBatch);
            //					}
            //
            //					Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            //				}
            //			}
            //			catch (Exception ex)
            //			{
            //				MessageBox.Show(ex.Message);
            //			}
            //			finally
            //			{
            //				Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            //			}
        }


        /// <summary>
        /// ��ȡ��ʷ���ݼ�¼
        /// </summary>
        public void ShowHistoryList()
        {
            DialogResult rs = MessageBox.Show("�������ཫ�����ǰ������ �Ƿ����?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
                return;

            Neusoft.HISFC.Management.Pharmacy.Item itemManager = new Neusoft.HISFC.Management.Pharmacy.Item();
            ArrayList alCheck = new ArrayList();
            alCheck = itemManager.GetCheckList(this.myPrivDept.ID, "0", "ALL");
            if (alCheck == null)
            {
                MessageBox.Show(itemManager.Err);
                return;
            }
            ArrayList alInfo = new ArrayList();
            foreach (Neusoft.NFC.Object.NeuObject info in alCheck)
            {
                Neusoft.NFC.Object.NeuObject temp = new Neusoft.NFC.Object.NeuObject();
                //��÷�����Ա����
                Neusoft.HISFC.Management.Manager.Person person = new Neusoft.HISFC.Management.Manager.Person();
                Neusoft.HISFC.Object.RADT.Person personName = person.GetPersonByID(info.Name);
                if (personName == null)
                {
                    MessageBox.Show("��÷�����Ա��Ϣʱ����");
                    return;
                }
                temp.ID = info.ID;
                temp.Name = personName.Name;		//�̵㵥��+������

                alInfo.Add(temp);
            }

            Neusoft.NFC.Object.NeuObject selectObj = new Neusoft.NFC.Object.NeuObject();
            string[] label = { "���ݺ�", "������" };
            float[] width = { 120F, 100F };
            bool[] visible = { true, true, false, false, false, false };
            ///---liuxq---///
            //			if (Function.ChooseItem(alInfo,label,width,visible,ref selectObj) == 0) 
            //			{
            //				return;
            //			}
            //			else 
            //			{				
            //				ArrayList al = new ArrayList();
            //			
            //				al = this.myItem.GetCheckDetailByCheckCode(this.myPrivDept.ID,selectObj.ID);
            //				if (al == null)
            //				{
            //					MessageBox.Show(this.myItem.Err);
            //					return;
            //				}
            //				Neusoft.NFC.Interface.Classes.Function.ShowWaitForm("���ڸ�����ѡ�̵㵥���з��ʴ���...");
            //				Application.DoEvents();
            //				int i = 1;
            //				foreach(Neusoft.HISFC.Object.Pharmacy.Check checkInfo in al)
            //				{
            //					Neusoft.NFC.Interface.Classes.Function.ShowWaitForm(i,al.Count);
            //					Application.DoEvents();
            //
            //					this.ucCheckManager1.AddData(this.myPrivDept.ID,checkInfo.Item.ID,checkInfo.BatchNo,checkInfo.PlaceCode,this.isBatch);
            //				}
            //
            //				Neusoft.NFC.Interface.Classes.Function.HideWaitForm();
            //			}

        }


       
        /// <summary>
        /// �������ʣ�����б��������ݣ�������ӣ�
        /// </summary>
        public void ShowItemAll()
        {
            this.dateBegin = this.ucCheckManager1.dtBegin.Value;
            this.dateEnd = this.ucCheckManager1.dtEnd.Value;

            string checkCode = this.myMetItem.GetMaxCheckStoreCode(this.myPrivDept.ID);

            if (this.ucChooseList.fpChooseList_Sheet1.RowCount <= 0)
                return;
            for (int i = 0; i < this.ucChooseList.fpChooseList_Sheet1.RowCount; i++)
            {
                string itemCode = this.ucChooseList.fpChooseList_Sheet1.Cells[i, 0].Text;
                string batchNo = this.ucChooseList.fpChooseList_Sheet1.Cells[i, 3].Text;
                string placeCode = this.ucChooseList.fpChooseList_Sheet1.Cells[i, 4].Text;
                this.ucCheckManager1.AddData(this.myPrivDept.ID, itemCode, checkCode, this.dateBegin, this.dateEnd, batchNo, placeCode, this.isBatch);
            }
        }
        #endregion

        private void ucCheckMain_Load(object sender, EventArgs e)
        {
            //�����¼�
            this.ucChooseList.tvList.AfterSelect += new TreeViewEventHandler(tvList_AfterSelect);

            #region �ж�Ȩ�޲���ȡ����Ա��Ϣ
            //�жϲ���Ա�Ƿ�ӵ���̵�Ȩ�ޣ����û������������˴���
            int privParm = Neusoft.Common.Class.Function.ChoosePivDept("0505", ref myPrivDept, this);
            if (privParm == 0)
                return;

            this.ucCheckManager1.myPrivDept = this.myPrivDept;
            try
            {
                Neusoft.NFC.Management.DataBaseManger data = new Neusoft.NFC.Management.DataBaseManger();
                this.myOperCode = ((Neusoft.HISFC.Object.RADT.Person)data.Operator).ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

            //��ÿⷿ���Ʋ������ж϶Ըÿⷿ�Ƿ����Ź���
            Neusoft.HISFC.Management.Manager.Controler ctrlMgr = new Neusoft.HISFC.Management.Manager.Controler();
            string ctrlStr = ctrlMgr.QueryControlerInfo("510001");
            if (ctrlStr == "1")
                this.isBatch = true;
            else
                this.isBatch = false;

            this.isCheckCStore = this.JudgePriv("0505");

            //��ʼ��������
            this.SetToolBatButton("Init");

            if (!this.isCheckCStore)
            {
                this.tbbCheckClose.Visible = false;
                this.tbbSave.Visible = false;
                this.tbbCheckAdd.Visible = false;
            }
            else
            {
                this.tbbAddSave.Visible = false;
            }

            this.ucCheckManager1.IsWindowCheck = !this.isCheckCStore;


            //��ʾ�����б�
            this.ShowCheckList();
            //��ʾ���ҩƷ�б�
            this.ShowDeptStorage(this.myPrivDept.ID, this.isBatch);
            int iWidth = 0;
            this.ucChooseList.GetColumnWidth(2, ref iWidth);
            if (iWidth > 0)
                this.panelLeft.Width = iWidth + 5;
            this.ucCheckManager1.dtBegin.Value = Neusoft.NFC.Function.NConvert.ToDateTime(this.myMetItem.GetMaxCheckStoreDate(this.myPrivDept.ID));
            this.ucCheckManager1.dtEnd.Value = this.myMetItem.GetDateTimeFromSysDateTime();
            this.dateBegin = this.ucCheckManager1.dtBegin.Value;
            this.dateEnd = this.ucCheckManager1.dtEnd.Value;

            #region ����ucCheckManager����
            //��ʽ��FarPoint��ʾ
            this.ucCheckManager1.SetFormat();
            //�Ƿ�������̵������༭
            this.ucCheckManager1.AllowEdit = true;
            //��ʼ�������FarPoint������ɾ��
            this.ucCheckManager1.AllowDel = false;

            #endregion
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //������ڵ�
            if (e.Node.Parent == null)
            {
                this.ucCheckManager1.ClearData();
                return;
            }
            //�̵㵥��
            this.nowCheckCode = e.Node.Tag.ToString();
            if (this.nowCheckCode == "" || this.nowCheckCode == null)
                return;
            this.ucCheckManager1.ClearData();		//�������
            this.ucCheckManager1.ShowCheckDetail(this.myPrivDept.ID, this.nowCheckCode);
        }
         * */
    }
}
