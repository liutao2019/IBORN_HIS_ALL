using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// ���ﴦ����ӡ�ӿ�
    /// </summary>
    public interface IRecipePrint
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="register"></param>
        void SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register);

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
        void PrintRecipe();
    }

    /// <summary>
    /// ����ҽ���кŽӿ�
    /// </summary>
    public interface IDiagInDisplay
    {
        /// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        Neusoft.HISFC.Models.Registration.Register RegInfo
        {
            get;
            set;
        }

        /// <summary>
        /// ҽ������
        /// </summary>
        Neusoft.FrameWork.Models.NeuObject ObjRoom
        {
            get;
            set;
        }

        /// <summary>
        /// ҽ���к�
        /// </summary>
        void DiagInDisplay();
    }

}
