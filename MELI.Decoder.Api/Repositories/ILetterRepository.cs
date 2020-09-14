using System.Threading.Tasks;

namespace MELI.Decoder.Api.Repositories
{
    public interface ILetterRepository
    {
        Letter GetLetterByMorse(string morse);
        Letter GetLetterByName(string name);
    }
}
