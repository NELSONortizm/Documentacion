public string ConstruirLocation(string common, string fulladdress, string crossStreet1, string crossStreet2, string zone)
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

            if (!string.IsNullOrWhiteSpace(zone))
            {
                respuesta = respuesta + "," + zone;
            }
            return respuesta;
        }

        public string ReemplazarPalabraReservada(string reservada, string valornodo, string cadena)
        {

            if (valornodo == "NewLine")
            {
                valornodo = Environment.NewLine;

            }
            if (valornodo == "Tab")
            {
                valornodo = "\t";
            }
            cadena = cadena.Replace(reservada, valornodo);
            return cadena;
        }

        public int extraer_max_characters_narrativa(string narrativa)
        {
            //[NARRATIVE:500]
            int respuesta = 0;
            int posicion_inicionarrativa;
            int posicion_finalnarrativa;
            posicion_inicionarrativa = narrativa.IndexOf("[NARRATIVE:");
            if (posicion_inicionarrativa > -1)
            {
                posicion_finalnarrativa = narrativa.IndexOf("]", posicion_inicionarrativa);
                if (posicion_finalnarrativa > -1 && (posicion_finalnarrativa - posicion_inicionarrativa - 11) > 0)
                {
                    respuesta = int.Parse(narrativa.Substring(posicion_inicionarrativa + 11, posicion_finalnarrativa - posicion_inicionarrativa - 11));
                }
            }
            return respuesta;
        }//

        public string reemplazar_narrativa(string narrativa, string cadena)
        {
            narrativa = narrativa.Trim();
            int max_characters = extraer_max_characters_narrativa(cadena);
            if (max_characters > 0)
            {
                if (narrativa.Length > max_characters)
                {
                    narrativa = narrativa.Substring(0, max_characters) + "...";
                }
                cadena = ReemplazarPalabraReservada("[NARRATIVE:" + max_characters.ToString() + "]", narrativa, cadena);
            }
            else
            {
                cadena = ReemplazarPalabraReservada("[NARRATIVE]", narrativa, cadena);
                cadena = ReemplazarPalabraReservada("[NARRATIVE:]", narrativa, cadena);
                cadena = ReemplazarPalabraReservada("[NARRATIVE:0]", narrativa, cadena);
            }
            return cadena;
        }

        public string extraer_agencias_asignadas(string agencia)
        {
            string respuesta = "";
            int posicion_inicioagencia;
            int posicion_finalagencia;
            posicion_inicioagencia = agencia.IndexOf("[ASSIGNED_UNITS:");
            if (posicion_inicioagencia > -1)
            {
                posicion_finalagencia = agencia.IndexOf("]", posicion_inicioagencia);
                if (posicion_finalagencia > -1 && (posicion_finalagencia - posicion_inicioagencia - 16) > 0)
                {
                    respuesta = agencia.Substring(posicion_inicioagencia + 16, posicion_finalagencia - posicion_inicioagencia - 16);
                }
            }           
            return respuesta;
        }//

        public string construir_lista_agencia(string lista_agencias)
        {
            string respuesta = "";
            if (!string.IsNullOrWhiteSpace(lista_agencias))
            {
                respuesta = lista_agencias.Replace('<', '*').Replace(">", "").Replace(",", "") + "*";

            }
            return respuesta;
        }