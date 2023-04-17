using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Pharmacy.In
{
    /// <summary>
    /// [��������: ���ҵ��ӿ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public interface IPhaInManager
    {
        ///// <summary>
        ///// ��ǰʵ��
        ///// </summary>
        //UFC.Pharmacy.In.ucPhaIn PhaInManager
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// ��ϸ��Ϣ¼��ؼ�
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
        /// ����ҩƷ��Ŀ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parms"></param>
        int AddItem(FarPoint.Win.Spread.SheetView sv, int activeRow);

        ///// <summary>
        ///// �����ݱ����������
        ///// </summary>
        ///// <param name="input">���ʵ����Ϣ</param>
        ///// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        //int AddDataToTable(FS.HISFC.Models.Pharmacy.Input input);

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
        /// ����
        /// </summary>
        /// <returns></returns>
        int ImportData();

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
        /// ע������
        /// </summary>
        /// <returns></returns>
        int Dispose();

    }
}
