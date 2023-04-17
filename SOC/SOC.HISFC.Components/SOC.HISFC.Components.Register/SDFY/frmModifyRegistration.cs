using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Register.SDFY
{
    public partial class frmModifyRegistration : Form
    {
        public frmModifyRegistration()
        {
            InitializeComponent();

            this.Init();
        }

        public frmModifyRegistration(FS.HISFC.Models.Registration.Register reg)
        {
            InitializeComponent();

            obj = reg;
            Init();
        }

        private FS.HISFC.Models.Registration.Register obj;

        /// <summary>
        /// 挂号实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get { return this.obj; }
        }


        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        private void Init()
        {

            this.Load += new EventHandler(frmModifyRegistration_Load);
            this.txtCardNo.KeyDown += new KeyEventHandler(txtCardNo_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.cmbSex.KeyDown += new KeyEventHandler(cmbSex_KeyDown);
            this.dtpBirthDay.KeyDown += new KeyEventHandler(dtBirthday_KeyDown);
            this.txtPhone.KeyDown += new KeyEventHandler(txtPhone_KeyDown);
            this.txtAdress.KeyDown += new KeyEventHandler(txtAdress_KeyDown);

            #region 生日年龄精确对应  ygch {67E6B35B-68C3-4573-A360-ABBBB6EA4346}
            this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
            //生日
            this.dtpBirthDay.Value = this.regMgr.GetDateTimeFromSysDateTime();//出生日期
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = "  0岁 0月 0天";
            this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            #endregion

            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);

            this.button1.Focus();
        }

        private void SetValue(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg == null)
            {
                reg = new FS.HISFC.Models.Registration.Register();
            }

            this.txtRecipeNo.Text = reg.InvoiceNO;
            this.txtSeeNo.Text = reg.OrderNO.ToString();
            this.txtName.Text = reg.Name;
            this.cmbSex.Tag = reg.Sex.ID;
            this.dtpBirthDay.Value = reg.Birthday;
            this.txtPhone.Text = reg.PhoneHome;
            this.txtAdress.Text = reg.AddressHome;
        }


        private void frmModifyRegistration_Load(object sender, EventArgs e)
        {
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            if (this.obj != null)
            {
                this.SetValue(obj);
                this.button1.Focus();
            }
        }



        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text.Trim();
                this.Clear();

                if (cardNo == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入病历号"), "提示");
                    this.txtCardNo.Focus();
                    return;
                }

                cardNo = cardNo.PadLeft(10, '0');
                this.txtCardNo.Text = cardNo;

                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().Date;
                //检索患者有效号
                ArrayList al = this.regMgr.Query(cardNo, permitDate);
                if (al == null)
                {
                    MessageBox.Show("检索患者挂号信息时出错!" + this.regMgr.Err, "提示");
                    return;
                }

                if (al.Count == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有符合条件的挂号信息"), "提示");
                    return;
                }

                if (al.Count == 1)
                {
                    this.obj = (FS.HISFC.Models.Registration.Register)al[0];
                }
                else
                {
                    if (this.getSelectRegInfo(al) == -1)
                    {
                        this.txtCardNo.Focus();
                        return;
                    }
                }

                this.SetValue(this.obj);

                this.txtName.Focus();
            }
        }

        private void Clear()
        {
            this.obj = null;

            this.txtCardNo.Text = "";
            this.txtRecipeNo.Text = "";
            this.txtSeeNo.Text = "";
            this.txtName.Text = "";
            this.cmbSex.Tag = "";
            this.txtPhone.Text = "";
            this.txtAdress.Text = "";
        }

        private int getSelectRegInfo(ArrayList alRegs)
        {
            FS.HISFC.Components.Registration.frmSelectRegister f = new FS.HISFC.Components.Registration.frmSelectRegister();

            f.SetRegInfo(alRegs);

            DialogResult dr = f.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.obj = f.Reg;
                f.Dispose();
            }
            else
            {
                f.Dispose();
                return -1;
            }

            return 0;
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbSex.Focus();
            }
        }

        private void cmbSex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtpBirthDay.Focus();
            }
        }

        private void dtBirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPhone.Focus();
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAdress.Focus();
            }
        }

        private void txtAdress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.valid() == -1) return;

            //
            this.obj.Name = this.txtName.Text.Trim();
            this.obj.Sex.ID = this.cmbSex.Tag.ToString();
            this.obj.Birthday = this.dtpBirthDay.Value;
            this.obj.PhoneHome = this.txtPhone.Text.Trim();
            this.obj.AddressHome = this.txtAdress.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private int valid()
        {
            if (this.obj == null || this.obj.ID == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择挂号信息"), "提示");
                this.txtCardNo.Focus();
                return -1;
            }

            if (this.txtName.Text.Trim() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                this.txtName.Focus();
                return -1;
            }


            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 40) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者名称最多可录入20个汉字"), "提示");
                this.txtName.Focus();
                return -1;
            }
            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者性别"), "提示");
                this.cmbSex.Focus();
                return -1;
            }


            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text.Trim(), 20) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系电话最多可录入20位数字"), "提示");
                this.txtPhone.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAdress.Text.Trim(), 60) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系人地址最多可录入30个汉字"), "提示");
                this.txtAdress.Focus();
                return -1;
            }

            //if ((this.txtPhone.Text == null || this.txtPhone.Text.Trim() == "") &&
            //    (this.txtAdress.Text == null || this.txtAdress.Text.Trim() == ""))
            //{
            //    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "联系电话和地址不能同时为空,必须输入一个" ), "提示" );
            //    this.txtPhone.Focus();
            //    return -1;
            //}

            #region 增加出生日期有效性判断 ygch
            if (this.dtpBirthDay.Value.Date > this.regMgr.GetDateTimeFromSysDateTime().Date)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期大于当前日期,请重新输入!"));
                this.dtpBirthDay.Focus();
                return -1;
            }
            #endregion
            //if(this.dtBirthday.Value

            return 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.No ;
            this.Close();
        }



        #region 年龄生日精确对照  ygch {67E6B35B-68C3-4573-A360-ABBBB6EA4346}

        /// <summary>
        /// 根据年龄算生日
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <returns></returns>
        private void ConvertBirthdayByAge()
        {
            DateTime birthDay = this.regMgr.GetDateTimeFromSysDateTime();
            string ageStr = this.txtAge.Text.Trim();
            ageStr = ageStr.Replace("_", "");
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(0, ageStr.IndexOf("岁")));
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(ageStr.IndexOf("岁") + 1, ageStr.IndexOf("月") - ageStr.IndexOf("岁") - 1));
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(ageStr.Substring(ageStr.IndexOf("月") + 1, ageStr.IndexOf("天") - ageStr.IndexOf("月") - 1));
            birthDay = birthDay.AddYears(-iyear).AddMonths(-iMonth).AddDays(-iDay - 1);
            this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
            if (birthDay < dtpBirthDay.MinDate || birthDay > dtpBirthDay.MaxDate)
            {
                MessageBox.Show("年龄输入过大请重新输入！");
                this.txtAge.Text = "  0岁 0月 0天";
                return;
            }
            this.dtpBirthDay.Value = birthDay;
            this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            this.ConvertBirthdayByAge();
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            string age = this.regMgr.GetAge(this.dtpBirthDay.Value, true);
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = age;
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            //this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            //this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            //this.cmbAgeUnit.SelectedIndexChanged -=new EventHandler(cmbAgeUnit_SelectedIndexChanged);
            //this.cmbAgeUnit.Text = age.Substring(age.Length - 1, 1);
            //this.cmbAgeUnit.SelectedIndexChanged += new EventHandler(cmbAgeUnit_SelectedIndexChanged);
        }

        #endregion
    }
}