using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

/// <summary>
/// [��������: ҽ��ճ��]<br></br>
/// [�� �� ��: ]<br></br>
/// [����ʱ��: ]<br></br>
/// <�޸ļ�¼
///		�޸���=''
///		�޸�ʱ��=''
///		�޸�Ŀ��=''
///		�޸�����=''
///  />
/// </summary>
namespace FS.HISFC.Components.Order.Classes
{
    
    #region ������

    /// <summary>
    /// ������
    /// </summary>
    internal class Clipboard
    {
        #region ����

        List<object> instanceCollection = new List<object>();

        /// <summary>
        /// ������
        /// </summary>
        IClipboard clipboard = null;

        #endregion

        #region ����

        /// <summary>
        /// Ĭ�ϲ���xml������
        /// </summary>
        public Clipboard()
        {
            clipboard = new BinaryClipboard();
        }

        /// <summary>
        /// ��������Ϊtype�ļ�����
        /// </summary>
        /// <param name="type"></param>
        public Clipboard(EnumClipboard type)
        {
            ClipboardEnumService service = new ClipboardEnumService();
            string classFullName = service.GetName(type);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            clipboard = assembly.CreateInstance(classFullName) as IClipboard;
            //����һ��������
            if (clipboard == null)
            {
                clipboard = new XmlClipboard();
            }
        }

        #endregion

        #region ��������

        /// <summary>
        /// ����������в��뵱ǰʵ��,����ֱ��ִ��copy
        /// Ҫcopy��ʹ��Copy();
        /// </summary>
        /// <param name="instance"></param>
        public void Add(object instance)
        {
            instanceCollection.Add(instance);
        }

        /// <summary>
        /// ��ռ�����
        /// </summary>
        public void Clear()
        {
            instanceCollection.Clear();
        }

        /// <summary>
        /// ����
        /// </summary>
        public bool Copy()
        {
            bool result = clipboard.Copy(instanceCollection);
            instanceCollection.Clear();
            return result; ;
        }

        /// <summary>
        /// ճ��
        /// ճ�����ݱ���װ��List-object�����б���
        /// </summary>
        /// <returns>����ճ������</returns>
        public object Paste()
        {
            return clipboard.Paste(instanceCollection.GetType());
        }

        #endregion
    }

    #endregion

    #region ö�ٺ�ö�ٷ�����

    /// <summary>
    /// ��������ö��
    /// </summary>
    enum EnumClipboard
    {
        Xml,
        Binary
    }

    /// <summary>
    /// ����ö�ٷ�����
    /// </summary>
    class ClipboardEnumService : EnumServiceBase
    {
        static System.Collections.Hashtable hashtable = new System.Collections.Hashtable();

        static ClipboardEnumService()
        {
            hashtable[EnumClipboard.Xml] = "FS.HISFC.Components.Order.Classes.XmlClipboard";
            hashtable[EnumClipboard.Binary] = "FS.HISFC.Components.Order.Classes.BinaryClipboard";
        }

        EnumClipboard myEnum;

        protected override Enum EnumItem
        {
            get
            {
                return myEnum;
            }
        }

        protected override System.Collections.Hashtable Items
        {
            get
            {
                return hashtable;
            }
        }
    }

    #endregion

    #region ������

    /// <summary>
    /// �����ӿڣ�һ��ֻ����һ��ʵ��
    /// </summary>
    interface IClipboard
    {
        object Paste(Type type);
        bool Copy(object parameter);
    }

    /// <summary>
    /// xml������
    /// </summary>
    class XmlClipboard : IClipboard
    {
        const string file = "clipboard.xml";

        public object Paste(Type type)
        {
            System.IO.Stream stream = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
                object instance = serializer.Deserialize(stream);
                stream.Flush();
                stream.Close();
                return instance;
            }
            catch
            {
                return null;
            }
            finally
            {
                stream.Close();
            }
        }

