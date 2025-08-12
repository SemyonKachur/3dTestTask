namespace Infrastructure.Services.Pools
{
    public interface IBulletsPool
    {
        public Bullet GetBullet();
        public void ReturnBullet(Bullet bullet);

    }
}