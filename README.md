# Práctica: Desayuno Asíncrono

## Objetivo

Comprender la importancia del diseño en la preparación de un desayuno asíncrono y aprender a optimizar tiempos de ejecución mediante concurrencia y control de tiempo en C#.

## Descripción

El desayuno consta de 7 acciones con un tiempo de ejecución conocido, algunas de ellas dependientes entre sí:

| # | Acción | Tiempo | Dependencia |
|---|--------|--------|-------------|
| 1 | Hacer café | 200 ms | — |
| 2 | Calentar sartén | 200 ms | — |
| 3 | Freír huevos | 300 ms | Sartén caliente (2) |
| 4 | Freír bacon | 300 ms | Sartén caliente (2) |
| 5 | Tostar pan | 200 ms | — |
| 6 | Untar mantequilla | 100 ms | Pan tostado (5) |
| 7 | Hacer zumo | 200 ms | — |

El usuario dispone de un límite de **500 ms**. Si el desayuno no está listo a tiempo, se muestra:

> ☕ "¡El café se ha enfriado! Los huevos y tostadas con café frío no tienen gracia..."

Se han implementado tres formas de ejecución (secuencial, asíncrona y paralela optimizada), cada una también con un timeout de 500 ms, lo que da un total de 6 escenarios medidos.

## Estructura del proyecto

| Clase | Responsabilidad |
|---|---|
| `Sincrono` | Ejecución secuencial bloqueante (`Thread.Sleep`) |
| `Asincrono` | Ejecución secuencial con `async/await` y ejecución paralela optimizada, ambas con `Task.Delay` |
| `EjecutorConTimeout` | Envuelve cualquier ejecución con un límite de tiempo (`Task.WhenAny`) |
| `Cronometro` | Mide y muestra el tiempo de cada ejecución |
| `Program` | Orquesta los 6 escenarios |

## Resultados

| Escenario | Sin timeout | Con timeout (500 ms) |
|---|---|---|
| Secuencial (síncrona) | 1563 ms | 515 ms — ☕ café frío |
| Asíncrona (async/await) | 1549 ms | 510 ms — ☕ café frío |
| Paralela (optimizada) | 509 ms | 510 ms — ☕ café frío |

## Preguntas

**1. ¿Qué diferencias has observado entre las soluciones?**

La secuencial y la de async/await tardan lo mismo, ~1550 ms, porque ambas esperan cada tarea antes de pasar a la siguiente. La diferencia entre ellas es que la asíncrona no bloquea el hilo mientras espera, aunque el tiempo total no cambie. La paralela tarda ~509 ms, unas tres veces menos, porque lanza a la vez las tareas independientes.

**2. ¿Qué acciones se pueden ejecutar a la vez y cuáles no? ¿Por qué?**

Café, zumo, tostada y sartén son independientes y pueden ir a la vez. Huevos y bacon necesitan la sartén caliente, y la mantequilla necesita el pan tostado, así que dependen de un paso anterior. El camino más largo es sartén → huevos/bacon (200 + 300 = 500 ms), y marca el tiempo mínimo posible.

**3. ¿Qué ha pasado con cada solución al introducir el timeout?**

Las tres saltaron al mensaje del café frío. La secuencial (515 ms) y la asíncrona (510 ms) tardan ~1550 ms sin timeout, así que su corte a los 500 ms era esperable. La paralela es la más reveladora: sin timeout tarda 509 ms, 9 ms por encima del límite de 500 ms, así que también falla, aunque por muy poco margen. Confirma que 500 ms es un límite demasiado ajustado incluso para la solución óptima.

**4. ¿El enfoque con mejor rendimiento es también el más seguro? ¿Por qué?**

No. Es el más rápido, pero las mediciones lo demuestran: al no tener margen sobre el límite de 500 ms, acaba fallando el timeout igual que las otras dos. Rendimiento y fiabilidad son cosas distintas: rápidez pero sin margen sigue siendo poco seguro.

**5. ¿Merece la pena complicarse con paralelismo o con control de tiempo?**

Sí, cuando hay tareas independientes y un límite de tiempo real: aquí bajó de 1550 a 509 ms. Pero los datos muestran que no basta con paralelizar, hace falta dejar margen respecto al límite, si no, el control de tiempo salta igualmente y toda la optimización se pierde.
