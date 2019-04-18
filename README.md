# Glitched Xtractor
[See it in English](https://github.com/glitchedhacktools/xtractor/readme=1#tutorial---english)
## Tutorial - Español
### 1. Extracción de las strings
![Ventana principal](/screenshots/xtractor2.PNG)

El funcionamiento es muy simple:
1. En primer lugar se debe cargar la ROM con el botón **Browse ROM...** (1).
1. Una vez se vea el nombre de la ROM en la barra de título, tan solo hay que escribir las direcciones de referencia en los cuadros **From Offset** y **To Offset** (3).
1. Opcionalmente, existe la opción **Try to get text only** (4) que permite filtrar automáticamente las strings del código.
1. Finalmente, sólo haría falta darle al botón **Extract strings** (2) para conseguir la lista.


### 2. Visualización de la lista de strings
![Archivo CSV resultado](/screenshots/xtractor3.PNG)

La lista de strings se guarda en formato CSV, el cuál es fácilmente modificable por Excel o similar (la mayoría de sistemas abren este formato con un editor de hojas de cálculo por defecto).
A continuación hay una recomendación sobre cómo trabajar con este fichero, pues será compatible con otra herramienta (en testeo actualmente).

5. **Offset:** Esta columna muestra el offset dónde encontrar la string.
1. **Changed:** En esta columna se debe indicar si el offset ha sido modificado por el usuario (Y) o no (N).
1. **Repointed:** Si la string ha sido repunteada, el nuevo offset debería ir aquí.
1. **String:** El contenido de la string se puede ver íntegro en esta columna.

No tiene pérdida y si la tuviera, estoy por el Discord bastante a menudo.

Notas:

* Si pones un rango muy grande de offsets puede tardar una eternidad.
* Es posible que, en el modo Try to get text only, se pase alguna string de largo si esta empieza por un caracter extraño.
* Puedes encontrar strings que no aparecen en scripts con el programa [Glitched Hex Transfer](https://whackahack.com/foro/t-56562/traduccion-hex-cristiano-cristiano-hex-glitched-hex-transfer)

## Tutorial - English
### 1. String Extraction
![Main Window](/screenshots/xtractor2.PNG)

Very simple operation:
1. First load the ROM with **Browse ROM...** button (1).
1. Once you see the ROM name in the title bar, just write the **From Offset** y **To Offset** (3) references.
1. Eventually, there is a **Try to get text only** (4) mode to automatically filter what is string and what is code.
1. Finally, just press **Extract strings** (2) button to get the string list.


### 2. Viewing the string list
![CSV result file](/screenshots/xtractor3.PNG)

The string list is saved in CSV file, which can be opened with Excel or similar spreadsheet software (CSV files are commonly opened by spreadsheet software by default).
Next there is a recommendation on how to work with this CSV file, so it will be able to be used with a new tool (currently in testing period).

5. **Offset:** The offset where to find each string.
1. **Changed:** Indicate here if the string was modifyed (Y) or not (N).
1. **Repointed:** If repointed, note here the new offset.
1. **String:** The full string content.

You can find us on Discord if you get lost.

Notes:

* A large range of offsets can last forever.
* The **Try to get text only** mode can skip some text if the string begins with an uncommon character.
* You can find hidden strings by using [Glitched Hex Transfer](https://whackahack.com/foro/t-56562/traduccion-hex-cristiano-cristiano-hex-glitched-hex-transfer)
