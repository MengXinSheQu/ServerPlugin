using CommandSystem;
using Exiled.API.Features;
using PlayerRoles;
using RemoteAdmin;
using System;

namespace ServerPlugin.Command
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class BroadcastChat : ICommand, IUsageProvider
    {
        public string Command { get; } = "bc";
        public string[] Aliases { get; } = new string[1] { "bc" };
        public string Description { get; } = "全局聊天";
        public string[] Usage { get; } = new string[] { "*文本" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender playerCommandSender)
            {
                Player player = Player.Get(playerCommandSender.PlayerId);
                if (player.IsVerified && player != null)
                {
                    if (arguments.Count > 0)
                    { 
                        if ((player.IsMuted || player.IsGlobalMuted || player.IsIntercomMuted))
                        {
                            if (Plugin.Instance.Config.MuteChat)
                            {
                                response = "<color=red>你已被禁言 无法聊天！</color>";
                                return false;
                            }
                        }
                        if (player.Role.Team == Team.Dead && !Plugin.Instance.Config.SpecatorChat)
                        {
                            response = "<color=red>你现在不能说话！</color>";
                            return false;
                        }
                        else
                        {
                            Map.Broadcast(4, $"<size=65%>{player.PlayerRole()} {player.Nickname} : {string.Join(" ", arguments)}</size>");
                            Log.Info($"[Plugin-Global-Chat]-[全局聊天][{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                            response = "<color=green>信息已发送!</color>";
                            return true;
                        }
                    }
                    else
                    {
                        response = "<color=red>请输入文本!</color>";
                        return false;
                    }
                }
                else
                {
                    response = "<color=red>你还没加入服务器！</color>";
                    return false;
                }
            }
            
            response = "<color=green>信息已发送!</color>";
            return true;
        }
    }
}
