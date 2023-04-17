using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FS.HISFC.Components.DrugStore.Base
{
    /// <summary>
    /// [�ؼ�����: MarkPropertyGrid]<br></br>
    /// [��������: ��Ч��������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    public partial class MarkPropertyGrid : System.Windows.Forms.PropertyGrid
    {
        public MarkPropertyGrid()
        {
            InitializeComponent();

            this.InitService();
        }

        public MarkPropertyGrid(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.InitService();
        }

        protected MarkService mkService;

        /// <summary>
        /// ��ʼ���Զ������
        /// </summary>
        private void InitService()
        {
            this.mkService = new MarkService(this);

            System.Type propertyGridType = typeof(System.Windows.Forms.PropertyGrid);
            System.Reflection.FieldInfo gridField = propertyGridType.GetField("gridView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //��ȡgridViewʵ��
            object gridViewRef = gridField.GetValue(this);

            System.Reflection.FieldInfo spInfo = gridField.FieldType.GetField("serviceProvider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //�����ֶ�ֵ
            spInfo.SetValue(gridViewRef, this.mkService);
        }

        protected override object GetService(Type service)
        {
            return GetServiceInternal(service);
        }

        public object GetServiceInternal(Type service)
        {
            if (service == typeof(System.Windows.Forms.Design.IUIService))
            {
                return this.mkService;
            }
            return base.GetService(service);
        }

        public object GetBaseService(Type service)
        {
            return base.GetService(service);
        }


        /// <summary>
        /// �Ƿ���������С����
        /// </summary>
        private bool isUseDecimalPlace = false;

        /// <summary>
        /// �Ƿ���������С����
        /// </summary>
        public bool IsUseDecimalPlace
        {
            get
            {
                return this.isUseDecimalPlace;
            }
            set
            {
                this.isUseDecimalPlace = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ�����ж�
        /// </summary>
        /// <returns></returns>
        private bool IsJudge()
        {
            System.Windows.Forms.GridItem gridItem = base.SelectedGridItem;
            if (gridItem == null)
            {
                return false;
            }

            if (gridItem.PropertyDescriptor.PropertyType == typeof(System.Decimal))
                return true;

            if (gridItem.PropertyDescriptor.PropertyType == typeof(System.Int16))
                return true;

            if (gridItem.PropertyDescriptor.PropertyType == typeof(System.Int32))
                return true;

            if (gridItem.PropertyDescriptor.PropertyType == typeof(System.Int64))
                return true;

            return false;
        }

        protected override bool ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            if (this.IsJudge())
            {
                if ((char)keyData == 229)                                 //����ȫ�Ǽ�����
                {
                    return true;
                }

                if (!this.isUseDecimalPlace)
                {
                    if ((char)keyData == 190)                               //.
                    {
                        return true;
                    }
                }

                if ((char)keyData == 8 || (char)keyData == 46)          //�س� �� Del��
                {
                    return base.ProcessDialogKey(keyData);
                }

                bool isValidNum = false;
                if ((char)keyData >= 48 && (char)keyData <= 57)         //��ͨ���ּ� 0 ~ 9
                {
                    isValidNum = true;
                }
                if ((char)keyData >= 96 && (char)keyData <= 105)           //С���̼� 0 �� 9
                {
                    isValidNum = true;
                }

                if (!isValidNum)
                {
                    return true;
                }   
            }
                        
            return base.ProcessDialogKey(keyData);
        }
    }

    /// <summary>
    /// ������ ���ڶԴ�����Ϣ�Ĵ���
    /// </summary>
    public partial class MarkService : System.Windows.Forms.Design.IUIService, IServiceProvider
    {

        public MarkService(MarkPropertyGrid markControl)
        {
            this.markProperty = markControl;
        }

        protected MarkPropertyGrid markProperty;

        private System.Windows.Forms.Design.IUIService defaultService = null;

        /// <summary>
        /// ��ȡԭ�ؼ�Ĭ��Service
        /// </summary>
        /// <returns></returns>
        private System.Windows.Forms.Design.IUIService GetDefaultService()
        {
            return this.markProperty.GetBaseService(typeof(System.Windows.Forms.Design.IUIService)) as System.Windows.Forms.Design.IUIService;
        }

        #region IUIService ��Ա

        /// <summary>
        /// ����༭���Ƿ������ʾ���������ģ�� (COM) �е�����ҳ���Ƶ�����༭������
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool CanShowComponentEditor(object component)
        {
            return false;
        }

        public System.Windows.Forms.IWin32Window GetDialogOwnerWindow()
        {
            if (this.defaultService != null)
            {
                return this.defaultService.GetDialogOwnerWindow();
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService == null)
                {
                    return null;
                }
                else
                {
                    return this.defaultService.GetDialogOwnerWindow();
                }
            }
        }

        public void SetUIDirty()
        {
            if (this.defaultService != null)
            {
                this.defaultService.SetUIDirty();
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService != null)
                {
                    this.defaultService.SetUIDirty();
                }
            }
        }

        public bool ShowComponentEditor(object component, System.Windows.Forms.IWin32Window parent)
        {
            if (this.defaultService != null)
            {
                return this.defaultService.ShowComponentEditor(component,parent);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService == null)
                {
                    return false;
                }
                else
                {
                    return this.defaultService.ShowComponentEditor(component, parent);
                }
            }
        }

        public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.Form form)
        {
            Type propGridType = typeof(System.Windows.Forms.PropertyGrid);
            System.Reflection.FieldInfo gvInfo = propGridType.GetField("gridView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //��ȡgridViewʵ��
            object gridViewRef = gvInfo.GetValue(this.markProperty);

            System.Reflection.FieldInfo edInfo = gvInfo.FieldType.GetField("errorDlg", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (form.GetType() == edInfo.FieldType)
            {
                //��ȡerrorDlgʵ��
                object errorDlgRef = edInfo.GetValue(gridViewRef);

                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ȷ������ֵ"));

                if (this.defaultService != null)
                {
                    return this.defaultService.ShowDialog(form);
                }
                else
                {
                    this.defaultService = this.GetDefaultService();
                    if (this.defaultService == null)
                    {
                        return System.Windows.Forms.DialogResult.Cancel;
                    }
                    else
                    {
                        return this.defaultService.ShowDialog(form);
                    }
                }

                //return System.Windows.Forms.DialogResult.OK;


                //System.Reflection.PropertyInfo msgInfo = edInfo.FieldType.GetProperty("Message");
                //System.Reflection.PropertyInfo detInfo = edInfo.FieldType.GetProperty("Details");

                //string det = ""; detInfo.SetValue(errorDlgRef, "sfd", null);//(errorDlgRef, System.Reflection.BindingFlags.NonPublic, null, null, null);
                //string msg = ""; msgInfo.SetValue(errorDlgRef, "ss", null);//(errorDlgRef, null));

                //IUIService iUI = m_PropertyGrid.GetServiceExInternal(typeof(IUIService)) as IUIService;
                //if (iUI == null)
                //    return DialogResult.Cancel;


                //return iUI.ShowDialog(form);

                //MessageBox.Show(m_PropertyGrid, msg + ": " + det, "Invalid PropertyValue");

                //return DialogResult.OK;
            }

            if (this.defaultService != null)
            {
                return this.defaultService.ShowDialog(form);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService == null)
                {
                    return System.Windows.Forms.DialogResult.Cancel;
                }
                else
                {
                    return this.defaultService.ShowDialog(form);
                }
            }
        }

        public void ShowError(Exception ex, string message)
        {
            if (this.defaultService != null)
            {
                this.defaultService.ShowError(ex,message);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService != null)
                {
                    this.defaultService.ShowError(ex, message);
                }
            }
        }

        public void ShowError(Exception ex)
        {
            if (this.defaultService != null)
            {
                this.defaultService.ShowError(ex);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService != null)
                {
                    this.defaultService.ShowError(ex);
                }
            }
        }

        public void ShowError(string message)
        {
            if (this.defaultService != null)
            {
                this.defaultService.ShowError( message);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService != null)
                {
                    this.defaultService.ShowError( message);
                }
            }
        }

        public System.Windows.Forms.DialogResult ShowMessage(string message, string caption, System.Windows.Forms.MessageBoxButtons buttons)
        {
            if (this.defaultService != null)
            {
                return this.defaultService.ShowMessage( message,caption,buttons);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService != null)
                {
                    return this.defaultService.ShowMessage(message, caption, buttons);
                }
                else
                {
                    return System.Windows.Forms.DialogResult.Cancel;
                }
            }
        }

        public void ShowMessage(string message, string caption)
        {
            if (this.defaultService != null)
            {
                this.defaultService.ShowMessage(message, caption);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService != null)
                {
                    this.defaultService.ShowMessage(message, caption);
                }
            }
        }

        public void ShowMessage(string message)
        {
            if (this.defaultService != null)
            {
                this.defaultService.ShowMessage(message);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService != null)
                {
                    this.defaultService.ShowMessage(message);
                }
            }
        }

        public bool ShowToolWindow(Guid toolWindow)
        {
            if (this.defaultService != null)
            {
                return this.defaultService.ShowToolWindow(toolWindow);
            }
            else
            {
                this.defaultService = this.GetDefaultService();
                if (this.defaultService != null)
                {
                    return this.defaultService.ShowToolWindow(toolWindow);
                }
                else
                {
                    return false;
                }
            }
        }

        public System.Collections.IDictionary Styles
        {
            get
            {
                if (this.defaultService != null)
                {
                    return this.defaultService.Styles;
                }
                else
                {
                    this.defaultService = this.GetDefaultService();
                    if (this.defaultService != null)
                    {
                        return this.defaultService.Styles;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        #endregion

        #region IServiceProvider ��Ա

        public object GetService(Type serviceType)
        {
            return this.markProperty.GetServiceInternal(serviceType);
        }

        #endregion
    }
}
