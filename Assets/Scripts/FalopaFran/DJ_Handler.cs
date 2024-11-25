using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ_Handler : MonoBehaviour
{
    public List<AudioSource> musicas = new List<AudioSource>();

    private AudioSource _currentAudioSource;
    public int currentAudioIndex;

    [Range(0.1f, 5f)] public float fadeDuration = 1f; // Duraci�n del fade en segundos.

    private static DJ_Handler instance; // Para manejar la persistencia �nica.

    private void Awake()
    {
        // Asegurarse de que solo exista una instancia de DJ_Handler.
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Si ya hay una instancia, destruir esta.
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Hacer que este objeto persista entre escenas.
    }

    private void Start()
    {
        if (musicas.Count > 0)
        {
            _currentAudioSource = musicas[currentAudioIndex];
            _currentAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("No hay m�sicas asignadas al DJ_Handler.");
        }
    }

    public void ChangeMusic(int index)
    {
        if (index < 0 || index >= musicas.Count )
            return; // Validaci�n: �ndice fuera de rango o es la misma m�sica.

        StartCoroutine(FadeMusic(index));
    }

    private IEnumerator FadeMusic(int newIndex)
    {
        // Fade out de la m�sica actual.
        if (_currentAudioSource != null)
        {
            float startVolume = _currentAudioSource.volume;

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                _currentAudioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
                yield return null;
            }

            _currentAudioSource.volume = 0;
            _currentAudioSource.Stop();
        }

        // Cambiar al nuevo AudioSource y hacer el fade in.
        _currentAudioSource = musicas[newIndex];
        currentAudioIndex = newIndex;
        _currentAudioSource.volume = 0;
        _currentAudioSource.Play();

        float targetVolume = 1f;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            _currentAudioSource.volume = Mathf.Lerp(0, targetVolume, t / fadeDuration);
            yield return null;
        }

        _currentAudioSource.volume = targetVolume;
    }
}
