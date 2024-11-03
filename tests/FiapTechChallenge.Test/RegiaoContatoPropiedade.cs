using Core.Entity;

namespace FiapTechChallenge.Test
{
    public class RegiaoContatoPropiedade
    {
        [Theory(DisplayName = "Retorna propriedades da classe Regiao Contato")]
        [Trait("RegiaoContato", "Validando a classe")]
        [InlineData(11, "Sudeste", "SP")]        
        public void RetornaPropriedadesDaClasseRegiaoContatoPropiedade(int ddd, string regiao, string estado)
        {
            //arrange

            //ação - act
            RegiaoContato regiaoContato = new RegiaoContato()
            {
                DDD = ddd,
                Estado = estado,
                Regiao = regiao
            };

            //validação - assert            
            Assert.Equal(ddd, regiaoContato.DDD);
            Assert.Equal(estado, regiaoContato.Estado);
            Assert.Equal(regiao, regiaoContato.Regiao);
        }
    }
}