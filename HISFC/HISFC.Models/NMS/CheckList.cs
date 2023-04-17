using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [���������������ʱ����˼�¼��ϸ��ʵ��]
    /// [�� �� �ߣ�����]
    /// [����ʱ�䣺2008-10-7]
    /// [ID:�ʱ���ϸ����ˮ��]
    /// </summary>
    [System.Serializable]
    public class CheckList:Neusoft.FrameWork.Models.NeuObject
    {
        #region ���캯��

        /// <summary>
        /// ���캯��[ID:�ʱ���ϸ����ˮ��]
        /// </summary>
        public CheckList()
        {
        }

        #endregion

        #region ����

        /// <summary>
        /// �ʱ�������ˮ��
        /// </summary>
        private string checkMainId;

        /// <summary>
        /// ģ����ϸ(ģ����ϸ��ˮ�ţ�ģ��������ˮ�š�������Ŀ������Ϣ�������ţ�������ʾ��ǣ�������Ŀ��Ϣ����Ŀ���)
        /// </summary>
        private ModelList modelListInfo = new ModelList();

        /// <summary>
        /// �������(��Ŀ����Ϊ���)
        /// </summary>
        private string itemPickNum;

        /// <summary>
        /// ��鲻�ϸ�����(��Ŀ����Ϊ���)
        /// </summary>
        private string itemRejectNum;

        #endregion

        #region ����

        /// <summary>
        /// �ʱ�����(�ʱ�������ˮ��)
        /// </summary>
        public string CheckMainId
        {
            get
            {
                return checkMainId;
            }
            set
            {
                checkMainId = value;
            }
        }

        /// <summary>
        /// ģ����ϸ(ģ����ϸ��ˮ�ţ�ģ��������ˮ�š�������Ŀ������Ϣ�������ţ�������ʾ��ǣ�������Ŀ��Ϣ����Ŀ���)
        /// </summary>
        public ModelList ModelListInfo
        {
            get
            {
                return modelListInfo;
            }
            set
            {
                modelListInfo = value;
            }
        }

        /// <summary>
        /// �������(��Ŀ����Ϊ���)
        /// </summary>
        public string ItemPickNum
        {
            get
            {
                return itemPickNum;
            }
            set
            {
                itemPickNum = value;
            }
        }

        /// <summary>
        /// ��鲻�ϸ�����(��Ŀ����Ϊ���)
        /// </summary>
        public string ItemRejectNum
        {
            get
            {
                return itemRejectNum;
            }
            set
            {
                itemRejectNum = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new CheckList Clone()
        {
            CheckList checklist = base.Clone() as CheckList;
            checklist.ModelListInfo= this.modelListInfo.Clone();

            return checklist;
        }

        #endregion


    }
}
