using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Order.OutPatientOrder.GYZL.Common;
using FS.FrameWork.Models;
using FS.HISFC.Models.Order.OutPatient;
using System.Drawing;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.DiagNoseBill
{
    /// <summary>
    /// 医生诊断证明书
    /// </summary>
    public partial class ucOrderProve : UserControl,
        FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {

        public ucOrderProve()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 员工显示工号的位数
        /// </summary>
        private int showEmplLength = -1;

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        /// <summary>
        /// 是否打印签名信息？（否则手工签名）
        /// </summary>
        private int isPrintSignInfo = -1;

        /// <summary>
        /// 是否英文
        /// </summary>
        public bool bEnglish = false;

        /// <summary>
        /// 存储处方组合号
        /// </summary>
        private Hashtable hsCombID = new Hashtable();

        /// <summary>
        /// 用法帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freHelper = null;

        /// <summary>
        /// 频次信息
        /// </summary>
        FS.HISFC.Models.Order.Frequency frequencyObj = null;

        private string judPrint = string.Empty;
        #endregion


        #region  业务类
        /// <summary>
        /// 医嘱原子业务层
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.Order OrderManagement = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 诊断原子业务层
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 管理
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 药品业务逻辑层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 常数维护业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region 函数

        /// <summary>
        /// 获取字节长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padLength"></param>
        /// <returns></returns>
        private string SetToByteLength(string str, int padLength)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 1;
                }
            }

            if (padLength - str.Length - len > 0)
            {
                return str + "".PadRight(padLength - str.Length - len, ' ');
            }
            else
            {
                return str;
            }
        }


        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {

            if (register == null)
            {
                return;
            }

            this.lblName.Text = register.Name;

            if (register.Pact.PayKind.ID == "03")
            {
                try
                {
                    FS.HISFC.BizLogic.Fee.PactUnitInfo pact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                    FS.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(register.Pact.ID);
                }
                catch
                { }
            }

            //年龄按照统一格式
            this.lblAge.Text = this.OrderManagement.GetAge(register.Birthday, false);

            this.lblSex.Text = register.Sex.Name;

            this.lblCardNo.Text = register.PID.CardNO;

            this.judPrint = register.User03;

            this.lblAddr.Text = register.AddressHome;

            try
            {
                ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                if (al == null)
                {
                    return;
                }
                string strDiagHappenNO = "";
                string strDiag = "";
                if (strDiagHappenNO == null || strDiagHappenNO == "")
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
                    {
                        if (diag != null && diag.Memo != null && diag.Memo != "")
                        {
                            strDiag += diag.Memo + "、";
                        }
                        else
                        {
                            strDiag += diag.DiagInfo.ICD10.Name;
                        }
                    }
                    strDiag = strDiag.TrimEnd(new char[] { '、' });
                    this.lblDiag.Text = strDiag;
                }
                else
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
                    {
                        if (diag.DiagInfo.HappenNo.ToString() == strDiagHappenNO)
                        {
                            this.lblDiag.Text = diag.Memo;
                        }
                    }
                }
            }
            catch
            { }
        }

        private void GetHospLogo()
        {
            Common.ComFunc cf = new FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.ComFunc();
            string erro = "出错";
            string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager || judPrint == "BD")
            {
                print.PrintPage(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }
        #endregion


        #region IOutPatientOrderPrint 成员
        /// <summary>
        ///  实现接口打印功能<泛型>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IList"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            //设置医院名称
            this.lblhos.Text = constMgr.GetHospitalName();

            //设置医院图标
            this.GetHospLogo();

            //设置病历信息
            this.SetPatientInfo(regObj);

            Dictionary<string, IList<FS.HISFC.Models.Order.OutPatient.Order>> recipeInfoDic = new Dictionary<string, IList<FS.HISFC.Models.Order.OutPatient.Order>>();

            this.Print();

            return 1;
        }


        /// <summary>
        /// 实现接口打印功能
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        #endregion

        #region IOutPatientOrderPrint Members


        public void SetPage(string pageStr)
        {
        }

        #endregion
    }
}
