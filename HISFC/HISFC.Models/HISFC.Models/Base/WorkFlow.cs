using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// [��������: �򵥹�������Ϣ��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// </summary>
    [System.Serializable]
    public class WorkFlow : FS.FrameWork.Models.NeuObject
    {
        public WorkFlow()
        {
        }

        #region ����

        /// <summary>
        /// ����״̬
        /// </summary>
        private string state;

        /// <summary>
        /// ��һ״̬
        /// </summary>
        private string nextState;

        /// <summary>
        /// �Ƿ���ҪȨ�޹���
        /// </summary>
        private bool isNeedCompetence;

        /// <summary>
        /// Ȩ�޼���
        /// </summary>
        private List<string> competenceList = new List<string>();

        /// <summary>
        /// ����(����)����
        /// </summary>
        private List<FS.FrameWork.Models.NeuObject> paramList = new List<FS.FrameWork.Models.NeuObject>();

        #endregion

        #region ����

        /// <summary>
        /// ����״̬
        /// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
                this.ID = value;
            }
        }

        /// <summary>
        /// ��һ״̬
        /// </summary>
        public string NextState
        {
            get
            {
                return this.nextState;
            }
            set
            {
                this.nextState = value;
            }
        }

        /// <summary>
        /// �Ƿ���ҪȨ�޹���
        /// </summary>
        public bool IsNeedCompetence
        {
            get
            {
                return this.isNeedCompetence;
            }
            set
            {
                this.isNeedCompetence = value;
            }
        }

        /// <summary>
        /// Ȩ�޼���
        /// </summary>
        public List<string> CompetenceList
        {
            get
            {
                return this.competenceList;
            }
            set
            {
                this.competenceList = value;
            }
        }

        /// <summary>
        /// ����(����)����
        /// </summary>
        public List<FS.FrameWork.Models.NeuObject> ParamList
        {
            get
            {
                return this.paramList;
            }
            set
            {
                this.paramList = value;                
            }
        }

        #endregion
    }
}
