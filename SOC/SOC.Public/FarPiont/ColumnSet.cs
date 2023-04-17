using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.FarPoint
{

    /// <summary>
    /// [功能描述: 列集合]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// </summary>
    public class ColumnSet
    {
        Column[] columns;

        /// <summary>
        /// 集合元素个数
        /// </summary>
        public int Count
        {
            get 
            {
                if (columns == null)
                {
                    throw new Exception("集合为null");
                }
                return columns.GetLength(0);
            }
        }

        /// <summary>
        /// 添加列元素
        /// </summary>
        /// <param name="columns">列</param>
        public void AddColumn(Column[] columns)
        {
            if (columns == null)
            {
                throw new Exception("无法将null加入集合");
            }
            this.columns = columns;
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="index">列索引</param>
        /// <param name="width">宽</param>
        public void SetColumnWith(int index, float width)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            columns[index].Width = width;
        }

        /// <summary>
        /// 设置列名称
        /// </summary>
        /// <param name="index">列索引</param>
        /// <param name="name">名称</param>
        public void SetColumnName(int index, string name)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            columns[index].Name = name;
        }

        /// <summary>
        /// 设置列tag
        /// </summary>
        /// <param name="index">列索引</param>
        /// <param name="tag">tag</param>
        public void SetColumnTag(int index, object tag)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            columns[index].Tag = tag;
        }

        /// <summary>
        /// 设置列可见性
        /// </summary>
        /// <param name="index">列索引</param>
        /// <param name="visible">是否可见</param>
        public void SetColumnVisible(int index, bool visible)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            columns[index].Visible = visible;
        }

        /// <summary>
        /// 设置列可见性
        /// </summary>
        /// <param name="index">列索引</param>
        /// <param name="locked">是否锁定</param>
        public void SetColumnLocked(int index, bool locked)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            columns[index].Locked = locked;
        }

        /// <summary>
        /// 设置列
        /// </summary>
        /// <param name="column">列</param>
        public void SetColumn(int index, Column column)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            columns[index] = column;
        }

        /// <summary>
        /// 根据列索引获取列名称
        /// </summary>
        /// <param name="name">列索引</param>
        /// <returns>列名称</returns>
        public string GetColumnName(int index)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            return columns[index].Name;
        }

        /// <summary>
        /// 根据列索引获取列宽度
        /// </summary>
        /// <param name="name">列索引</param>
        /// <returns>列宽度</returns>
        public float GetColumnWidth(int index)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            return columns[index].Width;
        }

        /// <summary>
        /// 根据列名称获取列宽度
        /// </summary>
        /// <param name="name">列名称</param>
        /// <returns>列宽度</returns>
        public float GetColumnWidth(string name)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index].Width;
                }
            }
            throw new Exception("无效列名称");
        }

        /// <summary>
        /// 根据列索引获取列可见性
        /// </summary>
        /// <param name="name">列索引</param>
        /// <returns>可见性</returns>
        public bool GetColumnVisible(int index)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            return columns[index].Visible;
        }

        /// <summary>
        /// 根据列名称获取列可见性
        /// </summary>
        /// <param name="name">列名称</param>
        /// <returns>可见性</returns>
        public bool GetColumnVisible(string name)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index].Visible;
                }
            }
            throw new Exception("无效列名称");
        }

        /// <summary>
        /// 根据列索引获取列是否锁定
        /// </summary>
        /// <param name="name">列索引</param>
        /// <returns>是否锁定</returns>
        public bool GetColumnLocked(int index)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            return columns[index].Locked;
        }

        /// <summary>
        /// 根据列名称获取列是否锁定
        /// </summary>
        /// <param name="name">列名称</param>
        /// <returns>是否锁定</returns>
        public bool GetColumnLocked(string name)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index].Locked;
                }
            }
            throw new Exception("无效列名称");
        }

        /// <summary>
        /// 根据列索引获取列Tag
        /// </summary>
        /// <param name="name">列索引</param>
        /// <returns>列Tag</returns>
        public object GetColumnTag(int index)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            return columns[index].Tag;
        }

        /// <summary>
        /// 根据列名称获取列Tag
        /// </summary>
        /// <param name="name">列名称</param>
        /// <returns>列Tag</returns>
        public object GetColumnTag(string name)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index].Tag;
                }
            }
            throw new Exception("无效列名称");
        }

        /// <summary>
        /// 根据列名称获取列索引
        /// </summary>
        /// <param name="name">列名称</param>
        /// <returns>列</returns>
        public Column GetColumn(string name)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            for (int index = 0; index < Count; index++)
            {
                if (columns[index].Name == name)
                {
                    return columns[index];
                }
            }
            throw new Exception("无效列名称");
        }

        /// <summary>
        /// 获取列
        /// </summary>
        /// <param name="index">列索引</param>
        /// <returns>列</returns>
        public Column GetColumn(int index)
        {
            if (columns == null)
            {
                throw new Exception("集合为null");
            }
            if (index < 0 || index > Count - 1)
            {
                throw new Exception("索引出界,不能为:" + index.ToString() + ",应该介于0和" + (columns.GetLength(0) - 1).ToString() + "间");
            }
            return columns[index];
        }
    }
}
