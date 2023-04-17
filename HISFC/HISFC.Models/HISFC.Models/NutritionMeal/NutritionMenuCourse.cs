using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.NutritionMeal
{
    [System.Serializable]
    public class NutritionMenuCourse : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        /// <summary>
        /// Terminal<br></br>
        /// [功能描述: 菜谱包含菜式维护]<br></br>
        /// [创 建 者: 王彦]<br></br>
        /// [创建时间: 2007-8-20]<br></br>
        /// <修改记录
        ///		修改人=''
        ///		修改时间=''
        ///		修改目的=''
        ///		修改描述=''
        ///  />
        /// </summary>
        public NutritionMenuCourse()
        {
        }

        #region 变量

        /// <summary>
        /// 菜式
        /// </summary>
        private FS.HISFC.Models.Board.Item item = new FS.HISFC.Models.Board.Item();

        /// <summary>
        /// 菜谱
        /// </summary>
        private FS.HISFC.Models.NutritionMeal.NutritionFoodMenu menu = new NutritionFoodMenu();

        /// <summary>
        /// 是否有效
        /// </summary>
        private bool isValid;

        /// <summary>
        /// 创建环境
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment createEnv = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// 使无效操作员
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment inValidOper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region 属性


        /// <summary>
        /// 使无效环境

        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment InvalidOper
        {
            get
            {
                return this.inValidOper;
            }
            set
            {
                this.inValidOper = value;
            }
        }

        /// <summary>
        /// 创建环境
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CreateEnv
        {
            get
            {
                return this.createEnv;
            }
            set
            {
                this.createEnv = value;
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        /// <summary>
        /// 菜式
        /// </summary>
        public FS.HISFC.Models.Board.Item Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// 菜谱
        /// </summary>
        public FS.HISFC.Models.NutritionMeal.NutritionFoodMenu Menu
        {
            get
            {
                return this.menu;
            }
            set
            {
                this.menu = value;
            }
        }

        #endregion

        #region 克隆

        public new NutritionMenuCourse Clone()
        {
            NutritionMenuCourse nutritionMenuCourse = base.Clone() as NutritionMenuCourse;

            nutritionMenuCourse.createEnv = this.createEnv.Clone();

            nutritionMenuCourse.item = this.item.Clone();

            nutritionMenuCourse.menu = this.menu.Clone();

            nutritionMenuCourse.inValidOper = this.inValidOper.Clone();
           
            return nutritionMenuCourse;
        }

        #endregion
    }
}
