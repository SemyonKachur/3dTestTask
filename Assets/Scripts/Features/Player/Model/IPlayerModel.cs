using System.Collections.Generic;
using Features.Player.Stats;

namespace Features.Player.Model
{
    public interface IPlayerModel
    {
        public List<ICharacterStat> Stats { get; }
    }
}