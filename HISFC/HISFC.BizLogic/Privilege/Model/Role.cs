using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// ��ɫʵ��
    /// </summary>
    [Serializable]
    public class Role:NeuObject
    {
       
        private string _parentId;
        private string _appId;
        private string _unitId;
        private string _description;
        private string _userId;
        private DateTime _operDate;

        #region Role ��Ա      

        /// <summary>
        /// ������ɫId
        /// </summary>        
        public string ParentId
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
            }
        }

        /// <summary>
        /// Ӧ��Id
        /// </summary>        
        public string AppId
        {
            get
            {
                return _appId;
            }
            set
            {
                _appId = value;
            }
        }

        /// <summary>
        /// ��֯��ԪId
        /// </summary>        
        public string UnitId
        {
            get
            {
                return _unitId;
            }
            set
            {
                _unitId = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>        
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        #endregion

        /// <summary>
        /// ���Ƶ�¼�˽�ɫ�Ŀ������
        /// </summary>
        private string limitDeptType;

        /// <summary>
        /// ���Ƶ�¼�˽�ɫ�Ŀ������
        /// </summary>
        public string LimitDeptType
        {
            get
            {
                return limitDeptType;
            }
            set
            {
                limitDeptType = value;
            }
        }

        /// <summary>
        /// �����û�Id
        /// </summary>        
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>        
        public DateTime OperDate
        {
            get
            {
                return _operDate;
            }
            set
            {
                _operDate = value;
            }
        }


        private FS.FrameWork.Models.NeuObject hospital = new NeuObject();

        /// <summary>
        /// 
        /// </summary>
        public FS.FrameWork.Models.NeuObject Hospital
        {
            get { return hospital; }
            set { hospital = value; }
        }


        /// <summary>
        /// ��ɫ��¡
        /// </summary>
        /// <returns></returns>
        public new Role Clone()
        {
            Role obj = base.Clone() as Role;
            obj.Hospital = this.Hospital.Clone();
            return obj;
        }
    }
}
