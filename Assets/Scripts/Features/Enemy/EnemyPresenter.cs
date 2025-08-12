using System.Linq;
using Features.Player.Stats;
using UniRx;

namespace Features.Enemy
{
    public class EnemyPresenter : IEnemyPresenter
    {
        private readonly IEnemyModel _model;
        private readonly IEnemyView _view;
        
        private CompositeDisposable _disposables;

        public EnemyPresenter(IEnemyModel model, IEnemyView view)
        {
            _model = model;
            _view = view;
            
            _disposables = new();
        }
        
        public void Initialize()
        {
            var health = _model.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Health);
            if (health != null)
            {
                health.CurrentValue.Subscribe(health =>
                {
                    if (health <= 0)
                    {
                        _model.IsDeath.Value = true;
                        _view.Dispose();
                    }
                }).AddTo(_disposables);
            }
            
            var speed = _model.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Speed);
            if (speed != null)
            {
                _view.SetSpeed(speed.CurrentValue.Value);
            }
            
            var attackRange = _model.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.AttackRange);
            if (attackRange != null)
            {
                _view.SetStopDistance(attackRange.CurrentValue.Value);
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}