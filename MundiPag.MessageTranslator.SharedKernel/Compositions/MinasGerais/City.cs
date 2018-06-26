﻿using System.Collections.Generic;

namespace MundiPag.MessageTranslator.SharedKernel.Compositions.MinasGerais
{
    public class City
    {
        public City()
        {
            Neighborhoods = new List<Neighborhood>();
        }

        public string Name { get; set; }

        public int Population { get; set; }

        public IList<Neighborhood> Neighborhoods { get; set; }
    }
}
