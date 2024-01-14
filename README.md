# EffectDisplay
[![Sponsor on Patreon](https://img.shields.io/badge/sponsor-patreon-orange.svg)](https://www.patreon.com/NOTIF247)
[![Download letest version](https://img.shields.io/badge/download-latest-red.svg)](https://github.com/NOTIF-API/EffectDisplay/releases)

This plugin for the game SCP:SL it allows the player to know its active effects
# how to use
If you want to hide the display for yourself, you can write the command `turn off` and then you will no longer see the display, but if you, on the contrary, want to see it, then write `turn on`
# What to do when eating bugs
If you find errors, you can contact me in any way convenient for you, but if you need my discord, then it is `notifapi`
Also, if you have ideas for adding something new, write there
# Configs
```yaml
effect_display:
# will the plugin be enabled
  is_enabled: true
  # Whether the message from the plugin will be visible (Helps if you find a bug)
  debug: false
  # time during which the text is displayed (0.9 seconds is suitable if the average ping is 80 ms)
  text_update_time: 0.9
  # What form will the message about what effect the player has enabled
  effect_message: '<align=left><size=13>Effect: {effect} is {type} will end {duration} with {intensivity} intensivity</size></align>'
  # If you think about it, it's clear that these lines are inserted into {type}
  bad_type_writing: '<color=red>Negative</color>'
  # If you think about it, it's clear that these lines are inserted into {type}
  good_type_writing: '<color=green>Positive</color>'
  # If you think about it, it's clear that these lines are inserted into {type}
  mixed_type_writing: '<color=#FB00FF>Mixed</color>'
  # list of effects that will not be displayed (automatic here are the effects that are issued by the game as technical https://en.scpslgame.com/index.php?title=Status_Effects)
  black_list_effect:
  - SoundtrackMute
  - InsufficientLighting
  # how any effect will be displayed on the screen
  effect_name_display:
    AmnesiaItems: AmnesiaItems
    AmnesiaVision: AmnesiaVision
    Asphyxiated: Asphyxiated
    Bleeding: Bleeding
    Blinded: Blinded
    Burned: Burned
    Concussed: Concussed
    Corroding: Corroding
    Deafened: Deafened
    Decontaminating: Decontaminating
    Disabled: Disabled
    Ensnared: Ensnared
    Exhausted: Exhausted
    Flashed: Flashed
    Hemorrhage: Hemorrhage
    Invigorated: Invigorated
    BodyshotReduction: BodyshotReduction
    Poisoned: Poisoned
    Scp207: Scp207
    Invisible: Invisible
    SinkHole: SinkHole
    DamageReduction: DamageReduction
    MovementBoost: MovementBoost
    RainbowTaste: RainbowTaste
    SeveredHands: SeveredHands
    Stained: Stained
    Vitality: Vitality
    Hypothermia: Hypothermia
    Scp1853: Scp1853
    CardiacArrest: CardiacArrest
    InsufficientLighting: InsufficientLighting
    SoundtrackMute: SoundtrackMute
    SpawnProtected: SpawnProtected
    Traumatized: Traumatized
    AntiScp207: AntiScp207
    Scanned: Scanned
    PocketCorroding: PocketCorroding
    SilentWalk: SilentWalk
    Strangled: Strangled
    Ghostly: Ghostly
  # defines the path to the database (do not change unless necessary
  path_to_database: '{ExiledConfigPath}/EffectDisplay/Player.db'
  # use database for save user chose
  db_using: true
```
