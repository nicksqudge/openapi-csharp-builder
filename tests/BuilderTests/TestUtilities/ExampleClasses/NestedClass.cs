using System;
using System.Collections.Generic;

namespace BuilderTests.TestUtilities.ExampleClasses
{
    public class NestedClass
    {
        public List<SinglePropertyClass> Single { get; set; }
        public MultiPropertyClass Multi { get; set; }
    }
}
