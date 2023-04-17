using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

namespace FS.HISFC.Components.Registration.SelfReg
{
    /// <summary>
    /// [功能描述: 自助挂号]<br></br>
    /// [创 建 者: 牛鑫元]<br></br>
    /// [创建时间: 2009-9]<br></br>
    /// <说明
    ///		贵港本地化
    ///  />
    /// </summary>
    public partial class ucSelfHelpReg : Form
    {
        public ucSelfHelpReg()
        {
            InitializeComponent();

        }

        #region 域
        /// <summary>
        ///  综合管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 如初转业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 挂号管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        private FS.HISFC.BizLogic.Registration.Schema schema = new FS.HISFC.BizLogic.Registration.Schema();

        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();

        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 医保接口代理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 患者管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT patientMgr = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper consHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 参数控制类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 读取的身份信息
        /// </summary>
        Functions.PERSONINFOW person = new Functions.PERSONINFOW();

        /// <summary>
        /// 是否读取信息
        /// </summary>
        bool isReadPatientInfo = true;
        //[DllImport("user32.dll")]
        //public static extern bool ReleaseCapture();
        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// 出诊医生集合
        /// </summary>
        DataSet dsItems;

        /// <summary>
        /// 患者排号方式 1医生 2科室 3诊区
        /// </summary>
        string strGetSeeNoType = string.Empty;

        /// <summary>
        /// 分页显示科室
        /// </summary>
        Hashtable htDept = new Hashtable();

        /// <summary>
        /// 分页显示医生
        /// </summary>
        Hashtable htDoct = new Hashtable();

        /// <summary>
        /// 科室页码
        /// </summary>
        int iPageDept = 1;

        /// <summary>
        /// 医生页码
        /// </summary>
        int iPageDoct = 1;

        /// <summary>
        /// 科室页数
        /// </summary>
        int iPageDeptCount = 1;

        /// <summary>
        /// 医生页数
        /// </summary>
        int iPageDoctCount = 1;

        /// <summary>
        /// 最长操作时间
        /// </summary>
        int iOperTime = 30;

        /// <summary>
        /// 查询的患者信息多于一个选择患者UC
        /// </summary>
        frmQueryPatientByName frmPatient = null;
        #endregion

        #region 属性
        /// <summary>
        /// 是否选用弹出窗口
        /// </summary>
        [Category("控件设置"), Description("是否选用弹出窗口"), DefaultValue(false)]
        //public bool IsPobForm
        //{
        //    set
        //    {
        //        this.plRight.Visible = !value;
        //        this.btChooseDept.Visible = value;
        //    }
        //    get
        //    {
        //        return (!this.plRight.Visible && this.btChooseDept.Visible);
        //    }
        //}
        #endregion

        #region 方法
        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <returns></returns>
        private int InitInfo()
        {
            this.FindForm().FormClosing += new FormClosingEventHandler(ucRegSelfHelp_FormClosing);
            this.FindForm().Resize += new EventHandler(ucRegSelfHelp_Resize);
            this.FindForm().Activated += new EventHandler(ucRegSelfHelp_Activated);
            this.FindForm().MaximizeBox = false;
            this.FindForm().MinimizeBox = false;
            this.FindForm().ControlBox = false;
            this.lblTip.Text = "欢迎使用自助挂号系统，请您";
            this.lblTipExtend.Text = "刷卡！";
            this.InitData();
            this.ShowDeptInfo();
            this.consHelper.ArrayObject = feeMgr.QueryPactUnitOutPatient();
            this.strGetSeeNoType = this.controlParamManager.GetControlParam<string>("ZZGH01", true, "1");
            return 1;
        }

        /// <summary>
        /// 出诊医生集合
        /// </summary>
        /// <returns></returns>
        private int InitData()
        {
            dsItems = new DataSet();
            dsItems.Tables.Add("Doct");
            dsItems.Tables["Doct"].Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("ID",System.Type.GetType("System.String")),
                    new DataColumn("Name",System.Type.GetType("System.String")),
                    new DataColumn("Spell_Code",System.Type.GetType("System.String")),
                    new DataColumn("Wb_code",System.Type.GetType("System.String")),					
                    new DataColumn("RegLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Reged",System.Type.GetType("System.Decimal")),
                    new DataColumn("TelLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Teled",System.Type.GetType("System.Decimal")),
                    new DataColumn("SpeLmt",System.Type.GetType("System.Decimal")),
                    new DataColumn("Sped",System.Type.GetType("System.Decimal")),
                    new DataColumn("BeginTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("EndTime",System.Type.GetType("System.DateTime")),
                    new DataColumn("Noon",System.Type.GetType("System.String")),
                    new DataColumn("IsAppend",System.Type.GetType("System.Boolean")),
                    new DataColumn("Memo",System.Type.GetType("System.String")),
                    new DataColumn("IsProfessor",System.Type.GetType("System.Boolean")),
                    new DataColumn("user_code",System.Type.GetType("System.String")),
                    //新增一列保存候诊人数 ygch {8BF9E56B-828E-4264-9D2F-B0B74FB920B5}
                    new DataColumn("num",System.Type.GetType("System.String"))
                });
            dsItems.CaseSensitive = false;

            return 1;
        }

        void ucRegSelfHelp_Activated(object sender, EventArgs e)
        {
            this.FindForm().WindowState = FormWindowState.Maximized;
        }

        void ucRegSelfHelp_Resize(object sender, EventArgs e)
        {
            this.FindForm().WindowState = FormWindowState.Maximized;
        }

        void ucRegSelfHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            HISFC.Components.Common.Forms.frmValidUserPassWord frm = new FS.HISFC.Components.Common.Forms.frmValidUserPassWord();

            DialogResult dia = frm.ShowDialog();

