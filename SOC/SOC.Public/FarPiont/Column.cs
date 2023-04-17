using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.FarPoint
{
    /// <summary>
    /// [功能描述: 列定义]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// </summary>
    public struct Column
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">列名称</param>
        /// <param name="with">列宽</param>
        public Column(string name, float width)
        {
            Name = name;
            Width = width;
            Visible = true;
            Locked = false;
            Tag = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">列名称</param>
        /// <param name="with">列宽</param>
        /// <param name="tag">object</param>
        public Column(string name, float width, object tag)
        {
            Name = name;
            Width = width;
            Visible = true;
            Locked = false; 
            Tag = tag;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">列名称</param>
        /// <param name="with">列宽</param>
        /// <param name="visible">可见性</param>
        /// <param name="locked">锁定</param>
        /// <param name="tag">object</param>
        public Column(string name, float width, bool visible, bool locked, object tag)
        {
            Name = name;
            Width = width;
            Visible = visible;
            Locked = locked;
            Tag = tag;
        }

        public string Name;
        public float Width;
        public object Tag;
        public bool Visible;
        public bool Locked;

    }

}
