using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Material
{
    public interface IMatManager
    {
        /// <summary>
        /// ���ҵ��ӿ�
        /// </summary>
        FS.FrameWork.WinForms.Controls.ucBaseControl InputModualUC
        {
            get;
        }


        /// <summary>
        /// ���ݱ��ʼ��
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        System.Data.DataTable InitDataTable();

        /// <summary>
        /// ������Ʒ��Ŀ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parms"></param>
        int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow);

        /// <summary>
        /// ������Ʒ��Ŀ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parms"></param>
        int AddItem(FarPoint.Win.Spread.SheetView sv, FS.HISFC.Models.Material.Input input);

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int ShowApplyList();

        /// <summary>
        /// ��ⵥ��
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int ShowInList();

        /// <summary>
        /// ���ⵥ��
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int ShowOutList();

        /// <summary>
        /// ��ʾ�ɹ���Ϣ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int ShowStockList();

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns>�ɹ�����True ��Ч��Ϣ����False</returns>
        bool Valid();

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sv">��ɾ����������Fp</param>
        /// <param name="delRowIndex">��ɾ��������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int Delete(FarPoint.Win.Spread.SheetView sv, int delRowIndex);

        void SetFormat();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int Clear();

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="filterStr">������ֵ</param>
        void Filter(string filterStr);

        /// <summary>
        /// ��������
        /// </summary>
        void SetFocusSelect();

        /// <summary>
        /// ����
        /// </summary>
        void Save();

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int Print();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        int Cancel();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        int ImportData();

        /// <summary>
        /// �ͷ���Դ
        /// //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
        /// </summary>
        void Dispose();
    }
}
