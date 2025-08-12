using Features.Player.View;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class PlayerFactoryData : MonoBehaviour
    {
        [field: SerializeField] public PlayerView Prefab { get; private set; }
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
    }
}