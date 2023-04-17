using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Controls
{
    /// <summary>
    /// 中大五院病房工作日志
    /// </summary>
    public partial class ucShiftRecord : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucShiftRecord()
        {
            InitializeComponent();
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
        }

        void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }

        #region 变量

        /// <summary>
        /// 是否显示时间段，否则只能查询一天
        /// </summary>
        private bool isShowDateBetween = false;

        /// <summary>
        /// 是否显示时间段，否则只能查询一天
        /// </summary>
        [Category("查询设置"), Description("是否显示时间段，否则只能查询一天")]
        public bool IsShowDateBetween
        {
            get
            {
                return isShowDateBetween;
            }
            set
            {
                isShowDateBetween = value;
            }
        }

        /// <summary>
        /// 是否是病案室使用
        /// </summary>
        bool isUseForBA = false;

        /// <summary>
        /// 是否是病案室使用
        /// </summary>
        [Category("查询设置"), Description("是否是病案室使用")]
        public bool IsUseForBA
        {
            get
            {
                return isUseForBA;
            }
            set
            {
                isUseForBA = value;

                lblDept.Visible = value;
                cmbDept.Visible = value;
            }
        }

        #region SQL

        /// <summary>
        /// 原有人数SQL
        /// </summary>
        private string sql_原有人数 = @"select nvl(count(distinct g.clinic_no),0)
