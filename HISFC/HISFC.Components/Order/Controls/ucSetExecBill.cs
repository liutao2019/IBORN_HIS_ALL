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
    /// ִ�е�����
    /// </summary>
    public partial class ucSetExecBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.FrameWork.Models.NeuObject objExecBill = new FS.FrameWork.Models.NeuObject();
        private FS.FrameWork.Public.ObjectHelper helper;

        private ArrayList alItem = new ArrayList();

        /// <summary>
        /// ��Ŀ�ڵ�
        /// </summary>
        private TreeNode tnItemList = new TreeNode();

        /// <summary>
        /// ҩƷ���ڵ�
        /// </summary>
        private TreeNode tnDragType = new TreeNode();

        /// <summary>
        /// �÷��ڵ�
        /// </summary>
        private TreeNode tnConstant = new TreeNode();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string errInfo = "";

        private int icont = 0;

        /// <summary>
        /// ��ʵ�ֽӿ���ʹ�õı���
        /// </summary>
        private FS.HISFC.BizLogic.Order.ExecBill execBillMgr = new FS.HISFC.BizLogic.Order.ExecBill();

        public static string strName = "";

        public ucSetExecBill()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ҽ������
        /// </summary>
        private ArrayList arrOrderType = new ArrayList();

        /// <summary>
        /// �����÷�����
        /// </summary>
        private ArrayList arrConstant = new ArrayList();
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private ArrayList arrItemList = new ArrayList();
        /// <summary>
        /// ҩƷ���
        /// </summary>
        private ArrayList arrDrugType = new ArrayList();
        /// <summary>
        /// ִ�е�����
        /// </summary>
        private ArrayList alExecBill = new ArrayList();

        #region ����

        private void AddInfo(int Branch, FS.FrameWork.Models.NeuObject neuObj, object obj)
        {
            string strText = neuObj.Name;

        }

        private void AddTreeNode(int root, string Name, object obj, int ImageIndex)
        {
            System.Windows.Forms.TreeNode Node = new System.Windows.Forms.TreeNode();
            try
            {
                Node.Text = Name;
                Node.Tag = obj;
            }
            catch { }
        }

        private void AddRootNode()
        {
            this.tvDrug.Nodes.Add("ҩ��ִ�е���Ŀѡ��");
            this.tvUndrug.Nodes.Add("��ҩ��ִ�е���Ŀѡ��");
        }

        #endregion

        #region ����

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBar.AddToolButton("�½�ִ�е�", "�½�ִ�е�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            this.toolBar.AddToolButton("�༭ִ�е�", "�༭ִ�е�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            this.toolBar.AddToolButton("ɾ��ִ�е�", "ɾ������ִ�е�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡȡ��, true, false, null);
            this.toolBar.AddToolButton("ɾ����ϸ", "ɾ��ִ�е���ϸ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            
            return toolBar;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ɾ��ִ�е�":
                    this.DeleteAllBill();
                    break;
                case "�½�ִ�е�":
                    this.AddNewBill();
                    break;
                case "�༭ִ�е�":
                    this.EditBill();
                    break;
                case "ɾ����ϸ":

                    if (MessageBox.Show("�Ƿ�ɾ������", "ȷ��", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                        {
                            if (neuSpread1.ActiveSheet.IsSelected(i, 0))
                            {
                                if (DeleteBillDetail(i) == -1)
                                {
                                    int active = neuSpread1.ActiveSheetIndex;
                                    this.BindFp();
                                    neuSpread1.ActiveSheetIndex = active;
                                }
                            }
                        }

                        for (int i = 0; i < this.neuSpread1.ActiveSheet.RowCount; i++)
                        {
                            if (neuSpread1.ActiveSheet.IsSelected(i, 0))
                            {
                                this.neuSpread1.ActiveSheet.Rows.Remove(i, 1);
                            }
                        }
                    }
                    break;
            }
        }

        #endregion

        /// <summary>
        /// �����ѱ�����Ŀ
        /// </summary>
        private void Filter()
        {
            //ҽ�����
            string orderType = "";
            //��Ŀ���
            string itemType = "";
            //�÷�
            string usage = "";

            for (int index = 0; index < this.neuSpread1.Sheets.Count; index++)
            {
                for (int i = 0; i < this.neuSpread1.Sheets[index].RowCount; i++)
                {
                    //orderType = this.neuSpread1.Sheets[index].Cells[i, 1].Text;
                    //itemType = this.neuSpread1.Sheets[index].Cells[i, 2].Text;
                    //usage = this.neuSpread1.Sheets[index].Cells[i, 3].Text;

                    orderType = this.neuSpread1.Sheets[index].Cells[i, 1].Tag.ToString(); //ҽ������,

                    itemType = this.neuSpread1.Sheets[index].Cells[i, 2].Tag.ToString();//��ҩϵͳ���ҩƷ���,
                    usage = this.neuSpread1.Sheets[index].Cells[i, 3].Tag.ToString();//ҩƷ�÷���

                    #region �����÷�ɾ���ڵ�

                    if (usage != "")
                    {
                        foreach (TreeNode node in this.tvDrug.Nodes)
                        {
                            if (orderType == node.Tag.ToString())
                            //if (orderType == node.Text)
                            {
                                foreach (TreeNode childnode in node.Nodes)
                                {
                                    //if (itemType == childnode.Text)
                                    if (itemType == childnode.Tag.ToString())
                                    {
                                        foreach (TreeNode n in childnode.Nodes)
                                        {
                                            //if (usage == n.Text)
                                            if (usage == n.Tag.ToString())
                                            {
                                                n.Remove();
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }

                        foreach (TreeNode node in this.tvUndrug.Nodes)
                        {
                            //if (orderType == node.Text)
                            if (orderType == node.Tag.ToString())
                            {
                                foreach (TreeNode childnode in node.Nodes)
                                {
                                    //if (itemType == childnode.Text)
                                    if (itemType == childnode.Tag.ToString())
                                    {
                                        foreach (TreeNode n in childnode.Nodes)
                                        {
                                            //if (usage == n.Text)
                                            if (usage == n.Tag.ToString())
                                            {
                                                n.Remove();
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    #endregion

                    #region ����ϵͳ���ɾ���ڵ�

                    else//��ҩƷ
                    {
                        foreach (TreeNode node in this.tvUndrug.Nodes)
                        {
                            //if (orderType == node.Text)
                            if (orderType == node.Tag.ToString())
                            {
                                foreach (TreeNode childnode in node.Nodes)
                                {
                                    //if (itemType == childnode.Text)
                                    if (itemType == childnode.Tag.ToString())
                                    {
                                        childnode.Remove();
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// ��ȡ�ѱ����ִ�е�����
        /// </summary>
        /// <returns></returns>
        private ArrayList GetFpSheet()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
            {
                al.Add(this.neuSpread1.Sheets[i].SheetName.Trim());
            }
            return al;
        }

        private bool IsRepeat()
        {
            bool bRet = true;
            for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
            {
                if (this.lblExecBillName.Text.Trim() == this.neuSpread1.Sheets[i].SheetName.Trim())
                {
                    MessageBox.Show("�����ظ���");
                    bRet = false;
                    grpExecBillD.Visible = true;
                }
            }
            return bRet;
        }

        /// <summary>
        /// ��ʼ��Tree
        /// </summary>
        private void InitTree()
        {
            Enum enDrug = FS.HISFC.Models.Base.EnumSysClass.P;
            //enDrug.ToString();
            arrDrugType.AddRange(this.alItem);

            //�����Ŀ����б�
            arrOrderType = CacheManager.InterMgr.QueryOrderTypeList();

            //����÷��б�
            arrConstant = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            arrItemList = FS.HISFC.Models.Base.SysClassEnumService.List();

            //ˢ����ʾ
            this.RefreshList();

            //���������Ѿ��е�
            this.Filter();
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        private void RefreshList()
        {
            this.tvDrug.Nodes.Clear();
            this.tvUndrug.Nodes.Clear();

            #region ҩ��ִ�е�
            try
            {
                for (int i = 0; i < arrOrderType.Count; i++)
                {
                    TreeNode node = new TreeNode(arrOrderType[i].ToString());
                    node.Tag = ((FS.FrameWork.Models.NeuObject)arrOrderType[i]).ID.ToString();

                    for (int j = 0; j < arrDrugType.Count; j++)
                    {
                        tnDragType = new TreeNode(arrDrugType[j].ToString());
                        tnDragType.Tag = ((FS.FrameWork.Models.NeuObject)arrDrugType[j]).ID.ToString();
                        for (int k = 0; k < arrConstant.Count; k++)
                        {
                            tnConstant = new TreeNode(arrConstant[k].ToString());
                            tnConstant.Tag = ((FS.FrameWork.Models.NeuObject)arrConstant[k]).ID.ToString();
                            tnDragType.Nodes.Add(tnConstant);
                        }
                        node.Nodes.Add(tnDragType);
                    }
                    this.tvDrug.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����" + ex.Message);
            }
            #endregion

            #region ��ҩ��ִ�е�
            try
            {
                for (int i = 0; i < arrOrderType.Count; i++)
                {
                    TreeNode node = new TreeNode(arrOrderType[i].ToString());
                    node.Tag = ((FS.FrameWork.Models.NeuObject)arrOrderType[i]).ID.ToString();

                    for (int j = 0; j < arrItemList.Count; j++)
                    {
                        if (((FS.FrameWork.Models.NeuObject)arrItemList[j]).ID.Substring(0, 1) != "P")
                        {
                            tnItemList = new TreeNode(arrItemList[j].ToString());
                            tnItemList.Tag = ((FS.FrameWork.Models.NeuObject)arrItemList[j]).ID.ToString();

                            //for (int k = 0; k < arrConstant.Count; k++)
                            //{
                            //    tnConstant = new TreeNode(arrConstant[k].ToString());
                            //    tnConstant.Tag = ((FS.FrameWork.Models.NeuObject)arrConstant[k]).ID.ToString();
                            //    tnItemList.Nodes.Add(tnConstant);
                            //}
                            node.Nodes.Add(tnItemList);
                        }
                    }
                    this.tvUndrug.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����" + ex.Message);
            }
            #endregion

            #region ��ҩ��ִ�е�����Ŀά��
            //��ҩ����Ŀִ�е�
            try
            {
                for (int i = 0; i < arrOrderType.Count; i++)
                {
                    TreeNode node = new TreeNode(arrOrderType[i].ToString());
                    node.Tag = ((FS.FrameWork.Models.NeuObject)arrOrderType[i]).ID.ToString();

                    for (int j = 0; j < arrItemList.Count; j++)
                    {
                        if (((FS.FrameWork.Models.NeuObject)arrItemList[j]).ID.Substring(0, 1) != "P")
                        {
                            tnItemList = new TreeNode(arrItemList[j].ToString());
                            //tnItemList.Tag = ((FS.HISFC.Object.Base.EnumSysClass)arrItemList[j]).ID.ToString();
                            tnItemList.Tag = ((FS.FrameWork.Models.NeuObject)arrItemList[j]).ID.ToString();
                            node.Nodes.Add(tnItemList);
                        }
                    }
                    this.tvUndrugItem.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����" + ex.Message);
            }
            #endregion
        }

        private void InitControl()
        {
            this.alItem = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            helper = new FS.FrameWork.Public.ObjectHelper(alItem);
            this.InitTree();
        }

        /// <summary>
        /// ����ִ�е�
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int SaveBill(ref string errInfo)
        {
            if (this.neuSpread1.Sheets.Count == 0)
                return -1;

            //�������Ŀִ�е����Ѿ��������
            FS.FrameWork.Models.NeuObject bill = this.neuSpread1.ActiveSheet.Tag as FS.FrameWork.Models.NeuObject;

            if (bill == null)
            {
                errInfo = "û���ҵ�ִ�е��ݣ�";
                return -1;
            }

            if (bill.Memo == "1")
            {
                return 0;
            }

            if (this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].SheetName.Trim() == "" || this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].SheetName.Trim() == null)
            {
                if (this.lblExecBillName.Text.Trim() == "" || this.lblExecBillName.Text.Trim() == null)
                {
                    errInfo = "�����뵥�ӵ�����";
                    return -1;
                }
                this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].SheetName = this.lblExecBillName.Text.Trim();
            }


            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                execBillMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                ArrayList al = new ArrayList();
                execBillMgr.Name = neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].SheetName.Trim();
                for (int i = 0; i < this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Rows.Count; i++)
                {
                    try
                    {
                        FS.FrameWork.Models.NeuObject objBill = new FS.FrameWork.Models.NeuObject();
                        //������д��objBill.ID ִ�е���ˮ�ţ�objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������,
                        //objBill.Name = this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].SheetName.Trim();//ִ�е���		
                        if (this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Tag != null)
                        {
                            objBill.ID = ((FS.FrameWork.Models.NeuObject)this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Tag).ID;
                        }
                        objBill.Memo = this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Cells[i, 0].Text.Trim();//ִ�е����ͣ�

                        objBill.User01 = this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Cells[i, 1].Tag.ToString(); //ҽ������,

                        objBill.User02 = this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Cells[i, 2].Tag.ToString();//��ҩϵͳ���ҩƷ���,
                        objBill.User03 = this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Cells[i, 3].Tag.ToString();//ҩƷ�÷���
                        
                        objBill.Name = this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Cells[i, 3].Text;  //�洢��ҩƷ������
                        al.Add(objBill);
                        objBill = null;
                    }
                    catch (Exception ex)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = ex.Message;
                        return -1;
                    }
                }

                string personId = execBillMgr.Operator.ID;
                string strNurse = (execBillMgr.Operator as FS.HISFC.Models.Base.Employee).Nurse.ID; //person.Nurse.ID.ToString();
                string BillID = "";//ִ�е���

                if (this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag == null)
                {
                    this.objExecBill.Name = neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].SheetName.Trim();
                    if (execBillMgr.SetExecBill(al, this.objExecBill, strNurse, ref BillID) == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                        this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag = BillID;
                        //MessageBox.Show("����ɹ�!");
                        return 0;
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        errInfo = execBillMgr.Err;
                        return -1;
                    }
                }
                else
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    for (int i = 0; i < al.Count; i++)
                    {
                        obj = (FS.FrameWork.Models.NeuObject)al[i];
                        if (execBillMgr.UpdateExecBill(obj, strNurse/*, obj.Memo*/) != 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            errInfo = "�������!" + execBillMgr.Err;
                            return -1;
                        }
                    }
                    if (this.lblExecBillName.Text.Trim() != "" || this.lblExecBillName.Text.Trim() != null)
                    {
                        if (execBillMgr.UpdateExecBillName(obj.ID, this.lblExecBillName.Text.Trim(), obj.User01, obj.User02) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            errInfo = "�������!" + execBillMgr.Err;
                            return -1;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();

                    return 0;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
        }


        /// <summary>
        /// farP����
        /// </summary>
        /// <param name="obj">ҽ������</param>
        /// <param name="i">���Ӹ���</param>
        protected void AddExecBill(FS.HISFC.Models.Order.Inpatient.Order obj, int i)
        {
            this.neuSpread1.Sheets[i].Rows.Add(0, 1);
            this.neuSpread1.Sheets[i].SetValue(0, 0, obj.Memo, false);
            //fpSpread1.Sheets[i].Cells[0,0].Text = obj.Memo.ToString();//ִ�е�����		
            this.neuSpread1.Sheets[i].SetValue(0, 1, obj.OrderType.Name, false);
            //fpSpread1.Sheets[i].Cells[0,1].Value  = obj.OrderType.Name;//ҽ������
            this.neuSpread1.Sheets[i].Cells[0, 1].Tag = obj.OrderType.ID;
            if (obj.Memo == "2")
            {
                this.neuSpread1.Sheets[i].SetValue(0, 2, obj.Item.SysClass.Name, false);
                //fpSpread1.Sheets[i].Cells[0,2].Value = obj.Item.SysClass.Name;//ҩƷ����
                this.neuSpread1.Sheets[i].Cells[0, 2].Tag = obj.Item.SysClass.ID;
            }
            else
            {
                this.neuSpread1.Sheets[i].SetValue(0, 2, helper.GetName(obj.Item.User01), false);
                //fpSpread1.Sheets[i].Cells[0,2].Value = obj.Item.SysClass.Name;//ҩƷ����
                this.neuSpread1.Sheets[i].Cells[0, 2].Tag = obj.Item.User01;
            }
            this.neuSpread1.Sheets[i].SetValue(0, 3, obj.Usage.Name, false);
            //fpSpread1.Sheets[i].Cells[0,3].Text = obj.Usage.Name;//���÷���
            this.neuSpread1.Sheets[i].Cells[0, 3].Tag = obj.Usage.ID;
            this.neuSpread1.Sheets[i].SetValue(0, 4, this.execBillMgr.Operator.Name, false);
            //fpSpread1.Sheets[i].Cells[0,4].Text = oCExecBill.Operator.Name.ToString();//����Ա
            this.neuSpread1.Sheets[i].Cells[0, 4].Tag = execBillMgr.Operator.ID.ToString();
            this.neuSpread1.Sheets[i].SetValue(0, 5, DateTime.Now.ToString(), false);
            //fpSpread1.Sheets[i].Cells[0,5].Text = oCExecBill.GetSysDate();//����ʱ��						
        }

        /// <summary>
        /// ����ϸ��ӵ�farpoint����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="i"></param>
        /// <param name="isItemDetail">�Ƿ���Ŀ��ϸִ�е�</param>
        protected void AddExecBill(FS.HISFC.Models.Order.Inpatient.Order obj, int i, bool isItemDetail)
        {
            this.neuSpread1.Sheets[i].Rows.Add(0, 1);

            this.neuSpread1.Sheets[i].SetValue(0, 0, obj.Memo, false);	//ҩƷ/��ҩƷ
            this.neuSpread1.Sheets[i].SetValue(0, 1, obj.OrderType.Name, false); //ҽ�����
            this.neuSpread1.Sheets[i].Cells[0, 1].Tag = obj.OrderType.ID;

            if (obj.Memo == "2")
            {
                this.neuSpread1.Sheets[i].SetValue(0, 2, obj.Item.SysClass.Name, false); //ϵͳ���
                this.neuSpread1.Sheets[i].Cells[0, 2].Tag = obj.Item.SysClass.ID;
            }
            else
            {
                this.neuSpread1.Sheets[i].SetValue(0, 2, helper.GetName(obj.Item.User01), false); //ҩƷ���
                this.neuSpread1.Sheets[i].Cells[0, 2].Tag = obj.Item.User01;
            }

            if (isItemDetail || !string.IsNullOrEmpty(obj.Item.Name))
            {
                this.neuSpread1.Sheets[i].SetValue(0, 3, obj.Item.Name, false); //��Ŀ����
                this.neuSpread1.Sheets[i].Cells[0, 3].Tag = obj.Item.ID;
                this.neuSpread1.Sheets[i].Rows[0].Tag = obj.Item;
            }
            else
            {
                this.neuSpread1.Sheets[i].SetValue(0, 3, obj.Usage.Name, false); //��������
                this.neuSpread1.Sheets[i].Cells[0, 3].Tag = obj.Usage.ID;
            }

            this.neuSpread1.Sheets[i].SetValue(0, 4, execBillMgr.Operator.Name, false);
            this.neuSpread1.Sheets[i].Cells[0, 4].Tag = execBillMgr.Operator.ID.ToString();
            this.neuSpread1.Sheets[i].SetValue(0, 5, DateTime.Now.ToString(), false);
        }

        private void InitFp(FS.FrameWork.Models.NeuObject execBill, int i)
        {
            this.neuSpread1.Sheets.Count = i + 1;
            if (execBill.Name == "")
            {
                this.neuSpread1.Sheets[i].SheetName = " ";
            }
            else
            {
                this.neuSpread1.Sheets[i].SheetName = execBill.Name;
            }

            this.neuSpread1.Sheets[i].ActiveSkin = this.neuSpread1_Sheet1.ActiveSkin;
            this.neuSpread1.Sheets[i].Rows.Default.Height = this.neuSpread1_Sheet1.Rows.Default.Height;
            this.neuSpread1.Sheets[i].ColumnHeader.Rows[0].Height = this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Height;

            this.neuSpread1.Sheets[i].Columns[0].Visible = false;
            this.neuSpread1.Sheets[i].Columns[1].Label = "ҽ������";
            this.neuSpread1.Sheets[i].Columns[2].Label = "��Ŀ���";

            //��ҩƷ��Ŀ��ϸִ�е�
            if (FS.FrameWork.Function.NConvert.ToBoolean(execBill.Memo))
            {
                this.neuSpread1.Sheets[i].Columns[3].Label = "��Ŀ����";
            }
            else
            {
                this.neuSpread1.Sheets[i].Columns[3].Label = "���÷���";
            }
            this.neuSpread1.Sheets[i].Columns[4].Label = "��ǰ����Ա";
            this.neuSpread1.Sheets[i].Columns[5].Label = "����ʱ��";

            this.neuSpread1.Sheets[i].Columns[1].Width = 150;
            this.neuSpread1.Sheets[i].Columns[2].Width = 150;
            this.neuSpread1.Sheets[i].Columns[3].Width = 150;
            this.neuSpread1.Sheets[i].Columns[4].Width = 150;
            this.neuSpread1.Sheets[i].Columns[5].Width = 150;

            this.neuSpread1.Sheets[i].RowCount = 0;
            this.neuSpread1.Sheets[i].ColumnCount = 6;
            this.neuSpread1.Sheets[i].GrayAreaBackColor = Color.WhiteSmoke;
            this.neuSpread1.Sheets[i].OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;

            this.neuSpread1.ActiveSheetIndex = i;
            this.neuSpread1.ActiveSheet.Tag = execBill;

            this.neuSpread1.Sheets[i].SetColumnMerge(1, FarPoint.Win.Spread.Model.MergePolicy.Always);
        }

        /// <summary>
        /// �����ѱ����ִ�е�
        /// </summary>
        private void BindFp()
        {
            FS.HISFC.Models.Base.Employee person = (execBillMgr.Operator as FS.HISFC.Models.Base.Employee);
            string strNurse = person.Nurse.ID.ToString();
            alExecBill = execBillMgr.QueryExecBill(strNurse);

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            ArrayList arrDetail = new ArrayList();
            FS.HISFC.Models.Order.Inpatient.Order objDetail = null;
            for (int i = 0; i < alExecBill.Count; i++)
            {
                obj = alExecBill[i] as FS.FrameWork.Models.NeuObject;
                InitFp(obj, i);
                arrDetail = execBillMgr.QueryExecBillDetail(obj.ID);

                if (arrDetail != null)
                {
                    for (int j = 0; j < arrDetail.Count; j++)
                    {
                        objDetail = arrDetail[j] as FS.HISFC.Models.Order.Inpatient.Order;
                        AddExecBill(objDetail, i, FS.FrameWork.Function.NConvert.ToBoolean(obj.Memo));
                    }
                }
                icont = alExecBill.Count;
            }
        }

        #endregion

        #region ����

        public string ExeBillName
        {
            get
            {
                return strName;
            }
            set
            {
                strName = value;
            }
        }
        #endregion

        #region �¼�

        private void EventResultChanged(ArrayList al)
        {

        }

        private void PrintInfo()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPreview(this.neuPanel2);
        }

        private void ucSetExecBill_Load(object sender, EventArgs e)
        {
            this.InitControl();

            grpExecBillD.Visible = false;
            grpExecBillName.Visible = true;
            tabItemType.Visible = true;
            BindFp();
            grpExecBillD.Visible = true;

            if (this.neuSpread1.Sheets.Count > 0)
            {
                this.Filter();

                this.neuSpread1.ActiveSheetIndex = 0;
                this.lblExecBillName.Text = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].SheetName;

                if (this.neuSpread1.Sheets.Count == 1)
                {
                    this.SetTabVisible();
                }
            }
        }

        private void tvDrug_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.neuSpread1.Sheets.Count == 0) return;
            FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();
            if (this.tvDrug.SelectedNode != null && this.tvDrug.SelectedNode.Parent != null)
            {
                //Ҷ�ӽڵ�---���÷���
                if (this.tvDrug.SelectedNode.Parent.Parent != null && this.tvDrug.SelectedNode.Parent != null)
                {
                    //ArrayList alTree = new ArrayList();
                    obj.ID = "";//ִ�е�id
                    obj.Memo = "1";//ҩƷ��ҩƷ
                    obj.OrderType.ID = this.tvDrug.SelectedNode.Parent.Parent.Tag.ToString();//ҽ�����id
                    obj.OrderType.Name = this.tvDrug.SelectedNode.Parent.Parent.Text;//ҽ���������
                    //[xuweizhe]obj.Item.SysClass.ID = this.tvDrug.SelectedNode.Parent.Tag.ToString();//ϵͳ���
                    obj.Item.User01 = this.tvDrug.SelectedNode.Parent.Tag.ToString();
                    obj.Usage.ID = this.tvDrug.SelectedNode.Tag.ToString();//�÷�id
                    obj.Usage.Name = this.tvDrug.SelectedNode.Text;
                    AddExecBill(obj, this.neuSpread1.ActiveSheetIndex);
                    this.tvDrug.SelectedNode.Parent.Nodes.RemoveAt(this.tvDrug.SelectedNode.Index);
                }
                else if (this.tvDrug.SelectedNode.Parent != null)
                {
                    //ҩƷ���ͽڵ�
                    for (int i = this.tvDrug.SelectedNode.Nodes.Count - 1; i >= 0; i--)
                    {
                        obj.ID = "";//ִ�е�id
                        obj.Memo = "1";//ҩƷ��ҩƷ
                        obj.OrderType.ID = this.tvDrug.SelectedNode.Parent.Tag.ToString();//ҽ�����id
                        obj.OrderType.Name = this.tvDrug.SelectedNode.Parent.Text;//ҽ���������
                        //[xuweizhe]obj.Item.SysClass.ID = this.tvDrug.SelectedNode.Tag.ToString();//ϵͳ���
                        obj.Usage.ID = this.tvDrug.SelectedNode.Nodes[i].Tag.ToString();//�÷�id
                        obj.Usage.Name = this.tvDrug.SelectedNode.Nodes[i].Text;
                        obj.Item.User01 = this.tvDrug.SelectedNode.Tag.ToString();
                        AddExecBill(obj, this.neuSpread1.ActiveSheetIndex);
                        this.tvDrug.SelectedNode.Nodes[i].Remove();
                    }
                }
            }
        }

        /// <summary>
        /// ��ҩ��ִ�е�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvUndrug_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.neuSpread1.Sheets.Count == 0)
                return;

            FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();

            if (this.tvUndrug.SelectedNode.Parent != null)
            {
                //�÷��ڵ�
                //if (this.tvUndrug.SelectedNode.Parent.Parent != null && this.tvUndrug.SelectedNode.Parent != null)
                //{
                //    obj.ID = "";//ִ�е�id
                //    obj.Memo = "2";//ҩƷ��ҩƷ
                //    obj.OrderType.ID = this.tvUndrug.SelectedNode.Parent.Parent.Tag.ToString();//ҽ�����id
                //    obj.OrderType.Name = this.tvUndrug.SelectedNode.Parent.Parent.Text;//ҽ���������
                //    obj.Item.SysClass.ID = this.tvUndrug.SelectedNode.Parent.Tag.ToString();
                //    obj.Item.SysClass.Name = this.tvUndrug.SelectedNode.Parent.Text;
                //    obj.Usage.ID = this.tvUndrug.SelectedNode.Tag.ToString();//�÷�id
                //    obj.Usage.Name = this.tvUndrug.SelectedNode.Text;
                //    AddExecBill(obj, this.neuSpread1.ActiveSheetIndex);
                //    this.tvUndrug.SelectedNode.Parent.Nodes.RemoveAt(this.tvUndrug.SelectedNode.Index);
                //}
                //else if (this.tvUndrug.SelectedNode.Parent != null)
                //{
                //    if (this.tvUndrug.SelectedNode.Nodes.Count == 0)
                //    {
                //        return;
                //    }

                //    for (int i = this.tvUndrug.SelectedNode.Nodes.Count - 1; i >= 0; i--)
                //    {
                //        obj.ID = "";//ִ�е�id
                //        obj.Memo = "2";//ҩƷ��ҩƷ
                //        obj.OrderType.ID = this.tvUndrug.SelectedNode.Parent.Tag.ToString();//ҽ�����id
                //        obj.OrderType.Name = tvUndrug.SelectedNode.Parent.Text;//ҽ���������
                //        obj.Usage.ID = this.tvUndrug.SelectedNode.Nodes[i].Tag.ToString(); //�÷�ID
                //        obj.Usage.Name = this.tvUndrug.SelectedNode.Nodes[i].Text; //�÷�����
                //        obj.Item.SysClass.ID = tvUndrug.SelectedNode.Tag.ToString();//ϵͳ���
                //        AddExecBill(obj, this.neuSpread1.ActiveSheetIndex);
                //    }
                //    tvUndrug.SelectedNode.Parent.Nodes.RemoveAt(tvUndrug.SelectedNode.Index);
                //}


                ArrayList alTree = new ArrayList();

                obj.ID = "";//ִ�е�id
                obj.Memo = "2";//ҩƷ��ҩƷ
                obj.OrderType.ID = tvUndrug.SelectedNode.Parent.Tag.ToString();//ҽ�����id
                obj.OrderType.Name = tvUndrug.SelectedNode.Parent.Text;//ҽ���������
                obj.Item.SysClass.ID = tvUndrug.SelectedNode.Tag.ToString();//ϵͳ���
                AddExecBill(obj, neuSpread1.ActiveSheetIndex);
                tvUndrug.SelectedNode.Parent.Nodes.RemoveAt(tvUndrug.SelectedNode.Index);
            }
            else
            {
                for (int i = this.tvUndrug.SelectedNode.Nodes.Count - 1; i >= 0; i--)
                {
                    obj.ID = "";//ִ�е�id
                    obj.Memo = "2";//ҩƷ��ҩƷ
                    obj.OrderType.ID = this.tvUndrug.SelectedNode.Tag.ToString();//ҽ�����id
                    obj.OrderType.Name = tvUndrug.SelectedNode.Text;//ҽ���������
                    obj.Item.SysClass.ID = tvUndrug.SelectedNode.Nodes[i].Tag.ToString();//ϵͳ���

                    AddExecBill(obj, this.neuSpread1.ActiveSheetIndex);
                    this.tvUndrug.SelectedNode.Nodes[i].Remove();
                }
            }
        }

        private void neuSpread1_ActiveSheetChanging(object sender, FarPoint.Win.Spread.ActiveSheetChangingEventArgs e)
        {
            if (this.neuSpread1.ActiveSheet != null)
            {
                if (this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag == null)
                {
                    DialogResult result;
                    if (this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Rows.Count > 0)
                    {
                        result = MessageBox.Show("�����ѱ��޸�����δ���̣����ڱ�����", "ȷ��", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            if (SaveBill(ref errInfo) == -1)
                            {
                                MessageBox.Show(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        if (result == DialogResult.No)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
            this.lblExecBillName.Text = this.neuSpread1.Sheets[e.ActivatedSheetIndex].SheetName;
        }

        /// <summary>
        /// ɾ����ϸ
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private int DeleteBillDetail(int row)
        {
            try
            {
                //ҽ������
                string orderType = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Cells[row, 1].Text;
                //ϵͳ���
                string sysClass = this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Cells[row, 2].Text;
                //ʹ�÷���
                string usage = this.neuSpread1.Sheets[neuSpread1.ActiveSheetIndex].Cells[row, 3].Text;

                if (((FS.FrameWork.Models.NeuObject)this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag).Memo != "1")
                {
                    //��ǰҳ���ǵ���Ŀ����
                    //�ָ��������б���
                    if (this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Cells[row, 0].Text == "1")
                    {
                        foreach (TreeNode node in this.tvDrug.Nodes)
                        {
                            if (orderType == node.Text)
                            {
                                foreach (TreeNode childnode in node.Nodes)
                                {
                                    if (sysClass == childnode.Text)
                                    {
                                        TreeNode obj = new TreeNode(usage);
                                        obj.Tag = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Cells[row, 3].Tag.ToString();
                                        childnode.Nodes.Add(obj);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode node in this.tvUndrug.Nodes)
                        {
                            if (orderType == node.Text)
                            {
                                TreeNode obj = new TreeNode(sysClass);
                                obj.Tag = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Cells[row, 2].Tag.ToString();
                                node.Nodes.Add(obj);
                                break;
                            }
                        }
                    }
                }

                if (this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag != null)
                {
                    string exeBillID = ((FS.FrameWork.Models.NeuObject)this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag).ID;//ִ�е���

                    if (exeBillID != null && exeBillID != "")
                    {
                        FS.HISFC.BizLogic.Order.ExecBill billMgr = new FS.HISFC.BizLogic.Order.ExecBill();

                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        billMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                        try
                        {
                            FS.FrameWork.Models.NeuObject objBill = new FS.FrameWork.Models.NeuObject();
                            //������д��objBill.ID ִ�е���ˮ�ţ�objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������,
                            // objBill.user02��ҩϵͳ���ҩƷ���,objBill.user03 ҩƷ�÷���			
                            objBill.Name = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].SheetName.Trim();//ִ�е���		
                            objBill.ID = exeBillID;
                            objBill.Memo = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Cells[neuSpread1.ActiveSheet.ActiveRowIndex, 0].Text.Trim();//ִ�е����ͣ�
                            objBill.User01 = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Cells[neuSpread1.ActiveSheet.ActiveRowIndex, 1].Tag.ToString(); //ҽ������,
                            objBill.User02 = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Cells[neuSpread1.ActiveSheet.ActiveRowIndex, 2].Tag.ToString();//��ҩϵͳ���ҩƷ���,
                            objBill.User03 = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Cells[neuSpread1.ActiveSheet.ActiveRowIndex, 3].Tag.ToString();//ҩƷ�÷���

                            if (((FS.FrameWork.Models.NeuObject)this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag).Memo != "1")
                            {
                                //��ǰҳ���ǵ���Ŀ����
                                if (billMgr.DeleteExecBill(objBill) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    MessageBox.Show(billMgr.Err, "��ʾ");
                                    return -1;
                                }
                            }
                            else
                            {
                                //��ǰҳ�ǵ���Ŀ���ͣ�ɾ��һ����Ŀ
                                //��DataSet���д���
                                if (this.unDrugItemSelect != null)
                                {
                                    DataRow delItemRow = this.unDrugItemSelect.ucInputUndrug.DsUndrugItem.Tables[objBill.User01].NewRow();
                                    FS.HISFC.Models.Base.Item delItem = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Rows[row].Tag as FS.HISFC.Models.Base.Item;

                                    delItemRow["����"] = delItem.ID;
                                    delItemRow["����"] = delItem.Name;
                                    delItemRow["���"] = delItem.Specs;
                                    delItemRow["�۸�"] = delItem.Price;
                                    delItemRow["��λ"] = delItem.PriceUnit;
                                    delItemRow["���"] = delItem.SysClass.ID;
                                    delItemRow["������"] = delItem.SysClass.ID;
                                    delItemRow["ƴ����"] = delItem.SpellCode;
                                    delItemRow["�����"] = delItem.WBCode;
                                    delItemRow["�Զ�����"] = delItem.UserCode;

                                    this.unDrugItemSelect.ucInputUndrug.DsUndrugItem.Tables[objBill.User01].Rows.Add(delItemRow);
                                }
                                if (billMgr.DeleteExecBillOneItem(objBill) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                    MessageBox.Show(billMgr.Err, "��ʾ");
                                    return -1;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(ex.Message);
                            return -1;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1.ActiveSheet == null)
                return;
            if (this.neuSpread1.ActiveSheet.ActiveRow == null)
                return;

            DialogResult result;
            result = MessageBox.Show("�Ƿ�ɾ������", "ȷ��", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.DeleteBillDetail(this.neuSpread1.ActiveSheet.ActiveRowIndex);

                this.neuSpread1.ActiveSheet.Rows.Remove(neuSpread1.ActiveSheet.ActiveRowIndex, 1);
            }
        }

        #endregion

        /// <summary>
        /// �½�ִ�е�
        /// </summary>
        /// <returns></returns>
        public int AddNewBill()
        {
            try
            {
                cResult r = new cResult();
                r.TextChanged += new TextChangedHandler(this.EventResultChanged);
                r.al = GetFpSheet();

                ucBillAdd ba = new ucBillAdd(r);
                ba.alExecBill = this.alExecBill;
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ba);
                this.objExecBill = ba.objExecBill;
                if (r.Result1 != "")
                {
                    grpExecBillName.Visible = true;
                    tabItemType.Visible = true;
                    lblExecBillName.Text = r.Result1;
                    lblExecBillName.Tag = "Add";
                    InitFp(objExecBill, icont);
                    this.SetTabVisible();

                    icont++;
                    grpExecBillName.Visible = true;

                    grpExecBillD.Visible = true;
                }
                return 1;
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
                return -1;
            }
        }

        /// <summary>
        /// �༭ִ�е�   by huangchw 2012-09-04
        /// </summary>
        /// <returns></returns>
        public int EditBill()
        {
            if (this.lblExecBillName.Text.Trim() == "") return -1;
            try
            {
                cResult r = new cResult();
                r.TextChanged += new TextChangedHandler(this.EventResultChanged);
                r.al = GetFpSheet();
                r.Result1 = this.lblExecBillName.Text;
                
                if (this.neuSpread1.ActiveSheetIndex < 0)
                    return -1;

                if (this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag == null)  //�޷���ȡִ�е�ID
                {
                    MessageBox.Show("�޷���ȡִ�е�");
                    return -1;
                }

                ucBillEdit be = new ucBillEdit();
                be.alExecBill = this.alExecBill;
                int index = this.neuSpread1.ActiveSheetIndex;  //���浱ǰindex

                #region ��ֵ��ֵ
                FS.FrameWork.Models.NeuObject editExecBill = this.neuSpread1.ActiveSheet.Tag as FS.FrameWork.Models.NeuObject;
                be.setValues(editExecBill , r);
                #endregion

                FS.FrameWork.WinForms.Classes.Function.ShowControl(be);

                this.objExecBill = be.objExecBill;//��ȡ�޸ĺ��ִ�е�

                if (objExecBill != null)
                {
                    this.neuSpread1.ActiveSheet.SheetName = this.objExecBill.Name;
                    this.lblExecBillName.Text = this.objExecBill.Name;
                    this.lblExecBillName.Tag = "Edit";
                    
                    InitFp(objExecBill, icont - 1); //��������ҳ
                    this.SetTabVisible();
                    BindFp();
                    this.neuSpread1.ActiveSheetIndex = index;
                    grpExecBillName.Visible = true;
                    grpExecBillD.Visible = true;
                }
                return 1;
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
                return -1;
            }
        }

        /// <summary>
        /// ɾ������ִ�е�
        /// </summary>
        /// <returns></returns>
        public int DeleteAllBill()
        {
            try
            {
                if (this.lblExecBillName.Text.Trim() == "") return -1;
                if (this.neuSpread1.ActiveSheet == null) return -1;

                if (MessageBox.Show("�Ƿ�ɾ��ִ�е���" + this.lblExecBillName.Text + "��", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return -1;

                if (this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag != null)
                {
                    if (execBillMgr.DeleteExecBill(((FS.FrameWork.Models.NeuObject)this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag).ID) != -1)
                    {
                        MessageBox.Show("ɾ���ɹ���");
                    }
                    else
                    {
                        MessageBox.Show("ɾ��ʧ��!" + execBillMgr.Err);
                    }
                    this.neuSpread1.Sheets.RemoveAt(this.neuSpread1.ActiveSheetIndex);
                    icont--;


                    if (neuSpread1.ActiveSheet.Tag != null)
                    {
                        if (((FS.FrameWork.Models.NeuObject)this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag).Memo == "1")
                        {
                            //��Ŀ����ִ�е�
                            if (this.unDrugItemSelect != null)
                            {
                                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("�������³�ʼ����Ŀ�б���ȴ���"); ;
                                Application.DoEvents();
                                if (this.unDrugItemSelect.ucInputUndrug.GetUndrugDS() == -1)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    return -1;
                                }
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            }
                        }
                    }
                }
                else
                {
                    this.neuSpread1.Sheets.RemoveAt(this.neuSpread1.ActiveSheetIndex);
                    icont--;
                }
                this.RefreshList();
                if (this.neuSpread1.Sheets.Count > 0)
                {
                    this.Filter();

                    lblExecBillName.Text = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].SheetName;
                }
                return 1;
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
                MessageBox.Show(Error);
                return -1;
            }
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <returns></returns>
        public int Modify()
        {
            try
            {
                cResult r = new cResult();
                r.TextChanged += new TextChangedHandler(this.EventResultChanged);
                r.al = GetFpSheet();
                r.Result1 = lblExecBillName.Text;

                if (this.neuSpread1.ActiveSheetIndex < 0)
                    return -1;

                if (this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag == null)
                {
                    r.Result2 = "";
                }
                else
                {
                    r.Result2 = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].Tag.ToString();
                }
                ucBillAdd ba = new ucBillAdd();
                ba.alExecBill = this.alExecBill;
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ba);

                if (r.Result1 != "")
                {
                    lblExecBillName.Text = r.Result1;
                    this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].SheetName = r.Result1;
                    grpExecBillName.Visible = true;
                }
                else
                {
                    lblExecBillName.Text = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].SheetName;
                }
                return 1;
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
                MessageBox.Show(Error);
                return -1;
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (SaveBill(ref errInfo) < 0)
            {
                MessageBox.Show(errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            lblExecBillName.Text = this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].SheetName; 
            MessageBox.Show("����ɹ���");

            this.Filter();

            return 1;
        }

        #region ��ҩƷ��Ŀִ�е�
        FS.HISFC.Components.Order.Controls.ucUndrugItemSelect unDrugItemSelect = null;

        /// <summary>
        /// ��ҩƷ��Ŀ��ǩҳѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvUndrugItem_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.neuSpread1.Sheets.Count == 0)
            {
                return;
            }
            if (this.tvUndrugItem.SelectedNode == null)
            {
                return;
            }

            FS.FrameWork.Models.NeuObject execBill = this.neuSpread1.ActiveSheet.Tag as FS.FrameWork.Models.NeuObject;

            if (execBill == null) //|| !FS.FrameWork.Function.NConvert.ToBoolean(execBill.Memo)
            {
                MessageBox.Show("��ѡ���ҩƷ��Ŀִ�е���");
                return;
            }

            if (e.Node.Parent == null)
            {
                MessageBox.Show("��ѡ��ҽ����Ŀ���");
                return;
            }

            if (this.unDrugItemSelect == null)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ������ȴ���");
                Application.DoEvents();
                this.unDrugItemSelect = new ucUndrugItemSelect();
                FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                this.unDrugItemSelect.NurseID = empl.Nurse.ID;

                if (this.unDrugItemSelect.Init() == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                this.unDrugItemSelect.ItemAllUpdate += new ucUndrugItemSelect.AllItemHandle(unDrugItemSelect_ItemAllUpdate);
                this.unDrugItemSelect.ItemOtherInsert += new ucUndrugItemSelect.ItemHandle(unDrugItemSelect_ItemOtherInsert);
                this.unDrugItemSelect.ItemInsert += new ucUndrugItemSelect.ItemHandle(unDrugItemSelect_ItemInsert);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            this.unDrugItemSelect.BillNO = execBill.ID;
            this.unDrugItemSelect.MyOrderType = e.Node.Parent.Tag.ToString();
            this.unDrugItemSelect.MySysClass = e.Node.Tag.ToString();
            FS.FrameWork.WinForms.Classes.Function.ShowControl(this.unDrugItemSelect);

        }

        /// <summary>
        /// ѡ��ʣ����Ŀ�¼�
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        int unDrugItemSelect_ItemOtherInsert(ArrayList items)
        {
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in items)
            {
                this.AddExecBill(order, this.neuSpread1.ActiveSheetIndex, true);
            }
            return 0;
        }

        /// <summary>
        /// ѡ�񵥸�������Ŀ�¼�
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        int unDrugItemSelect_ItemInsert(ArrayList items)
        {
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in items)
            {
                this.AddExecBill(order, this.neuSpread1.ActiveSheetIndex, true);
            }
            return 0;
        }

        /// <summary>
        /// ѡ��ȫ����Ŀ�¼�
        /// </summary>
        /// <param name="orderType"></param>
        /// <param name="sysClass"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        int unDrugItemSelect_ItemAllUpdate(string orderType, string sysClass, ArrayList items)
        {
            FS.FrameWork.Models.NeuObject activeExecBill = this.neuSpread1.ActiveSheet.Tag as FS.FrameWork.Models.NeuObject;
            //�����
            ArrayList alOrder = new ArrayList();
            for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
            {
                if (i != this.neuSpread1.ActiveSheetIndex)
                {
                    FS.FrameWork.Models.NeuObject execBill = this.neuSpread1.Sheets[i].Tag as FS.FrameWork.Models.NeuObject;
                    if (execBill != null)
                    {
                        if (FS.FrameWork.Function.NConvert.ToBoolean(execBill.Memo))
                        {
                            for (int j = this.neuSpread1.Sheets[i].RowCount - 1; j >= 0; j--)
                            {
                                if (this.neuSpread1.Sheets[i].Cells[j, 1].Tag.ToString() == orderType)
                                {
                                    object obj = this.neuSpread1.Sheets[i].Cells[j, 2].Tag as object;
                                    if (obj.ToString() == sysClass)
                                    {
                                        FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                                        order.ID = activeExecBill.ID;
                                        order.Memo = "2";
                                        order.OrderType.ID = orderType;
                                        order.OrderType.Name = this.neuSpread1.Sheets[i].Cells[j, 1].Text;
                                        order.Item.SysClass.ID = sysClass;
                                        order.Item.ID = this.neuSpread1.Sheets[i].Cells[j, 3].Tag.ToString();
                                        order.Item.Name = this.neuSpread1.Sheets[i].Cells[j, 3].Text;
                                        alOrder.Add(order);
                                        this.neuSpread1.Sheets[i].Rows.Remove(j, 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            items.AddRange(alOrder);
            //��ӵ���ǰsheet
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in items)
            {
                this.AddExecBill(order, this.neuSpread1.ActiveSheetIndex, true);
            }
            return 0;
        }

        //sheet�任
        private void neuSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            this.SetTabVisible();
        }

        /// <summary>
        /// tabҳ�任
        /// </summary>
        private void SetTabVisible()
        {
            FS.FrameWork.Models.NeuObject execBill = this.neuSpread1.ActiveSheet.Tag as FS.FrameWork.Models.NeuObject;
            if (execBill != null)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(execBill.Memo))
                {
                    if (!this.tabItemType.TabPages.Contains(this.tabUndrugItem))
                    {
                        this.tabItemType.TabPages.Add(this.tabUndrugItem);
                    }
                    this.tabItemType.SelectedTab = this.tabUndrugItem;
                }
                else
                {
                    if (!this.tabItemType.TabPages.Contains(this.tabdrug))
                    {
                        this.tabItemType.TabPages.Add(this.tabdrug);
                    }

                    if (!this.tabItemType.TabPages.Contains(this.tabUndrag))
                    {
                        this.tabItemType.TabPages.Add(this.tabUndrag);
                    }
                    this.tabItemType.SelectedTab = this.tabdrug;
                }
            }
        }
        #endregion

        private void filetype_Click(object sender, EventArgs e)
        {
            if (this.filetype.Text == "���")
            {
                this.filetype.Text = "�÷�";
            }
            else
            {
                this.filetype.Text = "���";
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            txtFilter.Text = txtFilter.Text.Trim();
            if (!string.IsNullOrEmpty(txtFilter.Text))
            {
                if (filetype.Text == "���")
                {
                    //this.neuSpread1.ActiveSheet.DataSource
                }
                else
                {

                }
            }
        }
    }

    public delegate void TextChangedHandler(ArrayList s);

    public class cResult
    {
        public string Result1 = "";
        public string Result2 = "";

        public event TextChangedHandler TextChanged;
        public ArrayList al = new ArrayList();
        public void ChangeText(ArrayList al)
        {
            if (al != null)
                TextChanged(al);
        }
    }
}
