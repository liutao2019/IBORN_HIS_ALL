using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    /// <summary>
    /// [功能描述: 盘点数据过滤设置的关键属性]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-08]<br></br>
    /// </summary>
    public class CheckFilterSetting
    {
        private ArrayList alDrugType = null;

        /// <summary>
        /// 药品类别
        /// </summary>
        public ArrayList AlDrugType
        {
            get { return alDrugType; }
            set { alDrugType = value; }
        }
        private ArrayList alDrugQuality = null;

        /// <summary>
        /// 药品性质
        /// </summary>
        public ArrayList AlDrugQuality
        {
            get { return alDrugQuality; }
            set { alDrugQuality = value; }
        }
        private ArrayList alDrugDosage = null;

        /// <summary>
        /// 药品剂型
        /// </summary>
        public ArrayList AlDrugDosage
        {
            get { return alDrugDosage; }
            set { alDrugDosage = value; }
        }

        private string startPlaceNO = "";

        /// <summary>
        /// 开始货位号
        /// </summary>
        public string StartPlaceNO
        {
            get { return startPlaceNO; }
            set { startPlaceNO = value; }
        }
        private string endPlaceNO = "";

        /// <summary>
        /// 结束货位号
        /// </summary>
        public string EndPlaceNO
        {
            get { return endPlaceNO; }
            set { endPlaceNO = value; }
        }
        private string startCustomNO = "";

        /// <summary>
        /// 开始自定义码
        /// </summary>
        public string StartCustomNO
        {
            get { return startCustomNO; }
            set { startCustomNO = value; }
        }
        private string endCustomNO = "";

        /// <summary>
        /// 结束自定义码
        /// </summary>
        public string EndCustomNO
        {
            get { return endCustomNO; }
            set { endCustomNO = value; }
        }

        private object param = null;

        /// <summary>
        /// 参数[备用]
        /// </summary>
        public object Param
        {
            get { return param; }
            set { param = value; }
        }
    }
}
