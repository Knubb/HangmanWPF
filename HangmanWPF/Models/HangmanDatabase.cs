using System;
using System.Collections.Generic;
using System.Data.SQLite;


namespace HangmanWPF.Models
{
    public class HangmanDatabase
    {
        private SQLiteConnection _Connection;

        private int _WordCount = -1;
        public int WordCount
        {
            get
            {
                if (_WordCount == -1)
                {
                    _WordCount = GetWordCount();
                }
                return _WordCount;
            }
        }


        //Constructor
        public HangmanDatabase()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            string connectionString;

            connectionString = "Data Source=C:\\Users\\knubb\\OneDrive\\Dokument\\Visual Studio 2017\\Projects\\HangmanWPF\\HangmanWPF\\HangmanData\\HangmanDataBase.db;Version=3";

            _Connection = new SQLiteConnection(connectionString);
        }

        public IEnumerable<string> GetRandomSetOfWords(int size)
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
            }


            return words;
        }

        //open connection to database
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

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                _Connection.Close();
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
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
    }
}
