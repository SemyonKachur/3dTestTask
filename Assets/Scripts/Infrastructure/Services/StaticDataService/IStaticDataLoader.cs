using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.StaticDataService
{
    public interface IStaticDataLoader
    {
        UniTask Load();
    }
}