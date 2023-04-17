using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    public class Index1
    {
        private int[] myArray = new int[100];
        public int this[int x]
        {
            get
            {
                if (x < 0 || x >= 100)
                    return 0;
                else
                    return myArray[x];
            }
            set
            {
                if (!(x < 0 || x >= 100))
                    myArray[x] = value;

            }
        }
    }

    public class IndexerClass1
    {
        private int[,] myArray = new int[100, 2];
        public int this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= 100 || y < 0 || y >= 2)
                    return 0;
                else
                    return myArray[x, y];
            }
            set
            {
                if (!(x < 0 || x >= 100 || y < 0 || y >= 2))
                    myArray[x, y] = value;
            }
        }
    }

    public class CNode1
    {
        ArrayList myAL = new ArrayList();
        public object data;
        public CNode next;
        public CNode1()
        {
            next = null;
        }

    }

    public class QueueCnode1
    {
        public QueueCnode1()
        {
        }

        public void Insert(object data, int i)
        {
            //����X//
            //�޸ı�//

            if (i >= 100)
                MessageBox.Show("�������鳤��");//('����');
            if (i < 1 || i > 100)
                MessageBox.Show("�Ƿ�λ��"); //('�Ƿ�λ��');


        }
        public void Remove(object data, int i)
        {

        }

    }
}
