using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucInterfaceSetting : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInterfaceSetting()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ���ӿ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitTreeView() 
        {
            TreeNode parentNode = new TreeNode();

            parentNode.Tag = new ArrayList();
            parentNode.Text = "�����շѽӿ�";

            this.tvInterface.Nodes.Add(parentNode);

            ArrayList interfaces = this.managerIntegrate.GetConstantList("MZINTF");
            if (interfaces == null) 
            {
                MessageBox.Show("�������ӿڲ������ó���!" + this.managerIntegrate.Err);

                return -1;
            }
            if (interfaces.Count == 0) 
            {
                MessageBox.Show("û��ά���κ�����ӿڲ���!");

                return -1;
            }

            foreach (FS.HISFC.Models.Base.Const c in interfaces) 
            {
                TreeNode tnInterface = new TreeNode();

                tnInterface.Tag = c.Memo;
                tnInterface.Text = c.Name;

                parentNode.Nodes.Add(tnInterface);
            }

            this.tvInterface.ExpandAll();

            return 1;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public virtual int Init() 
        {
            if (this.InitTreeView() == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �ж�ѡ����ļ��Ƿ����ʵ�ֽӿ�UC
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <param name="interfaceName">�ӿ���</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        protected virtual bool IsValidFileToInterface(string fileName, string interfaceName) 
        {
            bool isValid = false;

            try
            {
                Assembly a = Assembly.LoadFrom(fileName);
                System.Type[] types = a.GetTypes();

                foreach (System.Type type in types)
                {
                    if (type.GetInterface(interfaceName) != null)
                    {   
                        isValid = true;

                        break;
                    }
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show("����������!" + e.Message);

                return false;
            }

            if(!isValid)
            {
                MessageBox.Show("��ѡ���DLL������ʵ�ֽӿڵ�UC,������ѡ��!");

                return false;
            }

            return isValid;
        }

        /// <summary>
        /// ���ʵ�ʽӿ���
        /// </summary>
        /// <param name="interfaceFullName">�ӿ�ȫ��,���������ռ�</param>
        /// <returns>�ɹ� ʵ�ʽӿ��� ʧ�� null</returns>
        protected string GetInterfaceName(string interfaceFullName) 
        {
            string[] s = interfaceFullName.Split('.');

            int count = s.Length;

            return s[count - 1];
        }

        /// <summary>
        /// ���ʵ���ļ���
        /// </summary>
        /// <param name="dllFullName">ȫ��,��·��</param>
        /// <returns>�ɹ� ʵ���ļ��� ʧ�� null</returns>
        protected string GetDLLName(string dllFullName) 
        {
            string[] s = dllFullName.Split('\\');

            int count = s.Length;

            return s[count - 1];
        }

        /// <summary>
        /// Ԥ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected int Preview() 
        {
            if (this.tvInterface.SelectedNode.Level == 0)
            {
                return -1;
            }
            string objNameSpace = this.txtNameSpace.Text;
            string assemblyName = this.txtDll.Text.Substring(0, this.txtDll.Text.Length - 4);

            if (objNameSpace == string.Empty) 
            {
                MessageBox.Show("����ȷѡ����!");

                return -1;
            }

            System.Runtime.Remoting.ObjectHandle objPreview = null;

            try
            {

                objPreview = System.Activator.CreateInstance(assemblyName, objNameSpace);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ԥ��ʧ��!" + e.Message);

                return -1;
            }

            if (objPreview == null) 
            {
                MessageBox.Show("����ʧ��!��ȷ����ѡ���dll��uc����ȷ������!");

                return -1;
            }

            object obj = objPreview.Unwrap();

            if (obj is Form) 
            {
                ((Form)obj).ShowDialog();

                return 1;
            }

            if (obj is Control)
            {
                Control c = obj as Control;

                this.plPreview.Enabled = false;

                this.plPreview.Controls.Add(c);

                c.Location = new Point(0, 0);
            }
            else 
            {
                MessageBox.Show("�ÿؼ���֧��Ԥ��!");
            }

            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        protected void Clear() 
        {
            this.txtDll.Text = string.Empty;
            this.txtDll.Tag = string.Empty;
            this.txtNameSpace.Text = string.Empty;
            this.txtNameSpace.Tag = string.Empty;
            this.cmbControl.Items.Clear();
            this.cmbControl.Text = string.Empty;
            this.plPreview.Controls.Clear();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Save() 
        {
            FS.HISFC.Models.Base.ControlParam tempControlObj = null;//��ʱ������ʵ��;

            string tempControlValue = null;// �ӽ����ȡ�Ŀ��Ʋ���ֵ

            TreeNode selectedNode = this.tvInterface.SelectedNode;

            if (selectedNode == null || selectedNode.Level == 0) 
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


            string consName = FS.HISFC.BizProcess.Integrate.Const.GetOutpatientPlugInConstNameByInterfaceName(selectedNode.Tag.ToString());

            tempControlValue = this.txtDll.Tag.ToString().Replace(Application.StartupPath, string.Empty) + "|" + this.txtNameSpace.Text;
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = consName;
            tempControlObj.Name = selectedNode.Text;
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            int iReturn = this.managerIntegrate.InsertControlerInfo(tempControlObj);
            if (iReturn == -1)
            {
                //�����ظ���˵���Ѿ����ڲ���ֵ,��ôֱ�Ӹ���
                if (managerIntegrate.DBErrCode == 1)
                {
                    iReturn = this.managerIntegrate.UpdateControlerInfo(tempControlObj);
                    if (iReturn <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���¿��Ʋ���[" + tempControlObj.Name + "]ʧ��! ���Ʋ���ֵ:" + tempControlObj.ID + "\n������Ϣ:" + this.managerIntegrate.Err);

                        return -1;
                    }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������Ʋ���[" + tempControlObj.Name + "]ʧ��! ���Ʋ���ֵ:" + tempControlObj.ID + "\n������Ϣ:" + this.managerIntegrate.Err);

                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�!");

            return 1;
        }

        #endregion

        private void ucInterfaceSetting_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode) 
            {
                if (this.Init() == -1) 
                {
                    return;
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog1.ShowDialog();

            if (result == DialogResult.Cancel) 
            {
                return;
            }

            string fileName = this.openFileDialog1.FileName;

            if (fileName == string.Empty) 
            {
                MessageBox.Show("��ѡ��dll");

                return;
            }

            TreeNode selectedNode = this.tvInterface.SelectedNode;

            if (selectedNode == null || selectedNode.Level == 0)
            {
                return;
            }

            if (selectedNode.Tag == null)
            {
                MessageBox.Show("�ӿ������ؼ���ֵ����!");

                return;
            }

            string interfaceName = selectedNode.Tag.ToString();

            interfaceName = this.GetInterfaceName(interfaceName);

            bool isValid = this.IsValidFileToInterface(fileName, interfaceName);

            if (!isValid) 
            {
                return;
            }

            this.txtDll.Text = this.GetDLLName(fileName);
            this.txtDll.Tag = fileName;

            ArrayList ucList = new ArrayList();

            try
            {
                Assembly a = Assembly.LoadFrom(fileName);
                System.Type[] types = a.GetTypes();

                foreach (System.Type type in types)
                {
                    if (type.GetInterface(interfaceName) != null)
                    {
                        FS.FrameWork.Models.NeuObject objFile = new FS.FrameWork.Models.NeuObject();
                        objFile.ID = type.Namespace;
                        objFile.Name = type.Name;

                        ucList.Add(objFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����������!" + ex.Message);

                return;
            }

            this.cmbControl.AddItems(ucList);

            this.cmbControl.SelectedIndex = 0;
        }

        private void cmbControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtNameSpace.Text = this.cmbControl.Tag.ToString();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.Preview();
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            
            return base.OnSave(sender, neuObject);
        }

        private void tvInterface_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) 
            {
                this.Clear();

                return;
            }

            string consName = FS.HISFC.BizProcess.Integrate.Const.GetOutpatientPlugInConstNameByInterfaceName(e.Node.Tag.ToString());

            string controlValue = controlParamIntegrate.GetControlParam<string>(consName, true, string.Empty);

            if (controlValue == string.Empty) 
            {
                this.Clear();

                return;
            }

            string dllName = controlValue.Split('|')[0];
            string nameSpace = controlValue.Split('|')[1];
            string interfaceName = this.GetInterfaceName(e.Node.Tag.ToString());

            this.txtDll.Text = this.GetDLLName(dllName);
            this.txtDll.Tag = Application.StartupPath + dllName;
            this.txtNameSpace.Text = nameSpace;


            string fileName = this.txtDll.Tag.ToString();

            ArrayList ucList = new ArrayList();

            this.cmbControl.Items.Clear();

            try
            {
                Assembly a = Assembly.LoadFrom(fileName);
                System.Type[] types = a.GetTypes();

                foreach (System.Type type in types)
                {
                    if (type.GetInterface(interfaceName) != null)
                    {
                        FS.FrameWork.Models.NeuObject objFile = new FS.FrameWork.Models.NeuObject();
                        objFile.ID = type.Namespace + "." + type.Name;
                        objFile.Name = type.Name;

                        ucList.Add(objFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("����������!" + ex.Message);

                return;
            }

            this.cmbControl.AddItems(ucList);
            //{2F578D7F-9E77-491c-BFCE-940F702F8B06}
            this.cmbControl.SelectedIndexChanged -=new EventHandler(cmbControl_SelectedIndexChanged);
            this.cmbControl.Tag = nameSpace;
            this.cmbControl.SelectedIndexChanged+=new EventHandler(cmbControl_SelectedIndexChanged);
        }
    }
}
