using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.RecipePrint
{
    /// <summary>
    /// 处方打印
    /// </summary>
    public partial class ucRecipeBase : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.HISFC.BizProcess.Interface.IRecipePrint
    {
        public ucRecipeBase()
        {
            InitializeComponent();
        }

        Controls.ucTitleBase ucTitleBase;
        Controls.ucOperInfoBase ucOperInfoBase;
        Controls.ucOrderInfoBase ucOrderInfoBase;
        Controls.ucPatientInfoBase ucPatientInfoBase;

        #region IRecipePrint 成员

        public int PrintRecipe()
        {
            return 1;
        }

        public int PrintRecipeView(System.Collections.ArrayList alRecipe)
        {
            return 1;
        }

        string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        string recipeNo = "";

        public string RecipeNO
        {
            get
            {
                return recipeNo;
            }
            set
            {
                recipeNo = value;
            }
        }

        public int SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)
        {
            return 1;
        }

        #endregion
    }
}
