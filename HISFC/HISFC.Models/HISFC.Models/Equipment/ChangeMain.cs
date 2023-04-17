using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Equipment
{
    /// <summary>
    /// Company<br></br>
    /// [��������: ��Ƭ���ʵ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-11-21]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class ChangeMain : FS.HISFC.Models.Base.Spell
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public ChangeMain()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion ���캯��

        #region ����

        #region ˽�б���

        /// <summary>
        /// �ɿ�Ƭʵ��
        /// </summary>
        private Main oldMain = new Main();

        /// <summary>
        /// �¿�Ƭʵ��
        /// </summary>
        private Main newMain = new Main();

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment dealOper = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion ˽�б���

        #region ��������
        #endregion ��������

        #region ��������
        #endregion ��������

        #endregion ����

        #region ����

        /// <summary>
        /// �ɿ�Ƭʵ��
        /// </summary>
        public Main OldMain
        {
            get { return oldMain; }
            set { oldMain = value; }
        }

        /// <summary>
        /// �¿�Ƭʵ��
        /// </summary>
        public Main NewMain
        {
            get { return newMain; }
            set { newMain = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment DealOper
        {
            get { return dealOper; }
            set { dealOper = value; }
        }

        /// <summary>
        /// ����Ա
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        #endregion ����

        #region ����

        #region ��Դ�ͷ�
        #endregion ��Դ�ͷ�

        #region ��¡

        /// <summary>
        /// ������¡
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���DeMethodʵ�� ʧ�ܷ���null</returns>
        public new ChangeMain Clone()
        {
            ChangeMain changeMain = base.Clone() as ChangeMain;
            changeMain.OldMain = this.oldMain.Clone();
            changeMain.NewMain = this.newMain.Clone();
            changeMain.DealOper = this.dealOper.Clone();
            changeMain.Oper = this.oper.Clone();
            return changeMain;
        }

        #endregion ��¡

        #region ˽�з���
        #endregion ˽�з���

        #region ��������
        #endregion ��������

        #region ��������
        #endregion ��������

        #endregion ����

        #region �ӿ�ʵ��
        #endregion �ӿ�ʵ��

	
    }
}
