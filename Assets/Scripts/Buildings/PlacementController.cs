using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

//public class NewBehaviourScript : MonoBehaviour
//{
//    [Header("����")]
//    [SerializeField] private FieldSystem field;
//    [SerializeField] private BuildSystem builder;
//    [SerializeField] private TowerDefinition selected; // ���� ���� Ÿ��
//    [SerializeField] private NotEnoughGoldWarning Waring; // ��� ���� ��� UI
//    [SerializeField] private GameObject ghostModel; // ������ ��
//    [SerializeField] private Renderer[] ghostTargets // ������ �� �ٲ� ���
//    [SerializeField] private Color cancolor = Color.white;
//    [SerializeField] private Color cannotColor = Color.red;
//
//    private Quaternion _rot = Quaternion.identity;
//    private Vector3 _pos;
//    private bool _canPlace;
//
//    private void Awake()
//    {
//        if(ghostModel != null && ghostTargets == null)
//        {
//            ghostTargets = ghostModel.GetComponentsInChildren<Renderer>(true);
//        }
//    }
//
//    void Update()
//    {
//        if (selected == null || field == null || builder == null)
//        {
//            return;
//        }
//
//        bool hit = field.TryRaycastGround(Input.mousePosition, out Vector3 point);
//        if(!hit)
//        {
//            if (ghostModel != null) ghostModel.SetActive(false);
//            return;
//        }
//
//        _pos = field.Snap(point);
//
//        if(ghostModel != null)
//        {
//            ghostModel.SetActive(true);
//            ghostModel.transform.SetPositionAndRotation(_pos, _rot);
//        }
//
//        //Q / E ȸ��
//        if(Input.GetKeyDown(KeyCode.Q))
//        {
//            _rot *= Quaternion.Euler(0f, -90f, 0f);
//        }
//        if(Input.GetKeyDown(KeyCode.E))
//        {
//            _rot *= Quaternion.Euler(0f, +90f, 0f);
//        }
//
//        if(Input.)
//    }
//}
