using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.MTOrder
{
    public partial class ucMTArrange : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMTArrange()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmDoctShemaTemplet_Load);
            this.treeView1.BeforeSelect += new TreeViewCancelEventHandler(treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);

            this.tabControl1.Deselecting += new TabControlCancelEventHandler(tabControl1_SelectionChanging);
            this.tabControl1.Selected += new TabControlEventHandler(tabControl1_SelectionChanged);

            this.cmbMedTech.IsFlat = true;
            this.cmbMedTech.SelectedIndexChanged += new EventHandler(cmbMedTech_SelectedIndexChanged);
            this.cmbMedTech.KeyDown += new KeyEventHandler(cmbMedTech_KeyDown);
            this.ReflashTimer.Tick += new EventHandler(ReflashTimer_Tick);
        }

        #region ����

        private bool isCheckChangceAndSave = true;
        /// <summary>
        /// �Ƿ�����������ʾ����
        /// </summary>
        [Category("����"), Description("�Ƿ�����������ʾ����")]
        public bool IsCheckChangceAndSave
        {
            get { return isCheckChangceAndSave; }
            set { isCheckChangceAndSave = value; }
        }

        private Color stopColor = Color.Yellow;
        /// <summary>
        /// ͣ����ɫ
        /// </summary>
        [Category("����"), Description("ͣ����ɫ")]
        public Color StopColor
        {
            get { return stopColor; }
            set { stopColor = value; }
        }

        private Color expiredColor = Color.Silver;
        /// <summary>
        /// �����Ű���ɫ
        /// </summary>
        [Category("����"), Description("�����Ű���ɫ")]
        public Color ExpiredColor
        {
            get { return expiredColor; }
            set { expiredColor = value; }
        }

        private Color outColor = Color.Pink;
        /// <summary>
        /// �����Ű���ɫ
        /// </summary>
        [Category("����"), Description("�Ű�Ϊ0����ɫ")]
        public Color OutColor
        {
            get { return outColor; }
            set { outColor = value; }
        }

        private bool isLockExpired = true;
        /// <summary>
        /// �����Ű��Ƿ�����(������༭)
        /// </summary>
        [Category("����"), Description("�����Ű��Ƿ�����(������༭)")]
        public bool IsLockExpired
        {
            get { return isLockExpired; }
            set { isLockExpired = value; }
        }

        /// <summary>
        /// �Ƿ������Զ�ˢ��
        /// </summary>
        [Category("����"), Description("�Ƿ������Զ�ˢ��")]
        public bool IsAutoReflash
        {
            get
            {
                return ReflashTimer.Enabled;
            }
            set
            {
                ReflashTimer.Enabled = value;
            }
        }
        /// <summary>
        /// �Զ�ˢ��Ƶ��
        /// </summary>
        [Category("����"), Description("�Զ�ˢ��Ƶ��")]
        public int Interval
        {
            get
            {
                return ReflashTimer.Interval;
            }
            set
            {
                ReflashTimer.Interval = value;
            }
        }
        private bool isShowID=false;
        /// <summary>
        /// �Ƿ���ʾID��
        /// </summary>
        [Category("����"), Description("�Ƿ���ʾID��")]
        public bool IsShowID
        {
            get
            {
                return isShowID;
            }
            set
            {
                isShowID = value;
            }
        }
        #endregion
        /// <summary>
        /// �Ű�ؼ�����,�������
        /// </summary>
        protected MTOrder.ucSchema[] controls;
        /// <summary>
        /// �Ű�ģ�����
        /// </summary>
        protected FS.HISFC.Models.Base.EnumSchemaType SchemaType;

        private void frmDoctShemaTemplet_Load(object sender, EventArgs e)
        {

            this.InitTab();

            this.InitArray();

            this.InitMT();

            this.InitControls();

            //��ѯ������������
            this.QueryAll();

            this.QueryTodaySchema();

            this.treeView1.SelectedNode = this.treeView1.Nodes[0];

            this.treeView1_AfterSelect(null, null);

        }
        /// <summary>
        /// �����Ű�����
        /// </summary>
        private void InitTab()
        {
            FS.HISFC.BizLogic.MedicalTechnology.Arrange sMgr = new FS.HISFC.BizLogic.MedicalTechnology.Arrange();
            DateTime Current = sMgr.GetDateTimeFromSysDateTime();

            DateTime Monday = this.GetMonday(Current);

            this.setWeek(Monday);
        }
        private void setWeek(DateTime Monday)
        {
            string[] Week = new string[] { "һ", "��", "��", "��", "��", "��", "��" };

            for (int i = 0; i < 7; i++)
            {
                this.tabControl1.TabPages[i].Tag = Monday.AddDays(i);
                this.tabControl1.TabPages[i].Text = Monday.AddDays(i).ToString("yyyy-MM-dd") + "  " + Week[i];
            }
        }
        /// <summary>
        /// ��ȡ��ǰ�����������ڵ�����һ
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private DateTime GetMonday(DateTime current)
        {
            DayOfWeek today = current.DayOfWeek;

            int interval = 1 - (int)today;

            if (interval == 1)//������
            {
                interval = -6;//�������չ鵽������
            }

            return current.AddDays(interval);
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitArray()
        {
            controls = new MTOrder.ucSchema[7];

            controls[0] = this.ucSchema1;
            controls[1] = this.ucSchema2;
            controls[2] = this.ucSchema3;
            controls[3] = this.ucSchema4;
            controls[4] = this.ucSchema5;
            controls[5] = this.ucSchema6;
            controls[6] = this.ucSchema7;

            //������
            foreach (ucSchema us in controls)
            {
                us.IsCheckChangceAndSave = this.isCheckChangceAndSave;
                us.StopColor = this.StopColor;
                us.ExpiredColor = this.ExpiredColor;
                us.OutColor = this.OutColor;
                us.IsLockExpired = this.IsLockExpired;
                us.IsShowID = isShowID;
            }
        }

        /// <summary>
        /// ��ʼ��ģ��ؼ�
        /// </summary>
        private void InitControls()
        {
            MTOrder.ucSchema obj;

            for (int i = 0; i < 7; i++)
            {
                obj = controls[i];
                obj.Init(DateTime.Parse(tabControl1.TabPages[i].Tag.ToString()));
            }
        }

        /// <summary>
        /// �������������
        /// </summary>
        private void InitMT()
        {
            this.treeView1.Nodes.Clear();
            TreeNode parent = new TreeNode(FS.FrameWork.Management.Language.Msg("ҽ���б�"));
            this.treeView1.ImageList = this.treeView1.deptImageList;
            parent.ImageIndex = 5;
            parent.SelectedImageIndex = 5;
            this.treeView1.Nodes.Add(parent);

            FS.HISFC.BizLogic.Manager.Constant Mgr = new FS.HISFC.BizLogic.Manager.Constant();

            ArrayList al = Mgr.GetList("MT#MINFEE");
            if (al == null)
            {
                MessageBox.Show("��ȡҽ���б�ʱ����!" + Mgr.Err, "��ʾ");
                return;
            }

            foreach (FS.HISFC.Models.Base.Const MedTech in al)
            {
                TreeNode node = new TreeNode();
                node.Text = MedTech.Name;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 1;
                node.Tag = MedTech;

                parent.Nodes.Add(node);
            }

            this.cmbMedTech.AddItems(al);
            parent.ExpandAll();
        }

        /// <summary>
        /// ��ѯ�����Ű�
        /// </summary>
        private void QueryAll()
        {
            for (int i = 0; i < 7; i++)
            {
                this.tabControl1.SelectedIndex = i;
            }
        }


        /// <summary>
        /// Ĭ����ʾ�����Ű�
        /// </summary>
        private void QueryTodaySchema()
        {
            FS.HISFC.BizLogic.MedicalTechnology.Arrange MTMgr = new FS.HISFC.BizLogic.MedicalTechnology.Arrange();

            DateTime today = MTMgr.GetDateTimeFromSysDateTime();

            int Index = (int)today.DayOfWeek;
            Index--;
            if (Index == -1) Index = 6;

            this.tabControl1.SelectedIndex = Index;
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            int Index = this.tabControl1.SelectedIndex;

            if (controls[Index].IsChange())
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����Ѿ��ı�,�Ƿ񱣴�"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (controls[Index].Save() == -1)
                    {
                        e.Cancel = true;
                        controls[Index].Focus();
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node == null) return;

            FS.HISFC.Models.Base.Const Type = node.Tag as FS.HISFC.Models.Base.Const;

            controls[this.tabControl1.SelectedIndex].Query(Type);
        }
        
        /// <summary>
        /// ����
        /// </summary>
        private void Add()
        {
            controls[tabControl1.SelectedIndex].Add();
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        private void Del()
        {
            controls[tabControl1.SelectedIndex].Del();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            if (controls[tabControl1.SelectedIndex].Save() == -1)
            {
                controls[tabControl1.SelectedIndex].Focus();
                return;
            }

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"), "��ʾ");

            controls[tabControl1.SelectedIndex].Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Next()
        {
            DateTime Monday;

            
             Monday = this.GetMonday(DateTime.Parse(this.tabPage7.Tag.ToString()).AddDays(2));
                            
            this.setWeek(Monday);

            for (int i = 0; i < 7; i++)
            {
                this.controls[i].Tag = null;
                this.controls[i].SeeDate = DateTime.Parse(this.tabControl1.TabPages[i].Tag.ToString());
            }


            TreeNode node = this.treeView1.SelectedNode;
            if (node == null) return;

            FS.HISFC.Models.Base.Const Type = node.Tag as FS.HISFC.Models.Base.Const;

            controls[this.tabControl1.SelectedIndex].Query(Type);
        }

        private void Prior()
        {
            DateTime Monday;

            Monday = this.GetMonday(DateTime.Parse(this.tabPage1.Tag.ToString()).AddDays(-3));
            
            this.setWeek(Monday);

            for (int i = 0; i < 7; i++)
            {
                this.controls[i].Tag = null;
                this.controls[i].SeeDate = DateTime.Parse(this.tabControl1.TabPages[i].Tag.ToString());
            }


            TreeNode node = this.treeView1.SelectedNode;
            if (node == null) return;

            FS.HISFC.Models.Base.Const Type = node.Tag as FS.HISFC.Models.Base.Const;

            controls[this.tabControl1.SelectedIndex].Query(Type);
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        private void DelAll()
        {
            int Index = this.tabControl1.SelectedIndex;

            controls[Index].DelAll();
        }
        /// <summary>
        /// ����ģ��
        /// </summary>
        private void LoadTemplet()
        {
            frmSelectWeek f = new frmSelectWeek();

            DateTime week = DateTime.Parse(this.tabControl1.SelectedTab.Tag.ToString());
            f.SelectedWeek = week.DayOfWeek;
            if (f.ShowDialog() == DialogResult.Yes)
            {
                FS.HISFC.BizLogic.MedicalTechnology.Templet templetMgr = new FS.HISFC.BizLogic.MedicalTechnology.Templet();


                string ItemCode = "ALL";
                try
                {
                    //if (this.treeView1.SelectedNode.Index > 0)ʲô������Ӧ���Ը��ڵ����жϰ�
                    if (this.treeView1.SelectedNode.Parent != null)
                    {
                        ItemCode = (this.treeView1.SelectedNode.Tag as FS.HISFC.Models.Base.Const).ID;
                    }
                }
                catch (Exception)
                {                    
                }
               
                
                //��ȡȫ��ģ����Ϣ
                IList tempList = templetMgr.Query(f.SelectedWeek,ItemCode);
                if (tempList == null)
                {
                    MessageBox.Show("��ѯģ����Ϣʱ����!" + templetMgr.Err, "��ʾ");
                    return;
                }

                foreach (FS.HISFC.Models.MedicalTechnology.Templet templet in tempList)
                {
                    controls[this.tabControl1.SelectedIndex].Add(templet);
                }

                controls[this.tabControl1.SelectedIndex].Focus();
                f.Dispose();
            }
        }
        private void Find()
        {
            int Index = this.tabControl1.SelectedIndex;

            if (Index == 6)
            {
                Index = 0;
            }
            else
            {
                Index = Index + 1;
            }

            this.cmbMedTech.Focus();
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.Subtract || keyData == Keys.OemMinus)
            //{
            //    this.Del();
            //    return true;
            //}
            //else if (keyData == Keys.Add || keyData == Keys.Oemplus)
            //{
            //    this.Add();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();
            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            //{
            //    this.Save();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.C.GetHashCode())
            //{
            //    LoadTemplet();

            //    return true;
            //}
            //else if (keyData == Keys.F2)
            //{
            //    Next();

            //    return true;
            //}
            if (keyData == Keys.F1)
            {
                this.cmbMedTech.Focus();

                return true;
            }
            //else if (keyData == Keys.F3)
            //{
            //    this.Find();

            //    return true;
            //}

            return base.ProcessDialogKey(keyData);
        }
        // ѡ�������л���ʾ��������
        private void tabControl1_SelectionChanging(object sender, EventArgs e)
        {
            if (!this.isCheckChangceAndSave)
            {
                return;
            }
            int Index = this.tabControl1.SelectedIndex;

            if (controls[Index].IsChange())
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����Ѿ��޸�,�Ƿ񱣴�䶯"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (controls[Index].Save() == -1)
                    {
                        return;
                    }
                }
            }
        }

        private void tabControl1_SelectionChanged(object sender, EventArgs e)
        {
            //object obj = controls[this.tabControl1.SelectedIndex].Tag;

            //if (obj == null || obj.ToString() == "")
            //{
            //    this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            //    this.treeView1_AfterSelect(new object(), new System.Windows.Forms.TreeViewEventArgs(new TreeNode(), System.Windows.Forms.TreeViewAction.Unknown));

            //    controls[this.tabControl1.SelectedIndex].Tag = "Has Retrieve";
            //}
            this.treeView1_AfterSelect(null, null);
        }

        private void cmbMedTech_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
            {
                if ((node.Tag as FS.HISFC.Models.Base.Const).ID == this.cmbMedTech.Tag.ToString())
                {
                    this.treeView1.SelectedNode = node;
                    break;
                }
            }
        }

        private void cmbMedTech_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int Index = this.tabControl1.SelectedIndex;

                controls[Index].Focus();
            }
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("����ģ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C����, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X��һ��, true, false, null);
            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S��һ��, true, false, null);
            toolBarService.AddToolButton("ȫ��ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��, true, false, null);
            toolBarService.AddToolButton("ˢ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ˢ��":
                    this.Reflash();
                    break;
                case "����":
                    this.Add();

                    break;
                case "ɾ��":
                    this.Del();

                    break;
                case "����ģ��":
                    this.LoadTemplet();
                    break;
                case "����":
                    this.Find();
                    break;
                case "����":
                    Prior();

                    break;
                case "����":
                    Next();

                    break;
                case "ȫ��ɾ��":
                    DelAll();

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return base.OnSave(sender, neuObject);
        }
        /// <summary>
        /// �Զ�ˢ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ReflashTimer_Tick(object sender, EventArgs e)
        {
            Reflash();
        }

        /// <summary>
        /// ˢ��
        /// </summary>
        private void Reflash()
        {
            controls[this.tabControl1.SelectedIndex].Query();
        }
        /// <summary>
        /// �����Ű�XLS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            this.ExportData();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// ���ݵ���
        /// </summary>
        /// <returns></returns>
        private int ExportData()
        {
            FS.FrameWork.WinForms.Controls.NeuSpread fpSpreads = new FS.FrameWork.WinForms.Controls.NeuSpread();
            string[] Week = new string[] { "һ", "��", "��", "��", "��", "��", "��" };
            MTOrder.ucSchema sc;

            this.tabControl1.SelectedIndex = 0; //��֪Ϊ�ε�һ���޷����أ�ѡ����ٻ�ѡ
            this.QueryTodaySchema();
            
            for (int i = 0; i < 7; i++)
            {
                sc = controls[i];
                FarPoint.Win.Spread.SheetView sv = sc.GetFpSheet();
                sv.SheetName = "����" + Week[i];
                fpSpreads.Sheets.Add(sv);
            }
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel ������ (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    fpSpreads.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    MessageBox.Show("�����ɹ�", "��ʾ");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ݷ�������>>" + ex.Message);
                return -1;
            }
            return 1;
        }        




    }
}
