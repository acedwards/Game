using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels {

	public enum LevelType { main, ops };

    public LevelType Level { get; set; }

    public Levels()
    {
        Level = LevelType.main;
    }
}
