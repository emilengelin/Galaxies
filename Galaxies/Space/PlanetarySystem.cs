﻿using Galaxies.Controllers;
using Galaxies.Datas.Space;
using Galaxies.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Space
{

    class PlanetarySystem : IVisitable
    {

        public PlanetarySystemData Data { get; private set; }

        public Planet[] Planets { get; private set; }

        #region IVisitable

        public string Name
        {
            get
            {
                return Data.Name;
            }
        }

        public string Description
        {
            get
            {
                return Data.Description;
            }
        }

        public Texture2D VisitableTypeIcon
        {
            get
            {
                return null;
            }
        }

        #endregion

        public PlanetarySystem(PlanetarySystemData data)
        {
            this.Data = data;
        }

        private void CreatePlanets()
        {
            int nRndPlanets = Random.Next(Data.MinRandomPlanets, Data.MaxRandomPlanets + 1);

            Planets = new Planet[Data.PlanetIds.Length + nRndPlanets];

            for (int i = 0; i < Data.PlanetIds.Length; i++)
            {
                Planets[i] = new Planet(DataController.LoadData<PlanetData>(Data.PlanetIds[i], DataFileType.Planets));
            }

            for (int i = Data.PlanetIds.Length; i < Planets.Length; i++)
            {
                //Planets[i] = new Planet();
            }
        }

        #region IVisitable

        public void Visit()
        {
            GalaxyController.Visitables.Remove(this);

            CreatePlanets();
        }

        #endregion


    }

}