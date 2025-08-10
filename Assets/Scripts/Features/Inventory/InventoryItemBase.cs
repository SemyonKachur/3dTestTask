using Infrastructure.Services.SaveService;
using UniRx;

namespace Features.Inventory
{
    public class InventoryItemBase :  IItem
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string ImagePath { get; }
        public ReactiveProperty<int> Count { get; }
        public bool IsStackable { get; }
    }
}