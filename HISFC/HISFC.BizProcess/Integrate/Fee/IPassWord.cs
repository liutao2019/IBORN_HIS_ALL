using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.InterFacePassWord
{
    public interface IPassWord
    {
        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        ///// <summary>
        ///// ��֤����
        ///// </summary>
        ///// <returns></returns>
        //bool ValidPassWord
        //{
        //    get;
        //}
        ///// <summary>
        ///// ���￨��
        ///// </summary>
        //string CardNO
        //{
        //    get;
        //    set;
        //}
        ///// <summary>
        ///// �Ƿ���֤����
        ///// </summary>
        //bool IsOK
        //{
        //    get;
        //}
        /// <summary>
        /// ��֤����
        /// </summary>
        /// <returns></returns>
        bool ValidPassWord
        {
            get;
        }
        /// <summary>
        /// ���￨��
        /// </summary>
        Neusoft.HISFC.Models.RADT.Patient Patient
        {
            get;
            set;
        }
        /// <summary>
        /// �Ƿ���֤����
        /// </summary>
        bool IsOK
        {
            get;
        }
    }
}
