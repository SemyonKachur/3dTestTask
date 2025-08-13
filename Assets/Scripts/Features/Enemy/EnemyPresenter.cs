using System.Linq;
using Cysharp.Threading.Tasks;
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
            _model.IsDeath
                .Subscribe(async x => await ShowDeath(x))
                .AddTo(_disposables);
            
            var health = _model.Stats.FirstOrDefault(x => x.Id == CharacterStatTypeId.Health);
            if (health != null)
            {
                _view.InitHealth(health.MaxValue, health.CurrentValue.Value);
                health.CurrentValue.Subscribe(health =>
                {
                    _view.UpdateHealth(health);
                    
                    if (health <= 0)
                    {
                        _model.IsDeath.Value = true;
                    }
                }).AddTo(_disposables);
                
                _view.OnDamageRecieved
                    .Subscribe(damage => health.CurrentValue.Value -= damage)
                    .AddTo(_disposables);
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

        private async UniTask ShowDeath(bool isDeath)
        {
            if (isDeath)
            {
                _view.SetTarget(null);
                
                //TODO: death animations;
                await UniTask.NextFrame();
                _view.Dispose();
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}