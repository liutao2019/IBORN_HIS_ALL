using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace SOC.Local.Operation
{
    public partial class ucOperationStateManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 手术室状态管理
        /// addby cao-lin
        /// </summary>
        public ucOperationStateManager()
        {
            InitializeComponent();
        }

        #region 属性及变量
        /// <summary>
        /// 是否显示当日所有手术
        /// </summary>
        private bool isShowAllOps = false;
        [Category("设置"),Description("是否显示当日所有手术"),Browsable(true)]
        public bool IsShowAllOps
        {
            get { return isShowAllOps; }
            set { isShowAllOps = value; }
        }

        /// <summary>
        /// 过滤类型
        /// </summary>
        private EnumOperationState enumState = EnumOperationState.等待手术;
        [Category("设置"), Description("过滤类型"), Browsable(true)]
        public EnumOperationState EnumState
        {
            get { return enumState; }
            set { enumState = value; }
        }


        /// <summary>
        /// 当前登陆的手术台信息
        /// </summary>
        FS.HISFC.Models.Operation.OpsRoom curRoom = null;

        /// <summary>
        /// 手术管理类
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Operation.Operation operationManager = new FS.HISFC.BizProcess.Integrate.Operation.Operation();

        /// <summary>
        /// 手术台手术间管理类
        /// </summary>
        public static FS.HISFC.BizLogic.Operation.OpsTableManage tableManager = new FS.HISFC.BizLogic.Operation.OpsTableManage();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 手术台序管理类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper OperationOrderHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 存储当前手术状态
        /// </summary>
        private Hashtable hsOpsState = new Hashtable();
        #endregion

        #region 方法
        protected override void OnLoad(EventArgs e)
        {
            if (!isShowAllOps)
            {
                int param = -1;

                //判断登陆权限
                param = this.JuagePriv();

                if (param == -1)
                {

                    return;
                }

                //初始化设置
                this.Init();

                //查询数据
                this.QueryOnLoad();
            }
            else
            {
                this.Init();
                this.QueryOnLoad();
            }
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        private void QueryOnLoad()
        {
            this.Clear();

            if (!IsShowAllOps)
            {
                #region 按手术间过滤
                if (curRoom == null)
                {
                    return;
                }


                //获取所有手术信息
                ArrayList allOps = operationManager.GetOpsAppList(((FS.HISFC.Models.Base.Employee)operationManager.Operator).Dept.ID, FS.FrameWork.Function.NConvert.ToDateTime(this.neuOperationDate.Value.ToLongDateString()), FS.FrameWork.Function.NConvert.ToDateTime(this.neuOperationDate.Value.AddDays(1).ToLongDateString()), true);

                ArrayList allOpsStat = tableManager.GetOpsState(this.neuOperationDate.Value);



                if (allOps == null || allOps.Count == 0)
                {
                    return;
                }

                if (allOpsStat != null && allOpsStat.Count != 0)
                {
                    foreach (FS.FrameWork.Models.NeuObject opsState in allOpsStat)
                    {
                        hsOpsState.Add(opsState.ID, opsState);
                    }
                }

                //按科室台序排序
                CompareByDeptAndSort c = new CompareByDeptAndSort();
                c.OperatoinOrderHelper = this.OperationOrderHelper;
                allOps.Sort(c);

                foreach (FS.HISFC.Models.Operation.OperationAppllication opsInfo in allOps)
                {
                    if (opsInfo.RoomID != this.curRoom.ID)
                    {
                        continue;
                    }

                    this.neuOperationSpread_汇总.Rows.Add(this.neuOperationSpread_汇总.Rows.Count, 1);

                    //床号
                    //
                    ////{2D90BEDC-96B2-4a4f-8197-21DF10F5EE17}
                    //this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.床号].Text = opsInfo.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.床号].Text = opsInfo.PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? opsInfo.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4):opsInfo.PatientInfo.PVisit.PatientLocation.Bed.ID;

                    //科室
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.科室].Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(opsInfo.PatientInfo.PVisit.PatientLocation.Dept.ID);

                    //手术间
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.手术间].Text = curRoom.Name;
                    //手术名称
                    string opsName = string.Empty;

                    List<FS.HISFC.Models.Operation.OperationInfo> opsLists = operationManager.GetOpsInfoFromApp(opsInfo.ID);

                    foreach (FS.HISFC.Models.Operation.OperationInfo operationInfo in opsLists)
                    {
                        if (operationInfo.IsMainFlag)
                        {
                            opsName += operationInfo.OperationItem.Name;
                        }
                    }
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.手术名称].Text = opsName;
                    //台序
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.台序].Text = OperationOrderHelper.GetObjectFromName(opsInfo.BloodUnit).ID;

                    //性别
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.性别].Text = opsInfo.PatientInfo.Sex.Name;

                    //姓名
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.姓名].Text = opsInfo.PatientInfo.Name;

                    //住院号
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.住院号].Text = opsInfo.PatientInfo.PID.PatientNO;

                    //手术状态
                    string opsState = string.Empty;

                    FS.FrameWork.Models.NeuObject neuOpsState = hsOpsState[opsInfo.ID] as FS.FrameWork.Models.NeuObject;

                    if (neuOpsState == null)
                    {
                        opsState = EnumOperationState.未选择.ToString();
                    }
                    else
                    {
                        opsState = neuOpsState.Name;
                    }

                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.状态].Text = opsState;

                    this.neuOperationSpread_汇总.Rows[this.neuOperationSpread_汇总.Rows.Count - 1].Tag = opsInfo;
                }
                //this.Save();
                #endregion
            }
           
            else
            {
                #region 按状态过滤
                string deptCode = this.GetDeptCodeByMZDept(((FS.HISFC.Models.Base.Employee)tableManager.Operator).Dept.ID);
                ArrayList allOps = operationManager.GetOpsAppList(deptCode, FS.FrameWork.Function.NConvert.ToDateTime(this.neuOperationDate.Value.ToLongDateString()), FS.FrameWork.Function.NConvert.ToDateTime(this.neuOperationDate.Value.AddDays(1).ToLongDateString()), true);

                ArrayList allOpsStat = tableManager.GetOpsState(this.neuOperationDate.Value);
             

                if (allOps == null || allOps.Count == 0)
                {
                    return;
                }

                if (allOpsStat != null && allOpsStat.Count != 0)
                {
                    foreach (FS.FrameWork.Models.NeuObject opsState in allOpsStat)
                    {
                        hsOpsState.Add(opsState.ID, opsState);
                    }
                }
                foreach (FS.HISFC.Models.Operation.OperationAppllication opsInfo in allOps)
                {
                    if (hsOpsState[opsInfo.ID] == null)
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty((hsOpsState[opsInfo.ID] as FS.FrameWork.Models.NeuObject).Name)&&(hsOpsState[opsInfo.ID] as FS.FrameWork.Models.NeuObject).Name != enumState.ToString())
                    {
                        continue;
                    }
                    this.neuOperationSpread_汇总.Rows.Add(this.neuOperationSpread_汇总.Rows.Count, 1);

                    //床号
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.床号].Text = opsInfo.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);

                    //科室
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.科室].Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(opsInfo.PatientInfo.PVisit.PatientLocation.Dept.ID);

                    //手术间
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.手术间].Text = tableManager.GetRoomByID(opsInfo.RoomID).Name;
                    //手术名称
                    string opsName = string.Empty;

                    List<FS.HISFC.Models.Operation.OperationInfo> opsLists = operationManager.GetOpsInfoFromApp(opsInfo.ID);

                    foreach (FS.HISFC.Models.Operation.OperationInfo operationInfo in opsLists)
                    {
                        if (operationInfo.IsMainFlag)
                        {
                            opsName += operationInfo.OperationItem.Name;
                        }
                    }
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.手术名称].Text = opsName;
                    //台序
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.台序].Text = OperationOrderHelper.GetObjectFromName(opsInfo.BloodUnit).ID;

                    //性别
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.性别].Text = opsInfo.PatientInfo.Sex.Name;

                    //姓名
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.姓名].Text = opsInfo.PatientInfo.Name;

                    //住院号
                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.住院号].Text = opsInfo.PatientInfo.PID.PatientNO;

                    //手术状态
                    string opsState = string.Empty;

                    FS.FrameWork.Models.NeuObject neuOpsState = hsOpsState[opsInfo.ID] as FS.FrameWork.Models.NeuObject;

                    if (neuOpsState == null)
                    {
                        opsState = EnumOperationState.未选择.ToString();
                    }
                    else
                    {
                        opsState = neuOpsState.Name;
                    }

                    this.neuOperationSpread_汇总.Cells[this.neuOperationSpread_汇总.Rows.Count - 1, (int)EnumColSet.状态].Text = opsState;

                    this.neuOperationSpread_汇总.Rows[this.neuOperationSpread_汇总.Rows.Count - 1].Tag = opsInfo;
                }
                #endregion
            }
        }

        /// <summary>
        /// 根据麻醉科获取其对应的手术室
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetDeptCodeByMZDept(string deptCode)
        {
            string strSql = string.Empty;
            if (tableManager.Sql.GetSql("Operator.Operator.LED.GetOpsDept", ref strSql) == -1)
            {
                return string.Empty;
            }
            strSql = string.Format(strSql, deptCode);
            return tableManager.ExecSqlReturnOne(strSql);

        }

        /// <summary>
        /// 清空所有信息
        /// </summary>
        private void Clear()
        {
            this.neuOperationSpread_汇总.Rows.Count = 0;

            hsOpsState = new Hashtable();
        }

        /// <summary>
        /// 初始化设置
        /// </summary>
        private void Init()
        {
            //初始化日期
            this.InitDateTime();

            //初始化Farpoint列
            this.InitFarpoint();

            //加载常数字典
            this.InitCons();
   
        }

        /// <summary>
        /// 加载常数字典
        /// </summary>
        private void InitCons()
        {
            ArrayList alOperatoinOrder = consMgr.GetAllList("OperatoinOrder");
            OperationOrderHelper.ArrayObject = alOperatoinOrder;
        }

        /// <summary>
        /// 初始化Farpoint列设置
        /// </summary>
        private void InitFarpoint()
        {
            this.neuOperationSpread_汇总.Rows.Count = 0;

            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            
            string[] operationState = new string[(int)EnumOperationState.术毕回ICU + 1];

            operationState[0] = EnumOperationState.未选择.ToString();

            operationState[1] = EnumOperationState.等待手术.ToString();

            operationState[2] = EnumOperationState.手术中.ToString();

            operationState[3] = EnumOperationState.术毕回病房.ToString();

            operationState[4] = EnumOperationState.术毕回麻恢室.ToString();

            operationState[5] = EnumOperationState.术毕回ICU.ToString();

            comboBoxCellType.Items = operationState;

            this.neuOperationSpread_汇总.Columns[(int)EnumColSet.状态].CellType = comboBoxCellType;
            
        }

        /// <summary>
        /// 初始化日期
        /// </summary>
        private void InitDateTime()
        {
            this.neuOperationDate.Value = DateTime.Now.AddDays(0);
        }

        /// <summary>
        /// 判断登陆权限
        /// 根据维护的手术间信息判断登陆IP地址
        /// </summary>
        private int JuagePriv()
        {
            string localIP = GetLocalIp();
            if (string.IsNullOrEmpty(localIP))
            {
                MessageBox.Show("获取本地IP地址有误，请联系管理员!");
                return -1;
            }
            ArrayList allRoom = tableManager.GetRoomsByDept(((FS.HISFC.Models.Base.Employee)tableManager.Operator).Dept.ID);

            if (allRoom == null || allRoom.Count == 0)
            {
                MessageBox.Show("未找到当前登陆科室的有效手术间信息!");
                return -1;
            }

            bool isFind = false;
           
            foreach (FS.HISFC.Models.Operation.OpsRoom roomInfo in allRoom)
            {
                if (localIP == roomInfo.IpAddress)
                {
                    isFind = true;
                    curRoom = roomInfo.Clone();
                }
            }
            if (!isFind)
            {
                MessageBox.Show("未找到当前电脑ip对应的手术间，请联系信息科!");
                return -1;
            }
            if (!this.IsShowAllOps)
            {
                this.nlbCurRoom.Text = "温馨提示：您当前选择的是" + curRoom.Name;
            }
            return 1;
        }

        /// <summary>
        /// 保存手术信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        /// <summary>
        /// 保存手术信息
        /// </summary>
        private void Save()
        {
            if (this.neuOperationSpread_汇总.Rows.Count == 0)
            {
                return;
            }
            for (int index = 0; index < this.neuOperationSpread_汇总.Rows.Count; index++)
            {
                FS.HISFC.Models.Operation.OperationAppllication opsInfo = this.neuOperationSpread_汇总.Rows[index].Tag as FS.HISFC.Models.Operation.OperationAppllication;
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //如果存在则根据手术编号去更新，否则直接插入一条状态记录信息
                if (hsOpsState.Contains(opsInfo.ID))
                {
                    if (tableManager.UpdateOpsState(opsInfo, this.neuOperationSpread_汇总.Cells[index,(int)EnumColSet.状态].Text.ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(tableManager.Err);
                        return;
                    }
                }
                else
                {
                    if (tableManager.InsertOpsState(opsInfo,EnumOperationState.未选择.ToString()) == -1)
                    {
                        if (tableManager.UpdateOpsState(opsInfo, this.neuOperationSpread_汇总.Cells[index, (int)EnumColSet.状态].Text.ToString()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tableManager.Err);
                            return;
                        }
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功");
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryOnLoad();
            return 1;
        }

        /// <summary>
        /// 获取本地IP地址
        /// </summary>
        /// <returns></returns>
        private string GetLocalIp()
        {
           try
            {
                string HostName = Dns.GetHostName(); //得到主机名

                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {

                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                    return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取本机IP出错:" + ex.Message);
                return "";
            }
        }
        #endregion
    }

    /// <summary>
    /// Farpoint列枚举
    /// </summary>
    public enum EnumColSet
    { 
        手术间,
        台序,
        科室,
        床号,
        姓名,
        性别,
        住院号,
        手术名称,
        状态
    }

    public class CompareByDeptAndSort : IComparer
    {
        public FS.FrameWork.Public.ObjectHelper OperatoinOrderHelper = new FS.FrameWork.Public.ObjectHelper();
        #region IComparer 成员
        public int Compare(object x, object y)
        {
            if ((x is FS.HISFC.Models.Operation.OperationAppllication) && (y is FS.HISFC.Models.Operation.OperationAppllication))
            {
                string ox = string.Empty;
                string oy = string.Empty;
                if (!string.IsNullOrEmpty((x as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.ID) && !string.IsNullOrEmpty((x as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.ID) && !string.IsNullOrEmpty((x as FS.HISFC.Models.Operation.OperationAppllication).RoomID) && !string.IsNullOrEmpty((y as FS.HISFC.Models.Operation.OperationAppllication).RoomID))
                {
                    decimal oxRoom = 0;
                    decimal oyRoom = 0;
                    try
                    {
                        oxRoom = FS.FrameWork.Function.NConvert.ToDecimal(ucOperationStateManager.tableManager.GetRoomByID((x as FS.HISFC.Models.Operation.OperationAppllication).RoomID).InputCode);
                        oyRoom = FS.FrameWork.Function.NConvert.ToDecimal(ucOperationStateManager.tableManager.GetRoomByID((y as FS.HISFC.Models.Operation.OperationAppllication).RoomID).InputCode);
                    }
                    catch { }
                    ox = oxRoom.ToString() + ucOperationStateManager.tableManager.GetTableNameFromID((x as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.ID);

                    oy = oyRoom.ToString() + ucOperationStateManager.tableManager.GetTableNameFromID((y as FS.HISFC.Models.Operation.OperationAppllication).OpsTable.ID);
                }
                else
                {
                    ox = (x as FS.HISFC.Models.Operation.OperationAppllication).PatientInfo.PVisit.PatientLocation.NurseCell.ID + OperatoinOrderHelper.GetObjectFromName((x as FS.HISFC.Models.Operation.OperationAppllication).BloodUnit).ID;
                    oy = (y as FS.HISFC.Models.Operation.OperationAppllication).PatientInfo.PVisit.PatientLocation.NurseCell.ID + OperatoinOrderHelper.GetObjectFromName((y as FS.HISFC.Models.Operation.OperationAppllication).BloodUnit).ID;
                }
                return ox.CompareTo(oy);
            }
            return 1;
        }

        #endregion
    }
}
