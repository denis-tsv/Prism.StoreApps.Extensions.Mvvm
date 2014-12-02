using System.Threading.Tasks;

namespace Sample.Logic
{
    public interface IRepository
    {
        Task LoadSomeDataIntoMemory();
    }

    public class Repository : IRepository
    {
        public Task LoadSomeDataIntoMemory()
        {
            return Task.Delay(10);
        }
    }
}
