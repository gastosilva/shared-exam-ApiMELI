using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MELI.Decoder.Api.Repositories
{
    /// <summary>
    /// Repositorio de Letras.
    /// </summary>
    public class LetterRepository : ILetterRepository
    {
        List<Letter> Letters = new List<Letter>();
        public LetterRepository()
        {
            Letters.Add(new Letter("a", ".-"));
            Letters.Add(new Letter("b", "-..."));
            Letters.Add(new Letter("c", "-.-."));
            Letters.Add(new Letter("d", "-.."));
            Letters.Add(new Letter("e", "."));
            Letters.Add(new Letter("f", "..-."));
            Letters.Add(new Letter("g", "--."));
            Letters.Add(new Letter("h", "...."));
            Letters.Add(new Letter("i", ".."));
            Letters.Add(new Letter("j", ".---"));
            Letters.Add(new Letter("k", "-.-"));
            Letters.Add(new Letter("l", ".-.."));
            Letters.Add(new Letter("m", "--"));
            Letters.Add(new Letter("n", "-."));
            Letters.Add(new Letter("o", "---"));
            Letters.Add(new Letter("p", ".--."));
            Letters.Add(new Letter("q", "--.-"));
            Letters.Add(new Letter("r", ".-."));
            Letters.Add(new Letter("s", "..."));
            Letters.Add(new Letter("t", "-"));
            Letters.Add(new Letter("u", "..-"));
            Letters.Add(new Letter("v", "...-"));
            Letters.Add(new Letter("w", ".--"));
            Letters.Add(new Letter("x", "-..-"));
            Letters.Add(new Letter("y", "-.--"));
            Letters.Add(new Letter("z", "--.."));
            Letters.Add(new Letter("0", "-----"));
            Letters.Add(new Letter("1", ".-----"));
            Letters.Add(new Letter("2", "..---"));
            Letters.Add(new Letter("3", "...--"));
            Letters.Add(new Letter("4", "....-"));
            Letters.Add(new Letter("5", "....."));
            Letters.Add(new Letter("6", "-...."));
            Letters.Add(new Letter("7", "--..."));
            Letters.Add(new Letter("8", "---.."));
            Letters.Add(new Letter("9", "----."));
            Letters.Add(new Letter(" ", " "));
        }

        /// <summary>
        /// Obtiene un objeto de la letra a partir del símbolo en Código Morse.
        /// </summary>
        /// <param name="morse">Símbolo en Código Morse</param>
        /// <returns>Objeto de la letra.</returns>
        public Letter GetLetterByMorse(string morse)
        {
            return Letters.Where(l => l.MorseCode == morse).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene un objeto de la letra a partir del caracter en idioma humano.
        /// </summary>
        /// <param name="name">Caracter de la letra.</param>
        /// <returns>Objeto de la letra.</returns>
        public Letter GetLetterByName(string name)
        {
            return Letters.Where(l => l.Name == name.ToLower()).FirstOrDefault();
        }
    }
}
