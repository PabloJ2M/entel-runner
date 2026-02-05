using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

namespace Tutorial
{
    public class TutorialDirector : MonoBehaviour
    {
        //    [Header("UI")]
        //    [SerializeField] private GameObject tutorialPanel;
        //    [SerializeField] private TextMeshProUGUI instructionText;
        //    [SerializeField] private TextMeshProUGUI actionText;

        //    [Header("Prefabs")]
        //    [SerializeField] private GameObject hazardPrefab;
        //    [SerializeField] private GameObject coinPrefab;

        //    [Header("Spawn Settings")]
        //    [SerializeField] private float spawnX = 15f;
        //    [SerializeField] private Transform point1Transform;
        //    [SerializeField] private Transform point2Transform;
        //    [SerializeField] private float objectSpeed = 5f;

        //    [Header("Boss")]
        //    [SerializeField] private SpawnLagBoss bossScript;

        //    [Header("Real Game System")]
        //    [SerializeField] private GameObject spawnerParent;

        //    private float lowY;
        //    private float highY;
        //    private bool playerJumped = false;
        //    private int coinsCollected = 0;

        //    private void Awake()
        //    {
        //        lowY = point1Transform.position.y;
        //        highY = point2Transform.position.y;
        //    }

        //    private IEnumerator Start()
        //    {
        //        spawnerParent.SetActive(false);

        //        yield return new WaitForSecondsRealtime(2f);
        //        yield return Step1_PracticarSalto();
        //        yield return Step2_SaltoAlternado();
        //        yield return Step3_IntroduccionMonedas();
        //        yield return Step4_RecogerMonedas();
        //        yield return Step5_IntroduccionBoss();
        //        yield return Step6_DerrotarBoss();
        //        yield return Step7_Finalizacion();

        //        gameObject.SetActive(false);
        //    }

        //    private IEnumerator Step1_PracticarSalto()
        //    {
        //        ShowInstruction(
        //            "ESQUIVA OBSTACULOS",
        //            "Los obstaculos se acercan desde la derecha\nToca para saltar y esquivarlos"
        //        );

        //        yield return WaitForJump();
        //        HideInstruction();

        //        int jumpsNeeded = 3;
        //        int jumpsCompleted = 0;

        //        while (jumpsCompleted < jumpsNeeded)
        //        {
        //            SpawnObject(hazardPrefab, lowY);

        //            playerJumped = false;
        //            yield return WaitForJump();
        //            jumpsCompleted++;

        //            yield return new WaitForSeconds(2f);
        //        }

        //        yield return new WaitForSeconds(1f);
        //    }

        //    private IEnumerator Step2_SaltoAlternado()
        //    {
        //        ShowInstruction(
        //            "CAMBIA DE CARRIL",
        //            "Algunos obstaculos vienen arriba, otros abajo\nSalta entre los carriles para esquivarlos"
        //        );

        //        yield return WaitForJump();
        //        HideInstruction();

        //        float[] pattern = { lowY, highY, lowY, highY, lowY };

        //        for (int i = 0; i < pattern.Length; i++)
        //        {
        //            SpawnObject(hazardPrefab, pattern[i]);
        //            yield return new WaitForSeconds(2f);
        //        }

        //        yield return new WaitForSeconds(2f);
        //    }

        //    private IEnumerator Step3_IntroduccionMonedas()
        //    {
        //        ShowInstruction(
        //            "RECOGE MONEDAS",
        //            "Las monedas te dan puntos\nTocalas para recogerlas"
        //        );

        //        yield return WaitForJump();
        //        HideInstruction();
        //        yield return new WaitForSeconds(0.5f);
        //    }

        //    private IEnumerator Step4_RecogerMonedas()
        //    {
        //        ShowInstruction(
        //            "PRACTICA DE RECOLECCION",
        //            "Recoge las monedas saltando hacia ellas"
        //        );

        //        yield return WaitForJump();
        //        HideInstruction();

        //        coinsCollected = 0;
        //        int coinsNeeded = 3;

        //        while (coinsCollected < coinsNeeded)
        //        {
        //            float randomY = Random.value > 0.5f ? highY : lowY;
        //            SpawnObject(coinPrefab, randomY);

        //            int previousCoins = coinsCollected;
        //            float timeout = 0f;

        //            while (coinsCollected == previousCoins && timeout < 5f)
        //            {
        //                timeout += Time.deltaTime;
        //                yield return null;
        //            }

        //            yield return new WaitForSeconds(2f);
        //        }

        //        yield return new WaitForSeconds(1f);
        //    }

        //    private IEnumerator Step5_IntroduccionBoss()
        //    {
        //        ShowInstruction(
        //            "APARECE EL BOSS",
        //            "El Boss tiene 3 ataques:\nProyectiles (esquivalos)\nParalisis (toca rapido para escapar)\nWifi (recogelos para dañarlo)"
        //        );

        //        yield return WaitForJump();
        //        HideInstruction();
        //        yield return new WaitForSeconds(1f);
        //    }

        //    private IEnumerator Step6_DerrotarBoss()
        //    {
        //        ShowInstruction(
        //            "DERROTA AL BOSS",
        //            "Esquiva sus ataques y recoge el wifi\nEl boss tiene 2 vidas"
        //        );

        //        yield return WaitForJump();
        //        HideInstruction();

        //        bossScript.SpawnBoss();

        //        while (bossScript.IsBossActive)
        //        {
        //            yield return null;
        //        }

        //        yield return new WaitForSeconds(2f);
        //    }

        //    private IEnumerator Step7_Finalizacion()
        //    {
        //        ShowInstruction(
        //            "TUTORIAL COMPLETADO!",
        //            "Ahora estas listo para jugar\nEl juego se volvera mas dificil con el tiempo"
        //        );

        //        yield return WaitForJump();
        //        HideInstruction();

        //        yield return new WaitForSeconds(0.5f);
        //        spawnerParent.SetActive(true);
        //    }

        //    private void ShowInstruction(string title, string instruction)
        //    {
        //        tutorialPanel.SetActive(true);

        //        instructionText.text = title + "\n\n" + instruction;
        //        actionText.text = "TOCA PARA CONTINUAR";

        //        Time.timeScale = 0f;
        //    }

        //    private void HideInstruction()
        //    {
        //        tutorialPanel.SetActive(false);

        //        instructionText.text = "";
        //        actionText.text = "";

        //        Time.timeScale = 1f;
        //    }

        //    private IEnumerator WaitForJump()
        //    {
        //        playerJumped = false;

        //        while (!playerJumped)
        //        {
        //            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        //                playerJumped = true;

        //            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        //                playerJumped = true;

        //            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        //                playerJumped = true;

        //            yield return null;
        //        }
        //    }

        //    private void SpawnObject(GameObject prefab, float yPosition)
        //    {
        //        Vector3 spawnPos = new Vector3(spawnX, yPosition, 0f);
        //        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

        //        TutorialAutoMover mover = obj.GetComponent<TutorialAutoMover>();
        //        if (mover != null)
        //        {
        //            mover.SetSpeed(objectSpeed);
        //        }
        //    }

        //    public void OnCoinCollected()
        //    {
        //        coinsCollected++;
        //    }
    }
}