<?xml version="1.0" encoding="UTF-16"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var s1 s0 userCSharp ScriptNS0" version="1.0" xmlns:s1="http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0" xmlns:ns0="http://Mobiletec.Biztalk.FireHouse.Schemas.FireHouseData" xmlns:s0="http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2" xmlns:userCSharp="http://schemas.microsoft.com/BizTalk/2003/userCSharp" xmlns:ScriptNS0="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0">
  <xsl:output omit-xml-declaration="yes" method="xml" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="/s1:CAD_Message" />
  </xsl:template>
  <xsl:template match="/s1:CAD_Message">
    <xsl:variable name="var:v1" select="userCSharp:StringLeft(&quot;V01.00.00&quot; , &quot;10&quot;)" />
    <xsl:variable name="var:v2" select="userCSharp:StringLeft(&quot;INCI&quot; , &quot;4&quot;)" />
    <xsl:variable name="var:v4" select="userCSharp:LogicalExistence(boolean(Body/s0:CallEntry/s0:Call/s0:StreetDirection))" />
    <xsl:variable name="var:v5" select="userCSharp:LogicalNot(string($var:v4))" />
    <xsl:variable name="var:v7" select="userCSharp:StringLeft(&quot;USEI&quot; , &quot;4&quot;)" />
    <xsl:variable name="var:v8" select="userCSharp:StringLeft(&quot;INVL&quot; , &quot;4&quot;)" />
    <xsl:variable name="var:v9" select="userCSharp:StringConcat(&quot;N&quot;)" />
    <xsl:variable name="var:v10" select="userCSharp:StringConcat(&quot;IVPH&quot;)" />
    <xsl:variable name="var:v11" select="userCSharp:StringConcat(&quot;CAD&quot;)" />
    <ns0:Request>
      <Version>
        <xsl:value-of select="$var:v1" />
      </Version>
      <INCI>
        <INCI>
          <xsl:value-of select="$var:v2" />
        </INCI>
        <xsl:variable name="var:v3" select="ScriptNS0:ObtenerParametro(string(Header/AgencyName/text()) , string(Header/DepartmentName/text()) , string(Header/Target/text()) , &quot;FDID&quot;)" />
        <FDID>
          <xsl:value-of select="$var:v3" />
        </FDID>
        <xsl:element name="alm_date">
    <xsl:variable name="received" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and &#xD;&#xA;		  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Received' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
    <xsl:if test="$received !=''">
        <xsl:value-of select="concat(substring($received,6,2),'/',substring($received,9,2),'/',substring($received,1,4))" />
    </xsl:if>
