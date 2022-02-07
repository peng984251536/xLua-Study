using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameMode gameMode;

    // Start is called before the first frame update
    void Start()
    {
        AppConst.GameMode = this.gameMode;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
