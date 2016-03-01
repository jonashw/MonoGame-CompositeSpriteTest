using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace CompositeSpriteTest
{
    public static class KeyboardEventRegistry
    {
        private static readonly Dictionary<Keys, List<Action>> ActionsByKey = new Dictionary<Keys, List<Action>>();
        public static void OnKeyDown(Keys key, Action action)
        {
            if (ActionsByKey.ContainsKey(key))
            {
                ActionsByKey[key].Add(action);
            }
            else
            {
                ActionsByKey[key] = new List<Action>
                {
                    action
                };
            }
        }

        private static readonly HashSet<Keys> PressedKeys = new HashSet<Keys>();
        private static readonly HashSet<Keys> PreviouslyPressedKeys = new HashSet<Keys>();
        private static readonly HashSet<Keys> ReleasedKeys = new HashSet<Keys>();

        public static void Update(KeyboardState keyboardState)
        {
            foreach (var key in keyboardState.GetPressedKeys())
            {
                PressedKeys.Add(key);
            }
            ReleasedKeys.UnionWith(PreviouslyPressedKeys);
            ReleasedKeys.ExceptWith(PressedKeys);
            PressedKeys.ExceptWith(PreviouslyPressedKeys);
            foreach (var action in PressedKeys
                .Where(pressedKey => ActionsByKey.ContainsKey(pressedKey))
                .SelectMany(pressedKey => ActionsByKey[pressedKey]))
            {
                action();
            }
            PreviouslyPressedKeys.UnionWith(PressedKeys);
            PreviouslyPressedKeys.ExceptWith(ReleasedKeys);
            ReleasedKeys.Clear();
        }
    }
}