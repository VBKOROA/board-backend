namespace BoardApi.Data
{
    public interface IUnitOfWork
    {
        Task SaveChnages();
    }
}