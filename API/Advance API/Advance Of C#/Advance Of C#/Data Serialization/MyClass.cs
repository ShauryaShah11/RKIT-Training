﻿using System;

namespace Advance_Of_C_.Data_Serialization
{
    [Serializable]
    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NonSerialized] // This field will not be serialized
        public string SensitiveData;
    }
}
