﻿using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Galaxies.UI
{

    /// <summary>
    /// Allows the element to have a subset of UI Element children, while also enabling an easy way of selecting them.
    /// Disables the ability to click this element.
    /// </summary>
    abstract class UIContainer : UIElement
    {

        protected List<UIElement> Container { get; private set; } = new List<UIElement>();

        /// <summary>
        /// Raw size, without any padding or spacing applied.
        /// </summary>
        protected Vector2 RawSize { get; private set; }

        /// <summary>
        /// Padding inwards. Top, right, bottom, left.
        /// </summary>
        protected Vector4 Padding { get; private set; }

        /// <summary>
        /// Space between items. X (left and right) and Y (top and bottom).
        /// </summary>
        protected Vector2 Spacing { get; private set; }

        private bool ResponsiveSize { get; set; }

        public UIContainer(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size, Screen screen, Vector4 padding, Vector2 spacing, bool responsiveSize)
            : base(sprite, new Vector2(position.X - (padding.W + padding.Y) / 2f, position.Y - (padding.X + padding.Z / 2f)), rotation, color, new Vector2(size.X + padding.W + padding.Y, size.Y + padding.X + padding.Z), null, screen, false)
        {
            this.RawSize = size;

            this.Padding = padding;
            this.Spacing = spacing;

            this.ResponsiveSize = responsiveSize;
        }

        protected abstract void CalculatePositions();
        protected abstract void CalculateSize();

        #region Adding UI Elements

        /// <summary>
        /// Called whenever a new UI Element is added to the container.
        /// </summary>
        protected virtual void UIElementAdded(UIElement addedElement)
        {
            //Do nothing here.
            //This method will not be utilized by every child, therefore, keep it overridable.
        }

        /// <summary>
        /// Adds a new UI Element to the container.
        /// </summary>
        public T AddUIElement<T>(T uiElement) where T : UIElement
        {
            Container.Add(uiElement);

            if (uiElement.CanBeClicked)
            {
                //Add the UI Element to the screen's clickable items.
                screen.AddClickableUIElement(uiElement);
            }

            CalculatePositions();

            if (ResponsiveSize)
            {
                CalculateSize();
            }

            UIElementAdded(uiElement);

            return uiElement;
        }

        /// <summary>
        /// Adds multiple new UI Elements to the container.
        /// </summary>
        public void AddUIElements(params UIElement[] uiElements)
        {
            for (int i = 0; i < uiElements.Length; i++)
            {
                Container.Add(uiElements[i]);

                if (uiElements[i].CanBeClicked)
                {
                    //Add the UI Element to the screen's clickable items.
                    screen.AddClickableUIElement(uiElements[i]);
                }

                UIElementAdded(uiElements[i]);
            }

            CalculatePositions();

            if (ResponsiveSize)
            {
                CalculateSize();
            }
        }

        #endregion

        #region Removing UI Elements

        /// <summary>
        /// Called whenever a UI Element is removed from the container.
        /// </summary>
        /// <param name="removedElement"></param>
        protected virtual void UIElementRemoved(UIElement removedElement, int removedIndex)
        {
            //Do nothing here.
            //This method will not be utilized by every child, therefore, keep it overridable.
        }

        /// <summary>
        /// Removes the UI Element from the container and repositions all existing elements.
        /// </summary>
        public bool RemoveUIElement(UIElement uiElement)
        {
            int  index = Container.IndexOf(uiElement);
            bool value = Container.Remove(uiElement);

            screen.RemoveUIElement(uiElement);

            //Don't call the method unless the item existed.
            if (value)
            {
                UIElementRemoved(uiElement, index);
            }

            return value;
        }

        #endregion

        public override void Select()
        {
            //Do nothing, TODO: Remove as we already have Clickable set to false?
        }

        public override void Deselect()
        {
            //Do nothing, TODO: Remove as we already have Clickable set to false?
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (UIElement element in Container)
            {
                element.Draw(spriteBatch);
            }
        }

    }

}
