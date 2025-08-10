using System.Collections.Generic;
using Features.Player.Stats;

namespace Features.Player.Model
{
    public class PlayerModel : IPlayerModel
    {
        public List<ICharacterStat> Stats { get; private set; } = new();
    }
}