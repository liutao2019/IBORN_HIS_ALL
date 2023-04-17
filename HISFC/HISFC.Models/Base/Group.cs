using FS.FrameWork.Models;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// Group<br></br>
    /// [��������: ������]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2006-08-28]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class Group : Spell
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public Group()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// ʹ�÷�Χ 1����/2סԺ
        /// </summary>
        private ServiceTypes userType;

        /// <summary>
        /// �������
        /// </summary>
        private GroupKinds kind;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private NeuObject dept;

        /// <summary>
        /// ҽ����Ϣ
        /// </summary>
        private NeuObject doctor;

        /// <summary>
        /// �Ƿ���1�ǣ�0��
        /// </summary>
        private bool isShared;

        /// <summary>
        /// �Ƿ��Ѿ��ͷ���Դ Ĭ��Ϊfalseû���ͷ�
        /// </summary>
        private bool alreadyDisposed = false;


        //{C2922531-DEE7-43a0-AB7A-CDD7C58691BD} �༶���� yangw 20100916
        /// <summary>
        /// ���ڵ�ID
        /// </summary>
        private string parentID;
        #endregion

        #region ����

        /// <summary>
        /// ʹ�÷�Χ 1����/2סԺ
        /// </summary>
        public ServiceTypes UserType
        {
            get
            {
                if (userType == null)
                {
                    userType = new ServiceTypes();
                }
                return this.userType;
            }
            set
            {
                this.userType = value;
            }
        }

        /// <summary>
        /// ��������,1.ҽʦ���ף�2.��������
        /// </summary>
        public GroupKinds Kind
        {
            get
            {
                if (kind == null)
                {
                    kind = new GroupKinds();
                }
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public NeuObject Dept
        {
            get
            {
                if (dept == null)
                {
                    dept = new NeuObject();
                }
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ҽ����Ϣ
        /// </summary>
        public NeuObject Doctor
        {
            get
            {
                if (doctor == null)
                {
                    doctor = new NeuObject();
                }
                return this.doctor;
            }
            set
            {
                this.doctor = value;
            }
        }

        /// <summary>
        /// �Ƿ���1�ǣ�0��
        /// </summary>
        public bool IsShared
        {
            get
            {
                return this.isShared;
            }
            set
            {
                this.isShared = value;
            }
        }

        //{C2922531-DEE7-43a0-AB7A-CDD7C58691BD} �༶���� yangw 20100916
        /// <summary>
        /// ���ڵ�ID
        /// </summary>
        public string ParentID
        {
            get
            {
                if (parentID == null)
                {
                    parentID = string.Empty;
                }
                return parentID;
            }
            set
            {
                parentID = value;
            }
        }

        #endregion

        #region ����

        #region �ͷ���Դ
        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        /// <param name="isDisposing">�Ƿ��ͷ� true�� false��</param>
        protected override void Dispose(bool isDisposing)
        {
            if (alreadyDisposed)
            {
                return;
            }

            if (this.dept != null)
            {
                this.dept.Dispose();
                this.dept = null;
            }
            if (this.doctor != null)
            {
                this.doctor.Dispose();
                this.doctor = null;
            }

            base.Dispose(isDisposing);

            alreadyDisposed = true;
        }
        #endregion

        #region ��¡
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ���󸱱�</returns>
        public new Group Clone()
        {
            Group group = base.Clone() as Group;

            group.Dept = this.Dept.Clone();
            group.Doctor = this.Doctor.Clone();

            return group;
        }
        #endregion

        #endregion
    }

    #region ö��

    /// <summary>
    /// ��������
    /// </summary>
    public enum GroupKinds
    {
        /// <summary>
        /// ҽ��
        /// </summary>
        Doctor = 1,
        /// <summary>
        /// ����
        /// </summary>
        Dept = 2,
        /// <summary>
        /// ȫԺ
        /// </summary>
        All = 3,
        /// <summary>
        /// ����
        /// </summary>
        Fee = 4
    }

    #endregion
}