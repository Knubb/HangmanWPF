using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace HangmanWPF.Models
{
    public class WordRepository_SQLite : IWordRepository
    {
        private const string _ConnectionString = "Data Source =.\\HangmanData\\HangmanDataBase.db;Version=3";

        public IEnumerable<string> FetchRandomSetOfWords(int size)
        {
            List<string> words = new List<string>();

            using (SQLiteConnection connection = new SQLiteConnection(_ConnectionString))
            {
                try
                {
                    connection.Open();

                    string q = $"SELECT * FROM HangmanWords ORDER BY random() LIMIT {size.ToString()};";

                    using (SQLiteCommand cmd = new SQLiteCommand(q, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                words.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return words;
        }
    }
}
