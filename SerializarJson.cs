static void Main(string[] args)
        {
            // string agencia = "EVENT #[EVENT#] [CALLTYPEDESC] RECEIVED [RECDATETIME] @ [LOCATION] - NARRATIVE - [NARRATIVE]-[UNIT][ASSIGNED_UNITS:<AG1>,<FST>,<COV>]";
            //string agencia = "[EVENT#] [CALLTYPE]";
            //string rta = ReemplazarPalabraReservada("[CALLTYPE]", "104F", "[EVENT#] [CALLTYPE]");
            //Console.WriteLine(rta);
            // Console.WriteLine(extraer_agencias_asignadas(agencia));

            string json = File.ReadAllText(@"C:\Pruebas\LiveCadNet\STPSOnOTEvents_NotUnits.json");

            string livecadresult = JsonConvert.DeserializeObject<Dictionary<string, object>>(json)["LiveCadNetResult"].ToString();
            LiveCADNetResult deserialized = JsonConvert.DeserializeObject<LiveCADNetResult>(livecadresult);
            LiveCAD livecad = new LiveCAD();

            
            if (deserialized == null || deserialized.Events == null)
            {
                                
                livecad.Events = new List<Event>();
            }
            else
            {
                livecad.Events = deserialized.Events.Values.ToList();
            }

            if (deserialized == null || deserialized.Units == null)
            {

                
                livecad.Units = new List<UnitClass>();
            }
            else
            {
                livecad.Units = deserialized.Units.Values.ToList();
            }

            string[] unitsWithEventNumber = livecad.Units
                                                .Where(unit => string.IsNullOrWhiteSpace(unit.EventNumber) == false)
                                                .Select(unit => unit.EventNumber)
                                                .Distinct()
                                                .ToArray();

            AgencyEqualityComparer agencyEqualityComparer = new AgencyEqualityComparer();
            ConfigReader cr = new ConfigReader();

            livecad.Events.ForEach(e =>
            {
                e.NarrativesArray = e.CAD_CallNarrative.Values.ToList();
                e.UnitActivityLogArray = e.UnitActivityLog.Values.ToList();

                e.Agencies = livecad.Units
                                    .Where(u => u.EventNumber == e.EventNumber)
                                    .Select(u => new Agency { AgencyName = u.AgencyName, DepartmentName = u.DepartmentName })
                                    .Distinct(agencyEqualityComparer)
                                    .ToList();

                e.Agencies = e.Agencies
                                .Where(a => cr.ValidarAgenciaDestino(a.AgencyName, a.DepartmentName, "http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0#CAD_Message", "ADASHI")).ToList();

                e.IsActive = (e.Agencies.Count > 0);

            });

            List<Agency> agenciesInUnits = livecad.Units
                                            .Where(u => string.IsNullOrWhiteSpace(u.EventNumber) && !string.IsNullOrWhiteSpace(u.AgencyName) && !string.IsNullOrWhiteSpace(u.DepartmentName))
                                            .Select(u => new Agency { AgencyName = u.AgencyName, DepartmentName = u.DepartmentName })
                                            .Distinct(agencyEqualityComparer)
                                            .ToList();

            agenciesInUnits = agenciesInUnits
                                .Where(a => cr.ValidarAgenciaDestino(a.AgencyName, a.DepartmentName, "http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0#CAD_Message", "ADASHI")).ToList();


            agenciesInUnits.ForEach(a =>
                            livecad.Units
                                .Where(u => string.IsNullOrWhiteSpace(u.EventNumber) && u.AgencyName == a.AgencyName && u.DepartmentName == a.DepartmentName)
                                .ToList()
                                .ForEach(u => u.SendToTarget = true)
                            );

            //List<Event> eventWithUnits = livecad.Events
            //                            .Where(e => unitsWithEventNumber.Contains(e.EventNumber)).ToList();

            //livecad.ActiveEvents = eventWithUnits;

            XmlSerializer serializer = new XmlSerializer(typeof(LiveCAD));

            using (StreamWriter sw = new StreamWriter(@"C:\Pruebas\LiveCadNet\Result.xml"))
            {
                serializer.Serialize(sw, livecad);
            }

            Console.ReadLine();

            //string email = GetEmail("C155");
            //Console.WriteLine("Email :" + email);
            //Console.ReadLine();

        }//main