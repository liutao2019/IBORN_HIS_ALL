using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucDiagnose : UserControl
    {
        #region ˽�б���

        private DiaDetail mainDiag;

        private DiaDetail diag1;

        private DiaDetail diag2;

        private SpecDiagnose specDiag;
        /// <summary>
        /// ICD����
        /// </summary>
        private ArrayList arrICD;

        /// <summary>
        /// ����״̬
        /// </summary>
        private ArrayList arrCure;

        /// <summary>
        /// ICD������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase icdMgr;

        /// <summary>
        /// ��ϲ�����Ϣ����
        /// </summary>
        private BaseManage baseMange;

        /// <summary>
        /// �Ƿ�������������
        /// </summary>
        private bool isLoadICD = false;

        #endregion 

        #region ��������
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public DiaDetail MainDiag
        {
            get
            {
                GetMainDiag();
                return mainDiag;
            }
            set
            {
                mainDiag = value;
            }
        }

        public DiaDetail Diag1
        {
            get
            {
                GetOtherDiag1();
                return diag1;
            }
            set
            {
                diag1 = value;
            }
        }

        public DiaDetail Diag2
        {
            get
            {
                GetOtherDiag2();
                return diag2;
            }
            set
            {
                diag2 = value;
            }
        }

        public SpecDiagnose SpecDiag
        {
            get
            {
                GetMainDiag();
                GetOtherDiag1();
                GetOtherDiag2();
                return specDiag;
            }
            set
            {
                specDiag = value;
            }
        }
        #endregion      
        

        #region ���췽��
        public ucDiagnose()
        {
            InitializeComponent();
            arrICD = new ArrayList();
            arrCure = new ArrayList();
            mainDiag = new DiaDetail();
            diag1 = new DiaDetail();
            diag2 = new DiaDetail();
            specDiag = new SpecDiagnose();
            baseMange = new BaseManage();
            icdMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
        }
        #endregion

        #region ˽�з���
        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        private void SetMainDiag()
        {
            cmbMainDiag.Text = mainDiag.IcdName;
            cmbMainDiag.Tag = mainDiag.Icd;
            cmbMainMod.Tag = mainDiag.Mod;
            cmbMainMod.Text = mainDiag.ModName;
            txtP.Text = mainDiag.P_Code;
            txtN.Text = mainDiag.N_Code;
            txtM.Text = mainDiag.M_Code;
            txtT.Text = mainDiag.T_Code;
        }

        private void SetOtherDiag1()
        {
            cmbMainDiag1.Text = diag1.IcdName;
            cmbMainDiag1.Tag = diag1.Icd;
            cmbDiagMod1.Tag = diag1.Mod;
            cmbDiagMod1.Text = diag1.ModName;
            txtP1.Text = diag1.P_Code;
            txtN1.Text = diag1.N_Code;
            txtM1.Text = diag1.M_Code;
            txtT1.Text = diag1.T_Code;
        }

        private void SetOtherDiag2()
        {
            cmbMainDiag2.Text = diag2.IcdName;
            cmbMainDiag2.Tag = diag2.Icd;
            cmbDiagMod2.Tag = diag2.Mod;
            cmbDiagMod2.Text = diag2.ModName;
            txtP2.Text = diag2.P_Code;
            txtN2.Text = diag2.N_Code;
            txtM2.Text = diag2.M_Code;
            txtT2.Text = diag2.T_Code;
        }
        #endregion 

        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        private void GetMainDiag()
        {
            try
            {
                mainDiag.IcdName = cmbMainDiag.Text;
                if (cmbMainDiag.Tag != null)
                {
                    mainDiag.Icd = cmbMainDiag.Tag.ToString();
                }
                if (cmbMainMod.Tag != null)
                {
                    mainDiag.Mod = cmbMainMod.Tag.ToString();
                }
                mainDiag.ModName = cmbMainMod.Text;
                mainDiag.P_Code = txtP.Text;
                mainDiag.N_Code = txtN.Text;
                mainDiag.M_Code = txtM.Text;
                mainDiag.T_Code = txtT.Text;
                specDiag.Diag = mainDiag;
                if (txtCure.Tag != null && txtCure.Text.Trim() != "")
                {
                    specDiag.Main_DiagState = this.txtCure.Tag.ToString();
                }
            }
            catch
            { }
        }

        private void GetOtherDiag1()
        {
            diag1.IcdName = cmbMainDiag1.Text;
            diag1.Icd = cmbMainDiag1.Tag.ToString();
            diag1.ModName = cmbDiagMod1.Text;
            diag1.Mod = cmbDiagMod1.Tag.ToString();
            diag1.P_Code = txtP1.Text;
            diag1.N_Code = txtN1.Text;
            diag1.M_Code = txtM1.Text;
            diag1.T_Code= txtT1.Text;
            specDiag.Diag1 = diag1;
        }

        private void GetOtherDiag2()
        {
            diag2.IcdName = cmbMainDiag2.Text;
            diag2.Icd = cmbMainDiag2.Tag.ToString();
            diag2.Mod = cmbDiagMod2.Tag.ToString();
            diag2.ModName = cmbDiagMod2.Text;
            diag2.P_Code = txtP2.Text;
            diag2.N_Code = txtN2.Text;
            diag2.M_Code = txtM2.Text;
            diag2.T_Code = txtT2.Text;
            specDiag.Diag2 = diag1;
        }

        public void ClearContent()
        {
            cmbMainDiag.Text = "";
            cmbMainDiag.Tag = null;
            cmbMainMod.Tag = null;
            cmbMainMod.Text = null;
            txtP.Text = "";
            txtN.Text = "";
            txtM.Text = "";
            txtT.Text = "";

            cmbMainDiag1.Text = "";
            cmbMainDiag1.Tag = null;
            cmbDiagMod1.Tag = null;
            cmbDiagMod1.Text = "";
            txtP1.Text = "";
            txtN1.Text = "";
            txtM1.Text = "";
            txtT1.Text = "";

            cmbMainDiag2.Text = "";
            cmbMainDiag2.Tag = null;
            cmbDiagMod2.Tag = null;
            cmbDiagMod2.Text = "";
            txtP2.Text = "";
            txtN2.Text = "";
            txtM2.Text = "";
            txtT2.Text = "";
        }

        /// <summary>
        /// �󶨿���
        /// </summary>
        /// <param name="arrDept">�����б�</param>
        /// <param name="tag">���ҵ�tag</param>
        //public void DeptBinding(ArrayList arrDept, string tag)
        //{
        //    this.cmbDept.AddItems(arrDept);
        //    this.cmbDept.Tag = tag;
        //}
 

        /// <summary>
        /// ͨ��ȡ�õ�base��Ϣ������ҳ��
        /// </summary>
        /// <param name="specBase"></param>
        /// <param name="inDeptNo"></param>
        /// <param name="medGrp"></param>
        public void GetDiagnoseInfo(SpecDiagnose diagnose)
        {
            this.MainDiag = diagnose.Diag;
            this.Diag1 = diagnose.Diag1;
            this.Diag2 = diagnose.Diag2;
            this.SpecDiag = diagnose;
            SetMainDiag();
            SetOtherDiag1();
            SetOtherDiag2();           
        }
        #region �¼�
        private void ucDiagnose_Load(object sender, EventArgs e)
        {
            arrICD = icdMgr.ICDQuery(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            arrCure = con.GetList(FS.HISFC.Models.Base.EnumConstant.ZG);            
            
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FS.HISFC.BizLogic.Manager.MedicareGroup medGrp = new FS.HISFC.Management.Manager.MedicareGroup();
            //ArrayList arrGrp = new ArrayList();
            //medGrp.QueryMedicareGroupByDept(ref arrGrp, cmbDept.Tag.ToString());
            //cmbGrp.AddItems(arrGrp);
        }
        #endregion 

        private void rbtInput_CheckedChanged(object sender, EventArgs e)
        {

            if (!isLoadICD)
            {
                ///�������          ;
                this.txtCure.AddItems(arrCure);
                cmbMainDiag.AddItems(arrICD);
                cmbMainMod.AddItems(arrICD);
                cmbMainDiag1.AddItems(arrICD);
                cmbDiagMod1.AddItems(arrICD);
                cmbMainDiag2.AddItems(arrICD);
                cmbDiagMod2.AddItems(arrICD);
                isLoadICD = true;
            }
        }

        public bool IsInputDiagnose()
        {
            if (cmbDiagMod1.Text != "" || cmbDiagMod2.Text != "" || cmbMainMod.Text != "" || cmbMainDiag.Text != "" || cmbMainDiag1.Text != "" || cmbMainDiag2.Text != "")
            {
                return true;
            }
            else
                return false;
        }

    }

    #region �ڲ�������
    /// <summary>
    /// �����Ϣ
    /// </summary>
    public class DiagnoseInfo
    {
        public string inDeptNo = "";
        public string medGrp = "";
        public string mainDoc = "";
        public string cureInfo = "";
        public string clinicDiag = "";
        public string clinicDiagName = "";
        public string inDiag = "";
        public string inDiagName = "";
        public string outDiag = "";
        public string outDiagName = "";
        public string mainDiag = "";
        public string mainDiagName = "";
        public string mainDiag1 = "";
        public string mainDiag1Name = "";
        public string mainDiag2 = "";
        public string mainDiag2Name = "";
        public string posCode = "";
        public string posName = "";
    }
    #endregion
}
