using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using FS.HISFC.Models.Fee;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.BizProcess.Integrate.FeeInterface;
using FS.HISFC.Models.Base;
using FS.FrameWork.Function;
using FoShanDiseasePay.DataBase;
using FoShanDiseasePay.Jobs;
using System.Text.RegularExpressions;

namespace FoShanDiseasePay
{
    /// <summary>
    /// 出院志更新
    /// </summary>
    public partial class ucUpdateVest : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucUpdateVest()
        {
            InitializeComponent();


        }

        #region 事件
        /// 工具栏
        /// </summary>
        private ToolBarService toolBarService = new ToolBarService();

        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 患者入出转管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private string inPatientNo = "";
        /// <summary>
        /// 住院流水号
        /// </summary>
        public string InPatientNo
        {
            set { inPatientNo = value; }
        }

        public string PatientNo
        {
            set { this.txtNo.Text = value; }

        }
        public string Qty
        {
            set
            {
                this.lblQty.Text = value;
                txtNo_KeyDown(null, new KeyEventArgs(Keys.Enter) );

            }

        }
        /// <summary>
        /// 保存按钮是否可见
        /// </summary>
        public bool IsVise = false;

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //清屏
            this.Clear();
            if (this.IsShowSave)
            {
                this.QueryNEW();
            }
            
