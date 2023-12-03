using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugger : MonoBehaviour
{
    // Start is called before the first frame update
    bool showConsole;
    string input;

    public static DebugCommand KILL_ALL;
    public static DebugCommand RESTART;

    public List<object> commandList;


    private void Awake()
    {
        KILL_ALL = new DebugCommand("kill_all", "removes all NPCs from the scene", "kill_all", () =>
        {
            PlayerController.Instance.speed = 50;
        });

        RESTART = new DebugCommand("restart", "adds 1000 gold", "restart", () =>
        {
            SceneManager.LoadScene(0);
        });

        commandList = new List<object>
        {
            KILL_ALL,
        };
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            showConsole = !showConsole;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Command Run");
            HandleInput();
            input = "";
        }
    }

    

    private void OnGUI()
    {
        if(!showConsole) { return; }

        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width-20f, 20f), input);

    }

    private void HandleInput()
    {
        for(int i = 0; i< commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i]as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
    }
}
