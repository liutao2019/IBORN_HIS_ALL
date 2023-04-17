using System;
using System.Collections.Generic;
using System.Reflection;
using FS.HISFC.BizProcess.Interface.FeeInterface;

namespace FS.HISFC.BizProcess.Integrate.FeeInterface
{
    public abstract class MedcareInterfaceProxyBase
    {
        #region ����

        /// <summary>
        /// ҽ���ӿ�ʵ��
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare medcaredInterface = null;

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        protected FS.FrameWork.Management.Transaction trans = null;
        
        /// <summary>
        /// ������Ϣ
        /// </summary>
        protected string errMsg = string.Empty;

        /// <summary>
        /// ���õ�ҽ���ӿ�ʵ��
        /// </summary>
        protected object objInterface = null;//���ýӿ�ʵ��

        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        protected string pactCode = null;//��ͬ��λ����

        #endregion

        #region ����

        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        public string PactCode
        {
            set
            {
                string tmpPactCode = value;
                //����¸�ֵ�ĺ�ͬ��λ���벻����ԭ���ĺ�ͬ��λ����
                //˵�����ߵ�ҽ���ӿڿ��ܷ�������,�ѽӿ�ʵ�����,�����ҵ������·���
                //��Ӧ��ҽ���ӿ�ʵ��
                if (tmpPactCode != this.pactCode)
                {
                    this.objInterface = null;
                }

                pactCode = tmpPactCode;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return errMsg;
            }
        }

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        public FS.FrameWork.Management.Transaction Trans
        {
            set
            {
                this.trans = value;
                try
                {
                    if (objInterface == null)
                    {
                        //�����ǰ��ʵ��Ϊnull,���»��ҽ������ʵ��
                        objInterface = this.GetInterfaceFromPact(pactCode);
                        if (objInterface == null)
                        {
                            return;
                        }
                    }
                    //((FeeInterface.IMedcare)objInterface).SetTrans(trans);
                }
                catch (Exception e)
                {
                    this.errMsg = e.Message;

                    return;
                }
            }
        }

        #endregion

        #region ����


        /// <summary>
        /// ���ñ������ݿ�����
        /// </summary>
        /// <param name="t">��ǰ���ݿ�����</param>
        public void SetTrans(FS.FrameWork.Management.Transaction t)
        {
            this.trans = t;
            try
            {
                if (objInterface == null)
                {
                    //�����ǰ��ʵ��Ϊnull,���»��ҽ������ʵ��
                    objInterface = this.GetInterfaceFromPact(pactCode);
                    if (objInterface == null)
                    {
                        return;
                    }
                }
                //((FeeInterface.IMedcare)objInterface).SetTrans(t);
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return;
            }
        }

        /// <summary>
        /// ��ô�����Ϣ
        /// </summary>
        /// <returns>������Ϣ</returns>
        public string GetErrMsg()
        {
            return this.errMsg;
        }

        /// <summary>
        /// ���ú�ͬ��λ����
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        public void SetPactCode(string pactCode)
        {
            if (pactCode != this.pactCode)
            {
                this.objInterface = null;
            }

            this.pactCode = pactCode;
        }

        /// <summary>
        /// ͨ����ͬ��λ������
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�ɹ�: ҽ���ӿ�ʵ�� ʧ��: null</returns>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare GetInterfaceFromPact(string pactCode)
        {

            FS.FrameWork.Management.ControlParam myCtrl = new FS.FrameWork.Management.ControlParam();
            //TransΪȫ���� ����Ҫ����SetTrans
            //if (this.trans != null)
            //{
            //    myCtrl.SetTrans(trans.Trans);
            //}
            FS.HISFC.Models.Base.ControlParam con = myCtrl.QueryControlInfoByName(pactCode);
            if (con == null)
            {
                this.errMsg = "��õ��ýӿڴ���!" + myCtrl.Err;

                return null;
            }

            try
            {
                Assembly a = Assembly.LoadFrom(con.ControlerValue);
                System.Type[] types = a.GetTypes();
                foreach (System.Type type in types)
                {
                    if (type.GetInterface("IMedcare") != null)
                    {
                        objInterface = System.Activator.CreateInstance(type);
                    }
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return null;
            }

            return (FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface;
        }

        #region ���ݿ�����

        /// <summary>
        /// ���ݿ��ύ
        /// </summary>
        /// <returns>falseʧ�� ture�ɹ�</returns>
        public bool Commit()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "��ͬ��λû�и�ֵ";

                return false;
            }

            return this.Commit(this.pactCode);
        }

