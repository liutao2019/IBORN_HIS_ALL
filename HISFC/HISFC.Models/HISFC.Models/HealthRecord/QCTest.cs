using System;


namespace FS.HISFC.Models.HealthRecord
{


    /// <summary>
    /// QCTest 的摘要说明。基础质控信息登记 表的实体类
    /// </summary>
    [Serializable]
    public class QCTest : FS.FrameWork.Models.NeuObject
    {
        public QCTest()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        //		PARENT_CODE  VARCHAR2(14)                  父级医疗机构编码        
        //		CURRENT_CODE VARCHAR2(14)                  本级医疗机构编码        
        //		SEQUENCE_NO  VARCHAR2(10)                  考试流水号              
        //		TEST_DATE    DATE         Y                考试日期                
        //		EMPL_CODE    VARCHAR2(6)  Y                人员编号                
        //		EMPL_NAME    VARCHAR2(10) Y                人员姓名                
        //		MARK         NUMBER(4,2)  Y                分数                    
        //		LEVL_CODE    VARCHAR2(2)  Y                医师级别代码            
        //		LEVL_NAME    VARCHAR2(50) Y                医师级别名称            
        //		VALID_STATE  VARCHAR2(1)  Y                是否作废 0 有效 1  作废 
        //		OPER_CODE    VARCHAR2(6)  Y                录入操作员              
        //		OPER_DATE    DATE         Y                录入时间 
        #region  私有变量

        //ID 考试流水号
        //考试时间
        private string testDate;
        //人员 ID编码  name 名称
        private FS.FrameWork.Models.NeuObject emplInfo = new FS.FrameWork.Models.NeuObject();
        //分数
        private float mark;
        //医师 ID 级别编码 NAME 级别名称
        private FS.FrameWork.Models.NeuObject levelInfo = new FS.FrameWork.Models.NeuObject();
        //有效性标识
        private bool validState;
        //操作员
        private FS.FrameWork.Models.NeuObject operInfo = new FS.FrameWork.Models.NeuObject();
        //录入时间
        private DateTime operDate;
        #endregion

        #region 共有属性
        /// <summary>
        /// 操作员信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject OperInfo
        {
            get
            {
                return operInfo;
            }
            set
            {
                operInfo = value;
            }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperDate
        {
            get
            {
                return operDate;
            }
            set
            {
                operDate = value;
            }
        }
        /// <summary>
        /// 有效
        /// </summary>
        public bool ValidState
        {
            get
            {
                return validState;
            }
            set
            {
                validState = value;
            }
        }
        /// <summary>
        /// 医师级别 id 级别编码 name 几倍名称
        /// </summary>
        public FS.FrameWork.Models.NeuObject LevelInfo
        {
            get
            {
                return levelInfo;
            }
            set
            {
                levelInfo = value;
            }
        }
        /// <summary>
        /// 分数
        /// </summary>
        public float Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
            }
        }
        /// <summary>
        /// 考试时间
        /// </summary>
        public string TestDate
        {
            get
            {
                return testDate;
            }
            set
            {
                testDate = value;
            }
        }
        /// <summary>
        /// 参加考试人员信息 id 人员编码 name 人员名称 
        /// </summary>
        public FS.FrameWork.Models.NeuObject EmplInfo
        {
            get
            {
                return emplInfo;
            }
            set
            {
                emplInfo = value;
            }
        }
        #endregion


        public new QCTest Clone()
        {
            QCTest qct = (QCTest)base.Clone();
            qct.emplInfo = this.emplInfo.Clone();
            qct.levelInfo = this.levelInfo.Clone();
            qct.operInfo = this.operInfo.Clone();
            return qct;

        }

    }
}
