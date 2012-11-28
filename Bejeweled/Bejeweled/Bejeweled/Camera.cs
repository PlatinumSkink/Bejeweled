using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bejeweled
{
    class Camera
    {
        Vector2 cameraPosition;
        public Viewport viewport;

        public float Zoom { get; set; }
        public float Rotation { get; set; }
        Rectangle LimitRectangle { get; set; }
        public Vector2 Origin { get; set; }

        Rectangle? Limit { get; set; }

        public Vector2 Position
        {
            get { return cameraPosition; }
            set
            {
                cameraPosition = value;
                if (LimitRectangle != null)
                {
                    cameraPosition.X = MathHelper.Clamp(cameraPosition.X, ((Rectangle)LimitRectangle).X, ((Rectangle)LimitRectangle).Width/* - viewport.Width*/);
                    cameraPosition.Y = MathHelper.Clamp(cameraPosition.Y, ((Rectangle)LimitRectangle).Y, ((Rectangle)LimitRectangle).Height/* - viewport.Height*/);
                }
            }
        }
        public float X
        {
            get { return cameraPosition.X; }
            set
            {
                if (LimitRectangle != null)
                {
                    cameraPosition.X = MathHelper.Clamp(cameraPosition.X, ((Rectangle)LimitRectangle).X, ((Rectangle)LimitRectangle).Width - viewport.Width);
                }
            }
        }
        public float Y
        {
            get { return cameraPosition.Y; }
            set
            {
                if (LimitRectangle != null)
                {
                    cameraPosition.Y = MathHelper.Clamp(cameraPosition.Y, ((Rectangle)LimitRectangle).Y, ((Rectangle)LimitRectangle).Height - viewport.Height);
                }
            }
        }
        public Camera(Viewport _viewport, Rectangle? _limitRectangle)
        {
            viewport = _viewport;
            LimitRectangle = (Rectangle)_limitRectangle;
            Origin = new Vector2(viewport.Width / 2, viewport.Height / 2);
            Zoom = 1f;
            Rotation = 0f;
        }
        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0f))
                * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1f))
                * Matrix.CreateRotationZ(Rotation)
                * Matrix.CreateTranslation(new Vector3(Origin, 0f));
        }
    }
}
