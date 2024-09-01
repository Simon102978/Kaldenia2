# A fixer - Kaldenia2


```

# A faire ?

`
- Augmenter le niveau de hiding pour changer son nom (50 est un peu bas)
- Retirer la fonction pour pas collorer les trucs (le craft permet de faire une armure en Jolinar, et la garder en hue 0)
- Revoir le systeme d'armure, les armures donnent trop d'ar, et tu te retrouve avec 150 de resitance physique, quand le cap est a 86.
- Modifier le BedRollTent: 
   1- Temps de cast pour l'ouvrir
   2- Rajouter un Skills (Camping?)
   3- Bloquer si tu es pas sur Dirt, grass ou Désert
   4- Permettre au joueurs de la détruire
   5- Augmenter le prix a 10k ou le mettre en recompense de marchandises (?)
-Regarder les potions suivante : UltimeCurePotion, ElixirOfRebirth,BarrabHemolymphConcentrate, JukariBurnPoiltice, KurakAmbushersEssence, BarakoDraftOfMight,  UraliTranceTonic, SakkhraProphylaxisPotion, ScouringToxin 
-Revoir tout le systeme de taming.
-Revoir le systeme de Magie au complet.




```










# [ServUO]

[![Build Status](https://travis-ci.com/ServUO/ServUO.svg?branch=master)](https://travis-ci.com/ServUO/ServUO)
[![GitHub issues](https://img.shields.io/github/issues/servuo/servuo.svg)](https://github.com/ServUO/ServUO/issues)
[![GitHub release](https://img.shields.io/github/release/servuo/servuo.svg)](https://github.com/ServUO/ServUO/releases)
[![GitHub repo size](https://img.shields.io/github/repo-size/servuo/servuo.svg)](https://github.com/ServUO/ServUO/)
[![Discord](https://img.shields.io/discord/110970849628000256.svg)](https://discord.gg/0cQjvnFUN26nRt7y)
[![GitHub contributors](https://img.shields.io/github/contributors/servuo/servuo.svg)](https://github.com/ServUO/ServUO/graphs/contributors)
[![GitHub](https://img.shields.io/github/license/servuo/servuo.svg?color=a)](https://github.com/ServUO/ServUO/blob/master/LICENSE)


[ServUO] is a community driven Ultima Online Server Emulator written in C#.


#### Requirements

[.NET 5.0] Runtime and SDK


#### Windows

Run `Compile.WIN - Debug.bat` for development environments.
Run `Compile.WIN - Release.bat` for production environments.


#### OSX
```
brew install mono
brew install dotnet
dotnet build
```
To run `mono ServUO.exe`


#### Ubuntu / Debian
```
apt-get install zlib1g-dev mono-complete dotnet-sdk-5.0 
dotnet build
```
To run `mono ServUO.exe`


#### Summary

1. Starting with the `/Config` directory, find and edit `Server.cfg` to set up the essentials.
2. Go through the remaining `*.cfg` files to ensure they suit your needs.
3. For Windows, run `Compile.WIN - Debug.bat` to produce `ServUO.exe`, Linux users may run `Makefile`.
4. Run `ServUO.exe` to make sure everything boots up, if everything went well, you should see your IP adress being listened on the port you specified.
5. Load up UO and login! - If you require instructions on setting up your particular client, visit the Discord and ask!

   [ServUO]: <https://www.servuo.com>
   [.NET 5.0]: <https://dotnet.microsoft.com/download>
