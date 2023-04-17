using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.InpatientFee.Controls
{
    public partial class tvPatientListForPrepayByDept : FS.HISFC.Components.Common.Controls.tvPatientList
    {
        public tvPatientListForPrepayByDept()
        {
            InitializeComponent();
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return;
            }
            this.Refresh();
           
        }

        public string ShowType = "In";

        public string ShowDept = "1";

        public tvPatientListForPrepayByDept(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public override void Refresh()
        {
            //{707F2343-20AC-445b-9ACB-2B707C8EA249}
            InitControlParam();
            this.BeginUpdate();
            this.Nodes.Clear();
            if (manager == null)
                manager = new FS.HISFC.BizProcess.Integrate.RADT();


            ArrayList al = new ArrayList();//患者列表

            addPatientList(al);
            //显示所有患者列表
            this.SetPatient(al);

            this.EndUpdate();
            this.CollapseAll();
            this.Sort();
            //this.Scrollable = false;  

            //base.Refresh();
        }


        FS.HISFC.BizProcess.Integrate.RADT manager = null;
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        //出院召回的有效天数
        private int callBackVaildDays;
        public const string control_id = "ZY0001";
        private Dictionary<string, string> dictionaryDeptMark = new Dictionary<string, string>();

        /// <summary>
        /// 初始化控制参数,获得出院召回的有效天数
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            this.callBackVaildDays = ctrlParamIntegrate.GetControlParam<int>(control_id, true, 1);
            this.initDeptMark();
        }


        /// <summary>
        /// 根据病区站得到患者
        /// </summary>
        /// <param name="al"></param>
        private void addPatientList(ArrayList al)
        {
            ArrayList al1 =this.radtManager.PatientQueryByInState( "I','R");
            if (ShowDept == "1")
            {
                string deptCode = string.Empty;
                foreach (FS.HISFC.Models.RADT.PatientInfo patientInfo in al1)
                {
                    if (deptCode.Equals(patientInfo.PVisit.PatientLocation.Dept.ID) == false)
                    {
                        deptCode = patientInfo.PVisit.PatientLocation.Dept.ID;
                        if (dictionaryDeptMark.ContainsKey(deptCode))
                        {
                            al.Add("(" + dictionaryDeptMark[deptCode] + ")" + patientInfo.PVisit.PatientLocation.Dept.Name);
                        }
                        else
                        {
                            al.Add(patientInfo.PVisit.PatientLocation.Dept.Name);
                        }
                        al.Add(patientInfo);
                    }
                    else
                    {
                        al.Add(patientInfo);
                    }
                }
            }
        }

        private void initDeptMark()
        {
            string sql = @"select t.mark,t.dept_code from COM_DEPTSTAT t where t.stat_code = '00'
                                    and t.dept_code in 
                                    (select s.dept_code from com_department s where s.dept_type = 'I' and s.valid_state = '1')";

            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

            if (dbMgr.ExecQuery(sql) < 0)
            {
                return;
            }

            try
            {
                while (dbMgr.Reader.Read())
                {
                    if (dictionaryDeptMark.ContainsKey(dbMgr.Reader[1].ToString()) == false)
                    {
                        if (dbMgr.Reader[0].ToString().Length <= 2 && FS.FrameWork.Public.String.IsNumeric(dbMgr.Reader[0].ToString())==false)
                        {
                            dictionaryDeptMark[dbMgr.Reader[1].ToString()] = dbMgr.Reader[0].ToString().PadLeft(3, '0');
                        }
                        else
                        {
                            dictionaryDeptMark[dbMgr.Reader[1].ToString()] = dbMgr.Reader[0].ToString();
                        }
                    }
                }
            }
            finally
            {
                if (dbMgr.Reader != null && dbMgr.Reader.IsClosed == false)
                {
                    dbMgr.Reader.Close();
                }
            }
        }
 }
}
