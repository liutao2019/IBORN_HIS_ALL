using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.Fee.Interface.Components;
using FS.SOC.HISFC.Fee.Components.Helper;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    /// <summary>
    /// [功能描述:合同单位维护的合同单位显示界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-2]<br></br>
    /// 说明：
    /// </summary>
     partial class ucPropertyGrid : FS.FrameWork.WinForms.Controls.ucBaseControl,IPactInfoProperty
    {
        public ucPropertyGrid()
        {
            InitializeComponent();
        }

        private Object[] selectObjectPacts = null;
        private Object[] selectObjectItems = null;

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<object[]> propertyValueChanged;

        #region 初始化

        private void initEvents()
        {
            this.propertyGridItem.PropertyValueChanged -= new PropertyValueChangedEventHandler(propertyGridItem_PropertyValueChanged);
            this.propertyGridItem.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGridItem_PropertyValueChanged);

            this.propertyGridPact.PropertyValueChanged -= new PropertyValueChangedEventHandler(propertyGridPact_PropertyValueChanged);
            this.propertyGridPact.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGridPact_PropertyValueChanged);
        }

        #endregion

        #region 方法

        private void setProperty(object[] selectObjects, System.Windows.Forms.PropertyGrid propertyGrid)
        {
            if (selectObjects != null && selectObjects.Length > 0)
            {
                Object pactInfoClass = new Object();
                pactInfoClass = selectObjects[0] as Object;
                List<System.Reflection.PropertyInfo> list = new List<System.Reflection.PropertyInfo>();
                System.Reflection.PropertyInfo[] propertyInfos = pactInfoClass.GetType().GetProperties();

                for (int i = 1; i < selectObjects.Length; i++)
                {
                    Object o = selectObjects[i];
                    foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
                    {
                        if (propertyInfo.GetValue(pactInfoClass, null) is FS.FrameWork.Models.NeuObject)
                        {
                            FS.FrameWork.Models.NeuObject NeuObjA = propertyInfo.GetValue(pactInfoClass, null) as FS.FrameWork.Models.NeuObject;
                            FS.FrameWork.Models.NeuObject NeuObjB = o.GetType().GetProperty(propertyInfo.Name).GetValue(o, null) as FS.FrameWork.Models.NeuObject;

                            if (NeuObjA != null && NeuObjB != null && NeuObjB.ID != NeuObjA.ID)
                            {
                                list.Add(propertyInfo);
                            }
                        }
                        else if (propertyInfo.GetValue(pactInfoClass, null) != null && !propertyInfo.GetValue(pactInfoClass, null).Equals(o.GetType().GetProperty(propertyInfo.Name).GetValue(o, null)))
                        {
                            //不可变化的属性
                            list.Add(propertyInfo);
                        }
                    }
                }

                CustomPropertyCollection collection = new CustomPropertyCollection();

                //进行属性的添加
                foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
                {
                    if (!propertyInfo.CanWrite && selectObjects.Length > 1)
                    {
                        continue;
                    }
                    Object[] customAttr = propertyInfo.GetCustomAttributes(false);
                    CustomProperty customProperty = new CustomProperty();
                    customProperty.Name = propertyInfo.Name;
                    customProperty.PropertyNames = new string[] { propertyInfo.Name };
                    customProperty.ObjectSource = pactInfoClass;
                    customProperty.IsBrowsable = true;
                    
                    bool isAdd = true;
                    foreach (Object o in customAttr)
                    {
                        if (o is CategoryAttribute)
                        {
                            customProperty.Category = ((CategoryAttribute)o).Category;
                        }
                        else if (o is ReadOnlyAttribute)
                        {
                            if (selectObjects.Length > 1)
                            {
                                //customProperty.IsReadOnly = ((ReadOnlyAttribute)o).IsReadOnly;
                                isAdd = false;
                                continue;
                            }
                            else
                            {
                                customProperty.IsReadOnly = !propertyInfo.CanWrite;
                            }
                        }
                        else if (o is DescriptionAttribute)
                        {
                            customProperty.Description = ((DescriptionAttribute)o).Description;
                        }
                        else if (o is DefaultValueAttribute)
                        {
                            customProperty.DefaultValue = ((DefaultValueAttribute)o).Value;
                        }
                        else if (o is EditorAttribute)
                        {
                            customProperty.EditorType = Type.GetType(((EditorAttribute)o).EditorTypeName);
                        }
                    }

                    if (isAdd)
                    {
                        if (list.Contains(propertyInfo))
                        {
                            if (propertyInfo.CanWrite)
                            {
                                customProperty.Value = null;
                            }
                        }
                        collection.Add(customProperty);
                    }
                }

                propertyGrid.SelectedObject = collection;
            }
            else
            {
                propertyGrid.SelectedObject = null;
            }
        }

        #endregion

        #region 事件

        void propertyGridItem_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.selectObjectItems != null)
            {
                if (this.propertyValueChanged != null)
                {
                    foreach (Object o in this.selectObjectItems)
                    {
                        o.GetType().GetProperty(e.ChangedItem.Label).SetValue(o, e.ChangedItem.Value, null);
                    }

                    this.propertyValueChanged(this.selectObjectItems);
                }
            }
        }

        void propertyGridPact_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.selectObjectPacts != null)
            {
                if (this.propertyValueChanged != null)
                {
                    foreach (Object o in this.selectObjectPacts)
                    {
                        o.GetType().GetProperty(e.ChangedItem.Label).SetValue(o, e.ChangedItem.Value, null);
                    }

                    this.propertyValueChanged(this.selectObjectPacts);
                }
            }
        }

        #endregion

        #region IPactInfoProperty 成员

        public void ShowProperty(params object[] selectObjects)
        {
            this.initEvents();

            this.selectObjectPacts = selectObjects;
            this.setProperty(this.selectObjectPacts, this.propertyGridPact); 
            this.neuTabControl1.SelectedTab = this.tpPactInfo;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<object[]> PropertyValueChanged
        {
            get
            {
                return propertyValueChanged;
            }
            set
            {
                propertyValueChanged = value;
            }
        }

        public void ShowDetailProperty(params object[] selectObjects)
        {
            this.initEvents();

            this.selectObjectItems = selectObjects;
            this.setProperty(this.selectObjectItems, this.propertyGridItem);
            this.neuTabControl1.SelectedTab = this.tpItemInfo;
        }

        #endregion
    }
}
