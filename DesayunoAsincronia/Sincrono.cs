namespace DesayunoAsincronia;

/// <summary>
/// Desayuno SÍNCRONO: cada tarea bloquea el hilo hasta terminar (Thread.Sleep).
/// Nada se solapa: el tiempo total es la suma de todas las tareas.
/// </summary>
public class Sincrono
{
    public void Preparar()
    {
        HacerCafe();
        CalentarSarten();
        FreirHuevos();
        FreirBacon();
        TostarPan();
        UntarMantequilla();
        HacerZumo();
    }
 
    private void HacerCafe() => Thread.Sleep(200);
    private void CalentarSarten() => Thread.Sleep(200);
    private void FreirHuevos() => Thread.Sleep(300);
    private void FreirBacon() => Thread.Sleep(300);
    private void TostarPan() => Thread.Sleep(200);
    private void UntarMantequilla() => Thread.Sleep(100);
    private void HacerZumo() => Thread.Sleep(200);
}