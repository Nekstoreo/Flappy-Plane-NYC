using UnityEngine;
using System.Collections;

public class SkyTransitionController : MonoBehaviour
{
    [Header("Sprite Renderers de los Cielos")]
    public SpriteRenderer skyDayRenderer;
    public SpriteRenderer skyEveningRenderer; // Noche
    public SpriteRenderer skyAfternoonRenderer; // Tarde

    [Header("Tiempos de Transición (en segundos desde el inicio del ciclo actual)")]
    public float timeToStartAfternoon = 60f; // Tarde
    public float timeToStartEvening = 120f; // Noche
    public float timeToReturnToDay = 180f; // Día
    public float transitionDuration = 3f;

    private float cycleTimer = 0f; // Timer para el ciclo actual
    // El orden correcto es: Día -> Tarde -> Noche -> Día
    private enum SkyState { Day, TransitionToAfternoon, Afternoon, TransitionToEvening, Evening, TransitionToDay }
    private SkyState currentSkyState = SkyState.Day;

    void Start()
    {
        if (skyDayRenderer == null || skyAfternoonRenderer == null || skyEveningRenderer == null)
        {
            Debug.LogError("Uno o más SpriteRenderers de cielo no están asignados en SkyTransitionController.");
            enabled = false;
            return;
        }

        // Asegurarse de que los tiempos de transición sean secuenciales
        if (!(timeToStartAfternoon < timeToStartEvening && timeToStartEvening < timeToReturnToDay)) {
            Debug.LogError("Los tiempos de transición no son secuenciales (timeToStartAfternoon < timeToStartEvening < timeToReturnToDay). Ajusta los valores.");
            enabled = false;
            return;
        }

        InitializeSkies();
    }

    void InitializeSkies()
    {
        SetSkyAlpha(skyDayRenderer, 1f);
        SetSkyAlpha(skyAfternoonRenderer, 0f);
        SetSkyAlpha(skyEveningRenderer, 0f);
        currentSkyState = SkyState.Day;
        cycleTimer = 0f; // Iniciar el contador del ciclo
    }

    void Update()
    {
        if (!enabled) return;

        cycleTimer += Time.deltaTime;

        // Transición a Tarde
        if (currentSkyState == SkyState.Day && cycleTimer >= timeToStartAfternoon)
        {
            currentSkyState = SkyState.TransitionToAfternoon;
            StartCoroutine(FadeSkies(skyDayRenderer, skyAfternoonRenderer, SkyState.Afternoon));
        }
        // Transición a Noche
        else if (currentSkyState == SkyState.Afternoon && cycleTimer >= timeToStartEvening)
        {
            currentSkyState = SkyState.TransitionToEvening;
            StartCoroutine(FadeSkies(skyAfternoonRenderer, skyEveningRenderer, SkyState.Evening));
        }
        // Transición de vuelta a Día
        else if (currentSkyState == SkyState.Evening && cycleTimer >= timeToReturnToDay)
        {
            currentSkyState = SkyState.TransitionToDay;
            StartCoroutine(FadeSkies(skyEveningRenderer, skyDayRenderer, SkyState.Day));
        }
    }

    void SetSkyAlpha(SpriteRenderer renderer, float alpha)
    {
        if (renderer != null)
        {
            Color color = renderer.color;
            color.a = Mathf.Clamp01(alpha);
            renderer.color = color;
            renderer.enabled = alpha > 0.001f;
        }
    }

    IEnumerator FadeSkies(SpriteRenderer skyToFadeOut, SpriteRenderer skyToFadeIn, SkyState nextStateAfterTransition)
    {
        float elapsedTime = 0f;
        if (skyToFadeIn != null) skyToFadeIn.enabled = true;

        // Guardar los colores originales para no afectar el RGB si ya tuvieran tintes
        Color originalFadeOutColor = skyToFadeOut != null ? skyToFadeOut.color : Color.white;
        Color originalFadeInColor = skyToFadeIn != null ? skyToFadeIn.color : Color.white;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaRatio = Mathf.Clamp01(elapsedTime / transitionDuration);

            if (skyToFadeOut != null) {
                originalFadeOutColor.a = 1f - alphaRatio;
                skyToFadeOut.color = originalFadeOutColor;
                skyToFadeOut.enabled = originalFadeOutColor.a > 0.001f;
            }
            if (skyToFadeIn != null) {
                originalFadeInColor.a = alphaRatio;
                skyToFadeIn.color = originalFadeInColor;
                skyToFadeIn.enabled = originalFadeInColor.a > 0.001f;
            }
            
            yield return null;
        }

        if (skyToFadeOut != null) SetSkyAlpha(skyToFadeOut, 0f);
        if (skyToFadeIn != null) SetSkyAlpha(skyToFadeIn, 1f);

        currentSkyState = nextStateAfterTransition;

        // Si hemos vuelto al estado de Día, reiniciamos el temporizador del ciclo.
        if (nextStateAfterTransition == SkyState.Day)
        {
            cycleTimer = 0f; // Reinicia el contador para el nuevo ciclo Día -> Tarde -> Noche -> Día
        }
    }
}