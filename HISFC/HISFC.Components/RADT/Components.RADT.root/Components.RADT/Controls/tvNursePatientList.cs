using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 护士站患者列表]<br></br>
    /// 病房管理专用：本区患者、待接诊患者、转入患者、转出患者、出院患者
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人='张琦'
    ///		修改时间='2008-09-3'
    ///		修改目的='控制出院召回患者的有效天数'
    ///		修改描述='在出院患者列表中只显示在有效天数内的患者列表信息'
    ///  />
    /// </summary>
    public partial class tvNursePatientList : Neusoft.HISFC.Components.Common.Controls.tvPatientList
    {
        #region 初始化

        public tvNursePatientList()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                Init();
            }
        }

        public tvNursePatientList(string type)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.pateintType = type;
                Init();
            }
        }

        public tvNursePatientList(IContainer container)
            : this()
        {
            container.Add(this);

            //InitializeComponent();
        }

        #endregion

        #region 变量

        Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = null;
        Neusoft.HISFC.BizLogic.RADT.InPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 病区包含的科室列表
        /// </summary>
        private ArrayList alDepts = null;

        /// <summary>
        /// 当前操作员
        /// </summary>
        Neusoft.HISFC.Models.Base.Employee oper = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;

        /// <summary>
        /// 列表加载的患者类型
        /// 存储枚举的数值，如果多个用|区分
        /// </summary>
        private string pateintType = "ALL";

        /// <summary>
        /// 列表加载的患者类型
        /// 存储枚举的数值，如果多个用|区分
        /// </summary>
        public string PateintType
        {
            get
            {
                return pateintType;
            }
            set
            {
                pateintType = value;
            }
        }

        #endregion

        #region 出院召回天数控制 目前无用

        /// <summary>
        /// 出院召回的有效天数
        /// </summary>
        private int callBackVaildDays;

        /// <summary>
        /// 出院召回天数 控制参数
        /// </summary>
        public const string control_id = "ZY0001";

        /// <summary>
        /// 初始化控制参数,获得出院召回的有效天数
        /// </summary>
        private void InitControlParam()
        {
            Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
            this.callBackVaildDays = ctrlParamIntegrate.GetControlParam<int>(control_id, false, 1);
        }

        #endregion

        /// <summary>
        /// 获取病区包含的科室列表
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        private ArrayList GetDepts(string nurseCode)
        {
            if (alDepts == null)
            {
                alDepts = interMgr.QueryDepartment(nurseCode);

            }
            return alDepts;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            this.ShowType = enuShowType.Bed;
            this.Direction = enuShowDirection.Ahead;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return -1;
            }

            InitControlParam();

            this.Refresh();
            return 1;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public new void Refresh()
        {
            try
            {
                this.BeginUpdate();
                this.Nodes.Clear();

                if (radtIntegrate == null)
                {
                    radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();
                }

                ArrayList alPatientList = new ArrayList();//患者列表

                Dictionary<string, object> dicPaient = new Dictionary<string, object>();

                if (pateintType.ToUpper() == "ALL")
                {
                    ////本区在院患者
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.In));

                    ////显示本护理站待接珍患者
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.Arrive));

                    ////显示转入本护理站待接珍患者
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.ShiftIn));

                    ////显示本护理站转科申请的患者
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.ShiftOut));

                    ////显示本护理站出院登记的患者
                    //alPatientList.AddRange(GetPatientList(EnumPatientType.Out));


                    GetPatientListNew(EnumPatientType.In, ref dicPaient);
                    GetPatientListNew(EnumPatientType.Arrive, ref dicPaient);
                    GetPatientListNew(EnumPatientType.ShiftIn, ref dicPaient);
                    GetPatientListNew(EnumPatientType.ShiftOut, ref dicPaient);
                    GetPatientListNew(EnumPatientType.Out, ref dicPaient);
                }
                else
                {
                    string[] types = pateintType.Split('|');

                    for (int i = 0; i < types.Length; i++)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(types[i])
                                && Enum.IsDefined(typeof(EnumPatientType), Neusoft.FrameWork.Function.NConvert.ToInt32(types[i])))
                            {
                                EnumPatientType paitentType = (EnumPatientType)Neusoft.FrameWork.Function.NConvert.ToInt32(types[i]);

                                //alPatientList.AddRange(GetPatientList(paitentType));
                                GetPatientListNew(paitentType, ref dicPaient);
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                //显示所有患者列表
                //this.SetPatient(alPatientList);

                SetPatient(dicPaient);

                this.EndUpdate();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("刷新床位列表出错！\r\n" + ex.Message, "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取本病区患者
        /// </summary>
        /// <param name="al"></param>
        /// <param name="patientType"></param>
        private ArrayList GetPatientList(EnumPatientType patientType)
        {
            if (oper == null)
            {
                return null;
            }

            ArrayList alPatientList = new ArrayList();

            ArrayList al1 = new ArrayList();

            Neusoft.HISFC.Models.Base.EnumInState Status = Neusoft.HISFC.Models.Base.EnumInState.I;

            Neusoft.HISFC.Models.Base.Bed bedInfo = null;

            #region 本区在院患者
            if (patientType == EnumPatientType.In)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;

                //登陆的科室是否是病区
                //存在一个科室对应多个病区时，employee.Nurse.ID存的是登陆科室信息
                bool isNureseDept = true;

                al1 = this.radtManager.PatientQueryByNurseCell(oper.Nurse.ID, Status);

                ArrayList alDept = this.GetDepts(oper.Nurse.ID);

                //存在一个科室对应多个病区时，employee.Nurse.ID存的是登陆科室信息
                //此时如果登陆科室，而不是病区，则查询不到患者
                if (al1.Count == 0)
                {
                    isNureseDept = false;
                    foreach (Neusoft.FrameWork.Models.NeuObject objdept in alDept)
                    {
                        al1.AddRange(this.radtManager.PatientQueryByNurseCell(objdept.ID, Status));
                    }
                }

                if (alDept == null || alDept.Count < 2)
                {
                    //alPatientList.AddRange(al1);

                    //增加护理组显示<护理组,患者列表>
                    Dictionary<string, ArrayList> dicNurseList = new Dictionary<string, ArrayList>();
                    foreach (Neusoft.HISFC.Models.RADT.PatientInfo pInfo in al1)
                    {
                        bedInfo = interMgr.GetBed(pInfo.PVisit.PatientLocation.Bed.ID);
                        string tendGroup = "";
                        if (bedInfo != null)
                        {
                            tendGroup = bedInfo.TendGroup;
                        }
                        if (dicNurseList.ContainsKey(tendGroup))
                        {
                            ArrayList alTend = dicNurseList[tendGroup];
                            alTend.Add(pInfo);
                            dicNurseList[tendGroup] = alTend;
                        }
                        else
                        {
                            ArrayList alTend = new ArrayList();
                            alTend.Add(pInfo);
                            dicNurseList.Add(tendGroup, alTend);
                        }
                    }

                    //没有维护护理组时，不显示护理组
                    if (dicNurseList.Keys.Count == 1)
                    {
                        foreach (string key in dicNurseList.Keys)
                        {
                            alPatientList.Add("本区患者|" + EnumPatientType.In.ToString());
                            alPatientList.AddRange(dicNurseList[key]);
                        }
                    }
                    else
                    {
                        foreach (string key in dicNurseList.Keys)
                        {
                            if (string.IsNullOrEmpty(key))
                            {
                                alPatientList.Add("空" + "|" + EnumPatientType.In.ToString());
                            }
                            else
                            {
                                alPatientList.Add(key + "|" + EnumPatientType.In.ToString());
                            }
                            alPatientList.AddRange(dicNurseList[key]);
                        }
                    }
                }
                else
                {
                    Neusoft.FrameWork.Models.NeuObject objdept = null;
                    Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = null;

                    for (int i = 0; i < alDept.Count; i++)
                    {
                        objdept = alDept[i] as Neusoft.FrameWork.Models.NeuObject;

                        alPatientList.Add(objdept.Name + "|" + EnumPatientType.In.ToString());

                        //增加护理组显示<护理组,患者列表>
                        Dictionary<string, ArrayList> dicNurseList = new Dictionary<string, ArrayList>();

                        for (int j = 0; j < al1.Count; j++)
                        {
                            patientTemp = al1[j] as Neusoft.HISFC.Models.RADT.PatientInfo;

                            if (isNureseDept)
                            {
                                if (patientTemp.PVisit.PatientLocation.Dept.ID.Trim() != objdept.ID.Trim())
                                {
                                    continue;
                                }
                                //if (patientTemp.PVisit.PatientLocation.Dept.ID.Trim() == objdept.ID.Trim())
                                //{
                                //    alPatientList.Add(patientTemp);
                                //}
                            }
                            else
                            {
                                if (patientTemp.PVisit.PatientLocation.NurseCell.ID.Trim() != objdept.ID.Trim())
                                {
                                    continue; ;
                                }
                                //if (patientTemp.PVisit.PatientLocation.NurseCell.ID.Trim() == objdept.ID.Trim())
                                //{
                                //    alPatientList.Add(patientTemp);
                                //}
                            }

                            bedInfo = interMgr.GetBed(patientTemp.PVisit.PatientLocation.Bed.ID);
                            string tendGroup = "";
                            if (bedInfo != null)
                            {
                                tendGroup = bedInfo.TendGroup;
                            }

                            if (dicNurseList.ContainsKey(tendGroup))
                            {
                                ArrayList alTend = dicNurseList[tendGroup];
                                alTend.Add(patientTemp);
                                dicNurseList[tendGroup] = alTend;
                            }
                            else
                            {
                                ArrayList alTend = new ArrayList();
                                alTend.Add(patientTemp);
                                dicNurseList.Add(tendGroup, alTend);
                            }
                        }

                        //没有维护护理组时，不显示护理组
                        if (dicNurseList.Keys.Count == 1)
                        {
                            foreach (string key in dicNurseList.Keys)
                            {
                                if (string.IsNullOrEmpty(key))
                                {
                                    alPatientList.AddRange(dicNurseList[key]);
                                }
                            }
                        }
                        else
                        {
                            foreach (string key in dicNurseList.Keys)
                            {
                                if (string.IsNullOrEmpty(key))
                                {
                                    alPatientList.Add("空" + "|" + EnumPatientType.In.ToString());
                                }
                                else
                                {
                                    alPatientList.Add(key + "|" + EnumPatientType.In.ToString());
                                }
                                alPatientList.AddRange(dicNurseList[key]);
                            }
                        }
                    }
                }
            }
            #endregion

            #region 待接诊患者

            else if (patientType == EnumPatientType.Arrive)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.R;
                alPatientList.Add("待接诊患者|" + EnumPatientType.Arrive.ToString());
                al1 = this.radtIntegrate.QueryPatientByNurseCellAndState(oper.Nurse.ID, Status);//按科室接珍
                alPatientList.AddRange(al1);
            }
            #endregion

            #region 转出患者

            else if (patientType == EnumPatientType.ShiftOut)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;
                alPatientList.Add("转出患者|" + EnumPatientType.ShiftOut.ToString());
                al1 = this.radtManager.QueryPatientShiftOutApplyByNurseCell(oper.Nurse.ID, "1");
                alPatientList.AddRange(al1);
            }
            #endregion

            #region 转入患者

            else if (patientType == EnumPatientType.ShiftIn)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;
                alPatientList.Add("转入患者|" + EnumPatientType.ShiftIn.ToString());
                al1 = this.radtManager.QueryPatientShiftInApplyByNurseCell(oper.Nurse.ID, "1");				//按科室查转入申请的
                alPatientList.AddRange(al1);

            }
            #endregion

            #region 出院登记患者

            else if (patientType == EnumPatientType.Out)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.B;
                alPatientList.Add("出院登记患者|" + EnumPatientType.Out.ToString());
                //根据出院召回的有效天数查询出院登记患者信息
                al1 = this.radtManager.PatientQueryByNurseCellVaildDate(oper.Nurse.ID, Status, callBackVaildDays);
                alPatientList.AddRange(al1);
            }
            #endregion

            return alPatientList;
        }

        /// <summary>
        /// 获取本病区患者
        /// </summary>
        /// <param name="al"></param>
        /// <param name="patientType"></param>
        private int GetPatientListNew(EnumPatientType patientType, ref Dictionary<string, object> dicPatient)
        {
            if (oper == null)
            {
                return -1;
            }

            ArrayList alPatientList = new ArrayList();

            ArrayList al1 = new ArrayList();

            Neusoft.HISFC.Models.Base.Bed bedInfo = null;

            Neusoft.HISFC.Models.Base.EnumInState Status = Neusoft.HISFC.Models.Base.EnumInState.I;

            #region 本区在院患者

            if (patientType == EnumPatientType.In)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;

                //登陆的科室是否是病区
                //存在一个科室对应多个病区时，employee.Nurse.ID存的是登陆科室信息
                bool isNureseDept = true;

                al1 = this.radtManager.PatientQueryByNurseCell(oper.Nurse.ID, Status);

                ArrayList alDept = this.GetDepts(oper.Nurse.ID);

                //存在一个科室对应多个病区时，employee.Nurse.ID存的是登陆科室信息
                //此时如果登陆科室，而不是病区，则查询不到患者
                if (al1.Count == 0)
                {
                    isNureseDept = false;
                    foreach (Neusoft.FrameWork.Models.NeuObject objdept in alDept)
                    {
                        al1.AddRange(this.radtManager.PatientQueryByNurseCell(objdept.ID, Status));
                    }
                }

                if (alDept == null || alDept.Count < 2)
                {
                    //alPatientList.Add("本区患者|" + EnumPatientType.In.ToString());

                    //增加护理组显示<护理组,患者列表>
                    Dictionary<string, ArrayList> dicNurseList = new Dictionary<string, ArrayList>();
                    foreach (Neusoft.HISFC.Models.RADT.PatientInfo pInfo in al1)
                    {
                        bedInfo = interMgr.GetBed(pInfo.PVisit.PatientLocation.Bed.ID);
                        string tendGroup = "";
                        if (bedInfo != null)
                        {
                            tendGroup = bedInfo.TendGroup + "|" + EnumPatientType.In.ToString();
                        }

                        if (dicNurseList.ContainsKey(tendGroup))
                        {
                            ArrayList alTend = dicNurseList[tendGroup];
                            alTend.Add(pInfo);
                            dicNurseList[tendGroup] = alTend;
                        }
                        else
                        {
                            ArrayList alTend = new ArrayList();
                            alTend.Add(pInfo);
                            dicNurseList.Add(tendGroup, alTend);
                        }
                    }

                    //没有维护护理组时，不显示护理组
                    if (dicNurseList.Keys.Count == 1)
                    {
                        foreach (string key in dicNurseList.Keys)
                        {
                            //alPatientList.AddRange(dicNurseList[key]);
                            dicPatient.Add("本区患者" + "|" + EnumPatientType.In.ToString(), dicNurseList[key]);
                        }
                    }
                    else
                    {
                        dicPatient.Add("本区患者" + "|" + EnumPatientType.In.ToString(), dicNurseList);
                        //foreach (string key in dicNurseList.Keys)
                        //{
                        //    if (string.IsNullOrEmpty(key))
                        //    {
                        //        dicPatient.Add("未分组|TendGroup", dicNurseList[key]);
                        //    }
                        //    else
                        //    {
                        //        alPatientList.Add(key + "|" + EnumPatientType.In.ToString());
                        //        dicPatient.Add(key + "|TendGroup", dicNurseList[key]);
                        //    }
                        //}
                    }
                }
                else
                {
                    Neusoft.FrameWork.Models.NeuObject objdept = null;
                    Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = null;

                    for (int i = 0; i < alDept.Count; i++)
                    {
                        objdept = alDept[i] as Neusoft.FrameWork.Models.NeuObject;

                        //alPatientList.Add(objdept.Name + "|" + EnumPatientType.In.ToString());

                        //增加护理组显示<护理组,患者列表>
                        Dictionary<string, ArrayList> dicNurseList = new Dictionary<string, ArrayList>();

                        for (int j = 0; j < al1.Count; j++)
                        {
                            patientTemp = al1[j] as Neusoft.HISFC.Models.RADT.PatientInfo;

                            if (isNureseDept)
                            {
                                if (patientTemp.PVisit.PatientLocation.Dept.ID.Trim() != objdept.ID.Trim())
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (patientTemp.PVisit.PatientLocation.NurseCell.ID.Trim() != objdept.ID.Trim())
                                {
                                    continue; ;
                                }
                            }

                            bedInfo = interMgr.GetBed(patientTemp.PVisit.PatientLocation.Bed.ID);
                            string tendGroup = "";
                            if (bedInfo != null)
                            {
                                tendGroup = bedInfo.TendGroup + "|" + EnumPatientType.In.ToString();
                            }

                            //string tendGroup = SOC.HISFC.BizProcess.Cache.Common.GetBedInfo(patientTemp.PVisit.PatientLocation.Bed.ID).TendGroup + "|" + EnumPatientType.In.ToString();
                            if (dicNurseList.ContainsKey(tendGroup))
                            {
                                ArrayList alTend = dicNurseList[tendGroup];
                                alTend.Add(patientTemp);
                                dicNurseList[tendGroup] = alTend;
                            }
                            else
                            {
                                ArrayList alTend = new ArrayList();
                                alTend.Add(patientTemp);
                                dicNurseList.Add(tendGroup, alTend);
                            }
                        }

                        //没有维护护理组时，不显示护理组
                        if (dicNurseList.Keys.Count == 1)
                        {
                            foreach (string key in dicNurseList.Keys)
                            {
                                dicPatient.Add(objdept.Name + "|" + EnumPatientType.In.ToString(), dicNurseList[key]);
                            }
                        }
                        else
                        {
                            dicPatient.Add(objdept.Name + "|" + EnumPatientType.In.ToString(), dicNurseList);
                            //foreach (string key in dicNurseList.Keys)
                            //{
                            //    if (string.IsNullOrEmpty(key))
                            //    {
                            //        //alPatientList.Add("空" + "|" + EnumPatientType.In.ToString());
                            //        dicPatient.Add(objdept.Name + "|" + objdept.ID, dicNurseList[key]);
                            //    }
                            //    else
                            //    {
                            //        alPatientList.Add(key + "|" + EnumPatientType.In.ToString());
                            //        dicPatient.Add(objdept.Name + "|" + objdept.ID, dicNurseList[key]);
                            //    }
                            //    alPatientList.AddRange(dicNurseList[key]);
                            //}
                        }
                    }
                }
            }
            #endregion

            #region 待接诊患者

            else if (patientType == EnumPatientType.Arrive)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.R;
                //alPatientList.Add("待接诊患者|" + EnumPatientType.Arrive.ToString());
                al1 = this.radtIntegrate.QueryPatientByNurseCellAndState(oper.Nurse.ID, Status);//按科室接珍
                //alPatientList.AddRange(al1);
                dicPatient.Add("待接诊患者|" + EnumPatientType.Arrive.ToString(), al1);
            }
            #endregion

            #region 转出患者

            else if (patientType == EnumPatientType.ShiftOut)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;
                alPatientList.Add("转出患者|" + EnumPatientType.ShiftOut.ToString());
                al1 = this.radtManager.QueryPatientShiftOutApplyByNurseCell(oper.Nurse.ID, "1");
                alPatientList.AddRange(al1);
                dicPatient.Add("转出患者|" + EnumPatientType.ShiftOut.ToString(), al1);
            }
            #endregion

            #region 转入患者

            else if (patientType == EnumPatientType.ShiftIn)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.I;
                alPatientList.Add("转入患者|" + EnumPatientType.ShiftIn.ToString());
                al1 = this.radtManager.QueryPatientShiftInApplyByNurseCell(oper.Nurse.ID, "1");				//按科室查转入申请的
                alPatientList.AddRange(al1);
                dicPatient.Add("转入患者|" + EnumPatientType.ShiftIn.ToString(), al1);

            }
            #endregion

            #region 出院登记患者

            else if (patientType == EnumPatientType.Out)
            {
                Status = Neusoft.HISFC.Models.Base.EnumInState.B;
                alPatientList.Add("出院登记患者|" + EnumPatientType.Out.ToString());
                //根据出院召回的有效天数查询出院登记患者信息
                al1 = this.radtManager.PatientQueryByNurseCellVaildDate(oper.Nurse.ID, Status, callBackVaildDays);
                alPatientList.AddRange(al1);
                dicPatient.Add("出院登记患者|" + EnumPatientType.Out.ToString(), al1);
            }
            #endregion

            return 1;
        }
    }

    /// <summary>
    /// 患者类别
    /// </summary>
    public enum EnumPatientType
    {
        /// <summary>
        /// 本区在院患者
        /// </summary>
        In = 0,

        /// <summary>
        /// 待接诊患者
        /// </summary>
        Arrive = 1,

        /// <summary>
        /// 出院登记患者
        /// </summary>
        Out = 2,

        /// <summary>
        /// 转入患者
        /// </summary>
        ShiftIn = 3,

        /// <summary>
        /// 转出患者
        /// </summary>
        ShiftOut = 4,

        /// <summary>
        /// 科室列表
        /// </summary>
        DeptOrTend = 5
    }
}
