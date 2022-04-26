using Newtonsoft.Json;
using System;

namespace ByteTerraUtils
{
    internal class AdmProtocolInfo
    {
        
        [JsonProperty("isotope_header")]
        public string IsotopeHeader { get; set; }

        
        [JsonProperty("protocol_number")]
        public string ProtocolNumber { get; set; }

        
        [JsonProperty("protocol_date")]
        public string ProtocolDate { get; set; }

        
        [JsonProperty("atomic_number")]
        public string AtomicNumber { get; set; }

        
        [JsonProperty("element_name")]
        public string ElementName { get; set; }

        
        [JsonProperty("energy")]
        public string Energy { get; set; }

        
        [JsonProperty("test_stand")]
        public string TestStand { get; set; }

        
        [JsonProperty("begin_date")]
        public string BeginDate { get; set; }

        
        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        
        [JsonProperty("temperature")]
        public string Temperature { get; set; }

        
        [JsonProperty("pressure")]
        public string Pressure { get; set; }

        
        [JsonProperty("humidity")]
        public string Humidity { get; set; }

        
        [JsonProperty("N")]
        public string Ncoeff { get; set; }

        
        [JsonProperty("Phi")]
        public string Fcoeff { get; set; }

        
        [JsonProperty("K")]
        public string Kcoeff { get; set; }

        
        [JsonProperty("K_error")]
        public string Kerror { get; set; }

        
        [JsonProperty("fluence_uncertainty")]
        public string FluenceUncertainty { get; set; }

        
        [JsonProperty("continue_sout")]
        public string ContinueSout { get; set; }

        
        [JsonProperty("ion_name")]
        public string IonName { get; set; }

        
        [JsonProperty("calibrate_sout")]
        public string CalibrateSout { get; set; }

        
        [JsonProperty("continue_time")]
        public string ContinueTime { get; set; }

        
        [JsonProperty("TD1")]
        public string TD1 { get; set; }

        
        [JsonProperty("TD2")]
        public string TD2 { get; set; }

        
        [JsonProperty("TD3")]
        public string TD3 { get; set; }

        
        [JsonProperty("TD4")]
        public string TD4 { get; set; }

        
        [JsonProperty("TD5")]
        public string TD5 { get; set; }

        
        [JsonProperty("TD6")]
        public string TD6 { get; set; }

        
        [JsonProperty("TD7")]
        public string TD7 { get; set; }

        
        [JsonProperty("TD8")]
        public string TD8 { get; set; }

        
        [JsonProperty("TD9")]
        public string TD9 { get; set; }

        
        [JsonProperty("average")]
        public string Average { get; set; }

        public AdmProtocolInfo(Session session, Ion ion)
        {
            IsotopeHeader = $"ТЗЧ/{DateTime.Now.Date.Year}-{ion.Name}-{ion.NumSessionYear}/{ion.NumOutSession}-{session.SessionId}";
            ProtocolNumber = session.AdmProtocolCode;
            ProtocolDate = session.StartSession.Date.ToString("d");
            AtomicNumber = ion.Isotope.ToString();
            ElementName = ion.Name;
            Energy = ion.EnergySurface.ToString();
            TestStand = "ИИК 10К-400";
            BeginDate = session.StartSession.ToString("G");
            EndDate = session.EndSession.ToString("G");
            Temperature = session.TemperatureSession == -1 ? "-" : session.TemperatureSession.ToString();
            Pressure = session.Pressure == -1 ? "-" : session.Pressure.ToString();
            Humidity = session.Humidity == -1 ? "-" : session.Humidity.ToString();
            Ncoeff = session.StreamIntensity.ToString("0.00E00").Insert(5, "+");
            Fcoeff = session.MedTrackDetectors.ToString("0.00E00").Insert(5, "+");
            FluenceUncertainty = session.Heterogeneity == -1 ? "-" : session.Heterogeneity.ToString();
            Kcoeff = session.Kcoeff == -1 ? "-" : session.Kcoeff.ToString();
            Kerror = session.Error == -1 ? "-" : session.Error.ToString();
            ContinueSout = "";
            CalibrateSout = "\\sout";
            IonName = ion.Name;
            ContinueTime = session.EndSession.ToString("T");
            TD1 = session.TrackDetectors[0].ToString("0.00E00").Insert(5, "+");
            TD2 = "";
            TD3 = "";
            TD4 = "";
            TD5 = "";
            TD6 = "";
            TD7 = "";
            TD8 = "";
            TD9 = "";
            Average = session.MedTrackDetectors.ToString("0.00E00").Insert(5, "+");
            if (session.TrackDetectors.Count > 1)
            {
                TD2 = session.TrackDetectors[1].ToString("0.00E00").Insert(5, "+");
                if (session.TrackDetectors.Count > 2)
                {
                    TD3 = session.TrackDetectors[2].ToString("0.00E00").Insert(5, "+");
                    if (session.TrackDetectors.Count > 3)
                    {
                        TD4 = session.TrackDetectors[3].ToString("0.00E00").Insert(5, "+");
                        if (session.TrackDetectors.Count > 4)
                        {
                            TD5 = session.TrackDetectors[4].ToString("0.00E00").Insert(5, "+");
                            if (session.TrackDetectors.Count > 5)
                            {
                                TD6 = session.TrackDetectors[5].ToString("0.00E00").Insert(5, "+");
                                if (session.TrackDetectors.Count > 6)
                                {
                                    TD7 = session.TrackDetectors[6].ToString("0.00E00").Insert(5, "+");
                                    if (session.TrackDetectors.Count > 7)
                                    {
                                        TD8 = session.TrackDetectors[7].ToString("0.00E00").Insert(5, "+");
                                        if (session.TrackDetectors.Count > 8)
                                        {
                                            TD9 = session.TrackDetectors[8].ToString("0.00E00").Insert(5, "+");
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
