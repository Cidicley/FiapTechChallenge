using System.ComponentModel.DataAnnotations;
using Core.Entity.Base;

namespace Core.Entity
{
    public class Contato : EntityBase
    {
        [Required]
        [DeniedValues("string", "", null, ErrorMessage = "Necessário informar um valor para o campo Nome")]
        public string Nome { get; set; }
        [Required]
        public virtual RegiaoContato RegiaoContato { get; set; }
        [Required]
        public int Telefone { get; set; }
        [Required]
        [DeniedValues("string", "", null, ErrorMessage = "Necessário informar um valor para o campo e-mail")]
        public string Email { get; set; }
    }
}