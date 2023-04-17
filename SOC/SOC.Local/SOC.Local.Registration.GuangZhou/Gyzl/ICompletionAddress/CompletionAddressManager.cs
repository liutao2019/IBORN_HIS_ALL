﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Registration.GuangZhou.Gyzl.ICompletionAddress
{
    public class CompletionAddressManager : FS.FrameWork.Management.Database
    {
        public CompletionAddressManager()
        {
        }

        public ArrayList QueryAddressList(string curAddress)
        {
            string sql = string.Empty;
            int result = this.Sql.GetCommonSql("", ref sql);
            //暂时只处理curAddress为单一地址信息：
            //如：广州或广州市天河区，而不能处理广州天河或广州 天河
            if (result == -1)
            {
                sql = @"select name
                          from
                        (
                        select m.name||decode(n.name,'市辖区','','县','',n.name)||o.name name
                          from
                        (
                        select d.code,
                               d.name
                          from com_address d
                         where d.lever = '1'
                           and d.name not like '%不详%'
                           and d.name not like '%建设兵团%'
                         order by d.code
                        ) m,
                        (
                        select d.code,
                               d.name,
                               d.senior_address
                          from com_address d
                         where d.lever = '2'
                           and d.name not like '%不详%'
                          order by d.code
                        ) n,
                        (
                        select d.code,
                               d.name,
                               d.senior_address
                          from com_address d
                         where d.lever = '3'
                           and d.name not like '%不详%'
                          order by d.code
                        ) o
                         where n.senior_address = m.code
                           and o.senior_address = n.code
                        )
                         where name like '%{0}%'";
            }

            sql = string.Format(sql, curAddress);

            ArrayList addressList = new ArrayList();
            if (this.ExecQuery(sql) == -1)
            {
                return addressList;
            }
            else
            {
                try
                {
                    while (this.Reader.Read())
                    {
                        addressList.Add(this.Reader[0].ToString());
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
                return addressList;
            }
        }
    }
}
