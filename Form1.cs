using Flattiverse.Connector.Events;
using Flattiverse.Connector.GalaxyHierarchy;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Flatty2
{
    public partial class Form1 : Form
    {
        private DataManager dm;
        private Timer _timer;
        private CancellationTokenSource _cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
            dm = new DataManager();

            // Starte den asynchronen Ladevorgang
            _ = LoadGalaxyOneAsync();
            dm.GalaxyOneLog();
        }

        private async Task LoadGalaxyOneAsync()
        {
            try
            {
                // Lade GalaxyOne asynchron
                await dm.DoGalaxyOneUpdate();
                Debug.WriteLine("<dsdfsafdsaf");
                // Aktualisiere die UI nach erfolgreichem Laden der Daten
                //UpdateUI();
                _ = StartEventMonitoring();
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung: Zeige eine Nachricht an oder protokolliere den Fehler
                MessageBox.Show($"Fehler beim Laden der Galaxy-Daten: {ex.Message}");
            }
        }

        private async Task StartEventMonitoring()
        {
            try
            {
                // Überwache die Events und gebe sie in der Debug-Konsole aus
                await dm.CheckEvents();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Überwachen der Events: {ex.Message}");
            }
        }

        internal void SendChatMessage(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                dm.SendChatMessage(chatMessageBox.Text);
                chatMessageBox.Text = string.Empty;
            }
        }



        //private async Task LadeGalaxyOne(DataManager d)
        //{
        //    try
        //    {
        //        await d.DoGalaxyOneUpdate();
        //        d.GalaxyOneLog();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Fehler beim Laden der Galaxy-Daten: {ex.Message}");
        //    }
        //}

        //private void StartGalaxyOneUpdateTask(TimeSpan interval, CancellationToken cancellationToken)
        //{
        //    // Initialisiere den Timer
        //    _timer = new Timer(async _ =>
        //    {
        //        // Überprüfe, ob der Task abgebrochen wurde
        //        if (cancellationToken.IsCancellationRequested)
        //        {
        //            _timer?.Dispose();
        //            return;
        //        }

        //        // Führe das Update der Galaxy durch
        //        await UpdateGalaxyOneAsync();

        //    }, null, TimeSpan.Zero, interval);  // TimeSpan.Zero für den sofortigen Start
        //}




    }
}
