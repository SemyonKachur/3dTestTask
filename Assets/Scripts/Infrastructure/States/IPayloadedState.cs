using Cysharp.Threading.Tasks;

namespace Infrastructure.States
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public UniTask Enter(TPayload payload);
    }
}