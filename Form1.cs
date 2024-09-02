using Flattiverse.Connector.Events;
using Flattiverse.Connector.GalaxyHierarchy;
using Flattiverse.Connector.Units;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

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

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Color bg = Color.FromArgb(10, 10, 20);

            Graphics gg = e.Graphics;
            //g.Clear(bg);
            //LinearGradientBrush linearGradientBrush = new LinearGradientBrush(
            //        new PointF(200, 200),
            //        new PointF(3000, 3000),
            //        Color.AliceBlue,
            //        Color.Blue);
            //g.FillEllipse(linearGradientBrush, 200, 200, 300, 300);

            gg.FillEllipse(Brushes.Blue, 200, 200, 300, 300);

            //g.DrawString($" test name ", new Font("Arial", 12), Brushes.LightBlue, (float)CalculateOffset(moon).X, (float)CalculateOffset(moon).Y - 30 / scaling);

            foreach (Unit u in DataManager._Unit)
            {
                if (u.Kind == UnitKind.Planet)
                {
                    //Paint planet:
                    //g.FillEllipse(Brushes.LightBlue, u.Position.X, u.Position.Y, );
                    //LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new PointF((int)CalculateOffset(u).X + (int)u.Radius / scaling * 2, (int)CalculateOffset(u).Y + (int)u.Radius / scaling * 2), new PointF((int)CalculateOffset(u).X, (int)CalculateOffset(u).Y), Color.AliceBlue, Color.Blue);
                    //LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new PointF((int)u.Position.X, u.Position.Y)),new PointF((int)u.Position.X, u.Position.Y)), Color.AliceBlue, Color.Blue);

                    //LinearGradientBrush linearGradientBrush = new LinearGradientBrush(
                    //new PointF((int)u.Position.X+20, (int)u.Position.Y+20),
                    //new PointF((int)u.Position.X, (int)u.Position.Y),
                    //Color.AliceBlue,
                    //Color.Blue);
                    //g.FillEllipse(linearGradientBrush, MachMirRechteck(u.Position, u.Radius));
                    //gg.FillEllipse(Brushes.Red, (int)u.Position.X, (int)u.Position.Y, (int)u.Radius*2, (int)u.Radius*2);
                    gg.FillEllipse(Brushes.Orange, 200, 200, 300, 300);
                    Debug.WriteLine($"zeichne: + {u.Position.X} + {u.Position.Y} + {u.Radius}");
                    //linearGradientBrush.Dispose();

                }
            }
        }

        private RectangleF MachMirRechteck(Vector position, double radius)
        {
            //float x = (float)((position.X - myShip.Position.X - radius) / scaling + offset.X);
            //float y = (float)((position.Y - myShip.Position.Y - radius) / scaling + offset.Y);
            //float r = (float)radius / scaling;
            float x = (float)((position.X + radius));
            float y = (float)((position.Y + radius));
            float r = (float)radius;
            return new RectangleF(x, y, r * 2, r * 2);
        }

        private void refresh_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        //foreach (Unit u in dm.)
        //{
        //    //if(u.Kind == UnitKind.Planet)
        //    if (u is Planet p) { DrawPlanet(g, p);
        //    }
        //    //else if (u is Sun s) { DrawSun(g, s);
        //    //}
        //    //else if (u is BlackHole b) { DrawBlackHole(g, b); }
        //    //else if (u is Moon moon) { DrawMoon(g, moon); }
        //    //else if (u is Meteoroid met) { DrawMeteor(g, met); }
        //    //else if (u is Buoy bu) { DrawBu(g, bu); }
        //    //else if (u is PlayerUnit pu && pu.Name != myShip.Name) { DrawEnemy(g, pu); }
        //}


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
