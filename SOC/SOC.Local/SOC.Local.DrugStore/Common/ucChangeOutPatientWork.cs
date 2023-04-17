using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Function;
using Neusoft.FrameWork.Management;

namespace Neusoft.SOC.Local.DrugStore.Common
{
    public partial class ucChangeOutPatientWork : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucChangeOutPatientWork()
        {
            InitializeComponent();
            this.txtNO.KeyPress += new KeyPressEventHandler(txtNO_KeyPress);
            this.nlbInfo.DoubleClick += new EventHandler(nlbInfo_DoubleClick);
        }

      

        /// <summary>
        /// 权限编码
        /// </summary>
        private string privePowerString = "0310";

        /// <summary>
        /// 权限编码
        /// </summary>
        [Description("权限编码"), Category("设置"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }



        /// <summary>
        /// 当前登陆科室信息
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject deptInfo = new Neusoft.FrameWork.Models.NeuObject();

        #region 工具栏

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }
        #endregion

        private void Init()
        {
            this.checkBox1.Checked = true;
        }

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            Neusoft.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new Neusoft.HISFC.BizLogic.Manager.UserPowerDetailManager();
            if (string.IsNullOrEmpty(PrivePowerString))
            {
                PrivePowerString = "0310";
            }
            int param = Neusoft.HISFC.Components.Common.Classes.Function.ChoosePivDept(PrivePowerString, ref this.deptInfo);
            if (param == 0 || param == -1)
            {
                return -1;
            }


            this.nlbInfo.Text = "您选择的科室是【" + this.deptInfo.Name + "】";

            return 1;

        }

        private void Query()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            Neusoft.FrameWork.Models.NeuObject neuObj = new Neusoft.FrameWork.Models.NeuObject();

            LocalWorkLoadManager workLoadManager = new LocalWorkLoadManager();
            //处方号
            string recipeNo = this.txtNO.Text.ToString();
            //工作类型 0配药 1发药
            string type = "";

            if(string.IsNullOrEmpty(recipeNo))
            {
                MessageBox.Show("请输入处方号");
                return;
            }

            if(this.checkBox1.Checked)
            {
                type = "0";
                neuObj = workLoadManager.QueryOutPatientWorkLoad(recipeNo,deptInfo.ID.ToString(),type);
            }
            else
            {
                type = "1";
                neuObj = workLoadManager.QueryOutPatientWorkLoad(recipeNo,deptInfo.ID.ToString(),type);
            }

            if (neuObj == null)
            {
                MessageBox.Show("未能根据处方号查询到有效配发药信息，请核对");
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                this.neuSpread1_Sheet1.Cells[0, 0].Value = true;
                this.neuSpread1_Sheet1.Cells[0, 1].Text = neuObj.Name;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = neuObj.ID;
                this.neuSpread1_Sheet1.Cells[0, 3].Text = neuObj.Memo;
                this.neuSpread1_Sheet1.Cells[0, 4].Text = neuObj.User01;
                this.neuSpread1_Sheet1.Cells[0, 5].Text = neuObj.User02;
                if (neuObj.User03 == "0")
                {
                    this.neuSpread1_Sheet1.Cells[0, 6].Text = "配药";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[0, 6].Text = "发药";
                }
                
                for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
                {
                    this.neuSpread1_Sheet1.Cells[0, i].Locked = true;
                }
                    this.neuSpread1_Sheet1.Rows[0].Tag = neuObj;
        }

        private void Save()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return;

            string operCode = this.lbOperCode.Text;
            Neusoft.HISFC.BizLogic.Manager.Person personManager = new Neusoft.HISFC.BizLogic.Manager.Person();
            Neusoft.HISFC.Models.Base.Employee employee = personManager.GetPersonByID(operCode);
            if (employee == null || string.IsNullOrEmpty(employee.ID))
            {
                MessageBox.Show("输入的工号不对，请核对");
                return;
            }
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            LocalWorkLoadManager workLoadManager = new LocalWorkLoadManager();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {

                //if (Neusoft.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value))
                {
                    Neusoft.FrameWork.Models.NeuObject neuObj = this.neuSpread1_Sheet1.Rows[i].Tag as Neusoft.FrameWork.Models.NeuObject;

                    if (this.checkBox1.Checked)
                    {
                        if (workLoadManager.UpdateOutPatientWorkLoad(neuObj, operCode, "0") != 1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            return;
                        }
                    }
                    else
                    {
                        if (workLoadManager.UpdateOutPatientWorkLoad(neuObj, operCode, "1") != 1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            return;
                        }
                    }
                }

                Neusoft.FrameWork.Management.PublicTrans.Commit();

                MessageBox.Show("保存成功");
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.Init();
            }
            catch
            { }

            base.OnLoad(e);
        }



        void nlbInfo_DoubleClick(object sender, EventArgs e)
        {
            this.SetPriveDept();
        }

        void txtNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Query();
            }
        }

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }

            return this.SetPriveDept();
        }

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.checkBox2.Checked = false;
            }
            else
            {
                this.checkBox2.Checked = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {
                this.checkBox1.Checked = false;
            }
            else
            {
                this.checkBox1.Checked = true;
            }
        }

    }
}
