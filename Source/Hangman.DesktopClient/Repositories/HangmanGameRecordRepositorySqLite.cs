using HangmanWPF.Interfaces;
using HangmanWPF.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace HangmanWPF.Repositories
{
    public class HangmanGameRecordRepositorySqLite : IRepository<HangmanGameRecord>
    {
        private const string ConnectionString = "Data Source =.\\HangmanData\\HangmanDataBase.db;Version=3";

        public IEnumerable<HangmanGameRecord> Get()
        {
            var records = new List<HangmanGameRecord>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                const string q = "SELECT * FROM HangmanGameHistory";
                using (var cmd = new SQLiteCommand(q, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var word = reader.GetString(0);
                            var won = reader.GetBoolean(1);

                            records.Add(new HangmanGameRecord(word, won));
                        }
                    }
                }
            }

            return records;
        }

        public void Create(HangmanGameRecord record)
        {

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var q = $"INSERT INTO HangmanGameHistory (Word, Won) VALUES (@Word, @Won)";
                using (var cmd = new SQLiteCommand(q, connection))
                {
                    cmd.Parameters.Add("Word", DbType.String).Value = record.Word;
                    cmd.Parameters.Add("Won", DbType.Boolean).Value = record.Won;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
