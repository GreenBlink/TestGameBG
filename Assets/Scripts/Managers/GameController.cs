using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Ball> currentBalls = new List<Ball>();

    public static GameController instance;

    public List<Ball> prefabBalls = new List<Ball>();
    public TimeController timeController;
    public SaveManager saveManager;
    public UIController uIController;
    public Racket racketUp;
    public Racket racketBottom;
    public GameObject wallLeft;
    public GameObject wallRight;
    public float speedMoveRacket = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uIController.Init();
        StartGame();
    }

    public void StartGame()
    {
        InitNewRound();

        StartCoroutine(ControllerRacketProcess());
    }

    public void InitNewRound()
    {
        timeController.StopTime();

        CheakRecord();
        ClearAllBalls();
        InitNewBall();

        timeController.StartTime();
    }

    public void InitNewBall()
    {
        Ball currentBall = Instantiate(prefabBalls[Random.Range(0, prefabBalls.Count)]);
        currentBall.transform.position = Vector3.zero;
        currentBall.SetColor(uIController.GetColorBall());
        currentBalls.Add(currentBall);
    }

    public void ClearAllBalls()
    {
        for (int i = 0; i < currentBalls.Count; i++)
        {
            if (currentBalls[i] != null)
                currentBalls[i].DestroyBall();
        }

        currentBalls.Clear();
    }

    private void CheakRecord()
    {
        float currentRecord = saveManager.GetRecord();

        if (timeController.valueTime > currentRecord)
            saveManager.SaveRecord(timeController.valueTime);
    }

    private Vector3 GetMoveInput()
    {
        float force = Input.GetMouseButton(0) && !uIController.ui.activeSelf ? Input.mousePosition.x > Screen.width / 2 ? 1 : -1 : Input.GetAxis("Horizontal");
        return transform.right * force;
    }

    private IEnumerator ControllerRacketProcess()
    {
        Vector3 moveVector = new Vector3();

        while (true)
        {
            moveVector = GetMoveInput();

            racketUp.MoveRocket(moveVector, speedMoveRacket);
            racketBottom.MoveRocket(moveVector, speedMoveRacket);

            yield return null;
        }
    }
}
