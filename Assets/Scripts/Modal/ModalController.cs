using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Add this line to import the UnityEngine.Events namespace

public class ModalController : MonoBehaviour
{
    public UnityEvent<bool> ModalToggleEvent = new UnityEvent<bool>();
    public bool IsModalOpen { get; private set; } = false;

    public void ToggleModal()
    {
        IsModalOpen = !IsModalOpen;
        ModalToggleEvent.Invoke(IsModalOpen);
    }

    public void SetModal(bool value)
    {
        IsModalOpen = value;
        ModalToggleEvent.Invoke(IsModalOpen);
    }
}
