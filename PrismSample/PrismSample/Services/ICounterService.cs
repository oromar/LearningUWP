namespace PrismSample.Services
{
    public interface ICounterService
    {
        int GetCount();
        int IncrementCount(int value);
    }
}
