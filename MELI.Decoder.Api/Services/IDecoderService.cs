using System.Threading.Tasks;

namespace MELI.Decoder.Api.Services
{
    public interface IDecoderService
    {
        string DecodeBits2Morse(string bits);
        string Translate2Human(string morseText);
        string Translate2Morse(string text);
    }
}