using System.Collections.Generic;

namespace MundiPag.MessageTranslator.SharedKernel.Compositions.Rio
{
    public class Cidade
    {
        public Cidade()
        {
            Bairros = new List<Bairro>();
        }

        public string Nome { get; set; }

        public int Populacao { get; set; }

        public IList<Bairro> Bairros { get; set; }
    }
}
