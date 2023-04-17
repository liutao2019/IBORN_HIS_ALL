using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.FrameWork.Function;

namespace SOC.Local.Assign.ShenZhen.BinHai.IAssignDisplay
{
    public partial class ucQueueForDisplay : UserControl
    {
        public ucQueueForDisplay(bool isShowName)
        {
            InitializeComponent();
            isDiaplayName = isShowName;
          
            //查找护士站下面的科室
            FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alDepts = statManager.LoadByParent("14", ((FS.HISFC.Models.Base.Employee)statManager.Operator).Dept.ID);
            if (alDepts != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in alDepts)
                {
                    this.nurseStationLis += obj.Name + "—";
                }
                string temp = "—";
                this.nurseStationLis = this.nurseStationLis.Substring(0, this.nurseStationLis.Length - temp.Length);
            }
        }

        private FS.SOC.HISFC.Assign.Models.Queue queue = new FS.SOC.HISFC.Assign.Models.Queue();

        private bool isDiaplayName = true;

        /// <summary>
        /// 一屏显示数
        /// </summary>
        int totalNum = 8;

        /// <summary>
        /// 所有的患者信息
        /// </summary>
        ArrayList alPatients = new ArrayList();

        /// <summary>
        /// 当天日期
        /// </summary>
        DateTime dayDate = DateTime.Today;

        /// <summary>
        /// 分诊病人
        /// </summary>
        private ArrayList alAssign = new ArrayList();

        /// <summary>
        /// 护士站哈希表
        /// </summary>
        private string nurseStationLis = string.Empty;

