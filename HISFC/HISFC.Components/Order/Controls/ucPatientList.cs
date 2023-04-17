using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Components.Order.Classes;

namespace FS.HISFC.Components.Order.Controls
{
    //{FBE92A1C-323E-405e-9F46-ACCA9700DF2A}

    public delegate void PatientInfoDelegate(PatientInfoByJZ patient);
    public delegate void LoadPregnantData();

    public partial class ucPatientList : UserControl
    {

        FS.HISFC.Components.Order.Classes.PatientInfoByJZ patient = new PatientInfoByJZ();


        private string queryCondition = string.Empty;

        public List<PatientInfoByJZ> patientList = null;

        //  PatientInfo patientManager = new  PatientInfo();

        public PatientInfoDelegate patientInfo;

        public ucPatientList()
        {
            InitializeComponent();
            this.dataGridView1.CellContentDoubleClick += new DataGridViewCellEventHandler(dataGridView1_CellContentDoubleClick);

        }





        void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {

                this.patientInfo(patientList[dataGridView1.CurrentRow.Index]);
            }
            this.Parent.Dispose();
        }

        public string QueryCondition
        {
            get { return this.queryCondition; }
            set
            {
                this.queryCondition = value;
                //FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                //accountMgr.GetPatientInfo
                patientList = GetPatientBySome(this.queryCondition);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Escape)
            {
                this.Parent.Dispose();
                return true;
            }
            if (keyData == Keys.Enter)
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    this.patientInfo(patientList[dataGridView1.CurrentRow.Index]);
                }
                this.Parent.Dispose();
                return true;
            }
            return false;
        }

        private void ucPatientList_Load(object sender, EventArgs e)
        {

            foreach (PatientInfoByJZ patient in this.patientList)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Tag = patient;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0].Value = patient.CARD_NO;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[1].Value = patient.NAME;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[2].Value = patient.Age;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[3].Value = patient.Sex;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[4].Value = patient.IDENNO;
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[5].Value = patient.HOME_TEL;
            }
        }

        public List<PatientInfoByJZ> GetPatientBySome(string query)
        {
            //HISFC.Components.Operation.WebReference.IbornMobileService web = new HISFC.Components.Operation.WebReference.IbornMobileService();
            //string req = "<req><code>" + query + "</code><name></name></req>";
            //string result = web.GetPatientInfoByJZ(req);

            //System.Xml.XmlDocument doc = new XmlDocument();
            //doc.LoadXml(result);

            //System.Xml.XmlNodeList xnl = doc.SelectNodes("res/info");

            DataSet ds=new DataSet();
            string sql = string.Format("select * from com_patientinfo where (card_no like '%{0}%' or name like '%{1}%') and is_valid='1'",query,query);
            patient.ExecQuery(sql, ref ds);
            DataTable dt=ds.Tables[0];

            List<PatientInfoByJZ> patientList = new List<PatientInfoByJZ>();

            for(int i=0;i<dt.Rows.Count;i++)
            {
                int age = Convert.ToInt32(System.DateTime.Now.Year - Convert.ToDateTime(dt.Rows[i]["BIRTHDAY"].ToString()).Year);
                string sex = dt.Rows[i]["SEX_CODE"].ToString() == "F" ? "女" : "男";
                patientList.Add(new PatientInfoByJZ() { CARD_NO = dt.Rows[i]["CARD_NO"].ToString(), NAME = dt.Rows[i]["NAME"].ToString(), HOME_TEL = dt.Rows[i]["HOME_TEL"].ToString(), IDENNO = dt.Rows[i]["IDENNO"].ToString(), SEX_CODE = dt.Rows[i]["SEX_CODE"].ToString(), BIRTHDAY = dt.Rows[i]["BIRTHDAY"].ToString(), Age = age, Sex = sex });
            }

            return patientList;

        }

    }
}
