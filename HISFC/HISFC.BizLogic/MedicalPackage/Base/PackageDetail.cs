using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.MedicalPackage
{
    public class PackageDetail : Base.DataBase
	{
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.PackageDetail model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Insert";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, this.GetParameters(model));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public int Update(FS.HISFC.Models.MedicalPackage.PackageDetail model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Update", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Update";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, this.GetParameters(model));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(FS.HISFC.Models.MedicalPackage.PackageDetail model)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Delete", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Delete";
                return -1;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Delete.Where1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Delete.Where1";
                return -1;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, model.PackageID,model.SequenceNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        /// <summary>
        /// 根据套餐ID删除套餐明细
        /// </summary>
        public int DeleteByPackageID(string PackageID)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Delete", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Delete";
                return -1;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Delete.Where2", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Delete.Where2";
                return -1;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, PackageID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        /// <summary>
        /// 根据套餐ID查询明细
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList QueryPackageDetailByPackageID(string PackageID)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Select.Where1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Select.Where1";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, PackageID);
            }

            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return this.GetObjets(Sql);
        }

        /// <summary>
        /// 根据套餐编码和明细码
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.PackageDetail QueryPackageDetailByIDAndSeq(string PackageID,string SequenceNO)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.PackageDetail.Select.Where2", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.PackageDetail.Select.Where2";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, PackageID, SequenceNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            ArrayList al = this.GetObjets(Sql);

            if (al != null && al.Count > 0)
            {
                return al[0] as FS.HISFC.Models.MedicalPackage.PackageDetail;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList GetObjets(string sql)
        {
            if (this.ExecQuery(sql) < 0) return null;

            ArrayList packageArray = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.MedicalPackage.PackageDetail model = new FS.HISFC.Models.MedicalPackage.PackageDetail();
                    model.ExecDept = new FS.FrameWork.Models.NeuObject();
                    model.PackageID = this.Reader[0].ToString();
                    model.SequenceNO = this.Reader[1].ToString();
                    model.Item.ID = this.Reader[2].ToString();
                    model.Item.SysClass.ID = this.Reader[3].ToString();
                    model.ExecDept.ID = this.Reader[4].ToString();
                    model.Item.Price = Decimal.Parse((this.Reader[5]).ToString());
                    model.Item.Qty = Decimal.Parse((this.Reader[6]).ToString());
                    model.Unit = this.Reader[7].ToString();
                    model.UnitFlag = this.Reader[8].ToString();
                    model.Memo = this.Reader[9].ToString();
                    model.ModifyOper = this.Reader[10].ToString();
                    model.ModifyTime = DateTime.Parse(this.Reader[11].ToString());
                    model.CreateOper = this.Reader[12].ToString();
                    model.CreateTime = DateTime.Parse(this.Reader[13].ToString());

                    try
                    {
                        //{98A19959-7CD4-4f69-839D-D7020687D3A3}
                        model.Item.Name = this.Reader[14].ToString();
                        model.Item.Specs = this.Reader[15].ToString();
                    }
                    catch
                    {
                    }


                    packageArray.Add(model);
                }

                this.Reader.Close();
            }
            catch
            {
                this.Reader.Close();
            }

            return packageArray;
        }


        /// <summary>
        /// 通过实体获取参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.PackageDetail model)
        {
            if (model != null)
            {
                string[] Parameters = new string[]{ model.PackageID,
                                                    model.SequenceNO,
                                                    model.Item.ID,
                                                    model.Item.SysClass.ID.ToString(),
                                                    model.ExecDept.ID,
                                                    model.Item.Price.ToString(),
                                                    model.Item.Qty.ToString(),
                                                    model.Unit,
                                                    model.UnitFlag,
                                                    model.Memo,
                                                    model.ModifyOper,
                                                    model.ModifyTime.ToString(),
                                                    model.CreateOper,
                                                    model.CreateTime.ToString(),
                };

                return Parameters;
            }

            return null;
        }
	}
}
