datablock AudioProfile(Doom_PistolFireSound)
{
   filename    = "./dspistol.wav";
   description = LightClose3D;
   preload = true;
};

AddDamageType("Doom_Pistol",'<bitmap:Add-ons/Weapon_AEBase_GCatsSpecialWeapons/Icons/CI_Doom_Pistol> %1','%2 <bitmap:Add-ons/Weapon_AEBase_GCatsSpecialWeapons/Icons/CI_Doom_Pistol> %1',0.2,1);

//////////
// item //
//////////
datablock ItemData(Doom_PistolItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "base/data/shapes/empty.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Doom: Pistol";
	iconName = "./Icons/icon_Doom_Pistol";
	doColorShift = true;
	colorShiftColor = "1 1 1 1";

	 // Dynamic properties defined by the scripts
	image = Doom_PistolImage;
	canDrop = true;

	AEAmmo = 15;
	AEType = AE_LightPAmmoItem.getID();
	AEBase = 1;
    doomSprite = Doom_PistolSpriteImage;
	RPM = 1200;
	recoil = "Medium"; 
	uiColor = "1 1 1";
	description = "Gangsta niggas, PMC mercs, and law enforcement. The Glock 19 is a reliable and popular handgun among many fields of combat.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

package DOOMTESTItemPackage
{
	function ItemData::onAdd(%this, %obj)
	{
		%ret = parent::onAdd(%this, %obj);

		if (%obj.getDatablock().doomSprite !$= "")
		{
			%obj.mountImage(%obj.getDatablock().doomSprite,0);
		}
 //       talk(%obj.getDatablock().doomSprite);
		return %ret;
	}
};
activatePackage(DOOMTESTItemPackage);

datablock ShapeBaseImageData(Doom_PistolSpriteImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.25";
   eyeOffset = "0 0 0.4";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = Doom_PistolItem;
   ammo = " ";
   melee = false;
   armReady = true;
   doColorShift = false;
	
	stateName[0]                     	= "Activate";
	stateTransitionOnTimeout[0]       	= "yep";
	stateTimeoutValue[0]             	= inf;
	stateEmitter[0]					= Doom_PistolSpriteEmitter;
	stateEmitterTime[0]				= inf;
	stateEmitterNode[0]				= "muzzlePoint";
	
	stateName[1]                     	= "yep";
	stateTransitionOnTimeout[1]       	= "Activate";
	stateTimeoutValue[1]             	= inf;
	stateEmitter[1]					= Doom_PistolSpriteEmitter;
	stateEmitterTime[1]				= inf;
	stateEmitterNode[1]				= "muzzlePoint";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(Doom_PistolImage)
{
   // Basic Item properties
	shapeFile = "./Doom_Pistol/Doom_Pistol2.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.01 0.05 0.25";
   eyeOffset = "0 0.1 -0.04";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = Doom_PistolItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = Doom_PistolShellDebris;
   shellExitDir        = "1 0.2 1";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	doColorShift = false;
	colorShiftColor = Doom_PistolItem.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellSMG;
	shellSoundMin = 400; //min delay for when the shell sound plays
	shellSoundMax = 500; //max delay for when the shell sound plays

	muzzleFlashScale = "0.5 0.5 0.5";
	bulletScale = "1 1 1";

	projectileDamage = 18;
	projectileCount = 1;
	projectileHeadshotMult = 2.8;
	projectileVelocity = 200;
	projectileTagStrength = 0.15;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

    directDamageType = $DamageType::Doom_Pistol;
    directHeadDamageType = $DamageType::Doom_Pistol;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 250; // m
	spreadBase = 200;
	spreadMin = 200;
	spreadMax = 400;

	screenshakeMin = "0.025 0.025 0.025"; 
	screenshakeMax = "0.075 0.075 0.075"; 

	farShotSound = PistolADistantSound;
	farShotDistance = 40;
	staticTotalRange = 100;	
	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	projectileFalloffStart = 16;
	projectileFalloffEnd = 50;
	projectileFalloffDamage = 0.55;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.
   // Initial start up state
	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.25;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "Equip";

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;
	stateEmitter[2]					= AEBaseGenericFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	
	stateName[4]				= "LoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.1;
	stateTransitionOnTimeout[4]		= "LoadCheckB";
	
	stateName[5]				= "LoadCheckB";
	stateTransitionOnAmmo[5]		= "Ready";
	stateTransitionOnNotLoaded[5] = "Empty";
	stateTransitionOnNoAmmo[5]		= "Reload";

	stateName[6]				= "Reload";
	stateTimeoutValue[6]			= 0.85; //-0.15 for magazine weps since reloadend is now 0.25 instead of 0.1
	stateScript[6]				= "onReloadStart";
	stateTransitionOnTimeout[6]		= "ReloadEnd";
	stateWaitForTimeout[6]			= true;
	stateSequence[6]			= "Hidden";
    stateSound[6] = GCats_PistolReloadSound;
	
	stateName[7]				= "ReloadEnd";
	stateTimeoutValue[7]			= 0.25;
	stateScript[7]				= "onReloadEnd";
	stateTransitionOnTimeout[7]		= "Reloaded";
	stateSequence[7]			= "Equip";
	stateWaitForTimeout[7]			= true;
	
	stateName[8]				= "FireLoadCheckA";
	stateScript[8]				= "AEMagLoadCheck";
	stateTimeoutValue[8]			= 0.45;
	stateTransitionOnTimeout[8]		= "FireLoadCheckB";
	
	stateName[9]				= "FireLoadCheckB";
	stateTransitionOnAmmo[9]		= "Ready";
	stateTransitionOnNoAmmo[9]		= "Reload";	
	stateTransitionOnNotLoaded[9]  = "Ready";
		
	stateName[10]				= "Reloaded";
	stateTimeoutValue[10]			= 0.1;
	stateScript[10]				= "AEMagReloadAll";
	stateTransitionOnTimeout[10]		= "Ready";
	
	stateName[11]				= "ReadyLoop";
	stateTransitionOnTimeout[11]		= "Ready";

	stateName[12]          = "Empty";
	stateTransitionOnTriggerDown[12]  = "Dryfire";
	stateTransitionOnLoaded[12] = "Reload";
	stateScript[12]        = "AEOnEmpty";

	stateName[13]           = "Dryfire";
	stateTransitionOnTriggerUp[13] = "Empty";
	stateWaitForTimeout[13]    = false;
	stateScript[13]      = "onDryFire";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function Doom_PistolImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, Doom_PistolFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function Doom_PistolImage::onDryFire(%this, %obj, %slot)
{
	serverPlay3D(GCats_EmptySound, %obj.getHackPosition());
}

function Doom_PistolImage::onReloadEnd(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

// MAGAZINE DROPPING

function Doom_PistolImage::onReloadStart(%this,%obj,%slot)
{
   %obj.aeplayThread(2, shiftRight);
}

function Doom_PistolImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function Doom_PistolImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function Doom_PistolImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}