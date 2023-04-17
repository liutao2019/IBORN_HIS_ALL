using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.FrameWork.Models;

namespace Neusoft.HISFC.Models.Science
{
    /// <summary>
    /// Neusoft.HISFC.Models.Science.SubjectInfo<br></br>
    /// [��������:������Ϣʵ�� ]<br></br>
    /// [�� �� ��: ţ��Ԫ]<br></br>
    /// [����ʱ��: 2008-05-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class SubjectInfo : Neusoft.FrameWork.Models.NeuObject, Neusoft.HISFC.Models.Base.IValid
    {
        #region ����
        /// <summary>
        /// ��Ч��׼λ
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ������
        /// </summary>
        private string subjectID = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        private string subjectName = string.Empty;

        /// <summary>
        /// ��׼�ĺ�
        /// </summary>
        private string approveId = string.Empty;

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private string projectID = string.Empty;

        /// <summary>
        /// �������
        /// </summary>
        private NeuObject subjectType = new NeuObject ();

        /// <summary>
        /// �γ�����
        /// </summary>
        private NeuObject subjectProperty = new NeuObject();

        /// <summary>
        /// ����ѧ��
        /// </summary>
        private NeuObject subjectBelongKnowledge = new NeuObject ();

        /// <summary>
        /// ������Դ
        /// </summary>
        private NeuObject subjectSource = new NeuObject ();

        /// <summary>
        /// ����ȼ�
        /// </summary>
        private NeuObject subjectGrade = new NeuObject ();

        /// <summary>
        /// �μӵ�λ
        /// </summary>
        private string attendUnit = string.Empty;

        /// <summary>
        /// ������
        /// </summary>
        private NeuObject principal = new NeuObject ();

        /// <summary>
        /// ����
        /// </summary>
        private NeuObject dept = new NeuObject ();

        /// <summary>
        /// ר��
        /// </summary>
        private string specialtyDept = string.Empty;

        /// <summary>
        /// ��Ŀ��ʼʱ��
        /// </summary>
        private DateTime beginDate;

        /// <summary>
        /// ��Ŀ����ʱ��
        /// </summary>
        private DateTime endDate;

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private decimal subjectCost;

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        private Base.OperEnvironment operInfo = new Neusoft.HISFC.Models.Base.OperEnvironment ();

        /// <summary>
        /// ��׼ʱ��
        /// </summary>
        private DateTime approveDate;

        /// <summary>
        /// ��ʿվ��Ϣ
        /// </summary>
        private NeuObject nurseCell = new NeuObject ();

        #endregion

        #region ����

        #region IValid ��Ա
        /// <summary>
        /// ��Ч��ʶ
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        /// <summary>
        /// ������
        /// </summary>
        public string SubjectID
        {
            set
            {
                this.subjectID = value;
            }
            get
            {
                return this.subjectID;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string SubjectName
        {
            set
            {
                this.subjectName = value;
            }
            get
            {
                return this.subjectName;
            }
        }

        /// <summary>
        /// ��׼�ĺ�
        /// </summary>
        public string ApproveID
        {
            set
            {
                this.approveId = value;
            }
            get
            {
                return this.approveId;
            }
        }

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        public string ProjectID
        {
            set
            {
                this.projectID = value;
            }
            get
            {
                return this.projectID;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public NeuObject SubjectType
        {
            set
            {
                this.subjectType = value;
            }
            get
            {
                return this.subjectType;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public NeuObject SubjectProperty
        {
            set
            {
                this.subjectProperty = value;
            }
            get
            {
                return this.subjectProperty;
            }
        }

        /// <summary>
        /// ����ѧ��
        /// </summary>
        public NeuObject SubjectBelongKnowledge
        {
            set
            {
                this.subjectBelongKnowledge = value;
            }
            get
            {
                return this.subjectBelongKnowledge;
            }
        }

        /// <summary>
        /// ������Դ
        /// </summary>
        public NeuObject SubjectSource
        {
            set
            {
                this.subjectSource = value;
            }
            get
            {
                return this.subjectSource;
            }
        }

        /// <summary>
        /// ����ȼ�
        /// </summary>
        public NeuObject SubjectGrade
        {
            set
            {
                this.subjectGrade = value;
            }
            get
            {
                return this.subjectGrade;
            }
        }

        /// <summary>
        /// �μӵ�λ
        /// </summary>
        public string AttendUnit
        {
            set
            {
                this.attendUnit = value;
            }
            get
            {
                return this.attendUnit;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public NeuObject Principal
        {
            set
            {
                this.principal = value;
            }
            get
            {
                return this.principal;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public NeuObject Dept
        {
            set
            {
                this.dept = value;
            }
            get
            {
                return this.dept;
            }
        }

        /// <summary>
        /// ר��(ĿǰΪ������)
        /// </summary>
        public string SpecialtyDept
        {
            set
            {
                this.specialtyDept = value;
            }
            get
            {
                return this.specialtyDept;
            }
        }

        /// <summary>
        /// ��Ŀ��ʼʱ��
        /// </summary>
        public DateTime BeginDate
        {
            set
            {
                this.beginDate = value;
            }
            get
            {
                return this.beginDate;
            }
        }

        /// <summary>
        /// ��Ŀ����ʱ��
        /// </summary>
        public DateTime EndDate
        {
            set
            {
                this.endDate = value;
            }
            get
            {
                return this.endDate;
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public decimal SubjectCost
        {
            get
            {
                return this.subjectCost;
            }
            set
            {
                this.subjectCost = value;
            }
        }

        /// <summary>
        /// ��׼ʱ��
        /// </summary>
        public DateTime ApproveDate
        {
            get
            {
                return this.approveDate;
            }
            set
            {
                this.approveDate = value;
            }
        }

        /// <summary>
        /// ����Ա��Ϣ
        /// </summary>
        public Base.OperEnvironment OperInfo
        {
            get
            {
                return this.operInfo;
            }
            set
            {
                this.operInfo = value;
            }
        }

        /// <summary>
        /// ��ʿվ
        /// </summary>
        public NeuObject NurseCell
        {
            get
            {
                return this.nurseCell;
            }
            set
            {
                this.nurseCell = value;
            }
        }
        #endregion

        #region ����
        public new SubjectInfo Clone()
        {
            SubjectInfo subjectInfo = base.Clone() as SubjectInfo;
            //�������
            subjectInfo.SubjectType = this.SubjectType.Clone();

            //��������
            subjectInfo.SubjectProperty = this.SubjectProperty.Clone();

            //��������ѧ��
            subjectInfo.SubjectBelongKnowledge = this.SubjectBelongKnowledge.Clone();

            //������Դ
            subjectInfo.SubjectSource = this.SubjectSource.Clone();

            //����ȼ�
            subjectInfo.SubjectGrade = this.SubjectGrade.Clone();

            //������
            subjectInfo.Principal = this.Principal.Clone();

            //����
            subjectInfo.Dept = this.Dept.Clone();

            //����Ա��Ϣ
            subjectInfo.OperInfo = this.OperInfo.Clone();

            //��ʿվ��Ϣ
            subjectInfo.NurseCell = this.NurseCell.Clone();

            return subjectInfo;
        }
        #endregion
    }
}
