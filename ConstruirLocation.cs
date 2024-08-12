 public string ConstruirLocation(string common, string fulladdress, string crossStreet1, string crossStreet2)
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

            if (commonexist && !fulladdressexist && !crossStreet1exist && !crossStreet2exist)
                respuesta = common;

            if (commonexist && fulladdressexist && !crossStreet1exist && !crossStreet2exist)
                respuesta = common + " " + fulladdress;

            if (commonexist && fulladdressexist && crossStreet1exist && !crossStreet2exist)
                respuesta = common + " " + fulladdress + "," + crossStreet1;

            if (commonexist && fulladdressexist && crossStreet1exist && crossStreet2exist)
                respuesta = common + " " + fulladdress + "," + crossStreet1 + "/" + crossStreet2;

            if (commonexist && fulladdressexist && !crossStreet1exist && crossStreet2exist)
                respuesta = common + " " + fulladdress + "/" + crossStreet2;

            if (commonexist && !fulladdressexist && crossStreet1exist && crossStreet2exist)
                respuesta = common + " " + crossStreet1 + "/" + crossStreet2;

            if (commonexist && !fulladdressexist && !crossStreet1exist && crossStreet2exist)
                respuesta = common + "/" + crossStreet2;

            if (commonexist && !fulladdressexist && crossStreet1exist && !crossStreet2exist)
                respuesta = common + " " + crossStreet1;

            if (!commonexist && fulladdressexist && crossStreet1exist && crossStreet2exist)
                respuesta = fulladdress + "," + crossStreet1 + "/" + crossStreet2;

            if (!commonexist && fulladdressexist && crossStreet1exist && !crossStreet2exist)
                respuesta = fulladdress + "," + crossStreet1;

            if (!commonexist && fulladdressexist && !crossStreet1exist && crossStreet2exist)
                respuesta = fulladdress + "/" + crossStreet2;

            if (!commonexist && fulladdressexist && !crossStreet1exist && !crossStreet2exist)
                respuesta = fulladdress;

            if (!commonexist && !fulladdressexist && crossStreet1exist && crossStreet2exist)
                respuesta = crossStreet1 + "/" + crossStreet2;

            return respuesta;
        }