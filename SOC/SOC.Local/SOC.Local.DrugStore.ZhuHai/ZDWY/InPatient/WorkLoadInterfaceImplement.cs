using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.InPatient
{
    /// <summary>
    /// [功能描述: 住院药房工作量本地化实现]<br></br>
    /// [创 建 者: cao-lin]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// </summary>
    public class WorkLoadInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientWorkLoad
    {
        #region IInpatientWorkLoad 成员

        public string Init(FS.FrameWork.Models.NeuObject dept, string type, FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            return string.Empty;
        }

        public string Set(FS.FrameWork.Models.NeuObject dept, string type, FS.FrameWork.Models.NeuObject drugBillClass, System.Collections.ArrayList alApplyOutData)
        {
            SOC.Local.DrugStore.ZhuHai.ZDWY.InPatient.frmWorkLoad frmWorkLoad = new frmWorkLoad(dept.ID, dept.ID, drugBillClass.Memo, drugBillClass, alApplyOutData.Count);
            frmWorkLoad.ShowDialog();
            frmWorkLoad.TopMost = true;
            return "1";
        }

        #endregion
    }
}
