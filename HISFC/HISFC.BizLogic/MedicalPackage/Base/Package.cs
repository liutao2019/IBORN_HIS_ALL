using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.MedicalPackage
{
    public class Package : Base.DataBase
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Insert(FS.HISFC.Models.MedicalPackage.Package model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Insert", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Insert";
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
        public int Update(FS.HISFC.Models.MedicalPackage.Package model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Update", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Update";
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
        public int Delete(FS.HISFC.Models.MedicalPackage.Package model)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Delete", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Delete";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, model.ID);
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
        /// 根据ID查询套餐
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.Package QueryPackageByID(string PackageID)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select.Where1", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select.Where1";
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

            ArrayList al = this.GetObjets(Sql);

            if (al != null && al.Count > 0)
            {
                return al[0] as FS.HISFC.Models.MedicalPackage.Package;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据名称查询套餐
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList QueryPackageByName(string PackageName)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select.Where2", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select.Where2";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, PackageName);
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
        /// 根据类型查询套餐
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList QueryPackageByType(string Type)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select.Where3", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select.Where3";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, Type);
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
        /// 根据名称和类型查询套餐
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList QueryPackageByNameAndType(string PackageName,string Type)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select.Where4", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select.Where4";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, PackageName, Type);
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
        /// 根据父套餐编码查询
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList QueryPackageByParentCode(string parentCode)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select.Where5", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select.Where5";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, parentCode);
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
        /// 根据类型查询套餐
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ArrayList QueryParentPackage(string parentCode)
        {
            string Sql = "";
            string Where = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select";
                return null;
            }

            if (this.Sql.GetCommonSql("MedicalPackage.Package.Select.Where6", ref Where) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.Select.Where6";
                return null;
            }

            try
            {
                Sql += Where;
                Sql = string.Format(Sql, parentCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return this.GetObjets(Sql);
        }

        public decimal QueryPackageTotFee(string PackageID)
        {
            decimal tmp = -1m;

            string Sql = "";
            if (this.Sql.GetCommonSql("MedicalPackage.Package.QueryPackageTotFee", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.QueryPackageTotFee";
                return tmp;
            }

            Sql = string.Format(Sql, PackageID);

            if (this.ExecQuery(Sql) < 0) return tmp;

            try
            {
                if (this.Reader.Read())
                {
                    tmp = Decimal.Parse((this.Reader[0]).ToString());
                }

                this.Reader.Close();
            }
            catch
            {
                if (!this.Reader.IsClosed)
                    this.Reader.Close();
            }

            return tmp;

        }

        /// <summary>
        /// 设置套餐是否可用
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="valid"></param>
        /// <returns></returns>
        public int SetValidByID(string packageID,bool valid)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.UpdateValid", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.UpdateValid";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql, packageID, valid?"1":"0");
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
        /// 设置套餐是否可用
        /// </summary>
        /// <param name="model"></param>
        /// <param name="valid"></param>
        /// <returns></returns>
        public int SetValid(FS.HISFC.Models.MedicalPackage.Package model, bool valid)
        {
            string Sql = "";

            if (this.Sql.GetCommonSql("MedicalPackage.Package.UpdateValid", ref Sql) == -1)
            {
                this.Err = "没有找到MedicalPackage.Package.UpdateValid";
                return -1;
            }

            try
            {
                Sql = string.Format(Sql, model.ID, valid.ToString());
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
                    FS.HISFC.Models.MedicalPackage.Package model = new FS.HISFC.Models.MedicalPackage.Package();
                    model.ID = this.Reader[0].ToString();
                    model.Name = this.Reader[1].ToString();
                    model.SpellCode = this.Reader[2].ToString();
                    model.UserCode = this.Reader[3].ToString();
                    model.PackageType = this.Reader[4].ToString();
                    model.UserType = (FS.HISFC.Models.Base.ServiceTypes)Enum.Parse(typeof(FS.HISFC.Models.Base.ServiceTypes), this.Reader[5].ToString(), false);
                    model.SortID = System.Int32.Parse(this.Reader[6].ToString()); 
                    model.IsValid = this.Reader[7].ToString() == "1";
                    model.Memo = this.Reader[8].ToString();
                    model.PackageClass = this.Reader[9].ToString();  //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                    model.ParentCode = this.Reader[10].ToString();   //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                    model.ComboFlag = this.Reader[11].ToString();    //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                    model.MainFlag = this.Reader[12].ToString();     //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                    model.SpecialFlag = this.Reader[13].ToString();  //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                    model.ModifyOper = this.Reader[14].ToString();
                    model.ModifyTime = DateTime.Parse(this.Reader[15].ToString());
                    model.CreateOper = this.Reader[16].ToString();
                    model.CreateTime = DateTime.Parse(this.Reader[17].ToString());
                    model.User01 = this.Reader[18].ToString(); //总金额
                    model.User02 = this.Reader[19].ToString(); //套餐类型

                    packageArray.Add(model);
                }

                this.Reader.Close();
            }
            catch(Exception ex)
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
        private string[] GetParameters(FS.HISFC.Models.MedicalPackage.Package model)
        {
            if (model != null)
            {
                string[] Parameters = new string[]{ model.ID,
                                                   model.Name,
                                                   model.SpellCode,
                                                   model.UserCode,
                                                   model.PackageType,
                                                   model.UserType.ToString(),
                                                   model.SortID.ToString(),
                                                   model.IsValid?"1":"0",
                                                   model.Memo,
                                                   model.PackageClass,  //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                                                   model.ParentCode,    //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                                                   model.ComboFlag,     //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                                                   model.MainFlag,      //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
                                                   model.SpecialFlag,   //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
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
