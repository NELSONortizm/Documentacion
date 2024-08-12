<xsl:variable name="received" select="userCSharp:DateAddMilliseconds($enrouted,500)"/>

public string DateAddMilliseconds(string date, string milliseconds)
{
	string retval = "";
	double db = 0;
	if (IsDate(date) && IsNumeric(milliseconds, ref db))
	{
		DateTime dt = DateTime.Parse(date);
		int d = (int) db;
        //DateTime.AddMilliseconds(Double) Method (System) | Microsoft Docs
		dt = dt.AddMilliseconds(d);
		retval = dt.ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
	}
	return retval;
}


public bool IsNumeric(string val)
{
	if (val == null)
	{
		return false;
	}
	double d = 0;
	return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
}

public bool IsNumeric(string val, ref double d)
{
	if (val == null)
	{
		return false;
	}
	return Double.TryParse(val, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out d);
}

public bool IsDate(string val)
{
	bool retval = true;
	try
	{
		DateTime dt = Convert.ToDateTime(val, System.Globalization.CultureInfo.InvariantCulture);
	}
	catch (Exception)
	{
		retval = false;
	}
	return retval;
}