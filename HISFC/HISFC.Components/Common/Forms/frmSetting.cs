using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        #region ҵ������
        FS.HISFC.BizProcess.Interface.Common.IControlParamMaint Icpm = null;
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��TreeView
        /// </summary>
        private void InitTreeControl()
        {
            #region ����
            //����
            System.Reflection.Assembly assembly;
            //�����ļ���չ���ж��Ƿ���dll
            FileInfo fi;
            //��������������
            Type[] t;
            #endregion 

            #region ��ʼ��treeview

            //���Treeview��node
            this.trvControl.Nodes.Clear();
            this.trvControl.ImageList = this.trvControl.deptImageList;
            //���ظ��ڵ�
            TreeNode rnode = new TreeNode();
            
            rnode.Text = "ϵͳģ��";
            rnode.Tag = "Root";
            rnode.ImageIndex = 2;
            rnode.SelectedImageIndex = 2;
            //��ǰhis.exe�������ļ���·��
            string[] s = Directory.GetFiles(Application.StartupPath);
            foreach (string file in s)
            {
                #region ���س��򼯣��õ��ó�������������
                //�жϸ��ļ��Ƿ���.dll�ļ�
                fi = new FileInfo(file);
                if (fi.Extension.ToLower() != ".dll") continue;
                try
                {
                    //���س���
                    assembly = System.Reflection.Assembly.LoadFrom(file);
                    if (assembly == null)
                    {
                        continue;
                    }
                    //��ȡ�ó�������������
                    t = assembly.GetTypes();
                    if (t == null)
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
                #endregion

                #region ����õ��ö���
                foreach (Type type in t)
                {
                    //�жϸ��������Ƿ�̳�IControlParamMaint�ӿ�
                    if (type.GetInterface("IControlParamMaint") != null)
                    {
                        //ͨ������õ�objectHandle
                        System.Runtime.Remoting.ObjectHandle o = System.Activator.CreateInstance(type.Assembly.ToString(), type.Namespace + "." + type.Name);
                        if (o != null)
                        {
                            //�õ��ñ���װ�Ķ��󣬲�ת��ΪConrol
                            Icpm = o.Unwrap() as FS.HISFC.BizProcess.Interface.Common.IControlParamMaint;
                            TreeNode node = new TreeNode(Icpm.Description);
                            node.Tag = Icpm;
                            node.ImageIndex = 0;
                            node.ImageIndex = 1;
                            rnode.Nodes.Add(node);
                        }
                    }
                }
                
                #endregion
            }
            this.trvControl.Nodes.Add(rnode);
            this.trvControl.ExpandAll();
            #endregion
        }
        
        /// <summary>
        /// ��������
        /// </summary>
        private void Save()
        {
            if (Icpm == null) return;
            Icpm.Save();
        }

        #endregion

        #region �¼�
        private void frmSetting_Load(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ݣ����Ժ�^^");
            Application.DoEvents();
            InitTreeControl();
            if (this.trvControl.Nodes[0].Nodes.Count > 0)
                this.trvControl.SelectedNode = this.trvControl.Nodes[0].Nodes[0];
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void trvControl_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Tag.ToString() == "Root") return;
                if (this.PanelControl.Controls.Count > 0) this.PanelControl.Controls.Clear();
                Control c = null;
                c = (Control)e.Node.Tag;
                c.Dock = DockStyle.Fill;
                this.PanelControl.Controls.Add(c);
                Icpm = (FS.HISFC.BizProcess.Interface.Common.IControlParamMaint)c;
                Icpm.IsShowOwnButtons = false;
                if (Icpm.Init() < 0)
                {
                    MessageBox.Show(Icpm.Description + "ģ���ʼ��ʧ�ܣ�", "����");
                }
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}