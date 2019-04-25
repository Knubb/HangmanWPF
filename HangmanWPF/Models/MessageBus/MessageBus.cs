using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWPF.Models
{
    public sealed class MessageBus : IMessageBus
    {
        //Singleotn
        private static MessageBus _Instance = new MessageBus();
        public static MessageBus Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MessageBus();
                }

                return _Instance;
            }
        }

        private Dictionary<Type, List<MessageReference>> _Subscribers = new Dictionary<Type, List<MessageReference>>();

        public void Publish<TMessage>(TMessage message)
        {
            if (_Subscribers.ContainsKey(typeof(TMessage)))
            {
                var handlers = _Subscribers[typeof(TMessage)];

                foreach (var handler in handlers)
                {
                    var ac = new Action<TMessage>((Action<TMessage>)handler.DelegateReference);
                    ac.Invoke(message);
                }
            }
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            if (_Subscribers.ContainsKey(typeof(TMessage)))
            {
                //Add the handler to the list of delegates to be invoked when message of this type is published
                _Subscribers[typeof(TMessage)].Add(new MessageReference(handler));
            }
            else
            {
                //Create list of handlers
                var handlers = new List<MessageReference>();
                handlers.Add(new MessageReference(handler));
                _Subscribers[typeof(TMessage)] = handlers;
            }
        }

        public void Unsubscribe<TMessage>(Action<TMessage> handler)
        {

            if (_Subscribers.ContainsKey(typeof(TMessage)))
            {
                var handlers = _Subscribers[typeof(TMessage)];

                MessageReference targetReference = null;
                foreach (var reference in handlers)
                {
                    var action = (Action<TMessage>)reference.DelegateReference;
                    if ((action.Target == handler.Target) && action.Method.Equals(handler.Method))
                    {
                        targetReference = reference;
                        break;
                    }
                }
                handlers.Remove(targetReference);

                if (handlers.Count == 0)
                {
                    _Subscribers.Remove(typeof(TMessage));
                }
            }

        }
    }


    public class MessageReference
    {
        public Delegate DelegateReference { get; private set; }

        public MessageReference(Delegate action)
        {
            DelegateReference = action;
        }
    }
}
