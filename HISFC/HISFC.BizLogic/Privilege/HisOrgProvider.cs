using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Public;
using FS.HISFC.BizLogic.Privilege.Model;
using FS.HISFC.BizProcess.Interface.Privilege;

namespace FS.HISFC.BizLogic.Privilege
{
    public class HisOrgProvider:IPrivInfo
    {
        #region IPrivInfo 成员

        public string QueryAppID()
        {
            return "HIS";
        }

        public IList<FS.HISFC.Models.Privilege.Person> QueryPerson()
        {
            IList<FS.HISFC.Models.Privilege.Person> persons = new List<FS.HISFC.Models.Privilege.Person>();

            FS.HISFC.BizLogic.Manager.UserManager useManager = new FS.HISFC.BizLogic.Manager.UserManager();
            ArrayList employeeList = useManager.GetAllPeronList();           
            if (employeeList != null)
            {
                for (int i = 0; i < employeeList.Count; i++)
                {
                    FS.HISFC.Models.Privilege.Person Person = new FS.HISFC.Models.Privilege.Person();
                    Person.Id = ((FS.HISFC.Models.Base.Employee)employeeList[i]).ID;
                    Person.Name = ((FS.HISFC.Models.Base.Employee)employeeList[i]).Name;
                    Person.AppId = "HIS";
                    Person.Remark = "";
                    persons.Add(Person);
                }
            }
            return persons;
        }

        public IList<HISFC.Models.Privilege.Organization> QueryUnit()
        {
            //IList<HISFC.Models.Privilege.Organization> organizationList = new List<HISFC.Models.Privilege.Organization>();
            //List<Organization> list = null;
            //List<String> listType = null;

            //FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();

            //    listType = GetOrgType();
            //    //默认设置组织结构类型为第一个。
            //    if (listType != null)
            //    {
            //        list = dept.ge.Query(listType[0].Split('|')[0]);
            //    }
            //    else
            //    {
            //        return null;
            //    }

            ////LIST[0]为默认根节点；

            //PrivOrganization rootPrivOrg = new PrivOrganization();
            //// rootPrivOrg.Id = null;
            //rootPrivOrg.Name = listType[0].Split('|')[1];
            //rootPrivOrg.Level = 0;
            //rootPrivOrg.Type = listType[0].Split('|')[0];
            //organizationList.Add(rootPrivOrg);

            //foreach (Organization newOrg in list)
            //{
            //    PrivOrganization newPrivOrg = new PrivOrganization();
            //    newPrivOrg.Id = newOrg.Id;
            //    newPrivOrg.Name = newOrg.Name;
            //    newPrivOrg.AppId = newOrg.AppId;
            //    newPrivOrg.ClassCode = newOrg.ClassCode;
            //    newPrivOrg.Department = newOrg.Department;
            //    newPrivOrg.HasChildren = newOrg.HasChildren;
            //    newPrivOrg.Level = newOrg.Level;
            //    newPrivOrg.OrderNumber = newOrg.OrderNumber;
            //    newPrivOrg.Remark = newOrg.Remark;
            //    newPrivOrg.Type = newOrg.Type;
            //    newPrivOrg.Parent = newOrg.Parent;
            //    organizationList.Add(newPrivOrg);
            //}

            //return organizationList;
            return null;
        }

        public List<System.String> GetOrgType()
        {

            List<System.String> orgTypeList = new List<string>();
            foreach (EnumHelper.EnumObject newEnum in EnumHelper.Current.EnumList<OrganizationType>())
            {
                string orgString = newEnum.ID + "|" + newEnum.Name;
                orgTypeList.Add(orgString);
            }

            return orgTypeList;
        
        }

        #endregion
    }
}
