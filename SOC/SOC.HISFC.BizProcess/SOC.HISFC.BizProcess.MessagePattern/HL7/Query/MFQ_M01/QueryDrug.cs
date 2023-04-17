using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M01
{
     public  class QueryDrug
    {

        private int processMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 mfrM01, ref string errInfo)
        {

            FS.HISFC.Models.Pharmacy.Item drug = new FS.HISFC.Models.Pharmacy.Item();

            FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.DrugItem itemMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.DrugItem();

            drug = itemMgr.GetItemForRowa(o.QRD.GetWhatDataCodeValueQual(0).FirstDataCodeValue.Value);
           
            #region QRD

            mfrM01.QRD.QueryID.Value = o.QRD.QueryID.Value;

            #endregion

            #region MFI

            //MFI
            mfrM01.MFI.MasterFileIdentifier.Identifier.Value = "Drug";
            mfrM01.MFI.MasterFileIdentifier.Text.Value = "药品名称查询";
            mfrM01.MFI.MasterFileIdentifier.NameOfCodingSystem.Value = "HIS";
            //文件层事件代码
            mfrM01.MFI.FileLevelEventCode.Value = "UPD";
            mfrM01.MFI.EnteredDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            mfrM01.MFI.EffectiveDateTime.TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
            //应答层代码
            mfrM01.MFI.ResponseLevelCode.Value = "AL";

            #endregion


            #region MFE

            //MFE
            NHapi.Model.V24.Group.MFR_M01_MF_QUERY MF = mfrM01.GetMF_QUERY(mfrM01.MF_QUERYRepetitionsUsed);
            NHapi.Base.Model.Varies primaryKeyValue = MF.MFE.GetPrimaryKeyValueMFE(0);
            ((NHapi.Base.Model.GenericPrimitive)primaryKeyValue.Data).Value = drug.Product.BarCode;
            NHapi.Model.V24.Datatype.ID primaryKeyValueType = MF.MFE.GetPrimaryKeyValueType(0);
            primaryKeyValueType.Value = "CE";//编码元素



            #endregion

            #region Z15

                //Z段字段名称
            /*  Z段包含内容
              药品编码	ST	R	12
              商品名称	ST	O	50
              商品名自定义码	ST	O	16
              通用名	ST	O	50
              通用名自定义码	ST	O	50
              学名	ST	O	50
              学名自定义码	ST	O	16
              别名	ST	O	50
              别名自定义码	ST	O	16
                
              英文通用名	ST	O	32
              英文别名	ST	O	32
              英文名	ST	O	32
             
              国际编码	ST	O	20
              国家编码	ST	O	20
              
           */
            //Z段字段名称
            MF.Zxx.Name = "Z06";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(12, "").Value = drug.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.Name;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.UserCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.NameCollection.RegularName;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.RegularSpell.UserCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.NameCollection.FormalName;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.FormalSpell.UserCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.NameCollection.OtherName;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.OtherSpell.UserCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.NameCollection.EnglishRegularName;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.NameCollection.EnglishName;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.EnglishOtherName;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = drug.NationCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = drug.GBCode;

            /*
             *  系统类别        ST	O	3
             *  最小费用代码	ST	O	3
                药品类别	ST	O	1
                药品性质	ST	O	2
                规格	ST	O	32
                零售价	NM	O	12
                最新购入价	NM	O	12
                包装单位	ST	O	16
                包装数	NM	O	10
                最小单位	ST	O	16
                剂型编码	ST	O	10
                基本剂量	NM	O	12
                剂量单位	ST	O	16
                用法编码	ST	O	10
                频次编码	ST	O	6
                一次用量	NM	O	12
             */
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(3, "").Value = drug.SysClass.ID.ToString();

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(3, "").Value = drug.MinFee.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drug.Type.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drug.Quality.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(32, "").Value = drug.Specs;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(12, "").Value = drug.PriceCollection.RetailPrice.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(12, "").Value = drug.PriceCollection.PurchasePrice.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.PackUnit;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(10, "").Value = drug.PackQty.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.MinUnit;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = drug.DosageForm.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(12, "").Value = drug.BaseDose.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.DoseUnit;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = drug.Usage.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = drug.Frequency.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(12, "").Value = drug.OnceDose.ToString();

            /*              
                注意事项	ST	O	50
                一级药理作用	ST	O	20
                二级药理作用	ST	O	20
                三级药理作用	ST	O	20
                有效性标志 	ST	O	1
                自制标志 	ST	O	1
                OCT标志	ST	O	2
                GMP标志 	ST	O	1
                是否需要试敏	ST	O	1
                最新供药公司(在入库时更新，用于生成药品采购单)	ST	O	10
                产地	ST	O	50
                生产厂家	ST	O	10
                批文信息	ST	O	32
                有效成分	ST	O	50
                临嘱拆分类型	ST	O	1
                参考零售价2	NM	O	12
                扩展数据3长嘱拆分	ST	O	1
            */
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.Product.Caution;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = drug.PhyFunction1.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = drug.PhyFunction2.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(20, "").Value = drug.PhyFunction3.ID;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = (drug.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid) ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drug.Product.IsSelfMade ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drug.IsOTC ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drug.IsGMP ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drug.IsAllergy ? "1" : "0";
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = drug.Product.Company.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.Product.ProducingArea;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(10, "").Value = drug.Product.Producer.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(32, "").Value = drug.Product.ApprovalInfo;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(50, "").Value = drug.Ingredient;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drug.SplitType;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.NM>(12, "").Value = drug.RetailPrice2.ToString();
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(1, "").Value = drug.CDSplitType;

            /*
                 备注	ST	O	200
                 操作员	ST	O	6
                 操作时间	TS	O	26
             */

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(200, "").Value = drug.Memo;


            /*
                商品名拼音码	ST	O	18
                商品名五笔码	ST	O	16
                通用名拼音码	ST	O	16
                通用名五笔码	ST	O	50
                学名拼音码	    ST	O	16
                学名五笔码	    ST	O	16
                别名拼音码	    ST	O	16
                别名五笔码	    ST	O	16
            */
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(18, "").Value = drug.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.WBCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.RegularSpell.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.RegularSpell.WBCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.FormalSpell.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.FormalSpell.WBCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.OtherSpell.SpellCode;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(16, "").Value = drug.NameCollection.OtherSpell.WBCode;

            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(6, "").Value = FS.FrameWork.Management.Connection.Operator.ID;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.TS>(26, "").TimeOfAnEvent.Value = DateTime.Now.ToString("yyyyMMddHHmmss");

            //打包收费标记
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(5, "").Value = drug.SpecialFlag4;
            MF.Zxx.Add<NHapi.Model.V24.Datatype.ST>(5, "").Value = drug.SpecialFlag2; //限制用药标志

          #endregion


            return 1;
        }


        public int ProcessMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 ackMessage, ref string errInfo)
        {
            return this.processMessage(o, ref ackMessage, ref errInfo);
        }

    }
}
