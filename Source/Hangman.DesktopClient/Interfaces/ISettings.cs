namespace Hangman.DesktopClient.Interfaces
{
    public interface ISettings
    {
        /// <summary>
        /// Saves this objects members
        /// </summary>
        void Save();

        /// <summary>
        /// Poppulate this objects members with the saved values
        /// </summary>
        void LoadFromSource();
    }
}
