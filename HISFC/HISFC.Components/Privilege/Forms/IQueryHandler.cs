using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.UFC.Privilege.Forms
{
    /// <summary>
    /// [��������: ��ѯ���ܵĿؼ��ӿ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        /// ��ʼ��ѯ
        /// </summary>
        event System.EventHandler BeginQuery;
        /// <summary>
        /// ������ѯ
        /// </summary>
        event System.EventHandler EndQuery;
        /// <summary>
        /// ��ʼ����
        /// </summary>
        event System.EventHandler BeginSave;
        /// <summary>
        /// ��������
        /// </summary>
        event System.EventHandler EndSave;
        /// <summary>
        /// ��ʼ��ӡ
        /// </summary>
        event System.EventHandler BeginPrint;
        /// <summary>
        /// ������ӡ
        /// </summary>
        event System.EventHandler EndPrint;
        /// <summary>
        /// ��ʼˢ��
        /// </summary>
        event System.EventHandler BeginRefresh;
        /// <summary>
        /// ����ˢ��
        /// </summary>
        event System.EventHandler EndRefresh;
        /// <summary>
        /// ˢ�°�ť�仯
        /// </summary>
        event System.EventHandler RefreshChanged;
        /// <summary>
        /// ��ӡ��ť�仯
        /// </summary>
        event System.EventHandler PrintChanged;
        /// <summary>
        /// ��ѯ��ť�仯
        /// </summary>
        event System.EventHandler QueryChanged;
        /// <summary>
        /// ��ӡ���ð�ť�仯
        /// </summary>
        event System.EventHandler PrintSetChanged;
        /// <summary>
        /// ��ӡԤ���仯
        /// </summary>
        event System.EventHandler PrintPreviewChanged;
        /// <summary>
        /// �˳���ť�仯
        /// </summary>
        event System.EventHandler ExitChanged;
        /// <summary>
        /// ���水ť�仯
        /// </summary>
        event System.EventHandler SaveChanged;


        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        int Query(object sender, object neuObject);

        /// <summary>
        /// ���棬ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        int Save(object sender, object neuObject);

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        int Print(object sender, object neuObject);

        /// <summary>
        /// ���ô�ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        int SetPrint(object sender, object neuObject);

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        int PrintPreview(object sender, object neuObject);

        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        int Exit(object sender, object neuObject);

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        int Export(object sender, object neuObject);

        /// <summary>
        /// ˢ��
        /// </summary>
        /// <returns></returns>
        void Refresh();

        /// <summary>
        /// �ؼ��ı�
        /// </summary>
        string ControlText { get;}
    }
}
