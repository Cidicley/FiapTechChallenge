using Core.Entity;

namespace FiapTechChallenge.Test
{
    public class ContatoPropiedade
    {
        [Theory(DisplayName = "Retorna propriedades da classe Contato")]
        [Trait("Contato", "Validando a classe")]
        [InlineData("Nome",11, 111111111, "teste@teste.com", "Sudeste", "SP")]        
        public void RetornaPropriedadesDaClasseContato(string nome, int ddd, int telefone, string email, string regiao, string estado)
        {
            //arrange

            //ação - act
            Contato contato = new Contato()
            {
                Nome = nome,
                Email = email,
                Telefone = telefone,
                RegiaoContato = new RegiaoContato
                {
                    DDD = ddd,
                    Estado = estado,
                    Regiao = regiao
                }
            };

            //validação - assert
            Assert.Equal(nome, contato.Nome);
            Assert.Equal(email, contato.Email);
            Assert.Equal(telefone, contato.Telefone);
            Assert.Equal(ddd, contato.RegiaoContato.DDD);
            Assert.Equal(estado, contato.RegiaoContato.Estado);
            Assert.Equal(regiao, contato.RegiaoContato.Regiao);
        }        
    }
}