using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY
{
    public partial class ucRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        #region ������
        /// <summary>
        /// ������ù�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee patientMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// Ժע������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy drugMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// ���Һ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager DeptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ��Ա����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager PsMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �Һ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Registration.Register reg = null;
        /// <summary>
        /// ����ҵ��㺯��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ҩ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy storeMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ҽ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        private ArrayList al = new ArrayList();
      

        /// <summary>
        /// ҽ������
        /// </summary>
        private Hashtable htDoctors = new Hashtable();
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        Hashtable hsInfos = new Hashtable();
        /// <summary>
        /// �Ƿ��һ�εǼ�
        /// </summary>
        private bool IsFirstTime = false;
     

        /// <summary>
        /// ���ע��˳���
        /// </summary>
        private int maxInjectOrder = 0;
      
        /// <summary>
        /// ��ӡ��Ѳ�ӿ��ϵ��÷�
        /// </summary>
        private string printUsage = "iv.drip(�ţ�;";
      
        /// <summary>
        /// "������Һ�÷�����ӡƿǩ���÷��� ��ά��ID,���ݷ���;�ָ���"
        /// </summary>
        private string injectUsage = "";

        #region ע��˳���
        /// <summary>
        /// �Ƿ��Զ�����ע��˳���
        /// </summary>
        private bool IsAutoOrder = true;
        /// <summary>
        /// ��ǰע��˳���
        /// </summary>
        private int currentOrder = 0;
        #endregion

        /// <summary>
        /// �Ƿ���ʾ���ߵ���ɵǼǵ�ȫ����������Ƶ����ʾȫ����
        /// {24A47206-F111-4817-A7B4-353C21FC7724}
        /// </summary>
        private bool isShowAllInject = false;

        /// <summary>
        /// �����洢Ƶ��ʵ����ֵ�
        /// {24A47206-F111-4817-A7B4-353C21FC7724}
        /// </summary>
        private Dictionary<string, FS.HISFC.Models.Order.Frequency> dicFrequency = new Dictionary<string, FS.HISFC.Models.Order.Frequency>();

        /// <summary>
        /// �����洢��ӡ�ķ�ҩƷ��Ŀ
        /// </summary>
        private Dictionary<string, FS.FrameWork.Models.NeuObject> dicUndrugInject = new Dictionary<string, FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo IGetOrderNo = null;

        #endregion

        #region ����

        /// <summary>
        /// �����ļ�
        /// </summary>
        private string injectRegisterXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\injectRegister.xml";

        /// <summary>
        /// �Ƿ���ʾ���ߵ���ɵǼǵ�ȫ������
        /// {24A47206-F111-4817-A7B4-353C21FC7724}
        /// </summary>
        [Description("�Ƿ���ʾ���ߵ���ɵǼǵ�ȫ����������Ƶ����ʾȫ����"), Category("����"), DefaultValue("false")]
        public bool IsShowAllInject
        {
            get
            {
                return isShowAllInject;
            }
            set
            {
                isShowAllInject = value;
            }
        }

      

        [Description("�÷��Ƿ��ӡ��Ѳ�ӿ��ϣ�ά��ID,���ݷ���;�ָ���"), Category("����")]
        public string Usage
        {
            get
            {
                return this.printUsage;
            }
            set
            {
                this.printUsage = value;
            }
        }


        [Description("������Һ�÷�����ӡƿǩ���÷��� ��ά��ID,���ݷ���;�ָ���"), Category("����")]
        public string InjectUsage
        {
            get
            {
                return this.injectUsage;
            }
            set
            {
                this.injectUsage = value;
            }
        }

        /// <summary>
        /// �Ƿ�һ�δ�ӡ����Ժע����
        /// </summary>
        private bool isSaveAllInjectCount = true;

        /// <summary>
        /// �Ƿ�һ�δ�ӡ����Ժע����
        /// </summary>
        [Description("�Ƿ�һ�δ�ӡ����Ժע����,������ÿ�δ�ӡһ��"), Category("����")]
        public bool IsSaveAllInjectCount
        {
            get
            {
                return isSaveAllInjectCount;
            }
            set
            {
                isSaveAllInjectCount = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ���ӡƿǩ
        /// </summary>
        private bool isAutoPrint = true;
        [Description("�Ƿ��Զ���ӡѲ�ӿ�"), Category("����")]
        public bool IsAutoPrint
        {
            get
            {
                return isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }






        /// <summary>
        /// �Ƿ��Զ���������
        /// </summary>
        private bool isAutoSave = true;
        [Description("�Ƿ��Զ���������"), Category("����")]
        public bool IsAutoSave
        {
            get
            {
                return isAutoSave;
            }
            set
            {
                this.isAutoSave = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ���������
        /// </summary>
        private bool isUserOrderNumber = false;
        [Description("�Ƿ�ʹ��ע��˳���"), Category("����")]
        public bool IsUserOrderNumber
        {
            get
            {
                return isUserOrderNumber;
            }
            set
            {
                this.isUserOrderNumber = value;
            }
        }

        /// <summary>
        /// �Զ���ӡʱ�����Ƿ���ʾ
        /// </summary>
        private bool isMessageInSave = true;
        [Description("�Զ���ӡʱ�����Ƿ���ʾ"), Category("����")]
        public bool IsMessageInSave
        {
            get
            {
                return isMessageInSave;
            }
            set
            {
                this.isMessageInSave = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ���ض��÷��ķ�ҩƷ
        /// </summary>
        private bool isShowUndrug = true;
        [Description("�Ƿ���ʾ���ض��÷��ķ�ҩƷ"), Category("����")]
        public bool IsShowUndrug
        {
            get
            {
                return isShowUndrug;
            }
            set
            {
                this.isShowUndrug = value;
            }
        }

       

        #endregion

        #region ��ʼ��
        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.dtpStart.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(0 - this.beginDateIntervalDays);
            this.dtpEnd.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(1).AddSeconds(-1);
            this.lbCue.Text = "";
            this.initDoctor();
            this.txtCardNo.Focus();
            this.InitOrder();
        }

        /// <summary>
        /// ��ʼ��ҽ��
        /// </summary>
        private void initDoctor()
        {
            FS.HISFC.BizProcess.Integrate.Manager doctMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            al = doctMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in al)
                {
                    this.htDoctors.Add(p.ID, p.Name);
                }
            }
        }
        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ȫѡ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("��ӡƿǩ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton( "��ӡǩ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null );
            this.toolBarService.AddToolButton("��ӡע�䵥", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡѲ�ӵ�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton( "��ӡ���߿�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null );
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} ���Ӵ�ӡ������
            this.toolBarService.AddToolButton("��ӡ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            //{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
            this.toolBarService.AddToolButton("�޸�Ƥ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "ȫѡ":
                    this.SelectedAll(true);
                    break;
                case "ȡ��":
                    this.SelectedAll(false);
                    break;
                case "��ӡƿǩ":
                    this.PrintCure();
                    break;
                case "��ӡǩ����":
                    this.PrintItinerate();
                    break;
                case "��ӡע�䵥":
                    this.PrintInject();
                    break;
                case "��ӡѲ�ӵ�":
                    this.PrintInjectScoutCard();
                    break;
                case "��ӡ���߿�":
                    this.PrintPatient();
                    break;
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} ���Ӵ�ӡ������
                case "��ӡ������":
                    this.PrintNumber();
                    break;
                //{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
                case "�޸�Ƥ��":
                    this.ModifyHytest();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��ȡҽ�����Ƹ��ݴ���
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetDoctByID(string ID)
        {
            IDictionaryEnumerator dict = htDoctors.GetEnumerator();
            while (dict.MoveNext())
            {
                if (dict.Key.ToString() == ID)
                    return dict.Value.ToString();
            }

            return "";
        }
        /// <summary>
        /// ���ø�ʽ
        /// </summary>
        private void SetFP()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;
            this.neuSpread1_Sheet1.Columns[2].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[3].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[4].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[5].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[6].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[7].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[8].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[9].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[10].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[11].CellType = txtOnly;
            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            this.neuSpread1_Sheet1.Columns[13].CellType = txtOnly;

            if (System.IO.File.Exists(injectRegisterXml))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetNoon(DateTime dt)
        {
            string strNoon = "����";
            if (FS.FrameWork.Function.NConvert.ToInt32(dt.ToString("HH")) >= 12)
            {
                strNoon = "����";
            }
            return strNoon;
        }
        /// <summary>
        /// ѹ����ʾ
        /// </summary>
        private void LessShow()
        {

        }
        /// <summary>
        /// �ж��Ƿ�������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }
      
       
        

        /// <summary>
        /// ֻ��ʾ���һ�ε�
        /// </summary>
        /// <returns></returns>
        private int ShowLastOnly()
        {
            //ȡ�����clinic_code
            int maxClinic = 0;
            if (this.neuSpread1_Sheet1.RowCount <= 0) return -1;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item =
                    (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (FS.FrameWork.Function.NConvert.ToInt32(item.ID) > maxClinic)
                {
                    maxClinic = FS.FrameWork.Function.NConvert.ToInt32(item.ID);
                }
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item =
                    (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (item.ID != maxClinic.ToString())
                {
                    //this.fpSpread1_Sheet1.Rows[i].BackColor = System.Drawing.Color.LightYellow;
                    this.neuSpread1_Sheet1.SetValue(i, 0, false);
                    this.neuSpread1_Sheet1.Rows[i].Remove();
                }
            }
            return 0;
        }
        /// <summary>
        /// ������ע��˳��
        /// </summary>
        /// <returns></returns>
        private int GetMaxInjectOrder()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0) return 0;
            this.neuSpread1.StopCellEditing();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                    this.neuSpread1_Sheet1.GetText(i, 0) == "") continue;
                if (FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 1].Text) > maxInjectOrder)
                {
                    maxInjectOrder = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 1].Text);
                }
            }
            return maxInjectOrder;
        }

        /// <summary>
        /// ���ò�����Ϣ
        /// </summary>
        /// <param name="reg"></param>
        private void SetPatient(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg == null || reg.ID == "")
            {
                return;
            }
            else
            {
                int iAge = FS.FrameWork.Function.NConvert.ToInt32(System.DateTime.Now.ToString("yyyy"))
                    - FS.FrameWork.Function.NConvert.ToInt32(reg.Birthday.ToString("yyyy"));
                this.txtName.Text = reg.Name;
                this.txtSex.Text = reg.Sex.Name;
                this.txtBirthday.Text = reg.Birthday.ToString("yyyy-MM-dd");
                this.txtAge.Text = this.InjMgr.GetAge(reg.Birthday);//iAge.ToString();
                this.txtCardNo.Text = reg.PID.CardNO;

                #region ���
                ArrayList alDiagnoses = new ArrayList();
                alDiagnoses = (new FS.HISFC.BizLogic.HealthRecord.Diagnose()).QueryDiagnoseNoOps(reg.ID);
                this.txtDiagnoses.Text = "";
                if (alDiagnoses != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose dg in alDiagnoses)
                    {
                        if (dg.Memo == reg.ID || dg.Is30Disease == "0")
                        {//�ѷǱ��ιҺŵ���Ϲ��˵�������Ժ���Ϻܶ࣬ͨ�����ַ�ʽ�����Ļ���Ҫ��дһ��ҵ���
                            continue;
                        }
                        else
                        {//�ѱ��ιҺŵ�������������ŵ�lblDiagnose��
                            this.txtDiagnoses.Text += dg.DiagInfo.ICD10.Name + " ";
                        }
                    }
                }
                else
                {
                    this.txtDiagnoses.Text = "";
                }
                #endregion
            }
        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //{FAC1693A-3EBA-44b3-A1E3-6D6750A98D80}
                //this.neuSpread1_Sheet1.SetValue(i, 0, isSelected, false);
                this.neuSpread1_Sheet1.Cells[i, 0].Value = isSelected;
            }
        }
		

        #endregion

        #region  ��ӡ
        /// <summary>
        /// ��ӡ���߿�
        /// </summary>
        private void PrintPatient()
        {
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint patientPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            if (patientPrint == null)
            {
                patientPrint = new FS.HISFC.Components.Nurse.Print.ucPrintPatient() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
                //Nurse.Print.ucPrintPatient uc = new Nurse.Print.ucPrintPatient();
            }
            patientPrint.Init(al);
        }
        /// <summary>
        /// ��ӡƿǩ
        /// </summary>
        private void PrintCure()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint curePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            if (curePrint == null)
            {
                curePrint = new FS.HISFC.Components.Nurse.Print.ucPrintCure() as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            }
            curePrint.Init(al);
        }
        /// <summary>
        /// ��ӡע�䵥.
        /// </summary>
        private void PrintInject()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint injectPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            if (injectPrint == null)
            {
                injectPrint = new FS.HISFC.Components.Nurse.Print.ucPrintInject() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            }
            injectPrint.Init(al);
        }
        /// <summary>
        /// ��ӡǩ����
        /// </summary>
        private void PrintItinerate()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new FS.HISFC.Components.Nurse.Print.ucPrintItinerate() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = false;
            itineratePrint.Init(al);
        }
        /// <summary>
        /// ����ֽ�ŵ�ǩ����
        /// </summary>
        private void PrintItinerateLarge()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new FS.HISFC.Components.Nurse.Print.ucPrintItinerateLarge() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = false;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// {E73C3DF3-9DCF-4a46-ADE0-3D44663F6F7A}
        /// ��ӡ������ҺѲ�ӿ�
        /// </summary>
        private void PrintInjectScoutCard()
        { 
            int intReturn = this.GetAllPrintInjectList();
            if (intReturn==-1)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            int intCount = 0;
            foreach (ArrayList al in hsInfos.Values)
            {
                FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                if (itineratePrint == null)
                {
                    return;
                }

                itineratePrint.IsReprint = false;
                itineratePrint.Init(al);
            }
           
        }

        /// <summary>
        /// {30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} ���Ӻ�����
        /// </summary>
        private void PrintNumber()
        {
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
            ArrayList al = this.GetPrintInjectList();
            //			this.maxInjectOrder = 0;
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint numberPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            if (numberPrint == null)
            {
                numberPrint = new FS.HISFC.Components.Nurse.Print.ucPrintNumber() as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
                //Nurse.Print.ucPrintNumber uc = new Nurse.Print.ucPrintNumber();
            }
            numberPrint.Init(al);
        }

        /// <summary>
        /// ��ȡҪ��ӡ������
        /// (��ӡƿǩ)
        /// </summary>
        /// <returns></returns>
        private ArrayList GetPrintInjectList()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            this.neuSpread1.StopCellEditing();

            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                {
                    continue;
                }

                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, 11].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();
                info.Patient = reg;

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 2].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();

                if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (!injectUsage.Contains(detail.Order.Usage.ID+";"))
                    {
                        continue;
                    }
                }


                //ҽ������
                if (hsEmpl.Contains(detail.RecipeOper.ID))
                {
                    empl = hsEmpl[detail.RecipeOper.ID] as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    empl = this.PsMgr.GetEmployeeInfo(detail.RecipeOper.ID);

                    if (empl == null)
                    {
                        MessageBox.Show("��ȡ��Ա��Ϣʧ��:" + PsMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                        return new ArrayList();

                    }
                    hsEmpl.Add(empl.ID, empl);
                }

                info.Item.Order.ReciptDoctor.Name = empl.Name;
                info.Item.Order.ReciptDoctor.ID = empl.ID;
                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                }
                info.InjectOrder = strOrder;
                al.Add(info);
                //�жϽ�ƿ,���������ӵ�alJiePing��
                if (orderinfo.ExtendFlag1 == null || orderinfo.ExtendFlag1.Length < 1)
                    orderinfo.ExtendFlag1 = "1|";
                //				string[] str = orderinfo.Mark1.Split('|');
                int inum = FS.FrameWork.Function.NConvert.ToInt32(orderinfo.ExtendFlag1.Substring(0, 1));
                info.Memo = inum.ToString();
                //FS.neFS.HISFC.Components.Function.NConvert.ToInt32(str[0]);
                //				if(inum > 1)
                //				{
                //						FS.HISFC.Models.Nurse.Inject inj = new FS.HISFC.Models.Nurse.Inject();
                //						inj = info.Clone();
                //						inj.InjectOrder = (this.GetMaxInjectOrder() + 1).ToString();
                //						maxInjectOrder++;
                //						alJiePing.Add(inj);
                //					}
                //				}

                //{EB016FFE-0980-479c-879E-225462ECA6D0}
                info.PrintNo = detail.User02;
            }
            return al;
        }

        /// <summary>
        /// �Ƿ���Ҫ��ӡ���÷�
        /// </summary>
        /// <param name="usageCode"></param>
        /// <returns></returns>
        private bool IsPrintUsage(string usageCode)
        {
            //�������û��ά����ӡ�÷�ʱ������Ժע�÷���ӡ
            if (string.IsNullOrEmpty(printUsage))
            {
                return SOC.HISFC.BizProcess.Cache.Common.IsInnerInjectUsage(usageCode);
            }
            else
            {
                return printUsage.Contains(usageCode + ";");
            }
        }
       
        /// <summary>
        /// ��ȡҪ��ӡ�����ݣ���ά���÷���
        ///{0D976883-8A45-4a97-AFEF-7D8ED425C89A}
        /// </summary>
        /// <returns></returns>
        private int GetAllPrintInjectList()
        {
            this.SelectAll(true);
            this.neuSpread1.StopCellEditing();
            hsInfos.Clear();
            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                    continue;
                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, 11].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();
                if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (!IsPrintUsage(detail.Order.Usage.ID))
                    {
                        continue;
                    }
                    //if (!printUsage.Contains(detail.Order.Usage.ID + ";"))
                    //{
                    //    continue;
                    //}
                }

                info.Patient.ID = detail.Patient.ID;
                info.Patient.Name = reg.Name;
                info.Patient.Sex.ID = reg.Sex.ID;
                info.Patient.Birthday = reg.Birthday;
                info.Patient.Card.ID = this.txtCardNo.Text.Trim().PadLeft(10, '0');

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 2].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();

                //ҽ������
                if (hsEmpl.Contains(detail.RecipeOper.ID))
                {
                    empl = hsEmpl[detail.RecipeOper.ID] as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    empl = this.PsMgr.GetEmployeeInfo(detail.RecipeOper.ID);

                    if (empl == null)
                    {
                        MessageBox.Show("��ȡ��Ա��Ϣʧ��:" + PsMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                        return -1;
                    }
                    hsEmpl.Add(empl.ID, empl);
                }
                if (empl == null)
                {
                    MessageBox.Show("�Ҳ������ҽ����Ϣ");
                    return -1;
                }
                info.Item.Order.ReciptDoctor.Name = empl.Name;
                info.Item.Order.ReciptDoctor.ID = empl.ID;
                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                }
                info.Item.Days = detail.Days;
                //���ߵ���ע�䴦��ʱ��
                info.InjectOrder = strOrder;
                
                info.User03 = this.neuSpread1_Sheet1.Cells[i, 13].Text;

                string hypoTest = string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 11].Text) ? "" : "(" + this.neuSpread1_Sheet1.Cells[i, 11].Text + ")";

                if (orderinfo != null)
                {
                    //��עӦ����Memo+Ƥ��
                    //info.Memo = orderinfo.ExtendFlag1;
                    info.Memo = orderinfo.Memo;
                    info.Hypotest = orderinfo.HypoTest;
                }

                if (!hsInfos.ContainsKey(info.Item.Order.ReciptDoctor.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(info);
                    hsInfos.Add(info.Item.Order.ReciptDoctor.ID, al);
                }
                else
                {
                    ((ArrayList)hsInfos[info.Item.Order.ReciptDoctor.ID]).Add(info);
                }
            }
            return 1;
        }

        /// <summary>
        /// ��ȡ�����ȵ�ʹ�÷���
        /// </summary>
        /// <param name="IsInit">�Ƿ��ʼ��</param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Const GetFirstUsage()
        {
            FS.HISFC.Models.Fee.Outpatient.FeeItemList info = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            if (this.neuSpread1_Sheet1.RowCount <= 0) return new FS.HISFC.Models.Base.Const();

            int FirstCodeNum = 10000;
            FS.HISFC.Models.Base.Const retobj = new FS.HISFC.Models.Base.Const();
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    info = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                    FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstant("SPECIAL", info.Order.Usage.ID);
                    FS.HISFC.Models.Base.Const conobj = (FS.HISFC.Models.Base.Const)obj;

                    if (conobj.SortID < FirstCodeNum)
                    {
                        FirstCodeNum = conobj.SortID;
                        retobj = conobj;
                    }
                }
            }
            catch
            {
                return retobj;
            }

            return retobj;
        }
        #endregion	

        #region ע��˳��ŵĴ���
        /// <summary>
        /// ����Ĭ��ע��˳��
        /// </summary>
        private void SetInject()
        {

            #region  û�����ݾͲ�����,ֱ�ӷ���
            if (this.neuSpread1_Sheet1.RowCount <= 0) return;
            #endregion

            #region ���û��߽����ע��˳���
            if (this.chkIsOrder.Checked)
            {
                this.SetOrder();
            }
            else
            {
                this.txtOrder.Text = "0";
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 14].Text = this.txtOrder.Text;
                }
            }
            #endregion

            #region ����ÿ����Ŀ��ע��˳��
            int InjectOrder = 1;
            this.neuSpread1_Sheet1.SetValue(0, 1, 1, false);
            for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
            {

                if (this.neuSpread1_Sheet1.Cells[i, 7].Text == null || this.neuSpread1_Sheet1.Cells[i, 7].Text.Trim() == "")
                {
                    InjectOrder++;
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else if (this.neuSpread1_Sheet1.Cells[i, 7].Text != null && this.neuSpread1_Sheet1.Cells[i, 7].Text.Trim() != ""
                    //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                    && this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, 13].Text == this.neuSpread1_Sheet1.Cells[i - 1, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i - 1, 13].Text)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else
                {
                    InjectOrder++;
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
            }
            #endregion

        }

        /// <summary>
        /// ��ʼ��ע��˳���
        /// </summary>
        private void InitOrder()
        {
            //��ȡ�Ƿ��Զ�����ע��˳��
            try
            {
                bool isAutoInjectOrder = false;
                isAutoInjectOrder = FS.FrameWork.Function.NConvert.ToBoolean(this.conMgr.QueryControlerInfo("900005"));
                if (isAutoInjectOrder)
                {
                    this.chkIsOrder.Checked = true;
                    this.SetOrder();
                    this.lbLastOrder.Text = "�������һ��ע���:" +
                        (FS.FrameWork.Function.NConvert.ToInt32(this.txtOrder.Text.Trim()) - 1).ToString();
                }
                else
                {
                    this.chkIsOrder.Checked = false;
                    this.lbLastOrder.Text = "�������Զ�����ע��˳���!";
                    this.txtOrder.Text = "0";
                }
                //XmlDocument doc = new XmlDocument();
                //doc.Load(Application.StartupPath + "/Setting/NurseSetting.xml");
                //XmlNode node = doc.SelectSingleNode("//�Ƿ�����ע��˳��");

                //if (node != null && node.Attributes["isAutoInjectOrder"].Value.ToString() == "false")
                //{
                //    this.chkIsOrder.Checked = false;
                //    this.lbLastOrder.Text = "�������Զ�����ע��˳���!";
                //    this.txtOrder.Text = "0";
                //}
                //else
                //{
                //    this.chkIsOrder.Checked = true;
                //    this.SetOrder();
                //    this.lbLastOrder.Text = "�������һ��ע���:" +
                //        (FS.FrameWork.Function.NConvert.ToInt32(this.txtOrder.Text.Trim()) - 1).ToString();
                //}
            }
            catch //�������ļ�
            {
                this.chkIsOrder.Checked = false;
                this.lbLastOrder.Text = "�������Զ�����ע��˳���!";
                this.txtOrder.Text = "0";
            }


        }
        /// <summary>
        /// ����ע���
        /// </summary>
        private void SetOrder()
        {
            if (!this.chkIsOrder.Checked)
            {
                this.txtOrder.Text = "0";
                this.lbLastOrder.Text = "���ڱ���û�������Զ��������!";
                return;
            }
            //����Զ�����,���õ�һ�����,����ֵthis.currentOrder
            //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B} ��Ϊͨ���ӿ�ʵ�֣����û��������ԭ�����������
            this.CreateInterface();
            if (IGetOrderNo != null)
            {
                string orderNo = IGetOrderNo.GetOrderNo(this.reg);
                this.txtOrder.Text = orderNo;
                if (this.neuSpread1_Sheet1.Rows.Count == 0)
                {
                    return;
                }
                string comboAndInjectTime = this.neuSpread1_Sheet1.Cells[0, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[0, 13].Text;
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string rowComboAndInjectTime = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, 13].Text;
                    if (comboAndInjectTime != rowComboAndInjectTime)
                    {
                        comboAndInjectTime = rowComboAndInjectTime;
                        orderNo = IGetOrderNo.GetSamePatientNextOrderNo(orderNo);
                    }
                    this.neuSpread1_Sheet1.Cells[i, 14].Text = orderNo;
                }
                return;
            }
            else
            {
                FS.HISFC.Models.Nurse.Inject info = this.InjMgr.QueryLast();
                if (info != null && info.Booker.OperTime != System.DateTime.MinValue)
                {
                    if (info.Booker.OperTime.ToString("yyyy-MM-dd")
                        == this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd"))
                    {
                        this.txtOrder.Text = (FS.FrameWork.Function.NConvert.ToInt32(info.OrderNO) + 1).ToString();
                    }
                    else
                    {
                        this.txtOrder.Text = "1";
                    }
                }
                else
                {
                    this.txtOrder.Text = "1";
                }
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 14].Text = this.txtOrder.Text;
                }
            }
        }

        /// <summary>
        /// �����ӿ�
        /// {30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
        /// </summary>
        private void CreateInterface()
        {
            if (this.IGetOrderNo == null)
            {
                this.IGetOrderNo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo)) as FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ȷ�ϱ���( 1.met_nuo_inject�����¼  2.fin_ipb_feeitemlist������ȷ��Ժע������ȷ�ϱ�־)
        /// </summary>
        private int Save()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("û��Ҫ���������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            
            this.neuSpread1.StopCellEditing();
            int selectNum = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE" || this.neuSpread1_Sheet1.GetValue(i, 0).ToString() == "")
                {
                    selectNum++;
                }
            }
            if (selectNum >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("��ѡ��Ҫ���������", "��ʾ");
                return -1;
            }

            if (this.isUserOrderNumber)
            {
                #region �ж�������кŵ���Ч��
                if (this.txtOrder.Text == null || this.txtOrder.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("û���������˳���!");
                    this.txtOrder.Focus();
                    return -1;
                }
           
                else if (this.InjMgr.QueryInjectOrder(this.txtOrder.Text.Trim().ToString()).Count > 0)
                {
                    if (MessageBox.Show("�ö��к��Ѿ�ʹ��,�Ƿ����!", "��ʾ", System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        this.txtOrder.Focus();
                        return -1;
                    }
                }
             
                #endregion


                #region ���ע��˳��ŵ���Ч�ԣ������ͬ�ģ�ע��˳���Ҳ������ͬ��
                for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 7].Tag != null && this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() != "" &&
                        //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                        this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, 13].Text == this.neuSpread1_Sheet1.Cells[i - 1, 7].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i - 1, 13].Text
                        && this.neuSpread1_Sheet1.GetValue(i, 1).ToString() != this.neuSpread1_Sheet1.GetValue(i - 1, 1).ToString()
                        )
                    {
                        MessageBox.Show("��ͬ��ŵ�ע��˳��ű�����ͬ!", "��" + (i + 1).ToString() + "��");
                        return -1;
                    }
                }
                #endregion
            }

            #region ���Ժע��������Ч�ԣ������ͬ�ģ�ע��˳���Ҳ������ͬ��
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                string strnum = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                if (strnum == null || strnum == "")
                {
                    MessageBox.Show("Ժע��������Ϊ��!", "��" + (i + 1).ToString() + "��");
                    return -1;
                }
                if (!this.IsNum(strnum))
                {
                    MessageBox.Show("Ժע��������Ϊ����!", "��" + (i + 1).ToString() + "��");
                    return -1;
                }
                string completenum = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "TRUE")
                    if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) == 0)
                    {
                        continue;
                    }

                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) <= FS.FrameWork.Function.NConvert.ToInt32(completenum))
                    {
                        MessageBox.Show("Ժע��������!", "��" + (i + 1).ToString() + "��");
                        return -1;
                    }
                }
             
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            try
            {
                this.InjMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.drugMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.DeptMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.PsMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                DateTime confirmDate = this.InjMgr.GetDateTimeFromSysDateTime();

                FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
                FS.HISFC.Models.Nurse.Inject info = null;

                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                        this.neuSpread1_Sheet1.GetText(i, 0) == "")
                    {
                        continue;
                    }
                    detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;

                 

                    info = new FS.HISFC.Models.Nurse.Inject();

                    #region ʵ��ת����������Ŀ�շ���ϸʵ��FeeItemList��->ע��ʵ��Inject��

                    info.Patient = reg;
                    info.Patient.ID = detail.Patient.ID;
                    info.Patient.Name = reg.Name;
                    info.Patient.Sex.ID = reg.Sex.ID;
                    info.Patient.Birthday = reg.Birthday;
                    info.Patient.PID.CardNO= reg.PID.CardNO;

                    //info.Item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)detail.Item;
                    info.Item = detail;
                    info.Item.ID = detail.Item.ID;
                    info.Item.Name = detail.Item.Name;

                    //info.Item.Item.MinFee.ID = detail.Item.MinFee.ID;
                    //info.Item.Item.Price = detail.Item.Price;
                    
               

                    

                    info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 2].Text);
                    //������������
                    if (hsDept.Contains(detail.RecipeOper.Dept.ID))
                    {
                        dept = hsDept[detail.RecipeOper.Dept.ID] as FS.HISFC.Models.Base.Department;
                    }
                    else
                    {
                        dept = this.DeptMgr.GetDepartment(detail.RecipeOper.Dept.ID); 
                        if (dept == null)
                        {
                            MessageBox.Show("��ȡ�������Ҵ���:" + DeptMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                            return -1;

                        }
                        hsDept.Add(dept.ID, dept);
                    }

                    info.Item.Order.DoctorDept.Name = dept.Name;
                    info.Item.Order.DoctorDept.ID = dept.ID;
                    //ҽ������
                    if (hsEmpl.Contains(detail.RecipeOper.ID))
                    {
                        empl = hsEmpl[detail.RecipeOper.ID] as FS.HISFC.Models.Base.Employee;
                    }
                    else
                    {
                        empl = this.PsMgr.GetEmployeeInfo(detail.RecipeOper.ID);
                        if (empl == null)
                        {
                            MessageBox.Show("��ȡ��Ա��Ϣʧ��:" + PsMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                            return -1;

                        }
                        hsEmpl.Add(empl.ID, empl);
                    }

                    info.Item.Order.ReciptDoctor.Name = empl.Name;
                    info.Item.Order.ReciptDoctor.ID = empl.ID;
                    //�Ƿ�Ƥ��
                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && this.neuSpread1_Sheet1.Cells[i, 11].Tag.ToString().ToUpper() == "TRUE")
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                    }
                    else
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                    }
                    #endregion

                    info.ID = this.InjMgr.GetSequence("Nurse.Inject.GetSeq");
                    //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                    //info.OrderNO = this.txtOrder.Text.ToString();
                    info.OrderNO = this.neuSpread1_Sheet1.Cells[i, 14].Text;
                    //{24A47206-F111-4817-A7B4-353C21FC7724}
                    info.PrintNo = detail.User02;
                    info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();
                    info.Booker.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    info.Booker.OperTime = confirmDate;
                    info.Item.ExecOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    string strOrder = "";
                    if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                    {
                        strOrder = "";
                    }
                    else
                    {
                        strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                    }
                    info.InjectOrder = strOrder;
                    info.Item.Days = detail.Days;
                    string hypoTest = this.neuSpread1_Sheet1.Cells[i, 11].Text;

                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //��ע--(ȡҽ����ע)
                        FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                            (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, 11].Tag;
                        if (orderinfo != null)
                        {
                            //��עӦ����Memo+Ƥ��
                            //info.Memo = orderinfo.ExtendFlag1;
                            info.Memo = orderinfo.Memo;
                            info.Hypotest = orderinfo.HypoTest;
                        }
                    }

                    #region ��met_nuo_inject�У������¼
                    if (this.InjMgr.Insert(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.InjMgr.Err, "��ʾ");
                        return -1;
                    }
                    #endregion

                    #region ��fin_ipb_feeitemlist�У���������

                    //���µ�Ժע����
                    int updateInjectCount = 1;
                    //Ĭ�ϱ���һ���ע�����
                    //int updateInjectCount = SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(detail.Order.Frequency.ID);
                    if (isSaveAllInjectCount)
                    {
                        if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            updateInjectCount = detail.InjectCount;
                        }
                        else
                        {
                            updateInjectCount = FS.FrameWork.Function.NConvert.ToInt32(detail.Item.Qty);
                        }
                    }
                    if (this.patientMgr.UpdateConfirmInject(detail.Order.ID, detail.RecipeNO, detail.SequenceNO.ToString(), updateInjectCount) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.patientMgr.Err, "��ʾ");
                        return -1;
                    }
                    #endregion
                    info.Item.InjectCount = info.Item.InjectCount;
                   
                    this.lbLastOrder.Text = "�������һ��ע���:" + info.OrderNO;

                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            if (this.isMessageInSave)
            {
                MessageBox.Show("����ɹ�!", "��ʾ");
            }
            this.Clear();
            this.lbCue.Text = "";

            this.txtCardNo.SelectAll();
            this.txtRecipe.Text = "";
            this.txtCardNo.Text = "";
            this.txtCardNo.Focus();
            return 0;
        }
        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            this.txtOrder.Text = "";
            this.txtRecipe.Text = "";
            
            this.txtName.Text = string.Empty;
            this.txtDiagnoses.Text = string.Empty;
            this.txtBirthday.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtSex.Text = string.Empty;


            reg = null;
            this.txtCardNo.Focus();
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }
            string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

            //��ȡҽ�������Ĵ�����Ϣ��û��ȫ��ִ����ģ�
            DateTime dtFrom = this.dtpStart.Value.Date;
            DateTime dtTo = this.dtpEnd.Value.Date.AddDays(1);

            if (this.txtRecipe.Text.Trim() == "")
            {
                al = this.patientMgr.QueryOutpatientFeeItemListsZs(cardNo, dtFrom, dtTo);
            }
            else
            {
                al = this.patientMgr.QueryFeeDetailFromRecipeNO(this.txtRecipe.Text.Trim());

                if (al == null)
                {
                    return;
                }

                if (al.Count > 0)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)al[0];
                    reg = this.regMgr.GetByClinic(item.Patient.ID);
                    if (reg == null || reg.ID == "")
                    {
                        MessageBox.Show("û�в�����Ϊ:" + item.Patient.ID + "�Ļ���!", "��ʾ");
                        this.txtCardNo.Focus();
                        this.Clear();
                        return;
                    }

                    this.txtCardNo.Text = item.Patient.PID.CardNO;
                    this.SetPatient(reg);
                    this.txtRecipe.Text = "";
                    this.Query();

                    return;
                }

            }

            this.Query(al);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private void Query(ArrayList al)
        {
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("�û���û����Ҫȷ�ϵ�ҽ����Ϣ!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }

            this.AddDetail(al);

            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("��ʱ�����û�иû�����Ϣ!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                int confirmNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 3].Text);
                if (confirmNum == 0)
                {
                    this.lbCue.Text = "�״�Ժע,��˶�Ժע����!";
                    this.lbCue.ForeColor = System.Drawing.Color.Magenta;
                }
            }

            this.SelectAll(true);
            this.SetComb();

            this.SetFP();

            this.txtOrder.Focus();
            if (this.isUserOrderNumber)
            {
                this.SetInject();
            }

            //��ӡѲ�ӿ�
            if (this.isAutoPrint)
            {
                if (IsFirstTime)
                {
                    this.PrintInjectScoutCard();
                }
                this.PrintCure();
            }

            if (this.isAutoSave)
            {
                this.SelectAll(true);
                this.Save();
            }
        }
        /// <summary>
        /// �����Ŀ��ϸ
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(ArrayList details)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }

            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> tmpFeeList = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            if (details != null)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in al)
                {
                    if (detail.CancelType == FS.HISFC.Models.Base.CancelTypes.Canceled)
                    {
                        continue;
                    }
                    if (detail.TransType == FS.HISFC.Models.Base.TransTypes.Negative)
                    {
                        continue;
                    }


                    #region �ж��Ƿ���Ҫ��ӡע�䵥
                    if (detail.ConfirmedInjectCount == 0)
                    {
                        IsFirstTime = true;
                    }
                    #endregion

                    #region  Ժע���� <= ��ȷ��Ժע���� �Ĳ���ʾ

                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        //Ժע���� <= ��ȷ��Ժע���� �Ĳ���ʾ
                        if (detail.InjectCount != 0 && detail.InjectCount <= detail.ConfirmedInjectCount && !this.cbFinish.Checked)
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region  �жϷ�ҩƷ�Ƿ���ʾ
                    if (isShowUndrug)
                    {
                        if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                        {
                            //����Ժע�������������Ĳ���ʾ
                            if (detail.ConfirmedInjectCount == detail.Item.Qty)
                            {
                                continue;
                            }

                            if (!dicUndrugInject.ContainsKey(detail.Item.ID))
                            {
                                continue;
                            }

                        }
                    }
                    else
                    {
                        if (detail.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            continue;
                        }
                    }
                    #endregion

                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        #region ά����Щ�÷���ʾ�ڽ�����
                        if (!IsPrintUsage(detail.Order.Usage.ID ))
                        {
                            continue;
                        }
                        //if (!printUsage.Contains(detail.Order.Usage.ID))
                        //{
                        //    continue;
                        //}
                        #endregion
                    }

                    #region �Ƿ���ʾ0������
                    if (!chkNullNum.Checked && detail.InjectCount == 0)
                    {
                        continue;
                    }
                    #endregion

                    #region �����Ѿ��Ǽǵ�QD����ʾ��ע�����ε�BID����ʾ����ǰ���ע��һ�ε�BID������ʾ��(���ݽ���ĵǼ�ʱ��)
                    DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(
                        this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd 00:00:00"));
                    ArrayList alTodayInject = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO, detail.SequenceNO.ToString(), dt);
                    if (!this.dicFrequency.ContainsKey(detail.Order.Frequency.ID))
                    {
                        continue;
                    }

                    FS.HISFC.Models.Order.Frequency frequence = this.dicFrequency[detail.Order.Frequency.ID];
                    string[] injectTime = frequence.Time.Split('-');
                    
                    //������Ѿ�ȫ��ע����Ϻ�����
                    if (alTodayInject.Count >= injectTime.Length)
                    {
                        continue;
                    }
                    if (this.isShowAllInject)
                    {
                        for (int i = alTodayInject.Count; i < injectTime.Length; i++)
                        {
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList newDetail = detail.Clone();
                            newDetail.User03 = injectTime[i];
                            tmpFeeList.Add(newDetail);
                        }
                    }
                    else
                    {
                        //δ���ϴ�ע��ʱ��Ļ��������ٴεǼ�
                        if (alTodayInject.Count > 0)
                        {
                            DateTime lastInjectTime = FrameWork.Function.NConvert.ToDateTime(dt.ToString("yyyy-MM-dd ") + injectTime[alTodayInject.Count - 1] + ":00");
                            if (this.InjMgr.GetDateTimeFromSysDateTime() < lastInjectTime)
                            {
                                continue;
                            }
                        }
                        detail.User03 = injectTime[alTodayInject.Count];
                        tmpFeeList.Add(detail);
                    }
                    #endregion
                }

                //����
                tmpFeeList.Sort(new FeeItemListSort());
                //��ȡ��ӡ���
                this.CreateInterface();
                if (this.IGetOrderNo != null)
                {
                    this.IGetOrderNo.SetPrintNo(new ArrayList(tmpFeeList.ToArray()));
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in tmpFeeList)
                {
                    this.AddDetail(feeItem);
                }

                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.LessShow();
                }
            }
        }

        FS.HISFC.Models.Pharmacy.Item drug = null;

        /// <summary>
        /// ��������
        /// </summary>
        FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

        /// <summary>
        /// Ա����Ϣ
        /// </summary>
        FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();

        Hashtable hsEmpl = new Hashtable();

        FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;

        /// <summary>
        /// ��ſ�����Ϣ
        /// </summary>
        Hashtable hsDept = new Hashtable();

        FS.HISFC.Models.Pharmacy.ApplyOut applyOutObj = null;

        /// <summary>
        /// �����ϸ
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList info)
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            int row = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[row].Tag = info;

            #region "���ڸ�ֵ"
            #region ��ȡƤ����Ϣ
            string strTest = "��";
            //��ȡƤ����Ϣ
            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                drug = this.drugMgr.GetItem(info.Item.ID);
                if (drug == null)
                {
                    MessageBox.Show("��ȡҩƷƤ����Ϣʧ��!");
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                    return;
                }
                strTest = "��";
                if (drug.IsAllergy)
                {
                    strTest = "��";
                }
            }
            //
            #endregion

            if (hsDept.Contains(info.RecipeOper.Dept.ID))
            {
                dept = hsDept[info.RecipeOper.Dept.ID] as FS.HISFC.Models.Base.Department;
            }
            else
            {
                dept = this.DeptMgr.GetDepartment(info.RecipeOper.Dept.ID);
                if (dept == null)
                {
                    MessageBox.Show("��ȡ�������Ҵ���:" + DeptMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                    return;

                }
                hsDept.Add(dept.ID, dept);
            }

            //{765D340F-879B-4761-9FFA-E1A629886025}�ж�ҽ��������û��ά��
            if (dept!= null)
            {
                info.Order.DoctorDept.Name = dept.Name;
            }
            else
            {
                MessageBox.Show("ҽ������û��ά��");
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                return;
            }

            this.neuSpread1_Sheet1.SetValue(row, 1, "", false);//ע��˳���
            this.neuSpread1_Sheet1.SetValue(row, 2, info.InjectCount.ToString(), false);//Ժע����
            this.neuSpread1_Sheet1.SetValue(row, 3, info.ConfirmedInjectCount.ToString(), false);//�Ѿ�ȷ�ϵ�Ժע����
            this.neuSpread1_Sheet1.SetValue(row, 4, this.GetDoctByID(info.RecipeOper.ID), false);//����ҽ��
            this.neuSpread1_Sheet1.Cells[row, 4].Tag = info.Order.ReciptDoctor.ID;
            this.neuSpread1_Sheet1.SetValue(row, 5, dept.Name, false);//�Ʊ�
            this.neuSpread1_Sheet1.Cells[row, 5].Tag = info.Order.DoctorDept.ID;
            this.neuSpread1_Sheet1.SetValue(row, 6, info.Item.Name, false);//ҩƷ����
            this.neuSpread1_Sheet1.Cells[row, 7].Tag = info.Order.Combo.ID;//��Ϻ�
            this.neuSpread1_Sheet1.SetValue(row, 8, info.Order.DoseOnce.ToString() + info.Order.DoseUnit, false);//ÿ����
            this.neuSpread1_Sheet1.SetValue(row, 9, info.Order.Frequency.ID, false);//Ƶ��
            this.neuSpread1_Sheet1.Cells[row, 9].Tag = info.Order.Frequency.ID.ToString();
            this.neuSpread1_Sheet1.SetValue(row, 10, info.Order.Usage.Name, false);//�÷�
            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                this.neuSpread1_Sheet1.Cells[row, 11].Tag = drug.IsAllergy.ToString().ToUpper();
            }
            orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();

            if (info.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                orderinfo = orderMgr.GetOneOrder(info.Patient.ID, info.Order.ID);
                if (orderinfo != null)
                {
                    this.neuSpread1_Sheet1.SetText(row, 12, string.IsNullOrEmpty(orderinfo.Memo) ? " " : orderinfo.Memo);

                    if (orderinfo.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest)
                    {
                        if (!drug.IsAllergy)
                        {
                            orderinfo.HypoTest = 0;//���Ե�ֵΪ��
                        }
                    }
                    this.neuSpread1_Sheet1.Cells[row, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());
                    this.neuSpread1_Sheet1.Cells[row, 11].Tag = orderinfo;
                }
                else
                {
                    orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();
                    if (drug.IsAllergy)
                    {
                        orderinfo.Item = drug;
                        orderinfo.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest;
                    }
                    else
                    {
                        orderinfo.HypoTest = 0;
                    }

                    this.neuSpread1_Sheet1.Cells[row, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());
                    this.neuSpread1_Sheet1.Cells[row, 11].Tag = orderinfo;

                }
            }
            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            this.neuSpread1_Sheet1.Cells[row, 13].Text = info.User03;
            #endregion
        }

        /// <summary>
        /// ��ȡƤ����Ϣ
        /// </summary>
        /// <param name="hytestID"></param>
        /// <returns></returns>
        private string GetHyTestInfo(string hytestID)
        {
            switch (hytestID)
            {
                case "1":
                    return "����";
                case "2":
                    return "��Ƥ��";
                case "3":
                    return "����";
                case "4":
                    return "����";
                case "0":
                default:
                    return "��";
            }
        }

        /// <summary>
        /// ������Ϻ�
        /// </summary>
        private void SetComb()
        {
            int myCount = this.neuSpread1_Sheet1.RowCount;
            int i;
            //��һ��
            this.neuSpread1_Sheet1.SetValue(0, 7, "��");
            //�����
            this.neuSpread1_Sheet1.SetValue(myCount - 1, 7, "��");
            //�м���
            for (i = 1; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, 7].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, 7].Tag.ToString();

                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                string currentRowInjectTime = this.neuSpread1_Sheet1.Cells[i, 13].Text.ToString();
                string priorRowInjectTime = this.neuSpread1_Sheet1.Cells[prior, 13].Text.ToString();
                string nextRowInjectTime = this.neuSpread1_Sheet1.Cells[next, 13].Text.ToString();

                #region """""
                bool bl1 = true;
                bool bl2 = true;
                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                    bl1 = false;
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                    bl2 = false;
                //  ��
                if (bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "��");
                }
                //  ��
                if (bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "��");
                }
                //  ��
                if (!bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "��");
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "");
                }
                #endregion
            }
            //��û����ŵ�ȥ��
            for (i = 0; i < myCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() == "")
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "");
                }
            }
            //�ж���ĩ�� ����ţ���ֻ���Լ�һ�����ݵ����
            if (myCount == 1)
            {
                this.neuSpread1_Sheet1.SetValue(0, 7, "");
            }
            //ֻ����ĩ���У���ô��Ҫ�ж���Ű�
            if (myCount == 2)
            {
                if (this.neuSpread1_Sheet1.Cells[0, 7].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, 7].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                    this.neuSpread1_Sheet1.SetValue(1, 7, "");
                }
                //��ֹһ��ҩbid�����һ��
                if (this.neuSpread1_Sheet1.Cells[0, 13].Text.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, 13].Text.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                    this.neuSpread1_Sheet1.SetValue(1, 7, "");
                }
            }
            if (myCount > 2)
            {
                if (this.neuSpread1_Sheet1.GetValue(1, 7).ToString() != "��"
                    && this.neuSpread1_Sheet1.GetValue(1, 7).ToString() != "��")
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                }
                if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 7).ToString() != "��"
                    && this.neuSpread1_Sheet1.GetValue(myCount - 2, 7).ToString() != "��")
                {
                    this.neuSpread1_Sheet1.SetValue(myCount - 1, 7, "");
                }
            }

        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {

        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        private void SelectedAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //{FAC1693A-3EBA-44b3-A1E3-6D6750A98D80}
                //this.neuSpread1_Sheet1.SetValue(i, 0, isSelected, false);
                this.neuSpread1_Sheet1.Cells[i, 0].Value = isSelected;
            }
        }

        private void SelectedComb(bool isSelect)
        {
            
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            string combID = this.neuSpread1_Sheet1.Cells[row, 7].Tag.ToString();
            //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
            string injectTime = this.neuSpread1_Sheet1.Cells[row, 13].Text;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //{24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
                if (this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString() == combID && this.neuSpread1_Sheet1.Cells[i, 13].Text == injectTime) 
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = isSelect;
                }
            }

        }

        /// <summary>
        /// �޸�Ƥ����Ϣ//{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
        /// </summary>
        private void ModifyHytest()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value);
                if (isSelected)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.neuSpread1_Sheet1.Cells[i, 11].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (orderinfo.HypoTest <= FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest) continue;
                    al.Add(orderinfo);
                }

            }

            if (al.Count == 0)
            {
                return;
            }
            FS.HISFC.Components.Nurse.Forms.frmHypoTest frmHypoTest = new FS.HISFC.Components.Nurse.Forms.frmHypoTest();
            frmHypoTest.AlOrderList = al;
            DialogResult d = frmHypoTest.ShowDialog();
            if (d == DialogResult.OK)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value);
                    if (!isSelected)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.neuSpread1_Sheet1.Cells[i, 11].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    //if (orderinfo.HypoTest == 1)
                    //{
                    //    strHypoTest = "��";
                    //}
                    //else if (orderinfo.HypoTest == 2)
                    //{
                    //    strHypoTest = "��";
                    //}
                    //else if (orderinfo.HypoTest == 3)
                    //{
                    //    strHypoTest = "����";
                    //}
                    //else if (orderinfo.HypoTest == 4)
                    //{
                    //    strHypoTest = "����";
                    //}
                    this.neuSpread1_Sheet1.Cells[i, 11].Text = this.GetHyTestInfo(orderinfo.HypoTest.ToString());

                }
            }


        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ucRegister()
        {
            InitializeComponent();
        }

        private void ucDept_Load(object sender, EventArgs e)
        {
            this.Init();
            this.SetFP();
            //{24A47206-F111-4817-A7B4-353C21FC7724} ��ʼ��������
            this.InitHelper();
            //{EB016FFE-0980-479c-879E-225462ECA6D0} ƿǩ����
            this.ucCureReprint1.Init();
            this.Clear();
        }

        /// <summary>
        /// ��ʼ��������
        /// {24A47206-F111-4817-A7B4-353C21FC7724}
        /// </summary>
        private void InitHelper()
        {
            //Ƶ��
            ArrayList alFrequency = this.conMgr.QuereyFrequencyList();
            foreach (FS.HISFC.Models.Order.Frequency frequency in alFrequency)
            {
                if (!this.dicFrequency.ContainsKey(frequency.ID))
                {
                    this.dicFrequency.Add(frequency.ID, frequency);
                }
            }
            ArrayList alUndrugInject = this.conMgr.QueryConstantList("UndrugInject");
            foreach (FS.FrameWork.Models.NeuObject neuObject in alUndrugInject)
            {
                if (!this.dicUndrugInject.ContainsKey(neuObject.ID))
                {
                    this.dicUndrugInject.Add(neuObject.ID,neuObject);
                }
            }
        }

        FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
        FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ��ѯ�ĹҺŵ����ڼ��
        /// </summary>
        private int queryRegDays = 2;

        /// <summary>
        /// ��ѯ�ĹҺŵ����ڼ��
        /// </summary>
        [Category("��ѯ����"), Description("��ѯ�Һ���Ϣ�����ڼ�����ӽ����ϵĿ�ʼʱ����ǰ��ѯ���죩")]
        public int QueryRegDays
        {
            get
            {
                return queryRegDays;
            }
            set
            {
                queryRegDays = value;
            }
        }

        /// <summary>
        /// ��ʼʱ������ڼ��(�����ļ������)
        /// </summary>
        private int beginDateIntervalDays = 0;

        /// <summary>
        /// ��ʼʱ������ڼ��(�����ļ������)
        /// </summary>
        [Category("��ѯ����"), Description("��ʼʱ������ڼ��(�����ļ������)")]
        public int BeginDateIntervalDays
        {
            get
            {
                return beginDateIntervalDays;
            }
            set
            {
                beginDateIntervalDays = value;
            }
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtRecipe.Text = string.Empty;
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("�����벡����!", "��ʾ");
                    this.txtCardNo.Focus();
                    this.Clear();
                    return;
                }
                string strCardNO = this.txtCardNo.Text.Trim();//.PadLeft(10, '0');
                int iTemp = feeManage.ValidMarkNO(strCardNO, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("��Ч���ţ�����ϵ����Ա��");
                    this.txtCardNo.Focus();
                    this.Clear();
                    return ;
                }
                this.Clear();

                string cardNo = objCard.Patient.PID.CardNO;
                ArrayList alRegs = this.regMgr.Query(cardNo, this.dtpStart.Value.AddDays(0 - queryRegDays));
                if (alRegs == null || alRegs.Count == 0)
                {
                    MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                    this.txtCardNo.Focus();
                    this.Clear();
                    return;
                }
                reg = alRegs[0] as FS.HISFC.Models.Registration.Register;
                if (reg == null || reg.ID == "")
                {
                    MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                    this.txtCardNo.Focus();
                    this.Clear();
                    return;
                }

                this.txtCardNo.Text = cardNo;
                this.SetPatient(reg);

                //�ֽ�ע����Ŀ
                if (al != null)
                {                   
                    this.Query();
                }
                this.txtCardNo.Focus();
                this.txtRecipe.SelectAll();
            }
        }

        private void txtRecipe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCardNo.Text = string.Empty;
                if (this.txtRecipe.Text.Trim() == "")
                {
                    this.txtRecipe.Focus();
                    return;
                }
                this.Query();
                if (this.isUserOrderNumber)
                {
                    this.SetInject();
                }
                this.txtRecipe.Focus();
                this.txtRecipe.SelectAll();
            }
        }

        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            int altKey = Keys.Alt.GetHashCode();

            if (keyData == Keys.F1)
            {
                this.SelectAll(true);
                return true;
            }
            if (keyData == Keys.F2)
            {
                this.SelectAll(false);
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.S.GetHashCode())
            {
                if (this.Save() == 0)
                {
                    this.Print();
                }
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.Q.GetHashCode())
            {
                this.Query();
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.P.GetHashCode())
            {
                //
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            bool isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[row, 0].Value);
            this.SelectedComb(isSelect);
        }

        /// <summary>
        /// ����
        /// {24A47206-F111-4817-A7B4-353C21FC7724} ���߿��ԵǼ�ȫ������ע�䴦��
        /// </summary>
        public class FeeItemListSort : IComparer<FS.HISFC.Models.Fee.Outpatient.FeeItemList>
        {
            public int Compare(FS.HISFC.Models.Fee.Outpatient.FeeItemList x, FS.HISFC.Models.Fee.Outpatient.FeeItemList y)
            {
                //�Ȱ��մ�������
                if (x.RecipeNO != y.RecipeNO)
                {
                    return -y.RecipeNO.CompareTo(x.RecipeNO);
                }
                //��ע��ʱ������
                if (x.User03 != y.User03)
                {
                    string a = x.User03;
                    string b = y.User03;
                    if (a.Length < "12:00".Length)
                    {
                        a = a.PadLeft("12:00".Length, '0');
                    }
                    if (b.Length < "12:00".Length)
                    {
                        b = b.PadLeft("12:00".Length, '0');
                    }
                    return -b.CompareTo(a);
                }

                //����Ϻ�����
                if (x.Order.Combo.ID != y.Order.Combo.ID)
                {
                    return -y.Order.Combo.ID.CompareTo(x.Order.Combo.ID);
                }
                //���������
                if (x.SequenceNO != y.SequenceNO)
                {
                    return -y.SequenceNO.CompareTo(x.SequenceNO);
                }
                //ҩƷ����
                if (x.Item.ID != y.Item.ID)
                {
                    return -y.Item.ID.CompareTo(x.Item.ID);
                }
                return 0;
            }
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {

            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint);
                return types;
            }
        }

        #endregion
    }
}
