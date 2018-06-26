using System.Collections.Generic;

namespace MundiPag.MessageTranslator.SharedKernel.Compositions.MinasGerais
{
    public class Region
    {
        public Region()
        {
            Cities = new List<City>();
        }

        public string Name { get; set; }
        public IList<City> Cities { get; set; }
    }
}