        public bool Copy(object parameter)
        {
            System.IO.Stream stream = new System.IO.FileStream(file, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(parameter.GetType());
                serializer.Serialize(stream, parameter);
                stream.Flush();
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Copy" + ex.Message);
                return false;
            }
            finally
            {
                stream.Close();
            }
        }
    }

    /// <summary>
    /// �����Ƽ�����
    /// </summary>
    class BinaryClipboard : IClipboard
    {
        const string file = "clipboard.dat";

        public object Paste(Type type)
        {
            if (System.IO.File.Exists( file ) == false)
            {
                return null;
            }
            System.IO.Stream stream = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                object instance = formater.Deserialize(stream);
                stream.Flush();
                stream.Close();
                return instance;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Paste" + ex.Message);
                return null;
            }
            finally
            {
                stream.Close();
            }
        }

        public bool Copy(object parameter)
        {
            System.IO.Stream stream = new System.IO.FileStream(file, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formater.Serialize(stream, parameter);
                stream.Flush();
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Copy" + ex.Message);
                return false;
            }
            finally
            {
                stream.Close();
            }
        }
    }

    #endregion

    #region ��ʷҽ����ѯճ����

    /// <summary>
    /// ��ʷҽ����ѯճ����
    /// </summary>
    public class HistoryOrderClipboard
    {
        static List<string> data = new List<string>();

        #region {58825498-BE39-4233-B097-B04E13FA1B36}

        /// <summary>
        /// �Ƿ��ȡ����
        /// </summary>
        public static bool isReaded = true;

        /// <summary>
        /// �Ƿ��Ѿ��и��Ƶ�����
        /// </summary>
        public static bool IsHaveCopyData = false;

        ///// <summary>
        ///// �Ƿ��Ѿ��и��Ƶ�����
        ///// </summary>
        //public static bool IsHaveCopyData
        //{
        //    get
        //    {
        //        if (data.Count > 0)
        //        {
        //            isHaveCopyData = true;
        //        }
        //        else
        //        {
        //            isHaveCopyData = false;
        //        }

        //        return isHaveCopyData;
        //    }
        //    set
        //    {
        //        isHaveCopyData = value;
        //    }
        //}

        /// <summary>
        /// ������ˮ��
        /// </summary>
        public static string ClinicCode = "";
        #endregion

        private static void GetContent()
        {
            data.Clear();
            List<object> objdata = clipboard.Paste() as List<object>;
            if ((objdata != null) && (objdata.Count > 0))
            {
                for (int count = 0; count < objdata.Count; count++)
                {
                    data.Add(objdata[count].ToString());
                }
            }
            isReaded = true;

            IsHaveCopyData = true;
        }

        /// <summary>
        /// �Ӽ�������ҽ��ID�б�
        /// </summary>
        public static List<string> OrderList
        {
            get
            {
                if (!isReaded)
                {
                    GetContent();
                }
                if (data.Count <= 0)
                {
                    return null;
                }
                string[] array = new string[data.Count - 1];
                data.CopyTo(0, array, 0, array.Length);
                data.CopyTo(0, array, 0, array.Length);
                List<string> list = new List<string>(array);
                type = (ServiceTypes)Convert.ToInt32(data[data.Count - 1]);
                data.Clear();

                IsHaveCopyData = false;

                return list;
            }
        }

        private static FS.HISFC.Models.Base.ServiceTypes type = ServiceTypes.I;

        /// <summary>
        /// ��ʶ���ﻹ��סԺ
        /// </summary>
        public static FS.HISFC.Models.Base.ServiceTypes Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }


        static Clipboard clipboard = new Clipboard();

        /// <summary>
        /// ִ����ӣ�������
        /// </summary>
        /// <param name="instance"></param>
        public static void Add(object instance)
        {
            clipboard.Add(instance);
        }

        /// <summary>
        /// ִ�и���
        /// </summary>
        public static void Copy()
        {
            clipboard.Copy();
            isReaded = false;
            IsHaveCopyData = true;
        }
    }

    #endregion
}