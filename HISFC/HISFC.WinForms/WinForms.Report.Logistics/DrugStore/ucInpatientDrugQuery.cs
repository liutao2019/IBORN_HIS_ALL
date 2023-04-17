using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.Logistics.DrugStore
{
    /// <summary>
    /// [ģ������: סԺ��ҩ��ѯ]
    /// [�� �� ��: ��ú�]
    /// [����ʱ��: 2009-7-2]
    /// [��ע˵��: ҵ��㷽���Լ���ѯ��䶼���ϵ��˱��ؼ���]
    /// </summary>
    public partial class ucInpatientDrugQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {        
        public ucInpatientDrugQuery()
        {
            InitializeComponent();
        }

        #region ����

      
        /// <summary>
        /// ������Ϣ����ҵ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase diagnoseManager = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();

        private FS.HISFC.BizProcess.Integrate.RADT rdManager = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.HISFC.BizLogic.Pharmacy.DrugStore dsManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        private string popedomOpers = "";

        DrugQueryClass drugManager = new DrugQueryClass();

        /// <summary>
        /// ��ҩ����
        /// </summary>
        private string drugType = "";

        /// <summary>
        /// �˷�ҽ���������ҩ������
        /// </summary>
        private string orderTypeCompare = "or (t.billclass_code='R' and decode((select s.type_code from met_ipm_order s where s.mo_order=t.mo_order),'CF','090630150926','CZ','090623094701','LZ','090623094730','CD','090623094745','BL','090623094917','SQ','090623094904','') = '090623094745' )";

        #endregion

        #region ����

        /// <summary>
        /// �鿴����ҩ����Ȩ���б�
        /// </summary>
        [Description("�鿴����ҩ��Ȩ������"), Category("Ȩ������"), DefaultValue("")]
        public string PopedomOpers
        {
            get 
            {
                return popedomOpers;
            }
            set 
            {
                popedomOpers = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ���ؼ���Ϣ
        /// </summary>
        public void InitControls()
        {
            ArrayList alTemp = new ArrayList();
            ArrayList al = managerIntegrate.GetDeptmentByType("P");
            string currentOper = dsManager.Operator.ID;
            string currentDept = ((FS.HISFC.Models.Base.Employee)(dsManager.Operator)).Dept.ID;
            for (int i = 0; i < al.Count; i++)
            {
                FS.FrameWork.Models.NeuObject deptObj = al[i] as FS.FrameWork.Models.NeuObject;
                if (PopedomOpers.Contains(currentOper))
                {
                    alTemp.Add(deptObj);
                }
                else if (deptObj.ID == currentDept)
                {
                    alTemp.Add(deptObj);
                }
            }
            this.cbbDept.ClearItems();
            this.cbbDept.AddItems(alTemp);

            alTemp = new ArrayList();
            alTemp = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            alTemp.AddRange(managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP));
            alTemp.AddRange(managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.U));
            alTemp.AddRange(managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.T));
            FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
            tempObj.ID = "ALL";
            tempObj.Name = "ȫ������";
            alTemp.Insert(0, tempObj);
            this.cbbGetDrugDept.AddItems(alTemp);
            this.cbbGetDrugDept.Tag = "ALL";

            alTemp = new ArrayList();
            alTemp = dsManager.QueryDrugBillClassList();
            tempObj = new FS.FrameWork.Models.NeuObject();
            tempObj.ID = "ALL";
            tempObj.Name = "ȫ����ҩ��";
            alTemp.Insert(0, tempObj);
            this.cbbBillType1.AddItems(alTemp);
            this.cbbBillType1.Tag = "ALL";

            this.pnlDefine1.Height = 0;
            this.pnlDefine2.Height = 0;
            this.pnlDefine3.Height = 0;
        }


        /// <summary>
        /// ���÷�ҩ����
        /// </summary>
        private void SetDrugType()
        {
            if (this.rbtSend.Checked)
            {
                this.drugType = "Z1";
                this.chkSum.Enabled = false;
                //this.chkSum.Checked = false;
            }

            if (this.rbtQuit.Checked)
            {
                this.drugType = "Z2";
                this.chkSum.Enabled = false;
                //this.chkSum.Checked = false;
            }

            if (this.rbtAll.Checked)
            {
                this.drugType = "Z1','Z2";
                if (this.neuTabControl1.SelectedIndex == 2)
                {
                    this.chkSum.Enabled = false;
                }
                else
                {
                    this.chkSum.Enabled = true;
                }
                
                //this.chkSum.Checked = false;
            }
        }

        /// <summary>
        /// ��ҵ���ѯ��ҩ��Ϣ
        /// </summary>
        private void QueryType()
        {
            this.neuLabel12.Text = "��ҩҩ����" + this.cbbDept.Text;
            this.neuLabel11.Text = "��ӡʱ�䣺" + this.dtpStart.Value.ToString() + " --- " + this.dtpEnd.Value.ToString();
            string strSql = "";
            if (this.cbbBillType1.Tag.ToString() == "ALL")
            {
                if (this.rbtMinUnit.Checked)
                {
                    if (this.chkSum.Checked)
                    {
                        strSql = "select s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(t.out_num) as ��������,s.min_unit as ��λ,round(sum(t.out_num*s.retail_price/s.pack_qty),2) as �ܽ��,'' as ����,s.spell_code as ƴ����"
                             + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                             + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                             + " group by s.regular_name,s.trade_name,s.specs,s.min_unit,s.retail_price,s.pack_qty,s.spell_code"
                             + " order by s.regular_name";
                    }
                    else
                    {
                        strSql = "select s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(t.out_num) as ��������,s.min_unit as ��λ,round(sum(t.out_num*s.retail_price/s.pack_qty),2) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " group by s.regular_name,s.trade_name,s.specs,s.min_unit,s.retail_price,s.pack_qty,t.out_type,s.spell_code"
                               + " order by s.regular_name";
                    }
                }
                else
                {
                    if (this.chkSum.Checked)
                    {
                        strSql = "select s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(t.out_num)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(t.out_num*s.retail_price/s.pack_qty),2) as �ܽ��,'' as ����,s.spell_code as ƴ����"
                             + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                             + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                             + " group by s.regular_name,s.trade_name,s.specs,s.pack_unit,s.retail_price,s.pack_qty,s.spell_code"
                             + " order by s.regular_name";
                    }
                    else
                    {
                        strSql = "select s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(t.out_num)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(t.out_num*s.retail_price/s.pack_qty),2) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " group by s.regular_name,s.trade_name,s.specs,s.pack_unit,s.retail_price,s.pack_qty,t.out_type,s.spell_code"
                               + " order by s.regular_name";
                    }
                }
            }
            else ////////////
            {
                if (this.rbtMinUnit.Checked)
                {
                    if (this.chkSum.Checked)
                    {
                        strSql = "select s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days) as ��������,s.min_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),2) as �ܽ��,'' as ����,s.spell_code as ƴ����"
                             + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                             + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                             + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                             + " group by s.regular_name,s.trade_name,s.specs,s.min_unit,s.retail_price,s.pack_qty,s.spell_code"
                             + " order by s.regular_name";
                    }
                    else
                    {
                        strSql = "select s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days) as ��������,s.min_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),2) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " group by s.regular_name,s.trade_name,s.specs,s.min_unit,s.retail_price,s.pack_qty,t.class3_meaning_code,s.spell_code"
                               + " order by s.regular_name";
                    }
                }
                else
                {
                    if (this.chkSum.Checked)
                    {
                        strSql = "select s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),2) as �ܽ��,'' as ����,s.spell_code as ƴ����"
                             + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                             + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                             + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                             + " group by s.regular_name,s.trade_name,s.specs,s.pack_unit,s.retail_price,s.pack_qty,s.spell_code"
                             + " order by s.regular_name";
                    }
                    else
                    {
                        strSql = "select s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),2) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " group by s.regular_name,s.trade_name,s.specs,s.pack_unit,s.retail_price,s.pack_qty,t.class3_meaning_code,s.spell_code"
                               + " order by s.regular_name";
                    }
                }
            }


            DataTable dt = new DataTable();
            dt = drugManager.QueryInPharmacyDrug(strSql);
            DataView dvDrugList = new DataView(dt);
            this.fpType.DataSource = dvDrugList;
            this.fpType.Columns[0].Width = 160;
            this.fpType.Columns[1].Width = 70;
            this.fpType.Columns[2].Width = 70;
            this.fpType.Columns[3].Width = 70;
            this.fpType.Columns[4].Width = 50;
            this.fpType.Columns[5].Width = 90;
            this.fpType.Columns[6].Width = 35;
            this.fpType.Columns[7].Visible = false;
        }

        /// <summary>
        /// �����Ҳ�ѯ��ҩ��Ϣ
        /// </summary>
        private void QueryDept()
        {
            this.neuLabel9.Text = "��ҩҩ����" + this.cbbDept.Text;
            this.neuLabel8.Text = "��ӡʱ�䣺" + this.dtpStart.Value.ToString() + " --- " + this.dtpEnd.Value.ToString();
            string strSql = "";
            if (this.cbbBillType1.Tag.ToString() == "ALL")
            {
                if (this.rbtMinUnit.Checked)
                {
                    if (!this.chkSum.Checked)
                    {
                        if (this.cbbGetDrugDept.Tag.ToString() == "ALL")
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(t.out_num) as ��������,s.min_unit as ��λ,round(sum(t.out_num*s.retail_price/s.pack_qty),4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                                    + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " group by t.drug_storage_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.min_unit,t.out_type,s.spell_code"
                                    + " order by t.drug_storage_code,s.regular_name";
                        }
                        else
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(t.out_num) as ��������,s.min_unit as ��λ,round(sum(t.out_num*s.retail_price/s.pack_qty),4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                                    + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and t.drug_storage_code='" + this.cbbGetDrugDept.Tag.ToString() + "' "
                                    + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " group by t.drug_storage_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.min_unit,t.out_type,s.spell_code"
                                    + " order by t.drug_storage_code,s.regular_name";
                        }
                    }
                    else
                    {
                        if (this.cbbGetDrugDept.Tag.ToString() == "ALL")
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(t.out_num) as ��������,s.min_unit as ��λ,round(sum(t.out_num*s.retail_price/s.pack_qty),4) as �ܽ��,s.spell_code as ƴ����"
                                    + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " group by t.drug_storage_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.min_unit,s.spell_code"
                                    + " order by t.drug_storage_code,s.regular_name";
                        }
                        else
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(t.out_num) as ��������,s.min_unit as ��λ,round(sum(t.out_num*s.retail_price/s.pack_qty),4) as �ܽ��,s.spell_code as ƴ����"
                                    + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and t.drug_storage_code='" + this.cbbGetDrugDept.Tag.ToString() + "' "
                                    + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " group by t.drug_storage_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.min_unit,s.spell_code"
                                    + " order by t.drug_storage_code,s.regular_name";
                        }
                    }
                }
                else
                {
                    if (!this.chkSum.Checked)
                    {
                        if (this.cbbGetDrugDept.Tag.ToString() == "ALL")
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(t.out_num)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(t.out_num)*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                                    + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " group by t.drug_storage_code,s.regular_name,s.trade_name,s.specs,s.pack_unit,s.retail_price,s.pack_qty,t.out_type,s.spell_code"
                                    + " order by t.drug_storage_code,s.regular_name";
                        }
                        else
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(t.out_num)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(t.out_num)*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                                    + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and t.drug_storage_code='" + this.cbbGetDrugDept.Tag.ToString() + "' "
                                    + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " group by t.drug_storage_code,s.regular_name,s.trade_name,s.specs,s.pack_unit,s.retail_price,s.pack_qty,t.out_type,s.spell_code"
                                    + " order by t.drug_storage_code,s.regular_name";
                        }
                    }
                    else
                    {
                        if (this.cbbGetDrugDept.Tag.ToString() == "ALL")
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(t.out_num)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(t.out_num)*s.retail_price/s.pack_qty,4) as �ܽ��,s.spell_code as ƴ����"
                                    + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " group by t.drug_storage_code,s.regular_name,s.trade_name,s.specs,s.pack_unit,s.retail_price,s.pack_qty,s.spell_code"
                                    + " order by t.drug_storage_code,s.regular_name";
                        }
                        else
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(t.out_num)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(t.out_num)*s.retail_price/s.pack_qty,4) as �ܽ��,s.spell_code as ƴ����"
                                    + " from pha_com_output t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and t.drug_storage_code='" + this.cbbGetDrugDept.Tag.ToString() + "' "
                                    + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " group by t.drug_storage_code,s.regular_name,s.trade_name,s.specs,s.pack_unit,s.retail_price,s.pack_qty,s.spell_code"
                                    + " order by t.drug_storage_code,s.regular_name";
                        }
                    }
                }
            }/////////////////////
            else
            {
                if (this.rbtMinUnit.Checked)
                {
                    if (!this.chkSum.Checked)
                    {
                        if (this.cbbGetDrugDept.Tag.ToString() == "ALL")
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days) as ��������,s.min_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                                    + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                                    + " group by t.dept_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.min_unit,t.class3_meaning_code,s.spell_code"
                                    + " order by t.dept_code,s.regular_name";
                        }
                        else
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days) as ��������,s.min_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),4) as �ܽ��,s.spell_code as ƴ����"
                                    + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " and t.valid_state='1'  and t.dept_code='" + this.cbbGetDrugDept.Tag.ToString() + "' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                                    + " group by t.dept_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.min_unit,s.spell_code"
                                    + " order by t.dept_code,s.regular_name";
                        }
                    }
                    else
                    {
                        if (this.cbbGetDrugDept.Tag.ToString() == "ALL")
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days) as ��������,s.min_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),4) as �ܽ��,s.spell_code as ƴ����"
                                    + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                                    + " group by t.dept_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.min_unit,s.spell_code"
                                    + " order by t.dept_code,s.regular_name";
                        }
                        else
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days) as ��������,s.min_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                                    + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " and t.valid_state='1'  and t.dept_code='" + this.cbbGetDrugDept.Tag.ToString() + "' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                                    + " group by t.dept_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.min_unit,t.class3_meaning_code,s.spell_code"
                                    + " order by t.dept_code,s.regular_name";
                        }
                    }
                }
                else
                {
                    if (!this.chkSum.Checked)
                    {
                        if (this.cbbGetDrugDept.Tag.ToString() == "ALL")
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                                    + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                                    + " group by t.dept_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.pack_unit,t.class3_meaning_code,s.spell_code"
                                    + " order by t.dept_code,s.regular_name";
                        }
                        else
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,s.spell_code as ƴ����"
                                    + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ") and t.dept_code='" + this.cbbGetDrugDept.Tag.ToString() + "'"
                                    + " group by t.dept_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.pack_unit,t.class3_meaning_code,s.spell_code"
                                    + " order by t.dept_code,s.regular_name";
                        }
                    }
                    else
                    {
                        if (this.cbbGetDrugDept.Tag.ToString() == "ALL")
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),4) as �ܽ��,s.spell_code as ƴ����"
                                    + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                                    + " group by t.dept_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.pack_unit,s.spell_code"
                                    + " order by t.dept_code,s.regular_name";
                        }
                        else
                        {
                            strSql = "select (select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                                    + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days)/s.pack_qty,4) as ��������,s.pack_unit as ��λ,round(sum(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty),4) as �ܽ��,s.spell_code as ƴ����"
                                    + " from pha_com_applyout t,pha_com_baseinfo s where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' "
                                    + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                                    + " and t.valid_state='1' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ") and t.dept_code='" + this.cbbGetDrugDept.Tag.ToString() + "'"
                                    + " group by t.dept_code,s.regular_name,s.trade_name,s.specs,s.retail_price,s.pack_qty,s.pack_unit,s.spell_code"
                                    + " order by t.dept_code,s.regular_name";

                        }
                    }
                }
            }


            DataTable dt = new DataTable();
            dt = drugManager.QueryInPharmacyDrug(strSql);
            DataView dvDrugList = new DataView(dt);
            this.fpDept.DataSource = dvDrugList;
            this.fpDept.Columns[0].Width = 120;
            this.fpDept.Columns[1].Width = 160;
            this.fpDept.Columns[2].Width = 70;
            this.fpDept.Columns[3].Width = 70;
            this.fpDept.Columns[4].Width = 60;
            this.fpDept.Columns[5].Width = 35;
            this.fpDept.Columns[6].Width = 90;
            this.fpDept.Columns[7].Width = 35;
            if (!this.chkSum.Checked)
            {
                this.fpDept.Columns[8].Width = 35;
                this.fpDept.Columns[8].Visible = false;
            }
            else
            {
                this.fpDept.Columns[7].Visible = false;
            }
        }

        /// <summary>
        /// �����߲�ѯ��ҩ��Ϣ {ADD410D5-090B-4711-A4B7-635D4BE57F9C} ��ʾ������ ������֤�ź�����ֶ�  shi.x  2009-09-15 
        /// </summary> 
        private void QueryPatient()
        {
            this.lbDept.Text = "��ҩҩ����" + this.cbbDept.Text;
            this.lbDate.Text = "��ӡʱ�䣺" + this.dtpStart.Value.ToString() + " --- " + this.dtpEnd.Value.ToString();
            string strSql = "";

            if (this.cbbBillType1.Tag.ToString() == "ALL")
            {
                if (this.rbtMinUnit.Checked)
                {
                    if (this.rbtCaseNO.Checked)
                    {
                        /*strSql = "select t.druged_bill as ��ҩ����,p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,t.out_num as ��������,s.min_unit as ��λ,"
                               + " round(t.out_num*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,t.exam_date as ����ʱ��,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.get_person=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.patient_no like '%" + this.tbFilter.Text.Trim() + "%'"
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";*/
                        strSql = "select p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,t.out_num as ��������,s.min_unit as ��λ,"
                               + " round(t.out_num*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,t.exam_date as ����ʱ��,t.druged_bill as ��ҩ����,p.idenno as ���֤��,p.diag_name as �������,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.get_person=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.patient_no like '%" + this.tbFilter.Text.Trim() + "%'"
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";
                    }

                    if (this.rbtName.Checked)
                    {
                        /*strSql = "select t.druged_bill as ��ҩ����,p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,t.out_num as ��������,s.min_unit as ��λ,"
                               + " round(t.out_num*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,t.exam_date as ����ʱ��,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.get_person=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.name like '%" + this.tbFilter.Text.Trim() + "%'"
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";*/
                        strSql = "select p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,t.out_num as ��������,s.min_unit as ��λ,"
                               + " round(t.out_num*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,t.exam_date as ����ʱ��,t.druged_bill as ��ҩ����,p.idenno as ���֤��,p.diag_name as �������,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.get_person=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.name like '%" + this.tbFilter.Text.Trim() + "%'"
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";
                    }
                }
                else
                {
                    if (this.rbtCaseNO.Checked)
                    {
                       /* strSql = "select t.druged_bill as ��ҩ����,p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(t.out_num/s.pack_qty,4) as ��������,s.pack_unit as ��λ,"
                               + " round(t.out_num*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,t.exam_date as ����ʱ��,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.get_person=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.patient_no like '%" + this.tbFilter.Text.Trim() + "%'"
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";*/
                        strSql = "select p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(t.out_num/s.pack_qty,4) as ��������,s.pack_unit as ��λ,"
                               + " round(t.out_num*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,t.exam_date as ����ʱ��,t.druged_bill as ��ҩ����,p.idenno as ���֤��,p.diag_name as �������,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.get_person=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.patient_no like '%" + this.tbFilter.Text.Trim() + "%'"
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";
                    }

                    if (this.rbtName.Checked)
                    {
                       /* strSql = "select t.druged_bill as ��ҩ����,p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(t.out_num/s.pack_qty,4) as ��������,s.pack_unit as ��λ,"
                               + " round(t.out_num*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,t.exam_date as ����ʱ��,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.get_person=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.name like '%" + this.tbFilter.Text.Trim() + "%'"
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";*/
                        strSql = "select p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.drug_storage_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(t.out_num/s.pack_qty,4) as ��������,s.pack_unit as ��λ,"
                               + " round(t.out_num*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.out_type,'Z1','��ҩ','��ҩ') as ����,t.exam_date as ����ʱ��,t.druged_bill as ��ҩ����,p.idenno as ���֤��,p.diag_name as �������,s.spell_code as ƴ����"
                               + " from pha_com_output t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.out_type in ('" + this.drugType + "') and t.get_person=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.name like '%" + this.tbFilter.Text.Trim() + "%'"
                               + " and t.exam_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.exam_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";
                    }
                }
            }////////
            else
            {
                if (this.rbtMinUnit.Checked)
                {
                    if (this.rbtCaseNO.Checked)
                    {
                        /*strSql = "select t.druged_bill as ��ҩ����,p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days,4) as ��������,s.min_unit as ��λ,"
                               + " round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,t.print_date as ����ʱ��,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.patient_id=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.patient_no like '%" + this.tbFilter.Text.Trim() + "%' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";*/
                        strSql = "select p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                              + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days,4) as ��������,s.min_unit as ��λ,"
                              + " round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,t.print_date as ����ʱ��,t.druged_bill as ��ҩ����,p.idenno as ���֤��,p.diag_name as �������,s.spell_code as ƴ����"
                              + " from pha_com_applyout t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.patient_id=p.inpatient_no"
                              + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.patient_no like '%" + this.tbFilter.Text.Trim() + "%' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                              + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                              + " order by s.regular_name";
                    }

                    if (this.rbtName.Checked)
                    {
                        /*strSql = "select t.druged_bill as ��ҩ����,p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days,4) as ��������,s.min_unit as ��λ,"
                               + " round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,t.print_date as ����ʱ��,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.patient_id=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.name like '%" + this.tbFilter.Text.Trim() + "%' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";*/
                        strSql = "select p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,round(s.retail_price/s.pack_qty,4) as ����,round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days,4) as ��������,s.min_unit as ��λ,"
                               + " round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,t.print_date as ����ʱ��,t.druged_bill as ��ҩ����,p.idenno as ���֤��,p.diag_name as �������,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.patient_id=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.name like '%" + this.tbFilter.Text.Trim() + "%' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";
                    }
                }
                else
                {
                    if (this.rbtCaseNO.Checked)
                    {
                        /*strSql = "select t.druged_bill as ��ҩ����,p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days/s.pack_qty,4) as ��������,s.pack_unit as ��λ,"
                               + " round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,t.print_date as ����ʱ��,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.patient_id=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.patient_no like '%" + this.tbFilter.Text.Trim() + "%' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";*/
                        strSql = "select p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,"
                               + " s.retail_price as ����,round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days/s.pack_qty,4) as ��������,s.pack_unit as ��λ,"
                               + " round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,t.print_date as ����ʱ��,t.druged_bill as ��ҩ����,p.idenno as ���֤��,p.diag_name as �������,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.patient_id=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.patient_no like '%" + this.tbFilter.Text.Trim() + "%' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";
                    }

                    if (this.rbtName.Checked)
                    {
                        /*strSql = "select t.druged_bill as ��ҩ����,p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,"
                               + " s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,s.retail_price as ����,round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days/s.pack_qty,4) as ��������,s.pack_unit as ��λ,"
                               + " round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,t.print_date as ����ʱ��,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.patient_id=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.name like '%" + this.tbFilter.Text.Trim() + "%' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";*/
                        strSql = "select p.patient_no as סԺ��,p.name as ��������,(select dept_name from com_department where dept_code=t.dept_code) as ��ҩ����,s.regular_name||'('||s.trade_name||')' as ҩƷ����,s.specs as ���,"
                               + " s.retail_price as ����,round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days/s.pack_qty,4) as ��������,s.pack_unit as ��λ,"
                               + " round(decode(t.class3_meaning_code,'Z1',t.apply_num,-t.apply_num)*t.days*s.retail_price/s.pack_qty,4) as �ܽ��,decode(t.class3_meaning_code,'Z1','��ҩ','��ҩ') as ����,t.print_date as ����ʱ��,t.druged_bill as ��ҩ����,p.idenno as ���֤��,p.diag_name as �������,s.spell_code as ƴ����"
                               + " from pha_com_applyout t,pha_com_baseinfo s,fin_ipr_inmaininfo p where t.drug_code=s.drug_code and t.class3_meaning_code in ('" + this.drugType + "') and t.patient_id=p.inpatient_no"
                               + " and t.drug_dept_code='" + this.cbbDept.Tag.ToString() + "' and p.name like '%" + this.tbFilter.Text.Trim() + "%' and (t.billclass_code = '" + this.cbbBillType1.Tag.ToString() + "'" + orderTypeCompare + ")"
                               + " and t.print_date>=to_date('" + this.dtpStart.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') and t.print_date<to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss')"
                               + " order by s.regular_name";
                    }
                }
            }



            DataTable dt = new DataTable();
            dt = drugManager.QueryInPharmacyDrug(strSql);
            DataView dvDrugList = new DataView(dt);
            this.fpDrugedList.DataSource = dvDrugList;
            /*this.fpDrugedList.Columns[0].Width = 60;
            this.fpDrugedList.Columns[1].Width = 70;
            this.fpDrugedList.Columns[2].Width = 60;
            this.fpDrugedList.Columns[3].Width = 120;
            this.fpDrugedList.Columns[4].Width = 160;
            this.fpDrugedList.Columns[5].Width = 60;
            this.fpDrugedList.Columns[6].Width = 60;
            this.fpDrugedList.Columns[7].Width = 70;
            this.fpDrugedList.Columns[8].Width = 35;
            this.fpDrugedList.Columns[9].Width = 90;
            this.fpDrugedList.Columns[10].Width = 35;
            this.fpDrugedList.Columns[11].Width = 70;
            this.fpDrugedList.Columns[12].Visible = false;*/

            this.fpDrugedList.Columns[0].Width = 70;
            this.fpDrugedList.Columns[1].Width = 100;
            this.fpDrugedList.Columns[2].Width = 120;
            this.fpDrugedList.Columns[3].Width = 120;
            this.fpDrugedList.Columns[4].Width = 70;
            this.fpDrugedList.Columns[5].Width = 70;
            this.fpDrugedList.Columns[6].Width = 70;
            this.fpDrugedList.Columns[7].Width = 70;
            this.fpDrugedList.Columns[8].Width = 50;
            this.fpDrugedList.Columns[9].Width = 70;
            this.fpDrugedList.Columns[10].Width = 100;
            this.fpDrugedList.Columns[11].Width = 60;
            this.fpDrugedList.Columns[12].Width = 120;
            this.fpDrugedList.Columns[13].Width = 160;
            this.fpDrugedList.Columns[14].Visible = false;
        }

        #endregion

        #region �¼�

        protected override void OnLoad(EventArgs e)
        {
            this.dtpStart.Value = DateTime.Today.Date;
            this.dtpEnd.Value = DateTime.Today.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            this.InitControls();
            this.cbbDept.Tag = ((FS.HISFC.Models.Base.Employee)(dsManager.Operator)).Dept.ID;
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.dtpStart.Value > this.dtpEnd.Value)
            {
                MessageBox.Show( "��ѯ��ʼʱ�䲻�ܴ��ڲ�ѯ��ֹʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return -1;
            }

            if (this.cbbBillType1.Tag.ToString() != "ALL")
            {
                this.orderTypeCompare = " or (t.billclass_code='R' and decode((select s.type_code from met_ipm_order s where s.mo_order=t.mo_order),"
                + "'CF','090630150926','CZ','090623094701','LZ','090623094730','CD','090623094745','BL','090623094917','SQ','090623094904','') = '" + this.cbbBillType1.Tag.ToString() + "' ) ";
            }            
            this.SetDrugType();
            this.QueryType();
            this.QueryDept();
            this.QueryPatient();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print pt = new FS.FrameWork.WinForms.Classes.Print();
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                pt.PrintPreview(10, 10, this.neuPanel12);
            }
            else if (this.neuTabControl1.SelectedIndex == 1)
            {
                pt.PrintPreview(10, 10, this.neuPanel10);
            }
            else
            {
                pt.PrintPreview(10, 10, this.neuPanel2);
            }

            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.neuSpread4.Export();
            }
            else if (this.neuTabControl1.SelectedIndex == 1)
            {
                this.neuSpread3.Export();
            }
            else
            {
                this.neuSpread1.Export();
            }
            
            return base.Export(sender, neuObject);
        }

        private void tbDrug1_TextChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0 || true)
            {
                string strFilter = " ƴ���� like '%" + this.tbDrug1.Text.Trim() + "%'";
                DataView dv = this.fpType.DataSource as DataView;
                if (dv != null)
                {
                    dv.RowFilter = strFilter;
                    this.fpType.Columns[0].Width = 150;
                    this.fpType.Columns[1].Width = 70;
                    this.fpType.Columns[2].Width = 70;
                    this.fpType.Columns[3].Width = 70;
                    this.fpType.Columns[4].Width = 50;
                    this.fpType.Columns[5].Width = 50;
                    this.fpType.Columns[6].Width = 35;
                }
            }

            if (this.neuTabControl1.SelectedIndex == 1 || true)
            {
                string strFilter = " ƴ���� like '%" + this.tbDrug1.Text.Trim() + "%'";
                DataView dv = this.fpDept.DataSource as DataView;
                if (dv != null)
                {
                    dv.RowFilter = strFilter;
                    this.fpDept.Columns[0].Width = 100;
                    this.fpDept.Columns[1].Width = 130;
                    this.fpDept.Columns[2].Width = 70;
                    this.fpDept.Columns[3].Width = 70;
                    this.fpDept.Columns[4].Width = 60;
                    this.fpDept.Columns[5].Width = 50;
                    this.fpDept.Columns[6].Width = 70;
                    this.fpDept.Columns[7].Width = 35;
                }
            }

            if (this.neuTabControl1.SelectedIndex == 2 || true)
            {
                string strFilter = " ƴ���� like '%" + this.tbDrug1.Text.Trim() + "%'";
                DataView dv = this.fpDrugedList.DataSource as DataView;
                if (dv != null)
                {
                    dv.RowFilter = strFilter;
                    this.fpDrugedList.Columns[0].Width = 60;
                    this.fpDrugedList.Columns[1].Width = 70;
                    this.fpDrugedList.Columns[2].Width = 60;
                    this.fpDrugedList.Columns[3].Width = 100;
                    this.fpDrugedList.Columns[4].Width = 130;
                    this.fpDrugedList.Columns[5].Width = 60;
                    this.fpDrugedList.Columns[6].Width = 60;
                    this.fpDrugedList.Columns[7].Width = 70;
                    this.fpDrugedList.Columns[8].Width = 35;
                    this.fpDrugedList.Columns[9].Width = 70;
                    this.fpDrugedList.Columns[10].Width = 35;
                    this.fpDrugedList.Columns[11].Width = 100;
                }
            }
        }        

        private void neuButton4_Click(object sender, EventArgs e)
        {
            if (this.pnlDefine3.Height < 1)
            {
                this.pnlDefine3.Height = 30;
            }
            else
            {
                this.pnlDefine3.Height = 0;
            }
        }

        private void neuButton5_Click(object sender, EventArgs e)
        {
            if (this.pnlDefine2.Height < 1)
            {
                this.pnlDefine2.Height = 30;
            }
            else
            {
                this.pnlDefine2.Height = 0;
            }
        }

        private void neuButton6_Click(object sender, EventArgs e)
        {
            if (this.pnlDefine1.Height < 1)
            {
                this.pnlDefine1.Height = 30;
            }
            else
            {
                this.pnlDefine1.Height = 0;
            }
        }

        private void rbtSend_CheckedChanged(object sender, EventArgs e)
        {
            this.SetDrugType();
        }

        private void rbtQuit_CheckedChanged(object sender, EventArgs e)
        {
            this.SetDrugType();
        }

        private void rbtAll_CheckedChanged(object sender, EventArgs e)
        {
            this.SetDrugType();
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 2)
            {
                this.chkSum.Checked = false;
                this.chkSum.Enabled = false;
            }
            else
            {
                //this.chkSum.Checked = true;
                this.chkSum.Enabled = true;
            }
        }

        private void neuComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.neuSpread1.Font = new Font("����", Convert.ToInt32(this.cbbFontSize.Text.Trim()));
            this.neuSpread3.Font = new Font("����", Convert.ToInt32(this.cbbFontSize.Text.Trim()));
            this.neuSpread4.Font = new Font("����", Convert.ToInt32(this.cbbFontSize.Text.Trim()));
        }

        #endregion        
               
    }

    /// <summary>
    /// ��ѯҵ����
    /// </summary>
    public class DrugQueryClass : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// �ۺϲ�ѯҩ��סԺ��ҩ��Ϣ
        /// </summary>
        /// <param name="sqlStr">��ѯ���</param>
        /// <returns></returns>
        public System.Data.DataTable QueryInPharmacyDrug(string sqlStr)
        {
            System.Data.DataSet ds1 = new System.Data.DataSet();
            if (this.ExecQuery(sqlStr, ref ds1) == -1)
            {
                this.Err = "��ѯ��ҩ��Ϣʧ�ܣ�ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            return ds1.Tables[0];
        }
    }
}
