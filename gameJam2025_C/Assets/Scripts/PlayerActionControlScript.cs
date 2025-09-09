using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerActionControlScript : MonoBehaviour
{
    [Header("�A�N�V�����t���O")]
    bool actionFlag=true;
    bool changeHandFlag;
    bool catchCursorflag;
    [Header("�J�[�\���e�N�X�`��")]
    [Tooltip("�l�Ԃ�������Ƃ��̎�")]
    [SerializeField] Sprite catchCursor;
    [Tooltip("���I�������")]
    [SerializeField] Sprite punchCursor;
    [Tooltip("�����Ă����")]
    [SerializeField] Sprite punchingCursor;
    [Tooltip("�l�Ԃ�����ł����0")]
    [SerializeField] Sprite caught0Cursor;
    [Tooltip("�l�Ԃ�����ł���_�P")]
    [SerializeField] Sprite caught1Cursor;

    [SerializeField]RectTransform cursorImage;
    [SerializeField] private RectTransform canvasRectTransform;

    [Header("���C���J����")]
    [Tooltip("���C�L���X�g�Ɏg�p")]
    [SerializeField] Camera mainCamera;

    [Header("�ϐ��F�J�E���g�p")]
    public int count_breakRubble;
    public int count_savehuman;
    //�}�E�X�̍��W��ۑ�
    Vector2 mousePos;

    FallObjectControlScript fallControlScript;
    [SerializeField] GameObject fallController;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; // OS�J�[�\�����\����
        catchCursorflag = false;
        fallControlScript = fallController.GetComponent<FallObjectControlScript>();
        fallControlScript.spawnCount=0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHandFunc();
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
                Input.mousePosition,
                null,
                out pos
            );

        cursorImage.anchoredPosition = pos;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        foreach (RaycastHit hit in Physics.RaycastAll(ray))
        {
            if (actionFlag == true)
            {
                if (hit.collider.gameObject.tag == "FallingHuman" && catchCursorflag == false)
                {
                    Debug.Log(hit.collider.gameObject + "human");
                    Destroy(hit.collider.gameObject);
                    fallControlScript.spawnCount--;
                    count_savehuman++;
                    Debug.Log("saveHuman"+count_savehuman);
                }
                else if (hit.collider.gameObject.tag == "FallingRubble" && catchCursorflag == true)
                {
                    //cursorImage.GetComponent<Image>().sprite = punchingCursor;
                    Debug.Log(hit.collider.gameObject + "rubble");
                    Destroy(hit.collider.gameObject);
                    fallControlScript.spawnCount--;
                    count_breakRubble++;
                    Debug.Log("breakRubble" + count_breakRubble);
                    //cursorImage.GetComponent<Image>().sprite = punchCursor;
                }
            }
            //Debug.Log(actionFlag);

        }
        //Debug.Log(fallControlScript.spawnCount);
    }
    public void RShoulderDownFunc(InputAction.CallbackContext context)
    {
        if(context.phase==InputActionPhase.Performed)
        {
            actionFlag = true;
            Debug.Log("RShoulder Down");
        }
        
    }
    public void RTriggerDownFunc(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            changeHandFlag = true;
            Debug.Log("RTrigger Down");
        }
    }
    void ChangeHandFunc()
    {
        if (changeHandFlag == true)
        {
            if (catchCursorflag == true)
            {
                //Cursor.SetCursor(punchCursor, Vector2.zero, CursorMode.Auto);
                cursorImage.GetComponent<Image>().sprite = catchCursor;
                catchCursorflag = false;
            }
            else if(catchCursorflag==false)
            {
                //Cursor.SetCursor(catchCursor, Vector2.zero, CursorMode.Auto);
                cursorImage.GetComponent<Image>().sprite = punchCursor;
                catchCursorflag = true;
            }
        }
        changeHandFlag = false;
    }

}