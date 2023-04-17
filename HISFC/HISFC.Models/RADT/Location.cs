using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.RADT
{
    /// <summary>
    /// [��������: ����λ��ʵ��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2006-09-05]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='����ΰ'
    ///		�޸�ʱ��='2006-9-12'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary> 
    [System.Serializable]
    public class Location : NeuObject
    {
        /// <summary>
        /// ����λ����
        /// </summary>
        public Location()
        {
        }

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        private NeuObject dept;

        /// <summary>
        /// ����
        /// </summary>
        private NeuObject nurseCell;

        /// <summary>
        /// ¥
        /// </summary>
        private string building;

        /// <summary>
        /// ��
        /// </summary>
        private string floor;

        /// <summary>
        /// ��
        /// </summary>
        private string room;

        /// <summary>
        /// ����
        /// </summary>
        private Bed bed;

        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public NeuObject Dept
        {
            get
            {
                if (dept == null)
                {
                    dept = new NeuObject();
                }
                return this.dept; ;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public NeuObject NurseCell
        {
            get
            {
                if (nurseCell == null)
                {
                    nurseCell = new NeuObject();
                }
                return this.nurseCell;
            }
            set
            {
                this.nurseCell = value;
            }
        }

        /// <summary>
        /// ¥
        /// </summary>
        public string Building
        {
            get
            {
                return this.building;
            }
            set
            {
                this.building = value;
            }
        }

        /// <summary>
        /// ��
        /// </summary>
        public string Floor
        {
            get
            {
                return this.floor;
            }
            set
            {
                this.floor = value;
            }
        }

        /// <summary>
        /// ��
        /// </summary>
        public string Room
        {
            get
            {
                return this.room;
            }
            set
            {
                this.room = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public Bed Bed
        {
            get
            {
                if (bed == null)
                {
                    bed = new Bed();
                }
                return this.bed;
            }
            set
            {
                this.bed = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Location Clone()
        {
            Location location = base.MemberwiseClone() as Location;
            location.Bed = this.Bed.Clone();
            location.Dept = this.Dept.Clone();
            location.NurseCell = this.NurseCell.Clone();
            return location;
        }
        #endregion
    }
}
