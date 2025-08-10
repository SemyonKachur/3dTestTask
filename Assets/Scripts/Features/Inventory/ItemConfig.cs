using Infrastructure.Services.SaveService;
using UniRx;
using UnityEngine;

namespace Features.Inventory
{
    [CreateAssetMenu(fileName = nameof(ItemConfig), menuName = "Inventory/" + nameof(ItemConfig))]
    public class ItemConfig : ScriptableObject, IItem
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public string ImagePath { get; private set; }
        [field: SerializeField] public ReactiveProperty<int> Count { get; private set; }
        [field: SerializeField] public bool IsStackable { get; private set; }
    }
}