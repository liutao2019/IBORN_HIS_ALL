using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.NanZhuang
{
    /// <summary>
    /// 查询佛山合作医疗人员名单
    /// </summary>
    public partial class ucCooperatePatientMaintenance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 成员

        FS.SOC.Local.OutpatientFee.NanZhuang.Function fun = new Function();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// Manager业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 工具条
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 设置开始时间
        /// </summary>
        private string beginTime = "2011-12-20";

        /// <summary>
        /// 设置结束时间
        /// </summary>
        private string endTime = "2011-12-20";

        /// <summary>
        /// 医保合同单位编码
        /// </summary>
        private string pactCode = "2";
        #endregion

        #region 属性
        /// <summary>
        /// 设置开始时间
        /// </summary>
        [Category("控件设置"), Description("设置开始时间")]
        public string BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }

        /// <summary>
        /// 设置结束时间
        /// </summary>
        [Category("控件设置"), Description("设置结束时间")]
        public string EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        /// <summary>
        /// 医保合同单位编码
        /// </summary>
        [Category("控件设置"), Description("医保合同单位编码")]
        public string PactCode
        {
            get
            {
                return pactCode;
            }
            set
            {
                pactCode = value;
            }
        }
        #endregion

        #region 方法
        public ucCooperatePatientMaintenance()
        {
            InitializeComponent();
            {
            }
        }

        //初始化
        protected virtual int Init()
        {
            try
            {
                //性别列表
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
                this.cmbSex.Text = "男";

                this.dtpBeginDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(beginTime);
                this.dtpEndDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(endTime);

                ArrayList alDept=new ArrayList();
                ArrayList alssdw = managerIntegrate.GetConstantList("SSDW");
                if (alssdw == null)
                {
                    MessageBox.Show("查找合作医疗所属单位失败");
                    return -1;
                }
                FS.FrameWork.Models.NeuObject obj=new FS.FrameWork.Models.NeuObject();
                obj.ID="ALL";
                obj.Name="全部";
                alDept.Add(obj);
                alDept.AddRange(alssdw);
                this.cmbDepartment.AddItems(alDept);
                this.cmbDepartment.SelectedIndex = 0;

                //有效性列表
                ArrayList al = new ArrayList();
                FS.FrameWork.Models.NeuObject object1 = new FS.FrameWork.Models.NeuObject();
                FS.FrameWork.Models.NeuObject object2 = new FS.FrameWork.Models.NeuObject();
                object1.Name = "有效";
                object1.ID = "1";
                object2.Name = "无效";
                object2.ID = "0";
                al.Add(object1);
                al.Add(object2);
                this.cmbStateFlag.AddItems(al);
                this.cmbStateFlag.Text = "有效";

            }
            catch (Exception e)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message);

                return -1;
            }
            return 1;
        }
        //清屏
        public void Clear()
        {
            this.txtName.Tag = "";
            this.txtName.Text = "";
            this.txtAddress.Text = "";
            this.txtSiNO.Text = "";
            this.cmbDepartment.SelectedIndex = 0;
            this.dtpBeginDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(beginTime);
            this.dtpEndDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(endTime);
            this.dtpBrithday.Value = conMgr.GetDateTimeFromSysDateTime();
            this.txtIdenNO.Text = "";
            this.cmbSex.Text = "男";
            this.cmbStateFlag.Text = "有效";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        //查询
        public void Query()
        {
            string name = "";
            string idenNO = "";
            //当姓名和身份证号都为空时，查询所有
            if (this.txtName.Text=="")
            {
                name = "ALL";
            } 
            else
            {
                name = this.txtName.Text;
            }

            if (this.txtIdenNO.Text == "")
            {
                idenNO = "ALL";
            }
            else
            {
                idenNO = this.txtIdenNO.Text;
            }
            string department = this.cmbDepartment.Text.ToString();
            string stateFlag = this.cmbStateFlag.Tag.ToString();


            int intReturn = 0;
            DataSet ds = new DataSet();
            intReturn = fun.GetCooperatePatientByNameAndIdenNO(name, idenNO, department, stateFlag, ref ds);
            if (intReturn == -1)
            {
                MessageBox.Show("查找数据失败");
                return;
            }
            else
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount,1);
                    int row = this.neuSpread1_Sheet1.RowCount - 1;
                    this.neuSpread1_Sheet1.Rows[row].Tag = dr[0].ToString();//序列号
                    this.neuSpread1_Sheet1.SetValue(row, 0, dr[1].ToString(), false);//姓名
                    this.neuSpread1_Sheet1.SetValue(row, 1, dr[2].ToString(), false);//身份证号
                    this.neuSpread1_Sheet1.SetValue(row, 2, dr[3].ToString(), false);//社保号
                    this.neuSpread1_Sheet1.SetValue(row, 3, dr[4].ToString(), false);//出生日期
                    switch (dr[5].ToString())
                    {
                        case "M":
                            this.neuSpread1_Sheet1.SetValue(row, 4, "男", false);//性别
                            break;
                        case "F":
                            this.neuSpread1_Sheet1.SetValue(row, 4, "女", false);//性别
                            break;

                        default:
                            this.neuSpread1_Sheet1.SetValue(row, 4, "", false);//性别
                            break;
                    }

                    this.neuSpread1_Sheet1.SetValue(row, 5, dr[6].ToString(), false);//起始时间
                    this.neuSpread1_Sheet1.SetValue(row, 6, dr[7].ToString(), false);//终止时间
                    this.neuSpread1_Sheet1.SetValue(row, 7, dr[8].ToString(), false);//状态
                    this.neuSpread1_Sheet1.SetValue(row, 8, dr[9].ToString(), false);//地址
                    this.neuSpread1_Sheet1.SetValue(row, 9, dr[10].ToString(), false);//所属单位
                }
            }

        }

        //保存，其中包含增加和修改的方法
        public void Save()
        {
            string seqNo = "";
            if (this.txtName.Tag == null)
            {
                seqNo = "";
            }
            else
            {
                seqNo = this.txtName.Tag.ToString();
            }

            string name = this.txtName.Text.ToString();
            string idenNO = this.txtIdenNO.Text.ToString();
            string siNo = this.txtSiNO.Text.ToString();
            DateTime birthday = this.dtpBrithday.Value;
            string sex = this.cmbSex.Tag.ToString();
            switch (sex)
            {
                case "男":
                case "M":
                    sex = "M";
                    break;
                case "女":
                case "F":
                    sex = "F";
                    break;

                default:
                    sex = "U";
                    break;
            }


            DateTime beginDate = this.dtpBeginDate.Value;
            DateTime endDate = this.dtpEndDate.Value;
            string stateFlag = this.cmbStateFlag.Tag.ToString();
            string address = this.txtAddress.Text.ToString();
            string department = this.cmbDepartment.Text.ToString();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入姓名！");
                return;
            }
            if (string.IsNullOrEmpty(idenNO))
            {
                MessageBox.Show("请输入身份证号！");
                return;
            }
            if (string.IsNullOrEmpty(department))
            {
                MessageBox.Show("请输入所属单位！");
                return;
            }
            int intReturn = 0;

            if (stateFlag != "1")
            {
                intReturn = this.CheckValid();
                if (intReturn == -1)
                {
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (string.IsNullOrEmpty(seqNo))
            {
                intReturn = fun.InsertCooperatePatientByNameAndIDenNO(name, idenNO, sex, address, department, beginDate.ToString(), endDate.ToString(), stateFlag, birthday.ToString(), siNo);

                if (intReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入患者信息失败！");
                    return;
                }
            }
            else
            {
                intReturn = fun.UpdateCooperatePatientByNameAndIDenNO(name, idenNO, sex, address, department, beginDate.ToString(), endDate.ToString(), stateFlag, birthday.ToString(), siNo, seqNo);

                if (intReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新患者信息失败");
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功！");
            this.Clear();
        }

        private int Export()
        {
            if (this.neuSpread1.Export() == -1)
            {
                MessageBox.Show("导出失败！");
                return -1;
            }
            else
            {
                MessageBox.Show("导出成功！");
                return 1;
            }

            
        }


        public int CheckValid()
        {
            if (this.pactCode == "")
            {
                MessageBox.Show("请维护医保合同单位");
                return -1;
            }
            FS.HISFC.Models.Registration.Register r = new FS.HISFC.Models.Registration.Register();
            r.IDCard = this.txtIdenNO.Text.Trim();
            r.Pact.ID = this.pactCode.ToString();

            if (!string.IsNullOrEmpty(r.IDCard) && !string.IsNullOrEmpty(r.Pact.ID))
            {
                int iRes = this.medcareProxy.SetPactCode(r.Pact.ID);
                if (iRes != 1)
                {
                    MessageBox.Show("链接数据库失败");
                    return -1;
                }

                iRes = this.medcareProxy.QueryCanMedicare(r);
                if (iRes == -2)
                {
                    MessageBox.Show("链接数据库失败");
                    return -1;
                }
                else if (iRes == -1)
                {
                    MessageBox.Show("该患者不享受居民医保");
                    return -1;
                }

            }

            return 1;
        }
        //删除
        public void delete()
        {
            MessageBox.Show("");
        }
        #endregion

        #region 设置toolBar按钮
        /// <summary>
        /// 设置toolBar按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("清屏", "清屏！", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            return toolBarService;
        }
        /// <summary>
        /// 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    {
                        this.Clear();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

      
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return base.Export(sender, neuObject);
        }


        #endregion

        #region 事件
        //双击事件
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = e.Row;
            this.txtName.Tag = this.neuSpread1_Sheet1.Rows[row].Tag;//序列号
            this.txtName.Text = this.neuSpread1_Sheet1.Cells[row, 0].Text.ToString();
            this.txtIdenNO.Text = this.neuSpread1_Sheet1.Cells[row, 1].Text.ToString();
            this.txtSiNO.Text = this.neuSpread1_Sheet1.Cells[row, 2].Text.ToString();
            try
            {
                this.dtpBrithday.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[row, 3].Text.ToString());
            }
            catch
            {
            }

            string sex = this.neuSpread1_Sheet1.Cells[row, 4].Text.ToString();
            this.cmbSex.Text = sex;
            this.cmbSex.Tag = sex;

            try
            {
                this.dtpBeginDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[row, 5].Text.ToString());
                this.dtpEndDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[row, 6].Text.ToString());
            }
            catch
            {
            }

            string state = this.neuSpread1_Sheet1.Cells[row, 7].Text.ToString();
            this.cmbStateFlag.Tag = state;

            this.txtAddress.Text = this.neuSpread1_Sheet1.Cells[row, 8].Text.ToString();

            string depart = this.neuSpread1_Sheet1.Cells[row, 9].Text.ToString();
            this.cmbDepartment.Text = depart;
            this.cmbDepartment.Tag = depart;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtIdenNO.Focused)
                {
                    string error = string.Empty;
                    string idNO = this.txtIdenNO.Text.Trim();
                    if (idNO != string.Empty)
                    {
                        if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref error) < 0)
                        {
                            MessageBox.Show(error);
                            return true;
                        }
                        //根据身份证号获取患者性别
                        FS.FrameWork.Models.NeuObject obj = FS.HISFC.Components.Account.Class.Function.GetSexFromIdNO(idNO, ref error);
                        if (obj == null)
                        {
                            MessageBox.Show(error);
                            return true;
                        }
                        this.cmbSex.Tag = obj.ID;
                        //根据患者身份证号获取生日
                        string birthdate = FS.HISFC.Components.Account.Class.Function.GetBirthDayFromIdNO(idNO, ref error);
                        if (birthdate == "-1")
                        {
                            MessageBox.Show(error);
                            return true;
                        }
                        this.dtpBrithday.Value = FrameWork.Function.NConvert.ToDateTime(birthdate);
                    }

                }

               
                SendKeys.Send("{Tab}");

                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        private void ucCooperatePatientMaintenance_Load(object sender, EventArgs e)
        {
            this.Init();
        }



    }
}
