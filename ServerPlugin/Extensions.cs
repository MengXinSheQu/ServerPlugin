using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Lockers;
using PlayerRoles;
using System.Collections.Generic;
using System.Linq;

namespace ServerPlugin
{
    public static class Extensions
    {
        private static readonly List<Team> Foundation = new List<Team>()
        {
            Team.FoundationForces,
            Team.Scientists
        };
        private static readonly List<Team> ChaosInsurgency = new List<Team>()
        {
            Team.ChaosInsurgency,
            Team.ClassD
        };
        private static readonly List<Team> PluginTeam = new List<Team>()
        {
            Team.OtherAlive,
        };
        private static readonly List<Team> DeathTeam = new List<Team>()
        {
            Team.Dead,
        };
        private static readonly List<Team> SCPTeam = new List<Team>()
        {
            Team.SCPs,
        };
        public static bool GetTeam(this Player player, Player player1)
        {
            if (SCPTeam.Contains(player.Role.Team) && SCPTeam.Contains(player1.Role.Team))
                return true;
            if (DeathTeam.Contains(player.Role.Team) && DeathTeam.Contains(player1.Role.Team))
                return true;
            if (ChaosInsurgency.Contains(player.Role.Team) && ChaosInsurgency.Contains(player1.Role.Team))
                return true;
            if (Foundation.Contains(player.Role.Team) && Foundation.Contains(player1.Role.Team))
                return true;
            if (PluginTeam.Contains(player.Role.Team) && PluginTeam.Contains(player1.Role.Team))
                return true;
            return false;
        }
        public static string PlayerRole(this Player player)
        {
            if (player.IsScp)
            {
                switch (player.Role.Type)
                {
                    case RoleTypeId.Scp049:
                        return "<color=red>[SCP-049]</color>";
                    case RoleTypeId.Scp079:
                        return "<color=red>[SCP-079]</color>";
                    case RoleTypeId.Scp096:
                        return "<color=red>[SCP-096]</color>";
                    case RoleTypeId.Scp106:
                        return "<color=red>[SCP-106]</color>";
                    case RoleTypeId.Scp173:
                        return "<color=red>[SCP-173]</color>";
                    case RoleTypeId.Scp939:
                        return "<color=red>[SCP-939]</color>";
                    case RoleTypeId.Scp0492:
                        return "<color=red>[SCP-049-2]</color>";
                    case RoleTypeId.Scp3114:
                        return "<color=red>[SCP3114]</color>";
                }
            }
            else
            {
                switch (player.Role.Type)
                {
                    case RoleTypeId.None:
                        return "无";
                    case RoleTypeId.ClassD:
                        return "<color=orange>[D级人员]</color>";
                    case RoleTypeId.Scientist:
                        return "<color=yellow>[科学家]</color>";
                    case RoleTypeId.Spectator:
                        return "<color=white>[观察者]</color>";
                    case RoleTypeId.Tutorial:
                        return "<color=#FF00FF>[教程角色]</color>";
                    case RoleTypeId.Overwatch:
                        return "<color=#00FF00>[监管模式]</color>";
                    case RoleTypeId.ChaosConscript:
                        return "<color=green>[混沌征召兵]</color>";
                    case RoleTypeId.ChaosMarauder:
                        return "<color=green>[混沌掠夺者]</color>";
                    case RoleTypeId.ChaosRifleman:
                        return "<color=green>[混沌步枪手]</color>";
                    case RoleTypeId.ChaosRepressor:
                        return "<color=green>[混沌压制者]</color>";
                    case RoleTypeId.NtfSpecialist:
                        return "<color=#00FFFF>[NTF-收容专家]</color>";
                    case RoleTypeId.NtfPrivate:
                        return "<color=#00FFFF>[NTF-列兵]</color>";
                    case RoleTypeId.NtfCaptain:
                        return "<color=#00FFFF>[NTF-指挥官]</color>";
                    case RoleTypeId.NtfSergeant:
                        return "<color=#00FFFF>[NTF-中士]</color>";
                }
            }
            return "<color=white>[未知]</color>";
        }
        public static bool HasKeycardPermission(this Chamber locker, Player player)
        {
            foreach (Item item in player.Items.Where(x => x.Base is InventorySystem.Items.Keycards.KeycardItem keycardItem))
            {
                if (item is Keycard keycard && keycard.Permissions.HasFlagFast(locker.RequiredPermissions))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool HasKeycardPermission(this Generator generator, Player player)
        {
            foreach (Item item in player.Items)
            {
                if (item is Keycard keycard && keycard.Permissions.HasFlagFast(generator.KeycardPermissions))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool HasKeycardPermission(this Door door, Player player)
        {
            foreach (Item item in player.Items)
            {
                if (item is Keycard keycard && keycard.Permissions.HasFlagFast(door.KeycardPermissions))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
