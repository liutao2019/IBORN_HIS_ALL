using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Neusoft.HISFC.Models.Pharmacy;

namespace Neusoft.HISFC.BizProcess.Integrate.PharmacyInterface
{
    /// <summary>
    /// [��������: ҩƷҵ��ӿ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// </summary>
    public interface IInpatientDrug
    {
        /// <summary>
        /// ����ǰ
        /// </summary>
        event System.EventHandler BeginSaveEvent;

        /// <summary>
        /// �����
        /// </summary>
        event System.EventHandler EndSaveEvent;

        /// <summary>
        /// ���ݴ���ĳ����������ݣ���ʾ�ڿؼ���
        /// </summary>
        /// <param name="alApplyOut">������������</param>
        void ShowData(ArrayList alApplyOut);

        /// <summary>
        /// Checkȫ������
        /// </summary>
        void CheckAll();

        /// <summary>
        /// û��Check�κ�����
        /// </summary>
        void CheckNone();

        /// <summary>
        /// ���ȫ������
        /// </summary>
        void Clear();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>1�ɹ���-1ʧ��</returns>
        int Save(Neusoft.HISFC.Models.Pharmacy.DrugMessage drugMessage);

        /// <summary>
        /// ��ӡ
        /// </summary>
        void Print();

        /// <summary>
        /// Ԥ��
        /// </summary>
        void Preview();
    }

    /// <summary>
    /// ҩƷ��ҩ��/��ǩ ��ӡ�ӿ� 
    /// </summary>
    public interface IDrugPrint
    {
        /// <summary>
        /// ���ﻼ����Ϣ
        /// </summary>
        Neusoft.HISFC.Models.Registration.Register OutpatientInfo
        {
            get;
            set;
        }

        /// <summary>
        /// סԺ��������
        /// </summary>
        Neusoft.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get;
            set;
        }

        /// <summary>
        /// ���δ�ӡ��ǩ��ҳ��
        /// </summary>
        decimal LabelTotNum
        {
            set;
        }

        /// <summary>
        /// һ�δ�ӡҩƷ����������
        /// </summary>
        decimal DrugTotNum
        {
            set;
        }

        /// <summary>
        /// ��ӡ�°�ҩ��ǩ ����ҩƷ
        /// </summary>
        /// <param name="info">��ҩ����</param>
        void AddSingle(ApplyOut info);

        /// <summary>
        /// ��ӡ��ҩ��ǩ ��ϴ�ӡ 
        /// </summary>
        /// <param name="alCombo">��ӡ�������</param>
        void AddCombo(ArrayList alCombo);

        /// <summary>
        /// ��ӡ��ҩ�嵥
        /// </summary>
        /// <param name="al">���д���ӡ����</param>
        void AddAllData(ArrayList al);

        /// <summary>
        /// ��ҩ����ӡ ��ʾȫ������
        /// </summary>
        /// <param name="al">����ӡ�İ�ҩ������Ϣ</param>
        /// <param name="drugBillClass">��ҩ֪ͨ��Ϣ</param>
        void AddAllData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugBillClass drugBillClass);

        /// <summary>
        /// ��ҩ����ӡ ��ʾȫ������
        /// </summary>
        /// <param name="al">����ӡ�İ�ҩ������Ϣ</param>
        /// <param name="drugRecipe">���ﴦ��������Ϣ</param>
        void AddAllData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe);

        /// <summary>
        /// ��ӡ��ҩ��
        /// </summary>
        void Print();

        /// <summary>
        /// Ԥ����ҩ��
        /// </summary>
        void Preview();
    }

    /// <summary>
    /// ������Ϣ��ʾ�ӿ�
    /// </summary>
    public interface IOutpatientShow
    {
        /// <summary>
        /// ����ʾ��������
        /// </summary>
        /// <param name="drugRecipe"></param>
        void ShowInfo(Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe);
    }

    /// <summary>
    /// ҩƷLED������ʾ�ӿ�
    /// </summary>
    public interface IOutpatientLEDShow
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="drugRecipe">����ʾ����</param>
        void SetShowData(List<Neusoft.HISFC.Models.Pharmacy.DrugRecipe> drugRecipe);

        /// <summary>
        /// ����Ļ��ʾ
        /// </summary>
        void Show();
    }

    /// <summary>
    /// ҩƷ���/���ⵥ�ݴ�ӡ
    /// </summary>
    public interface IBillPrint
    {
        /// <summary>
        /// ���ʹ�ӡ����
        /// </summary>
        /// <param name="billNO">���ݺ�</param>
        /// <returns></returns>
        int SetData(string billNO);

        /// <summary>
        /// ���ʹ�ӡ����
        /// </summary>
        /// <param name="alPrintData">����ӡ����</param>
        /// <param name="privType">ϵͳ���� Class3_Meaning_Code</param>
        /// <returns></returns>
        int SetData(ArrayList alPrintData, string privType);

        /// <summary>
        /// ���ʹ�ӡ����
        /// </summary>
        /// <param name="alPrintData"></param>
        /// <param name="billType">enum BillType</param>
        /// <returns></returns>
        int SetData(ArrayList alPrintData, BillType billType);

        int Print();

        int Prieview();
    }

    /// <summary>
    /// ��������
    /// </summary>
    public enum BillType
    {
        /// <summary>
        /// ���
        /// </summary>
        Input,
        /// <summary>
        /// ����
        /// </summary>
        Output,
        /// <summary>
        /// ���ƻ�
        /// </summary>
        InPlan,
        /// <summary>
        /// �ɹ��ƻ�
        /// </summary>
        StockPlan,
        /// <summary>
        /// �̵�
        /// </summary>
        Check,
        /// <summary>
        /// ����
        /// </summary>
        Adjust,
        /// <summary>
        /// �ڲ��������              //{0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636} ʵ���ڲ�������뵥��ӡ
        /// </summary>
        InnerApplyIn
    }


    /// <summary>
    /// ���ñ�ǩ��ӡ
    /// </summary>
    public interface ICompoundPrint
    {
        /// <summary>
        /// ���δ�ӡ��ǩ��ҳ��
        /// </summary>
        decimal LabelTotNum
        {
            set;
        }

        /// <summary>
        /// סԺ��������
        /// </summary>
        Neusoft.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get;
            set;
        }

        /// <summary>
        /// ��ӡ ��ϴ�ӡ 
        /// </summary>
        /// <param name="alCombo">��ӡ�������</param>
        void AddCombo(ArrayList alCombo);

        /// <summary>
        /// �����д�ӡ���ݴ���
        /// </summary>
        /// <param name="al">���д���ӡ����</param>
        void AddAllData(ArrayList al);

        void Clear();

        int Print();

        int Prieview();
    }

    /// <summary>
    /// �����ӡ�ӿڹ��� ͨ���ýӿڹ������ش�ӡ�ӿ�IDrugPrint
    /// </summary>
    public interface IOutpatientPrintFactory
    {
        IDrugPrint GetInstance(Neusoft.HISFC.Models.Pharmacy.DrugTerminal terminal);
    }
}
