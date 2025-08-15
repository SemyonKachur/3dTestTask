using UniRx;

namespace Features.Inventory
{
    public interface IItem
    {
        public string Id { get; }
        public ItemType ItemType { get; }
        public string Name { get; }
        public string Description { get; }
        
        public string ImagePath { get; }
        
        public ReactiveProperty<int> Count { get; }
        public bool IsStackable { get; }

        public IItem Clone();
    }
}