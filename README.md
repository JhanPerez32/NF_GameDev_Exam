# NF_GameDev_Exam
Repository for Neun Farben Exam Game Dev

- NOTE: develop/subMain branch is my Temporary Main

# Unity Version used
Unity 6.0 (6000.0.50f1)

# Selected Project
Tower Defense Game

# Showcase
# Turret

Overview:

- It can be used for Ground or Air based on how the turret was setup. Its unique spray-and-pray mechanic unleashes a wide spread of bullets, saturating the target area rather than locking on to individual enemies.
      
Key Features:

- 360° Body Rotation: Smooth full-circle rotation lets the turret scan and engage threats from any direction. You can customize the rotation angle to limit its field of fire as needed.
      
- Adjustable Gun Firing Arc: Both the turret base and gun barrel have tunable rotation limits, allowing you to control the size of the firing cone and adapt to different combat scenarios.
      
- Spray-and-Pray Firing Mechanic: Bullets spread randomly within the firing arc rather than tracking targets precisely, creating a scattershot effect ideal for suppressing groups of enemies.

- Randomized Muzzle Flash effect when firing, firing logic is alternative in which if the Turret has multiple Gun, it will start firing first on the first barrel and up to the last barrel.

# Dynamic Turret Shop

Overview:
- The Dynamic Turret Shop is an in-game UI system that automatically populates a selection of turrets based on the TurretScriptable assets assigned in the Turret Options List within the Unity Editor. This system enables flexible turret configuration and rapid scalability without requiring manual changes to the shop UI.

Key Features:

- Automatically generates shop entries for each TurretScriptable added to the Turret Options list, displaying their respective icons, names, stats, and cost.

- Easily supports adding or removing turrets by simply modifying the list in the editor. No additional coding or UI setup required.

- Each turret is defined using a TurretScriptable asset that holds all relevant turret data (e.g., name, cost, damage, fire rate, icon).

- The shop presents turret options in a clean and organized layout, with buttons to preview or purchase each turret.
