using MELI.Decoder.Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MELI.Decoder.Api.Services
{
    /// <summary>
    /// Servicio de Decodificación de Mensajes.
    /// </summary>
    public class DecoderService : IDecoderService
    {
        private ILetterRepository letterRepository { get; set; }
        public DecoderService(ILetterRepository letterRepositoryInit)
        {
            letterRepository = letterRepositoryInit;
        }

        /// <summary>
        /// Convierte un mensaje en bits a texto en Código Morse.
        /// </summary>
        /// <param name="bits">Texto escrito en bits.</param>
        /// <returns>Texto escrito en Código Morse.</returns>
        public string DecodeBits2Morse(string bits)
        {
            Pattern pattern = LearnPattern(bits);
            string morseCode = DecodeBitsFromPattern(bits, pattern);
            return morseCode;
        }

        /// <summary>
        /// Traduce un texto escrito en Código Morse a idioma humano.
        /// </summary>
        /// <param name="morseText">Texto escrito en Código Morse.</param>
        /// <returns>Texto escrito en idioma humano.</returns>
        public string Translate2Human(string morseText)
        {
            string humanText = "";
            List<string> wordsSplitted = SplitWithSpaces(morseText);
            for (int i = 0; i < wordsSplitted.Count; i++)
            {
                string word = wordsSplitted[i];
                Letter letter = letterRepository.GetLetterByMorse(word.ToString());
                if (letter != null)
                {
                    humanText += letter.Name;
                }
                else
                {
                    return "";
                }
            }
            return humanText.ToUpper();
        }

        /// <summary>
        /// Traduce un texto escrito en idioma humano a Código Morse.
        /// </summary>
        /// <param name="text">Texto escrito en idioma humano.</param>
        /// <returns>Texto escrito en Código Morse.</returns>
        public string Translate2Morse(string text)
        {
            string morseCode = "";
            for (int i = 0; i < text.Length; i++)
            {
                Letter letter = letterRepository.GetLetterByName(text[i].ToString());
                morseCode += letter?.MorseCode ?? "?";
                morseCode += " ";
            }
            return morseCode.TrimEnd();
        }

        /// <summary>
        /// Identifica el patrón a utilizar para decodificar el texto escrito en bits.
        /// </summary>
        /// <param name="bits">Texto escrito en bits.</param>
        /// <returns>Objeto del patrón para decodificar los bits.</returns>
        private Pattern LearnPattern(string bits)
        {
            List<int> zerosAppearence;
            List<int> onesAppearence;
            (zerosAppearence, onesAppearence) = GetZerosOnesAppearencesFromBits(bits);
            Pattern pattern = MakePatternWithAppearences(zerosAppearence, onesAppearence);
            return pattern;
        }

        /// <summary>
        /// Arma el patrón a partir de apariciones de ceros y unos.
        /// </summary>
        /// <param name="zerosAppearence">Listado de apariciones de ceros.</param>
        /// <param name="onesAppearence">Listado de apariciones de unos.</param>
        /// <returns>Objeto de patrón de identificación.</returns>
        private Pattern MakePatternWithAppearences(List<int> zerosAppearence, List<int> onesAppearence)
        {
            Pattern pattern = new Pattern(0, 0, 0);
            int startLimitDot = onesAppearence.Min();
            int startLimitDash = startLimitDot + 3; //Los guiones duran tres veces la duración de un punto.
            int endLimitDot = onesAppearence.Where(n => n < startLimitDash).Max();

            pattern.Dot = endLimitDot;

            int startLimitPause = zerosAppearence.Min();
            int startLimitLetter = startLimitPause + 3;//Las pausas entre letras duran tres veces más que la duración de una pausa entre símbolos.
            int startLimitWord = startLimitPause + 7;//Las pausas entre palabras duran siete veces más que la duración de una pausa entre símbolos.
            int endLimitLetter = zerosAppearence.Where(n => n < startLimitWord).Max();
            int endLimitPause = zerosAppearence.Where(n => n < startLimitLetter).Max();

            pattern.Pause = endLimitPause;
            pattern.PauseLetter = endLimitLetter;
            return pattern;
        }

        /// <summary>
        /// Obtiene las apariciones de ceros y unos de una cadena de bits.
        /// </summary>
        /// <param name="bits">Cadena de bits.</param>
        /// <returns>Tupla de listados de apariciones de ceros y de unos.</returns>
        private (List<int>, List<int>) GetZerosOnesAppearencesFromBits(string bits)
        {
            char bit;
            int contZeros = 0;
            int contOnes = 0;
            List<int> onesAppearence = new List<int>();
            List<int> zerosAppearence = new List<int>();

            //Obtengo las distintas cantidades de apariciones de ceros y unos.
            for (int i = 0; i < bits.Length; i++)
            {
                bit = bits[i];
                if (bit == '0')
                {
                    contZeros++;
                    if (contOnes > 0)
                    {
                        onesAppearence.Add(contOnes);
                    }
                    contOnes = 0;
                }
                else
                {
                    contOnes++;
                    if (contZeros > 0)
                    {
                        zerosAppearence.Add(contZeros);
                    }
                    contZeros = 0;
                }
            }
            zerosAppearence.RemoveAt(zerosAppearence.Count - 1); //Remuevo la aparición de la finalización de mensaje, para no considerarla en el patrón.
            zerosAppearence.RemoveAt(0);//Remuevo la primera aparición que representa el comienzo del mensaje.
            return (zerosAppearence, onesAppearence);
        }

        /// <summary>
        /// A partir del patrón, traduce el texto escrito en bits a Código Morse.
        /// </summary>
        /// <param name="bits">Texto escrito en bits.</param>
        /// <param name="pattern">Objeto del patrón de identificación.</param>
        /// <returns>Texto escrito en Código Morse.</returns>
        private string DecodeBitsFromPattern(string bits, Pattern pattern)
        {
            string morseCode = "";
            int dotLimit = pattern.Dot;
            int pauseLimit = pattern.Pause;
            int pauseLetterLimit = pattern.PauseLetter;
            int contZeros = 0;
            int contOnes = 0;
            char bit;
            for (int i = 0; i < bits.Length; i++)
            {
                bit = bits[i];
                if (bit == '0')
                {
                    contZeros++;
                    if (contOnes > 0)
                    {
                        if (contOnes <= dotLimit)
                        {
                            morseCode += ".";
                        }
                        else
                        {
                            morseCode += "-";
                        }
                    }
                    contOnes = 0;
                }
                else
                {
                    contOnes++;
                    if (contZeros > 0)
                    {
                        if (contZeros > pauseLimit)
                        {
                            if (contZeros <= pauseLetterLimit)
                            {
                                morseCode += " ";
                            }
                            else
                            {
                                morseCode += "   ";
                            }
                        }
                    }
                    contZeros = 0;
                }
            }
            return morseCode.Trim();
        }

        /// <summary>
        /// Split del texto considerando al espacio como un caracter más.
        /// </summary>
        /// <param name="text">Texto a dividir.</param>
        /// <returns>Lista de los strings divididos.</returns>
        private List<string> SplitWithSpaces(string text)
        {
            string[] wordsSplitted = text.Split(' ');
            List<string> wordsSplittedWithSpace = new List<string>();
            bool previousSpace = false;
            for (int i = 0; i < wordsSplitted.Length; i++)
            {
                string word = wordsSplitted[i];
                if (word == "")
                {
                    if (previousSpace)
                    {
                        wordsSplittedWithSpace.Add(" ");
                        previousSpace = false;
                    }
                    else
                    {
                        previousSpace = true;
                    }
                }
                else
                {
                    wordsSplittedWithSpace.Add(word);
                }
            }
            return wordsSplittedWithSpace;
        }
    }
}