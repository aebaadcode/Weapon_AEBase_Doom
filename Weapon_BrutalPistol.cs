datablock AudioProfile(BrutalDoom_PistolFireSound)
{
   filename    = "./BD_PistolFire.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(BrutalDoom_PistolReloadSound)
{
   filename    = "./BD_PistolReload.wav";
   description = AudioClose3d;
   preload = true;
};

AddDamageType("BrutalDoom_Pistol",'<bitmap:Add-ons/Weapon_AEBase_GCatsSpecialWeapons/Icons/CI_BrutalDoom_Pistol> %1','%2 <bitmap:Add-ons/Weapon_AEBase_GCatsSpecialWeapons/Icons/CI_BrutalDoom_Pistol> %1',0.2,1);

//////////
// item //
//////////
datablock ItemData(BrutalDoom_PistolItem)
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
	uiName = "Brutal Doom: Pistol";
	iconName = "./Icons/icon_BrutalDoom_Pistol";
	doColorShift = false;
	colorShiftColor = "1 1 1 1";

	 // Dynamic properties defined by the scripts
	image = BrutalDoom_PistolEquipImage;
	canDrop = true;

	AEAmmo = 15;
	AEType = AE_LightPAmmoItem.getID();
	AEBase = 1;
    doomSprite = BrutalDoom_PistolSpriteImage;
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

datablock ShapeBaseImageData(BrutalDoom_PistolSpriteImage)
{
   shapeFile = "base/data/shapes/empty.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 -0.25";
   eyeOffset = "0 0 0.4";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BrutalDoom_PistolItem;
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
datablock ShapeBaseImageData(BrutalDoom_PistolImage)
{
   // Basic Item properties
	shapeFile = "./BrutalDoom_Pistol/BrutalDoom_Pistol.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.01 0.05 0.25";
   eyeOffset = "-0.0022 0.1 -0.05";
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
   item = BrutalDoom_PistolItem;
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
	colorShiftColor = BrutalDoom_PistolItem.colorShiftColor;//"0.400 0.196 0 1.000";

	safetyImage = BrutalDoom_PistolBurstImage;
	reloadImage = BrutalDoom_PistolReloadImage;

	shellSound = AEShellSMG;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "0.5 0.5 0.5";
	bulletScale = "1 1 1";

	projectileDamage = 15;
	projectileCount = 1;
	projectileHeadshotMult = 2.8;
	projectileVelocity = 200;
	projectileTagStrength = 0.15;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

    directDamageType = $DamageType::BrutalDoom_Pistol;
    directHeadDamageType = $DamageType::BrutalDoom_Pistol;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 250; // m
	spreadBase = 250;
	spreadMin = 250;
	spreadMax = 500;

	screenshakeMin = "0.1 0.1 0.1"; 
	screenshakeMax = "0.15 0.15 0.15"; 

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
   // well as a no-ammo->dryfire BD_Idle state.
   // Initial start up state
	stateName[0]                     	= "Activate";
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "BD_Idle";

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateSequence[1]                = "BD_Idle";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "ReloadStart";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

// firing start

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "BD_Fire1";
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "BD_Fire1";
	stateTransitionOnTimeout[3]     = "BD_Fire2";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]			= 0.0325;
	stateSequence[3]                = "BD_Fire1";
	
	stateName[4]                    = "BD_Fire2";
	stateTransitionOnTimeout[4]     = "BD_Fire3";
	stateAllowImageChange[4]        = false;
	stateTimeoutValue[4]			= 0.03;
	stateSequence[4]                = "BD_Fire2";
	
	stateName[5]                    = "BD_Fire3";
	stateTransitionOnTimeout[5]     = "FireLoadCheckA";
	stateAllowImageChange[5]        = false;
	stateTimeoutValue[5]			= 0.03;
	stateSequence[5]                = "BD_Fire3";
	
// firing over
	
	stateName[6]				= "LoadCheckA";
	stateScript[6]				= "AEMagLoadCheck";
	stateTimeoutValue[6]			= 0.1;
	stateTransitionOnTimeout[6]		= "LoadCheckB";
	
	stateName[7]				= "LoadCheckB";
	stateTransitionOnAmmo[7]		= "Ready";
	stateTransitionOnNotLoaded[7] = "Empty";
	stateTransitionOnNoAmmo[7]		= "ReloadStart";

	stateName[8]				= "ReloadStart";
	stateTimeoutValue[8]			= 0.25;
	stateScript[8]				= "onReloadStart";
	stateTransitionOnTimeout[8]		= "Ready";
	stateWaitForTimeout[8]			= true;

	stateName[10]				= "FireLoadCheckA";
	stateScript[10]				= "AEMagLoadCheck";
	stateTimeoutValue[10]			= 0.0325;
	stateSequence[10]                = "BD_Idle";
	stateTransitionOnTimeout[10]		= "SemiAutoCheck";
	stateWaitForTimeout[10]			= true;
	
	stateName[11]				= "FireLoadCheckB";
	stateTransitionOnAmmo[11]		= "Ready";
	stateTransitionOnNoAmmo[11]		= "ReloadStart";	
	stateTransitionOnNotLoaded[11]  = "Empty";

	stateName[12]          = "Empty";
	stateSequence[12]                = "BD_Idle";
	stateTransitionOnTriggerDown[12]  = "Dryfire";
	stateTransitionOnLoaded[12] = "ReloadStart";
	stateScript[12]        = "AEOnEmpty";

	stateName[13]           = "Dryfire";
	stateSequence[13]                = "BD_Idle";
	stateTransitionOnTriggerUp[13] = "Empty";
	stateWaitForTimeout[13]    = false;
	stateScript[13]      = "onDryFire";
	
	stateName[14]           = "SemiAutoCheck";
	stateSequence[14]                = "BD_Idle";
	stateTransitionOnTriggerUp[14] = "FireLoadCheckB";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BrutalDoom_PistolImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
  %obj.playAudio(0, BrutalDoom_PistolFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function BrutalDoom_PistolImage::onDryFire(%this, %obj, %slot)
{
	serverPlay3D(GCats_EmptySound, %obj.getHackPosition());
}

// MAGAZINE DROPPING

function BrutalDoom_PistolImage::onReloadStart(%this,%obj,%slot)
{
    %obj.mountImage(%this.reloadImage, 0);
}

function BrutalDoom_PistolImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function BrutalDoom_PistolImage::onMount(%this,%obj,%slot)
{
    %obj.lastPistolMode = %this;
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BrutalDoom_PistolImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(BrutalDoom_PistolMagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "-0.25 0.1 0";
   rotation = eulerToMatrix( "0 0 0" );	
	
	casing = Doom_MagDebris;
	shellExitDir        = "0 1 0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;	
	shellVelocity       = 4.0;
	
	stateName[0]					= "Ready";
	stateTimeoutValue[0]			= 0.01;
	stateTransitionOnTimeout[0] 	= "EjectA";
	
	stateName[1]					= "EjectA";
	stateEjectShell[1]				= true;
	stateTimeoutValue[1]			= 1;
	stateTransitionOnTimeout[1] 	= "Done";
	
	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};

function BrutalDoom_PistolMagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

// RELOAD

datablock ShapeBaseImageData(BrutalDoom_PistolReloadImage)
{
   // Basic Item properties
	shapeFile = "./BrutalDoom_Pistol/BrutalDoom_Pistol.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.01 0.05 0.25";
   eyeOffset = "-0.0022 0.1 -0.05";
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
   item = BrutalDoom_PistolItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	doColorShift = false;
	colorShiftColor = BrutalDoom_PistolItem.colorShiftColor;//"0.400 0.196 0 1.000";

	sourceImage = BrutalDoom_PistolImage;

   //casing = " ";

// reload start

	stateName[0]				= "BD_Reload1";
	stateTimeoutValue[0]			= 0.03;
	stateScript[0]				= "onReloadStart";
	stateTransitionOnTimeout[0]		= "BD_Reload2";
	stateSequence[0]			= "BD_Reload1";
	
	stateName[1]				= "BD_Reload2";
	stateTimeoutValue[1]			= 0.0325;
	stateTransitionOnTimeout[1]		= "BD_Reload3";
	stateSequence[1]			= "BD_Reload2";

	stateName[2]				= "BD_Reload3";
	stateTimeoutValue[2]			= 0.03;
	stateTransitionOnTimeout[2]		= "BD_Reload4";
	stateSequence[2]			= "BD_Reload3";

	stateName[3]				= "BD_Reload4";
	stateTimeoutValue[3]			= 0.0325;
	stateTransitionOnTimeout[3]		= "BD_Reload5";
	stateSequence[3]			= "BD_Reload4";

	stateName[4]				= "BD_Reload5";
	stateTimeoutValue[4]			= 0.03;
	stateTransitionOnTimeout[4]		= "BD_Reload6";
	stateSequence[4]			= "BD_Reload5";

	stateName[5]				= "BD_Reload6";
	stateTimeoutValue[5]			= 0.0325;
	stateTransitionOnTimeout[5]		= "BD_Reload7";
	stateSequence[5]			= "BD_Reload6";

	stateName[6]				= "BD_Reload7";
	stateTimeoutValue[6]			= 0.03;
	stateTransitionOnTimeout[6]		= "BD_Reload8";
	stateSequence[6]			= "BD_Reload7";

	stateName[7]				= "BD_Reload8";
	stateTimeoutValue[7]			= 0.0325;
	stateTransitionOnTimeout[7]		= "BD_Reload9";
	stateSequence[7]			= "BD_Reload8";

	stateName[8]				= "BD_Reload9";
	stateTimeoutValue[8]			= 0.03;
	stateTransitionOnTimeout[8]		= "BD_Reload10";
	stateSequence[8]			= "BD_Reload9";

	stateName[9]				= "BD_Reload10";
	stateTimeoutValue[9]			= 0.0325;
	stateTransitionOnTimeout[9]		= "BD_Reload11";
	stateSequence[9]			= "BD_Reload10";

	stateName[10]				= "BD_Reload11";
	stateTimeoutValue[10]			= 0.0325;
	stateTransitionOnTimeout[10]		= "BD_Reload12";
	stateSequence[10]			= "BD_Reload11";

	stateName[11]				= "BD_Reload12";
	stateTimeoutValue[11]			= 0.03;
	stateTransitionOnTimeout[11]		= "BD_Reload13";
	stateSequence[11]			= "BD_Reload12";

	stateName[12]				= "BD_Reload13";
	stateTimeoutValue[12]			= 0.0325;
	stateTransitionOnTimeout[12]		= "BD_Reload14";
	stateScript[12]				= "onReloadEnd";
	stateSequence[12]			= "BD_Reload13";

	stateName[13]				= "BD_Reload14";
	stateTimeoutValue[13]			= 0.03;
	stateTransitionOnTimeout[13]		= "BD_Reload15";
	stateSequence[13]			= "BD_Reload14";

	stateName[14]				= "BD_Reload15";
	stateTimeoutValue[14]			= 0.0325;
	stateTransitionOnTimeout[14]		= "BD_Reload16";
	stateSequence[14]			= "BD_Reload15";

	stateName[15]				= "BD_Reload16";
	stateTimeoutValue[15]			= 0.03;
	stateTransitionOnTimeout[15]		= "BD_Reload17";
	stateSequence[15]			= "BD_Reload16";

	stateName[16]				= "BD_Reload17";
	stateTimeoutValue[16]			= 0.0325;
	stateTransitionOnTimeout[16]		= "BD_Reload18";
	stateSequence[16]			= "BD_Reload17";

	stateName[17]				= "BD_Reload18";
	stateTimeoutValue[17]			= 0.03;
	stateTransitionOnTimeout[17]		= "BD_Reload19";
	stateSequence[17]			= "BD_Reload18";

	stateName[18]				= "BD_Reload19";
	stateTimeoutValue[18]			= 0.0325;
	stateTransitionOnTimeout[18]		= "BD_Reload20";
	stateSequence[18]			= "BD_Reload19";

	stateName[19]				= "BD_Reload20";
	stateTimeoutValue[19]			= 0.03;
	stateTransitionOnTimeout[19]		= "Reloaded";
	stateSequence[19]			= "BD_Reload20";
	
// reload end
		
	stateName[20]				= "Reloaded";
	stateTimeoutValue[20]			= 0.1;
	stateScript[20]				= "onReloaded";
	stateTransitionOnTimeout[20]		= "Ready";
};

function BrutalDoom_PistolReloadImage::onReloadStart(%this,%obj,%slot)
{
   %obj.baadDisplayAmmo(%this);
   %obj.stopAudio(1); 
   %obj.playAudio(1, BrutalDoom_PistolReloadSound);
   %obj.reload3Schedule = %this.schedule(50,onMagDrop,%obj,%slot);
   %obj.reload4Schedule = schedule(getRandom(500,600),0,serverPlay3D,AEMagMetalAr @ getRandom(1,3) @ Sound,%obj.getPosition());
   %obj.aeplayThread(2, shiftRight);
}

// HIDES ALL HAND NODES

function BrutalDoom_PistolReloadImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BrutalDoom_PistolReloadImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

function BrutalDoom_PistolReloadImage::onReloaded(%this,%obj,%slot)
{
    if(%obj.lastPistolMode $= "") %obj.lastPistolMode = %this.sourceImage;
	%obj.mountImage(%obj.lastPistolMode, 0);
}

function BrutalDoom_PistolReloadImage::onReloadEnd(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	Parent::AEMagReloadAll(%this, %obj, %slot);
}

function BrutalDoom_PistolReloadImage::onMagDrop(%this,%obj,%slot)
{
	%a = new aiPlayer()
	{
		datablock = emptyPlayer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};
	%a.setDamageLevel(100);
	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(BrutalDoom_PistolMagImage,0);
	%a.schedule(1000,delete);
}

//EQUIP IMAGE

datablock ShapeBaseImageData(BrutalDoom_PistolEquipImage)
{
	shapeFile = "./BrutalDoom_Pistol/BrutalDoom_Pistol.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.01 0.05 0.25";
   eyeOffset = "-0.0022 0.1 -0.05";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = BrutalDoom_PistolItem;
   sourceImage = BrutalDoom_PistolImage;
   ammo = " ";
   melee = false;
   armReady = true;
   hideHands = false;
   doColorShift = false;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.1;
	stateTransitionOnTimeout[0]       	= "Done";
	stateSequence[0]			= "Equip";
	
	stateName[1]                     	= "Done";
	stateScript[1]				= "onDone";

};

function BrutalDoom_PistolEquipImage::onMount(%this,%obj,%slot)
{
    %obj.showText = 0;
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

function BrutalDoom_PistolEquipImage::onUnMount(%this, %obj, %slot)
{
	%obj.showText = 0;
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}

function BrutalDoom_PistolEquipImage::onDone(%this,%obj,%slot)
{
    if(%obj.lastPistolMode $= "") %obj.lastPistolMode = %this.sourceImage;
	%obj.mountImage(%obj.lastPistolMode, 0);
}




/// BURST FIRE MODE

// IGNORE THIS FOR NOW


datablock ShapeBaseImageData(BrutalDoom_PistolBurstImage)
{
   // Basic Item properties
	shapeFile = "./BrutalDoom_Pistol/BrutalDoom_Pistol.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.01 0.05 0.25";
   eyeOffset = "-0.0022 0.1 -0.05";
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
   item = BrutalDoom_PistolItem;
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
	colorShiftColor = BrutalDoom_PistolItem.colorShiftColor;//"0.400 0.196 0 1.000";

	isSafetyImage = true;	
    safetyImage = BrutalDoom_PistolImage;
	reloadImage = BrutalDoom_PistolReloadImage;

	shellSound = AEShellSMG;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "0.5 0.5 0.5";
	bulletScale = "1 1 1";

	projectileDamage = 15;
	projectileCount = 1;
	projectileHeadshotMult = 2.8;
	projectileVelocity = 200;
	projectileTagStrength = 0.15;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

    directDamageType = $DamageType::BrutalDoom_Pistol;
    directHeadDamageType = $DamageType::BrutalDoom_Pistol;

	spreadBurst = 3; // how much shots it takes to trigger spread i think
	spreadReset = 400; // m
	spreadBase = 750;
	spreadMin = 750;
	spreadMax = 1000;

	screenshakeMin = "0.1 0.1 0.1"; 
	screenshakeMax = "0.15 0.15 0.15"; 

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
   // well as a no-ammo->dryfire BD_Idle state.
   // Initial start up state
	
	stateName[0]                     	= "Activate";
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "BD_Idle";

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateSequence[1]                = "BD_Idle";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "ReloadStart";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

// firing start 1

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "BD_Fire1";
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "BD_Fire1";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]			= 0.0325;
	stateSequence[3]                = "BD_Fire1";
	
	stateName[4]				= "FireLoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.03;
	stateSequence[4]                = "BD_Fire2";
	stateTransitionOnTimeout[4]		= "FireLoadCheckB";
	
	stateName[5]				= "FireLoadCheckB";
	stateTransitionOnAmmo[5]		= "preFire2";
	stateTransitionOnNoAmmo[5]		= "FireLoadCheckA3";	
	stateTransitionOnNotLoaded[5]  = "Ready";
	
// firing over 1
	
// firing start 2

	stateName[6]                       = "preFire2";
	stateTransitionOnTimeout[6]        = "BD_Fire1B";
	stateScript[6]                     = "AEOnFire";
	stateFire[6]                       = true;
	stateEjectShell[6]                       = true;

	stateName[7]                    = "BD_Fire1B";
	stateTransitionOnTimeout[7]     = "FireLoadCheckA2";
	stateEmitter[7]					= AEBaseSmokeEmitter;
	stateEmitterTime[7]				= 0.05;
	stateEmitterNode[7]				= "muzzlePoint";
	stateAllowImageChange[7]        = false;
	stateTimeoutValue[7]			= 0.0325;
	stateSequence[7]                = "BD_Fire1";
	
	stateName[8]				= "FireLoadCheckA2";
	stateScript[8]				= "AEMagLoadCheck";
	stateTimeoutValue[8]			= 0.03;
	stateSequence[8]                = "BD_Fire2";
	stateTransitionOnTimeout[8]		= "FireLoadCheckB2";
	
	stateName[9]				= "FireLoadCheckB2";
	stateTransitionOnAmmo[9]		= "preFire3";
	stateTransitionOnNoAmmo[9]		= "FireLoadCheckA3";	
	stateTransitionOnNotLoaded[9]  = "Ready";
	
// firing over 2

// firing start 3

	stateName[10]                       = "preFire3";
	stateTransitionOnTimeout[10]        = "BD_Fire1C";
	stateScript[10]                     = "AEOnFire";
	stateFire[10]                       = true;
	stateEjectShell[10]                       = true;

	stateName[11]                    = "BD_Fire1C";
	stateTransitionOnTimeout[11]     = "BD_Fire2C";
	stateEmitter[11]					= AEBaseSmokeEmitter;
	stateEmitterTime[11]				= 0.05;
	stateEmitterNode[11]				= "muzzlePoint";
	stateAllowImageChange[11]        = false;
	stateTimeoutValue[11]			= 0.0325;
	stateSequence[11]                = "BD_Fire1";
	
	stateName[12]                    = "BD_Fire2C";
	stateTransitionOnTimeout[12]     = "BD_Fire3C";
	stateAllowImageChange[12]        = false;
	stateTimeoutValue[12]			= 0.03;
	stateSequence[12]                = "BD_Fire2";
	
	stateName[13]                    = "BD_Fire3C";
	stateTransitionOnTimeout[13]     = "FireLoadCheckA3";
	stateAllowImageChange[13]        = false;
	stateTimeoutValue[13]			= 0.03;
	stateSequence[13]                = "BD_Fire3";
	
	stateName[14]				= "FireLoadCheckA3";
	stateScript[14]				= "AEMagLoadCheck";
	stateTimeoutValue[14]			= 0.0325;
	stateSequence[14]                = "BD_Idle";
	stateTransitionOnTimeout[14]		= "SemiAutoCheck";
	
	stateName[15]				= "FireLoadCheckB3";
	stateTransitionOnAmmo[15]		= "Ready";
	stateTransitionOnNoAmmo[15]		= "ReloadStart";	
	stateTransitionOnNotLoaded[15]  = "Ready";
	
// firing over 3
	
	stateName[16]				= "LoadCheckA";
	stateScript[16]				= "AEMagLoadCheck";
	stateTimeoutValue[16]			= 0.1;
	stateTransitionOnTimeout[16]		= "LoadCheckB";
	
	stateName[17]				= "LoadCheckB";
	stateTransitionOnAmmo[17]		= "Ready";
	stateTransitionOnNotLoaded[17] = "Empty";
	stateTransitionOnNoAmmo[17]		= "ReloadStart";

// reload start

	stateName[18]				= "ReloadStart";
	stateTimeoutValue[18]			= 0.25;
	stateScript[18]				= "onReloadStart";
	stateTransitionOnTimeout[18]		= "Ready";
	stateWaitForTimeout[18]			= true;
	
// reload end
		
	stateName[19]				= "Reloaded";
	stateTimeoutValue[19]			= 0.1;
	stateScript[19]				= "AEMagReloadAll";
	stateTransitionOnTimeout[19]		= "Ready";

	stateName[20]          = "Empty";
	stateSequence[20]                = "BD_Idle";
	stateTransitionOnTriggerDown[20]  = "Dryfire";
	stateTransitionOnLoaded[20] = "ReloadStart";
	stateScript[20]        = "AEOnEmpty";

	stateName[21]           = "Dryfire";
	stateSequence[21]                = "BD_Idle";
	stateTransitionOnTriggerUp[21] = "Empty";
	stateWaitForTimeout[21]    = false;
	stateScript[21]      = "onDryFire";
	
	stateName[22]           = "SemiAutoCheck";
	stateSequence[22]                = "BD_Idle";
	stateTransitionOnTriggerUp[22] = "FireLoadCheckB3";
	stateTimeoutValue[22]			= 0.1;
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function BrutalDoom_PistolBurstImage::AEOnFire(%this,%obj,%slot)
{	
	BrutalDoom_PistolImage::AEOnFire(%this,%obj,%slot);
}

function BrutalDoom_PistolBurstImage::onDryFire(%this, %obj, %slot)
{
	BrutalDoom_PistolImage::onDryFire(%this,%obj,%slot);
}

function BrutalDoom_PistolBurstImage::onReloadEnd(%this,%obj,%slot)
{
	BrutalDoom_PistolImage::onReloadEnd(%this,%obj,%slot);
}

// MAGAZINE DROPPING

function BrutalDoom_PistolBurstImage::onReloadStart(%this,%obj,%slot)
{
	%obj.showText = 0;
	BrutalDoom_PistolImage::onReloadStart(%this,%obj,%slot);
}

function BrutalDoom_PistolBurstImage::onReady(%this,%obj,%slot)
{
	BrutalDoom_PistolImage::onReady(%this,%obj,%slot);
}

// HIDES ALL HAND NODES

function BrutalDoom_PistolBurstImage::onMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection") && %obj.showText == 1)
	{
		messageClient(%obj.client, '', "<font:arial bold:20>\c2Switched to Burst");
		%obj.showText = 0;
	}
	BrutalDoom_PistolImage::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function BrutalDoom_PistolBurstImage::onUnMount(%this,%obj,%slot)
{
	if(isObject(%obj.client) && %obj.client.IsA("GameConnection") && %obj.showText == 1)
	{
		messageClient(%obj.client, '', "<font:arial bold:20>Switched to Single");
		%obj.showText = 0;
	}
	BrutalDoom_PistolImage::onUnMount(%this,%obj,%slot);
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function BrutalDoom_PistolBurstImage::onMagDrop(%this,%obj,%slot)
{
	BrutalDoom_PistolImage::onMagDrop(%this,%obj,%slot);
}