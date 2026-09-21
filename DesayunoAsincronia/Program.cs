using DesayunoAsincronia;

var sincrono = new Sincrono();
var asincrono = new Asincrono();
 
// La versión síncrona se lanza en un hilo del pool para poder medirla y
// aplicarle el timeout igual que a las demás
var ejecuciones = new (string Nombre, Func<Task> Accion)[]
{
    ("Secuencial (síncrona)",  () => Task.Run(sincrono.Preparar)),
    ("Asíncrona (async/await)", asincrono.PrepararAsync),
    ("Paralela (optimizada)",   asincrono.PrepararEnParaleloAsync),
};
 
Console.WriteLine("=== EJECUCIÓN NORMAL ===");
foreach (var (nombre, accion) in ejecuciones)
{
    Console.WriteLine($"\n--> {nombre}:");
    await Cronometro.MedirAsync(accion);
}
 
Console.WriteLine("\n=== EJECUCIÓN CON TIMEOUT (500 ms) ===");
foreach (var (nombre, accion) in ejecuciones)
{
    Console.WriteLine($"\n--> {nombre} con timeout:");
    await Cronometro.MedirAsync(() => EjecutorConTimeout.EjecutarAsync(accion, timeoutMs: 500));
}