using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Microsoft.MixedReality.Toolkit.Input;
using TMPro;

public class SyncedCube : MonoBehaviour, IPunInstantiateMagicCallback, IMixedRealityFocusHandler, IMixedRealityTouchHandler
{
    // Place this script on a prefab called "Cube" located in the Assets/Resources folder

    public void InstantiateCube()
    {
        // Instantiates a cube at (0, 3, 5) and passes it's PhotonView's ViewID to UpdateText
        GameObject cube = PhotonNetwork.Instantiate("Cube", new Vector3(0, 3, 5), Quaternion.identity, 0);
        PhotonView photonView = cube.GetComponent<PhotonView>();
        photonView.RPC("UpdateText", RpcTarget.AllBuffered, photonView.ViewID);
    }

    [PunRPC]
    void UpdateText(int id)
    {
        // Finds the GameObject associated with the ViewID, sets the text in the cube prefabs canvas to the cube's ViewID
        GameObject cube = PhotonView.Find(id).gameObject;
        cube.transform.Find("Canvas/ViewID").GetComponentInChildren<TextMeshProUGUI>().SetText(id.ToString());
    }

    public void OnFocusEnter(FocusEventData eventData)
    {
        // On gaze request ownership of the PhotonView of that object
        PhotonView photonView = this.GetComponent<PhotonView>();
        photonView?.RequestOwnership();
    }

    public void OnFocusExit(FocusEventData eventData)
    {
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        // On touch request ownership of the PhotonView of that object
        PhotonView photonView = this.GetComponent<PhotonView>();
        photonView?.RequestOwnership();
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // On a PhotonNetwork.Instantiate call file the instantiated object under the "Root" GameObject
        var parent = GameObject.Find("Root");
        this.transform.SetParent(parent.transform, true);
    }
}