using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Capybara : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _timeBetweenAttacks = 2f;
    [SerializeField] private float _navMeshMultiplier = 25f;

    [SerializeField] private AudioClip _audioClip;

    private Player _player;
    private Vector3 _target;
    private NavMeshAgent _navMeshAgent;
    private AudioSource _audioSource;
    private bool _isGameOver = false;
    private bool _attackInCooldown = false;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        _player.OnGameOver += GameOver;
        _player.OnGameRestart += GameRestart;
        _player.OnGameContinue += GameContinue;
    }

    private void OnDisable()
    {
        _player.OnGameOver -= GameOver;
        _player.OnGameRestart -= GameRestart;
        _player.OnGameContinue -= GameContinue;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        if (Random.Range(0, 21) == 0)
            _audioSource.clip = _audioClip;
        StartCoroutine(NewTargetPosition());
    }

    private void Update()
    {
        if (_isGameOver == false)
        {
            transform.LookAt(_player.transform);
            Move();
        }
        else if (_isGameOver == true)
        {
            transform.Rotate(new Vector3(0, 2f, 0));
            Quaternion rotateVector = transform.rotation;
            rotateVector.x = 0;
            rotateVector.z = 0;
            transform.rotation = rotateVector;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_attackInCooldown == false)
            {
                _attackInCooldown = true;
                _player.TakeDamage(_damage);
                StartCoroutine(StartAttackCooldown());
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator StartAttackCooldown()
    {
        yield return new WaitForSeconds(_timeBetweenAttacks);
        _attackInCooldown = false;
    }

    private void GameOver()
    {
        _isGameOver = true;
        _navMeshAgent.enabled = false;
        _audioSource.Pause();
    }

    private void GameRestart()
    {
        Destroy(gameObject);
    }

    private void GameContinue()
    {
        _isGameOver = false;
        _navMeshAgent.enabled = true;
        _audioSource.Play();
    }

    private void Move()
    {
        _navMeshAgent.SetDestination(_target);
    }

    private IEnumerator NewTargetPosition()
    {
        _target = _player.transform.position;
        yield return new WaitForSeconds(GetDistance(_player.transform.position)/_navMeshMultiplier);
        StartCoroutine(NewTargetPosition());
    }

    private float GetDistance(Vector3 target)
    {
        return Vector3.Distance(transform.position, target);
    }
}
