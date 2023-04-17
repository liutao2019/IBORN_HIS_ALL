using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Nurse.Controls.SDFY
{
    /// <summary>
    /// ������в�ѯ
    /// </summary>
    internal partial class ucQueueQuery : UserControl
    {
        public ucQueueQuery()
        {
            InitializeComponent();
        }

        public ucQueueQuery(FS.FrameWork.Models.NeuObject obj)
            : this()
        {
            if (obj == null) return;
            this.btnRefresh.Tag = obj;
        }

        #region ����

        private FS.FrameWork.Models.NeuObject objNurse = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 
        /// </summary>
        public FS.FrameWork.Models.NeuObject ObjNurse
        {
            get
            {
                return this.objNurse;
            }
            set
            {
                this.objNurse = value;
            }
        }

        private ArrayList queueInfo = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        public ArrayList QueueInfo
        {
            get
            {
                return this.queueInfo;
            }
            set
            {
                this.queueInfo = value;
            }
        }

        #endregion

        #region  ������

        private FS.HISFC.BizLogic.Nurse.Queue myQueue = new FS.HISFC.BizLogic.Nurse.Queue();

        private Hashtable htNoon = new Hashtable();
        private Hashtable htDoct = new Hashtable();

        public delegate void RefQueue(ArrayList alQueue);
        public event RefQueue RefList;

        private FS.HISFC.BizProcess.Integrate.Registration.Registration  myMgr = null;
        private FS.HISFC.BizProcess.Integrate.Manager personMgr = null;

        #endregion


        /// <summary>
        /// ˢ���б�
        /// </summary>
        /// <param name="person"></param>
        private void RefreshList(FS.FrameWork.Models.NeuObject nurse)
        {
            try
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);

                //����������ά����Ϣ
                this.QueueInfo = this.myQueue.Query(nurse.ID, this.dtDate.Value.ToShortDateString());

                this.neuSpread1_Sheet1.Tag = nurse;

                if (QueueInfo != null)
                {
                    foreach (FS.HISFC.Models.Nurse.Queue obj in QueueInfo)
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                        int row = this.neuSpread1_Sheet1.RowCount - 1;
                        this.neuSpread1_Sheet1.Rows[row].Tag = obj;

                        //��������
                        this.neuSpread1_Sheet1.SetValue(row, 0, obj.Name, false);
                        //���
                        obj.Noon.Name = this.GetNoonNameByID(obj.Noon.ID);
                        this.neuSpread1_Sheet1.SetValue(row, 1, obj.Noon.Name, false);
                        //��ʾ˳��
                        this.neuSpread1_Sheet1.SetValue(row, 2, obj.Order, false);
                        //�Ƿ���Ч
                        this.neuSpread1_Sheet1.SetValue(row, 3, obj.IsValid ? "��Ч" : "��Ч", false);
                        //��������
                        this.neuSpread1_Sheet1.SetValue(row, 4, obj.QueueDate, false);
                        //����ҽ��
                        obj.Doctor.Name = this.GetDoctNameByID(obj.Doctor.ID);
                        this.neuSpread1_Sheet1.SetValue(row, 5, obj.Doctor.Name, false);
                        //����
                        this.neuSpread1_Sheet1.SetValue(row, 6, obj.SRoom.Name, false);
                        //��ע
                        this.neuSpread1_Sheet1.SetValue(row, 7, obj.Memo, false);
                        //����Ա
                        this.neuSpread1_Sheet1.SetValue(row, 8, obj.Oper.ID, false);
                        //����ʱ��
                        this.neuSpread1_Sheet1.SetValue(row, 9, this.myQueue.GetDateTimeFromSysDateTime(), false);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + this.myQueue.Err);
            }
        }
        private void GetQueue()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.Rows[i].Tag == null)
                    return;
                FS.HISFC.Models.Nurse.Queue obj = new FS.HISFC.Models.Nurse.Queue();
                obj = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Nurse.Queue;
                this.QueueInfo.Add(obj);
            }
        }

        private void Init()
        {
            ArrayList al = new ArrayList();
            if (this.myMgr == null) this.myMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            al = this.myMgr.Query();
            foreach (FS.HISFC.Models.Registration.Noon noon in al)
            {
                this.htNoon.Add(noon.ID, noon.Name);
            }

            if (this.personMgr == null) this.personMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //�õ�ҽ���б�
            al = new ArrayList();
            al = this.personMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            foreach (FS.HISFC.Models.Base.Employee person in al)
            {
                this.htDoct.Add(person.ID, person.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetNoonNameByID(string ID)
        {
            IDictionaryEnumerator dict = this.htNoon.GetEnumerator();
            while (dict.MoveNext())
            {
                if (dict.Key.ToString() == ID)
                    return dict.Value.ToString();
            }
            return ID;
        }

        private string GetDoctNameByID(string ID)
        {
            IDictionaryEnumerator dict = this.htDoct.GetEnumerator();
            while (dict.MoveNext())
            {
                if (dict.Key.ToString() == ID)
                    return dict.Value.ToString();
            }
            return ID;
        }

        private void ucQueueQuery_Load(object sender, EventArgs e)
        {
            this.Init();
            this.FindForm().Text = "ģ��";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.btnRefresh.Tag == null)
                return;
            this.ObjNurse = this.btnRefresh.Tag as FS.FrameWork.Models.NeuObject;
            this.RefreshList(ObjNurse);
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.RefList(this.QueueInfo);
            this.FindForm().Close();
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

    }
}
