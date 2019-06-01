using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FriendlyBaseHealthController : MonoBehaviour
{
    [SerializeField]
    private int _hitPoints = 10;

    [SerializeField]
    private Text _healthText;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private AudioClip _damageSfx;

    [SerializeField]
    private ParticleSystem _deathFx;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _gameOverText.gameObject.SetActive(false);

        if (_healthText == null)
        {
            return;
        }

        _healthText.text = $"Base Health: {_hitPoints}";
    }

    private void OnTriggerEnter(Collider collider)
    {
        _hitPoints -= 1;
        _healthText.text = $"Base Health: {_hitPoints}";
        _audio.PlayOneShot(_damageSfx);
        if (_hitPoints == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            var deathFx = Instantiate(_deathFx, transform.position, Quaternion.identity);
            deathFx.Play();
            Destroy(deathFx.gameObject, deathFx.main.duration);
            Invoke("ResetGame", 3f);
        }
    }

    private void ResetGame() => SceneManager.LoadScene(0);
}
