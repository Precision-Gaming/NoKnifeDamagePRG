using System;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;


namespace NoKnifeDamagePRG;


public class NoKnifeDamagePRG : BasePlugin
{
    public override string ModuleAuthor => "sphaxa";
    public override string ModuleName => "NoKnifeDamage for PRG";
    public override string ModuleVersion => "v1.0.2";

    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerHurt>(OnPlayerHurt, HookMode.Pre);
    }

    private HookResult OnPlayerHurt(EventPlayerHurt @event, GameEventInfo info)
    {
        if (!@event.Userid.IsValid)
        {
            return HookResult.Continue;
        }

        CCSPlayerController player = @event.Userid;

        if (player.Connected != PlayerConnectedState.PlayerConnected)
        {
            return HookResult.Continue;
        }

        if (!player.PlayerPawn.IsValid)
        {
            return HookResult.Continue;
        }

        if (@event.Weapon == "knife")
        {
            if (@event.Userid.PlayerPawn.Value.Health + @event.DmgHealth <= 100)
            {
                @event.Userid.PlayerPawn.Value.Health = @event.Userid.PlayerPawn.Value.Health + @event.DmgHealth;
            } else
            {
                @event.Userid.PlayerPawn.Value.Health = 100;
            }
        }

        @event.Userid.PlayerPawn.Value.VelocityModifier = 1;

        return HookResult.Continue;
    }
}