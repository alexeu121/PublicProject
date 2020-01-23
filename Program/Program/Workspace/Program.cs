﻿using System.Collections.Generic;
using System.Linq;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.ManagedObjects.Antagonists;
using Program.ManagedObjects.Protagonists;
using Program.UnmanagedSources;

namespace Program.WorkSpace
{
    public enum InitialData
    {
        Wall,
        Empty,
        SmallCoin,
        BigCoin,
        Pacman,
        Blinky,
        Pinky,
        Inky,
        Clyde
    }

    public class PointData
    {
        public Coordinate coord { get; set; }

        public InitialData InitData { get; set; }   //properties for initial objects

    }

    class Program
    {
        static bool[,] Grid = new bool[21, 27];        //grid for walls, data from App.Config file

        static void Main(string[] args)
        {
            List<IGameObject> Collection = ObjectsBuilder.CreateInitData();

            //refs=====================
            Pacman pacman = Collection.OfType<Pacman>().FirstOrDefault();
            Master master = new Master();
            if (pacman != null) pacman.MasterObj = master;          //ref to pacman from master
            master.CollectionOfAllObjects = Collection;         //give collection of all elements to master
            Collection.Add(master);
            //=========================

            Engine.Run(Collection);       //when load, transmit collection of objects for show and processing
        }
              
    }
}