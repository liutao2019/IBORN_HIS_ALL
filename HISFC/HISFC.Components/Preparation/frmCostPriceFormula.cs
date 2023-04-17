using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PManager = FS.HISFC.BizLogic.Pharmacy.Preparation;
using PObject = FS.HISFC.Models.Preparation;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.Preparation
{
    /// <summary>
    /// <br></br>
    /// [��������: �ɱ��ۼ��㹫ʽ]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-04]<br></br>
    /// </summary>
    public partial class frmCostPriceFormula : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmCostPriceFormula ( )
        {
            InitializeComponent ( );
            this.ShowList ( );
        }
        #region ����
       
        /// <summary>
        /// ���ڵ�ʵ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject neuObject;
        /// <summary>
        /// ���ڵ�ʵ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject NeuObject
        {
            get
            {
                neuObject = this.tvDrugList1.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
                return neuObject;
            }
            set
            {
                neuObject = value;
            }
        }
        
        #endregion


        #region ����
        /// <summary>
        /// ���б����
        /// </summary>
        /// <returns></returns>
        protected void ShowList ( )
        {
            this.tvDrugList1.ShowDrugList ( );

            
        }
        #endregion

        #region �¼�
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click ( object sender , EventArgs e )
        {
            //this.ucCostPriceFormula1.OnSave ( NeuObject.ID );
        }
        /// <summary>
        /// �ǽڵ�仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDrugList1_AfterSelect ( object sender , TreeViewEventArgs e )
        {
            if ( e.Node.Level != 1 )
            {
                return;
            }

            this.ucCostPriceFormula1.ShowPrescription ( NeuObject.ID );
        }
        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click ( object sender , EventArgs e )
        {
            this.Close ( );
        }
        #endregion
        
       
    }
}