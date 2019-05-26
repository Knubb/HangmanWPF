using Hangman.DesktopClient.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Hangman.DesktopClient.Repositories
{
    public class ImageSetRepositorySqLite : IImageSetRepository
    {
        private const string ConnectionString = "Data Source =.\\HangmanData\\HangmanDataBase.db;Version=3";
        private static readonly IList<string> _images = new[]
        {
            "Image0", "Image1", "Image2", "Image3", "Image4",
            "Image5", "Image6", "Image7", "Image8"
        };


        public IEnumerable<IEnumerable<byte[]>> GetAll()
        {
            var imagesetsdata = new List<List<byte[]>>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                const string q = "SELECT * FROM HangmanImageSets";
                using (var cmd = new SQLiteCommand(q, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        LoadImages(reader, imagesetsdata);
                    }
                }
            }

            return imagesetsdata;
        }

        private static void LoadImages(SQLiteDataReader reader, List<List<byte[]>> imagesetsdata)
        {
            while (reader.Read())
            {
                var current = new List<byte[]>();

                foreach (var image in _images)
                {
                    current.Add((byte[])reader[image]);
                }

                imagesetsdata.Add(current);
            }
        }

        public IEnumerable<byte[]> GetRandom()
        {
            var imagedata = new List<byte[]>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var q = "SELECT * FROM HangmanImageSets ORDER BY random() LIMIT 1";
                using (var cmd = new SQLiteCommand(q, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foreach (var image in _images)
                            {
                                imagedata.Add((byte[])reader[image]);
                            }
                        }
                    }
                }
            }

            return imagedata;
        }

        public void Create(IList<byte[]> images)
        {

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var q = $"INSERT INTO HangmanImageSets (Image0, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8)" +
                                $"VALUES (@Image0, @Image1, @Image2, @Image3, @Image4, @Image5, @Image6, @Image7, @Image8 )";

                using (var cmd = new SQLiteCommand(q, connection))
                {
                    for (var i = 0; i < images.Count; i++)
                    {
                        cmd.Parameters.Add(_images[i], DbType.Binary).Value = images[i];
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
