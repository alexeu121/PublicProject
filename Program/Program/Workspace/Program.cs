using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using PacmanEngine.Components.Actors;
using PacmanEngine.Components.Base;
using PacmanEngine.Components.Graphics;
using Program.ManagedObjects.Antagonists;
using Program.ManagedObjects.Protagonists;
using Program.UnmanagedSources;

namespace Program.Workspace
{
    class Program
    {
        static void Main(string[] args)
        {
            var objects = ObjectsBuilder.PrepareObjects();

            Engine.Run(objects);
        }
    }
}
