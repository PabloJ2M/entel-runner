using UnityEngine;
using System.Collections;
using Gameplay.Movement;
using UnityEngine.InputSystem;
using Unity.Pool;
using System.Linq;

public class SpawnLagBoss : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] private float initialSpawnDelay = 30f;
    [SerializeField] private float respawnCooldown = 60f;

    [Header("Combat")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float attackInterval = 2f;
    [SerializeField] private float shotDelay = 0.8f;

    [Header("Paralysis")]
    [SerializeField] private int tapsToEscape = 8;
    [SerializeField] private float paralysisCooldown = 10f;
    [SerializeField] private GameObject paralysisUI;

    [Header("References")]
    [SerializeField] private GameObject bossVisuals;
    [SerializeField] private Transform[] lanes;
    [SerializeField] private SpawnerPointRandom[] spawnersToStop;

    [Header("Prefabs")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject counterPrefab;

    private int currentHealth;
    private bool bossActive;
    private bool paralyzed;

    private int currentTaps;
    private float nextParalysisTime;

    private GameObject player;
    private Jump playerJump;
    private Rigidbody2D playerRb;
    private Collider2D playerCollider;
    private float defaultGravity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerJump = player.GetComponent<Jump>();
            playerRb = player.GetComponent<Rigidbody2D>();
            playerCollider = player.GetComponent<Collider2D>();

            if (playerRb != null)
                defaultGravity = playerRb.gravityScale;
        }

        bossVisuals.SetActive(false);
        if (paralysisUI) paralysisUI.SetActive(false);

        Invoke(nameof(SpawnBoss), initialSpawnDelay);
    }

    private void Update()
    {
        if (!PlayerIsAlive())
        {
            if (bossActive) StopBoss();
            return;
        }

        if (!bossActive) return;

        if (paralyzed && Pointer.current != null)
        {
            if (Pointer.current.press.wasPressedThisFrame)
            {
                currentTaps++;
                if (currentTaps >= tapsToEscape)
                    EndParalysis();
            }
        }
    }

    private bool PlayerIsAlive()
    {
        if (player == null || !player.activeInHierarchy) return false;
        if (playerCollider != null && !playerCollider.enabled) return false;

        if (!paralyzed && playerJump != null && !playerJump.enabled)
        {
            return false;
        }

        return true;
    }

    public void SpawnBoss()
    {
        if (bossActive || !PlayerIsAlive()) return;

        bossActive = true;
        currentHealth = maxHealth;
        bossVisuals.SetActive(true);
        nextParalysisTime = Time.time + 5f;

        ToggleSpawners(false);
        StartCoroutine(CombatLoop());
    }

    private IEnumerator CombatLoop()
    {
        while (bossActive && currentHealth > 0)
        {
            if (!PlayerIsAlive()) { StopBoss(); yield break; }

            yield return new WaitForSeconds(attackInterval);

            if (paralyzed) continue;

            if (!PlayerIsAlive()) { StopBoss(); yield break; }

            float r = Random.value;

            if (r < 0.3f && Time.time >= nextParalysisTime)
            {
                nextParalysisTime = Time.time + paralysisCooldown;
                StartCoroutine(ParalysisAttack());
            }
            else if (r < 0.8f)
            {
                StartCoroutine(ShootHighLow());
            }
            else
            {
                DropCounter();
            }
        }
    }

    private IEnumerator ShootHighLow()
    {
        if (lanes.Length < 2) yield break;

        var sortedLanes = lanes.OrderBy(l => l.position.y).ToArray();
        Transform lowLane = sortedLanes[0];
        Transform highLane = sortedLanes[sortedLanes.Length - 1];

        bool startHigh = Random.value > 0.5f;

        Instantiate(projectilePrefab, startHigh ? highLane.position : lowLane.position, Quaternion.identity);

        yield return new WaitForSeconds(shotDelay);

        if (PlayerIsAlive())
        {
            Instantiate(projectilePrefab, startHigh ? lowLane.position : highLane.position, Quaternion.identity);
        }
    }

    private IEnumerator ParalysisAttack()
    {
        if (!PlayerIsAlive()) yield break;

        paralyzed = true;
        currentTaps = 0;

        if (paralysisUI) paralysisUI.SetActive(true);

        if (playerJump) playerJump.enabled = false;
        if (playerRb)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.gravityScale = 0;
        }

        while (paralyzed && PlayerIsAlive())
            yield return null;

        if (PlayerIsAlive())
        {
            EndParalysis();
        }
    }

    private void EndParalysis()
    {

        if (player != null && player.activeInHierarchy)
        {
            if (playerJump) playerJump.enabled = true;
            if (playerRb) playerRb.gravityScale = defaultGravity;
        }

        if (paralysisUI) paralysisUI.SetActive(false);

        paralyzed = false;
    }

    private void DropCounter()
    {
        if (!PlayerIsAlive()) return;
        Transform lane = lanes[Random.Range(0, lanes.Length)];
        Instantiate(counterPrefab, lane.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) DefeatBoss();
    }

    private void DefeatBoss()
    {
        bossActive = false;
        StopAllCoroutines();
        EndParalysis();

        bossVisuals.SetActive(false);
        ToggleSpawners(true);

        if (PlayerIsAlive())
        {
            Invoke(nameof(SpawnBoss), respawnCooldown);
        }
    }

    private void StopBoss()
    {
        bossActive = false;
        StopAllCoroutines();

        paralyzed = false;
        if (paralysisUI) paralysisUI.SetActive(false);

        if (bossVisuals) bossVisuals.SetActive(false);
        ToggleSpawners(true);
    }

    private void ToggleSpawners(bool state)
    {
        foreach (var s in spawnersToStop)
        {
            if (s != null) s.enabled = state;
        }
    }

    private void OnDisable()
    {
        paralyzed = false;
        if (paralysisUI) paralysisUI.SetActive(false);
    }
}