using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Lockers;
using System.Linq;

namespace ServerPlugin
{
    public static class Extensions
    {
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
