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
  debug: true
  # will merge with other Hint service providers (for example HintServiceMeow) - if they are installed, it will switch itself
  third_party: true
  # will a database be used
  is_database_use: true
  # the time period for which information is updated
  update_time: 0.899999976
  # these lines will be displayed for each effect type separately, allowing you to customize them
  effect_line:
    Mixed: <color=purple>%effect%</color> - %time%/%duration% lvl %intensity%
    Positive: <color=green>%effect%</color> - %time%/%duration% lvl %intensity%
    Negative: <color=red>%effect%</color> - %time%/%duration% lvl %intensity%
    Technical: ' '
  # defines a list of effects that the player will not see (the effects of the technical process are hidden)
  black_list:
  - InsufficientLighting
  - SoundtrackMute
  - FogControl
  # https://discord.com/channels/656673194693885975/1172647045237067788/1172647045237067788 determines the name of the effect from the existing list to the one you specify
  effect_translation:
    None: UnkownEffect
  # defines the database name in the path (required at the end of .db)
  database_name: 'data.db'
  # folder location current database
  path_to_data_base: 'C:\Users\User\AppData\Roaming\EXILED\Configs\EffectDisplay'
  # List of roles for which the effects display will not be displayed (the roles of the dead are ignored without configs sets)
  ignored_roles:
  - None
  - Spectator
  # Standard settings for displaying information, used in the absence of any supported Hint providers
  native_hint_settings:
  # Text size
    font_size: 12
    # Text aligment
    aligment: 'Left'
  # If you use MeowHintService for Exiled then these settings will be useful for customizing the display
  meow_hint_settings:
  # Text size
    font_size: 16
    # Position Y Horizontal coordinate 0 -> 1080
    y_coordinate: 900
    # Position X Vertical coordinate -1200 -> 1200
    x_coordinate: -1200
    # Hint aligment (Left, Right, Center)
    aligment: 'Left'
    # Hint vertical aligment (Top, Bottom, Middle)
    vertical_aligment: 'Bottom'
  # What text will the user see when hovering over a question mark in the settings?
  enabled_display_description: 'Determines whether the display of enabled effects is enabled, replaces .display in the console'
```
## How edit
| effect_line | description |
| ------------ | ----- |
| %duration%   | Replaces the line with the maximum duration of the effect (I personally noticed a problem when using an item that gives effects more than 2 times, the duration is summed up and the effect time remains the same) |
| %time%       | Replaces the string with the remaining time until the end of the effect |
| %effect%     | Replaces with the effect name specified in the configuration or its standard form |
| %intensity%  | Replaces the effect strength which is calculated in game `byte`, that is from 0 to 255 |
