﻿﻿/*
 * Copyright (C) 2011 mooege project
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using Mooege.Core.GS.Common.Types.Math;
using Mooege.Core.GS.Map;

namespace Mooege.Core.GS.Objects
{
    public abstract class WorldObject : DynamicObject, IRevealable
    {
        protected World _world;
        public virtual World World
        {
            get { return this._world; }
            set { this._world = value; }
        }

        public float Scale { get; set; }
        public float RotationAmount { get; set; }

        protected Vector3D _rotationAxis;
        public Vector3D RotationAxis
        {
            get { return _rotationAxis; }
            set { this._rotationAxis = value; }
        }

        protected Vector3D _position;
        public virtual Vector3D Position
        {
            get { return _position; }
            set { _position = value; }
        }

        protected WorldObject(World world, uint dynamicID)
            : base(dynamicID)
        {
            // Specifically avoid calling the potentially overridden setter for this.World
            this._world = world;
            this._world.Game.StartTracking(this);
            this._rotationAxis = new Vector3D();
            this._position = new Vector3D();
        }

        public sealed override void Destroy()
        {
            World world = this.World;
            this.World = null; // Will Leave() the world for Actors (see deriving implementation of the setter for this.World)
            world.Game.EndTracking(this);
        }

        public abstract bool Reveal(Mooege.Core.GS.Player.Player player);
        public abstract bool Unreveal(Mooege.Core.GS.Player.Player player);
    }
}
