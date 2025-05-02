using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;
using System.Linq;

namespace ServerPlugin.Command
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class TeamChat : ICommand,IUsageProvider
    {
        public string Command { get; } = "cc";
        public string[] Aliases { get; } = new string[1] { "cc" };
        public string Description { get; } = "阵营聊天";
        public string[] Usage { get; } = new string[] { "*文本" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender playerCommandSender)
            {
                Player player = Player.Get(playerCommandSender.PlayerId);
                if (player != null && player.IsVerified)
                {
                    if (arguments.Count > 0)
                    {
                        if ((player.IsMuted || player.IsGlobalMuted || player.IsIntercomMuted) && Plugin.Instance.Config.MuteChat)
                        {
                            response = "<color=red>你已被禁言 无法聊天！</color>";
                            return false;
                        }
                        Log.Info($"[Plugin-Team-Chat]-[{player.Nickname}][{player.UserId}]:[{string.Join(" ", arguments)}]");
                        foreach (Player target in Player.List.Where(x => x.GetTeam(player)))
                            target.Broadcast(4,$"<size=65%>[阵营聊天] {player.PlayerRole()} {player.Nickname} : {string.Join(" ", arguments)}</size>");
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
