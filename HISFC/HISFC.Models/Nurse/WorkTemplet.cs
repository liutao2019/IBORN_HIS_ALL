using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Nurse
{
    /// <summary>
    /// <br>RegLevel</br>
    /// <br>[��������: ��ʿ�Ű�ʵ��]</br>
    /// <br>[�� �� ��: ����]</br>
    /// <br>[����ʱ��: 2007-9-9]</br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class WorkTemplet : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid
    {
        public WorkTemplet()
        {

        }
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        private DayOfWeek week = DayOfWeek.Monday;

        public DayOfWeek Week
        {
            get { return week; }
            set { week = value; }
        }
        /// <summary>
        /// �Ű໤ʿվ������ʱ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject nurseCell = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject NurseCell
        {
            get { return nurseCell; }
            set { nurseCell = value; }
        }
        /// <summary>
        /// �Ű����
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject Dept
        {
            get { return dept; }
            set { dept = value; }
        }
        /// <summary>
        /// �Ű���Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject employee = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject Employee
        {
            get { return employee; }
            set { employee = value; }
        }
        /// <summary>
        /// ��Ա����
        /// </summary>
        private FS.FrameWork.Models.NeuObject emplType = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject EmplType
        {
            get { return emplType; }
            set { emplType = value; }
        }
        /// <summary>
        /// �Ű����
        /// </summary>
        private FS.FrameWork.Models.NeuObject noon = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject Noon
        {
            get { return noon; }
            set { noon = value; }
        }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime begin = DateTime.MinValue;

        public DateTime Begin
        {
            get { return begin; }
            set { begin = value; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private DateTime end = DateTime.MinValue;

        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid = false;
        /// <summary>
        /// ԭ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject reason = new FS.FrameWork.Models.NeuObject();

        public FS.FrameWork.Models.NeuObject Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new WorkTemplet Clone()
        {
            WorkTemplet obj = base.Clone() as WorkTemplet;

            obj.NurseCell = this.NurseCell.Clone();
            obj.Dept = this.Dept.Clone();
            obj.Employee = this.Employee.Clone();
            obj.EmplType = this.EmplType.Clone();
            obj.Noon = this.noon.Clone();
            obj.Oper = this.oper.Clone();
            obj.Reason = this.reason.Clone();

            return obj;
        }
        #endregion

        #region IValid ��Ա

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
    }
}
