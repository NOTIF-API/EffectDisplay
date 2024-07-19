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
# will the plugin be active?
is_enabled: false
# will information be displayed for the developer, will help when errors are detected
debug: false
# will a database be used
is_database_use: true
# these lines will be displayed for each effect type separately, allowing you to customize them
effect_line:
  Mixed: <size=12>%effect% is <color=\"purple\">Mixed</color> end after %time%|%duration%
  Positive: <size=12>%effect% is <color=\"green\">Positive</color> end after %time%|%duration%
  Negative: <size=12>%effect% is <color=\"red\">Negative</color> end after %time%|%duration%
# decomposes the text on the screen to change only to what is processed by align
hint_location: '<align=left>'
# defines a list of effects that the player will not see (the effects of the technical process are automatically hidden)
black_list:
- CardiacArrest
- InsufficientLighting
# https://discord.com/channels/656673194693885975/1172647045237067788/1172647045237067788 determines the name of the effect from the existing list to the one you specify
effect_translation:
  Bleeding: Blinded
# defines the database name in the path (required at the end of .db)
database_name: 'data.db'
# locates the database
path_to_data_base: 'C:\Users\NOTIF-ZTA23\AppData\Roaming\EXILED\Configs\EffectDisplay'
# List of roles for which the effects display will not be displayed (the roles of the dead are ignored)
ignored_roles:
  - Spectator
  - None
```
## How edit
| effect_line | description |
| ------------ | ----- |
| %duration%   | when specified in the line, it is replaced by the duration of the effect issued |
| %time%       | when specified in the line, it is replaced by the time after which the effect will end |
| %effect%     | when specified in a line, it is replaced with the name of the effect that is specified in `effect_translation` or its standard value |
