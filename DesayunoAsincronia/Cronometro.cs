using System.Diagnostics;

namespace DesayunoAsincronia;

/// <summary>
/// Mide el tiempo que tarda en ejecutarse una acción.
/// </summary>
public static class Cronometro
{
    public static async Task MedirAsync(Func<Task> funcion)
    {
        Stopwatch sw = Stopwatch.StartNew();
        await funcion();
        sw.Stop();
 
        Console.WriteLine($"Tiempo transcurrido: {sw.ElapsedMilliseconds} ms");
    }
}