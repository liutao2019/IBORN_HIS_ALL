using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou.OutPatient
{
    public class GetBalanceResult : IBorn.SI.GuangZhou.AbstractService<FS.HISFC.Models.Registration.Register, FS.HISFC.Models.Registration.Register>
    {

        public override bool Transcation
        {
            get { return false; }
        }

        protected override int Excute(FS.HISFC.Models.Registration.Register r, ref System.Data.DataTable dt, params object[] appendParams)
        {
            string strSQL = "SELECT JYDJH, FYPC, YYBH, GMSFHM, ZYH, RYRQ, JSRQ, ZYZJE, SBZFJE, " +
                "ZHZFJE, BFXMZFJE, QFJE, GRZFJE1, GRZFJE2, GRZFJE3, CXZFJE, YYFDJE, ZFYY, BZ1, BZ2, BZ3, DRBZ FROM HIS_MZJS WHERE JYDJH = '" + r.SIMainInfo.RegNo + "'" +
                " and fypc = (select max(fypc) from his_mzjs where jydjh = '" + r.SIMainInfo.RegNo + "' )";
            dt = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteDataTable(strSQL);
            if (dt == null)
            {
                this.ErrorMsg = "查询医保患者结算信息失败!";
                return -1;
            }
            return 1;
        }

        protected override int GetResult(System.Data.DataTable dt, FS.HISFC.Models.Registration.Register r, ref FS.HISFC.Models.Registration.Register reg, params object[] appendParams)
        {
            if (dt == null)
            {
                return base.GetResult(dt, r, ref reg, appendParams);
            }
            if (dt.Rows.Count == 0)
            {
                this.ErrorMsg = "没有结算信息，请先在医保端进行结算";
                return -1;
            }
            reg = r.Clone();
            System.Data.DataRow dr = dt.Rows[0];
            reg.SIMainInfo.RegNo = dr[0].ToString();
            reg.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(dr[1].ToString());
            reg.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(dr[6].ToString());
            reg.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[7].ToString());
            reg.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[8].ToString());
            reg.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[9].ToString());
            reg.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[10].ToString());
            reg.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[11].ToString());
            reg.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[12].ToString());
            reg.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[13].ToString());
            reg.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[14].ToString());
            reg.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[15].ToString());
            reg.SIMainInfo.Memo = dr[16].ToString();
            reg.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[17].ToString());
            reg.SIMainInfo.User01 = dr[18].ToString();
            reg.SIMainInfo.User02 = dr[19].ToString();
            reg.SIMainInfo.User03 = dr[20].ToString();
            reg.SIMainInfo.ReadFlag = FS.FrameWork.Function.NConvert.ToInt32(dr[21].ToString());
            return 1;
        }
    }
}
