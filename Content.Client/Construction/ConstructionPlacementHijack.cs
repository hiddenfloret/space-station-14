﻿using Content.Client.GameObjects.Components.Construction;
using Content.Client.GameObjects.EntitySystems;
using Content.Shared.Construction;
using Robust.Client.Placement;
using Robust.Client.Utility;
using Robust.Shared.Interfaces.GameObjects;
using Robust.Shared.Map;

namespace Content.Client.Construction
{
    public class ConstructionPlacementHijack : PlacementHijack
    {
        private readonly ConstructionSystem _constructionSystem;
        private readonly ConstructionPrototype _prototype;

        public ConstructionPlacementHijack(ConstructionSystem constructionSystem, ConstructionPrototype prototype)
        {
            _constructionSystem = constructionSystem;
            _prototype = prototype;
        }

        /// <inheritdoc />
        public override bool HijackPlacementRequest(GridCoordinates coords)
        {
            if (_prototype != null)
            {
                var dir = Manager.Direction;
                _constructionSystem.SpawnGhost(_prototype, coords, dir);
            }
            return true;
        }

        /// <inheritdoc />
        public override bool HijackDeletion(IEntity entity)
        {
            if (entity.TryGetComponent(out ConstructionGhostComponent ghost))
            {
                _constructionSystem.ClearGhost(ghost.GhostID);
            }
            return true;
        }

        /// <inheritdoc />
        public override void StartHijack(PlacementManager manager)
        {
            base.StartHijack(manager);

            manager.CurrentBaseSprite = _prototype.Icon.DirFrame0();
        }
    }
}
