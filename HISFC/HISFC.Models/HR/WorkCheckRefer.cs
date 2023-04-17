using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    /// <br>Neusoft.HISFC.Models.HR.WorkCheckRefer</br>
    /// <br>[��������: ����Ա����ʵ�壬ָ������Ա���Զ���Щ���ҽ��п���ά��]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2008-08-04]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class WorkCheckRefer : Neusoft.FrameWork.Models.NeuObject
    {

        #region �ֶ�

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject workChecker = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ڿ���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ա
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment oper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject WorkChecker
        {
            get
            {
                return workChecker;
            }
            set
            {
                workChecker = value;
            }
        }

        /// <summary>
        /// ���ڿ���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                return oper;
            }
            set
            {
                oper = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new WorkCheckRefer Clone()
        {
            WorkCheckRefer wcRefer = base.Clone() as WorkCheckRefer;

            wcRefer.WorkChecker = this.WorkChecker.Clone();
            wcRefer.Dept = this.Dept.Clone();
            wcRefer.Oper = this.Oper.Clone();

            return wcRefer;
        }

        #endregion
    }
}
