using UnityEngine;
using UnityEngine.InputSystem;

public class Navigator : MonoBehaviour
{
	[SerializeField] GameObject marker;
	[SerializeField] LayerMask agentLayerMask = Physics.AllLayers;
	[SerializeField] LayerMask navLayerMask = Physics.AllLayers;
	
	Camera activeCamera;
	NavAgent navAgent = null;

	void Start()
	{
		activeCamera = Camera.main;
	}

	void Update()
	{
		if (Mouse.current.rightButton.wasPressedThisFrame)
		{
			Ray ray = activeCamera.ScreenPointToRay(Mouse.current.position.value);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, 500.0f, agentLayerMask))
			{
				if (hitInfo.collider.gameObject.TryGetComponent<NavAgent>(out navAgent))
				{
					marker.SetActive(true);
				}
			}
			else if (navAgent != null && Physics.Raycast(ray, out hitInfo, 500.0f, navLayerMask))
			{
				navAgent.Destination = hitInfo.point;
			}
		}

		if (navAgent != null)
		{
			marker.transform.position = navAgent.transform.position + Vector3.up * 2;
		}
	}
}
