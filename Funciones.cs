public string ReemplazarPalabraReservada(string reservada, string valornodo, string cadena )
{
    
    if(valornodo =="NewLine")
    {
        valornodo = Environment.NewLine;
        
    }
    if(valornodo =="Tab")     
    {
        valornodo = "\t";
    }
    cadena = cadena.Replace(reservada,valornodo);
	return cadena;
}



public string ConstruirLocation(string common, string fulladdress, string crossStreet1, string crossStreet2 )
{
    string respuesta = "";
    common = common.Trim();
    fulladdress = fulladdress.Trim();
    crossStreet1 = crossStreet1.Trim();
    crossStreet2 = crossStreet2.Trim();
    bool commonexist, fulladdressexist, crossStreet1exist, crossStreet2exist;
    commonexist = (common != "");
    fulladdressexist = (fulladdress != "");
    crossStreet1exist = (crossStreet1 != "");
    crossStreet2exist = (crossStreet2 != "");
    
    switch((commonexist,fulladdressexist,crossStreet1exist,crossStreet2exist))
    {
         case (true, false, false, false):  
              respuesta = common;
              break;
         case (true, true, false, false):  
              respuesta = common + " " + fulladdress;
              break;         
    }      
    return respuesta;
}


public string ConstruirLocation(string common, string fulladdress, string crossStreet1, string crossStreet2 )
{
    string respuesta = "";
    common = common.Trim();
    fulladdress = fulladdress.Trim();
    crossStreet1 = crossStreet1.Trim();
    crossStreet2 = crossStreet2.Trim();
    bool commonexist, fulladdressexist, crossStreet1exist, crossStreet2exist;
    commonexist = (common != "");
    fulladdressexist = (fulladdress != "");
    crossStreet1exist = (crossStreet1 != "");
    crossStreet2exist = (crossStreet2 != "");
    
    switch()
    {
         case commonexist && !fulladdressexist && !crossStreet1exist && !crossStreet2exist:  
              respuesta = common;
              break;
         case commonexist && fulladdressexist && !crossStreet1exist && !crossStreet2exist:  
              respuesta = common + " " + fulladdress;
              break;
         case commonexist && fulladdressexist && crossStreet1exist && !crossStreet2exist:  
              respuesta = common + " " + fulladdress + "," + crossStreet1;
              break;
         case commonexist && fulladdressexist && crossStreet1exist && crossStreet2exist:  
              respuesta = common + " " + fulladdress + "," + crossStreet1 + "/" + crossStreet2;
              break;
         case commonexist && fulladdressexist && !crossStreet1exist && crossStreet2exist:  
              respuesta = common + " " + fulladdress + "/" + crossStreet2;
              break;
         case commonexist && !fulladdressexist && crossStreet1exist && crossStreet2exist:  
              respuesta = common + " " + crossStreet1 + "/" + crossStreet2;
              break;
         case commonexist && !fulladdressexist && !crossStreet1exist && crossStreet2exist:  
              respuesta = common + "/" + crossStreet2;
              break;
         case commonexist && !fulladdressexist && crossStreet1exist && !crossStreet2exist:  
              respuesta = common + " " + crossStreet1;
              break;
         case !commonexist && fulladdressexist && crossStreet1exist && crossStreet2exist:  
              respuesta = fulladdress + "," + crossStreet1 + "/" + crossStreet2;
              break;
         case !commonexist && fulladdressexist && crossStreet1exist && !crossStreet2exist:  
              respuesta = fulladdress + "," + crossStreet1;
              break;
         case !commonexist && fulladdressexist && !crossStreet1exist && crossStreet2exist:  
              respuesta = fulladdress + "/" + crossStreet2;
              break;
         case !commonexist && fulladdressexist && !crossStreet1exist && !crossStreet2exist:  
              respuesta = fulladdress;
              break;
          case !commonexist && !fulladdressexist && crossStreet1exist && crossStreet2exist:  
              respuesta = crossStreet1 + "/" + crossStreet2;
              break;
    }      
    return respuesta;
}