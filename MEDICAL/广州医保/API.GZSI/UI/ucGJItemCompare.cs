using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace API.GZSI.UI
{
    public partial class ucGJItemCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucGJItemCompare()
        {
            InitializeComponent();
            this.cmbItem.AddItems(localMgr.GetGDItemInfo());
            this.cmbItemLevel.AddItems(localMgr.GetItemLevel());
            this.GetCompareInfo();
        }
        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();



        private void btClear_Click(object sender, EventArgs e)
        {
            this.Clear(true);
        }

        private void Clear(bool isC)
        {
            if (isC)
            {
                this.cmbItem.Tag = null;
            }
            this.txtGBCode.Text = "";
            this.txtGBName.Text = "";
            this.txtMemo.Text = "";
        }

        DataSet ds = null;

        private void GetCompareInfo()
        {
            string sql = @"select 
                                'False',--选择
                                i.his_code,--系统编码
                                e.his_name,--系统名称
                                e.regularname,--系统别名
                                e.his_user_code,--上传码
                                e.center_code,--中心码
                                e.center_name,--项目名称
                                decode(e.center_sys_class,'X','西药','Z','中药','L','诊疗项目','F','医疗服务设施')  as center_sys_class,--项目类别
                                fun_get_dictionary_name('MINFEE', i.fee_code)  fee_code,--最小费用
                                --decode(i.item_grade,'1','甲类','2','乙类','3','丙类','-') item_grade, --甲乙类
                                decode(e.CENTER_ITEM_GRADE,'1','甲类','2','乙类','3','自费') item_grade, --甲乙类
                                e.center_specs,--医保规格
                                i.specs ,--医院规格
                                e.center_dose,--医保剂型
                                fun_get_dictionary_name('DOSAGEFORM', i.jxbm) yyjx,--医院剂型                      
                                fun_get_company_name(i.producer_code),--厂家
                                i.approve_info,--批准文号
                                i.gjbm,--医院国家编码
                                e.center_memo--医保备注
                                from fin_com_compare e
                                left join (select  
                                                (case when (select t.control_value from com_controlargument t where t.control_code = 'FSMZ16'
                                                )='0' then b.gb_code else b.custom_code end) gdCode,
                                                b.drug_code his_code,
                                                b.class_code class_code,
                                                b.fee_code  fee_code,
                                                b.item_grade item_grade, 
                                                b.specs specs,
                                                b.producer_code  producer_code,
                                                b.approve_info approve_info,
                                                b.gb_code gjbm,
                                                b.dose_model_code jxbm
                                            from pha_com_baseinfo b
                                            union all
                                            select 
                                                (case when (select t.control_value from com_controlargument t where t.control_code = 'FSMZ16'
                                                )='0' then u.gb_code else u.input_code end) gdCode,
                                                u.item_code his_code,
                                                u.sys_class class_code,
                                                u.fee_code fee_code,
                                                u.item_grade item_grade, 
                                                u.specs specs,
                                                '' producer_code,
                                                '' approve_info,
                                                u.gb_code gjbm,
                                                '' jxbm
                                            from  fin_com_undruginfo u) i
                                on e.his_user_code = i.gdCode
                                where e.pact_code = '4'";
            this.ds = new DataSet();
            int res = constMgr.ExecQuery(sql, ref this.ds);
            this.fpCompareInfo_Sheet1.DataSource = this.ds.Tables[0].DefaultView;//{405B92DC-4786-4c78-9476-8841F28FF5FE}

            this.setFpView();
            #region 加载太慢
            //if (this.ds.Tables[0].Rows == null || this.ds.Tables[0].Rows.Count <= 0) return;

            //Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据!请稍后!");

            //this.fpCompareInfo_Sheet1.RowCount = 0;
            //foreach (DataRow dr in this.ds.Tables[0].Rows) {
            //    this.fpCompareInfo_Sheet1.Rows.Add(0, 1);
            //    this.fpCompareInfo_Sheet1.Cells[0, 0].Value = dr[0].ToString();
            //    this.fpCompareInfo_Sheet1.Cells[0, 1].Text = dr[1].ToString();
            //    this.fpCompareInfo_Sheet1.Cells[0, 2].Text = dr[2].ToString();
            //    this.fpCompareInfo_Sheet1.Cells[0, 3].Text = dr[3].ToString();

            //    this.fpCompareInfo_Sheet1.Cells[0, 4].Text = dr[4].ToString();//姓名
            //    this.fpCompareInfo_Sheet1.Cells[0, 5].Text = dr[5].ToString();
            //    this.fpCompareInfo_Sheet1.Cells[0, 6].Text = dr[6].ToString();

            //    this.fpCompareInfo_Sheet1.Cells[0, 7].Text = dr[7].ToString();
            //    this.fpCompareInfo_Sheet1.Cells[0, 8].Text = dr[8].ToString();
            //    this.fpCompareInfo_Sheet1.Cells[0, 9].Text = dr[9].ToString();

            //    this.fpCompareInfo_Sheet1.Cells[0, 10].Text = dr[10].ToString();
            //    this.fpCompareInfo_Sheet1.Cells[0, 11].Text = dr[11].ToString();

            //    this.fpCompareInfo_Sheet1.Cells[0, 12].Text = dr[12].ToString();
            //}

            //Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            #endregion
        }
        private void setFpView()
        {
            this.fpCompareInfo_Sheet1.Columns[0].Width = 30F;
            this.fpCompareInfo_Sheet1.Columns[2].Width = 200F;
            FarPoint.Win.Spread.CellType.CheckBoxCellType t = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            for (int k = 0; k < this.fpCompareInfo_Sheet1.Rows.Count; k++)
            {
                this.fpCompareInfo_Sheet1.Cells[k, 0].CellType = t;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (this.cmbItem.Tag == null || string.IsNullOrEmpty(this.cmbItem.Tag.ToString()))
            {
                MessageBox.Show("请选择医院项目！");
                return;
            }
            if (string.IsNullOrEmpty(this.txtGBCode.Text))
            {
                MessageBox.Show("请输入国家编码！");
                return;
            }

            string sql = @"select count(*) from fin_com_compare e
                        where e.his_code= '{0}'
                        and e.pact_code = '4'";
            sql = string.Format(sql, this.cmbItem.Tag.ToString());

            string res = constMgr.ExecSqlReturnOne(sql);
            sql = "";
            if (int.Parse(res) > 0)
            {
                if (MessageBox.Show("已存在项目对照" + this.cmbItem.Tag.ToString() + this.cmbItem.Text + "，是否更新信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //TOOL,需要修改
                    sql = @"update fin_com_compare e
                            set e.center_code = '{2}',
                                e.center_memo = '{3}',
                                e.center_name = '{5}',
                                e.center_item_grade='{6}',
                                e.oper_code = '{7}',
                                e.oper_date = sysdate
                            where e.pact_code = '4'
                            and e.his_code = '{0}'"; //CENTER_ITEM_GRADE
                }
                else
                {
                    return;
                }
            }
            else
            {
                //TOOL,需要修改
                sql = @"insert into fin_com_compare e
                (e.pact_code,e.his_code,e.his_name,e.center_code,e.center_memo,e.his_user_code,e.center_name,e.center_item_grade,e.oper_code,e.oper_date)
                values
                ('4','{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',sysdate)";
            }

            if (!string.IsNullOrEmpty(sql))
            {
                sql = string.Format(sql, this.cmbItem.Tag.ToString(), this.cmbItem.Text,
                    this.txtGBCode.Text, this.txtMemo.Text,this.lUserCode.Text,this.txtGBName.Text,this.cmbItemLevel.Tag.ToString(),
                    this.constMgr.Operator.ID.ToString());
                if (this.constMgr.ExecNoQuery(sql) < 0)
                {
                    MessageBox.Show("保存国标失败！" + constMgr.Err);
                    return;
                }
                MessageBox.Show("保存成功！");
            }

        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbHIS.Text = this.cmbItem.Tag.ToString();//获取本位码、国药准字、药品规格、生产厂家
            DataTable dt = localMgr.GetItemInfo(this.cmbItem.Tag.ToString());
            if (dt != null || dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.lbHIS.Text = string.IsNullOrEmpty(dr[2].ToString()) ? "" : "项目名称：" + dr[2].ToString();
                this.lbHIS.Text += string.IsNullOrEmpty(dr[0].ToString()) ? "" : "\n系统编码：" + dr[0].ToString();
                this.lbHIS.Text += string.IsNullOrEmpty(dr[1].ToString()) ? "" : "      上传码：" + dr[1].ToString();
                this.lUserCode.Text = dr[1].ToString();//保存使用
                this.lbHIS.Text += string.IsNullOrEmpty(dr[3].ToString()) ? "" : "\n费用类别：" + dr[3].ToString();
                this.lbHIS.Text += string.IsNullOrEmpty(dr[4].ToString()) ? "" : "      规格：" + dr[4].ToString();
                this.lbHIS.Text += string.IsNullOrEmpty(dr[5].ToString()) ? "" : "\n生产厂家：" + dr[5].ToString();
                this.lbHIS.Text += string.IsNullOrEmpty(dr[6].ToString()) ? "" : "\n国药准字：" + dr[6].ToString();
                this.lbHIS.Text += string.IsNullOrEmpty(dr[7].ToString()) ? "" : "      国家编码：" + dr[7].ToString();
                this.txtGBCode.Text = dr[8].ToString();
                this.txtMemo.Text = dr[9].ToString();
                this.txtGBName.Text = dr[10].ToString();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //删除对照
            for (int k = 0; k < this.fpCompareInfo_Sheet1.Rows.Count; k++)
            {
                bool isChoose = Convert.ToBoolean(this.fpCompareInfo_Sheet1.Cells[k, 0].Value);
                if (isChoose)
                {
                    string sql = @"delete from fin_com_compare e where e.his_code = '{0}' and e.pact_code = '4'";
                    sql = string.Format(sql, this.fpCompareInfo_Sheet1.Cells[k, 1].Text);

                    if (constMgr.ExecNoQuery(sql) <= 0)
                    {
                        MessageBox.Show("删除对照失败！【" + this.fpCompareInfo_Sheet1.Cells[k, 2].Text + "】");
                        continue;
                    }
                }
            }
            MessageBox.Show("删除对照结束！");
            this.GetCompareInfo();
        }

        private void ntbQueryInfo_TextChanged(object sender, EventArgs e)
        {
            if (this.ds.Tables[0] == null || this.ds.Tables[0].DefaultView == null)
            {
                return;
            }
            this.ds.Tables[0].DefaultView.RowFilter = string.Format("his_code like '%{0}%' or his_name like '%{0}%'", this.ntbQueryInfo.Text.Trim());
            this.fpCompareInfo_Sheet1.DataSource = this.ds.Tables[0].DefaultView;//{405B92DC-4786-4c78-9476-8841F28FF5FE}
            this.setFpView();
        }

        private void fpNeedUpload_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int rownum = e.Row;
            if (rownum < 0)
            {
                return;
            }
            this.cmbItem.Tag = this.fpCompareInfo_Sheet1.Cells[rownum, 1].Text;//流水号
            string level="3"; 
            if(this.fpCompareInfo_Sheet1.Cells[rownum, 9].Text.Trim().ToString()=="甲类")
                level="1";
            else if(this.fpCompareInfo_Sheet1.Cells[rownum, 9].Text.Trim().ToString()=="乙类")
                level="2";

            this.cmbItemLevel.Tag = level;//医保等级
        }
    }
}
