namespace Leo.Interface
{
    public interface IDamageable
    {
        public int HP { get; set; }
        public void TakeDamage(int damage);
        public void Die();
    }
}