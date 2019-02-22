﻿using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIPlanetarySystem : UIGroup
    {

        public UIPlanetarySystem(Texture2D sprite, Vector2 position, EventArg onClick, Screen screen, IVisitable visitable) : base(sprite, position, 0, Color.White, new Vector2(300, 200), onClick, screen)
        {
            AddUIElement(new UIElement(visitable.VisitableTypeIcon, new Vector2(-125, -75), 0, Color.Red, new Vector2(50), null, screen, false));
            AddUIElement(new UIText(SpriteHelper.Arial_Font, visitable.Name, TextAlign.MiddleLeft, 5, new Vector2(25, -75), 0, Color.White, new Vector2(250, 50), screen));
            AddUIElement(new UIText(SpriteHelper.Arial_Font, visitable.Description, TextAlign.TopLeft, 5, new Vector2(0, 25), 0, Color.White, new Vector2(300, 150), screen));
        }

    }

}
