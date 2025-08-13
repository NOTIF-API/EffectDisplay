# EffectDisplay
[![Sponsor on DonatAlerts](https://img.shields.io/badge/sponsor-alerts-orange.svg)](https://www.donationalerts.com/r/asmrmilo21)
[![Download letest version](https://img.shields.io/badge/download-latest-red.svg)](https://github.com/NOTIF-API/EffectDisplay/releases)

This plugin for the game SCP:SL it allows the player to know its active effects.
# how to use plugin for server or client
1. **Client and display of information**.
   - Use the `.display` command in the game console to toggle the display of information on the player's screen.
   - You can also use the SSS(Server Specifiq Settings) function to turn on or off the display of active effects.
2. **Using the database and its features**.
   - In the `path_to_data_base` configuration, a path is automatically created and further work is carried out along it. You can specify the path to existing data, but for convenience I would use `separate file` to avoid competition.
   - The `is_database_use` configuration determines whether the plugin will use databases to store user selections. If the position is `false`, the user will not be able to turn off the display if desired. What will ensure stable display of active effects to all players without taking into account their wishes.
# What to do when errors are detected
**If you suddenly find an error or a flaw that needs to be corrected. If you see that the plugin is missing something, you can submit a suggestion about what could be added**.
1. You can contact me via [Issues](https://github.com/NOTIF-API/EffectDisplay/issues).
2. You can contact me via Discord under the name `notifapi` or `NOTIF` if you are looking for connections through servers with SCP or Exiled.
# Configs
```yaml
# will the plugin be active?
is_enabled: true
# will information be displayed for the developer, will help when errors are detected
debug: false
# will merge with other Hint service providers (for example HintServiceMeow) - if they are installed, it will switch itself
third_party: false
# will a database be used
data_base_enabled: true
# the time period for which information is updated
update_time: 0.9
# these lines will be displayed for each effect type separately, allowing you to customize them
effect_line:
  Negative: '<color=red>%effect%</color> -> %time%/%duration% LVL: %intensity%'
  Mixed: '<color=purple>%effect%</color> -> %time%/%duration% LVL: %intensity%'
  Technical: ''
  Positive: '<color=green>%effect%</color> -> %time%/%duration% LVL: %intensity%'
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
path_to_data_base: 'C:\Users\AnyUser\AppData\Roaming\EXILED\Configs\EffectDisplay'
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
# Will the plugin notify you of a new update
check_for_update: true
# SSS component ID for the main field item (do not duplicate with others)
header_id: 2030
# Display text when hovering over a question mark
header_description: 'Provides settings for Effect Display'
# SSS component ID for the TwoButton (do not duplicate with others)
two_button_id: 2031
# Name of TwoButton field
two_button_label: 'Time effect display'
# First option name
two_button_enabled: 'ON'
# Second option name
two_button_disabled: 'OFF'
```
## How edit
| effect_line | description |
| ------------ | ----- |
| %duration%   | Replaces the line with the maximum duration of the effect (I personally noticed a problem when using an item that gives effects more than 2 times, the duration is summed up and the effect time remains the same) |
| %time%       | Replaces the string with the remaining time until the end of the effect |
| %effect%     | Replaces with the effect name specified in the configuration or its standard form |
| %intensity%  | Replaces the effect strength which is calculated in game `byte`, that is from 0 to 255 |
