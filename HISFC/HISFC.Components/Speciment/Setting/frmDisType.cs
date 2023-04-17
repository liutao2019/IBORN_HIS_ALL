using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class frmDisType : Form
    {
        //���ֹ������
        private DisTypeManage disTypeManage;
        private OrgTypeManage orgTypeManage;
        private SpecBarCodeManage specBarCodeManage;
        //��������ʵ��
        private DiseaseType disType;
        private Dictionary<int, string> dicType;
        //�Ի��������
        private string title;
        //��������
        private string disTypeName;
        private string disTypeAbre;

        public frmDisType()
        {
            InitializeComponent();
            disTypeManage = new DisTypeManage();
            disType = new DiseaseType();
            dicType = new Dictionary<int, string>();
            title = "������������";
            disTypeName = "";
            disTypeAbre = "";
            specBarCodeManage = new SpecBarCodeManage();
            orgTypeManage = new OrgTypeManage();
        }

        /// <summary>
        /// �������Ͱ�
        /// </summary>
        private void DisTypeBinding()
        {
            dicType = disTypeManage.GetAllDisType();
            if (dicType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicType;
                cmbDisType.DisplayMember = "Value";
                cmbDisType.ValueMember = "Key";
                cmbDisType.DataSource = bs;
            }
        }

        /// <summary>
        /// ��֯���Ͱ�
        /// </summary>
        private void OrgTypeBinding()
        {
            Dictionary<int, string> orgTypeDic = new Dictionary<int, string>();
            orgTypeDic = orgTypeManage.GetAllOrgType();
            if (orgTypeDic.Count > 0)
            {
                BindingSource bsTmp = new BindingSource();
                bsTmp.DataSource = orgTypeDic;
                cmbOrgType.ValueMember = "Key";
                cmbOrgType.DisplayMember = "Value";
                cmbOrgType.DataSource = bsTmp;                
            }
        }
        
        /// <summary>
        /// ��ҳ���ȡ��������
        /// </summary>
        /// <param name="oper">��������</param>
        /// <returns></returns>
        private string ValidateSetting(string oper)
        {
            string disTypeName = txtName.Text.TrimStart().TrimEnd();// cmbOrgType.SelectedText;         
            if (disTypeName == "")
            {
                MessageBox.Show("���Ʋ���Ϊ�գ�", title);
                return "";
            }
            if (txtAbb.Text.Trim() == "")
            {
                MessageBox.Show("������д����Ϊ�գ�", title);
                return "";
            }
            else
            {
                //�鿴�Ƿ������ͬ����д
                string disAbre= "";
                disAbre = specBarCodeManage.ExecSqlReturnOne("select DISABRRE from SPEC_SUBBARCODE where DISABRRE in ('" + txtAbb.Text.Trim() + "')");
                if (oper == "Insert" && disAbre != "" && disAbre != "-1")
                {
                    MessageBox.Show("������д�ظ���", title);
                    return "";
                }
                if (disAbre != "" && disAbre != "-1" && disAbre != disTypeAbre)
                {
                    MessageBox.Show("������д�ظ���", title);
                    return "";
                }
            }
            if (disTypeName != "")
            {
                try
                {
                    disType.OrgOrBld = cmbOrgType.SelectedValue.ToString();
                }
                catch
                { }
                switch (oper)
                {
                    case ("Insert"):
                        disType.DiseaseName = disTypeName;
                        if (dicType.ContainsValue(disTypeName))
                        {
                            MessageBox.Show("�����ظ������������ã�", title);
                            return "";
                        }
                        disType.DiseaseColor = txtColor.BackColor.ToArgb().ToString();
                        return "Insert";

                    case ("Update"):
                        disType.DisTypeID = Convert.ToInt32(cmbDisType.SelectedValue);
                        disType.DiseaseName = disTypeName;
                        if (dicType.ContainsValue(disTypeName)&&cmbDisType.Text!=disTypeName)
                        {
                            MessageBox.Show("�����ظ������������ã�", title);
                            return "";
                        }
                        disType.DiseaseColor = txtColor.BackColor.ToArgb().ToString();
                        return "Update";
                    default:
                        return "";
                }
            }
            return "";
        }

       
        /// <summary>
        /// ���没������
        /// </summary>
        private int SaveType(string oper)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            specBarCodeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            disTypeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                string disOper = ValidateSetting(oper);
                switch (disOper)
                {
                    case (""):
                        return 0;
                    case ("Update"):
                        if (specBarCodeManage.ExecNoQuery("update SPEC_SUBBARCODE set DISEASETYPE='" + txtName.Text.TrimStart().TrimEnd() + "',DISABRRE='" + txtAbb.Text.Trim() + "' where DISEASETYPE='" + disTypeName + "'") <= -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                        if (disTypeManage.UpdateDisType(disType) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                        break;

                    case ("Insert"):
                        System.Collections.ArrayList arrTmp = specBarCodeManage.GetAllSpecTypeByDisType();
                        if (arrTmp != null && arrTmp.Count > 0)
                        {
                            foreach (SpecBarCode bar in arrTmp)
                            {
                                SpecBarCode newBar = bar;
                                newBar.DisType = disType.DiseaseName;
                                newBar.Sequence = "1";
                                newBar.DisAbrre = txtAbb.Text.Trim();
                                if (specBarCodeManage.InsertBarCode(newBar) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    return -1;
                                }
                            }
                        }
                        string disId =  "";
                        disTypeManage.GetNextSequence(ref disId);
                        disType.DisTypeID = Convert.ToInt32(disId);
                        if (disTypeManage.InsertOrgType(disType) == -1)
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

        private void frmDisType_Load(object sender, EventArgs e)
        {
            OrgTypeBinding();
            DisTypeBinding();
        }

        private void cmbDisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Text = cmbDisType.Text;
            disType = disTypeManage.SelectDisByID(cmbDisType.SelectedValue.ToString());           
            try
            {
                disTypeName = cmbDisType.Text;
                txtAbb.Text = specBarCodeManage.ExecSqlReturnOne("select DISABRRE from SPEC_SUBBARCODE where DISEASETYPE = '" + disTypeName + "'");
                disTypeAbre = txtAbb.Text;
                cmbOrgType.SelectedValue = Convert.ToInt32(disType.OrgOrBld);
                txtColor.BackColor = Color.FromArgb(Convert.ToInt32(disType.DiseaseColor));
             }
            catch
            {
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string btnText = btnSave.Text;            
            if (btnText == "����")
            {
                int save = SaveType("Insert");
                if (save > 0)
                {
                    MessageBox.Show("����ɹ���", title);
                    DisTypeBinding();
                }
                if (save == -1)
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int save = SaveType("Update");
            
            if (save >= 1)
            {
                MessageBox.Show("���³ɹ���", title);
                DisTypeBinding();
            }
            if (save == -1)
            {
                MessageBox.Show("����ʧ�ܣ�", title);
            }
        }

        private void txtColor_Click(object sender, EventArgs e)
        {
            colorDia.ShowDialog();
            this.txtColor.BackColor = this.colorDia.Color;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //id = 16 ����ɾ����Ӳ�Թ涨
            if (cmbDisType.SelectedValue == null || cmbDisType.Text == "" || cmbDisType.SelectedValue.ToString()== "16")
                return;
            string sql = @"select DISEASETYPEID
            from SPEC_SOURCE ss
            where ss.DISEASETYPEID = {0} ";

            sql = string.Format(sql, cmbDisType.SelectedValue);

            string res = disTypeManage.ExecSqlReturnOne(sql);
            if (res != "-1")
            {
                MessageBox.Show("�ò�������ʹ�ã�����ɾ��");
                return;
            }

            DialogResult dr = MessageBox.Show("�Ƿ�ɾ������: " + cmbDisType.Text, "ɾ��", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            
            disTypeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                string delDisSql = @"delete
                                from SPEC_DISEASETYPE d
                                where d.DISEASETYPEID = {0}";

                string delSubCodeSql = @"delete
                                from SPEC_SUBBARCODE s
                                where s.DISEASETYPE = '{0}'";

                delDisSql = string.Format(delDisSql, cmbDisType.SelectedValue);
                //18918169405
                delSubCodeSql = string.Format(delSubCodeSql, cmbDisType.Text.Trim());

                if (disTypeManage.ExecNoQuery(delDisSql) <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ��ʧ�ܣ�" + disTypeManage.Err);
                    return;
                }
                if (disTypeManage.ExecNoQuery(delSubCodeSql) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ��ʧ�ܣ�" + disTypeManage.Err);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("ɾ���ɹ�");
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ��ʧ�ܣ�" + disTypeManage.Err);
            }             
            DisTypeBinding();
        }

    }
}