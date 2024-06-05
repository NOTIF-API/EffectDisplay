# EffectDisplay
[![Sponsor on Patreon](https://img.shields.io/badge/sponsor-patreon-orange.svg)](https://www.patreon.com/NOTIF247)
[![Download letest version](https://img.shields.io/badge/download-latest-red.svg)](https://github.com/NOTIF-API/EffectDisplay/releases)

This plugin for the game SCP:SL it allows the player to know its active effects
# how to use
If you want to hide the display for yourself, you can write the command `display`
# What to do when eating bugs
If you find errors, you can contact me in any way convenient for you, but if you need my discord, then it is `notifapi`
Also, if you have ideas for adding something new, write there
# Configs
```yaml
effect_display:
# will the plugin be active?
  is_enabled: true
  # will information be displayed for the developer, will help when errors are detected
  debug: false
  # will a database be used
  is_database_use: true
  # This message will be displayed on each line
  effect_line_message: '<size=12>%effect% is %type% end after %time% second''s</size>'
  # effects combining harm and benefit and nothing (207 and its anti)
  mixed_effect: '<color=ff00cc>Mixed</color>'
  # Only good effect
  good_effect: '<color=green>Positive</color>'
  # Only bad effect
  bad_effect: '<color=red>Negative</color>'
  # decomposes the text on the screen to change only to what is processed by align
  hint_location: '<align=left>'
  # defines a list of effects that the player will not see (the effects of the technical process are automatically hidden)
  black_list:
  - InsufficientLighting
  - SoundtrackMute
  # https://discord.com/channels/656673194693885975/1172647045237067788/1172647045237067788 determines the name of the effect from the existing list to the one you specify
  effect_translation:
    Blinded: Blinded
  # defines the database name in the path (required at the end of .db)
  database_name: 'data.db'
  # locates the database
  path_to_data_base: 'C:\Users\NOTIF-ZTA23\AppData\Roaming\EXILED\Configs\EffectDisplay'
```
