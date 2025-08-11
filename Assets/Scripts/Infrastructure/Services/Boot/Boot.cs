using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Boot
{
    public class Boot : MonoBehaviour
    {
        private Game _game;

        [Inject]
        public void Construct(DiContainer container)
        {
            _game = new Game(container);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}