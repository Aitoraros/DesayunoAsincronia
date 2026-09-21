namespace DesayunoAsincronia;

/// <summary>
/// Ejecuta cualquier desayuno con un tiempo máximo de espera.
/// </summary>
public static class EjecutorConTimeout
{
    public static async Task EjecutarAsync(Func<Task> accionDesayuno, int timeoutMs = 500)
    {
        using var cts = new CancellationTokenSource();
 
        try
        {
            Task tareaDesayuno = accionDesayuno();
            Task tareaTimeout = Task.Delay(timeoutMs, cts.Token);
 
            Task tareaCompletada = await Task.WhenAny(tareaDesayuno, tareaTimeout);
 
            if (tareaCompletada == tareaTimeout)
            {
                Console.WriteLine("☕ ¡El café se ha enfriado! Los huevos y tostadas con café frío no tienen gracia...");
            }
            else
            {
                cts.Cancel();          // se cancela el Task.Delay del timeout
                await tareaDesayuno;   // lanza una posible excepcion del desayuno
                Console.WriteLine("✅ ¡Desayuno listo a tiempo!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}