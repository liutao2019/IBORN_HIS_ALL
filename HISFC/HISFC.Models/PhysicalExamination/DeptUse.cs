using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Object.PhysicalExamination
{
    /// <summary>
    /// DeptUse<br></br>
    /// [��������: �Ƴ��ù���]<br></br>
    /// [�� �� ��: �ſ���]<br></br>
    /// [����ʱ��: 2007-03-2]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class DeptUse : NFC.Object.NeuObject
    {
        #region ˽�б���
        /// <summary>
        /// ִ�п���
        /// </summary>
        private NFC.Object.NeuObject execDeptInfo = new Neusoft.NFC.Object.NeuObject() ;
        /// <summary>
        /// ����
        /// </summary>
        private NFC.Object.NeuObject deptInfo = new Neusoft.NFC.Object.NeuObject();
        /// <summary>
        /// ��ʶ 
        /// </summary>
        private string unitFlag;
        /// <summary>
        /// �Ƿ�ͳ��
        /// </summary>
        public string isStat;
        /// <summary>
        /// ��Ŀ
        /// </summary>
        public Neusoft.HISFC.Object.Base.Item item = new Neusoft.HISFC.Object.Base.Item();
        #endregion 

        #region ����
        /// <summary>
        /// ִ�п���
        /// </summary>
        public NFC.Object.NeuObject ExeDept
        {
            get
            { 
                return execDeptInfo;
            }
            set
            {
                execDeptInfo = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public NFC.Object.NeuObject DeptInfo
        {
            get
            { 
                return deptInfo;
            }
            set
            {
                deptInfo = value;
            }
        }
        /// <summary>
        /// ��Ŀ
        /// </summary>
        public Neusoft.HISFC.Object.Base.Item Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }
        /// <summary>
        /// ��ʾ��ϸ��������
        /// </summary>
        public string UnitFlag
        {
            get
            {
                return unitFlag;
            }
            set
            {
                unitFlag = value;
            }
        }
        /// <summary>
        /// �Ƿ�ͳ��
        /// </summary>
        public string ISStat
        {
            get
            {
                return isStat;
            }
            set
            {
                isStat = value;
            }
        }
        #endregion 

        #region ��¡
        public new DeptUse Clone()
        {
            DeptUse obj = base.Clone() as DeptUse;
            obj.ExeDept = this.ExeDept.Clone();
            obj.DeptInfo = this.DeptInfo.Clone(); 
            obj.item = this.item.Clone();
            return obj;
        }
        #endregion 

        #region ����
        /// <summary>
        /// ִ�п���
        /// </summary>
        [Obsolete("��������ExeDept����",true)]
        public NFC.Object.NeuObject ExecDeptInfo
        {
            get
            {
                return execDeptInfo;
            }
            set
            {
                execDeptInfo = value;
            }
        }
        #endregion 
    }
}