        /// <summary>
        /// 设置界面字体
        /// </summary>
        /// <param name="fontSize"></param>
        public void SetFontSize(float titleFontSize, float fpFontSize, float bottomFontSize)
        {
            Font titleFont = new System.Drawing.Font("宋体", titleFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Font = titleFont;
            this.lblRoom.Font = titleFont;

            Font fpFont = new System.Drawing.Font("宋体", fpFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Rows.Default.Font = fpFont;
            this.neuSpread1_Sheet1.Rows.Default.Height = fpFont.Height + 2;

            Font bottomFont = new System.Drawing.Font("宋体", bottomFontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWaitCount.Font = bottomFont;

        }

        public FS.SOC.HISFC.Assign.Models.Queue Queue
        {
            get
            {
                return this.queue;
            }
            set
            {
                this.queue = value;
                FS.HISFC.Models.Base.Employee currOper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.SOC.HISFC.Assign.BizLogic.Assign asignManager = new FS.SOC.HISFC.Assign.BizLogic.Assign();
                this.lblName.Text = string.IsNullOrEmpty(this.nurseStationLis) == true ? queue.AssignDept.Name : this.nurseStationLis;
                this.lblRoom.Text = "";
                DateTime currenttime = CommonController.CreateInstance().GetSystemTime();
                ArrayList alQueueNum = (new FS.SOC.HISFC.Assign.BizLogic.Queue()).QueryNurseQueueNum(queue.AssignNurse.ID, currenttime.Date);
                FS.FrameWork.Models.NeuObject queueNum = new FS.FrameWork.Models.NeuObject();
                if (alQueueNum.Count > 0)
                { queueNum = alQueueNum[0] as FS.FrameWork.Models.NeuObject; }
                this.lblWaitCount.Text = "候诊人数：" + NConvert.ToDecimal(queueNum.Name);
                //this.neuSpread1_Sheet1.RowCount = 0;
                //this.AddItem(al, queue);
                //al = asignManager.Query(currOper.Dept.ID, queue.QueueDate.Date, FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
                //ArrayList al = asignManager.QueryFirstCall(currOper.Nurse.ID, currenttime.ToString("yyyy-MM-dd"));
                ArrayList al = asignManager.Query(currOper.Dept.ID, queue.QueueDate.Date, FS.HISFC.Models.Nurse.EnuTriageStatus.In);
                if (al != null && al.Count > 0)
                {
                    //ArrayList all = asignManager.Query(currOper.Dept.ID, queue.QueueDate.Date, FS.HISFC.Models.Nurse.EnuTriageStatus.In);
                    ArrayList alDelay = asignManager.Query(currOper.Dept.ID, queue.QueueDate.Date, FS.HISFC.Models.Nurse.EnuTriageStatus.Delay);
                    ArrayList alAssign = asignManager.Query(currOper.Dept.ID, queue.QueueDate.Date, FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive);
                    //alAssign.AddRange(all);
                    alAssign.AddRange(alDelay);
                    this.AddItem(al, queue, alAssign);
                }
                if (currenttime.Date > dayDate)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        this.neuSpread1_Sheet1.Cells[i, 0].Text = "";
                        this.neuSpread1_Sheet1.Cells[i, 1].Text = "";
                        this.neuSpread1_Sheet1.Cells[i, 2].Text = "";
                        this.neuSpread1_Sheet1.Cells[i, 3].Text = "";
                        this.neuSpread1_Sheet1.Cells[i, 4].Text = "";
                        dayDate = currenttime.Date;
                        alPatients.Clear();
                    }
                }
                //this.lblName.Text = queue.Name + "（" + queue.Order.ToString() + "）";
            }
        }

        private void AddItem(ArrayList al, FS.SOC.HISFC.Assign.Models.Queue obj, ArrayList alAssign)
        {
            if (al != null)
            {
                //for (int i = 0; i < al.Count; i++)
                foreach (FS.SOC.HISFC.Assign.Models.Assign assign in al)
                {
                    #region 显示修改
                    if (this.neuSpread1_Sheet1.RowCount > 0)
                    {
                        alPatients.Clear();
                        
                        for (int rowCount = 0; rowCount < this.neuSpread1_Sheet1.RowCount; rowCount++)
                        {
                            FS.FrameWork.Models.NeuObject objTemp = new FS.FrameWork.Models.NeuObject();
                            objTemp.ID = this.neuSpread1_Sheet1.Cells[rowCount, 0].Text;
                            objTemp.Name = this.neuSpread1_Sheet1.Cells[rowCount, 1].Text;
                            objTemp.Memo = this.neuSpread1_Sheet1.Cells[rowCount, 2].Text;
                            if (alAssign != null && alAssign.Count > 0)
                            {
                                objTemp.User01 = "已诊";
                                foreach (FS.SOC.HISFC.Assign.Models.Assign objAssign in alAssign)
                                {
                                    if (objAssign.SeeNO == objTemp.ID)
                                    {
                                        if (objAssign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                                        {
                                            objTemp.User01 = "就诊";//this.neuSpread1_Sheet1.Cells[rowCount, 3].Text
                                        }
                                        else if (objAssign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Arrive)
                                        {
                                            objTemp.User01 = "到诊";
                                        }
                                        else
                                        {
                                            objTemp.User01 = "暂不看诊";
                                        }
                                    }
                                }
                            }
                            objTemp.User02 = this.neuSpread1_Sheet1.Cells[rowCount, 4].Text;
                            if (!string.IsNullOrEmpty(objTemp.ID))
                            {
                                alPatients.Add(objTemp);
                            }
                        }
                    }
                    #endregion

                    //FS.SOC.HISFC.Assign.Models.Assign assign = al[i] as FS.SOC.HISFC.Assign.Models.Assign;
                    //int row = this.neuSpread1_Sheet1.RowCount;
                    //this.neuSpread1_Sheet1.AddRows(row, 1);
                    //this.neuSpread1_Sheet1.Cells[row, 0].Text = queue.Order.ToString() + "-" + assign.SeeNO.ToString();
                    this.neuSpread1_Sheet1.Cells[0, 0].Text = assign.SeeNO.ToString();

                    //this.neuSpread1_Sheet1.Cells[row, 1].Text = assign.Register.Name;

                    if (isDiaplayName)
                    {
                        this.neuSpread1_Sheet1.Cells[0, 1].Text = assign.Register.Name;
                    }
                    else
                    {
                        //string name = assign.Register.Name;
                        //if (assign.Register.Name.Length > 0)
                        //{
                        //    name = assign.Register.Name.Substring(0, 1);
                        //    for (int ii = 0; ii < assign.Register.Name.Length - 1; ii++)
                        //    {
                        //        name += "*";
                        //    }
                        //}

                        //this.neuSpread1_Sheet1.Cells[0, 1].Text = name;
                        this.neuSpread1_Sheet1.Cells[0, 1].Text = assign.Register.DoctorInfo.SeeNO.ToString();// + assign.Register.Name.Substring(0, 1).ToString() + "**";
                    }


                    if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                    {
                        this.neuSpread1_Sheet1.Cells[0, 2].Text = assign.Queue.SRoom.Name;
                        this.neuSpread1_Sheet1.Cells[0, 3].Text = "就诊";
                        this.neuSpread1_Sheet1.Cells[0, 4].Text = assign.InTime.ToShortDateString();
                        //this.neuSpread1_Sheet1.Cells[0, 0].ForeColor = Color.Lime;
                        this.neuSpread1_Sheet1.Cells[0, 1].ForeColor = Color.Red;
                        this.neuSpread1_Sheet1.Cells[0, 2].ForeColor = Color.Red;
                        this.neuSpread1_Sheet1.Cells[0, 3].ForeColor = Color.Red;
                    }
                    //else if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Triage)
                    //{
                    //    this.neuSpread1_Sheet1.Cells[0, 2].Text = assign.Queue.SRoom.Name;
                    //    this.neuSpread1_Sheet1.Cells[0, 3].Text = "请等候";
                    //    //this.neuSpread1_Sheet1.Cells[0, 0].ForeColor = Color.Gold;
                    //    this.neuSpread1_Sheet1.Cells[0, 1].ForeColor = Color.Gold;
                    //    this.neuSpread1_Sheet1.Cells[0, 2].ForeColor = Color.Gold;
                    //    this.neuSpread1_Sheet1.Cells[0, 3].ForeColor = Color.Gold;
                    //}

                    #region 显示修改
                    if (alPatients.Count > 0)
                    {
                        //删除在屏幕已添加患者信息
                        for (int count = 0; count < alPatients.Count; count++)
                        {
                            FS.FrameWork.Models.NeuObject objal = alPatients[count] as FS.FrameWork.Models.NeuObject;

                            if (assign.SeeNO == objal.ID)
                            {
                                alPatients.Remove(objal);
                                count--;
                            }
                        }
                    }


                    #region 添加已有的患者信息
                    if (alPatients.Count > 0)
                    {
                        if (alPatients.Count > totalNum - 1)
                        {
                            for (int count = 0; count < totalNum - 1; count++)
                            {
                                FS.FrameWork.Models.NeuObject objAl = alPatients[count] as FS.FrameWork.Models.NeuObject;

                                this.neuSpread1_Sheet1.Cells[count + 1, 0].Text = objAl.ID;
                                //if (objAl.ID.IndexOf('预') > 0)
                                //    this.neuSpread1_Sheet1.Cells[count + 1, 0].Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                                //else
                                //    this.neuSpread1_Sheet1.Cells[count + 1, 0].Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                                this.neuSpread1_Sheet1.Cells[count + 1, 1].Text = objAl.Name;
                                this.neuSpread1_Sheet1.Cells[count + 1, 2].Text = objAl.Memo;
                                this.neuSpread1_Sheet1.Cells[count + 1, 3].Text = objAl.User01;
                                this.neuSpread1_Sheet1.Cells[count + 1, 4].Text = objAl.User02;
                                dayDate = NConvert.ToDateTime(objAl.User02);
                                //this.neuSpread1_Sheet1.Cells[count + 1, 0].ForeColor = Color.Lime;
                                if (objAl.User01 == "就诊")
                                {
                                    this.neuSpread1_Sheet1.Cells[count + 1, 1].ForeColor = Color.Lime;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 2].ForeColor = Color.Lime;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 3].ForeColor = Color.Lime;
                                }
                                else if (objAl.User01 == "到诊")
                                {
                                    this.neuSpread1_Sheet1.Cells[count + 1, 1].ForeColor = Color.Gold;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 2].ForeColor = Color.Gold;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 3].ForeColor = Color.Gold;
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.Cells[count + 1, 1].ForeColor = Color.Gold;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 2].ForeColor = Color.Gold;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 3].ForeColor = Color.Gold;
                                }
                            }
                        }
                        else
                        {
                            for (int count = 0; count < alPatients.Count; count++)
                            {
                                FS.FrameWork.Models.NeuObject objAl = alPatients[count] as FS.FrameWork.Models.NeuObject;
                                this.neuSpread1_Sheet1.Cells[count + 1, 0].Text = objAl.ID;
                                //if (objAl.ID.IndexOf('预') > 0)
                                //    this.neuSpread1_Sheet1.Cells[count + 1, 0].Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                                //else
                                //    this.neuSpread1_Sheet1.Cells[count + 1, 0].Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                                this.neuSpread1_Sheet1.Cells[count + 1, 1].Text = objAl.Name;
                                this.neuSpread1_Sheet1.Cells[count + 1, 2].Text = objAl.Memo;
                                this.neuSpread1_Sheet1.Cells[count + 1, 3].Text = objAl.User01;
                                this.neuSpread1_Sheet1.Cells[count + 1, 4].Text = objAl.User02;
                                dayDate = NConvert.ToDateTime(objAl.User02);
                                //this.neuSpread1_Sheet1.Cells[count + 1, 0].ForeColor = Color.Lime;
                                if (objAl.User01 == "就诊")
                                {
                                    this.neuSpread1_Sheet1.Cells[count + 1, 1].ForeColor = Color.Lime;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 2].ForeColor = Color.Lime;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 3].ForeColor = Color.Lime;
                                }
                                else if (objAl.User01 == "到诊")
                                {
                                    this.neuSpread1_Sheet1.Cells[count + 1, 1].ForeColor = Color.Gold;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 2].ForeColor = Color.Gold;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 3].ForeColor = Color.Gold;
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.Cells[count + 1, 1].ForeColor = Color.Gold;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 2].ForeColor = Color.Gold;
                                    this.neuSpread1_Sheet1.Cells[count + 1, 3].ForeColor = Color.Gold;
                                }
                            }
                        }
                    }
                    #endregion
                    #endregion
                }

            }

        }
    }
}
