using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Fee;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Fee
{
    public class PatientDiscountCardLogic : FS.FrameWork.Management.Database
    {

        #region 私有方法
        /// <summary>
        /// 检索卡卷信息
        /// </summary>
        /// <param name="sql">执行的查询SQL语句</param>
        /// <param name="args">SQL语句的参数</param>
        /// <returns>成功:卡卷信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        private ArrayList QueryPatientDiscountCardBySql(string sql, params string[] args)
        {
            ArrayList PatientCards = new ArrayList();

            //执行SQL语句
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    PatientDiscountCard patientCard = new PatientDiscountCard();
                    patientCard.CardNo = this.Reader[0].ToString();
                    patientCard.CardKind = this.Reader[1].ToString();
                    patientCard.CardName = this.Reader[2].ToString();
                    patientCard.GetName = this.Reader[3].ToString();
                    patientCard.GetCardNo = this.Reader[4].ToString();
                    patientCard.GetTime = NConvert.ToDateTime(this.Reader[5].ToString());
                    patientCard.GetPhone = this.Reader[6].ToString();
                    patientCard.GetOper = this.Reader[7].ToString();
                    patientCard.UsedName = this.Reader[8].ToString();
                    patientCard.UsedCardNo = this.Reader[9].ToString();
                    patientCard.UsedPhone = this.Reader[10].ToString();
                    patientCard.UsedTime = NConvert.ToDateTime(this.Reader[11].ToString());
                    patientCard.UsedState = this.Reader[12].ToString();
                    patientCard.UsedOper = this.Reader[13].ToString();

                    PatientCards.Add(patientCard);
                }//循环结束

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return PatientCards;
        }

        /// <summary>
        /// 获得update或者insert退的传入参数数组
        /// </summary>
        /// <param name="invoice">卡卷实体类</param>
        /// <returns>参数数组</returns>
        private string[] GetPatientCardParams(PatientDiscountCard patientCard)
        {
            string[] args =
				{
                    patientCard.CardNo,
                    patientCard.CardKind,
                    patientCard.CardName,
                    patientCard.GetName,
                    patientCard.GetCardNo,
                    patientCard.GetTime.ToString(),
                    patientCard.GetPhone,
                    patientCard.GetOper,
                    patientCard.UsedName,
                    patientCard.UsedCardNo,
                    patientCard.UsedPhone,
                    patientCard.UsedTime.ToString(),
                    patientCard.UsedState,
                    patientCard.UsedOper
				};

            return args;
        }
        #endregion 

        #region 公有方法

        #region 插入
        /// <summary>
        /// 插入一条取卡信息.
        /// </summary>
        /// <param name="invoice">卡卷信息类</param>
        /// <returns> 成功: 1 失败: -1 没有插入记录: 0</returns>
        public int InsertPatientCard(PatientDiscountCard patientCard)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.PatientDiscountCardLogic.InsertPatientDiscountCard", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.PatientDiscountCardLogic.InsertPatientDiscountCard的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetPatientCardParams(patientCard));
        }

        #endregion

        #region 更新

        public int UpdatePatientCardByCardKindAndNo(PatientDiscountCard patientCard)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.PatientDiscountCardLogic.UpdatePatientCardByCardKindAndNo", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.PatientDiscountCardLogic.UpdatePatientCardByCardKindAndNo的SQL语句";

                return -1;
            }
            sql = string.Format(sql, patientCard.CardNo, patientCard.CardKind, patientCard.UsedName, patientCard.UsedCardNo, patientCard.UsedPhone,
                patientCard.UsedTime, patientCard.UsedState, patientCard.UsedOper);
            return this.ExecNoQuery(sql);
        }

        #endregion 

        #region 删除
        /// <summary>
        /// 根据卡类型和卡号删除领取记录
        /// </summary>
        /// <param name="CardNoNO"></param>
        /// <param name="cardKind"></param>
        /// <returns></returns>
        public int DeletePatientCardByCardKindAndNo(string CardNo, string cardKind)
        {
            string sql = string.Empty;

            ArrayList dsiscountCards = new ArrayList();

            if (this.Sql.GetCommonSql("Fee.PatientDiscountCardLogic.SelectPatientDiscountCard.ByCardNoAndKind", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.PatientDiscountCardLogic.SelectPatientDiscountCard.ByCardNoAndKind的SQL语句";

                return -1;
            }

            dsiscountCards = QueryPatientDiscountCardBySql(sql, CardNo, cardKind);
            //该卡号存在已使用的记录不可退回
            if (dsiscountCards != null && dsiscountCards.Count > 0)
            {
                return -1;
            }

            if (this.Sql.GetCommonSql("Fee.PatientDiscountCardLogic.DeletePatientCardByCardKindAndNo", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.PatientDiscountCardLogic.DeletePatientCardByCardKindAndNo的SQL语句";

                return -1;
            }

            sql = string.Format(sql, CardNo, cardKind);

            return this.ExecNoQuery(sql);
        }

        #endregion 

        #region 查询

        /// <summary>
        /// 查询所有取卡记录
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryAllPatientCard()
        {
            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.PatientDiscountCardLogic.SelectALLDiscountCards", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.PatientDiscountCardLogic.SelectALLDiscountCards的SQL语句";

                return null;
            }

            return this.QueryPatientDiscountCardBySql(sql);
        }


        // <summary>
        /// 检测新卡号是否有效：
        /// </summary>
        /// <returns>有效true, 无效 false</returns>
        public bool NewCardNoIsValid(string NewCardNO, string cardKind)
        {
            string sql = string.Empty;

            ArrayList dsiscountCards = new ArrayList();

            if (this.Sql.GetCommonSql("Fee.PatientDiscountCardLogic.SelectPatientDiscountCard.ByCardNo", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.PatientDiscountCardLogic.SelectPatientDiscountCard.ByCardNo的SQL语句";

                return false;
            }

            dsiscountCards = QueryPatientDiscountCardBySql(sql, NewCardNO, cardKind);

            //如果没有符合条件的卡,说明可以生成
            if (dsiscountCards == null || dsiscountCards.Count == 0)
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// 查询所有取卡记录
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPatientCardByCardKindAndNO(string cardNO, string cardKind)
        {
            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.PatientDiscountCardLogic.SelectPatientDiscountCard.ByCardNo", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.PatientDiscountCardLogic.SelectPatientDiscountCard.ByCardNo的SQL语句";

                return null;
            }

            return this.QueryPatientDiscountCardBySql(sql, cardNO, cardKind);
        }


        #endregion 

        #endregion
    }
}
