using System.ComponentModel.DataAnnotations;

namespace Core.Input
{
    public class ContatoInput
    {
        [Required]
        [DeniedValues("string", "", null, ErrorMessage = "Necessário informar um valor para o campo Nome")]
        public string Nome { get; set; }
        /// <summary>
        /// Valores 10 e 30 são aceitos
        /// </summary>
        [Required]        
        [Range(minimum: 11, maximum: 99, ErrorMessage = "Necessário informar um DDD válido")]
        public int DDD { get; set; }
        [Required]
        [DeniedValues("string", "", null, ErrorMessage = "Necessário informar um valor para o campo Telefone")]
        public string Telefone { get; set; }
        [Required]
        [DeniedValues("string", "", null, ErrorMessage = "Necessário informar um valor para o campo e-mail")]
        public string Email { get; set; }
    }
}

