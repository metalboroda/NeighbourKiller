namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(float damageAmount);

        public bool IsDead { get; }
    }
}