using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Project
{
    public class AccountInfo
    {
        string curAccountID = "";

        public string AccountID
        {
            get { return curAccountID; }
            set { curAccountID = value; }
        }

        string curPassword = "";

        public string Password
        {
            get { return curPassword; }
            set { curPassword = value; }
        }

        string curEMail = "";

        public string EMail
        {
            get { return curEMail; }
            set { curEMail = value; }
        }

        string curRoleID = "";

        /// <summary>
        /// 角色编码，区分客户、开发、测试等角色
        /// </summary>
        public string RoleID
        {
            get { return curRoleID; }
            set { curRoleID = value; }
        }

        string curState = "";

        /// <summary>
        /// 账号状态：0申请，1已分配角色，2停用
        /// </summary>
        public string State
        {
            get { return curState; }
            set { curState = value; }
        }

        string curGrantAccout = "";

        /// <summary>
        /// 授权给本账号的账号
        /// </summary>
        public string GrantAccout
        {
            get { return curGrantAccout; }
            set { curGrantAccout = value; }
        }

        DateTime curSetTime = new DateTime();

        public DateTime SetTime
        {
            get { return curSetTime; }
            set { curSetTime = value; }
        }

        DateTime curGrantTime = new DateTime();

        public DateTime GrantTime
        {
            get { return curGrantTime; }
            set { curGrantTime = value; }
        }

        DateTime curStopTime = new DateTime();

        public DateTime StopTime
        {
            get { return curStopTime; }
            set { curStopTime = value; }
        }

        string curAddress = "";

        public string Address
        {
            get { return curAddress; }
            set { curAddress = value; }
        }

        string curTelephone = "";

        public string Telephone
        {
            get { return curTelephone; }
            set { curTelephone = value; }
        }

    }
}
