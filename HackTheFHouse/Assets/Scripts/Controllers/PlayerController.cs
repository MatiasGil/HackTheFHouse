using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Animator animatorController;

    [SerializeField]
    private ElectricElement activeElectricElement;

    private bool moving = false;

    [SerializeField]
    private bool canMove = false;

    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (!GameController.Instance.Playing) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))      MoveUp();
        if (Input.GetKeyDown(KeyCode.DownArrow))    MoveDown();
        if (Input.GetKeyDown(KeyCode.RightArrow))   MoveRight();
        if (Input.GetKeyDown(KeyCode.LeftArrow))    MoveLeft();
    }

    public void StartGame()
    {
        animatorController.SetTrigger("startgame");
        Invoke("ChangePosition", .25f);
        canMove = true;
    }

    private void CantMove()
    {
        AudioController.Instance.PlaySFX("Error");
        //canMove = true;
    }

    private void MoveLeft()
    {
        if (moving)
            return;

        if (activeElectricElement.leftRelation.electricElement != null)
        {
            AudioController.Instance.PlaySFX("Movimiento");
            moving = true;
            Invoke("ChangePosition", .25f);
            Invoke("EnableMoving", .38f);
            activeElectricElement.PlayerDeparted(Direction.left);
            activeElectricElement = activeElectricElement.leftRelation.electricElement;
            animatorController.SetTrigger("move");
        }
        else
            CantMove();
    }

    private void MoveRight()
    {
        if (moving)
            return;

        if (activeElectricElement.rightRelation.electricElement != null)
        {
            AudioController.Instance.PlaySFX("Movimiento");
            moving = true;
            Invoke("ChangePosition", .25f);
            Invoke("EnableMoving", .38f);
            activeElectricElement.PlayerDeparted(Direction.right);
            activeElectricElement = activeElectricElement.rightRelation.electricElement;
            animatorController.SetTrigger("move");
        }
        else
            CantMove();
    }

    private void MoveUp()
    {
        if (moving)
            return;

        if (activeElectricElement.topRelation.electricElement != null)
        {
            AudioController.Instance.PlaySFX("Movimiento");
            moving = true;
            Invoke("ChangePosition", .25f);
            Invoke("EnableMoving", .38f);
            activeElectricElement.PlayerDeparted(Direction.up);
            activeElectricElement = activeElectricElement.topRelation.electricElement;
            animatorController.SetTrigger("move");
        }
        else
            CantMove();
    }

    private void MoveDown()
    {
        if (moving)
            return;

        if (activeElectricElement.botRelation.electricElement != null)
        {
            AudioController.Instance.PlaySFX("Movimiento");
            moving = true;
            Invoke("ChangePosition", .25f);
            Invoke("EnableMoving", .38f);
            activeElectricElement.PlayerDeparted(Direction.down);
            activeElectricElement = activeElectricElement.botRelation.electricElement;
            animatorController.SetTrigger("move");
        }
        else
            CantMove();
    }

    private void ChangePosition()
    {
        Vector3 targetPosition = activeElectricElement.ActualPosition;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        activeElectricElement.PlayerArrived();
    }

    private void EnableMoving()
    {
        moving = false;
    }
}
