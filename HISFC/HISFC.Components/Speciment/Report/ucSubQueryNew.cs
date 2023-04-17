using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucSubQueryNew : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private SubSpecManage subMgr = new SubSpecManage();
        private string curSql = "";
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private string orgOrBld = "";
        private SpecTypeManage typeMgr = new SpecTypeManage();
        private DisTypeManage disMgr = new DisTypeManage();
        private IceBoxManage ibMgr = new IceBoxManage();
        private FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase icdMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
        private List<string> specIdList = new List<string>();
        private string specIds = "(0,";
        private string specBar = string.Empty;
        private bool isAddAppOutData = false;

        private SpecApplyOutManage appMgr = new SpecApplyOutManage();
        private ApplyTableManage appTabMgr = new ApplyTableManage();
        private ucShowAppInfo ucShowApp = new ucShowAppInfo();

        private ucUserOrder ucUserOrderControl = new ucUserOrder();

        private string bldFilter = string.Empty;

        /// <summary>
        /// ��ϸ����ͼ
        /// </summary>
        private DataView dtDataView = new DataView();
        public DataView DtDataView
        {
            get
            {
                return this.dtDataView;
            }
            set
            {
                this.dtDataView = value;
            }
        }

        /// <summary>
        /// ������ͼ
        /// </summary>
        private DataView stkDataView = new DataView();
        public DataView StkDataView
        {
            get
            {
                return this.stkDataView;
            }
            set
            {
                this.stkDataView = value;
            }
        }

        //��¼ԭ����ϸ��dataview
        private DataView oldDv = new DataView();
        private bool isOldData = true;

        private bool hasWhere = false;

        /// <summary>
        /// ��һ��ѯ��
        /// </summary>
        private enum ColFs
        {
            SPECID,
            Դ����,
            ����,
            �걾��,
            ����,
            ������,
            �Ա�,
            ����,
            ����,
            ����,
            ����,
            ����״��,
            ְҵ,
            ������,
            ���֤����,
            ������λ,
            ��λ�绰,
            ���ڵ�ַ,
            ��ͥ�绰,
            ��ϵ��,
            ��ϵ,
            ��ϵ��ַ,
            ��ϵ�绰,
            ��Ժʱ��,
            ��Ժʱ��,
            �ʹ����,
            �ʹ�ҽ��,
            ȡ������,
            D��,
            DT,
            DP,
            DN,
            DL,
            DE,
            DS,
            R��,
            RT,
            RP,
            RN,
            RL,
            RE,
            RS,
            P��,
            PT,
            PP,
            PN,
            PL,
            PE,
            PS,
            ����,
            DTO,
            DPO,
            DNO,
            DLO,
            DEO,
            DSO,
            RTO,
            RPO,
            RNO,
            RLO,
            REO,
            RSO,
            PTO,
            PPO,
            PNO,
            PLO,
            PEO,
            PSO,
            Ѫ���װ,
            Ѫ����װ,
            ��ϸ����װ,
            Ѫ��������,
            Ѫ����,
            Ѫ���������,
            Ѫ�����,
            ��ϸ���������,
            ��ϸ�����,
            ����ʱ��,
            ��Ѫ����ʱ��,
            ������,
            ���,
            ��ע
        }

        /// <summary>
        /// �ڶ���ѯ��
        /// </summary>
        private enum Cols
        {
            סԺ��ˮ��,
            ������,
            ����,
            �걾��,
            ����,
            SPECID,
            Ѫ��,
            Ѫ��,
            ϸ��,
            DNA,
            RNA,
            ����,
            ��Բ���,
            ��Ա걾��,
            MSSPECID
        }

        /// <summary>
        /// ������ѯ��
        /// </summary>
        private enum ColTd
        {
            ����,
            ת�Ʋ�λ,
            ���Ʒ�ʽ,
            ���Ƴ�ʽ,
            ����װ��,
            ���Ʒ�ʽ,
            ����ʱ��,
            ����֢,
            ���Ʒ���,
            HBsAg,
            HCVAb,
            HIVAb,
            ת��,
            ���ʱ��,
            һ�����,
            ֢״����,
            �Ƿ񸴷�,
            ��÷�ʽ
        }

        public ucSubQueryNew()
        {
            InitializeComponent();
        }

        private void InitSheet1()
        {
            neuSpread2_Sheet1.Columns.Count = Convert.ToInt32(Cols.MSSPECID) + 1;

            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.סԺ��ˮ��)].Width = 120;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.סԺ��ˮ��)].Label = Cols.סԺ��ˮ��.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.�걾��)].Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.�걾��)].Label = Cols.�걾��.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.Ѫ��)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.Ѫ��)].Label = Cols.Ѫ��.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.SPECID)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.SPECID)].Label = Cols.SPECID.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.Ѫ��)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.Ѫ��)].Label = Cols.Ѫ��.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.ϸ��)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.ϸ��)].Label = Cols.ϸ��.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.DNA)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.DNA)].Label = Cols.DNA.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.RNA)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.RNA)].Label = Cols.RNA.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.����)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.����)].Label = Cols.����.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.��Բ���)].Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.��Բ���)].Label = Cols.��Բ���.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.��Ա걾��)].Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.��Ա걾��)].Label = Cols.��Ա걾��.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.MSSPECID)].Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.MSSPECID)].Label = Cols.MSSPECID.ToString();

           
        }

        private void InitSheet(FarPoint.Win.Spread.SheetView sheet)
        {
            sheet.Columns.Count = Convert.ToInt32(ColFs.��ע) + 1;
            sheet.Columns[Convert.ToInt32(ColFs.SPECID)].Visible = false;

            sheet.Columns[Convert.ToInt32(ColFs.Դ����)].Width = 95;
            sheet.Columns[Convert.ToInt32(ColFs.Դ����)].Label = ColFs.Դ����.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.����)].Width = 50;
            sheet.Columns[Convert.ToInt32(ColFs.����)].Label = ColFs.����.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.�걾��)].Width = 82;
            sheet.Columns[Convert.ToInt32(ColFs.�걾��)].Label = ColFs.�걾��.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.����)].Width = 78;
            sheet.Columns[Convert.ToInt32(ColFs.����)].Label = ColFs.����.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.������)].Width = 90;
            sheet.Columns[Convert.ToInt32(ColFs.������)].Label = ColFs.������.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.�Ա�)].Width = 30;
            sheet.Columns[Convert.ToInt32(ColFs.�Ա�)].Label = ColFs.�Ա�.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.����)].Width = 90;
            sheet.Columns[Convert.ToInt32(ColFs.����)].Label = ColFs.����.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.�ʹ����)].Width = 90;
            sheet.Columns[Convert.ToInt32(ColFs.�ʹ����)].Label = ColFs.�ʹ����.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.�ʹ�ҽ��)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.�ʹ�ҽ��)].Label = ColFs.�ʹ�ҽ��.ToString();
            
            sheet.Columns[Convert.ToInt32(ColFs.ȡ������)].Width = 70;
            sheet.Columns[Convert.ToInt32(ColFs.ȡ������)].Label = ColFs.ȡ������.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.����ʱ��)].Width = 90;
            sheet.Columns[Convert.ToInt32(ColFs.����ʱ��)].Label = ColFs.����ʱ��.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.��Ѫ����ʱ��)].Width = 60;
            sheet.Columns[Convert.ToInt32(ColFs.��Ѫ����ʱ��)].Label = ColFs.��Ѫ����ʱ��.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.������)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.������)].Label = ColFs.������.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.���)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.���)].Label = ColFs.���.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.��ע)].Width = 120;
            sheet.Columns[Convert.ToInt32(ColFs.��ע)].Label = ColFs.��ע.ToString();

            #region DNA
            sheet.Columns[Convert.ToInt32(ColFs.D��)].Width = 30;
            sheet.Columns[Convert.ToInt32(ColFs.D��)].Label = ColFs.D��.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.DT)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.DT)].Label = ColFs.DT.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.DP)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.DP)].Label = ColFs.DP.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.DN)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.DN)].Label = ColFs.DN.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.DL)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.DL)].Label = ColFs.DL.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.DE)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.DE)].Label = ColFs.DE.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.DS)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.DS)].Label = ColFs.DS.ToString();
            #endregion

            #region RNA
            sheet.Columns[Convert.ToInt32(ColFs.R��)].Width = 30;
            sheet.Columns[Convert.ToInt32(ColFs.R��)].Label = ColFs.R��.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.RT)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.RT)].Label = ColFs.RT.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.RP)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.RP)].Label = ColFs.RP.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.RN)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.RN)].Label = ColFs.RN.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.RL)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.RL)].Label = ColFs.RL.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.RE)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.RE)].Label = ColFs.RE.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.RS)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.RS)].Label = ColFs.RS.ToString();
            #endregion

            #region ����
            sheet.Columns[Convert.ToInt32(ColFs.PT)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.PT)].Label = ColFs.PT.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.PP)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.PP)].Label = ColFs.PP.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.PN)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.PN)].Label = ColFs.PN.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.PL)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.PL)].Label = ColFs.PL.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.PE)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.PE)].Label = ColFs.PE.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.PS)].Width = 40;
            sheet.Columns[Convert.ToInt32(ColFs.PS)].Label = ColFs.PS.ToString();
            #endregion

            #region Ѫ��װ
            sheet.Columns[Convert.ToInt32(ColFs.Ѫ���װ)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.Ѫ���װ)].Label = ColFs.Ѫ���װ.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.Ѫ����װ)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.Ѫ����װ)].Label = ColFs.Ѫ����װ.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.��ϸ����װ)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.��ϸ����װ)].Label = ColFs.��ϸ����װ.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.Ѫ����)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.Ѫ����)].Label = ColFs.Ѫ����.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.Ѫ��������)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.Ѫ��������)].Label = ColFs.Ѫ��������.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.Ѫ�����)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.Ѫ�����)].Label = ColFs.Ѫ�����.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.Ѫ���������)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.Ѫ���������)].Label = ColFs.Ѫ���������.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.��ϸ�����)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.��ϸ�����)].Label = ColFs.��ϸ�����.ToString();

            sheet.Columns[Convert.ToInt32(ColFs.��ϸ���������)].Width = 80;
            sheet.Columns[Convert.ToInt32(ColFs.��ϸ���������)].Label = ColFs.��ϸ���������.ToString();
            #endregion

            for (int i = 0; i < sheet.Columns.Count; i++)
            {
                sheet.ColumnHeader.Rows[0].Height = 30;
            }
        }

        /// <summary>
        /// ��ϸ�Ĳ�ѯ����
        /// </summary>
        /// <returns></returns>
        private string GetSql()
        {
            #region
            //Ѫ�걾��ϸSQL
            string bSql = @"with t as
                     (SELECT DISTINCT
                     s.SPECID, 
                     s.HISBARCODE Դ����,
                     d.DISEASENAME ����,
                     s.SPEC_NO �걾��,
                     p.pname ����, 
                     p.CARD_NO ������,
                     (case p.GENDER when 'F' then 'Ů' else '��' end) �Ա�,
                     p.BIRTHDAY ����,
                     p.CONTACTNUM ��ϵ�绰,
                     p.HOMEPHONENUM ��ͥ�绰,
                     (select di.name from COM_DICTIONARY di where p.NATIONALITY = di.CODE and di.type = 'COUNTRY') ����,
                     (select di.name from COM_DICTIONARY di where p.NATION = di.CODE and di.type = 'NATION') ����,
                     p.IDCARDNO ���֤��,
                     p.HOME ����,
                     p.ADDRESS ��ͥסַ,
                     (case p.ISMARR  when 'M' then '�ѻ�' when 'S' then 'δ��' when 'D' then '����' when 'R' then '�ٻ�' when 'A' then '�־�' when 'W' then 'ɥż' else '��' end) ��״,
                     (select i.IN_DATE from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��Ժʱ��,
                     (select i.OUT_DATE from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��Ժʱ��,
                     (select i.WORK_NAME from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ������λ,
                     (select i.WORK_TEL from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) �����绰,
                     (select i.LINKMAN_NAME from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��ϵ��,
                     (select i.LINKMAN_ADD from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��ϵ��ַ,
                     (select i.RELA_CODE from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��ϵ�˹�ϵ,
                     (select di.name from FIN_IPR_INMAININFO i,COM_DICTIONARY di where i.INPATIENT_NO = s.INPATIENT_NO and di.code = i.PROF_CODE and di.type = 'PROFESSION') ְҵ,
                     (select di.name from FIN_IPR_INMAININFO i,COM_DICTIONARY di where i.INPATIENT_NO = s.INPATIENT_NO and di.code = i.BIRTH_AREA and di.TYPE = 'AREA') ������,
                     (case s.ISHIS when 'O' then s.DEPTNO else (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO and rownum = 1) end) �ʹ����,
                     (case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) �ʹ�ҽ��,
                     s.OPERTIME ����ʱ��,
                     s.SENDDATE ��Ѫ����ʱ��,
                     s.OPEREMP ������,
                     s.OPERPOSNAME ��������, 
                     sd.MAIN_DIANAME ���,
                     (case ss.STATUS when '1' then '�ڿ�' when '2' then '���' when '3' then '�ѻ�' when '4' then '����' else '' end) �ڿ�״̬,
                    (select substr(to_char(st.capacity,'0.00') || '*' ||st.SUBCOUNT,1,7) from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 1) Ѫ����װ,
                    (select substr(to_char(st.capacity,'0.00') || '*' ||st.SUBCOUNT,1,7) from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 2) Ѫ���װ,
                    (select substr(to_char(st.capacity,'0.00') || '*' ||st.SUBCOUNT,1,7) from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 3) ��ϸ����װ,
                    nvl((select st.SOTRECOUNT from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 1),0) Ѫ�����,
                    nvl((select st.SUBCOUNT - st.SOTRECOUNT from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 1),0) Ѫ���������,
                    nvl((select st.SOTRECOUNT  from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 2),0) Ѫ����,
                    nvl((select st.SUBCOUNT - st.SOTRECOUNT from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 2),0) Ѫ��������,
                    nvl((select st.SOTRECOUNT  from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 3),0) ��ϸ�����,
                    nvl((select st.SUBCOUNT - st.SOTRECOUNT from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 3),0) ��ϸ���������,
                    s.MARK ��ע
                    from SPEC_SOURCE s
                    join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                    join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID
				    join SPEC_SUBSPEC ss on s.SPECID = ss.SPECID 
                    left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
				    left join SPEC_BOX b on ss.BOXID = b.BOXID";
            //��֯��ϸSQL
            string oSql = @"with ta as
                         (
                         SELECT DISTINCT 
                         s.HISBARCODE Դ����,
                         d.DISEASENAME ����,
                         s.SPEC_NO �걾��,
                         p.pname ����,
                         p.CARD_NO ������,
                         (case p.GENDER when 'F' then 'Ů' else '��' end) �Ա�,
                         p.BIRTHDAY ����,
                        p.CONTACTNUM ��ϵ�绰,
                     p.HOMEPHONENUM ��ͥ�绰,
                     (select di.name from COM_DICTIONARY di where p.NATIONALITY = di.CODE and di.type = 'COUNTRY') ����,
                     (select di.name from COM_DICTIONARY di where p.NATION = di.CODE and di.type = 'NATION') ����,
                     p.IDCARDNO ���֤��,
                     p.HOME ����,
                     p.ADDRESS ��ͥסַ,
                     (case p.ISMARR  when 'M' then '�ѻ�' when 'S' then 'δ��' when 'D' then '����' when 'R' then '�ٻ�' when 'A' then '�־�' when 'W' then 'ɥż' else '��' end) ��״,
                     (select i.IN_DATE from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��Ժʱ��,
                     (select i.OUT_DATE from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��Ժʱ��,
                     (select i.WORK_NAME from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ������λ,
                     (select i.WORK_TEL from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) �����绰,
                     (select i.LINKMAN_NAME from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��ϵ��,
                     (select i.LINKMAN_ADD from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��ϵ��ַ,
                     (select i.RELA_CODE from FIN_IPR_INMAININFO i where i.INPATIENT_NO = s.INPATIENT_NO) ��ϵ�˹�ϵ,
                     (select di.name from FIN_IPR_INMAININFO i,COM_DICTIONARY di where i.INPATIENT_NO = s.INPATIENT_NO and di.code = i.PROF_CODE and di.type = 'PROFESSION') ְҵ,
                     (select di.name from FIN_IPR_INMAININFO i,COM_DICTIONARY di where i.INPATIENT_NO = s.INPATIENT_NO and di.code = i.BIRTH_AREA and di.TYPE = 'AREA') ������,
                         (case s.ISHIS when 'O' then s.DEPTNO else (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO and rownum = 1) end) �ʹ����,
                         (case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) �ʹ�ҽ��,
                         s.OPERTIME ����ʱ��,
                         s.OPEREMP ������,
                         s.MARK ��ע,
                         s.EXT_2 ���,
                         s.SENDDATE ��Ѫ����ʱ��,
                         (select so.BAOMOENTIRE FROM SPEC_SOURCE_STORE so where so.SPECID = ss.SPECID and so.SOTREID = ss.STOREID) ȡ������,
                         ss.SPECTYPEID,                     
                         ss.storeid,
                         ss.specid
                       from SPEC_SUBSPEC ss join  SPEC_SOURCE s on ss.specid = s.specid
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID					 
                       left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
                       left join SPEC_BOX b on ss.BOXID = b.BOXID";
            string rtSQL = string.Empty; 
            if (rbtBld.Checked)
            {
                rtSQL = bSql;
            }
            else
            {
                rtSQL = oSql;
            }
            return rtSQL;
            #endregion
        }

        /// <summary>
        /// ��ϸ��SQL
        /// </summary>
        private string SetDetSql()
        {
            #region
            curSql = GetSql();
            #endregion

            string oper1 = "";
            string oper2 = "";
            if (chkAmg.Checked)
            {
                oper1 = "like";
                oper2 = "%";
            }
            else
            {
                oper1 = "=";
                oper2 = "";
            }
            List<string> conCourceList = new List<string>();
            if (rbtBld.Checked)
            {
                curSql += " where  (s.ORGORBLOOD='B' and ss.SPECID>0 and ss.STOREID>0 and ss.BOXID>0 ) ";
            }
            else
            {
                curSql += " where (s.ORGORBLOOD='O' and ss.SPECID>0 and ss.STOREID>0 and ss.BOXID>0 ) ";
            }

            string sourceCon = "(";
            #region �걾��ѯ
            if (chkSource.Checked)
            {
                if (cmbSpecType.Text.Trim() != "" && cmbSpecType.SelectedValue != null)
                {
                    conCourceList.Add("\n ss.SPECTYPEID = " + cmbSpecType.SelectedValue.ToString() + " ");
                }
                if (cmbDept.Text.Trim() != "")
                {
                    string dept = cmbDept.SelectedValue == null ? "-1" : cmbDept.SelectedValue.ToString();
                    conCourceList.Add("\n (s.DEPTNO oper1 'oper2" + dept + "oper2' OR s.DEPTNO oper1 'oper2" + cmbDept.Text.Trim() + "oper2') ");
                }
                if (cmbDisType.SelectedValue != null && cmbDisType.Text.Trim() != "")
                {
                    conCourceList.Add("\n d.DISEASETYPEID= " + cmbDisType.SelectedValue.ToString() + " ");
                }
                string time = "\n s.OPERTIME BETWEEN to_date('{0}','yyyy-mm-dd hh24:mi:ss') AND to_date('{1}','yyyy-mm-dd hh24:mi:ss') ";
                time = string.Format(time, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1.0).ToString());
                conCourceList.Add(time);
                if (cmbSendDoc.Tag.ToString() != "" || cmbSendDoc.Text.Trim() != "")
                {
                    string doc = cmbSendDoc.Tag == null ? "-1" : cmbSendDoc.Tag.ToString();
                    conCourceList.Add("\n (s.SENDDOCID oper1 'oper2" + cmbSendDoc.Text + "oper2' OR s.SENDDOCID oper1 'oper2" + doc + "oper2') ");
                }

                string tmpOper = rbtAndSource.Checked ? " AND " : " OR ";

                for (int i = 0; i < conCourceList.Count; i++)
                {
                    string s = conCourceList[i];
                    s = s.Replace("oper1", oper1);
                    s = s.Replace("oper2", oper2);
                    sourceCon += s;
                    if (i < conCourceList.Count - 1)
                    {
                        sourceCon += tmpOper;
                    }
                    else
                    {
                        sourceCon += ")";
                    }
                }
            }
            #endregion

            string pCon = "(";

            #region ���ղ���������ѯ
            List<string> patList = new List<string>();
            if (chkPat.Checked)
            {
                string cardNo = txtCardNo.Text.Trim();
                string name = txtName.Text.Trim();
                //string nation = txtNationlity.Text.Trim();
                string sexName = cmbSex.Text.Trim();

                if (cardNo != "")
                {
                    patList.Add("\n p.CARD_NO oper1 'oper2" + cardNo.PadLeft(10, '0') + "oper2' ");
                }
                if (name != "")
                {
                    patList.Add("\n p.pname oper1 'oper2" + name + "oper2' ");
                }
                if (sexName != "")
                {
                    if (sexName == "��")
                    {
                        patList.Add("\n p.GENDER = 'M' ");
                    }
                    else
                    {
                        patList.Add("\n p.GENDER = 'F' ");
                    }
                }
                if (cbxBirthday.Checked)
                {
                    string startBirth = dtpBirthStart.Value.Date.ToString();
                    string endBirth = dtpBirthEnd.Value.Date.AddDays(1.0).ToString();
                    string birth = "\n p.BIRTHDAY BETWEEN '{0}' AND '{1}' ";

                    birth = string.Format(birth, startBirth, endBirth);
                    patList.Add(birth);
                }
                string tmpOper = rbtPAnd.Checked ? " AND " : " OR ";

                for (int i = 0; i < patList.Count; i++)
                {
                    string s = patList[i];
                    s = s.Replace("oper1", oper1);
                    s = s.Replace("oper2", oper2);
                    pCon += s;
                    if (i < patList.Count - 1)
                    {
                        pCon += tmpOper;
                    }
                    else
                    {
                        pCon += ")";
                    }
                }
            }
            #endregion

            string diagCon = "(";

            #region ������ϲ�ѯ
            List<string> diagList = new List<string>();
            if (chkDiag.Checked)
            {
                string diag = cmbDiaMain.Text.TrimStart().TrimEnd(); //;// txtCardNo.Text.Trim();
                string diag1 = cmbDiaMain1.Text.TrimStart().TrimEnd();
                ;// txtName.Text.Trim();
                string diag2 = cmbDiaMain2.Text.TrimStart().TrimEnd();
                ;// txtName.Text.Trim();
                //�������
                string FsDiag = cmbFirstDiag.Text.TrimStart().TrimEnd();

                string diagComment = txtDiaComment.Text.TrimStart().TrimEnd();
                if (diag != "")
                {
                    diagList.Add("\nsd.MAIN_DIANAME oper1 'oper2" + diag + "oper2' ");
                }
                if (diag1 != "")
                {
                    diagList.Add("\nsd.MAIN_DIANAME1 oper1 'oper2" + diag1 + "oper2' ");
                }
                if (diag2 != "")
                {
                    diagList.Add("\nsd.MAIN_DIANAME2 oper1 'oper2" + diag2 + "oper2' ");
                }
                if (FsDiag != "")
                {
                    diagList.Add(" s.EXT_2 oper1 'oper2" + FsDiag + "oper2' ");
                }
                string tmpOper = rbtDAnd.Checked ? " AND " : " OR ";

                for (int i = 0; i < diagList.Count; i++)
                {
                    string s = diagList[i];
                    s = s.Replace("oper1", oper1);
                    s = s.Replace("oper2", oper2);
                    diagCon += s;
                    if (i < diagList.Count - 1)
                    {
                        diagCon += tmpOper;
                    }
                    else
                    {
                        diagCon += ")";
                    }
                }
            }
            #endregion

            string locCon = "(";

            #region ����λ��
            List<string> locList = new List<string>();
            if (chkLoc.Checked)
            {
                if (cmbIceBox.Text.Trim() != "" && cmbIceBox.SelectedValue != null)
                {
                    locList.Add("\n integer(substr(b.BOXBARCODE,3,3)) = " + cmbIceBox.SelectedValue.ToString() + " ");
                }
                if (cmbLayer.Text.Trim() != "")
                {
                    locList.Add("\n integer(substr(b.BOXBARCODE,8,(case length(BOXBARCODE) when 14 then 1 else 2 end))) = " + cmbLayer.Text.Trim() + " ");
                }
                if (cmbShelf.Text.Trim() != "")
                {
                    locList.Add("\n b.DESCAPTYPE = 'J' And integer(substr(b.BOXBARCODE,11,1)) = " + cmbShelf.Text.Trim() + " ");
                }
                if (txtBox1.Text.Trim() != "")
                {
                    locList.Add("\n b.MARK >= '" + txtBox1.Text.TrimEnd().TrimStart() + "' ");
                }

                if (txtBox2.Text.Trim() != "")
                {
                    locList.Add("\n b.MARK <= '" + txtBox2.Text.TrimEnd().TrimStart() + "' ");
                }
                for (int i = 0; i < locList.Count; i++)
                {
                    string s = locList[i];

                    locCon += s;
                    if (i < locList.Count - 1)
                    {
                        locCon += " And ";
                    }
                    else
                    {
                        locCon += ")";
                    }
                }
            }
            #endregion

            if (sourceCon.Length > 3)
            {
                curSql += " lastOperator " + sourceCon;
            }
            if (pCon.Length > 3)
            {
                curSql += " lastOperator " + pCon;
            }
            if (diagCon.Length > 3)
            {
                curSql += " lastOperator " + diagCon;
            }
            if (locCon.Length > 3)
            {
                curSql += " lastOperator " + locCon;
            }

            if (!chk115.Checked && !chk863.Checked)
            {
                curSql += "\n AND (b.SPECUSE = '' or b.SPECUSE is null)";
            }

            if (chk115.Checked)
            {
                curSql += "\n lastOperator b.SPECUSE = '1'";
            }
            if (chk863.Checked)
            {
                curSql += "\n lastOperator b.SPECUSE = '8'";
            }

            if (chkOr.Checked)
            {
                curSql = curSql.Replace("lastOperator", "OR");
            }
            else
            {
                curSql = curSql.Replace("lastOperator", "AND");
            }

            if (txtNo1.Text.Trim() == "" && txtNo2.Text.Trim() == "")
            {
                hasWhere = false;
                if (this.rbtBld.Checked)
                {
                    curSql += "\n ) select * from t ";
                }
                else
                {
                    curSql += @"), 
                    ts as 
					(
   					  select st.SPECID,st.SPECTYPEID,st.BAOMOENTIRE,st.SOTREID,st.TUMORTYPE,sum(st.SUBCOUNT) subcnt,sum(st.SOTRECOUNT)	storecnt									
										from SPEC_SOURCE_STORE st where 
										st.SOTREID in (select storeid from ta)
											 group by  st.SPECID,st.SPECTYPEID,st.SOTREID,st.TUMORTYPE,st.BAOMOENTIRE
											
					),
                    TDS as
					(select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4 AND ts.TUMORTYPE = '1'),
					TRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '1'),
					TPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '1'),
					PDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '3'),
					PRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '3'),
					PPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '3'),
                    NDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '4'),
					NRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '4'),
					NPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '4'),
					SDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '2'),
					SRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '2'),
					SPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '2'),
                    LDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '8'),
					LRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '8'),
					LPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '8'),
                    EDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '5'),
					ERS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '5'),
					EPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '5')
					select distinct ta.SPECID,ta.Դ����,ta. ����,ta.�걾��, ta.����,ta.������, ta.�Ա�,ta.����,ta.����,ta.��ͥסַ,ta.��״,
                     ta.�ʹ����,ta.�ʹ�ҽ��,ta.ȡ������,ta.���,ta.��Ѫ����ʱ��,ta.��ϵ�绰,ta.��ͥ�绰,ta.����,ta.����,ta.���֤��,
                     ta.��Ժʱ��,ta.��Ժʱ��,ta.������λ,ta.�����绰,ta.��ϵ��,ta.��ϵ��ַ,ta.��ϵ�˹�ϵ,ta.ְҵ,ta.������,
                      'DNA' D��,
                      nvl(TDS.storecnt,0) DT,--DNA
                      nvl(PDS.storecnt,0) DP,
                      nvl(NDS.storecnt,0) DN,
                      nvl(LDS.storecnt,0) DL,
                      nvl(EDS.storecnt,0) DE,
                      nvl(SDS.storecnt,0) DS,
                      'RNA' R��,
                      nvl(TRS.storecnt,0) RT,--RNA
                      nvl(PRS.storecnt,0) RP,
                      nvl(NRS.storecnt,0) RN,
                      nvl(LRS.storecnt,0) RL,
                      nvl(ERS.storecnt,0) RE,
                      nvl(SRS.storecnt,0) RS,
                      '����' P��,
                      nvl(TPS.storecnt,0) PT,--����
                      nvl(PPS.storecnt,0) PP,
                      nvl(NPS.storecnt,0) PN,
                      nvl(LPS.storecnt,0) PL,
                      nvl(EPS.storecnt,0) PE,
                      nvl(SPS.storecnt,0) PS,
                      '����' ����,
                      nvl(TDS.subcnt,0) - nvl(TDS.storecnt,0) DTO,--DNA
                      nvl(PDS.subcnt,0) - nvl(PDS.storecnt,0) DPO,
                      nvl(NDS.subcnt,0) - nvl(NDS.storecnt,0) DNO,
                      nvl(LDS.subcnt,0) - nvl(LDS.storecnt,0) DLO,
                      nvl(EDS.subcnt,0) - nvl(EDS.storecnt,0) DEO,
                      nvl(SDS.subcnt,0) - nvl(SDS.storecnt,0) DSO,
                      nvl(TRS.subcnt,0) - nvl(TRS.storecnt,0) RTO,--RNA
                      nvl(PRS.subcnt,0) - nvl(PRS.storecnt,0) RPO,
                      nvl(NRS.subcnt,0) - nvl(NRS.storecnt,0) RNO,
                      nvl(LRS.subcnt,0) - nvl(LRS.storecnt,0) RLO,
                      nvl(ERS.subcnt,0) - nvl(ERS.storecnt,0) REO,
                      nvl(SRS.subcnt,0) - nvl(SRS.storecnt,0) RSO,
                      nvl(TPS.subcnt,0) - nvl(TPS.storecnt,0) PTO,--����
                      nvl(PPS.subcnt,0) - nvl(PPS.storecnt,0) PPO,
                      nvl(NPS.subcnt,0) - nvl(NPS.storecnt,0) PNO,
                      nvl(LPS.subcnt,0) - nvl(LPS.storecnt,0) PLO,
                      nvl(EPS.subcnt,0) - nvl(EPS.storecnt,0) PEO,
                      nvl(SPS.subcnt,0) - nvl(SPS.storecnt,0) PSO, 
                      ta.����ʱ��,ta.������,ta.��ע 
                      from ta left join TDS on ta.SPECID=TDS.SPECID and TDS.BAOMOENTIRE = ta.ȡ������
                      left join TRS on TRS.SPECID=ta.SPECID and TRS.BAOMOENTIRE = ta.ȡ������
					 left join TPS on TPS.SPECID=ta.SPECID and TPS.BAOMOENTIRE = ta.ȡ������
					 left join PDS on PDS.SPECID=ta.SPECID and PDS.BAOMOENTIRE = ta.ȡ������
					 left join PRS on PRS.SPECID=ta.SPECID and PRS.BAOMOENTIRE = ta.ȡ������
					 left join PPS on PPS.SPECID=ta.SPECID and PPS.BAOMOENTIRE = ta.ȡ������
					 left join NDS on NDS.SPECID=ta.SPECID and NDS.BAOMOENTIRE = ta.ȡ������ 
					 left join NRS on NRS.SPECID=ta.SPECID and NRS.BAOMOENTIRE = ta.ȡ������
					 left join NPS on NPS.SPECID=ta.SPECID and NPS.BAOMOENTIRE = ta.ȡ������
                     left join LDS on LDS.SPECID=ta.SPECID and LDS.BAOMOENTIRE = ta.ȡ������
					 left join LRS on LRS.SPECID=ta.SPECID and LRS.BAOMOENTIRE = ta.ȡ������
					 left join LPS on LPS.SPECID=ta.SPECID and LPS.BAOMOENTIRE = ta.ȡ������
                     left join EDS on EDS.SPECID=ta.SPECID and EDS.BAOMOENTIRE = ta.ȡ������
					 left join ERS on ERS.SPECID=ta.SPECID and ERS.BAOMOENTIRE = ta.ȡ������
					 left join EPS on EPS.SPECID=ta.SPECID and EPS.BAOMOENTIRE = ta.ȡ������
                     left join SDS on SDS.SPECID=ta.SPECID and SDS.BAOMOENTIRE = ta.ȡ������
					 left join SRS on SRS.SPECID=ta.SPECID and SRS.BAOMOENTIRE = ta.ȡ������
					 left join SPS on SPS.SPECID=ta.SPECID and SPS.BAOMOENTIRE = ta.ȡ������";
                    if (cmbQCZQ.Text.Trim() != "")
                    {
                        curSql += " where ta.ȡ������ ='" + cmbQCZQ.Text.Trim() + "'";
                        hasWhere = true;
                    }
                }
            }

            else
            {
                hasWhere = true;
                curSql += "\n )";
                int num1 = 0;
                int num2 = 999999;
                if (txtNo1.Text.Trim() != "")
                {
                    try
                    {
                        num1 = Convert.ToInt32(txtNo1.Text.Trim());
                    }
                    catch
                    { }
                }

                if (txtNo2.Text.Trim() != "")
                {
                    try
                    {
                        num2 = Convert.ToInt32(txtNo2.Text.Trim());
                    }
                    catch
                    { }
                }
                if (this.rbtBld.Checked)
                {
                    curSql += " \n select * from t where int(�걾��) between " + num1.ToString() + " and " + num2.ToString();
                }
                else
                {
                    string curTmp = @", 
                    ts as 
					(
   					  select st.SPECID,st.SPECTYPEID,st.BAOMOENTIRE,st.SOTREID,st.TUMORTYPE,sum(st.SUBCOUNT) subcnt,sum(st.SOTRECOUNT)	storecnt									
										from SPEC_SOURCE_STORE st where 
										st.SOTREID in (select storeid from ta)
											 group by  st.SPECID,st.SPECTYPEID,st.SOTREID,st.TUMORTYPE,st.BAOMOENTIRE
											
					),
                    TDS as
					(select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4 AND ts.TUMORTYPE = '1'),
					TRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '1'),
					TPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '1'),
					PDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '3'),
					PRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '3'),
					PPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '3'),
                    NDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '4'),
					NRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '4'),
					NPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '4'),
					SDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '2'),
					SRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '2'),
					SPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '2'),
                    LDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '8'),
					LRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '8'),
					LPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '8'),
                    EDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '5'),
					ERS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '5'),
					EPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '5')
					select distinct ta.SPECID,ta.Դ����,ta. ����,ta.�걾��, ta.����,ta.������, ta.�Ա�,ta.����,ta.����,ta.��ͥסַ,ta.��״,
                     ta.�ʹ����,ta.�ʹ�ҽ��,ta.ȡ������,ta.���,ta.��Ѫ����ʱ��,ta.��ϵ�绰,ta.��ͥ�绰,ta.����,ta.����,ta.���֤��,
                     ta.��Ժʱ��,ta.��Ժʱ��,ta.������λ,ta.�����绰,ta.��ϵ��,ta.��ϵ��ַ,ta.��ϵ�˹�ϵ,ta.ְҵ,ta.������,
                      'DNA' D��,
                      nvl(TDS.storecnt,0) DT,--DNA
                      nvl(PDS.storecnt,0) DP,
                      nvl(NDS.storecnt,0) DN,
                      nvl(LDS.storecnt,0) DL,
                      nvl(EDS.storecnt,0) DE,
                      nvl(SDS.storecnt,0) DS,
                      'RNA' R��,
                      nvl(TRS.storecnt,0) RT,--RNA
                      nvl(PRS.storecnt,0) RP,
                      nvl(NRS.storecnt,0) RN,
                      nvl(LRS.storecnt,0) RL,
                      nvl(ERS.storecnt,0) RE,
                      nvl(SRS.storecnt,0) RS,
                      '����' P��,
                      nvl(TPS.storecnt,0) PT,--����
                      nvl(PPS.storecnt,0) PP,
                      nvl(NPS.storecnt,0) PN,
                      nvl(LPS.storecnt,0) PL,
                      nvl(EPS.storecnt,0) PE,
                      nvl(SPS.storecnt,0) PS,
                      '����' ����,
                      nvl(TDS.subcnt,0) - nvl(TDS.storecnt,0) DTO,--DNA
                      nvl(PDS.subcnt,0) - nvl(PDS.storecnt,0) DPO,
                      nvl(NDS.subcnt,0) - nvl(NDS.storecnt,0) DNO,
                      nvl(LDS.subcnt,0) - nvl(LDS.storecnt,0) DLO,
                      nvl(EDS.subcnt,0) - nvl(EDS.storecnt,0) DEO,
                      nvl(SDS.subcnt,0) - nvl(SDS.storecnt,0) DSO,
                      nvl(TRS.subcnt,0) - nvl(TRS.storecnt,0) RTO,--RNA
                      nvl(PRS.subcnt,0) - nvl(PRS.storecnt,0) RPO,
                      nvl(NRS.subcnt,0) - nvl(NRS.storecnt,0) RNO,
                      nvl(LRS.subcnt,0) - nvl(LRS.storecnt,0) RLO,
                      nvl(ERS.subcnt,0) - nvl(ERS.storecnt,0) REO,
                      nvl(SRS.subcnt,0) - nvl(SRS.storecnt,0) RSO,
                      nvl(TPS.subcnt,0) - nvl(TPS.storecnt,0) PTO,--����
                      nvl(PPS.subcnt,0) - nvl(PPS.storecnt,0) PPO,
                      nvl(NPS.subcnt,0) - nvl(NPS.storecnt,0) PNO,
                      nvl(LPS.subcnt,0) - nvl(LPS.storecnt,0) PLO,
                      nvl(EPS.subcnt,0) - nvl(EPS.storecnt,0) PEO,
                      nvl(SPS.subcnt,0) - nvl(SPS.storecnt,0) PSO, 
                      ta.����ʱ��,ta.������,ta.��ע 
                      from ta left join TDS on ta.SPECID=TDS.SPECID and TDS.BAOMOENTIRE = ta.ȡ������
                      left join TRS on TRS.SPECID=ta.SPECID and TRS.BAOMOENTIRE = ta.ȡ������
					 left join TPS on TPS.SPECID=ta.SPECID and TPS.BAOMOENTIRE = ta.ȡ������
					 left join PDS on PDS.SPECID=ta.SPECID and PDS.BAOMOENTIRE = ta.ȡ������
					 left join PRS on PRS.SPECID=ta.SPECID and PRS.BAOMOENTIRE = ta.ȡ������
					 left join PPS on PPS.SPECID=ta.SPECID and PPS.BAOMOENTIRE = ta.ȡ������
					 left join NDS on NDS.SPECID=ta.SPECID and NDS.BAOMOENTIRE = ta.ȡ������ 
					 left join NRS on NRS.SPECID=ta.SPECID and NRS.BAOMOENTIRE = ta.ȡ������
					 left join NPS on NPS.SPECID=ta.SPECID and NPS.BAOMOENTIRE = ta.ȡ������
                     left join LDS on LDS.SPECID=ta.SPECID and LDS.BAOMOENTIRE = ta.ȡ������
					 left join LRS on LRS.SPECID=ta.SPECID and LRS.BAOMOENTIRE = ta.ȡ������
					 left join LPS on LPS.SPECID=ta.SPECID and LPS.BAOMOENTIRE = ta.ȡ������
                     left join EDS on EDS.SPECID=ta.SPECID and EDS.BAOMOENTIRE = ta.ȡ������
					 left join ERS on ERS.SPECID=ta.SPECID and ERS.BAOMOENTIRE = ta.ȡ������
					 left join EPS on EPS.SPECID=ta.SPECID and EPS.BAOMOENTIRE = ta.ȡ������
                     left join SDS on SDS.SPECID=ta.SPECID and SDS.BAOMOENTIRE = ta.ȡ������
					 left join SRS on SRS.SPECID=ta.SPECID and SRS.BAOMOENTIRE = ta.ȡ������
					 left join SPS on SPS.SPECID=ta.SPECID and SPS.BAOMOENTIRE = ta.ȡ������
                     where int(ta.�걾��) between {0} and {1}";
                    curTmp = string.Format(curTmp, num1.ToString(), num2.ToString());
                    curSql += curTmp;
                    if (cmbQCZQ.Text.Trim() != "")
                    {
                        curSql += " and ta.ȡ������ ='" + cmbQCZQ.Text.Trim() + "'";
                    }
                }
            }
            return curSql;
        }

        /// <summary>
        /// ���ܵ�SQL
        /// </summary>
        private string GetSumSql(int tabIndex)
        {
            string tmpSpecs = "";
            if (tabIndex == 2)
            {
                string tmpSpecId = string.Empty;
                if (neuSpread1_Sheet1.RowCount > 0)
                {
                    for (int k = 0; k < neuSpread1_Sheet1.RowCount; k++)
                    {
                        if (k == neuSpread1_Sheet1.RowCount - 1)
                        {
                            tmpSpecId += neuSpread1_Sheet1.Cells[k, Convert.ToInt32(ColFs.SPECID)].Text.Trim() + ")";
                        }
                        else
                        {
                            tmpSpecId += neuSpread1_Sheet1.Cells[k, Convert.ToInt32(ColFs.SPECID)].Text.Trim() + ",";
                        }
                    }
                }
                tmpSpecs = tmpSpecId;
            }
            else
            {
                tmpSpecs = specIds;
            }
            string tsql = "";
            #region��SQL
            tsql = @"with   ta as
 					(
                    select distinct s.SPEC_NO c,
				    ss.SPECID,p.card_no, p.pname, ss.storeid, d.DISEASENAME, SPECTYPEID, s.INPATIENT_NO
					from SPEC_SUBSPEC ss join spec_source s on ss.specid = s.specid
					left join SPEC_DISEASETYPE d on s.DISEASETYPEID = d.DISEASETYPEID
                    left join spec_patient p on s.patientid = p.patientid
					where s.OPERTIME BETWEEN to_date('{0}','yyyy-mm-dd hh24:mi:ss') AND to_date('{1}','yyyy-mm-dd hh24:mi:ss')	and ss.SPECID>0 and ss.STOREID>0 and ss.BOXID>0
					and s.ORGORBLOOD = '{2}'";

            tsql += " and ss.SPECID in (" + tmpSpecs + "),";

            tsql += @"	ts as 
					(
   					  select st.SPECID,st.SPECTYPEID,st.SOTREID, sum(st.SUBCOUNT) subcnt										
										from SPEC_SOURCE_STORE st where -- st.STORETIME	 BETWEEN '2010-10-18 00:00:00' AND '2010-11-19 00:00:00'
										--and 
										st.SOTREID in (select storeid from ta)
											 group by  st.SPECID,st.SPECTYPEID,st.SOTREID
											
					),

					xj as
					(select ts.SPECID, ts.subcnt from ts  where  ts.SPECTYPEID = 1),
					xq as(
					select ts.SPECID, ts.subcnt from ts  where  ts.SPECTYPEID = 2),
					xb as(
					select ts.SPECID, ts.subcnt from ts  where  ts.SPECTYPEID = 3),
					dna as(
					select ts.SPECID, ts.subcnt from ts  where  ts.SPECTYPEID = 4),
					RNA as(
					select ts.SPECID, ts.subcnt from ts  where  ts.SPECTYPEID = 5),
					lk as(
					select ts.SPECID, ts.subcnt from ts  where  ts.SPECTYPEID = 7),
					ma as
					(
 					select distinct s.specid,d.DISEASENAME,s.INPATIENT_NO
 					--(case length(ss.SUBBARCODE) when 9 then substr(ss.subbarcode,2,6) else substr(ss.subbarcode,1,6) end) c
 					from spec_source s left join  SPEC_DISEASETYPE d on  s.DISEASETYPEID = d.DISEASETYPEID where s.INPATIENT_NO in
 					(
  					select ta.INPATIENT_NO
						from ta 
						where ta.INPATIENT_NO<>''
					 )and s.ORGORBLOOD = '{3}'
					),
					mas as
					(
      					select distinct s.SPEC_NO c,s.specid
								from SPEC_SOURCE s
								where s.specid in
								(
								 select specid
								 from ma
								)				 
					)
					select distinct ta.inpatient_no סԺ��ˮ��, ta.card_no ������,ta.name ����, ta.c �걾��, ta.DISEASENAME ����,ta.specid,xj.subcnt Ѫ��,xq.subcnt Ѫ�� ,xb.subcnt ϸ��,
					dna.subcnt DNA ,rna.subcnt RNA,lk.subcnt ����, ma.DISEASENAME ��Բ���,mas.c ��Ա걾��,ma.specid MSSPECID

					from ta left join xj on ta.SPECID=xj.SPECID left join xq on xq.SPECID=ta.SPECID 
					 left join xb on xb.SPECID=ta.SPECID 
					 left join dna on dna.SPECID=ta.SPECID 
					 left join rna on rna.SPECID=ta.SPECID 
					 left join lk on lk.SPECID=ta.SPECID 
					 join ma on ta.inpatient_no = ma.inpatient_no
					 join mas on ma.specid = mas.specid ";
            return tsql;
            #endregion
        }

        /// <summary>
        /// �걾�����Ϣ
        /// </summary>
        private void GetSumData()
        {
            string tmpSql = GetSumSql(2);
            if (rbtBld.Checked)
            {
                tmpSql = string.Format(tmpSql, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1).ToString(), "B", "O");

            }
            else
            {
                tmpSql = string.Format(tmpSql, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1).ToString(), "O", "B");
            }

            DataSet ds = new DataSet();
            subMgr.ExecQuery(tmpSql, ref ds);
            neuSpread2_Sheet1.RowCount = 0;
            neuSpread2_Sheet1.AutoGenerateColumns = false;
            neuSpread2_Sheet1.DataAutoSizeColumns = false;
            neuSpread2_Sheet1.DataSource = ds;

            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.סԺ��ˮ��), "סԺ��ˮ��");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.�걾��), "�걾��");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.������), "������");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.����), "����");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.����), "����");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.SPECID), "SPECID");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.Ѫ��), "Ѫ��");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.Ѫ��), "Ѫ��");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.ϸ��), "ϸ��");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.DNA), "DNA");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.RNA), "RNA");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.����), "����");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.��Բ���), "��Բ���");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.��Ա걾��), "��Ա걾��");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.MSSPECID), "MSSPECID");

            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.SPECID)].Visible = false;//.Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.MSSPECID)].Visible = false;//.Width = 100;

            if (rbtBld.Checked)
            {

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.DNA)].Visible = false;//.Width = 100;

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.RNA)].Visible = false;//.Width = 100;

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.����)].Visible = false;//.Width = 100;
            }

            else
            {
                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.Ѫ��)].Visible = false;// Width = 100;

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.Ѫ��)].Visible = false;//.Width = 100;

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.ϸ��)].Visible = false;//.Width = 100; 
            }

            tpgPair.Text = "�걾�����Ϣ (��: " + neuSpread2_Sheet1.RowCount.ToString() + " ��)";

        }

        /// <summary>
        /// �ӻ�ȡExcel�еĲ����ż����γ�sql���
        /// </summary>
        /// <returns></returns>
        private string GetCardCol()
        {
            ExlToDb2Manage exlMgr = new ExlToDb2Manage();
            DataSet ds = new DataSet();
            exlMgr.ExlConnect(txtCardNoExl.Text, ref ds);
            if (ds == null)
            {
                MessageBox.Show("��ȡ�ļ�ʧ��");
            }
            string cardCols = string.Empty;
            if (hasWhere)
            {
                cardCols = " \n and ������ in (";
            }
            else
            {
                cardCols = " \n where ������ in (";
            }
            int index = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    if (index == ds.Tables[0].Rows.Count - 1)
                    {
                        index++;
                        cardCols += "'" + dr[0].ToString().Trim().PadLeft(10, '0') + "'" + ")\n";
                    }
                    else
                    {
                        index++;
                        cardCols += "'" + dr[0].ToString().Trim().PadLeft(10, '0') + "'" + ",\n";
                    }
                }
                catch
                {
                    index++;
                    continue;
                }
            }
            if (hasWhere)
            {
                if (cardCols.Trim().Contains("()"))
                {
                    cardCols = "and ������ in ('')";
                }
            }
            else
            {
                if (cardCols.Trim().Contains("()"))
                {
                    cardCols = "where ������ in ('')";
                }
            }
            
            return SetDetSql() + cardCols;
           
        }

        /// <summary>
        /// �ӻ�ȡExcel�еı걾�ż����γ�sql���
        /// </summary>
        /// <returns></returns>
        private string GetSpecCol()
        {
            chkSource.Checked = true;
            string specNoSql = SetDetSql();
            ExlToDb2Manage exlMgr = new ExlToDb2Manage();
            DataSet ds = new DataSet();
            exlMgr.ExlConnect(txtSpecNoExl.Text, ref ds);
            if (ds == null)
            {
                MessageBox.Show("��ȡ�ļ�ʧ��");
            }

            int index = 0;
            //, t as ( select * from s where substr(�걾��,1,1)>='0' and substr(�걾��,1,1)<='9')
           // string specCols = ") ";
           
           string specCols = " where �걾�� in( ";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                try
                {
                    if (specCols.Trim() == "")
                    {
                        index++;
                        continue;
                    }
                    if (index == ds.Tables[0].Rows.Count - 1)
                    {
                        index++;
                        specCols += "'" + dr[0].ToString().Trim().PadLeft(6,'0') + "')\n";
                    }
                    else
                    {
                        index++;
                        specCols += "'" + dr[0].ToString().Trim().PadLeft(6,'0') + "',\n";
                    }
                }
                catch
                {
                    index++;
                    continue;
                }
            }
            return specNoSql + specCols;
        }

        /// <summary>
        /// �ڶ���ѯ����漰���������ѯ
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private string GetStockSql(int tabIndex)
        {
            string tsql = "";
            //Ѫ
            if (tabIndex == 0)
            {
                tsql = @"with t as
                     (SELECT DISTINCT
                     s.SPECID, 
                     s.HISBARCODE Դ����,
                     d.DISEASENAME ����,
                     s.SPEC_NO �걾��,
                     p.pname ����, 
                     p.CARD_NO ������,
                     (case p.GENDER when 'F' then 'Ů' else '��' end) �Ա�,
                    nvl((select st.SOTRECOUNT from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 1),0) Ѫ�����,
                    nvl((select st.SUBCOUNT - st.SOTRECOUNT from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 1),0) Ѫ���������,
                    nvl((select st.SOTRECOUNT  from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 2),0) Ѫ����,
                    nvl((select st.SUBCOUNT - st.SOTRECOUNT from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 2),0) Ѫ��������,
                    nvl((select st.SOTRECOUNT  from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 3),0) ��ϸ�����,
                    nvl((select st.SUBCOUNT - st.SOTRECOUNT from SPEC_SOURCE_STORE st where s.SPECID = st.SPECID and st.SPECTYPEID = 3),0) ��ϸ���������,
                    s.MARK ��ע
                    from SPEC_SOURCE s
                    join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                    join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID
				    join SPEC_SUBSPEC ss on s.SPECID = ss.SPECID 
                    left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
				    left join SPEC_BOX b on ss.BOXID = b.BOXID";
            }
            else //��֯
            {

            }
            
            return tsql;
        }

        /// <summary>
        /// �ӻ��ܵı���д����γɵ�SQL���
        /// </summary>
        /// <returns></returns>
        private string GetSqlFromSum()
        {
            string tmp = GetSql();
            //string specId1 = neuSpread2_Sheet1.Cells[neuSpread2_Sheet1.ActiveRowIndex, Convert.ToInt32(Cols.SPECID)].Text.Trim();
            //string specId2 = neuSpread2_Sheet1.Cells[neuSpread2_Sheet1.ActiveRowIndex, Convert.ToInt32(Cols.MSSPECID)].Text.Trim();
            //if (specId1 == "")
            //{
            //    specId1 = "0";
            //}
            //if (specId2 == "")
            //{
            //    specId2 = "0";
            //}

            //tmp += " where s.specid in (" + specId1 + "," + specId2 + ")\n ) select * from s";
            return tmp;
        }

        /// <summary>
        /// �ӿ��ı���д����γɵ�SQL���
        /// </summary>
        /// <returns></returns>
        private string GetSqlFromStock()
        {
            string curSql = string.Empty;
            #region
            if (rbtOrg.Checked)
            {
                curSql = GetStockSql(1);
            }
            else if (rbtBld.Checked)
            {
                curSql = GetStockSql(0);
            }
            #endregion

            string oper1 = "";
            string oper2 = "";
            if (chkAmg.Checked)
            {
                oper1 = "like";
                oper2 = "%";
            }
            else
            {
                oper1 = "=";
                oper2 = "";
            }
            List<string> conCourceList = new List<string>();
            if (rbtBld.Checked)
            {
                curSql += " where  (s.ORGORBLOOD='B' and ss.SPECID>0 and ss.STOREID>0 and ss.BOXID>0 ) ";
            }
            else
            {
                curSql += " where (s.ORGORBLOOD='O' and ss.SPECID>0 and ss.STOREID>0 and ss.BOXID>0 ) ";
            }

            string sourceCon = "(";
            #region �걾��ѯ
            if (chkSource.Checked)
            {
                if (cmbSpecType.Text.Trim() != "" && cmbSpecType.SelectedValue != null)
                {
                    conCourceList.Add("\n t.SPECIMENTTYPEID = " + cmbSpecType.SelectedValue.ToString() + " ");
                }
                if (cmbDept.Text.Trim() != "")
                {
                    string dept = cmbDept.SelectedValue == null ? "-1" : cmbDept.SelectedValue.ToString();
                    conCourceList.Add("\n (s.DEPTNO oper1 'oper2" + dept + "oper2' OR s.DEPTNO oper1 'oper2" + cmbDept.Text.Trim() + "oper2') ");
                }
                if (cmbDisType.SelectedValue != null && cmbDisType.Text.Trim() != "")
                {
                    conCourceList.Add("\n d.DISEASETYPEID= " + cmbDisType.SelectedValue.ToString() + " ");
                }
                string time = "\n s.OPERTIME BETWEEN to_date('{0}','yyyy-mm-dd hh24:mi:ss') AND to_date('{1}','yyyy-mm-dd hh24:mi:ss') ";
                time = string.Format(time, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1.0).ToString());
                conCourceList.Add(time);
                if (cmbSendDoc.Tag.ToString() != "" || cmbSendDoc.Text.Trim() != "")
                {
                    string doc = cmbSendDoc.Tag == null ? "-1" : cmbSendDoc.Tag.ToString();
                    conCourceList.Add("\n (s.SENDDOCID oper1 'oper2" + cmbSendDoc.Text + "oper2' OR s.SENDDOCID oper1 'oper2" + doc + "oper2') ");
                }

                string tmpOper = rbtAndSource.Checked ? " AND " : " OR ";

                for (int i = 0; i < conCourceList.Count; i++)
                {
                    string s = conCourceList[i];
                    s = s.Replace("oper1", oper1);
                    s = s.Replace("oper2", oper2);
                    sourceCon += s;
                    if (i < conCourceList.Count - 1)
                    {
                        sourceCon += tmpOper;
                    }
                    else
                    {
                        sourceCon += ")";
                    }
                }
            }
            #endregion

            string pCon = "(";

            #region ���ղ���������ѯ
            List<string> patList = new List<string>();
            if (chkPat.Checked)
            {
                string cardNo = txtCardNo.Text.Trim();
                string name = txtName.Text.Trim();
                //string nation = txtNationlity.Text.Trim();
                string sexName = cmbSex.Text.Trim();

                if (cardNo != "")
                {
                    patList.Add("\n p.CARD_NO oper1 'oper2" + cardNo.PadLeft(10, '0') + "oper2' ");
                }
                if (name != "")
                {
                    patList.Add("\n p.pname oper1 'oper2" + name + "oper2' ");
                }
                if (sexName != "")
                {
                    if (sexName == "��")
                    {
                        patList.Add("\n p.GENDER = 'M' ");
                    }
                    else
                    {
                        patList.Add("\n p.GENDER = 'F' ");
                    }
                }
                if (cbxBirthday.Checked)
                {
                    string startBirth = dtpBirthStart.Value.Date.ToString();
                    string endBirth = dtpBirthEnd.Value.Date.AddDays(1.0).ToString();
                    string birth = "\n p.BIRTHDAY BETWEEN '{0}' AND '{1}' ";

                    birth = string.Format(birth, startBirth, endBirth);
                    patList.Add(birth);
                }
                string tmpOper = rbtPAnd.Checked ? " AND " : " OR ";

                for (int i = 0; i < patList.Count; i++)
                {
                    string s = patList[i];
                    s = s.Replace("oper1", oper1);
                    s = s.Replace("oper2", oper2);
                    pCon += s;
                    if (i < patList.Count - 1)
                    {
                        pCon += tmpOper;
                    }
                    else
                    {
                        pCon += ")";
                    }
                }
            }
            #endregion

            string diagCon = "(";

            #region ������ϲ�ѯ
            List<string> diagList = new List<string>();
            if (chkDiag.Checked)
            {
                string diag = cmbDiaMain.Text.TrimStart().TrimEnd(); //;// txtCardNo.Text.Trim();
                string diag1 = cmbDiaMain1.Text.TrimStart().TrimEnd();
                ;// txtName.Text.Trim();
                string diag2 = cmbDiaMain2.Text.TrimStart().TrimEnd();
                ;// txtName.Text.Trim();
                //�������
                string FsDiag = cmbFirstDiag.Text.TrimStart().TrimEnd();

                string diagComment = txtDiaComment.Text.TrimStart().TrimEnd();
                if (diag != "")
                {
                    diagList.Add("\nsd.MAIN_DIANAME oper1 'oper2" + diag + "oper2' ");
                }
                if (diag1 != "")
                {
                    diagList.Add("\nsd.MAIN_DIANAME1 oper1 'oper2" + diag1 + "oper2' ");
                }
                if (diag2 != "")
                {
                    diagList.Add("\nsd.MAIN_DIANAME2 oper1 'oper2" + diag2 + "oper2' ");
                }
                if (FsDiag != "")
                {
                    diagList.Add(" s.EXT_2 oper1 'oper2" + FsDiag + "oper2' ");
                }
                string tmpOper = rbtDAnd.Checked ? " AND " : " OR ";

                for (int i = 0; i < diagList.Count; i++)
                {
                    string s = diagList[i];
                    s = s.Replace("oper1", oper1);
                    s = s.Replace("oper2", oper2);
                    diagCon += s;
                    if (i < diagList.Count - 1)
                    {
                        diagCon += tmpOper;
                    }
                    else
                    {
                        diagCon += ")";
                    }
                }
            }
            #endregion

            string locCon = "(";

            #region ����λ��
            List<string> locList = new List<string>();
            if (chkLoc.Checked)
            {
                if (cmbIceBox.Text.Trim() != "" && cmbIceBox.SelectedValue != null)
                {
                    locList.Add("\n integer(substr(b.BOXBARCODE,3,3)) = " + cmbIceBox.SelectedValue.ToString() + " ");
                }
                if (cmbLayer.Text.Trim() != "")
                {
                    locList.Add("\n integer(substr(b.BOXBARCODE,8,(case length(BOXBARCODE) when 14 then 1 else 2 end))) = " + cmbLayer.Text.Trim() + " ");
                }
                if (cmbShelf.Text.Trim() != "")
                {
                    locList.Add("\n b.DESCAPTYPE = 'J' And integer(substr(b.BOXBARCODE,11,1)) = " + cmbShelf.Text.Trim() + " ");
                }
                if (txtBox1.Text.Trim() != "")
                {
                    locList.Add("\n b.MARK >= '" + txtBox1.Text.TrimEnd().TrimStart() + "' ");
                }

                if (txtBox2.Text.Trim() != "")
                {
                    locList.Add("\n b.MARK <= '" + txtBox2.Text.TrimEnd().TrimStart() + "' ");
                }
                for (int i = 0; i < locList.Count; i++)
                {
                    string s = locList[i];

                    locCon += s;
                    if (i < locList.Count - 1)
                    {
                        locCon += " And ";
                    }
                    else
                    {
                        locCon += ")";
                    }
                }
            }
            #endregion

            if (sourceCon.Length > 3)
            {
                curSql += " lastOperator " + sourceCon;
            }
            if (pCon.Length > 3)
            {
                curSql += " lastOperator " + pCon;
            }
            if (diagCon.Length > 3)
            {
                curSql += " lastOperator " + diagCon;
            }
            if (locCon.Length > 3)
            {
                curSql += " lastOperator " + locCon;
            }

            if (!chk115.Checked && !chk863.Checked)
            {
                curSql += "\n AND (b.SPECUSE = '' or b.SPECUSE is null)";
            }

            if (chk115.Checked)
            {
                curSql += "\n lastOperator b.SPECUSE = '1'";
            }
            if (chk863.Checked)
            {
                curSql += "\n lastOperator b.SPECUSE = '8'";
            }

            if (chkOr.Checked)
            {
                curSql = curSql.Replace("lastOperator", "OR");
            }
            else
            {
                curSql = curSql.Replace("lastOperator", "AND");
            }

            if (txtNo1.Text.Trim() == "" && txtNo2.Text.Trim() == "")
            {
                hasWhere = false;
                if (this.rbtBld.Checked)
                {
                    curSql += "\n ) select * from t ";
                }
                else
                {
                    curSql += @"), 
                    ts as 
					(
   					  select st.SPECID,st.SPECTYPEID,st.BAOMOENTIRE,st.SOTREID,st.TUMORTYPE,sum(st.SUBCOUNT) subcnt,sum(st.SOTRECOUNT)	storecnt									
										from SPEC_SOURCE_STORE st where 
										st.SOTREID in (select storeid from ta)
											 group by  st.SPECID,st.SPECTYPEID,st.SOTREID,st.TUMORTYPE,st.BAOMOENTIRE
											
					),
                    TDS as
					(select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4 AND ts.TUMORTYPE = '1'),
					TRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '1'),
					TPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '1'),
					PDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '3'),
					PRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '3'),
					PPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '3'),
                    NDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '4'),
					NRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '4'),
					NPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '4'),
					SDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '2'),
					SRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '2'),
					SPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '2'),
                    LDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '8'),
					LRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '8'),
					LPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '8'),
                    EDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '5'),
					ERS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '5'),
					EPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '5')
					select distinct ta.SPECID,ta.Դ����,ta. ����,ta.�걾��, ta.����,ta.������, ta.�Ա�,ta.����,ta.����,ta.��ͥסַ,ta.��״,
                     ta.�ʹ����,ta.�ʹ�ҽ��,ta.ȡ������,ta.���,ta.��Ѫ����ʱ��,ta.��ϵ�绰,ta.��ͥ�绰,ta.����,ta.����,ta.���֤��,
                     ta.��Ժʱ��,ta.��Ժʱ��,ta.������λ,ta.�����绰,ta.��ϵ��,ta.��ϵ��ַ,ta.��ϵ�˹�ϵ,ta.ְҵ,ta.������,
                      'DNA' D��,
                      nvl(TDS.storecnt,0) DT,--DNA
                      nvl(PDS.storecnt,0) DP,
                      nvl(NDS.storecnt,0) DN,
                      nvl(LDS.storecnt,0) DL,
                      nvl(EDS.storecnt,0) DE,
                      nvl(SDS.storecnt,0) DS,
                      'RNA' R��,
                      nvl(TRS.storecnt,0) RT,--RNA
                      nvl(PRS.storecnt,0) RP,
                      nvl(NRS.storecnt,0) RN,
                      nvl(LRS.storecnt,0) RL,
                      nvl(ERS.storecnt,0) RE,
                      nvl(SRS.storecnt,0) RS,
                      '����' P��,
                      nvl(TPS.storecnt,0) PT,--����
                      nvl(PPS.storecnt,0) PP,
                      nvl(NPS.storecnt,0) PN,
                      nvl(LPS.storecnt,0) PL,
                      nvl(EPS.storecnt,0) PE,
                      nvl(SPS.storecnt,0) PS, 
                      ta.����ʱ��,ta.������,ta.��ע 
                      from ta left join TDS on ta.SPECID=TDS.SPECID and TDS.BAOMOENTIRE = ta.ȡ������
                      left join TRS on TRS.SPECID=ta.SPECID and TRS.BAOMOENTIRE = ta.ȡ������
					 left join TPS on TPS.SPECID=ta.SPECID and TPS.BAOMOENTIRE = ta.ȡ������
					 left join PDS on PDS.SPECID=ta.SPECID and PDS.BAOMOENTIRE = ta.ȡ������
					 left join PRS on PRS.SPECID=ta.SPECID and PRS.BAOMOENTIRE = ta.ȡ������
					 left join PPS on PPS.SPECID=ta.SPECID and PPS.BAOMOENTIRE = ta.ȡ������
					 left join NDS on NDS.SPECID=ta.SPECID and NDS.BAOMOENTIRE = ta.ȡ������ 
					 left join NRS on NRS.SPECID=ta.SPECID and NRS.BAOMOENTIRE = ta.ȡ������
					 left join NPS on NPS.SPECID=ta.SPECID and NPS.BAOMOENTIRE = ta.ȡ������
                     left join LDS on LDS.SPECID=ta.SPECID and LDS.BAOMOENTIRE = ta.ȡ������
					 left join LRS on LRS.SPECID=ta.SPECID and LRS.BAOMOENTIRE = ta.ȡ������
					 left join LPS on LPS.SPECID=ta.SPECID and LPS.BAOMOENTIRE = ta.ȡ������
                     left join EDS on EDS.SPECID=ta.SPECID and EDS.BAOMOENTIRE = ta.ȡ������
					 left join ERS on ERS.SPECID=ta.SPECID and ERS.BAOMOENTIRE = ta.ȡ������
					 left join EPS on EPS.SPECID=ta.SPECID and EPS.BAOMOENTIRE = ta.ȡ������
                     left join SDS on SDS.SPECID=ta.SPECID and SDS.BAOMOENTIRE = ta.ȡ������
					 left join SRS on SRS.SPECID=ta.SPECID and SRS.BAOMOENTIRE = ta.ȡ������
					 left join SPS on SPS.SPECID=ta.SPECID and SPS.BAOMOENTIRE = ta.ȡ������";
                    if (cmbQCZQ.Text.Trim() != "")
                    {
                        curSql += " where ta.ȡ������ ='" + cmbQCZQ.Text.Trim() + "'";
                        hasWhere = true;
                    }
                }
            }

            else
            {
                hasWhere = true;
                curSql += "\n )";
                int num1 = 0;
                int num2 = 999999;
                if (txtNo1.Text.Trim() != "")
                {
                    try
                    {
                        num1 = Convert.ToInt32(txtNo1.Text.Trim());
                    }
                    catch
                    {
                    }
                }

                if (txtNo2.Text.Trim() != "")
                {
                    try
                    {
                        num2 = Convert.ToInt32(txtNo2.Text.Trim());
                    }
                    catch
                    {
                    }
                }
                if (this.rbtBld.Checked)
                {
                    curSql += " \n select * from t where int(�걾��) between " + num1.ToString() + " and " + num2.ToString();
                }
                else
                {
                    string curTmp = @", 
                    ts as 
					(
   					  select st.SPECID,st.SPECTYPEID,st.BAOMOENTIRE,st.SOTREID,st.TUMORTYPE,sum(st.SUBCOUNT) subcnt,sum(st.SOTRECOUNT)	storecnt									
										from SPEC_SOURCE_STORE st where 
										st.SOTREID in (select storeid from ta)
											 group by  st.SPECID,st.SPECTYPEID,st.SOTREID,st.TUMORTYPE,st.BAOMOENTIRE
											
					),
                    TDS as
					(select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4 AND ts.TUMORTYPE = '1'),
					TRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '1'),
					TPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '1'),
					PDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '3'),
					PRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '3'),
					PPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '3'),
                    NDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '4'),
					NRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '4'),
					NPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '4'),
					SDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '2'),
					SRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '2'),
					SPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '2'),
                    LDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '8'),
					LRS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '8'),
					LPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '8'),
                    EDS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4  AND ts.TUMORTYPE = '5'),
					ERS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5  AND ts.TUMORTYPE = '5'),
					EPS as(
					select ts.SPECID,ts.BAOMOENTIRE, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7  AND ts.TUMORTYPE = '5')
					select distinct ta.SPECID,ta.Դ����,ta. ����,ta.�걾��, ta.����,ta.������, ta.�Ա�,ta.����,ta.����,ta.��ͥסַ,ta.��״,
                     ta.�ʹ����,ta.�ʹ�ҽ��,ta.ȡ������,ta.���,ta.��Ѫ����ʱ��,ta.��ϵ�绰,ta.��ͥ�绰,ta.����,ta.����,ta.���֤��,
                     ta.��Ժʱ��,ta.��Ժʱ��,ta.������λ,ta.�����绰,ta.��ϵ��,ta.��ϵ��ַ,ta.��ϵ�˹�ϵ,ta.ְҵ,ta.������,
                      'DNA' D��,
                      nvl(TDS.storecnt,0) DT,--DNA
                      nvl(PDS.storecnt,0) DP,
                      nvl(NDS.storecnt,0) DN,
                      nvl(LDS.storecnt,0) DL,
                      nvl(EDS.storecnt,0) DE,
                      nvl(SDS.storecnt,0) DS,
                      'RNA' R��,
                      nvl(TRS.storecnt,0) RT,--RNA
                      nvl(PRS.storecnt,0) RP,
                      nvl(NRS.storecnt,0) RN,
                      nvl(LRS.storecnt,0) RL,
                      nvl(ERS.storecnt,0) RE,
                      nvl(SRS.storecnt,0) RS,
                      '����' P��,
                      nvl(TPS.storecnt,0) PT,--����
                      nvl(PPS.storecnt,0) PP,
                      nvl(NPS.storecnt,0) PN,
                      nvl(LPS.storecnt,0) PL,
                      nvl(EPS.storecnt,0) PE,
                      nvl(SPS.storecnt,0) PS, 
                      ta.����ʱ��,ta.������,ta.��ע 
                      from ta left join TDS on ta.SPECID=TDS.SPECID and TDS.BAOMOENTIRE = ta.ȡ������
                      left join TRS on TRS.SPECID=ta.SPECID and TRS.BAOMOENTIRE = ta.ȡ������
					 left join TPS on TPS.SPECID=ta.SPECID and TPS.BAOMOENTIRE = ta.ȡ������
					 left join PDS on PDS.SPECID=ta.SPECID and PDS.BAOMOENTIRE = ta.ȡ������
					 left join PRS on PRS.SPECID=ta.SPECID and PRS.BAOMOENTIRE = ta.ȡ������
					 left join PPS on PPS.SPECID=ta.SPECID and PPS.BAOMOENTIRE = ta.ȡ������
					 left join NDS on NDS.SPECID=ta.SPECID and NDS.BAOMOENTIRE = ta.ȡ������ 
					 left join NRS on NRS.SPECID=ta.SPECID and NRS.BAOMOENTIRE = ta.ȡ������
					 left join NPS on NPS.SPECID=ta.SPECID and NPS.BAOMOENTIRE = ta.ȡ������
                     left join LDS on LDS.SPECID=ta.SPECID and LDS.BAOMOENTIRE = ta.ȡ������
					 left join LRS on LRS.SPECID=ta.SPECID and LRS.BAOMOENTIRE = ta.ȡ������
					 left join LPS on LPS.SPECID=ta.SPECID and LPS.BAOMOENTIRE = ta.ȡ������
                     left join EDS on EDS.SPECID=ta.SPECID and EDS.BAOMOENTIRE = ta.ȡ������
					 left join ERS on ERS.SPECID=ta.SPECID and ERS.BAOMOENTIRE = ta.ȡ������
					 left join EPS on EPS.SPECID=ta.SPECID and EPS.BAOMOENTIRE = ta.ȡ������
                     left join SDS on SDS.SPECID=ta.SPECID and SDS.BAOMOENTIRE = ta.ȡ������
					 left join SRS on SRS.SPECID=ta.SPECID and SRS.BAOMOENTIRE = ta.ȡ������
					 left join SPS on SPS.SPECID=ta.SPECID and SPS.BAOMOENTIRE = ta.ȡ������
                     where int(ta.�걾��) between {0} and {1}";
                    curTmp = string.Format(curTmp, num1.ToString(), num2.ToString());
                    curSql += curTmp;
                    if (cmbQCZQ.Text.Trim() != "")
                    {
                        curSql += " and ta.ȡ������ ='" + cmbQCZQ.Text.Trim() + "'";
                    }
                }
            }
            return curSql;
        }

        /// <summary>
        /// �ӿ��ı���д����γɵ�SQL���
        /// </summary>
        /// <returns></returns>
        private string GetStockFilterSql()
        {
            string tmp = GetSql();
            string specId1 = string.Empty;
            //for (int k = 0; k < neuSpread3_Sheet1.RowCount; k++)
            //{
            //    if (k == neuSpread3_Sheet1.RowCount - 1)
            //    {
            //        specId1 += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.SPECID)].Text.Trim() + ")";
            //    }
            //    else
            //    {
            //        specId1 += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.SPECID)].Text.Trim() + ",";
            //    }
            //}
            
            if (specId1 == "")
            {
                specId1 = "0";
            }

            tmp += " where s.specid in (" + specId1 + " and ss.STATUS in ('1','3') \n ) select * from t";

            if (!string.IsNullOrEmpty(this.bldFilter))
            {
                tmp += this.bldFilter;
            }
            return tmp;
        }

        /// <summary>
        /// ��ȡ��ü�¼
        /// </summary>
        /// <returns></returns>
        private void GetVisitRecord()
        {
            try
            {
                string strVisit = string.Empty;
                //����ϱ�ȡ�������ݺܶ�Ϊ�գ�������Ҳ�������޷�ȡ��
                /* select distinct P.CARD_NO ������,P.NAME ����,SO.SPEC_NO �걾��,(SELECT SD.DISEASENAME FROM SPEC_DISEASETYPE SD WHERE SD.DISEASETYPEID = SO.DISEASETYPEID) ����,
                        BS.HBSAG hbsag ,BS.HCV_AB hcvab,BS.HIV_AB hivab,BS.PATH_NUMB �����,TR.RMODEID ���Ʒ�ʽ,TR.RPROCESSID ���Ƴ���,
                        TR.RDEVICEID ����װ��,TR.CMETHOD ���Ʒ���,DN.DIAG_OUTSTATE �������,
                        DN.SECOND_ICD ��̬��,DN.PERIOR_CODE ����,DN.ICD_CODE �����,DN.DIAG_NAME �������
                        FROM SPEC_PATIENT P join SPEC_SOURCE SO on SO.PATIENTID = P.PATIENTID
                        left join MET_CAS_DIAGNOSE DN on SO.INPATIENT_NO = DN.INPATIENT_NO
                        left join MET_CAS_BASE BS on  SO.INPATIENT_NO = BS.INPATIENT_NO
                        left join MET_CAS_TUMOUR TR on SO.INPATIENT_NO = TR.INPATIENT_NO
                        where DN.DIAG_KIND = '1' AND DN.OPER_TYPE = '2'and */
                strVisit = @"with ta as 
                        (select D.CARD_NO ������,(SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.CIRCS AND N.TYPE = 'CASE07') һ�����,
                        (SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.SYMPTOM AND N.TYPE = 'CASE10') ֢״����,
                        D.DEAD_TIME ����ʱ��,(SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.DEAD_REASON AND N.TYPE = 'CASE08') ����,
                        D.VISIT_TIME ĩ�����ʱ��,D.RECRUDESCE_TIME ����ʱ��,
                        (SELECT  N.NAME  FROM COM_DICTIONARY N WHERE N.CODE = D.SEQUELA AND N.TYPE = 'CASE09') ����֢,
                        (SELECT  N.NAME  FROM COM_DICTIONARY N WHERE N.CODE = D.TRANSFER_POSITION AND N.TYPE = 'CASE11') ת�Ʋ�λ
                        FROM MET_CAS_VISITRECORD D,MET_CAS_VISIT V where D.CARD_NO = V.CARD_NO AND D.VISIT_TIME = V.LAST_TIME
                        and D.CARD_NO in ('{0}),
                        tb as
                        (select distinct P.CARD_NO ������,P.PNAME ����,SO.SPEC_NO �걾��,(SELECT SD.DISEASENAME FROM SPEC_DISEASETYPE SD WHERE SD.DISEASETYPEID = SO.DISEASETYPEID) ����,
                        BS.HBSAG hbsag ,BS.HCV_AB hcvab,BS.HIV_AB hivab,BS.PATH_NUMB �����,TR.RMODEID ���Ʒ�ʽ,TR.RPROCESSID ���Ƴ���,
                        TR.RDEVICEID ����װ��,TR.CMETHOD ���Ʒ���,DN.MAIN_DIAGSTATE �������,
                        DN.MOD_ICD ��̬��,DN.EXT3 ����,DN.MAIN_DIACODE �����,DN.MAIN_DIANAME �������
                        FROM SPEC_PATIENT P join SPEC_SOURCE SO on SO.PATIENTID = P.PATIENTID
                        left join SPEC_DIAGNOSE DN on SO.SPECID = DN.SPECID
                        left join MET_CAS_BASE BS on  SO.INPATIENT_NO = BS.INPATIENT_NO
                        left join MET_CAS_TUMOUR TR on SO.INPATIENT_NO = TR.INPATIENT_NO
                        where
                        P.CARD_NO in  ('{0}
                        and SO.SPECID in ({1})
                        select tb.������,tb.����,tb.�걾��,tb.����,tb.��̬��,tb.����,tb.�����,tb.�������,
                        tb.hbsag,tb.hcvab,tb.hivab,tb.�����,tb.���Ʒ�ʽ,tb.���Ƴ���,tb.����װ��,tb.���Ʒ���,tb.�������,
                        (select ta.һ����� from ta where ta.������ = tb.������) һ�����,
                        (select ta.֢״���� from ta  where ta.������ = tb.������) ֢״����,
                        (select ta.����ʱ�� from ta  where ta.������ = tb.������) ����ʱ��,
                        (select ta.���� from ta  where ta.������ = tb.������) ����,
                        (select ta.ĩ�����ʱ�� from ta  where ta.������ = tb.������) ĩ�����ʱ��,
                        (select ta.����ʱ�� from ta  where ta.������ = tb.������) ����ʱ��,
                        (select ta.����֢ from ta  where ta.������ = tb.������) ����֢,
                        (select ta.ת�Ʋ�λ from ta  where ta.������ = tb.������) ת�Ʋ�λ
                        from tb order by tb.�걾�� ";
                string strSpecId = string.Empty;
                string strCardNo = string.Empty;

                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    for (int k = 0; k < neuSpread1_Sheet1.RowCount; k++)
                    {
                        if (k == neuSpread1_Sheet1.RowCount - 1)
                        {
                            strCardNo += neuSpread1_Sheet1.Cells[k, Convert.ToInt32(ColFs.������)].Text.Trim() + "')";
                            strSpecId += neuSpread1_Sheet1.Cells[k, Convert.ToInt32(ColFs.SPECID)].Text.Trim() + ")";
                        }
                        else
                        {
                            strCardNo += neuSpread1_Sheet1.Cells[k, Convert.ToInt32(ColFs.������)].Text.Trim() + "','";
                            strSpecId += neuSpread1_Sheet1.Cells[k, Convert.ToInt32(ColFs.SPECID)].Text.Trim() + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(strCardNo))
                    {
                        strVisit = string.Format(strVisit, strCardNo, strSpecId);
                        sheetView1.RowCount = 0;
                        DataSet dsTmp = new DataSet();
                        subMgr.ExecQuery(strVisit, ref dsTmp);

                        if (dsTmp != null && dsTmp.Tables.Count > 0)
                        {
                            sheetView1.DataSource = dsTmp;

                            DataView dvTmp = new DataView(dsTmp.Tables[0]);

                            if (dvTmp.Count > 0)
                            {
                                sheetView1.DataSource = dvTmp;
                                this.tpgOtherRt.Text = "������ò�ѯ��� (��: " + dvTmp.Count.ToString() + " ��)";
                            }
                            for (int jk = 0; jk < sheetView1.ColumnCount; jk++)
                            {
                                sheetView1.Columns[jk].Width = 80;
                            }
                        }
                        else
                        {
                            this.tpgOtherRt.Text = "������ò�ѯ��� (��: 0��)";
                        }
                    }
                    else
                    {
                        this.tpgOtherRt.Text = "������ò�ѯ��� (��: 0��)";
                    }
                }
                else
                {
                    MessageBox.Show("������ò�ѯ��ѡ�ڵ�һ��ѯ�н���²��ܹ�������");
                    return;
                }
            }
            catch
            {
            }
            
        }

        /// <summary>
        /// ����ӻ����д��� detail = true
        /// </summary>
        /// <param name="detail"></param>
        private void GetDetailData(int detail)
        {
            switch (detail)
            {
                case 4:
                    curSql = GetSqlFromSum();
                    break;
                case 5:
                    curSql = GetSqlFromStock();
                    break;
                case 2 :
                    curSql = SetDetSql();
                    break;
                case 8:
                    curSql = GetCardCol();
                    break;
                case 9:
                    curSql = GetSpecCol();
                    break;
                case 7:
                    curSql = this.GetSql() + " where ss.SUBBARCODE in ({0})" + "\n ) select * from t";
                    curSql = string.Format(curSql, specBar);
                    break;
                case 10:
                    curSql = GetStockFilterSql();
                    break;
                default :
                    return;
            }
            curSql += " order by �걾�� ";

            neuSpread1_Sheet1.RowCount = 0;
            DataSet ds = new DataSet();
            subMgr.ExecQuery(curSql, ref ds);
            neuSpread1_Sheet1.AutoGenerateColumns = false;
            neuSpread1_Sheet1.DataAutoSizeColumns = false;
            if (ds != null && ds.Tables.Count > 0)
            {
                neuSpread1_Sheet1.DataSource = ds;

                this.DtDataView = new DataView(ds.Tables[0]);
                if (this.isOldData)
                {
                    //��¼ԭ����ϸ��dataview
                    this.oldDv = this.DtDataView;
                }
                if (this.DtDataView.Count == 0)
                {
                    MessageBox.Show("û�в�ѯ������Ҫ���¼����������������ߵ���Excel��");
                    return;
                }
                neuSpread1_Sheet1.DataSource = this.DtDataView;
            }
            else
            {
                MessageBox.Show("û�в�ѯ������Ҫ���¼����������������ߵ���Excel��");
                return;
            }

            this.BindingSheet1(detail);
        }

        private void BindingSheet1(int detail)
        {
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.SPECID), "SPECID");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.Դ����), "Դ����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.�걾��), "�걾��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.������), "������");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.�Ա�), "�Ա�");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.�ʹ����), "�ʹ����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.�ʹ�ҽ��), "�ʹ�ҽ��");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����ʱ��), "����ʱ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.������), "������");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ע), "��ע");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.���), "���");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��Ѫ����ʱ��), "��Ѫ����ʱ��");

            #region ��ѡ������Ϣ
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.ְҵ), "ְҵ");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.���֤����), "���֤��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��Ժʱ��), "��Ժʱ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��Ժʱ��), "��Ժʱ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��λ�绰), "�����绰");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.������λ), "������λ");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.���ڵ�ַ), "��ͥסַ");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ͥ�绰), "��ͥ�绰");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ϵ��), "��ϵ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ϵ��ַ), "��ϵ��ַ");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ϵ�绰), "��ϵ�绰");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ϵ), "��ϵ�˹�ϵ");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����״��), "��״");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.������), "������");
            #endregion

            #region ��֯
            if (!rbtBld.Checked)
            {
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.ȡ������), "ȡ������");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.D��), "D��");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DT), "DT");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DP), "DP");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DN), "DN");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DL), "DL");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DE), "DE");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DS), "DS");

                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.R��), "R��");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RT), "RT");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RN), "RN");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RP), "RP");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RL), "RL");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RE), "RE");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RS), "RS");

                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.P��), "P��");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PT), "PT");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PP), "PP");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PN), "PN");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PL), "PL");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PE), "PE");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PS), "PS");

                //����
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.����), "����");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DTO), "DTO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DPO), "DPO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DNO), "DNO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DLO), "DLO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DEO), "DEO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.DSO), "DSO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RTO), "RTO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RNO), "RNO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RPO), "RPO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RLO), "RLO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.REO), "REO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.RSO), "RSO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PTO), "PTO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PPO), "PPO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PNO), "PNO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PLO), "PLO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PEO), "PEO");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.PSO), "PSO");

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ���װ)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ����װ)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϸ����װ)].Visible = false;

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ����)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ��������)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ�����)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ���������)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϸ�����)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϸ���������)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.D��)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DT)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DP)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DN)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DL)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DE)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DS)].Visible = false;

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.P��)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PT)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PP)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PN)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PL)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PE)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PS)].Visible = false;

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.R��)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RT)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RP)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RN)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RL)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RE)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RS)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.ȡ������)].Visible = true;

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RTO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RPO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RNO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RLO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.REO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RSO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PTO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PPO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PNO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PLO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PEO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PSO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DTO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DPO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DNO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DLO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DEO)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DSO)].Visible = true;
                orgOrBld = "O";
            }
            #endregion

            #region Ѫ�걾
            if (rbtBld.Checked)
            {
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.Ѫ���װ), "Ѫ���װ");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.Ѫ����װ), "Ѫ����װ");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ϸ����װ), "��ϸ����װ");

                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.Ѫ����), "Ѫ����");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.Ѫ��������), "Ѫ��������");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.Ѫ�����), "Ѫ�����");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.Ѫ���������), "Ѫ���������");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ϸ�����), "��ϸ�����");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(ColFs.��ϸ���������), "��ϸ���������");

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.ȡ������)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ���װ)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ����װ)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϸ����װ)].Visible = true;

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.D��)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DT)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DP)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DN)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DL)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DE)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DS)].Visible = false;

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.P��)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PT)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PP)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PN)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PL)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PE)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PS)].Visible = false;

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.R��)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RT)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RP)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RN)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RL)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RE)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RS)].Visible = false;

                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RTO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RPO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RNO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RLO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.REO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RSO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PTO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PPO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PNO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PLO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PEO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PSO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DTO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DPO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DNO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DLO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DEO)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DSO)].Visible = false;
                orgOrBld = "B";
            }
            #endregion
            for (int i = 1; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
                neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
            }
            tpQuery.SelectedIndex = 2;
            specIdList = new List<string>();
            tpQuery.TabPages[2].Text = "��һ��ѯ��� (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
        }

        /// <summary>
        /// �󶨱걾����
        /// </summary>
        private void BindingSpecType()
        {
            ArrayList arrType = new ArrayList();
            arrType = typeMgr.GetAllSpecType();
            if (arrType.Count > 0)
            {
                cmbSpecType.DisplayMember = "SpecTypeName";
                cmbSpecType.ValueMember = "SpecTypeID";
                cmbSpecType.DataSource = arrType;
            }
        }

        /// <summary>
        /// �󶨱���
        /// </summary>
        private void BindingIceBox()
        {
            ArrayList arrIcebox = new ArrayList();
            arrIcebox = ibMgr.GetAllIceBox();
            if (arrIcebox == null || arrIcebox.Count <= 0)
            {
                return;
            }
            cmbIceBox.DataSource = arrIcebox;
            cmbIceBox.DisplayMember = "IceBoxName";
            cmbIceBox.ValueMember = "IceBoxId";
        }
        private void GetAllEmp()
        {

            FS.HISFC.BizLogic.Manager.Person personList = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList arrPerson = personList.GetEmployee(EnumEmployeeType.D);
            cmbSendDoc.AddItems(arrPerson);
            ArrayList alDepts = new ArrayList();
            FS.HISFC.BizLogic.Manager.DepartmentStatManager manager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            alDepts = manager.LoadAll();//.GetMultiDept(loginPerson.ID);
            this.cmbDept.AddItems(alDepts);
        }
        private void DisTypeBinding()
        {
            Dictionary<int, string> dicDisType = disMgr.GetAllDisType();
            if (dicDisType.Count > 0)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dicDisType;
                cmbDisType.DisplayMember = "Value";
                cmbDisType.ValueMember = "Key";
                cmbDisType.DataSource = bs;
            }
            cmbDisType.Text = "";
        }


        private void ucDayLoadRep_Load(object sender, EventArgs e)
        {
            rbtBld.Checked = true;
            SetDetSql();
            this.InitSheet(neuSpread1_Sheet1);
            //this.InitSheet(sheetView1);
            this.InitSheet1();

            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizProcess.Integrate.Manager dpMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            DisTypeBinding();
            GetAllEmp();
            BindingSpecType();
            BindingIceBox();

            ArrayList arrICD = new ArrayList();
            arrICD = icdMgr.ICDQuery(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
            cmbDiaMain.AddItems(arrICD);
            cmbDiaMain1.AddItems(arrICD);
            cmbDiaMain2.AddItems(arrICD);
            cmbFirstDiag.AddItems(arrICD);
            cmbSpecType.Text = "";

            ArrayList posList = new ArrayList();
            posList = con.GetList("SpecPos");
            cmbQCZQ.AddItems(posList);
        }

        private void LoadPos()
        {
            //if (chkPos.Checked)
            //{
            //    neuSpread1_Sheet1.ColumnCount++;
            //    neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].Label = " �걾λ������";
            //    neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].Width = 300;
            //    for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            //    {
            //        string specId = neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
            //        string specTypeId = neuSpread1_Sheet1.Cells[i, neuSpread1_Sheet1.ColumnCount - 2].Text.Trim();
            //        ArrayList arrSub = subMgr.GetSubSpecBySpecId(specId);
            //        Dictionary<string, List<string>> dicPos = new Dictionary<string, List<string>>();

            //        foreach (SubSpec s in arrSub)
            //        {
            //            if (s.SpecTypeId.ToString() != specTypeId)
            //            {
            //                continue;
            //            }
            //            SpecBoxManage boxMgr = new SpecBoxManage();
            //            SpecBox box = new SpecBox();
            //            box = boxMgr.GetBoxById(s.BoxId.ToString());
            //            string loc = ParseLocation.ParseSpecBox(box.BoxBarCode);
            //            if (dicPos.ContainsKey(loc))
            //            {
            //                dicPos[loc].Add(s.SubBarCode);
            //            }
            //            else
            //            {
            //                dicPos.Add(loc, new List<string>());
            //                dicPos[loc].Add(s.SubBarCode);
            //            }
            //        }
            //        string subLoc = "";
            //        foreach (KeyValuePair<string, List<string>> kv in dicPos)
            //        {
            //            subLoc += kv.Key + ",\n";
            //            foreach (string s in kv.Value)
            //            {
            //                subLoc += " " + s + ",";
            //            }
            //        }
            //        neuSpread1_Sheet1.Cells[i, neuSpread1_Sheet1.Columns.Count - 1].Text = subLoc;
            //        //neuSpread1_Sheet1[i]
            //    }
            //}
        }

        private void GetSubPos()
        {
            try
            {
                SpecOutOper specOut = new SpecOutOper();
                ArrayList arrSpec = new ArrayList();
                string tmpSql = @"select * from SPEC_subspec  where STORETIME between '{0}' and '{1}'";
                string sql = string.Format(tmpSql, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1).ToString(), txtCardNo.Text.Trim().PadLeft(10, '0'));
                specOut.PrintTitle = "�걾λ������";
                arrSpec = subMgr.GetSubSpec(sql);
                List<SubSpec> subList = new List<SubSpec>();
                foreach (SubSpec s in arrSpec)
                {
                    subList.Add(s);
                }
                specOut.PrintOutSpec(subList, null);
            }
            catch
            { }

        }

        private void SetVisible()
        {
            #region ������Ŀ�ɼ���
            
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Դ����)].Visible = chkSpecId.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = cbxDis.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.�ʹ�ҽ��)].Visible = chkSendDoc.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.�ʹ����)].Visible = chkDept.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.������)].Visible = chkOperName.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����ʱ��)].Visible = chkColTime.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ע)].Visible = chkComment.Checked;

            #region ��ѡ������Ϣ
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.ְҵ)].Visible = cbxProfession.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.���֤����)].Visible = cbxIDNo.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��Ժʱ��)].Visible = cbxInDate.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = cbxCountry.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = cbxNative.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = cbxNation.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��Ժʱ��)].Visible = cbxOutDate.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��λ�绰)].Visible = cbxWorkTel.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.������λ)].Visible = cbxWorkPlace.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.���ڵ�ַ)].Visible = cbxPlace.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ͥ�绰)].Visible = chkHomeNum.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϵ��)].Visible = cbxRPerson.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϵ��ַ)].Visible = chkAdress.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϵ�绰)].Visible = chkContNum.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϵ)].Visible = chkNation.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����״��)].Visible = chkMarr.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.������)].Visible = chkOperName.Checked;
            #endregion

            if (cbxBloodTime.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��Ѫ����ʱ��)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��Ѫ����ʱ��)].Visible = false;
            }
            #region Ѫ
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ����)].Visible = cbxSStock.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ��������)].Visible = cbxSOut.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ�����)].Visible = cbxPStock.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ���������)].Visible = cbxPOut.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϸ�����)].Visible = cbxWStock.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϸ���������)].Visible = cbxWOut.Checked;

            if (cbxSSub.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ���װ)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ���װ)].Visible = false;
            }
            if (cbxPSub.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ����װ)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.Ѫ����װ)].Visible = false;
            }
            if (cbxWSub.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϸ����װ)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.��ϸ����װ)].Visible = false;
            }
            #endregion
            if (cbxSpecID.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.�걾��)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.�걾��)].Visible = false;
            }
            if (chkColPos.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.ȡ������)].Visible = chkColPos.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.ȡ������)].Visible = false;
            }

            if (cbxDiag.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.���)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.���)].Visible = false;
            }

            if (cbxPName.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = false;
            }
            if (cbxCardNo.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.������)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.������)].Visible = false;
            }
            if (cbxSex.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.�Ա�)].Visible = cbxSex.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.�Ա�)].Visible = false;
            }
            if (cbxAge.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = cbxAge.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.����)].Visible = false;
            }
            #region ��֯
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DTO)].Visible = cbxDTO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DPO)].Visible = cbxDPO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DNO)].Visible = cbxDNO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DLO)].Visible = cbxDLO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DEO)].Visible = cbxDEO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DSO)].Visible = cbxDSO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PTO)].Visible = cbxPTO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PPO)].Visible = cbxPPO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PNO)].Visible = cbxPNO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PLO)].Visible = cbxPLO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PEO)].Visible = cbxPEO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PSO)].Visible = cbxPSO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RTO)].Visible = cbxRTO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RPO)].Visible = cbxRPO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RNO)].Visible = cbxRNO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RLO)].Visible = cbxRNO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.REO)].Visible = cbxREO.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RSO)].Visible = cbxRSO.Checked;
            //DNA
            if (cbxDT.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DT)].Visible = cbxDT.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DT)].Visible = false;
            }
            if (cbxDP.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DP)].Visible = cbxDP.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DP)].Visible = false;
            }
            if (cbxDN.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DN)].Visible = cbxDN.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DN)].Visible = false;
            }
            if (cbxDL.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DL)].Visible = cbxDL.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DL)].Visible = false;
            }
            if (cbxDE.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DE)].Visible = cbxDE.Checked;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DE)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DE)].Visible = false;
            }
            if (cbxDS.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DS)].Visible = cbxDS.Checked;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DS)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.DS)].Visible = false;
            }

            //RNA
            if (cbxRT.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RT)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RT)].Visible = cbxRT.Checked;

            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RT)].Visible = false;
            }
            if (cbxRP.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RP)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RP)].Visible = cbxRP.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RP)].Visible = false;
            }
            if (cbxRN.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RN)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RN)].Visible = cbxRN.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RN)].Visible = false;
            }
            if (cbxRL.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RL)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RL)].Visible = cbxRL.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RL)].Visible = false;
            }
            if (cbxRE.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RE)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RE)].Visible = cbxRE.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RE)].Visible = false;
            }
            if (cbxRS.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RS)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RS)].Visible = cbxRS.Checked;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.RS)].Visible = false;
            }

            //����
            if (cbxPT.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PT)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PT)].Visible = false;
            }
            if (cbxPP.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PP)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PP)].Visible = false;
            }
            if (cbxPN.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PN)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PN)].Visible = false;
            }
            if (cbxPL.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PL)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PL)].Visible = false;
            }
            if (cbxPE.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PE)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PE)].Visible = false;
            }
            if (cbxPS.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PS)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(ColFs.PS)].Visible = false;
            }
            #endregion
            #endregion
        }

        private void Query(int index)
        {
            //��һ��ѯ
            if (cbxFirQuery.Checked)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��һ��ϸ��Ϣ�����Ժ�...");
                Application.DoEvents();
                GetDetailData(index);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            //�걾�����Ϣ��ѯ ������һ��ѯ
            if (cbxPair.Checked)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�걾�����Ϣ�����Ժ�...");
                Application.DoEvents();
                GetSumData();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            #region ���������Ϣ lingk20110829
            if (cbxVisitCase.Checked)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
                Application.DoEvents();
                this.GetVisitRecord();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            #endregion

            SetVisible();

        }

        /// <summary>
        /// �����ѡ�걾
        /// </summary>
        private void Clear()
        {
            //sheetView1.RowCount = 0;
            //this.tpgOCondition.Text = "��ѡ�걾 (��:" + sheetView1.RowCount.ToString() + " )";
        }

        public override int Print(object sender, object neuObject)
        {
            return base.Print(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Controls.NeuSpread ns = new FS.FrameWork.WinForms.Controls.NeuSpread();
            ns = (tpQuery.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Controls.NeuSpread);

            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "��ѯ���������ѡ��Excel�ļ�����λ��";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "�걾";
            DialogResult dr = saveFileDiaglog.ShowDialog();
            switch (tpQuery.SelectedIndex)
            {
                case 3:
                    neuSpread3.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                    break;
                case 4:
                    neuSpread2.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                    break;
                case 2:
                    //neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders); 
                    //SpreadToExlHelp.ExportExl(neuSpread1_Sheet1, 1, new int[] { },saveFileDiaglog.FileName,true);
                    neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                    break;
                case 6:
                    //neuSpread5.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                    break;
                default:
                    //SpreadToExlHelp.ExportExl(sheetView1, Convert.ToInt32(ColFs.chk), new int[] { }, saveFileDiaglog.FileName,false);
                    //neuSpread4.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                    break;
            }
             
            return base.Export(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("λ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡԤ��, true, false, null);
            this.toolBarService.AddToolButton("��ӡλ����Ϣ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "��ӡλ����Ϣ":
                    break;
                //
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }         

        private void SelectedAppRow(string appId)
        {
            //this.txtApplyNum.Text = appId;
        }

        #region �¼�
        private void chkPos_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cmbSpecType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkSpecId_CheckedChanged(object sender, EventArgs e)
        {
            SetVisible();
        }

        private void rbtBld_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBld.Checked)
            {
                chk863.Visible = false;
                chk115.Visible = false;
                label16.Visible = false;
                cmbQCZQ.Visible = false;
                cbxDT.Checked = false;
                cbxDP.Checked = false;
                cbxDN.Checked = false;
                cbxDL.Checked = false;
                cbxDE.Checked = false;
                cbxDS.Checked = false;
                cbxRT.Checked = false;
                cbxRP.Checked = false;
                cbxRN.Checked = false;
                cbxRL.Checked = false;
                cbxRE.Checked = false;
                cbxRS.Checked = false;
                cbxPT.Checked = false;
                cbxPP.Checked = false;
                cbxPN.Checked = false;
                cbxPL.Checked = false;
                cbxPE.Checked = false;
                cbxPS.Checked = false;
                chkColPos.Checked = false;

                #region ����֯��ʾ�����ò��ɼ�
                cbxDT.Checked = false;
                cbxDP.Checked = false;
                cbxDN.Checked = false;
                cbxDL.Checked = false;

                //cbxDOut.Checked = false;
                //cbxDStock.Checked = false;
                //cbxROut.Checked = false;
                //cbxRStock.Checked = false;
                //cbxLOut.Checked = false;
                //cbxLStock.Checked = false;
                //cbxBOut.Checked = false;
                //cbxBStock.Checked = false;
                #endregion

                #region ��Ѫ��ʾ�����ÿɼ�
                if (cbxSecQuery.Checked)
                {
                    cbxSOut.Checked = true;
                    cbxSStock.Checked = true;
                    cbxPOut.Checked = true;
                    cbxPStock.Checked = true;
                    cbxWOut.Checked = true;
                    cbxWStock.Checked = true;
                }
                else
                {
                    cbxSOut.Checked = false;
                    cbxSStock.Checked = false;
                    cbxPOut.Checked = false;
                    cbxPStock.Checked = false;
                    cbxWOut.Checked = false;
                    cbxWStock.Checked = false;
                }
                cbxSSub.Checked = true;

                cbxPSub.Checked = true;

                cbxWSub.Checked = true;
                #endregion

                if (cbxSecQuery.Checked)
                {
                    checkBox4.Checked = true;
                    cbxOrgInfo.Checked = false;
                }
                else
                {
                    checkBox4.Checked = false;
                    cbxOrgInfo.Checked = false;
                }
            }
            else
            {
                #region ����֯��ʾ�����ÿɼ�
                cbxDT.Checked = true;
                cbxDP.Checked = true;
                cbxDN.Checked = true;
                cbxDL.Checked = true;

                //cbxDOut.Checked = true;
                //cbxDStock.Checked = true;
                //cbxROut.Checked = true;
                //cbxRStock.Checked = true;
                //cbxLOut.Checked = true;
                //cbxLStock.Checked = true;
                //cbxBOut.Checked = true;
                //cbxBStock.Checked = true;

                label16.Visible = true;
                cmbQCZQ.Visible = true;
                cbxDT.Checked = true;
                cbxDP.Checked = true;
                cbxDN.Checked = true;
                cbxDL.Checked = false;
                cbxDE.Checked = false;
                cbxDS.Checked = false;
                cbxRT.Checked = true;
                cbxRP.Checked = true;
                cbxRN.Checked = true;
                cbxRL.Checked = false;
                cbxRE.Checked = false;
                cbxRS.Checked = false;
                cbxPT.Checked = true;
                cbxPP.Checked = true;
                cbxPN.Checked = true;
                cbxPL.Checked = false;
                cbxPE.Checked = false;
                cbxPS.Checked = false;
                chkColPos.Checked = true;
                #endregion

                #region ��Ѫ��ʾ�����ò��ɼ�
                //cbxSOut.Checked = false;
                //cbxSStock.Checked = false;
                //cbxSSub.Checked = false;
                //cbxPOut.Checked = false;
                //cbxPStock.Checked = false;
                //cbxPSub.Checked = false;
                //cbxWOut.Checked = false;
                //cbxWStock.Checked = false;
                //cbxWSub.Checked = false;
                #endregion

                chk863.Visible = true;
                chk115.Visible = true;
                if (cbxSecQuery.Checked)
                {
                    checkBox4.Checked = false;
                    cbxOrgInfo.Checked = true;
                }
                else
                {
                    checkBox4.Checked = false;
                    cbxOrgInfo.Checked = false;
                }
            }
        }

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                GetDetailData(4);
            }
            catch
            {
            }
        }

        private void neuSpread3_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                this.bldFilter = string.Empty;
                GetDetailData(5);
            }
            catch
            {
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
            Application.DoEvents();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void btnQueryCardNo_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(cmbDisType.Text.Trim()))
                {
                    DialogResult dRt = MessageBox.Show("û��ѡ�в����Ƿ������ѯ��", "��ʾ", MessageBoxButtons.YesNo);
                    if (dRt == DialogResult.Yes)
                    {
                        Query(8);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Query(8);
                }
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("��ѯExcel�쳣��������Excel�������Ƿ���ȷ��");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbDisType.Text.Trim()))
                {
                    DialogResult dRt = MessageBox.Show("û��ѡ�в����Ƿ������ѯ��", "��ʾ", MessageBoxButtons.YesNo);
                    if (dRt == DialogResult.Yes)
                    {
                        Query(9);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Query(9);
                }
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("��ѯExcel�쳣��������Excel�������Ƿ���ȷ��");
            }
        }

        private void btnB1_Click(object sender, EventArgs e)
        {
            try
            {
                fileDialog.ShowDialog();
                txtCardNoExl.Text = fileDialog.FileName;
            }
            catch
            {
            }
        }

        private void btnB2_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            txtSpecNoExl.Text = fileDialog.FileName;
        }

        /// <summary>
        /// ��һ������ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSource.Checked || chkPat.Checked || chkPos.Checked || chkLoc.Checked || chkDiag.Checked || chkApply.Checked)
                {
                    Query(2);
                }
                else
                {
                    MessageBox.Show("������ѡ��һ����Ϊ������");
                    return;
                }
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        private void txtNo1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNo1.Text.Trim() != "")
                {
                    txtNo2.Focus();
                }
            }
        }

        private void txtNo2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNo2.Text.Trim() != "")
                {
                    cmbDisType.Focus();
                }
            }
        }

        private void cbxOrgAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbxOrgAll.CheckState == CheckState.Checked)
            {
                if (rbtOrg.Checked)
                {
                    MessageBox.Show("��ѡ�в�ѯ�����б걾����Ϊ����֯����");
                    return;
                }
                cbxOrgAll.Text = "ȫ��ѡ";
                cbxDT.Checked = true;
                cbxDP.Checked = true;
                cbxDN.Checked = true;
                cbxDL.Checked = true;
                cbxDE.Checked = true;
                cbxDS.Checked = true;
                cbxRT.Checked = true;
                cbxRP.Checked = true;
                cbxRN.Checked = true;
                cbxRL.Checked = true;
                cbxRE.Checked = true;
                cbxRS.Checked = true;
                cbxPT.Checked = true;
                cbxPP.Checked = true;
                cbxPN.Checked = true;
                cbxPL.Checked = true;
                cbxPE.Checked = true;
                cbxPS.Checked = true;
            }
            else if (cbxOrgAll.CheckState == CheckState.Unchecked)
            {
                cbxOrgAll.Text = "ȫѡ";
                cbxDT.Checked = false;
                cbxDP.Checked = false;
                cbxDN.Checked = false;
                cbxDL.Checked = false;
                cbxDE.Checked = false;
                cbxDS.Checked = false;
                cbxRT.Checked = false;
                cbxRP.Checked = false;
                cbxRN.Checked = false;
                cbxRL.Checked = false;
                cbxRE.Checked = false;
                cbxRS.Checked = false;
                cbxPT.Checked = false;
                cbxPP.Checked = false;
                cbxPN.Checked = false;
                cbxPL.Checked = false;
                cbxPE.Checked = false;
                cbxPS.Checked = false;
            }
            else
            {
                cbxOrgAll.Text = "�ָ�";
                cbxDT.Checked = true;
                cbxDP.Checked = true;
                cbxDN.Checked = true;
                cbxDL.Checked = false;
                cbxDE.Checked = false;
                cbxDS.Checked = false;
                cbxRT.Checked = true;
                cbxRP.Checked = true;
                cbxRN.Checked = true;
                cbxRL.Checked = false;
                cbxRE.Checked = false;
                cbxRS.Checked = false;
                cbxPT.Checked = true;
                cbxPP.Checked = true;
                cbxPN.Checked = true;
                cbxPL.Checked = false;
                cbxPE.Checked = false;
                cbxPS.Checked = false;
            }
            SetVisible();
        }

        /// <summary>
        /// ȫѡ�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxChsVCInfo_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbxChsVCInfo.CheckState == CheckState.Checked)
            {
                cbxVisitResult.Checked = true;
                cbxZL.Checked = true;
                cbxCrics.Checked = true;
                cbxZZBX.Checked = true;
                cbxDeadR.Checked = true;
                cbxDeadTime.Checked = true;
                cbxVisitTime.Checked = true;
                cbxVisitType.Checked = true;
                cbxSFFF.Checked = true;
            }
            else if (cbxChsVCInfo.CheckState == CheckState.Unchecked)
            {
                cbxVisitResult.Checked = false;
                cbxZL.Checked = false;
                cbxCrics.Checked = false;
                cbxZZBX.Checked = false;
                cbxDeadR.Checked = false;
                cbxDeadTime.Checked = false;
                cbxVisitTime.Checked = false;
                cbxVisitType.Checked = false;
                cbxSFFF.Checked = false;
            }
            else
            {
                cbxVisitResult.Checked = true;
                cbxZL.Checked = true;
                cbxCrics.Checked = true;
                cbxZZBX.Checked = true;
                cbxDeadR.Checked = true;
                cbxDeadTime.Checked = true;
                cbxVisitTime.Checked = true;
                cbxVisitType.Checked = true;
                cbxSFFF.Checked = true;
            }
        }

        /// <summary>
        /// ȫѡ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxChsCaseInfo_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbxChsCaseInfo.CheckState == CheckState.Checked)
            {
                cbxFLFS.Checked = true;
                cbxFLCS.Checked = true;
                cbxFLZZ.Checked = true;
                cbxFLFF.Checked = true;
                cbxHBsAg.Checked = true;
                cbxHCV.Checked = true;
                cbxHIV.Checked = true;
                cbxZG.Checked = true;
                cbxTsPosition.Checked = true;
                cbxHY.Checked = true;
            }
            else if (cbxChsCaseInfo.CheckState == CheckState.Unchecked)
            {
                cbxFLFS.Checked = false;
                cbxFLCS.Checked = false;
                cbxFLZZ.Checked = false;
                cbxFLFF.Checked = false;
                cbxHBsAg.Checked = false;
                cbxHCV.Checked = false;
                cbxHIV.Checked = false;
                cbxZG.Checked = false;
                cbxTsPosition.Checked = false;
                cbxHY.Checked = false;
            }
            else
            {
                cbxFLFS.Checked = true;
                cbxFLCS.Checked = true;
                cbxFLZZ.Checked = true;
                cbxFLFF.Checked = true;
                cbxHBsAg.Checked = true;
                cbxHCV.Checked = true;
                cbxHIV.Checked = true;
                cbxZG.Checked = true;
                cbxTsPosition.Checked = true;
                cbxHY.Checked = true;
            }
        }

        private void cbxChsPInfo_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbxChsPInfo.CheckState == CheckState.Checked)
            {
                cbxInDate.Checked = true;
                cbxOutDate.Checked = true;
                cbxNation.Checked = true;
                cbxCountry.Checked = true;
                cbxNative.Checked = true;
                cbxRPerson.Checked = true;
                chkNation.Checked = true;
                chkAdress.Checked = true;
                chkContNum.Checked = true;
                chkMarr.Checked = true;
                cbxProfession.Checked = true;
                cbxBirthPlace.Checked = true;
                cbxIDNo.Checked = true;
                cbxWorkPlace.Checked = true;
                cbxWorkTel.Checked = true;
                cbxPlace.Checked = true;
                chkHomeNum.Checked = true;
            }
            else if (cbxChsPInfo.CheckState == CheckState.Unchecked)
            {
                cbxInDate.Checked = false;
                cbxOutDate.Checked = false;
                cbxNation.Checked = false;
                cbxCountry.Checked = false;
                cbxNative.Checked = false;
                cbxRPerson.Checked = false;
                chkNation.Checked = false;
                chkAdress.Checked = false;
                chkContNum.Checked = false;
                chkMarr.Checked = false;
                cbxProfession.Checked = false;
                cbxBirthPlace.Checked = false;
                cbxIDNo.Checked = false;
                cbxWorkPlace.Checked = false;
                cbxWorkTel.Checked = false;
                cbxPlace.Checked = false;
                chkHomeNum.Checked = false;
            }
            else
            {
                cbxInDate.Checked = true;
                cbxOutDate.Checked = true;
                cbxNation.Checked = true;
                cbxCountry.Checked = true;
                cbxNative.Checked = true;
                cbxRPerson.Checked = true;
                chkNation.Checked = true;
                chkAdress.Checked = true;
                chkContNum.Checked = true;
                chkMarr.Checked = true;
                cbxProfession.Checked = true;
                cbxBirthPlace.Checked = true;
                cbxIDNo.Checked = true;
                cbxWorkPlace.Checked = true;
                cbxWorkTel.Checked = true;
                cbxPlace.Checked = true;
                chkHomeNum.Checked = true;
            }
        }

        private void cbxSpecInfo_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbxSpecInfo.CheckState == CheckState.Checked)
            {
                cbxCDiag.Checked = true;
                cbxDDiag.Checked = true;
                cbxDiagNo.Checked = true;
                cbxDiagName.Checked = true;
                cbxICD.Checked = true;
                cbxShape.Checked = true;
                cbxTNM.Checked = true;
                cbxOtherDiag.Checked = true;
                cbxCompare.Checked = true;
            }
            else if (cbxSpecInfo.CheckState == CheckState.Unchecked)
            {
                cbxCDiag.Checked = false;
                cbxDDiag.Checked = false;
                cbxDiagNo.Checked = false;
                cbxDiagName.Checked = false;
                cbxICD.Checked = false;
                cbxShape.Checked = false;
                cbxTNM.Checked = false;
                cbxOtherDiag.Checked = false;
                cbxCompare.Checked = false;
            }
            else
            {
                cbxCDiag.Checked = true;
                cbxDDiag.Checked = true;
                cbxDiagNo.Checked = true;
                cbxDiagName.Checked = true;
                cbxICD.Checked = true;
                cbxShape.Checked = true;
                cbxTNM.Checked = true;
                cbxOtherDiag.Checked = true;
                cbxCompare.Checked = true;
            }
        }

        private void rbtOrg_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtOrg.Checked)
            {
                cbxSSub.Checked = false;
                cbxPSub.Checked = false;
                cbxWSub.Checked = false;
                if (cbxSecQuery.Checked)
                {
                    checkBox4.Checked = false;
                    cbxOrgInfo.Checked = true;
                }
                else
                {
                    checkBox4.Checked = false;
                    cbxOrgInfo.Checked = false;
                }
            }
        }

        private void cbxSecQuery_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtOrg.Checked)
            {
                checkBox4.Checked = false;
                cbxOrgInfo.Checked = true;
            }
            else if (rbtBld.Checked)
            {
                checkBox4.Checked = true;
                cbxOrgInfo.Checked = false;
            }
        }
        
        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {
                if (rbtOrg.Checked)
                {
                    MessageBox.Show("��ѡ�в�ѯ�����б걾����Ϊ��Ѫ����");
                    return;
                }
                cbxSOut.Checked = true;
                cbxSStock.Checked = true;
                cbxPOut.Checked = true;
                cbxPStock.Checked = true;
                cbxWOut.Checked = true;
                cbxWStock.Checked = true;
            }
            else if (checkBox4.CheckState == CheckState.Unchecked)
            {
                cbxSOut.Checked = false;
                cbxSStock.Checked = false;
                cbxPOut.Checked = false;
                cbxPStock.Checked = false;
                cbxWOut.Checked = false;
                cbxWStock.Checked = false;
            }
            else
            {
                cbxSOut.Checked = true;
                cbxSStock.Checked = true;
                cbxPOut.Checked = true;
                cbxPStock.Checked = true;
                cbxWOut.Checked = true;
                cbxWStock.Checked = true;
            }
        }

        private void cbxOrgInfo_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbxOrgInfo.CheckState == CheckState.Checked)
            {
                //if (rbtOrg.Checked)
                //{
                //    MessageBox.Show("��ѡ�в�ѯ�����б걾����Ϊ����֯����");
                //    return;
                //}
                cbxDTO.Checked = true;
                cbxDPO.Checked = true;
                cbxDNO.Checked = true;
                cbxDLO.Checked = true;
                cbxDEO.Checked = true;
                cbxDSO.Checked = true;
                cbxRTO.Checked = true;
                cbxRPO.Checked = true;
                cbxRNO.Checked = true;
                cbxRLO.Checked = true;
                cbxREO.Checked = true;
                cbxRSO.Checked = true;
                cbxPTO.Checked = true;
                cbxPPO.Checked = true;
                cbxPNO.Checked = true;
                cbxPLO.Checked = true;
                cbxPEO.Checked = true;
                cbxPSO.Checked = true;
            }
            else if (cbxOrgInfo.CheckState == CheckState.Unchecked)
            {
                cbxDTO.Checked = false;
                cbxDPO.Checked = false;
                cbxDNO.Checked = false;
                cbxDLO.Checked = false;
                cbxDEO.Checked = false;
                cbxDSO.Checked = false;
                cbxRTO.Checked = false;
                cbxRPO.Checked = false;
                cbxRNO.Checked = false;
                cbxRLO.Checked = false;
                cbxREO.Checked = false;
                cbxRSO.Checked = false;
                cbxPTO.Checked = false;
                cbxPPO.Checked = false;
                cbxPNO.Checked = false;
                cbxPLO.Checked = false;
                cbxPEO.Checked = false;
                cbxPSO.Checked = false;
            }
            else
            {
                cbxDTO.Checked = true;
                cbxDPO.Checked = true;
                cbxDNO.Checked = true;
                cbxDLO.Checked = false;
                cbxDEO.Checked = false;
                cbxDSO.Checked = false;
                cbxRTO.Checked = true;
                cbxRPO.Checked = true;
                cbxRNO.Checked = true;
                cbxRLO.Checked = false;
                cbxREO.Checked = false;
                cbxRSO.Checked = false;
                cbxPTO.Checked = true;
                cbxPPO.Checked = true;
                cbxPNO.Checked = true;
                cbxPLO.Checked = false;
                cbxPEO.Checked = false;
                cbxPSO.Checked = false;
            }
        }
        #endregion
    }
}
