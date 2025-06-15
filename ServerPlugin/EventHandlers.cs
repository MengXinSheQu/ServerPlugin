using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp330;
using MEC;
using System.Collections.Generic;
using System.Linq;

namespace ServerPlugin
{
    public class EventHandlers
    {
        /// <summary>
        /// <para>提供的API 列表储存无法免手持卡的玩家 该列表不会自动清除</para>
        /// <para>Plugin APIList , Contains can't use RemoteKeycard players, this list can't auto clear</para>
        /// </summary>
        public static List<Player> BlacklistPlayers = new List<Player>();
        public void RegEvent() 
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
            Exiled.Events.Handlers.Player.Spawned += OnSpawned;
            Exiled.Events.Handlers.Player.Dying += OnDying;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.DroppingAmmo += OnDroppingAmmo;
            Exiled.Events.Handlers.Player.DroppingItem += OnDroppingItem;
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            Exiled.Events.Handlers.Player.InteractingLocker += OnInteractingLocker;
            Exiled.Events.Handlers.Player.UnlockingGenerator += OnUnlockingGenerator;
            Exiled.Events.Handlers.Scp330.InteractingScp330 += OnInteractingScp330;
        }
        public void UnRegEvent()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStart;
            Exiled.Events.Handlers.Player.Spawned -= OnSpawned;
            Exiled.Events.Handlers.Player.Dying -= OnDying;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.DroppingAmmo -= OnDroppingAmmo;
            Exiled.Events.Handlers.Player.DroppingItem -= OnDroppingItem;
            Exiled.Events.Handlers.Player.InteractingDoor -= OnInteractingDoor;
            Exiled.Events.Handlers.Player.InteractingLocker -= OnInteractingLocker;
            Exiled.Events.Handlers.Player.UnlockingGenerator -= OnUnlockingGenerator;
            Exiled.Events.Handlers.Scp330.InteractingScp330 -= OnInteractingScp330;
        }
        private void OnSpawned(SpawnedEventArgs ev) 
        { 
            if(Plugin.Instance.Config.NoFog)
                ev.Player.EnableEffect(EffectType.FogControl);
        }
        private void OnInteractingScp330(InteractingScp330EventArgs Args)
        {
            if (Args.UsageCount < Plugin.Instance.Config.Candys)
                Args.ShouldSever = false;
        }
        private void OnHurting(HurtingEventArgs Args)
        {
            if (Args.DamageHandler.Type == DamageType.Scp207 && Plugin.Instance.Config.DisableSCP207Hurt)
                Args.Amount = 0f;
            if (Args.DamageHandler.Type == DamageType.Poison && Plugin.Instance.Config.DisablePoison)
                Args.Amount = 0f;
        }
        private void OnDroppingAmmo(DroppingAmmoEventArgs Args)
        {
            if (Plugin.Instance.Config.InfAmmo)
                Args.IsAllowed = false;
        }
        private void OnDroppingItem(DroppingItemEventArgs Args)
        {
            if (Args.Item.IsAmmo && Plugin.Instance.Config.InfAmmo)
                Args.IsAllowed = false;
        }
        private void OnRoundStart()
        {
            if (!Plugin.Instance.Config.InfAmmo)
                return;
            Timing.KillCoroutines($"{Plugin.Package}:InfAmmo");
            Timing.RunCoroutine(InfAmmo(), $"{Plugin.Package}:InfAmmo");
        }
        private void OnDying(DyingEventArgs ev)
        {
            if(Plugin.Instance.Config.InfAmmo)
                ev.Player.ClearAmmo();
        }
        private IEnumerator<float> InfAmmo()
        {
            ItemType[] Gun = 
            {
                ItemType.GunA7,
                ItemType.GunAK,
                ItemType.GunCOM15,
                ItemType.GunCOM18,
                ItemType.GunCom45,
                ItemType.GunCrossvec,
                ItemType.GunE11SR,
                ItemType.GunFSP9,
                ItemType.GunFRMG0,
                ItemType.GunLogicer,
                ItemType.GunRevolver,
                ItemType.GunShotgun,
            };
            while(true)
            {
                foreach (Player player in Player.List.Where(x => x.IsAlive))
                {
                    foreach (Item item in player.Items.Where(item => Gun.Contains(item.Type)))
                    {
                        switch (item.Type)
                        {
                            case ItemType.GunE11SR:
                            case ItemType.GunFRMG0:
                                if (!player.HasItem(ItemType.Ammo556x45))
                                    player.SetAmmo(AmmoType.Nato556, 120);
                                break;
                            case ItemType.GunAK:
                            case ItemType.GunA7:
                            case ItemType.GunLogicer:
                                if (!player.HasItem(ItemType.Ammo762x39))
                                    player.SetAmmo(AmmoType.Nato762, 120);
                                break;
                            case ItemType.GunCOM15:
                            case ItemType.GunCOM18:
                            case ItemType.GunCom45:
                            case ItemType.GunFSP9:
                            case ItemType.GunCrossvec:
                                if (!player.HasItem(ItemType.Ammo9x19))
                                    player.SetAmmo(AmmoType.Nato9, 120);
                                break;
                            case ItemType.GunRevolver:
                                if (!player.HasItem(ItemType.Ammo44cal))
                                    player.SetAmmo(AmmoType.Ammo44Cal, 120);
                                break;
                            case ItemType.GunShotgun:
                                if (!player.HasItem(ItemType.Ammo12gauge))
                                    player.SetAmmo(AmmoType.Ammo12Gauge, 14);
                                break;
                        }
                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }
        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!Plugin.Instance.Config.RemoteKeycard || BlacklistPlayers.Contains(ev.Player))
                return;
            if (ev.Player.IsScp && Plugin.Instance.Config.RemoteKeycard_Scp)
                return;
            if (ev.Door.HasKeycardPermission(ev.Player) && !ev.Door.IsLocked)
                ev.IsAllowed = true;
        }
        private void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
        {
            if (!Plugin.Instance.Config.RemoteKeycard || BlacklistPlayers.Contains(ev.Player))
                return;
            if (ev.Player.IsScp && Plugin.Instance.Config.RemoteKeycard_Scp)
                return;
            if (ev.Generator.HasKeycardPermission(ev.Player))
                ev.IsAllowed = true;
        }
        private void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (!Plugin.Instance.Config.RemoteKeycard || BlacklistPlayers.Contains(ev.Player))
                return;
            if (ev.Player.IsScp && Plugin.Instance.Config.RemoteKeycard_Scp)
                return;
            if (ev.InteractingChamber.HasKeycardPermission(ev.Player))
                ev.IsAllowed = true;
        }
    }
}
