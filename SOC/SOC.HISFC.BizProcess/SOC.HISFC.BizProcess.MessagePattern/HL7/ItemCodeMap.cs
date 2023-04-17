using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7
{
    /// <summary>
    /// [功能描述: 主键对照]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public  class ItemCodeMapManager:FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 插入对照信息
        /// </summary>
        /// <param name="itemType">项目类别</param>
        /// <param name="hisCode"></param>
        /// <param name="hl7Code"></param>
        /// <returns></returns>
        public int Insert(EnumItemCodeMap itemType, FS.FrameWork.Models.NeuObject hisCode, FS.FrameWork.Models.NeuObject hl7Code, string systemCode)
        {
            string sql = string.Format("insert into HL7_ITEM_MAP(RUID,HIS_ITEM_CODE,HIS_ITEM_NAME,HL7_ITEM_CODE,HL7_ITEM_NAME,HL7_CODE_SYSTEM,HL7_CODE_SYSTEM2) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", Guid.NewGuid().ToString(), hisCode.ID, hisCode.Name, hl7Code.ID, hl7Code.Name, itemType, systemCode);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 根据HISCode查询HL7Code
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetHL7Code(EnumItemCodeMap itemType, FS.FrameWork.Models.NeuObject hisCode)
        {
            string sql = string.Format("select HL7_ITEM_CODE,HL7_ITEM_NAME from HL7_ITEM_MAP where HL7_CODE_SYSTEM='{0}' and HIS_ITEM_CODE='{1}' and HIS_ITEM_NAME='{2}' order by HL7_ITEM_CODE desc ", itemType, hisCode.ID, hisCode.Name);

            if (this.ExecQuery(sql) < 0)
            {
                return null;
            }

            if (this.Reader != null)
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                if (this.Reader.Read())
                {
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                }

                if(this.Reader.IsClosed==false)
                {
                    this.Reader.Close();
                }

                return obj;
            }

            return null;

        }

        /// <summary>
        /// 根据HL7Code查询HISCode
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetHISCode(EnumItemCodeMap itemType, FS.FrameWork.Models.NeuObject hl7Code,string systemCode)
        {
            string sql = string.Format("select HIS_ITEM_CODE,HIS_ITEM_NAME from HL7_ITEM_MAP where HL7_CODE_SYSTEM='{0}' and HL7_ITEM_CODE='{1}' and HL7_ITEM_NAME='{2}' and (HL7_CODE_SYSTEM2='{3}' or nvl(HL7_CODE_SYSTEM2,' ')=' ') order by HIS_ITEM_CODE desc", itemType, hl7Code.ID, hl7Code.Name,systemCode);

            if (this.ExecQuery(sql) < 0)
            {
                return null;
            }

            if (this.Reader != null)
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                if (this.Reader.Read())
                {
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                }

                if (this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }

                return obj;
            }

            return null;
        }

        /// <summary>
        /// 根据HL7传过来的数据删除
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="hl7Code"></param>
        /// <returns></returns>
        public int Delete(EnumItemCodeMap itemType, FS.FrameWork.Models.NeuObject hl7Code,string systemCode)
        {

            string sql = string.Format("delete from HL7_ITEM_MAP where HL7_ITEM_CODE='{0}' and HL7_ITEM_NAME='{1}'  and HL7_CODE_SYSTEM='{2}' and (HL7_CODE_SYSTEM2='{3}' or nvl(HL7_CODE_SYSTEM2,' ')=' ')", hl7Code.ID, hl7Code.Name, itemType,systemCode);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// cis处方号分---开插入对照信息
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="hisCode"></param>
        /// <param name="hl7Code"></param>
        /// <returns></returns>
        public int InsertSeparate(EnumItemCodeMap itemType, FS.FrameWork.Models.NeuObject hisCode, FS.FrameWork.Models.NeuObject hl7Code, FS.FrameWork.Models.NeuObject hl7Cis)
        {
            string sql = string.Format("insert into HL7_ITEM_MAP(RUID,HIS_ITEM_CODE,HIS_ITEM_NAME,HL7_ITEM_CODE,HL7_ITEM_NAME,HL7_CODE_SYSTEM,hl7_item_code2,hl7_item_name2) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", Guid.NewGuid().ToString(), hisCode.ID, hisCode.Name, hl7Code.ID, hl7Code.Name, itemType, hl7Cis.ID, hl7Cis.Name);

            return this.ExecNoQuery(sql);
        }
    }

    public enum EnumItemCodeMap
    {
        /// <summary>
        /// 预约网站流水号ID
        /// </summary>
        RegisterBooking,
        /// <summary>
        /// 预约网站患者ID
        /// </summary>
        RegisterBookingCardNO,
        /// <summary>
        /// 门诊医嘱流水号
        /// </summary>
        OutpatientOrder,
        /// <summary>
        /// 住院医嘱流水号
        /// </summary>
        InpatientOrder,
        /// <summary>
        /// 住院医嘱退药流水号
        /// </summary>
        InpatientOrderQuitFee

    }
}
