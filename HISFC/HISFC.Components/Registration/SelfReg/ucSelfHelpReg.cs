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
    /// [��������: �����Һ�]<br></br>
    /// [�� �� ��: ţ��Ԫ]<br></br>
    /// [����ʱ��: 2009-9]<br></br>
    /// <˵��
    ///		��۱��ػ�
    ///  />
    /// </summary>
    public partial class ucSelfHelpReg : Form
    {
        public ucSelfHelpReg()
        {
            InitializeComponent();

        }

        #region ��
        /// <summary>
        ///  �ۺϹ���ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���תҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// �ҺŹ���ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        private FS.HISFC.BizLogic.Registration.Schema schema = new FS.HISFC.BizLogic.Registration.Schema();

        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();

        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ҽ���ӿڴ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// ���߹�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT patientMgr = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ��ͬ��λ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper consHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��ȡ�������Ϣ
        /// </summary>
        Functions.PERSONINFOW person = new Functions.PERSONINFOW();

        /// <summary>
        /// �Ƿ��ȡ��Ϣ
        /// </summary>
        bool isReadPatientInfo = true;
        //[DllImport("user32.dll")]
        //public static extern bool ReleaseCapture();
        //[DllImport("user32.dll")]
        //public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// ����ҽ������
        /// </summary>
        DataSet dsItems;

        /// <summary>
        /// �����źŷ�ʽ 1ҽ�� 2���� 3����
        /// </summary>
        string strGetSeeNoType = string.Empty;

        /// <summary>
        /// ��ҳ��ʾ����
        /// </summary>
        Hashtable htDept = new Hashtable();

        /// <summary>
        /// ��ҳ��ʾҽ��
        /// </summary>
        Hashtable htDoct = new Hashtable();

        /// <summary>
        /// ����ҳ��
        /// </summary>
        int iPageDept = 1;

        /// <summary>
        /// ҽ��ҳ��
        /// </summary>
        int iPageDoct = 1;

        /// <summary>
        /// ����ҳ��
        /// </summary>
        int iPageDeptCount = 1;

        /// <summary>
        /// ҽ��ҳ��
        /// </summary>
        int iPageDoctCount = 1;

        /// <summary>
        /// �����ʱ��
        /// </summary>
        int iOperTime = 30;

        /// <summary>
        /// ��ѯ�Ļ�����Ϣ����һ��ѡ����UC
        /// </summary>
        frmQueryPatientByName frmPatient = null;
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ�ѡ�õ�������
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ѡ�õ�������"), DefaultValue(false)]
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

        #region ����
        /// <summary>
        /// ��ʼ����Ϣ
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
            this.lblTip.Text = "��ӭʹ�������Һ�ϵͳ������";
            this.lblTipExtend.Text = "ˢ����";
            this.InitData();
            this.ShowDeptInfo();
            this.consHelper.ArrayObject = feeMgr.QueryPactUnitOutPatient();
            this.strGetSeeNoType = this.controlParamManager.GetControlParam<string>("ZZGH01", true, "1");
            return 1;
        }

        /// <summary>
        /// ����ҽ������
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
                    //����һ�б���������� ygch {8BF9E56B-828E-4264-9D2F-B0B74FB920B5}
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
        /// ���ݾ��￨�Ų�ѯ���߻�����Ϣ
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string cardNO)
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtIntegrate.QueryComPatientInfo(cardNO);
            if (patientInfo == null)
            {
                MessageBox.Show("��ѯ���߻�����Ϣ����");
                return null;
            }

            if (string.IsNullOrEmpty(patientInfo.PID.CardNO))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û���ҵ��û�����Ϣ"));
                return null;
            }

            //���渳ֵ
            this.ucSelfHelpPatientInfo1.PatientInfo = patientInfo;



            this.txtDept.Focus();
            this.lblTip.Text = FS.FrameWork.Management.Language.Msg("����ѡ��Һſ���");
            this.lblTipExtend.Text = "";
            return patientInfo;
        }

        /// <summary>
        /// ��ȡ�Һ���Ϣ
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

                //�Һ���ˮ��
                register.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
                register.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//������

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
                register.DoctorInfo.Templet.RegLevel.Name = "��ͨ�Һ�";

                FS.HISFC.Models.Base.Noon noon = this.getNoon(register.DoctorInfo.SeeDate);
                register.DoctorInfo.Templet.Noon = noon;
                register.DoctorInfo.Templet.Begin = register.DoctorInfo.SeeDate.Date;
                register.DoctorInfo.Templet.End = register.DoctorInfo.SeeDate.Date;
                int returnValue = this.GetRegFee(register);
                //if (returnValue < 0)
                //{
                //    MessageBox.Show("��ùҺŷ�ʧ��");
                //    return null;
                //}
                register.RegLvlFee.RegFee = 0;
                register.RegLvlFee.ChkFee = 0;
                register.RegLvlFee.OwnDigFee = 0;
                register.RegLvlFee.OthFee = 0;
                register.OwnCost = 0;
                //������
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
                //��ѯ���߾����¼����
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
                        MessageBox.Show("��ѯ���߾����¼����");
                        return null;
                    }
                }

                if (register.DoctorInfo.Templet.Noon.ID == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("δά�������Ϣ,����ά��"), "��ʾ");
                    return null;
                }
                register.DoctorInfo.Templet.ID = "";
            }

            return register;
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Noon getNoon(DateTime current)
        {
            FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

            System.Collections.ArrayList alNoon = noonMgr.Query();
            if (alNoon == null)
            {
                MessageBox.Show("��ȡ�����Ϣʱ����!" + noonMgr.Err, "��ʾ");
                return null;
            }
            if (alNoon == null) return null;
            /*
             * ��������Ϊ���Ӧ���ǰ���һ��ȫ��ʱ�����磺06~12,����:12~18����Ϊ����,
             * ʵ�����Ϊҽ������ʱ���,�������Ϊ08~11:30������Ϊ14~17:30
             * ��������Һ�Ա����������ʱ��ιҺ�,���п�����ʾ���δά��
             * ���Ը�Ϊ���ݴ���ʱ�����ڵ�������磺9��30��06~12֮�䣬��ô���ж��Ƿ��������
             * 06~12֮�䣬ȫ������˵��9:30���Ǹ�������
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
            if (p == null)//����
            {
                return -1;
            }
            if (p.ID == null || p.ID == "")//û��ά���Һŷ�
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
        /// ����
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
            this.lblTip.Text = "��ӭʹ�������Һ�ϵͳ������";
            this.lblTipExtend.Text = "ˢ����";
            this.isReadPatientInfo = true;
            this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
            this.ResetSheet2();
        }

        /// <summary>
        /// ����ҽ������ҵĿ������
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

            #region �����źŷ�ʽ

            if (this.strGetSeeNoType == "1" && doctID != null && doctID != "")
            {
                Type = "1";//ҽ��
                Subject = doctID;
            }
            else if (this.strGetSeeNoType == "2")
            {
                Type = "2";//����
                Subject = deptID;
            }
            else if (this.strGetSeeNoType == "3")
            {
                Type = "3";//����
                Subject = deptID;
            }

            #endregion

            //���¿������
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //��ȡ�������		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ����ȫԺ�������
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(DateTime current, ref int seeNo,
            ref string Err)
        {
            //���¿������
            //ȫԺ��ȫ����������������Ч��Ĭ�� 1
            if (this.regMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = regMgr.Err;
                return -1;
            }

            //��ȡȫԺ�������
            if (this.regMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = regMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ����farpoint
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

            // decimal rowCount = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(alColections.Count / 3)); //�������
            int myMod = 0;
            int rowCount = Math.DivRem(alColections.Count, 3, out myMod);

            if (myMod > 0)
            {
                rowCount = rowCount + 1;
            }

            int j = 0;
            for (int i = 0; i < alColections.Count; i++)
            {
                int k = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(i / 3))); //�������

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
                    btCell.Picture = global::FS.HISFC.Components.Registration.Properties.Resources.����;
                    this.neuSpread1_Sheet1.Cells[k, mod].Tag = obj;
                }
                else if (strType == "Doct")
                {
                    this.neuSpread1_Sheet2.RowCount = FS.FrameWork.Function.NConvert.ToInt32(rowCount);
                    this.neuSpread1_Sheet2.ColumnCount = 3;

                    btCell.Text = obj.Name + "\n(" + obj.Memo + ")";
                    this.neuSpread1_Sheet2.Cells[k, mod].CellType = btCell;
                    btCell.Picture = global::FS.HISFC.Components.Registration.Properties.Resources.ҽ��;
                    this.neuSpread1_Sheet2.Cells[k, mod].Tag = obj;
                }
            }
        }

        /// <summary>
        /// ����farpoint����sheetҳ
        /// </summary>
        private void ResetSheet1()
        {
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.SheetName = "ҽ��";
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
        /// ����farpointҽ��sheetҳ
        /// </summary>
        private void ResetSheet2()
        {
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.SheetName = "ҽ��";
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
        /// ��ҳ��ʾ
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
        /// ���ùҺ���Ϣ
        /// </summary>
        /// <returns></returns>
        private int ShowDeptInfo()
        {
            ArrayList alDept = this.managerIntegrate.QueryRegDepartment();
            if (alDept == null)
            {
                MessageBox.Show("��ѯ�Һſ��ҳ���" + this.managerIntegrate.Err);
            }
            this.ShowPages(alDept, "Dept");
            this.SetFarpointValue(htDept[1] as ArrayList, "Dept");
            return 1;
        }

        /// <summary>
        /// ����ҽ���б�
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
                    MessageBox.Show("δ�������ÿ����µĳ���ҽ������ֱ�ӵ�����Һš�", "��ʾ");
                    return -1;
                }

                dsItems.Tables["Doct"].Rows.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    dsItems.Tables["Doct"].Rows.Add(new object[]
                    {
                        row[0],//ҽ������
                        row[1],//ҽ������
                        row[12],//ƴ����
                        row[13],//�����						
                        row[5],//�Һ��޶�
                        row[6],//�ѹҺ���
                        row[7],//ԤԼ�޶�
                        row[8],//��ԤԼ��
                        row[9],//�����޶�
                        row[10],//�����ѹ�
                        row[3],//��ʼʱ��
                        row[4],//����ʱ��
                        row[2],//���
                        FS.FrameWork.Function.NConvert.ToBoolean(row[11]),
                        row[14],
                        FS.FrameWork.Function.NConvert.ToBoolean(row[15]),//�Ƿ����
                        //row[16]
                        //ygch row[16]��ҽ����������� row[17]��ר�ҵĺ�������{8BF9E56B-828E-4264-9D2F-B0B74FB920B5} 
                        row[16],
                        row[17]
                    });
                }

                DataRow rows;
                ArrayList alDoct = new ArrayList();
                for (int i = 0; i < dsItems.Tables["Doct"].Rows.Count; i++)
                {
                    rows = dsItems.Tables["Doct"].Rows[i];
                    //�ظ��Ĳ����
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

        #region ���»��߻�����Ϣ
        /// <summary>
        /// ���»��߻�����Ϣ
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

            if (rtn == 0)//û�и��µ�������Ϣ������
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
                Err = "���滼��֤����Ϣʧ�ܣ�" + feeMgr.Err;
                return -1;
            }

            return 0;
        }
        #endregion

        /// <summary>
        /// ��ӡ�Һŷ�Ʊ
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj)
        {
            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            if (regprint == null)
            {
                MessageBox.Show("�Һ�Ʊ��ӡ�ӿ�ά������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            regprint.SetPrintValue(regObj);
            regprint.Print();
        }
        #endregion

        #region �¼�
        /// <summary>
        /// ѡ����ҵ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btChooseDept_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ѡ��Һſ��ҷ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frm_ChooseItem(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = sender as FS.FrameWork.Models.NeuObject;
            if (obj == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ��Һſ���"));
                return;
            }
            this.txtDept.Text = obj.Name;
            this.txtDept.Tag = obj;
        }

        /// <summary>
        /// �س�
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
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����������￨��"));
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
        /// �Һ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDept.Text) || this.txtDept.Tag == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ѡ��Һſ��ң�"));
                this.lblTip.Text = FS.FrameWork.Management.Language.Msg("����ѡ��Һſ���");
                this.lblTipExtend.Text = "";
                return;
            }

            FS.HISFC.Models.Registration.Register register = this.GetRegisterInfo();
            if (register == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ˢ����"));
                this.Clear();
                this.txtCardNO.Focus();
                return;
            }

            DialogResult dr = MessageBox.Show("��ѡ��ĹҺſ���Ϊ��" + register.DoctorInfo.Templet.Dept.Name +
                "\n��ѡ��Ŀ���ҽ��Ϊ��" + register.DoctorInfo.Templet.Doct.Name +
                "\n�Ƿ������", "��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.No)
            {
                return;
            }
            if (dr == DialogResult.Cancel)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���Ѿ�ȡ���˱��ιҺŲ�����ллʹ�ã�"));
                this.Clear();
                return;
            }

            register = this.GetRegisterInfo();
            if (register == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ˢ����"));
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

                    //2�������
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
                        MessageBox.Show("�����Һ�ʧ�ܣ�������ˢ����" + Err, "��ʾ");
                        this.Clear();
                        return;
                    }

                    this.strGetSeeNoType = "3";
                    register.DoctorInfo.SeeNO = orderNO;

                    //����ȫԺ���
                    if (this.Update(register.InputOper.OperTime, ref orderNO, ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����Һ�ʧ�ܣ�������ˢ����" + Err, "��ʾ");
                        this.Clear();
                        return;
                    }
                    register.OrderNO = orderNO;//ȫԺ���

                    #region �����ӿ�ʵ��
                    this.MedcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    this.MedcareInterfaceProxy.Connect();
                    this.MedcareInterfaceProxy.BeginTranscation();

                    returnValue = this.MedcareInterfaceProxy.UploadRegInfoOutpatient(register);
                    if (returnValue == -1)
                    {
                        this.MedcareInterfaceProxy.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ϴ��Һ���Ϣʧ�ܣ�������ˢ����") + this.MedcareInterfaceProxy.ErrMsg);
                        this.Clear();
                        return;
                    }
                    register.OwnCost = 0;// register.SIMainInfo.OwnCost;  //�Էѽ��
                    register.PubCost = 0;// register.SIMainInfo.PubCost;  //ͳ����
                    register.PayCost = 0;// register.SIMainInfo.PayCost;  //�ʻ����
                    #endregion

                    // ����Һ�����
                    returnValue = this.regMgr.Insert(register);
                    if (returnValue < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.MedcareInterfaceProxy.CancelRegInfoOutpatient(register);
                        MessageBox.Show("�Һ�ʧ�ܣ�������ˢ����" + this.regMgr.Err);
                        this.Clear();
                        return;
                    }

                    //���»��߻�����Ϣ
                    if (this.UpdatePatientinfo(register, this.patientMgr, this.regMgr, ref Err) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.MedcareInterfaceProxy.CancelRegInfoOutpatient(register);
                        MessageBox.Show("�����Һ�ʧ�ܣ�������ˢ����" + Err, "��ʾ");
                        this.Clear();
                        return;
                    }
                    #region �Զ�����
                    if (this.txtDoct.Tag != null && this.txtDoct.Text != "")
                    {
                        if (this.managerIntegrate.TriageForRegistration(register, "0", 0, false) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.MedcareInterfaceProxy.CancelRegInfoOutpatient(register);
                            MessageBox.Show("�Զ�����ʧ�ܣ�\r��ɸô����ԭ�������δά����ҽ���ķ�����С�");
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
                    MessageBox.Show("�����Һ�ʧ�ܣ�������ˢ����", "��ʾ");
                    this.Clear();
                    return;
                }
                this.iOperTime = 30;
                this.lblOperTime.Text = "00:" + (iOperTime).ToString();
                this.timer2.Enabled = false;
                this.Print(register);
                //MessageBox.Show("�����Һųɹ���ллʹ�ã�");
                //\n���ιҺŽ��:" + (register.OwnCost + register.PubCost + register.PayCost).ToString() + "\nллʹ�ã�");
                this.Clear();
            }
            else
            {
                MessageBox.Show("û�л�����Ϣ��������ˢ��");
                this.Clear();
                this.lblTip.Text = FS.FrameWork.Management.Language.Msg("��ӭʹ�������Һ�ϵͳ������");
                this.lblTipExtend.Text = "ˢ����";
            }
        }

        /// <summary>
        /// ����
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
                    MessageBox.Show("ѡ��Ŀ�������");
                    return;
                }
                this.txtDept.Text = obj.Name;
                this.txtDept.Tag = obj;
                //this.lblTip.Text = "�������[�Һ�]!";
                this.lblTip.Text = "����ѡ��ҽ����";
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
                    this.lblTip.Text = "�������[�Һ�]!";
                    this.lblTipExtend.Text = "";

                    this.btOk.Focus();
                    this.MouseMove(this.ptReg);
                    this.MouseLeave(this.ptDoct);
                }
            }
            //this.FindForm().WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btQuit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btClear_MouseMove(object sender, MouseEventArgs e)
        {
            //this.tbClear.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.����_2;
            this.MouseMove(sender);
        }

        private void btClear_MouseLeave(object sender, EventArgs e)
        {
            //this.tbClear.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.����_1;
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
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.����_4;
                }
                if (control.Name == "btQuit")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.�˳�_2;
                }
                if (control.Name == "btOk")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.�Һ�_4;
                }
                if (control.Name == "btReadCard")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.ˢ��_2;
                }
                if (control.Name == "btPrePage")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.��һҳ_1;
                }
                if (control.Name == "btNextPage")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.��һҳ_1;
                }
            }
            if (control.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuPictureBox")
            {
                PictureBox pt = (PictureBox)control;

                if (pt.Name == "ptDept")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.����Ҳ�_2;
                }
                if (pt.Name == "ptDoct")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.����Ҳ�_4;
                }
                if (pt.Name == "ptReg")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.����Һ�_2;
                }
                if (pt.Name == "pbReadCard")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.ˢ��_2;
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
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.����_3;
                }
                if (control.Name == "btQuit")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.�˳�_1;
                }
                if (control.Name == "btOk")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.�Һ�_3;
                }
                if (control.Name == "btReadCard")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.ˢ��_1;
                }
                if (control.Name == "btPrePage")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.��һҳ;
                }
                if (control.Name == "btNextPage")
                {
                    control.BackgroundImage = global::FS.HISFC.Components.Registration.Properties.Resources.��һҳ;
                }
            }
            if (control.GetType().FullName == "FS.FrameWork.WinForms.Controls.NeuPictureBox")
            {
                PictureBox pt = (PictureBox)control;

                if (pt.Name == "ptDept")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.����Ҳ�_1;
                }
                if (pt.Name == "ptDoct")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.����Ҳ�_3;
                }
                if (pt.Name == "ptReg")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.����Һ�_1;
                }
                if (pt.Name == "pbReadCard")
                {
                    pt.Image = global::FS.HISFC.Components.Registration.Properties.Resources.ˢ��_1;
                }
            }
        }

        /// <summary>
        /// ��ʱ�������ȴ�����
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
                //���豸
                result = Functions.OpenCardReader(0, 2, 115200);
                if (result != 0)
                {
                    this.isReadPatientInfo = false;
                    MessageBox.Show("��ʼ���豸ʧ�ܣ�������ˢ����");
                    this.Clear();
                    return;
                }
                //����
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
                            MessageBox.Show("δ�ܳɹ��Զ��������ţ�������ˢ����", "��ʾ");
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
                    if (person.sex == "��")
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
                    this.patientInfo.IDCardType.Name = "���֤";
                    this.patientInfo.Birthday = new DateTime(Convert.ToInt32(person.birthday.Substring(0, 4)),
                        Convert.ToInt32(person.birthday.Substring(4, 2)), Convert.ToInt32(person.birthday.Substring(6, 2)));
                    this.patientInfo.AddressHome = person.address;
                    this.patientInfo.IDCard = person.cardId;
                    #region �ж��籣���
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
                MessageBox.Show("ˢ��ʧ�ܣ�������ˢ����" + ex.Message);
                this.Clear();
                return;
            }
            this.isReadPatientInfo = false;
        }

        /// <summary>
        /// ����ʱ�䵹��ʱ
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
        /// ѡ����
        /// </summary>
        /// <param name="patientInfo"></param>
        void frm_SelectedPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.patientInfo.PID.CardNO = patientInfo.PID.CardNO;
        }

        /// <summary>
        /// ��һҳ
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
        /// ��һҳ
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
