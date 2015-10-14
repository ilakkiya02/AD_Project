﻿using Game1.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Patrik.GameProject
{
   public class Player : Entity
    {
        public enum Commands
        {
               MoveLeft, MoveRight, MoveUp, MoveDown
        }

        private Inputs input;
        public Vector2 PosAim { private set; get; }

        public Player(Vector2 position, SimulationWorld world, Inputs input) : base(Globals.player, position, 250, 40, world)
        {
            this.input = input;
            PosAim = position;
        }

        public override void Update(float delta)
        {
            Vector2 dir = new Vector2(0, 0);
            if (input.KeyPressed(Globals.inputTable.Get(Commands.MoveDown)))
                dir.Y += 1;
            if (input.KeyPressed(Globals.inputTable.Get(Commands.MoveUp)))
                dir.Y -= 1;
            if (input.KeyPressed(Globals.inputTable.Get(Commands.MoveLeft)))
                dir.X -= 1;
            if (input.KeyPressed(Globals.inputTable.Get(Commands.MoveRight)))
                dir.X += 1;
            if (dir.X != 0)
                dir.Normalize();

            Move(dir);

            PosAim = input.GetPosCamera();

            rotation = (float)Math.Atan2(PosAim.Y - position.Y, PosAim.X - position.X);

            base.Update(delta);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
