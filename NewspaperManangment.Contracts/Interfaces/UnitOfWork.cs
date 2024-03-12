namespace NewspaperManangment.Contracts.Interfaces;

public interface UnitOfWork
{
    Task Begin();
    Task Commit();
    Task Complete();
    Task RollBack();
}