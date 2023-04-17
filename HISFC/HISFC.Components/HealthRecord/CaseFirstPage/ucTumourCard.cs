using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucTumourCard<br></br>
    /// [功能描述: 病案肿瘤信息录入]<br></br>
    /// [创 建 者: 张俊义]<br></br>
    /// [创建时间: 2007-04-20]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucTumourCard : UserControl
    {
        public ucTumourCard()
        {
            InitializeComponent();
        }

        #region  全局变量
        //当前活动下拉列表
        private FS.FrameWork.WinForms.Controls.PopUpListBox listBoxActive = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        //但前活动控件
        private System.Windows.Forms.Control contralActive = new Control();
        private DataTable dtTumour = new DataTable("肿瘤");
        private FS.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //配置文件的路径 
        private string filePath = Application.StartupPath + "\\profile\\ucTumourCard1.xml";
        //病人基本信息表
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        //单位列表
        FS.FrameWork.Public.ObjectHelper UnitListHelper = new FS.FrameWork.Public.ObjectHelper();
        //疗程列表
        FS.FrameWork.Public.ObjectHelper PeriodListHelper = new FS.FrameWork.Public.ObjectHelper();
        //结果列表
        FS.FrameWork.Public.ObjectHelper ResultListHelper = new FS.FrameWork.Public.ObjectHelper();
        //结果列表
        FS.FrameWork.Public.ObjectHelper TumStageListHelper = new FS.FrameWork.Public.ObjectHelper();
        //但前选中的信息
        private FS.FrameWork.Models.NeuObject selectObj;
        
        //肿瘤药物
        static  List<FS.HISFC.Models.Pharmacy.Item> druglist = new List<FS.HISFC.Models.Pharmacy.Item>();
        #endregion

        /// <summary>
        /// 病人信息
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }

        private string isHaveTum = string.Empty;
        /// <summary>
        /// 确认是否存在妇婴卡信息
        /// </summary>
        public string IsHavedTum
        {
            get
            {
                if (this.cmbIsHaveTum.Tag == null)
                {
                    this.isHaveTum = string.Empty;
                }
                else
                {
                    this.isHaveTum = this.cmbIsHaveTum.Tag.ToString();
                }
                return this.isHaveTum;
            }
            set
            {
                this.isHaveTum = value;
                this.cmbIsHaveTum.Tag = this.isHaveTum;
            }
        }
        #region 肿瘤主表操作函数
        
        #region 回车事件
        private void gy1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txttime1.Focus();
            }
        }

        private void time1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtday1.Focus();
            }
        }

        private void day1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtbegindate1.Focus();
            }
        }

        private void begin_date1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtenddate1.Focus();
            }
        }

        private void end_date1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtgy2.Focus();
            }
        }

        private void gy2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txttime2.Focus();
            }
        }

        private void time2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtday2.Focus();
            }
        }

        private void day2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtbegindate2.Focus();
            }
        }

        private void begin_date2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtenddate2.Focus();
            }
        }

        private void end_date2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtgy3.Focus();
            }
        }

        private void gy3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txttime3.Focus();
            }
        }

        private void time3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtday3.Focus();
            }
        }

        private void day3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtbegindate3.Focus();
            }
        }

        private void begin_date3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtenddate3.Focus();
            }
        }

        private void end_date3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCmodeid.Focus();
            }
        }
        #endregion
        /// <summary>
        /// 设置列下拉列表
        /// </summary>
        private void initList2()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                //肿瘤分期类型
                ArrayList TumourTypeList = con.GetList("CASETUMOURTYPE");
                this.txtTumourType.AddItems(TumourTypeList);
                //分期
                ArrayList TumourStageList = con.GetList("CASETUMOURSTAGE");
                this.txtTumourStage.AddItems(TumourStageList);
                TumStageListHelper.ArrayObject = TumourStageList;
                //部位
                ArrayList PositionList = con.GetList("CASEPOSITION");
                this.txtPosition.AddItems(PositionList);
                //放疗方式 
                ArrayList RmodeidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATETYPE);
                this.txtRmodeid.AddItems(RmodeidList);

                //放疗程式 
                ArrayList RprocessidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATEPERIOD);
                this.txtRprocessid.AddItems(RprocessidList);

                //放疗装置
                ArrayList RdeviceidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATEDEVICE);
                this.txtRdeviceid.AddItems(RdeviceidList);

                //化疗方式
                ArrayList CmodeidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CHEMOTHERAPY);
                this.txtCmodeid.AddItems(CmodeidList);

                //化疗方法
                ArrayList CmethodList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CHEMOTHERAPYWAY);
                this.txtCmethod.AddItems(CmethodList);
                //从常数表中获取是否需要肿瘤卡有无选择
                ArrayList listHavedTum = con.GetList("CASEHAVEDTUM");
                if (listHavedTum != null && listHavedTum.Count > 0)
                {
                    this.label31.Visible = true;
                    this.cmbIsHaveTum.Visible = true;
                }
                else
                {
                    this.label31.Visible = false;
                    this.cmbIsHaveTum.Visible = false;
                }
                ArrayList Havedlist = con.GetList("CASENOTORHAVED");
                if (Havedlist != null)
                {
                    this.cmbIsHaveTum.AddItems(Havedlist);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 设置下拉列表的格式
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private int InitList(FS.FrameWork.WinForms.Controls.PopUpListBox listBox, ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }
            try
            {
                //加载列表
                listBox.AddItems(list);
                listBox.Visible = false;
                Controls.Add(listBox);
                //隐藏
                listBox.Hide();
                //设置边框
                listBox.BorderStyle = BorderStyle.FixedSingle;
                listBox.BringToFront();
                //单击事件
                listBox.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(ListBox_SelectItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 选择项目列表 按键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ListBox_SelectItem(Keys key)
        {
            GetSelectItem();
            return 0;
        }
        /// <summary>
        /// 获取选中得项
        /// </summary>
        /// <returns></returns>
        private int GetSelectItem()
        {
            int rtn = listBoxActive.GetSelectedItem(out selectObj);
            if (selectObj == null)
            {
                return -1;
            }
            if (selectObj.ID != "")
            {
                this.contralActive.Tag = selectObj.ID;
                this.contralActive.Text = selectObj.Name;
            }
            else
            {
                this.contralActive.Tag = null;
            }
            this.listBoxActive.Visible = false;
            return rtn;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (listBoxActive != null) //有下拉列表的 
                {
                    if (listBoxActive.Visible == true)
                    {
                        GetSelectItem();
                    }
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Qty) //没有下拉列表的  注意要用else 否则会发生一次走两格的事情
                {
                    if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                    {
                        this.fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                    }
                    else
                    {
                        this.AddRow();
                    }
                }
            }
            if (keyData == Keys.Escape)
            {
                listBoxActive.Visible = false;
            }
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 设置ICDList的可见性
        /// </summary>
        /// <param name="result"></param>
        private void ListBoxActiveVisible(bool result)
        {
            if (result)
            {
                //				int i = contral.Top +contral.Height + ICDListBox.Height;
                //				if(i <= this.Height)
                //					ICDListBox.Location=new System.Drawing.Point(contral.Left, i - ICDListBox.Height);				
                //				else
                //					ICDListBox.Location=new System.Drawing.Point(contral.Left, contral.Top - ICDListBox.Height);
                listBoxActive.Location = new System.Drawing.Point(contralActive.Location.X, contralActive.Location.Y + contralActive.Height + 2);
                listBoxActive.SelectNone = true;
                listBoxActive.Width = 100;
            }
            else
            {

            }
            listBoxActive.BringToFront();
            try
            {
                if (contralActive.Text != "")
                {
                    listBoxActive.Filter(contralActive.Text);
                }
                else
                {
                    listBoxActive.Filter(contralActive.Text);
                    listBoxActive.SelectedIndex = -1;
                }
            }
            catch { }
            listBoxActive.Visible = result;
        }
        /// <summary>
        /// 有下拉列表的textBox 在改动时筛选数据
        /// </summary>
        private void ListFilter()
        {
            try
            {
                listBoxActive.Filter(contralActive.Text);
            }
            catch { }
        }
        /// <summary>
        /// 将实体中的数据显示到界面上 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int ConvertInfoToPanel(FS.HISFC.Models.HealthRecord.Tumour info)
        {
            try
            {
                txtTumourType.Tag = info.Tumour_Type;//肿瘤分期类型
                txtTumourT.Text = info.Tumour_T;//T
                txtTumourN.Text = info.Tumour_N;//N
                txtTumourM.Text = info.Tumour_M;//M
                if (info.Tumour_Stage==null || TumStageListHelper.GetName(info.Tumour_Stage) == null
                    || TumStageListHelper.GetName(info.Tumour_Stage) == "")
                {
                    txtTumourStage.Text = info.Tumour_Stage;//分期
                }
                else
                {
                    txtTumourStage.Tag = info.Tumour_Stage;//分期
                }
                txtRmodeid.Tag = info.Rmodeid;//放疗方式
                txtRprocessid.Tag = info.Rprocessid;//放疗程式
                txtRdeviceid.Tag = info.Rdeviceid;//放疗装置
                txtgy1.Text = info.Gy1.ToString();//原发灶计量
                txttime1.Text = info.Time1.ToString();//原发次数
                txtday1.Text = info.Day1.ToString();
                if (info.BeginDate1 != System.DateTime.MinValue)
                {
                    dtbegindate1.Value = info.BeginDate1;
                }
                else
                {
                    dtbegindate1.Value = System.DateTime.Now;
                }
                if (info.EndDate1 != System.DateTime.MinValue)
                {
                    dtenddate1.Value = info.EndDate1;
                }
                else
                {
                    dtenddate1.Value = System.DateTime.Now;
                }
                txtgy2.Text = info.Gy2.ToString(); //区域淋巴结
                txttime2.Text = info.Time2.ToString();
                txtday2.Text = info.Day2.ToString();
                if (info.BeginDate2 != System.DateTime.MinValue)
                {
                    dtbegindate2.Value = info.BeginDate2;
                }
                else
                {
                    dtbegindate2.Value = System.DateTime.Now;
                }
                if (info.EndDate2 != System.DateTime.MinValue)
                {
                    dtenddate2.Value = info.EndDate2;
                }
                else
                {
                    dtenddate2.Value = System.DateTime.Now;
                }
                txtPosition.Text = info.Position;//转移灶位置
                txtgy3.Text = info.Gy3.ToString();//转移灶计量
                txttime3.Text = info.Time3.ToString();
                txtday3.Text = info.Day3.ToString();
                if (info.BeginDate3 != System.DateTime.MinValue)
                {
                    dtbegindate3.Value = info.BeginDate3;
                }
                else
                {
                    dtbegindate3.Value = System.DateTime.Now;
                }
                if (info.EndDate3 != System.DateTime.MinValue)
                {
                    dtenddate3.Value = info.EndDate3;
                }
                else
                {
                    dtenddate3.Value = System.DateTime.Now;
                }
                txtCmodeid.Tag = info.Cmodeid;//化疗方式
                txtCmethod.Tag = info.Cmethod;//化疗方法
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 数据校验  校验失败返回 -1 成功返回 1 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int ValueTumourSate(FS.HISFC.Models.HealthRecord.Tumour info)
        {
            if (info == null)
            {
                MessageBox.Show("肿瘤信息为空");
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Rmodeid,20))
            {
                MessageBox.Show("放疗方式 编码过长");
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Rprocessid, 20))
            {
                MessageBox.Show("放疗程式 编码过长");
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Rdeviceid,20))
            {
                MessageBox.Show("放疗装置 编码过长");
                return -1;
            }
            //化疗方式    
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Cmodeid,20))
            {
                MessageBox.Show("放疗装置 编码过长");
                return -1;
            }
            //化疗方法 
            if (!FS.FrameWork.Public.String.ValidMaxLengh(info.Cmethod,20))
            {
                MessageBox.Show("放疗装置 编码过长");
                return -1;
            }
            if (info.Gy1 > (decimal)9999.99)
            {
                MessageBox.Show("原发灶计量 过大");
                return -1;
            }
            if (info.Gy1 < 0)
            {
                MessageBox.Show("原发灶计量 不能小于零");
                return -1;
            }
            if (info.Time1 < 0)
            {
                MessageBox.Show("原发灶次数 不能小于零");
                return -1;
            }
            if (info.Time1 > (decimal)9999.99)
            {
                MessageBox.Show("原发灶次数 过大");
                return -1;
            }
            if (info.Day1 < 0)
            {
                MessageBox.Show("原发灶天数 不能小于零");
                return -1;
            }
            if (info.Day1 > (decimal)9999.99)
            {
                MessageBox.Show("原发灶天数 过大");
                return -1;
            }
            if (info.Gy2 < 0)
            {
                MessageBox.Show("区域淋巴结计量 不能小于零");
                return -1;
            }
            if (info.Gy2 > (decimal)9999.99)
            {
                MessageBox.Show("区域淋巴结计量 过大");
                return -1;
            }
            if (info.Time2 < 0)
            {
                MessageBox.Show("区域淋巴结次数 不能小于零");
                return -1;
            }
            if (info.Time2 > (decimal)9999.99)
            {
                MessageBox.Show("区域淋巴结次数 过大");
                return -1;
            }
            if (info.Day2 < 0)
            {
                MessageBox.Show("区域淋巴结天数 不能小于零");
                return -1;
            }

            if (info.Day2 > (decimal)9999.99)
            {
                MessageBox.Show("区域淋巴结天数 过大");
                return -1;
            }

            if (info.Gy3 < 0)
            {
                MessageBox.Show("转移灶计量计量 不能小于零");
                return -1;
            }

            if (info.Gy3 > (decimal)9999.99)
            {
                MessageBox.Show("转移灶计量计量 过大");
                return -1;
            }

            if (info.Time3 < 0)
            {
                MessageBox.Show("转移灶计量次数 不能小于零");
                return -1;
            }
            if (info.Time3 > (decimal)9999.99)
            {
                MessageBox.Show("转移灶计量次数 过大");
                return -1;
            }

            if (info.Day3 < 0)
            {
                MessageBox.Show("转移灶计量天数 不能小于零");
                return -1;
            }

            if (info.Day3 > (decimal)9999.99)
            {
                MessageBox.Show("转移灶计量天数 过大");
                return -1;
            }
            if ((info.Tumour_Type!=null ||info.Tumour_Stage!=null || info.Rmodeid != null || info.Rprocessid != null || info.Rdeviceid != null || info.Cmodeid != null || info.Cmethod != null)
               && (info.Tumour_Type != "" || info.Tumour_Stage != "" || info.Rmodeid != "" || info.Rprocessid != "" || info.Rdeviceid != "" || info.Cmodeid != "" || info.Cmethod != "")
                )
            {
                return 2;
            }
            return 1;
        }
        /// <summary>
        /// 将界面上的数据 收集到实体上 
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.HealthRecord.Tumour GetTumourInfo()
        {
            FS.HISFC.Models.HealthRecord.Tumour info = new FS.HISFC.Models.HealthRecord.Tumour();

            try
            {
                info.InpatientNo = this.patientInfo.ID; //住院流水号
                //肿瘤分期类型
                if (txtTumourType.Tag != null)
                {
                    info.Tumour_Type = this.txtTumourType.Tag.ToString();
                }
                else
                {
                    info.Tumour_Type = string.Empty;
                }
                //T
                info.Tumour_T = this.txtTumourT.Text.Trim();
                //N
                info.Tumour_N = this.txtTumourN.Text.Trim();
                //M
                info.Tumour_M = this.txtTumourM.Text.Trim();
                //分期
                if (this.txtTumourStage.Tag != null && this.txtTumourStage.Tag.ToString() != "" && this.txtTumourStage.Text.Trim()!="")
                {
                    info.Tumour_Stage = this.txtTumourStage.Tag.ToString();
                }
                else
                {
                    info.Tumour_Stage = this.txtTumourStage.Text;
                }

                if (txtRmodeid.Tag != null && txtRmodeid.Text.Trim()!="")
                {
                    info.Rmodeid = txtRmodeid.Tag.ToString();//放疗方式
                }
                else
                {
                    info.Rmodeid = string.Empty;
                }
                if (txtRprocessid.Tag != null && txtRprocessid.Text.Trim()!="")
                {
                    info.Rprocessid = txtRprocessid.Tag.ToString();//放疗程式
                }
                else
                {
                    info.Rprocessid = string.Empty;
                }
                if (txtRdeviceid.Tag != null && txtRdeviceid.Text.Trim()!="")
                {
                    info.Rdeviceid = txtRdeviceid.Tag.ToString();//放疗装置
                }
                else
                {
                    info.Rdeviceid = string.Empty;
                }
                info.Gy1 = FS.FrameWork.Function.NConvert.ToDecimal(txtgy1.Text);//原发灶计量
                info.Time1 = FS.FrameWork.Function.NConvert.ToDecimal(txttime1.Text);//原发次数
                info.Day1 = FS.FrameWork.Function.NConvert.ToDecimal(txtday1.Text);
                info.BeginDate1 = dtbegindate1.Value;
                info.EndDate1 = dtenddate1.Value;
                info.Gy2 = FS.FrameWork.Function.NConvert.ToDecimal(txtgy2.Text); //区域淋巴结
                info.Time2 = FS.FrameWork.Function.NConvert.ToDecimal(txttime2.Text);
                info.Day2 = FS.FrameWork.Function.NConvert.ToDecimal(txtday2.Text);
                info.BeginDate2 = dtbegindate2.Value;
                info.EndDate2 = dtenddate2.Value;
                info.Position = this.txtPosition.Text.Trim();//转移灶位置
                info.Gy3 = FS.FrameWork.Function.NConvert.ToDecimal(txtgy3.Text);//转移灶计量
                info.Time3 = FS.FrameWork.Function.NConvert.ToDecimal(txttime3.Text);
                info.Day3 = FS.FrameWork.Function.NConvert.ToDecimal(txtday3.Text);
                info.BeginDate3 = dtbegindate3.Value;
                info.EndDate3 = dtenddate3.Value;
                if (txtCmodeid.Tag != null && txtCmodeid.Text.Trim()!="")
                {
                    info.Cmodeid = txtCmodeid.Tag.ToString();//化疗方式
                }
                else
                {
                    info.Cmodeid = string.Empty;
                }
                if (txtCmethod.Tag != null && txtCmethod.Text.Trim()!="")
                {
                    info.Cmethod = txtCmethod.Tag.ToString();//化疗方法
                }
                else
                {
                    info.Cmethod = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return info;
        }
        /// <summary>
        /// 设置 只读性 
        /// </summary>
        /// <param name="type"></param>
        private void SetTumourReadOnly(bool type)
        {
            txtTumourType.ReadOnly = type;//肿瘤分期类型
            txtTumourT.ReadOnly = type;//原发肿瘤
            txtTumourN.ReadOnly = type;//淋巴转移
            txtTumourM.ReadOnly = type;//远程转移
            txtTumourStage.ReadOnly = type;//分期
            txtRmodeid.ReadOnly = type;//放疗方式
            txtRprocessid.ReadOnly = type;//放疗程式
            txtRdeviceid.ReadOnly = type;//放疗装置
            txtgy1.ReadOnly = type;//原发灶计量
            txttime1.ReadOnly = type;//原发次数
            txtday1.ReadOnly = type;
            dtbegindate1.Enabled = !type;
            dtenddate1.Enabled = !type;
            txtgy2.ReadOnly = type; //区域淋巴结
            txttime2.ReadOnly = type;
            txtday2.ReadOnly = type;
            dtbegindate2.Enabled = !type;
            dtenddate2.Enabled = !type;
            txtPosition.ReadOnly = type;//转移灶位置
            txtgy3.ReadOnly = type;//转移灶计量
            txttime3.ReadOnly = type;
            txtday3.ReadOnly = type;
            dtbegindate3.Enabled = !type;
            dtenddate3.Enabled = !type;
            txtCmodeid.ReadOnly = type;//化疗方式
            txtCmethod.ReadOnly = type;//化疗方法
            this.btAdd.Enabled = !type;
            this.btDelete.Enabled = !type;
        }
        /// <summary>
        /// 清空控件值
        /// </summary>
        private void ClearTumourInfo()
        {
            txtTumourType.Tag = null;
            txtTumourT.Text = "";
            txtTumourN.Text = "";
            this.txtTumourM.Text = "";
            this.txtTumourStage.Tag = null;
            this.txtPosition.Tag = null;

            txtRmodeid.Text = "";//放疗方式
            txtRmodeid.Tag = null;
            txtRprocessid.Text = "";//放疗程式
            txtRprocessid.Tag = null;
            txtRdeviceid.Text = "";//放疗装置
            txtRdeviceid.Tag = null;
            txtgy1.Text = "";//原发灶计量
            txttime1.Text = "";//原发次数
            txtday1.Text = "";
            dtbegindate1.Value = System.DateTime.Now;
            dtenddate1.Value = System.DateTime.Now;
            txtgy2.Text = ""; //区域淋巴结
            txttime2.Text = "";
            txtday2.Text = "";
            dtbegindate2.Value = System.DateTime.Now;
            dtenddate2.Value = System.DateTime.Now;
            txtgy3.Text = "";//转移灶计量
            txttime3.Text = "";
            txtday3.Text = "";
            dtbegindate3.Value = System.DateTime.Now;
            dtenddate3.Value = System.DateTime.Now;
            txtCmodeid.Tag = null;//化疗方式
            txtCmodeid.Text = "";//化疗方式
            txtCmethod.Tag = null;//化疗方法
            txtCmethod.Text = "";//化疗方法
        }
        #endregion

        #region  肿瘤明细操作函数
        /// <summary>
        /// 设置活动单元格
        /// </summary>
        public void SetActiveCells()
        {
            try
            {
                this.fpEnter1_Sheet1.SetActiveCell(0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 限定格的宽度很可见性 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpEnter1_Sheet1.Columns[0].Width = 63; //起始日期
            this.fpEnter1_Sheet1.Columns[1].Width = 63;//结束日期
            this.fpEnter1_Sheet1.Columns[2].Width = 129;//药物名称
            this.fpEnter1_Sheet1.Columns[6].Width = 60;//用量
            this.fpEnter1_Sheet1.Columns[3].Width = 40; //单位
            this.fpEnter1_Sheet1.Columns[4].Width = 40; //疗程
            this.fpEnter1_Sheet1.Columns[5].Width = 80; //结果
            this.fpEnter1_Sheet1.Columns[7].Width = 100; //药品编码
            this.fpEnter1_Sheet1.Columns[7].Locked = true;//药品编码
            this.fpEnter1_Sheet1.Columns[8].Visible = false; //序号
        }
        /// <summary>
        /// 清空原有的数据
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtTumour != null)
            {
                this.dtTumour.Clear();
                ClearTumourInfo();
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("肿瘤表为null");
            }
            return 1;
        }
        public int SetReadOnly(bool type)
        {
            if (type)
            {
                this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                SetTumourReadOnly(type);
            }
            else
            {
                this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                SetTumourReadOnly(type);
            }
            return 0;
        }
        /// <summary>
        /// 校验数据的合法性。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ValueState(ArrayList list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.HealthRecord.TumourDetail obj in list)
            {
                if (obj.InpatientNO == "" || obj.InpatientNO == null)
                {
                    MessageBox.Show("肿瘤信息 住院流水号不能为空");
                    return -1;
                }

                if (obj.InpatientNO.Length > 14)
                {
                    MessageBox.Show("肿瘤信息 住院流水号过长");
                    return -1;
                }
                if (obj.DrugInfo.Name == null || obj.DrugInfo.Name == "")
                {
                    MessageBox.Show("肿瘤信息 药物名称不能为空");
                    return -1;
                }
                else if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DrugInfo.Name, 50))
                {
                    MessageBox.Show("肿瘤信息 药物名称过长");
                    return -1;
                }
                if (obj.Qty == 0)
                {
                    MessageBox.Show("肿瘤信息" + obj.DrugInfo.Name + " 计量不能为零");
                    return -1;
                }
                else if (obj.Qty > 10000)
                {
                    MessageBox.Show("肿瘤信息" + obj.DrugInfo.Name + " 计量过大");
                    return -1;
                }
                else if (obj.Qty < 0)
                {
                    MessageBox.Show("肿瘤信息" + obj.DrugInfo.Name + " 计量不能小于零");
                    return -1;
                }

                if (obj.Unit == null || obj.Unit == "")
                {
                    MessageBox.Show("请填写 肿瘤信息" + obj.DrugInfo.Name + " 的单位");
                    return -1;
                }
                else if (obj.Unit.Length > 8)
                {
                    MessageBox.Show("肿瘤信息" + obj.DrugInfo.Name + " 单位长度过大");
                    return -1;
                }
            }
            return 0;
        }
        /// <summary>
        /// 保存对表做的所有修改
        /// </summary>
        /// <returns></returns>
        public int fpEnterSaveChanges()
        {
            try
            {
                this.dtTumour.AcceptChanges();
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 将保存完的数据回写到表中
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int fpEnterSaveChanges(ArrayList list)
        {
            AddInfoToTable(list);
            dtTumour.AcceptChanges();
            LockFpEnter();
            return 0;
        }
        public int AddInfoToTable(ArrayList list)
        {
            if (this.dtTumour != null)
            {
                this.dtTumour.Clear();
                this.dtTumour.AcceptChanges();
            }
            if (list == null)
            {
                return -1;
            }

            //循环插入数据
            foreach (FS.HISFC.Models.HealthRecord.TumourDetail info in list)
            {
                DataRow row = dtTumour.NewRow();
                SetRow(info, row);
                dtTumour.Rows.Add(row);
            }
            //更改标志
            dtTumour.AcceptChanges();
            LockFpEnter();
            return 0;
        }
        /// <summary>
        /// 返回当前数据行数
        /// </summary>
        /// <returns></returns>
        public int GetfpSpreadRowCount()
        {
            return fpEnter1_Sheet1.Rows.Count;
        }
        /// <summary>
        /// 如果reset 为真 则清空现有数据 并保存更改  为假 只是保存当前更改
        /// creator:zhangjunyi@FS.com
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        public bool Reset(bool reset)
        {
            if (reset)
            {
                //清空数据 保存更改 
                if (dtTumour != null)
                {
                    dtTumour.Clear();
                    dtTumour.AcceptChanges();
                }
            }
            else
            {
                //保存更改
                dtTumour.AcceptChanges();
            }
            return true;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitInfo()
        {
            try
            {
                //初始化表
                InitDateTable();
                //设置下拉列表
                this.initList();
                //设置下拉列表
                initList2();
                fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitDateTable()
        {
            try
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type floatType = typeof(System.Single);

                dtTumour.Columns.AddRange(new DataColumn[]{
														   new DataColumn("起始日期", dtType),	//0
                                                           new DataColumn("结束日期", dtType),	//1
														   new DataColumn("药物名称", strType),	 //2
														   new DataColumn("单位", strType),//3
														   new DataColumn("疗程", strType),//4
														   new DataColumn("结果", strType),//5
														   new DataColumn("用量", strType),//6
														   new DataColumn("药品编码", strType),//7
														   new DataColumn("序号", intType)});//8
                //绑定数据源
                this.fpEnter1_Sheet1.DataSource = dtTumour;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool GetList(string strType, ArrayList list)
        {
            try
            {
                this.fpEnter1.StopCellEditing();
                foreach (DataRow dr in this.dtTumour.Rows)
                {
                    dr.EndEdit();
                }
                switch (strType)
                {
                    case "A":
                        //增加的数据
                        DataTable AddTable = this.dtTumour.GetChanges(DataRowState.Added);
                        GetChangeInfo(AddTable, list);
                        break;
                    case "M":
                        DataTable ModTable = this.dtTumour.GetChanges(DataRowState.Modified);
                        GetChangeInfo(ModTable, list);
                        break;
                    case "D":
                        DataTable DelTable = this.dtTumour.GetChanges(DataRowState.Deleted);
                        if (DelTable != null)
                        {
                            DelTable.RejectChanges();
                        }
                        GetChangeInfo(DelTable, list);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 获取修改过的信息
        /// </summary>
        /// <returns></returns>
        private bool GetChangeInfo(DataTable tempTable, ArrayList list)
        {
            if (tempTable == null)
            {
                return true;
            }
            try
            {
                FS.HISFC.Models.HealthRecord.TumourDetail info = null;
                foreach (DataRow row in tempTable.Rows)
                {
                    info = new FS.HISFC.Models.HealthRecord.TumourDetail();
                    info.InpatientNO = this.patientInfo.ID;
                    if (row["起始日期"] != DBNull.Value)
                    {
                        info.CureDate = FS.FrameWork.Function.NConvert.ToDateTime(row["起始日期"].ToString());
                    }
                    if (row["结束日期"] != DBNull.Value)
                    {
                        info.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row["结束日期"].ToString());
                    }
                    if (row["药物名称"] != DBNull.Value)
                    {
                        info.DrugInfo.Name = row["药物名称"].ToString().Replace("'", "’"); ;//1
                    }
                    if (row["用量"] != DBNull.Value)
                    {
                        info.Qty = FS.FrameWork.Function.NConvert.ToDecimal(row["用量"]);//1
                    }
                    if (row["单位"] != DBNull.Value)
                    {
                        info.Unit = this.UnitListHelper.GetID(row["单位"].ToString());//2
                    }
                    if (row["疗程"] != DBNull.Value)
                    {
                        info.Period = this.PeriodListHelper.GetID(row["疗程"].ToString());//2
                    }
                    if (row["结果"] != DBNull.Value)
                    {
                        info.Result = this.ResultListHelper.GetID(row["结果"].ToString());//2
                    }
                    if (row["药品编码"] != DBNull.Value)
                    {
                        info.DrugInfo.ID = row["药品编码"].ToString();//2
                    }
                    if (row["序号"] != DBNull.Value)
                    {
                        info.HappenNO = FS.FrameWork.Function.NConvert.ToInt32(row["序号"]);//1
                    }
                    list.Add(info);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 删除当前行 
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            this.fpEnter1.SetAllListBoxUnvisible();
            this.fpEnter1.EditModePermanent = false;
            this.fpEnter1.EditModeReplace = false;
            if (fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);
            }
            if (fpEnter1_Sheet1.Rows.Count == 0)
            {
                this.fpEnter1.SetAllListBoxUnvisible();
            } 
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// 删除空白的行
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            this.fpEnter1.SetAllListBoxUnvisible();
            this.fpEnter1.EditModePermanent = false;
            this.fpEnter1.EditModeReplace = false;
            if (fpEnter1_Sheet1.Rows.Count == 1)
            {
                //第一行编码为空 
                if (fpEnter1_Sheet1.Cells[0, 1].Text == "")
                {
                    fpEnter1_Sheet1.Rows.Remove(0, 1);
                }
            } 
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// 查询并显示数据
        /// </summary>
        /// <returns>出错返回 －1 正常 0 不允许有病案1  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            if (patient == null)
            {
                return -1;
            }
            patientInfo = patient;
            if (patientInfo.CaseState == "0")
            {
                //不允许有病案
                return 1;
            }
            FS.HISFC.BizLogic.HealthRecord.Tumour tumour = new FS.HISFC.BizLogic.HealthRecord.Tumour();
            //查询符合条件的数据
            ArrayList list = tumour.QueryTumourDetail(patient.ID);
            AddInfoToTable(list);
            FS.HISFC.Models.HealthRecord.Tumour obj = tumour.GetTumour(patient.ID);
            if (obj == null)
            {
                MessageBox.Show("获取肿瘤信息出错");
                return -1;
            }
            this.ConvertInfoToPanel(obj);
            return 0;

        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="info"></param>
        private void SetRow(FS.HISFC.Models.HealthRecord.TumourDetail info, DataRow row)
        {
            row["起始日期"] = info.CureDate;//0
            row["结束日期"] = info.OperInfo.OperTime;//1
            row["药物名称"] = info.DrugInfo.Name.Replace("’", "'"); ;//2
            row["用量"] = info.Qty;//3
            row["单位"] = UnitListHelper.GetName(info.Unit);            //4
            row["疗程"] = PeriodListHelper.GetName(info.Period);//5
            row["结果"] = ResultListHelper.GetName(info.Result);//6
            row["药品编码"] = info.DrugInfo.ID;//7
            row["序号"] = info.HappenNO;//8
        }
        private enum Col
        {
            colTime = 0,//起始日期
            colEndTime=1,//结束日期
            DrugName = 2,//药品名称
            Unit = 3,//单位
            Preiod = 4,//疗程
            Result = 5,//结果
            Qty = 6,//剂量
            DrugCode = 7 //药品编码

        }
        /// <summary>
        /// 设置列下拉列表
        /// </summary>
        private void initList()
        {
            try
            {
                FS.HISFC.BizLogic.HealthRecord.Tumour da = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                FS.HISFC.BizProcess.Integrate.Pharmacy item = new FS.HISFC.BizProcess.Integrate.Pharmacy();
                fpEnter1.SetWidthAndHeight(200, 200);
                fpEnter1.SelectNone = true;
                //FS.HISFC.BizProcess.Integrate.Management.Pharmacy.Item itemMgr = new FS.HISFC.BizProcess.Pharmacy.Item();
                //药品信息
                if (druglist == null || druglist.Count == 0)
                {
                    druglist = item.QueryItemList(true);
                }
                ArrayList temp = new ArrayList(druglist.ToArray());
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.DrugName, temp);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.DrugCode, temp);
                //药品信息不显示ID号
                this.fpEnter1.SetIDVisiable(this.fpEnter1_Sheet1, (int)Col.DrugName, false);
                this.fpEnter1.SetIDVisiable(this.fpEnter1_Sheet1, (int)Col.DrugCode, false);
                //单位列表
                ArrayList UnitList = con.GetList(FS.HISFC.Models.Base.EnumConstant.DOSEUNIT);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Unit, UnitList);
                UnitListHelper.ArrayObject = UnitList;

                //疗程列表 
                ArrayList PeriodList = con.GetList(FS.HISFC.Models.Base.EnumConstant.PERIODOFTREATMENT);// da.GetPeriodList();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Preiod, PeriodList);
                PeriodListHelper.ArrayObject = PeriodList;

                //j结果列表
                ArrayList ResultList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATERESULT);// da.GetResultList();
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)Col.Result, ResultList);
                ResultListHelper.ArrayObject = ResultList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ucTumourCard_Load(object sender, System.EventArgs e)
        {
            //定义响应按键事件
            fpEnter1.KeyEnter+=new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
            fpEnter1.SetItem +=new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            fpEnter1.ShowListWhenOfFocus = true;
            fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }
        /// <summary>
        /// 按键响应处理
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                //				MessageBox.Show("Enter,可以自己添加处理事件，比如跳到下一cell");
                //回车
                if (this.fpEnter1.ContainsFocus)
                {
                    int i = this.fpEnter1_Sheet1.ActiveColumnIndex;
                    if (i == (int)Col.DrugName || i == (int)Col.Unit || i == (int)Col.Preiod || i == (int)Col.Result || i == (int)Col.DrugCode)
                    {
                        ProcessDept();
                    }
                    else if (i == (int)Col.colTime)
                    {
                        this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 1);
                    }
                    else if (i == (int)Col.Qty)
                    {
                        if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                        {
                            this.fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                        }
                        else
                        {
                            this.AddRow();
                        }
                    }
                }
            }
            else if (key == Keys.Up)
            {
                //				MessageBox.Show("Up,可以自己添加处理事件，比如无下拉列表时，跳到下列，显示下拉控件时，在下拉控件上下移动");
            }
            else if (key == Keys.Down)
            {
                //				MessageBox.Show("Down，可以自己添加处理事件，比如无下拉列表时，跳到上列，显示下拉控件时，在下拉控件上下移动");
            }
            else if (key == Keys.Escape)
            {
                //				MessageBox.Show("Escape,取消列表可见");
            }
            return 0;
        }
        /// <summary>
        /// 添加一行项目
        /// </summary>
        /// <returns></returns>
        public int AddRow()
        {
            try
            {
                if (fpEnter1_Sheet1.Rows.Count < 1)
                {
                    //增加一行空值
                    DataRow row = dtTumour.NewRow();
                    row["序号"] = 1;
                    row["起始日期"] = System.DateTime.Now;
                    row["结束日期"] = System.DateTime.Now;
                    row["用量"] = 0;//2
                    dtTumour.Rows.Add(row);
                }
                else
                {
                    //增加一行
                    int j = fpEnter1_Sheet1.Rows.Count;
                    this.fpEnter1_Sheet1.Rows.Add(j, 1);
                    for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    {
                        fpEnter1_Sheet1.Cells[j, i].Value = fpEnter1_Sheet1.Cells[j - 1, i].Value;
                    }
                }
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.Rows.Count, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 添加一行项目
        /// </summary>
        /// <returns></returns>
        public int InsertRow()
        {
            try
            {
                if (this.fpEnter1_Sheet1.RowCount == 0)
                {
                    this.AddRow();
                }
                else
                {
                    //增加一行
                    int actRow = fpEnter1_Sheet1.ActiveRowIndex + 1;
                    this.fpEnter1_Sheet1.Rows.Add(actRow, 1);
                    //for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    //{
                    //    if (i == 0)
                    //    {
                    //        fpEnter1_Sheet1.Cells[actRow, i].Value = "其他诊断";
                    //    }
                    //    else
                    //    {
                    //        fpEnter1_Sheet1.Cells[actRow, i].Value = fpEnter1_Sheet1.Cells[actRow - 1, i].Value;
                    //    }
                    //}
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(actRow, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 选则选中的项
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }
        /// <summary>
        /// 处理回车操作 ，并且取出数据
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            int CurrentRow = fpEnter1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.DrugName)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.DrugName);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //药品名称
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                //药品编码
                fpEnter1_Sheet1.Cells[CurrentRow, (int)Col.DrugCode].Text = item.ID;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Unit);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.DrugName)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.DrugName);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //药品名称
                fpEnter1_Sheet1.ActiveCell.Text = item.ID;
                //药品编码
                fpEnter1_Sheet1.Cells[CurrentRow, (int)Col.DrugName].Text = item.Name;
                fpEnter1.Focus();
                //
                if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                {
                    this.fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                }
                //				else
                //				{
                //					this.AddRow();
                //				}
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Unit)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Unit);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //药品计量单位
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Preiod);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Preiod)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Preiod);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                // 疗程
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Result);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Result)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Result);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                //j结果
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Qty);
                //
                //				if(fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count -1)
                //				{
                //					this.fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex +1 ,0);
                //				}
                return 0;
            }

            return 0;
        }
        /// <summary>
        /// 设置网格得宽度 和是否打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            //			FS.Common.Controls.ucSetCol uc = new FS.Common.Controls.ucSetCol();
            //			uc.FilePath = this.filePath;
            //			uc.GoDisplay += new FS.Common.Controls.ucSetCol.DisplayNow(uc_GoDisplay);
            //			FS.neuFC.Interface.Classes.Function.PopShowControl(uc);
        }
        /// <summary>
        /// 调整fpSpread1_Sheet1的宽度等 保存后触发的事件
        /// </summary>
        private void uc_GoDisplay()
        {
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.Rows.Count > 0)
            {
                //删除当前行
                this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);
            }

        }
        #endregion  

        private void btAdd_Click(object sender, EventArgs e)
        {
            //this.AddRow();
            this.InsertRow();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            this.DeleteActiveRow();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }

    }
}
