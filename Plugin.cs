using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using SDG.Unturned;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Unturned;
using System.Reflection;

namespace AutomatedMaxplayers
{
    public class Plugin : RocketPlugin<Configuration>
    {
        protected override void Load()
        {
            Logger.Log("AutomatedMaxPlayers by Commando53 has been loaded.");
            U.Events.OnPlayerConnected += new UnturnedEvents.PlayerConnected(this.OnConnect);
            if (Configuration.Instance.DecreaseAfterPlayersLeave)
            {
                U.Events.OnPlayerDisconnected += new UnturnedEvents.PlayerDisconnected(this.OnDisconnect);
            }
        }
        protected override void Unload()
        {
            Logger.Log("AutomatedMaxPlayers by Commando53 has been unloaded.");
            U.Events.OnPlayerConnected -= new UnturnedEvents.PlayerConnected(this.OnConnect);
            if (Configuration.Instance.DecreaseAfterPlayersLeave)
            {
                U.Events.OnPlayerDisconnected -= new UnturnedEvents.PlayerDisconnected(this.OnDisconnect);
            }
        }
        public void OnConnect(UnturnedPlayer player)
        {
            if (Provider.clients.Count == (Convert.ToInt32(Provider.maxPlayers) - 1) || Provider.clients.Count == Convert.ToInt32(Provider.maxPlayers))
            {
                if ((Convert.ToInt32(Provider.maxPlayers) + Convert.ToInt32(Configuration.Instance.IncreasePlayersBy)) >= Convert.ToInt32(Configuration.Instance.DontGoOverThan))
                {
                    Logger.Log("Maxplayers has been set to " + Convert.ToString(Configuration.Instance.DontGoOverThan));
                    Provider.maxPlayers = Configuration.Instance.DontGoOverThan;
                    return;
                }
                Logger.Log("Maxplayers has been set to " + Convert.ToString(Convert.ToInt32(Provider.maxPlayers) + Convert.ToInt32(Configuration.Instance.IncreasePlayersBy)));
                Provider.maxPlayers += Configuration.Instance.IncreasePlayersBy;

            }
        }
        public void OnDisconnect(UnturnedPlayer player)
        {
            if (Configuration.Instance.DecreaseAfterPlayersLeave && Convert.ToInt32(Provider.maxPlayers) > Convert.ToInt32(Configuration.Instance.DontGoLessThan) && Convert.ToInt32(Provider.maxPlayers) == Convert.ToInt32(Configuration.Instance.DontGoOverThan) || Provider.clients.Count <= (Convert.ToInt32(Provider.maxPlayers) - Convert.ToInt32(Configuration.Instance.IncreasePlayersBy)))
            {
                if (Convert.ToInt32(Provider.maxPlayers) == Convert.ToInt32(Configuration.Instance.DontGoLessThan))
                {
                    return;
                }
                if ((Convert.ToInt32(Provider.maxPlayers) - Convert.ToInt32(Configuration.Instance.IncreasePlayersBy)) < Convert.ToInt32(Configuration.Instance.DontGoLessThan))
                {
                    Logger.Log("Maxplayers has been set to " + Convert.ToString(Configuration.Instance.DontGoLessThan));
                    Provider.maxPlayers = Configuration.Instance.DontGoLessThan;
                    return;
                }
                Logger.Log("Maxplayers has been set to " + Convert.ToString(Convert.ToInt32(Provider.maxPlayers) - Convert.ToInt32(Configuration.Instance.IncreasePlayersBy)));
                Provider.maxPlayers -= Configuration.Instance.IncreasePlayersBy;
            }
        }
    }
}
