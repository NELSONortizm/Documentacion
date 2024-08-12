public string RepleaceSeveralSpacesbyOne(string value)
        {

          string _value = Regex.Replace(value.Trim(), " {2,}", " ");
            _value = _value.Replace(' ', '+');
            return _value;

        }