using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Frequency
    {
        public int Receive(NHapi.Model.V24.Message.MFN_M01 MFN, ref string errInfo)
        {
            FS.HISFC.Models.Order.Frequency frequency = new FS.HISFC.Models.Order.Frequency();
            FS.HISFC.BizLogic.Manager.Frequency frequencyMgr = new FS.HISFC.BizLogic.Manager.Frequency();
            /*  1	6	ST	R	code	编码	
                2	60	ST	R	name	名称	
                3	8	ST	O	py_code	拼音码	
                4	8	ST	O	d_code	自定义码	
                5	1	ST	O	print_name	打印名称	
                6	1	ST	O	week_day	周标志	
                7	1	ST	O	deleted_flag	删除标记	1 删除 0正常
                8	5	ST	O	sort_code	排序码	m
                9	7	ST	O	doc_used	医生使用标志	
            */
            NHapi.Model.V24.Segment.ZA3 ZA3 = MFN.GetMF().GetStructure<NHapi.Model.V24.Segment.ZA3>();
            frequency.ID = ZA3.Get<NHapi.Model.V24.Datatype.ST>(1).Value;
            frequency.Name = ZA3.Get<NHapi.Model.V24.Datatype.ST>(2).Value;
            if (frequency.Name.Length > 30)
            {
                frequency.Name = frequency.Name.Substring(0, 30);
            }
            frequency.SpellCode = ZA3.Get<NHapi.Model.V24.Datatype.ST>(3).Value;
            frequency.Usage.ID = "All";
            frequency.Usage.Name = "全部";
            frequency.Dept.ID = "ROOT";
           
            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = MFN.GetMF(0);
            /*
            string sql = @"update met_com_deptfrequency 
                                        set   CIS_FREQUENCY_CODE='{1}',
                                              CIS_FREQUENCY_NAME='{2}',
                                              CIS_PRINT_NAME = '{3}',
                                              FREQUENCY_TIME = decode(FREQUENCY_TIME,'FREQUENCY_TIME','{4}',FREQUENCY_TIME) 
                                        where DEPT_CODE='ROOT' and 
                                              FREQUENCY_CODE='{0}' and
                                              USAGE_CODE = 'All'";
              */
            string frequenceTime = "'FREQUENCY_TIME'";
            
            if (ZA3.NumFields() > 9)
            {
                if (!string.IsNullOrEmpty(ZA3.Get<NHapi.Model.V24.Datatype.ST>(10).Value))
                {
                    frequenceTime = "'" + ZA3.Get<NHapi.Model.V24.Datatype.ST>(10).Value + "'";
                }
            }
           // sql = string.Format(sql, frequency.ID, frequency.ID, frequency.Name, ZA3.Get<NHapi.Model.V24.Datatype.ST>(5).Value, frequenceTime);
            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int param = 1;
            if (ZA3.Get<NHapi.Model.V24.Datatype.ST>(7).Value == "0") //正常
            {
                param = frequencyMgr.Set(frequency);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = frequencyMgr.Err;
                    return -1;
                }
                else
                {
                    param = 1;
                }
            }
            else  //删除
            {   param = frequencyMgr.Del(frequency);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = frequencyMgr.Err;
                    return -1;
                }
                else
                {
                    param = 1;
                }
            
            }
            //param = frequencyMgr.ExecNoQuery(sql);
            //if (param != 1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    errInfo = frequencyMgr.Err;
            //    return -1;
            //}
            FS.FrameWork.Management.PublicTrans.Commit();
            return param;
        }
    }
}
