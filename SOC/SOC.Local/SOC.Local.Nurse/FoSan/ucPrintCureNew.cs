using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Nurse.FoSan
{
    /// <summary>
    /// ucPrintCureNew<br></br>
    /// <Font color='#FF1111'>[功能描述:门诊注射瓶签打印{EB016FFE-0980-479c-879E-225462ECA6D0}]</Font><br></br>
    /// [创 建 者: 耿晓雷]<br></br>
    /// [创建时间: 2010-7-29]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///		/>
    /// </summary>
    public partial class ucPrintCureNew : Neusoft.FrameWork.WinForms.Controls.ucBaseControl,Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint,Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint
    {
        #region 构造函数
        public ucPrintCureNew()
        {
            InitializeComponent();
        }

        private bool isReprint = false;

        /// <summary>
        /// 是否补打
        /// </summary>
        public bool IsReprint
        {
            get
            {
                return isReprint;
            }
            set
            {
                isReprint = value;
                //this.lblReprint.Visible = value;
            }
        }
        #endregion

        /// <summary>
        /// 门诊费用管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee patientMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 缓存用法
        /// </summary>
        public static Neusoft.FrameWork.Public.ObjectHelper usageHelper;

        #region 注射类用法

        /// <summary>
        /// 判断用法是否为注射类用法
        /// </summary>
        /// <param name="usageNO">用法代码</param>
        /// <returns></returns>
        public static bool IsInjectUsage(string usageNO)
        {
            bool isInjectUsage = false;

            if (string.IsNullOrEmpty(usageNO))
            {
                return isInjectUsage;
            }

            if (usageHelper == null)
            {
                usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();
                Neusoft.HISFC.BizLogic.Manager.Constant constantMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();
                usageHelper.ArrayObject = constantMgr.GetAllList(Neusoft.HISFC.Models.Base.EnumConstant.USAGE);
            }
            if (usageHelper == null || usageHelper.ArrayObject == null)
            {
                return isInjectUsage;
            }
            Neusoft.FrameWork.Models.NeuObject neuObject = usageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return isInjectUsage;
            }
            Neusoft.HISFC.Models.Base.Const usage = (Neusoft.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return isInjectUsage;
            }
            switch (usage.UserCode)
            {
                //case "IAST"://皮试
                //    isInjectUsage = true;
                //    break;
                //case "IH"://皮下注射
                //    isInjectUsage = true;
                //    break;
                //case "IM"://肌注
                //    isInjectUsage = true;
                //    break;
                //case "IV"://静注
                //    isInjectUsage = true;
                //    break;
                //case "IVD"://静滴
                //    isInjectUsage = true;
                //    break;
                //case "IZ"://肿瘤注射
                //    isInjectUsage = true;
                //    break;
                //case "IO"://其它注射
                //    isInjectUsage = true;
                //    break;
                case "IAST"://皮试
                    isInjectUsage = true;
                    break;
                case "IH"://皮下注射
                    isInjectUsage = true;
                    break;
                case "IV"://静注
                    isInjectUsage = true;
                    break;
                case "IM"://肌注
                    isInjectUsage = true;
                    break;
                case "IZ"://肿瘤注射
                    isInjectUsage = true;
                    break;
                case "IO"://其它注射
                    isInjectUsage = true;
                    break;
                default:
                    isInjectUsage = false;
                    break;
            }

            return isInjectUsage;
        }

        #endregion

        #region IInjectCurePrint 成员

        public void Init(System.Collections.ArrayList alPrintData)
        {
            //用来将要分开打的数据分开
            Neusoft.HISFC.Models.Nurse.Inject info = alPrintData[0] as Neusoft.HISFC.Models.Nurse.Inject;
            if (!IsReprint && info.Item.ConfirmedInjectCount == 0 && info.User03 != "true")
            {
                ucPrintItinerateLarge ucPrintIL = new ucPrintItinerateLarge();
                ucPrintIL.Init(alPrintData);
            }
            Hashtable htInject = new Hashtable();
            foreach (Neusoft.HISFC.Models.Nurse.Inject inject in alPrintData)
            {
                if(IsInjectUsage(inject.Item.Order.Usage.ID))
                {
                    continue;
                }
                string key = inject.Item.Order.Combo.ID;
                if (htInject.ContainsKey(key))
                {
                    List<Neusoft.HISFC.Models.Nurse.Inject> injectList = htInject[key] as List<Neusoft.HISFC.Models.Nurse.Inject>;
                    injectList.Add(inject);
                }
                else
                {
                    List<Neusoft.HISFC.Models.Nurse.Inject> injectList = new List<Neusoft.HISFC.Models.Nurse.Inject>();
                    htInject.Add(key, injectList);
                    injectList.Add(inject);
                }
            }
            //分别打印
            int controlsHeight = 0;
            int pageCount = 1;
            int totPageCount = htInject.Count;
            foreach (string key in htInject.Keys)
            {
                ucPrintCureNewControl ucPrint = new ucPrintCureNewControl();
                ucPrint.SetData(htInject[key] as List<Neusoft.HISFC.Models.Nurse.Inject>, totPageCount, pageCount, this.IsReprint);
                pageCount++;
                ucPrint.Dock = DockStyle.Top;
                this.Controls.Add(ucPrint);
                controlsHeight += ucPrint.Height;
            }
            //控件高度
            this.Height = controlsHeight;

        }

        #endregion
    }
}
