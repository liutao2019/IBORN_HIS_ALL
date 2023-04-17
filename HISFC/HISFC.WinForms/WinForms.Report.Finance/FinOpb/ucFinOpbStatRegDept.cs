using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Finance.FinOpb
{
    public partial class ucFinOpbStatRegDept : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        /// <summary>
        /// 门诊收入报表
        /// </summary>
        public ucFinOpbStatRegDept()
        {
            InitializeComponent();
            InitDept();
            InitDoc();
           // InitMetGroup();
        }
        /// <summary>
        /// 变量
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager manger = new FS.HISFC.BizProcess.Integrate.Manager();
        private ArrayList alDept = new ArrayList();
        private ArrayList alDoc = new ArrayList();
        private ArrayList alMetGroup = new ArrayList();
        private string deptID = string.Empty;
        private string deptName = string.Empty;
        private string docID = string.Empty;
        private string docName = string.Empty;
        private string mgID = string.Empty;
        private string mgName = string.Empty;

         
        /// <summary>
        /// 初始化科室
        /// </summary>
        private void InitDept()
        {
            this.isAcross = true;
            this.isSort = false;
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            alDept.Add(obj);
            ArrayList dept = this.manger.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
            alDept.AddRange(dept);
            this.cbDept.AddItems(alDept);
            this.cbDept.SelectedIndex = 0;
            return;
        }
        /// <summary>
        /// 初始化医生
        /// </summary>
        private void InitDoc()
        {
            FS.FrameWork.Models.NeuObject obj= new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            alDoc.Add(obj);
            ArrayList doc = manger.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            alDoc.AddRange(doc);
            this.cbDoc.AddItems(alDoc);
            this.cbDoc.SelectedIndex = 0;
            return;
        }
        /// <summary>
        /// 初始化治疗组
        /// </summary>
        //private void InitMetGroup()
        //{
        //    FS.HISFC.BizLogic.RADT.InPatient Manager = new FS.HISFC.BizLogic.RADT.InPatient();
        //    ArrayList MetGroup = new ArrayList();
        //    String strSql = "SELECT code,NAME,spell_code,wb_code FROM com_dictionary WHERE TYPE='MedicalGroup'";
        //    strSql = string.Format(strSql);
        //    DataSet ds = new DataSet();
        //    if (Manager.ExecQuery(strSql, ref ds) == -1)
        //    {
        //        return;
        //    }
        //    if (ds == null || ds.Tables[0] == null)
        //    {
        //        MessageBox.Show("查询错误", "警告,用法加载错误！");
        //    }
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
        //            obj.ID = ds.Tables[0].Rows[i][0].ToString();
        //            obj.Name = ds.Tables[0].Rows[i][1].ToString();
        //            obj.SpellCode = ds.Tables[0].Rows[i][2].ToString();
        //            obj.WBCode = ds.Tables[0].Rows[i][3].ToString();
        //            MetGroup.Add(obj);
        //        }

        //    }
        //    else
        //    {
        //        return;
        //    }

        //    FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
        //    obj1.ID = "ALL";
        //    obj1.Name = "全部";
        //    alMetGroup.Add(obj1);
        //    alMetGroup.AddRange(MetGroup);
        //    this.cbMedGroup.AddItems(alMetGroup);
        //    this.cbMedGroup.SelectedIndex = 0;
        //    return;
        //}
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
                return -1;
            //return base.OnRetrieve(base.beginTime, base.endTime, deptID, docID, mgID, deptName, docName, mgName);
            return base.OnRetrieve(base.beginTime, base.endTime, deptID, docID,  deptName, docName, mgName);
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            deptID = ((FS.FrameWork.Models.NeuObject)alDept[this.cbDept.SelectedIndex]).ID.ToString();
            deptName = ((FS.FrameWork.Models.NeuObject)alDept[this.cbDept.SelectedIndex]).Name.ToString();
        }

        //private void cbMedGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.mgID = ((FS.FrameWork.Models.NeuObject)alMetGroup[this.cbMedGroup.SelectedIndex]).ID.ToString();
        //    this.mgName = ((FS.FrameWork.Models.NeuObject)alMetGroup[this.cbMedGroup.SelectedIndex]).Name.ToString();
        //}

        private void cbDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.docID = ((FS.FrameWork.Models.NeuObject)alDoc[this.cbDoc.SelectedIndex]).ID.ToString();
            this.docName = ((FS.FrameWork.Models.NeuObject)alDoc[this.cbDoc.SelectedIndex]).Name.ToString();
        }
    }
}
