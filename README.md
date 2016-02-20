# Mr Plat - WebGL Platform Game 


Mr. Plat is from a friendly world just travelling through the area. His ship crashes and now he must find all three new pieces of this ship to keep on travelling, plus some gold ... you know how it is....Platform game using Unity2D WebGL.

Role: Developer/Designer - C# scripting 

Description/Tech : The framework is pretty simple. It consists of view mediators, game manager/controller for each level and a persistent singleton model that is loaded throughout the game session. This game consists of multiple scenes that are loaded as needed. SplashScreen is the first scene to run and build from, it contains the loader. StartMenu, GameOverScene and FinalGameScreen take care of the navigation start and end of the game. The stage rounds are designed with Tile system. I used Tiled Map Editor application (http://www.mapeditor.org/) and a Unity utility (Tiled2Unity - http://www.seanba.com/introtiled2unity.html) to build the layout of the round background and foreground, including tile collision. The clouds in the background use a parallax method using the iTween plugin. The assets come from asset package by Kenney Vleugels (www.kenney.nl) and the Fantasy Mobile GUI by Konstantin Janson (konstantin.janson@freenet.de) and yuq229

Notes: Built with Unity 5.3.2f1 for WEBGL platform. Currently this game only works on the latest version of Firefox. It is keyboard arrow button based. forward, back and up (to jump).

