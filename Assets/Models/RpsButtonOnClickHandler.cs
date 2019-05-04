using System;
using Interfaces;

namespace Models
{
    public class RpsButtonOnClickHandler
    {
        private readonly Action<RpsButton> _action;

        public RpsButtonOnClickHandler(Action<RpsButton> action)
        {
            _action = action;
        }

        public void OnClick(RpsButton button)
        {
            _action(button);
        }
    }
}