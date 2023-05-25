using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteMap.Classes
{
    public class SpriteAnimation
    {
        private KeyboardState _currentKbState;
        private KeyboardState _previousKbState;

        private float _timer;
        private float _interval = 200f;
        private int _currentFrame;
        private int _spriteWidth;
        private int _spriteHeight;
        private int _spriteSpeed = 3;
        private Rectangle _sourceRect;
        private Vector2 _position;
        private Vector2 _origin;

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public Vector2 Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public Texture2D Texture { get; set; }

        public Rectangle SourceRect
        {
            get => _sourceRect;
            set => _sourceRect = value;
        }

        public SpriteAnimation(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight)
        {
            Texture = texture;
            _currentFrame = currentFrame;
            _spriteWidth = spriteWidth;
            _spriteHeight = spriteHeight;
        }

        public void HandleSpriteMovement(GameTime gameTime)
        {
            _previousKbState = _currentKbState;
            _currentKbState = Keyboard.GetState();
            _spriteSpeed = 2;
            _interval = 50;

            _sourceRect = new Rectangle(_currentFrame * _spriteWidth, 0, _spriteWidth, _spriteHeight);

            if (_currentKbState.IsKeyDown(Keys.D))
            {
                AnimateRight(gameTime);
                if (_position.X < 784)
                {
                    _position.X += _spriteSpeed;
                }
            }

            if (_currentKbState.IsKeyDown(Keys.A))
            {
                AnimateLeft(gameTime);
                if (_position.X > 16)
                {
                    _position.X -= _spriteSpeed;
                }
            }

            if (_currentKbState.IsKeyDown(Keys.S))
            {
                AnimateDown(gameTime);
                if (_position.Y < 456)
                {
                    _position.Y += _spriteSpeed;
                }
            }

            if (_currentKbState.IsKeyDown(Keys.W))
            {
                AnimateUp(gameTime);
                if (_position.Y > 24)
                {
                    _position.Y -= _spriteSpeed;
                }
            }

            _origin = new Vector2(_sourceRect.Width / 2, _sourceRect.Height / 2);
        }

        public void AnimateUp(GameTime gameTime)
        {
            Animate(gameTime, 12, 15);
        }

        public void AnimateDown(GameTime gameTime)
        {
            Animate(gameTime, 0, 3);
        }

        public void AnimateRight(GameTime gameTime)
        {
            Animate(gameTime, 8, 11);
        }

        public void AnimateLeft(GameTime gameTime)
        {
            Animate(gameTime, 4, 7);
        }

        public void Animate(GameTime gameTime, int startFrame, int endFrame)
        {
            if (_currentKbState != _previousKbState)
            {
                _currentFrame = startFrame;
            }

            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_timer > _interval)
            {
                _currentFrame++;

                if (_currentFrame > endFrame)
                {
                    _currentFrame = startFrame;
                }
                _timer = 0f;
            }
        }
    }
}