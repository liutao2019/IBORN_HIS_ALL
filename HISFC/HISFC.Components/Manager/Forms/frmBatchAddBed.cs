using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Manager.Forms
{
    /// <summary>
    /// 床位维护
    /// </summary>
    public partial class frmBatchAddBed : Form
    {
        //护理站编号  在增加时用
        protected string bedRoomNO = string.Empty;
        public string BedRoomNO
        {
            set
            {
                bedRoomNO = value;
            }
        }

        //护理站编号  在增加时用
        protected string nurseStation = string.Empty;
        public string NurseStation
        {
            set
            {
                nurseStation = value;
            }
        }

        public frmBatchAddBed(bool isUpdate)
        {
            InitializeComponent();
            if (isUpdate)
            {
                txtBedNo.Enabled = false;
                this.cmbNurse.Enabled = false;
            }
            this.isUpdate = isUpdate;
            this.Init();
        }

        protected void Init()
        {
            FS.HISFC.BizLogic.Manager.Department Dept = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizLogic.Manager.Constant content = new FS.HISFC.BizLogic.Manager.Constant();
            this.cmbNurse.AddItems(Dept.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.N));//护士站列表
            this.cmdBedGrade.AddItems(content.GetList(FS.HISFC.Models.Base.EnumConstant.BEDGRADE));//床位等级
            this.cmbBedWeave.AddItems(FS.HISFC.Models.Base.BedRankEnumService.List());//床位编制
            this.cmbBedStatus.AddItems(FS.HISFC.Models.Base.BedStatusEnumService.List());//床位状态
            this.tbCount.Text = "1";
        }
        protected bool isUpdate = false;
        public string Err = "";
        FS.HISFC.BizLogic.Manager.Bed bed = new FS.HISFC.BizLogic.Manager.Bed();
        protected int CheckValid()
        {
            if (this.cmbNurse.SelectedItem == null)
            {
                this.Err = "护理站号不存在";
                return -1;
            }
            if (this.txtBedNo.Text == "")
            {
                this.Err = "床号为空，请填写！";
                return -1;
            }
            if (txtBedNo.Enabled)
            {
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtBedNo.Text, 6))
                {
                    this.Err = "床号过长，请重新填写！";
                    return -1;
                }
            }
            
            if (txtBedNo.Text != "")
            {
                for (int i = 0; i < int.Parse(tbCount.Text.Trim()) - 1; i++)
                {
                    int bedNo;
                    bedNo = int.Parse(txtBedNo.Text)  + i;
                    int temp = bed.IsExistBedNo(this.cmbNurse.SelectedItem.ID + bedNo);
                    if (temp == 0)
                    {
                        //没有
                    }
                    else if (temp == 1)
                    {
                        this.Err = "已经存在床位号 " + bedNo + "请修改！";
                        txtBedNo.Focus();
                        return -1;
                    }
                }
            }

            if (this.txtWardNo.Text == "")
            {
                this.Err = "病房号为空，请填写！";
                return -1;
            }
            if (this.cmdBedGrade.Text == "")
            {
                this.Err = "床位等级为空，请选择！";
                return -1;
            }
            if (this.cmbBedWeave.Text == "")
            {
                this.Err = "床位编制为空，请选择！";
                return -1;
            }
            if (this.cmbBedStatus.Text == "")
            {
                this.Err = "床位状态为空，请选择！";
                return -1;
            }
            
            if(!FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text,14))
            {
                this.Err = "床位电话最长为14位,请重新输入";
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWardNo.Text, 10))
            {
                this.Err = "病室号过长,请重新输入";
                return -1;
            }
            return 0;
        }

        public void SetBedInfo(FS.HISFC.Models.Base.Bed bedInfo)
        {
            if (bedInfo != null)
            {

                this.cmbNurse.Tag = bedInfo.NurseStation.ID;//护士站编号
                this.txtWardNo.Text = bedInfo.SickRoom.ID;//病区号
                this.txtBedNo.Text = bedInfo.ID;//病床号
                this.cmdBedGrade.Tag = bedInfo.BedGrade.ID.ToString();//病床等级
                this.cmdBedGrade.Text = bedInfo.BedGrade.Name;
                this.cmbBedStatus.Tag = bedInfo.Status.ID.ToString();//病床状态
                this.cmbBedStatus.Text = bedInfo.Status.Name;
                this.cmbBedWeave.Tag = bedInfo.BedRankEnumService.ID.ToString();//病床编制
                this.cmbBedWeave.Text = bedInfo.BedRankEnumService.Name;
                this.txtPhone.Text = bedInfo.Phone;//电话
                this.txtSort.Text = bedInfo.SortID.ToString();//顺序号
                this.txtOwn.Text = bedInfo.OwnerPc.Trim();//归属
                if (isUpdate)
                {
                    if (bedInfo.Status.ID.ToString() == "O" ||
                        bedInfo.Status.ID.ToString() == "R" ||
                        bedInfo.Status.ID.ToString() == "W") //占用床位不能修改状态
                    {
                        this.cmbBedStatus.Enabled = false;
                    }
                }
            }
        }
        FS.HISFC.Models.Base.Bed BedInfo = null;
        public void GetBedInfo(string bedNo)
        {
            if (BedInfo == null)
            {
                BedInfo = new FS.HISFC.Models.Base.Bed();
            }
            if (BedInfo.InpatientNO == "" || BedInfo.InpatientNO == null)
            {
                BedInfo.InpatientNO = "N";
            }
            BedInfo.NurseStation.ID = cmbNurse.Tag.ToString();//护士站编号
            
            BedInfo.SickRoom.ID = this.txtWardNo.Text.Trim();//病区号

            BedInfo.ID = bedNo.Trim();//病床号
            BedInfo.BedGrade.ID = this.cmdBedGrade.Tag.ToString();//病床等级
            BedInfo.Status.ID = this.cmbBedStatus.Tag.ToString();//病床状态
            BedInfo.BedRankEnumService.ID = this.cmbBedWeave.Tag.ToString();//病床编制
            BedInfo.Phone = txtPhone.Text.Trim();//电话
            BedInfo.SortID = int.Parse(this.txtSort.Text);//顺序号
            BedInfo.OwnerPc = this.txtOwn.Text.Trim();//归属
            //{AF7C0F4A-2521-460a-A3F9-A30D7A4EB942}  
            BedInfo.IsValid = true;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValid() != -1)
                {
                    
                    int iParm=0;

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    bed.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    
                    for (int i = 0; i <= int.Parse(tbCount.Text.Trim()) - 1; i++)
                    {
                        int bedNo;
                        string newbedNo = "";
                        bedNo = int.Parse(txtBedNo.Text) + i;
                        if (txtBedNo.Text.Length > bedNo.ToString().Length)
                        {
                            newbedNo = bedNo.ToString().PadLeft(txtBedNo.Text.Length , '0');
                        }
                        else
                        {
                            newbedNo = bedNo.ToString();
                        }
                        this.GetBedInfo(newbedNo);
                        //{6A55FE10-D8BA-40da-AFFE-B3020AC26716}
                        BedInfo.SortID = int.Parse(txtSort.Text)+i;

                        if (isUpdate)
                        {
                            iParm = bed.UpdateBedInfo(BedInfo);
                            //嵌入对其他系统或其他业务模块的信息传送桥接处理
                            string errInfo = "";
                            ArrayList alInfo = new ArrayList();
                            alInfo.Add(BedInfo);
                            int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Bed, ref errInfo);

                            if (param == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                Function.ShowMessage("床位添加失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            iParm = bed.CreatBedInfo(BedInfo);
                            //嵌入对其他系统或其他业务模块的信息传送桥接处理
                            string errInfo = "";
                            ArrayList alInfo = new ArrayList();
                            alInfo.Add(BedInfo);
                            int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Bed, ref errInfo);

                            if (param == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                Function.ShowMessage("床位添加失败，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    //{619F3CBF-7954-4d5e-B815-C66987E15C60}  床位数控制校验
                    if (Components.Manager.Classes.Function.BedVerify() == false)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }

                    if (iParm <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();;
                        MessageBox.Show(this.bed.Err);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();;
                        MessageBox.Show("保存成功！");
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(Err);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                System.Windows.Forms.SendKeys.Send("{tab}");
            }
        }

        private void frmBedManager_Load(object sender, EventArgs e)
        {
            if (!isUpdate)
            {
                this.cmbNurse.Tag = this.nurseStation;
                this.txtWardNo.Text = this.bedRoomNO;
            }
        }
    }
}