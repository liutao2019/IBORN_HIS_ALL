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
        /// 明细表视图
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
        /// 库存表视图
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

        //记录原来明细的dataview
        private DataView oldDv = new DataView();
        private bool isOldData = true;

        //select max(ss.SUBBARCODE), t.SPECIMENTNAME,d.DISEASENAME
        //from SPEC_SUBSPEC ss, spec_source s, SPEC_DISEASETYPE d, SPEC_TYPE t
        //where ss.SPECID = s.specid and s.DISEASETYPEID>14 and s.DISEASETYPEID = d.DISEASETYPEID and t.SPECIMENTTYPEID = ss.SPECTYPEID
        //group by t.SPECIMENTNAME, d.DISEASENAME

        private enum CurPos
        {
            chk,
            标本源ID,
            源条码,
            标本号,
            //抗凝血支数,
            //抗凝血容量,
            //非抗凝血支数,
            //非抗凝血容量, 
            操作人,
            备注,
            配对,
            姓名,
            病历号,
            标本条码,
            标本类型,
            病种,
            操作时间,
            送存医生,
            科室,
            取材脏器,
            肿物性质,
            在库状态,
            标本属性,
            上次返回时间,
            约定返回时间,
            转移部位,
            标本容量,
            脏器描述,
            标本详细描述,
            诊疗卡号,
            住院流水号,
            当前治疗阶段,
            手术名称,
            主诊断,
            主诊断形态码,
            诊断1,
            诊断1形态码,
            诊断2,
            诊断2形态码,
            诊断备注,
            性别,
            国籍,
            民族,
            住址,
            婚姻状况,
            生日,
            联系电话,
            家庭电话,
            //分装数量,                      
            标本盒位置,
            行,
            列,
            SPECTYPEID
        }

        private enum Cols
        {
            住院流水号,
            病历号,
            姓名,
            标本号,
            病种,
            SPECID,
            血浆,
            血清,
            细胞,
            DNA,
            RNA,
            蜡块,
            配对病种,
            配对标本号,
            MSSPECID
        }

        private enum StoPos
        {
            住院流水号,
            SPECID,
            源条码,
            操作人,
            备注,
            配对,
            姓名,
            标本号,
            病历号,
            病种,
            操作时间,
            送存医生,
            科室,           
            当前治疗阶段,
            手术名称,
            主诊断,
            主诊断形态码,
            诊断1,
            诊断1形态码,
            诊断2,
            诊断2形态码,
            诊断备注,
            性别,
            国籍,
            民族,
            住址,
            婚姻状况,
            生日,
            联系电话,
            家庭电话//,
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

            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.住院流水号)].Width = 120;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.住院流水号)].Label = Cols.住院流水号.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.标本号)].Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.标本号)].Label = Cols.标本号.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.血浆)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.血浆)].Label = Cols.血浆.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.SPECID)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.SPECID)].Label = Cols.SPECID.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.血清)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.血清)].Label = Cols.血清.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.细胞)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.细胞)].Label = Cols.细胞.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.DNA)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.DNA)].Label = Cols.DNA.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.RNA)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.RNA)].Label = Cols.RNA.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.蜡块)].Width = 60;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.蜡块)].Label = Cols.蜡块.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.配对病种)].Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.配对病种)].Label = Cols.配对病种.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.配对标本号)].Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.配对标本号)].Label = Cols.配对标本号.ToString();


            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.MSSPECID)].Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.MSSPECID)].Label = Cols.MSSPECID.ToString();

           
        }

        private void InitSheet(FarPoint.Win.Spread.SheetView sheet)
        {
            sheet.Columns.Count = Convert.ToInt32(CurPos.SPECTYPEID) + 1;
            FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            
            sheet.Columns[Convert.ToInt32(CurPos.chk)].Label = "选择";
            sheet.Columns[Convert.ToInt32(CurPos.chk)].CellType = chkType;
            sheet.Columns[Convert.ToInt32(CurPos.chk)].Width = 30;

            sheet.Columns[Convert.ToInt32(CurPos.标本源ID)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.标本源ID)].Label = CurPos.标本源ID.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.源条码)].Width = 100;
            sheet.Columns[Convert.ToInt32(CurPos.源条码)].Label = CurPos.源条码.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.标本号)].Width = 100;
            sheet.Columns[Convert.ToInt32(CurPos.标本号)].Label = CurPos.标本号.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.姓名)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.姓名)].Label = CurPos.姓名.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.病历号)].Width = 100;
            sheet.Columns[Convert.ToInt32(CurPos.病历号)].Label = CurPos.病历号.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.标本条码)].Width = 120;
            sheet.Columns[Convert.ToInt32(CurPos.标本条码)].Label = CurPos.标本条码.ToString();
            //sheet.Columns[Convert.ToInt32(CurPos.分装数量)].Width = 80;
            //sheet.Columns[Convert.ToInt32(CurPos.分装数量)].Label = CurPos.分装数量.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.病种)].Width = 40;
            sheet.Columns[Convert.ToInt32(CurPos.病种)].Label = CurPos.病种.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.标本类型)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.标本类型)].Label = CurPos.标本类型.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.操作时间)].Width = 90;
            sheet.Columns[Convert.ToInt32(CurPos.操作时间)].Label = CurPos.操作时间.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.送存医生)].Width = 100;
            sheet.Columns[Convert.ToInt32(CurPos.送存医生)].Label = CurPos.送存医生.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.科室)].Width = 120;
            sheet.Columns[Convert.ToInt32(CurPos.科室)].Label = CurPos.科室.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.取材脏器)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.取材脏器)].Label = CurPos.取材脏器.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Label = CurPos.SPECTYPEID.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Visible = false;// = 80;
            sheet.Columns[Convert.ToInt32(CurPos.肿物性质)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.肿物性质)].Label = CurPos.肿物性质.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.操作人)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.操作人)].Label = CurPos.操作人.ToString();


            sheet.Columns[Convert.ToInt32(CurPos.备注)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.备注)].Label = CurPos.备注.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.配对)].Width = 40;
            sheet.Columns[Convert.ToInt32(CurPos.配对)].Label = CurPos.配对.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.在库状态)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.在库状态)].Label = CurPos.在库状态.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.标本属性)].Width = 50;
            sheet.Columns[Convert.ToInt32(CurPos.标本属性)].Label = CurPos.标本属性.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.上次返回时间)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.上次返回时间)].Label = CurPos.上次返回时间.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.约定返回时间)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.约定返回时间)].Label = CurPos.约定返回时间.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.转移部位)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.转移部位)].Label = CurPos.转移部位.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.标本容量)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.标本容量)].Label = CurPos.标本容量.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.脏器描述)].Width = 80;
            sheet.Columns[Convert.ToInt32(CurPos.脏器描述)].Label = CurPos.脏器描述.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.标本详细描述)].Width = 120;
            sheet.Columns[Convert.ToInt32(CurPos.标本详细描述)].Label = CurPos.标本详细描述.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.诊疗卡号)].Width = 60;
            sheet.Columns[Convert.ToInt32(CurPos.诊疗卡号)].Label = CurPos.诊疗卡号.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.住院流水号)].Label = CurPos.住院流水号.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.当前治疗阶段)].Label = CurPos.当前治疗阶段.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.手术名称)].Label = CurPos.手术名称.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.主诊断)].Label = CurPos.主诊断.ToString();


            sheet.Columns[Convert.ToInt32(CurPos.主诊断形态码)].Label = CurPos.主诊断形态码.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.诊断1)].Label = CurPos.诊断1.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.诊断1形态码)].Label = CurPos.诊断1形态码.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.诊断2)].Label = CurPos.诊断2.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.诊断2形态码)].Label = CurPos.诊断2形态码.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.诊断备注)].Label = CurPos.诊断备注.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.性别)].Label = CurPos.性别.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.国籍)].Label = CurPos.国籍.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.民族)].Label = CurPos.民族.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.住址)].Label = CurPos.住址.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.婚姻状况)].Label = CurPos.婚姻状况.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.生日)].Label = CurPos.生日.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.联系电话)].Label = CurPos.联系电话.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.家庭电话)].Label = CurPos.家庭电话.ToString();

            sheet.Columns[Convert.ToInt32(CurPos.标本盒位置)].Label = CurPos.标本盒位置.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.列)].Label = CurPos.列.ToString();
            sheet.Columns[Convert.ToInt32(CurPos.行)].Label = CurPos.行.ToString();

            for (int i = 0; i < sheet.Columns.Count; i++)
            {
                sheet.ColumnHeader.Rows[0].Height = 30;
            }
        }

        /// <summary>
        /// 明细的查询条件
        /// </summary>
        /// <returns></returns>
        private string GetSql()
        {
            #region
            return @"
                         with t as
                         (
                         SELECT DISTINCT 
                         s.SPECID 标本源ID,
                         s.HISBARCODE 源条码, 
                         s.OPEREMP 操作人,
                         s.MARK 备注,
                         ( case s.MATCHFLAG when '1' then '是' else '否'end) 配对, p.pname 姓名, 
                         s.SPEC_NO 标本号,
  						 p.CARD_NO 病历号,
                         ss.SUBBARCODE 标本条码,
                         d.DISEASENAME 病种, 
                         s.OPERTIME 操作时间, 
						(case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) 送存医生,
                        (case s.ISHIS when 'O' then s.DEPTNO else (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO and rownum = 1) end)科室,
                        (case s.ORGORBLOOD when 'O' then st.TUMORPOS else '' end) 取材脏器,
						(case st.TUMORTYPE when '1' then '肿物' when '2' then '子瘤' when '3' then '癌旁' when '4' then '正常' when '5' then '癌栓' when '8' then '淋巴结' else '' end) 肿物性质,
						(case ss.STATUS when '1' then '在库' when '2' then '借出' when '3' then '已还' when '4' then '用完' else '' end) 在库状态,
						(case st.TUMORPOR when '1' then '原发' when '2' then '复发' when '3' then '转移' when '13' then '原发，转移' when '23' then '复发，转移' else '' end) 标本属性,
						ss.LASTRETURNTIME 上次返回时间, 
                         ss.DATERETURNTIME 约定返回时间, 
                         st.TRANSPOS 转移部位, 
                         st.CAPACITY 标本容量,
						st.BAOMOENTIRE 脏器描述, 
                         st.MARK 标本详细描述, 
                         p.IC_CARDNO 诊疗卡号,
                         s.INPATIENT_NO 住院流水号,
						s.GETPEORID 当前治疗阶段, 
                         s.OPERPOSNAME 手术名称, 
                         sd.MAIN_DIANAME 主诊断,
                         sd.MAIN_DIACODE 主诊断形态码,
						sd.MAIN_DIANAME1 诊断1, 
                         sd.MAIN_DIACODE1 诊断1形态码,
                         sd.MAIN_DIANAME2 诊断2, 
                         sd.MAIN_DIACODE2 诊断2形态码,
						sd.MARK 诊断备注, 
                         (case p.GENDER when 'F' then '女' else '男' end) 性别,
						(select name  from COM_DICTIONARY where type = 'COUNTRY' and code = p.NATIONALITY and rownum = 1)国籍,
            			(select name  from COM_DICTIONARY where type = 'NATION' and code = p.NATION and rownum = 1) 民族,
            			p.ADDRESS 住址,
                         (select name from COM_DICTIONARY where type = 'MaritalStatus' and code = p.ISMARR and rownum = 1)婚姻状况,
           				p.BIRTHDAY 生日, 
                         p.CONTACTNUM 联系电话, 
                         p.HOMEPHONENUM 家庭电话, 
                         b.BOXBARCODE 标本盒位置, 
                         ss.BOXENDROW 行,ss.BOXENDCOL 列,
						 t.SPECIMENTNAME 标本类型,
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
        /// 明细的SQL
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
            #region 标本查询
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

            #region 按照病人条件查询
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
                    if (sexName == "男")
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

            #region 随访要求
            if (cbVistDead.CheckState == CheckState.Checked)
            {
                curSql += " and rcd.IS_DEAD = '1'";
            }
            else if (cbVistDead.CheckState == CheckState.Unchecked)
            {
                curSql += " and rcd.IS_DEAD = '0'";
            }
            else //加个空格吧，否则出不来门诊及空军医院数据
            {
                curSql += " ";
            }
            #endregion

            string diagCon = "(";

            #region 按照诊断查询
            List<string> diagList = new List<string>();
            if (chkDiag.Checked)
            {
                string diag = cmbDiaMain.Text.TrimStart().TrimEnd(); //;// txtCardNo.Text.Trim();
                string diag1 = cmbDiaMain1.Text.TrimStart().TrimEnd(); ;// txtName.Text.Trim();
                string diag2 = cmbDiaMain2.Text.TrimStart().TrimEnd(); ;// txtName.Text.Trim();
                //初步诊断
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

            #region 按照位置
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
            {//, t as ( select * from s where substr(标本号,1,1)>='0' and substr(标本号,1,1)<='9')
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
                curSql += " \n select * from t where int(标本号) between " + num1.ToString() + " and " + num2.ToString();
            }
            return curSql;
        }

        /// <summary>
        /// 汇总的SQL
        /// </summary>
        private string GetSumSql(int tabIndex)
        {
            string tmpSpecs = "";
            if (tabIndex == 2)
            {
                string tmpSpecId = neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, Convert.ToInt32(CurPos.标本源ID)].Text;
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
            #region　SQL
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
					select distinct ta.inpatient_no 住院流水号, ta.card_no 病历号,ta.name 姓名, ta.c 标本号, ta.DISEASENAME 病种,ta.specid,xj.subcnt 血浆,xq.subcnt 血清 ,xb.subcnt 细胞,
					dna.subcnt DNA ,rna.subcnt RNA,lk.subcnt 蜡块, ma.DISEASENAME 配对病种,mas.c 配对标本号,ma.specid MSSPECID

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
        /// 从获取Excel中的病历号集合形成sql语句
        /// </summary>
        /// <returns></returns>
        private string GetCardCol()
        {
            ExlToDb2Manage exlMgr = new ExlToDb2Manage();
            DataSet ds = new DataSet();
            exlMgr.ExlConnect(txtCardNoExl.Text, ref ds);
            if (ds == null)
            {
                MessageBox.Show("读取文件失败");
            }
            //" \n select * from t where int(标本号) between " + num1.ToString() + " and " + num2.ToString();
            string cardCols = " \n where t.病历号 in (";
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
                cardCols = "where t.病历号 in ('')";
            }
            return SetDetSql() + cardCols;
           
        }

        /// <summary>
        /// 从获取Excel中的标本号集合形成sql语句
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
                MessageBox.Show("读取文件失败");
            }

            int index = 0;
            //, t as ( select * from s where substr(标本号,1,1)>='0' and substr(标本号,1,1)<='9')
           // string specCols = ") ";
           
           string specCols = " where 标本号 in( ";
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
        /// 库存的Sql
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <returns></returns>
        private string GetStockSql(int tabIndex)
        {
            string tmpSpecs = "";

            //从第二个tab页中传来，表示查看指定标本的信息
            if (tabIndex == 2)
            {
                string tmpSpecId = neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex, Convert.ToInt32(CurPos.标本源ID)].Text;
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
                         s.INPATIENT_NO 住院流水号,
                         s.SPECID,
                         s.HISBARCODE 源条码, 
                         s.OPEREMP 操作人,
                         s.MARK 备注,
                         ( case s.MATCHFLAG when '1' then '是' else '否'end) 配对, p.pname 姓名, 
                         s.SPEC_NO 标本号,
  						 p.CARD_NO 病历号,
                         d.DISEASENAME 病种, 
                         s.OPERTIME 操作时间, 
						(case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) 送存医生,
                        (case s.ISHIS when 'O' then s.DEPTNO else (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO and rownum = 1) end)科室,
						 s.GETPEORID 当前治疗阶段, 
                         s.OPERPOSNAME 手术名称, 
                         sd.MAIN_DIANAME 主诊断,
                         sd.MAIN_DIACODE 主诊断形态码,
						 sd.MAIN_DIANAME1 诊断1, 
                         sd.MAIN_DIACODE1 诊断1形态码,
                         sd.MAIN_DIANAME2 诊断2, 
                         sd.MAIN_DIACODE2 诊断2形态码,
						 sd.MARK 诊断备注, 
                         (case p.GENDER when 'F' then '女' else '男' end) 性别,
						(select name  from COM_DICTIONARY where type = 'COUNTRY' and code = p.NATIONALITY and rownum = 1)国籍,
            			(select name  from COM_DICTIONARY where type = 'NATION' and code = p.NATION and rownum = 1) 民族,
            			p.ADDRESS 住址,
                         (select name from COM_DICTIONARY where type = 'MaritalStatus' and code = p.ISMARR and rownum = 1)婚姻状况,
           				p.BIRTHDAY 生日, 
                         p.CONTACTNUM 联系电话, 
                         p.HOMEPHONENUM 家庭电话,   
                         ss.SPECTYPEID,                     
                        ss.storeid
                       from SPEC_SUBSPEC ss join  SPEC_SOURCE s on ss.specid = s.specid
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID					 
                       left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
                       where s.OPERTIME BETWEEN to_date('{0}','yyyy-mm-dd hh24:mi:ss') AND to_date('{1}','yyyy-mm-dd hh24:mi:ss')	and ss.SPECID>0 and ss.STOREID>0 and ss.BOXID>0
					and s.ORGORBLOOD = '{2}'";

            #region　SQL
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
					
					select distinct ta.住院流水号, ta.SPECID,ta.源条码, ta.操作人,ta.备注,ta.配对, ta.姓名,  ta.标本号,
                    ta.病历号,ta. 病种, ta.操作时间, ta.送存医生, ta.科室,ta.当前治疗阶段,ta.手术名称,  ta.主诊断,
                    ta.主诊断形态码,ta.诊断1,   ta.诊断1形态码,ta.诊断2,  ta.诊断2形态码,ta.诊断备注,  ta.性别,ta.国籍,
                    ta.民族,ta.住址,ta.婚姻状况,ta.生日, ta.联系电话, ta.家庭电话, --inpatient_no 住院流水号, ta.card_no 病历号,ta.name 姓名, ta.c 标本号, ta.DISEASENAME 病种,ta.specid,
                    ";
            if (rbtBld.Checked)
            {
                tsql += @"	nvl(xj.subcnt,0) 血浆,nvl(xj.storecnt,0) 血浆库存,nvl(xq.subcnt,0) 血清 ,nvl(xq.storecnt,0) 血清库存 ,nvl(xb.subcnt,0) 细胞,nvl(xb.storecnt,0) 细胞库存";

            }
            else
            {
                tsql += @"	dna.subcnt DNA ,nvl(dna.storecnt,0) DNA库存 ,rna.subcnt RNA,nvl(rna.storecnt,0) RNA库存,lk.subcnt 蜡块,nvl(lk.storecnt,0) 蜡块库存";
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

            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.住院流水号), "住院流水号");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.标本号), "标本号");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.病历号), "病历号");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.姓名), "姓名");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.病种), "病种");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.SPECID), "SPECID");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.血浆), "血浆");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.血清), "血清");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.细胞), "细胞");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.DNA), "DNA");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.RNA), "RNA");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.蜡块), "蜡块");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.配对病种), "配对病种");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.配对标本号), "配对标本号");
            neuSpread2_Sheet1.BindDataColumn(Convert.ToInt32(Cols.MSSPECID), "MSSPECID");

            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.SPECID)].Visible = false;//.Width = 100;
            neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.MSSPECID)].Visible = false;//.Width = 100;

            if (rbtBld.Checked)
            {

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.DNA)].Visible = false;//.Width = 100;

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.RNA)].Visible = false;//.Width = 100;

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.蜡块)].Visible = false;//.Width = 100;
            }

            else
            {
                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.血浆)].Visible = false;// Width = 100;

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.血清)].Visible = false;//.Width = 100;

                neuSpread2_Sheet1.Columns[Convert.ToInt32(Cols.细胞)].Visible = false;//.Width = 100; 
            }

            tpQuery.TabPages[3].Text = "配对信息 (共: " + neuSpread2_Sheet1.RowCount.ToString() + " 条)";

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
            tpStock.Text = "库存信息 (共 " + neuSpread3_Sheet1.RowCount.ToString() + " 条记录";
        }

        /// <summary>
        /// 从汇总的表格中传入形成的SQL语句
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
        /// 从库存的表格中传入形成的SQL语句
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
        /// 从库存的表格中传入形成的SQL语句
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
        /// 获取随访记录
        /// </summary>
        /// <returns></returns>
        private void GetVisitRecord()
        {
            try
            {
                string strVisit = string.Empty;
                /*
                strVisit = @"select distinct V.CARD_NO 病历号,P.NAME 姓名,SO.SPEC_NO 标本号,
                        (SELECT SD.DISEASENAME FROM SPEC_DISEASETYPE SD WHERE SD.DISEASETYPEID = SO.DISEASETYPEID) 病种,
                        N.SECOND_ICD 形态码,N.PERIOR_CODE 分期,N.ICD_CODE 诊断码,N.DIAG_NAME 诊断名称,
                        (SELECT NY.NAME FROM COM_DICTIONARY NY WHERE NY.CODE = D.CIRCS AND NY.TYPE = 'CASE07') 一般情况,
                        (SELECT NY.NAME FROM COM_DICTIONARY NY WHERE NY.CODE = D.SYMPTOM AND NY.TYPE = 'CASE10') 症状表现,
                        D.DEAD_TIME 死亡时间,(SELECT NY.NAME FROM COM_DICTIONARY NY WHERE NY.CODE = D.DEAD_REASON AND NY.TYPE = 'CASE08') 死因,
                        D.VISIT_TIME 末次随访时间,D.RECRUDESCE_TIME 复发时间,
                        (SELECT NY.NAME  FROM COM_DICTIONARY NY WHERE NY.CODE = D.TRANSFER_POSITION AND NY.TYPE = 'CASE11') 转移部位
                        FROM MET_CAS_VISITRECORD D,MET_CAS_VISIT V,SPEC_PATIENT P,MET_CAS_DIAGNOSE N, SPEC_SOURCE SO
                        WHERE D.CARD_NO in  ('{0} AND SO.INPATIENT_NO = N.INPATIENT_NO  AND SO.PATIENTID = P.PATIENTID
                        AND D.CARD_NO = V.CARD_NO AND V.CARD_NO = P.CARD_NO AND D.CARD_NO = P.CARD_NO 
                        AND D.VISIT_TIME = V.LAST_TIME AND N.DIAG_KIND = '1' and SO.SPECID in ({1}";
                */
                //要求没有随访的也要出来，无聊需求，增加大量SQL查询lingk20110831，都变成数据库编程了
                strVisit = @"with ta as 
                        (select D.CARD_NO 病历号,(SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.CIRCS AND N.TYPE = 'CASE07') 一般情况,
                        (SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.SYMPTOM AND N.TYPE = 'CASE10') 症状表现,
                        D.DEAD_TIME 死亡时间,(SELECT N.NAME FROM COM_DICTIONARY N WHERE N.CODE = D.DEAD_REASON AND N.TYPE = 'CASE08') 死因,
                        D.VISIT_TIME 末次随访时间,D.RECRUDESCE_TIME 复发时间,
                        (SELECT  N.NAME  FROM COM_DICTIONARY N WHERE N.CODE = D.TRANSFER_POSITION AND N.TYPE = 'CASE11') 转移部位
                        FROM MET_CAS_VISITRECORD D,MET_CAS_VISIT V where D.CARD_NO = V.CARD_NO AND D.VISIT_TIME = V.LAST_TIME
                        and D.CARD_NO in ('{0}),
                        tb as
                        (select distinct P.CARD_NO 病历号,P.PNAME 姓名,SO.SPEC_NO 标本号,(SELECT SD.DISEASENAME FROM SPEC_DISEASETYPE SD WHERE SD.DISEASETYPEID = SO.DISEASETYPEID) 病种,
                        DN.SECOND_ICD 形态码,DN.PERIOR_CODE 分期,DN.ICD_CODE 诊断码,DN.DIAG_NAME 诊断名称
                        FROM SPEC_PATIENT P join SPEC_SOURCE SO on SO.PATIENTID = P.PATIENTID
                        left join MET_CAS_DIAGNOSE DN on SO.INPATIENT_NO = DN.INPATIENT_NO
                        where DN.DIAG_KIND = '1' AND DN.OPER_TYPE = '2'
                        and P.CARD_NO in  ('{0}
                        and SO.SPECID in ({1})
                        select tb.病历号,tb.姓名,tb.标本号,tb.病种,tb.形态码,tb.分期,tb.诊断码,tb.诊断名称,
                        (select ta.一般情况 from ta where ta.病历号 = tb.病历号) 一般情况,
                        (select ta.症状表现 from ta  where ta.病历号 = tb.病历号) 症状表现,
                        (select ta.死亡时间 from ta  where ta.病历号 = tb.病历号) 死亡时间,
                        (select ta.死因 from ta  where ta.病历号 = tb.病历号) 死因,
                        (select ta.末次随访时间 from ta  where ta.病历号 = tb.病历号) 末次随访时间,
                        (select ta.复发时间 from ta  where ta.病历号 = tb.病历号) 复发时间,
                        (select ta.转移部位 from ta  where ta.病历号 = tb.病历号) 转移部位
                        from tb";
                string strSpecId = string.Empty;
                string strCardNo = string.Empty;

                if (this.neuSpread3_Sheet1.Rows.Count > 0)
                {
                    for (int k = 0; k < neuSpread3_Sheet1.RowCount; k++)
                    {
                        if (k == neuSpread3_Sheet1.RowCount - 1)
                        {
                            strCardNo += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.病历号)].Text.Trim() + "')";
                            strSpecId += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.SPECID)].Text.Trim() + ")";
                        }
                        else
                        {
                            strCardNo += neuSpread3_Sheet1.Cells[k, Convert.ToInt32(StoPos.病历号)].Text.Trim() + "','";
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
                                this.tpgVisit.Text = "随访信息 (共: " + dvTmp.Count.ToString() + " 条)";
                            }
                        }
                        else
                        {
                            this.tpgVisit.Text = "随访信息 (共: 0条)";
                        }
                    }
                    else
                    {
                        this.tpgVisit.Text = "随访信息 (共: 0条)";
                    }
                }
            }
            catch
            {
            }
            
        }

        /// <summary>
        /// 如果从汇总中传入 detail = true
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
                    //记录原来明细的dataview
                    this.oldDv = this.DtDataView;
                }
                if (this.DtDataView.Count == 0)
                {
                    MessageBox.Show("没有查询出符合要求记录，请检查检索条件或者导入Excel！");
                    return;
                }
                neuSpread1_Sheet1.DataSource = this.DtDataView;
            }
            else
            {
                MessageBox.Show("没有查询出符合要求记录，请检查检索条件或者导入Excel！");
                return;
            }
            #region 屏蔽，用单独函数来绑定
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本源ID), "标本源ID");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.源条码), "源条码");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.姓名), "姓名");
            ////neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.分装数量), "分装数量"); 
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.病种), "病种");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本类型), "标本类型");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.操作时间), "操作时间");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.送存医生), "送存医生");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.科室), "科室");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.病历号), "病历号");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本条码), "标本条码");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本号), "标本号");
            ////neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.肿物性质), "肿物性质");
            //if (!rbtBld.Checked)
            //{
            //    neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.取材脏器), "取材脏器");
            //    neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.肿物性质), "肿物性质");
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本容量)].Visible = false;
            //    //chkOrgDet.Visible = true;
            //    //chkOrgDet.Checked = false;
            //    //chkColPos.Visible = true;
            //    //chkColPos.Checked = false;
            //    orgOrBld = "O";
            //}
            //if (rbtBld.Checked)
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.取材脏器)].Visible = false;
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.肿物性质)].Visible = false;
            //    //chkOrgDet.Visible = false;
            //    //chkOrgDet.Checked = false;
            //    //chkColPos.Visible = false;
            //    //chkColPos.Checked = false;
            //    //chkColPos
            //    orgOrBld = "B";
            //}
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.SPECTYPEID), "SPECTYPEID");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.操作人), "操作人");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.备注), "备注");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.配对), "配对");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.在库状态), "在库状态");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本属性), "标本属性");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.上次返回时间), "上次返回时间");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.约定返回时间), "约定返回时间");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.转移部位), "转移部位");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本容量), "标本容量");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.脏器描述), "脏器描述");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本详细描述), "标本详细描述");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊疗卡号), "诊疗卡号");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.住院流水号), "住院流水号");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.当前治疗阶段), "当前治疗阶段");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.手术名称), "手术名称");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.主诊断), "主诊断");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.主诊断形态码), "主诊断形态码");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断1), "诊断1");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断1形态码), "诊断1形态码");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断2), "诊断1");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断2形态码), "诊断2形态码");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断备注), "诊断备注");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.性别), "性别");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.国籍), "国籍");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.民族), "民族");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.住址), "住址");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.婚姻状况), "婚姻状况");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.生日), "生日");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.家庭电话), "家庭电话");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.联系电话), "联系电话");

            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.列), "列");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.行), "行");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本盒位置), "标本盒位置");

            //for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            //{
            //    neuSpread1_Sheet1.Rows[i].Height = 25;
            //    string cureState = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.当前治疗阶段)].Text.Trim();
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
            //        neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.当前治疗阶段)].Text = cureState;
            //        //string boxBarCode = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.标本盒位置)].Text.Trim();
            //        //neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.标本盒位置)].Text = ParseLocation.ParseSpecBox(boxBarCode);
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
            //tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
            //if (detail == 2 || detail == 9 || detail == 8)
            //{
            //    for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            //    {
            //        string specid = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.标本源ID)].Text.Trim();
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
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本源ID), "标本源ID");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.源条码), "源条码");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.姓名), "姓名");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.分装数量), "分装数量"); 
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.病种), "病种");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本类型), "标本类型");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.操作时间), "操作时间");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.送存医生), "送存医生");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.科室), "科室");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.病历号), "病历号");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本条码), "标本条码");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本号), "标本号");
            //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.肿物性质), "肿物性质");
            if (!rbtBld.Checked)
            {
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.取材脏器), "取材脏器");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.肿物性质), "肿物性质");
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本容量)].Visible = false;
                //chkOrgDet.Visible = true;
                //chkOrgDet.Checked = false;
                //chkColPos.Visible = true;
                //chkColPos.Checked = false;
                orgOrBld = "O";
            }
            if (rbtBld.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.取材脏器)].Visible = false;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.肿物性质)].Visible = false;
                //chkOrgDet.Visible = false;
                //chkOrgDet.Checked = false;
                //chkColPos.Visible = false;
                //chkColPos.Checked = false;
                //chkColPos
                orgOrBld = "B";
            }
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.SPECTYPEID), "SPECTYPEID");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.操作人), "操作人");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.备注), "备注");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.配对), "配对");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.在库状态), "在库状态");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本属性), "标本属性");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.上次返回时间), "上次返回时间");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.约定返回时间), "约定返回时间");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.转移部位), "转移部位");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本容量), "标本容量");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.脏器描述), "脏器描述");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本详细描述), "标本详细描述");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊疗卡号), "诊疗卡号");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.住院流水号), "住院流水号");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.当前治疗阶段), "当前治疗阶段");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.手术名称), "手术名称");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.主诊断), "主诊断");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.主诊断形态码), "主诊断形态码");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断1), "诊断1");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断1形态码), "诊断1形态码");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断2), "诊断1");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断2形态码), "诊断2形态码");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.诊断备注), "诊断备注");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.性别), "性别");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.国籍), "国籍");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.民族), "民族");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.住址), "住址");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.婚姻状况), "婚姻状况");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.生日), "生日");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.家庭电话), "家庭电话");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.联系电话), "联系电话");

            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.列), "列");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.行), "行");
            neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本盒位置), "标本盒位置");

            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                neuSpread1_Sheet1.Rows[i].Height = 25;
                string cureState = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.当前治疗阶段)].Text.Trim();
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
                    neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.当前治疗阶段)].Text = cureState;
                    //string boxBarCode = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.标本盒位置)].Text.Trim();
                    //neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.标本盒位置)].Text = ParseLocation.ParseSpecBox(boxBarCode);
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
            tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
            if (detail == 2 || detail == 9 || detail == 8)
            {
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string specid = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.标本源ID)].Text.Trim();
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
        /// 绑定标本类型
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
        /// 绑定冰箱
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
            //查询国家列表
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
                neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].Label = " 标本位置详情";
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
                specOut.PrintTitle = "标本位置详情";
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
            #region 设置项目可见性
            if (neuSpread3_Sheet1.DataSource != null)
            {
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.源条码)].Visible = chkHisBarCode.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.SPECID)].Visible = false;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.SPECID)].Visible = chkSpecId.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.送存医生)].Visible = chkSendDoc.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.科室)].Visible = chkDept.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.操作人)].Visible = chkOperName.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.操作时间)].Visible = chkColTime.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.备注)].Visible = chkComment.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.配对)].Visible = chkMatch.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.住院流水号)].Visible = chkInHosNum.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.当前治疗阶段)].Visible = chkCureDet.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.手术名称)].Visible = chkOperDet.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.性别)].Visible = chkGender.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.国籍)].Visible = chkNationality.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.民族)].Visible = chkNation.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.住址)].Visible = chkAdress.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.婚姻状况)].Visible = chkMarr.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.生日)].Visible = chkBirth.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.联系电话)].Visible = chkContNum.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.家庭电话)].Visible = chkHomeNum.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.主诊断)].Visible = chkMain.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.主诊断形态码)].Visible = chkMod.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.诊断1)].Visible = chkMain1.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.诊断1形态码)].Visible = chkMod1.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.诊断2)].Visible = chkMain2.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.诊断2形态码)].Visible = chkMod2.Checked;
                neuSpread3_Sheet1.Columns[Convert.ToInt32(StoPos.诊断备注)].Visible = chkMain3.Checked;
            }
            
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本源ID)].Visible = chkSpecId.Checked;
            
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.源条码)].Visible = chkHisBarCode.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.病种)].Visible = chkDisType.Checked;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.送存医生)].Visible = chkSendDoc.Checked;
               
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.科室)].Visible = chkDept.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.操作人)].Visible = chkOperName.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.操作时间)].Visible = chkColTime.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.备注)].Visible = chkComment.Checked;
                
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.配对)].Visible = chkMatch.Checked;
                
            

            if (chkSpecNo.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本号)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本号)].Visible = false;
            }

            if (chkSpecCode.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本条码)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本条码)].Visible = false;
            }

            if (chkSpecType.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本类型)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本类型)].Visible = false;
            }

            if (chkSpecType.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本类型)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本类型)].Visible = false;
            }

            if (chkStatus.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.在库状态)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.在库状态)].Visible = false;
            }

            if (chkTumorPor.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.肿物性质)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.肿物性质)].Visible = false;
            }

            if (chkTumor.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本属性)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本属性)].Visible = false;
            }


            if (chkLastRet.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.上次返回时间)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.上次返回时间)].Visible = false;
            }

            if (chkRetTime.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.约定返回时间)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.约定返回时间)].Visible = false;
            }

            if (chkTransPos.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.转移部位)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.转移部位)].Visible = false;
            }

            if (chkCap.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本容量)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本容量)].Visible = false;
            }
            if (chkOrgDet.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.脏器描述)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.脏器描述)].Visible = false;
            }

            //if (chkColPos.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.取材脏器)].Visible = chkColPos.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.取材脏器)].Visible = false;
            //}

            if (chkDet.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本详细描述)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本详细描述)].Visible = false;
            }

            if (chkName.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.姓名)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.姓名)].Visible = false;
            }


            ///
            if (chkCardNo.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.病历号)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.病历号)].Visible = false;
            }

            if (chkIcCardNo.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊疗卡号)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊疗卡号)].Visible = false;
            }

            //if (chkInHosNum.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.住院流水号)].Visible = chkInHosNum.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.住院流水号)].Visible = false;
            //}

            //if (chkCureDet.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.当前治疗阶段)].Visible = chkCureDet.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.当前治疗阶段)].Visible = false;
            //}

            //if (chkOperDet.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.手术名称)].Visible = chkOperDet.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.手术名称)].Visible = false;
            //}

            //if (chkGender.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.性别)].Visible = chkGender.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.性别)].Visible = false;
            //}

            //if (chkNationality.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.国籍)].Visible = chkNationality.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.国籍)].Visible = false;
            //}

            //if (chkNation.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.民族)].Visible = chkNation.Checked;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.民族)].Visible = false;
            //}

            //if (chkAdress.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.住址)].Visible = chkAdress.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.住址)].Visible = false;
            //}

            //if (chkMarr.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.婚姻状况)].Visible = chkMarr.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.婚姻状况)].Visible = false;
            //}

            //if (chkBirth.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.生日)].Visible = chkBirth.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.生日)].Visible = false;
            //}

            //if (chkContNum.Checked)
            //{
              
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.联系电话)].Visible = chkContNum.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.联系电话)].Visible = false;
            //}//chkHomeNum

            //if (chkHomeNum.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.家庭电话)].Visible = chkHomeNum.Checked;
                
            //  neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.家庭电话)].Visible = true;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.家庭电话)].Visible = false;
            //}

            ////
            //if (chkMain.Checked)
            //{
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.主诊断)].Visible = chkMain.Checked;
                
                //neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.主诊断)].Visible = true;
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.主诊断)].Visible = false;
            //}
            //if (chkMod.Checked)
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.主诊断形态码)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.主诊断形态码)].Visible = chkMod.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.主诊断形态码)].Visible = false;
            //}
            //if (chkMain1.Checked)
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断1)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断1)].Visible = chkMain1.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断1)].Visible = false;
            //}

            //if (chkMod1.Checked)
            //{
               // neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断1形态码)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断1形态码)].Visible = chkMod1.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断1形态码)].Visible = false;
            //}
            //if (chkMain2.Checked)
            //{
                //neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断2)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断2)].Visible = chkMain2.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断2)].Visible = false;
            //}

            //if (chkMod2.Checked)
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断2形态码)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断2形态码)].Visible = chkMod2.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断2形态码)].Visible = false;
            //}
            //if (chkMain3.Checked)
            //{
                //neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断备注)].Visible = true;
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断备注)].Visible = chkMain3.Checked;
                
            //}
            //else
            //{
            //    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.诊断备注)].Visible = false;
            //}

            ////
            if (chkRow.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.行)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.行)].Visible = false;
            }
            if (chkCol.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.列)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.列)].Visible = false;
            }
            if (chkLocDet.Checked)
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本盒位置)].Visible = true;
            }
            else
            {
                neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本盒位置)].Visible = false;
            }
            #endregion
        }

        private void Query(int index)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询明细信息，请稍候...");
            Application.DoEvents();
            GetDetailData(index);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (cbxPD.Checked)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询配对信息，请稍候...");
                Application.DoEvents();
                GetSumData();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询库存信息，请稍候...");
            Application.DoEvents();
            GetStockData(1);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            #region 随访信息 lingk20110829
            if (cbxVisitInfo.Checked)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询随访信息，请稍候...");
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
                string inStatus = neuSpread1_Sheet1.Cells[i, Convert.ToInt32(CurPos.在库状态)].Value.ToString();
                if (inStatus == "用完" || inStatus == "借出")
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
                       string boxBarCode = sheetView1.Cells[sheetView1.RowCount - 1, Convert.ToInt32(CurPos.标本盒位置)].Text;
                       try
                       {
                           string loc = ParseLocation.ParseSpecBox(boxBarCode);
                           sheetView1.Cells[sheetView1.RowCount - 1, Convert.ToInt32(CurPos.标本盒位置)].Text = loc;

                       }
                       catch
                       { }
                   }
                }
            }

            sheetView1.Columns[Convert.ToInt32(CurPos.chk)].Visible = false;
            sheetView1.Columns[Convert.ToInt32(CurPos.标本盒位置)].Visible = true;
            sheetView1.Columns[Convert.ToInt32(CurPos.列)].Visible = true;
            sheetView1.Columns[Convert.ToInt32(CurPos.行)].Visible = true;

            this.tabPage5.Text = "已选标本 (共:" + sheetView1.RowCount.ToString() + " )";

        }

        /// <summary>
        /// 保存已存标本
        /// </summary>
        private void SaveChoosedSub()
        {
            if (sheetView1.RowCount == 0)
            {
                MessageBox.Show("没有已选标本！请把已查询数据加入已选标本。");
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
                        MessageBox.Show("申请单号必须是数字,请重新填写");
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
                            MessageBox.Show("申请单号为[" + tmp.ToString() + "]的申请单已经审批不能再次修改！");
                            return;
                        }
                        if (appTmp.User03.ToString() == "已出库")
                        {
                            MessageBox.Show("申请单号为[" + tmp.ToString() + "]的申请单已经出库不能再次修改！");
                            return;
                        }
                    }
                    string rbtChd = "筛选";
                    ArrayList appTab = outOper.GetSubSpecOut(tmp.ToString());
                    if ((appTab != null) && (appTab.Count > 0))
                    {
                        //判断是否为追加数据（specOut.Oper="Imp"申请状态）,还是覆盖数据（原specOut.Oper="Del"删除状态,新数据specOut.Oper="Merge"合并状态）
                        if (rbtEnd.Checked) //|| (this.isAddAppOutData))
                        {
                            string updSql = @"update SPEC_APPLY_OUT set OPER = 'Del' 
                                          where RELATEID = {0}";
                            updSql = string.Format(updSql, tmp.ToString());
                            if (outOper.UpdateSpecOut(updSql) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新原有申请数据失败！");
                                return;
                            }
                            rbtChd = "覆盖筛选";
                            outOper.Oper = "Merge";
                        }
                        if (rbtFirst.Checked) //&& (!this.isAddAppOutData))
                        {
                            rbtChd = "追加筛选";
                            outOper.Oper = "Imp";
                        }
                    }
                    else
                    {
                        outOper.Oper = "Imp";
                    }
                    for (int i = 0; i < sheetView1.RowCount; i++)
                    {
                        string specBarCode = sheetView1.Cells[i, Convert.ToInt32(CurPos.标本条码)].Text.Trim();
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
                                    MessageBox.Show("追加的标本,标本号为[" + specBarCode + "]的标本已经存在，追加失败！");
                                    return;
                                }
                            }
                            SubSpec tmpSpec = new SubSpec();
                            if (outOper.GetSubSpecById(specBarCode, ref tmpSpec) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("根据标本条码获取标本出错！");
                                return;
                            }
                            if (tmpSpec != null)
                            {
                                if (outOper.SaveApplyOutInfo(tmpSpec, 1, "2") <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("保存已选标本出错！");
                                    return;
                                }
                            }
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("根据标本条码获取标本出错，条码号为[" + specBarCode + "]");
                                return;
                            }
                        }
                    }
                    //插入进度表（终筛）
                    UserApply userApply = new UserApply();
                    userApply.ApplyId = tmp;
                    userApply.UserId = loginPerson.ID.ToString();
                    if (rbtChd == "筛选")
                    {
                        userApply.ScheduleId = "Q3";
                    }
                    else if (rbtChd == "覆盖筛选")
                    {
                        userApply.ScheduleId = "Q5";
                    }
                    else if (rbtChd == "追加筛选")
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
                                          where APPLICATIONID = {1} and (SCHEDULE ='终筛' 
                                          or SCHEDULE = '筛选')";
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
                        MessageBox.Show("插入进度表失败!", userApply.Schedule);
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("成功保存已选标本！");
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存已选标本异常错误！");
                    return;
                }
            }
        }

        /// <summary>
        /// 清空已选标本
        /// </summary>
        private void Clear()
        {
            sheetView1.RowCount = 0;
            this.tabPage5.Text = "已选标本 (共:" + sheetView1.RowCount.ToString() + " )";
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
                    MessageBox.Show("请至少选择一项作为条件！");
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

            saveFileDiaglog.Title = "查询结果导出，选择Excel文件保存位置";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "标本";
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
            this.toolBarService.AddToolButton("位置详情", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印预览, true, false, null);
            this.toolBarService.AddToolButton("打印位置信息", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("标本详情", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("加入已选标本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            this.toolBarService.AddToolButton("复合条件过滤", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S设置, true, false, null);
            this.toolBarService.AddToolButton("保存已选标本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M模版删除, true, false, null);
            this.toolBarService.AddToolButton("清空已选标本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            this.toolBarService.AddToolButton("血标本筛选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.W未完成的, true, false, null);
            this.toolBarService.AddToolButton("组织标本筛选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z召回, true, false, null);
            this.toolBarService.AddToolButton("条件查询顺序", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S设备添加, true, false, null);

            this.toolBarService.AddToolButton("多列复合条件过滤", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);

            this.toolBarService.AddToolButton("查询配对随访信息", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查找, true, false, null);

            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "多列复合条件过滤":
                    #region
                    frmUserSelectMuil frmSelect = new frmUserSelectMuil();
                    ArrayList SelectField = new ArrayList();
                    ArrayList arrSelectInfo = new ArrayList();
                    if (this.DtDataView.Count != 0 && (tpQuery.TabPages[2].Visible == true || tpQuery.TabPages[4].Visible == true))
                    {
                        if (tpQuery.TabPages[2].Visible == true)//对详细信息farpoint的筛选
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
                                    DialogResult result = MessageBox.Show("当前筛选是否清除之前过滤条件？", "提示", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        this.DtDataView.RowFilter = frmSelect.SelectSql;
                                        tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
                                    }
                                    else
                                    {
                                        this.DtDataView.RowFilter +=" AND "+ frmSelect.SelectSql;
                                        tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
                                    }
                                }
                                else
                                {
                                    this.DtDataView.RowFilter = frmSelect.SelectSql;
                                    tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
                                }
                            }
                        }
                        else//对库存信息farpoint的筛选
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
                                    DialogResult result = MessageBox.Show("当前筛选是否清除之前过滤条件？", "提示", MessageBoxButtons.YesNo);
                                    if (result == DialogResult.Yes)
                                    {
                                        this.StkDataView.RowFilter = frmSelect.SelectSql;
                                        tpQuery.TabPages[4].Text = "库存信息 (共: " + neuSpread3_Sheet1.RowCount.ToString() + " 条)";
                                    }
                                    else
                                    {
                                        this.StkDataView.RowFilter += " AND " + frmSelect.SelectSql;
                                        tpQuery.TabPages[4].Text = "库存信息 (共: " + neuSpread3_Sheet1.RowCount.ToString() + " 条)";
                                    }
                                }
                                else
                                {
                                    this.StkDataView.RowFilter = frmSelect.SelectSql;
                                    tpQuery.TabPages[4].Text = "库存信息 (共: " + neuSpread3_Sheet1.RowCount.ToString() + " 条)";
                                }
                            }
                            //设置下格式，要不跟之前不一样了
                            for (int i = 0; i < neuSpread3_Sheet1.ColumnCount; i++)
                            {
                                neuSpread3_Sheet1.Columns[i].Width = 80;
                            }
                            neuSpread3_Sheet1.Columns[5].Visible = false;
                            //if (!string.IsNullOrEmpty(frmSelect.SelectSql))
                            //{
                            //    this.DtDataView.RowFilter += " AND " + frmSelect.SelectSql;
                            //}
                            #region 将库存信息的筛选同步到详细信息中
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
                                //记录原来明细的dataview
                                this.oldDv = this.DtDataView;
                            }
                            neuSpread1_Sheet1.DataSource = this.DtDataView;
                            tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
                            #endregion
                        }
                    }
                    #endregion
                    break;

                case "条件查询顺序":
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
                            //设置下格式，要不跟之前不一样了
                            for (int i = 0; i < neuSpread3_Sheet1.ColumnCount; i++)
                            {
                                neuSpread3_Sheet1.Columns[i].Width = 80;
                            }
                            neuSpread3_Sheet1.Columns[5].Visible = false;
                        }
                    }
                    #endregion
                    break;

                case "复合条件过滤":
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
                            DialogResult result = MessageBox.Show("当前筛选是否清除之前过滤条件？", "提示", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                if (!string.IsNullOrEmpty(frmFilter.RtFilter))
                                {
                                    try
                                    {
                                        string filter = "1=1";
                                        filter = frmFilter.RtFilter;
                                        this.DtDataView.RowFilter = filter;
                                        tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
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
                                        tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
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
                                    tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
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
                        tpQuery.TabPages[2].Text = "详细信息 (共: " + neuSpread1_Sheet1.RowCount.ToString() + " 条)";
                    }
                    #endregion
                    break;
                case "加入已选标本":
                    AddSpec();
                    break;
                case "查询配对随访信息":
                    try
                    {
                        //配对
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询配对信息，请稍候...");
                        Application.DoEvents();
                        GetSumData();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        //随访
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询随访信息，请稍候...");
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
                case "保存已选标本":
                    this.SaveChoosedSub();
                    break;
                case "清空已选标本":
                    this.Clear();
                    break;
                case "位置详情":
                    this.GetSubPos();
                    break;
                case "组织标本筛选":
                    if (!this.rbtOrg.Checked)
                    {
                        MessageBox.Show("请确认选择标本是组织标本后再使用此功能，谢谢！");
                    }
                    else if (StkDataView.Count == 0)
                    {
                        MessageBox.Show("请先查询标本，然后进行筛选，谢谢！");
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
                                tpStock.Text = "库存信息 (共 " + neuSpread3_Sheet1.RowCount.ToString() + " 条记录";
                            }
                            if (!string.IsNullOrEmpty(this.bldFilter))
                            {
                                this.isOldData = false;
                                this.GetDetailData(10);
                            }
                            else //取消筛选
                            {
                                this.DtDataView = this.oldDv;
                                neuSpread1_Sheet1.DataSource = this.DtDataView;
                                this.BindingSheet1(2);
                            }
                        }
                    }
                    break;
                case "血标本筛选":
                    if (!this.rbtBld.Checked)
                    {
                        MessageBox.Show("请确认选择标本是血标本后再使用此功能，谢谢！");
                    }
                    else if (StkDataView.Count== 0)
                    {
                        MessageBox.Show("请先查询标本，然后进行筛选，谢谢！");
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
                                tpStock.Text = "库存信息 (共 " + neuSpread3_Sheet1.RowCount.ToString() + " 条记录";
                            }
                            if (!string.IsNullOrEmpty(this.bldFilter))
                            {
                                this.isOldData = false;
                                this.GetDetailData(10);
                            }
                            else //取消筛选
                            {
                                this.DtDataView = this.oldDv;
                                neuSpread1_Sheet1.DataSource = this.DtDataView;
                                this.BindingSheet1(2);
                            }
                        }
                    }
                    break;
                case "标本详情":

                    try
                    {
                        int specId = Convert.ToInt32(neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRow.Index, Convert.ToInt32(CurPos.标本源ID)].Text.Trim());

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
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询库存信息，请稍候...");
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
                    DialogResult dRt = MessageBox.Show("没有选中病种是否继续查询？", "提示", MessageBoxButtons.YesNo);
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
                MessageBox.Show("查询Excel异常错误，请检查Excel中数据是否正确！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbDisType.Text.Trim()))
                {
                    DialogResult dRt = MessageBox.Show("没有选中病种是否继续查询？", "提示", MessageBoxButtons.YesNo);
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
                MessageBox.Show("查询Excel异常错误，请检查Excel中数据是否正确！");
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
            //1、输入申请人、科室及时间的查询（返回结果数组去选择申请单号，类似收费时弹出多次挂号患者）
            //2、直接输入申请号关联
            applyID = this.txtApplyNum.Text.Trim();
            #region 有填写申请号查询
            if (!string.IsNullOrEmpty(applyID))
            {
                if (this.sheetView1.RowCount > 0)
                {
                    DialogResult result = MessageBox.Show("查询数据是否清除当前已选标本数据？", "提示", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        this.sheetView1.RowCount = 0;
                    }
                }
                this.SetApplyData(applyID);
            }
            #endregion
            #region 填写其他信息查询
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
                        DialogResult result = MessageBox.Show("查询数据是否清除当前已选标本数据？", "提示", MessageBoxButtons.YesNo);
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
                cbVistDead.Text = "死亡";
            }
            else if (cbVistDead.CheckState == CheckState.Unchecked)
            {
                cbVistDead.Text = "未死亡";
            }
            else
            {
                cbVistDead.Text = "全部";
            }
        }

    }
}
