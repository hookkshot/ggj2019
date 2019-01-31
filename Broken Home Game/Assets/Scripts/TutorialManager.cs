using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class TutorialManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent LessonDismissed = new UnityEvent();
    [HideInInspector]
    public UnityEvent LessonActivated = new UnityEvent();

    private Lesson current;

    [SerializeField]
    private List<Lesson> lessons; 

    public void LevelLoaded(string name)
    {
        current = lessons.FirstOrDefault(l => l.LevelName.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));
        if(current != null)
        {
            current.LessonUI.SetActive(true);
            LessonActivated.Invoke();
        }
    }

    private void Update()
    {
        if (current == null) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            LessonDismissed.Invoke();
            lessons.Remove(current);
            current.LessonUI.SetActive(false);
            current = null;
        }
    }

    
}

[System.Serializable]
public class Lesson
{
    public string LevelName;
    public GameObject LessonUI;
}
