using System;

namespace Assets.Scripts.EventBus
{
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        public Action<T> OnEvent { get; private set; }

        public object Owner { get; }

        public EventBinding(Action<T> onEvent, object owner = null)
        {
            OnEvent = onEvent ?? throw new ArgumentNullException(nameof(onEvent));
            Owner = owner;
        }

        public EventBinding(Action onEventNoArgs, object owner = null)
        {
            if (onEventNoArgs == null) throw new ArgumentNullException(nameof(onEventNoArgs));

            OnEvent = _ => onEventNoArgs();
            Owner = owner;
        }

        public void Add(Action<T> onEvent) => OnEvent += onEvent ?? throw new ArgumentNullException(nameof(onEvent));
        public void Remove(Action<T> onEvent) => OnEvent -= onEvent;
    }
}