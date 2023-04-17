using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FS.HISFC.Components.Manager.Items
{
    public partial class ucUnDrugItems : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ˽�г�Ա
        
        //private FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// ��ҩƷ��Ŀҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeItem = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ��ȡ��С�������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant cons = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ������ͼ,���ڹ���
        /// </summary>
        protected DataView dvUndrugItem = new DataView();

        /// <summary>
        /// �����ļ���·��
        /// </summary>
        private string filePath = Application.StartupPath + @".\profile\UndrugItemSet.xml";

        /// <summary>
        /// ��С���ô���
        /// </summary>
        private Hashtable htFeeType = new Hashtable();
        /// <summary>
        /// Managerҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ϵͳ���(ע��: ��-����, ֵ-ID),��Ϊ�˷���ʹ�����Բ�������,
        /// ��������,�Ǹ�Ҫ�� (���˺���) Ҳһ��ĵ�.
        /// </summary>
        private Hashtable htClassType = new Hashtable();
        /// <summary>
        /// ���Ʋ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlArguments = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// ����ID,NAME
        /// </summary>
        private Hashtable htExecDept = new Hashtable();

        private DataTable dataTable = new DataTable();
        private FS.FrameWork.Public.ObjectHelper applicabilityAreaHelp = new FS.FrameWork.Public.ObjectHelper();
        protected List<FS.HISFC.Models.Fee.Item.Undrug> undrugList = new List<FS.HISFC.Models.Fee.Item.Undrug>();

        #endregion


        public ucUnDrugItems()
        {
            InitializeComponent();
        }

        private void ucUnDrugItems_Load(object sender, EventArgs e)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�������...", false);
                Application.DoEvents();
                this.FpItems.DataSource = this.dataTable;


                CreateEmptyDS();
                FillMinFeeType();
                FillClassType();
                FillExecDept();
                FillFarPoint(/*CreateEmptyDS()*/);
                //this.FpItems.DataSource = this.dvUndrugItem;

                //FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.FpItems, this.filePath);
                this.cmbItemPriceType.AddItems(managerIntegrate.GetConstantList("ITEMPRICETYPE"));
                //{55CFCB36-B084-4a56-95AD-2CDED962ADC4}
                this.FpItems.Columns[47].AllowAutoFilter=true;
                this.FpItems.Columns[45].AllowAutoFilter = true;
                this.FpItems.Columns[16].AllowAutoFilter = true;
                if (!controlArguments.GetControlParam<bool>("B00001"))
                {
                    this.cmbItemPriceType.Visible = false;
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        /// <summary>
        /// ע�Ṥ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //return base.OnInit(sender, neuObject, param);
            this.toolBarService.AddToolButton("������", "����������ʾ�ؼ�����",FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            this.toolBarService.AddToolButton("���", "�������", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "��������", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            this.toolBarService.AddToolButton("��������", "�������ݵ�EXCEL�ļ�", FS.FrameWork.WinForms.Classes.EnumImageList.D����, true, false, null);
            this.toolBarService.AddToolButton("������Ŀ����", "������Ŀ", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);//{C5A4CFD7-EBDA-4908-9330-16D2E39A8435}
            return this.toolBarService;
        }

        
        /// <summary>
        /// ����ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //base.ToolStrip_ItemClicked(sender, e);
            switch (e.ClickedItem.Text.Trim())
            {
                case "������":
                    SetColumn();
                    break;
                case "���":
                    AddItem();
                    break;
                case "ɾ��":
                    DeleteRow();
                    break;
                case "��������":
                    ExportToExcel();
                    break;
                case "������Ŀ����"://{C5A4CFD7-EBDA-4908-9330-16D2E39A8435}
                    AddMaterialItem();
                    break;
                default: break;
            }

        }
        
        #region ��������ť������

        /// <summary>
        /// ������:"������"��ť�Ĵ�����
        /// </summary>
        private void SetColumn()
        {
            FS.HISFC.Components.Common.Controls.ucSetColumn uc = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            //uc.DisplayEvent += new EventHandler(uc_DisplayEvent);
            uc.SetDataTable(this.filePath, this.FpItems);
            uc.DisplayEvent += new EventHandler(uc_DisplayEvent);

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }

        void uc_DisplayEvent(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.FpItems, this.filePath);
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�������...", false);
            Application.DoEvents();
            CreateEmptyDS();
            FillFarPoint(/*CreateEmptyDS()*/);
            //this.FpItems.DataSource = this.dvUndrugItem;

            //FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.FpItems, this.filePath);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void AddMaterialItem()//{C5A4CFD7-EBDA-4908-9330-16D2E39A8435}
        {
            Forms.ucMaterialItems uc = new Forms.ucMaterialItems();
            uc.ShowDialog();
        }

        /// <summary>
        /// ������:"���"��ť�Ĵ�����
        /// </summary>
        private void AddItem()
        {
            ucHandleItems handleItem = new ucHandleItems(true);
            handleItem.InsertSuccessed += new InsertSuccessHandler(handleItem_InsertSuccessed);
            handleItem.IsAddLine = true;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(handleItem);
        }

        /// <summary>
        /// ������:"ɾ��"��ť�Ĵ�����
        /// </summary>
        private void DeleteRow()
        {
            if (MessageBox.Show("�Ƿ�ɾ������Ŀ,ɾ���󽫲��ָܻ�", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            int index = this.FpItems.ActiveRow.Index;
            string code = this.FpItems.GetText(index, this.GetCloumn("��Ŀ���"));

            //if(this.item.IsUsed(code))
            if (this.feeItem.IsUsed(code))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ŀ�Ѿ�ʹ��,����ɾ��!"), "��Ϣ");
                return;
            }
            //if (this.item.DeleteUndrugItemByCode(code) <= 0)
            if( this.feeItem.DeleteUndrugItemByCode(code) <= 0 ) 
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ŀɾ��ʧ��!"), "��Ϣ");
                return;
            }
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ŀɾ���ɹ�!"), "��Ϣ");
            this.Delete();
        }

        /// <summary>
        /// ������:"����"��ť�������
        /// </summary>
        private void ExportToExcel()
        {
            if (this.FpItems.Rows.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û��Ҫ���������!"), "��Ϣ");
                return;
            }
            if (this.neuSpreadItems.Export() == 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ݵ����ɹ�!"), "��Ϣ");
            }
        }

        #endregion

        #region ˽�и�������

        /// <summary>
        /// ����DataSet
        /// </summary>
        /// <returns></returns>
        private /*DataSet*/void CreateEmptyDS()
        {
            //DataSet ds = new DataSet();

            #region ����DataTable
            //DataTable dt = new DataTable();


            if (System.IO.File.Exists(this.filePath))
            {
                this.dataTable = new DataTable();
                //FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePath, this.dataTable, ref this.dvUndrugItem, this.FpItems);
                //FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.FpItems, this.filePath);
                //this.CreateTable();
                XmlDocument doc = new XmlDocument();
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(this.filePath, System.Text.Encoding.Default);
                    string streamXml = sr.ReadToEnd();
                    sr.Close();
                    doc.LoadXml(streamXml);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡXml�����ļ��������� ���������ļ��Ƿ���ȷ") + ex.Message);
                    return;
                }

                XmlNodeList nodes = doc.SelectNodes("//Column");

                string tempString = "";

                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["type"].Value == "TextCellType" || node.Attributes["type"].Value == "ComboBoxCellType")
                    {
                        tempString = "System.String";
                    }
                    else if (node.Attributes["type"].Value == "CheckBoxCellType")
                    {
                        tempString = "System.Boolean";
                    }
                    this.dataTable.Columns.Add(new DataColumn(node.Attributes["displayname"].Value,
                        System.Type.GetType(tempString)));
                }

                //this.FpItems.DataAutoHeadings = true;

                //this.dvUndrugItem = new DataView(this.dataTable);
                //this.FpItems.DataSource = this.dataTable;

                //FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.FpItems, this.filePath);
            }
            else
            {
                #region ��ʱ����
                //this.dataTable = new DataTable();
                //this.dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("��Ŀ���", typeof(string)),
                //                                   new DataColumn("��Ŀ����", typeof(string)),
                //                                   new DataColumn("ϵͳ���", typeof(string)),
                //                                   new DataColumn("���ô���", typeof(string)),
                //                                   new DataColumn("������", typeof(string)),
                //                                   new DataColumn("ƴ����", typeof(string)),
                //                                   new DataColumn("�����", typeof(string)),
                //                                   new DataColumn("���ұ���", typeof(string)),
                //                                   new DataColumn("���ʱ���", typeof(string)),
                //                                   new DataColumn("Ĭ�ϼ�", typeof(string)),
                //                                   new DataColumn("��λ", typeof(string)),
                //                                   new DataColumn("����ӳɱ���", typeof(string)),
                //                                   new DataColumn("�ƻ��������", typeof(string)),
                //                                   new DataColumn("�ض�������Ŀ", typeof(string)),
                //                                   new DataColumn("�������־", typeof(string)),
                //                                   new DataColumn("ȷ�ϱ�־", typeof(string)),
                //                                   new DataColumn("��Ч�Ա�ʶ", typeof(string)),
                //                                   new DataColumn("���", typeof(string)),
                //                                   new DataColumn("ִ�п���", typeof(string)),
                //                                   new DataColumn("�豸���", typeof(string)),
                //                                   new DataColumn("�걾", typeof(string)),
                //                                   new DataColumn("��������", typeof(string)),
                //                                   new DataColumn("��������", typeof(string)),
                //                                   new DataColumn("������ģ", typeof(string)),
                //                                   new DataColumn("�Ƿ����", typeof(string)),
                //                                   new DataColumn("��ע", typeof(string)),
                //                                   new DataColumn("��ͯ��", typeof(string)),
                //                                   new DataColumn("�����", typeof(string)),
                //                                   new DataColumn("ʡ����", typeof(string)),
                //                                   new DataColumn("������", typeof(string)),
                //                                   new DataColumn("�Է���Ŀ", typeof(string)),
                //                                   new DataColumn("�����ʶ1", typeof(string)),
                //                                   new DataColumn("�����ʶ2", typeof(string)),
                //                                   new DataColumn("����1", typeof(string)),
                //                                   new DataColumn("����2", typeof(string)),
                //                                   new DataColumn("��������", typeof(string)),
                //                                   new DataColumn("ר������", typeof(string)),
                //                                   new DataColumn("��ʷ�����", typeof(string)),
                //                                   new DataColumn("���Ҫ��", typeof(string)),
                //                                   new DataColumn("ע������", typeof(string)),
                //                                   new DataColumn("֪��ͬ����", typeof(string)),
                //                                   new DataColumn("������뵥����", typeof(string)),
                //                                   new DataColumn("�Ƿ���ҪԤԼ", typeof(string)),
                //                                   new DataColumn("��Ŀ��Χ", typeof(string)),
                //                                   new DataColumn("��ĿԼ��", typeof(string)),
                //                                   new DataColumn("�Ƿ�����", typeof(string))
                //                                  });
                #endregion

                this.CreateTable();
                this.FpItems.DataSource = this.dataTable;
                //this.dvUndrugItem = new DataView(this.dataTable);
                //this.FpItems.DataSource = this.dvUndrugItem;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.FpItems, this.filePath);
            }


            //FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.FpItems, this.filePath);
            //this.dvUndrugItem.Table = this.dataTable;
            //this.dvUndrugItem.Sort = "��Ŀ��� DESC";
            //FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(FpItems, filePath);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = this.dataTable.Columns["��Ŀ���"];
            this.dataTable.PrimaryKey = keys;
            #endregion
        }

        /// <summary>
        /// ���"��С���ô���"�ؼ�
        /// </summary>
        private void FillMinFeeType()
        {
            ArrayList alMinFee = cons.GetAllList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (alMinFee == null)
            {
                return;
            }
            for (int i = 0, j = alMinFee.Count; i < j; i++)
            {
                htFeeType.Add(((FS.FrameWork.Models.NeuObject)alMinFee[i]).ID, ((FS.FrameWork.Models.NeuObject)alMinFee[i]).Name);
            }

            this.cbFeeType.AddItems(alMinFee);
            ArrayList applicabilityArea = cons.GetAllList("APPLICABILITYAREA");
            if (applicabilityArea == null)
            {
                MessageBox.Show("��ȡ���÷�Χʧ�� " + cons.Err);
            }
            applicabilityAreaHelp.ArrayObject = applicabilityArea; 
            //DictionaryEntry de;
            //IEnumerator en=this.htFeeType.GetEnumerator();
            //while (en.MoveNext())
            //{
            //    de = (DictionaryEntry)en.Current;
            //    this.cbFeeType.Items.Add(de.Value.ToString());
            //}
        }

        /// <summary>
        /// ���"ϵͳ���"�ؼ�
        /// </summary>
        private void FillClassType()
        {
            ArrayList alClassType = FS.HISFC.Models.Base.SysClassEnumService.List();
            if (alClassType == null)
            {
                return;
            }

            for (int i = 0, j = alClassType.Count; i < j; i++)
            {
                htClassType.Add(((FS.FrameWork.Models.NeuObject)alClassType[i]).ID, ((FS.FrameWork.Models.NeuObject)alClassType[i]).Name);
            }

            this.cbClassType.AddItems(alClassType);
        }

        /// <summary>
        /// ��ȡ����ִ�п���
        /// </summary>
        private void FillExecDept()
        {
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();

            ArrayList alExecDept = dept.GetDeptmentAll();

            if (alExecDept == null)
            {
                return;
            }

            for (int i = 0, j = alExecDept.Count; i < j; i++)
            {
                this.htExecDept.Add(((FS.FrameWork.Models.NeuObject)alExecDept[i]).ID, ((FS.FrameWork.Models.NeuObject)alExecDept[i]).Name);
            }
        }

        /// <summary>
        /// ����ָ��������,�õ���Ӧ��λ������
        /// </summary>
        /// <param name="name">����</param>
        /// <returns>>=0:λ������,  -1:ʧ��</returns>
        protected int GetCloumn(string name)
        {
            for (int i = 0; i < this.FpItems.Columns.Count; i++)
            {
                if (name == this.FpItems.Columns[i].Label)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// ��ʼ��FarPoint
        /// </summary>
        /// <param name="ds">���ݼ�</param>
        private void FillFarPoint(/*DataSet ds*/)
        {
            //ArrayList alItems = this.feeItem.QueryValidItems();

            List<FS.HISFC.Models.Fee.Item.Undrug> itemList = this.feeItem.QueryAllItemsList();

            if (itemList == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ��Ŀʧ��!"));
                return;
            }

            this.undrugList = itemList;

            if (this.FpItems.Rows.Count > 0)
            {
                this.FpItems.Rows.Remove(0, this.FpItems.Rows.Count);
            }

            #region
            
            for (int i = 0, j = itemList.Count; i < j; i++)
            {
                //FS.HISFC.Models.Fee.Item.Undrug undrug = (FS.HISFC.Models.Fee.Item.Undrug)alItems[i];
                FS.HISFC.Models.Fee.Item.Undrug undrug = itemList[i];
                //DataRow dr = ds.Tables[0].NewRow();
                DataRow dr;
                if (this.dataTable.Rows.Count > 0)
                {
                    if (this.dataTable.Rows.Find(undrug.ID.ToString()) == null)
                    {
                        dr = this.dataTable.NewRow();
                    }
                    else
                    {
                        throw new Exception("�����ظ�");
                    }
                }
                else
                {
                    dr = this.dataTable.NewRow();
                }

                dr["��Ŀ���"] = undrug.ID;
                dr["��Ŀ����"] = undrug.Name;

                dr["ϵͳ���"] = undrug.SysClass.Name;
                try
                {
                    dr["ִ�п���"] = undrug.ExecDept == "" ? "" : this.htExecDept[undrug.ExecDept].ToString();
                }
                catch
                {
                    dr["ִ�п���"] = "";
                }

                try
                {
                    dr["���ô���"] = this.htFeeType[undrug.MinFee.ID].ToString();//
                }
                catch
                {
                    dr["���ô���"] = "";
                }

                dr["������"] = undrug.UserCode;
                dr["ƴ����"] = undrug.SpellCode;
                dr["�����"] = undrug.WBCode;
                dr["���ұ���"] = undrug.GBCode;
                dr["���ʱ���"] = undrug.NationCode;
                dr["Ĭ�ϼ�"] = undrug.Price;
                dr["��λ"] = undrug.PriceUnit;
                dr["����ӳɱ���"] = undrug.FTRate.EMCRate.ToString();
                dr["�ƻ��������"] = undrug.IsFamilyPlanning ? "��" : "��";

                //���Ҳ������,�Ժ�������
                dr["�ض�������Ŀ"] = undrug.User01;

                //û��ת��������ֻ����ʾ1��2
                switch (undrug.Grade)
                {
                    case "1":
                        dr["�������־"] = "��";
                        break;
                    case "2":
                        dr["�������־"] = "��";
                        break;
                    case "3":
                        dr["�������־"] = "��";
                        break;
                    default:
                        dr["�������־"] = "";
                        break;
                }
                //dr["�������־"] = undrug.Grade;

                dr["ȷ�ϱ�־"] = undrug.IsNeedConfirm ? "��Ҫ" : "����Ҫ";
                switch (undrug.ValidState)
                {
                    case "0":
                        dr["��Ч�Ա�ʶ"] = "ͣ��";
                        break;
                    case "1":
                        dr["��Ч�Ա�ʶ"] = "����";
                        break;
                    case "2":
                        dr["��Ч�Ա�ʶ"] = "����";
                        break;
                    default:
                        dr["��Ч�Ա�ʶ"] = "";
                        break;
                }
                dr["���"] = undrug.Specs;
                dr["�豸���"] = undrug.MachineNO;
                dr["�걾"] = undrug.CheckBody;
                dr["��������"] = undrug.OperationInfo.ID;
                dr["��������"] = undrug.OperationType.ID;
                dr["������ģ"] = undrug.OperationScale.ID;
                dr["�Ƿ����"] = undrug.IsCompareToMaterial ? "��" : "û��";
                dr["��ע"] = undrug.Memo;
                dr["��ͯ��"] = undrug.ChildPrice.ToString();
                dr["�����"] = undrug.SpecialPrice.ToString();
                switch (undrug.SpecialFlag)
                {
                    case "0":
                        dr["ʡ����"] = "������";
                        break;
                    case "1":
                        dr["ʡ����"] = "����";
                        break;
                    default:
                        dr["ʡ����"] = "";
                        break;
                }
                switch (undrug.SpecialFlag1)
                {
                    case "0":
                        dr["������"] = "������";
                        break;
                    case "1":
                        dr["������"] = "����";
                        break;
                    default:
                        dr["������"] = "";
                        break;
                }
                switch (undrug.SpecialFlag2)
                {
                    case "0":
                        dr["�Է���Ŀ"] = "����";
                        break;
                    case "1":
                        dr["�Է���Ŀ"] = "��";
                        break;
                    default:
                        dr["�Է���Ŀ"] = "";
                        break;
                }
                switch (undrug.SpecialFlag3)
                {
                    case "0":
                        dr["�����ʶ1"] = "����";
                        break;
                    case "1":
                        dr["�����ʶ1"] = "��";
                        break;
                    default:
                        dr["�����ʶ1"] = "";
                        break;
                }
                switch (undrug.SpecialFlag4)
                {
                    case "0":
                        dr["�����ʶ2"] = "����";
                        break;
                    case "1":
                        dr["�����ʶ2"] = "��";
                        break;
                    default:
                        dr["�����ʶ2"] = "";
                        break;
                }

                //��û����,���ƻ�������
                dr["����1"] = undrug.User02;

                //��û����,���ƻ�������
                dr["����2"] = undrug.User03;

                dr["��������"] = undrug.DiseaseType.ID;
                dr["ר������"] = undrug.SpecialDept.ID;
                dr["��ʷ�����"] = undrug.MedicalRecord;
                dr["���Ҫ��"] = undrug.CheckRequest;
                dr["ע������"] = undrug.Notice;
                dr["֪��ͬ����"] = undrug.IsConsent ? "��Ҫ" : "����Ҫ";
                dr["������뵥����"] = undrug.CheckApplyDept;
                dr["�Ƿ���ҪԤԼ"] = undrug.IsNeedBespeak ? "��Ҫ" : "����Ҫ";
                dr["��Ŀ��Χ"] = undrug.ItemArea;
                dr["��ĿԼ��"] = undrug.ItemException;
                dr["���÷�Χ"] = this.applicabilityAreaHelp.GetName(undrug.ApplicabilityArea);
                /*�Ƿ�����(0,��ϸ; 1,����)[2007/01/01  xuweizhe]*/
                dr["�Ƿ�����"] = undrug.UnitFlag == "1" ? "��" : "��";
                dr["��۷������"] = undrug.ItemPriceType;
                dr["������"] = undrug.Oper.ID;
                dr["ͣ��ʱ��"] = undrug.Oper.OperTime;
                this.dataTable.Rows.Add(dr);
            }
            #endregion

            this.dataTable.AcceptChanges();
            //this.dvUndrugItem.Sort = "��Ŀ��� DESC";
            this.dvUndrugItem = this.dataTable.DefaultView;

            this.dvUndrugItem.Sort = "������ ASC,��Ŀ��� ASC";
            this.FpItems.DataSource = this.dvUndrugItem;
            this.FpItems.Columns[31].Visible = false;
            this.FpItems.Columns[32].Visible = false;
            this.FpItems.Columns[33].Visible = false;
            this.FpItems.Columns[34].Visible = false;
            //this.FpItems.DataSource = this.dvUndrugItem;
        }

        /// <summary>
        /// ���³ɹ�ʱ�¼���������е��õĺ���
        /// </summary>
        /// <param name="undrug">��ҩƷʵ��</param>
        private void Update(FS.HISFC.Models.Fee.Item.Undrug undrug)
        {
            this.dvUndrugItem.Sort = "������ ASC,��Ŀ��� ASC";
            //�ڵ���DataView�����FindRows����֮ǰ����Ҫ����DataView�����Sort���ԣ���ΪFindRows�����Ǹ���Sort��������ָ����������������
            //DataRowView[] drvs = this.dvUndrugItem.FindRows(undrug.ID);
            DataRow[] drvs = this.dvUndrugItem.Table.Select("��Ŀ���='" + undrug.ID + "'");
            this.dvUndrugItem.AllowEdit = true;
            drvs[0].BeginEdit();

            drvs[0]["��Ŀ���"] = undrug.ID;
            drvs[0]["��Ŀ����"] = undrug.Name;

            drvs[0]["ϵͳ���"] = undrug.SysClass.Name;
            drvs[0]["ִ�п���"] = undrug.ExecDept == "" ? "" : this.htExecDept[undrug.ExecDept].ToString();
            drvs[0]["���ô���"] = this.htFeeType[undrug.MinFee.ID].ToString();//

            drvs[0]["������"] = undrug.UserCode;
            drvs[0]["ƴ����"] = undrug.SpellCode;
            drvs[0]["�����"] = undrug.WBCode;
            drvs[0]["���ұ���"] = undrug.GBCode;
            drvs[0]["���ʱ���"] = undrug.NationCode;
            drvs[0]["Ĭ�ϼ�"] = undrug.Price;
            drvs[0]["��λ"] = undrug.PriceUnit;
            drvs[0]["����ӳɱ���"] = undrug.FTRate.EMCRate.ToString();
            drvs[0]["�ƻ��������"] = undrug.IsFamilyPlanning ? "��" : "��";

            //���Ҳ������,�Ժ�������
            drvs[0]["�ض�������Ŀ"] = undrug.User01;

            //û��ת��������ֻ����ʾ1��2
            switch (undrug.Grade)
            {
                case "1":
                    drvs[0]["�������־"] = "��";
                    break;
                case "2":
                    drvs[0]["�������־"] = "��";
                    break;
                case "3":
                    drvs[0]["�������־"] = "��";
                    break;
                default:
                    drvs[0]["�������־"] = "";
                    break;
            }
            //dr["�������־"] = undrug.Grade;

            drvs[0]["ȷ�ϱ�־"] = undrug.IsNeedConfirm ? "��Ҫ" : "����Ҫ";
            switch (undrug.ValidState)
            {
                case "0":
                    drvs[0]["��Ч�Ա�ʶ"] = "ͣ��";
                    break;
                case "1":
                    drvs[0]["��Ч�Ա�ʶ"] = "����";
                    break;
                case "2":
                    drvs[0]["��Ч�Ա�ʶ"] = "����";
                    break;
                default:
                    drvs[0]["��Ч�Ա�ʶ"] = "";
                    break;
            }
            drvs[0]["���"] = undrug.Specs;
            drvs[0]["�豸���"] = undrug.MachineNO;
            drvs[0]["�걾"] = undrug.CheckBody;
            drvs[0]["��������"] = undrug.OperationInfo.ID;
            drvs[0]["��������"] = undrug.OperationType.ID;
            drvs[0]["������ģ"] = undrug.OperationScale.ID;
            drvs[0]["�Ƿ����"] = undrug.IsCompareToMaterial ? "��" : "û��";
            drvs[0]["��ע"] = undrug.Memo;
            drvs[0]["��ͯ��"] = undrug.ChildPrice.ToString();
            drvs[0]["�����"] = undrug.SpecialPrice.ToString();
            switch (undrug.SpecialFlag)
            {
                case "0":
                    drvs[0]["ʡ����"] = "������";
                    break;
                case "1":
                    drvs[0]["ʡ����"] = "����";
                    break;
                default:
                    drvs[0]["ʡ����"] = "";
                    break;
            }
            switch (undrug.SpecialFlag1)
            {
                case "0":
                    drvs[0]["������"] = "������";
                    break;
                case "1":
                    drvs[0]["������"] = "����";
                    break;
                default:
                    drvs[0]["������"] = "";
                    break;
            }
            switch (undrug.SpecialFlag2)
            {
                case "0":
                    drvs[0]["�Է���Ŀ"] = "����";
                    break;
                case "1":
                    drvs[0]["�Է���Ŀ"] = "��";
                    break;
                default:
                    drvs[0]["�Է���Ŀ"] = "";
                    break;
            }
            switch (undrug.SpecialFlag3)
            {
                case "0":
                    drvs[0]["�����ʶ1"] = "����";
                    break;
                case "1":
                    drvs[0]["�����ʶ1"] = "��";
                    break;
                default:
                    drvs[0]["�����ʶ1"] = "";
                    break;
            }
            switch (undrug.SpecialFlag4)
            {
                case "0":
                    drvs[0]["�����ʶ2"] = "����";
                    break;
                case "1":
                    drvs[0]["�����ʶ2"] = "��";
                    break;
                default:
                    drvs[0]["�����ʶ2"] = "";
                    break;
            }

            //��û����,���ƻ�������
            drvs[0]["����1"] = undrug.User02;

            //��û����,���ƻ�������
            drvs[0]["����2"] = undrug.User03;

            drvs[0]["��������"] = undrug.DiseaseType.ID;
            drvs[0]["ר������"] = undrug.SpecialDept.ID;
            drvs[0]["��ʷ�����"] = undrug.MedicalRecord;
            drvs[0]["���Ҫ��"] = undrug.CheckRequest;
            drvs[0]["ע������"] = undrug.Notice;
            drvs[0]["֪��ͬ����"] = undrug.IsConsent ? "��Ҫ" : "����Ҫ";
            drvs[0]["������뵥����"] = undrug.CheckApplyDept;
            drvs[0]["�Ƿ���ҪԤԼ"] = undrug.IsNeedBespeak ? "��Ҫ" : "����Ҫ";
            drvs[0]["��Ŀ��Χ"] = undrug.ItemArea;
            drvs[0]["��ĿԼ��"] = undrug.ItemException;
            drvs[0]["�Ƿ�����"] = undrug.UnitFlag == "1" ? "��" : "��";
            drvs[0]["���÷�Χ"] = this.applicabilityAreaHelp.GetName(undrug.ApplicabilityArea);
            drvs[0].EndEdit();
        }

        /// <summary>
        /// ����ɹ�ʱ�¼���������е��õĺ���
        /// </summary>
        /// <param name="undrug">��ҩƷʵ��</param>
        private void Insert(FS.HISFC.Models.Fee.Item.Undrug undrug)
        {
            this.dvUndrugItem.Sort = "������ ASC,��Ŀ��� ASC";
            DataRowView drv = this.dvUndrugItem.AddNew();

            drv["��Ŀ���"] = undrug.ID;
            drv["��Ŀ����"] = undrug.Name;

            drv["ϵͳ���"] = undrug.SysClass.Name;
            drv["ִ�п���"] = undrug.ExecDept == "" ? "" : this.htExecDept[undrug.ExecDept].ToString();
            drv["���ô���"] = this.htFeeType[undrug.MinFee.ID].ToString();//

            drv["������"] = undrug.UserCode;
            drv["ƴ����"] = undrug.SpellCode;
            drv["�����"] = undrug.WBCode;
            drv["���ұ���"] = undrug.GBCode;
            drv["���ʱ���"] = undrug.NationCode;
            drv["Ĭ�ϼ�"] = undrug.Price;
            drv["��λ"] = undrug.PriceUnit;
            drv["����ӳɱ���"] = undrug.FTRate.EMCRate.ToString();
            drv["�ƻ��������"] = undrug.IsFamilyPlanning ? "��" : "��";

            //���Ҳ������,�Ժ�������
            drv["�ض�������Ŀ"] = undrug.User01;

            //û��ת��������ֻ����ʾ1��2
            switch (undrug.Grade)
            {
                case "1":
                    drv["�������־"] = "��";
                    break;
                case "2":
                    drv["�������־"] = "��";
                    break;
                case "3":
                    drv["�������־"] = "��";
                    break;
                default:
                    drv["�������־"] = "";
                    break;
            }
            //dr["�������־"] = undrug.Grade;

            drv["ȷ�ϱ�־"] = undrug.IsNeedConfirm ? "��Ҫ" : "����Ҫ";
            switch (undrug.ValidState)
            {
                case "0":
                    drv["��Ч�Ա�ʶ"] = "ͣ��";
                    break;
                case "1":
                    drv["��Ч�Ա�ʶ"] = "����";
                    break;
                case "2":
                    drv["��Ч�Ա�ʶ"] = "����";
                    break;
                default:
                    drv["��Ч�Ա�ʶ"] = "";
                    break;
            }
            drv["���"] = undrug.Specs;
            drv["�豸���"] = undrug.MachineNO;
            drv["�걾"] = undrug.CheckBody;
            drv["��������"] = undrug.OperationInfo.ID;
            drv["��������"] = undrug.OperationType.ID;
            drv["������ģ"] = undrug.OperationScale.ID;
            drv["�Ƿ����"] = undrug.IsCompareToMaterial ? "��" : "û��";
            drv["��ע"] = undrug.Memo;
            drv["��ͯ��"] = undrug.ChildPrice.ToString();
            drv["�����"] = undrug.SpecialPrice.ToString();
            switch (undrug.SpecialFlag)
            {
                case "0":
                    drv["ʡ����"] = "������";
                    break;
                case "1":
                    drv["ʡ����"] = "����";
                    break;
                default:
                    drv["ʡ����"] = "";
                    break;
            }
            switch (undrug.SpecialFlag1)
            {
                case "0":
                    drv["������"] = "������";
                    break;
                case "1":
                    drv["������"] = "����";
                    break;
                default:
                    drv["������"] = "";
                    break;
            }
            switch (undrug.SpecialFlag2)
            {
                case "0":
                    drv["�Է���Ŀ"] = "����";
                    break;
                case "1":
                    drv["�Է���Ŀ"] = "��";
                    break;
                default:
                    drv["�Է���Ŀ"] = "";
                    break;
            }
            switch (undrug.SpecialFlag3)
            {
                case "0":
                    drv["�����ʶ1"] = "����";
                    break;
                case "1":
                    drv["�����ʶ1"] = "��";
                    break;
                default:
                    drv["�����ʶ1"] = "";
                    break;
            }
            switch (undrug.SpecialFlag4)
            {
                case "0":
                    drv["�����ʶ2"] = "����";
                    break;
                case "1":
                    drv["�����ʶ2"] = "��";
                    break;
                default:
                    drv["�����ʶ2"] = "";
                    break;
            }

            //��û����,���ƻ�������
            drv["����1"] = undrug.User02;

            //��û����,���ƻ�������
            drv["����2"] = undrug.User03;

            drv["��������"] = undrug.DiseaseType.ID;
            drv["ר������"] = undrug.SpecialDept.ID;
            drv["��ʷ�����"] = undrug.MedicalRecord;
            drv["���Ҫ��"] = undrug.CheckRequest;
            drv["ע������"] = undrug.Notice;
            drv["֪��ͬ����"] = undrug.IsConsent ? "��Ҫ" : "����Ҫ";
            drv["������뵥����"] = undrug.CheckApplyDept;
            drv["�Ƿ���ҪԤԼ"] = undrug.IsNeedBespeak ? "��Ҫ" : "����Ҫ";
            drv["��Ŀ��Χ"] = undrug.ItemArea;
            drv["��ĿԼ��"] = undrug.ItemException;
            drv["�Ƿ�����"] = undrug.UnitFlag == "1" ? "��" : "��";
            drv["���÷�Χ"] =  this.applicabilityAreaHelp.GetName(undrug.ApplicabilityArea); 

            drv.EndEdit();
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        private void Delete()
        {
            int index = this.FpItems.ActiveRow.Index;
            this.dvUndrugItem.Delete(index);
        }
                
        /// <summary>
        /// ���˺���{46DDA07A-37AC-4394-A9D9-E31A4C26E045}feng.ch��������״̬���Ƿ����׹���
        /// </summary>
        /// <param name="whereValue">������</param>
        /// <param name="sct">ϵͳ���</param>
        /// <param name="sft">��С���ô���</param>
        protected virtual void GenerateRowFilter(string whereValue, string sct, string sft,string state,string tag,string itemType)
        {
            StringBuilder sb = new StringBuilder();
            this.dvUndrugItem.AllowDelete = true;
            this.dvUndrugItem.AllowEdit = true;
            this.dvUndrugItem.AllowNew = true;

            #region ����
            //if (whereValue != "")
            //{
            //    sb.Append("������ like '");
            //    sb.Append(whereValue.ToUpper());
            //    sb.Append("%' or  ƴ���� like '");
            //    sb.Append(whereValue.ToUpper());
            //    sb.Append("%' or ����� like '");
            //    sb.Append(whereValue.ToUpper());
            //    sb.Append("%' ");
            //}
            //if (sct != "")
            //{
            //    if (whereValue != "")
            //    {
            //        sb.Append("and ϵͳ���='");
            //    }
            //    else
            //    {
            //        sb.Append(" ϵͳ���='");
            //    }
            //    sb.Append(sct);
            //    sb.Append("' ");
            //}
            //if (sft != "")
            //{
            //    if (whereValue != "" || sct != "")
            //    {
            //        sb.Append("and ���ô���='");
            //    }
            //    else
            //    {
            //        sb.Append(" ���ô���='");
            //    }
            //    sb.Append(sft);
            //    sb.Append("' ");
            //}

            //if (whereValue.Trim() == "" || sct.Trim() == "" || sft.Trim() == "")
            //{
            //    return;
            //}
            #endregion


            if (whereValue == "" && sft == "" && sct == "" && state == "" && tag == "" && itemType=="")
            {
                this.dvUndrugItem.RowFilter = "";
                this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                return;
            }

            string where = whereValue.Trim().Equals("") ? "" : whereValue.ToUpper();
            string condition = "";
            if (where != "")
            {
                condition = "(������ like '" + where;
                condition += "%' or  ƴ���� like '%" + where;
                condition += "%' or ����� like '%" + where;
                condition += "%' or ��Ŀ���� like '%" + where;
                //condition += "%' or ��Ŀ��� like '%" + where;
                condition += "%') ";
                if (sct == "")
                {
                    condition += " or ϵͳ���='";
                    condition += sct;
                    condition += "' ";
                }
                else
                {
                    condition += " and ϵͳ���='";
                    condition += sct;
                    condition += "' ";
                }
                if (sft == "")
                {
                    condition += " or ���ô���='";
                    condition += sft;
                    condition += "' ";
                }
                else
                {
                    condition += " and ���ô���='";
                    condition += sft;
                    condition += "' ";
                }
                //{46DDA07A-37AC-4394-A9D9-E31A4C26E045}
                if (state == "")
                {
                }
                else
                {
                    condition += " and ��Ч�Ա�ʶ='";
                    condition += state;
                    condition += "' ";
                }
                if (tag == "")
                {
                }
                else
                {
                    condition += " and �Ƿ�����='";
                    condition += tag;
                    condition += "' ";
                }
                if (itemType == "")
                {
                }
                else
                {
                    condition += " and ��۷������='";
                    condition += itemType;
                    condition += "' ";
                }
                this.dvUndrugItem.RowFilter = condition;
                this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
            }
            else
            {
                //����Կո�ʼ�򷵻�
                //if (whereValue.StartsWith(" ") || sct.StartsWith(" ") || sft.StartsWith(" ") || state.StartsWith(" ") || tag.StartsWith(" ") || itemType.StartsWith(" "))
                //{
                //    return;
                //}
                //if (whereValue.StartsWith(" ") && sct.StartsWith(" ") && sft.StartsWith(" ") && state.StartsWith(" ") && tag.StartsWith(" ") && itemType.StartsWith(" "))
                //{
                //    return;
                //}
                if (sct.Trim() != "" && sft.Trim() != "" && state.Trim() != "" && tag.Trim() != "" && itemType.Trim()!="")
                {
                    condition += " ϵͳ���='";
                    condition += sct.Trim();
                    condition += "' ";
                    condition += " and ���ô���='";
                    condition += sft.Trim();
                    condition += "' ";
                    condition += " and ��Ч�Ա�ʶ='";
                    condition += state.Trim();
                    condition += "' ";
                    condition += " and �Ƿ�����='";
                    condition += tag.Trim();
                    condition += "' ";
                    condition += " and ��۷������='";
                    condition += itemType.Trim();
                    condition += "' ";
                    this.dvUndrugItem.RowFilter = condition;
                    this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                }
                else
                {
            //��ׯ�޸� {C8FB77C6-955E-4231-83A6-BB34B93665B7}
                    if (sct.Trim() != "" && sft.Trim() == "" && state.Trim() == "")
                    {
                        condition += " ϵͳ���='";
                        condition += sct.Trim();
                        condition += "' ";
                        this.dvUndrugItem.RowFilter = condition;
                        this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                    else if (sft.Trim() != "" && sct.Trim() == "" && state.Trim() == "")
                    {
                        condition += " ���ô���='";
                        condition += sft.Trim();
                        condition += "' ";
                        this.dvUndrugItem.RowFilter = condition;
                        this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                    else if (state.Trim() != "" && sct.Trim() == "" && sft.Trim() == "")
                    {
                        condition += " ��Ч�Ա�ʶ='";
                        condition += state.Trim();
                        condition += "' ";
                        this.dvUndrugItem.RowFilter = condition;
                        this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                    else if (sct.Trim() != "" && sft.Trim() != "" && state.Trim() != "")
                    {
                        condition += " ϵͳ���='";
                        condition += sct.Trim();
                        condition += "' ";
                        condition += " and ���ô���='";
                        condition += sft.Trim();
                        condition += "' ";
                        condition += " and ��Ч�Ա�ʶ='";
                        condition += state.Trim();
                        condition += "' ";
                        this.dvUndrugItem.RowFilter = condition;
                        this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                    else if (sct.Trim() != "" && state.Trim() != "" && sft.Trim() == "")
                    {
                        condition += " ϵͳ���='";
                        condition += sct.Trim();
                        condition += "' ";
                        condition += " and ��Ч�Ա�ʶ='";
                        condition += state.Trim();
                        condition += "' ";
                        this.dvUndrugItem.RowFilter = condition;
                        this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                    else if (sct.Trim() != "" && sft.Trim() != "" && state.Trim() == "")
                    {
                        condition += " ϵͳ���='";
                        condition += sct.Trim();
                        condition += "' ";
                        condition += " and ���ô���='";
                        condition += sft.Trim();
                        condition += "' ";
                        this.dvUndrugItem.RowFilter = condition;
                        this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                    else if (sft.Trim() != "" && state.Trim() != "" && sct.Trim() == "")
                    {
                        condition += " ϵͳ���='";
                        condition += sct.Trim();
                        condition += "' ";
                        condition += " and ��Ч�Ա�ʶ='";
                        condition += tag.Trim();
                        condition += "' ";
                        this.dvUndrugItem.RowFilter = condition;
                        this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
                    }

                    //condition += " ϵͳ���='";
                    //condition += sct.Trim();
                    //condition += "' ";
                    //condition += " or ���ô���='";
                    //condition += sft.Trim();
                    //condition += "' ";
                    //condition += " or ��Ч�Ա�ʶ='";
                    //condition += state.Trim();
                    //condition += "' ";
                    //condition += " or �Ƿ�����='";
                    //condition += tag.Trim();
                    //condition += "' ";
                    //condition += " or ��۷������='";
                    //condition += itemType.Trim();
                    //condition += "' ";
                   
                }
            }
            
            //this.dvUndrugItem.RowFilter = condition;
            //this.dvUndrugItem.RowStateFilter = DataViewRowState.CurrentRows;
        }

        private void CreateTable()
        {
            this.dataTable = new DataTable();

            this.dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("��Ŀ���", typeof(string)),
                                                   new DataColumn("��Ŀ����", typeof(string)),
                                                   new DataColumn("ϵͳ���", typeof(string)),
                                                   new DataColumn("���ô���", typeof(string)),
                                                   new DataColumn("������", typeof(string)),
                                                   new DataColumn("ƴ����", typeof(string)),
                                                   new DataColumn("�����", typeof(string)),
                                                   new DataColumn("���ұ���", typeof(string)),
                                                   new DataColumn("���ʱ���", typeof(string)),
                                                   new DataColumn("Ĭ�ϼ�", typeof(string)),
                                                   new DataColumn("��λ", typeof(string)),
                                                   new DataColumn("����ӳɱ���", typeof(string)),
                                                   new DataColumn("�ƻ��������", typeof(string)),
                                                   new DataColumn("�ض�������Ŀ", typeof(string)),
                                                   new DataColumn("�������־", typeof(string)),
                                                   new DataColumn("ȷ�ϱ�־", typeof(string)),
                                                   new DataColumn("��Ч�Ա�ʶ", typeof(string)),
                                                   new DataColumn("���", typeof(string)),
                                                   new DataColumn("ִ�п���", typeof(string)),
                                                   new DataColumn("�豸���", typeof(string)),
                                                   new DataColumn("�걾", typeof(string)),
                                                   new DataColumn("��������", typeof(string)),
                                                   new DataColumn("��������", typeof(string)),
                                                   new DataColumn("������ģ", typeof(string)),
                                                   new DataColumn("�Ƿ����", typeof(string)),
                                                   new DataColumn("��ע", typeof(string)),
                                                   new DataColumn("��ͯ��", typeof(string)),
                                                   new DataColumn("�����", typeof(string)),
                                                   new DataColumn("ʡ����", typeof(string)),
                                                   new DataColumn("������", typeof(string)),
                                                   new DataColumn("�Է���Ŀ", typeof(string)),
                                                   new DataColumn("�����ʶ1", typeof(string)),
                                                   new DataColumn("�����ʶ2", typeof(string)),
                                                   new DataColumn("����1", typeof(string)),
                                                   new DataColumn("����2", typeof(string)),
                                                   new DataColumn("��������", typeof(string)),
                                                   new DataColumn("ר������", typeof(string)),
                                                   new DataColumn("��ʷ�����", typeof(string)),
                                                   new DataColumn("���Ҫ��", typeof(string)),
                                                   new DataColumn("ע������", typeof(string)),
                                                   new DataColumn("֪��ͬ����", typeof(string)),
                                                   new DataColumn("������뵥����", typeof(string)),
                                                   new DataColumn("�Ƿ���ҪԤԼ", typeof(string)),
                                                   new DataColumn("��Ŀ��Χ", typeof(string)),
                                                   new DataColumn("��ĿԼ��", typeof(string)),
                                                   new DataColumn("�Ƿ�����", typeof(string)),
                                                   new DataColumn("���÷�Χ",typeof(string)),
                                                   new DataColumn("��۷������",typeof(string)),
                                                   new DataColumn("������",typeof(string)),
                                                   new DataColumn("ͣ��ʱ��",typeof(string))
                                                  });
        }

        #endregion
        private bool canModifyPrice = false;
        /// <summary>
        /// �Ƿ������޸ļ۸�
        /// </summary>
        [Category("�ؼ�����"), Description("����ǰ�Ƿ������޸ļ۸�")]
        public bool CanModifyPrice
        {
            get
            {
                return canModifyPrice;
            }
            set
            {
                canModifyPrice = value;
            }
        }
        private void tbQueryValue_TextChanged(object sender, EventArgs e)
        {
            string sClassType = "";
            string sFeeType = "";
            if (!this.cbClassType.Text.Equals("") && this.cbClassType.Text != null)
            {
                //sClassType = this.htClassType[this.cbClassType.Text].ToString();
                sClassType = this.cbClassType.Text;
            }
            if (!this.cbFeeType.Text.Equals("") && this.cbFeeType.Text != null)
            {
                sFeeType = this.cbFeeType.Text;//.ToString();
            }
            string tag = "";
            if (this.cmbIsCom.Text != "")
            {
                tag = this.cmbIsCom.Text.Substring(0, 1);
            }
            GenerateRowFilter(this.tbQueryValue.Text, sClassType, sFeeType, this.cmbState.Text,tag,this.cmbItemPriceType.Text);
            //GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text);
        }

        private void cbClassType_TextChanged(object sender, EventArgs e)
        {
            string tag = "";
            if (this.cmbIsCom.Text != "")
            {
                tag = this.cmbIsCom.Text.Substring(0, 1);
            }
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text, this.cmbState.Text, tag,this.cmbItemPriceType.Text);
        }

        private void cbFeeType_TextChanged(object sender, EventArgs e)
        {
            string tag = "";
            if (this.cmbIsCom.Text != "")
            {
                tag = this.cmbIsCom.Text.Substring(0, 1);
            }
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text, this.cmbState.Text, tag,this.cmbItemPriceType.Text);
        }

        protected void neuSpreadItems_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
            
            undrug.ID = this.FpItems.GetText(e.Row, this.GetCloumn("��Ŀ���"));
            undrug.Name = this.FpItems.GetText(e.Row, this.GetCloumn("��Ŀ����"));

            //ϵͳ���
            undrug.SysClass.Name = this.FpItems.GetText(e.Row, this.GetCloumn("ϵͳ���"));

            //��С���ô���
            undrug.MinFee.Name = this.FpItems.GetText(e.Row, this.GetCloumn("���ô���"));//

            //������
            undrug.UserCode = this.FpItems.GetText(e.Row, this.GetCloumn("������"));
            undrug.SpellCode = this.FpItems.GetText(e.Row, this.GetCloumn("ƴ����"));
            undrug.WBCode = this.FpItems.GetText(e.Row, this.GetCloumn("�����"));
            undrug.GBCode = this.FpItems.GetText(e.Row, this.GetCloumn("���ұ���"));
            undrug.NationCode = this.FpItems.GetText(e.Row, this.GetCloumn("���ʱ���"));
            string tempStr = FpItems.GetText(e.Row, this.GetCloumn("���÷�Χ")); 
            undrug.ApplicabilityArea = applicabilityAreaHelp.GetID(tempStr); 

            //Ĭ�ϼ�
            undrug.Price = this.FpItems.GetText(e.Row, this.GetCloumn("Ĭ�ϼ�")) == "" ? 0 : Convert.ToDecimal(this.FpItems.GetText(e.Row, this.GetCloumn("Ĭ�ϼ�")));

            undrug.PriceUnit = this.FpItems.GetText(e.Row, this.GetCloumn("��λ"));

            //����ӳɱ���
            try
            {
                undrug.FTRate.EMCRate = this.FpItems.GetText(e.Row, this.GetCloumn("����ӳɱ���")) == "" ? 0 : Convert.ToDecimal(this.FpItems.GetText(e.Row, 11));
            }
            catch
            {
            }

            //�ƻ��������
            undrug.IsFamilyPlanning = this.FpItems.GetText(e.Row, this.GetCloumn("�ƻ��������")) == "��" ? true : false;

            //���Ҳ������,�Ժ�������
            //undrug.User01;//13;

            //�������־
            undrug.Grade = this.FpItems.GetText(e.Row, this.GetCloumn("�������־"));

            
            //�Ƿ���Ҫȷ��
            undrug.IsNeedConfirm = this.FpItems.GetText(e.Row, this.GetCloumn("ȷ�ϱ�־")) == "��Ҫ" ? true : false;

            //��Ŀ״̬
            //switch (this.FpItems.GetText(e.Row, 16).Trim())
            //{
            //    case "����":
            //        undrug.ValidState = "0";
            //        break;
            //    case "ͣ��":
            //        undrug.ValidState = "1";
            //        break;
            //    case "����":
            //        undrug.ValidState = "2";
            //        break;
            //    default:
            //        undrug.ValidState = "";
            //        break;
            //}
            undrug.ValidState = this.FpItems.GetText(e.Row, this.GetCloumn("��Ч�Ա�ʶ")).Trim();

            //���
            undrug.Specs = this.FpItems.GetText(e.Row, this.GetCloumn("���"));
            undrug.ExecDept = this.FpItems.GetText(e.Row, this.GetCloumn("ִ�п���"));
            undrug.MachineNO = this.FpItems.GetText(e.Row, this.GetCloumn("�豸���"));
            undrug.CheckBody = this.FpItems.GetText(e.Row, this.GetCloumn("�걾"));//

            //����
            undrug.OperationInfo.ID = this.FpItems.GetText(e.Row, this.GetCloumn("��������"));
            undrug.OperationType.ID = this.FpItems.GetText(e.Row, this.GetCloumn("��������"));
            undrug.OperationScale.ID = this.FpItems.GetText(e.Row ,this.GetCloumn("������ģ"));

            //����
            undrug.IsCompareToMaterial = this.FpItems.GetText(e.Row, this.GetCloumn("�Ƿ����")) == "��" ? true : false; 
            
            undrug.Memo = this.FpItems.GetText(e.Row, this.GetCloumn("��ע"));

            //��ͯ��
            try
            {
                undrug.ChildPrice = this.FpItems.GetText(e.Row, this.GetCloumn("��ͯ��")) == "" ? 0 : Convert.ToDecimal(this.FpItems.GetText(e.Row, 26));
            }
            catch
            {
            }
            //�����
            try
            {
                undrug.SpecialPrice = this.FpItems.GetText(e.Row, this.GetCloumn("�����")) == "" ? 0 : Convert.ToDecimal(this.FpItems.GetText(e.Row, 27));
            }
            catch
            {
            }

            switch (this.FpItems.GetText(e.Row, this.GetCloumn("ʡ����")).Trim())// undrug.SpecialFlag)
            {
                case "������":
                    undrug.SpecialFlag = "0";
                    break;
                case "����":
                    undrug.SpecialFlag = "1";
                    break;
                default:
                    undrug.SpecialFlag = "";
                    break;
            }
            switch (this.FpItems.GetText(e.Row, this.GetCloumn("������")).Trim())// undrug.SpecialFlag1)
            {
                case "������":
                    undrug.SpecialFlag1 = "0";
                    break;
                case "����":
                    undrug.SpecialFlag1 = "1";
                    break;
                default:
                    undrug.SpecialFlag1 = "";
                    break;
            }
            
            switch (this.FpItems.GetText(e.Row, this.GetCloumn("�Է���Ŀ")).Trim())// undrug.SpecialFlag2)
            {
                case "����":
                    undrug.SpecialFlag2 = "0";
                    break;
                case "��":
                    undrug.SpecialFlag2 = "1";
                    break;
                default:
                    undrug.SpecialFlag2 = "";
                    break;
            }
            switch (this.FpItems.GetText(e.Row, this.GetCloumn("�����ʶ1")).Trim())// undrug.SpecialFlag3)
            {
                case "����":
                    undrug.SpecialFlag3 = "0";
                    break;
                case "��":
                    undrug.SpecialFlag3 = "1";
                    break;
                default:
                    undrug.SpecialFlag3 = "";
                    break;
            }
            switch (this.FpItems.GetText(e.Row, this.GetCloumn("�����ʶ2")).Trim())// undrug.SpecialFlag4)
            {
                case "����":
                    undrug.SpecialFlag4 = "0";
                    break;
                case "��":
                    undrug.SpecialFlag4 = "1";
                    break;
                default:
                    undrug.SpecialFlag4 = "";
                    break;
            }

            //��û����,���ƻ�������
            //undrug.User02;//33

            //��û����,���ƻ�������
            //undrug.User03;//34
            
            undrug.DiseaseType.ID = this.FpItems.GetText(e.Row, this.GetCloumn("��������"));
            undrug.SpecialDept.ID = this.FpItems.GetText(e.Row, this.GetCloumn("ר������"));
            undrug.MedicalRecord = this.FpItems.GetText(e.Row, this.GetCloumn("��ʷ�����"));
            undrug.CheckRequest = this.FpItems.GetText(e.Row, this.GetCloumn("���Ҫ��"));
            undrug.Notice = this.FpItems.GetText(e.Row, this.GetCloumn("ע������"));
            undrug.IsConsent = this.FpItems.GetText(e.Row, this.GetCloumn("֪��ͬ����")) == "��Ҫ" ? true : false;
            undrug.CheckApplyDept = this.FpItems.GetText(e.Row, this.GetCloumn("������뵥����"));
            undrug.IsNeedBespeak = this.FpItems.GetText(e.Row, this.GetCloumn("�Ƿ���ҪԤԼ")) == "��Ҫ" ? true : false;
            undrug.ItemArea = this.FpItems.GetText(e.Row, this.GetCloumn("��Ŀ��Χ"));
            undrug.ItemException = this.FpItems.GetText(e.Row, this.GetCloumn("��ĿԼ��"));

            
            FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
            //{55CFCB36-B084-4a56-95AD-2CDED962ADC4}
            undrug.ItemPriceType = this.FpItems.GetText(e.Row, this.GetCloumn("��۷������"));
            undrug.IsOrderPrint = item.SelectUndrugDeptListByCode(undrug.ID).IsOrderPrint;
            //{2A5608D8-26AD-47d7-82CC-81375722FF72}
            undrug.DeptList = item.SelectUndrugDeptListByCode(undrug.ID).DeptList;
            //�Ƿ�����(0,��ϸ; 1,����)[2007/01/01  xuweizhe]
            undrug.UnitFlag = this.FpItems.GetText(e.Row, this.GetCloumn("�Ƿ�����"));
            if (undrug.UnitFlag == "��")
            {
                undrug.UnitFlag = "1";
            }
            else
            {
                undrug.UnitFlag = "0";
            }

            ucHandleItems handleItem = new ucHandleItems(false);
            handleItem.SaveSuccessed += new SaveSuccessHandler(handleItem_SaveSuccessed);
            handleItem.UpdateUndrugItems(undrug);
            handleItem.CanModifyPrice = canModifyPrice;
            handleItem.IsAddLine = false;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(handleItem);
        }

        private void neuSpreadItems_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.FpItems, this.filePath);

        }

        #region �Զ����¼�������

        /// <summary>
        /// ���³ɹ�������
        /// </summary>
        /// <param name="undrug">��ҩƷ��Ŀʵ��</param>
        void handleItem_SaveSuccessed(FS.HISFC.Models.Fee.Item.Undrug undrug)
        {
            //throw new Exception("The method or operation is not implemented.");
            this.Update(undrug);
        }

        /// <summary>
        /// ����ɹ�������
        /// </summary>
        /// <param name="undrug">��ҩƷ��Ŀʵ��</param>
        void handleItem_InsertSuccessed(FS.HISFC.Models.Fee.Item.Undrug undrug)
        {
            Insert(undrug);
            //throw new Exception("The method or operation is not implemented.");

        }

        #endregion

        //private void neuSpreadItems_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    this.dvUndrugItem.Sort = this.FpItems.Columns[e.Column].Label + "  ASC";
        //}
        //{46DDA07A-37AC-4394-A9D9-E31A4C26E045}
        private void nComboBox1_TextChanged(object sender, EventArgs e)
        {
            string tag = "";
            if (this.cmbIsCom.Text != "")
            {
                tag = this.cmbIsCom.Text.Substring(0, 1);
            }
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text, this.cmbState.Text, tag, this.cmbItemPriceType.Text);
        }
        //{46DDA07A-37AC-4394-A9D9-E31A4C26E045}
        private void cmbIsCom_TextChanged(object sender, EventArgs e)
        {
            string tag = "";
            if (this.cmbIsCom.Text != "")
            {
                tag = this.cmbIsCom.Text.Substring(2, 1);
            }
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text, this.cmbState.Text, tag, this.cmbItemPriceType.Text);
        }

        private void cmbItemPriceType_TextChanged(object sender, EventArgs e)
        {
            string tag = "";
            if (this.cmbIsCom.Text != "")
            {
                tag = this.cmbIsCom.Text.Substring(0, 1);
            }
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text, this.cmbState.Text, tag,this.cmbItemPriceType.Text);
        }

        protected virtual void neuSpreadItems_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        /* ������,���ǻ�������.
        private void cbClassType_SelectedValueChanged(object sender, EventArgs e)
        {
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text);
        }
        private void cbFeeType_SelectedValueChanged(object sender, EventArgs e)
        {
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text);
        }
        private void cbClassType_TextUpdate(object sender, System.EventArgs e)
        {
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text);
        }

        private void cbFeeType_TextUpdate(object sender, System.EventArgs e)
        {
            GenerateRowFilter(this.tbQueryValue.Text, this.cbClassType.Text, this.cbFeeType.Text);
        }
        */
    }
}