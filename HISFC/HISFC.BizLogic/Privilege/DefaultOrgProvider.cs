using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;
using FS.HISFC.BizProcess.Interface.Privilege;

namespace FS.HISFC.BizLogic.Privilege
{
    class DefaultOrgProvider : IPrivInfo
    {
        #region IPrivInfo ��Ա

        /// <summary>
        /// Ĭ��ʵ��AppId
        /// </summary>
        /// <returns></returns>
        public string QueryAppID()
        {
            return "NEU";
        }

        /// <summary>
        /// Ĭ��ʵ����Ա��ѯ
        /// </summary>
        /// <returns></returns>
        public IList<FS.HISFC.Models.Privilege.Person> QueryPerson()
        {
            FS.HISFC.Models.Privilege.Person _person = new FS.HISFC.Models.Privilege.Person();
            _person.Id = "admin";
            _person.Name = "����Ա";
            _person.Remark = "ϵͳĬ�Ϲ���Ա,����ɾ��";
            _person.AppId = "NEU";
  

            IList<FS.HISFC.Models.Privilege.Person> _list = new List<FS.HISFC.Models.Privilege.Person>();
            _list.Add(_person);

            return _list;
        }

        /// <summary>
        /// Ĭ��ʵ����֯�ṹ��ѯ
        /// </summary>
        /// <returns></returns>
        public IList<HISFC.Models.Privilege.Organization> QueryUnit()
        {
            //Unit _unit = new Unit();
            //_unit.Id = "root";
            //_unit.Name = "�������";
            //_unit.ParentId = "";
            //_unit.Description = "ϵͳĬ����֯��Ԫ,����ɾ��";
            //_unit.AppId = "NEU";

            //IList<HISFC.Models.Privilege.Organization> list = new List<HISFC.Models.Privilege.Organization>();
            //_list.Add(_unit);

            return null;
        }

        /// <summary>
        /// Ĭ�ϻ����֯�ṹ���
        /// </summary>
        /// <returns></returns>
        public List<String> GetOrgType()
        {
            return null;

        }
        #endregion
    }
}
