using HangmanWPF.Models;
using Xunit;

namespace HangmanDatabaseXUnitTests
{
    public class HangmanDataBaseTest
    {
        //[Fact]
        //public void ConnectToDataBaseTest()
        //{
        //    //Arrange
        //    HangmanDatabase db = new HangmanDatabase();

        //    //Act


        //    //Assert




        //}

        [Fact]
        public void GetWordCountTest()
        {
            //Prepare
            HangmanDatabase db = new HangmanDatabase();

            int expected = -1;

            //Run
            int actual = db.WordCount;

            //Assert
            Assert.NotEqual(expected, actual);
        }
    }
}
