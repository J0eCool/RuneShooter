using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraZoom : JComponent {
	[SerializeField] private float minScale = 5.0f;
	[SerializeField] private float maxScale = 12.0f;
	[SerializeField] private float scrollZoomSpeed = 250.0f;
	[SerializeField] private float pinchZoomSpeed = 0.1f;

	private Camera[] cameras;

	protected override void OnStart() {
		cameras = GetComponentsInChildren<Camera>();
	}

	protected override void OnUpdate(float dT) {
		float scroll = scrollWheelZoom(dT);
		float pinch = touchPinchZoom();
		float zoom = MathUtil.AbsMax(scroll, pinch);
		applyDeltaZoom(zoom);
	}

	private void applyDeltaZoom(float deltaZoom) {
		if (deltaZoom != 0.0f) {
			foreach (var camera in cameras) {
				float size = camera.orthographicSize;
				size -= deltaZoom;
				size = Mathf.Clamp(size, minScale, maxScale);
				camera.orthographicSize = size;
			}
		}
	}

	private float scrollWheelZoom(float dT) {
		return Input.GetAxis("Mouse ScrollWheel") * scrollZoomSpeed * dT;
	}

	private float touchPinchZoom() {
		if (Input.touchCount < 2) {
			return 0.0f;
		}
		Touch t1 = Input.GetTouch(0);
		Touch t2 = Input.GetTouch(1);
		Vector2 t1PrevPos = t1.position - t1.deltaPosition;
		Vector2 t2PrevPos = t2.position - t2.deltaPosition;
		Vector2 prevDelta = t1PrevPos - t2PrevPos;
		Vector2 curDelta = t1.position - t2.position;
		float dist = curDelta.magnitude - prevDelta.magnitude;
		return dist * pinchZoomSpeed;
	}
}
