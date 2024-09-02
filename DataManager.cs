using Flattiverse.Connector.Events;
using Flattiverse.Connector.GalaxyHierarchy;
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
                            //for (int i = 0; i < 1; i++)
                            //{
                            //    GalaxyOne.Chat("Thomas SPAMMT");
                            //}
                        }
                        finally { }
                    }
                    else if (TheEvent.Kind == EventKind.ChatGalaxy) 
                    { 
                        GalaxyChatPlayerEvent g = (GalaxyChatPlayerEvent) TheEvent;
                        Debug.WriteLine($"Galaxy, {g.Player.Name} sabbelt: {g.Message}"); 
                    }                    
                    else if (TheEvent.Kind == EventKind.ChatPlayer) 
                    { 
                        ChatPlayerEvent g = (ChatPlayerEvent) TheEvent;
                        Debug.WriteLine($"private, {g.Player.Name} sabbelt: {g.Message}"); 
                    }
                    else { 
                        Debug.WriteLine($"({GalTick}) { TheEvent.Kind.ToString()}");
                        
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
