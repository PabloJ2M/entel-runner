using UnityEngine;
using System.Collections;
using TMPro;

namespace Tutorial
{
    public class TutorialDirector : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI tutorialText;

        [Header("Prefabs")]
        [SerializeField] private GameObject hazardPrefab;
        [SerializeField] private GameObject coinPrefab;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnX = 15f;
        [SerializeField] private Transform point1Transform;
        [SerializeField] private Transform point2Transform;
        [SerializeField] private float objectSpeed = 5f;

        [Header("Boss")]
        [SerializeField] private SpawnLagBoss bossScript;

        [Header("Real Game System")]
        [SerializeField] private GameObject spawnerParent;

        private float lowY;
        private float highY;

        private void Awake()
        {
            lowY = point1Transform.position.y;
            highY = point2Transform.position.y;
        }

        private IEnumerator Start()
        {
            spawnerParent.SetActive(false);
            yield return PracticaPhase();
            yield return RitmoPhase();
            yield return CoinsPhase();
            yield return BossPhase();
            yield return FinalPhase();
            gameObject.SetActive(false);
        }

        private IEnumerator PracticaPhase()
        {
            tutorialText.text = "SALTA PARA ESQUIVAR";

            for (int i = 0; i < 3; i++)
            {
                SpawnObject(hazardPrefab, lowY);
                yield return new WaitForSeconds(2.5f);
            }
        }

        private IEnumerator RitmoPhase()
        {
            tutorialText.text = "SALTA ENTRE ALTO Y BAJO";

            SpawnObject(hazardPrefab, lowY);
            yield return new WaitForSeconds(2f);

            SpawnObject(hazardPrefab, highY);
            yield return new WaitForSeconds(2f);

            SpawnObject(hazardPrefab, lowY);
            yield return new WaitForSeconds(2f);

            SpawnObject(hazardPrefab, lowY);
            yield return new WaitForSeconds(2f);

            SpawnObject(hazardPrefab, highY);
            yield return new WaitForSeconds(2f);
        }

        private IEnumerator CoinsPhase()
        {
            tutorialText.text = "RECOGE MONEDAS";

            SpawnObject(coinPrefab, lowY);
            yield return new WaitForSeconds(2f);

            SpawnObject(coinPrefab, highY);
            yield return new WaitForSeconds(2f);

            SpawnObject(coinPrefab, lowY);
            yield return new WaitForSeconds(3f);
        }

        private IEnumerator BossPhase()
        {
            tutorialText.text = "DERROTA AL BOSS - ESQUIVA Y RECOGE RAYOS";
            yield return new WaitForSeconds(1f);

            bossScript.SpawnBoss();

            while (bossScript.IsBossActive)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            tutorialText.text = "BOSS DERROTADO";
            yield return new WaitForSeconds(2f);
        }

        private IEnumerator FinalPhase()
        {
            tutorialText.text = "COMIENZA EL JUEGO";
            yield return new WaitForSeconds(1.5f);

            tutorialText.text = "";
            spawnerParent.SetActive(true);

            yield return new WaitForSeconds(0.5f);
        }

        private void SpawnObject(GameObject prefab, float yPosition)
        {
            Vector3 spawnPos = new Vector3(spawnX, yPosition, 0f);
            GameObject spawnedObject = Instantiate(prefab, spawnPos, Quaternion.identity);

            TutorialAutoMover mover = spawnedObject.GetComponent<TutorialAutoMover>();
            if (mover != null)
            {
                mover.SetSpeed(objectSpeed);
            }
        }
    }
}