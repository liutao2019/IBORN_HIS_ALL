using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Local.PacsInterface.ZDLY.IPacsResult
{
    /// <summary>
    /// 中大六院检查结果查询
    /// </summary>
    public partial class frmPacsResult : Form, FS.HISFC.BizProcess.Interface.Common.IPacs
    {
        public frmPacsResult()
        {
            InitializeComponent();
        }

        #region IPacs 成员

        public bool CheckOrder(FS.HISFC.Models.Order.Order Order)
        {
            return true;
        }

        public int Commit()
        {
            return 1;
        }

        public int Connect()
        {
            return 1;
        }

        public int Disconnect()
        {
            return 1;
        }

        string resultType = "";
        public string ResultType
        {
            get
            {
                return resultType;
            }
            set
            {
                resultType = value;
            }
        }

        string errCode = "";

        string errMsg = "";

        public string ErrCode
        {
            get
            {
                return errCode;
            }
            set
            {
                errCode = value;
            }
        }

        public string ErrMsg
        {
            get
            {
                return errMsg;
            }
            set
            {
                errMsg = value;
            }
        }

        public bool IsReportValid(string id)
        {
            return true;
        }

        /// <summary>
        /// 患者编号类型 ， 1 门诊 ， 2 住院 ， 3 处方号 ， 4社保号
        /// </summary>
        string operationMode = "1";

        /// <summary>
        /// 患者编号类型 ， 1 门诊 ， 2 住院 ， 3 处方号 ， 4社保号
        /// </summary>
        public string OprationMode
        {
            get
            {
                return operationMode;
            }
            set
            {
                operationMode = value;
            }
        }

        /// <summary>
        /// 图像类型， 1 图像 ， 2 报告
        /// </summary>
        string pacsViewType = "2";

        /// <summary>
        /// 图像类型， 1 图像 ， 2 报告
        /// </summary>
        public string PacsViewType
        {
            get
            {
                return pacsViewType;
            }
            set
            {
                pacsViewType = value;
            }
        }

        public int PlaceOrder(List<FS.HISFC.Models.Order.Order> OrderList)
        {
            return 1;
        }

        public int PlaceOrder(FS.HISFC.Models.Order.Order Order)
        {
            return 1;
        }

        public string[] QueryResult()
        {
            return null;
        }

        public int Rollback()
        {
            return 1;
        }

        /// <summary>
        /// 当前患者
        /// </summary>
        FS.HISFC.Models.RADT.Patient myPatient = null;

        public int SetPatient(FS.HISFC.Models.RADT.Patient Patient)
        {
            myPatient = Patient;
            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            return;
        }

        public void ShowForm()
        {
            return;
        }

        public int ShowResult(string id)
        {
            return 1;
        }

        public int ShowResultByPatient()
        {
            if (myPatient == null)
            {
                errMsg = "患者信息为空！";
                return 0;
            }

            return 1;
        }

        #endregion

    }
}
