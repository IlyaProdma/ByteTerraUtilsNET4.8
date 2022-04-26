using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ByteTerraUtils
{
    public class DocumentUtils
    {
        private GoogleUtils _GoogleUtils;

        private enum ProtocolType
        {
            AdmissionProtocol,
            StandardMonitorProtocol,
            NonStandardMonitorProtocol
        }

        public DocumentUtils()
        {
            _GoogleUtils = null;
        }

        public DocumentUtils(string apiKey, string applicationName)
        {
            _GoogleUtils = new GoogleUtils(apiKey, applicationName);
        }

        public DocumentUtils(string serviceAccountEmail, string jsonfile, string applicationName)
        {
            _GoogleUtils = new GoogleUtils(serviceAccountEmail, jsonfile, applicationName);
        }

        private string CreatePDF(string jsonName, string template, string outputName)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C cd createdoc && python createdoc.py {jsonName} {template} {Directory.GetCurrentDirectory()}/docs/{outputName}";
            startInfo.UseShellExecute = true;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process = new Process();
            startInfo.Arguments = $"/C latexmk -cd {Directory.GetCurrentDirectory()}/docs/{outputName}.tex -xelatex -halt-on-error -synctex=0 -interaction=nonstopmode";// {outputName}.tex";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process = new Process();
            startInfo.Arguments = $"/C latexmk -c -cd {Directory.GetCurrentDirectory()}/docs/{outputName}.tex && cd docs && erase {outputName}.tex";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            return $"{Directory.GetCurrentDirectory()}/docs/{outputName}.pdf";
        }

        public string CreateAdmissionReport(string spreadSheetId, int sessionId)
        {
            if (_GoogleUtils == null)
            {
                throw new InvalidOperationException("No google account");
            }
            List<Object> dataRow = _GoogleUtils.GetRowBySessionNumber(spreadSheetId, "Data", sessionId);
            List<Object> timingRow = _GoogleUtils.GetRowBySessionNumber(spreadSheetId, "Timing", sessionId);
            if (dataRow.Count == 0 || timingRow.Count == 0)
            {
                throw new InvalidOperationException("No such sessionId");
            }
            List<Object> ionInfo = _GoogleUtils.GetIonInfoByEnvCode(spreadSheetId, dataRow[6].ToString());
            return CreateAdmissionReport(dataRow, timingRow, ionInfo);
        }

        public string CreateAdmissionReport(List<Object> dataRow, List<Object> timingRow, List<Object> ionInfo)
        {
            if (dataRow.Count == 0 || timingRow.Count == 0)
            {
                throw new InvalidOperationException("No such sessionId");
            }
            Session session;
            Ion ion;
            try
            {
                session = new Session(dataRow, timingRow);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("Not enough info");
            }
            try
            {
                ion = new Ion(ionInfo);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("Not enough info");
            }
            if (!CanCreateProtocol(session, ProtocolType.AdmissionProtocol))
            {
                throw new InvalidOperationException("Not enough info");
            }
            AdmProtocolInfo protocolInfo = new AdmProtocolInfo(session, ion);
            string jsonString = JsonConvert.SerializeObject(protocolInfo, Formatting.Indented);
            File.WriteAllText("createdoc/temp.json", jsonString);
            return CreatePDF("temp.json", "dopusk.template", $"admission_{session.SessionId}");
        }

        public string CreateMonitorReport(string spreadSheetId, int sessionId)
        {
            if (_GoogleUtils == null)
            {
                throw new InvalidOperationException("No google account");
            }
            List<Object> dataRow = _GoogleUtils.GetRowBySessionNumber(spreadSheetId, "Data", sessionId);
            List<Object> timingRow = _GoogleUtils.GetRowBySessionNumber(spreadSheetId, "Timing", sessionId);
            if (dataRow.Count == 0 || timingRow.Count == 0)
            {
                throw new InvalidOperationException("No such sessionId");
            }
            List<Object> ionInfo = _GoogleUtils.GetIonInfoByEnvCode(spreadSheetId, dataRow[6].ToString());
            return CreateMonitorReport(dataRow, timingRow, ionInfo);
        }

        public string CreateMonitorReport(List<Object> dataRow, List<Object> timingRow, List<Object> ionInfo)
        {
            if (dataRow.Count == 0 || timingRow.Count == 0)
            {
                throw new InvalidOperationException("No such sessionId");
            }
            Session session;
            Ion ion;
            try
            {
                session = new Session(dataRow, timingRow);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("Not enough info");
            }
            try
            {
                ion = new Ion(ionInfo);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidOperationException("Not enough info");
            }
            if (!CanCreateProtocol(session, ProtocolType.StandardMonitorProtocol) &&
                !CanCreateProtocol(session, ProtocolType.NonStandardMonitorProtocol))
            {
                throw new InvalidOperationException("Not enough info");
            }
            if (session.TrackDetectors.Count <= 2)
            {
                return CreateStandardMonitorReport(session, ion);
            }
            else
            {
                return CreateNonStandardMonitorReport(session, ion);
            }
        }

        private string CreateStandardMonitorReport(Session session, Ion ion)
        {
            StandardMonitorProtocol protocolInfo = new StandardMonitorProtocol(session, ion);
            string jsonString = JsonConvert.SerializeObject(protocolInfo, Formatting.Indented);
            File.WriteAllText("createdoc/temp.json", jsonString);
            return CreatePDF("temp.json", "monitoring.template", $"monitoring_{session.SessionId}");
        }

        private string CreateNonStandardMonitorReport(Session session, Ion ion)
        {
            NonStandardMonitorProtocol protocolInfo = new NonStandardMonitorProtocol(session, ion);
            string jsonString = JsonConvert.SerializeObject(protocolInfo, Formatting.Indented);
            File.WriteAllText("createdoc/temp.json", jsonString);
            return CreatePDF("temp.json", "monitoring_nonstandard.template", $"monitoring_{session.SessionId}");
        }

        private bool CanCreateProtocol(Session session, ProtocolType protocolType)
        {
            if (session.OnlineDetectors.Contains(-1) || session.TrackDetectors.Count() == 0 ||
                session.Kcoeff == -1 || session.Error == -1)
            {
                return false;
            }
            if ((protocolType == ProtocolType.StandardMonitorProtocol || protocolType == ProtocolType.AdmissionProtocol) &&
                session.Heterogeneity == -1)
            {
                return false;
            }
            else if (protocolType == ProtocolType.NonStandardMonitorProtocol &&
              (session.HeterogeneityLeft == -1 || session.HeterogeneityRight == -1))
            {
                return false;
            }
            return true;
        }
    }
}
