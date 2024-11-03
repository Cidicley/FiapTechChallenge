using Core.Models;

namespace FiapTechChallengeApi.Interfaces
{
    public interface ITokenService
    {
        public string GetToken(Usuario usuario);
    }
}
