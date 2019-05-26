using Hangman.DesktopClient.Interfaces;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Hangman.DesktopClient.Repositories
{
    public class WordRepositorySqLite : IWordRepository
    {
        private const string ConnectionString = "Data Source =.\\HangmanData\\HangmanDataBase.db;Version=3";

        public IEnumerable<string> GetRandomSet(int size)
        {
            var words = new List<string>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var q = $"SELECT * FROM HangmanWords ORDER BY random() LIMIT {size.ToString()};";
                using (var cmd = new SQLiteCommand(q, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            words.Add(reader.GetString(0));
                        }
                    }
                }

            }

            return words;
        }
    }
}
