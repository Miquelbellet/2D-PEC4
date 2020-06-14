# PEC4
Practica PEC4 de l'assignatura de Programació de Videojocs en 2D.

Per veure i poder descarregar aquest projecte anar a: [https://gitlab.com/miquel.bellet/PEC4/-/tags/PEC4](https://gitlab.com/miquel.bellet/PEC4/-/tags/PEC4)

## Funcionament del joc
Aquest joc és una rèplica del joc "Shovel Knight". El joc comença a la pantalla de títol del joc on després pots ara a la pantalla del menú. Des d'allà pots començar a jugar, configurar les opcions del joc si n'hi haguessin o sortir del joc.
L'objectiu del joc és acabar amb el "Boss" final que et portarà a la pantalla de crèdits per si vols tornar-hi a jugar.
Durant el nivell es van activant uns punts de reaparició on si et mors podràs reaparèixer allà i en el lloc on moris apareixeran tres bosses d'or que si les reculls recuperaràs la meitat de l'or amb que t'havies mort.

## Com está fet
Aquest projecte de Unity consta de 4 escenes, la de títol, menú, joc i crèdits. Es fan servir 6 scrips de control d'escenes, 3 de control d'enemics, 5 pels objectes "Prefabs" creats i 2 per la lògica del personatge.
S'ha fet que l'objecte del personatge té 3 subfills amb trigger colliders que s'activen depenent de l'acció del jugador: Prémer la lletra "S" mentre s'està saltant activa "jumpAttack", l'atac simple cap a la dreta i l'atac simple cap a l'esquerre.
L'or s'obté amb les gemmes que es recullen al matar diferents enemics, destruir blocs de sorra o desenterrar tresors amagats. En una millora d'aquest projecte es podran comprar elements per millorar el nostre personatge amb aquestes gemmes.
Es va trigar bastant temps per organitzar, configurar i col·locar tots els diferents sprites tant de decoració com de terreny del joc. 

Tots els sprites de Shovel Knight han sigut descarregat d'aquesta pàgina: [https://www.spriters-resource.com/pc_computer/shovelknight/](https://www.spriters-resource.com/pc_computer/shovelknight/)

Els efectes sonors i la música del videojoc han sigut descarregats d'aquesta página web: [https://www.sounds-resource.com/pc_computer/shovelknighttreasuretrove/sound/17148/](https://www.sounds-resource.com/pc_computer/shovelknighttreasuretrove/sound/17148/)

Video demostratiu del joc: [https://www.youtube.com/watch?v=d2zp0hSKlK4](https://www.youtube.com/watch?v=d2zp0hSKlK4)

Moltes gràcies i que gaudiu del joc!