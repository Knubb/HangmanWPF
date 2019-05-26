using Hangman.DesktopClient.Interfaces;
using Hangman.DesktopClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Hangman.DesktopClient.Repositories
{
    public class HangmanGameRecordRepositorySqLite : IGameRecordRepository
    {
        private const string _ConnectionString = "Data Source =.\\HangmanData\\HangmanDataBase.db;Version=3";

        public IEnumerable<HangmanGameRecord> GetAll()
        {
            List<HangmanGameRecord> records = new List<HangmanGameRecord>();

            using (SQLiteConnection connection = new SQLiteConnection(_ConnectionString))
            {
                try
                {
                    connection.Open();

                    string q = "SELECT * FROM HangmanGameHistory";

                    using (SQLiteCommand cmd = new SQLiteCommand(q, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var word = reader.GetString(0);
                                bool won = reader.GetBoolean(1);

                                records.Add(new HangmanGameRecord(word, won));
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return records;
        }

        public void Create(HangmanGameRecord record)
        {

            using (SQLiteConnection connection = new SQLiteConnection(_ConnectionString))
            {
                try
                {
                    connection.Open();

                    string q = $"INSERT INTO HangmanGameHistory (Word, Won) VALUES (@Word, @Won)";

                    using (SQLiteCommand cmd = new SQLiteCommand(q, connection))
                    {
                        cmd.Parameters.Add("Word", DbType.String).Value = record.Word;
                        cmd.Parameters.Add("Won", DbType.Boolean).Value = record.Won;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
