using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface
{
    #region �鷽��ʵ��

    ///// <summary>
    ///// ������ӡ�ӿڣ��鷽��
    ///// </summary>
    //public abstract class IRecipePrint : IRecipePrintBase
    //{
    //    #region IRecipePrintBase ��Ա

    //    public virtual void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
    //    {
    //        return;
    //    }

    //    public virtual string RecipeNO
    //    {
    //        get
    //        {
    //            return null;
    //        }
    //        set
    //        {
    //        }
    //    }

    //    public void ppp()
    //    {
    //    }

    //    public virtual void PrintRecipe()
    //    {
    //        return;
    //    }

    //    public virtual void PrintRecipeView()
    //    {
    //        return;
    //    }

    //    public virtual string Printer
    //    {
    //        get
    //        {
    //            return null;
    //        }
    //        set
    //        {
    //        }
    //    }

    //    #endregion
    //}

    #endregion

    #region �ӿڶ���

    /// <summary>
    /// ���ﴦ����ӡ�ӿ�
    /// </summary>
    public interface IRecipePrint
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="register"></param>
        int SetPatientInfo(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// ������
        /// </summary>
        string RecipeNO
        {
            get;
            set;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        int PrintRecipe();

        /// <summary>
        /// ��ӡԤ����
        /// </summary>
        int PrintRecipeView(System.Collections.ArrayList alRecipe); 

        /// <summary>
        /// ���ô�ӡ��
        /// </summary>
        string Printer
        {
            get;
            set;
        }
    }

    #endregion
}
