using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
namespace Neusoft.UFC.Privilege.Common
{
    /// <summary>
    /// [��������: ���ര���õ�ToolBar������]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class PrivilegelBar
    {
        public PrivilegelBar()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        protected List<ToolStripItem> toolButtons = new List<ToolStripItem>();

        /// <summary>
        /// ���ToolButton
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tooltip"></param>
        /// <param name="imageIndex"></param>
        /// <param name="e"></param>
        public void AddToolButton(string text, string tooltip, Image image, bool enabled, bool isChecked, System.EventHandler e)
        {

            ToolStripButton tb = new ToolStripButton(text);
            tb.Tag = e;
            tb.Enabled = enabled;
            tb.Checked = isChecked;         //Robin Add
            tb.ToolTipText = tooltip;
            tb.Image = image;
            tb.ImageScaling = ToolStripItemImageScaling.SizeToFit;   //Robin Add
            this.toolButtons.Add(tb);

        }

        /// <summary>
        /// ���ӷָ���
        /// </summary>
        public void AddToolSeparator()
        {
            ToolStripSeparator _sp = new ToolStripSeparator();
            this.toolButtons.Add(_sp);
        }

        /// <summary>
        /// ���ToolButton
        /// </summary>
        public void Clear()
        {
            this.toolButtons.Clear();
        }

        /// <summary>
        /// ����toolButton��ť�ɲ�����
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enabled"></param>
        public void SetToolButtonEnabled(string text, bool enabled)
        {
            foreach (ToolStripItem _item in this.toolButtons)
            {
                if (_item.GetType() == typeof(ToolStripButton) && _item.Text == text)
                {
                    _item.Enabled = enabled;
                    break;
                }
            }
        }

        /// <summary>
        /// �������ToolButton
        /// </summary>
        /// <returns></returns>
        public IList<ToolStripItem> GetToolButtons()
        {
            return this.toolButtons;
        }

        /// <summary>
        /// �������ToolButton
        /// </summary>
        /// <returns></returns>
        public ToolStripItem[] GetToolStripButtons()
        {
            ToolStripItem[] _collect = new ToolStripItem[toolButtons.Count];

            for (int i = 0; i < toolButtons.Count; i++)
            {
                _collect[i] = toolButtons[i];
            }

            return _collect;
        }
        /// <summary>
        /// �������ƻ�ȡBotton
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public System.Windows.Forms.ToolStripButton GetToolButton(string text)
        {
            foreach (ToolStripItem _item in this.toolButtons)
            {
                if (_item.GetType() == typeof(ToolStripButton) && _item.Text == text)
                    return _item as ToolStripButton;
            }

            return null;
        }
    }
}
