using Cysharp.Threading.Tasks;

namespace Infrastructure.Factory
{
    public interface IUIFactory
    {
        UniTask CreateUIRoot();
    }
}