namespace MELI.Decoder.Api
{
    public class Pattern
    {
        public int Dot { get; set; }

        public int Pause { get; set; }

        public int PauseLetter { get; set; }

        public Pattern(int dot, int pause, int pauseLetter)
        {
            Dot = dot;
            Pause = pause;
            PauseLetter = pauseLetter;
        }

    }
}
