public string RemoveNonNumeric(string text)
        {
            string newText = "";

            if (String.IsNullOrEmpty(text))
            {
                return newText;
            }

            newText = Regex.Replace(text, "[^0-9]", "");

            return newText;
        }