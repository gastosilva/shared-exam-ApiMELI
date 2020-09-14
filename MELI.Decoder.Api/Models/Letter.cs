namespace MELI.Decoder.Api
{
    public class Letter
    {
        public string Name { get; set; }

        public string MorseCode { get; set; }

        public Letter(string nameInit, string morseCodeInit)
        {
            Name = nameInit;
            MorseCode = morseCodeInit;
        }
    }
}