using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lasers_HowDoTheyWork
{
    public class Photon
    {
        public Vector2 Position;
        public Vector2 Velocity;

        public void Update()
        {
            Position += Velocity;
        }
    }

    public class Game1 : Game
    {
        // Code
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _pointTexture;
        private int _width;
        private int _height;
        private readonly Random _random = new Random();
        
        // Universe
        private const float _c = 10.0f;
        private const int _photonCount = 10000;

        // World
        private readonly Vector2 _light = new Vector2(200, 300);

        // Dynamic
        private List<Photon> _photons = new List<Photon>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _width = _graphics.GraphicsDevice.DisplayMode.Width;
            _height = _graphics.GraphicsDevice.DisplayMode.Height - 100;

            _graphics.PreferredBackBufferHeight = _height;
            _graphics.PreferredBackBufferWidth = _width;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _pointTexture = Texture2D.FromFile(GraphicsDevice, "Content/blank.png");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(_photons.Count < _photonCount)
            {
                for (int i = 0; i < 100; i++)
                {
                    AddPhoton();
                }
            }

            _photons.ForEach(p => p.Update());

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void AddPhoton()
        {
            float theta = (float)_random.NextDouble() * MathF.PI * 2.0f;
            var dx = MathF.Cos(theta) * _c;
            var dy = MathF.Sin(theta) * _c;

            var newPhoton = new Photon();
            newPhoton.Position = _light;
            newPhoton.Velocity = new Vector2(dx, dy);

            _photons.Add(newPhoton);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            foreach (var photon in _photons)
            {
                _spriteBatch.Draw(
                    _pointTexture,
                    new Rectangle((int)photon.Position.X - 1, (int)photon.Position.Y - 1, 2, 2),
                    Color.Yellow);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
