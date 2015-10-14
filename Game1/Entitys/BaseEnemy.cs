﻿using Game1.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Patrik.GameProject;
using System;

namespace Game1.Entitys
{
    public class BaseEnemy : Entity
    {
        Vector2 target;

        public BaseEnemy(Texture2D texture, Vector2 position, float speed, int size, SimulationWorld world) : base(texture, position, speed, size, world)
        {

        }

        public override void Update(float delta)
        {
            base.Update(delta);


            if (world.RayCast(GetPosition(), world.Player.GetPosition()))
            {
                Face(world.Player.GetPosition());
                Fire();
            }
            else
            {
                rotation = (float)Math.Atan2(direction.Y, direction.X);
            }

            float dst = (world.Player.GetPosition() - position).Length();
            if (dst < Tile.SIZE * 2.6f)
            {
                target = Vector2.Zero;
                return;
            }


            if (target != Vector2.Zero && (target - position).Length() > 2)
            {
                float range = (target - position).Length();
                //position = Vector2.Lerp(position, target, delta * speed / range);
                direction = (target - position);
                direction.Normalize();

            }
            else
            {
                RebuildPath();
            }
        }

        private void RebuildPath()
        {
            var path = world.PathFinder.Pathfind(((position) / Tile.SIZE).ToPoint(), ((world.Player.GetPosition()) / Tile.SIZE).ToPoint()); //- new Vector2(Tile.SIZE/2f) , new Vector2(Tile.SIZE / 2f)
            if (path.Count > 1)
                target = path[1].ToVector2() * Tile.SIZE + new Vector2(Tile.SIZE / 2f);
        }
    }
}
