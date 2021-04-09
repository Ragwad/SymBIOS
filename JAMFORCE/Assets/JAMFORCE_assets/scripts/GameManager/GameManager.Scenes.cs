using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    public enum Scenes { Home, Level1, Level2, Level3, Boss }

    [Header("~@ Scenes @~")]
    public Scenes scene;

    //------------------------------------------------------------------------------------------------------------------------------

    void InitScenes()
        => SceneManager.sceneLoaded += (scene, loadMode) => this.scene = (Scenes)scene.buildIndex;

    //------------------------------------------------------------------------------------------------------------------------------

    IEnumerator ELoadScene()
    {
        yield return SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);
        animator.CrossFadeInFixedTime((int)BaseStates.onLoad, 0, (int)Layers.Base);
    }
}