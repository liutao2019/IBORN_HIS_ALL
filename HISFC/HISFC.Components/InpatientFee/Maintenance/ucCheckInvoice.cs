using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;
namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    public partial class ucCheckInvoice : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCheckInvoice()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ��Ʊҵ���
        /// </summary>
        //{6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
        //protected FS.HISFC.BizLogic.Fee.InvoiceService invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceService();
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// ��Ʊ����
        /// </summary>
        /// 
        //{6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
        //FS.HISFC.Models.Fee.EnumInvoiceType enumType ;
        private string enumType = string.Empty;

        private string enumName = string.Empty; //���ڵ���excel
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private string begin = string.Empty;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private string end = string.Empty;
        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerInteger = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �տ�ԱID
        /// </summary>
        string casher = string.Empty;

        /// <summary>
        /// ���ؼ�ѡ����ʵ��
        /// </summary>
        object obj = null;
        #endregion

        #region ������
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ȫѡ", "��ȫ����������Ϊѡ��״̬", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ��", "��ȫ����������Ϊδѡ��״̬", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ȫѡ":
                    {
                        IsSelectAll(true);
                        break;
                    }
                case "ȡ��":
                    {
                        IsSelectAll(false);
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ����toolbarservice��export��add by zhy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            this.Exportinfo();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// �������ݳ�excel
        /// </summary>
        protected virtual void Exportinfo()
        {
            try
            {
                bool ret = false;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                //saveFileDialog1.Filter = "Excel |.xls";
                saveFileDialog1.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx|(*.txt)|*.txt|(*.dbf)|*.dbf";
                saveFileDialog1.Title = "��������";
                saveFileDialog1.FileName = enumName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                       ret = neuSpread1.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    }
                    if (ret)
                    {
                        MessageBox.Show("�ɹ���������");
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// �Ƿ�Farpoint��������Ϊѡ��״̬
        /// </summary>
        /// <param name="bl">true :ѡ�� false:δѡ��</param>
        protected virtual void IsSelectAll(bool bl)
        {
            int count = this.neuSpread1_Sheet1.Rows.Count;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = bl.ToString();
                }
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            InitTree();
            //��ʼ����Ա��Ϣ
            ArrayList al = managerInteger.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            if (al != null)
            {
                NeuObject obj = new NeuObject();
                obj.ID = "ALL";
                obj.Name = "ȫ��";
                al.Insert(0, obj);
                this.cbsky.AddItems(al);
                this.cbsky.Tag = "ALL";
            }
            
        }

        /// <summary>
        /// ��ʼ��TreeView
        /// </summary>
        private void InitTree()
        {
            if (tree.Nodes.Count > 0)
            {
                tree.Nodes.Clear();
            }
            TreeNode root = new TreeNode();
            root.Text = "��Ʊ����";
            root.ImageIndex = 2;
            tree.Nodes.Add(root);
            //{6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
            //ArrayList al = FS.HISFC.Models.Fee.InvoiceTypeEnumService.List();
            FS.HISFC.BizLogic.Manager.Constant myCont = new FS.HISFC.BizLogic.Manager.Constant ();
            ArrayList al = myCont.GetList("GetInvoiceType");
            if (al == null || al.Count == 0)
            {
                return;
            }
            TreeNode node ;
            foreach (NeuObject obj in al)
            {
                //{6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
                //if (obj.ID == FS.HISFC.Models.Fee.EnumInvoiceType.C.ToString() || obj.ID == FS.HISFC.Models.Fee.EnumInvoiceType.I.ToString())
                if (obj.ID == "C" || obj.ID == "I" || obj.ID == "R" || obj.ID == "P")
                {
                    node = new TreeNode();
                    node.Text = obj.Name;
                    node.Tag = obj;
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;
                    root.Nodes.Add(node);
                }
            }
            this.tree.ExpandAll();

        }
/// <summary>
/// ��ȡ�Һ�Ʊ��
/// </summary>
/// <param name="begin"></param>
/// <param name="end"></param>
/// <param name="casher"></param>
        private void GetOutpatientFeeRegInvoice(string begin, string end, string casher)
        {

         //   List<NeuObject> list = new List<NeuObject>();
            List<FS.HISFC.Models.Fee.Invoice> list = new List<FS.HISFC.Models.Fee.Invoice>();
            if (invoiceServiceManager.GetOutpatientFeeRegInvoice(begin, end, casher, ref list) == -1)
            {
                MessageBox.Show(invoiceServiceManager.Err);
                return;
            }
            int count = list.Count;
            this.neuSpread1_Sheet1.Rows.Count = count;
            #region ԭ���Ĵ���
            //for (int i = 0; i < count; i++)
            //{
            //    this.neuSpread1_Sheet1.Cells[i, 0].Text = "true";
            //    this.neuSpread1_Sheet1.Cells[i, 1].Text = list[i].ID;
            //    string[] myStrs=list[i].User03.Split('|');
            //    if (myStrs.Length > 1)
            //    {
            //        this.neuSpread1_Sheet1.Cells[i, 2].Text = myStrs[1];// ӡˢ��
            //    }
            //    else 
            //    {
            //        this.neuSpread1_Sheet1.Cells[i, 2].Text = list[i].User03;
            //    }
            //    //�����˽������У������ݴ���FS.FrameWork.Models.NeuObject ʵ���Name�ֶ�
            //    //Nameֵ���磺"��Ч|������"
            //    string[] strs = list[i].Name.Trim().Split('|');
            //    this.neuSpread1_Sheet1.Cells[i, 3].Text = strs[0];// list[i].Name;
            //    this.neuSpread1_Sheet1.Cells[i, 4].Text = list[i].Memo;
            //    //������
            //    if (strs.Length > 1)
            //    {
            //        this.neuSpread1_Sheet1.Cells[i, 5].Text = strs[1];
            //    }
            //    this.neuSpread1_Sheet1.Rows[i].Tag = list[i];
            //}
            #endregion
            for (int i = 0; i < count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = "true";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = list[i].ID;
                this.neuSpread1_Sheet1.Cells[i, 2].Text = list[i].Name;
                this.neuSpread1_Sheet1.Cells[i, 3].Text = list[i].User01;
                this.neuSpread1_Sheet1.Cells[i, 4].Text = list[i].Memo;
                this.neuSpread1_Sheet1.Cells[i, 5].Text = list[i].User02;
                this.neuSpread1_Sheet1.Cells[i, 6].Text = list[i].User03;
                this.neuSpread1_Sheet1.Cells[i, 7].Text = list[i].BeginNO;
                this.neuSpread1_Sheet1.Cells[i, 8].Text = list[i].EndNO;
                this.neuSpread1_Sheet1.Rows[i].Tag=list[i];
            }
        }
        /// <summary>
        /// ��ȡ���﷢Ʊ����
        /// </summary>
        private void GetOutpatientFeeInvoice(string begin,string end,string casher)
        {
            
        //    List<NeuObject> list = new List<NeuObject>();
             List<FS.HISFC.Models.Fee.Invoice> list=new List<FS.HISFC.Models.Fee.Invoice>();
           
            if (invoiceServiceManager.GetOutpatientFeeInvoice(begin, end,casher, ref list) == -1)
            {
                MessageBox.Show(invoiceServiceManager.Err);
                return;
            }
            int count = list.Count;
            this.neuSpread1_Sheet1.Rows.Count = count;
            #region ԭ���Ĵ���
            //for (int i = 0; i < count; i++)
            //{
            //    this.neuSpread1_Sheet1.Cells[i, 0].Text = "true";
            //    this.neuSpread1_Sheet1.Cells[i, 1].Text = list[i].ID;
            //    //�����˽������У������ݴ���FS.FrameWork.Models.NeuObject ʵ���Name�ֶ�
            //    //Nameֵ���磺"��Ч|������"
            //    string[] myStrs = list[i].User03.Split('|');
            //    if (myStrs.Length > 1)
            //    {
            //        this.neuSpread1_Sheet1.Cells[i, 2].Text = myStrs[1];
            //    }
            //    else
            //    {
            //        this.neuSpread1_Sheet1.Cells[i, 2].Text = list[i].User03;
            //    }

            //    string[] strs=list[i].Name.Trim().Split('|');
            //    this.neuSpread1_Sheet1.Cells[i, 3].Text = strs[0];// list[i].Name;
            //    this.neuSpread1_Sheet1.Cells[i, 4].Text = list[i].Memo;
            //    //������
            //    if (strs.Length>1)
            //    {
            //        this.neuSpread1_Sheet1.Cells[i, 5].Text = strs[1];                    
            //    }
            //    this.neuSpread1_Sheet1.Rows[i].Tag = list[i];
            //}
            #endregion
            for (int i = 0; i < count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = "true";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = list[i].ID;
                this.neuSpread1_Sheet1.Cells[i, 2].Text = list[i].Name;
                this.neuSpread1_Sheet1.Cells[i, 3].Text = list[i].User01;
                this.neuSpread1_Sheet1.Cells[i, 4].Text = list[i].Memo;
                this.neuSpread1_Sheet1.Cells[i, 5].Text = list[i].User02;
                this.neuSpread1_Sheet1.Cells[i, 6].Text = list[i].User03;
                this.neuSpread1_Sheet1.Cells[i, 7].Text = list[i].BeginNO;
              //  this.neuSpread1_Sheet1.Cells[i, 6].Text = list[i].;
            //   this.neuSpread1_Sheet1.Cells[i, 6].Text = list[i].User03;
             //   this.neuSpread1_Sheet1.Cells[i, 6].Text = list[i].BeginNO;
                this.neuSpread1_Sheet1.Cells[i, 8].Text = list[i].EndNO;
                this.neuSpread1_Sheet1.Rows[i].Tag = list[i];
            }
        }

        /// <summary>
        /// ��ȡסԺ��Ʊ����
        /// </summary>
        private void GetInpatientFeeInvoice(string begin, string end, string casher)
        {
            //List<NeuObject> list = new List<NeuObject>();
            List<FS.HISFC.Models.Fee.Invoice> list = new List<FS.HISFC.Models.Fee.Invoice>();
            if (invoiceServiceManager.GetInpatientFeeInvoice(begin, end,casher, ref list) == -1)
            {
                MessageBox.Show(invoiceServiceManager.Err);
                return;
            }
            int count = list.Count;
            this.neuSpread1_Sheet1.Rows.Count = count;
            for (int i = 0; i < count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = "true";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = list[i].ID;
                this.neuSpread1_Sheet1.Cells[i, 2].Text = list[i].Name;
                this.neuSpread1_Sheet1.Cells[i, 3].Text = list[i].User01;                
                this.neuSpread1_Sheet1.Cells[i, 4].Text = list[i].Memo;
                this.neuSpread1_Sheet1.Cells[i, 5].Text = list[i].User02;
                this.neuSpread1_Sheet1.Cells[i, 6].Text = list[i].User03;
                this.neuSpread1_Sheet1.Cells[i, 7].Text = list[i].BeginNO;
                this.neuSpread1_Sheet1.Cells[i, 8].Text = list[i].EndNO;
                this.neuSpread1_Sheet1.Rows[i].Tag = list[i];
            }
        }

        /// <summary>
        /// ��ȡԤ����Ʊ
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="casher"></param>
        private void GetInpatientPrepayInvoice(string begin, string end, string casher)
        {
          //  List<NeuObject> list = new List<NeuObject>();
            List<FS.HISFC.Models.Fee.Invoice> list = new List<FS.HISFC.Models.Fee.Invoice>();
            if (invoiceServiceManager.GetInpatientPrepayInvoice(begin, end, casher, ref list) == -1)
            {
                MessageBox.Show(invoiceServiceManager.Err);
                return;
            }
            int count = list.Count;
            this.neuSpread1_Sheet1.Rows.Count = count;
            for (int i = 0; i < count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = "true";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = list[i].ID;
                this.neuSpread1_Sheet1.Cells[i, 2].Text = list[i].Name;
                this.neuSpread1_Sheet1.Cells[i, 3].Text = list[i].User01;
                
                this.neuSpread1_Sheet1.Cells[i, 4].Text = list[i].Memo;
                this.neuSpread1_Sheet1.Cells[i, 5].Text = list[i].User02;
                this.neuSpread1_Sheet1.Cells[i, 6].Text = list[i].User03;
                this.neuSpread1_Sheet1.Cells[i, 7].Text = list[i].BeginNO;
                this.neuSpread1_Sheet1.Cells[i, 8].Text = list[i].EndNO;
                this.neuSpread1_Sheet1.Rows[i].Tag = list[i];
            }
        }

        //��ѯ����
        protected void Query()
        {
            //��ֹʱ��
            begin = this.dtBegin.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00";
            end = this.dtEnd.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59";

            string indexStr = (obj as NeuObject).ID;
            enumType = indexStr;
            //{6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
            //(FS.HISFC.Models.Fee.EnumInvoiceType)Enum.Parse(typeof(FS.HISFC.Models.Fee.EnumInvoiceType),indexStr);
            if (this.cbsky.Tag == null)
            {
                casher = "ALL";
            }
            else
            {
                casher = cbsky.Tag.ToString();
            }
            GetInvoiceData(begin, end, casher);
        }
        /// <summary>
        /// ��������
        /// </summary>
        protected virtual void Save()
        {
            if (this.tree.SelectedNode.Tag == null) return;
            int count=this.neuSpread1_Sheet1.Rows.Count;
            if (count == 0) return;
            //����Ա
            NeuObject oper = invoiceServiceManager.Operator;
            //����ʱ��
            string operTime = invoiceServiceManager.GetDateTimeFromSysDateTime().ToString();
            int resultValue = 0;
            //��ʼ����
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.invoiceServiceManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                for (int i = 0; i < count; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text.ToLower() == "false") continue;
                    NeuObject obj = this.neuSpread1_Sheet1.Rows[i].Tag as NeuObject;
                    //���﷢Ʊ����{6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
                    //if (enumType == FS.HISFC.Models.Fee.EnumInvoiceType.C)
                    if (enumType == "C")
                    {
                        resultValue=this.invoiceServiceManager.SaveCheckOutPatientFeeInvoice(obj, operTime.ToString(), oper.ID, begin, end);
                    }
                    //סԺ��Ʊ����{6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
                    //if (enumType == FS.HISFC.Models.Fee.EnumInvoiceType.I)
                    if (enumType == "I")
                    {
                        resultValue=this.invoiceServiceManager.SaveCheckInpatientFeeInvoice(obj, operTime.ToString(), oper.ID, begin, end);
                    }
                    if (resultValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������ʧ�ܣ�" + this.invoiceServiceManager.Err);
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�������ݳɹ���");

                this.GetInvoiceData(begin, end, casher);
            }
            catch(Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ���ط�Ʊ����
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void GetInvoiceData(string begin, string end,string casher)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ����������Ժ�^-^");
            Application.DoEvents();
            switch (enumType)
            {
                //���﷢Ʊ {6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
                //case FS.HISFC.Models.Fee.EnumInvoiceType.C:
                //    {
                //        GetOutpatientFeeInvoice(begin, end, casher);
                //        break;
                //    }
                //case FS.HISFC.Models.Fee.EnumInvoiceType.I:
                //    {
                //        GetInpatientFeeInvoice(begin, end, casher);
                //        break;
                //    }
                case "C":
                    {
                        GetOutpatientFeeInvoice(begin, end, casher);
                        break;
                    }
                case "I":
                    {
                        GetInpatientFeeInvoice(begin, end, casher);
                        break;
                    }
                case "R":
                    {
                        GetOutpatientFeeRegInvoice(begin, end, casher);
                        break;
                    }
                case "P":
                    {
                        GetInpatientPrepayInvoice(begin, end, casher);
                        break;
                    }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }



        #endregion

        #region �¼�
        private void ucCheckInvoice_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            obj = e.Node.Tag;
            if (obj == null) return;
            //��ֹʱ��
            begin = this.dtBegin.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00";
            end = this.dtEnd.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59";

            string indexStr = (obj as NeuObject).ID;
            string indexName = (obj as NeuObject).Name;
            enumType = indexStr;
            enumName = indexName;
            //{6A6FDD3F-ACBB-4ff7-80BC-6AD0FF69360E}
                //(FS.HISFC.Models.Fee.EnumInvoiceType)Enum.Parse(typeof(FS.HISFC.Models.Fee.EnumInvoiceType),indexStr);
            if (this.cbsky.Tag == null)
            {
                casher = "ALL";
            }
            else
            {
                casher = cbsky.Tag.ToString();
            }
            GetInvoiceData(begin, end, casher);
        }
        #endregion

        private void cbsky_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.casher = this.cbsky.Tag.ToString();
            begin = this.dtBegin.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00";
            end = this.dtEnd.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59";
            this.GetInvoiceData(this.begin, this.end, casher);
        }
    }
}
