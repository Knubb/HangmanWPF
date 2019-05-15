using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace HangmanWPF.Models
{
    public class ImageSetRepository_SQLite : IImageSetRepository
    {
        private const string _ConnectionString = "Data Source =.\\HangmanData\\HangmanDataBase.db;Version=3";

        public IEnumerable<IEnumerable<byte[]>> FetchAllImageSets()
        {
            List<List<byte[]>> imagesetsdata = new List<List<byte[]>>();

            using (SQLiteConnection connection = new SQLiteConnection(_ConnectionString))
            {
                try
                {
                    connection.Open();

                    string q = "SELECT * FROM HangmanImageSets";

                    using (SQLiteCommand cmd = new SQLiteCommand(q, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<byte[]> current = new List<byte[]>();

                                current.Add((byte[])reader["Image0"]);
                                current.Add((byte[])reader["Image1"]);
                                current.Add((byte[])reader["Image2"]);
                                current.Add((byte[])reader["Image3"]);
                                current.Add((byte[])reader["Image4"]);
                                current.Add((byte[])reader["Image5"]);
                                current.Add((byte[])reader["Image6"]);
                                current.Add((byte[])reader["Image7"]);
                                current.Add((byte[])reader["Image8"]);

                                imagesetsdata.Add(current);
                            }
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }

            return imagesetsdata;
        }

        public IEnumerable<byte[]> FetchRandomImageSet()
        {
            List<byte[]> imagedata = new List<byte[]>();

            using (SQLiteConnection connection = new SQLiteConnection(_ConnectionString))
            {
                try
                {
                    connection.Open();

                    string q = "SELECT * FROM HangmanImageSets ORDER BY random() LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(q, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                imagedata.Add((byte[])reader["Image0"]);
                                imagedata.Add((byte[])reader["Image1"]);
                                imagedata.Add((byte[])reader["Image2"]);
                                imagedata.Add((byte[])reader["Image3"]);
                                imagedata.Add((byte[])reader["Image4"]);
                                imagedata.Add((byte[])reader["Image5"]);
                                imagedata.Add((byte[])reader["Image6"]);
                                imagedata.Add((byte[])reader["Image7"]);
                                imagedata.Add((byte[])reader["Image8"]);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return imagedata;
        }

        public void InsertImageSet(IList<byte[]> images)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_ConnectionString))
            {
                try
                {
                    connection.Open();

                    string q = $"INSERT INTO HangmanImageSets (ID, Image0, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8)" +
                                    $"VALUES ( @ID, @Image0, @Image1, @Image2, @Image3, @Image4, @Image5, @Image6, @Image7, @Image8 )";

                    using (SQLiteCommand cmd = new SQLiteCommand(q, connection))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {

                            cmd.Parameters.Add("ID", DbType.Int32).Value =  new Random().Next();

                            cmd.Parameters.Add("Image0", DbType.Binary).Value = images[0];
                            cmd.Parameters.Add("Image1", DbType.Binary).Value = images[1];
                            cmd.Parameters.Add("Image2", DbType.Binary).Value = images[2];
                            cmd.Parameters.Add("Image3", DbType.Binary).Value = images[3];
                            cmd.Parameters.Add("Image4", DbType.Binary).Value = images[4];
                            cmd.Parameters.Add("Image5", DbType.Binary).Value = images[5];
                            cmd.Parameters.Add("Image6", DbType.Binary).Value = images[6];
                            cmd.Parameters.Add("Image7", DbType.Binary).Value = images[7];
                            cmd.Parameters.Add("Image8", DbType.Binary).Value = images[8];

                            cmd.ExecuteNonQuery();
                        }
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
