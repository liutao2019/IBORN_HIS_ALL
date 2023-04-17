using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// �˻����̹Һ�
    /// </summary>
    public partial class ucAccountRegister : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Registration.INurseArrayRegister
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucAccountRegister()
        {
            InitializeComponent();
        }

        #region ����
        private ArrayList al = new ArrayList();

        /// <summary>
        /// ��������б�
        /// </summary>
        private ArrayList alDept = new ArrayList();

        /// <summary>
        /// ҽ���б�
        /// </summary>
        private ArrayList alDoct = new ArrayList();

        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �Һż��������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLevel RegLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();

        /// <summary>
        /// �ҺŹ���ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// �Ű������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema SchemaMgr = new FS.HISFC.BizLogic.Registration.Schema();

        /// <summary>
        /// �Һŷѹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
        
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = null;
        private DataSet dsItems;

        FS.HISFC.Models.Registration.Register regObj = null;

        ArrayList alNoon = null;
        #endregion

        #region ��ʼ���Һż���initRegLevel()

        /// <summary>
        /// ��ʼ���Һż���
        /// </summary>
        /// <returns></returns>
        private int initRegLevel()
        {
            al = this.getRegLevelFromXML();
            if (al == null) return -1;
            ///�������û������,�����ݿ��ж�ȡ 
            if (al.Count == 0)
            {
                al = this.RegLevelMgr.Query(true);
            }

            if (al == null)
            {
                MessageBox.Show("��ѯ�Һż������!" + this.RegLevelMgr.Err, "��ʾ");
                return -1;
            }

            this.cmbRegLevel.AddItems(al);
            
            return 0;
        }
        #endregion

        #region ��XML��ȡ�Һż����Ȩ��getRegLevelFromXML()
        /// <summary>
        /// �ӱ��ض�ȡ�Һż���,Ȩ�޿���
        /// </summary>
        /// <returns></returns>
        private ArrayList getRegLevelFromXML()
        {
            ArrayList alLists = new ArrayList();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/RegLevelList.xml");
            }
            catch { return alLists; }
            try
            {
                XmlNodeList nodes = doc.SelectNodes(@"//Level");
                foreach (XmlNode node in nodes)
                {
                    FS.HISFC.Models.Registration.RegLevel level = new FS.HISFC.Models.Registration.RegLevel();
                    level.ID = node.Attributes["ID"].Value;//
                    level.Name = node.Attributes["Name"].Value;
                    level.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsExpert"].Value);
                    level.IsFaculty = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsFaculty"].Value);
                    level.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsSpecial"].Value);
                    level.IsDefault = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["IsDefault"].Value);
                    alLists.Add(level);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡ�Һż������!" + e.Message);
                return null;
            }

            return alLists;
        }
        #endregion

        private void initdept()
        {
            //��ȡ�����������
            this.alDept = this.GetClinicDepts();
            cmbDept.AddItems(alDept);
        }

        private ArrayList GetClinicDepts()
        {
            al = this.conMgr.QueryRegDepartment();
            if (al == null)
            {
                MessageBox.Show("��ȡ�������ʱ����!" + this.conMgr.Err, "��ʾ");
                return null;
            }

            return al;
        }

        private void InitDoct()
        {
            alDoct = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alDoct == null)
            {
                MessageBox.Show("��ȡ����ҽ���б�ʱ����!" + conMgr.Err, "��ʾ");
                alDoct = new ArrayList();
            }

            this.cmbDoctor.AddItems(alDoct);
        
        }

        private void initCombox()
        {
            initRegLevel();
            initdept();
            InitDoct();
        }

        private int valid()
        {
            if (this.cmbRegLevel.Tag == null || this.cmbRegLevel.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ��Һż���!", "��ʾ");
                this.cmbRegLevel.Focus();
                return -1;
            }
            //�õ��Һż������
            FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;

            if ((this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == ""))
            {
                MessageBox.Show("������Һſ���!", "��ʾ");
                this.cmbDept.Focus();
                return -1;
            }

            if (this.cmbDept.SelectedItem == null)
            {
                MessageBox.Show("��ѡ��Һſ���!", "��ʾ");
                this.cmbDept.Focus();
                return -1;
            }
            //�о��Һſ��Ҳ�Ӧ������д��Ӧ����ѡ��õ���
            if (this.cmbDept.Text != this.cmbDept.SelectedItem.Name && this.cmbDept.Text != this.cmbDept.Tag.ToString())
            {
                MessageBox.Show("��������ȷ�ĹҺſ���!", "��ʾ");
                this.cmbDept.Focus();
                return -1;
            }
            //ר�ҺŻ�������ű���ָ��ҽ��
            if ((level.IsExpert || level.IsSpecial) &&
                (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == ""))
            {
                //��ʾӦ����ר�ҺŻ�������ű���ָ������ҽ��
                MessageBox.Show("ר�Һű���ָ������ҽ��!", "��ʾ");
                this.cmbDoctor.Focus();
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ������Ӧ�Һ���Ϣ(ģ��,�ѹ�,ʣ�����Ϣ)
        /// </summary>
        private void QueryRegLevl()
        {
            //�ָ���ʼ״̬
            this.cmbDept.Tag = "";
            this.cmbDoctor.Tag = "";

            #region ���ɹҺż����Ӧ�Ŀ��ҡ�ҽ���б�
            FS.HISFC.Models.Registration.RegLevel Level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            if (Level == null)
            {
                return;
            }
            if (Level.IsExpert || Level.IsSpecial)//ר�ҡ�����
            {
                #region ר��
                //�����Ҳ����ר�ҵĿ����б�
                this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Doct);

                //����Combox�����б�
                //{920686B9-AD51-496e-9240-5A6DA098404E}
                this.addRegDeptToCombox();
  

                //���ҽ���б�,��ѡ����Һ��ټ�������ר��
                ArrayList al = new ArrayList();
                this.cmbDoctor.AddItems(al);
               
                #endregion
            }
            else if (Level.IsFaculty)//ר��
            {
                #region ר��
                //��ȡ����ר���б�
                this.GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType.Dept);

                 this.addRegDeptToCombox();

                //���ҽ���б�,ר�Ʋ���Ҫѡ��ҽ��
                ArrayList al = new ArrayList();
                this.cmbDoctor.AddItems(al);
                #endregion
            }
            else//��ͨ
            {
                //��ʾ�����б�
                this.cmbDept.AddItems(this.al);

            }
            #endregion

        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int GetSchemaDept(FS.HISFC.Models.Base.EnumSchemaType type)
        {
            DataSet ds = new DataSet();

            ds = this.SchemaMgr.QueryDept(this.dtBookingDate.Value.Date,
                                        this.registerManager.GetDateTimeFromSysDateTime(), type);
            if (ds == null)
            {
                MessageBox.Show(this.SchemaMgr.Err, "��ʾ");
                return -1;
            }

            this.addDeptToDataSet(ds, type);

            return 0;
        }

        /// <summary>
        /// ��ר�ơ�ר�ҳ��������ӵ�DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="type"></param>
        private void addDeptToDataSet(DataSet ds, FS.HISFC.Models.Base.EnumSchemaType type)
        {
            dsItems.Tables[0].Rows.Clear();
            //DateTime current = this.regMgr.GetDateTimeFromSysDateTime() ;

            if (type == FS.HISFC.Models.Base.EnumSchemaType.Dept)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dsItems.Tables["Dept"].Rows.Add(new object[]
                        {
                            row[0],//���Ҵ���
                            row[1],//��������
                            row[10],//ƴ����
                            row[11],//�����
                            row[12],//�Զ�����
                            row[5],//�Һ��޶�
                            row[6],//�ѹҺ���
                            row[7],//ԤԼ�޶�
                            row[8],//��ԤԼ��
                            row[3],//��ʼʱ��
                            row[4],//����ʱ��
                            row[2],//���
                            FS.FrameWork.Function.NConvert.ToBoolean(row[9])
                        });
                }
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dsItems.Tables["Dept"].Rows.Add(new object[]
                        {
                            row[0],//���Ҵ���
                            row[1],//��������
                            row[2],//ƴ����
                            row[3],//�����
                            row[4],//�Զ�����
                            0,//�Һ��޶�
                            0,//�ѹҺ���
                            0,//ԤԼ�޶�
                            0,//��ԤԼ��
                            DateTime.MinValue,//��ʼʱ��
                            DateTime.MinValue,//����ʱ��
                            "",//���
                            false
                        });
                }
            }
        }


        /// <summary>
        /// init Reg department combox
        /// </summary>
        private void addRegDeptToCombox()
        {
            DataRow row;
            al = new ArrayList();

            for (int i = 0; i < this.dsItems.Tables["Dept"].Rows.Count; i++)
            {
                row = this.dsItems.Tables["Dept"].Rows[i];
                //�ظ��Ĳ����
                if (i > 0 && row["ID"].ToString() == dsItems.Tables["Dept"].Rows[i - 1]["ID"].ToString()) continue;

                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                dept.ID = row["ID"].ToString();
                dept.Name = row["Name"].ToString();
                dept.SpellCode = row["Spell_Code"].ToString();
                dept.WBCode = row["Wb_Code"].ToString();
                dept.UserCode = row["Input_Code"].ToString();

                this.al.Add(dept);
            }

            this.cmbDept.AddItems(this.al);
        }

        private void ucAccountRegister_Load(object sender, EventArgs e)
        {
            initCombox();
            InitNoon();
            this.Clear();
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            if (this.valid() != -1)
            {
                if (GetReg() < 0) return;
                if (OnGetRegister != null)
                {
                    this.OnGetRegister(ref this.regObj);
                }
                this.FindForm().Close();
            }


        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            //this.isfinish = false;
            this.FindForm().Close();
        }

        #region INurseArrayRegister ��Ա

        public event FS.HISFC.BizProcess.Interface.Registration.GetRegisterHander OnGetRegister;

        public FS.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        #endregion

        private int GetReg()
        {
            this.regObj = new FS.HISFC.Models.Registration.Register();

            FS.FrameWork.Models.NeuObject regDept = this.cmbDept.SelectedItem;

            FS.FrameWork.Models.NeuObject regLevel = this.cmbRegLevel.SelectedItem;

            //��ȡ������ˮ��
            regObj.ID = this.registerManager.GetSequence("Registration.Register.ClinicID");

            //������
            regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;
            //������
            regObj.PID.CardNO = patient.PID.CardNO;
            //�Һ�����
            regObj.DoctorInfo.SeeDate = this.registerManager.GetDateTimeFromSysDateTime();
            //�Һ�������
            regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.registerManager.GetDateTimeFromSysDateTime());
            //�Һż���
            regObj.DoctorInfo.Templet.RegLevel.ID = regLevel.ID;
            //�Һż�������
            regObj.DoctorInfo.Templet.RegLevel.Name = regLevel.Name;
            //�����������Է�
            regObj.Pact.PayKind.ID = "01";
            //��������Է�
            regObj.Pact.PayKind.Name = "�Է�";
            //�Һſ��ұ���
            regObj.DoctorInfo.Templet.Dept.ID = regDept.ID;
            //�Һſ�������
            regObj.DoctorInfo.Templet.Dept.Name = regDept.Name;
            //ҽ������
            if (this.cmbDoctor.SelectedIndex == -1)
            {
                regObj.DoctorInfo.Templet.Doct.ID = "";
                regObj.DoctorInfo.Templet.Doct.Name = "";
            }
            else
            {
                regObj.DoctorInfo.Templet.Doct.ID = this.cmbDoctor.SelectedItem.ID;
                regObj.DoctorInfo.Templet.Doct.Name = this.cmbDoctor.SelectedItem.Name;
            }
            //����ҽ������


            regObj.Name = patient.Name;//��������

            regObj.Sex.ID = patient.Sex.ID;//�Ա�

            regObj.Birthday = patient.Birthday;//��������

            //�ֳ��Һ� 
            regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;

            //�˻�֧���ĺ�ͬ��λ�Ƿ�Ϊ�ֽ�����

            regObj.Pact.ID = "1";//��ͬ��λ

            regObj.Pact.Name = "�ֽ�";

            regObj.SSN = "";//ҽ��֤��

            regObj.PhoneHome =  patient.PhoneHome;//��ϵ�绰

            regObj.AddressHome = patient.AddressHome;//��ϵ��ַ

            regObj.CardType.ID = patient.IDCardType.ID;//֤������

            #region �Һŷ�
            int rtn = ConvertRegFeeToObject(regObj);
            if (rtn == -1)
            {
                MessageBox.Show("��ȡ�Һŷѳ���!", "��ʾ");
                return -1;
            }
            if (rtn == 1)
            {
                MessageBox.Show("�ùҺż���δά���Һŷ�,����ά���Һŷ�!", "��ʾ");
                return -1;
            }

            //��û���Ӧ�ա�����
            ConvertCostToObject(regObj);
            #endregion

            //������

            //this.txtRecipeNo.Text = "";
            //��txtRecipeNo.Text��������

            regObj.RecipeNO = this.GetRecipeNo(this.registerManager.Operator.ID);

            //�Ƿ��շ�
            regObj.IsFee = false;
            //�Һ�״̬
            regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            //�Ƿ���
            regObj.IsSee = false;
            //�ҺŲ���Ա
            regObj.InputOper.ID = this.registerManager.Operator.ID;
            //�ҺŲ���Ա��������
            regObj.InputOper.OperTime = this.registerManager.GetDateTimeFromSysDateTime();

            // add by niuxinyuan
            //���ϲ���Ա
            regObj.CancelOper.ID = "";
            regObj.CancelOper.OperTime = DateTime.MinValue;
            //���֤��
            regObj.IDCard = patient.IDCard;

            regObj.IsAccount = true;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime current = this.registerManager.GetDateTimeFromSysDateTime();
            #region ���¿������
            int orderNo = 0;
            string Err = string.Empty;
            //2�������		
            if (this.UpdateSeeID(this.regObj.DoctorInfo.Templet.Dept.ID, this.regObj.DoctorInfo.Templet.Doct.ID,
                this.regObj.DoctorInfo.Templet.Noon.ID, this.regObj.DoctorInfo.SeeDate, ref orderNo,
                ref Err) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Err, "��ʾ");
                return -1;
            }

            regObj.DoctorInfo.SeeNO = orderNo;

            //ר�ҡ�ר�ơ����ԤԼ�Ÿ����Ű��޶�

            #region schema

            if (this.UpdateSchema(this.regObj.RegType, ref orderNo, ref Err, regObj.DoctorInfo.Templet.RegLevel) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (Err != "") MessageBox.Show(Err, "��ʾ");
                return -1;
            }

            regObj.DoctorInfo.SeeNO = orderNo;

            #endregion

            //1ȫԺ��ˮ��			
            if (this.Update(current, ref orderNo, ref Err) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Err, "��ʾ");
                return -1;
            }

            regObj.OrderNO = orderNo;

            #endregion

            if (this.registerManager.Insert(regObj) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.registerManager.Err, "��ʾ");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }


        #region ��Ӧ�ɽ��תΪ�Һ�ʵ��ConvertCostToObject
        /// <summary>
        /// ��Ӧ�ɽ��תΪ�Һ�ʵ��,
        /// ���Բ�����Ϊref��������
        /// </summary>
        /// <param name="obj"></param>
        private void ConvertCostToObject(FS.HISFC.Models.Registration.Register obj)
        {
            //ownCost�Էѽ��,pubCost�������
            decimal othFee = 0, ownCost = 0, pubCost = 0;
            //���ӷ�
            othFee = obj.RegLvlFee.OthFee; //add by niux

            //�õ�����Ӧ�����
            this.getCost(obj.RegLvlFee.RegFee, obj.RegLvlFee.ChkFee, obj.RegLvlFee.OwnDigFee,
                    ref othFee, ref ownCost, ref pubCost, this.regObj.PID.CardNO);

            obj.RegLvlFee.OthFee = othFee;
            obj.OwnCost = ownCost;
            obj.PubCost = pubCost;

        }
        #endregion

        #region ��û���Ӧ�����������getCost
        /// <summary>
        /// ��û���Ӧ�����������
        /// </summary>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <param name="digPub"></param>
        /// <param name="ownCost"></param>
        /// <param name="pubCost"></param>
        /// <param name="cardNo"></param>		
        private void getCost(decimal regFee, decimal chkFee, decimal digFee, ref decimal othFee,
            ref decimal ownCost, ref decimal pubCost, string cardNo)
        {
            ownCost = regFee + chkFee + othFee + digFee;
            pubCost = 0;
        }
        #endregion

        #region ��Ӧ�ɽ��תΪ�Һ�ʵ��ConvertRegFeeToObject
        /// <summary>
        /// ��Ӧ�ɽ��תΪ�Һ�ʵ��,
        /// ���Բ�����Ϊref��������
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int ConvertRegFeeToObject(FS.HISFC.Models.Registration.Register obj)
        {
            decimal regFee = 0, chkFee = 0, digFee = 0, othFee = 0;

            //����ͬ��λ�͹Һż���õ��Һŷ�
            int rtn = this.GetRegFee(obj.Pact.ID, obj.DoctorInfo.Templet.RegLevel.ID,
                          ref regFee, ref chkFee, ref digFee, ref othFee);

            //�Һŷѣ����ѣ����ѣ�������
            obj.RegLvlFee.RegFee = regFee;
            obj.RegLvlFee.ChkFee = chkFee;
            obj.RegLvlFee.OwnDigFee = digFee;
            obj.RegLvlFee.OthFee = othFee;

            return rtn;
        }

        #endregion

        #region ��ȡ�Һŷ�GetRegFee
        /// <summary>
        /// ��ȡ�Һŷ�
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="regLvlID"></param>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <param name="digPubFee"></param>
        /// <returns></returns>
        private int GetRegFee(string pactID, string regLvlID, ref decimal regFee, ref decimal chkFee,
            ref decimal digFee, ref decimal othFee)
        {
            FS.HISFC.Models.Registration.RegLvlFee p = this.regFeeMgr.Get(pactID, regLvlID);
            if (p == null)//����
            {
                return -1;
            }
            if (p.ID == null || p.ID == "")//û��ά���Һŷ�
            {
                return 1;
            }

            regFee = p.RegFee;
            chkFee = p.ChkFee;
            digFee = p.OwnDigFee;
            othFee = p.OthFee;

            return 0;
        }
        #endregion

        #region ���¿����޶�
        /// <summary>
        /// ���¿����޶�
        /// </summary>
        /// <param name="SchMgr"></param>
        /// <param name="regType"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSchema(FS.HISFC.Models.Base.EnumRegType regType, ref int seeNo, ref string Err, FS.HISFC.Models.Registration.RegLevel level)
        {
            int rtn = 1;
            //�Һż���

            if (level.IsFaculty || level.IsExpert)//ר�ҡ�ר��,�۹Һ��޶�
            {


                //�ж��޶��Ƿ�����Һ�

                if (this.IsPermitOverrun(regType, (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                                            level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }
            else if (level.IsSpecial)//����������޶�
            {
                rtn = SchemaMgr.Increase(
                    (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                    false, false, false, true);

                //�ж��޶��Ƿ�����Һ�

                if (this.IsPermitOverrun(regType, (this.dtBookingDate.Tag as FS.HISFC.Models.Registration.Schema).Templet.ID,
                                    level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }

            if (rtn == -1)
            {
                Err = "�����Ű࿴���޶�ʱ����!" + SchemaMgr.Err;
                return -1;
            }

            if (rtn == 0)
            {
                Err = "ҽ���Ű���Ϣ�Ѿ��ı�,������ѡ����ʱ��!";
                return -1;
            }

            return 0;
        }
        #endregion

        #region �жϳ����Һ��޶��Ƿ�����Һ�
        /// <summary>
        /// �жϳ����Һ��޶��Ƿ�����Һ�
        /// </summary>
        /// <param name="schMgr"></param>
        /// <param name="regType"></param>
        /// <param name="schemaID"></param>
        /// <param name="level"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int IsPermitOverrun(FS.HISFC.Models.Base.EnumRegType regType,
                    string schemaID, FS.HISFC.Models.Registration.RegLevel level,
                    ref int seeNo, ref string Err)
        {
            bool isOverrun = false;//�Ƿ񳬶�

            FS.HISFC.Models.Registration.Schema schema = this.SchemaMgr.GetByID(schemaID);
            if (schema == null || schema.Templet.ID == "")
            {
                Err = "��ѯ�Ű���Ϣ����!" + SchemaMgr.Err;
                return -1;
            }
            if (level.IsExpert || level.IsFaculty)//ר�ҡ�ר���ж��޶��Ƿ�����ѹҺ�
            {
                if (schema.Templet.RegQuota - schema.RegedQTY < 0)
                {
                    isOverrun = true;
                }
                seeNo = schema.SeeNO;
            }
            else if (level.IsSpecial)//�����ж������޶��Ƿ񳬱�
            {
                if (schema.Templet.SpeQuota - schema.SpedQTY < 0)
                {
                    isOverrun = true;
                }
                seeNo = schema.SeeNO;
            }

            if (isOverrun)
            {
                //�ӺŲ�����ʾ
                if (schema.Templet.IsAppend) return 0;
                Err = "�Ѿ����������Ű��޶�,���ܹҺ�!";
                return -1;
           
            }

            return 0;
        }
        #endregion

        #region ���¿������
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
            if (this.registerManager.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = registerManager.Err;
                return -1;
            }

            //��ȡȫԺ�������
            if (registerManager.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = registerManager.Err;
                return -1;
            }

            return 0;
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

            #region ""

            if (doctID != null && doctID != "")
            {
                Type = "1";//ҽ��
                Subject = doctID;
            }
            else
            {
                Type = "2";//����
                Subject = deptID;
            }

            #endregion

            //���¿������
            if (this.registerManager.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.registerManager.Err;
                return -1;
            }

            //��ȡ�������		
            if (this.registerManager.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.registerManager.Err;
                return -1;
            }

            return 0;
        }

        #endregion

        #region ��ȡ��ǰ������private void GetRecipeNo(string OperID)
        /// <summary>
        /// ��ȡ��ǰ������
        /// </summary>
        /// <param name="OperID"></param>		
        private string GetRecipeNo(string OperID)
        {
            FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstansObj("RegRecipeNo", OperID);
            if (obj == null)
            {
                MessageBox.Show("��ȡ�����ų���!" + this.conMgr.Err, "��ʾ");
                return null;
            }
            string recipeNo = string.Empty;
            if (obj.Name == "")
            {
                recipeNo = "0";
            }
            else
            {
                recipeNo = obj.Name;
            }
            return recipeNo;
        }

        #endregion

        #region ��ʼ�����InitNoon()
        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private void InitNoon()
        {
            FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();
            this.alNoon = noonMgr.Query();
            if (alNoon == null)
            {
                MessageBox.Show("��ȡ�����Ϣʱ����!" + noonMgr.Err, "��ʾ");
                return;
            }
        }
        #endregion

        #region ���ݵ�ǰʱ���ȡ��� private string getNoon(DateTime current)
        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string getNoon(DateTime current)
        {
            if (this.alNoon == null) return "";
            /*
             * ��������Ϊ���Ӧ���ǰ���һ��ȫ��ʱ�����磺06~12,����:12~18����Ϊ����,
             * ʵ�����Ϊҽ������ʱ���,�������Ϊ08~11:30������Ϊ14~17:30
             * ��������Һ�Ա����������ʱ��ιҺ�,���п�����ʾ���δά��
             * ���Ը�Ϊ���ݴ���ʱ�����ڵ�������磺9��30��06~12֮�䣬��ô���ж��Ƿ��������
             * 06~12֮�䣬ȫ������˵��9:30���Ǹ�������
             */
            //			foreach(FS.HISFC.Object.Registration.Noon obj in alNoon)
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
                    return obj.ID;
                }
            }

            return "";
        }
        #endregion

        private void cmbRegLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Clear()
        {
            this.cmbRegLevel.Tag = string.Empty;
            this.cmbDept.Tag = string.Empty;
            this.cmbDoctor.Tag = string.Empty;
        }

    }
}
