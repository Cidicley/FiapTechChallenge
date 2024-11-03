namespace Core.Input.Dto
{
    public class ContatoDto
    {
        public int Id { get; set; }        
        public string Nome { get; set; }
        public RegiaoContatoDto RegiaoContato { get; set; }
        public int Telefone { get; set; }
        public string Email { get; set; }
    }
}
