using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigurationMenu : MonoBehaviour
{
    public GameObject[] arrowButtons;
    public Button centreButton;
    public Sprite centreButtonDefaultSprite;

    GameManager _gameManager;
    Arrow _nextCentredArrow = Constants.ARROWS.Min();
    bool _resetCentreButton = false;

    private void Start() {
        _gameManager = FindObjectOfType<GameManager>();

        InitConfigurationPanel();
    }

    public void PlayGame ()
    {
        Debug.Log("Moving from Configuration Scene to next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        Debug.Log("Moving from Configuration Scene to previous scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void InitConfigurationPanel() {
        // loop through gameManager.arrowConfiguration (using Arrow enum)
        foreach (Arrow arrow in Constants.ARROWS) {
            Pad assignedPad = _gameManager.GetArrowConfiguration(arrow);

            // the 8 arrows
            if (assignedPad != Pad.Centre) {
                switch(arrow) {
                    case Arrow.TopLeftDiag: {
                        SetArrowButtonSelected(arrow, (assignedPad == Pad.TopLeftDiag));
                        break;
                    }
                    case Arrow.Top: {
                        SetArrowButtonSelected(arrow, (assignedPad == Pad.Top));
                        break;
                    }
                    case Arrow.TopRightDiag: {
                        SetArrowButtonSelected(arrow, (assignedPad == Pad.TopRightDiag));
                        break;
                    }
                    case Arrow.Left: {
                        SetArrowButtonSelected(arrow, (assignedPad == Pad.Left));
                        break;
                    }
                    case Arrow.Right: {
                        SetArrowButtonSelected(arrow, (assignedPad == Pad.Right));
                        break;
                    }
                    case Arrow.BottomLeftDiag: {
                        SetArrowButtonSelected(arrow, (assignedPad == Pad.BottomLeftDiag));
                        break;
                    }
                    case Arrow.Bottom: {
                        SetArrowButtonSelected(arrow, (assignedPad == Pad.Bottom));
                        break;
                    }
                    case Arrow.BottomRightDiag: {
                        SetArrowButtonSelected(arrow, (assignedPad == Pad.BottomRightDiag));
                        break;
                    }
                }
            } else { // the centre button
                // change centre button to this arrow (UI)
                SetCentreButtonUI(arrow);

                // set this arrow button to disabled (UI)
                SetArrowButtonInteractable(arrow, false);

                MoveCentreButtonForward(arrow);
            }
        }
    }

    private void MoveCentreButtonForward(Arrow arrow) {
        // set the nextCentredArrow to next arrow
        _nextCentredArrow = arrow + 1;
        // if nextCentredArrow > last arrow
        // skip for 1 round
        if (_nextCentredArrow > Constants.ARROWS.Max()) {
            _nextCentredArrow = Constants.ARROWS.Min();
            _resetCentreButton = true;
        }
    }

    private void SetCentreButtonUI(Arrow arrow) {
        centreButton.GetComponent<Image>().sprite = arrowButtons[(int) arrow].transform.Find("Enabled").GetComponent<Image>().sprite;
    }

    private void SetArrowButtonSelected(Arrow arrow, bool selected) {
        GameObject arrowButton = arrowButtons[(int) arrow];
        
        // set enabled according to "selected"
        arrowButton.transform.Find("Enabled").gameObject.SetActive(selected);
        arrowButton.transform.Find("Disabled").gameObject.SetActive(!selected);
    }

    private void SetArrowButtonInteractable(Arrow arrow, bool interactable) {
        GameObject arrowButton = arrowButtons[(int) arrow];
        
        // set interactable to all button children
        foreach (Transform child in arrowButton.transform) {
            child.GetComponent<Button>().interactable = interactable;
        }
    }

    public void ArrowButtonOnSelect(int arrowButton) {
        Arrow arrow = (Arrow) arrowButton;

        switch (arrow) {
            case Arrow.TopLeftDiag: {
                _gameManager.SetArrowConfiguration(arrow, Pad.TopLeftDiag);
                break;
            }
            case Arrow.Top: {
                _gameManager.SetArrowConfiguration(arrow, Pad.Top);
                break;
            }
            case Arrow.TopRightDiag: {
                _gameManager.SetArrowConfiguration(arrow, Pad.TopRightDiag);
                break;
            }
            case Arrow.Left: {
                _gameManager.SetArrowConfiguration(arrow, Pad.Left);
                break;
            }
            case Arrow.Right: {
                _gameManager.SetArrowConfiguration(arrow, Pad.Right);
                break;
            }
            case Arrow.BottomLeftDiag: {
                _gameManager.SetArrowConfiguration(arrow, Pad.BottomLeftDiag);
                break;
            }
            case Arrow.Bottom: {
                _gameManager.SetArrowConfiguration(arrow, Pad.Bottom);
                break;
            }
            case Arrow.BottomRightDiag: {
                _gameManager.SetArrowConfiguration(arrow, Pad.BottomRightDiag);
                break;
            }
        }
    }

    public void ArrowButtonOnDisselect(int arrowButton) {
        Arrow arrow = (Arrow) arrowButton;
        
        _gameManager.SetArrowConfiguration(arrow, Pad.Null);
    }

    public void CentreButtonOnClick() {
        bool arrowCentred = false;

        // reset previous centred arrow
        // loop through gameManager.arrowConfiguration (using Arrow enum)
        foreach (Arrow arrow in Constants.ARROWS) {
            Pad assignedPad = _gameManager.GetArrowConfiguration(arrow);

            // previous centred arrow
            if (assignedPad == Pad.Centre) {
                // set this arrow button to enabled (UI)
                SetArrowButtonInteractable(arrow, true);

                _gameManager.SetArrowConfiguration(arrow, Pad.Null);

                break;
            }
        }
        
        if (_resetCentreButton) {
            // set centre button = default (UI)
            centreButton.GetComponent<Image>().sprite = centreButtonDefaultSprite;

            _resetCentreButton = false;
        } else {
            // loop through gameManager.arrowConfiguration (using Arrow enum)
            foreach (Arrow arrow in Constants.ARROWS) {
                // starting from nextCentredArrow
                if (arrow >= _nextCentredArrow) {
                    Pad assignedPad = _gameManager.GetArrowConfiguration(arrow);

                    // available to be centred
                    if (assignedPad == Pad.Null) {
                        _gameManager.SetArrowConfiguration(arrow, Pad.Centre);
                        
                        // change centre button to this arrow (UI)
                        SetCentreButtonUI(arrow);

                        // set this arrow button to disabled (UI)
                        SetArrowButtonInteractable(arrow, false);
                        
                        MoveCentreButtonForward(arrow);
                        
                        arrowCentred = true;

                        break;
                    }
                }
            }

            // cant find any arrow to centre
            if (arrowCentred == false) {
                // set centre button = default (UI)
                centreButton.GetComponent<Image>().sprite = centreButtonDefaultSprite;

                // reset nextCentredArrow
                _nextCentredArrow = Constants.ARROWS.Min();
            }
        }
    }
}
