﻿using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace Simple.Xml.Structure.UnitTests
{
    public class AutoSubstituteData : AutoDataAttribute
    {
        public AutoSubstituteData() : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
            
        }
    }
}
