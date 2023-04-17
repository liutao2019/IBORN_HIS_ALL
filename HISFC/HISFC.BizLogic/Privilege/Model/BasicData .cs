using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.Privilege.Model
{
    //Add Service Contract sign
    /// <summary>
    /// ʵ�����(���е�ʵ���඼Ҫ�̳���)
    /// �ṩ�������� 
    /// Id:string
    /// Name:string
    /// Remark:string
    /// ClassCode:string
    /// </summary>
    [Serializable]
    public class BasicData 
    {
        /// <summary>
        /// ���ݱ�ʶ��
        /// </summary>
        protected String id;

        /// <summary>
        /// ���ݱ�ʶ��
        /// </summary>
        public String Id
        {
            get { return id; }
            set { id = value == null ? "" : value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        protected String name;

        /// <summary>
        /// ��������
        /// </summary>
        public String Name
        {
            get { return name; }
            set { name = value == null ? "" : value; }
        }

        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        protected String remark;

        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        public String Remark
        {
            get { return remark; }
            set { remark = value == null ? "" : value; }
        }


        ///// <summary>
        ///// ����HL7��չ
        ///// </summary>
        //public String ClassCode
        //{
        //    get
        //    {
        //        if (PropertyCollection.ContainsKey("ClassCode"))
        //            return (String)PropertyCollection["ClassCode"];
        //        else
        //            return "";
        //    }
        //    set { SetProperty("ClassCode",value); }
        //}

    }
}
