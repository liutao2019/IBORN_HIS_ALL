using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Material.Base
{
    public partial class ucPrivePowerReport : FS.SOC.Local.Material.Base.BaseReport
    {
        /// <summary>
        /// ucPrivePowerReport<br></br>
        /// [��������: ucPrivePowerReport����Ȩ�ޱ�������ҩƷ������]<br></br>
        /// [�� �� ��: zengft]<br></br>
        /// [����ʱ��: 2008-9-6]<br></br>
        /// <�޸ļ�¼ 
        ///		�޸���='' 
        ///		�޸�ʱ��='yyyy-mm-dd' 
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
        ///  />
        /// </summary>
        public ucPrivePowerReport()
        {
            InitializeComponent();

            this.ntxtBillNO.KeyPress += new KeyPressEventHandler(ntxtBillNO_KeyPress);
        }


        private string deptPrivCode = string.Empty;



        /// <summary>
        /// ����Ȩ��
        /// </summary>
        [Description("�����������Ƿ�ʹ��Ȩ����"), Category("Prive����Ȩ��"), Browsable(true)]
        public string DeptPrivCode
        {
            get
            {
                return deptPrivCode;
            }
            set
            {
                deptPrivCode = value;
               
            }
        }



        /// <summary>
        /// 
        /// </summary>
        [Category("��չ��Ϣ"), Description("���ع�����˾���߿���"), Browsable(true)]
        public myDeptType ShowTypeName
        {
            get
            {
                if (this.nlblDept.Text.Contains(myDeptType.��˾.ToString()))
                {
                    return myDeptType.��˾;
                }
                else if (this.nlblDept.Text.Contains(myDeptType.����.ToString()))
                {
                    return myDeptType.����;
                }
                else if (this.nlblDept.Text.Contains(myDeptType.��λ.ToString()))
                {
                    return myDeptType.��λ;
                }
                return myDeptType.����;
            }
            set
            {
                this.nlblDept.Text = value.ToString() + "��";
            }
        }


        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitData()
        {
            FS.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic baseLogic = new FS.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic();
            FS.HISFC.BizLogic.Material.BizLogic.Base.CompanyLogic companyLogic = new FS.HISFC.BizLogic.Material.BizLogic.Base.CompanyLogic();
            FS.HISFC.BizLogic.Material.BizLogic.Base.KindLogic kindLogic = new FS.HISFC.BizLogic.Material.BizLogic.Base.KindLogic();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

            try
            {
                List<FS.HISFC.BizLogic.Material.Object.MatBase> alMatBase = baseLogic.QueryBaseList(true);
                System.Collections.ArrayList alObject = new System.Collections.ArrayList();
                foreach (FS.HISFC.BizLogic.Material.Object.MatBase matBase in alMatBase)
                {
                    FS.FrameWork.Models.NeuObject neuObject = matBase as FS.FrameWork.Models.NeuObject;
                    neuObject.ID = matBase.ID;
                    neuObject.Name = matBase.Name; ;
                    neuObject.Memo = matBase.Specs;
                    alObject.Add(neuObject);
                }
                this.ncmbDrug.AddItems(alObject);
                if (ShowTypeName == myDeptType.����)
                {
                    if (string.IsNullOrEmpty(deptPrivCode))
                    {
                        this.ncmbDept.AddItems(deptMgr.GetDeptmentAll());

                    }
                    else 
                    {
                        System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> alTmpDept = this.priPowerMgr.QueryUserPriv(priPowerMgr.Operator.ID, deptPrivCode);
                        if (alTmpDept != null && alTmpDept.Count > 0)
                        {
                            this.ncmbDept.AddItems(alTmpDept);
                            this.ncmbDept.SelectedIndex = 0;
                        }
                    }

                }
                else if (ShowTypeName == myDeptType.��˾)
                {
                    ArrayList alComp = new ArrayList();
                    List<FS.HISFC.BizLogic.Material.Object.MatCompany> alCompany = companyLogic.QueryCompany(FS.HISFC.BizLogic.Material.BizLogic.EnumCompanyType.������˾, true);
                    foreach (FS.HISFC.BizLogic.Material.Object.MatCompany comp in alCompany)
                    {
                        FS.FrameWork.Models.NeuObject neuObject = comp as FS.FrameWork.Models.NeuObject;
                        neuObject.ID = comp.ID;
                        neuObject.Name = comp.Name;
                        alComp.Add(neuObject);
                    }
                    this.ncmbDept.AddItems(alComp);
                }
                else
                {
                    this.nlblDept.Enabled = false;
                    this.ncmbDept.Enabled = false;
                }

                ArrayList alKind = new ArrayList();
                List<FS.HISFC.BizLogic.Material.Object.MatKind> alMatKind = kindLogic.QueryKindList(true);

                foreach (FS.HISFC.BizLogic.Material.Object.MatKind kind in alMatKind)
                {
                    FS.FrameWork.Models.NeuObject neuObject = kind as FS.FrameWork.Models.NeuObject;
                    neuObject.ID = kind.ID;
                    neuObject.Name = kind.Name;
                    alKind.Add(neuObject);
                }

                this.ncmbDrugType.AddItems(alKind);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��ȡ��ѯ����
        /// </summary>
        /// <returns></returns>
        private new string[] GetQueryConditions()
        {
            if (!this.IsUseCustomType)
            {
                if (this.IsDeptAsCondition && this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "", "", "", "", "", "" };
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        parm[0] = this.cmbDept.Tag.ToString();
                    }
                    if (string.IsNullOrEmpty(this.ntxtBillNO.Text))
                    {
                        parm[1] = this.dtStart.Value.ToString();
                        parm[2] = this.dtEnd.Value.ToString();
                    }
                    else
                    {
                        parm[1] = this.dtStart.MinDate.ToString();
                        parm[2] = this.dtEnd.MaxDate.ToString();
                    }
                    parm[3] = this.GetParm()[0];
                    parm[4] = this.GetParm()[1];
                    parm[5] = this.GetParm()[2];
                    parm[6] = this.GetParm()[3];

                    return parm;
                }
                if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "", "", "", "", "" };
                    if (string.IsNullOrEmpty(this.ntxtBillNO.Text))
                    {
                        parm[0] = this.dtStart.Value.ToString();
                        parm[1] = this.dtEnd.Value.ToString();
                    }
                    else
                    {
                        parm[0] = this.dtStart.MinDate.ToString();
                        parm[1] = this.dtEnd.MaxDate.ToString();
                    }
                    parm[2] = this.GetParm()[0];
                    parm[3] = this.GetParm()[1];
                    parm[4] = this.GetParm()[2];
                    parm[5] = this.GetParm()[3];
                    return parm;
                }
                if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "", "", "", "" };
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        parm[0] = this.cmbDept.Tag.ToString();
                    }
                    parm[1] = this.GetParm()[0];
                    parm[2] = this.GetParm()[1];
                    parm[3] = this.GetParm()[2];
                    parm[4] = this.GetParm()[3];
                    return parm;
                }
            }
            else
            {
                if (this.IsDeptAsCondition && this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "", "AAAA", "", "", "", "" };
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        parm[0] = this.cmbDept.Tag.ToString();
                    }
                    if (string.IsNullOrEmpty(this.ntxtBillNO.Text))
                    {
                        parm[1] = this.dtStart.Value.ToString();
                        parm[2] = this.dtEnd.Value.ToString();
                    }
                    else
                    {
                        parm[1] = this.dtStart.MinDate.ToString();
                        parm[2] = this.dtEnd.MaxDate.ToString();
                    }
                    if (this.cmbCustomType.Tag != null && !string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()) && !string.IsNullOrEmpty(this.cmbCustomType.Text.Trim()))
                    {
                        parm[3] = this.cmbCustomType.Tag.ToString();
                    }
                    parm[4] = this.GetParm()[0];
                    parm[5] = this.GetParm()[1];
                    parm[6] = this.GetParm()[2];
                    parm[7] = this.GetParm()[3];

                    return parm;
                }
                if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
                {
                    string[] parm = { "", "", "AAAA", "", "", "", "" };
                    if (string.IsNullOrEmpty(this.ntxtBillNO.Text))
                    {
                        parm[0] = this.dtStart.Value.ToString();
                        parm[1] = this.dtEnd.Value.ToString();
                    }
                    else
                    {
                        parm[0] = this.dtStart.MinDate.ToString();
                        parm[1] = this.dtEnd.MaxDate.ToString();
                    }
                    if (this.cmbCustomType.Tag != null && !string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()) && !string.IsNullOrEmpty(this.cmbCustomType.Text.Trim()))
                    {
                        parm[2] = this.cmbCustomType.Tag.ToString();
                    }
                    parm[3] = this.GetParm()[0];
                    parm[4] = this.GetParm()[1];
                    parm[5] = this.GetParm()[2];
                    parm[6] = this.GetParm()[3];
                    return parm;
                }
                if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
                {
                    string[] parm = { "", "AAAA", "", "", "", "" };
                    if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                    {
                        parm[0] = this.cmbDept.Tag.ToString();
                    }
                    if (this.cmbCustomType.Tag != null && !string.IsNullOrEmpty(this.cmbCustomType.Tag.ToString()) && !string.IsNullOrEmpty(this.cmbCustomType.Text.Trim()))
                    {
                        parm[1] = this.cmbCustomType.Tag.ToString();
                    }
                    parm[2] = this.GetParm()[0];
                    parm[3] = this.GetParm()[1];
                    parm[4] = this.GetParm()[2];
                    parm[5] = this.GetParm()[3];
                    return parm;
                }
            }
            string[] parmNull = { "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null" };
            return parmNull;
        }

        /// <summary>
        /// ��ȡ������ѯ����
        /// </summary>
        /// <returns></returns>
        private string[] GetParm()
        {
            string billNO = this.ntxtBillNO.Text.Trim();
            if (string.IsNullOrEmpty(billNO))
            {
                billNO = "AAAA";
            }

            string drugNO = "AAAA";
            if (this.ncmbDrug.Tag != null && !string.IsNullOrEmpty(this.ncmbDrug.Tag.ToString()) && !string.IsNullOrEmpty(this.ncmbDrug.Text.Trim()))
            {
                drugNO = this.ncmbDrug.Tag.ToString();
            }
            string deptNO = "AAAA";
            if (this.ncmbDept.Tag != null && !string.IsNullOrEmpty(this.ncmbDept.Tag.ToString()) && !string.IsNullOrEmpty(this.ncmbDept.Text.Trim()))
            {
                deptNO = this.ncmbDept.Tag.ToString();
            }
            string drugType = "AAAA";
            if (this.ncmbDrugType.Tag != null && !string.IsNullOrEmpty(this.ncmbDrugType.Tag.ToString()) && !string.IsNullOrEmpty(this.ncmbDrugType.Text.Trim()))
            {
                drugType = this.ncmbDrugType.Tag.ToString();
            }

            return new string[] { billNO, drugNO, deptNO, drugType };
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = this.GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            return base.Print(sender, neuObject);
        }

        /// <summary>
        /// ���ش���
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //�����ʱ����ѯ
            if (DesignMode)
            {
                return;
            }
            this.QueryDataWhenInit = false;

            this.InitData();
            base.OnLoad(e);
            if (this.cmbDept.alItems != null && this.cmbDept.alItems.Count > 0)
            {
                this.cmbDept.SelectedIndex = 0;
                this.cmbDept.Tag = (this.cmbDept.alItems[0] as FS.FrameWork.Models.NeuObject).ID;
            }

        }

        /// <summary>
        /// ���ð�ť�����ò�ѯʱ��Ϊ�½�ʱ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int SetPrint(object sender, object neuObject)
        {
            return base.SetPrint(sender, neuObject);
        }

        void ntxtBillNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.ncmbDrug.Select();
                this.ncmbDrug.Focus();
            }
        }

        /// <summary>
        /// ��˾����
        /// </summary>
        public enum myDeptType
        {
            ����,
            ��˾,
            ��λ,
            ����
        }
    }
}
