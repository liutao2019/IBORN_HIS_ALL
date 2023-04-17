using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Ticker;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// {05394A98-2BC1-4EC2-B07C-C9A01C0807D3}卡卷核销
    /// </summary>
    public class CardTickerBLL : FS.FrameWork.Management.Database
    {
       
    //{E40946A4-FEB0-4842-BEAB-472BD85F1829}
        //{F55EE363-24DB-4a01-B540-49761A5ADBC6}
        public int Insert(TickerWriteOffRecord model)
        {
            string sqlNO = "select TICK_SEQ.NEXTVAL from dual";
            string seq = this.ExecSqlReturnOne(sqlNO);
            if (!string.IsNullOrEmpty(seq))
            {
                model.ID = Convert.ToInt32(seq);
            }
            string sql = @"insert into TICKER_WRITEOFF_RECORD
  (id, tickno, card_no, writeofffee, consutype, oper_id, oper_name, createdate,title,tcontent,RECEIVEPERSONNAME)
values
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', to_date('{7}','yyyy-MM-dd hh24:mi:ss'),'{8}','{9}','{10}')";//{E40946A4-FEB0-4842-BEAB-472BD85F1829}
            sql = string.Format(sql, model.ID, model.TickerNO, model.Card_NO, model.WriteOffFee, model.ConSutype, model.Oper_ID, model.Oper_Name, this.GetDateTimeFromSysDateTime().ToString(),model.Title,model.Tcontent,model.ReceivePersonName);
            int i = this.ExecNoQuery(sql);
            return i;
        }
    }
}
