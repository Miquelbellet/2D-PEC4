# PEC4
Practica PEC4 de l'assignatura de Programació de Videojocs en 2D.

Per veure i poder descarregar aquest projecte anar a: [https://gitlab.com/miquel.bellet/PEC4/-/tags/PEC4](https://gitlab.com/miquel.bellet/PEC4/-/tags/PEC4)

## Funcionament del joc
Aquest joc és una rèplica del joc "Worms Armagedon". El joc comença a la pantalla de menú on pots triar a quin mapa jugar i depenent del mapa hi haurà un nombre diferent de jugadors. Un cop entres a la partida, has d'acabar amb els enemics mitjançant les armes proporcionades i el primer grup que acabi amb tots els membres del grup contrari guanya la partida.
Es pot jugar contra enemics amb Intel·ligència Artificial o si sou dos jugadors, es pot desactivar la IA i jugar l'un contra l'altre.

## Com está fet
Aquest projecte de Unity està creat des de zero, sense fer servir el "template" proporcionat. S'ha creat una escena de menú on es pot seleccionar el mapa on es vol jugar la partida i si es vol jugar contra enemics amb Intel·ligència Artificial o sense ella. Un cop entrem a la partida es genera un terreny depenent del mapa que s'hagi seleccionat, obtingut del link proporcionat a la part inferior tot i que se li han aplicat uns quants canvis, i s'activen els personatges i les decoracions del mapa pertinent.
El joc consta de 13 "Scripts" per controlar el joc, 1 per controlar el Menú, 2 per controlar la generació del mapa i la seva col·lisió, 1 per controlar el joc en general i els torns a cada ronda, 1 per controlar la UI, 1 per controlar els efectes sonors, 1 per afegir funcions al Rigidbody per fer el tir parabòlic, 1 per l'atac aeri del personatge i 5 per controlar les accions del jugador.
Per activar els jugadors i saber quants n'hi ha a la partida i quin és el que està jugant, hi ha un objecte a l'escena amb 5 objectes on cada un té els jugadors per cada mapa que s'activen en començar la partida sabent quin mapa s'ha seleccionat. Un cop estan activats tots els jugadors, es posen en una llista i s'inicialitza un comptador que serà el que seleccioni quin jugador és el que està actiu en cada torn. Quan es vulgui canviar el jugador actiu només s'haurà de sumar +1 al comptador i activar el següent jugador de la llista.
Cada objecte de jugador té 5 "Scripts" afegits per controlar les seves accions, estan separats per controlar la vida i l'equip del jugador, les animacions que ha de fer en cada moment, detectar el moviment que pot fer, quines armes es poden fer servir i com funcionen aquestes i per últim, l'script que controla la Intel·ligència Artificial que s'activa quan és un enemic i li toca jugar.

Tots els sprites de Shovel Knight han sigut descarregat d'aquesta pàgina: [https://www.spriters-resource.com/pc_computer/shovelknight/](https://www.spriters-resource.com/pc_computer/shovelknight/)

Els efectes sonors i la música del videojoc han sigut descarregats d'aquesta página web: [https://www.sounds-resource.com/pc_computer/shovelknighttreasuretrove/sound/17148/](https://www.sounds-resource.com/pc_computer/shovelknighttreasuretrove/sound/17148/)

Video demostratiu del joc: 

Moltes gràcies i que gaudiu del joc!