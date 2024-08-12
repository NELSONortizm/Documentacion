using System.Xml;

// Crear un objeto XmlDocument
XmlDocument xmlDoc = new XmlDocument();

// Crear el nodo raíz del documento XML
XmlElement rootNode = xmlDoc.CreateElement("root");
xmlDoc.AppendChild(rootNode);

// Crear un nodo hijo del nodo raíz
XmlElement childNode = xmlDoc.CreateElement("child");
rootNode.AppendChild(childNode);

// Agregar un atributo al nodo hijo
XmlAttribute attribute = xmlDoc.CreateAttribute("attribute");
attribute.Value = "value";
childNode.Attributes.Append(attribute);

// Agregar contenido al nodo hijo
childNode.InnerText = "Contenido del elemento hijo";

// Guardar el documento XML en un archivo
xmlDoc.Save("archivo.xml");


//***************************************************


using System.Xml.Linq;

// ...

// Obtener el objeto XmlNode que deseas recorrer (por ejemplo, el nodo raíz de un archivo XML)
XmlNode nodoRaiz = doc.DocumentElement;

// Convertir el objeto XmlNode a un objeto XNode de LINQ to XML
XNode xNodoRaiz = XNode.ReadFrom(nodoRaiz.CreateNavigator());

// Recorrer todos los nodos secundarios del nodo raíz utilizando LINQ to XML
foreach (XNode xNodo in xNodoRaiz.Nodes())
{
    // Hacer algo con cada nodo
    Console.WriteLine("Tipo del nodo: {0}", xNodo.NodeType);

    // Si el nodo es un elemento, se puede acceder a sus atributos
    if (xNodo is XElement xElemento)
    {
        Console.WriteLine("Nombre del elemento: {0}", xElemento.Name.LocalName);
        Console.WriteLine("Atributos del elemento:");
        foreach (XAttribute xAtributo in xElemento.Attributes())
        {
            Console.WriteLine("{0} = {1}", xAtributo.Name.LocalName, xAtributo.Value);
        }
    }
}

***
using System;
using System.Linq;
using System.Xml.Linq;

// ...

// Cargar el archivo XML en un objeto XDocument
XDocument doc = XDocument.Load("personas.xml");

// Consultar los nodos "persona" cuya edad es mayor a 30
var personas = from p in doc.Descendants("persona")
               where (int)p.Element("edad") > 30
               select new
               {
                   Id = (int)p.Attribute("id"),
                   Nombre = (string)p.Element("nombre"),
                   Apellido = (string)p.Element("apellido"),
                   Edad = (int)p.Element("edad")
               };

// Imprimir los resultados de la consulta
foreach (var persona in personas)
{
    Console.WriteLine("Persona {0} - {1} {2}, edad {3}", persona.Id, persona.Nombre, persona.Apellido, persona.Edad);
}


//***************************************************
using System;
using System.Xml;

// ...

// Cargar el archivo XML en un objeto XmlDocument
XmlDocument doc = new XmlDocument();
doc.Load("ejemplo.xml");

// Obtener la lista de todos los nodos del documento
XmlNodeList nodos = doc.DocumentElement.ChildNodes;

// Iterar todos los nodos
foreach (XmlNode nodo in nodos)
{
    // Imprimir el nombre del nodo
    Console.WriteLine(nodo.Name);

    // Iterar los atributos del nodo (si tiene)
    if (nodo.Attributes != null)
    {
        foreach (XmlAttribute atributo in nodo.Attributes)
        {
            Console.WriteLine("{0}={1}", atributo.Name, atributo.Value);
        }
    }

    // Iterar los nodos hijos del nodo (si tiene)
    if (nodo.HasChildNodes)
    {
        foreach (XmlNode hijo in nodo.ChildNodes)
        {
            Console.WriteLine("{0}={1}", hijo.Name, hijo.InnerText);
        }
    }
}