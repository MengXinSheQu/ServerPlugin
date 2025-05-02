# 介绍:
<b>ServerPlugin 是一个携带多个基础功能的插件，例如:
- Scp207不掉血
- Scp1853与Scp207不中毒
- 无限子弹
- 基础聊天指令(.bc .cc .ac)
- Scp330拾取上限修改
- 感应卡

<b>这些功能是一个服务器上基础的功能</b>

# 使用方法:
将ServerPlugin放入EX对应的`Plugins`文件夹内，插件便会进行加载

目前只支持14.1beta版本的EX

# 配置文件:

下面是默认生成的配置文件 可以在原有的基础进行修改

<b>注: Exiled的配置文件位于Configs中，而是进行了拆分，该插件配置文件路径为:</b>

```\EXILED\Configs\Plugins\server_plugin```

```
# 启动插件
is_enabled: true
# 启动DeBug
debug: false
# SCP-207 & SCP-1853 no hurt /SCP-1853不中毒
disable_poison: true
# SCP-207不掉血
disable_s_c_p207_hurt: true
# 无限子弹
inf_ammo: true
# 无迷雾
no_fog: true
# 拾取Scp330最大数量
candys: 2
# 免手持卡
remote_keycard: true
# 免手持卡作用于SCP
remote_keycard__scp: false
# 旁观者可使用.bc
specator_chat: false
# 旁观者可使用聊天
mute_chat: false

```