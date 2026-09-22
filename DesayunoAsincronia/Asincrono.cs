namespace DesayunoAsincronia;

/// <summary>
/// Desayuno ASÍNCRONO: las tareas devuelven Task y no bloquean el hilo.
/// Incluye dos formas de ejecutarlo: secuencial con async/await y en paralelo.
/// </summary>
public class Asincrono
{
    public async Task PrepararAsync()
    {
        await HacerCafeAsync();
        await CalentarSartenAsync();
        await FreirHuevosAsync();
        await FreirBaconAsync();
        await TostarPanAsync();
        await UntarMantequillaAsync();
        await HacerZumoAsync();
    }
 
    public async Task PrepararEnParaleloAsync()
    {
        Task cafe = HacerCafeAsync();
        Task zumo = HacerZumoAsync();
        Task pan = PrepararTostadaAsync();
        Task sarten = PrepararSartenAsync();
 
        await Task.WhenAll(cafe, zumo, pan, sarten);
    }
 
    private async Task PrepararTostadaAsync()
    {
        await TostarPanAsync();
        await UntarMantequillaAsync();
    }
 
    private async Task PrepararSartenAsync()
    {
        await CalentarSartenAsync();
        await Task.WhenAll(FreirHuevosAsync(), FreirBaconAsync());
    }
 
    private Task HacerCafeAsync() => Task.Delay(200);
    private Task CalentarSartenAsync() => Task.Delay(200);
    private Task FreirHuevosAsync() => Task.Delay(300);
    private Task FreirBaconAsync() => Task.Delay(300);
    private Task TostarPanAsync() => Task.Delay(200);
    private Task UntarMantequillaAsync() => Task.Delay(100);
    private Task HacerZumoAsync() => Task.Delay(200);
}