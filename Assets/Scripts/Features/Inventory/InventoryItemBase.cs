using System;
using UniRx;

namespace Features.Inventory
{
    [Serializable]
    public class InventoryItemBase :  IItem
    {
        public string Id { get; set; }
        public ItemType ItemType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public ReactiveProperty<int> Count { get; set; }
        public bool IsStackable { get; set; }

        public InventoryItemBase()
        {
        }
        
        public InventoryItemBase(IItem config)
        {
            Id = config.Id;
            ItemType = config.ItemType;
            Name = config.Name;
            Description = config.Description;
            ImagePath = config.ImagePath;
            Count = new ReactiveProperty<int>(config.Count.Value);
            IsStackable = config.IsStackable;
        }
    }
}