using UnityEngine;

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;
	public float duration;
	public bool lookForward;
	public SplineWalkerMode mode;
	private float progress;
	private bool goingForward = true;
	[SerializeField]
	public Transform passenger;

	TreeMakerTreeHandler tree;

	private void Update () {
		if (goingForward) {
			progress += (Time.deltaTime / duration);
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;

					if(passenger != null)
                    {
						tree.GiftBear(passenger.gameObject);
						gameObject.SetActive(false);
					}
					passenger = null;
				}
				else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
		else {
			progress -= Time.deltaTime / duration;
			if (progress < 0f) {
				progress = - progress;
				goingForward = true;
			}
		}

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		if (lookForward) {
			transform.LookAt(position + spline.GetDirection(progress));
		}
	}

    private void OnEnable()
    {
		tree = FindObjectOfType<TreeMakerTreeHandler>();
		progress = 0f;
		passenger = transform.GetChild(transform.childCount-1);
    }
}