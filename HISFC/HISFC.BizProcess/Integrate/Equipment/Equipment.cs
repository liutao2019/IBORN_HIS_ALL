using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.HISFC.BizProcess.Integrate.Equipment
{    
    /// <summary>
    /// Base<br></br>
    /// [��������: ���ϵ��豸������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-11-2]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Equipment : IntegrateBase
    {
        /// <summary>
	    /// ���캯��
	    /// </summary>
        public Equipment()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

	    #region ����

	    #region ˽��
	    #endregion

	    #region ����

        /// <summary>
        /// ��Ŀ��Ϣҵ���
        /// </summary>
        protected Neusoft.HISFC.BizLogic.Equipment.Base baseInfoMgr = new Neusoft.HISFC.BizLogic.Equipment.Base();

	    #endregion

	    #region ����
	    #endregion

	    #endregion

	    #region ����
	    #endregion

	    #region ����

	    #region ˽��
	    #endregion

	    #region ����
	    #endregion

	    #region ����

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="trans">���ݿ�����</param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            baseInfoMgr.SetTrans(trans);
            this.trans = trans;
        }

        /// <summary>
        /// �����豸�����ȡһ����Ŀ��Ϣ
        /// </summary>
        /// <param name="EquCode">�豸���루LOG_EQU_BASEINFO��������</param>
        /// <returns>�豸��Ŀ��Ϣʵ�� ʧ�ܣ�null</returns>
        public Neusoft.HISFC.Models.Equipment.EquipBase GetBaseInfo(string EquCode)
        {
            this.SetDB(baseInfoMgr);
            return baseInfoMgr.GetBaseInfo(EquCode);
        }
                
        /// <summary>
        /// ����Ŀ��Ϣ���в���һ����¼
        /// </summary>
        /// <param name="BaseInfo">��Ŀʵ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int InsertBaseInfo(Neusoft.HISFC.Models.Equipment.EquipBase BaseInfo)
        {
            this.SetDB(baseInfoMgr);
            if (baseInfoMgr.InsertBaseInfo(BaseInfo) == -1)
            {
                return -1;
            }
            return 1;
        }
                
        /// <summary>
        /// ȡ��Ŀ��Ϣ�б�
        /// </summary>
        /// <returns>��Ŀ��Ϣ���飬������null</returns>
        public ArrayList QueryAllBaseInfo()
        {
            this.SetDB(baseInfoMgr);
            return baseInfoMgr.QueryAllBaseInfo();
        }
        
        /// <summary>
        /// �����豸��������ȡ��Ŀ��Ϣ�б�
        /// </summary>
        /// <returns>��Ŀ��Ϣ���飬������null</returns>
        public ArrayList QueryAllBaseInfoByKind(string ID)
        {
            this.SetDB(baseInfoMgr);
            return baseInfoMgr.QueryAllBaseInfoByKind(ID);
        }
                
        /// <summary>
        /// ������Ŀ��Ϣ����һ����¼
        /// </summary>
        /// <param name="BaseInfo">��Ŀʵ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int UpdateBaseInfo(Neusoft.HISFC.Models.Equipment.EquipBase BaseInfo)
        {
            this.SetDB(baseInfoMgr);
            return baseInfoMgr.UpdateBaseInfo(BaseInfo);
        }
                
        /// <summary>
        /// ������Ŀʵ��䶯���ݣ�����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
        /// </summary>
        /// <param name="BaseInfo">��Ŀ��Ϣʵ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int SetBaseInfo(Neusoft.HISFC.Models.Equipment.EquipBase BaseInfo)
        {
            this.SetDB(baseInfoMgr);
            return baseInfoMgr.SetBaseInfo(BaseInfo);
        }

	    #endregion

	    #endregion

    }
}
