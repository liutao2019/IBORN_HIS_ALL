<ReportQueryInfo Type="FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.ReportQueryInfo,SOC.HISFC.OutpatientFee.Components"><List><List Type="FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.QueryControl,SOC.HISFC.OutpatientFee.Components"><Index>0</Index><Name>dtBeginTime</Name><Text>开始时间：</Text><IsAddText>true</IsAddText><ControlType Type="FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType.DateTimeType,SOC.HISFC.OutpatientFee.Components"><AddDays>-1</AddDays><AddMonths>0</AddMonths><CustomFormat>yyyy-MM-dd</CustomFormat><DefaultDataSource></DefaultDataSource><Format>Custom</Format><IsLike>false</IsLike><LikeStr>%{0}%</LikeStr><Size Type="System.Drawing.Size,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"><Width>120</Width><Height>20</Height></Size><Location Type="System.Drawing.Point,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"><X>10</X><Y>2</Y></Location><QueryDataSource>Dictionary</QueryDataSource><DataSourceTypeName></DataSourceTypeName><Enabled>true</Enabled></ControlType></List><List Type="FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.QueryControl,SOC.HISFC.OutpatientFee.Components"><Index>1</Index><Name>dtEndTime</Name><Text>结束时间：</Text><IsAddText>true</IsAddText><ControlType Type="FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType.DateTimeType,SOC.HISFC.OutpatientFee.Components"><AddDays>0</AddDays><AddMonths>0</AddMonths><CustomFormat>yyyy-MM-dd</CustomFormat><DefaultDataSource></DefaultDataSource><Format>Custom</Format><IsLike>false</IsLike><LikeStr>%{0}%</LikeStr><Size Type="System.Drawing.Size,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"><Width>120</Width><Height>20</Height></Size><Location Type="System.Drawing.Point,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"><X>220</X><Y>2</Y></Location><QueryDataSource>Dictionary</QueryDataSource><DataSourceTypeName></DataSourceTypeName><Enabled>true</Enabled></ControlType></List></List><QueryDataSource><QueryDataSource Type="FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.QueryDataSource,SOC.HISFC.OutpatientFee.Components"><Name>dtOpsIncom</Name><Sql> select 科室,
    sum(decode(月份,substr(月份,1,5)||'01',手术例数,0)) 一月,
    sum(decode(月份,substr(月份,1,5)||'02',手术例数,0)) 二月,
    sum(decode(月份,substr(月份,1,5)||'03',手术例数,0)) 三月,
    sum(decode(月份,substr(月份,1,5)||'04',手术例数,0)) 四月,
    sum(decode(月份,substr(月份,1,5)||'05',手术例数,0)) 五月,
    sum(decode(月份,substr(月份,1,5)||'06',手术例数,0)) 六月,
    sum(decode(月份,substr(月份,1,5)||'07',手术例数,0)) 七月,
    sum(decode(月份,substr(月份,1,5)||'08',手术例数,0)) 八月,
    sum(decode(月份,substr(月份,1,5)||'09',手术例数,0)) 九月,
    sum(decode(月份,substr(月份,1,5)||'10',手术例数,0)) 十月,
    sum(decode(月份,substr(月份,1,5)||'11',手术例数,0)) 十一月,
    sum(decode(月份,substr(月份,1,5)||'12',手术例数,0)) 十二月,
    (select count(distinct t.operationno) from fin_ipb_itemlist c, met_ops_apply t
         where c.fee_date &gt; to_date('&amp;dtBeginTime', 'yyyy-mm-dd hh24:mi:ss')
           and c.fee_date &lt; to_date('&amp;dtEndTime', 'yyyy-mm-dd hh24:mi:ss')
           and c.recipe_deptcode = '&amp;CurrentDeptID'
           and t.clinic_code = c.inpatient_no
           and t.execstatus = '4'
           and t.ynvalid = '1' and fun_get_dept_name(c.inhos_deptcode) = 科室) 合计
     from
    (
       select fun_get_dept_name(c.inhos_deptcode) 科室,to_char(add_monthS(c.fee_date, 0), 'YYYY-MM') 月份,
               count(distinct t.operationno) 手术例数
          from fin_ipb_itemlist c, met_ops_apply t
         where c.fee_date &gt; to_date('&amp;dtBeginTime', 'yyyy-mm-dd hh24:mi:ss')
           and c.fee_date &lt; to_date('&amp;dtEndTime', 'yyyy-mm-dd hh24:mi:ss')
           and c.recipe_deptcode = '&amp;CurrentDeptID'
           and t.clinic_code = c.inpatient_no
           and t.execstatus = '4'
           and t.ynvalid = '1'
         group by c.inhos_deptcode,to_char(add_monthS(c.fee_date, 0), 'YYYY-MM')
         ) group by 科室</Sql><AddMapRow>false</AddMapRow><AddMapColumn>false</AddMapColumn><AddMapData>false</AddMapData><AddMapSourceData>false</AddMapSourceData><IsCross>false</IsCross><SqlType>MainReportUsing</SqlType><CrossRows></CrossRows><CrossColumns></CrossColumns><CrossValues></CrossValues><CrossCombinColumns></CrossCombinColumns><SumColumns>科室</SumColumns><CrossGroupColumns></CrossGroupColumns><IsSumRow>true</IsSumRow><SumRows>科室</SumRows><RowGroup /></QueryDataSource><QueryDataSource Type="FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.QueryDataSource,SOC.HISFC.OutpatientFee.Components"><Name>dtTimeCondition</Name><Sql>select '开始时间：'||'&amp;dtBeginTime'||'   结束时间：'||'&amp;dtEndTime' from dual</Sql><AddMapRow>false</AddMapRow><AddMapColumn>false</AddMapColumn><AddMapData>false</AddMapData><AddMapSourceData>false</AddMapSourceData><IsCross>false</IsCross><SqlType>MainReportUsing</SqlType><CrossRows></CrossRows><CrossColumns></CrossColumns><CrossValues></CrossValues><CrossCombinColumns></CrossCombinColumns><SumColumns></SumColumns><CrossGroupColumns></CrossGroupColumns><IsSumRow>true</IsSumRow><SumRows></SumRows><RowGroup /></QueryDataSource></QueryDataSource><QueryFilePath>Report\手术室\科室手术例数查询设置.xm</QueryFilePath><PrintInfo Type="FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.PrintInfo,SOC.HISFC.OutpatientFee.Components"><PaperSize Type="System.Drawing.Printing.PaperSize,System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"><Height>1169</Height><PaperName>A4</PaperName><RawKind>0</RawKind><Width>850</Width></PaperSize><SelectPage>false</SelectPage></PrintInfo></ReportQueryInfo>