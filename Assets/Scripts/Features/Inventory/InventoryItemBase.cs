using System;
using UniRx;

namespace Features.Inventory
{
    [Serializable]
    public class InventoryItemBase :  IItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public ReactiveProperty<int> Count { get; set; }
        public bool IsStackable { get; set; }
    }
}