using System;
using UnityEngine;

public class CreateNote : MonoBehaviour
{
    [Header("Note Settings")]
    [SerializeField] private GameObject[] note;
    private string[] notes = null;
    private NoteQueue noteQueue;
    private ArrowShowing arrowShow;
    private Vector3 directionVector = Vector3.zero;
    private Material noteMaterial;

    private int addValue = 2;
    private int addRotationValue = 0;
    private int VisibleCount = 0;
    private int noteIndex = 0;
    private bool isChangeXValue = false, isRight = false;

    private bool isVisible = true;

    private void Awake()
    {
        arrowShow = FindObjectOfType<ArrowShowing>().GetComponent<ArrowShowing>();
        noteQueue = GetComponent<NoteQueue>();
    }

    private void Start()
    {
        CreateNotes();
        arrowShow.GetTurnNote();
    }
    public void CreateNotes()
    {
        ParsingNotes();
    }

    private void ParsingNotes()
    {
        TextAsset textAsset = null;
        if(Information.Instance.currentDiff == DifficultType.Fairy)
        {
            textAsset = Information.Instance.currentSong.ChaeboFile_Fairytale;
        }
        else if (Information.Instance.currentDiff == DifficultType.Dream)
        {
            textAsset = Information.Instance.currentSong.ChaeboFile_Dream;
        }
        else
        {
            textAsset = Information.Instance.currentSong.ChaeboFile_Nightmare;
        }
        notes = textAsset.text.Split("\n");
        GiveNoteType();
    }

    #region 노트 타입에 따른 효과를 적용하는 함수

    private void GiveNoteType()
    {
        int noteInteger = 0;
        float noteTiming = 0f;
        string[] noteInfo;
        foreach (string note in notes)
        {
            noteInfo = note.Split(",");
            noteTiming = int.Parse(noteInfo[2]) / 1000f;
            noteInteger = int.Parse(noteInfo[0]);
            noteInteger = NoteIntParse(noteInteger);
            SettingNote(noteInteger, noteTiming);
        }
        SettingNote(4, 0);
        FindObjectOfType<CamRotationSystem>().SetRotationTiming();
        FindObjectOfType<SetEventTimingSystem>().SetTiming();
        //FindObjectOfType<EventStartSystem>().SetEventTiming();
    }

    private int NoteIntParse(int noteInteger)
    {
        switch(noteInteger)
        {
            case 42:
                noteInteger = -2;
                break;
            case 128:
                noteInteger = -1;
                break;
            case 213:
                noteInteger = 0;
                break;
            case 298:
                noteInteger = 1;
                break;
            case 384:
                noteInteger = 2;
                break;
            case 469:
                noteInteger = 3;
                break;
        }
        return noteInteger;
    }

    private void SettingNote(int noteInteger, float noteTiming)
    {
        bool isTurn = false;
        // +2를 하는 이유는 noteInteger의 최소값이 -2이기 떄문.(0부터 시작해야뎀)
        CreateNoteObject(noteInteger + 2, directionVector, noteTiming);
        switch (noteInteger)
        {
            case 2:
                isRight = true;
                isTurn = !isTurn;
                break;
            case -2:
                isRight = false;
                isTurn = !isTurn;
                break;
        }

        NoteRotation(isTurn);
        NoteDirection(isTurn);
    }

    #endregion

    #region 노트 방향을 정해주는 함수

    private void NoteRotation(bool isTurn)
    {
        if (isTurn)
        {
            if (isRight)
                addRotationValue += 90;
            else
                addRotationValue -= 90;
        }
    }

    private void NoteDirection(bool isTurn)
    {
        // 회전 노트인지 확인
        if (isTurn)
        {
            // X 값이 바뀌고 있는지 확인
            if (isChangeXValue)
            {
                // 오른쪽인지 확인
                if (isRight)
                {
                    addValue *= -1;
                }
                isChangeXValue = false;
                directionVector.z += addValue;
            }
            else
            {
                if (!isRight)
                {
                    addValue *= -1;
                }
                isChangeXValue = true;
                directionVector.x += addValue;
            }
        }
        else
        {
            if (isChangeXValue)
            {
                directionVector.x += addValue;
            }
            else
            {
                directionVector.z += addValue;
            }
        }
    }

