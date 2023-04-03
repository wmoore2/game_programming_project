Components to build:
    Scene:
        Open plane split up into a grid
        - 'heart' or something that the player needs to protect -> we call this the goober
        - enemy spawn point
        - some way to start next roung
    Obstacles
        - placing in open grid space
        - will carve itself out of the navmesh - super easy to do
    Towers
        - placed on obstacles, can be upgraded - rate of fire and damage
        - interact with obstacle to place and then upgrade afterwards
        - shoot enemies
    Player
        - shoots gun
        - picks up loot
        - builds towers/obstacles
        - how can player get on top of obstacles? or maybe not allowed, and make obstacles semi-transparent. Maybe the narrative could be that the obstacles are made of electricity and the goober is the power supply
    Building mode
        - how can we allow player to place stuff? special building mode that only allows placing of obstacles
        - building should check path - maybe store it as a graph?
    Dropped items
        - Dropped by enemies
        - currency displayed in HUD
    Enemies
        - Normal and elite - elite have more health, more damage, bigger, drop more loot
        - pathfinding through player placed obstacles - navmesh
        - attack player
        - drops scraps
    Gameplay loop
        - enemies come in waves that are predictable
        - next round is activated through the goober
        - allow round limit of some sort
    