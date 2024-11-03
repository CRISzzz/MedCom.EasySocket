using MedCom.EasySocket.Core;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V23.Datatype;
using NHapi.Model.V23.Group;
using NHapi.Model.V23.Message;
using NHapi.Model.V23.Segment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCom.EasySocket.HL7.HandlersV23
{
    public class ORU_R01_HL7PkgHandler: MsgSender
    {
        public override string CreateMessage(PatientReport report)
        {
            if (report == null || report.IndicatorDic.Count == 0)
                throw new ArgumentNullException("No test result");
            ORU_R01 oruR01 = new ORU_R01();
            ORU_R01_ORDER_OBSERVATION orderObservation = oruR01.GetRESPONSE().GetORDER_OBSERVATION();

            oruR01.MSH.FieldSeparator.Value = "|";
            oruR01.MSH.EncodingCharacters.Value = @"^~\&";
            oruR01.MSH.SendingApplication.NamespaceID.Value = report.CompanyShortName;
            oruR01.MSH.SendingFacility.NamespaceID.Value = report.FicilityID;
            oruR01.MSH.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(DateTime.Now);
            oruR01.MSH.ProcessingID.ProcessingID.Value = "P";
            oruR01.MSH.VersionID.Value = "2.3";

            PID pid = oruR01.GetRESPONSE().PATIENT.PID;
            pid.SetIDPatientID.Value = "1";
            pid.DateOfBirth.TimeOfAnEvent.SetShortDate(report.BirthDate);
            pid.Sex.Value = report.Gender;
            var patient = pid.GetPatientName(0);
            patient.FamilyName.Value = report.FamilyName;
            patient.GivenName.Value = report.GivenName;

            OBR obr = orderObservation.OBR;
            obr.PlacerOrderNumber.UniversalID.Value = report.SampleNo;
            obr.ObservationEndDateTime.TimeOfAnEvent.SetLongDateWithSecond(report.TestTime);

            int index = 0;
            foreach (KeyValuePair<string, Indicator> keyValue in report.IndicatorDic)
            {
                ORU_R01_OBSERVATION observation = orderObservation.GetOBSERVATION(index);
                index++;
                OBX obx = observation.OBX;
                obx.SetIDOBX.Value = index.ToString();
                obx.ValueType.Value = keyValue.Value.ValueType.ToString();
                if (keyValue.Value.IsQC)
                    obx.ObservationIdentifier.Identifier.Value = report.QCidentifier;
                obx.ObservationSubID.Value = keyValue.Key;
                CE ce = new CE(oruR01);
                ce.Identifier.Value = keyValue.Value.Value;
                Varies varies = obx.GetObservationValue(0);
                varies.Data = ce;
                obx.Units.Identifier.Value = keyValue.Value.Unit;
            }
            PipeParser parser = new PipeParser();
            string encodedMessage = parser.Encode(oruR01);
            return encodedMessage;
        }
    }
}
