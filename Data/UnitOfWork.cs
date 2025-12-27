namespace BoardApi.Data
{
    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        private readonly AppDbContext _db = db;

        public async Task SaveChnages()
        {
            await _db.SaveChangesAsync();
        }
    }
}