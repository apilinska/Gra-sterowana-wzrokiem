﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBoxController : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite box_selected;
    public Sprite box;

    private bool selected;
    private int i = 0;
    private int j = 0;
    private int number;


    void Start()
    {
        SelectButton();
        GetComponent<Button>().onClick.AddListener(() => ChangeState());
    }

    public void SetCoordinates(int i, int j) {
        this.i = i;
        this.j = j;
    }

    public void SetNumber(int number) {
        this.number = number;
        GetComponentInChildren<Text>().text = number.ToString();
    }

    public void SetNumberAndCoordinates(int number, int i, int j) {
        SetNumber(number);
        SetCoordinates(i, j);
    }

    public int GetNumber() {
        return this.number;
    }

    public int GetNumberIfSelected() {
        if(selected) return number;
        else return 0;
    }

    void ChangeState() {
        MathController.NewMove();
        if(selected) UnselectButton();
        else SelectButton();
        GetComponentInParent<BoardController>().ChangeState(i,j);
    }

    void SelectButton() {
        selected = true;
        GetComponent<Image>().sprite = box_selected;
    }

    void UnselectButton() {
        selected = false;
        GetComponent<Image>().sprite = box;
    }
}
