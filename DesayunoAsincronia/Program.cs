using DesayunoAsincronia;

var sincrono = new Sincrono();
var asincrono = new Asincrono();

Console.WriteLine("=== EJECUCIÓN NORMAL ===");

Console.WriteLine("\n--> Secuencial (síncrona):");
await Cronometro.MedirAsync(() => Task.Run(sincrono.Preparar));

Console.WriteLine("\n--> Asíncrona (async/await):");
await Cronometro.MedirAsync(asincrono.PrepararAsync);

Console.WriteLine("\n--> Paralela (optimizada):");
await Cronometro.MedirAsync(asincrono.PrepararEnParaleloAsync);

Console.WriteLine("\n=== EJECUCIÓN CON TIMEOUT (500 ms) ===");

Console.WriteLine("\n--> Secuencial (síncrona) con timeout:");
await Cronometro.MedirAsync(() =>
    EjecutorConTimeout.EjecutarAsync(() => Task.Run(sincrono.Preparar), timeoutMs: 500));

Console.WriteLine("\n--> Asíncrona (async/await) con timeout:");
await Cronometro.MedirAsync(() =>
    EjecutorConTimeout.EjecutarAsync(asincrono.PrepararAsync, timeoutMs: 500));

Console.WriteLine("\n--> Paralela (optimizada) con timeout:");
await Cronometro.MedirAsync(() =>
    EjecutorConTimeout.EjecutarAsync(asincrono.PrepararEnParaleloAsync, timeoutMs: 500));