    #endregion
    private void CreateNoteObject(int num, Vector3 dir, float noteTiming)
    {
        GameObject noteObject = Instantiate(note[num], dir, Quaternion.Euler(0, addRotationValue,0));

        

        if (VisibleCount > 0)
        {
            noteObject.GetComponent<MeshRenderer>().enabled = false;
            VisibleCount--;
        }


        if (num == 5)
        {
            CreateEventNote(Information.Instance.currentSong.SongID);
            AddEventScript(noteObject);
        }
        Material footMat = SelectNoteMaterial(num - 2);
        Material backgroundMat = SelectbackgroundNoteMaterial(num - 2);
        if(num == 6)
        {
            footMat = SelectNoteMaterial(0);
            backgroundMat = SelectbackgroundNoteMaterial(0);
        }
        Material[] mat = noteObject.GetComponent<MeshRenderer>().materials;
        mat[0] = backgroundMat;
        mat[1] = footMat;
        noteObject.GetComponent<MeshRenderer>().materials = mat;

        if(num != 6)
        {
            Note noteScript = noteObject.GetComponent<Note>();
            noteScript.noteTiming = noteTiming;
            noteScript.noteIndex = noteIndex++;
            noteQueue.AddNote(noteScript);
        }

        noteObject.isStatic = true;
    }

    private void AddEventScript(GameObject note)
    {
        switch(Information.Instance.currentSong.SongID + 1)
        {
            case 1:
                note.AddComponent<EventNote_One>();
                break;

            case 2:
                note.AddComponent<EventNote_Two>();
                break;

            case 3:
                note.AddComponent<EventNote_Three>();
                break;

            case 4:
                note.AddComponent<EventNote_Four>();
                break;

            case 5:
                note.AddComponent<EventNote_Five>();
                break;

            case 6:
                note.AddComponent<EventNote_Six>();
                break;

            case 7:
                note.AddComponent<EventNote_Seven>();
                break;

            case 8:
                note.AddComponent<EventNote_Eight>();
                break;

            case 9:
                note.AddComponent<EventNote_Nine>();
                break;

            case 10:
                note.AddComponent<EventNote_Ten>();
                break;
            default:
                break;
        }
    }

    private Material SelectNoteMaterial(int num)
    {
        Material material = null;
        NoteSkin noteMats = Information.Instance.SkinDictionary[Information.Instance.GameData.selectedSkin];
        switch(num)
        {
            case -2: // LR
                material = noteMats.LeftRotate_Mat;
                break;
            case -1: // LS
                material = noteMats.LeftStep_Mat;
                break;
            case 0: // NONE
                material = noteMats.NoneStep_Mat;
                break;
            case 1: // RS
                material = noteMats.RightStep_Mat;
                break;
            case 2: // RR
                material = noteMats.RightRotate_Mat;
                break;
            case 3: // EVT
                material = noteMats.Event_Mats[Information.Instance.currentSong.SongID];
                break;
        }
        return material;
    }
    private Material SelectbackgroundNoteMaterial(int num)
    {
        Material material = null;
        RoadMats noteMats = Information.Instance.currentSong.RoadMats;
        switch (num)
        {
            case -2: // LR
                material = noteMats.LeftTurnRoadMat;
                break;
            case -1: // LS
            case 0: // NONE
            case 1: // RS
            case 3: // EVT
                material = noteMats.NoneRoadMat;
                break;
            case 2: // RR
                material = noteMats.RightTurnRoadMat;
                break;
        }
        if (noteQueue.ListCount == 0) material = noteMats.StartRoadMat;
        return material;
    }

    private void CreateEventNote(int stageLevel)
    {
        switch(stageLevel)
        {
            case 2:
                VisibleCount += 1;
                break;
            case 3:
                VisibleCount += 2;
                directionVector.y += 2;
                break;
        }
    }
}