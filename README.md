# Simple.Xml ![Build status](https://ci.appveyor.com/api/projects/status/v13ag0vi4gvijfeb?svg=true)
Library for dynamically creating XML structure.

# Motivation
I've created this library because in one project I faced issue with creating many LinqToXml elements. It is inspired by [Simple.Data](https://github.com/markrendle/Simple.Data) great library.

# Examples of usage

* `XML` string creation :

Following:
```csharp
var builder = new DynamicXmlBuilder();
var document = builder.NewDocument;
var body = doc.Head.Body;
body.Element();
body.Element();
body.Element = "content";

var xml = document.ToXml();
```

creates :

```
<Head>
    <Body>
        <Element>
        </Element>
        <Element>
        </Element>
        <Element>
            content
        </Element>
    </Body>
</Head>
```

* `XElement` creation:

Following:
```csharp
var builder = new DynamicXmlBuilder(new Namespaces { { "c", "http://www.w3.org" } });
var document = builder.NewDocument;
document.c_Head(new Attributes {{"c_attr", "value"}}).c_Body = "content";

var xElement = document.ToXElement();
```

is equivalent to:

```csharp
var xElement = new XElement(XName.Get("Head", "http://www.w3.org"), 
				new XAttribute(XName.Get("attr", "http://www.w3.org"), value)
				new XElement(XName.Get("Body", "http://www.w3.org"), "content"));
```
