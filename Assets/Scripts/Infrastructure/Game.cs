using Infrastructure.States;
using Zenject;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(DiContainer container)
        {
            StateMachine = new GameStateMachine(container);
        }
    }
}