            if (dia == DialogResult.OK)
            {
            }
            else
            {
                e.Cancel = true;
            }
            return;
        }


        /// <summary>
        /// 根据就诊卡号查询患者基本信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string cardNO)
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtIntegrate.QueryComPatientInfo(cardNO);
            if (patientInfo == null)
            {
                MessageBox.Show("查询患者基本信息出错");
                return null;
            }

            if (string.IsNullOrEmpty(patientInfo.PID.CardNO))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有找到该患者信息"));
                return null;
            }

            //界面赋值
            this.ucSelfHelpPatientInfo1.PatientInfo = patientInfo;



            this.txtDept.Focus();
            this.lblTip.Text = FS.FrameWork.Management.Language.Msg("请您选择挂号科室");
            this.lblTipExtend.Text = "";
            return patientInfo;
        }

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register GetRegisterInfo()
        {
            FS.HISFC.Models.Registration.Register register = null;

            if (this.patientInfo != null && !string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
            {
                register = new FS.HISFC.Models.Registration.Register();
                register.PID.CardNO = this.patientInfo.PID.CardNO;
                register.Name = this.patientInfo.Name;
                register.Sex.ID = this.patientInfo.Sex.ID;
                register.Birthday = this.patientInfo.Birthday;
                register.Pact.ID = this.patientInfo.Pact.ID;
                register.Pact.PayKind.ID = this.patientInfo.Pact.PayKind.ID;
                register.SSN = this.patientInfo.SSN;
                register.PhoneHome = this.patientInfo.PhoneHome;
                register.AddressHome = this.patientInfo.AddressHome;
                register.IDCard = this.patientInfo.IDCard;
                register.NormalName = this.patientInfo.NormalName;
                register.IsEncrypt = this.patientInfo.IsEncrypt;
                register.IDCard = this.patientInfo.IDCard;
                if (this.patientInfo.IsEncrypt == true)
                {
                    register.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.patientInfo.NormalName);
                }

                register.CardType.ID = this.patientInfo.Memo;

                //挂号流水号
                register.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
                register.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//正交易

                //this.regObj.DoctorInfo.Templet.RegLevel.ID = this.cmbRegLevel.Tag.ToString();
                //this.regObj.DoctorInfo.Templet.RegLevel.Name = this.cmbRegLevel.Text;
                register.DoctorInfo.Templet.Dept.ID = (this.txtDept.Tag as FS.FrameWork.Models.NeuObject).ID;
                register.DoctorInfo.Templet.Dept.Name = (this.txtDept.Tag as FS.FrameWork.Models.NeuObject).Name;
                if (this.txtDoct.Tag != null)
                {
                    register.DoctorInfo.Templet.Doct.ID = (this.txtDoct.Tag as FS.FrameWork.Models.NeuObject).ID;
                    register.DoctorInfo.Templet.Doct.Name = (this.txtDoct.Tag as FS.FrameWork.Models.NeuObject).Name;
                }
                else
                {
                    register.DoctorInfo.Templet.Doct.ID = string.Empty;
                    register.DoctorInfo.Templet.Doct.Name = string.Empty;
                }
                register.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;
                register.Pact = this.patientInfo.Pact;

                register.DoctorInfo.SeeDate = this.regMgr.GetDateTimeFromSysDateTime();
                register.DoctorInfo.Templet.RegLevel.ID = "1";
                register.DoctorInfo.Templet.RegLevel.Name = "普通挂号";

                FS.HISFC.Models.Base.Noon noon = this.getNoon(register.DoctorInfo.SeeDate);
                register.DoctorInfo.Templet.Noon = noon;
                register.DoctorInfo.Templet.Begin = register.DoctorInfo.SeeDate.Date;
                register.DoctorInfo.Templet.End = register.DoctorInfo.SeeDate.Date;
                int returnValue = this.GetRegFee(register);
                //if (returnValue < 0)
                //{
                //    MessageBox.Show("获得挂号费失败");
                //    return null;
                //}
                register.RegLvlFee.RegFee = 0;
                register.RegLvlFee.ChkFee = 0;
                register.RegLvlFee.OwnDigFee = 0;
                register.RegLvlFee.OthFee = 0;
                register.OwnCost = 0;
                //处方号
                //  this.regObj.InvoiceNO = this.txtRecipeNo.Text.Trim();
                register.RecipeNO = "1";
                register.IsFee = false;
                register.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
                register.IsSee = false;
                register.InputOper.ID = this.regMgr.Operator.ID;
                register.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
                //add by niuxinyuan
                register.DoctorInfo.SeeDate = register.InputOper.OperTime;
                register.CancelOper.ID = "";
                register.CancelOper.OperTime = DateTime.MinValue;
                string invoice = this.feeIntegrate.GetNewInvoiceNO("R");
                if (invoice == null)
                {
                    MessageBox.Show(this.feeIntegrate.Err);
                    return null;
                }

                register.InvoiceNO = invoice;
                //查询患者就诊记录出错
                int regCount = this.regMgr.QueryRegiterByCardNO(register.PID.CardNO);
                if (regCount == 1)
                {
                    register.IsFirst = false;
                }
                else
                {
                    if (regCount == 0)
                    {
                        register.IsFirst = true;
                    }
                    else
                    {
                        MessageBox.Show("查询患者就诊记录出错");
                        return null;
                    }
                }

                if (register.DoctorInfo.Templet.Noon.ID == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("未维护午别信息,请先维护"), "提示");
                    return null;
                }
                register.DoctorInfo.Templet.ID = "";
            }

            return register;
        }

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Noon getNoon(DateTime current)
        {
            FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

            System.Collections.ArrayList alNoon = noonMgr.Query();
            if (alNoon == null)
            {
                MessageBox.Show("获取午别信息时出错!" + noonMgr.Err, "提示");
                return null;
            }
            if (alNoon == null) return null;
            /*
             * 理解错误：以为午别应该是包含一天全部时间上午：06~12,下午:12~18其余为晚上,
             * 实际午别为医生出诊时间段,上午可能为08~11:30，下午为14~17:30
             * 所以如果挂号员如果不在这个时间段挂号,就有可能提示午别未维护
             * 所以改为根据传人时间所在的午别例如：9：30在06~12之间，那么就判断是否有午别在
             * 06~12之间，全包含就说明9:30是那个午别代码
             */
            //			foreach(FS.HISFC.Models.Registration.Noon obj in alNoon)
            //			{
            //				if(int.Parse(current.ToString("HHmmss"))>=int.Parse(obj.BeginTime.ToString("HHmmss"))&&
            //					int.Parse(current.ToString("HHmmss"))<int.Parse(obj.EndTime.ToString("HHmmss")))
            //				{
            //					return obj.ID;
            //				}
            //			}



            //int[,] zones = new int[,] { { 0, 120000 }, { 120000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(current.ToString("HHmmss"));
            //int begin = 0, end = 0;

            //for (int i = 0; i < 3; i++)
            //{
            //    if (zones[i, 0] <= time && zones[i, 1] > time)
            //    {
            //        begin = zones[i, 0];
            //        end = zones[i, 1];
            //        break;
            //    }
            //}

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (time >= int.Parse(obj.StartTime.ToString("HHmmss")) &&
                   time <= int.Parse(obj.EndTime.ToString("HHmmss")))
                {
                    return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns></returns>
        private int GetRegFee(FS.HISFC.Models.Registration.Register regObj)
        {
            FS.HISFC.Models.Registration.RegLvlFee p = this.regFeeMgr.Get(regObj.Pact.ID, regObj.DoctorInfo.Templet.RegLevel.ID);
            if (p == null)//出错
            {
                return -1;
            }
            if (p.ID == null || p.ID == "")//没有维护挂号费
            {
                return 1;
            }

            regObj.RegLvlFee = p;

            regObj.OwnCost = p.ChkFee + p.OwnDigFee + p.RegFee + p.OthFee;
            regObj.PayCost = 0;
            regObj.PubCost = 0;

            return 0;
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.txtCardNO.Text = string.Empty;
            this.txtCardNO.Focus();
            this.ucSelfHelpPatientInfo1.Clear();
            this.txtDept.Text = string.Empty;
            this.txtDept.Tag = null;
            this.txtDoct.Text = string.Empty;
            this.txtDoct.Tag = null;
            this.patientInfo = null;
            this.lblTip.Text = "欢迎使用自助挂号系统，请您";
            this.lblTipExtend.Text = "刷卡！";
            this.isReadPatientInfo = true;
            this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
            this.ResetSheet2();
        }

        /// <summary>
        /// 更新医生或科室的看诊序号
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            #region 患者排号方式

            if (this.strGetSeeNoType == "1" && doctID != null && doctID != "")
            {
                Type = "1";//医生
                Subject = doctID;
            }
            else if (this.strGetSeeNoType == "2")
            {
                Type = "2";//科室
                Subject = deptID;
            }
            else if (this.strGetSeeNoType == "3")
            {
                Type = "3";//诊区
                Subject = deptID;
            }

            #endregion

            //更新看诊序号
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //获取看诊序号		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 更新全院看诊序号
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(DateTime current, ref int seeNo,
            ref string Err)
        {
            //更新看诊序号
            //全院是全天大排序，所以午别不生效，默认 1
            if (this.regMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = regMgr.Err;
                return -1;
            }

            //获取全院看诊序号
            if (this.regMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = regMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 设置farpoint
        /// </summary>
        /// <param name="alColections"></param>
        /// <param name="strType"></param>
        private void SetFarpointValue(ArrayList alColections, string strType)
        {
            if (alColections == null || alColections.Count == 0)
            {
                if (strType == "Doct")
                {
                    this.ResetSheet2();
                    return;
                }
                else if (strType == "Dept")
                {
                    return;
                }
            }

            // decimal rowCount = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(alColections.Count / 3)); //求余和商
            int myMod = 0;
            int rowCount = Math.DivRem(alColections.Count, 3, out myMod);

            if (myMod > 0)
            {
                rowCount = rowCount + 1;
            }

            int j = 0;
            for (int i = 0; i < alColections.Count; i++)
            {
                int k = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(i / 3))); //求余和商

                int mod = 0;

                Math.DivRem(i, 3, out mod);

                FS.FrameWork.Models.NeuObject obj = alColections[i] as FS.FrameWork.Models.NeuObject;

                FarPoint.Win.Spread.CellType.ButtonCellType btCell = new FarPoint.Win.Spread.CellType.ButtonCellType();
                if (strType == "Dept")
                {
                    this.neuSpread1_Sheet1.RowCount = FS.FrameWork.Function.NConvert.ToInt32(rowCount);
                    this.neuSpread1_Sheet1.ColumnCount = 3;

                    btCell.Text = obj.Name;
                    this.neuSpread1_Sheet1.Cells[k, mod].CellType = btCell;
                    btCell.Picture = global::FS.HISFC.Components.Registration.Properties.Resources.科室;
                    this.neuSpread1_Sheet1.Cells[k, mod].Tag = obj;
                }
                else if (strType == "Doct")
                {
                    this.neuSpread1_Sheet2.RowCount = FS.FrameWork.Function.NConvert.ToInt32(rowCount);
                    this.neuSpread1_Sheet2.ColumnCount = 3;

                    btCell.Text = obj.Name + "\n(" + obj.Memo + ")";
                    this.neuSpread1_Sheet2.Cells[k, mod].CellType = btCell;
                    btCell.Picture = global::FS.HISFC.Components.Registration.Properties.Resources.医生;
                    this.neuSpread1_Sheet2.Cells[k, mod].Tag = obj;
                }
            }
        }

        /// <summary>
        /// 重置farpoint科室sheet页
        /// </summary>
        private void ResetSheet1()
        {
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "医生";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet1.ColumnCount = 3;
            this.neuSpread1_Sheet1.ColumnHeader.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = 5;
            this.neuSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet1.Columns.Get(0).Width = 121F;
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 121F;
            this.neuSpread1_Sheet1.Columns.Get(2).Width = 121F;
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.Rows.Get(0).Height = 108F;
            this.neuSpread1_Sheet1.Rows.Get(1).Height = 108F;
            this.neuSpread1_Sheet1.Rows.Get(2).Height = 108F;
            this.neuSpread1_Sheet1.Rows.Get(3).Height = 108F;
            this.neuSpread1_Sheet1.Rows.Get(4).Height = 108F;
            this.neuSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
        }

        /// <summary>
        /// 重置farpoint医生sheet页
        /// </summary>
        private void ResetSheet2()
        {
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.SheetName = "医生";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.neuSpread1_Sheet2.ColumnCount = 3;
            this.neuSpread1_Sheet2.ColumnHeader.RowCount = 0;
            this.neuSpread1_Sheet2.RowCount = 5;
            this.neuSpread1_Sheet2.RowHeader.ColumnCount = 0;
            this.neuSpread1_Sheet2.Columns.Get(0).Width = 121F;
            this.neuSpread1_Sheet2.Columns.Get(1).Width = 121F;
            this.neuSpread1_Sheet2.Columns.Get(2).Width = 121F;
            this.neuSpread1_Sheet2.GrayAreaBackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.Rows.Get(0).Height = 108F;
            this.neuSpread1_Sheet2.Rows.Get(1).Height = 108F;
            this.neuSpread1_Sheet2.Rows.Get(2).Height = 108F;
            this.neuSpread1_Sheet2.Rows.Get(3).Height = 108F;
            this.neuSpread1_Sheet2.Rows.Get(4).Height = 108F;
            this.neuSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
        }

        /// <summary>
        /// 分页显示
        /// </summary>
        /// <param name="al"></param>
        /// <param name="strType"></param>
        private void ShowPages(ArrayList al, string strType)
        {
            if (al == null || al.Count == 0)
            {
                if (strType == "Doct")
                {
                    htDoct.Clear();
                }
                return;
            }

            int myMod = 0;
            ArrayList alTemp = null;
            if (strType == "Dept")
            {
                iPageDeptCount = Math.DivRem(al.Count, 15, out myMod);
                if (myMod > 0)
                {
                    iPageDeptCount = iPageDeptCount + 1;
                }
                htDept.Clear();
                if (iPageDeptCount == 1)
                {
                    alTemp = new ArrayList();
                    alTemp.AddRange(al.GetRange(0, al.Count));
                    htDept.Add(1, alTemp);
                }
                else
                {
                    for (int i = 1; i <= iPageDeptCount; i++)
                    {
                        alTemp = new ArrayList();
                        if (al.Count > 15)
                        {
                            alTemp.AddRange(al.GetRange(0, 15));
                            al.RemoveRange(0, 15);
                        }
                        else
                        {
                            alTemp.AddRange(al.GetRange(0, al.Count));
                        }
                        htDept.Add(i, alTemp);
                    }
                }
            }
            else if (strType == "Doct")
            {
                iPageDoctCount = Math.DivRem(al.Count, 15, out myMod);
                if (myMod > 0)
                {
                    iPageDoctCount = iPageDoctCount + 1;
                }
                htDoct.Clear();
                if (iPageDoctCount == 1)
                {
                    alTemp = new ArrayList();
                    alTemp.AddRange(al.GetRange(0, al.Count));
                    htDoct.Add(1, alTemp);
                }
                else
                {
                    for (int i = 1; i <= iPageDoctCount; i++)
                    {
                        alTemp = new ArrayList();
                        if (al.Count > 15)
                        {
                            alTemp.AddRange(al.GetRange(0, 15));
                            al.RemoveRange(0, 15);
                        }
                        else
                        {
                            alTemp.AddRange(al.GetRange(0, al.Count));
                        }
                        htDoct.Add(i, alTemp);
                    }
                }
            }
        }

        /// <summary>
        /// 设置挂号信息
        /// </summary>
        /// <returns></returns>
        private int ShowDeptInfo()
        {
            ArrayList alDept = this.managerIntegrate.QueryRegDepartment();
            if (alDept == null)
            {
                MessageBox.Show("查询挂号科室出错" + this.managerIntegrate.Err);
            }
            this.ShowPages(alDept, "Dept");
            this.SetFarpointValue(htDept[1] as ArrayList, "Dept");
            return 1;
        }

        /// <summary>
        /// 加载医生列表
        /// </summary>
        /// <returns></returns>
        private int ShowDoctInfo()
        {
            if (!string.IsNullOrEmpty(this.txtDept.Text) && this.txtDept.Tag != null)
            {
                this.ResetSheet2();
                DataSet ds = new DataSet();
                DateTime now = this.schema.GetDateTimeFromSysDateTime();
                DateTime seeDate = now.Date;
                FS.HISFC.Models.Base.Noon noon = this.getNoon(this.regMgr.GetDateTimeFromSysDateTime());
                ds = this.schema.QueryDoct(seeDate, now,
                    (this.txtDept.Tag as FS.FrameWork.Models.NeuObject).ID, seeDate.AddDays(1), noon.ID);

                if (ds == null)
                {
                    this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
                    this.btOk.Focus();
                    MessageBox.Show("未检索到该科室下的出诊医生，请直接点击【挂号】", "提示");
                    return -1;
                }

                dsItems.Tables["Doct"].Rows.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    dsItems.Tables["Doct"].Rows.Add(new object[]
                    {
                        row[0],//医生代码
                        row[1],//医生名称
                        row[12],//拼音吗
                        row[13],//五笔码						
                        row[5],//挂号限额
                        row[6],//已挂号数
                        row[7],//预约限额
                        row[8],//已预约数
                        row[9],//特诊限额
                        row[10],//特诊已挂
                        row[3],//开始时间
                        row[4],//结束时间
                        row[2],//午别
                        FS.FrameWork.Function.NConvert.ToBoolean(row[11]),
                        row[14],
                        FS.FrameWork.Function.NConvert.ToBoolean(row[15]),//是否教授
                        //row[16]
                        //ygch row[16]放医生的门诊编码 row[17]放专家的候诊人数{8BF9E56B-828E-4264-9D2F-B0B74FB920B5} 
                        row[16],
                        row[17]
                    });
                }

                DataRow rows;
                ArrayList alDoct = new ArrayList();
                for (int i = 0; i < dsItems.Tables["Doct"].Rows.Count; i++)
                {
                    rows = dsItems.Tables["Doct"].Rows[i];
                    //重复的不添加
                    if (i > 0 && rows["ID"].ToString() == dsItems.Tables["Doct"].Rows[i - 1]["ID"].ToString()) continue;
                    FS.FrameWork.Models.NeuObject p = new FS.FrameWork.Models.NeuObject();
                    p.ID = rows["ID"].ToString();
                    p.Name = rows["Name"].ToString();
                    p.Memo = rows["num"].ToString();
                    alDoct.Add(p);
                }
                this.ShowPages(alDoct, "Doct");
                this.SetFarpointValue(htDoct[1] as ArrayList, "Doct");
            }
            return 1;
        }

        #region 更新患者基本信息
        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="patMgr"></param>
        /// <param name="registerMgr"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdatePatientinfo(FS.HISFC.Models.Registration.Register regInfo,
            FS.HISFC.BizProcess.Integrate.RADT patMgr, FS.HISFC.BizLogic.Registration.Register registerMgr,
            ref string Err)
        {
            int rtn = registerMgr.Update(FS.HISFC.BizLogic.Registration.EnumUpdateStatus.PatientInfo,
                                            regInfo);

            if (rtn == -1)
            {
                Err = registerMgr.Err;
                return -1;
            }

            if (rtn == 0)//没有更新到患者信息，插入
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

                p.PID.CardNO = regInfo.PID.CardNO;
                p.Name = regInfo.Name;
                p.Sex.ID = regInfo.Sex.ID;
                p.Birthday = regInfo.Birthday;
                p.Pact = regInfo.Pact;
                p.Pact.PayKind.ID = regInfo.Pact.PayKind.ID;
                p.SSN = regInfo.SSN;
                p.PhoneHome = regInfo.PhoneHome;
                p.AddressHome = regInfo.AddressHome;
                p.IDCard = regInfo.IDCard;
                p.IDCardType = regInfo.CardType;
                p.NormalName = regInfo.NormalName;
                p.IsEncrypt = regInfo.IsEncrypt;

                if (patientMgr.RegisterComPatient(p) == -1)
                {
                    Err = patientMgr.Err;
                    return -1;
                }

            }
            if (feeMgr.UpdatePatientIden(regInfo) < 0)
            {
                Err = "保存患者证件信息失败！" + feeMgr.Err;
                return -1;
            }

            return 0;
        }
        #endregion

        /// <summary>
        /// 打印挂号发票
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj)
        {
            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            if (regprint == null)
            {
                MessageBox.Show("挂号票打印接口维护出错！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            regprint.SetPrintValue(regObj);
            regprint.Print();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 选择科室弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btChooseDept_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择挂号科室方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frm_ChooseItem(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = sender as FS.FrameWork.Models.NeuObject;
            if (obj == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择挂号科室"));
                return;
            }
            this.txtDept.Text = obj.Name;
            this.txtDept.Tag = obj;
        }

        /// <summary>
        /// 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNO = this.txtCardNO.Text.Trim();

                this.ucSelfHelpPatientInfo1.Clear();
                if (string.IsNullOrEmpty(cardNO))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请您输入就诊卡号"));
                    return;
                }

                cardNO = cardNO.PadLeft(10, '0');
                this.patientInfo = this.GetPatientInfo(cardNO);

                this.txtDept.Focus();

            }
        }

        void txtCardNO_Enter(object sender, System.EventArgs e)
        {
            this.MouseMove(this.pbReadCard);
            this.MouseLeave(this.ptReg);
            this.MouseLeave(this.ptDept);
        }

        void txtCardNO_Leave(object sender, System.EventArgs e)
        {
            this.MouseLeave(this.pbReadCard);
        }

        void txtDept_Leave(object sender, System.EventArgs e)
        {
            this.MouseLeave(this.ptDept);
        }

        void txtDept_Enter(object sender, System.EventArgs e)
        {
            this.MouseMove(this.ptDept);
            this.MouseLeave(this.ptReg);
            this.MouseLeave(this.pbReadCard);
        }

        private void txtDept_Click(object sender, EventArgs e)
        {
            this.SetFarpointValue(htDept[iPageDept] as ArrayList, "Dept");
            this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
        }

        private void txtDoct_Leave(object sender, EventArgs e)
        {
            this.MouseLeave(this.ptDoct);
        }

        private void txtDoct_Click(object sender, EventArgs e)
        {
            this.ShowDoctInfo();
            this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;
        }

        /// <summary>
        /// 挂号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDept.Text) || this.txtDept.Tag == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请您选择挂号科室！"));
                this.lblTip.Text = FS.FrameWork.Management.Language.Msg("请您选择挂号科室");
                this.lblTipExtend.Text = "";
                return;
            }

            FS.HISFC.Models.Registration.Register register = this.GetRegisterInfo();
            if (register == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请您重新刷卡！"));
                this.Clear();
                this.txtCardNO.Focus();
                return;
            }

            DialogResult dr = MessageBox.Show("您选择的挂号科室为：" + register.DoctorInfo.Templet.Dept.Name +
                "\n您选择的看诊医生为：" + register.DoctorInfo.Templet.Doct.Name +
                "\n是否继续？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.No)
            {
                return;
            }
            if (dr == DialogResult.Cancel)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("您已经取消了本次挂号操作，谢谢使用！"));
                this.Clear();
                return;
            }

            register = this.GetRegisterInfo();
            if (register == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请您重新刷卡！"));
                this.Clear();
                this.txtCardNO.Focus();
                return;
            }

            if (register != null)
            {
                int returnValue = 0;
                this.MedcareInterfaceProxy.SetPactCode(register.Pact.ID);
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                try
                {
                    int orderNO = 0;
                    string Err = string.Empty;
                    string deptID = string.Empty;

                    //2看诊序号
                    if (this.strGetSeeNoType == "2")
                    {
                        deptID = register.DoctorInfo.Templet.Dept.ID;
                    }
                    else if (this.strGetSeeNoType == "3")
                    {
                        ArrayList alNurse = this.managerIntegrate.QueryNurseStationByDept(register.DoctorInfo.Templet.Dept, "14");
                        if (alNurse == null || alNurse.Count <= 0)
                        {
                            this.strGetSeeNoType = "2";
                            deptID = register.DoctorInfo.Templet.Dept.ID;
                        }
                        else
                        {
                            if (register.DoctorInfo.Templet.Doct.ID == "" || register.DoctorInfo.Templet.Doct.ID == null)
                            {
                                this.strGetSeeNoType = "2";
                                deptID = register.DoctorInfo.Templet.Dept.ID;
                            }
                            else
                            {
                                FS.FrameWork.Models.NeuObject nurseObj = alNurse[0] as FS.FrameWork.Models.NeuObject;
                                deptID = nurseObj.ID;
                            }
                        }
                    }

                    returnValue = this.UpdateSeeID(deptID, register.DoctorInfo.Templet.Doct.ID, register.DoctorInfo.Templet.Noon.ID, register.DoctorInfo.SeeDate, ref orderNO, ref Err);

                    if (returnValue < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.strGetSeeNoType = "3";
                        MessageBox.Show("自助挂号失败，请重新刷卡！" + Err, "提示");
                        this.Clear();
                        return;
                    }

                    this.strGetSeeNoType = "3";
                    register.DoctorInfo.SeeNO = orderNO;

                    //更新全院序号
                    if (this.Update(register.InputOper.OperTime, ref orderNO, ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("自助挂号失败，请重新刷卡！" + Err, "提示");
                        this.Clear();
                        return;
                    }
                    register.OrderNO = orderNO;//全院序号

                    #region 待遇接口实现
                    this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    this.MedcareInterfaceProxy.Connect();
                    this.MedcareInterfaceProxy.BeginTranscation();

                    returnValue = this.MedcareInterfaceProxy.UploadRegInfoOutpatient(register);
                    if (returnValue == -1)
                    {
                        this.MedcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("上传挂号信息失败！请重新刷卡！") + this.MedcareInterfaceProxy.ErrMsg);
                        this.Clear();
                        return;
                    }
                    register.OwnCost = 0;// register.SIMainInfo.OwnCost;  //自费金额
                    register.PubCost = 0;// register.SIMainInfo.PubCost;  //统筹金额
                    register.PayCost = 0;// register.SIMainInfo.PayCost;  //帐户金额
                    #endregion

                    // 插入挂号主表
                    returnValue = this.regMgr.Insert(register);
                    if (returnValue < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.MedcareInterfaceProxy.CancelRegInfoOutpatient(register);
                        MessageBox.Show("挂号失败！请重新刷卡！" + this.regMgr.Err);
                        this.Clear();
                        return;
                    }

                    //更新患者基本信息
                    if (this.UpdatePatientinfo(register, this.patientMgr, this.regMgr, ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.MedcareInterfaceProxy.CancelRegInfoOutpatient(register);
                        MessageBox.Show("自助挂号失败，请重新刷卡！" + Err, "提示");
                        this.Clear();
                        return;
                    }
                    #region 自动分诊
                    if (this.txtDoct.Tag != null && this.txtDoct.Text != "")
                    {
                        if (this.managerIntegrate.TriageForRegistration(register, "0", 0, false) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.MedcareInterfaceProxy.CancelRegInfoOutpatient(register);
                            MessageBox.Show("自动分诊失败！\r造成该错误的原因可能是未维护该医生的分诊队列。");
                            this.Clear();
                            return;
                        }
                    }
                    #endregion
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.MedcareInterfaceProxy.Commit();
                    this.MedcareInterfaceProxy.Disconnect();
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("自助挂号失败，请重新刷卡！", "提示");
                    this.Clear();
                    return;
                }
                this.iOperTime = 30;
                this.lblOperTime.Text = "00:" + (iOperTime).ToString();
                this.timer2.Enabled = false;
                this.Print(register);
                //MessageBox.Show("自助挂号成功！谢谢使用！");
                //\n本次挂号金额:" + (register.OwnCost + register.PubCost + register.PayCost).ToString() + "\n谢谢使用！");
                this.Clear();
            }
            else
            {
                MessageBox.Show("没有患者信息，请重新刷卡");
                this.Clear();
                this.lblTip.Text = FS.FrameWork.Management.Language.Msg("欢迎使用自助挂号系统，请您");
                this.lblTipExtend.Text = "刷卡！";
            }
        }

        /// <summary>
        /// 清屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClear_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void btReadCard_Click(object sender, EventArgs e)
        {

        }

        int width, height;
        protected override void OnLoad(EventArgs e)
        {
            this.InitInfo();
            //width = this.FindForm().Width;
            //height = this.FindForm().Height;
            this.FindForm().WindowState = FormWindowState.Maximized;

            //try
            //{
            //    if (this.FindForm().GetType() == typeof(FS.FrameWork.WinForms.Forms.frmBaseForm))
            //    {
            //        (this.FindForm() as FS.FrameWork.WinForms.Forms.frmBaseForm).toolBar1.Visible = false;
            //        (this.FindForm() as FS.FrameWork.WinForms.Forms.frmBaseForm). = false;
            //    }
            //    (this.FindForm() as FS.FrameWork.WinForms.Forms.frmBaseForm).toolBar1.Visible = false;
            //}
            //catch { 


            this.BackColor = Color.FromArgb(244, 244, 252);

            this.MouseLeave(this.btReadCard);
            this.MouseLeave(this.btClear);
            this.MouseLeave(this.btOk);
            this.MouseLeave(this.btQuit);
            this.MouseLeave(this.btPrePage);
            this.MouseLeave(this.btNextPage);

            base.OnLoad(e);
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1)
            {
                FS.FrameWork.Models.NeuObject obj = this.neuSpread1_Sheet1.ActiveCell.Tag as FS.FrameWork.Models.NeuObject;
                if (obj == null)
                {
                    MessageBox.Show("选择的科室有误！");
                    return;
                }
                this.txtDept.Text = obj.Name;
                this.txtDept.Tag = obj;
                //this.lblTip.Text = "请您点击[挂号]!";
                this.lblTip.Text = "请您选择医生！";
                this.lblTipExtend.Text = "";

                this.txtDoct.Focus();
                this.txtDoct.Tag = null;
                this.txtDoct.Text = "";
                this.ShowDoctInfo();
                this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;
                this.MouseMove(this.ptDoct);
                this.MouseLeave(this.ptDept);
                this.MouseLeave(this.pbReadCard);
            }
            else if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet2)
            {
                FS.FrameWork.Models.NeuObject obj = this.neuSpread1_Sheet2.ActiveCell.Tag as FS.FrameWork.Models.NeuObject;
                if (obj != null)
                {
                    this.txtDoct.Text = obj.Name;
                    this.txtDoct.Tag = obj;
                    this.lblTip.Text = "请您点击[挂号]!";
                    this.lblTipExtend.Text = "";

                    this.btOk.Focus();
                    this.MouseMove(this.ptReg);
                    this.MouseLeave(this.ptDoct);
                }
            }
            //this.FindForm().WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btQuit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btClear_MouseMove(object sender, MouseEventArgs e)
        {
            //this.tbClear.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.清屏_2;
            this.MouseMove(sender);
        }

        private void btClear_MouseLeave(object sender, EventArgs e)
        {
            //this.tbClear.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.清屏_1;
            this.MouseLeave(sender);
        }

        private void btPrePage_MouseMove(object sender, MouseEventArgs e)
        {
            this.MouseMove(sender);
        }

        private void btPrePage_MouseLeave(object sender, EventArgs e)
        {
            this.MouseLeave(sender);
        }

        private void btNextPage_MouseMove(object sender, MouseEventArgs e)
        {
            this.MouseMove(sender);
        }

        private void btNextPage_MouseLeave(object sender, EventArgs e)
        {
            this.MouseLeave(sender);
        }

        private void MouseMove(object sender)
        {
            Control control = (Control)sender;

            if (control.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuButton")
            {
                if (control.Name == "btClear")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.清屏_4;
                }
                if (control.Name == "btQuit")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.退出_2;
                }
                if (control.Name == "btOk")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.挂号_4;
                }
                if (control.Name == "btReadCard")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.刷卡_2;
                }
                if (control.Name == "btPrePage")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.上一页_1;
                }
                if (control.Name == "btNextPage")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.下一页_1;
                }
            }
            if (control.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuPictureBox")
            {
                PictureBox pt = (PictureBox)control;

                if (pt.Name == "ptDept")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.点击右侧_2;
                }
                if (pt.Name == "ptDoct")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.点击右侧_4;
                }
                if (pt.Name == "ptReg")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.点击挂号_2;
                }
                if (pt.Name == "pbReadCard")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.刷卡_2;
                }

            }
        }

        private void MouseLeave(object sender)
        {
            Control control = (Control)sender;

            if (control.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuButton")
            {
                if (control.Name == "btClear")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.清屏_3;
                }
                if (control.Name == "btQuit")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.退出_1;
                }
                if (control.Name == "btOk")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.挂号_3;
                }
                if (control.Name == "btReadCard")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.刷卡_1;
                }
                if (control.Name == "btPrePage")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.上一页;
                }
                if (control.Name == "btNextPage")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.下一页;
                }
            }
            if (control.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuPictureBox")
            {
                PictureBox pt = (PictureBox)control;

                if (pt.Name == "ptDept")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.点击右侧_1;
                }
                if (pt.Name == "ptDoct")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.点击右侧_3;
                }
                if (pt.Name == "ptReg")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.点击挂号_1;
                }
                if (pt.Name == "pbReadCard")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.刷卡_1;
                }
            }
        }

        /// <summary>
        /// 定时触发，等待读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!this.isReadPatientInfo)
            {
                return;
            }
            Int32 result;
            string imagePath = Application.StartupPath + "\\image.bmp";

            try
            {
                //打开设备
                result = Functions.OpenCardReader(0, 2, 115200);
                if (result != 0)
                {
                    this.isReadPatientInfo = false;
                    MessageBox.Show("初始化设备失败！请重新刷卡！");
                    this.Clear();
                    return;
                }
                //读卡
                result = Functions.GetPersonMsgW(ref person, imagePath);
                if (result == 0)
                {
                    this.isReadPatientInfo = false;
                    this.timer2.Enabled = false;
                    iOperTime = 30;
                    this.lblOperTime.Text = "00:" + (iOperTime).ToString();
                    this.patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    if (frmPatient == null)
                    {
                        frmPatient = new frmQueryPatientByName();
                    }
                    frmPatient.SelectedPatient -= new frmQueryPatientByName.GetPatient(frm_SelectedPatient);
                    frmPatient.SelectedPatient += new frmQueryPatientByName.GetPatient(frm_SelectedPatient);
                    frmPatient.IdenNO = person.cardId;
                    this.timer2.Enabled = true;
                    //this.patientInfo = this.radtIntegrate.QueryComPatientInfoByIDNO(person.cardId);
                    if (this.patientInfo == null || this.patientInfo.PID.CardNO == null)
                    {
                        long autoGetCardNO;
                        autoGetCardNO = regMgr.AutoGetCardNOForSelfHelpReg();
                        if (autoGetCardNO == -1)
                        {
                            this.isReadPatientInfo = false;
                            MessageBox.Show("未能成功自动产生卡号，请重新刷卡！", "提示");
                            this.Clear();
                            return;
                        }
                        else
                        {
                            this.patientInfo.PID.CardNO = autoGetCardNO.ToString();
                        }
                    }
                    this.txtCardNO.Text = this.patientInfo.PID.CardNO;
                    this.patientInfo.Name = person.name;
                    if (person.sex == "男")
                    {
                        this.patientInfo.Sex.ID = "M";
                        this.patientInfo.Sex.Name = person.sex;
                    }
                    else
                    {
                        this.patientInfo.Sex.ID = "F";
                        this.patientInfo.Sex.Name = person.sex;
                    }
                    this.patientInfo.IDCardType.ID = "01";
                    this.patientInfo.IDCardType.Name = "身份证";
                    this.patientInfo.Birthday = new DateTime(Convert.ToInt32(person.birthday.Substring(0, 4)),
                        Convert.ToInt32(person.birthday.Substring(4, 2)), Convert.ToInt32(person.birthday.Substring(6, 2)));
                    this.patientInfo.AddressHome = person.address;
                    this.patientInfo.IDCard = person.cardId;
                    #region 判断社保身份
                    this.MedcareInterfaceProxy.SetPactCode("2");
                    this.MedcareInterfaceProxy.Connect();
                    FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
                    r.Name = person.name;
                    r.IDCard = person.cardId;
                    if (this.MedcareInterfaceProxy.QueryCanMedicare(r) == -1)
                    {
                        this.patientInfo.Pact.ID = "1";
                        this.patientInfo.Pact.Name = this.consHelper.GetObjectFromID(this.patientInfo.Pact.ID).Name;
                        this.patientInfo.Pact.PayKind.ID = "01";
                    }
                    else
                    {
                        if (r.Pact.ID != this.patientInfo.Pact.ID)
                        {
                            this.patientInfo.Pact.ID = r.Pact.ID;
                            this.patientInfo.Pact.Name = this.consHelper.GetObjectFromID(this.patientInfo.Pact.ID).Name;
                            this.patientInfo.Pact.PayKind.ID = "02";
                        }
                    }
                    this.MedcareInterfaceProxy.Disconnect();
                    #endregion
                    this.ucSelfHelpPatientInfo1.PatientInfo = this.patientInfo;
                    this.isReadPatientInfo = true;
                }
                else
                {
                    result = Functions.CloseCardReader();
                    return;
                }
                result = Functions.CloseCardReader();
                if (result != 0)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                this.isReadPatientInfo = false;
                MessageBox.Show("刷卡失败！请重新刷卡！" + ex.Message);
                this.Clear();
                return;
            }
            this.isReadPatientInfo = false;
        }

        /// <summary>
        /// 操作时间倒计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (iOperTime == 0)
            {
                iOperTime = 30;
                this.lblOperTime.Text = "00:" + (iOperTime).ToString();
                this.timer2.Enabled = false;
                this.Clear();
                return;
            }
            if (iOperTime-- <= 10)
            {
                this.lblOperTime.Text = "00:0" + (iOperTime).ToString();
            }
            else
            {
                this.lblOperTime.Text = "00:" + (iOperTime).ToString();
            }
        }

        /// <summary>
        /// 选择患者
        /// </summary>
        /// <param name="patientInfo"></param>
        void frm_SelectedPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.patientInfo.PID.CardNO = patientInfo.PID.CardNO;
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btNextPage_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1)
            {
                iPageDept++;
                if (iPageDept > iPageDeptCount)
                {
                    iPageDept--;
                    return;
                }
                this.ResetSheet1();
                this.SetFarpointValue(htDept[iPageDept] as ArrayList, "Dept");
            }
            else if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet2)
            {
                iPageDoct++;
                if (iPageDoct > iPageDoctCount)
                {
                    iPageDoct--;
                    return;
                }
                this.ResetSheet2();
                this.SetFarpointValue(htDoct[iPageDoct] as ArrayList, "Doct");
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPrePage_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1)
            {
                iPageDept--;
                if (iPageDept < 1)
                {
                    iPageDept++;
                    return;
                }
                this.ResetSheet1();
                this.SetFarpointValue(htDept[iPageDept] as ArrayList, "Dept");
            }
            else if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet2)
            {
                iPageDoct--;
                if (iPageDoct < 1)
                {
                    iPageDoct++;
                    return;
                }
                this.ResetSheet2();
                this.SetFarpointValue(htDoct[iPageDoct] as ArrayList, "Doct");
            }
        }

        #endregion
    }
}
