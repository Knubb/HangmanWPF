using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace HangmanWPF.Models
{
    public class HangmanDataUploaderSQLite : IHangmanDataUploader
    {
        private const string _ConnectionString = "Data Source =.\\HangmanData\\HangmanDataBase.db;Version=3";
        private const int _ImageSetSize = 9;

        private SQLiteConnection _Connection;

        public HangmanDataUploaderSQLite()
        {
            Initialize();
        }
        private void Initialize()
        {
            _Connection = new SQLiteConnection(_ConnectionString);
        }

        public void InsertHistoryRecord(HangmanGameRecord record)
        {
            if (OpenConnection())
            {

                string q = $"INSERT INTO HangmanGameHistory (Word, Won) VALUES (@Word, @Won)";

                SQLiteCommand cmd = new SQLiteCommand(q, _Connection);

                cmd.Parameters.Add("Word", DbType.String).Value = record.Word;
                cmd.Parameters.Add("Won", DbType.Boolean).Value = record.Won;

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                CloseConnection();
            }
        }

        public void InsertImageSet(IList<byte[]> images)
        {

            if (images.Count != _ImageSetSize)
            {
                throw new InvalidOperationException("Collection count does not match requirements");
            }

            //Get Imagesetcount to figure out what ID we should set

            var count = GetImageSetCount();

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
    }
}
