using Cysharp.Threading.Tasks;

namespace Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        public UniTask CreateUIRoot()
        {
            //TODO: add ui factory
            return UniTask.CompletedTask;
        }
    }
}