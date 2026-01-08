using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // static으로 유일성이 보장되는 인스턴스 생성
    static Managers Instance { get { Init();  return s_instance; } } // 유일한 매니저를 갖고온다.

    #region Contents
    GameManager _game = new GameManager();

    public static GameManager Game { get { return Instance._game; } }
    #endregion


    #region Core
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEX _scene = new SceneManagerEX();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();


    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get {  return Instance._input; } }
    public static PoolManager Pool {  get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEX Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)//게임 오브젝트가 없다면 생성해주자
            {
                go = new GameObject { name = "@Managers" };//이름이 @Managers인 게임 오브젝트 생성
                go.AddComponent<Managers>();//Managers 스크립트를 go로 가져와서 컴포넌트로 사용
            }

            DontDestroyOnLoad(go);//게임 오브젝트의 삭제를 막을수있음
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
        
        //초기화
    }
    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
