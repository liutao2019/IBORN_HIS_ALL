using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.SpecType
{
    public partial class frmSpecType : Form
    {
        //��֯���͹������
        private OrgTypeManage orgTypeManage;
        //�걾���͹������
        private SpecTypeManage specTypemanage;
        private SpecBarCodeManage specBarCodeManage;
        private DisTypeManage disTypeManage;
        private FS.HISFC.Models.Speciment.SpecType specType;
        private OrgType orgType;
        private Dictionary<int, string> orgTypeDic;
        private Dictionary<int, string> specTypeDic;
        private string title = "�걾��������";
        private string specTypeAbrre = "";

        public frmSpecType()
        {
            InitializeComponent();
            specTypemanage = new SpecTypeManage();
            orgTypeManage = new OrgTypeManage();
            specBarCodeManage = new SpecBarCodeManage();
            disTypeManage = new DisTypeManage();
            orgTypeDic = new Dictionary<int,string>();
            specTypeDic = new Dictionary<int, string>();
            specType = new FS.HISFC.Models.Speciment.SpecType();
            orgType = new OrgType();
        }

        /// <summary>
        /// ��ҳ���ȡ�걾����
        /// </summary>
        private string SetSecType(string oper)
        {
            string specName = txtName.Text.TrimStart().TrimEnd();
            ArrayList arrAll = specTypemanage.GetAllSpecType();
            bool exsited = false;

            if (arrAll != null && arrAll.Count > 0)
            {
                foreach (FS.HISFC.Models.Speciment.SpecType type in arrAll)
                {
                    if (specName == type.SpecTypeName)
                    {
                        exsited = true;
                        break;
                    }
                }
            }
            if (txtAbrre.Text.Trim() == "")
            {
                MessageBox.Show("��д����Ϊ�գ�", title);
                return "";
            }

            if (specName == "")
                return "";

            //�鿴�Ƿ������ͬ����д
            string typeAbre = "";
            typeAbre = specBarCodeManage.ExecSqlReturnOne("select SPECTYPEABRRE from SPEC_SUBBARCODE where SPECTYPEABRRE in ('" + txtAbrre.Text.Trim() + "')");
            if (oper == "Insert" && typeAbre != "" && typeAbre != "-1")
            {
                MessageBox.Show("��д�ظ���", title);
                return "";
            }
            if (typeAbre != "" && typeAbre != "-1" && typeAbre != specTypeAbrre)
            {
                MessageBox.Show("��д�ظ���", title);
                return "";
            }
            specType.OrgType.OrgTypeID = Convert.ToInt32(cmbOrgType.SelectedValue);
            specType.DefaultCnt = Convert.ToInt32(nupCnt.Value);
            specType.IsShow = chk.Checked ? "1" : "0";
            specType.Ext1 = nudCapacity.Value.ToString();
            switch (oper)
            {
                case("Insert"):
                    if (exsited)
                    {
                        MessageBox.Show("�걾���Ʋ����ظ�!", title);
                        return "";
                    }
                    specType.SpecTypeName = specName;
                    specType.SpecTypeID = specTypemanage.GetSequence();
                    //if (specTypeDic.ContainsValue(specName))
                    //{
                    //    MessageBox.Show("�걾���Ʋ����ظ�!", title);
                    //    return "";
                    //}
                    specType.SpecColor = txtColor.BackColor.ToArgb().ToString();
                    //object c = ccmbColor.SelectedItem;
                    return "Insert";

                case("Update"):
                    if (exsited && cmbSpecName.Text != specName)
                    {
                        MessageBox.Show("�걾���Ʋ����ظ�!", title);
                        return "";
                    }
                    specType.SpecTypeName = specName; 
                    specType.SpecTypeID = Convert.ToInt32(cmbSpecName.SelectedValue);
                    specType.SpecColor  = txtColor.BackColor.ToArgb().ToString();
                    return "Update";

                case("Delete"):
                    specType.SpecTypeID = Convert.ToInt32(cmbSpecName.SelectedValue);
                    return "Delete";

                default:
                    return "";
            }          
        }

        /// <summary>
        /// ����걾���ͻ�걾��֯����
        /// </summary>
        private int SaveType(string oper)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            specBarCodeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specTypemanage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                string specOper = SetSecType(oper);
                switch (specOper)
                {
                    case ("Update"):
                        if (specBarCodeManage.ExecNoQuery("update SPEC_SUBBARCODE set SPECTYPE='" + txtName.Text.TrimStart().TrimEnd() + "',SPECTYPEABRRE='" + txtAbrre.Text.Trim() + "' where SPECTYPE='" + cmbSpecName.Text.TrimEnd().TrimStart() + "'") <= -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                        if (specTypemanage.UpdateSpecType(specType) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                        break;
                    case ("Insert"):
                        ArrayList arrTmp = new ArrayList();
                        arrTmp = specBarCodeManage.GetAllDisTypeBySpecType();
                        foreach (SpecBarCode bar in arrTmp)
                        {
                            SpecBarCode newBar = bar;
                            newBar.SpecType = specType.SpecTypeName;
                            newBar.OrgOrBld = cmbOrgType.SelectedValue.ToString();
                            newBar.Sequence = "1";
                            newBar.SpecTypeAbrre = txtAbrre.Text.Trim();
                            if (specBarCodeManage.InsertBarCode(newBar) == -1)
                            {                               
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                return -1;
                            }
                        }
                        if (specTypemanage.InsertSpecType(specType) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                        break;
                    case ("Delete"):

                        if (specTypemanage.DeleteSpecTypeByID(cmbSpecName.SelectedValue.ToString()) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                        break;
                    default:
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return 0;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                return 1;
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

        }
        /// <summary>
        /// ��֯���Ͱ�
        /// </summary>
        private void OrgTypeBinding()
        {
            orgTypeDic = orgTypeManage.GetAllOrgType();
            if (orgTypeDic.Count > 0)
            {
                //orgTypeDic.Add(-1,"");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = orgTypeDic;               
                cmbOrgType.ValueMember = "Key";
                cmbOrgType.DisplayMember = "Value";
                cmbOrgType.DataSource = bsTmp;
                cmbOrgType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// ������֯����ID��ȡ�걾����
        /// </summary>
        /// <param name="orgTypeID"></param>
        private void SpecTypeBinding(string orgTypeID)
        {
            cmbSpecName.DataSource = null; 
            specTypeDic = specTypemanage.GetSpecTypeByOrgID(orgTypeID);
            if (specTypeDic.Count > 0)
            {
                //specTypeDic.Add(-1, "");
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = specTypeDic;
                //cmbSpecName.DataSource = bsTmp;
                cmbSpecName.ValueMember = "Key";
                cmbSpecName.DisplayMember = "Value";                
                cmbSpecName.DataSource = bsTmp;
                cmbSpecName.SelectedIndex = 0;
            }            
        }

        /// <summary>
        /// �󶨱걾���͵���ɫ
        /// </summary>
        private void SpecColorBinding()
        {
            //string specColor = ccmbColor.SelectedItem.ToString();
            //string specColor = spectypem//;specTypemanage.GetColorByType(specTypeID);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {          
            string btnText = btnSave.Text;
            if (btnText == "����")
            {
                int result = SaveType("Insert");
                if (result == 1)
                {
                    MessageBox.Show("����ɹ���", title);
                    SpecTypeBinding(cmbOrgType.SelectedValue.ToString());
                }
                if (result == -1)
                {
                    MessageBox.Show("����ʧ�ܣ�", title);
                }
                btnSave.Text = "���";
            }
            else
            {
                this.txtName.Text = "";
                this.btnSave.Text = "����"; 
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            int save = SaveType("Update");

            if (save == 1)
            {
                MessageBox.Show("���³ɹ���", title);
            }
            if (save == -1)
            {
                MessageBox.Show("����ʧ�ܣ�", title);
            }
            SpecTypeBinding(cmbOrgType.SelectedValue.ToString());
        }

        private void cmbOrgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orgTypeName = cmbOrgType.Text;
            if (orgTypeName == "Ѫ")
            {
                lblMl.Visible = true;
            }
            else
            {
                lblMl.Visible = false;
            }
            int orgId = orgTypeManage.SelectOrgByName(orgTypeName);
            if (orgId > 0)
            {
                SpecTypeBinding(orgId.ToString());
                SpecColorBinding();
            }
        }

        private void cmbSpecName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                specType = new FS.HISFC.Models.Speciment.SpecType();
                specType = specTypemanage.GetSpecTypeById(cmbSpecName.SelectedValue.ToString());
                txtAbrre.Text = specBarCodeManage.ExecSqlReturnOne("select SPECTYPEABRRE from SPEC_SUBBARCODE where SPECTYPE = '" + cmbSpecName.Text.TrimEnd().TrimStart() + "'");
                specTypeAbrre = txtAbrre.Text;
                txtName.Text = specType.SpecTypeName;
                string color = specType.SpecColor;
                chk.Checked = specType.IsShow == "1" ? true : false;
                nupCnt.Value = specType.DefaultCnt;
                nudCapacity.Value = Convert.ToDecimal(specType.Ext1);
                if (color.Trim() != "")
                {
                    txtColor.BackColor = Color.FromArgb(Convert.ToInt32(color));
                }
            }
            catch
            { }
        }

        private void frmSpecType_Load(object sender, EventArgs e)
        {
            OrgTypeBinding();
        }

        private void txtColor_Click(object sender, EventArgs e)
        {
            this.diaColor.ShowDialog();
            txtColor.BackColor =  diaColor.Color;
            // int color = diaColor.Color.ToArgb();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Id = 9 ����ɾ����Ӳ�Թ涨��
            if (cmbSpecName.SelectedValue == null || cmbSpecName.Text.Trim() == "" || cmbSpecName.SelectedValue.ToString() == "9")
            {
                return;
            }
            string sql = @"select t.ORGTYPEID
            from SPEC_SUBSPEC ss join SPEC_TYPE t on ss.SPECTYPEID = t.SPECIMENTTYPEID 
            where t.SPECTYPEID = {0}";

            sql = string.Format(sql, cmbSpecName.SelectedValue);
            string result = orgTypeManage.ExecSqlReturnOne(sql);

            if (result != "-1")
            {
                MessageBox.Show("����ʹ�ã�����ɾ��");
                return;
            }

            DialogResult dr = MessageBox.Show("�Ƿ�ɾ��: " + cmbSpecName.Text, "ɾ��", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            int save = SaveType("Delete");
            if (save == 1)
            {
                MessageBox.Show("ɾ���ɹ���", title);
            }
            if (save == -1)
            {
                MessageBox.Show("ɾ��ʧ�ܣ�", title);
            }
            SpecTypeBinding(cmbOrgType.SelectedValue.ToString());
        }
    }
}