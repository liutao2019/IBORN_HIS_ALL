using System;
using System.Collections;
namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// ϵͳ����ö����
    /// </summary>
    ///      
    [System.Serializable]
    public class ConstEnumService : EnumServiceBase
    {
        static ConstEnumService()
        {
            items[EnumConstant.REGTYPE] = "�ű�";
            items[EnumConstant.BANK] = "����";
            items[EnumConstant.NATION] = "����";
            items[EnumConstant.DIST] = "����";
            items[EnumConstant.COUNTRY] = "����";
            items[EnumConstant.ITEMTYPE] = "��Ŀ���";
            items[EnumConstant.INAVENUE] = "��Ժ;��";
            items[EnumConstant.INSOURCE] = "��Ժ��Դ";
            items[EnumConstant.PROFESSION] = "ְҵ";
            items[EnumConstant.CASEPROFESSION] = "ְҵ";
            items[EnumConstant.CITY] = "����";
            items[EnumConstant.RELATIVE] = "�벡�˹�ϵ";
            items[EnumConstant.INCIRCS] = "��Ժ���";
            items[EnumConstant.PAYKIND] = "�������";
            items[EnumConstant.MEDICALTYPE] = "ҽ�����";
            items[EnumConstant.BEDGRADE] = "��λ�ȼ�";
            items[EnumConstant.DERATEFEETYPE] = "��������";
            items[EnumConstant.POSITION] = "ְ��";
            items[EnumConstant.POLITICS] = "������ò";
            items[EnumConstant.LEVEL] = "ְ��";
            items[EnumConstant.AREA] = "����";
            items[EnumConstant.EMPLSTATUS] = "Ա��״̬";
            items[EnumConstant.EDUCATION] = "ѧ��";
            items[EnumConstant.ZG] = "����ת�����";
            items[EnumConstant.MINFEE] = "��С����";
            items[EnumConstant.USWAY] = "ʹ�÷�ʽ";
            items[EnumConstant.USAGE] = "ʹ�÷���";
            items[EnumConstant.DOSAGEFORM] = "����";
            items[EnumConstant.PHYFUNCTION] = "ҩ������";
            items[EnumConstant.PRICEFORM] = "�۸���ʽ";
            items[EnumConstant.PACKUNIT] = "��װ��λ";
            items[EnumConstant.MINUNIT] = "��С��λ";
            items[EnumConstant.DOSEUNIT] = "������λ";
            items[EnumConstant.ANESTYPE] = "��������";
            items[EnumConstant.INCITYPE] = "�п�����";
            items[EnumConstant.OPEPOS] = "������λ";
            items[EnumConstant.OPERATETYPE] = "������ģ";
            items[EnumConstant.EFFECT] = "Ч��";
            items[EnumConstant.DEMUKIND] = "��ʹ��ʽ";
            items[EnumConstant.DEMUMODEL] = "����";
            items[EnumConstant.PACUSTATUS] = "����PACU״��";
            items[EnumConstant.BLOODTYPE] = "��Ѫ����";
            items[EnumConstant.DIAGTYPE] = "�������";
            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}

            //items[EnumConstant.PACTUNIT]   = "��ͬ��λ";
            items[EnumConstant.DCREASON] = "ҽ��ֹͣԭ��";
            items[EnumConstant.SECTION] = "��鲿λ";
            items[EnumConstant.LABSAMPLE] = "��������";
            items[EnumConstant.DISEASECLASS] = "��������";
            items[EnumConstant.SPECIALDEPT] = "ר��";
            items[EnumConstant.REPORT] = "����";
            items[EnumConstant.STATTYPE] = "ͳ������";
            items[EnumConstant.EMPLTYPE] = "��Ա���";
            items[EnumConstant.REMARK] = "��ע";
            items[EnumConstant.HOSPITAL] = "��Ժ";
            items[EnumConstant.EMERGENCY] = "����˵��";
            items[EnumConstant.FEECODESTAT] = "��������";
            items[EnumConstant.EMRTYPE] = "��������";
            items[EnumConstant.ORDERMEMO] = "ҽ����ע";
            items[EnumConstant.SGYDW] = "ʡ��ҽ��λ";
            items[EnumConstant.DIAGPERIOD] = "����";
            items[EnumConstant.DIAGLEVEL] = "�ּ�";
            items[EnumConstant.AFTERMATH] = "�������";
            items[EnumConstant.TECHSTYLE] = "��Ŀ����";
            items[EnumConstant.TECHASS] = "��Ŀ���";
            items[EnumConstant.DIAGCLASS] = "��������";
            items[EnumConstant.EXECTIME] = "����ִ������";
            items[EnumConstant.COMPTYPE] = "��쵥λ";
            items[EnumConstant.SURNAME] = "�ټ���COMPTYPE";
            items[EnumConstant.EMPLOYEETYPE] = "��Ա����";
            items[EnumConstant.AREAMEDICAL] = "����ҽ��֤��";
            items[EnumConstant.DRUGQUALITY] = "ҩƷ����";
            items[EnumConstant.EXAMLEVEL] = "�����ݼ���";
            items[EnumConstant.EXAMSPECALTYPE] = "�����������";
            items[EnumConstant.OPERATIONTYPE] = "������������";
            items[EnumConstant.CHILDBEARINGRESULT] = "������";
            items[EnumConstant.BREATHSTATE] = "����״̬";
            items[EnumConstant.PHARMACYALLERGIC] = "ҩ�����";
            items[EnumConstant.DIAGNOSEACCORD] = "��Ϸ���";
            items[EnumConstant.CASEQUALITY] = "��������";
            items[EnumConstant.RHSTATE] = "RH���";
            items[EnumConstant.PERIODOFTREATMENT] = "�Ƴ��б�";
            items[EnumConstant.RADIATERESULT] = "�Ƴ̽��";
            items[EnumConstant.RADIATETYPE] = "���Ʒ�ʽ";
            items[EnumConstant.RADIATEPERIOD] = "���Ƴ�ʽ";
            items[EnumConstant.RADIATEDEVICE] = "����װ��";
            items[EnumConstant.CHEMOTHERAPY] = "���Ʒ�ʽ";
            items[EnumConstant.CHEMOTHERAPYWAY] = "���Ʒ���";
            items[EnumConstant.BLOODREACTION] = "ѪҺ��Ӧ";
            items[EnumConstant.ACCORDSTAT] = "�Ƿ����";
            items[EnumConstant.EQU_CHARGE_TYPE] = "�����豸����";
            items[EnumConstant.EQU_CUSTODY_GRADE] = "���ܵȼ�";
            items[EnumConstant.EQU_DESHOW] = "�۾�����";
            items[EnumConstant.EQU_DETYPE] = "�۾ɷ�ʽ";
            items[EnumConstant.EQU_FEE_IN] = "�豸������Ŀ";
            items[EnumConstant.EQU_FEE_OUT] = "�豸֧����Ŀ";
            items[EnumConstant.EQU_FEE_SOURCE] = "������Դ";
            items[EnumConstant.EQU_FOREIGN] = "�����Դ";
            items[EnumConstant.EQU_GAUGE_TYPE] = "������������";
            items[EnumConstant.EQU_LEAD_TYPE] = "�������豸����";
            items[EnumConstant.EQU_PROPERTY_RIGHT] = "��Ȩ����";
            items[EnumConstant.EQU_SOURCE] = "�豸��Դ";
            items[EnumConstant.EQU_STATE] = "�豸״̬";
            items[EnumConstant.EQU_UNIT] = "�豸��λ";
            items[EnumConstant.EQU_USE] = "�豸��;";
            items[EnumConstant.EQU_WASTE] = "���ϴ�����";
            items[EnumConstant.MONEY] = "�����������";

            ////{7D094A18-0FC9-4e8b-A8E6-901E55D4C20C}

            items[EnumConstant.OUT_TYPE] = "��Ժ��ʽ";
            items[EnumConstant.CURE_TYPE] = "�������";
            items[EnumConstant.USE_CHA_MED] = "������ҩ�Ƽ�";
            items[EnumConstant.YES_NO] = "�Ƿ�";
            //{36D85B5D-9C7A-43ad-8A6C-576955DDC97E}
            items[EnumConstant.WORKNAME] = "������λ";
            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
            items[EnumConstant.PAYKIND] = "֧����ʽ";
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            items[EnumConstant.ANESWAY] = "�������";
            items[EnumConstant.PAYMODESCASH] = "֧����ʽ�ֽ�������";

            //{b2a1f044-36fb-4beb-b1d4-017d8a2b0c65}
            items[EnumConstant.EMR_EDUCATIONAL] = "��������";
            items[EnumConstant.MEDICATIONKNOWLEDGE] = "��ҩ֪ʶ";
            items[EnumConstant.DIETKNOWLEDGE] = "��ʳ֪ʶ";
            items[EnumConstant.DISEASEKNOWLEDGE] = "����֪ʶ";
            items[EnumConstant.EDUCATIONALEFFECT] = "����Ч��";
            items[EnumConstant.PAYMODESCASH] = "��ͨ��ʶ";
        }

        EnumConstant enumConst;

        #region ����

        /// <summary>
        /// ����ö������
        /// </summary>
        protected static Hashtable items = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// ����ö������
        /// </summary>
        protected override Hashtable Items
        {
            get
            {
                return items;
            }
        }
        protected override System.Enum EnumItem
        {
            get
            {
                return this.enumConst;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
        #endregion


    }
    /// <summary>
    /// ����ö��
    /// </summary>
    public enum EnumConstant
    {

        /// <SUMMARY>
        /// �ű�
        /// </SUMMARY>
        ///
        REGTYPE,

        /// <SUMMARY>
        ///����
        /// </SUMMARY>
        BANK,

        /// <SUMMARY>
        /// ����
        /// </SUMMARY>
        /// 
        NATION,

        /// <SUMMARY>
        /// ����
        /// </SUMMARY>
        /// 
        DIST,

        /// <SUMMARY>
        /// ����
        /// </SUMMARY>
        /// 
        COUNTRY,

        /// <SUMMARY>
        /// ��Ŀ���-ҩƷ����ά���õ�һЩ
        /// </SUMMARY>
        /// 
        ITEMTYPE,

        /// <SUMMARY>
        /// ��Ժ;��
        /// </SUMMARY>
        /// 
        INAVENUE,

        /// <SUMMARY>
        /// ��Ժ��Դ
        /// </SUMMARY>
        /// 
        INSOURCE,

        /// <SUMMARY>
        /// ְҵ
        /// </SUMMARY>
        /// 
        PROFESSION,
        /// <SUMMARY>
        /// binganְҵ
        /// </SUMMARY>
        /// 
        CASEPROFESSION,

        /// <SUMMARY>
        ///����
        /// </SUMMARY>
        /// 
        CITY,

        /// <SUMMARY>
        /// �벡�˹�ϵ
        /// </SUMMARY>
        /// 
        RELATIVE,

        /// <SUMMARY>
        /// ��Ժ���
        /// </SUMMARY>
        /// 
        INCIRCS,

        /// <SUMMARY>
        /// �������-�������
        ///  01-�Է�  02-���� 03-������ְ 04-�������� 05-���Ѹ߸� 
        /// </SUMMARY>
        /// 
        PAYKIND,

        /// <SUMMARY>
        /// ҽ�����
        /// </SUMMARY>
        MEDICALTYPE,

        /// <SUMMARY>
        /// ��λ�ȼ� 
        /// </SUMMARY>
        /// 
        BEDGRADE,

        /// <SUMMARY>
        /// ��������
        /// </SUMMARY>
        DERATEFEETYPE,

        /// <SUMMARY>
        /// ְ��
        /// </SUMMARY>
        POSITION,

        /// <SUMMARY>
        /// ������ò
        /// </SUMMARY>
        POLITICS,

        /// <SUMMARY>
        /// ְ��
        /// </SUMMARY>
        LEVEL,

        /// <SUMMARY>
        /// ����
        /// </SUMMARY>
        AREA,

        /// <SUMMARY>
        /// Ա��״̬
        /// </SUMMARY>
        EMPLSTATUS,

        /// <SUMMARY>
        /// ѧ��
        /// </SUMMARY>
        EDUCATION,

        /// <summary>
        /// ����ת�����
        /// </summary>
        ZG,

        /// <summary>
        /// ��С����
        /// </summary>
        MINFEE,

        /// <SUMMARY>
        /// ʹ�÷�ʽ��ҩƷ
        /// </SUMMARY>
        USWAY,

        /// <SUMMARY>
        /// ʹ�÷���-ҩƷ
        /// </SUMMARY>
        /// 
        USAGE,

        /// <SUMMARY>
        /// ����-ҩƷ
        /// </SUMMARY>
        /// 
        DOSAGEFORM,

        /// <SUMMARY>
        /// ҩ������-ҩƷ
        /// </SUMMARY>
        /// 
        PHYFUNCTION,

        /// <SUMMARY>
        /// �۸���ʽ-ҩƷ
        /// </SUMMARY>
        /// 
        PRICEFORM,

        /// <SUMMARY>
        /// ��װ��λ-ҩƷ
        /// </SUMMARY>
        /// 
        PACKUNIT,

        /// <SUMMARY>
        /// ��С��λ-ҩƷ
        /// </SUMMARY>
        /// 
        MINUNIT,

        /// <SUMMARY>
        /// ������λ-ҩƷ
        /// </SUMMARY>
        /// 
        DOSEUNIT,

        /// <SUMMARY>
        /// �������ͣ���������
        /// </SUMMARY>
        ANESTYPE,

        /// <SUMMARY>
        /// �п����ͣ���������
        /// </SUMMARY>
        INCITYPE,

        /// <SUMMARY>
        /// ������λ����������
        /// </SUMMARY>
        OPEPOS,

        /// <SUMMARY>
        /// ������ģ����������
        /// </SUMMARY>
        OPERATETYPE,

        /// <SUMMARY>
        /// Ч������������
        /// </SUMMARY>
        EFFECT,

        /// <SUMMARY>
        /// ��ʹ��ʽ����������
        /// </SUMMARY>
        DEMUKIND,

        /// <SUMMARY>
        /// ���ͣ���������
        /// </SUMMARY>
        DEMUMODEL,

        /// <SUMMARY>
        /// ����PACU״������������
        /// </SUMMARY>
        PACUSTATUS,

        /// <SUMMARY>
        /// ��Ѫ���ͣ�Ѫ��
        /// </SUMMARY>
        BLOODTYPE,

        /// <SUMMARY>
        /// �������
        /// </SUMMARY>
        DIAGTYPE,

        ///<SUMMARY>
        /// ��ͬ��λ
        ///</SUMMARY>
        ///{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
        //[Obsolete("����,ȡ��ͬ��λ����FeeIntegrate.QueryPactUnitAll() ����", true)]
        //PACTUNIT,

        /// <SUMMARY>
        /// ҽ��ֹͣԭ��
        /// </SUMMARY>
        DCREASON,

        /// <SUMMARY>
        /// ��鲿λ
        /// </SUMMARY>
        SECTION,

        /// <SUMMARY>
        /// ��������
        /// </SUMMARY>
        LABSAMPLE,

        /// <summary>
        /// ��������Ŀ�ļ�������
        /// </summary>
        DISEASECLASS,

        /// <summary>
        /// ��������Ŀ��ר�Ʒ���
        /// </summary>
        SPECIALDEPT,

        /// <summary>
        ///	����
        /// </summary>
        REPORT,

        /// <summary>
        ///	ͳ������
        /// </summary>
        STATTYPE,

        /// <summary>
        /// ��Ա���
        /// </summary>
        EMPLTYPE,

        /// <SUMMARY>
        /// ��ע
        /// </SUMMARY>
        /// 
        REMARK,

        /// <summary>
        /// ��Ժ
        /// </summary>
        HOSPITAL,

        /// <summary>
        /// ����˵��
        /// </summary>
        EMERGENCY,

        /// <summary>
        /// ��������
        /// </summary>
        FEECODESTAT,

        /// <summary>
        /// ��������,��סԺ���������̼�¼��������ҳ��
        /// </summary>
        EMRTYPE,

        /// <summary>
        /// ҽ����ע
        /// </summary>
        ORDERMEMO,

        /// <summary>
        /// ʡ��ҽ��λ
        /// </summary>
        SGYDW,

        /// <summary>
        /// ����
        /// </summary>
        DIAGPERIOD,

        /// <summary>
        /// �ּ�
        /// </summary>
        DIAGLEVEL,

        /// <summary>
        /// �������
        /// </summary>
        AFTERMATH,

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        TECHSTYLE,

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        TECHASS,

        /// <summary>
        /// ��������
        /// </summary>
        DIAGCLASS,

        /// <summary>
        /// ����ִ������
        /// </summary>
        EXECTIME,

        /// <summary>
        /// ����ִ������
        /// </summary>
        COMPTYPE,

        /// <summary>
        /// �ټ���COMPTYPE
        /// </summary>
        SURNAME,

        /// <summary>
        /// ��Ա����
        /// </summary>
        EMPLOYEETYPE,

        /// <summary>
        /// ����ҽ��֤��
        /// </summary>
        AREAMEDICAL,

        /// <summary>
        /// ҩƷ���� 
        /// </summary>
        DRUGQUALITY,
        /// <summary>
        /// �����ݼ���
        /// </summary>
        EXAMLEVEL,
        /// <summary>
        /// �����������
        /// </summary>
        EXAMSPECALTYPE,
        /// <summary>
        /// ������������
        /// </summary>
        OPERATIONTYPE,
        /// <summary>
        /// ������
        /// </summary>
        CHILDBEARINGRESULT,
        /// <summary>
        /// ����״̬
        /// </summary>
        BREATHSTATE,
        /// <summary>
        /// ҩ�����
        /// </summary>
        PHARMACYALLERGIC,
        /// <summary>
        /// ��Ϸ���
        /// </summary>
        DIAGNOSEACCORD,
        /// <summary>
        /// ��������
        /// </summary>
        CASEQUALITY,
        /// <summary>
        /// RH���
        /// </summary>
        RHSTATE,
        /// <summary>
        /// ��Ѫ��Ӧ
        /// </summary>
        BLOODREACTION,
        /// <summary>
        /// �Ƴ��б�
        /// </summary>
        PERIODOFTREATMENT,
        /// <summary>
        /// �Ƴ̽��
        /// </summary>
        RADIATERESULT,
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        RADIATETYPE,
        /// <summary>
        /// ���Ƴ�ʽ
        /// </summary>
        RADIATEPERIOD,
        /// <summary>
        /// ����װ��
        /// </summary>
        RADIATEDEVICE,
        /// <summary>
        /// ���Ʒ�ʽ
        /// </summary>
        CHEMOTHERAPY,
        /// <summary>
        /// ���Ʒ���
        /// </summary>
        CHEMOTHERAPYWAY,
        /// <summary>
        /// �Ƿ����
        /// </summary>
        ACCORDSTAT,

        /// <summary>
        /// �����豸����
        /// </summary>
        EQU_CHARGE_TYPE,
        /// <summary>
        /// ���ܵȼ�
        /// </summary>
        EQU_CUSTODY_GRADE,
        /// <summary>
        /// �۾�����
        /// </summary>
        EQU_DESHOW,
        /// <summary>
        /// �۾ɷ�ʽ
        /// </summary>
        EQU_DETYPE,
        /// <summary>
        /// �豸������Ŀ
        /// </summary>
        EQU_FEE_IN,
        /// <summary>
        /// �豸֧����Ŀ
        /// </summary>
        EQU_FEE_OUT,
        /// <summary>
        /// ������Դ
        /// </summary>
        EQU_FEE_SOURCE,
        /// <summary>
        /// �����Դ
        /// </summary>
        EQU_FOREIGN,
        /// <summary>
        /// ������������
        /// </summary>
        EQU_GAUGE_TYPE,
        /// <summary>
        /// �������豸����
        /// </summary>
        EQU_LEAD_TYPE,
        /// <summary>
        /// ��Ȩ����
        /// </summary>
        EQU_PROPERTY_RIGHT,
        /// <summary>
        /// �豸��Դ
        /// </summary>
        EQU_SOURCE,
        /// <summary>
        /// �豸״̬
        /// </summary>
        EQU_STATE,
        /// <summary>
        /// �豸��λ
        /// </summary>
        EQU_UNIT,
        /// <summary>
        /// �豸��;
        /// </summary>
        EQU_USE,
        /// <summary>
        /// ���ϴ�����
        /// </summary>
        EQU_WASTE,
        /// <summary>
        /// �����������
        /// </summary>
        MONEY,
        /// <summary>
        /// ��Ժ��ʽ
        /// </summary>
        OUT_TYPE,
        /// <summary>
        /// �������
        /// </summary>
        CURE_TYPE,
        /// <summary>
        /// ������ҩ�Ƽ�
        /// </summary>
        USE_CHA_MED,
        /// <summary>
        /// �Ƿ�
        /// </summary>
        YES_NO,
        //{36D85B5D-9C7A-43ad-8A6C-576955DDC97E}
        /// <summary>
        /// ������λ
        /// </summary>
        WORKNAME,

        //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
        PAYMODES,
        //
        // ժҪ:
        //     רҵ
        SPECIALITY,
        //
        // ժҪ:
        //     ִҵ����
        VOCATIONTYPE,
        //{B9DDCC10-3380-4212-99E5-BB909643F11B}
        /// <summary>
        /// ������𣨾����ѡ�飬ҽ������ʱ��д��
        /// </summary>
        ANESWAY,
        //
        // ժҪ:
        //     ϵͳ���(����ά����ͬ��λ)
        SYSTYPE,
        /// <summary>
        /// ֧����ʽ���ֽ������չ�ϵ������֧����ʽ�޸�{3A31B775-3A98-4b6d-ABE0-555280ED7D06}
        /// </summary>
        PAYMODESCASH,

        /// <summary>
        /// ��������
        /// </summary>
        EMR_EDUCATIONAL,
        /// <summary>
        /// ��ҩ֪ʶ
        /// </summary>
        MEDICATIONKNOWLEDGE,
        /// <summary>
        /// ��ʳ֪ʶ
        /// </summary>

        DIETKNOWLEDGE,

        /// <summary>
        /// ����֪ʶ
        /// </summary>
        DISEASEKNOWLEDGE,
        /// <summary>
        /// ����Ч��
        /// 
        /// </summary>
        EDUCATIONALEFFECT,
        /// <summary>
        ///  ��ͨ��ʶ
        /// </summary>
        TRAFFICKNOWLEDGE

    }
}
