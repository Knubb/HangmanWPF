using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace HangmanWPF.Models
{
    public class HangmanDataFetcherSQLite : IHangmanDataFetcher, IImagSetUploader, IGameHistoryFetcher, IGameRecordUploader
    {

        private const string _ConnectionString = "Data Source =.\\HangmanData\\HangmanDataBase.db;Version=3";
        private const int _ImageSetSize = 9;

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

        private Stack<string> _CachedWords = new Stack<string>();

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

        public IEnumerable<byte[]> FetchRandomImageSetData()
        {
            return GetRandomImageSet();
        }

        public IEnumerable<IEnumerable<byte[]>> FetchAllImageSetsAsData()
        {
            return GetAllImageSets();
        }

        private void Initialize()
        {
            _Connection = new SQLiteConnection(_ConnectionString);
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

                cmd.Dispose();
                reader.Close();
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

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }

            return res;
        }

        public void FetchImageSetByID(int id)
        {
            GetImageSetByID(id);
        }

        private IEnumerable<byte[]> GetImageSetByID(int id)
        {

            List<byte[]> imagedata = new List<byte[]>();

            if (OpenConnection())
            {

                string q = $"SELECT * FROM HangmanImageSets WHERE ID {id}";

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

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }

            return imagedata;
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

                cmd.Dispose();
                reader.Close();
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

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }

            return imagedata;
        }

        private IEnumerable<IEnumerable<byte[]>> GetAllImageSets()
        {
            
            List<List<byte[]>> imagesetdata = new List<List<byte[]>>();

            if (OpenConnection())
            {

                string q = "SELECT * FROM HangmanImageSets";

                SQLiteCommand cmd = new SQLiteCommand(q, _Connection);

                SQLiteDataReader reader = cmd.ExecuteReader();


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

                    imagesetdata.Add(current);
                }

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }

            return imagesetdata;


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

        public void InsertImageSet(IList<byte[]> images)
        {

            if (images.Count != _ImageSetSize)
            {
                throw new InvalidOperationException("Collection count does not match requirements");
            }

            //Get Imagesetcount to figure out what ID we should set

            var count = ImageSetCount;

            if (OpenConnection())
            {

                string cmnd = $"INSERT INTO HangmanImageSets (ID, Image0, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8)" +
                                $"VALUES ( @ID, @Image0, @Image1, @Image2, @Image3, @Image4, @Image5, @Image6, @Image7, @Image8 )";

                SQLiteCommand cmd = new SQLiteCommand(cmnd, _Connection);

                cmd.Parameters.Add("ID", DbType.Int32).Value = count + 1;

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

                cmd.Dispose();
                CloseConnection();
            }
        }

        public IEnumerable<HangmanGameRecord> FetchHistory()
        {
            List<HangmanGameRecord> records = new List<HangmanGameRecord>();

            if (OpenConnection())
            {

                string q = $"SELECT * FROM HangmanGameHistory";

                SQLiteCommand cmd = new SQLiteCommand(q, _Connection);

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Creat the record struct
                    var word = reader.GetString(0);
                    bool won = reader.GetBoolean(1);

                    records.Add(new HangmanGameRecord(word, won));
                }

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }

            return records;
        }

        public void InsertHistoryRecord(HangmanGameRecord record)
        {
            if (OpenConnection())
            {

                string q = $"INSERT INTO HangmanGameHistory (Word, Won?) VALUES (@Word, @Won?)";

                SQLiteCommand cmd = new SQLiteCommand(q, _Connection);

                cmd.Parameters.Add("Word", DbType.String).Value = record.Word;
                cmd.Parameters.Add("Won?", DbType.Boolean).Value = record.Won;

                cmd.Dispose();
                CloseConnection();
            }
        }
    }
}
