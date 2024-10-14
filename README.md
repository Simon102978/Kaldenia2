# A fixer - Kaldenia 2

Systeme d'armure / arme :

-- Revoir le systeme d'armure, les armures donnent trop d'ar, et tu te retrouve avec 150 de resitance physique, quand le cap est a 86.
-- Luck armure d'arachnide + utiliter de la luck
-- Retirer la fonction pour pas collorer les trucs (le craft permet de faire une armure en Jolinar, et la garder en hue 0)
-- Dans baseweapon, suprimer : 
            if (Quality == ItemQuality.Exceptional)
            {
                double div = Siege.SiegeShard ? 12.5 : 20;

                Attributes.WeaponDamage += (int)(from.Skills.ArmsLore.Value / div);
                from.CheckSkill(SkillName.ArmsLore, 0, 100);
            }
--le bonus de qualité s'applique deux fois lorsque l'item est crafté (baseweapon) épic devrait être 40% damage increase, si crafté tombe à 80. je l'ai laissez comme ça pour K2. à retirer pour un prochain.
--Rendre reparable capes de courage et compagnie.

Systeme de maison
--Faire en sorte que les loyer soit prelever automatiquement apres les salaires, au lieux d'etre activé manuellement.
--Bug qui fait en sorte que les houses soit vider (Je soupsonne que les houses etait mal setter, ou que le owner avait plus de cash.)
--Quand on touche a l'area d'une maison custom et qui a une vanity, celle si disparait.
--Les crops plantées dans une maison se suppriment apres un certain temps

Suprimer le systeme d enchantement.
--revamper runechisel
--C'est assez simple en fait, lorsqu'on désenchante une pièce d'équipement qui a été enchanté avec une rune de resistance, le bonus reste... ce qui fait qu'on peut stacké sans trop de mal ses resists et je ne pense pas que ca soit voulu ainsi
--Rune de best weapon vs arcs.

Revoir le systeme de Magie au complet.
--Sort de paladinats // bushido et regs.
--Protection en arcane retire entierement le MR et 13 physical resist.
--Valider que les aoe touche pas les invocations pour les sortilèges.
--Gift of renewal Bypass l'assomage
--Certains summons sont trop fort // Revoir le systeme de dispel.

Revoir tout le systeme de taming.
--La résurrection de pets semble avoir un problème. La perte de skill fonctionne et cest tout à fait légit, par contre il doit y avoir un conflit entre la perte de skill et le script de level. Mon ostard est rendu niveau 10 / 17 et elle n'a plus aucun bonus de vie que j'ai sélectionner en la montant de niveau. Elle est de retour à sa vie de départ.
--Aussi on ne peut pas augmenter les résistances comme cela est indiquer. On obtient toujours un message d'erreur comme dequoi on a plus assez de ''trait'' pour l'augmenter. On est limiter à pouvoir monter la vie d'environ +12 après tu peux juste choisir stamina ou mana qui sont useless (mana serait pratique pour un pet qui cast quand même).
--Quand on délog, le systeme ne calcule plus nos points de famillier supplémentaire donc tout ce qui dépasse 3 s'envoit en stable.
--Faire de quoi, pour limiter la resurection des pets, c'est juste trop fort :p
--les animaux tamed sont beaucoup trop fort. - virer Jako de basecreature et opter pour le newanimalloregump plutôt.

Skills 
--Augmenter le range de tracking
--Augmenter le niveau de hiding pour changer son nom (50 est un peu bas)

Classe
-- evolution de Styliste Epicier
-- Possibilité si la classe est une classe artisane, de combiner avec ton metier pour arriver a un metier combiner.
-- Point d'evolution au niveau 30 -- faire un autre palier de classe ?

Systeme de fe
--FAire en sorte que seul le main personnage (Perso avec le plus de Fe), puisse avoir le boost x3 de fe // et faire que le boost soit aussi dynamique, qui varie en fonction du max 
--Ajout de fiole d'experience Négative

Esclaves: 
--les esclaves sont beaucoup trop fort
--Barde // Systeme d'Armure
--Equiper des arcs ? 
--Montage de skills ?
--Enlever le bonded ?

-Autres
--Le point cest plus que si le + 5 stats qui prends autour de 1 minute a s'activer Fuck aik les Potions, qui sont rendu juste Outright Meilleure on sentends, rendu la ca sert pu a rien de donner le + 5 , surtout que le SKill a 0 incidence sur les stats donner.
--Regarder les potions suivante : UltimeCurePotion, ElixirOfRebirth,BarrabHemolymphConcentrate, JukariBurnPoiltice, KurakAmbushersEssence, BarakoDraftOfMight,  UraliTranceTonic, SakkhraProphylaxisPotion, ScouringToxin 
--Les idoles, font en sorte que si un monstres a ete tamer lors de l'idole, celui-ci peut etre deleter avec la fin de l'idole.
--Les monstres des idoles pouvaient entrer en ville.
--Equiper les items movable false
--Checker les pirate capitaine, pourquoi y'en a pas de spawner (Le boat one, pas les nouveau customs qui ont ete rajouter.)
--ça serait pas pire un blank parfum ou on peut changer le hue et le nom
--on peut bucher sur le sable et ça nous pop des buches
--si tous les joueurs sont mort dans le rayon des monstres sur l'eau ils deviennent invisible et le bateau reste pris. 
--.Posseder et Basehire - perte du npc lors de la mort, -- Mettre le systeme de classe qui se transfere.
-- Modifier le BedRollTent: 
   1- Temps de cast pour l'ouvrir
   2- Rajouter un Skills (Camping?)
   3- Bloquer si tu es pas sur Dirt, grass ou Désert
   4- Permettre au joueurs de la détruire
   5- Augmenter le prix a 10k ou le mettre en recompense de marchandises (?)
--Dynamiser le système d'achat et vente aux NPC, plus tu achète moins c'est cher plus tu vends moins tu obtient. revoir comment le npc rachète les trucs pour moins limiter les items acceptés
--Mettre un temps de cast sur les cordes de bateau, pour monter et desscendre
--Dans le menu de couture, dans `Divers, si on fabrique draps, ca transforme nos tissus en vetements (apparence gumps de tissus combiner) et on ne peux pas pratiquer avec.
--elven stove n'est pas considerer comme une source de chaleur pour faire fondre la cire.


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
