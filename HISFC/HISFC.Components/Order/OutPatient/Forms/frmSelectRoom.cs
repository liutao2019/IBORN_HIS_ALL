using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    /// <summary>
    /// 门诊医生诊室选择界面
    /// </summary>
    public partial class frmSelectRoom : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmSelectRoom(ArrayList aldepts)
        {
            InitializeComponent();

            if (aldepts == null)
            {
                aldepts = new ArrayList();
            }

            this.alDepts = aldepts;

            //增加取消功能 2011-1-3 houwb{DA7F7F3C-C9A6-4bcf-9181-93A6238B13F7}
            //this += new CancelEventHandler(frmSelectRoom_Closing);
        }


        #region 变量
        
        FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();
        
        private bool isManager = CacheManager.LogEmpl.IsManager;

        public event FS.FrameWork.WinForms.Forms.OKHandler OKEvent;
        public ArrayList alFZDepts;
        private ArrayList alDepts = null;//科室数组

        /// <summary>
        /// 是否启用分诊系统 1 启用 其他 不启用
        /// </summary>
        public bool pValue;

        #endregion

        private void frmSelectRoom_Load(object sender, System.EventArgs e)
		{
		     this.helper.ArrayObject = alFZDepts;
             this.SetList();
		}
		
		/// <summary>
		/// 显示列表
		/// </summary>
		private void SetList()
		{
			this.neuListBox1.Items.Clear();

            foreach (FS.FrameWork.Models.NeuObject obj in alDepts)
            {
                try
                {
                    if (this.pValue 
                        && this.helper.GetObjectFromID((CacheManager.OutOrderMgr.Operator as FS.HISFC.Models.Base.Employee).Dept.ID) != null)
                    {
                        ArrayList al = new ArrayList();
                        al = CacheManager.InterMgr.QuerySeatByRoomNo(obj.ID);
                        if (al.Count <= 0)
                        {
                            continue;
                        }
                        for (int i = 0; i < al.Count; i++)
                        {
                            FS.FrameWork.Models.NeuObject rObj = al[i] as FS.FrameWork.Models.NeuObject;
                            rObj.Name = obj.Name + "--" + rObj.Name;
                            this.neuListBox1.Items.Add(rObj);
                        }
                    }
                    else
                    {
                        this.neuListBox1.Items.Add(obj);
                    }
                }
                catch { }
            }

            #region 获取分诊队列中诊室信息

            FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();
            DateTime current = queueMgr.GetDateTimeFromSysDateTime();
            string noonID = GetNoon(current);//午别
            if (!string.IsNullOrEmpty(noonID))
            {
                FS.HISFC.Models.Nurse.Queue queueObj = queueMgr.GetQueueByDoct(queueMgr.Operator.ID, current, noonID);

                if (queueObj != null)
                {
                    FS.FrameWork.Models.NeuObject rObj=null;
                    for (int i = 0; i < this.neuListBox1.Items.Count;i++ )
                    {
                        rObj = neuListBox1.Items[i] as FS.FrameWork.Models.NeuObject;

                        if (rObj.ID == queueObj.Console.ID)
                        {
                            this.neuListBox1.SelectedItem = rObj;
                            break;
                        }
                    }

                    return;
                }
            }

            #endregion

            //if (this.neuListBox1.Items.Count > 0)
            //{
            //    this.neuListBox1.SelectedIndex = 0;
            //}
		}

        ArrayList alNoon = null;

        /// <summary>
        /// 获取午别
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string GetNoon(DateTime current)
        {
            if (alNoon == null)
            {
                FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

                alNoon = noonMgr.Query();
            }
            if (alNoon == null)
            {
                return null;
            }
            int time = int.Parse(current.ToString("HHmmss"));

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (time >= int.Parse(obj.StartTime.ToString("HHmmss")) &&
                   time <= int.Parse(obj.EndTime.ToString("HHmmss")))
                {
                    return obj.ID;
                }
            }

            return "";
        }

		/// <summary>
		/// 获得当前房间
		/// </summary>
		public FS.FrameWork.Models.NeuObject SelectRoom
		{
			get
			{
				return (FS.FrameWork.Models.NeuObject)this.neuListBox1.SelectedItem;
			}
		}

		/// <summary>
		/// 关闭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmSelectRoom_Closing(object sender, CancelEventArgs e)
		{
            //this.Close();
		}

        private void cancel_Click(object sender, EventArgs e)
        {
            //增加取消功能 2011-1-3 houwb{DA7F7F3C-C9A6-4bcf-9181-93A6238B13F7}
            this.DialogResult = DialogResult.No;
            this.Close();
        }


        /// <summary>
        /// 是否用新分诊流程
        /// </summary>
        private bool isNewAssign = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("LHMZ02", false, "0"));

        /// <summary>
        /// 是否医生站自动更新分诊队列
        /// </summary>
        private bool isAutoDoctQueue = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("LHMZ09", false, "0"));

        #region 进入诊台
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuListBox1_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = (FS.FrameWork.Models.NeuObject)this.neuListBox1.SelectedItem;
                if (this.CheckUpdateQueue(obj) != 0)
                {
                    this.DialogResult = DialogResult.OK;
                    OKEvent(sender, obj);
                }
            }
            catch { }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = (FS.FrameWork.Models.NeuObject)this.neuListBox1.SelectedItem;
                if (this.CheckUpdateQueue(obj) != 0)
                {
                    this.DialogResult = DialogResult.OK;
                    OKEvent(sender, obj);
                }
            }
            catch { }
        }

        /// <summary>
        /// 敲回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuListBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    FS.FrameWork.Models.NeuObject obj = (FS.FrameWork.Models.NeuObject)this.neuListBox1.SelectedItem;
                    if (this.CheckUpdateQueue(obj) != 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        OKEvent(sender, obj);
                    }
                }
                catch { }
            }
        }
        #endregion

        /// <summary>
        /// 更新门诊队列表的条件判断
        /// </summary>
        /// <param name="obj"></param>
        private int CheckUpdateQueue(FS.FrameWork.Models.NeuObject obj)
        {
            //新分诊流程 且 开启自动更新队列才调用
            //同时排除管理员，防止管理员调试正式库时更新了正常的数据
            if (this.isNewAssign && this.isAutoDoctQueue && !this.isManager)
            {
                int rtn = this.UpdateDoctQueue(obj);
                switch (rtn)
                {
                    case 1: break;
                    case 0: MessageBox.Show("您要登录的诊台已被其他医生占用，请确认诊台。\n\n"
                        + "若诊台确认无误，请通知护士删除对应的医生队列。"); break;
                    case -1: MessageBox.Show("写入医生队列信息出错。", "不影响看诊"); break;
                    case -2: MessageBox.Show("没有找到对应的门诊科室。", "不影响看诊"); break;
                }
                return rtn;
            }
            return 1;
        }

        /// <summary>
        /// 将当前登录的医生更新进门诊队列表，用于新分诊流程
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>1 队列被正常写入  0 诊台被占用  -1 出错  -2 没有找到对应的门诊科室</returns>
        private int UpdateDoctQueue(FS.FrameWork.Models.NeuObject obj)
        {
            FS.HISFC.Models.Nurse.Seat seatObj = obj as FS.HISFC.Models.Nurse.Seat;
            FS.HISFC.Models.Nurse.Queue queueObj = new FS.HISFC.Models.Nurse.Queue();

            FS.HISFC.BizLogic.Nurse.Queue queueBizlogic = new FS.HISFC.BizLogic.Nurse.Queue();
            FS.HISFC.BizLogic.Manager.Department departBizlogic = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizLogic.Registration.Noon noonBizlogic = new FS.HISFC.BizLogic.Registration.Noon();
            DateTime dtNow = noonBizlogic.GetDateTimeFromSysDateTime();

            #region 队列赋值
            queueObj.Noon.ID = noonBizlogic.getNoon(dtNow).ID;
            queueObj.Doctor.ID = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).ID;
            queueObj.Doctor.Name = ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).Name;
            queueObj.ExpertFlag =
                ((FS.HISFC.Models.Base.Employee)(FS.FrameWork.Management.Connection.Operator)).IsExpert ? "1" : "0";
            queueObj.Dept.ID = ((FS.HISFC.Models.Base.Employee)queueBizlogic.Operator).Dept.ID;
            queueObj.Dept.Name = ((FS.HISFC.Models.Base.Employee)queueBizlogic.Operator).Dept.Name;

            queueObj.SRoom.ID = seatObj.PRoom.ID;
            queueObj.SRoom.Name = seatObj.PRoom.Name;
            queueObj.Console.ID = seatObj.ID;
            queueObj.Console.Name = seatObj.Name;

            queueObj.QueueDate = dtNow;
            #endregion

            ArrayList alDept = departBizlogic.GetNurseStationFromDept(queueObj.Dept);

            if (alDept != null && alDept.Count > 0)
            {
                queueObj.ID = (alDept[0] as FS.FrameWork.Models.NeuObject).ID;//门诊护士站ID
                return queueBizlogic.UpdateDoctQueue(queueObj);
            }
            else
            {
                return -2;
            }
        }

        /// <summary>
        /// 临时出诊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

