using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.FarPoint
{
    /// <summary>
    /// [��������: �ж���]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-9]<br></br>
    /// </summary>
    public struct Column
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="with">�п�</param>
        public Column(string name, float width)
        {
            Name = name;
            Width = width;
            Visible = true;
            Locked = false;
            Tag = null;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="with">�п�</param>
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
        /// ���캯��
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="with">�п�</param>
        /// <param name="visible">�ɼ���</param>
        /// <param name="locked">����</param>
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
