using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.Core
{
    public class PatientReport
    {

        public string FamilyName { get; set; }

        public string GivenName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        public DateTime TestTime { get; set; }

        public string SampleNo { get; set; }

        public virtual bool IsUrgent { get; set; }

        public string TestType { get; set; }

        public string CompanyShortName { get; set; }

        public string FicilityID { get; set; }

        public virtual string QCidentifier { get; set; }

        public Dictionary<string, Indicator> IndicatorDic { get; set; }
    }
}
