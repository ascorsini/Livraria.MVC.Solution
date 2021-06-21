using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.MVC.Models
{
    public class LivroViewModel
    {
        public int ISBN { get; set; }
        public string Autor { get; set; }
        public string Nome { get; set; }
        public string Preco { get; set; }
        public string DataPublicacao { get; set; }
        public string ImagemCapa { get; set; }

        public List<LivroViewModel> ListaLivros { get; set; }
    }
}
