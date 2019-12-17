using System;

namespace ClassLib.util
{
    public abstract class AocDay
    {
        protected string[] Input => _inputLazy.Value;

        private readonly Lazy<string[]> _inputLazy;

        protected AocDay()
        {
            _inputLazy = new Lazy<string[]>(() => InputHandler.ReadFile( $"{GetType().Name}.txt"));
        }
    }
}