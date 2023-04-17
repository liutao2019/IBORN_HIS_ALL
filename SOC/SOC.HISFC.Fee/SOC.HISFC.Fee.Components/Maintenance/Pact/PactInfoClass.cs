using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    public class PactInfoClass
    {
        #region 构造函数

        public PactInfoClass()
            : this(null)
        {
        }

        public PactInfoClass(FS.HISFC.Models.Base.PactInfo pactInfo)
        {
            this.pactInfo = pactInfo ?? new FS.HISFC.Models.Base.PactInfo();
        }

        #endregion

        #region 变量

        private FS.HISFC.Models.Base.PactInfo pactInfo = null;

        #endregion

        #region 属性

        #region 基本信息

        [CategoryAttribute("A基本信息"),
        DescriptionAttribute("合同单位的代码"),
        DefaultValueAttribute("")]
        [ReadOnly(true)]
        public string 代码
        {
            get
            {
                return this.pactInfo.ID;
            }
        }

        [CategoryAttribute("A基本信息"),
        DescriptionAttribute("合同单位的名称"),
        DefaultValueAttribute("")]
        [ReadOnly(true)]
        public string 名称
        {
            get
            {
                return this.pactInfo.Name;
            }
            set
            {
                this.pactInfo.Name = value;
            }
        }

        [CategoryAttribute("A基本信息"),
        DescriptionAttribute("合同单位的名称简称"),
        DefaultValueAttribute("")]
        [ReadOnly(true)]
        public string 简称
        {
            get
            {
                return this.pactInfo.ShortName;
            }
            set
            {
                this.pactInfo.ShortName = value;
            }
        }

        [CategoryAttribute("A基本信息"),
        DescriptionAttribute("合同单位的顺序号"),
        DefaultValueAttribute(0)]
        [ReadOnly(true)]
        public int 序号
        {
            get
            {
                return this.pactInfo.SortID;
            }
            set
            {
                this.pactInfo.SortID = value;
            }
        }


        #endregion

        #region 费用相关

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("公费报销比例"),
        DefaultValueAttribute(0.0)]
        public decimal 公费比例
        {
            get
            {
                return this.pactInfo.Rate.PubRate;
            }
            set
            {
                this.pactInfo.Rate.PubRate = value;
            }
        }

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("自付报销比例"),
        DefaultValueAttribute(0.0)]
        public decimal 自付比例
        {
            get
            {
                return this.pactInfo.Rate.PayRate;
            }
            set
            {
                this.pactInfo.Rate.PayRate = value;
            }
        }

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("自费报销比例"),
        DefaultValueAttribute(1.0)]
        public decimal 自费比例
        {
            get
            {
                return this.pactInfo.Rate.OwnRate;
            }
            set
            {
                this.pactInfo.Rate.OwnRate = value;
            }
        }

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("优惠报销比例"),
        DefaultValueAttribute(0.0)]
        public decimal 优惠比例
        {
            get
            {
                return this.pactInfo.Rate.RebateRate;
            }
            set
            {
                this.pactInfo.Rate.RebateRate = value;
            }
        }

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("欠费报销比例"),
        DefaultValueAttribute(0.0)]
        public decimal 欠费比例
        {
            get
            {
                return this.pactInfo.Rate.ArrearageRate;
            }
            set
            {
                this.pactInfo.Rate.ArrearageRate = value;
            }
        }

        #endregion

        #region 限额相关

        [CategoryAttribute("C限额相关"),
        DescriptionAttribute("日限额"),
        DefaultValueAttribute(0.0)]
        public decimal 日限额
        {
            get
            {
                return this.pactInfo.DayQuota;
            }
            set
            {
                this.pactInfo.DayQuota = value;
            }
        }

        [CategoryAttribute("C限额相关"),
        DescriptionAttribute("月限额"),
        DefaultValueAttribute(0.0)]
        public decimal 月限额
        {
            get
            {
                return this.pactInfo.MonthQuota;
            }
            set
            {
                this.pactInfo.MonthQuota = value;
            }
        }

        [CategoryAttribute("C限额相关"),
        DescriptionAttribute("年限额"),
        DefaultValueAttribute(0.0)]
        public decimal 年限额
        {
            get
            {
                return this.pactInfo.YearQuota;
            }
            set
            {
                this.pactInfo.YearQuota = value;
            }
        }

        [CategoryAttribute("C限额相关"),
        DescriptionAttribute("一次限额"),
        DefaultValueAttribute(0.0)]
        public decimal 一次限额
        {
            get
            {
                return this.pactInfo.OnceQuota;
            }
            set
            {
                this.pactInfo.OnceQuota = value;
            }
        }

        [CategoryAttribute("C限额相关"),
        DescriptionAttribute("床位上限"),
        DefaultValueAttribute(0.0)]
        public decimal 床位上限
        {
            get
            {
                return this.pactInfo.BedQuota;
            }
            set
            {
                this.pactInfo.BedQuota = value;
            }
        }

        [CategoryAttribute("C限额相关"),
        DescriptionAttribute("空调上限"),
        DefaultValueAttribute(0.0)]
        public decimal 空调上限
        {
            get
            {
                return this.pactInfo.AirConditionQuota;
            }
            set
            {
                this.pactInfo.AirConditionQuota = value;
            }
        }

        #endregion

        #region 系统设置

        [CategoryAttribute("D系统设置"),
        DescriptionAttribute("结算类别，用于区分自费、医保、公费、特约单位、本院职工等类别的限制算法"),
        DefaultValueAttribute("")]
        [Editor(typeof(PayKindTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public FS.FrameWork.Models.NeuObject 结算类别
        {
            get
            {
                return PayKindTypeEditor.PayKindHelper.GetObjectFromID(this.pactInfo.PayKind.ID);
            }
            set
            {
                if (value != null)
                {
                    this.pactInfo.PayKind.ID = value.ID;
                    this.pactInfo.PayKind.Name = value.Name;
                }
            }
        }

        [CategoryAttribute("D系统设置"),
        DescriptionAttribute("价格形式，用于区分购入价、零售价、其他价格的算法"),
        DefaultValueAttribute("")]
        [Editor(typeof(PriceFormTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public FS.FrameWork.Models.NeuObject 价格形式
        {
            get
            {
                return PriceFormTypeEditor.PriceFormHelper.GetObjectFromID(this.pactInfo.PriceForm);
            }
            set
            {
                if (value != null)
                {
                    this.pactInfo.PriceForm = value.ID;
                }
            }
        }

        [CategoryAttribute("D系统设置"),
        DescriptionAttribute("显示类别，用于区分门诊、住院、体检、系统等系统的显示"),
        DefaultValueAttribute("")]
        [Editor(typeof(SystemTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public FS.FrameWork.Models.NeuObject 显示类别
        {
            get
            {
                return SystemTypeEditor.SystemTypeHelper.GetObjectFromID(this.pactInfo.PactSystemType);
            }
            set
            {
                if (value != null)
                {
                    this.pactInfo.PactSystemType = value.ID;
                }
            }
        }

        [CategoryAttribute("D系统设置"),
        DescriptionAttribute("项目类别，用于区分药品和非药品用的合同单位"),
        DefaultValueAttribute("")]
        [Editor(typeof(ItemTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public FS.FrameWork.Models.NeuObject 项目类别
        {
            get
            {
                return ItemTypeEditor.ItemTypeHelper.GetObjectFromID(this.pactInfo.ItemType);
            }
            set
            {
                if (value != null)
                {
                    this.pactInfo.ItemType = value.ID;
                }
            }
        }

        [CategoryAttribute("D系统设置"),
        DescriptionAttribute(@"待遇算法的DLL名称，位于\Plugins\SI目录下"),
        DefaultValueAttribute("")]
        [Editor(typeof(SIDllTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public FS.FrameWork.Models.NeuObject 待遇算法
        {
            get
            {
                return SIDllTypeEditor.SIDllHelper.GetObjectFromID(this.pactInfo.PactDllName);
            }
            set
            {
                if (value != null)
                {
                    this.pactInfo.PactDllName = value.ID;
                    this.pactInfo.PactDllDescription = value.Name;
                }
            }
        }

        #endregion

        #region 开关设置

        [CategoryAttribute("F开关设置"),
        DescriptionAttribute("是否需医疗证"),
        DefaultValueAttribute(EnumBoolean.否)]
        public EnumBoolean 是否需医疗证
        {
            get
            {
                return this.pactInfo.IsNeedMCard ? EnumBoolean.是 : EnumBoolean.否;
            }
            set
            {
                this.pactInfo.IsNeedMCard = (value == EnumBoolean.是 ? true : false);
            }
        }

        [CategoryAttribute("F开关设置"),
        DescriptionAttribute("是否进行监控"),
        DefaultValueAttribute(EnumBoolean.否)]
        public EnumBoolean 是否进行监控
        {
            get
            {
                return this.pactInfo.IsInControl ? EnumBoolean.是 : EnumBoolean.否;
            }
            set
            {
                this.pactInfo.IsInControl = (value == EnumBoolean.是 ? true : false);
            }
        }

        [CategoryAttribute("F开关设置"),
        DescriptionAttribute("是否婴儿标记"),
        DefaultValueAttribute(EnumBoolean.否)]
        public EnumBoolean 是否婴儿标记
        {
            get
            {
                return this.pactInfo.Rate.IsBabyShared ? EnumBoolean.是 : EnumBoolean.否;
            }
            set
            {
                this.pactInfo.Rate.IsBabyShared = (value == EnumBoolean.是 ? true : false);
            }
        }

        [CategoryAttribute("F开关设置"),
        DescriptionAttribute("是否变更明细"),
        DefaultValueAttribute(EnumBoolean.否)]
        public EnumBoolean 是否变更明细
        {
            get
            {
                return this.pactInfo.IsNeedMCard ? EnumBoolean.是 : EnumBoolean.否;
            }
            set
            {
                this.pactInfo.IsNeedMCard = (value==EnumBoolean.是 ? true : false);
            }
        }

        [CategoryAttribute("F开关设置"),
        DescriptionAttribute("是否有效"),
        DefaultValueAttribute(EnumBoolean.是)]
        public EnumBoolean 是否有效
        {
            get
            {
                return this.pactInfo.ValidState == "1" ? EnumBoolean.是 : EnumBoolean.否;
            }
            set
            {
                this.pactInfo.ValidState = (value == EnumBoolean.是 ? "1" : "0");
            }
        }

        #endregion

        #endregion

        #region 方法

        public new FS.HISFC.Models.Base.PactInfo ToString()
        {
            return this.pactInfo;
        }

        #endregion

        #region 内部类

      public  abstract  class BaseTypeEditor : System.Drawing.Design.UITypeEditor
        {
            private ArrayList al = new ArrayList();
            public BaseTypeEditor(ArrayList al)
            {
                this.al = al;
            }

            System.Windows.Forms.Design.IWindowsFormsEditorService editorService = null;

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                if (context != null && context.Instance != null && provider != null)
                {
                    editorService = (System.Windows.Forms.Design.IWindowsFormsEditorService)provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
                    if (editorService != null)
                    {

                        System.Windows.Forms.ListBox editorControl = new System.Windows.Forms.ListBox();
                        editorControl.Items.AddRange(
                            al.ToArray()
                            );
                        editorControl.MouseClick += new System.Windows.Forms.MouseEventHandler(editorControl_MouseDoubleClick);
                        editorService.DropDownControl(editorControl);
                        if (editorControl.SelectedItem != null)
                        {
                            value = editorControl.SelectedItem;
                        }
                        return base.EditValue(context, provider, value);
                    }
                }

                return base.EditValue(context, provider, value);

            }

            void editorControl_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                if (editorService != null)
                {
                    editorService.CloseDropDown();
                }
            }

            public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                if (context != null && context.Instance != null)
                {
                    return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
                }

                return base.GetEditStyle(context);
            }

        }

        /// <summary>
        /// 合同单位下拉选择框
        /// </summary>
       public   class PayKindTypeEditor : BaseTypeEditor
        {
             private static FS.FrameWork.Public.ObjectHelper payKindHelper = new FS.FrameWork.Public.ObjectHelper(FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.PAYKIND));

            public PayKindTypeEditor()
                : base(payKindHelper.ArrayObject)
            {
            }


            /// <summary>
            /// 价格形式帮助属性
            /// </summary>
            public static FS.FrameWork.Public.ObjectHelper PayKindHelper
            {
                get
                {
                    return payKindHelper;
                }
            }
        }

        /// <summary>
        /// 价格形式下拉选择框
        /// </summary>
        public class PriceFormTypeEditor : BaseTypeEditor
        {
            private static FS.FrameWork.Public.ObjectHelper priceFormHelper = new FS.FrameWork.Public.ObjectHelper(new ArrayList(new NeuObject[] {
            new NeuObject("0","默认价",string.Empty),
            new NeuObject("1","特诊价",string.Empty),
            new NeuObject("2","儿童价",string.Empty),
            new NeuObject("3","购入价",string.Empty)
            }));

            public PriceFormTypeEditor():
                base(priceFormHelper.ArrayObject)
            {
            }

            /// <summary>
            /// 价格形式帮助属性
            /// </summary>
            public static FS.FrameWork.Public.ObjectHelper PriceFormHelper
            {
                get
                {
                    return priceFormHelper;
                }
            }

        }

         /// <summary>
         /// 显示类别下拉选择框
         /// </summary>
        public class SystemTypeEditor : BaseTypeEditor
         {
             private static FS.FrameWork.Public.ObjectHelper systemTypeHelper = new FS.FrameWork.Public.ObjectHelper(new ArrayList(new NeuObject[] {
            new NeuObject("0","全院",string.Empty),
            new NeuObject("1","门诊",string.Empty),
            new NeuObject("2","住院",string.Empty),
            new NeuObject("3","系统",string.Empty)
            }));

             public SystemTypeEditor():
                 base(systemTypeHelper.ArrayObject)
             {
             }

             /// <summary>
             /// 显示类别帮助属性
             /// </summary>
             public static FS.FrameWork.Public.ObjectHelper SystemTypeHelper
             {
                 get
                 {
                     return systemTypeHelper;
                 }
             }

         }

         /// <summary>
         /// 项目类别下拉选择框
         /// </summary>
        public class ItemTypeEditor : BaseTypeEditor
         {
             private static FS.FrameWork.Public.ObjectHelper itemTypeHelper = new FS.FrameWork.Public.ObjectHelper(new ArrayList(new NeuObject[] {
            new NeuObject("0","全部",string.Empty),
            new NeuObject("1","药品",string.Empty),
            new NeuObject("2","非药品",string.Empty)
            }));

             public ItemTypeEditor():base(itemTypeHelper.ArrayObject)
             {
             }

             /// <summary>
             /// 显示类别帮助属性
             /// </summary>
             public static FS.FrameWork.Public.ObjectHelper ItemTypeHelper
             {
                 get
                 {
                     return itemTypeHelper;
                 }
             }

         }

        /// <summary>
        /// 医保待遇接口Dll
        /// </summary>
        public class SIDllTypeEditor : BaseTypeEditor
         {
             protected static string DllPath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Plugins\SI\";

             /// <summary>
             /// 通过反射查找所有医保待遇DLL
             /// </summary>
             /// <returns></returns>
             protected static  ArrayList GetDllName()
             {
                 string[] sPath = System.IO.Directory.GetFiles(DllPath);
                 if (sPath.Length == 0)
                     return null;
                 ArrayList list = new ArrayList();
                 foreach (string path in sPath)
                 {
                     System.IO.FileInfo fi = new System.IO.FileInfo(path);
                     if (fi.Extension.ToLower() == ".dll")
                     {
                         FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare im = GetDllInterface(path);
                         if (im == null)
                             continue;
                         list.Add(new FS.FrameWork.Models.NeuObject(fi.Name, im.Description, string.Empty));
                     }
                 }

                 return list;
             }

             /// <summary>
             /// 根据Dll查找dll描述
             /// </summary>
             /// <param name="path">dll路径</param>
             /// <returns></returns>
             protected static  FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare GetDllInterface(string path)
             {
                 try
                 {
                     System.Reflection.Assembly assmbly = System.Reflection.Assembly.LoadFile(path);
                     Type[] t = assmbly.GetTypes();
                     if (t == null)
                         return null;
                     foreach (Type type in t)
                     {
                         if (type.GetInterface(typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare).ToString()) != null)
                         {
                             return (FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)System.Activator.CreateInstance(type);
                         }
                     }
                     return null;
                 }
                 catch
                 {
                     return null;
                 }
             }

             private static FS.FrameWork.Public.ObjectHelper sIDllTypeHelper = new FS.FrameWork.Public.ObjectHelper(GetDllName());

             public SIDllTypeEditor():base(sIDllTypeHelper.ArrayObject)
             {
                 
             }

             /// <summary>
             /// 显示类别帮助属性
             /// </summary>
             public static FS.FrameWork.Public.ObjectHelper SIDllHelper
             {
                 get
                 {
                     return sIDllTypeHelper;
                 }
             }
         }

        #endregion

    }

    /// <summary>
    /// Boolean枚举转换
    /// </summary>
    public enum EnumBoolean
    {
        是 = 1,
        否 = 0
    }


}
