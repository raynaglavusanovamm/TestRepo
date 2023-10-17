﻿using FluentAssertions.Equivalency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpLatestRayna.Dropbox.Model
{
    public class PropertyGroup
    {
        public List<Field> fields { get; set; }
        public string template_id { get; set; }
    }
}
