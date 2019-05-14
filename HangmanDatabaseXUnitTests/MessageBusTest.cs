using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using HangmanWPF.Models;


namespace HangmanWPF.Tests
{
    public class MessageBusTest
    {

        private readonly MessageBus _Bus;
        private TestMessage _TestMessage;

        //XUnit cretes a new instance of class for each test-method, so shared setup code is executed in constructor.
        public MessageBusTest()
        {
            _Bus = new MessageBus();
            _TestMessage = new TestMessage();

            //Subscirbe to <TestMessage>
            _Bus.Subscribe<TestMessage>(HandleMessage);
        }

        [Fact]
        public void PublishMessageTest()
        {

            PublishTestMessage();

            //Assert
            Assert.True(_TestMessage.HasBeenInvoked);


        }

        [Fact]
        public void UnsubscribeTest()
        {

            PublishTestMessage();

            if (_TestMessage.HasBeenInvoked)
            {

                _TestMessage.HasBeenInvoked = false;

                _Bus.Unsubscribe<TestMessage>(HandleMessage);

                PublishTestMessage();

                Assert.False(_TestMessage.HasBeenInvoked);
            }
            else
            {
                throw new Xunit.Sdk.XunitException("No TestMessage subscription");
            }
        }

        private void PublishTestMessage()
        {
            _Bus.Publish<TestMessage>(_TestMessage);
        }
        private void HandleMessage(TestMessage message)
        {
            message.HasBeenInvoked = true;
        }


    }

    public class TestMessage
    {
        public bool HasBeenInvoked { get; set; } = false;
    }
}
