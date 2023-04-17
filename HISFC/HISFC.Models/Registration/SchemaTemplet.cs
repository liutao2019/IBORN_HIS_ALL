using System;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// <br>RegLevel</br>
    /// <br>[功能描述: 排班模板实体]</br>
    /// <br>[创 建 者: 黄小卫]</br>
    /// <br>[创建时间: 2007-2-1]</br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class SchemaTemplet : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        public SchemaTemplet()
        {                
        }

        #region 变量
        
        /// <summary>
        /// 挂号级别
        /// </summary>
        private RegLevel regLevel;
        /// <summary>
        /// 星期
        /// </summary>
        private DayOfWeek week = DayOfWeek.Monday;
        /// <summary>
        /// 排班类型
        /// </summary>
        private FS.HISFC.Models.Base.EnumSchemaType schemaType = FS.HISFC.Models.Base.EnumSchemaType.Dept;
        /// <summary>
        /// 出诊科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept;
        /// <summary>
        /// 出诊医生,模板类型为科室时,默认doct为'None'
        /// </summary>
        private FS.FrameWork.Models.NeuObject doct;
        /// <summary>
        /// 出诊医生类型
        /// </summary>
        private FS.FrameWork.Models.NeuObject doctType;
        /// <summary>
        /// 出诊午别
        /// </summary>
        private FS.FrameWork.Models.NeuObject noon;
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime begin = DateTime.MinValue;
        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime end = DateTime.MinValue;
        /// <summary>
        /// 来人挂号限额
        /// </summary>
        private decimal regQuota = 0m;
        /// <summary>
        /// 电话挂号限额
        /// </summary>
        private decimal telQuota = 0m;
        /// <summary>
        /// 特诊挂号限额
        /// </summary>
        private decimal speQuota = 0m;
        /// <summary>
        /// 是否有效
        /// </summary>
        private bool isValid = false;
        /// <summary>
        /// 是否加号
        /// </summary>
        private bool isAppend = false;        
        /// <summary>
        /// 停诊原因
        /// </summary>
        private FS.FrameWork.Models.NeuObject stopReason;
        /// <summary>
        /// 操作环境
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper;
        /// <summary>
        /// 停止人
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment stop;

        /// <summary>
        /// 诊室
        /// </summary>
        private FS.HISFC.Models.Nurse.Room room;

        /// <summary>
        /// 诊室
        /// </summary>
        public FS.HISFC.Models.Nurse.Room Room
        {
            get
            {
                if (this.room == null)
                {
                    this.room = new FS.HISFC.Models.Nurse.Room();
                }
                return room;
            }
            set { room = value; }
        }

        /// <summary>
        /// 诊台
        /// </summary>
        private FS.HISFC.Models.Nurse.Seat console;

        /// <summary>
        /// 诊台
        /// </summary>
        public FS.HISFC.Models.Nurse.Seat Console
        {
            get
            {
                if (console == null)
                {
                    console = new FS.HISFC.Models.Nurse.Seat();
                }
                return console;
            }
            set { console = value; }
        }


        #endregion
        /// <summary>
        /// 排期类型
        /// </summary>
        public string ScheduleType { set; get; }

        #region 属性

        /// <summary>
        /// 挂号级别
        /// </summary>
        public RegLevel RegLevel
        {
            get {
                if (this.regLevel == null)
                {
                    this.regLevel = new RegLevel();
                }
                return this.regLevel; }
            set { this.regLevel = value; }
        }

        /// <summary>
        /// 星期
        /// </summary>
        public DayOfWeek Week
        {
            get { return week; }
            set { week = value; }
        }

        /// <summary>
        /// 排班类型
        /// </summary>
        public FS.HISFC.Models.Base.EnumSchemaType EnumSchemaType
        {
            get { return schemaType; }
            set { schemaType = value; }
        }

        /// <summary>
        /// 出诊科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get {
                if (this.dept == null)
                {
                    this.dept = new FS.FrameWork.Models.NeuObject();
                }
                return dept; }
            set { dept = value; }
        }

        /// <summary>
        /// 出诊医师
        /// </summary>
        public FS.FrameWork.Models.NeuObject Doct
        {
            get {
                if (this.doct == null)
                {
                    this.doct = new FS.FrameWork.Models.NeuObject();
                }

                return doct; }
            set { doct = value; }
        }

        /// <summary>
        /// 医生类别
        /// </summary>
        public FS.FrameWork.Models.NeuObject DoctType
        {
            get {
                if (this.doctType == null)
                {
                    this.doctType = new FS.FrameWork.Models.NeuObject();
                }
                
                return this.doctType; }
            set { this.doctType = value; }
        }

        /// <summary>
        /// 午别
        /// </summary>
        public FS.FrameWork.Models.NeuObject Noon
        {
            get {
                if (this.noon == null)
                {
                    this.noon = new FS.FrameWork.Models.NeuObject();
                }
                return noon; }
            set { noon = value; }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Begin
        {
            get { return begin; }
            set { begin = value; }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }

        /// <summary>
        /// 现场挂号限额
        /// </summary>
        public decimal RegQuota
        {
            get { return regQuota; }
            set { regQuota = value; }
        }

        /// <summary>
        /// 电话挂号限额
        /// </summary>
        public decimal TelQuota
        {
            get { return telQuota; }
            set { telQuota = value; }
        }

        /// <summary>
        /// 特诊挂号限额
        /// </summary>
        public decimal SpeQuota
        {
            get { return speQuota; }
            set { speQuota = value; }
        }

        /// <summary>
        /// 是否加号
        /// </summary>
        public bool IsAppend
        {
            get { return isAppend; }
            set { isAppend = value; }
        }

        /// <summary>
        /// 停诊原因
        /// </summary>
        public FS.FrameWork.Models.NeuObject StopReason
        {
            get {
                if (this.stopReason == null)
                {
                    this.stopReason = new FS.FrameWork.Models.NeuObject();
                }
                return this.stopReason; }
            set { this.stopReason = value; }
        }

        /// <summary>
        /// 操作环境变量
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get {
                if (this.oper == null)
                {
                    this.oper = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return oper; }
            set { oper = value; }
        }

        /// <summary>
        /// 停止人
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Stop
        {
            get {
                if (this.stop == null)
                {
                    this.stop = new FS.HISFC.Models.Base.OperEnvironment();
                }
                return this.stop; }
            set { this.stop = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new SchemaTemplet Clone()
        {
            SchemaTemplet obj = base.Clone() as SchemaTemplet;

            obj.regLevel = this.RegLevel.Clone();
            obj.Dept = this.Dept.Clone();
            obj.Doct = this.Doct.Clone();
            obj.DoctType = this.DoctType.Clone();
            obj.Noon = this.Noon.Clone();
            obj.StopReason = this.StopReason.Clone();
            obj.Oper = this.Oper.Clone();
            obj.Stop = this.Stop.Clone();

            obj.Room = this.Room.Clone();
            obj.Console = Console.Clone();
                        
            return obj;
        }
        #endregion

        #region IValid 成员

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion
    }    
}
