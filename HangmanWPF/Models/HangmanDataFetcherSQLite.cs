using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace HangmanWPF.Models
{
    public class HangmanDataFetcherSQLite : IHangmanDataFetcher
    {
        private SQLiteConnection _Connection;

        private int _WordCount = -1;
        public int WordCount
        {
            get
            {
                if (_WordCount < 0)
                {
                    _WordCount = GetWordCount();
                }
                return _WordCount;
            }
        }

        private int _ImageSetCount = -1;

        private Stack<string> _CachedWords = new Stack<string>();

        public int ImageSetCount
        {
            get
            {
                if (_ImageSetCount < 0)
                {
                    _ImageSetCount = GetImageSetCount();
                }
                return _ImageSetCount;
            }
        }

        //Constructor
        public HangmanDataFetcherSQLite()
        {
            Initialize();
        }


        public string FetchRandomWord()
        {
            if (_CachedWords.Count < 1)
            {
                PopulateWordCache();
            }

            return _CachedWords.Pop().ToUpper();
        }

        public IEnumerable<byte[]> FetchRandomImageSet()
        {
            return GetRandomImageSet();
        }
       

        private void Initialize()
        {
            string connectionString = "Data Source=C:\\Users\\knubb\\OneDrive\\Egna projekt\\Git\\Repositories\\HangmanWPF\\HangmanWPF\\HangmanData\\HangmanDataBase.db;Version=3";

            _Connection = new SQLiteConnection(connectionString);
        }

        private int GetWordCount()
        {
            int res = -1;

            if (OpenConnection())
            {

                string q = "SELECT COUNT(*) FROM HangmanWords";

                SQLiteCommand cmd = new SQLiteCommand(q, _Connection);

                SQLiteDataReader reader = cmd.ExecuteReader();

                //Get returned value

                if (reader.Read())
                {
                    res = reader.GetInt32(0);
                }

                CloseConnection();
            }

            return res;
        }

        private int GetImageSetCount()
        {
            int res = -1;

            if (OpenConnection())
            {

                string q = "SELECT COUNT(*) FROM HangmanImageSets";

                SQLiteCommand cmd = new SQLiteCommand(q, _Connection);

                SQLiteDataReader reader = cmd.ExecuteReader();

                //Get returned value

                if (reader.Read())
                {
                    res = reader.GetInt32(0);
                }

                CloseConnection();
            }

            return res;
        }

        private void PopulateWordCache()
        {

            //Query the DB for words and cache them
            foreach (var word in GetRandomSetOfWords(100))
            {
                _CachedWords.Push(word);
            }
        }

        private IEnumerable<string> GetRandomSetOfWords(int size)
        {
            List<string> words = new List<string>();

            if (OpenConnection())
            {

                string q = $"SELECT * FROM HangmanWords ORDER BY random() LIMIT {size.ToString()};";

                SQLiteCommand cmd = new SQLiteCommand(q, _Connection);

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    words.Add(reader.GetString(0));

                }

                CloseConnection();
            }

            return words;
        }

        private IEnumerable<byte[]> GetRandomImageSet()
        {

            List<byte[]> imagedata = new List<byte[]>();

            if (OpenConnection())
            {

                string q = "SELECT * FROM HangmanImageSets ORDER BY random() LIMIT 1";

                SQLiteCommand cmd = new SQLiteCommand(q, _Connection);

                SQLiteDataReader reader = cmd.ExecuteReader();


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

                CloseConnection();
            }

            return imagedata;
        }

        private bool OpenConnection()
        {
            try
            {
                _Connection.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.ErrorCode)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                _Connection.Close();
                return true;
            }
            catch (SQLiteException)
            {
                throw;
            }
        }

        //public void InsertImageSet(List<byte[]> images)
        //{

        //    if (OpenConnection())
        //    {

        //        string cmnd = $"INSERT INTO HangmanImageSets (ID, Image0, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8)" +
        //                        $"VALUES ( @ID, @Image0, @Image1, @Image2, @Image3, @Image4, @Image5, @Image6, @Image7, @Image8 )";

        //        SQLiteCommand command = new SQLiteCommand(cmnd, _Connection);

        //        command.Parameters.Add("ID", DbType.Int16).Value = 1;

        //        command.Parameters.Add("Image0", DbType.Binary).Value = images[0];
        //        command.Parameters.Add("Image1", DbType.Binary).Value = images[1];
        //        command.Parameters.Add("Image2", DbType.Binary).Value = images[2];
        //        command.Parameters.Add("Image3", DbType.Binary).Value = images[3];
        //        command.Parameters.Add("Image4", DbType.Binary).Value = images[4];
        //        command.Parameters.Add("Image5", DbType.Binary).Value = images[5];
        //        command.Parameters.Add("Image6", DbType.Binary).Value = images[6];
        //        command.Parameters.Add("Image7", DbType.Binary).Value = images[7];
        //        command.Parameters.Add("Image8", DbType.Binary).Value = images[8];

        //        command.ExecuteNonQuery();

        //        CloseConnection();
        //    }
        //}
    }
}
