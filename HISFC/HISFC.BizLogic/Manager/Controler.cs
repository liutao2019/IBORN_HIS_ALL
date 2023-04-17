using System;
using System.Collections;
using System.Collections.Generic;

namespace Neusoft.HISFC.BizLogic.Manager
{
	/// <summary>
	/// Controler ��ժҪ˵����
	/// </summary>
	public class Controler:Neusoft.FrameWork.Management.Database
	{
		public Controler()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        /// <summary>
        /// ���Ʋ������� Key ControlCode  Value ControlValue
        /// </summary>
        private static Dictionary<string, string> controlDictionary = new Dictionary<string, string>();

		/// <summary>
		/// ��ӿ�����Ϣ
		/// </summary>
		/// <param name="Controler">���Ʋ�����Ϣ</param>
		/// <returns>�ɹ�����1ʧ�ܷ���-1</returns>
		public int AddControlerInfo(Neusoft.HISFC.Models.Base.Controler Controler)
		{
			string strSql = "";
            if (this.Sql.GetSql( "AddControlerInfo.1", ref strSql ) == -1)
            {
                return -1;
            }

			try
			{
				//0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���4����Ա5����ʱ��
				strSql = string.Format(strSql,Controler.ID,Controler.Name,Controler.ControlerValue,Neusoft.FrameWork.Function.NConvert.ToInt32(Controler.IsVisible).ToString(),
					this.Operator.ID,this.GetSysDateTime());
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// ���¿�����Ϣ
		/// </summary>
		/// <param name="Controler">���Ʋ�����Ϣ</param>
		/// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
		public int UpdateControlerInfo(Neusoft.HISFC.Models.Base.Controler Controler)
		{
			string strSql = "";
            if (this.Sql.GetSql( "UpdateControlerInfo.1", ref strSql ) == -1)
            {
                return -1;
            }

			try
			{
				//0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���4����Ա5����ʱ��
                strSql = string.Format( strSql, Controler.ID, Controler.Name, Controler.ControlerValue, Neusoft.FrameWork.Function.NConvert.ToInt32( Controler.IsVisible ).ToString(),
					this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}

		/// <summary>
		/// ����������Ϣ ֻ��ʾ�ÿͻ����Կ�������Ϣ
		/// </summary>
		/// <returns></returns>
		public ArrayList QueryControlerInfo()
		{
			string strSql = "";
			ArrayList al = new ArrayList();
            if (this.Sql.GetSql( "QueryControlerInfo.1", ref strSql ) == -1)
            {
                return null;
            }

            if (this.ExecQuery( strSql ) == -1)
            {
                return null;
            }

			//0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���
			while (this.Reader.Read())
			{
				Neusoft.HISFC.Models.Base.Controler Controler = new Neusoft.HISFC.Models.Base.Controler();
                try
                {
                    Controler.ID = this.Reader[0].ToString();
                    Controler.Name = this.Reader[1].ToString();
                    Controler.ControlerValue = this.Reader[2].ToString();
                    Controler.IsVisible = Neusoft.FrameWork.Function.NConvert.ToBoolean( this.Reader[3].ToString() );
                    Controler.User01 = this.Reader[4].ToString();
                    Controler.User02 = this.Reader[5].ToString();
                }
                catch (Exception ex)
                {
                    this.Err = "��ѯ������Ϣ��ֵ����!" + ex.Message;
                    return null;
                }
                finally
                {
                    this.Reader.Close();
                }
				al.Add(Controler);
			}

			return al;
		}

        /// <summary>
        /// ���ݿ������������������͵�ֵ
        /// �����´����ݿ���ȡ
        /// </summary>
        /// <param name="controlCode"></param>
        /// <returns></returns>
        public string QueryControlerInfo(string controlCode)
        {
            return this.QueryControlerInfo( controlCode, false );
        }

        /// <summary>
        /// ���ݿ������������������͵�ֵ
        /// </summary>
        /// <param name="controlCode"></param>
        /// <param name="isRefresh"></param>
        /// <returns></returns>
        public string QueryControlerInfo(string controlCode, bool isRefresh)
        {
            //������ȡ
            if (isRefresh == false)
            {
                if (controlDictionary.ContainsKey( controlCode ) == true)         //�Ѱ���
                {
                    return controlDictionary[controlCode];
                }
            }

            string strSql = "";
            if (this.Sql.GetSql( "QueryControlerInfo.2", ref strSql ) == -1)
            {
                return "";
            }
            try
            {
                //0���Ʋ�������
                strSql = string.Format( strSql, controlCode );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return "";
            }
            string strValue = this.ExecSqlReturnOne( strSql );

            if (controlDictionary.ContainsKey( controlCode ) == false)         //�������ö�Ӧ ��ӵ�������
            {
                controlDictionary.Add( controlCode, strValue );
            }

            return strValue;
        }

		/// <summary>
		/// ���������������Ϣ
		/// </summary>
		/// <param name="ctrlCode"></param>
		/// <returns></returns>
        public Neusoft.HISFC.Models.Base.Controler QueryControlInfoByCode(string ctrlCode)
        {
            string strSql = "";

            if (this.Sql.GetSql( "QueryControlInfoByCode", ref strSql ) == -1)
            {
                return null;
            }

            strSql = string.Format( strSql, ctrlCode );
            if (this.ExecQuery( strSql ) == -1)
            {
                return null;
            }
            Neusoft.HISFC.Models.Base.Controler Controler = null;
            //0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���
            while (this.Reader.Read())
            {
                Controler = new Neusoft.HISFC.Models.Base.Controler();
                try
                {
                    Controler.ID = this.Reader[0].ToString();
                    Controler.Name = this.Reader[1].ToString();
                    Controler.ControlerValue = this.Reader[2].ToString();
                    Controler.IsVisible = Neusoft.FrameWork.Function.NConvert.ToBoolean( this.Reader[3].ToString() );
                    Controler.User01 = this.Reader[4].ToString();
                    Controler.User02 = this.Reader[5].ToString();
                }
                catch (Exception ex)
                {
                    this.Err = "��ѯ������Ϣ��ֵ����!" + ex.Message;

                    return null;
                }
                finally
                {
                    this.Reader.Close();
                }
            }

            return Controler;
        }

		/// <summary>
		/// ���������������Ϣ
		/// </summary>
		/// <param name="ctrlName"></param>
		/// <returns></returns>
		public Neusoft.HISFC.Models.Base.Controler QueryControlInfoByName(string ctrlName)
		{
			string strSql = "";

            if (this.Sql.GetSql( "QueryControlInfoByName", ref strSql ) == -1)
            {
                return null;
            }

			strSql = string.Format(strSql,ctrlName);
            if (this.ExecQuery( strSql ) == -1)
            {
                return null;
            }
			Neusoft.HISFC.Models.Base.Controler Controler = null;
			//0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���
			while (this.Reader.Read())
			{
				Controler = new Neusoft.HISFC.Models.Base.Controler();
                try
                {
                    Controler.ID = this.Reader[0].ToString();
                    Controler.Name = this.Reader[1].ToString();
                    Controler.ControlerValue = this.Reader[2].ToString();
                    Controler.IsVisible = Neusoft.FrameWork.Function.NConvert.ToBoolean( this.Reader[3].ToString() );
                    Controler.User01 = this.Reader[4].ToString();
                    Controler.User02 = this.Reader[5].ToString();
                }
                catch (Exception ex)
                {
                    this.Err = "��ѯ������Ϣ��ֵ����!" + ex.Message;
                    return null;
                }
                finally
                {
                    this.Reader.Close();
                }
			}

			return Controler;
		}

		public ArrayList QueryControlInfoByKind(string Kind)
		{
			string strSql = "";
			ArrayList al = new ArrayList();
            if (this.Sql.GetSql( "QueryControlInfoByKind", ref strSql ) == -1)
            {
                return null;
            }

			strSql = string.Format(strSql,Kind);
            if (this.ExecQuery( strSql ) == -1)
            {
                return null;
            }

			//0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���
			while (this.Reader.Read())
			{
				Neusoft.HISFC.Models.Base.Controler Controler = new Neusoft.HISFC.Models.Base.Controler();
                try
                {
                    Controler.ID = this.Reader[0].ToString();
                    Controler.Name = this.Reader[1].ToString();
                    Controler.ControlerValue = this.Reader[2].ToString();
                    Controler.IsVisible = Neusoft.FrameWork.Function.NConvert.ToBoolean( this.Reader[3].ToString() );
                    Controler.User01 = this.Reader[4].ToString();
                    Controler.User02 = this.Reader[5].ToString();
                }
                catch (Exception ex)
                {
                    this.Err = "��ѯ������Ϣ��ֵ����!" + ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                finally
                {
                    this.Reader.Close();
                }
				al.Add(Controler);
			}

			return al;
		}
	}
}
