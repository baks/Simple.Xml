using System;
using Simple.Xml.Structure;
using Simple.Xml.Structure.Constructs;

namespace Simple.Xml.Dynamic
{
    public class DynamicXmlBuilder
    {
        private static Func<BaseDynamicElement, BaseDynamicElement> DefaultGraphDecorator => element => element;

        private static readonly Func<Namespaces, Func<BaseDynamicElement, BaseDynamicElement>, BaseDynamicElement>
            DefaultGraph = (namespaces, graphDecorator) => new DynamicToXmlForwardHandler(
                new DynamicElement(new RootElement(namespaces, new ElementCollector()), new ElementFactory(),
                    graphDecorator));

        private readonly Func<BaseDynamicElement, BaseDynamicElement> graphDecorator;
        private readonly Namespaces namespaces;

        public DynamicXmlBuilder() : this(Namespaces.EmptyNamespaces, DefaultGraphDecorator)
        {
            
        }

        public DynamicXmlBuilder(Namespaces namespaces) : this(namespaces, DefaultGraphDecorator)
        {
        }

        public DynamicXmlBuilder(Func<BaseDynamicElement, BaseDynamicElement> graphDecorator)
            : this(Namespaces.EmptyNamespaces, graphDecorator)
        {
        }

        public DynamicXmlBuilder(Namespaces namespaces, Func<BaseDynamicElement, BaseDynamicElement> graphDecorator)
        {
            if (namespaces == null)
            {
                throw new ArgumentNullException(nameof(namespaces));
            }
            if (graphDecorator == null)
            {
                throw new ArgumentNullException(nameof(graphDecorator));
            }
            this.namespaces = namespaces;
            this.graphDecorator = graphDecorator;
        }

        public dynamic NewDocument => graphDecorator(DefaultGraph(this.namespaces, graphDecorator));
    }
}
