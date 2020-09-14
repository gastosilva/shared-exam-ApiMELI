using MELI.Decoder.Api.Repositories;
using MELI.Decoder.Api.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MELI.Decoder.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertToHumanTextTest()
        {
            string text = "HOLA MELI";
            string morseCode = ".... --- .-.. .-   -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string textConverted = decoder.Translate2Human(morseCode);
            Assert.AreEqual(text, textConverted);
        }

        [Test]
        public void ConvertToHumanTextTest2()
        {
            string text = "HOLAMELI";
            string morseCode = ".... --- .-.. .- -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string textConverted = decoder.Translate2Human(morseCode);
            Assert.AreEqual(text, textConverted);
        }

        [Test]
        public void ConvertToMorseCodeTest()
        {
            string text = "HOLA MELI";
            string morseCode = ".... --- .-.. .-   -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string morseConverted = decoder.Translate2Morse(text);
            Assert.AreEqual(morseCode, morseConverted);
        }

        [Test]
        public void ConvertToMorseCodeTest2()
        {
            string text = "HOLAMELI";
            string morseCode = ".... --- .-.. .- -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string morseConverted = decoder.Translate2Morse(text);
            Assert.AreEqual(morseCode, morseConverted);
        }

        [Test]
        public void ConvertToHumanTextWithMoreThanOneSpaceTest()
        {
            string text = "HOLA   MELI";
            string morseCode = ".... --- .-.. .-       -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string morseConverted = decoder.Translate2Morse(text);
            Assert.AreEqual(morseCode, morseConverted);
        }

        [Test]
        public void DecodeBitsToMorseTest()
        {
            string bits = "000000001101101100111000001111110001111110011111100000001110111111110111011100000001100011111100000111111001111110000000110000110111111110111011100000011011100000000000";
            string morseCode = ".... --- .-.. .- -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string morseConverted = decoder.DecodeBits2Morse(bits);
            Assert.AreEqual(morseCode, morseConverted);
        }

        [Test]
        public void DecodeBitsToMorseTest2()
        {
            string bits = "000000001101110110011100000111111000111111001111110000000111011111111011101110000000110001111110000001111110011111100000001100000110111111110111011100000011011100000000000";
            string morseCode = ".... --- .-.. .- -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string morseConverted = decoder.DecodeBits2Morse(bits);
            Assert.AreEqual(morseCode, morseConverted);
        }

        [Test]
        public void DecodeBitsToMorseTest3()
        {
            string bits = "00000000000000001101110110011100000011111100011111100111111000000011101111111101110111000000011000111111100000000111111001111111000000011000001101111111101110111000000110111000000000000000000000";
            string morseCode = ".... --- .-.. .-   -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string morseConverted = decoder.DecodeBits2Morse(bits);
            Assert.AreEqual(morseCode, morseConverted);
        }

        [Test]
        public void DecodeBitsToMorseTest4()
        {
            string bits = "000000000000000011110111011001110000001111110001111110011111100000001110111111110111011100000001100011111110000000000000011111100111111100000001110000011011111111011101111000000110111000000000000000000000";
            string morseCode = ".... --- .-.. .-   -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string morseConverted = decoder.DecodeBits2Morse(bits);
            Assert.AreEqual(morseCode, morseConverted);
        }

        [Test]
        public void DecodeBitsToMorseTest5()
        {
            string bits = "000000000000000010111011001110000001111110001111110011111100000001110111111110111011100000001110001111100000000000000111111001111111000000010000011011111111011101110000001110111000000000000000000000";
            string morseCode = ".... --- .-.. .-   -- . .-.. ..";
            LetterRepository letterRepository = new LetterRepository();
            DecoderService decoder = new DecoderService(letterRepository);
            string morseConverted = decoder.DecodeBits2Morse(bits);
            Assert.AreEqual(morseCode, morseConverted);
        }
    }
}