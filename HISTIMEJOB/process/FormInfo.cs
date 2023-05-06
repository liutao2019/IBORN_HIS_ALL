using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OADAL
{
    public class FormInfo
    {
        public int count { set; get; }

        public string definitionVersionsId { set; get; }

        public string formName { set; get; }

        public string personName { set; get; }

        public string typeName { set; get; }

        public List<versionInfo> versionDefinition { set; get; }

        public int versions { set; get; }


    }

    public class versionInfo
    {
        public int count { set; get; }

        public string createdTime { set; get; }

        public string endTime { set; get; }

        public string ID { set; get; }

        public string versions { set; get; }
    }
}
