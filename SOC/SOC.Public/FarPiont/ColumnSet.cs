using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.FarPoint
{

    /// <summary>
    /// [��������: �м���]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-9]<br></br>
    /// </summary>
    public class ColumnSet
    {
        Column[] columns;

        /// <summary>
        /// ����Ԫ�ظ���
        /// </summary>
        public int Count
        {
            get 
            {
                if (columns == null)
                {
                    throw new Exception("����Ϊnull");
                }
                return columns.GetLength(0);
            }
        }

        /// <summary>
        /// �����Ԫ��
        /// </summary>
        /// <param name="columns">��</param>
        public void AddColumn(Column[] columns)
        {
            if (columns == null)
            {
                throw new Exception("�޷���null���뼯��");
            }
            this.columns = columns;
        }

        /// <summary>
        /// �����п�
        /// </summary>
        /// <param name="index">������</param>
        /// <param name="width">��</param>
        public void SetColumnWith(int index, float width)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            columns[index].Width = width;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="index">������</param>
        /// <param name="name">����</param>
        public void SetColumnName(int index, string name)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            columns[index].Name = name;
        }

        /// <summary>
        /// ������tag
        /// </summary>
        /// <param name="index">������</param>
        /// <param name="tag">tag</param>
        public void SetColumnTag(int index, object tag)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            columns[index].Tag = tag;
        }

        /// <summary>
        /// �����пɼ���
        /// </summary>
        /// <param name="index">������</param>
        /// <param name="visible">�Ƿ�ɼ�</param>
        public void SetColumnVisible(int index, bool visible)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            columns[index].Visible = visible;
        }

        /// <summary>
        /// �����пɼ���
        /// </summary>
        /// <param name="index">������</param>
        /// <param name="locked">�Ƿ�����</param>
        public void SetColumnLocked(int index, bool locked)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            columns[index].Locked = locked;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="column">��</param>
        public void SetColumn(int index, Column column)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            columns[index] = column;
        }

        /// <summary>
        /// ������������ȡ������
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>������</returns>
        public string GetColumnName(int index)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            return columns[index].Name;
        }

        /// <summary>
        /// ������������ȡ�п��
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>�п��</returns>
        public float GetColumnWidth(int index)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            return columns[index].Width;
        }

        /// <summary>
        /// ���������ƻ�ȡ�п��
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>�п��</returns>
        public float GetColumnWidth(string name)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index].Width;
                }
            }
            throw new Exception("��Ч������");
        }

        /// <summary>
        /// ������������ȡ�пɼ���
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>�ɼ���</returns>
        public bool GetColumnVisible(int index)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            return columns[index].Visible;
        }

        /// <summary>
        /// ���������ƻ�ȡ�пɼ���
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>�ɼ���</returns>
        public bool GetColumnVisible(string name)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index].Visible;
                }
            }
            throw new Exception("��Ч������");
        }

        /// <summary>
        /// ������������ȡ���Ƿ�����
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>�Ƿ�����</returns>
        public bool GetColumnLocked(int index)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            return columns[index].Locked;
        }

        /// <summary>
        /// ���������ƻ�ȡ���Ƿ�����
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>�Ƿ�����</returns>
        public bool GetColumnLocked(string name)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index].Locked;
                }
            }
            throw new Exception("��Ч������");
        }

        /// <summary>
        /// ������������ȡ��Tag
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>��Tag</returns>
        public object GetColumnTag(int index)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            return columns[index].Tag;
        }

        /// <summary>
        /// ���������ƻ�ȡ��Tag
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>��Tag</returns>
        public object GetColumnTag(string name)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index].Tag;
                }
            }
            throw new Exception("��Ч������");
        }

        /// <summary>
        /// ���������ƻ�ȡ������
        /// </summary>
        /// <param name="name">������</param>
        /// <returns>��</returns>
        public Column GetColumn(string name)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index];
                }
            }
            throw new Exception("��Ч������");
        }

        /// <summary>
        /// ��ȡ��
        /// </summary>
        /// <param name="index">������</param>
        /// <returns>��</returns>
        public Column GetColumn(int index)
        {
            if (columns == null)
            {
                throw new Exception("����Ϊnull");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("��������,����Ϊ:" + index.ToString() + ",Ӧ�ý���0��" + (columns.GetLength(0) - 1).ToString() + "��");
            }
            return columns[index];
        }
    }
}
