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
    public partial class ucSubQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
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

        //select max(ss.SUBBARCODE), t.SPECIMENTNAME,d.DISEASENAME
        //from SPEC_SUBSPEC ss, spec_source s, SPEC_DISEASETYPE d, SPEC_TYPE t
        //where ss.SPECID = s.specid and s.DISEASETYPEID>14 and s.DISEASETYPEID = d.DISEASETYPEID and t.SPECIMENTTYPEID = ss.SPECTYPEID
        //group by t.SPECIMENTNAME, d.DISEASENAME

        private enum CurPos
        {
            chk,
            �걾ԴID,
            Դ����,
            �걾��,
            //����Ѫ֧��,
            //����Ѫ����,
            //�ǿ���Ѫ֧��,
            //�ǿ���Ѫ����, 
            ������,
            ��ע,
            ���,
            ����,
            ������,
            �걾����,
            �걾����,
            ����,
            ����ʱ��,
            �ʹ�ҽ��,
            ����,
            ȡ������,
            ��������,
            �ڿ�״̬,
            �걾����,
            �ϴη���ʱ��,
            Լ������ʱ��,
            ת�Ʋ�λ,
            �걾����,
            ��������,
            �걾��ϸ����,
            ���ƿ���,
            סԺ��ˮ��,
            ��ǰ���ƽ׶�,
            ��������,
            �����,
            �������̬��,
            ���1,
            ���1��̬��,
            ���2,
            ���2��̬��,
            ��ϱ�ע,
            �Ա�,
            ����,
            ����,
            סַ,
            ����״��,
            ����,
            ��ϵ�绰,
            ��ͥ�绰,
            //��װ����,                      
            �걾��λ��,
            ��,
            ��,
            SPECTYPEID
        }

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

        private enum StoPos
        {
            סԺ��ˮ��,
            SPECID,
            Դ����,
            ������,
            ��ע,
            ���,
            ����,
            �걾��,
            ������,
            ����,
            ����ʱ��,
            �ʹ�ҽ��,
            ����,           
            ��ǰ���ƽ׶�,
            ��������,
            �����,
            �������̬��,
            ���1,
            ���1��̬��,
            ���2,
            ���2��̬��,
            ��ϱ�ע,
            �Ա�,
            ����,
            ����,
            סַ,
            ����״��,
            ����,
            ��ϵ�绰,
            ��ͥ�绰//,
            //SPECTYPEID,
           // storeid
        }

        public ucSubQuery()
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
            sheet.Columns.Count = Convert.ToInt32(CurPos.SPECTYPEID) + 1;
            FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            
            sheet.Columns[Convert.ToInt32(CurPos.chk)].Label = "ѡ��";
            sheet.Columns[Convert.ToInt32(CurPos.chk)].CellType = chkType;
            sheet.Columns[Convert.ToInt32(CurPos.chk)].Width = 30;

            sheet.Columns[Convert.ToInt32(CurPos.�걾ԴID)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.�걾ԴID)].Label = CurPos.�걾ԴID.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.Դ����)].Width = 100;
            sheet.Columns[Convert.ToInt32(CurPos.Դ����)].Label = CurPos.Դ����.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�걾��)].Width = 100;
            sheet.Columns[Convert.ToInt32(CurPos.�걾��)].Label = CurPos.�걾��.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.����)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.����)].Label = CurPos.����.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.������)].Width = 100;
            sheet.Columns[Convert.ToInt32(CurPos.������)].Label = CurPos.������.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�걾����)].Width = 120;
            sheet.Columns[Convert.ToInt32(CurPos.�걾����)].Label = CurPos.�걾����.ToString();
            //sheet.Columns[Convert.ToInt32(CurPos.��װ����)].Width = 80;
            //sheet.Columns[Convert.ToInt32(CurPos.��װ����)].Label = CurPos.��װ����.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.����)].Width = 40;
            sheet.Columns[Convert.ToInt32(CurPos.����)].Label = CurPos.����.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.�걾����)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.�걾����)].Label = CurPos.�걾����.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.����ʱ��)].Width = 90;
            sheet.Columns[Convert.ToInt32(CurPos.����ʱ��)].Label = CurPos.����ʱ��.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.�ʹ�ҽ��)].Width = 100;
            sheet.Columns[Convert.ToInt32(CurPos.�ʹ�ҽ��)].Label = CurPos.�ʹ�ҽ��.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.����)].Width = 120;
            sheet.Columns[Convert.ToInt32(CurPos.����)].Label = CurPos.����.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.ȡ������)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.ȡ������)].Label = CurPos.ȡ������.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Label = CurPos.SPECTYPEID.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Visible = false;// = 80;
            sheet.Columns[Convert.ToInt32(CurPos.��������)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.��������)].Label = CurPos.��������.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.������)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.������)].Label = CurPos.������.ToString();


            sheet.Columns[Convert.ToInt32(CurPos.��ע)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.��ע)].Label = CurPos.��ע.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.���)].Width = 40;
            sheet.Columns[Convert.ToInt32(CurPos.���)].Label = CurPos.���.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�ڿ�״̬)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.�ڿ�״̬)].Label = CurPos.�ڿ�״̬.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�걾����)].Width = 50;
            sheet.Columns[Convert.ToInt32(CurPos.�걾����)].Label = CurPos.�걾����.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�ϴη���ʱ��)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.�ϴη���ʱ��)].Label = CurPos.�ϴη���ʱ��.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.Լ������ʱ��)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.Լ������ʱ��)].Label = CurPos.Լ������ʱ��.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.ת�Ʋ�λ)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.ת�Ʋ�λ)].Label = CurPos.ת�Ʋ�λ.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�걾����)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.�걾����)].Label = CurPos.�걾����.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.��������)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.��������)].Label = CurPos.��������.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�걾��ϸ����)].Width = 120;
            sheet.Columns[Convert.ToInt32(CurPos.�걾��ϸ����)].Label = CurPos.�걾��ϸ����.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.���ƿ���)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.���ƿ���)].Label = CurPos.���ƿ���.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.סԺ��ˮ��)].Label = CurPos.סԺ��ˮ��.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.��ǰ���ƽ׶�)].Label = CurPos.��ǰ���ƽ׶�.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.��������)].Label = CurPos.��������.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�����)].Label = CurPos.�����.ToString();


            sheet.Columns[Convert.ToInt32(CurPos.�������̬��)].Label = CurPos.�������̬��.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.���1)].Label = CurPos.���1.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.���1��̬��)].Label = CurPos.���1��̬��.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.���2)].Label = CurPos.���2.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.���2��̬��)].Label = CurPos.���2��̬��.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.��ϱ�ע)].Label = CurPos.��ϱ�ע.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�Ա�)].Label = CurPos.�Ա�.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.����)].Label = CurPos.����.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.����)].Label = CurPos.����.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.סַ)].Label = CurPos.סַ.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.����״��)].Label = CurPos.����״��.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.����)].Label = CurPos.����.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.��ϵ�绰)].Label = CurPos.��ϵ�绰.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.��ͥ�绰)].Label = CurPos.��ͥ�绰.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.�걾��λ��)].Label = CurPos.�걾��λ��.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.��)].Label = CurPos.��.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.��)].Label = CurPos.��.ToString();

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
            return @"
                         with t as
                         (
                         SELECT DISTINCT 
                         s.SPECID �걾ԴID,
                         s.HISBARCODE Դ����, 
                         s.OPEREMP ������,
                         s.MARK ��ע,
                         ( case s.MATCHFLAG when '1' then '��' else '��'end) ���, p.pname ����, 
                         s.SPEC_NO �걾��,
  						 p.CARD_NO ������,
                         ss.SUBBARCODE �걾����,
                         d.DISEASENAME ����, 
                         s.OPERTIME ����ʱ��, 
						(case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) �ʹ�ҽ��,
                        (case s.ISHIS when 'O' then s.DEPTNO else (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO and rownum = 1) end)����,
                        (case s.ORGORBLOOD when 'O' then st.TUMORPOS else '' end) ȡ������,
						(case st.TUMORTYPE when '1' then '����' when '2' then '����' when '3' then '����' when '4' then '����' when '5' then '��˨' when '8' then '�ܰͽ�' else '' end) ��������,
						(case ss.STATUS when '1' then '�ڿ�' when '2' then '���' when '3' then '�ѻ�' when '4' then '����' else '' end) �ڿ�״̬,
						(case st.TUMORPOR when '1' then 'ԭ��' when '2' then '����' when '3' then 'ת��' when '13' then 'ԭ����ת��' when '23' then '������ת��' else '' end) �걾����,
						ss.LASTRETURNTIME �ϴη���ʱ��, 
                         ss.DATERETURNTIME Լ������ʱ��, 
                         st.TRANSPOS ת�Ʋ�λ, 
                         st.CAPACITY �걾����,
						st.BAOMOENTIRE ��������, 
                         st.MARK �걾��ϸ����, 
                         p.IC_CARDNO ���ƿ���,
                         s.INPATIENT_NO סԺ��ˮ��,
						s.GETPEORID ��ǰ���ƽ׶�, 
                         s.OPERPOSNAME ��������, 
                         sd.MAIN_DIANAME �����,
                         sd.MAIN_DIACODE �������̬��,
						sd.MAIN_DIANAME1 ���1, 
                         sd.MAIN_DIACODE1 ���1��̬��,
                         sd.MAIN_DIANAME2 ���2, 
                         sd.MAIN_DIACODE2 ���2��̬��,
						sd.MARK ��ϱ�ע, 
                         (case p.GENDER when 'F' then 'Ů' else '��' end) �Ա�,
						(select name  from COM_DICTIONARY where type = 'COUNTRY' and code = p.NATIONALITY and rownum = 1)����,
            			(select name  from COM_DICTIONARY where type = 'NATION' and code = p.NATION and rownum = 1) ����,
            			p.ADDRESS סַ,
                         (select name from COM_DICTIONARY where type = 'MaritalStatus' and code = p.ISMARR and rownum = 1)����״��,
           				p.BIRTHDAY ����, 
                         p.CONTACTNUM ��ϵ�绰, 
                         p.HOMEPHONENUM ��ͥ�绰, 
                         b.BOXBARCODE �걾��λ��, 
                         ss.BOXENDROW ��,ss.BOXENDCOL ��,
						 t.SPECIMENTNAME �걾����,
                         st.SPECTYPEID
                       from SPEC_SOURCE s join SPEC_SOURCE_STORE st on s.SPECID = st.SPECID 
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_TYPE t ON st.SPECTYPEID = t.SPECIMENTTYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID
                       left join MET_CAS_VISITRECORD rcd on rcd.CARD_NO = p.CARD_NO
					   join SPEC_SUBSPEC ss on s.SPECID = ss.SPECID and st.SOTREID = ss.STOREID
                       left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
					   left join SPEC_BOX b on ss.BOXID = b.BOXID";
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

            #region ���Ҫ��
            if (cbVistDead.CheckState == CheckState.Checked)
            {
                curSql += " and rcd.IS_DEAD = '1'";
            }
            else if (cbVistDead.CheckState == CheckState.Unchecked)
            {
                curSql += " and rcd.IS_DEAD = '0'";
            }
            else //�Ӹ��ո�ɣ�������������Ｐ�վ�ҽԺ����
            {
                curSql += " ";
            }
            #endregion

            string diagCon = "(";

            #region ������ϲ�ѯ
            List<string> diagList = new List<string>();
            if (chkDiag.Checked)
            {
                string diag = cmbDiaMain.Text.TrimStart().TrimEnd(); //;// txtCardNo.Text.Trim();
                string diag1 = cmbDiaMain1.Text.TrimStart().TrimEnd(); ;// txtName.Text.Trim();
                string diag2 = cmbDiaMain2.Text.TrimStart().TrimEnd(); ;// txtName.Text.Trim();
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


            curSql += "\n order by s.SPECID,s.HISBARCODE";

            if (txtNo1.Text.Trim() == "" && txtNo2.Text.Trim() == "")
            {//select * from s
                curSql += "\n ) select * from t";
            }

            else
            {//, t as ( select * from s where substr(�걾��,1,1)>='0' and substr(�걾��,1,1)<='9')
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
                curSql += " \n select * from t where int(�걾��) between " + num1.ToString() + " and " + num2.ToString();
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
                string tmpSpecId = neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, Convert.ToInt32(CurPos.�걾ԴID)].Text;
                if (tmpSpecId == "")
                    tmpSpecs = "(0) ";
                else
                    tmpSpecs = "(" + tmpSpecId + ")";
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

            tsql += " and ss.SPECID in " + tmpSpecs + "),";

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
            //" \n select * from t where int(�걾��) between " + num1.ToString() + " and " + num2.ToString();
            string cardCols = " \n where t.������ in (";
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
            if (cardCols.Trim().Contains("()"))
            {
                cardCols = "where t.������ in ('')";
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
        /// ����Sql
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private string GetStockSql(int tabIndex)
        {
            string tmpSpecs = "";

            //�ӵڶ���tabҳ�д�������ʾ�鿴ָ���걾����Ϣ
            if (tabIndex == 2)
            {
                string tmpSpecId = neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, Convert.ToInt32(CurPos.�걾ԴID)].Text;
                if (tmpSpecId == "")
                    tmpSpecs = "(0)";
                else
                    tmpSpecs = "("+ tmpSpecId +")";

            }
            else
            {
                tmpSpecs = specIds;
            }

            string tsql = "";

            tsql = @"with ta as
                         (
                         SELECT DISTINCT 
                         s.INPATIENT_NO סԺ��ˮ��,
                         s.SPECID,
                         s.HISBARCODE Դ����, 
                         s.OPEREMP ������,
                         s.MARK ��ע,
                         ( case s.MATCHFLAG when '1' then '��' else '��'end) ���, p.pname ����, 
                         s.SPEC_NO �걾��,
  						 p.CARD_NO ������,
                         d.DISEASENAME ����, 
                         s.OPERTIME ����ʱ��, 
						(case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) �ʹ�ҽ��,
                        (case s.ISHIS when 'O' then s.DEPTNO else (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO and rownum = 1) end)����,
						 s.GETPEORID ��ǰ���ƽ׶�, 
                         s.OPERPOSNAME ��������, 
                         sd.MAIN_DIANAME �����,
                         sd.MAIN_DIACODE �������̬��,
						 sd.MAIN_DIANAME1 ���1, 
                         sd.MAIN_DIACODE1 ���1��̬��,
                         sd.MAIN_DIANAME2 ���2, 
                         sd.MAIN_DIACODE2 ���2��̬��,
						 sd.MARK ��ϱ�ע, 
                         (case p.GENDER when 'F' then 'Ů' else '��' end) �Ա�,
						(select name  from COM_DICTIONARY where type = 'COUNTRY' and code = p.NATIONALITY and rownum = 1)����,
            			(select name  from COM_DICTIONARY where type = 'NATION' and code = p.NATION and rownum = 1) ����,
            			p.ADDRESS סַ,
                         (select name from COM_DICTIONARY where type = 'MaritalStatus' and code = p.ISMARR and rownum = 1)����״��,
           				p.BIRTHDAY ����, 
                         p.CONTACTNUM ��ϵ�绰, 
                         p.HOMEPHONENUM ��ͥ�绰,   
                         ss.SPECTYPEID,                     
                        ss.storeid
                       from SPEC_SUBSPEC ss join  SPEC_SOURCE s on ss.specid = s.specid
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID					 
                       left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
                       where s.OPERTIME BETWEEN to_date('{0}','yyyy-mm-dd hh24:mi:ss') AND to_date('{1}','yyyy-mm-dd hh24:mi:ss')	and ss.SPECID>0 and ss.STOREID>0 and ss.BOXID>0
					and s.ORGORBLOOD = '{2}'";

            #region��SQL
//            tsql = @"with   ta as
// 					(
//                    select s.spec_no c,
//				    s.SPECID,p.card_no, p.name, ss.storeid, d.DISEASENAME, SPECTYPEID, s.INPATIENT_NO
//					from SPEC_SUBSPEC ss join spec_source s on ss.specid = s.specid
//					left join SPEC_DISEASETYPE d on s.DISEASETYPEID = d.DISEASETYPEID
//                    left join spec_patient p on s.patientid = p.patientid
//					where s.OPERTIME BETWEEN '{0}' AND '{1}'	and ss.SPECID>0 and ss.STOREID>0 and ss.BOXID>0
//					and s.ORGORBLOOD = '{2}'";

            tsql += " and ss.SPECID in " + tmpSpecs + "),";

            tsql += @"	ts as 
					(
   					  select st.SPECID,st.SPECTYPEID,st.SOTREID, sum(st.SUBCOUNT) subcnt,sum(st.SOTRECOUNT)	storecnt									
										from SPEC_SOURCE_STORE st where 
										st.SOTREID in (select storeid from ta)
											 group by  st.SPECID,st.SPECTYPEID,st.SOTREID
											
					),

					xj as
					(select ts.SPECID, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 1),
					xq as(
					select ts.SPECID, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 2),
					xb as(
					select ts.SPECID, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 3),
					dna as(
					select ts.SPECID, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 4),
					RNA as(
					select ts.SPECID, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 5),
					lk as(
					select ts.SPECID, ts.subcnt,ts.storecnt from ts  where  ts.SPECTYPEID = 7)
					
					select distinct ta.סԺ��ˮ��, ta.SPECID,ta.Դ����, ta.������,ta.��ע,ta.���, ta.����,  ta.�걾��,
                    ta.������,ta. ����, ta.����ʱ��, ta.�ʹ�ҽ��, ta.����,ta.��ǰ���ƽ׶�,ta.��������,  ta.�����,
                    ta.�������̬��,ta.���1,   ta.���1��̬��,ta.���2,  ta.���2��̬��,ta.��ϱ�ע,  ta.�Ա�,ta.����,
                    ta.����,ta.סַ,ta.����״��,ta.����, ta.��ϵ�绰, ta.��ͥ�绰, --inpatient_no סԺ��ˮ��, ta.card_no ������,ta.name ����, ta.c �걾��, ta.DISEASENAME ����,ta.specid,
                    ";
            if (rbtBld.Checked)
            {
                tsql += @"	nvl(xj.subcnt,0) Ѫ��,nvl(xj.storecnt,0) Ѫ�����,nvl(xq.subcnt,0) Ѫ�� ,nvl(xq.storecnt,0) Ѫ���� ,nvl(xb.subcnt,0) ϸ��,nvl(xb.storecnt,0) ϸ�����";

            }
            else
            {
                tsql += @"	dna.subcnt DNA ,nvl(dna.storecnt,0) DNA��� ,rna.subcnt RNA,nvl(rna.storecnt,0) RNA���,lk.subcnt ����,nvl(lk.storecnt,0) ������";
            }

            tsql += @" from ta left join xj on ta.SPECID=xj.SPECID left join xq on xq.SPECID=ta.SPECID 
					 left join xb on xb.SPECID=ta.SPECID 
					 left join dna on dna.SPECID=ta.SPECID 
					 left join rna on rna.SPECID=ta.SPECID 
					 left join lk on lk.SPECID=ta.SPECID 
					 ";
            return tsql;
            #endregion
        }

        private void GetSumData()
        {
            string tmpSql = GetSumSql(1);
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

            tpQuery.TabPages[3].Text = "�����Ϣ (��: " + neuSpread2_Sheet1.RowCount.ToString() + " ��)";

        }

        private void GetStockData(int tabIndex)
        {
            string tmpSql = GetStockSql(tabIndex);
            if (rbtBld.Checked)
            {
                tmpSql = string.Format(tmpSql, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1).ToString(), "B");

            }
            else
            {
                tmpSql = string.Format(tmpSql, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1).ToString(), "O");
            }

            DataSet ds = new DataSet();
            subMgr.ExecQuery(tmpSql, ref ds);
            neuSpread3_Sheet1.DataSource = ds;

            this.StkDataView = new DataView(ds.Tables[0]);
            neuSpread3_Sheet1.DataSource = this.StkDataView;

            for (int i = 0; i < neuSpread3_Sheet1.ColumnCount; i++)
            {
                neuSpread3_Sheet1.Columns[i].Width = 80;
            }
            neuSpread3_Sheet1.Columns[5].Visible = false;
            tpStock.Text = "�����Ϣ (�� " + neuSpread3_Sheet1.RowCount.ToString() + " ����¼";
        }

        /// <summary>
        /// �ӻ��ܵı���д����γɵ�SQL���
        /// </summary>
        /// <returns></returns>
        private string GetSqlFromSum()
        {
            string tmp = GetSql();
            string specId1 = neuSpread2_Sheet1.Cells[neuSpread2_Sheet1.ActiveRowIndex, Convert.ToInt32(Cols.SPECID)].Text.Trim();
            string specId2 = neuSpread2_Sheet1.Cells[neuSpread2_Sheet1.ActiveRowIndex, Convert.ToInt32(Cols.MSSPECID)].Text.Trim();
            if (specId1 == "")
            {
                specId1 = "0";
            }
            if (specId2 == "")
            {
                specId2 = "0";
            }

            tmp += " where s.specid in (" + specId1 + "," + specId2 + ")\n ) select * from s";
            return tmp;
        }

        /// <summary>
        /// �ӿ��ı���д����γɵ�SQL���
        /// </summary>
        /// <returns></returns>
        private string GetSqlFromStock()
        {
            string tmp = GetSql();
            string specId1 = neuSpread3_Sheet1.Cells[neuSpread3_Sheet1.ActiveRowIndex,Convert.ToInt32(StoPos.SPECID)].Text.Trim();
            if (specId1 == "")
            {
                specId1 = "0";
            }

            tmp += " where s.specid in (" + specId1 + ")\n ) select * from t";

            if (!string.IsNullOrEmpty(this.bldFilter))
            {
                tmp += this.bldFilter;
            }
            return tmp;
        }

        /// <summary>
        /// �ӿ��ı���д����γɵ�SQL���
        /// </summary>
        /// <returns></returns>
        private string GetStockFilterSql()
        {
            string tmp = GetSql();
            string specId1 = string.Empty;
            for (int k = 0; k < neuSpread3_Sheet1.RowCount; k++)
            {
                if (k == neuSpread3_Sheet1.RowCount - 1)
                {
                    specId1 += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.SPECID)].Text.Trim() + ")";
                }
                else
                {
                    specId1 += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.SPECID)].Text.Trim() + ",";
                }
            }
            
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
                /*
                strVisit = @"select distinct V.CARD_NO ������,P.NAME ����,SO.SPEC_NO �걾��,
                        (SELECT SD.DISEASENAME FROM SPEC_DISEASETYPE SD WHERE SD.DISEASETYPEID = SO.DISEASETYPEID) ����,
                        N.SECOND_ICD ��̬��,N.PERIOR_CODE ����,N.ICD_CODE �����,N.DIAG_NAME �������,
                        (SELECT NY.NAME FROM COM_DICTIONARY NY WHERE NY.CODE = D.CIRCS AND NY.TYPE = 'CASE07') һ�����,
                        (SELECT NY.NAME FROM COM_DICTIONARY NY WHERE NY.CODE = D.SYMPTOM AND NY.TYPE = 'CASE10') ֢״����,
                        D.DEAD_TIME ����ʱ��,(SELECT NY.NAME FROM COM_DICTIONARY NY WHERE NY.CODE = D.DEAD_REASON AND NY.TYPE = 'CASE08') ����,
                        D.VISIT_TIME ĩ�����ʱ��,D.RECRUDESCE_TIME ����ʱ��,
                        (SELECT NY.NAME  FROM COM_DICTIONARY NY WHERE NY.CODE = D.TRANSFER_POSITION AND NY.TYPE = 'CASE11') ת�Ʋ�λ
                        FROM MET_CAS_VISITRECORD D,MET_CAS_VISIT V,SPEC_PATIENT P,MET_CAS_DIAGNOSE N, SPEC_SOURCE SO
                        WHERE D.CARD_NO in  ('{0} AND SO.INPATIENT_NO = N.INPATIENT_NO  AND SO.PATIENTID = P.PATIENTID
                        AND D.CARD_NO = V.CARD_NO AND V.CARD_NO = P.CARD_NO AND D.CARD_NO = P.CARD_NO 
                        AND D.VISIT_TIME = V.LAST_TIME AND N.DIAG_KIND = '1' and SO.SPECID in ({1}";
                */
                //Ҫ��û����õ�ҲҪ�����������������Ӵ���SQL��ѯlingk20110831����������ݿ�����
                strVisit = @"with ta as 
                        (select D.CARD_NO ������,(SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.CIRCS AND N.TYPE = 'CASE07') һ�����,
                        (SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.SYMPTOM AND N.TYPE = 'CASE10') ֢״����,
                        D.DEAD_TIME ����ʱ��,(SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.DEAD_REASON AND N.TYPE = 'CASE08') ����,
                        D.VISIT_TIME ĩ�����ʱ��,D.RECRUDESCE_TIME ����ʱ��,
                        (SELECT  N.NAME  FROM COM_DICTIONARY N WHERE N.CODE = D.TRANSFER_POSITION AND N.TYPE = 'CASE11') ת�Ʋ�λ
                        FROM MET_CAS_VISITRECORD D,MET_CAS_VISIT V where D.CARD_NO = V.CARD_NO AND D.VISIT_TIME = V.LAST_TIME
                        and D.CARD_NO in ('{0}),
                        tb as
                        (select distinct P.CARD_NO ������,P.PNAME ����,SO.SPEC_NO �걾��,(SELECT SD.DISEASENAME FROM SPEC_DISEASETYPE SD WHERE SD.DISEASETYPEID = SO.DISEASETYPEID) ����,
                        DN.SECOND_ICD ��̬��,DN.PERIOR_CODE ����,DN.ICD_CODE �����,DN.DIAG_NAME �������
                        FROM SPEC_PATIENT P join SPEC_SOURCE SO on SO.PATIENTID = P.PATIENTID
                        left join MET_CAS_DIAGNOSE DN on SO.INPATIENT_NO = DN.INPATIENT_NO
                        where DN.DIAG_KIND = '1' AND DN.OPER_TYPE = '2'
                        and P.CARD_NO in  ('{0}
                        and SO.SPECID in ({1})
                        select tb.������,tb.����,tb.�걾��,tb.����,tb.��̬��,tb.����,tb.�����,tb.�������,
                        (select ta.һ����� from ta where ta.������ = tb.������) һ�����,
                        (select ta.֢״���� from ta  where ta.������ = tb.������) ֢״����,
                        (select ta.����ʱ�� from ta  where ta.������ = tb.������) ����ʱ��,
                        (select ta.���� from ta  where ta.������ = tb.������) ����,
                        (select ta.ĩ�����ʱ�� from ta  where ta.������ = tb.������) ĩ�����ʱ��,
                        (select ta.����ʱ�� from ta  where ta.������ = tb.������) ����ʱ��,
                        (select ta.ת�Ʋ�λ from ta  where ta.������ = tb.������) ת�Ʋ�λ
                        from tb";
                string strSpecId = string.Empty;
                string strCardNo = string.Empty;

                if (this.neuSpread3_Sheet1.Rows.Count > 0)
                {
                    for (int k = 0; k < neuSpread3_Sheet1.RowCount; k++)
                    {
                        if (k == neuSpread3_Sheet1.RowCount - 1)
                        {
                            strCardNo += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.������)].Text.Trim() + "')";
                            strSpecId += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.SPECID)].Text.Trim() + ")";
                        }
                        else
                        {
                            strCardNo += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.������)].Text.Trim() + "','";
                            strSpecId += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.SPECID)].Text.Trim() + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(strCardNo))
                    {
                        strVisit = string.Format(strVisit, strCardNo, strSpecId);
                        sheetView2.RowCount = 0;
                        DataSet dsTmp = new DataSet();
                        subMgr.ExecQuery(strVisit, ref dsTmp);

                        if (dsTmp != null && dsTmp.Tables.Count > 0)
                        {
                            //sheetView2.DataSource = dsTmp;

                            DataView dvTmp = new DataView(dsTmp.Tables[0]);

                            if (dvTmp.Count > 0)
                            {
                                sheetView2.DataSource = dvTmp;
                                this.tpgVisit.Text = "�����Ϣ (��: " + dvTmp.Count.ToString() + " ��)";
                            }
                        }
                        else
                        {
                            this.tpgVisit.Text = "�����Ϣ (��: 0��)";
                        }
                    }
                    else
                    {
                        this.tpgVisit.Text = "�����Ϣ (��: 0��)";
                    }
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
            #region ���Σ��õ�����������
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾ԴID), "�걾ԴID");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.Դ����), "Դ����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");
            ////neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��װ����), "��װ����"); 
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾����), "�걾����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����ʱ��), "����ʱ��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�ʹ�ҽ��), "�ʹ�ҽ��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.������), "������");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾����), "�걾����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾��), "�걾��");
            ////neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��������), "��������");
            //if (!rbtBld.Checked)
            //{
            //    neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.ȡ������), "ȡ������");
            //    neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��������), "��������");
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = false;
            //    //chkOrgDet.Visible = true;
            //    //chkOrgDet.Checked = false;
            //    //chkColPos.Visible = true;
            //    //chkColPos.Checked = false;
            //    orgOrBld = "O";
            //}
            //if (rbtBld.Checked)
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.ȡ������)].Visible = false;
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��������)].Visible = false;
            //    //chkOrgDet.Visible = false;
            //    //chkOrgDet.Checked = false;
            //    //chkColPos.Visible = false;
            //    //chkColPos.Checked = false;
            //    //chkColPos
            //    orgOrBld = "B";
            //}
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.SPECTYPEID), "SPECTYPEID");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.������), "������");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ע), "��ע");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���), "���");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�ڿ�״̬), "�ڿ�״̬");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾����), "�걾����");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�ϴη���ʱ��), "�ϴη���ʱ��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.Լ������ʱ��), "Լ������ʱ��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.ת�Ʋ�λ), "ת�Ʋ�λ");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾����), "�걾����");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��������), "��������");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾��ϸ����), "�걾��ϸ����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���ƿ���), "���ƿ���");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.סԺ��ˮ��), "סԺ��ˮ��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ǰ���ƽ׶�), "��ǰ���ƽ׶�");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��������), "��������");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�����), "�����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�������̬��), "�������̬��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���1), "���1");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���1��̬��), "���1��̬��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���2), "���1");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���2��̬��), "���2��̬��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ϱ�ע), "��ϱ�ע");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�Ա�), "�Ա�");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.סַ), "סַ");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����״��), "����״��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ͥ�绰), "��ͥ�绰");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ϵ�绰), "��ϵ�绰");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��), "��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��), "��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾��λ��), "�걾��λ��");

            //for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            //{
            //    neuSpread1_Sheet1.Rows[i].Height = 25;
            //    string cureState = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.��ǰ���ƽ׶�)].Text.Trim();
            //    char[] tmp = cureState.ToCharArray();
            //    cureState = "";
            //    foreach (char c in tmp)
            //    {
            //        string p = c.ToString().Trim();
            //        if (p == "")
            //        {
            //            continue;
            //        }
            //        try
            //        {
            //            cureState += Enum.GetName(typeof(Constant.GetPeriod), Convert.ToInt32(p));
            //        }
            //        catch
            //        {

            //        }
            //    }
            //    try
            //    {
            //        neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.��ǰ���ƽ׶�)].Text = cureState;
            //        //string boxBarCode = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.�걾��λ��)].Text.Trim();
            //        //neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.�걾��λ��)].Text = ParseLocation.ParseSpecBox(boxBarCode);
            //        if (detail == 7)
            //        {
            //            neuSpread1_Sheet1.Cells[i, 0].Value = "true";
            //        }
            //    }
            //    catch
            //    { }
            //}
            //for (int i = 1; i < neuSpread1_Sheet1.ColumnCount; i++)
            //{
            //    neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
            //    neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
            //}
            //tpQuery.SelectedIndex = 2;
            //specIdList = new List<string>();
            //tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
            //if (detail == 2 || detail == 9 || detail == 8)
            //{
            //    for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            //    {
            //        string specid = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.�걾ԴID)].Text.Trim();
            //        if (!specIdList.Contains(specid))
            //        {
            //            specIdList.Add(specid);
            //        }
            //    }
            //    specIds = "(0,";
            //    for (int i = 0; i < specIdList.Count; i++)
            //    {
            //        if (i == specIdList.Count - 1)
            //        {
            //            specIds += specIdList[i] + ")";
            //        }
            //        else
            //        {
            //            specIds += specIdList[i] + ",";
            //        }

            //    }
            //}
            #endregion
            this.BindingSheet1(detail);
        }

        private void BindingSheet1(int detail)
        {
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾ԴID), "�걾ԴID");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.Դ����), "Դ����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��װ����), "��װ����"); 
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾����), "�걾����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����ʱ��), "����ʱ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�ʹ�ҽ��), "�ʹ�ҽ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.������), "������");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾����), "�걾����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾��), "�걾��");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��������), "��������");
            if (!rbtBld.Checked)
            {
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.ȡ������), "ȡ������");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��������), "��������");
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = false;
                //chkOrgDet.Visible = true;
                //chkOrgDet.Checked = false;
                //chkColPos.Visible = true;
                //chkColPos.Checked = false;
                orgOrBld = "O";
            }
            if (rbtBld.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.ȡ������)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��������)].Visible = false;
                //chkOrgDet.Visible = false;
                //chkOrgDet.Checked = false;
                //chkColPos.Visible = false;
                //chkColPos.Checked = false;
                //chkColPos
                orgOrBld = "B";
            }
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.SPECTYPEID), "SPECTYPEID");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.������), "������");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ע), "��ע");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���), "���");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�ڿ�״̬), "�ڿ�״̬");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾����), "�걾����");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�ϴη���ʱ��), "�ϴη���ʱ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.Լ������ʱ��), "Լ������ʱ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.ת�Ʋ�λ), "ת�Ʋ�λ");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾����), "�걾����");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��������), "��������");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾��ϸ����), "�걾��ϸ����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���ƿ���), "���ƿ���");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.סԺ��ˮ��), "סԺ��ˮ��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ǰ���ƽ׶�), "��ǰ���ƽ׶�");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��������), "��������");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�����), "�����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�������̬��), "�������̬��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���1), "���1");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���1��̬��), "���1��̬��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���2), "���1");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.���2��̬��), "���2��̬��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ϱ�ע), "��ϱ�ע");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�Ա�), "�Ա�");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.סַ), "סַ");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����״��), "����״��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.����), "����");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ͥ�绰), "��ͥ�绰");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��ϵ�绰), "��ϵ�绰");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��), "��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.��), "��");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.�걾��λ��), "�걾��λ��");

            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                neuSpread1_Sheet1.Rows[i].Height = 25;
                string cureState = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.��ǰ���ƽ׶�)].Text.Trim();
                char[] tmp = cureState.ToCharArray();
                cureState = "";
                foreach (char c in tmp)
                {
                    string p = c.ToString().Trim();
                    if (p == "")
                    {
                        continue;
                    }
                    try
                    {
                        cureState += Enum.GetName(typeof(Constant.GetPeriod), Convert.ToInt32(p));
                    }
                    catch
                    {

                    }
                }
                try
                {
                    neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.��ǰ���ƽ׶�)].Text = cureState;
                    //string boxBarCode = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.�걾��λ��)].Text.Trim();
                    //neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.�걾��λ��)].Text = ParseLocation.ParseSpecBox(boxBarCode);
                    if (detail == 7)
                    {
                        neuSpread1_Sheet1.Cells[i, 0].Value = "true";
                    }
                }
                catch
                {
                }
            }
            for (int i = 1; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
                neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
            }
            tpQuery.SelectedIndex = 2;
            specIdList = new List<string>();
            tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
            if (detail == 2 || detail == 9 || detail == 8)
            {
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string specid = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.�걾ԴID)].Text.Trim();
                    if (!specIdList.Contains(specid))
                    {
                        specIdList.Add(specid);
                    }
                }
                specIds = "(0,";
                for (int i = 0; i < specIdList.Count; i++)
                {
                    if (i == specIdList.Count - 1)
                    {
                        specIds += specIdList[i] + ")";
                    }
                    else
                    {
                        specIds += specIdList[i] + ",";
                    }

                }
            }

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
            this.InitSheet(sheetView1);
            this.InitSheet1();

            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizProcess.Integrate.Manager dpMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //��ѯ�����б�
            ArrayList countryList = new ArrayList();
            countryList = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.txtNationlity.AddItems(countryList);

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

            ArrayList alDept = new ArrayList();
            alDept = dpMgr.GetDepartment();
            if ((alDept != null) && (alDept.Count > 0))
            {
                this.cmbAppDept.AddItems(alDept);
            }
            ArrayList alEmpl = new ArrayList();
            alEmpl = dpMgr.QueryEmployeeAll();
            if ((alEmpl != null) && (alEmpl.Count > 0))
            {
                this.cmbOper.AddItems(alEmpl);
            }
            System.DateTime dt = Convert.ToDateTime(con.GetSysDate());
            this.dtEnd.Value = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
            this.dtStart.Value = this.dtEnd.Value.AddSeconds(1).AddDays(-8);
        }

        private void LoadPos()
        {
            if (chkPos.Checked)
            {
                neuSpread1_Sheet1.ColumnCount++;
                neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].Label = " �걾λ������";
                neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].Width = 300;
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    string specId = neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                    string specTypeId = neuSpread1_Sheet1.Cells[i, neuSpread1_Sheet1.ColumnCount - 2].Text.Trim();
                    ArrayList arrSub = subMgr.GetSubSpecBySpecId(specId);
                    Dictionary<string, List<string>> dicPos = new Dictionary<string, List<string>>();

                    foreach (SubSpec s in arrSub)
                    {
                        if (s.SpecTypeId.ToString() != specTypeId)
                        {
                            continue;
                        }
                        SpecBoxManage boxMgr = new SpecBoxManage();
                        SpecBox box = new SpecBox();
                        box = boxMgr.GetBoxById(s.BoxId.ToString());
                        string loc = ParseLocation.ParseSpecBox(box.BoxBarCode);
                        if (dicPos.ContainsKey(loc))
                        {
                            dicPos[loc].Add(s.SubBarCode);
                        }
                        else
                        {
                            dicPos.Add(loc, new List<string>());
                            dicPos[loc].Add(s.SubBarCode);
                        }
                    }
                    string subLoc = "";
                    foreach (KeyValuePair<string, List<string>> kv in dicPos)
                    {
                        subLoc += kv.Key + ",\n";
                        foreach (string s in kv.Value)
                        {
                            subLoc += " " + s + ",";
                        }
                    }
                    neuSpread1_Sheet1.Cells[i, neuSpread1_Sheet1.Columns.Count - 1].Text = subLoc;
                    //neuSpread1_Sheet1[i]
                }
            }
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
            if (neuSpread3_Sheet1.DataSource != null)
            {
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.Դ����)].Visible = chkHisBarCode.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.SPECID)].Visible = false;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.SPECID)].Visible = chkSpecId.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.�ʹ�ҽ��)].Visible = chkSendDoc.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.����)].Visible = chkDept.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.������)].Visible = chkOperName.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.����ʱ��)].Visible = chkColTime.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.��ע)].Visible = chkComment.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.���)].Visible = chkMatch.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.סԺ��ˮ��)].Visible = chkInHosNum.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.��ǰ���ƽ׶�)].Visible = chkCureDet.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.��������)].Visible = chkOperDet.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.�Ա�)].Visible = chkGender.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.����)].Visible = chkNationality.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.����)].Visible = chkNation.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.סַ)].Visible = chkAdress.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.����״��)].Visible = chkMarr.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.����)].Visible = chkBirth.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.��ϵ�绰)].Visible = chkContNum.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.��ͥ�绰)].Visible = chkHomeNum.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.�����)].Visible = chkMain.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.�������̬��)].Visible = chkMod.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.���1)].Visible = chkMain1.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.���1��̬��)].Visible = chkMod1.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.���2)].Visible = chkMain2.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.���2��̬��)].Visible = chkMod2.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.��ϱ�ע)].Visible = chkMain3.Checked;
            }
            
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾ԴID)].Visible = chkSpecId.Checked;
            
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.Դ����)].Visible = chkHisBarCode.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = chkDisType.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�ʹ�ҽ��)].Visible = chkSendDoc.Checked;
               
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = chkDept.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.������)].Visible = chkOperName.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����ʱ��)].Visible = chkColTime.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ע)].Visible = chkComment.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���)].Visible = chkMatch.Checked;
                
            

            if (chkSpecNo.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾��)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾��)].Visible = false;
            }

            if (chkSpecCode.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = false;
            }

            if (chkSpecType.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = false;
            }

            if (chkSpecType.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = false;
            }

            if (chkStatus.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�ڿ�״̬)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�ڿ�״̬)].Visible = false;
            }

            if (chkTumorPor.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��������)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��������)].Visible = false;
            }

            if (chkTumor.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = false;
            }


            if (chkLastRet.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�ϴη���ʱ��)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�ϴη���ʱ��)].Visible = false;
            }

            if (chkRetTime.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.Լ������ʱ��)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.Լ������ʱ��)].Visible = false;
            }

            if (chkTransPos.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.ת�Ʋ�λ)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.ת�Ʋ�λ)].Visible = false;
            }

            if (chkCap.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾����)].Visible = false;
            }
            if (chkOrgDet.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��������)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��������)].Visible = false;
            }

            //if (chkColPos.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.ȡ������)].Visible = chkColPos.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.ȡ������)].Visible = false;
            //}

            if (chkDet.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾��ϸ����)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾��ϸ����)].Visible = false;
            }

            if (chkName.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = false;
            }


            ///
            if (chkCardNo.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.������)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.������)].Visible = false;
            }

            if (chkIcCardNo.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���ƿ���)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���ƿ���)].Visible = false;
            }

            //if (chkInHosNum.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.סԺ��ˮ��)].Visible = chkInHosNum.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.סԺ��ˮ��)].Visible = false;
            //}

            //if (chkCureDet.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ǰ���ƽ׶�)].Visible = chkCureDet.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ǰ���ƽ׶�)].Visible = false;
            //}

            //if (chkOperDet.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��������)].Visible = chkOperDet.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��������)].Visible = false;
            //}

            //if (chkGender.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�Ա�)].Visible = chkGender.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�Ա�)].Visible = false;
            //}

            //if (chkNationality.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = chkNationality.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = false;
            //}

            //if (chkNation.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = chkNation.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = false;
            //}

            //if (chkAdress.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.סַ)].Visible = chkAdress.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.סַ)].Visible = false;
            //}

            //if (chkMarr.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����״��)].Visible = chkMarr.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����״��)].Visible = false;
            //}

            //if (chkBirth.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = chkBirth.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.����)].Visible = false;
            //}

            //if (chkContNum.Checked)
            //{
              
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ϵ�绰)].Visible = chkContNum.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ϵ�绰)].Visible = false;
            //}//chkHomeNum

            //if (chkHomeNum.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ͥ�绰)].Visible = chkHomeNum.Checked;
                
            //  neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ͥ�绰)].Visible = true;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ͥ�绰)].Visible = false;
            //}

            ////
            //if (chkMain.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�����)].Visible = chkMain.Checked;
                
                //neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�����)].Visible = true;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�����)].Visible = false;
            //}
            //if (chkMod.Checked)
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�������̬��)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�������̬��)].Visible = chkMod.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�������̬��)].Visible = false;
            //}
            //if (chkMain1.Checked)
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���1)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���1)].Visible = chkMain1.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���1)].Visible = false;
            //}

            //if (chkMod1.Checked)
            //{
               // neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���1��̬��)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���1��̬��)].Visible = chkMod1.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���1��̬��)].Visible = false;
            //}
            //if (chkMain2.Checked)
            //{
                //neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���2)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���2)].Visible = chkMain2.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���2)].Visible = false;
            //}

            //if (chkMod2.Checked)
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���2��̬��)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���2��̬��)].Visible = chkMod2.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.���2��̬��)].Visible = false;
            //}
            //if (chkMain3.Checked)
            //{
                //neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ϱ�ע)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ϱ�ע)].Visible = chkMain3.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��ϱ�ע)].Visible = false;
            //}

            ////
            if (chkRow.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��)].Visible = false;
            }
            if (chkCol.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.��)].Visible = false;
            }
            if (chkLocDet.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾��λ��)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.�걾��λ��)].Visible = false;
            }
            #endregion
        }

        private void Query(int index)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ϸ��Ϣ�����Ժ�...");
            Application.DoEvents();
            GetDetailData(index);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (cbxPD.Checked)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
                Application.DoEvents();
                GetSumData();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
            Application.DoEvents();
            GetStockData(1);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            #region �����Ϣ lingk20110829
            if (cbxVisitInfo.Checked)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
                Application.DoEvents();
                this.GetVisitRecord();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            #endregion

            for (int i = 0; i < neuSpread2_Sheet1.Columns.Count; i++)
            {
                neuSpread2_Sheet1.ColumnHeader.Rows[0].Height = 30;
                neuSpread2_Sheet1.Columns[i].AllowAutoFilter = true;
                neuSpread2_Sheet1.Columns[i].AllowAutoSort = true;
            }


            for (int i = 0; i < neuSpread3_Sheet1.Columns.Count; i++)
            {
                neuSpread3_Sheet1.ColumnHeader.Rows[0].Height = 30;
                neuSpread3_Sheet1.Columns[i].AllowAutoFilter = true;
                neuSpread3_Sheet1.Columns[i].AllowAutoSort = true;
            }

            for (int i = 0; i < this.sheetView1.Columns.Count; i++)
            {
                sheetView1.ColumnHeader.Rows[0].Height = 30;
                sheetView1.Columns[i].AllowAutoFilter = true;
                sheetView1.Columns[i].AllowAutoSort = true;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 30;
                neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
                neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
            }

            for (int i = 0; i < this.sheetView2.Columns.Count; i++)
            {
                if (i <= 6)
                {
                    this.sheetView2.Columns[i].Width = 78;
                }
                if (i == 0)
                {
                    this.sheetView2.Columns[i].Width = 88;
                }
                sheetView2.ColumnHeader.Rows[0].Height = 30;
                sheetView2.Columns[i].AllowAutoFilter = true;
                sheetView2.Columns[i].AllowAutoSort = true;
            }

            SetVisible();

        }

        private void AddSpec()
        {
            for (int i = 0; i < neuSpread1_Sheet1.RowCount;i++ )
            {
                string inStatus = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.�ڿ�״̬)].Value.ToString();
                if (inStatus == "����" || inStatus == "���")
                {
                    continue;
                }
                object chk = neuSpread1_Sheet1.Cells[i, 0].Value;
                if (chk == null || chk.ToString().ToUpper() == "FALSE" || chk.ToString() == "0")
                {
                    continue;
                }
                else
                {
                   sheetView1.Rows.Add(sheetView1.RowCount,1);
                   for (int j = 0; j < sheetView1.ColumnCount; j++)
                   {
                       sheetView1.Cells[sheetView1.RowCount - 1, j].Text = neuSpread1_Sheet1.Cells[i, j].Text;
                       sheetView1.Columns[j].Visible = neuSpread1_Sheet1.Columns[j].Visible;
                       string boxBarCode = sheetView1.Cells[sheetView1.RowCount - 1, Convert.ToInt32(CurPos.�걾��λ��)].Text;
                       try
                       {
                           string loc = ParseLocation.ParseSpecBox(boxBarCode);
                           sheetView1.Cells[sheetView1.RowCount - 1, Convert.ToInt32(CurPos.�걾��λ��)].Text = loc;

                       }
                       catch
                       { }
                   }
                }
            }

            sheetView1.Columns[Convert.ToInt32(CurPos.chk)].Visible = false;
            sheetView1.Columns[Convert.ToInt32(CurPos.�걾��λ��)].Visible = true;
            sheetView1.Columns[Convert.ToInt32(CurPos.��)].Visible = true;
            sheetView1.Columns[Convert.ToInt32(CurPos.��)].Visible = true;

            this.tabPage5.Text = "��ѡ�걾 (��:" + sheetView1.RowCount.ToString() + " )";

        }

        /// <summary>
        /// �����Ѵ�걾
        /// </summary>
        private void SaveChoosedSub()
        {
            if (sheetView1.RowCount == 0)
            {
                MessageBox.Show("û����ѡ�걾������Ѳ�ѯ���ݼ�����ѡ�걾��");
                return;
            }
            else
            {
                FS.HISFC.Models.Base.Employee loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                int tmp = 0;
                if (txtApplyNum.Text.Trim() != "")
                {
                    try
                    {
                        tmp = Convert.ToInt32(txtApplyNum.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("���뵥�ű���������,��������д");
                        return;
                    }
                }
                SpecOutOper outOper = new SpecOutOper(loginPerson);
                outOper.IsDirect = false;
                UserApplyManage userApplyManage = new UserApplyManage();
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                try
                {
                    outOper.ApplyNum = tmp.ToString();
                    outOper.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    ApplyTable appTmp = outOper.QueryApplyByID(tmp.ToString());
                    if (appTmp != null)
                    {
                        if (appTmp.ImpProcess.StartsWith("O"))
                        {
                            MessageBox.Show("���뵥��Ϊ[" + tmp.ToString() + "]�����뵥�Ѿ����������ٴ��޸ģ�");
                            return;
                        }
                        if (appTmp.User03.ToString() == "�ѳ���")
                        {
                            MessageBox.Show("���뵥��Ϊ[" + tmp.ToString() + "]�����뵥�Ѿ����ⲻ���ٴ��޸ģ�");
                            return;
                        }
                    }
                    string rbtChd = "ɸѡ";
                    ArrayList appTab = outOper.GetSubSpecOut(tmp.ToString());
                    if ((appTab != null) && (appTab.Count > 0))
                    {
                        //�ж��Ƿ�Ϊ׷�����ݣ�specOut.Oper="Imp"����״̬��,���Ǹ������ݣ�ԭspecOut.Oper="Del"ɾ��״̬,������specOut.Oper="Merge"�ϲ�״̬��
                        if (rbtEnd.Checked) //|| (this.isAddAppOutData))
                        {
                            string updSql = @"update SPEC_APPLY_OUT set OPER = 'Del' 
                                          where RELATEID = {0}";
                            updSql = string.Format(updSql, tmp.ToString());
                            if (outOper.UpdateSpecOut(updSql) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����ԭ����������ʧ�ܣ�");
                                return;
                            }
                            rbtChd = "����ɸѡ";
                            outOper.Oper = "Merge";
                        }
                        if (rbtFirst.Checked) //&& (!this.isAddAppOutData))
                        {
                            rbtChd = "׷��ɸѡ";
                            outOper.Oper = "Imp";
                        }
                    }
                    else
                    {
                        outOper.Oper = "Imp";
                    }
                    for (int i = 0; i < sheetView1.RowCount; i++)
                    {
                        string specBarCode = sheetView1.Cells[i, Convert.ToInt32(CurPos.�걾����)].Text.Trim();
                        if (!string.IsNullOrEmpty(specBarCode))
                        {
                            if (rbtFirst.Checked)
                            {
                                bool sign = false;
                                foreach (FS.HISFC.Models.Speciment.SpecOut outTmp in appTab)
                                {
                                    if (specBarCode == outTmp.SubSpecBarCode)
                                    {
                                        sign = true;
                                        break;
                                    }
                                }
                                if (sign)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("׷�ӵı걾,�걾��Ϊ[" + specBarCode + "]�ı걾�Ѿ����ڣ�׷��ʧ�ܣ�");
                                    return;
                                }
                            }
                            SubSpec tmpSpec = new SubSpec();
                            if (outOper.GetSubSpecById(specBarCode, ref tmpSpec) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���ݱ걾�����ȡ�걾����");
                                return;
                            }
                            if (tmpSpec != null)
                            {
                                if (outOper.SaveApplyOutInfo(tmpSpec, 1, "2") <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("������ѡ�걾����");
                                    return;
                                }
                            }
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���ݱ걾�����ȡ�걾���������Ϊ[" + specBarCode + "]");
                                return;
                            }
                        }
                    }
                    //������ȱ���ɸ��
                    UserApply userApply = new UserApply();
                    userApply.ApplyId = tmp;
                    userApply.UserId = loginPerson.ID.ToString();
                    if (rbtChd == "ɸѡ")
                    {
                        userApply.ScheduleId = "Q3";
                    }
                    else if (rbtChd == "����ɸѡ")
                    {
                        userApply.ScheduleId = "Q5";
                    }
                    else if (rbtChd == "׷��ɸѡ")
                    {
                        userApply.ScheduleId = "Q4";
                    }
                    userApply.Schedule = rbtChd;
                    userApply.CurDate = System.DateTime.Now;
                    userApply.OperId = loginPerson.ID;
                    userApply.OperName = loginPerson.Name;

                    int result = -1;
                    if (outOper.Oper == "Merge")
                    {
                        string strSql = @"update SPEC_USERAPPLICATION set SCHEDULE = '{0}'
                                          where APPLICATIONID = {1} and (SCHEDULE ='��ɸ' 
                                          or SCHEDULE = 'ɸѡ')";
                        strSql = string.Format(strSql, rbtChd, tmp);
                        result = userApplyManage.UpdateUserApply(strSql);
                    }
                    else
                    {
                        string sequence = "";
                        userApplyManage.GetNextSequence(ref sequence);
                        userApply.UserAppId = Convert.ToInt32(sequence);
                        result = userApplyManage.InsertUserApply(userApply);
                    }
                    if (result == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������ȱ�ʧ��!", userApply.Schedule);
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("�ɹ�������ѡ�걾��");
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������ѡ�걾�쳣����");
                    return;
                }
            }
        }

        /// <summary>
        /// �����ѡ�걾
        /// </summary>
        private void Clear()
        {
            sheetView1.RowCount = 0;
            this.tabPage5.Text = "��ѡ�걾 (��:" + sheetView1.RowCount.ToString() + " )";
        }

        public override int Query(object sender, object neuObject)
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
                    return 0;
                }
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
             
            return base.Query(sender, neuObject);
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
                case 4:
                    

                    if (dr == DialogResult.OK)
                    {                      
                        
                        FarPoint.Win.ComplexBorderSide bs = new FarPoint.Win.ComplexBorderSide(Color.Black, 1);

                      FarPoint.Win.ComplexBorder bord = new FarPoint.Win.ComplexBorder(bs, bs, bs, bs);

                        for (int i = 0; i < ns.Sheets[0].ColumnCount; i++)
                        {
                            ns.Sheets[0].Columns[i].Border = bord;                            
                        }

                        ns.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                    }
                    break;
                case 2:
                    neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders); 
                    //SpreadToExlHelp.ExportExl(neuSpread1_Sheet1, Convert.ToInt32(CurPos.chk), new int[] { },saveFileDiaglog.FileName,true);
                    break;
                case 6:
                    neuSpread5.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                    break;
                default:
                    //SpreadToExlHelp.ExportExl(sheetView1, Convert.ToInt32(CurPos.chk), new int[] { }, saveFileDiaglog.FileName,false);
                    neuSpread4.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                    break;
            }
             
            return base.Export(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("λ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡԤ��, true, false, null);
            this.toolBarService.AddToolButton("��ӡλ����Ϣ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("�걾����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("������ѡ�걾", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("������������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);
            this.toolBarService.AddToolButton("������ѡ�걾", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Mģ��ɾ��, true, false, null);
            this.toolBarService.AddToolButton("�����ѡ�걾", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

            this.toolBarService.AddToolButton("Ѫ�걾ɸѡ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Wδ��ɵ�, true, false, null);
            this.toolBarService.AddToolButton("��֯�걾ɸѡ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z�ٻ�, true, false, null);
            this.toolBarService.AddToolButton("������ѯ˳��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S�豸���, true, false, null);

            this.toolBarService.AddToolButton("���и�����������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S�ֶ�¼��, true, false, null);

            this.toolBarService.AddToolButton("��ѯ��������Ϣ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C����, true, false, null);

            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "���и�����������":
                    #region
                    frmUserSelectMuil frmSelect = new frmUserSelectMuil();
                    ArrayList SelectField = new ArrayList();
                    ArrayList arrSelectInfo = new ArrayList();
                    if (this.DtDataView.Count != 0 && (tpQuery.TabPages[2].Visible == true || tpQuery.TabPages[4].Visible == true))
                    {
                        if (tpQuery.TabPages[2].Visible == true)//����ϸ��Ϣfarpoint��ɸѡ
                        {                            
                            for (int n = 1; n < neuSpread1_Sheet1.ColumnCount; n++)
                            {
                                if (neuSpread1_Sheet1.Columns[n].Visible == true)
                                {
                                    cTable cTableTemp = new cTable();
                                    cTableTemp.Item = neuSpread1_Sheet1.Columns[n].Label.ToString();
                                    ArrayList arrForCTableTemp = new ArrayList();
                                    this.DtDataView = neuSpread1_Sheet1.GetDataView(true);
                                    if (neuSpread1_Sheet1.RowCount >= 1)
                                    {
                                        for(int m = 0; m < neuSpread1_Sheet1.RowCount; m++)
                                        {
                                            bool fign = false;
                                            foreach (string cc in arrForCTableTemp)
                                            {
                                                if (neuSpread1_Sheet1.Cells[m, n].Text.Trim().ToString() == "" || cc == neuSpread1_Sheet1.Cells[m, n].Text.Trim().ToString())
                                                {
                                                    fign = true;
                                                }
                                            }
                                            if (!fign)
                                            {
                                                arrForCTableTemp.Add(neuSpread1_Sheet1.Cells[m, n].Text.Trim().ToString());
                                            }
                                        }
                                        cTableTemp.ArrSelectBy = arrForCTableTemp;
                                    }
                                    SelectField.Add(neuSpread1_Sheet1.Columns[n].Label.ToString());
                                    arrSelectInfo.Add(cTableTemp);
                                }
                            }
                            frmSelect.Arrlist = SelectField;
                            frmSelect.ArrcTableInfo = arrSelectInfo;
                            frmSelect.StartPosition = FormStartPosition.CenterScreen;
                            frmSelect.ShowDialog();
                            if (!string.IsNullOrEmpty(frmSelect.SelectSql))
                            {
                                if (!string.IsNullOrEmpty(this.DtDataView.RowFilter))
                                {
                                    DialogResult result = MessageBox.Show("��ǰɸѡ�Ƿ����֮ǰ����������", "��ʾ", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        this.DtDataView.RowFilter = frmSelect.SelectSql;
                                        tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
                                    }
                                    else
                                    {
                                        this.DtDataView.RowFilter +=" AND "+ frmSelect.SelectSql;
                                        tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
                                    }
                                }
                                else
                                {
                                    this.DtDataView.RowFilter = frmSelect.SelectSql;
                                    tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
                                }
                            }
                        }
                        else//�Կ����Ϣfarpoint��ɸѡ
                        {
                            for (int n = 1; n < neuSpread3_Sheet1.ColumnCount; n++)
                            {
                                if (neuSpread3_Sheet1.Columns[n].Visible == true)
                                {
                                    cTable cTableTemp = new cTable();
                                    cTableTemp.Item = neuSpread3_Sheet1.Columns[n].Label.ToString();
                                    ArrayList arrForCTableTemp = new ArrayList();
                                    //this.DtDataView = neuSpread3_Sheet1.GetDataView(true);
                                    if (neuSpread3_Sheet1.RowCount >= 1)
                                    {
                                        for (int m = 0; m < neuSpread3_Sheet1.RowCount; m++)
                                        {
                                            bool fign = false;
                                            foreach (string cc in arrForCTableTemp)
                                            {
                                                if (neuSpread3_Sheet1.Cells[m, n].Text.Trim().ToString() == "" || cc == neuSpread3_Sheet1.Cells[m, n].Text.Trim().ToString())
                                                {
                                                    fign = true;
                                                }
                                            }
                                            if (!fign)
                                            {
                                                arrForCTableTemp.Add(neuSpread3_Sheet1.Cells[m, n].Text.Trim().ToString());
                                            }
                                        }
                                        cTableTemp.ArrSelectBy = arrForCTableTemp;
                                    }
                                    SelectField.Add(neuSpread3_Sheet1.Columns[n].Label.ToString());
                                    arrSelectInfo.Add(cTableTemp);
                                }
                            }
                            frmSelect.Arrlist = SelectField;
                            frmSelect.ArrcTableInfo = arrSelectInfo;
                            frmSelect.StartPosition = FormStartPosition.CenterScreen;
                            frmSelect.ShowDialog();
                            if (!string.IsNullOrEmpty(frmSelect.SelectSql))
                            {
                                if (!string.IsNullOrEmpty(this.StkDataView.RowFilter))
                                {
                                    DialogResult result = MessageBox.Show("��ǰɸѡ�Ƿ����֮ǰ����������", "��ʾ", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        this.StkDataView.RowFilter = frmSelect.SelectSql;
                                        tpQuery.TabPages[4].Text = "�����Ϣ (��: " + neuSpread3_Sheet1.RowCount.ToString() + " ��)";
                                    }
                                    else
                                    {
                                        this.StkDataView.RowFilter += " AND " + frmSelect.SelectSql;
                                        tpQuery.TabPages[4].Text = "�����Ϣ (��: " + neuSpread3_Sheet1.RowCount.ToString() + " ��)";
                                    }
                                }
                                else
                                {
                                    this.StkDataView.RowFilter = frmSelect.SelectSql;
                                    tpQuery.TabPages[4].Text = "�����Ϣ (��: " + neuSpread3_Sheet1.RowCount.ToString() + " ��)";
                                }
                            }
                            //�����¸�ʽ��Ҫ����֮ǰ��һ����
                            for (int i = 0; i < neuSpread3_Sheet1.ColumnCount; i++)
                            {
                                neuSpread3_Sheet1.Columns[i].Width = 80;
                            }
                            neuSpread3_Sheet1.Columns[5].Visible = false;
                            //if (!string.IsNullOrEmpty(frmSelect.SelectSql))
                            //{
                            //    this.DtDataView.RowFilter += " AND " + frmSelect.SelectSql;
                            //}
                            #region �������Ϣ��ɸѡͬ������ϸ��Ϣ��
                            ArrayList arrSpecId = new ArrayList();
                            for (int m = 0; m < neuSpread3_Sheet1.RowCount; m++)
                            {
                                string specId = neuSpread3_Sheet1.Cells[m, Convert.ToInt32(StoPos.SPECID)].Text.Trim();
                                if (specId != null)
                                {
                                    arrSpecId.Add(specId);
                                }
                            }
                            string tmp = GetSql();
                            string specCondition = null;
                            foreach (string specid in arrSpecId)
                            {
                                specCondition += specid + ",";
                            }
                            specCondition = specCondition.Substring(0, specCondition.Length - 1);
                            tmp += " where s.specid in (" + specCondition + ")\n ) select * from t";
                            if (!string.IsNullOrEmpty(this.bldFilter))
                            {
                                tmp += this.bldFilter;
                            }
                            neuSpread1_Sheet1.RowCount = 0;
                            DataSet ds = new DataSet();
                            subMgr.ExecQuery(tmp, ref ds);
                            neuSpread1_Sheet1.AutoGenerateColumns = false;
                            neuSpread1_Sheet1.DataAutoSizeColumns = false;
                            neuSpread1_Sheet1.DataSource = ds;

                            this.DtDataView = new DataView(ds.Tables[0]);
                            if (this.isOldData)
                            {
                                //��¼ԭ����ϸ��dataview
                                this.oldDv = this.DtDataView;
                            }
                            neuSpread1_Sheet1.DataSource = this.DtDataView;
                            tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
                            #endregion
                        }
                    }
                    #endregion
                    break;

                case "������ѯ˳��":
                    #region
                    frmUserOrder frmOrder = new frmUserOrder();
                    ArrayList OrderField = new ArrayList();
                    if (this.DtDataView.Count != 0 && (tpQuery.TabPages[2].Visible == true || tpQuery.TabPages[4].Visible == true))
                    {
                        if (tpQuery.TabPages[2].Visible == true)
                        {                            
                            for (int n = 1; n < neuSpread1_Sheet1.ColumnCount; n++)
                            {
                                if (neuSpread1_Sheet1.Columns[n].Visible == true)
                                {
                                    OrderField.Add(neuSpread1_Sheet1.Columns[n].Label.ToString());
                                }
                            }                            

                            frmOrder.Arrlist = OrderField;
                            frmOrder.StartPosition = FormStartPosition.CenterScreen;
                            frmOrder.ShowDialog();
                            if (!string.IsNullOrEmpty(frmOrder.OrderSql))
                            {
                                
                                this.DtDataView.Sort = frmOrder.OrderSql;
                            }
                        }
                        else
                        {
                            for (int n = 1; n < neuSpread3_Sheet1.ColumnCount; n++)
                            {
                                if (neuSpread3_Sheet1.Columns[n].Visible == true)
                                {
                                    OrderField.Add(neuSpread3_Sheet1.Columns[n].Label.ToString());
                                }
                            }
                            frmOrder.Arrlist = OrderField;
                            frmOrder.StartPosition = FormStartPosition.CenterScreen;
                            frmOrder.ShowDialog();
                            if (!string.IsNullOrEmpty(frmOrder.OrderSql))
                            {
                                this.stkDataView.Sort = "";
                                this.stkDataView.Sort = frmOrder.OrderSql;
                            }
                            //�����¸�ʽ��Ҫ����֮ǰ��һ����
                            for (int i = 0; i < neuSpread3_Sheet1.ColumnCount; i++)
                            {
                                neuSpread3_Sheet1.Columns[i].Width = 80;
                            }
                            neuSpread3_Sheet1.Columns[5].Visible = false;
                        }
                    }
                    #endregion
                    break;

                case "������������":
                    #region
                    frmCombFilter frmFilter = new frmCombFilter();
                    string colName = neuSpread1_Sheet1.ActiveColumn.Label.ToString();
                    ArrayList alValues = new ArrayList();
                    for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                    {
                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        obj.ID = i.ToString();
                        obj.Name = this.neuSpread1_Sheet1.Cells[i, neuSpread1_Sheet1.ActiveColumnIndex].Text.Trim();
                        alValues.Add(obj);
                    }
                    frmFilter.AlCurValues = alValues;
                    frmFilter.CurrentGrpName = colName;
                    frmFilter.StartPosition = FormStartPosition.CenterScreen;
                    if (!((Form)frmFilter).Visible)
                    {
                        this.Focus();
                        frmFilter.ShowDialog();
                    }
                    if (frmFilter.RtResult == 1)
                    {
                        if (!string.IsNullOrEmpty(this.DtDataView.RowFilter))
                        {
                            DialogResult result = MessageBox.Show("��ǰɸѡ�Ƿ����֮ǰ����������", "��ʾ", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                if (!string.IsNullOrEmpty(frmFilter.RtFilter))
                                {
                                    try
                                    {
                                        string filter = "1=1";
                                        filter = frmFilter.RtFilter;
                                        this.DtDataView.RowFilter = filter;
                                        tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(frmFilter.RtFilter))
                                {
                                    try
                                    {
                                        this.DtDataView.RowFilter += " AND " + frmFilter.RtFilter;
                                        tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                        else
                        {
                            string filter = "1=1";
                            if (!string.IsNullOrEmpty(frmFilter.RtFilter))
                            {
                                try
                                {
                                    filter = frmFilter.RtFilter;
                                    this.DtDataView.RowFilter = filter;
                                    tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else if (frmFilter.RtResult == 0)
                    {
                        this.DtDataView.RowFilter = frmFilter.RtFilter;
                        tpQuery.TabPages[2].Text = "��ϸ��Ϣ (��: " + neuSpread1_Sheet1.RowCount.ToString() + " ��)";
                    }
                    #endregion
                    break;
                case "������ѡ�걾":
                    AddSpec();
                    break;
                case "��ѯ��������Ϣ":
                    try
                    {
                        //���
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
                        Application.DoEvents();
                        GetSumData();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        //���
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
                        Application.DoEvents();
                        this.GetVisitRecord();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                        for (int i = 0; i < neuSpread2_Sheet1.Columns.Count; i++)
                        {
                            neuSpread2_Sheet1.ColumnHeader.Rows[0].Height = 30;
                            neuSpread2_Sheet1.Columns[i].AllowAutoFilter = true;
                            neuSpread2_Sheet1.Columns[i].AllowAutoSort = true;
                        }

                        for (int i = 0; i < this.sheetView2.Columns.Count; i++)
                        {
                            if (i <= 6)
                            {
                                this.sheetView2.Columns[i].Width = 78;
                            }
                            if (i == 0)
                            {
                                this.sheetView2.Columns[i].Width = 88;
                            }
                            sheetView2.ColumnHeader.Rows[0].Height = 30;
                            sheetView2.Columns[i].AllowAutoFilter = true;
                            sheetView2.Columns[i].AllowAutoSort = true;
                        }

                        SetVisible();

                    }
                    catch
                    {
                        break;
                    }
                    break;
                case "������ѡ�걾":
                    this.SaveChoosedSub();
                    break;
                case "�����ѡ�걾":
                    this.Clear();
                    break;
                case "λ������":
                    this.GetSubPos();
                    break;
                case "��֯�걾ɸѡ":
                    if (!this.rbtOrg.Checked)
                    {
                        MessageBox.Show("��ȷ��ѡ��걾����֯�걾����ʹ�ô˹��ܣ�лл��");
                    }
                    else if (StkDataView.Count == 0)
                    {
                        MessageBox.Show("���Ȳ�ѯ�걾��Ȼ�����ɸѡ��лл��");
                    }
                    else
                    {
                        frmOrgFilter frmBf = new frmOrgFilter();
                        frmBf.StartPosition = FormStartPosition.CenterScreen;
                        if (!((Form)frmBf).Visible)
                        {
                            this.Focus();
                            frmBf.ShowDialog();
                        }
                        if (frmBf.DlRt == DialogResult.OK)
                        {
                            this.bldFilter = frmBf.SqlStr;
                            if (!string.IsNullOrEmpty(frmBf.FilterSql))
                            {
                                this.StkDataView.RowFilter = frmBf.FilterSql;
                                for (int i = 0; i < neuSpread3_Sheet1.ColumnCount; i++)
                                {
                                    neuSpread3_Sheet1.Columns[i].Width = 80;
                                }
                                neuSpread3_Sheet1.Columns[5].Visible = false;
                                tpStock.Text = "�����Ϣ (�� " + neuSpread3_Sheet1.RowCount.ToString() + " ����¼";
                            }
                            if (!string.IsNullOrEmpty(this.bldFilter))
                            {
                                this.isOldData = false;
                                this.GetDetailData(10);
                            }
                            else //ȡ��ɸѡ
                            {
                                this.DtDataView = this.oldDv;
                                neuSpread1_Sheet1.DataSource = this.DtDataView;
                                this.BindingSheet1(2);
                            }
                        }
                    }
                    break;
                case "Ѫ�걾ɸѡ":
                    if (!this.rbtBld.Checked)
                    {
                        MessageBox.Show("��ȷ��ѡ��걾��Ѫ�걾����ʹ�ô˹��ܣ�лл��");
                    }
                    else if (StkDataView.Count== 0)
                    {
                        MessageBox.Show("���Ȳ�ѯ�걾��Ȼ�����ɸѡ��лл��");
                     }
                    else
                    {
                        frmBldFilter frmBf = new frmBldFilter();
                        frmBf.StartPosition = FormStartPosition.CenterScreen;
                        if (!((Form)frmBf).Visible)
                        {
                            this.Focus();
                            frmBf.ShowDialog();
                        }
                        if (frmBf.DlRt == DialogResult.OK)
                        {
                            this.bldFilter = frmBf.SqlStr;
                            if (!string.IsNullOrEmpty(frmBf.FilterSql))
                            {
                                this.StkDataView.RowFilter = frmBf.FilterSql;
                                for (int i = 0; i < neuSpread3_Sheet1.ColumnCount; i++)
                                {
                                    neuSpread3_Sheet1.Columns[i].Width = 80;
                                }
                                neuSpread3_Sheet1.Columns[5].Visible = false;
                                tpStock.Text = "�����Ϣ (�� " + neuSpread3_Sheet1.RowCount.ToString() + " ����¼";
                            }
                            if (!string.IsNullOrEmpty(this.bldFilter))
                            {
                                this.isOldData = false;
                                this.GetDetailData(10);
                            }
                            else //ȡ��ɸѡ
                            {
                                this.DtDataView = this.oldDv;
                                neuSpread1_Sheet1.DataSource = this.DtDataView;
                                this.BindingSheet1(2);
                            }
                        }
                    }
                    break;
                case "�걾����":

                    try
                    {
                        int specId = Convert.ToInt32(neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRow.Index, Convert.ToInt32(CurPos.�걾ԴID)].Text.Trim());

                        if (rbtBld.Checked)
                        {
                            ucSpecimentSource ucBld = new ucSpecimentSource();
                            ucBld.SpecId = specId;
                            Size size = new Size();
                            size.Height = 800;
                            size.Width = 1280;
                            ucBld.Size = size;

                            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucBld, FormBorderStyle.FixedSingle, FormWindowState.Maximized);
                            //FS.FrameWork.WinForms.Classes.Function.PopForm popForm = new FS.FrameWork.WinForms.Forms.BaseForm();


                        }
                        if (rbtOrg.Checked)
                        {
                            ucSpecSourceForOrg ucOrg = new ucSpecSourceForOrg();
                            ucOrg.SpecId = specId;
                            // FormBorderStyle formStyle = new FormBorderStyle();
                            Size size = new Size();
                            size.Height = 800;
                            size.Width = 1280;
                            ucOrg.Size = size;
                            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucOrg, FormBorderStyle.FixedSingle, FormWindowState.Maximized);
                            //FS.FrameWork.WinForms.Classes.Function.PopForm popForm = new FS.FrameWork.WinForms.Forms.BaseForm();

                        }
                    }
                    catch
                    { }
                    break;
                //
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }         

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
                chkOrgDet.Visible = false;
                chkOrgDet.Checked = false;
                chkColPos.Visible = false;
                chkColPos.Checked = false;
            }
            else
            {
                chk863.Visible = true;
                chk115.Visible = true;
                chkOrgDet.Visible = true;
                chkOrgDet.Checked = false;
                chkColPos.Visible = true;
                chkColPos.Checked = false;
            }
        }

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                GetDetailData(4);
            }
            catch
            { }
        }

        private void neuSpread3_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                this.bldFilter = string.Empty;
                GetDetailData(5);
            }
            catch
            { }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ϣ�����Ժ�...");
            Application.DoEvents();
            GetStockData(2);
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
            { }
        }

        private void btnB2_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            txtSpecNoExl.Text = fileDialog.FileName;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.chk)].Value = 1;
                }
            }
            if (!chkAll.Checked)
            {
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.chk)].Value = 0;
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string applyID = string.Empty;
            string appEmpl = string.Empty;
            string appDept = string.Empty;
            DateTime appBdt = new DateTime();
            DateTime appEdt = new DateTime();
            //1�����������ˡ����Ҽ�ʱ��Ĳ�ѯ�����ؽ������ȥѡ�����뵥�ţ������շ�ʱ������ιҺŻ��ߣ�
            //2��ֱ����������Ź���
            applyID = this.txtApplyNum.Text.Trim();
            #region ����д����Ų�ѯ
            if (!string.IsNullOrEmpty(applyID))
            {
                if (this.sheetView1.RowCount > 0)
                {
                    DialogResult result = MessageBox.Show("��ѯ�����Ƿ������ǰ��ѡ�걾���ݣ�", "��ʾ", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        this.sheetView1.RowCount = 0;
                    }
                }
                this.SetApplyData(applyID);
            }
            #endregion
            #region ��д������Ϣ��ѯ
            else
            {
                if (this.cmbOper.Tag == null)
                {
                    appEmpl = "%";
                }
                else
                {
                    if (string.IsNullOrEmpty(this.cmbOper.Tag.ToString()))
                    {
                        appEmpl = "%";
                    }
                    else
                    {
                        appEmpl = this.cmbOper.Tag.ToString();
                    }
                }
                if (this.cmbAppDept.Tag == null)
                {
                    appDept = "%";
                }
                else
                {
                    if (string.IsNullOrEmpty(this.cmbAppDept.Tag.ToString()))
                    {
                        appDept = "%";
                    }
                    else
                    {
                        appDept = this.cmbAppDept.Tag.ToString();
                    }
                }
                appBdt = this.dtStart.Value;
                appEdt = this.dtEnd.Value;
                ArrayList alApp = this.appTabMgr.GetSubApply(appEmpl, appDept, appBdt, appEdt);
                if ((alApp != null) && (alApp.Count > 0))
                {
                    ucShowApp.AlApplyTables = alApp;
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucShowApp);
                    //ucShowApp.SelectedAppRow += new ucShowAppInfo.GetAppRow(ucShowApp_SelectedAppRow);

                    if (ucShowApp.RtRs == -1)
                    {
                        return;
                    }
                    else
                    {
                        this.SelectedAppRow(ucShowApp.AppID);
                    }
                }
            }
            #endregion
        }

        private void SelectedAppRow(string appId)
        {
            this.sheetView1.RowCount = 0;
            this.SetApplyData(appId);
            this.txtApplyNum.Text = appId;
        }

        private void txtApplyNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string applyID = string.Empty;
                applyID = this.txtApplyNum.Text.Trim();
                if (!string.IsNullOrEmpty(applyID))
                {
                    if (this.sheetView1.RowCount > 0)
                    {
                        DialogResult result = MessageBox.Show("��ѯ�����Ƿ������ǰ��ѡ�걾���ݣ�", "��ʾ", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            this.sheetView1.RowCount = 0;
                        }
                    }
                    this.SetApplyData(applyID);
                }
            }
        }

        private void SetApplyData(string applyID)
        {
            ArrayList appTab = this.appMgr.GetSubSpecOut(applyID);
            if ((appTab != null) && (appTab.Count > 0))
            {
                string alBar = string.Empty;
                foreach (SpecOut spOut in appTab)
                {
                    if (string.IsNullOrEmpty(alBar))
                    {
                        alBar = "'" + spOut.SubSpecBarCode + "'";
                    }
                    else
                    {
                        alBar = alBar + "," + "'" + spOut.SubSpecBarCode + "'";
                    }
                }
                if (!string.IsNullOrEmpty(alBar))
                {
                    this.specBar = alBar;
                }
                this.GetDetailData(7);
                this.SetVisible();
                this.AddSpec();
                this.isAddAppOutData = true;
                this.tpQuery.SelectedIndex = 5;
            }
        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (neuSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }
            else
            {
                int iSelectionCount = 0;
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    for (int columnNum = 2; columnNum < neuSpread1_Sheet1.ColumnCount - 1; columnNum++)
                    {
                        if (neuSpread1_Sheet1.IsSelected(i, columnNum))
                        {
                            iSelectionCount++;
                            break;
                        }
                    }
                }
                labelSelectSum.Text = iSelectionCount.ToString();

                
            }
        }

        private void cbVistDead_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbVistDead.CheckState == CheckState.Checked)
            {
                cbVistDead.Text = "����";
            }
            else if (cbVistDead.CheckState == CheckState.Unchecked)
            {
                cbVistDead.Text = "δ����";
            }
            else
            {
                cbVistDead.Text = "ȫ��";
            }
        }

    }
}
