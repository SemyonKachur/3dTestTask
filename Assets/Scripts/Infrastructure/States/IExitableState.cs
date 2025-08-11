using Cysharp.Threading.Tasks;

namespace Infrastructure.States
{
    public interface IExitableState
    {
        public UniTask Exit();
    }
}