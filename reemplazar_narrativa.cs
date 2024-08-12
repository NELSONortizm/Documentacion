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
        
public  int extraer_max_characters_narrativa(string narrativa)
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