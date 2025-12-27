using System;
using System.Collections.Generic;

namespace ADayInTheOffice.Core
{
    /// <summary>
    /// Minimal event bus: pub/sub by message type.
    /// </summary>
    public sealed class SignalBus
    {
        private readonly Dictionary<Type, Delegate> _handlers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            var t = typeof(T);
            _handlers.TryGetValue(t, out var existing);
            _handlers[t] = (Action<T>)existing + handler;
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var t = typeof(T);
            if (_handlers.TryGetValue(t, out var existing))
                _handlers[t] = (Action<T>)existing - handler;
        }

        public void Publish<T>(T message)
        {
            var t = typeof(T);
            if (_handlers.TryGetValue(t, out var existing) && existing is Action<T> typed)
                typed.Invoke(message);
        }
    }
}
