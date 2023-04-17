using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.SOC.Local.Account.ZhuHai.ZDWY.OpenCard
{
    public partial class frmMiltScreen : Form, FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen
    {
        public frmMiltScreen()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 项目信息
        /// </summary>
        private DataSet dsItem = null;

        /// <summary>
        /// 费用待遇接口
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = null;

        /// <summary>
        /// 属于药品的最小费用列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region 属性

        /// <summary>
        /// 属于药品的最小费用列表
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper DrugFeeCodeHelper 
        {
            set 
            {
                this.drugFeeCodeHelper = value;
            }
        }

        /// <summary>
        /// 设置待遇接口变量
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy 
        {
            set 
            {
                this.medcareInterfaceProxy = value;
            }
        }
        private bool isPreeFee = false;
        ///
        /// <summary>
        /// 医保患者是否预结算
        /// </summary>
        public bool IsPreFee
        {
            set
            {
                this.isPreeFee = value;
            }
            get
            {
                return this.isPreeFee;
            }
        }


        #endregion

        #region 方法

        /// <summary>
        /// 设置属于药品的最小费用列表
        /// </summary>
        /// <param name="drugFeeCodeHelper">属于药品的最小费用列表</param>
        public void SetFeeCodeIsDrugArrayListObj(FS.FrameWork.Public.ObjectHelper drugFeeCodeHelper) 
        {
            this.drugFeeCodeHelper = drugFeeCodeHelper;
        }

        /// <summary>
        /// 设置待遇接口变量
        /// </summary>
        /// <param name="medcareInterfaceProxy">接口变量</param>
        public void SetMedcareInterfaceProxy(FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy) 
        {
            this.medcareInterfaceProxy = medcareInterfaceProxy;
        }

        
        #endregion


        /// <summary>
        /// 设置项目信息
        /// </summary>
        /// <param name="dsItem">项目信息集合</param>
        public void SetDataSet(DataSet dsItem) 
        {
            this.dsItem = dsItem;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public int Init() 
        {
            if (this.drugFeeCodeHelper != null && (this.drugFeeCodeHelper.ArrayObject == null || this.drugFeeCodeHelper.ArrayObject.Count == 0))
            {
                ArrayList drugFeeCodeList = this.managerIntegrate.GetConstantList("DrugMinFee");
                if (drugFeeCodeList == null)
                {
                    MessageBox.Show(Language.Msg("获得药品最小费用列表出错!") + this.managerIntegrate.Err);

                    return -1;
                }
                
                this.drugFeeCodeHelper.ArrayObject = drugFeeCodeList;
            }

            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear() 
        {
            

            this.tbDrugCost.Text = "";
            
            this.tbRealOwnCost.Text = "";
            this.tbReturnCost.Text = "";
             
            this.tbTotCost.Text = "";
            this.tbPayCost.Text = "";
             
            this.tbDrugCost.Text = "";
            this.lblPaientInfo.Text = "请注意核对基本信息！";//patient.Name + "  正在办理划价交费，请稍后";
             
        }

        #region IMultiScreen 成员

        public System.Collections.Generic.List<Object> ListInfo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.Clear();
                
                if (value != null)
                {
                    //患者信息，卡号，收费员id，收费员姓名
                    this.SetShowInfomation(value[0] as FS .HISFC .Models .RADT .PatientInfo , value[1] as string, value[2] as string, value[3] as string);
                    
                }
                
            }
        }

        public void SetShowInfomation(FS .HISFC .Models .RADT .PatientInfo a, string b, string c, string d)
        {
            if (c.ToString() == "")
            { 
                this.neuPanel4.Visible = true;
                this.neuPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
                this.neuLabel9.Visible = false;
                this.neuLabel10.Visible = false;


                this.lblPaientInfo.Text = "请注意核对基本信息！";
                this.neuLabel7.Text = "姓名：";
                this.tbRealOwnCost.Text = a.Name.ToString();
                this.neuLabel6.Text = "性别：";
                this.tbReturnCost.Text = a.Sex.ToString();
                this.neuLabel8.Text = "出生年月：";
                this.tbTotCost.Text = a.Birthday.ToShortDateString();//a.Age.ToString();
                 
                this.tbPayCost.Text = a.Kin.RelationPhone.ToString();
                 
                //this.tbDrugSendInfo.Text = "卡号：" + b.ToString();
            }
            else
            {//显示初始化界面
                this.neuPanel4.Visible = false;
                this.neuLabel9.Visible = true;
                this.neuLabel9.Text = c.ToString() + "为您服务";// d.ToString();
                this.neuLabel10.Visible = true;
            }

        }

        public void ScreenInvisible()
        {
            this.Visible = false;
        }

        public int ShowScreen()
        {
            this.Clear();

            if (Screen.AllScreens.Length > 1)
            {
                this.Show();

                //this.DesktopBounds = Screen.AllScreens[1].Bounds;
                //this.DesktopBounds = Screen.AllScreens[0].Bounds;
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            return 0;
        }

        #endregion

        #region IMultiScreen 成员

        public int CloseScreen()
        {
            //this.Close();
            this.ScreenInvisible();
            return 0;
        }
        #endregion
    }
}
