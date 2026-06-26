using Assets.Scripts.EventBus;

namespace EventsFolder
{
    public class WeaponEvents
    {
        public struct AmmoChangedEventArgs : IEvent
        {
            public int currentAmmo;
            public int maxAmmo;
        }

        public struct WeaponFiredEventArgs : IEvent { }
    }
}