XmlDocument xmlDoc = new XmlDocument();
xmlDoc.LoadXml(XMLtoReturn);//cargo el string en la variable XML

public string AssignGpsValuesfromString(string rpvall)
        {

            string gpstimeofday = string.Empty; //size 5           

            string latitud = string.Empty; //size 8            
            string longitud = string.Empty; //size 9
            string speed = string.Empty; // size3
            string heading = string.Empty;//size 3
            string source = string.Empty; //size 1
            string age = string.Empty; //1
            string IDinitial = string.Empty; //from ID= to ;
            string ID = string.Empty; //from ID= to ;  
            string RPVXml = string.Empty;

            try
            {
                gpstimeofday = rpvall.Substring(3, 5);
                latitud = rpvall.Substring(8, 8);//antes (9,7)
                latitud = latitud.Substring(0, 3) + "." + latitud.Substring(3, 5); // +4132191 => +41.32191
                longitud = rpvall.Substring(16, 9);
                longitud = longitud.Substring(0, 4) + "." + longitud.Substring(4, 5); //-07388195 = > -073.88195

                speed = rpvall.Substring(25, 3);
                heading = rpvall.Substring(28, 3);
                source = rpvall.Substring(31, 1);
                age = rpvall.Substring(32, 1);
                //IDinitial = rpvall.Substring(33, 9);
                //ID = TakeIDFromString(IDinitial);///call 
                ID = rpvall.Substring(37, 4);

                //saving values in XML
                // @"<ns0:Root xmlns:ns0='http://Mobiletec.AVLOtsego.Schemas.RPV'>

                RPVXml = string.Format(
               @"<ns0:Root>
        <gpstimeofday>{0}</gpstimeofday>
        <latitud>{1}</latitud>
        <longitud>{2}</longitud>
        <speed>{3}</speed>
        <heading>{4}</heading>
        <source>{5}</source>
        <age>{6}</age>
        <ID>{7}</ID>
    </ns0:Root>", gpstimeofday, latitud, longitud, speed, heading, source, age, ID);
                //   string xmlname = "RPV" + DateTime.Now.Millisecond.ToString() + ".xml";

            }//try