        /// <summary>
        /// ���ݿ�ع�
        /// </summary>
        /// <returns>falseʧ�� ture�ɹ�</returns>
        public bool Rollback()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "��ͬ��λû�и�ֵ";

                return false;
            }

            return this.Rollback(this.pactCode);
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        /// <returns>falseʧ�� ture�ɹ�</returns>
        public bool Connect()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "��ͬ��λû�и�ֵ";

                return false;
            }

            return this.Connect(this.pactCode);
        }

        /// <summary>
        /// �Ͽ����ݿ�����
        /// </summary>
        /// <returns>falseʧ�� ture�ɹ�</returns>
        public bool Disconnect()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "��ͬ��λû�и�ֵ";

                return false;
            }

            return this.Disconnect(this.pactCode);
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <returns>falseʧ�� ture�ɹ�</returns>
        public bool Connect(string pactCode)
        {
            if (pactCode != this.pactCode)
            {
                objInterface = null;
            }
            try
            {
                if (objInterface == null)
                {
                    objInterface = this.GetInterfaceFromPact(pactCode);
                    if (objInterface == null)
                    {
                        return false;
                    }
                }
                
                long lReturn = ((FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface).Connect();
                
                if (lReturn < 0)
                {
                    this.errMsg = ((FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface).ErrMsg;

                    return false;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return false;
            }

            return true;
        }

        /// <summary>
        /// ���ݿ��ύ
        /// </summary>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <returns>falseʧ�� ture�ɹ�</returns>
        public bool Commit(string pactCode)
        {
            try
            {
                if (objInterface == null)
                {
                    objInterface = this.GetInterfaceFromPact(pactCode);
                    if (objInterface == null)
                    {
                        return false;
                    }
                }

                long lReturn = ((FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface).Commit();
                
                if (lReturn < 0)
                {
                    this.errMsg = ((FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface).ErrMsg;

                    return false;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return false;
            }

            return true;
        }

        /// <summary>
        /// ���ݿ�ع�
        /// </summary>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <returns>falseʧ�� ture�ɹ�</returns>
        public bool Rollback(string pactCode)
        {
            try
            {
                if (objInterface == null)
                {
                    objInterface = this.GetInterfaceFromPact(pactCode);

                    if (objInterface == null)
                    {
                        return false;
                    }
                }
                long lReturn = ((FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface).Rollback();

                if (lReturn < 0)
                {
                    this.errMsg = ((FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface).ErrMsg;

                    return false;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return false;
            }

            return true;
        }

        /// <summary>
        /// �Ͽ����ݿ�����
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>falseʧ�� ture�ɹ�</returns>
        public bool Disconnect(string pactCode)
        {
            try
            {
                if (objInterface == null)
                {
                    objInterface = this.GetInterfaceFromPact(pactCode);
                    if (objInterface == null)
                    {
                        return false;
                    }
                }

                long lReturn = ((FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface).Disconnect();

                if (lReturn < 0)
                {
                    this.errMsg = ((FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare)objInterface).ErrMsg;

                    return false;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ʼһ��������
        /// </summary>
        /// <returns>�ɹ� true ʧ��false</returns>
        public bool BeginTranscation()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "��ͬ��λû�и�ֵ";

                return false;
            }
            return this.BeginTranscation(this.pactCode);
        }

        /// <summary>
        /// ��ʼһ��������
        /// </summary>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <returns></returns>
        public bool BeginTranscation(string pactCode)
        {
            try
            {
                if (objInterface == null)
                {
                    objInterface = this.GetInterfaceFromPact(pactCode);
                    if (objInterface == null)
                    {
                        return false;
                    }
                }

                ((IMedcare)objInterface).BeginTranscation();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}
