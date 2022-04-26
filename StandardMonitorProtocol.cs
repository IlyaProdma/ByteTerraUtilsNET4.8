using Newtonsoft.Json;
using System;

namespace ByteTerraUtils
{
    internal class StandardMonitorProtocol
    {
        [JsonProperty("isotope_header")]
        
        public string IsotopeHeader { get; set; }

        [JsonProperty("seans")]
        
        public string SessionId { get; set; }

        [JsonProperty("test_stand")]
        
        public string TestStand { get; set; }

        [JsonProperty("organization")]
        
        public string Organization { get; set; }

        [JsonProperty("cipher")]
        
        public string Cipher { get; set; }

        [JsonProperty("irradiated_item")]
        
        public string IrradiatedItem { get; set; }

        [JsonProperty("begin_date")]
        
        public string BeginDate { get; set; }

        [JsonProperty("duration")]
        
        public string Duration { get; set; }

        [JsonProperty("angle")]
        
        public string Angle { get; set; }

        [JsonProperty("temperature")]
        
        public string Temperature { get; set; }

        [JsonProperty("degrader_material")]
        
        public string DegraderMaterial { get; set; }

        [JsonProperty("thickness")]
        
        public string Thickness { get; set; }

        [JsonProperty("element_name")]
        
        public string ElementName { get; set; }

        [JsonProperty("atomic_number")]
        
        public string AtomicNumber { get; set; }

        [JsonProperty("E")]
        
        public string Energy { get; set; }

        [JsonProperty("E_error")]
        
        public string EnergyError { get; set; }

        [JsonProperty("R")]
        
        public string Run { get; set; }

        [JsonProperty("R_error")]
        
        public string RunError { get; set; }

        [JsonProperty("energy_loss")]
        
        public string LES { get; set; }

        [JsonProperty("energy_loss_error")]
        
        public string LESError { get; set; }

        [JsonProperty("proportional_1")]
        
        public string Proportional1 { get; set; }

        [JsonProperty("proportional_2")]
        
        public string Proportional2 { get; set; }

        [JsonProperty("proportional_3")]
        
        public string Proportional3 { get; set; }

        [JsonProperty("proportional_4")]
        
        public string Proportional4 { get; set; }

        [JsonProperty("proportional_average")]
        
        public string ProportionalAverage { get; set; }

        [JsonProperty("K_theoretical")]
        
        public string KTheoretical { get; set; }

        [JsonProperty("K_error")]
        
        public string KError { get; set; }

        [JsonProperty("protocol_number")]
        
        public string ProtocolNumber { get; set; }

        [JsonProperty("K_measured")]
        
        public string KMeasured { get; set; }

        [JsonProperty("detector_1")]
        
        public string Detector1 { get; set; }

        [JsonProperty("detector_2")]
        
        public string Detector2 { get; set; }

        [JsonProperty("detector_3")]
        
        public string Detector3 { get; set; }

        [JsonProperty("detector_4")]
        
        public string Detector4 { get; set; }

        [JsonProperty("detector_5")]
        
        public string Detector5 { get; set; }

        [JsonProperty("detector_6")]
        
        public string Detector6 { get; set; }

        [JsonProperty("detector_7")]
        
        public string Detector7 { get; set; }

        [JsonProperty("detector_8")]
        
        public string Detector8 { get; set; }

        [JsonProperty("detector_9")]
        
        public string Detector9 { get; set; }

        [JsonProperty("heterogenity")]
        
        public string Heterogenity { get; set; }

        public StandardMonitorProtocol(Session session, Ion ion)
        {
            SessionId = session.SessionId.ToString();
            Organization = session.OrgName;
            Cipher = "XX-XXXX";
            IrradiatedItem = session.ObjTests[0];
            BeginDate = session.StartSession.ToString("G");
            Duration = session.IrradiationTime.ToString();
            Angle = session.IrradiationAngle.ToString();
            DegraderMaterial = ion.DegradorMaterial;
            Thickness = ion.DegradDepth <= 0 ? "-" : ion.DegradDepth.ToString();
            IsotopeHeader = $"ТЗЧ/{DateTime.Now.Date.Year}-{ion.Name}-{ion.NumSessionYear}/{ion.NumOutSession}-{session.SessionId}";
            ProtocolNumber = session.AdmProtocolCode;
            AtomicNumber = ion.Isotope.ToString();
            ElementName = ion.Name;
            Energy = ion.EnergySurface.ToString();
            EnergyError = ion.ErrorTestObj.ToString();
            Run = ion.Run.ToString();
            RunError = ion.ErrorRun.ToString();
            LES = ion.LES.ToString();
            LESError = ion.ErrorLES.ToString();
            Proportional1 = session.OnlineDetectors[0].ToString("0.00E00").Insert(5, "+");
            Proportional2 = session.OnlineDetectors[1].ToString("0.00E00").Insert(5, "+");
            Proportional3 = session.OnlineDetectors[2].ToString("0.00E00").Insert(5, "+");
            Proportional4 = session.OnlineDetectors[3].ToString("0.00E00").Insert(5, "+");
            ProportionalAverage = session.MedOnlineDetectors.ToString("0.00E00").Insert(5, "+");
            TestStand = "ИИК 10К-400";
            BeginDate = session.StartSession.ToString("G");
            Temperature = session.TemperatureSession.ToString();
            KTheoretical = session.Kcoeff.ToString();
            KMeasured = "";
            KError = session.Error.ToString();
            Heterogenity = session.Heterogeneity.ToString();
            Detector1 = session.TrackDetectors[0].ToString("0.00E00").Insert(5, "+");
            Detector2 = "";
            Detector3 = "";
            Detector4 = "";
            Detector5 = "";
            Detector6 = "";
            Detector7 = "";
            Detector8 = "";
            Detector9 = "";
            if (session.TrackDetectors.Count > 1)
            {
                Detector2 = session.TrackDetectors[1].ToString("0.00E00").Insert(5, "+");
                if (session.TrackDetectors.Count > 2)
                {
                    Detector3 = session.TrackDetectors[2].ToString("0.00E00").Insert(5, "+");
                    if (session.TrackDetectors.Count > 3)
                    {
                        Detector4 = session.TrackDetectors[3].ToString("0.00E00").Insert(5, "+");
                        if (session.TrackDetectors.Count > 4)
                        {
                            Detector5 = session.TrackDetectors[4].ToString("0.00E00").Insert(5, "+");
                            if (session.TrackDetectors.Count > 5)
                            {
                                Detector6 = session.TrackDetectors[5].ToString("0.00E00").Insert(5, "+");
                                if (session.TrackDetectors.Count > 6)
                                {
                                    Detector7 = session.TrackDetectors[6].ToString("0.00E00").Insert(5, "+");
                                    if (session.TrackDetectors.Count > 7)
                                    {
                                        Detector8 = session.TrackDetectors[7].ToString("0.00E00").Insert(5, "+");
                                        if (session.TrackDetectors.Count > 8)
                                        {
                                            Detector9 = session.TrackDetectors[8].ToString("0.00E00").Insert(5, "+");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
