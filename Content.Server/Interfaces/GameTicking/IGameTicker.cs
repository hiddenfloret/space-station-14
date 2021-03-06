using System;
using System.Collections.Generic;
using Content.Server.GameTicking;
using Robust.Server.Interfaces.Player;
using Robust.Shared.Map;
using Robust.Shared.Timing;

namespace Content.Server.Interfaces.GameTicking
{
    /// <summary>
    ///     The game ticker is responsible for managing the round-by-round system of the game.
    /// </summary>
    public interface IGameTicker
    {
        GameRunLevel RunLevel { get; }

        event Action<GameRunLevelChangedEventArgs> OnRunLevelChanged;
        event Action<GameRuleAddedEventArgs> OnRuleAdded;

        void Initialize();
        void Update(FrameEventArgs frameEventArgs);

        void RestartRound();
        void StartRound();
        void EndRound();

        void Respawn(IPlayerSession targetPlayer);
        void MakeObserve(IPlayerSession player);
        void MakeJoinGame(IPlayerSession player);
        void ToggleReady(IPlayerSession player, bool ready);

        GridCoordinates GetLateJoinSpawnPoint();
        GridCoordinates GetJobSpawnPoint(string jobId);
        GridCoordinates GetObserverSpawnPoint();

        // GameRule system.
        T AddGameRule<T>() where T : GameRule, new();
        bool HasGameRule(Type type);
        void RemoveGameRule(GameRule rule);
        IEnumerable<GameRule> ActiveGameRules { get; }

        void SetStartPreset(Type type);
        void SetStartPreset(string type);
    }
}
