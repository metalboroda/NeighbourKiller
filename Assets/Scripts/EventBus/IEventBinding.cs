using System;

namespace Assets.Scripts.EventBus
{
    public interface IEventBinding<in T> where T : IEvent
    {
        Action<T> OnEvent { get; }
        object Owner { get; }
    }
}