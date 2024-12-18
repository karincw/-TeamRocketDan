namespace Mochi.Interface
{
    public interface IDamageable
    {
        public int HP { get; set; }
        public void TakeDamage(int damage);
    }
}