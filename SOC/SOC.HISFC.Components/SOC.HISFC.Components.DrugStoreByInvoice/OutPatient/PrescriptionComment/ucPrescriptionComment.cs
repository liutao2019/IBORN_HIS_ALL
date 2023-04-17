using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.PrescriptionComment
{
    /// <summary>
    /// [功能描述: 门诊处方点评]<br></br>
    /// [创 建 者: cao-lin]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucPrescriptionComment : Base.ucDrugBaseComponent
    {

        #region 属性
        /// <summary>
        /// 是否按发票号显示列表
        /// </summary>
        private bool isShowByInvoice = true;

        /// <summary>
        ///是否按发票号显示列表
        /// </summary>
        [Description("是否按发票号显示列表"), Category("设置"), Browsable(true)]
        public bool IsShowByInvoice
        {
            get { return isShowByInvoice; }
            set { isShowByInvoice = value; }
        }
        #endregion

        public ucPrescriptionComment()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            return this.Query();
        }

        /// <summary>
        /// 查询处方列表
        /// </summary>
        /// <returns></returns>
        private int Query()
        {

            this.ucPrescriptionCommentTree1.Clear();
            this.ucPatientInfo1.Clear();
            this.ucRecipeDetail1.Clear();

            DateTime beginTime = this.ucPrescriptionCommentTree1.BeginTime;
            DateTime endTime = this.ucPrescriptionCommentTree1.EndTime;
            //string billNo = this.ucPrescriptionCommentTree1

            //1、未打印不加载，属于配药台
            Function function = new Function();
            //ArrayList al2 = function.QueryDrugRecipe();
            //if (al2 != null)
            //{
            //    this.ucPrescriptionCommentTree1.ShowRecipeList(al2, true, "0", isShowByInvoice);
            //}

            return 1;
        }
    }
}
