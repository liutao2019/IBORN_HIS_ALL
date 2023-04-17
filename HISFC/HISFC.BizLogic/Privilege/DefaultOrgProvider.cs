using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;
using FS.HISFC.BizProcess.Interface.Privilege;

namespace FS.HISFC.BizLogic.Privilege
{
    class DefaultOrgProvider : IPrivInfo
    {
        #region IPrivInfo 成员

        /// <summary>
        /// 默认实现AppId
        /// </summary>
        /// <returns></returns>
        public string QueryAppID()
        {
            return "NEU";
        }

        /// <summary>
        /// 默认实现人员查询
        /// </summary>
        /// <returns></returns>
        public IList<FS.HISFC.Models.Privilege.Person> QueryPerson()
        {
            FS.HISFC.Models.Privilege.Person _person = new FS.HISFC.Models.Privilege.Person();
            _person.Id = "admin";
            _person.Name = "管理员";
            _person.Remark = "系统默认管理员,不能删除";
            _person.AppId = "NEU";
  

            IList<FS.HISFC.Models.Privilege.Person> _list = new List<FS.HISFC.Models.Privilege.Person>();
            _list.Add(_person);

            return _list;
        }

        /// <summary>
        /// 默认实现组织结构查询
        /// </summary>
        /// <returns></returns>
        public IList<HISFC.Models.Privilege.Organization> QueryUnit()
        {
            //Unit _unit = new Unit();
            //_unit.Id = "root";
            //_unit.Name = "管理机构";
            //_unit.ParentId = "";
            //_unit.Description = "系统默认组织单元,不能删除";
            //_unit.AppId = "NEU";

            //IList<HISFC.Models.Privilege.Organization> list = new List<HISFC.Models.Privilege.Organization>();
            //_list.Add(_unit);

            return null;
        }

        /// <summary>
        /// 默认获得组织结构类别
        /// </summary>
        /// <returns></returns>
        public List<String> GetOrgType()
        {
            return null;

        }
        #endregion
    }
}
