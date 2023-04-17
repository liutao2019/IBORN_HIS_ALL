using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Common 
{
    /// <summary>
    /// [��������: ���Ͽ��Ʋ���������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-4-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class ControlParam : Integrate.IntegrateBase
    {

        #region ����

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.FrameWork.Management.ControlParam controlParam = new FS.FrameWork.Management.ControlParam();

        #endregion

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;

            controlParam.SetTrans(this.trans);
            
            base.SetTrans(trans);
        }

        #region ����

        #region ������ѯ


        /// <summary>
        /// ������ѯ���Ʋ���
        /// </summary>
        /// <typeparam name="T">��õĲ������� ����Int</typeparam>
        public int GetBatchControlParam(ref System.Collections.Hashtable hsControls)
        {
            this.SetDB(controlParam);

            return controlParam.QueryBatchControlerInfo(ref hsControls);
        }

        #endregion

        /// <summary>
        /// ��ÿ��Ʋ���ֵ,������ݿ�����ԭ�򷵻������T���� defaultValue
        /// </summary>
        /// <typeparam name="T">��õĲ������� ����Int</typeparam>
        /// <param name="controlCode">��������</param>
        /// <param name="isRefresh">�Ƿ�����ˢ�����ݿ�</param>
        /// <param name="defaultValue">T���͵�Ĭ��ֵ</param>
        /// <returns>�ɹ� ��ǰT���Ͳ���ֵ ʧ�� T ���͵Ĵ���Ĭ��ֵ</returns>
        public T GetControlParam<T>(string controlCode, bool isRefresh, T defaultValue) 
        {
            this.SetDB(controlParam);

            string tempReturnValue = controlParam.QueryControlerInfo(controlCode, isRefresh);

            //���������ô���,Ĭ�Ϸ���false
            if (tempReturnValue == null || tempReturnValue == "-1")
            {
                return defaultValue;
            }

            T tempValue = default(T);

            switch (Type.GetTypeCode(typeof(T))) 
            {
                case TypeCode.String:
                    tempValue = (T)(object)tempReturnValue;

                    break;
                case TypeCode.Int32:
                    tempValue = (T)(object)FS.FrameWork.Function.NConvert.ToInt32(tempReturnValue);

                    break;
                case TypeCode.Boolean:
                    tempValue = (T)(object)FS.FrameWork.Function.NConvert.ToBoolean(tempReturnValue);

                    break;
                case TypeCode.Decimal:
                    tempValue = (T)(object)FS.FrameWork.Function.NConvert.ToDecimal(tempReturnValue);

                    break;
                case TypeCode.DateTime:
                    tempValue = (T)(object)FS.FrameWork.Function.NConvert.ToDateTime(tempReturnValue);

                    break;
            }

            return tempValue;
        }

        /// <summary>
        /// ��ÿ��Ʋ���ֵ,������ݿ�����ԭ�򷵻�T����default(T)
        /// </summary>
        /// <typeparam name="T">��õĲ������� ����Int</typeparam>
        /// <param name="controlCode">��������</param>
        /// <param name="isRefresh">�Ƿ�����ˢ�����ݿ�</param>
        /// <returns>�ɹ� ��ǰT���Ͳ���ֵ ʧ�� T����default(T)</returns>
        public T GetControlParam<T>(string controlCode, bool isRefresh)
        {
            return this.GetControlParam<T>(controlCode, isRefresh, default(T));
        }

        /// <summary>
        /// ��ÿ��Ʋ���ֵ(ÿ�ζ�����ˢ�����ݿ�),������ݿ�����ԭ�򷵻�T����default(T)
        /// </summary>
        /// <typeparam name="T">��õĲ������� ����Int</typeparam>
        /// <param name="controlCode">��������</param>
        /// <returns>�ɹ� ��ǰT���Ͳ���ֵ ʧ�� T����default(T)</returns>
        public T GetControlParam<T>(string controlCode)
        {
            return this.GetControlParam<T>(controlCode, true, default(T));
        }

        #endregion
    }
}
