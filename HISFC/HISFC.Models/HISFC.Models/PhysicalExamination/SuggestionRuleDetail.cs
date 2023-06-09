using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.PhysicalExamination
{
    /// <summary>
    /// SuggestionRuleDetail<br></br>
    /// [功能描述: 体检建议规则明细]<br></br>
    /// [创 建 者: 张俊义]<br></br>
    /// [创建时间: 2007-06-10]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class SuggestionRuleDetail :FS.FrameWork.Models.NeuObject
    {
        #region 私有变量  
        // ID 规则明细编码
        //Name 规则明细名称
        private SuggestionRule rule = new SuggestionRule(); //主规则 
        private FS.FrameWork.Models.NeuObject itemType = new FS.FrameWork.Models.NeuObject();//规则类型
        private FS.FrameWork.Models.NeuObject firstOperation = new FS.FrameWork.Models.NeuObject();//操作符号1
        private FS.FrameWork.Models.NeuObject firstDetailValue = new FS.FrameWork.Models.NeuObject();//值1
        private FS.FrameWork.Models.NeuObject secondOperation = new FS.FrameWork.Models.NeuObject();//操作符号2
        private FS.FrameWork.Models.NeuObject secondDetailValue = new FS.FrameWork.Models.NeuObject();//值2
        //操作员信息
        private FS.HISFC.Models.Base.OperEnvironment operInfo = new FS.HISFC.Models.Base.OperEnvironment();
        #endregion 

        #region  共有属性 
        /// <summary>
        /// 规则 
        /// </summary>
        public SuggestionRule Rule
        {
            get
            {
                return rule;
            }
            set
            {
                rule = value;
            }
        } 
        /// <summary>
        /// 操作符号
        /// </summary>
        public FS.FrameWork.Models.NeuObject FirstOperation
        {
            get
            {
                return firstOperation;
            }
            set
            {
                firstOperation = value;
            }
        }
        /// <summary>
        /// 值1
        /// </summary>
        public FS.FrameWork.Models.NeuObject FirstDetailValue
        {
            get
            {
                return firstDetailValue;
            }
            set
            {
                firstDetailValue = value;
            }
        }
        /// <summary>
        ///规则类型 
        /// </summary>
        public FS.FrameWork.Models.NeuObject ItemType
        {
            get
            {
                return itemType;
            }
            set
            {
                itemType = value;
            }
        }
        /// <summary>
        /// 操作符号
        /// </summary>
        public FS.FrameWork.Models.NeuObject SecondOperation
        {
            get
            {
                return secondOperation;
            }
            set
            {
                secondOperation = value;
            }
        }
        /// <summary>
        /// 值
        /// </summary>
        public FS.FrameWork.Models.NeuObject SecondDetailValue
        {
            get
            {
                return secondDetailValue;
            }
            set
            {
                secondDetailValue = value;
            }
        }
        #endregion 

        #region 克隆函数
        #endregion 
    }
}
