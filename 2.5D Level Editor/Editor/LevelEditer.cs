using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class LevelEditer : GameObjectLibrary
{
    public LevelEditer()
        : base()
    {
        EditorStartUp(20, 20);
        Reset();
    }
}