using System.ComponentModel.DataAnnotations;
using Core.Entity.Base;

namespace Core.Entity
{
    public class RegiaoContato : EntityBase
    {
        [Required]
        /// <summary>
        /// Valores 10 e 30 são aceitos
        /// </summary>
        [Range(minimum: 11, maximum: 99)]
        public int DDD { get; set; }
        [Required]
        [DeniedValues("string", "", null, ErrorMessage = "Necessário informar um valor para o campo Região")]
        public string Regiao { get; set; }
        [Required]
        [DeniedValues("string", "", null, ErrorMessage = "Necessário informar um valor para o campo Estado")]
        public string Estado { get; set; }
    }
}