</xsl:element>
        <xsl:element name="alm_time">		  
		  <xsl:variable name="received1" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and &#xD;&#xA;		  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Received' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		  <xsl:value-of select="substring($received1,12,8)" />
	  </xsl:element>
        <xsl:call-template name="Template_inci_no">
          <xsl:with-param name="Agency" select="string(Header/AgencyName/text())" />
          <xsl:with-param name="Department" select="string(Header/DepartmentName/text())" />
        </xsl:call-template>
        <xsl:element name="station">		  
		  <xsl:variable name="departmentId" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='DepartmentID' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		  <xsl:value-of select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='AssociatedDepartments' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Department' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='DepartmentID' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2'] =$departmentId ]/*[local-name()='ZoneFieldLevel2' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
  </xsl:element>
        <xsl:call-template name="Template_addr_vic">
          <xsl:with-param name="fulladdress" select="string(Body/s0:CallEntry/s0:Call/s0:FullAddress/text())" />
          <xsl:with-param name="streetname" select="string(Body/s0:CallEntry/s0:Call/s0:StreetName/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_addr_type">
          <xsl:with-param name="fulladdress" select="string(Body/s0:CallEntry/s0:Call/s0:FullAddress/text())" />
          <xsl:with-param name="crosstreet1" select="string(Body/s0:CallEntry/s0:Call/s0:CrossStreet1/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;st_prefix&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:HouseAlpha/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_number">
          <xsl:with-param name="fulladdress" select="string(Body/s0:CallEntry/s0:Call/s0:FullAddress/text())" />
          <xsl:with-param name="HouseNumber" select="string(Body/s0:CallEntry/s0:Call/s0:HouseNumber/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_street">
          <xsl:with-param name="fulladdress" select="string(Body/s0:CallEntry/s0:Call/s0:FullAddress/text())" />
          <xsl:with-param name="streetname" select="string(Body/s0:CallEntry/s0:Call/s0:StreetName/text())" />
          <xsl:with-param name="cs1streetname" select="string(Body/s0:CallEntry/s0:Call/s0:CS1StreetName/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_st_type">
          <xsl:with-param name="fulladdress" select="string(Body/s0:CallEntry/s0:Call/s0:FullAddress/text())" />
          <xsl:with-param name="streettype" select="string(Body/s0:CallEntry/s0:Call/s0:StreetType/text())" />
          <xsl:with-param name="cs1type" select="string(Body/s0:CallEntry/s0:Call/s0:CS1Type/text())" />
        </xsl:call-template>
        <xsl:if test="Body/s0:CallEntry/s0:Call/s0:StreetDirection">
          <Street_x0020_Suffix>
            <xsl:value-of select="Body/s0:CallEntry/s0:Call/s0:StreetDirection/text()" />
          </Street_x0020_Suffix>
        </xsl:if>
        <xsl:if test="string($var:v5)='true'">
          <xsl:variable name="var:v6" select="&quot;&quot;" />
          <Street_x0020_Suffix>
            <xsl:value-of select="$var:v6" />
          </Street_x0020_Suffix>
        </xsl:if>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;addr_2&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:Common/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;apt_room&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:Suite/text())" />
        </xsl:call-template>
        <xst_prefix>
          <xsl:text />
        </xst_prefix>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;xstreet&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CS2StreetName/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;xst_type&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CS2Type/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;xst_suffix&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CS2Direction/text())" />
        </xsl:call-template>
        <Rural>
          <xsl:text />
        </Rural>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;city&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:City/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;state&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:State/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;zip&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:ZipCode/text())" />
        </xsl:call-template>
        <census>
          <xsl:text />
        </census>
        <xsl:element name="zone">		  
		  <xsl:variable name="departmentId" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='DepartmentID' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		  <xsl:value-of select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='AssociatedDepartments' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Department' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='DepartmentID' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2'] =$departmentId ]/*[local-name()='ZoneFieldLevel2' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
  </xsl:element>
        <county>
          <xsl:text />
        </county>
        <township>
          <xsl:text />
        </township>
        <latitude>
          <xsl:value-of select="Body/s0:CallEntry/s0:Call/s0:LocationLatitude/text()" />
        </latitude>
        <longitude>
          <xsl:value-of select="Body/s0:CallEntry/s0:Call/s0:LocationLongitude/text()" />
        </longitude>
        <xsl:element name="inci_type">		  
		  <xsl:variable name="departmentId" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='DepartmentID' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		  <xsl:value-of select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='AssociatedDepartments' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Department' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='DepartmentID' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2'] =$departmentId ]/*[local-name()='CallType' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
  </xsl:element>
        <xsl:call-template name="Template_MultAID">
          <xsl:with-param name="Agency" select="string(Header/AgencyName/text())" />
          <xsl:with-param name="Department" select="string(Header/DepartmentName/text())" />
        </xsl:call-template>
        <xsl:element name="disp_date">		  
		  <xsl:variable name="received" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Event' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Dispatched' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		   <xsl:if test="$received !=''">
            <xsl:value-of select="concat(substring($received,6,2),'/',substring($received,9,2),'/',substring($received,1,4))" />
          </xsl:if>	
  </xsl:element>
        <xsl:element name="disp_time">		  
		  <xsl:variable name="received" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Event' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Dispatched' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		  <xsl:value-of select="substring($received,12,8)" />
  </xsl:element>
        <xsl:element name="arv_date">		  
		  <xsl:variable name="received" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Event' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Arrived' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
          <xsl:if test="$received !=''">
            <xsl:value-of select="concat(substring($received,6,2),'/',substring($received,9,2),'/',substring($received,1,4))" />
          </xsl:if>		  
  </xsl:element>
        <xsl:element name="arv_time">		  
		  <xsl:variable name="received" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Event' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Arrived' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		  <xsl:value-of select="substring($received,12,8)" />
  </xsl:element>
        <xsl:element name="ctrl_date">	
          <xsl:variable name="Depto_name" select="/*[local-name()='CAD_Message' ]/*[local-name()='Header']/*[local-name()='DepartmentName']" />
		  
          <xsl:variable name="Agency_var" select="/*[local-name()='CAD_Message' ]/*[local-name()='Header']/*[local-name()='AgencyName']" />
		  
         <xsl:variable name="Agency_name" select="/*[local-name()='CAD_Message' ]/*[local-name()='Body']/*[local-name()='CallEntry']/*[local-name()='Responders']/*[local-name()='Agency' and child::*[local-name()='Agency']=$Agency_var and child::*[local-name()='DepartmentName']=$Depto_name ]/*[local-name()='AgencyName']" />		  
		 
		 <xsl:variable name="statusdatev" select="/*[local-name()='CAD_Message' ]/*[local-name()='Body']/*[local-name()='CallEntry']/*[local-name()='Responders']/*[local-name()='Unit' and child::*[local-name()='AgencyName']=$Agency_name]/*[local-name()='UnitStatus' and child::*[local-name()='Status']='UC']" /> 

		  <xsl:for-each select="$statusdatev">		  
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
        <xsl:element name="ctrl_time">	
          <xsl:variable name="Depto_name" select="/*[local-name()='CAD_Message']/*[local-name()='Header' and namespace-uri()='']/*[local-name()='DepartmentName' and namespace-uri()='']" />
		  
          <xsl:variable name="Agency_var" select="/*[local-name()='CAD_Message']/*[local-name()='Header' and namespace-uri()='']/*[local-name()='AgencyName' and namespace-uri()='']" />
		  
         <xsl:variable name="Agency_name" select="/*[local-name()='CAD_Message']/*[local-name()='Body']/*[local-name()='CallEntry']/*[local-name()='Responders']/*[local-name()='Agency' and child::*[local-name()='Agency']=$Agency_var and child::*[local-name()='DepartmentName']=$Depto_name ]/*[local-name()='AgencyName']" />		  
		 
		 <xsl:variable name="statusdatev" select="/*[local-name()='CAD_Message']/*[local-name()='Body']/*[local-name()='CallEntry']/*[local-name()='Responders']/*[local-name()='Unit' and child::*[local-name()='AgencyName']=$Agency_name]/*[local-name()='UnitStatus' and child::*[local-name()='Status']='UC']" /> 

		  <xsl:for-each select="$statusdatev">		  
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				
				<xsl:if test="position() = 1 and text()!=''">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
        <xsl:element name="Clr_date">		  
		  <xsl:variable name="received" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Event' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='LastClear' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
          <xsl:if test="$received !=''">
            <xsl:value-of select="concat(substring($received,6,2),'/',substring($received,9,2),'/',substring($received,1,4))" />
          </xsl:if>
  </xsl:element>
        <xsl:element name="Clr_time">		  
		  <xsl:variable name="received" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Event' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='LastClear' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		   <xsl:value-of select="substring($received,12,8)" />
  </xsl:element>
        <shift>
          <xsl:text />
        </shift>
        <alarms>
          <xsl:text />
        </alarms>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;alm_type&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:ReceivedFrom/text())" />
        </xsl:call-template>
        <e911_Used>
          <xsl:text />
        </e911_Used>
        <xsl:element name="district">		  
		  <xsl:variable name="departmentId" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='DepartmentID' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		  <xsl:value-of select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='AssociatedDepartments' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Department' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='DepartmentID' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2'] =$departmentId ]/*[local-name()='ZoneFieldLevel1' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
  </xsl:element>
        <xsl:element name="narrative">	         
		 <xsl:for-each select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Narrative' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				    <xsl:value-of select="concat(./s0:CreateDate,' ')" /> <xsl:value-of select="concat(./s0:Narrative,'  ')" />
		</xsl:for-each>
  </xsl:element>
      </INCI>
      <USEI>
        <USEI>
          <xsl:value-of select="$var:v7" />
        </USEI>
        <xsl:element name="CallType">		  
		  <xsl:variable name="departmentId" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='DepartmentID' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
		  <xsl:value-of select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='AssociatedDepartments' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Department' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='DepartmentID' and  namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2'] =$departmentId ]/*[local-name()='CallType' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']" />
  </xsl:element>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;ReceivedFrom&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:ReceivedFrom/text())" />
        </xsl:call-template>
      </USEI>
      <INVL>
        <INVL>
          <xsl:value-of select="$var:v8" />
        </INVL>
        <Invl_type>
          <xsl:text />
        </Invl_type>
        <Owner>
          <xsl:text />
        </Owner>
        <Occupant>
          <xsl:text />
        </Occupant>
        <Prefix>
          <xsl:text />
        </Prefix>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;Last&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompLastName/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;First&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompFirstName/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;Middle&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompMiddleName/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;Suffix&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompSuffix/text())" />
        </xsl:call-template>
        <Institution>
          <xsl:text />
        </Institution>
        <Addr_same>
          <xsl:value-of select="$var:v9" />
        </Addr_same>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;Addr_1&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompFullAddress/text())" />
        </xsl:call-template>
        <Addr_2>
          <xsl:text />
        </Addr_2>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;Apt_room&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompSuite/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;City&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompCity/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;State&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompState/text())" />
        </xsl:call-template>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;Zip&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompZipCode/text())" />
        </xsl:call-template>
        <xsl:element name="Gender">	
          <xsl:variable name="persons" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Person' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='Classification' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']='COMPLAINANT'] " />		 
		  <xsl:for-each select="$persons">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:value-of select="./s0:Sex" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
        <xsl:element name="Age_yrs">	
          <xsl:variable name="persons" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Person' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='Classification' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']='COMPLAINANT'] " />		 
		  <xsl:for-each select="$persons">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:value-of select="./s0:Age" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
        <xsl:element name="dob">
    <xsl:variable name="persons" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Person' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='Classification' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']='COMPLAINANT'] " />
    <xsl:for-each select="$persons">
        <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
        <xsl:if test="position() = 1">
            <xsl:variable name="date_" select="./s0:DOB" />
            <xsl:if test="$date_ !=''">
                <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
            </xsl:if>
        </xsl:if>
    </xsl:for-each>
</xsl:element>
        <xsl:element name="ssn">	
          <xsl:variable name="persons" select="/*[local-name()='CAD_Message' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract_With_Header/3.0']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Call' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']/*[local-name()='Person' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2' and child::*[local-name()='Classification' and namespace-uri()='http://MobileTec.Biztalk.Cad.Schemas.Events/CAD_Event_DataContract/3.2']='COMPLAINANT'] " />		 
		  <xsl:for-each select="$persons">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:value-of select="./s0:SSN" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
        <xsl:call-template name="Template_mapeo_directo">
          <xsl:with-param name="nombre_nodo" select="&quot;Notes&quot;" />
          <xsl:with-param name="valor_nodo" select="string(Body/s0:CallEntry/s0:Call/s0:CompNotes/text())" />
        </xsl:call-template>
      </INVL>
      <IVPH>
        <IVPH>
          <xsl:value-of select="$var:v10" />
        </IVPH>
        <code>
          <xsl:value-of select="$var:v11" />
        </code>
        <phone>
          <xsl:value-of select="Body/s0:CallEntry/s0:Call/s0:CompPhoneNumber/text()" />
        </phone>
        <ext>
          <xsl:value-of select="Body/s0:CallEntry/s0:Call/s0:CompExtNumber/text()" />
        </ext>
      </IVPH>
      <xsl:for-each select="Body/s0:CallEntry/s0:Responders">
        <xsl:for-each select="s0:Unit">
          <xsl:variable name="var:v12" select="userCSharp:StringConcat(&quot;UNIT&quot;)" />
          <UNIT>
            <UNIT>
              <xsl:value-of select="$var:v12" />
            </UNIT>
            <Empty>
              <xsl:text />
            </Empty>
            <Unit>
              <xsl:value-of select="s0:UnitName/text()" />
            </Unit>
            <Resp_Code>
              <xsl:text />
            </Resp_Code>
            <fire>
              <xsl:text />
            </fire>
            <Medical>
              <xsl:text />
            </Medical>
            <Rescue>
              <xsl:text />
            </Rescue>
            <Other>
              <xsl:text />
            </Other>
            <xsl:element name="Notif_Date">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='DI']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="Notif_Time">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='DI']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>		   
  </xsl:element>
            <xsl:element name="Roll_Date">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='EN']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="Roll_Time">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='EN']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>	
  </xsl:element>
            <xsl:element name="Cancelled">	
          <xsl:variable name="statusdatev" select="count(./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL'])" />	
           
	<xsl:choose>
		  <xsl:when test="$statusdatev &gt; 0">N</xsl:when> 
		  <xsl:otherwise>Y</xsl:otherwise> 
	 </xsl:choose>
  </xsl:element>
            <xsl:element name="Cancel_Date">	
          <xsl:variable name="cancel_" select="count(./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL'])" />	
           
		  <xsl:if test="$cancel_ = 0">
		      <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='REASSIGN']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
		  </xsl:if> 	 
  </xsl:element>
            <xsl:element name="Cancel_Time">	
          <xsl:variable name="cancel_" select="count(./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL'])" />	
           
		  <xsl:if test="$cancel_ = 0">
		      <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='REASSIGN']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				     <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>
		  </xsl:if> 	 
  </xsl:element>
            <xsl:element name="Arv_Date">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='AS']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="Arv_Time">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='AS']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>		   
  </xsl:element>
            <xsl:element name="Pt_Date">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='AS-P']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="Pt_Time">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='AS-P']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>		   
  </xsl:element>
            <xsl:element name="Lv_Date">
    <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and contains('*TH*TR*', concat('*',child::*[local-name()='Status'],'*'))]" />
    <xsl:for-each select="$statusdatev">
        <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
        <xsl:if test="position() = 1">
            <xsl:variable name="date_" select="./s0:StatusDate" />
            <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
        </xsl:if>
    </xsl:for-each>
</xsl:element>
            <xsl:element name="Lv_Time">
    <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and contains('*TH*TR*', concat('*',child::*[local-name()='Status'],'*'))]" />
    <xsl:for-each select="$statusdatev">
        <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
        <xsl:if test="position() = 1">
            <xsl:variable name="date_" select="./s0:StatusDate" />
            <xsl:value-of select="substring($date_,12,8)" />
        </xsl:if>
    </xsl:for-each>
</xsl:element>
            <xsl:element name="Dest_Date">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='AH']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="Dest_Time">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='AH']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>		   
  </xsl:element>
            <xsl:element name="Clr_Date">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="Clr_Time">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>		   
  </xsl:element>
            <xsl:element name="In_Date">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and contains('*CL*REASSIGN*', concat('*',child::*[local-name()='Status'],'*'))]" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="In_Time">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and contains('*CL*REASSIGN*', concat('*',child::*[local-name()='Status'],'*'))]" />	 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				      <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:call-template name="calculo_horas" />
            <xsl:call-template name="Template_Mileage">
              <xsl:with-param name="beginning" select="string(s0:BeginningMileage/text())" />
              <xsl:with-param name="ending" select="string(s0:EndingMileage/text())" />
            </xsl:call-template>
          </UNIT>
        </xsl:for-each>
      </xsl:for-each>
      <xsl:for-each select="Body/s0:CallEntry/s0:Responders">
        <xsl:for-each select="s0:Unit">
          <xsl:variable name="var:v13" select="userCSharp:StringTrimLeft(&quot;AID&quot;)" />
          <AID>
            <AID>
              <xsl:value-of select="$var:v13" />
            </AID>
            <xsl:variable name="var:v14" select="ScriptNS0:ObtenerParametro(string(../../../../Header/AgencyName/text()) , string(../../../../Header/DepartmentName/text()) , string(../../../../Header/Target/text()) , &quot;DEPT&quot;)" />
            <MA_DEPT>
              <xsl:value-of select="$var:v14" />
            </MA_DEPT>
            <xsl:call-template name="Template_their_inci">
              <xsl:with-param name="Agency" select="string(../../../../Header/AgencyName/text())" />
              <xsl:with-param name="Department" select="string(../../../../Header/DepartmentName/text())" />
            </xsl:call-template>
            <xsl:call-template name="Template_MultAID_AID">
              <xsl:with-param name="Agency" select="string(../../../../Header/AgencyName/text())" />
              <xsl:with-param name="Department" select="string(../../../../Header/DepartmentName/text())" />
            </xsl:call-template>
            <FIRE>
              <xsl:text />
            </FIRE>
            <MEDICAL>
              <xsl:text />
            </MEDICAL>
            <RESCUE>
              <xsl:text />
            </RESCUE>
            <OTHER>
              <xsl:text />
            </OTHER>
            <xsl:element name="NOTIF_DATE">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='DI']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="NOTIF_TIME">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='DI']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>		   
  </xsl:element>
            <xsl:element name="CANCELLED">	
          <xsl:variable name="statusdatev" select="count(./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL'])" />	
           
	<xsl:choose>
		  <xsl:when test="$statusdatev &gt; 0">N</xsl:when> 
		  <xsl:otherwise>Y</xsl:otherwise> 
	 </xsl:choose>
  </xsl:element>
            <xsl:element name="CANCEL_DATE">	
          <xsl:variable name="cancel_" select="count(./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL'])" />	
           
		  <xsl:if test="$cancel_ = 0">
		      <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='REASSIGN']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
		  </xsl:if> 	 
  </xsl:element>
            <xsl:element name="CANCEL_TIME">	
          <xsl:variable name="cancel_" select="count(./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL'])" />	
           
		  <xsl:if test="$cancel_ = 0">
		      <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='REASSIGN']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				     <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>
		  </xsl:if> 	 
  </xsl:element>
            <xsl:element name="ARV_DATE">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='AS']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="ARV_TIME">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='AS']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>		   
  </xsl:element>
            <xsl:element name="CLR_DATE">	
         <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="concat(substring($date_,6,2),'/',substring($date_,9,2),'/',substring($date_,1,4))" />
				</xsl:if>
		   </xsl:for-each>
  </xsl:element>
            <xsl:element name="CLR_TIME">	
          <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='CL']" />		 
		  <xsl:for-each select="$statusdatev">
				 <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
				<xsl:if test="position() = 1">
				   <xsl:variable name="date_" select="./s0:StatusDate" />
				    <xsl:value-of select="substring($date_,12,8)" />
				</xsl:if>
		   </xsl:for-each>		   
  </xsl:element>
            <APP_SUPP>
              <xsl:text />
            </APP_SUPP>
            <PER_SUPP>
              <xsl:text />
            </PER_SUPP>
            <APP_EMS>
              <xsl:text />
            </APP_EMS>
            <PER_EMS>
              <xsl:text />
            </PER_EMS>
            <APP_RESCUE>
              <xsl:text />
            </APP_RESCUE>
            <PER_RESCUE>
              <xsl:text />
            </PER_RESCUE>
            <APP_OTHER>
              <xsl:text />
            </APP_OTHER>
            <PER_OTHER>
              <xsl:text />
            </PER_OTHER>
          </AID>
        </xsl:for-each>
      </xsl:for-each>
    </ns0:Request>
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="userCSharp"><![CDATA[
public string StringLeft(string str, string count)
{
	string retval = "";
	double d = 0;
	if (str != null && IsNumeric(count, ref d))
	{
		int i = (int) d;
		if (i > 0)
		{ 
			if (i <= str.Length)
			{
				retval = str.Substring(0, i);
			}
			else
			{
				retval = str;
			}
		}
	}
	return retval;
}


public bool LogicalExistence(bool val)
{
	return val;
}


public bool LogicalNot(string val)
{
	return !ValToBool(val);
}


public string StringConcat(string param0)
{
   return param0;
}


public string StringTrimLeft(string str)
{
	if (str == null)
	{
		return "";
	}
	return str.TrimStart(null);
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

public bool ValToBool(string val)
{
	if (val != null)
	{
		if (string.Compare(val, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
		{
			return true;
		}
		if (string.Compare(val, bool.FalseString, StringComparison.OrdinalIgnoreCase) == 0)
		{
			return false;
		}
		val = val.Trim();
		if (string.Compare(val, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
		{
			return true;
		}
		if (string.Compare(val, bool.FalseString, StringComparison.OrdinalIgnoreCase) == 0)
		{
			return false;
		}
		double d = 0;
		if (IsNumeric(val, ref d))
		{
			return (d > 0);
		}
	}
	return false;
}


]]></msxsl:script>
  <xsl:template name="Template_addr_vic">
<xsl:param name="fulladdress" />
<xsl:param name="streetname" />
<xsl:element name="addr_vic">

	<xsl:choose>
		  <xsl:when test="$fulladdress !=''">1</xsl:when> 
		  <xsl:otherwise> 
			   <xsl:if test="$streetname !=''">2</xsl:if> 	
		  </xsl:otherwise> 
	 </xsl:choose>
</xsl:element>

</xsl:template>
  <xsl:template name="Template_addr_type">
<xsl:param name="fulladdress" />
<xsl:param name="crosstreet1" />
<xsl:element name="addr_type">

	<xsl:choose>
		  <xsl:when test="$fulladdress !=''">1</xsl:when> 
		  <xsl:otherwise> 
			   <xsl:if test="$crosstreet1 !=''">2</xsl:if> 	
		  </xsl:otherwise> 
	 </xsl:choose>
</xsl:element>

</xsl:template>
  <xsl:template name="Template_number">
<xsl:param name="fulladdress" />
<xsl:param name="HouseNumber" />
<xsl:element name="number">
		  <xsl:if test="$fulladdress !=''">
		   <xsl:value-of select="$HouseNumber" />
		  </xsl:if> 
</xsl:element>
</xsl:template>
  <xsl:template name="Template_street">
<xsl:param name="fulladdress" />
<xsl:param name="streetname" />
<xsl:param name="cs1streetname" />
<xsl:element name="street">

	<xsl:choose>
		  <xsl:when test="$fulladdress !=''">
		    <xsl:value-of select="$streetname" />
		  </xsl:when> 
		  <xsl:otherwise> 
			   	 <xsl:value-of select="$cs1streetname" />
		  </xsl:otherwise> 
	 </xsl:choose>
</xsl:element>
</xsl:template>
  <xsl:template name="Template_st_type">
<xsl:param name="fulladdress" />
<xsl:param name="streettype" />
<xsl:param name="cs1type" />
<xsl:element name="st_type">

	<xsl:choose>
		  <xsl:when test="$fulladdress !=''">
		    <xsl:value-of select="$streettype" />
		  </xsl:when> 
		  <xsl:otherwise> 
			   	 <xsl:value-of select="$cs1type" />
		  </xsl:otherwise> 
	 </xsl:choose>
</xsl:element>
</xsl:template>
  <xsl:template name="Template_MultAID">
    <xsl:param name="Agency" />
    <xsl:param name="Department" />
    <xsl:element name="mutl_aid">
       <xsl:variable name="cont_agencias" select="count(/*[local-name()='CAD_Message']/*[local-name()='Body']/*[local-name()='CallEntry']/*[local-name()='Responders']/*[local-name()='Agency']/*[local-name()='DepartmentName' and text()=$Department])" />
       <xsl:choose>
         <xsl:when test="$cont_agencias = 1">N</xsl:when>
         <xsl:otherwise>
            <xsl:choose>
              <xsl:when test="/*[local-name()='CAD_Message']/*[local-name()='Body']/*[local-name()='CallEntry' ]/*[local-name()='Responders']/*[local-name()='Agency' and child::*[local-name()='DepartmentName' and text()=$Department] and child::*[local-name()='Agency' and text()=$Agency] ]/*[local-name()='Sequence']=1">1</xsl:when>
              <xsl:otherwise>3</xsl:otherwise>
            </xsl:choose>
         </xsl:otherwise>
       </xsl:choose>
    </xsl:element>
</xsl:template>
  <xsl:template name="Template_inci_no">
    <xsl:param name="Agency" />
    <xsl:param name="Department" />
    <xsl:element name="inci_no">
        <xsl:variable name="casenumber" select="/*[local-name()='CAD_Message']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry']/*[local-name()='Responders']/*[local-name()='Agency' and child::*[local-name()='DepartmentName' and text()=$Department]]/*[local-name()='AgencyCaseNumbers' and child::*[local-name()='Agency' and text()=$Agency] and child::*[local-name()='Sequence' and text()=1]]/*[local-name()='CaseNumber']" />
        <xsl:value-of select="substring($casenumber,3,3)" />
        <xsl:variable name="numero" select="substring($casenumber,6)" />
        <xsl:variable name="numero3" select="concat('0000000',$numero)" />
        <xsl:value-of select="substring($numero3,string-length($numero3)-6,7)" />
    </xsl:element>
</xsl:template>
  <xsl:template name="Template_mapeo_directo">
    <xsl:param name="nombre_nodo" />
    <xsl:param name="valor_nodo" />
    <xsl:element name="{$nombre_nodo}">
        <xsl:value-of select="$valor_nodo" />
    </xsl:element>
</xsl:template>
  <xsl:template name="calculo_horas">
    <xsl:variable name="statusdatev" select="./*[local-name()='UnitStatus'and contains('*CL*REASSIGN*', concat('*',child::*[local-name()='Status'],'*'))]" />
    <xsl:variable name="date_cl_reassign">
        <xsl:for-each select="$statusdatev">
            <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
            <xsl:if test="position() = 1">
                <xsl:value-of select="./s0:StatusDate" />
            </xsl:if>
        </xsl:for-each>
    </xsl:variable>
    <xsl:variable name="statusdatev1" select="./*[local-name()='UnitStatus' and child::*[local-name()='Status']='DI']" />
    <xsl:variable name="date_DI">
        <xsl:for-each select="$statusdatev1">
            <xsl:sort select="./s0:Sequence" order="ascending" data-type="number" />
            <xsl:if test="position() = 1">
                <xsl:value-of select="./s0:StatusDate" />
            </xsl:if>
        </xsl:for-each>
    </xsl:variable>
    <xsl:variable name="start">
        <xsl:call-template name="dateTime-to-seconds">
            <xsl:with-param name="dateTime" select="$date_DI" />
        </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="end">
        <xsl:call-template name="dateTime-to-seconds">
            <xsl:with-param name="dateTime" select="$date_cl_reassign" />
        </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="duration" select="$end - $start" />
    <Hours>
        <xsl:variable name="horafinal" select="$duration div 3600" />
        <xsl:value-of select="substring($horafinal,1,8)" />
       <!--  <xsl:value-of select="$start"/>-
        <xsl:value-of select="$end"/>-
        <xsl:value-of select="$date_cl_reassign"/>-
        <xsl:value-of select="$date_DI"/> -->
    </Hours>
</xsl:template>
  <xsl:template name="dateTime-to-seconds">
    <xsl:param name="dateTime" />
    <xsl:variable name="date" select="substring-before($dateTime, 'T')" />
    <xsl:variable name="time" select="substring-after($dateTime, 'T')" />
    <xsl:variable name="local-time">
       <xsl:choose>
          <xsl:when test="string-length($time)=18">
              <xsl:value-of select="substring($time, 1, string-length($time) - 6)" />
          </xsl:when>
          <xsl:otherwise>
            <xsl:value-of select="$time" />
          </xsl:otherwise>
       </xsl:choose>
    </xsl:variable>
    <!-- <xsl:variable name="local-time" select="substring($time, 1, string-length($time) - 6)"/> -->
    <xsl:variable name="offset" select="substring-after($time, $local-time)" />
    <xsl:variable name="year" select="substring($date, 1, 4)" />
    <xsl:variable name="month" select="substring($date, 6, 2)" />
    <xsl:variable name="day" select="substring($date, 9, 2)" />
    <xsl:variable name="hour" select="substring($local-time, 1, 2)" />
    <xsl:variable name="minute" select="substring($local-time, 4, 2)" />
    <xsl:variable name="second" select="substring($local-time, 7)" />
    <!-- <xsl:variable name="offset-sign" select="1 - 2 * starts-with($offset, '-')"/>
    <xsl:variable name="offset-hour" select="substring($offset, 2, 2) * $offset-sign"/>
    <xsl:variable name="offset-minute" select="substring($offset, 5, 2) * $offset-sign"/> -->
    <xsl:variable name="a" select="floor((14 - $month) div 12)" />
    <xsl:variable name="y" select="$year + 4800 - $a" />
    <xsl:variable name="m" select="$month + 12*$a - 3" />
    <xsl:variable name="jd" select="$day + floor((153*$m + 2) div 5) + 365*$y + floor($y div 4) - floor($y div 100) + floor($y div 400) - 32045" />
    <!-- <xsl:value-of select="86400*$jd + 3600*$hour + 60*$minute + $second - 3600*$offset-hour - 60*$offset-minute"/> -->
    <xsl:value-of select="86400*$jd + 3600*$hour + 60*$minute + $second" />
</xsl:template>
  <xsl:template name="Template_Mileage">
<xsl:param name="beginning" />
<xsl:param name="ending" />
<xsl:element name="Mileage">

	<xsl:variable name="millas" select="$ending - $beginning" />
	<xsl:value-of select="substring($millas,1,8)" />	

</xsl:element>

</xsl:template>
  <xsl:template name="Template_their_inci">
    <xsl:param name="Agency" />
    <xsl:param name="Department" />
    <xsl:element name="THEIR_INCI">
        <xsl:variable name="casenumber" select="/*[local-name()='CAD_Message']/*[local-name()='Body' and namespace-uri()='']/*[local-name()='CallEntry']/*[local-name()='Responders']/*[local-name()='Agency' and child::*[local-name()='DepartmentName' and text()=$Department]]/*[local-name()='AgencyCaseNumbers' and child::*[local-name()='Agency' and text()=$Agency] and child::*[local-name()='Sequence' and text()=1]]/*[local-name()='CaseNumber']" />
        <xsl:value-of select="substring($casenumber,1,4)" />        
        <xsl:value-of select="substring($casenumber,6)" />
    </xsl:element>
</xsl:template>
  <xsl:template name="Template_MultAID_AID">
    <xsl:param name="Agency" />
    <xsl:param name="Department" />
    <xsl:element name="MUTL_AID">
        <xsl:variable name="cont_agencias" select="count(/*[local-name()='CAD_Message']/*[local-name()='Body']/*[local-name()='CallEntry']/*[local-name()='Responders']/*[local-name()='Agency']/*[local-name()='DepartmentName' and text()=$Department])" />
        <xsl:choose>
            <xsl:when test="$cont_agencias = 1">N</xsl:when>
            <xsl:otherwise>
                <xsl:choose>
                    <xsl:when test="/*[local-name()='CAD_Message']/*[local-name()='Body']/*[local-name()='CallEntry' ]/*[local-name()='Responders']/*[local-name()='Agency' and child::*[local-name()='DepartmentName' and text()=$Department] and child::*[local-name()='Agency' and text()=$Agency] ]/*[local-name()='Sequence']=1">1</xsl:when>
                    <xsl:otherwise>3</xsl:otherwise>
                </xsl:choose>
            </xsl:otherwise>
        </xsl:choose>
    </xsl:element>
</xsl:template>
</xsl:stylesheet>