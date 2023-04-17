using System;

namespace FS.HISFC.Models.Board
{
	/// <summary>
	/// Item ��ժҪ˵����
	/// ��ʳ��Ŀ��Ϣʵ��
	/// </summary>
    /// 
    [System.Serializable]
	public class Item:FS.HISFC.Models.Base.Item
	{
        /// <summary>
        /// ִ�п���
        /// </summary>
        private FS.FrameWork.Models.NeuObject executeDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ִ�п���
        /// </summary>
        public FS.FrameWork.Models.NeuObject ExecuteDept
        {
            get
            {
                return this.executeDept;
            }
            set
            {
                this.executeDept = value;
            }
        }


		public Item()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        /// <summary>
        /// ���ƶ���
        /// </summary>
        /// <returns>ʵ��</returns>
        public new Item Clone()
        {
            Item item = base.Clone() as Item;
            item.executeDept = this.executeDept.Clone();

            return item;
        }
	}
}