            base.OnLoad(e);
        }

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            this.toolBarService.AddToolButton("待上传患者", "待上传患者查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.toolBarService.AddToolButton("一键上传", "一键上传病种分值付费接口", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

       

            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //switch (e.ClickedItem.Text)
            //{
            //    case "待上传患者":
            //        this.Clear();
            //        break;
            //}

            base.ToolStrip_ItemClicked(sender, e);
        }

        private string strClNo = "";
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            this.txtRYQK.Text = "";
            this.txtRYQK.Enabled = true;
            this.txtZLGC.Text = "";
            this.txtZLGC.Enabled = true;
            this.txtCYQK.Text = "";
            this.txtCYQK.Enabled = true;
            this.txtCYYZ.Text = "";
            this.txtCYYZ.Enabled = true;
            this.txtSWJLRYQK.Text = "";
            this.txtSWJLRYQK.Enabled = true;
            this.txtSWJLZLGC.Text = "";
            this.txtSWJLZLGC.Enabled = true;
            this.lblPatientInfo.Text = "患者信息：";
        }
        public bool IsShowSave
        {
            get
            {
                return this.button1.Visible;
            }
            set
            {
                this.button1.Visible = value;
            }
        }

        public void QueryNEW()
        {
            this.button1.Visible = true;
            this.OnQuery(null, null); 
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Clear();
            string strNo = this.txtNo.Text.Trim();
            //strClNo = strNo;
            if (string.IsNullOrEmpty(strNo))
            {
                return -1;

            }
            if (string.IsNullOrEmpty(this.lblQty.Text.Trim()))
            {
                MessageBox.Show("请输入住院次数！");
                return -1;
            }
            strNo = strNo.PadLeft(10, '0');
            strClNo = this.getInpatientNo(strNo, this.lblQty.Text);
            if (string.IsNullOrEmpty(strClNo))
            {
                MessageBox.Show("找不到患者信息！");
                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo inPatientInfo = this.radtIntegrate.GetPatientInfomation(strClNo);
            if (inPatientInfo == null)
            {
                MessageBox.Show("找不到患者信息！");
                return -1;

            }

            this.lblPatientInfo.Text = "患者信息：" + inPatientInfo.Name + "  " + inPatientInfo.Sex.Name + "  所在科室：" + inPatientInfo.PVisit.PatientLocation.Dept.Name + "  入院时间：" + inPatientInfo.PVisit.InTime.ToString("yyyy年MM月dd日") + "  出院时间：" + inPatientInfo.PVisit.OutTime.ToString("yyyy年MM月dd日");


            string RYQK = "";
            string ZLGC = "";
            string CYQK = "";
            string CYYZ = "";
            string SWJLRYQK = "";
            string SWJLZLGC = "";

            RYQK = this.QueryRYQK(strClNo);
            ZLGC = this.QueryZLGC(strClNo);
            CYQK = this.QueryCYQK(strClNo);
            CYYZ = this.QueryCYYZ(strClNo);
            SWJLRYQK = this.QuerySWJLRYQK(strClNo);
            SWJLZLGC = this.QuerySWJLZLGC(strClNo);

            if (!string.IsNullOrEmpty(RYQK))
            {
                this.txtRYQK.Text = RYQK;
                this.txtRYQK.Enabled = false;
            }
            else
            {
                this.txtRYQK.Enabled = true;
            }

            if (!string.IsNullOrEmpty(ZLGC))
            {
                this.txtZLGC.Text = ZLGC;
                this.txtZLGC.Enabled = false;
            }
            else
            {
                this.txtZLGC.Enabled = true;
            }
            if (!string.IsNullOrEmpty(CYQK))
            {
                this.txtCYQK.Text = CYQK;
                this.txtCYQK.Enabled = false;
            }
            else
            {
                this.txtCYQK.Enabled = true;
            }
            if (!string.IsNullOrEmpty(CYYZ))
            {
                this.txtCYYZ.Text = CYYZ;
                this.txtCYYZ.Enabled = false;
            }
            else
            {
                this.txtCYYZ.Enabled = true;
            }
            if (!string.IsNullOrEmpty(SWJLRYQK))
            {
                this.txtSWJLRYQK.Text = SWJLRYQK;
                this.txtSWJLRYQK.Enabled = false;
            }
            else
            {
                this.txtSWJLRYQK.Enabled = true;
            }
            if (!string.IsNullOrEmpty(SWJLZLGC))
            {
                this.txtSWJLZLGC.Text = SWJLZLGC;
                this.txtSWJLZLGC.Enabled = false;
            }
            else
            {
                this.txtSWJLZLGC.Enabled = true;
            }
            return base.OnQuery(sender, neuObject);
        }

        public string getInpatientNo(string patientNo ,string qty)
        {
            string sql = "select a.inpatient_no from fin_ipr_inmaininfo a where a.patient_no = '{0}' and a.in_times = '{1}'";
            sql = string.Format(sql, patientNo, qty);
            string str = this.con.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                str = "";
            }

            return str;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            con.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //设置事务

            //if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            //{
            //}
            //else
            {
                if (this.txtRYQK.Enabled && !string.IsNullOrEmpty(this.txtRYQK.Text))
                {
                    if (this.UpdateRYQK(this.strClNo, this.txtRYQK.Text) <= 0)
                    {

                        if (this.InsertLog(strClNo, this.txtRYQK.Text, this.txtZLGC.Text, this.txtCYQK.Text, this.txtCYYZ.Text) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新出院志出错！");
                            return -1;
                        }
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //MessageBox.Show("更新入院情况出错！");
                        //return -1;
                    }
                }

                if (this.txtZLGC.Enabled && !string.IsNullOrEmpty(this.txtZLGC.Text))
                {
                    if (this.UpdateZLGC(this.strClNo, this.txtZLGC.Text) <= 0)
                    {

                        if (this.InsertLog(strClNo, this.txtRYQK.Text, this.txtZLGC.Text, this.txtCYQK.Text, this.txtCYYZ.Text) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新出院志出错！");
                            return -1;
                        }

                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //MessageBox.Show("更新诊疗过程出错！");
                        //return -1;
                    }
                }
                if (this.txtCYQK.Enabled && !string.IsNullOrEmpty(this.txtCYQK.Text))
                {
                    if (this.UpdateCYQK(this.strClNo, this.txtCYQK.Text) <= 0)
                    {


                        if (this.InsertLog(strClNo, this.txtRYQK.Text, this.txtZLGC.Text, this.txtCYQK.Text, this.txtCYYZ.Text) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新出院志出错！");
                            return -1;
                        }
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //MessageBox.Show("更新出院情况出错！");
                        //return -1;
                    }
                }
                if (this.txtCYYZ.Enabled && !string.IsNullOrEmpty(this.txtCYYZ.Text))
                {
                    if (this.UpdateCYYZ(this.strClNo, this.txtCYYZ.Text) <= 0)
                    {


                        if (this.InsertLog(strClNo, this.txtRYQK.Text, this.txtZLGC.Text, this.txtCYQK.Text, this.txtCYYZ.Text) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新出院志出错！");
                            return -1;
                        }
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        //MessageBox.Show("更新出院医嘱出错！");
                        //return -1;
                    }
                }
                if (this.txtSWJLRYQK.Enabled && !string.IsNullOrEmpty(this.txtSWJLRYQK.Text))
                {
                    if (this.UpdateSWJLRYQK(this.strClNo, this.txtSWJLRYQK.Text) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新死亡记录入院情况出错！");
                        return -1;
                    }
                }
                if (this.txtSWJLZLGC.Enabled && !string.IsNullOrEmpty(this.txtSWJLZLGC.Text))
                {
                    if (this.UpdateSWJLZLGC(this.strClNo, this.txtSWJLZLGC.Text) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新死亡记录诊疗过程出错！");
                        return -1;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("更新成功！");
            return base.OnSave(sender, neuObject);
        }
        private int InsertLog(string inpatienNo, string ryqk, string zlgc, string cyqk,string cyyz)
        {
            if (FS.FrameWork.Management.Connection.Hospital.Name != "佛山市第一人民医院禅城医院")
            {
                return -1;
            }
            string sql1 = @"insert into XSRCYJL
                             (A,B,C,D,E,F,H,I)
                             values 
                             ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
            sql1 = string.Format(sql1, inpatienNo, "",ryqk, zlgc, cyqk, cyyz,"","");

            string sql2 = @"update XSRCYJL
                             set C = '{1}',
                                 D = '{2}',
                                 E = '{3}',
                                 F = '{4}'
                             where A = '{0}'";
            sql2 = string.Format(sql2, inpatienNo, ryqk, zlgc, cyqk, cyyz);
            if (this.con.ExecNoQuery(sql1) <= 0)
            {
                return this.con.ExecNoQuery(sql2);
            }

            return 1;
        }

        /// <summary>
        /// 查询入院情况
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        public string QueryRYQK(string strNo)
        {
            string str = "";
            string sql = "";
            sql = @"SELECT FUN_GET_INPATIENT_INCONDITION('{0}') 入院情况 FROM DUAL ";
            if (false)
            {
                if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
                {
                    sql = @"
                            select nodevalue 
                            from (
                            select e.nodevalue
                            from datastore_emr e
                            where e.index1='{0}'
                            and e.nodename='入院情况'
                            and e.parentnodename='出院记录\入院\入院情况'
                            and e.datatype='出院相关'
                            and e.id = (select max(e1.id) from  datastore_emr e1
                                       where e1.index1='{0}'
                                        and e1.nodename='入院情况'
                                        and e1.parentnodename='出院记录\入院\入院情况'
                                        and e1.datatype='出院相关')
                            union all

                            select e.nodevalue
                            from datastore_emr e
                            where e.index1='{0}'
                            and e.nodename='入院情况'
                            and e.parentnodename='死亡记录\入院\入院情况'
                            and e.datatype='死亡相关'
                            and e.id = (select max(e1.id) from  datastore_emr e1
                                       where e1.index1='{0}'
                                        and e1.nodename='入院情况'
                                        and e1.parentnodename='死亡记录\入院\入院情况'
                                        and e1.datatype='死亡相关')
                            union all

                            select e.nodevalue
                            from datastore_emr e
                            where e.index1='{0}'
                            and e.nodename='入院情况'
                            and e.parentnodename='死亡记录\入院\入院情况'
                            and e.datatype='死亡相关'
                            and e.id = (select max(e1.id) from  datastore_emr e1
                                       where e1.index1='{0}'
                                        and e1.nodename='入院情况'
                                        and e1.parentnodename='二十四小时入院死亡记录\住院情况\入院情况'
                                        and e1.datatype='死亡相关')
                            )";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
                {
                    sql = @"select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}'
                        and e.nodename='入院情况'
                        and e.parentnodename='出院记录\入院\入院情况'
                        and e.datatype='出院相关'
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}'
                                    and e1.nodename='入院情况'
                                    and e1.parentnodename='出院记录\入院\入院情况'
                                    and e1.datatype='出院相关')";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
                {
                    sql = @"select i.value 
                      from rcd_record_item i,pt_inpatient_cure b
                      where i.element_id='363'
                      and b.id = i.inpatient_id
                      and b.inpatient_code= '{0}'";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
                {
                    sql = @"select i.value 
                      from rcd_record_item i,pt_inpatient_cure b
                      where i.element_id='363'
                      and b.id = i.inpatient_id
                      and b.inpatient_code= '{0}'";
                }
            }
            sql = string.Format(sql, strNo);
            str = this.con.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                str = "";
            }
            return str;
        }
        /// <summary>
        /// 查询诊疗过程
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        public string QueryZLGC(string strNo)
        {
            string str = "";
            string sql = "";
            sql = @"SELECT FUN_GET_INPATIENT_INPROGRESS('{0}') 诊疗过程 FROM DUAL ";
            if (false)
            {
                if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
                {
                    sql = @"
                        select nodevalue
                        from (select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='住院情况'
                        and e.parentnodename='出院记录\住院\住院情况'
                        and e.datatype='出院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='住院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='出院记录\住院\住院情况'
                                    and e1.datatype='出院相关')
                                    union all
                                    select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='住院情况'
                        and e.parentnodename='二十四小时入出院出院记录\住院\住院情况'
                        and e.datatype='入院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='住院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='二十四小时入出院出院记录\住院\住院情况'
                                    and e1.datatype='入院相关')   union all
                                    select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='住院情况'
                        and e.parentnodename='死亡记录\住院\住院情况'
                        and e.datatype='出院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='住院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='死亡记录\住院\住院情况'
                                    and e1.datatype='出院相关')
                                    union all 

                        select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='入院情况'
                        and e.parentnodename='二十四小时入出院出院记录\入院\入院情况'
                        and e.datatype='入院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='入院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='二十四小时入出院出院记录\入院\入院情况'
                                    and e1.datatype='入院相关')
                                    union all
                          select nvl(D,C) nodevalue
                             from XSRCYJL
                             where A = '{0}             '
                             and rownum = '1'
                        )
                        where rownum = 1";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
                {
                    sql = @"select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}'
                        and e.nodename='住院情况'
                        and e.parentnodename='出院记录\住院\住院情况'
                        and e.datatype='出院相关'
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}'
                                    and e1.nodename='住院情况'
                                    and e1.parentnodename='出院记录\住院\住院情况'
                                    and e1.datatype='出院相关')";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
                {
                    sql = @"  select i.value 
                          from rcd_record_item i,pt_inpatient_cure b
                          where i.element_id='200062'
                          and b.id = i.inpatient_id
                          and b.inpatient_code='{0}'";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
                {
                    sql = @"  select i.value 
                          from rcd_record_item i,pt_inpatient_cure b
                          where i.element_id='200062'
                          and b.id = i.inpatient_id
                          and b.inpatient_code='{0}'";
                }
            }
            sql = string.Format(sql, strNo);
            str = this.con.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 查询出院情况
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        public string QueryCYQK(string strNo)
        {
            string str = "";
            string sql = "";

            sql = @"SELECT FUN_GET_INPATIENT_OUTSUMMARY('{0}') 出院情况 FROM DUAL ";
            if (false)
            {
                if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
                {
                    sql = @"
                        select nodevalue 
                        from (select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='出院情况'
                        and e.parentnodename='出院记录\出院\出院情况'
                        and e.datatype='出院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='出院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='出院记录\出院\出院情况'
                                    and e1.datatype='出院相关')
                                    union all
                                    select '死亡' nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='死亡原因'
                        and e.parentnodename='死亡记录\死亡\死亡原因'
                        and e.datatype='出院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='死亡原因'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='死亡记录\死亡\死亡原因'
                                    and e1.datatype='出院相关')

                                    union all
                                    select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='出院情况'
                        and e.parentnodename='二十四小时入出院出院记录\出院\出院情况'
                        and e.datatype='入院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='出院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='二十四小时入出院出院记录\出院\出院情况'
                                    and e1.datatype='入院相关')
                                   
                                    union all
                        select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='入院情况'
                        and e.parentnodename='二十四小时入出院出院记录\入院\入院情况'
                        and e.datatype='入院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='入院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='二十四小时入出院出院记录\入院\入院情况'
                                    and e1.datatype='入院相关')
                                    union all
                          select nvl(E,C) nodevalue
                             from XSRCYJL
                             where A = '{0}             '
                             and rownum = '1'
                        )";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
                {
                    sql = @"select e.nodevalue
                            from datastore_emr e
                            where e.index1='{0}'
                            and e.nodename='出院情况'
                            and e.parentnodename='出院记录\出院\出院情况'
                            and e.datatype='出院相关'
                            and e.id = (select max(e1.id) from  datastore_emr e1
                                       where e1.index1='{0}'
                                        and e1.nodename='出院情况'
                                        and e1.parentnodename='出院记录\出院\出院情况'
                                        and e1.datatype='出院相关')";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
                {
                    sql = @" select i.value
                          from rcd_record_item i,pt_inpatient_cure b
                          where i.element_id='367'
                          and b.id = i.inpatient_id
                          and b.inpatient_code='{0}'";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
                {
                    sql = @" select i.value
                          from rcd_record_item i,pt_inpatient_cure b
                          where i.element_id='367'
                          and b.id = i.inpatient_id
                          and b.inpatient_code='{0}'";
                }
            }
            sql = string.Format(sql, strNo);
            str = this.con.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 查询出院医嘱
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        public string QueryCYYZ(string strNo)
        {
            string str = "";
            string sql = "";
            sql = @"SELECT FUN_GET_INPATIENT_OUTORDER('{0}') 出院医嘱 FROM DUAL ";
            if (false)
            {
                if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
                {
                    sql = @"
                        select nodevalue
                        from (select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='出院带药'
                        and e.parentnodename='出院记录\出院\出院带药'
                        and e.datatype='出院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='出院带药'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='出院记录\出院\出院带药'
                                    and e1.datatype='出院相关')

                                    union all
                                      select '无' nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='死亡原因'
                        and e.parentnodename='死亡记录\死亡\死亡原因'
                        and e.datatype='出院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='死亡原因'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='死亡记录\死亡\死亡原因'
                                    and e1.datatype='出院相关')


                                    union all
                                    select '无' nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='出院情况'
                        and e.parentnodename='二十四小时入出院出院记录\出院\出院情况'
                        and e.datatype='入院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='出院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='二十四小时入出院出院记录\出院\出院情况'
                                    and e1.datatype='入院相关')
                                      union all
                        select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}             '
                        and e.nodename='入院情况'
                        and e.parentnodename='二十四小时入出院出院记录\入院\入院情况'
                        and e.datatype='入院相关'
                        and e.nodevalue is not null
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}             '
                                    and e1.nodename='入院情况'
                                    and e1.nodevalue is not null
                                    and e1.parentnodename='二十四小时入出院出院记录\入院\入院情况'
                                    and e1.datatype='入院相关')
                                    union all
                          select nvl(F,C) nodevalue
                             from XSRCYJL
                             where A = '{0}             '
                             and rownum = '1'
                        )
                        where rownum = 1";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
                {
                    sql = @"select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}'
                        and e.nodename='出院带药'
                        and e.parentnodename='出院记录\出院\出院带药'
                        and e.datatype='出院相关'
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}'
                                    and e1.nodename='出院带药'
                                    and e1.parentnodename='出院记录\出院\出院带药'
                                    and e1.datatype='出院相关')";
                }
                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
                {
                    sql = @" select i.value 
                          from rcd_record_item i,pt_inpatient_cure b
                          where i.element_id='369'
                          and b.id = i.inpatient_id
                          and b.inpatient_code='{0}'";
                }

                else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
                {
                    sql = @" select i.value
                          from rcd_record_item i,pt_inpatient_cure b
                          where i.element_id='369'
                          and b.id = i.inpatient_id
                          and b.inpatient_code='{0}'";
                }
            }
            sql = string.Format(sql, strNo);
            str = this.con.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 查询死亡入院情况
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        public string QuerySWJLRYQK(string strNo)
        {
            string str = "";
            string sql = "";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                sql = @"select e.nodevalue
                            from datastore_emr e
                            where e.index1='{0}'
                            and e.nodename='入院情况'
                            and e.parentnodename='死亡记录\入院\入院情况'
                            and e.datatype='死亡相关'
                            and e.nodevalue is not null
                            and e.id = (select max(e1.id) from  datastore_emr e1
                                       where e1.index1='{0}'
                                        and e1.nodename='入院情况'
                                        and e1.nodevalue is not null
                                        and e1.parentnodename='死亡记录\入院\入院情况'
                                        and e1.datatype='死亡相关')";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                sql = @"select e.nodevalue
                            from datastore_emr e
                            where e.index1='{0}'
                            and e.nodename='入院情况'
                            and e.parentnodename='死亡记录\入院\入院情况'
                            and e.datatype='死亡相关'
                            and e.id = (select max(e1.id) from  datastore_emr e1
                                       where e1.index1='{0}'
                                        and e1.nodename='入院情况'
                                        and e1.parentnodename='死亡记录\入院\入院情况'
                                        and e1.datatype='死亡相关')";
            }
            sql = string.Format(sql, strNo);
            str = this.con.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 查询死亡诊疗过程
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        public string QuerySWJLZLGC(string strNo)
        {
            string str = "";
            string sql = "";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                sql = @"select e.nodevalue
                            from datastore_emr e
                            where e.index1='{0}'
                            and e.nodename='住院情况'
                            and e.parentnodename='死亡记录\住院\住院情况'
                            and e.datatype='死亡相关'
                            and e.nodevalue is not null
                            and e.id = (select max(e1.id) from  datastore_emr e1
                                       where e1.index1='{0}'
                                        and e1.nodename='住院情况'
                                        and e1.nodevalue is not null
                                        and e1.parentnodename='死亡记录\住院\住院情况'
                                        and e1.datatype='死亡相关')";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                sql = @" select e.nodevalue
                        from datastore_emr e
                        where e.index1='{0}'
                        and e.nodename='住院情况'
                        and e.parentnodename='死亡记录\住院\住院情况'
                        and e.datatype='死亡相关'
                        and e.id = (select max(e1.id) from  datastore_emr e1
                                   where e1.index1='{0}'
                                    and e1.nodename='住院情况'
                                    and e1.parentnodename='死亡记录\住院\住院情况'
                                    and e1.datatype='死亡相关')";
            }

            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
            {
                sql = @"select i.value 
                          from rcd_record_item i,pt_inpatient_cure b
                          where i.element_id='372'
                          and b.id = i.inpatient_id
                          and b.inpatient_code='{0}'";
            }

            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                sql = @"select i.value 
                          from rcd_record_item i,pt_inpatient_cure b
                          where i.element_id='653'
                          and b.id = i.inpatient_id
                          and b.inpatient_code='{0}'";
            }
            sql = string.Format(sql, strNo);
            str = this.con.ExecSqlReturnOne(sql);
            if (str == "-1")
            {
                str = "";
            }
            return str;
        }

        /// <summary>
        /// 更新入院情况
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        private int UpdateRYQK(string strNo, string nr)
        {
            string sql = "";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                sql = @"update datastore_emr e
                        set e.nodevalue = '{1}'
                        where e.index1= '{0}'
                        and e.nodename='入院情况'
                        and e.parentnodename='出院记录\入院\入院情况'
                        and e.datatype='出院相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                sql = @"update datastore_emr e
                        set e.nodevalue = '{1}'
                        where e.index1= '{0}'
                        and e.nodename='入院情况'
                        and e.parentnodename='出院记录\入院\入院情况'
                        and e.datatype='出院相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '363'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '363'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            sql = string.Format(sql, strNo, nr);
            return this.con.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新诊疗过程
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        private int UpdateZLGC(string strNo, string nr)
        {
            string sql = "";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                sql = @"update datastore_emr e
                        set e.nodevalue = '{1}'
                        where e.index1= '{0}'
                        and e.nodename='住院情况'
                        and e.parentnodename='出院记录\住院\住院情况'
                        and e.datatype='出院相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                sql = @"update datastore_emr e
                        set e.nodevalue = '{1}'
                        where e.index1= '{0}'
                        and e.nodename='住院情况'
                        and e.parentnodename='出院记录\住院\住院情况'
                        and e.datatype='出院相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '200062'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '200062'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            sql = string.Format(sql, strNo, nr);
            return this.con.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新出院情况
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        private int UpdateCYQK(string strNo, string nr)
        {
            string sql = "";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                sql = @"update datastore_emr e
                            set e.nodevalue = '{1}'
                            where e.index1= '{0}'
                            and e.nodename='出院情况'
                            and e.parentnodename='出院记录\出院\出院情况'
                            and e.datatype='出院相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                sql = @"update datastore_emr e
                            set e.nodevalue = '{1}'
                            where e.index1= '{0}'
                            and e.nodename='出院情况'
                            and e.parentnodename='出院记录\出院\出院情况'
                            and e.datatype='出院相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '367'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '367'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            sql = string.Format(sql, strNo, nr);
            return this.con.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新出院医嘱
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        private int UpdateCYYZ(string strNo, string nr)
        {
            string sql = "";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                sql = @"update datastore_emr e
                        set e.nodevalue = '{1}'
                        where e.index1= '{0}'
                        and e.nodename='出院带药'
                        and e.parentnodename='出院记录\出院\出院带药'
                        and e.datatype='出院相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                sql = @"update datastore_emr e
                        set e.nodevalue = '{1}'
                        where e.index1= '{0}'
                        and e.nodename='出院带药'
                        and e.parentnodename='出院记录\出院\出院带药'
                        and e.datatype='出院相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '369'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '369'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            sql = string.Format(sql, strNo, nr);
            return this.con.ExecNoQuery(sql);
        }


        /// <summary>
        /// 更新死亡入院情况
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        private int UpdateSWJLRYQK(string strNo, string nr)
        {
            string sql = "";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                sql = @"update datastore_emr e
                        set e.nodevalue = '{1}'
                        where e.index1= '{0}'
                        and e.nodename='入院情况'
                        and e.parentnodename='死亡记录\入院\入院情况'
                        and e.datatype='死亡相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                sql = @"update datastore_emr e
                        set e.nodevalue = '{1}'
                        where e.index1= '{0}'
                        and e.nodename='入院情况'
                        and e.parentnodename='死亡记录\入院\入院情况'
                        and e.datatype='死亡相关'";
            }
            sql = string.Format(sql, strNo, nr);
            return this.con.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新死亡诊疗过程
        /// </summary>
        /// <param name="strNo"></param>
        /// <returns></returns>
        private int UpdateSWJLZLGC(string strNo, string nr)
        {
            string sql = "";
            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                sql = @"update datastore_emr e
                    set e.nodevalue = '{1}'
                where e.index1= '{0}'
                and e.nodename='住院情况'
                and e.parentnodename='死亡记录\住院\住院情况'
                and e.datatype='死亡相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                sql = @"update datastore_emr e
                    set e.nodevalue = '{1}'
                where e.index1= '{0}'
                and e.nodename='住院情况'
                and e.parentnodename='死亡记录\住院\住院情况'
                and e.datatype='死亡相关'";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '372'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            else if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                sql = @"  update rcd_record_item 
                          set value = '{1}'
                          where element_id = '372'
                          and inpatient_id in (select b.id from pt_inpatient_cure b where  b.inpatient_code= '{0}')";
            }
            sql = string.Format(sql, strNo, nr);
            return this.con.ExecNoQuery(sql);
        }
        #endregion

        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.txtNo.Text.Trim()))
                {
                    this.OnQuery(null,null);
                }
                else
                {
                    this.Clear();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.OnSave(null,new object());
        }

    }
}
