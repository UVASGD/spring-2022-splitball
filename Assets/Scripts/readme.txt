Scripts:

bossAI: Contains the AI for the 'boss' prefab including phase logic, firing patterns and tracking HP
bossHit: Calculates how the boss takes damage using boxes located out of bounds, returns the ball when they go too far out of bounds, resets when takes damage
boxBullet: Logic process for each part of the boss that is launched then the bossAi fires it
bulletFire: the fire and collison detection for the bullets fired by the regular enemies
bulletSpawn: spawns the bullet in the apporprate rotation with offset and then fires it
bulletSpawnStraight: used by enemies to only have them fire in a line
enemyAiPath: ai for enemy that will follow target given to it
enemyAiStill: ai for enemy that will stay still and shoot
enemyAiPatrol: ai for enemy that will patrol two points while shooting
EnemyHealthBar: enemy health bar drawer
getScale: used to for collison detection in enemyAiPath to know when to slow down
playerBoundBoss: prevents player from leaving a box, bouncing them back inside

Interactable: given to objects that interact with others on the map, how the boost pad boosts the player and balls

BallArrow: responsable for drawing the arrow aiming the player toward the ball
CameraFollow: Follows given object in inspector while staying within a boundbox also given in the inspector
Line Drawer: draws a line between the two players, current purpose is used to track the midpoint between the two players which the camera follows
PlayerControler and Player2Controler: responsible for player control. Sets max speed, responsible for dashing (what happened when you pressed space in dashball), player collison with power ups and enemies, and hp
PlayerData: default hp and current hp of player

Ball: calculates the ball's position and ability to go through certain walls. check's interaction with stickypads. IS RESPONSIBLE FOR SCENE CHANGING WHEN IT REACHES THE GOAL OF EACH LEVEL. destroyes enemies. 
BoostPad: launches forward whatever is on it so long as it has the 'Interactable' componet 
Destructable: given to all things that can take damage and can die, tracks hp and calculates their death
Door: used to manipulate doors. Given buttons in array have to all be set to true before open animation plays
LogicActivator: given to buttons to determine state and what objects can turn off and on. used for stickybutton (only ball can activate), blue button (only ball can activate), red button (both can activate), and switch (turned on and off by either)
LogicInteractable: Given to objects so 'LogicActivator' knows if they can activate them or not
PortalIn: moves player from one portal to another. closes after.









