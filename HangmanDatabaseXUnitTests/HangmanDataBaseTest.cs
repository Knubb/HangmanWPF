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
            //Arrange
            HangmanDatabase db = new HangmanDatabase();

            //Act
            int actual = db.WordCount;

            //Assert
            Assert.True(actual > -1);
        }
    }
}
