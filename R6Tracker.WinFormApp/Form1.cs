using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using R6T.Model;
using R6T.Model.ViewModels;
using R6T.Scraper;

namespace R6Tracker.WinFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetPlayers();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                var oMain = new Main();
                oMain.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private List<Player> GetPlayers()
        {
            var oR6TrackerEntities = new R6TrackerEntities();
            var players = oR6TrackerEntities.Players.ToList();

            gridPlayers.DataSource = players;

            return players;
        }

        private async void btnSyncPlayerData_Click(object sender, EventArgs e)
        {
            var row = gridViewPlayers.GetFocusedRow() as Player;
            if (row != null)
            {
                var alias = row.Alias;
                if (!String.IsNullOrEmpty(alias))
                {
                    var oMain = new Main();
                    oMain.InitSelenium();
                    await oMain.ScrapeUserData(row);
                }
                else
                {
                    throw new Exception("No alias");
                }
            }
            else
            {
                throw new Exception("Row is null");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var row = gridViewPlayers.GetFocusedRow() as Player;
            if (row != null)
            {
                var alias = row.Alias;
                if (String.IsNullOrEmpty(alias))
                {
                    throw new Exception("Alias null");
                }
                else if (String.IsNullOrEmpty(row.PlayerName))
                {
                    throw new Exception("PlayerName null");
                }
                else
                {
                    var oR6TrackerEntities = new R6TrackerEntities();
                    var player = oR6TrackerEntities.Players.SingleOrDefault(s => s.PlayerId == row.PlayerId);
                    if (player != null)
                    {
                        player.Alias = row.Alias;
                        player.PlayerName = row.PlayerName;
                        player.IsActive = row.IsActive;
                        oR6TrackerEntities.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception("Row is null");
            }
        }
    }
}
