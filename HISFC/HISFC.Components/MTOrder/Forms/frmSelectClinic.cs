using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.MTOrder.Forms
{
    public partial class frmSelectClinic : Form
    {
        #region 属性
        /// <summary>
        /// 历史预约
        /// </summary>
        public List<HISFC.Models.RADT.Patient> PatientList = new List<HISFC.Models.RADT.Patient>();

        /// <summary>
        /// 用户选择的ClinicNo
        /// </summary>
        public string ClinicNo { get; set; }
        /// <summary>
        /// 需要查找信息的门诊/住院号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 是否门诊
        /// </summary>
        public bool IsClinic { get; set; }

        private int searchDays=14;
        /// <summary>
        /// 查找天数
        /// </summary>
        public int SearchDays { get { return searchDays; } set { this.searchDays = value; } }
        #endregion

        #region 列
        enum cols
        {
            No = 0,
            CardNo,
            ClinicNo,
            ApplyDate,
            Name,
            Num
        }
        #endregion

        #region  域
        /// <summary>
        /// 申请历史信息列表
        /// </summary>
        private DataTable dsItems = new DataTable();
        private DataView dv;

        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 住院病人信息管理类
        /// </summary>
        private HISFC.BizLogic.RADT.InPatient inPatMgr = new HISFC.BizLogic.RADT.InPatient();

        #endregion
        public frmSelectClinic()
        {
            InitializeComponent();
            #region 加载事件
            this.Leave += new System.EventHandler(this.frmSelectHistory_Leave);
            this.Load += new EventHandler(frmSelectHistory_Load);
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.neuSpread1_CellDoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KeyDown);
            neuSpread1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KeyDown);
            #endregion

            FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
            string rtn = ctlMgr.QueryControlerInfo("YJYYSJ",true);
            if (rtn == null || rtn == "-1" || rtn == "") searchDays = 7;
            else searchDays = int.Parse(rtn);
        }
        #region 初始化
        void frmSelectHistory_Load(object sender, EventArgs e)
        {
            InitPatientList();
            if (PatientList == null || PatientList.Count < 1)
            {
                MessageBox.Show("没有相关信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Cancel;
                return;
            }else
            if (PatientList.Count == 1)
            {
                this.ClinicNo = (PatientList[0] as HISFC.Models.RADT.Patient).ID;
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            else
            {
                InitDataTable();
                InitSpread();
                neuSpread1_Sheet1.ActiveRowIndex = 1;
            }
        }
        private void InitPatientList()
        {
            ArrayList al = null;
            if (IsClinic)
                al = regMgr.Query(CardNo, DateTime.Now.AddDays(-1 * SearchDays));
            else
                al = inPatMgr.GetPatientInfoByPatientNo(CardNo);
            if (al == null || al.Count < 1)
            {
                MessageBox.Show("没有病人相关信息", "提示");
                this.DialogResult = DialogResult.Cancel;
                return;
            }
            else
                foreach (HISFC.Models.RADT.Patient p in al)
                {
                    PatientList.Add(p);
                }

        }
        /// <summary>
        /// 初始化DataTable
        /// </summary>
        private void InitDataTable()
        {
            dsItems = new DataTable("Templet");

            dsItems.Columns.AddRange(new DataColumn[]
			{
				new DataColumn(cols.No.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.CardNo.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ClinicNo.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.ApplyDate.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.Name.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.Num.ToString(),System.Type.GetType("System.String"))
			});
        }
        /// <summary>
        /// 初始化Spread
        /// </summary>
        private void InitSpread()
        {
            try
            {
                dsItems.Clear();
                int Index = 1;
                if (IsClinic)
                    PatientList.Select(t =>
                    {
                        {
                            HISFC.Models.Registration.Register r = (HISFC.Models.Registration.Register)t;
                            return new { r.ID, r.PID.CardNO, r.DoctorInfo.SeeDate, r.Name };
                        }

                    }).ToList().ForEach(t =>
                        {
                            dsItems.Rows.Add(new object[]
                            {
                            Index++,
                            t.CardNO,
                            t.ID,
                            t.SeeDate.ToString("yyyy-MM-dd"),
                            t.Name
                            });
                        });
                else

                    PatientList.Select(t =>
                    {
                        HISFC.Models.RADT.PatientInfo r = (HISFC.Models.RADT.PatientInfo)t;
                        return new { r.ID, r.PID.PatientNO, r.PVisit.InTime, r.Name };
                    }).ToList().ForEach(t =>{
                        dsItems.Rows.Add(new object[]
                                            {
                                            Index++,
                                            t.PatientNO,
                                            t.ID,
                                            t.InTime.ToString("yyyy-MM-dd"),
                                            t.Name
                                            });
                    });
            }

            catch (Exception e)
            {
                MessageBox.Show("历史信息生成DataTable时出错!" + e.Message, "提示");
                return;
            }
            dsItems.AcceptChanges();
            dv = dsItems.DefaultView;
            neuSpread1_Sheet1.DataSource = dv;
            SetFpFormat();
        }
        /// <summary>
        /// 设置Farpoint显示格式
        /// </summary>
        private void SetFpFormat()
        {
            this.neuSpread1_Sheet1.Columns[(int)cols.Num].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)cols.No].Width = 30F;
            this.neuSpread1_Sheet1.Columns[(int)cols.CardNo].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ClinicNo].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyDate].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Name].Width = 80F;
            //this.neuSpread1_Sheet1.Columns[(int)cols.Num].Width = 90F;
        }
        #endregion
        private void frmSelectHistory_Leave(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ClinicNo = neuSpread1_Sheet1.GetText(neuSpread1_Sheet1.ActiveRowIndex, (int)cols.ClinicNo);
            this.DialogResult = DialogResult.OK;
        }
        private void frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClinicNo = neuSpread1_Sheet1.GetText(neuSpread1_Sheet1.ActiveRowIndex, (int)cols.ClinicNo);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
