using Flattiverse.Connector.Events;
using Flattiverse.Connector.GalaxyHierarchy;
using Flattiverse.Connector.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Flatty2
{
    class DataManager
    {
        public Galaxy? GalaxyOne { get; private set; }
        public Galaxy? GalaxyTwo { get; private set; }
        public static int GalTick;
        public static List<Unit> _Unit = new List<Unit>();
        //List<Task> tasks = new List<Task>();

        public async Task DoGalaxyOneUpdate()
        {
            
            try
            {
                GalaxyOne = await Galaxy.Connect("wss://www.flattiverse.com/game/galaxies/0","2e3e3415879320806268c64661fff6820874d8296435af023859acb4c10ce4fb","Test").ConfigureAwait(false);
                Debug.WriteLine($" * {GalaxyOne.Name} / [{GalaxyOne.Player.Id}] {GalaxyOne.Player.Name} / {GalaxyOne.Player.Team.Name} / {GalaxyOne.Player.Kind}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler bei der Verbindung zu GalaxyOne: {ex.Message}");

            }
        }
        public async void DoGalaxyTwoUpdate()
        {
            GalaxyTwo = await Galaxy.Connect("wss://www.flattiverse.com/game/galaxies/0").ConfigureAwait(false);
            }

        public void GalaxyOneLog()
        {
            Debug.WriteLine("SHITTY KACKAAAA!");
            if (GalaxyOne != null)
            {
                Debug.WriteLine($"LOG: * {GalaxyOne.Name} / [{GalaxyOne.Player.Id}] {GalaxyOne.Player.Name} / {GalaxyOne.Player.Team.Name} / {GalaxyOne.Player.Kind}");
            }
            Debug.WriteLine("############################################");
        }

        public async Task CheckEvents()
        {
            Chat chat = new Chat();
            FlattiverseEvent? TheEvent;
           
            

            try
            {
                while (true)
                {
                    //Debug.WriteLine(await GalaxyOne.NextEvent().ConfigureAwait(false));
                    TheEvent = await GalaxyOne.NextEvent().ConfigureAwait(false);
                    if (TheEvent.Kind == EventKind.GalaxyTick) 
                    {
                        GalaxyTickEvent g = (GalaxyTickEvent) TheEvent;
                        GalTick = g.Tick;
                        //Debug.WriteLine(GalTick);
                        try
                        {
                            //GalaxyOne.Chat("Thomas SPAMMT - aber nur zum testen!");                            
                        }
                        finally { }
                    }
                    else if (TheEvent.Kind == EventKind.ChatGalaxy) 
                    { 
                        GalaxyChatPlayerEvent g = (GalaxyChatPlayerEvent) TheEvent;
                        Debug.WriteLine($"Galaxy, {g.Player.Name}: {g.Message}");
                        chat.AddChatMessage($"Galaxy, {g.Player.Name}: {g.Message}");
                    }                    
                    else if (TheEvent.Kind == EventKind.ChatPlayer) 
                    { 
                        ChatPlayerEvent g = (ChatPlayerEvent) TheEvent;
                        Debug.WriteLine($"private, {g.Player.Name} sabbelt: {g.Message}");
                        chat.AddChatMessage($"{g.Player.Name}: {g.Message}");
                    }
                    else if (TheEvent.Kind == EventKind.NewUnit)
                    {
                        Debug.WriteLine("unit added");
                        NewUnitFlattiverseEvent g = (NewUnitFlattiverseEvent) TheEvent ;
                        
                        _Unit.Add(g.Unit);
                        
                        foreach (Unit u  in _Unit) { Debug.WriteLine($"X: {u.Position.X} + kind: {u.Kind.ToString()}"); }
                    }
                    else { 
                        Debug.WriteLine($"({GalTick}) { TheEvent.Kind.ToString()}");
                        Debug.WriteLine(TheEvent.ToString());
                        
                    }

                    
                }
                    
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"!!! Exception: {exception.Message}");
            }
        }
        internal void SendChatMessage(string message)
        {
            try { GalaxyOne.Chat(message); }
            finally { }
        }

    }
}
