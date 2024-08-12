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