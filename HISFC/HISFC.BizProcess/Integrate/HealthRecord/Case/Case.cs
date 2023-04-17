using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizProcess.Integrate.HealthRecord.Case
{
    /// <summary>
    /// [��������: �����������]<br></br>
    /// [�� �� ��: ��ȫ]<br></br>
    /// [����ʱ��: 2007-9-11]<br></br>
    /// </summary>
    public class Case : IntegrateBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public Case()
        {
        }

        #region ����

        /// <summary>
        /// ���ҹ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// ��Ա����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();

        //{D2BDB9B8-7D50-4a66-8D1C-28EA0420592F}
        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //{D2BDB9B8-7D50-4a66-8D1C-28EA0420592F}
        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            this.con.SetTrans(trans);
            this.dept.SetTrans(trans);
            this.person.SetTrans(trans);
        }

        /// <summary>
        /// ��ȡ���п����б�
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDeptmentAll()
        {
            this.SetDB(this.dept);
            return this.dept.GetDeptmentAll();
        }

        /// <summary>
        /// ���ݿ��ұ�Ż�ÿ�����Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Department GetDeptmentById(string id)
        {
            this.SetDB(this.dept);
            return this.dept.GetDeptmentById(id);
        }

        /// <summary>
        /// ��ȡ������Ա�б�
        /// </summary>
        /// <returns></returns>
        public ArrayList GetEmployeeAll()
        {
            this.SetDB(this.person);
            return this.person.GetEmployeeAll();
        }

        /// <summary>
        /// ����Ա����Ż��Ա����Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Employee GetPersonByID(string id)
        {
            this.SetDB(this.person);
            return this.person.GetPersonByID(id);
        }

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <returns></returns>
        public ArrayList GetCaseConstant(string str)
        {
            this.SetDB(con);
            return con.GetList(str);
        }

        /// <summary>
        /// ����id��ѯ����
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public NeuObject GetConstant(string type, string id)
        {
            this.SetDB(con);
            return this.con.GetConstant(type, id);
        }

        #endregion

    }
}
