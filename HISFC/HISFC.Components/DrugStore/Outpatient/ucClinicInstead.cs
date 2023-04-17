using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Outpatient
{
    /// <summary>
    /// [��������: �����ֱ�ӷ�ҩ���� ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// <˵��>���ܴ����ƣ�����Զ�ˢ�¹���</˵��>
    /// <�޸ļ�¼>
    ///    1.�����ն˺��Զ�ˢ������ն���ʾ�б� by Sunjh 2010-9-10 {405F3104-5981-4cc4-8662-E9879A842AAC}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucClinicInstead : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucClinicInstead()
        {
            InitializeComponent();
        }


        public delegate void ProcessMessageHandler(object sender, string msg);

        public delegate void MyTreeSelectHandler(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe);

        public delegate void MyOperChangedHandler(FS.FrameWork.Models.NeuObject oper);

        /// <summary>
        /// �Դ�ӡ���Ľ��ı���λ��
        /// </summary>
        private delegate void ChangePrintNodeHandler(TreeNode node, bool targetNextNodeType);

        /// <summary>
        /// �������й����з��͵���Ϣ
        /// </summary>
        public event ProcessMessageHandler ProcessMessageEvent;

        ///// <summary>
        ///// ���б�ѡ���¼�
        ///// </summary>
        //public event MyTreeSelectHandler MyTreeSelectEvent;

        ///// <summary>
        ///// ��/��ҩ����Ա�����仯
        ///// </summary>
        //public event MyOperChangedHandler OperChangedEvent;

        ///// <summary>
        ///// ����
        ///// </summary>
        //public event System.EventHandler SaveRecipeEvent;

        #region �����

        /// <summary>
        /// ҩ������������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �����ն�
        /// </summary>
        List<FS.HISFC.Models.Pharmacy.DrugTerminal> terinalList = new List<FS.HISFC.Models.Pharmacy.DrugTerminal>();

        /// <summary>
        /// ��ѯʱ������ʱ������
        /// </summary>
        private DateTime minQueryDate = System.DateTime.MinValue;

        /// <summary>
        /// ��׼����
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ������Ա��Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject operInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ��ǲ�ҩ��ʽ��ӡ ��ҩ��ӡʱ�Ƿ��ӡ��ҩ��ҩ��������ӡ��ǩ
        /// </summary>
        private bool isHerbalPrint = false;

        /// <summary>
        /// ��ӡ������
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory printFactory = null;
        #endregion

        #region ������

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper terminalHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Ա������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region ����

        /// <summary>
        /// �����ն˼��� 
        /// </summary>
        protected List<FS.HISFC.Models.Pharmacy.DrugTerminal> TerminalList
        {
            set
            {
                this.terinalList = value;

                if (value != null)
                {
                    this.lbInfo.Text = "�����նˣ�";
                    foreach (FS.HISFC.Models.Pharmacy.DrugTerminal info in value)
                    {
                        if (info.Dept.Name != "")
                        {
                            this.lbInfo.Text = this.lbInfo.Text + "  " + info.Dept.Name + "��" + info.Name;
                        }
                        else
                        {
                            this.lbInfo.Text = this.lbInfo.Text + "  " + info.Dept.Name + "��" + info.Name;
                        }
                    }

                    this.ShowList("1");
                }
            }
        }

        /// <summary>
        /// ��ǰ�ն�
        /// </summary>
        protected FS.HISFC.Models.Pharmacy.DrugTerminal OperTerminal
        {
            get
            {
                if (this.cmbTerminal.Text != "")
                {
                    return (FS.HISFC.Models.Pharmacy.DrugTerminal)this.terminalHelper.GetObjectFromID(this.cmbTerminal.Tag.ToString());
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //���ӹ�����
            this.toolBarService.AddToolButton("ˢ��", "ˢ�»����б���ʾ������δ��ӡ���Ĵ���", FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            this.toolBarService.AddToolButton("�����ն�", "������ͬʱ��ҩ��ҩ�����ն�", FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.ucClinicDrug1.terminal = this.OperTerminal;
            if (this.OperTerminal == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ��ǰ�ն�"));
                return -1;
            }

            if (this.ucClinicDrug1.Save() == 1)
            {
                TreeNode selectNode = this.tvList.SelectedNode;

                this.tvList.Nodes.Remove(selectNode);
            }

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ˢ��":
                    this.ShowList("0");
                    break;
                case "�����ն�":
                    this.Set();
                    //�����ն˺��Զ�ˢ������ն���ʾ�б� by Sunjh 2010-9-10 {405F3104-5981-4cc4-8662-E9879A842AAC}
                    this.ShowList("0");
                    break;
            }

        }

        protected override int OnPrint(object sender, object neuObject)
        {
            //{58947EA5-F81D-4bf0-A14A-D686CB78F1E3} ��ӡʵ��  
            if (string.IsNullOrEmpty(this.cmbTerminal.Text))
            {
                MessageBox.Show("����ѡ��ҩ�ն��Ա�ȷ�ϵ��ݴ�ӡ��ʽ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            TreeNode selectNode = this.tvList.SelectedNode;

            this.PrintLabel(selectNode);

            return base.OnPrint(sender, neuObject);
        }

        #endregion

        #region ��ʼ������

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        protected virtual void Init()
        {
            #region �������ʼ��

            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList alDept = managerIntegrate.GetDepartment();
            if (alDept == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ؿ����б�������"));
                return;
            }

            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            ArrayList alTerminal = this.drugStoreManager.QueryDrugTerminalByTerminalType("0");
            if (alTerminal == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ط�ҩ�ն��б�������") + drugStoreManager.Err);
                return;
            }

            this.terminalHelper = new FS.FrameWork.Public.ObjectHelper(alTerminal);

            ArrayList alPerson = managerIntegrate.QueryEmployeeAll();
            if (alPerson == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ա�б�������"));
                return;
            }

            this.personHelper = new FS.FrameWork.Public.ObjectHelper(alPerson);

            #endregion

            this.tvList.ImageList = this.tvList.groupImageList;

            this.approveDept = ((FS.HISFC.Models.Base.Employee)this.drugStoreManager.Operator).Dept;
            this.operInfo = this.drugStoreManager.Operator;

            this.ucClinicDrug1.ApproveDept = this.approveDept;

            ArrayList sendTerminalList = this.drugStoreManager.QueryDrugTerminalByDeptCode(this.approveDept.ID, "0");
            if (sendTerminalList != null)
            {
                this.terinalList = new List<FS.HISFC.Models.Pharmacy.DrugTerminal>();
                foreach (FS.HISFC.Models.Pharmacy.DrugTerminal info in sendTerminalList)
                {
                    info.Dept.Name = this.approveDept.Name;
                    this.terinalList.Add(info);
                }
            }

            this.TerminalList = this.terinalList;
            this.cmbTerminal.AddItems(sendTerminalList);            

            this.ucClinicDrug1.OperInfo = this.operInfo;
            this.ucClinicDrug1.funModle = OutpatientFun.DirectSend;
            this.ucClinicDrug1.Init();            
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void Set()
        {
            ucTerminalSelect uc = new ucTerminalSelect();

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

            if (uc.Result == DialogResult.Cancel)
            {
                return;
            }

            List<FS.HISFC.Models.Pharmacy.DrugTerminal> tList = uc.GetTerminalList();

            if (tList != null && tList.Count > 0)
            {
                this.TerminalList = tList;
            }
            else
            {
                ArrayList sendTerminalList = this.drugStoreManager.QueryDrugTerminalByDeptCode(this.approveDept.ID, "0");
                if (sendTerminalList != null)
                {
                    this.terinalList = new List<FS.HISFC.Models.Pharmacy.DrugTerminal>();
                    foreach (FS.HISFC.Models.Pharmacy.DrugTerminal info in sendTerminalList)
                    {
                        info.Dept.Name = this.approveDept.Name;
                        this.terinalList.Add(info);
                    }

                    this.TerminalList = this.terinalList;
                }
            }
        }

        /// <summary>
        /// ��ӡ��ʼ��
        /// </summary>
        protected virtual void PrintInit()
        {
            object factoryInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory)) as FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;

            if (factoryInstance != null)
            {
                FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory factory = factoryInstance as FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory;

                this.printFactory = factory;

                if (this.printFactory == null)
                {
                    MessageBox.Show("δ���õ��ݴ�ӡ��ʵ�֣����޷�������ҩ���ݴ�ӡ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (this.OperTerminal == null || string.IsNullOrEmpty(this.OperTerminal.ID))
                {
                    return;
                }

                FS.HISFC.Components.DrugStore.Function.IDrugPrint = factory.GetInstance(this.OperTerminal);
            }

            if (FS.HISFC.Components.DrugStore.Function.IDrugPrint == null)
            {
                MessageBox.Show("δ���õ��ݴ�ӡ��ʵ�֣����޷�������ҩ���ݴ�ӡ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            return;

            //#region �����ȡ��ǩ��ʽ

            ////string dllName = "Report";
            //string className = "Report.DrugStore.ucRecipeLabel";

            ////�����ǩ��ӡ�ӿ�ʵ����
            //FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //string labelValue = ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Clinic_Print_Label, true, "Report.DrugStore.ucRecipeLabel");
            ////�����ҩ��ӡ�ӿ�ʵ����
            //string billValue = ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Clinic_Print_Bill, true, "Report.DrugStore.ucOutHerbalBill");

            ////Ĭ�ϱ�ǩ��ӡ
            //className = labelValue;
            ////��ȡ���ؿ��Ʋ��� �ж��Ƿ���ò�ҩ��ӡ��ʽ
            //string strErr = "";
            //ArrayList alParm = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("ClinicDrug", "PrintList", out strErr);
            //if (alParm != null && alParm.Count > 0)
            //{
            //    if ((alParm[0] as string) == "1")
            //    {
            //        className = billValue;
            //        this.isHerbalPrint = true;
            //    }
            //}

            //object[] o = new object[] { };

            //try
            //{
            //    System.Runtime.Remoting.ObjectHandle objHandel = System.Activator.CreateInstance("Report", className, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);
            //    object oLabel = objHandel.Unwrap();

            //    FS.HISFC.Components.DrugStore.Function.IDrugPrint = oLabel as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint;

            //}
            //catch (System.TypeLoadException ex)
            //{
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //    MessageBox.Show(Language.Msg("��ǩ�����ռ���Ч\n" + ex.Message));
            //}

            //#endregion
        }

        #endregion

        #region �б����ݼ���

        /// <summary>
        /// �����б���ʾ 
        /// </summary>
        public virtual void ShowList(string state)
        {
            ArrayList alList = new ArrayList();

            DateTime queryTime = new DateTime(1, 1, 1, 0, 0, 0);

            foreach (FS.HISFC.Models.Pharmacy.DrugTerminal tempTerminal in this.terinalList)
            {
                ArrayList alTemp = new ArrayList();
                //������� 0 ��ҩ 1 ��ҩ
                alTemp = this.drugStoreManager.QueryList(tempTerminal.Dept.ID, tempTerminal.ID,"0",state, queryTime);
                string strMsg = "";
                if (alTemp == null)
                    strMsg = "�˴�ˢ�·�������...." + this.drugStoreManager.Err;
                else
                    strMsg = "��ȡ" + alTemp.Count + "�����ݣ��ȴ��´�ˢ��....";
                if (this.ProcessMessageEvent != null)
                {
                    this.ProcessMessageEvent("ShowList", strMsg);
                }

                if (alTemp.Count == 0)
                {
                    continue;
                }

                alList.Add(alTemp);               
            }

            this.AddListToTree(alList);
        }

        /// <summary>
        /// �����Ϳؼ��ڼ���������ʾ
        /// </summary>
        /// <param name="alRecipeList"></param>
        protected virtual void AddListToTree(ArrayList alRecipeList)
        {
            foreach (ArrayList alList in alRecipeList)
            {
                TreeNode deptNode = null;
                TreeNode terminalNode = null;
                foreach (FS.HISFC.Models.Pharmacy.DrugRecipe info in alList)
                {
                    #region ���ҽڵ����

                    deptNode = this.FindDeptNode(info);
                    if (deptNode == null)
                    {
                        deptNode = this.AddDeptNode(info);
                    }
                    #endregion

                    #region �����ն˽ڵ�

                    terminalNode = this.FindTerminalNode(info, deptNode);
                    if (terminalNode == null)
                    {
                        terminalNode = this.AddTerminalNode(info);
                        deptNode.Nodes.Add(terminalNode);
                    }

                    #endregion

                    #region ���ӻ��߽ڵ�

                    #endregion

                    TreeNode recipeNode = this.FindPatientNode(info,terminalNode);
                    if (recipeNode == null)
                    {
                        recipeNode = this.AddRecipeNode(info);
                        recipeNode.Tag = info;

                        terminalNode.Nodes.Add(recipeNode);
                    }                                        
                }

                //�����´β�ѯʱ��
                this.GetMinDrugedDate(alList);   
            }

            if (this.tvList.Nodes.Count > 0)
            {
                this.tvList.Nodes[0].Expand();
            }

            this.AutoPrint();
        }

        /// <summary>
        /// ��ȡ����Ĵ������������������ҩʱ��
        /// </summary>
        /// <param name="drugRecipeAl">������������</param>
        private void GetMinDrugedDate(ArrayList drugRecipeAl)
        {
            if (drugRecipeAl.Count > 0)
            {
                this.minQueryDate = System.DateTime.MinValue;
                foreach (FS.HISFC.Models.Pharmacy.DrugRecipe info in drugRecipeAl)
                {
                    if (info.DrugedOper.OperTime > this.minQueryDate)
                    {
                        this.minQueryDate = info.DrugedOper.OperTime;
                    }
                }
            }

            //�����ʱ��������һ�� Sql�����ʹ�õĴ��ڵ���
            this.minQueryDate = this.minQueryDate.AddSeconds(1);
        }

        /// <summary>
        /// ��ӿ��ҽڵ�
        /// </summary>
        /// <param name="drugRecipe"></param>
        protected TreeNode AddDeptNode(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            TreeNode deptNode = new TreeNode(this.deptHelper.GetName(drugRecipe.StockDept.ID));
            deptNode.ImageIndex = 0;
            deptNode.Tag = drugRecipe.StockDept.ID;

            this.tvList.Nodes.Add(deptNode);

            return deptNode;
        }

        protected TreeNode FindDeptNode(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            foreach (TreeNode node in this.tvList.Nodes)
            {
                if (node.Tag.ToString() == drugRecipe.StockDept.ID)
                {
                    return node;
                }
            }

            return null;
        }

        protected TreeNode AddTerminalNode(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            TreeNode terminalNode = new TreeNode(this.terminalHelper.GetName(drugRecipe.SendTerminal.ID));
            terminalNode.Tag = drugRecipe.SendTerminal.ID;
            terminalNode.ImageIndex = 1;
            return terminalNode;
        }

        /// <summary>
        /// �����ն˽ڵ�
        /// </summary>
        /// <param name="drugRecipe"></param>
        /// <param name="deptNode"></param>
        /// <returns></returns>
        protected TreeNode FindTerminalNode(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, TreeNode deptNode)
        {
            foreach (TreeNode terminalNode in deptNode.Nodes)
            {
                if (terminalNode.Tag.ToString() == drugRecipe.SendTerminal.ID)
                {
                    return terminalNode;
                }
            }

            return null;
        }

        protected TreeNode AddRecipeNode(FS.HISFC.Models.Pharmacy.DrugRecipe drugRcipe)
        {
            TreeNode recipeNode = new TreeNode(drugRcipe.PatientName);
            recipeNode.Tag = drugRcipe;
            recipeNode.ImageIndex = 2;
            return recipeNode;
        }

        /// <summary>
        /// ���һ��߽ڵ�
        /// </summary>
        /// <param name="drugRecipe"></param>
        /// <param name="deptNode"></param>
        /// <returns></returns>
        protected TreeNode FindPatientNode(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, TreeNode terminalNode)
        {
            foreach (TreeNode patientNode in terminalNode.Nodes)
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe tempRecipe = patientNode.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;

                if (tempRecipe.RecipeNO == drugRecipe.RecipeNO)
                {
                    return patientNode;
                }
            }

            return null;
        }

        #endregion

        #region ���ݴ�ӡ

        public virtual int AutoPrint()
        {
            if (this.tvList.Nodes.Count <= 0)
            {
                return -1;
            }

            try
            {
                foreach (TreeNode deptNode in this.tvList.Nodes)
                {
                    foreach (TreeNode terminalNode in deptNode.Nodes)
                    {
                        foreach (TreeNode recipeNode in terminalNode.Nodes)
                        {
                            FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = recipeNode.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                            if (drugRecipe.RecipeState != "0")
                            {
                                continue;
                            }
                            this.PrintLabel( recipeNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return 1;
        }

        /// <summary>
        /// �ڵ����ݴ�ӡ
        /// </summary>
        /// <param name="printNode"></param>
        protected virtual int PrintLabel(TreeNode printNode)
        {
            ArrayList alValidate = new ArrayList();
            FS.HISFC.Models.Pharmacy.DrugRecipe info = printNode.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
            if (info == null)       //���ҩ���򴰿�ʱ ��ֵΪnull
            {
                return -1;
            }
            string detailState = "0";
            if (info.RecipeState == "0" || info.RecipeState == "1")
            {
                detailState = "0";
            }
            else if (info.RecipeState == "2")
            {
                detailState = "1";
            }
            else
            {
                detailState = "2";
            }

            ArrayList alInfo = this.itemManager.QueryApplyOutListForClinic(info.StockDept.ID, "M1", detailState, info.RecipeNO);
            if (alInfo == null)
            {
                MessageBox.Show(Language.Msg("��ӡ��ҩ�� ��ȡ����ҩ��ϸ����!") + itemManager.Err);
                return -1;
            }
            //ֻȡ��Ч��¼
            alValidate = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp in alInfo)
            {
                if (applyOutTemp.ValidState != FS.HISFC.Models.Base.EnumValidState.Invalid)
                    alValidate.Add(applyOutTemp);
            }

            if (alValidate.Count > 0)
            {
                if (this.Print(info, alValidate) == -1)
                {
                    return -1;
                }
            }

            //���´�ӡ���  //��״̬Ϊ0�����ݸ��´�ӡ���
            if (info.RecipeState == "0")
            {
                int parm = this.drugStoreManager.UpdateDrugRecipeState(info.StockDept.ID, info.RecipeNO, "M1", "0", "1");
                if (parm == -1)
                {
                    MessageBox.Show(Language.Msg("���°�ҩ����ϸ δ��ӡ״̬Ϊ�Ѵ�ӡ״̬ʧ��!") + this.drugStoreManager.Err);
                    return -1;
                }
                info.RecipeState = "1";
            }

            return 1;
        }

        /// <summary>
        /// ִ�д�ӡ
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal int Print(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, ArrayList al)
        {
            if (Function.IDrugPrint == null)
            {
                return -1;
            }

            //һ��ֻ��ӡһ�������ŵ�
            //�����ʱ������Ϻš�Ժע��Ƿ��� ���ڴ�ӡ
            //applyOut.User01 ��ҩ���ں� applyOut.User02 Ժע����

            if (al.Count <= 0)
            {
                return 1;
            }

            FS.HISFC.Models.Registration.Register patientInfo = null;		//������Ϣ

            #region ������Ϣ��ȡ

            //��ȡ������Ϣ
            FS.HISFC.BizProcess.Integrate.Registration.Registration regManager = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            patientInfo = regManager.GetByClinic(drugRecipe.ClinicNO);

            #endregion

            #region ��ҩ����ҩ����ҩ��ӡ
            if (this.isHerbalPrint)
            {
                patientInfo.User01 = drugRecipe.FeeOper.OperTime.ToString();

                patientInfo.DoctorInfo.Templet.Doct.Name = this.personHelper.GetName(drugRecipe.Doct.ID);

                Function.IDrugPrint.OutpatientInfo = patientInfo;

                Function.IDrugPrint.AddAllData(al);
                Function.IDrugPrint.Print();

                return 1;
            }
            #endregion

            #region ��ȡ��ǩ��ҳ��
            string privCombo = "";												//�ϴ�ҽ����Ϻ�
            int iRecipeTotNum = 0;												//�������ӡ��ǩ��ҳ��
            string recipeNo = "";		//������
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut temp in al)
            {
                //temp.SendWindow = this.terminal.SendWindow.Name;
                if (privCombo == temp.CombNO && temp.CombNO != "")
                {
                    continue;
                }
                else
                {
                    iRecipeTotNum = iRecipeTotNum + 1;
                    privCombo = temp.CombNO;
                }

                recipeNo = temp.RecipeNO;
            }
            #endregion

            Function.IDrugPrint.LabelTotNum = iRecipeTotNum;
            Function.IDrugPrint.DrugTotNum = al.Count;
            if (patientInfo != null)
            {
                patientInfo.User02 = al.Count.ToString();
                patientInfo.User01 = drugRecipe.FeeOper.OperTime.ToString();

                patientInfo.DoctorInfo.Templet.Doct.Name = this.personHelper.GetName(drugRecipe.Doct.ID);

                patientInfo.User03 = drugRecipe.RecipeNO;

                Function.IDrugPrint.OutpatientInfo = patientInfo;
            }

            privCombo = "-1";
            ArrayList alCombo = new ArrayList();

            #region ��ǩ��ӡ
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                //info.SendWindow = this.terminal.SendWindow.Name;
                if (privCombo == "-1" || (privCombo == info.CombNO && info.CombNO != ""))
                {
                    alCombo.Add(info);
                    privCombo = info.CombNO;
                    continue;
                }
                else			//��ͬ������
                {
                    if (alCombo.Count == 1)
                        Function.IDrugPrint.AddSingle(alCombo[0] as FS.HISFC.Models.Pharmacy.ApplyOut);
                    else
                        Function.IDrugPrint.AddCombo(alCombo);
                    Function.IDrugPrint.Print();

                    privCombo = info.CombNO;
                    alCombo = new ArrayList();

                    alCombo.Add(info);
                }
            }
            if (alCombo.Count == 0)
            {
                return 1;
            }
            if (alCombo.Count > 1)
            {
                Function.IDrugPrint.AddCombo(alCombo);
            }
            else
            {
                Function.IDrugPrint.AddSingle(alCombo[0] as FS.HISFC.Models.Pharmacy.ApplyOut);
            }

            Function.IDrugPrint.Print();

            #endregion

            return 1;
        }

        #endregion

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectNode = this.tvList.SelectedNode;

            if (selectNode.Tag is FS.HISFC.Models.Pharmacy.DrugRecipe)
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = selectNode.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                this.ucClinicDrug1.OperDept = drugRecipe.StockDept;

                this.ucClinicDrug1.ShowData(drugRecipe);
            }
        }

        private void ucClinicInstead_Load(object sender, EventArgs e)
        {
            this.Init();

            this.PrintInit();            
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[2];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientPrintFactory);
                printType[1] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint);

                return printType;
            }
        }

        #endregion

        private void cmbTerminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.HISFC.Components.DrugStore.Function.IDrugPrint = this.printFactory.GetInstance(this.OperTerminal);
        }
    }
}
