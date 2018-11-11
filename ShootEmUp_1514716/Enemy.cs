﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp_1514716
{
    class Enemy : Entity
    {
        private int timeUntilStart = 60;
        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();

        public bool IsActive { get { return timeUntilStart <= 0; } }

        public Enemy(Texture2D image, Vector2 position)
        {
            this.image = image;
            Position = position;
            Radius = image.Width / 2f;
            color = Color.Transparent;
        }
        public override void Update()
        {
            if (timeUntilStart <= 0)
            {
                // enemy behaviour logic goes here .
                if (timeUntilStart <= 0)
                    ApplyBehaviours();
                // ...

            }
            else
            {
                timeUntilStart --;
                color = Color.White * (1 - timeUntilStart / 60f ) ;
            }
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, GameRoot.ScreenSize
            - Size / 2);
            Velocity *= 0.4f;
        }
        public void WasShot()
        {
            IsExpired = true;
        }

        IEnumerable<int> FollowPlayer(float acceleration = 1f )
        {
            while (true)
            {
                Velocity += (PlayerShip.Instance.Position -
                Position).ScaleTo(acceleration);
                if (Velocity != Vector2.Zero)
                    Orientation = Velocity.ToAngle();
                yield return 0;
            }
        }
        {
            behaviours.Add(behaviour.GetEnumerator());
        }

        private void ApplyBehaviours()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if (!behaviours[i].MoveNext())
                    behaviours.RemoveAt(i --);
            }
        }
        {
            var enemy = new Enemy(GameRoot.Seeker, position);
            enemy.AddBehaviour(enemy.FollowPlayer());
            return enemy;
        }
        {
            var d = Position - other.Position;
            Velocity += 10 * d / (d.LengthSquared() + 1);
        }
    }
}