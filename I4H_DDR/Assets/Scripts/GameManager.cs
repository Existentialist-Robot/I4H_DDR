using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // REMARKS:
    // The key (Arrow) is the 8 possible arrow
    // The value (Pad) is the actual pad players selected to use, Pad.Null means not selected
    Dictionary<Arrow, Pad> _arrowConfiguration = new Dictionary<Arrow, Pad>();

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        InitVariable();
    }

    private void InitVariable() {
        // REMARKS: default ArrowConfiguration is 
        // Arrow.Top, Arrow.Left, Arrow.Right, Arrow.Bottom
        foreach (Arrow arrow in (Arrow[]) Enum.GetValues(typeof(Arrow))) {
            switch (arrow) {
                case Arrow.Top: {
                    _arrowConfiguration.Add(arrow, Pad.Top);
                    break;
                }
                case Arrow.Left: {
                    _arrowConfiguration.Add(arrow, Pad.Left);
                    break;
                }
                case Arrow.Right: {
                    _arrowConfiguration.Add(arrow, Pad.Right);
                    break;
                }
                case Arrow.Bottom: {
                    _arrowConfiguration.Add(arrow, Pad.Bottom);
                    break;
                }
                default: {
                    _arrowConfiguration.Add(arrow, Pad.Null);
                    break;
                }
            }
        }
    }

    public Pad GetArrowConfiguration(Arrow arrow) {
        return _arrowConfiguration[arrow];
    }

    public void SetArrowConfiguration(Arrow arrow, Pad pad) {
        _arrowConfiguration[arrow] = pad;
    }
}
