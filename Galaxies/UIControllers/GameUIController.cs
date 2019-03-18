﻿using Galaxies.Controllers;
using Galaxies.Extensions;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Galaxies.UIControllers
{

    static class GameUIController
    {

        public static Screen CurrentScreen { get; private set; }

        public static GameWindow Window { get; set; }

        #region Window helpers

        public static int WindowWidth
        {
            get
            {
                return Window.ClientBounds.Width;
            }
        }

        public static int WindowHeight
        {
            get
            {
                return Window.ClientBounds.Height;
            }
        }

        #endregion

        public static void Update(GameTime gameTime)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Draw(spriteBatch);
            }
        }

        #region Screens

        private static void CreateScreen(Screen newScreen)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.DestroyScreen();
            }

            CreateLoadingScreen();

            //Task.Run(() => CreateScreenAsync(newScreen));
            CreateScreenAsync(newScreen);

            //int x = 0;

            //Loading screen is automatically removed
        }

        private async static void CreateScreenAsync(Screen newScreen)
        {
            //await newScreen.CreateUIAsync();
            await Task.Run(() => newScreen.CreateUIAsync());
            //await Task.Run(() => DoSomethingAsync(newScreen.UIElements.Count));
            //await newScreen.CreateUIAsync();
            CurrentScreen = newScreen;
        }

        static async void DoSomethingAsync(int x)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromSeconds(10))
            {
                //Do something
            }

            s.Stop();

            GIF g = SpriteHelper.Citadel_Background_Animation_1;

            Console.WriteLine("DoSomething " + x);
        }

        private static void CreateLoadingScreen()
        {
            LoadingScreen loadingScreen = new LoadingScreen();
            loadingScreen.CreateUIAsync();

            CurrentScreen = loadingScreen;
        }

        public static void CreateMenuScreen()
        {
            GameController.GameState = GameState.MainMenu;

            CreateScreen(new MenuScreen());
        }

        public static void CreateGalaxyScreen()
        {
            GameController.GameState = GameState.Galaxy;

            CreateScreen(new GalaxyScreen());
        }

        public static void CreatePlanetarySystemScreen()
        {
            GameController.GameState = GameState.PlanetarySystem;

            CreateScreen(new PlanetarySystemScreen());
        }

        public static void CreateCombatScreen()
        {
            GameController.GameState = GameState.Combat;

            CreateScreen(new CombatScreen());
        }

        public static void CreateCitadelScreen()
        {
            //Refill the all the player ship template's stats:
            ShipyardController.RefillShipStats();

            GameController.GameState = GameState.Citadel;

            CreateScreen(new CitadelScreen());
        }

        public static void CreateGameOverScreen()
        {
            GameController.GameState = GameState.GameOver;

            CreateScreen(new GameOverScreen());
        }

        #endregion

        #region Position helpers

        public static int WidthPercent(float percent)
        {
            return (int)Math.Round(Window.ClientBounds.Width * percent, 0);
        }

        public static int HeightPercent(float percent)
        {
            return (int)Math.Round(Window.ClientBounds.Height * percent, 0);
        }

        #endregion

    }

}
