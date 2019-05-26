namespace Hangman.DesktopClient.Models
{
    public struct HangmanGameRecord
    {
        public string Word { get; set; }
        public bool Won { get; set; }

        public HangmanGameRecord(string word, bool won)
        {
            Word = word;
            Won = won;
        }
    }
}
