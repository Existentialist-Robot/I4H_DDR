using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // REMARKS:
    // The key (ArrowConceptual) is where physical pad placement
    // The value (ArrowActual) is the actual pad players selected to use, ArrowActual.Null means not selected
    Dictionary<ArrowConceptual, ArrowActual> ArrowConfiguration = new Dictionary<ArrowConceptual, ArrowActual>();

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        InitVariable();
    }

    private void InitVariable() {
        // REMARKS: default ArrowConfiguration is 
        // Arrow.Top, Arrow.Left, Arrow.Right, Arrow.Bottom
        foreach (ArrowConceptual arrowConceptual in (ArrowConceptual[]) Enum.GetValues(typeof(ArrowConceptual))) {
            switch (arrowConceptual) {
                case ArrowConceptual.Top: {
                    ArrowConfiguration[arrowConceptual] = ArrowActual.Top;
                    break;
                }
                case ArrowConceptual.Left: {
                    ArrowConfiguration[arrowConceptual] = ArrowActual.Left;
                    break;
                }
                case ArrowConceptual.Right: {
                    ArrowConfiguration[arrowConceptual] = ArrowActual.Right;
                    break;
                }
                case ArrowConceptual.Bottom: {
                    ArrowConfiguration[arrowConceptual] = ArrowActual.Bottom;
                    break;
                }
                default: {
                    ArrowConfiguration[arrowConceptual] = ArrowActual.Null;
                    break;
                }
            }
        }
    }

    public void UpdateArrowConfiguration(int idx) {

    }
}