from com_bedinfo_log g
where (g.dept_code in (select rr.dept_code
  from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
   and rr.stat_code = '01'
   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
and g.clinic_no !='N'
and g.log_date>= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')-1
and g.log_date<= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
/*and g.dept_code in (select y.code from com_dictionary y
       where y.type='DEPTBED'
       and y.code=g.dept_code
       and y.valid_state='1')*/";

        /// <summary>
        /// 原有人数SQL
        /// </summary>
        [Category("查询设置"), Description("原有人数SQL")]
        public string Sql_原有人数
        {
            get { return sql_原有人数; }
            set { sql_原有人数 = value; }
        }

//        private string sql_入院人数 = @"--入院人数
//        select nvl(sum(case
//                         when s.shift_type = 'K' then
//                          count((s.clinic_no))
//                         when s.shift_type = 'C' then
//                          count((s.clinic_no))
//                         when s.shift_type = 'EC' then
//                          count((s.clinic_no))
//                         when s.shift_type = 'CK' then
//                          -count((s.clinic_no))
//                         else
//                          0
//                       end),
//                   0) 入院人数
//          from com_shiftdata s, fin_ipr_inmaininfo i
//         where s.clinic_no = i.inpatient_no
//           and (s.shift_type in('K','CK','C','EC')) /*'接诊'*/
//            
//         and (s.oper_deptcode  in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//           and i.patient_no not like 'B%'
//           and not exists(select 1 from com_shiftdata a
//           where a.clinic_no=s.clinic_no
//           and a.shift_type in ('OF')
//           )
//           and s.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
//           and s.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
//         group by s.shift_type";

        private string sql_入院人数 = @"--入院人数
        select nvl(sum(case
                         when s.shift_type = 'K' then
                          count((s.clinic_no))
                         when (select nvl(sum(case
                                    when c.shift_type = 'BA' then
                                     count((c.clinic_no))*2
                                     when c.shift_type = 'C' then
                                     -count((c.clinic_no))
                                    else
                                     0
                                  end),
                              0)
                             from com_shiftdata c
                            where c.clinic_no in (select s.clinic_no from com_shiftdata s)
                              and c.shift_type in ('BA','C')
                            group by c.shift_type) = '1'
                            THEN COUNT((S.CLINIC_NO))
                         when s.shift_type = 'CK' then
                          -count((s.clinic_no))
                         else
                          0
                       end),
                   0) 入院人数
          from com_shiftdata s, fin_ipr_inmaininfo i
         where s.clinic_no = i.inpatient_no
           and (s.shift_type in('K','CK','C')) /*'接诊'*/
            
         and (s.oper_deptcode  in (select rr.dept_code
  from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
   and rr.stat_code = '01'
   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
           and i.patient_no not like 'B%'
           and not exists(select 1 from com_shiftdata a
           where a.clinic_no=s.clinic_no
           and a.shift_type in ('OF')
           )
           and s.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
           and s.oper_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
         group by s.shift_type";

        /// <summary>
        /// 入院人数SQL
        /// </summary>
        [Category("查询设置"), Description("入院人数SQL")]
        public string Sql_入院人数
        {
            get { return sql_入院人数; }
            set { sql_入院人数 = value; }
        }

        private string sql_出院人数 = @"--出院汇总
            select nvl(sum(出院病人数),0) 出院病人数 from (
            select (case when s.shift_type='O' then count((s.clinic_no)) else 0 end) 出院病人数
            from com_shiftdata s,fin_ipr_inmaininfo i
            where s.clinic_no=i.inpatient_no
            and s.shift_type in ('O')/*出院等级*/
            and (s.old_data_code  in (select rr.dept_code
  from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
   and rr.stat_code = '01'
   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
                and s.shift_cause not in ('修改科室','修改病区')
            and i.patient_no not like 'B%'
                       and not exists(select 1 from com_shiftdata a
                       where a.clinic_no=s.clinic_no
                       and a.shift_type='OF')
            and s.oper_date>= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
            and s.oper_date<= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
            group by s.shift_type)tt";
//        private string sql_出院人数 = @"--出院汇总
//select nvl(sum(出院病人数), 0) 出院病人数
//  from (select (case
//                 when s.shift_type = 'O' then
//                  count((s.clinic_no))
//                 when s.shift_type = 'EO' then
//                  count((s.clinic_no))
//                 else
//                  0
//               end) 出院病人数
//          from com_shiftdata s, fin_ipr_inmaininfo i
//         where s.clinic_no = i.inpatient_no
//           and s.shift_type in ('O','EO') /*出院等级*/
//           and (s.old_data_code in
//               (select rr.dept_code
//                   from com_deptstat rr
//                  where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//                    and rr.stat_code = '01'
//                    and rr.pardep_code <> 'AAAA') or '{2}' = 'ALL')
//           and s.shift_cause not in ('修改科室', '修改病区')
//           and i.patient_no not like 'B%'
//           and not exists
//         (select 1
//                  from com_shiftdata a
//                 where a.clinic_no = s.clinic_no
//                   and a.shift_type = 'OF')
//           and s.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
//           and s.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
//         group by s.shift_type) tt
//";
        /// <summary>
        /// 死亡人数SQL
        /// </summary>
        [Category("查询设置"), Description("死亡人数SQL")]
        public string Sql_死亡人数
        {
            get { return sql_死亡人数; }
            set { sql_死亡人数 = value; }
        }

        private string sql_死亡人数 = @"--出院汇总
select nvl(sum(出院病人数),0) 出院病人数 from (
select (case when s.shift_type='O' then count((s.clinic_no)) else 0 end) 出院病人数
from com_shiftdata s,fin_ipr_inmaininfo i
where s.clinic_no=i.inpatient_no
and s.shift_type = 'O'/*出院等级*/
and (s.old_data_code  in (select rr.dept_code
  from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
   and rr.stat_code = '01'
   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
and s.shift_cause not in ('修改科室','修改病区')
and i.patient_no not like 'B%'
           and not exists(select 1 from com_shiftdata a
           where a.clinic_no=s.clinic_no
           and a.shift_type='OF')
and i.zg =(select y.code from com_dictionary y
                  where y.type='ZG'
                  and y.valid_state='1'
                  and y.name like '%死亡%')
and s.oper_date>= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and s.oper_date<= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
group by s.shift_type)tt";

        /// <summary>
        /// 24死亡人数SQL
        /// </summary>
        [Category("查询设置"), Description("24死亡人数SQL")]
        public string Sql_24死亡人数
        {
            get { return sql_24死亡人数; }
            set { sql_24死亡人数 = value; }
        }

        private string sql_24死亡人数 = @"--出院汇总
select nvl(sum(出院病人数),0) 出院病人数 from (
select (case when s.shift_type='O' then count((s.clinic_no)) else 0 end) 出院病人数
from com_shiftdata s,fin_ipr_inmaininfo i
where s.clinic_no=i.inpatient_no
and s.shift_type = 'O'/*出院等级*/
and (s.old_data_code  in (select rr.dept_code
  from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
   and rr.stat_code = '01'
   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
and s.shift_cause not in ('修改科室','修改病区')
and i.patient_no not like 'B%'
           and not exists(select 1 from com_shiftdata a
           where a.clinic_no=s.clinic_no
           and a.shift_type='OF')
and i.out_date<i.in_date+1
and i.zg =(select y.code from com_dictionary y
                  where y.type='ZG'
                  and y.valid_state='1'
                  and y.name like '%死亡%')
and s.oper_date>= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and s.oper_date<= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
group by s.shift_type)tt";

        /// <summary>
        /// 出院人数SQL
        /// </summary>
        [Category("查询设置"), Description("出院人数SQL")]
        public string Sql_出院人数
        {
            get { return sql_出院人数; }
            set { sql_出院人数 = value; }
        }

//        private string sql_转入人数 = @"--转入病人数
//select nvl(sum(转入病人数),0) 转入病人数 from (
//select (case when s.shift_type='CN' then count(distinct(s.clinic_no)) else 0 end) 转入病人数
//from com_shiftdata s,fin_ipr_inmaininfo i
//where s.clinic_no=i.inpatient_no
//and s.shift_type = 'RI' /*转入*/
//and (s.new_data_code  in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//    and s.shift_cause not in ('修改科室','修改病区')
//and i.patient_no not like 'B%'
//           and not exists(select 1 from com_shiftdata a
//           where a.clinic_no=s.clinic_no
//           and a.shift_type='OF')
//and s.oper_date>= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
//and s.oper_date< to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
//group by s.shift_type)tt";
        private string sql_转入人数 = @"--转入病人数
select nvl(sum(转入病人数),0) 转入病人数 from (
select (case when s.shift_type='RI' then count(s.clinic_no) else 0 end) 转入病人数
from com_shiftdata s,fin_ipr_inmaininfo i
where s.clinic_no=i.inpatient_no
and s.shift_type = 'RI' /*转入*/
and (s.new_data_code  in (select rr.dept_code
 from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
  and rr.stat_code = '01'
  and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
    and s.shift_cause not in ('修改科室','修改病区')
and i.patient_no not like 'B%'
           and not exists(select 1 from com_shiftdata a
           where a.clinic_no=s.clinic_no
           and a.shift_type='OF')
and s.oper_date>= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and s.oper_date< to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
group by s.shift_type)tt";

        /// <summary>
        /// 转入人数SQL
        /// </summary>
        [Category("查询设置"), Description("转入人数SQL")]
        public string Sql_转入人数
        {
            get { return sql_转入人数; }
            set { sql_转入人数 = value; }
        }

//        private string sql_转出人数 = @"--转出病人数
//select nvl(sum(转出病人数),0) 转出病人数 from (
//select (case when s.shift_type='CNO' then count(distinct(s.clinic_no)) else 0 end) 转出病人数
//from com_shiftdata s,fin_ipr_inmaininfo i
//where s.clinic_no=i.inpatient_no
//and s.shift_type='RO' /*转出*/
//and (s.old_data_code   in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//    and s.shift_cause not in ('修改科室','修改病区')
//and i.patient_no not like 'B%'
//           and not exists(select 1 from com_shiftdata a
//           where a.clinic_no=s.clinic_no
//           and a.shift_type='OF')
//and s.oper_date>= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
//and s.oper_date< to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
//group by s.shift_type)tt";//and (s.old_data_code ='{2}' or '{2}'='ALL')
        private string sql_转出人数 = @"--转出病人数
select nvl(sum(转出病人数),0) 转出病人数 from (
select (case when s.shift_type='RO' then count(s.clinic_no) else 0 end) 转出病人数
from com_shiftdata s,fin_ipr_inmaininfo i
where s.clinic_no=i.inpatient_no
and s.shift_type='RO' /*转出*/
and (s.old_data_code   in (select rr.dept_code
  from com_deptstat rr
  where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
  and rr.stat_code = '01'
  and rr.pardep_code <> 'AAAA') or '{2}'='ALL')

    and s.shift_cause not in ('修改科室','修改病区')
and i.patient_no not like 'B%'
           and not exists(select 1 from com_shiftdata a
           where a.clinic_no=s.clinic_no
           and a.shift_type='OF')
and s.oper_date>= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
and s.oper_date< to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
group by s.shift_type)tt";

        /// <summary>
        /// 转出人数SQL
        /// </summary>
        [Category("查询设置"), Description("转出人数SQL")]
        public string Sql_转出人数
        {
            get { return sql_转出人数; }
            set { sql_转出人数 = value; }
        }
        /// <summary>
        /// 入出转SQL
        /// </summary>
        private string sql_入出转 = @"--入出转明细
select distinct --shifttype,
                --科室,
                 床号,
                住院号,
                姓名,
                性别,
                --年龄,
                --入本区时间,
                --病历回收时间,
                入院诊断,
                出院诊断,
                情况,
                转入前原病区,
                转往病区,
                类型,
                oper_date
  from (select (case
                 when s.shift_type = 'CN' or s.shift_type = 'O' then
                  s.new_data_name
                 else
                  s.old_data_name
               end) 科室,
               replace(i.bed_no, i.nurse_cell_code, '') 床号,
               i.patient_no 住院号,
               i.name 姓名,
               decode(i.sex_code, 'M', '男', 'F', '女') 性别,
               fun_get_age(i.birthday) 年龄,
               (case
                 when s.shift_type = 'CN' then
                  s.oper_date
                 else
                  i.in_date
               end) 入本区时间,
               '' 病历回收时间,
               (case
                 when s.shift_type = 'O' then
                  '出院病人'
                  when s.shift_type='EO' then
                  '出院登记取消'
                 when s.shift_type = 'RO' then
                  '转出病区'
                 when s.shift_type = 'RI' then
                  '转入病区'
               
                 when (select nvl(sum(case
                                        when c.shift_type = 'K' then
                                         count((c.clinic_no))
                                      
                                        when c.shift_type = 'CK' then
                                         -count((c.clinic_no))
                                        else
                                         0
                                      end),
                                  0)
                         from com_shiftdata c
                        where c.clinic_no = s.clinic_no
                          and c.oper_date >=
                              to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
                          and c.oper_date <=
                              to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                          and c.shift_type in ('K', 'CK')
                        group by c.shift_type) = '1' then
                  '入院病人'
                  when s.shift_type = 'C' then
                  '出院召回'
                  when s.shift_type = 'EC' then
                  '出院召回取消'
               end) 类型,
               (select to_char(wm_concat(f.CLINIC_DIAGNOSE))
                  from V_FIN_IPR_INMAININFO f
                 where f.inpatient_no = i.inpatient_no) 入院诊断,
               (case s.shift_type
                 when 'O' then
                  (select to_char(wm_concat(f.diag_name))
                     from met_cas_diagnose f
                    where f.inpatient_no = i.inpatient_no
                      and f.valid_flag = '1'
                      and f.diag_kind in ('14', '15'))
               end) 出院诊断,
               (case s.shift_type
                 when 'O' then
                  (SELECT C.NAME
                     FROM COM_DICTIONARY C
                    WHERE C.TYPE = 'ZG'
                      AND C.CODE = i.zg)
               end) 情况,
               (case s.shift_type
                 when 'RI' then
                  old_data_name
               end) 转入前原病区,
               (case s.shift_type
                 when 'RO' then
                  new_data_name
               end) 转往病区,
               s.shift_type shifttype,
               s.oper_date
          from com_shiftdata s, fin_ipr_inmaininfo i
         where s.clinic_no = i.inpatient_no
           and (((s.shift_type in('K','C','EC') /*'接诊'*/
               ) and not exists
                (select 1
                    from com_shiftdata a
                   where a.clinic_no = s.clinic_no
                     and a.shift_type in ('OF')) and
                (s.oper_deptcode in
                (select rr.dept_code
                     from com_deptstat rr
                    where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
                      and rr.stat_code = '01'
                      and rr.pardep_code <> 'AAAA') or '{2}' = 'ALL')) or
               (s.shift_type = 'RI' /*转入*/
               and
               (s.new_data_code in
               (select rr.dept_code
                     from com_deptstat rr
                    where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
                      and rr.stat_code = '01'
                      and rr.pardep_code <> 'AAAA') or '{2}' = 'ALL') and
               s.shift_cause not in ('修改科室', '修改病区')) or
               ((s.shift_type in ('O','EO') /*出院登记*/
               ) and
               (s.old_data_code in
               (select rr.dept_code
                     from com_deptstat rr
                    where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
                      and rr.stat_code = '01'
                      and rr.pardep_code <> 'AAAA') or '{2}' = 'ALL') and
               s.shift_cause not in ('修改科室', '修改病区')) or
               (s.shift_type = 'RO' /*转出*/
               and
               (s.old_data_code in
               (select rr.dept_code
                     from com_deptstat rr
                    where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
                      and rr.stat_code = '01'
                      and rr.pardep_code <> 'AAAA') or '{2}' = 'ALL') and
               s.shift_cause not in ('修改科室', '修改病区')))
           and i.patient_no not like 'B%'
           and not exists
         (select 1
                  from com_shiftdata a
                 where a.clinic_no = s.clinic_no
                   and a.shift_type IN ('OF'))
           and s.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
           and s.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
 order by 类型 desc, oper_date
";
//        private string sql_入出转 = @"
//                    --入出转明细
//                    select distinct --shifttype,
//                                    --科室,
//                                    床号,
//                                    住院号,
//                                    姓名,
//                                    性别,
//                                    --年龄,
//                                    --入本区时间,
//                                    --病历回收时间,
//                                    入院诊断,
//                                    出院诊断,
//                                    情况,
//                                    转入前原病区,
//                                    转往病区,
//                                    类型,
//                                    oper_date
//                      from (select (case
//                                     when s.shift_type = 'CN' or s.shift_type = 'O' then
//                                      s.new_data_name
//                                     else
//                                      s.old_data_name
//                                   end) 科室,
//                                   replace(i.bed_no, i.nurse_cell_code, '') 床号,
//                                   i.patient_no 住院号,
//                                   i.name 姓名,
//                                   decode(i.sex_code, 'M', '男', 'F', '女') 性别,
//                                   fun_get_age(i.birthday) 年龄,
//                                   (case
//                                     when s.shift_type = 'CN' then
//                                      s.oper_date
//                                     else
//                                      i.in_date
//                                   end) 入本区时间,
//                                   '' 病历回收时间,
//                                   (case when s.shift_type
//                                     = 'O' then
//                                      '出院病人'
//                                    when s.shift_type = 'RO' then
//                                      '转出病区'
//                                    when s.shift_type = 'RI' then
//                                      '转入病区'
//
//                                     when (select nvl(sum(case
//                                           when c.shift_type = 'K' then
//                                           count((c.clinic_no))
//
//                                           when c.shift_type = 'CK' then
//                                           -count((c.clinic_no))
//                                           else
//                                           0
//                                           end),
//                                           0) from com_shiftdata c 
//                                           where c.clinic_no=s.clinic_no 
//                                           and c.oper_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
//                                           and c.oper_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
//                                           and c.shift_type in('K','CK') group by c.shift_type)='1'
//                                     then '入院病人'
//                                     when (select nvl(sum(case
//                                    when c.shift_type = 'BA' then
//                                     count((c.clinic_no))*2
//                                     when c.shift_type = 'C' then
//                                     -count((c.clinic_no))
//                                    else
//                                     0
//                                  end),
//                              0)
//                             from com_shiftdata c
//                            where c.clinic_no = s.clinic_no
//                              and c.shift_type in ('BA','C')
//                            group by c.shift_type) = '1' then                 
//                                         '出院召回'             end) 类型, 
//                                   (select to_char(wm_concat(f.CLINIC_DIAGNOSE))
//                                     from V_FIN_IPR_INMAININFO f
//                                     where f.inpatient_no = i.inpatient_no) 入院诊断,
//                                   (case s.shift_type
//                                     when 'O' then
//                                      (select to_char(wm_concat(f.diag_name))
//                                         from met_cas_diagnose f
//                                        where f.inpatient_no = i.inpatient_no
//                                        and f.valid_flag='1'
//                                        and f.diag_kind in ('14','15'))
//                                   end) 出院诊断,
//                                    (case s.shift_type
//                                     when 'O' then  (SELECT C.NAME FROM COM_DICTIONARY C WHERE C.TYPE = 'ZG' AND C.CODE = i.zg) end) 情况,                    
//                                      (case s.shift_type   when 'RI' then old_data_name    end) 转入前原病区, 
//                                      (case s.shift_type   when 'RO' then new_data_name    end) 转往病区,
//                                   s.shift_type shifttype,           
//                                   s.oper_date
//                              from com_shiftdata s, fin_ipr_inmaininfo i
//                             where s.clinic_no = i.inpatient_no
//                             and  (((s.shift_type='K' /*'接诊'*/
//                                    or s.shift_type='C' /*召回*/)
//                                 and not exists(select 1 from com_shiftdata a
//                                   where a.clinic_no=s.clinic_no
//                                   and a.shift_type in ('OF')
//                                   )
//                                and (s.oper_deptcode  in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL'))
//                                  or (s.shift_type='RI'/*转入*/
//                                      and (s.new_data_code   in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//                                      and s.shift_cause not in ('修改科室', '修改病区'))
//                                  or ((s.shift_type in ('O') /*出院登记*/
//                                       )  
//                                       and (s.old_data_code  in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//                                       and s.shift_cause not in ('修改科室', '修改病区'))
//                                   or (s.shift_type = 'RO' /*转出*/
//                                       and (s.old_data_code  in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//                                       and s.shift_cause not in ('修改科室', '修改病区'))
//                                  )
//                               and i.patient_no not like 'B%'
//                               and not exists(select 1 from com_shiftdata a
//                               where a.clinic_no=s.clinic_no
//                               and a.shift_type IN('OF')
//                              )
//                               and s.oper_date >=
//                                   to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
//                               and s.oper_date <=
//                                   to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
//                     order by 类型 desc,oper_date";
        /// <summary>
        /// 入出转SQL
        /// </summary>
        [Category("查询设置"), Description("入出转SQL")]
        public string Sql_入出转
        {
            get { return sql_入出转; }
            set { sql_入出转 = value; }
        }

        string sql_现有人数 = @"select sum(人数)
                        from
                        (select case when  to_date('{1}','yyyy-mm-dd hh24:mi:ss')=TRUNC(SYSDATE)+1-1/86400
         then (select count(*)  from fin_ipr_inmaininfo g
       where (g.dept_code  in (select rr.dept_code
  from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
   and rr.stat_code = '01'
   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
           and g.patient_no not like 'B%'
       and g.in_state='I')
           else (select nvl(count(distinct g.clinic_no), 0)  from com_bedinfo_log g
       where (g.dept_code  in (select rr.dept_code
  from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
   and rr.stat_code = '01'
   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
       and g.bed_state!='W'
       and g.bed_state!='N'
       and g.log_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')-1
       and g.log_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss'))
       end 人数 from dual)";

        public string Sql_现有人数
        {
            get { return sql_现有人数; }
            set { sql_现有人数 = value; }
        }

//        string sql_陪人数 = @"select nvl(count(*),0)
//          from met_ipm_order r
//         where r.item_name like '%陪人%'
//           and r.mo_stat != '3'
//           and not exists(select 1 from com_shiftdata a
//           where a.clinic_no=r.inpatient_no
//           and a.shift_type='OF')
//           and (r.dept_code  in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//           and r.date_bgn >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
//           and r.date_bgn < to_date('{1}','yyyy-mm-dd hh24:mi:ss')";
        string sql_陪人数 = @"select (select nvl(count(distinct r.inpatient_no), 0) from met_ipm_order r 
where r.item_name like '%陪%人%'
and r.mo_stat not in ('3','4') 
and r.execute_flag='0'  
and r.date_bgn < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')   
and not exists (select 1  from com_shiftdata a   
where a.clinic_no = r.inpatient_no   
 and a.shift_type in ('OF')) 
 and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
 and r.date_end < to_date('0002-01-01 00:00:01', 'yyyy-mm-dd hh24:mi:ss')) + 
(select nvl(count(distinct r.inpatient_no), 0) from met_ipm_order r 
where r.item_name like '%陪%人%'
and r.mo_stat not in ('3','4') 
and r.execute_flag='1'  
and r.date_bgn > to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')   
and not exists (select 1  from com_shiftdata a   
where a.clinic_no = r.inpatient_no   
 and a.shift_type in ('OF')) 
 and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
 and r.date_bgn < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')) + 
  (select count(distinct r.inpatient_no)    
   from met_ipm_order r where r.item_name like '%陪%人%'
    and r.mo_stat in('3','4') and not exists (select 1 from com_shiftdata a    
    where a.clinic_no = r.inpatient_no and a.shift_type in ('OF'))
    and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
    and r.date_bgn <to_date('{1}','yyyy-mm-dd hh24:mi:ss')
    and r.date_end > to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))from dual";

        public string Sql_陪人数
        {
            get { return sql_陪人数; }
            set { sql_陪人数 = value; }
        }

//        string sql_一级护理 = @"select nvl(count(*),0)
//          from met_ipm_order r
//         where r.item_name like '%一级护理%'
//           and r.mo_stat != '3'
//           and not exists(select 1 from com_shiftdata a
//           where a.clinic_no=r.inpatient_no
//           and a.shift_type='OF')
//           and (r.dept_code  in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//           and r.date_bgn >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
//           and r.date_bgn < to_date('{1}','yyyy-mm-dd hh24:mi:ss')";
        string sql_一级护理 = @"select (select nvl(count(distinct r.inpatient_no), 0) from met_ipm_order r 
where r.item_name like '%一级护理%'
and r.mo_stat not in ('3','4') 
and r.execute_flag='0' 
and r.date_bgn < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')   
and not exists (select 1  from com_shiftdata a   
where a.clinic_no = r.inpatient_no   
 and a.shift_type in ('OF')
 ) 
 and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
 and r.date_end < to_date('0002-01-01 00:00:01', 'yyyy-mm-dd hh24:mi:ss')) + (select nvl(count(distinct r.inpatient_no), 0) from met_ipm_order r 
where r.item_name like '%一级护理%'
and r.mo_stat not in ('3','4') 
and r.execute_flag='1' 
and r.date_bgn > to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')   
and not exists (select 1  from com_shiftdata a   
where a.clinic_no = r.inpatient_no   
 and a.shift_type in ('OF')
 ) 
 and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
 and r.date_bgn < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')) + 
  (select count(distinct r.inpatient_no)    
   from met_ipm_order r where r.item_name like '%一级护理%'
    and r.mo_stat in('3','4') and not exists (select 1 from com_shiftdata a    
    where a.clinic_no = r.inpatient_no and a.shift_type in ('OF')
    )
    and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
    and r.date_bgn <to_date('{1}','yyyy-mm-dd hh24:mi:ss')
    and r.date_end > to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))from dual";

        public string Sql_一级护理
        {
            get { return sql_一级护理; }
            set { sql_一级护理 = value; }
        }

//        string sql_危重病人数 = @"select nvl(count(*),0)
//          from met_ipm_order r
//         where (r.item_name like '%病危%' or r.item_name like '%病重%')
//           and r.mo_stat != '3'
//           and not exists(select 1 from com_shiftdata a
//           where a.clinic_no=r.inpatient_no
//           and a.shift_type='OF')
//           and (r.dept_code  in (select rr.dept_code
//  from com_deptstat rr
// where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
//   and rr.stat_code = '01'
//   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
//           and r.date_bgn >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
//           and r.date_bgn < to_date('{1}','yyyy-mm-dd hh24:mi:ss')";
        string sql_危重病人数 = @"select (select nvl(count(distinct r.inpatient_no), 0) from met_ipm_order r 
where (r.item_name like '%病危%' or r.item_name like '%病重%')
and r.mo_stat not in ('3','4')  
and r.execute_flag='0'
and r.date_bgn < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')   
and not exists (select 1  from com_shiftdata a   
where a.clinic_no = r.inpatient_no   
 and a.shift_type in ('OF')
 ) 
 and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
 and r.date_end < to_date('0002-01-01 00:00:01', 'yyyy-mm-dd hh24:mi:ss')) + (select nvl(count(distinct r.inpatient_no), 0) from met_ipm_order r 
where (r.item_name like '%病危%' or r.item_name like '%病重%')
and r.mo_stat not in ('3','4')  
and r.execute_flag='1'
and r.date_bgn >to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')   
and not exists (select 1  from com_shiftdata a   
where a.clinic_no = r.inpatient_no   
 and a.shift_type in ('OF')
 ) 
 and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
 and r.date_bgn < to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')) + 
  (select count(distinct r.inpatient_no)    
   from met_ipm_order r 
   where (r.item_name like '%病危%' or r.item_name like '%病重%')
    and r.mo_stat in('3','4') and not exists (select 1 from com_shiftdata a    
    where a.clinic_no = r.inpatient_no and a.shift_type in ('OF')
          )
    and (r.dept_code  in (select rr.dept_code 
 from com_deptstat rr where (rr.pardep_code = '{2}' or rr.dept_code = '{2}') 
 and rr.stat_code = '01' and rr.pardep_code <> 'AAAA') or '{2}'='ALL') 
    and r.date_bgn <to_date('{1}','yyyy-mm-dd hh24:mi:ss')
    and r.date_end > to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))from dual";

        public string Sql_危重病人数
        {
            get { return sql_危重病人数; }
            set { sql_危重病人数 = value; }
        }

//        string sql_新生儿人数 = @"--入院人数
//        select nvl(sum(case
//                         when s.shift_type = 'K' then
//                          count((s.clinic_no))
//                         when s.shift_type = 'C' then
//                          count((s.clinic_no))
//                         else
//                          0
//                       end),
//                   0) 入院人数
//          from com_shiftdata s, fin_ipr_inmaininfo i
//         where s.clinic_no = i.inpatient_no
//           and ((s.shift_type='K' /*'接诊'*/
//           and s.old_data_code=(select g.pardep_code from com_deptstat g
//                                where g.stat_code='01'
//                                and (g.dept_code='{2}'  or '{2}'='ALL'))
//            and exists(select 1 from com_shiftdata a
//                              where a.clinic_no=s.clinic_no
//                              and a.shift_type in ('B'/*登记*/,'CD'/*修改科室*/)
//                              and (a.new_data_code='{2}' or '{2}'='ALL')))
//            or (s.shift_type='C' /*召回*/
//         and exists(select 1 from com_shiftdata a
//                              where a.clinic_no=s.clinic_no
//                              and a.shift_type in ('EO'/*出院登记取消*/)
//                              and (a.old_data_code='{2}' or '{2}'='ALL'))))
//           and i.patient_no like 'B%'
//           and not exists(select 1 from com_shiftdata a
//           where a.clinic_no=s.clinic_no
//           and a.shift_type='OF')
//           and s.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
//           and s.oper_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')
//         group by s.shift_type";

        private string sql_新生儿人数 = @"--入院人数
        select nvl(sum(case
                         when s.shift_type = 'K' then
                          count((s.clinic_no))
                         when s.shift_type = 'C' then
                          count((s.clinic_no))
                         else
                          0
                       end),
                   0) 入院人数
          from com_shiftdata s, fin_ipr_inmaininfo i
         where s.clinic_no = i.inpatient_no
           and (s.shift_type='K' /*'接诊'*/
            or s.shift_type='C' /*召回*/)
         and (s.oper_deptcode  in (select rr.dept_code
  from com_deptstat rr
 where (rr.pardep_code = '{2}' or rr.dept_code = '{2}')
   and rr.stat_code = '01'
   and rr.pardep_code <> 'AAAA') or '{2}'='ALL')
           and i.patient_no like 'B%'
           and not exists(select 1 from com_shiftdata a
           where a.clinic_no=s.clinic_no
           and a.shift_type IN ('OF')
           )
           and s.oper_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
           and s.oper_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')
         group by s.shift_type";

        public string Sql_新生儿人数
        {
            get { return sql_新生儿人数; }
            set { sql_新生儿人数 = value; }
        }
      

        #endregion

        private FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (isShowDateBetween)
            {
                lblBetween.Visible = true;
                dateTimePicker2.Visible = true;
            }
            else
            {
                lblBetween.Visible = false;
                dateTimePicker2.Visible = false;
            }

            //先把明细全部隐藏
            int row = GetDataRow("明细表");
            for (int index = 0; index < fpSpread1_Sheet1.RowCount; index++)
            {
                if (index >= row)
                {
                    fpSpread1_Sheet1.Rows[index].Visible = false;
                }
            }

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptMgr.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject allObj = new FS.FrameWork.Models.NeuObject();
            allObj.ID = "ALL";
            allObj.Name = "全部";
            allObj.Memo = "所有病区";
            al.Add(allObj);
            al.AddRange(alDept);
            cmbDept.AddItems(al);
            cmbDept.SelectedIndex = 0;

            //时间段
            DateTime dtNow = deptMgr.GetDateTimeFromSysDateTime();
            this.dateTimePicker1.Value = dtNow.Date.AddDays(-1);
            this.dateTimePicker2.Value = dtNow.Date.AddSeconds(-1);

            base.OnLoad(e);
        }

        private void Clear()
        {
            #region 初始化表格

            //this.fpSpread1_Sheet1.RowCount = 1;
            //this.fpSpread1_Sheet1.RowCount = 20;

            //fpSpread1_Sheet1.Rows[1].Tag = "汇总表";
            //fpSpread1_Sheet1.Cells[1, 1].Text = "原有人数";
            //fpSpread1_Sheet1.Cells[1, 1].ColumnSpan = 2;
            //fpSpread1_Sheet1.Cells[1, 2].Text = "入院人数";
            //fpSpread1_Sheet1.Cells[1, 2].ColumnSpan = 2;
            //fpSpread1_Sheet1.Cells[1, 3].Text = "他科转入人数";
            //fpSpread1_Sheet1.Cells[1, 3].ColumnSpan = 2;
            //fpSpread1_Sheet1.Cells[1, 4].Text = "出院人数";
            //fpSpread1_Sheet1.Cells[2, 4].Text = "出院";
            //fpSpread1_Sheet1.Cells[2, 5].Text = "死亡";
            //fpSpread1_Sheet1.Cells[2, 6].Text = "24H死亡";
            //fpSpread1_Sheet1.Cells[1, 7].Text = "转往他科人数";
            //fpSpread1_Sheet1.Cells[1, 7].ColumnSpan = 2;
            //fpSpread1_Sheet1.Cells[1, 8].Text = "现有人数";
            //fpSpread1_Sheet1.Cells[1, 8].ColumnSpan = 2;
            //fpSpread1_Sheet1.Cells[1, 9].Text = "陪人数";
            //fpSpread1_Sheet1.Cells[1, 9].ColumnSpan = 2;
            //fpSpread1_Sheet1.Cells[1, 10].Text = "一级护理";
            //fpSpread1_Sheet1.Cells[1, 10].ColumnSpan = 2;
            //fpSpread1_Sheet1.Cells[1, 11].Text = "危重病人数";
            //fpSpread1_Sheet1.Cells[1, 11].ColumnSpan = 2;
            //fpSpread1_Sheet1.Cells[1, 12].Text = "新生儿人数";
            //fpSpread1_Sheet1.Cells[1, 12].ColumnSpan = 2;


            //fpSpread1_Sheet1.Rows[5].Tag = "入院表";
            //fpSpread1_Sheet1.Cells[5, 1].Text = "入院表";
            //fpSpread1_Sheet1.Cells[6, 1].Text = "床号";

            #region 定义

            FarPoint.Win.ComplexBorder complexBorder146 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder147 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder148 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder149 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder150 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder151 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder152 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder153 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder154 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder155 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder156 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder157 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder158 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder159 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder160 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder161 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder162 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder163 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder164 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder165 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder166 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder167 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder168 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder169 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder170 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder171 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder172 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder173 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder174 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder175 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder176 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder177 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder178 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder179 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder180 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder181 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder182 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder183 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder184 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder185 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder186 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder187 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder188 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder189 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder190 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder191 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder192 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder193 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder194 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder195 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder196 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType22 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder197 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder198 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder199 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder200 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder201 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType23 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder202 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder203 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder204 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder205 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder206 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder207 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder208 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder209 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder210 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder211 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder212 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder213 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder214 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder215 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder216 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder217 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder218 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder219 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder220 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType24 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder221 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder222 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder223 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder224 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder225 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder226 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType25 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder227 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder228 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder229 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder230 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder231 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder232 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder233 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder234 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder235 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder236 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder237 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder238 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder239 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder240 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder241 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder242 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder243 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder244 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType26 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder245 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder246 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder247 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder248 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder249 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder250 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType27 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder251 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder252 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder253 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder254 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder255 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder256 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder257 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder258 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder259 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder260 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder261 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder262 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder263 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder264 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder265 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder266 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder267 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder268 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType28 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder269 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder270 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder271 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder272 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder273 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder274 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.Spread.CellType.TextCellType textCellType29 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder275 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder276 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder277 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.ThinLine));
            FarPoint.Win.ComplexBorder complexBorder278 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType30 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder279 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType31 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder280 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType32 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder281 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType33 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder282 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType34 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder283 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType35 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder284 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType36 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder285 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType37 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder286 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType38 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder287 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType39 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder288 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType40 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder289 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType41 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.ComplexBorder complexBorder290 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(System.Drawing.Color.White));
            FarPoint.Win.Spread.CellType.TextCellType textCellType42 = new FarPoint.Win.Spread.CellType.TextCellType();

            #endregion

            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            //this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 13;
            this.fpSpread1_Sheet1.ColumnHeader.RowCount = 0;
            this.fpSpread1_Sheet1.RowCount = 20;
            //this.fpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin2", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.White, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpSpread1_Sheet1.Cells.Get(1, 1).Border = complexBorder146;
            this.fpSpread1_Sheet1.Cells.Get(1, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 1).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 1).Value = "原有人数";
            this.fpSpread1_Sheet1.Cells.Get(1, 2).Border = complexBorder147;
            this.fpSpread1_Sheet1.Cells.Get(1, 2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 2).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 2).Value = "入院人数";
            this.fpSpread1_Sheet1.Cells.Get(1, 3).Border = complexBorder148;
            this.fpSpread1_Sheet1.Cells.Get(1, 3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 3).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 3).Value = "他科转入人数";
            this.fpSpread1_Sheet1.Cells.Get(1, 4).Border = complexBorder149;
            this.fpSpread1_Sheet1.Cells.Get(1, 4).ColumnSpan = 3;
            this.fpSpread1_Sheet1.Cells.Get(1, 4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 4).Value = "出院人数";
            this.fpSpread1_Sheet1.Cells.Get(1, 5).Border = complexBorder150;
            this.fpSpread1_Sheet1.Cells.Get(1, 5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 6).Border = complexBorder151;
            this.fpSpread1_Sheet1.Cells.Get(1, 6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 7).Border = complexBorder152;
            this.fpSpread1_Sheet1.Cells.Get(1, 7).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 7).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 7).Value = "转往他科人数";
            this.fpSpread1_Sheet1.Cells.Get(1, 8).Border = complexBorder153;
            this.fpSpread1_Sheet1.Cells.Get(1, 8).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 8).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 8).Value = "现有人数";
            this.fpSpread1_Sheet1.Cells.Get(1, 9).Border = complexBorder154;
            this.fpSpread1_Sheet1.Cells.Get(1, 9).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 9).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 9).Value = "陪人数";
            this.fpSpread1_Sheet1.Cells.Get(1, 10).Border = complexBorder155;
            this.fpSpread1_Sheet1.Cells.Get(1, 10).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 10).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 10).Value = "一级护理";
            this.fpSpread1_Sheet1.Cells.Get(1, 11).Border = complexBorder156;
            this.fpSpread1_Sheet1.Cells.Get(1, 11).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 11).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 11).Value = "危重病人数";
            this.fpSpread1_Sheet1.Cells.Get(1, 12).Border = complexBorder157;
            this.fpSpread1_Sheet1.Cells.Get(1, 12).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(1, 12).RowSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(1, 12).Value = "新生儿人数";
            this.fpSpread1_Sheet1.Cells.Get(2, 1).Border = complexBorder158;
            this.fpSpread1_Sheet1.Cells.Get(2, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 2).Border = complexBorder159;
            this.fpSpread1_Sheet1.Cells.Get(2, 2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 3).Border = complexBorder160;
            this.fpSpread1_Sheet1.Cells.Get(2, 3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 4).Border = complexBorder161;
            this.fpSpread1_Sheet1.Cells.Get(2, 4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 4).Value = "出院";
            this.fpSpread1_Sheet1.Cells.Get(2, 5).Border = complexBorder162;
            this.fpSpread1_Sheet1.Cells.Get(2, 5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 5).Value = "死亡";
            this.fpSpread1_Sheet1.Cells.Get(2, 6).Border = complexBorder163;
            this.fpSpread1_Sheet1.Cells.Get(2, 6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 6).Value = "24H死亡";
            this.fpSpread1_Sheet1.Cells.Get(2, 7).Border = complexBorder164;
            this.fpSpread1_Sheet1.Cells.Get(2, 7).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 8).Border = complexBorder165;
            this.fpSpread1_Sheet1.Cells.Get(2, 8).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 9).Border = complexBorder166;
            this.fpSpread1_Sheet1.Cells.Get(2, 9).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 10).Border = complexBorder167;
            this.fpSpread1_Sheet1.Cells.Get(2, 10).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 11).Border = complexBorder168;
            this.fpSpread1_Sheet1.Cells.Get(2, 11).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(2, 12).Border = complexBorder169;
            this.fpSpread1_Sheet1.Cells.Get(2, 12).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 1).Border = complexBorder170;
            this.fpSpread1_Sheet1.Cells.Get(3, 1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 2).Border = complexBorder171;
            this.fpSpread1_Sheet1.Cells.Get(3, 2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 3).Border = complexBorder172;
            this.fpSpread1_Sheet1.Cells.Get(3, 3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 4).Border = complexBorder173;
            this.fpSpread1_Sheet1.Cells.Get(3, 4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 5).Border = complexBorder174;
            this.fpSpread1_Sheet1.Cells.Get(3, 5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 6).Border = complexBorder175;
            this.fpSpread1_Sheet1.Cells.Get(3, 6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 7).Border = complexBorder176;
            this.fpSpread1_Sheet1.Cells.Get(3, 7).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 8).Border = complexBorder177;
            this.fpSpread1_Sheet1.Cells.Get(3, 8).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 9).Border = complexBorder178;
            this.fpSpread1_Sheet1.Cells.Get(3, 9).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 10).Border = complexBorder179;
            this.fpSpread1_Sheet1.Cells.Get(3, 10).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 11).Border = complexBorder180;
            this.fpSpread1_Sheet1.Cells.Get(3, 11).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(3, 12).Border = complexBorder181;
            this.fpSpread1_Sheet1.Cells.Get(3, 12).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Cells.Get(5, 1).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(5, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(5, 1).Value = "入院表";
            this.fpSpread1_Sheet1.Cells.Get(6, 1).Border = complexBorder182;
            this.fpSpread1_Sheet1.Cells.Get(6, 1).Value = "床号";
            this.fpSpread1_Sheet1.Cells.Get(6, 2).Border = complexBorder183;
            this.fpSpread1_Sheet1.Cells.Get(6, 2).Value = "住院号";
            this.fpSpread1_Sheet1.Cells.Get(6, 3).Border = complexBorder184;
            this.fpSpread1_Sheet1.Cells.Get(6, 3).Value = "姓名";
            this.fpSpread1_Sheet1.Cells.Get(6, 4).Border = complexBorder185;
            this.fpSpread1_Sheet1.Cells.Get(6, 4).Value = "性别";
            this.fpSpread1_Sheet1.Cells.Get(6, 5).Border = complexBorder186;
            this.fpSpread1_Sheet1.Cells.Get(6, 5).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(6, 5).Value = "入院诊断";
            this.fpSpread1_Sheet1.Cells.Get(6, 6).Border = complexBorder187;
            this.fpSpread1_Sheet1.Cells.Get(6, 7).Border = complexBorder188;
            this.fpSpread1_Sheet1.Cells.Get(6, 7).Value = "床号";
            this.fpSpread1_Sheet1.Cells.Get(6, 8).Border = complexBorder189;
            this.fpSpread1_Sheet1.Cells.Get(6, 8).Value = "住院号";
            this.fpSpread1_Sheet1.Cells.Get(6, 9).Border = complexBorder190;
            this.fpSpread1_Sheet1.Cells.Get(6, 9).Value = "姓名";
            this.fpSpread1_Sheet1.Cells.Get(6, 10).Border = complexBorder191;
            this.fpSpread1_Sheet1.Cells.Get(6, 10).Value = "性别";
            this.fpSpread1_Sheet1.Cells.Get(6, 11).Border = complexBorder192;
            this.fpSpread1_Sheet1.Cells.Get(6, 11).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(6, 11).Value = "入院诊断";
            this.fpSpread1_Sheet1.Cells.Get(6, 12).Border = complexBorder193;
            this.fpSpread1_Sheet1.Cells.Get(7, 1).Border = complexBorder194;
            this.fpSpread1_Sheet1.Cells.Get(7, 2).Border = complexBorder195;
            this.fpSpread1_Sheet1.Cells.Get(7, 2).Value = "01234567890";
            this.fpSpread1_Sheet1.Cells.Get(7, 3).Border = complexBorder196;
            this.fpSpread1_Sheet1.Cells.Get(7, 3).CellType = textCellType22;
            this.fpSpread1_Sheet1.Cells.Get(7, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(7, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(7, 4).Border = complexBorder197;
            this.fpSpread1_Sheet1.Cells.Get(7, 5).Border = complexBorder198;
            this.fpSpread1_Sheet1.Cells.Get(7, 5).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(7, 6).Border = complexBorder199;
            this.fpSpread1_Sheet1.Cells.Get(7, 7).Border = complexBorder200;
            this.fpSpread1_Sheet1.Cells.Get(7, 8).Border = complexBorder201;
            this.fpSpread1_Sheet1.Cells.Get(7, 8).CellType = textCellType23;
            this.fpSpread1_Sheet1.Cells.Get(7, 9).Border = complexBorder202;
            this.fpSpread1_Sheet1.Cells.Get(7, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(7, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(7, 10).Border = complexBorder203;
            this.fpSpread1_Sheet1.Cells.Get(7, 11).Border = complexBorder204;
            this.fpSpread1_Sheet1.Cells.Get(7, 11).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(7, 12).Border = complexBorder205;
            this.fpSpread1_Sheet1.Cells.Get(9, 1).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(9, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(9, 1).Value = "出院表";
            this.fpSpread1_Sheet1.Cells.Get(10, 1).Border = complexBorder206;
            this.fpSpread1_Sheet1.Cells.Get(10, 1).Value = "床号";
            this.fpSpread1_Sheet1.Cells.Get(10, 2).Border = complexBorder207;
            this.fpSpread1_Sheet1.Cells.Get(10, 2).Value = "住院号";
            this.fpSpread1_Sheet1.Cells.Get(10, 3).Border = complexBorder208;
            this.fpSpread1_Sheet1.Cells.Get(10, 3).Value = "姓名";
            this.fpSpread1_Sheet1.Cells.Get(10, 4).Border = complexBorder209;
            this.fpSpread1_Sheet1.Cells.Get(10, 4).Value = "性别";
            this.fpSpread1_Sheet1.Cells.Get(10, 5).Border = complexBorder210;
            this.fpSpread1_Sheet1.Cells.Get(10, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 5).Value = "出院诊断";
            this.fpSpread1_Sheet1.Cells.Get(10, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 6).Border = complexBorder211;
            this.fpSpread1_Sheet1.Cells.Get(10, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 6).Value = "情况";
            this.fpSpread1_Sheet1.Cells.Get(10, 6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 7).Border = complexBorder212;
            this.fpSpread1_Sheet1.Cells.Get(10, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 7).Value = "床号";
            this.fpSpread1_Sheet1.Cells.Get(10, 7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 8).Border = complexBorder213;
            this.fpSpread1_Sheet1.Cells.Get(10, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 8).Value = "住院号";
            this.fpSpread1_Sheet1.Cells.Get(10, 8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 9).Border = complexBorder214;
            this.fpSpread1_Sheet1.Cells.Get(10, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 9).Value = "姓名";
            this.fpSpread1_Sheet1.Cells.Get(10, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 10).Border = complexBorder215;
            this.fpSpread1_Sheet1.Cells.Get(10, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 10).Value = "性别";
            this.fpSpread1_Sheet1.Cells.Get(10, 10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 11).Border = complexBorder216;
            this.fpSpread1_Sheet1.Cells.Get(10, 11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 11).Value = "出院诊断";
            this.fpSpread1_Sheet1.Cells.Get(10, 11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 12).Border = complexBorder217;
            this.fpSpread1_Sheet1.Cells.Get(10, 12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(10, 12).Value = "情况";
            this.fpSpread1_Sheet1.Cells.Get(10, 12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(11, 1).Border = complexBorder218;
            this.fpSpread1_Sheet1.Cells.Get(11, 2).Border = complexBorder219;
            this.fpSpread1_Sheet1.Cells.Get(11, 3).Border = complexBorder220;
            this.fpSpread1_Sheet1.Cells.Get(11, 3).CellType = textCellType24;
            this.fpSpread1_Sheet1.Cells.Get(11, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(11, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(11, 4).Border = complexBorder221;
            this.fpSpread1_Sheet1.Cells.Get(11, 5).Border = complexBorder222;
            this.fpSpread1_Sheet1.Cells.Get(11, 6).Border = complexBorder223;
            this.fpSpread1_Sheet1.Cells.Get(11, 7).Border = complexBorder224;
            this.fpSpread1_Sheet1.Cells.Get(11, 8).Border = complexBorder225;
            this.fpSpread1_Sheet1.Cells.Get(11, 9).Border = complexBorder226;
            this.fpSpread1_Sheet1.Cells.Get(11, 9).CellType = textCellType25;
            this.fpSpread1_Sheet1.Cells.Get(11, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(11, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(11, 10).Border = complexBorder227;
            this.fpSpread1_Sheet1.Cells.Get(11, 11).Border = complexBorder228;
            this.fpSpread1_Sheet1.Cells.Get(11, 12).Border = complexBorder229;
            this.fpSpread1_Sheet1.Cells.Get(13, 1).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(13, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(13, 1).Value = "转入表";
            this.fpSpread1_Sheet1.Cells.Get(14, 1).Border = complexBorder230;
            this.fpSpread1_Sheet1.Cells.Get(14, 1).Value = "床号";
            this.fpSpread1_Sheet1.Cells.Get(14, 2).Border = complexBorder231;
            this.fpSpread1_Sheet1.Cells.Get(14, 2).Value = "住院号";
            this.fpSpread1_Sheet1.Cells.Get(14, 3).Border = complexBorder232;
            this.fpSpread1_Sheet1.Cells.Get(14, 3).Value = "姓名";
            this.fpSpread1_Sheet1.Cells.Get(14, 4).Border = complexBorder233;
            this.fpSpread1_Sheet1.Cells.Get(14, 4).Value = "性别";
            this.fpSpread1_Sheet1.Cells.Get(14, 5).Border = complexBorder234;
            this.fpSpread1_Sheet1.Cells.Get(14, 5).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(14, 5).Value = "转入前原病区";
            this.fpSpread1_Sheet1.Cells.Get(14, 6).Border = complexBorder235;
            this.fpSpread1_Sheet1.Cells.Get(14, 7).Border = complexBorder236;
            this.fpSpread1_Sheet1.Cells.Get(14, 7).Value = "床号";
            this.fpSpread1_Sheet1.Cells.Get(14, 8).Border = complexBorder237;
            this.fpSpread1_Sheet1.Cells.Get(14, 8).Value = "住院号";
            this.fpSpread1_Sheet1.Cells.Get(14, 9).Border = complexBorder238;
            this.fpSpread1_Sheet1.Cells.Get(14, 9).Value = "姓名";
            this.fpSpread1_Sheet1.Cells.Get(14, 10).Border = complexBorder239;
            this.fpSpread1_Sheet1.Cells.Get(14, 10).Value = "性别";
            this.fpSpread1_Sheet1.Cells.Get(14, 11).Border = complexBorder240;
            this.fpSpread1_Sheet1.Cells.Get(14, 11).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(14, 11).Value = "转入前原病区";
            this.fpSpread1_Sheet1.Cells.Get(14, 12).Border = complexBorder241;
            this.fpSpread1_Sheet1.Cells.Get(15, 1).Border = complexBorder242;
            this.fpSpread1_Sheet1.Cells.Get(15, 2).Border = complexBorder243;
            this.fpSpread1_Sheet1.Cells.Get(15, 3).Border = complexBorder244;
            this.fpSpread1_Sheet1.Cells.Get(15, 3).CellType = textCellType26;
            this.fpSpread1_Sheet1.Cells.Get(15, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(15, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(15, 4).Border = complexBorder245;
            this.fpSpread1_Sheet1.Cells.Get(15, 5).Border = complexBorder246;
            this.fpSpread1_Sheet1.Cells.Get(15, 5).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(15, 6).Border = complexBorder247;
            this.fpSpread1_Sheet1.Cells.Get(15, 7).Border = complexBorder248;
            this.fpSpread1_Sheet1.Cells.Get(15, 8).Border = complexBorder249;
            this.fpSpread1_Sheet1.Cells.Get(15, 9).Border = complexBorder250;
            this.fpSpread1_Sheet1.Cells.Get(15, 9).CellType = textCellType27;
            this.fpSpread1_Sheet1.Cells.Get(15, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(15, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(15, 10).Border = complexBorder251;
            this.fpSpread1_Sheet1.Cells.Get(15, 11).Border = complexBorder252;
            this.fpSpread1_Sheet1.Cells.Get(15, 11).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(15, 12).Border = complexBorder253;
            this.fpSpread1_Sheet1.Cells.Get(17, 1).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(17, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(17, 1).Value = "转出表";
            this.fpSpread1_Sheet1.Cells.Get(18, 1).Border = complexBorder254;
            this.fpSpread1_Sheet1.Cells.Get(18, 1).Value = "床号";
            this.fpSpread1_Sheet1.Cells.Get(18, 2).Border = complexBorder255;
            this.fpSpread1_Sheet1.Cells.Get(18, 2).Value = "住院号";
            this.fpSpread1_Sheet1.Cells.Get(18, 3).Border = complexBorder256;
            this.fpSpread1_Sheet1.Cells.Get(18, 3).Value = "姓名";
            this.fpSpread1_Sheet1.Cells.Get(18, 4).Border = complexBorder257;
            this.fpSpread1_Sheet1.Cells.Get(18, 4).Value = "性别";
            this.fpSpread1_Sheet1.Cells.Get(18, 5).Border = complexBorder258;
            this.fpSpread1_Sheet1.Cells.Get(18, 5).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(18, 5).Value = "转往病区";
            this.fpSpread1_Sheet1.Cells.Get(18, 6).Border = complexBorder259;
            this.fpSpread1_Sheet1.Cells.Get(18, 7).Border = complexBorder260;
            this.fpSpread1_Sheet1.Cells.Get(18, 7).Value = "床号";
            this.fpSpread1_Sheet1.Cells.Get(18, 8).Border = complexBorder261;
            this.fpSpread1_Sheet1.Cells.Get(18, 8).Value = "住院号";
            this.fpSpread1_Sheet1.Cells.Get(18, 9).Border = complexBorder262;
            this.fpSpread1_Sheet1.Cells.Get(18, 9).Value = "姓名";
            this.fpSpread1_Sheet1.Cells.Get(18, 10).Border = complexBorder263;
            this.fpSpread1_Sheet1.Cells.Get(18, 10).Value = "性别";
            this.fpSpread1_Sheet1.Cells.Get(18, 11).Border = complexBorder264;
            this.fpSpread1_Sheet1.Cells.Get(18, 11).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(18, 11).Value = "转往病区";
            this.fpSpread1_Sheet1.Cells.Get(18, 12).Border = complexBorder265;
            this.fpSpread1_Sheet1.Cells.Get(19, 1).Border = complexBorder266;
            this.fpSpread1_Sheet1.Cells.Get(19, 2).Border = complexBorder267;
            this.fpSpread1_Sheet1.Cells.Get(19, 3).Border = complexBorder268;
            this.fpSpread1_Sheet1.Cells.Get(19, 3).CellType = textCellType28;
            this.fpSpread1_Sheet1.Cells.Get(19, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(19, 3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(19, 4).Border = complexBorder269;
            this.fpSpread1_Sheet1.Cells.Get(19, 5).Border = complexBorder270;
            this.fpSpread1_Sheet1.Cells.Get(19, 5).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(19, 6).Border = complexBorder271;
            this.fpSpread1_Sheet1.Cells.Get(19, 7).Border = complexBorder272;
            this.fpSpread1_Sheet1.Cells.Get(19, 8).Border = complexBorder273;
            this.fpSpread1_Sheet1.Cells.Get(19, 9).Border = complexBorder274;
            this.fpSpread1_Sheet1.Cells.Get(19, 9).CellType = textCellType29;
            this.fpSpread1_Sheet1.Cells.Get(19, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.fpSpread1_Sheet1.Cells.Get(19, 9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Cells.Get(19, 10).Border = complexBorder275;
            this.fpSpread1_Sheet1.Cells.Get(19, 11).Border = complexBorder276;
            this.fpSpread1_Sheet1.Cells.Get(19, 11).ColumnSpan = 2;
            this.fpSpread1_Sheet1.Cells.Get(19, 12).Border = complexBorder277;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Columns.Get(0).Border = complexBorder278;
            textCellType30.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(0).CellType = textCellType30;
            this.fpSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 3F;
            this.fpSpread1_Sheet1.Columns.Get(1).Border = complexBorder279;
            textCellType31.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType31;
            this.fpSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 35F;
            this.fpSpread1_Sheet1.Columns.Get(2).Border = complexBorder280;
            textCellType32.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = textCellType32;
            this.fpSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 72F;
            this.fpSpread1_Sheet1.Columns.Get(3).Border = complexBorder281;
            textCellType33.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = textCellType33;
            this.fpSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 56F;
            this.fpSpread1_Sheet1.Columns.Get(4).Border = complexBorder282;
            textCellType34.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(4).CellType = textCellType34;
            this.fpSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(5).Border = complexBorder283;
            textCellType35.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(5).CellType = textCellType35;
            this.fpSpread1_Sheet1.Columns.Get(5).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 75F;
            this.fpSpread1_Sheet1.Columns.Get(6).Border = complexBorder284;
            textCellType36.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(6).CellType = textCellType36;
            this.fpSpread1_Sheet1.Columns.Get(6).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(7).Border = complexBorder285;
            textCellType37.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(7).CellType = textCellType37;
            this.fpSpread1_Sheet1.Columns.Get(7).Font = new System.Drawing.Font("宋体",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 54F;
            this.fpSpread1_Sheet1.Columns.Get(8).Border = complexBorder286;
            textCellType38.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(8).CellType = textCellType38;
            this.fpSpread1_Sheet1.Columns.Get(8).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 72F;
            this.fpSpread1_Sheet1.Columns.Get(9).Border = complexBorder287;
            textCellType39.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(9).CellType = textCellType39;
            this.fpSpread1_Sheet1.Columns.Get(9).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(9).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(9).Width = 56F;
            this.fpSpread1_Sheet1.Columns.Get(10).Border = complexBorder288;
            textCellType40.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(10).CellType = textCellType40;
            this.fpSpread1_Sheet1.Columns.Get(10).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(10).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(10).Width = 40F;
            this.fpSpread1_Sheet1.Columns.Get(11).Border = complexBorder289;
            textCellType41.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(11).CellType = textCellType41;
            this.fpSpread1_Sheet1.Columns.Get(11).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(11).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(11).Width = 75F;
            this.fpSpread1_Sheet1.Columns.Get(12).Border = complexBorder290;
            textCellType42.WordWrap = true;
            this.fpSpread1_Sheet1.Columns.Get(12).CellType = textCellType42;
            this.fpSpread1_Sheet1.Columns.Get(12).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(12).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Columns.Get(12).Width = 40F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpSpread1_Sheet1.Rows.Default.Height = 25F;
            this.fpSpread1_Sheet1.Rows.Get(0).Height = 14F;
            this.fpSpread1_Sheet1.Rows.Get(1).Tag = "汇总表";
            this.fpSpread1_Sheet1.Rows.Get(2).Tag = "";
            this.fpSpread1_Sheet1.Rows.Get(3).Tag = "";
            this.fpSpread1_Sheet1.Rows.Get(4).Tag = "明细表";
            this.fpSpread1_Sheet1.Rows.Get(5).Tag = "入院表";
            this.fpSpread1_Sheet1.Rows.Get(7).Tag = "";
            this.fpSpread1_Sheet1.Rows.Get(9).Tag = "出院表";
            this.fpSpread1_Sheet1.Rows.Get(13).Tag = "转入表";
            this.fpSpread1_Sheet1.Rows.Get(17).Tag = "转出表";
            this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "CornerDefault";
            this.fpSpread1_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;

            #endregion

            //先把明细全部隐藏
            int row = GetDataRow("明细表");
            for (int index = 0; index < fpSpread1_Sheet1.RowCount; index++)
            {
                if (index >= row)
                {
                    fpSpread1_Sheet1.Rows[index].Visible = false;
                }
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            FS.HISFC.Models.Base.Employee empl=(FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

            Clear();

            this.lblNurse.Text = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.Name;
            lblDate.Text = dateTimePicker1.Value.ToShortDateString();

            string exeSQL = string.Empty;
            string fromDate = dateTimePicker1.Value.Date.AddDays(0).AddSeconds(0).ToString();
            string endDate = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1).ToString();
            if (!isShowDateBetween)
            {
                endDate = dateTimePicker1.Value.Date.AddDays(1).AddSeconds(-1).ToString();
            }

            string dept = empl.Nurse.ID;
            if (cmbDept.Visible)
            {
                if (cmbDept.Tag != null)
                {
                    dept = cmbDept.Tag.ToString();
                }
                else
                {
                    dept = "ALL";
                }
            }

            if (FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                if (MessageBox.Show("你是管理员可以享受管理员特权哦！~要不要试一下？？", "no zuo no die", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //dept = "ALL";
                    FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(2, "why you try!", "你享受了管理员特权！\r\n\r\n目前查询是全院汇总的病房日志！", ToolTipIcon.Info);
                }
                else
                {
                    MessageBox.Show("自己放弃的就不要怪别人了~~\r\n\r\n过了这村就没这店了！！", "you no try", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (string.IsNullOrEmpty(dept))
            {
                dept = empl.Dept.ID;
            }

            FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
            
            //人数
            int qty = 0;
            //数据操作的行数：汇总数据、转入数据、转出数据等
            int row = 0;

            #region 汇总

            row = GetDataRow("汇总表");
            row = row + 2;

            //原有人数
            exeSQL = sql_原有人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            int qty_原有 = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 1].Text = qty_原有.ToString();

            //入院人数
            exeSQL = sql_入院人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            int qty_入院 = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 2].Text = qty_入院.ToString();

            //他科转入人数
            exeSQL = sql_转入人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            int qty_转入 = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 3].Text = qty_转入.ToString();

            //出院人数
            exeSQL = sql_出院人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            int qty_出院 = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 4].Text = qty_出院.ToString();

            //死亡人数
            exeSQL = sql_死亡人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            int qty_死亡 = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 5].Text = qty_死亡.ToString();

            //24小时死亡人数
            exeSQL = sql_24死亡人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            int qty_24死亡 = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 6].Text = qty_24死亡.ToString();

            //转出人数
            exeSQL = sql_转出人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            int qty_转出 = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 7].Text = qty_转出.ToString();


            int qty_temp = 0;
            //现有人数
            //qty = qty_原有 + qty_入院 + qty_转入 - qty_出院 - qty_转出;
            //fpSpread1_Sheet1.Cells[row, 8].Text = qty.ToString();

            exeSQL = sql_现有人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            qty_temp = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 8].Text = qty_temp.ToString();

            //陪人数
            exeSQL = sql_陪人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            qty_temp = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 9].Text = qty_temp.ToString();

            //一级护理
            exeSQL = sql_一级护理;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            qty_temp = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 10].Text = qty_temp.ToString();

            //危重病人数
            exeSQL = sql_危重病人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            qty_temp = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 11].Text = qty_temp.ToString();

            //新生人人数
            exeSQL = sql_新生儿人数;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            qty_temp = FS.FrameWork.Function.NConvert.ToInt32(dbMgr.ExecSqlReturnOne(exeSQL, "0"));
            fpSpread1_Sheet1.Cells[row, 12].Text = qty_temp.ToString();

            #endregion

            #region 入出转明细

            DataSet ds = null;
            exeSQL = sql_入出转;
            exeSQL = string.Format(exeSQL, fromDate, endDate, dept);
            if (dbMgr.ExecQuery(exeSQL, ref ds) == -1)
            {
                MessageBox.Show("查询病房日志出现错误！请联系管理员！\r\n" + dbMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            Dictionary<string, ArrayList> dicDetail = new Dictionary<string, ArrayList>();

            //数据中类型分类：出院病人、转出病区、转入病区、入院病人、出院召回
            foreach (DataRow dRow in ds.Tables[0].Rows)
            {
                string flag = "";
                switch (dRow["类型"].ToString())
                {
                    case "入院病人":
                        flag = "入院表";
                        break;
                    case "出院病人":
                        flag = "出院表";
                        break;
                    case "转入病区":
                        flag = "转入表";
                        break;
                    case "转出病区":
                        flag = "转出表";
                        break;
                    case "出院召回":
                        flag = "入院表";
                        break;
                    case "出院登记取消":
                        flag = "出院表";
                        break;
                    case "出院召回取消":
                        flag = "入院表";
                        break;
                    default:
                        //flag = "入院表";
                        break;
                }

                if (dicDetail.ContainsKey(flag))
                {
                    dicDetail[flag].Add(dRow);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(dRow);
                    dicDetail.Add(flag, al);
                }
            }

            int columnCount = fpSpread1_Sheet1.ColumnCount;

            //添加明细
            foreach (string key in dicDetail.Keys)
            {
                row = GetDataRow(key);
                fpSpread1_Sheet1.Rows[row].Visible = true;
                row = row + 1;
                fpSpread1_Sheet1.Rows[row].Visible = true;
                row = row + 1;
                fpSpread1_Sheet1.Rows[row].Visible = true;

                //每行显示的数据数
                int totdataCount = 2;
                //当前添加的数据数
                int currentdataCount = 1;

                int rowIndex = 1;
                foreach (DataRow dRow in dicDetail[key])
                {
                    if (dRow["类型"].ToString() != "")
                    {
                        int addCur = (currentdataCount - 1) * 6;
                        fpSpread1_Sheet1.Cells[row, addCur + 1].Text = dRow["床号"].ToString();
                        if (dRow["类型"].ToString() == "出院召回")
                        {
                            fpSpread1_Sheet1.Cells[row, addCur + 2].Text = "*" + dRow["住院号"].ToString();
                        }
                       
                        else if (dRow["类型"].ToString() == "出院登记取消")
                        {
                            fpSpread1_Sheet1.Cells[row, addCur + 2].Text = "*" + dRow["住院号"].ToString();
                        }
                       
                        else if (dRow["类型"].ToString() == "出院召回取消")
                        {
                            fpSpread1_Sheet1.Cells[row, addCur + 2].Text = "*" + dRow["住院号"].ToString();
                        }
                        else
                        {
                            fpSpread1_Sheet1.Cells[row, addCur + 2].Text = dRow["住院号"].ToString();
                        }
                        fpSpread1_Sheet1.Cells[row, addCur + 3].Text = dRow["姓名"].ToString();
                        fpSpread1_Sheet1.Cells[row, addCur + 4].Text = dRow["性别"].ToString();

                        if (key == "入院表")
                        {
                            fpSpread1_Sheet1.Cells[row, addCur + 5].Text = dRow["入院诊断"].ToString();
                            //fpSpread1_Sheet1.Cells[row, addCur + 6].Text = dRow["科室"].ToString();
                        }
                        else if (key == "出院表")
                        {
                            fpSpread1_Sheet1.Cells[row, addCur + 5].Text = dRow["出院诊断"].ToString();
                            fpSpread1_Sheet1.Cells[row, addCur + 6].Text = dRow["情况"].ToString();
                            //fpSpread1_Sheet1.Cells[row, addCur + 7].Text = dRow["科室"].ToString();
                        }
                        else if (key == "转入表")
                        {
                            fpSpread1_Sheet1.Cells[row, addCur + 5].Text = dRow["转入前原病区"].ToString();
                        }
                        else if (key == "转出表")
                        {
                            fpSpread1_Sheet1.Cells[row, addCur + 5].Text = dRow["转往病区"].ToString();
                        }


                        currentdataCount += 1;
                        if (currentdataCount > totdataCount

                            //最后一行的时候 就不添加空行了
                            && rowIndex != dicDetail[key].Count)
                        {
                            fpSpread1_Sheet1.AddRows(row + 1, 1);
                            fpSpread1_Sheet1.CopyRange(row, 0, row + 1, 0, 1, columnCount, false);
                            for (int col = 0; col < columnCount; col++)
                            {
                                fpSpread1_Sheet1.Cells[row + 1, col].Text = "";
                            }

                            row += 1;
                            fpSpread1_Sheet1.Rows[row].Visible = true;

                            currentdataCount = 1;
                        }

                        rowIndex += 1;
                    }
                }
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 获取数据所在行
        /// </summary>
        /// <param name="tagInfo"></param>
        /// <returns></returns>
        private int GetDataRow(string tagInfo)
        {
            for (int row = 0; row < fpSpread1_Sheet1.RowCount; row++)
            {
                if (fpSpread1_Sheet1.Rows[row].Tag != null
                    && fpSpread1_Sheet1.Rows[row].Tag.ToString() == tagInfo)
                {
                    return row;
                }
            }

            return 0;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsShowFarPointBorder = false;

            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            //FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("病房日志");
            //if (pSize != null)
            //{
            //    print.SetPageSize(pSize);
            //}
            //else
            //{
            //    print.SetPageSize(new FS.HISFC.Models.Base.PageSize("病房日志", 850, 550));
            //}
            print.PrintPage(0, 0, this);
            return 1;
        }

        protected override int OnPrintPreview(object sender, object neuObject)
        {
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsShowFarPointBorder = false;
            print.PrintPreview(0, 0, pnMain);
            return 1;
        }
    }
}
