using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using eaglib;
using enableGame;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class newGameButton : MonoBehaviour {

    public int sceneToLoad;
    private GameObject ableSingleton;
    private GameObject parameterHandler;
    private GameObject miscSingleman;
    private GUIElement _citadelGUIthing;
    private SpriteRenderer _spriteRenderer;

    // Use this for initialization
    void Start()
    {
        ableSingleton = GameObject.Find("_AbleSessionCreator(Singleton)");
        parameterHandler = GameObject.Find("_ParameterHandler(Clone)");
        miscSingleman = GameObject.Find("MiscellaneousSingleton");
    }

    public int playDelay = 5;
    public TextMesh countdown;
    IEnumerator DelayLoad()
    {
        for (int i = playDelay; i >= 0; i--)
        {
            countdown.text = "Starting game in " + i;
            yield return new WaitForSeconds(1.0f);
        }
        DontDestroyOnLoad(ableSingleton);
        DontDestroyOnLoad(parameterHandler);
        DontDestroyOnLoad(miscSingleman);
        //EnableAPI.Instance.CreateNewSession(EnableAPI.Game, EnableAPI.Version);
        Application.LoadLevel(sceneToLoad);
        //   Application.LoadLevel("Game");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

}

    

