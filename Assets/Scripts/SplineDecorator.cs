using UnityEngine;

public class SplineDecorator : MonoBehaviour {

	public BezierSpline spline;

	public int frequency;

	public bool lookForward;

	public Transform[] items;

    public bool colorGradient;

    public Color startColor;
    public Color secondColor;

    public Color endColor;

	public void Populate () {
		if (frequency <= 0 || items == null || items.Length == 0) {
			return;
		}
		float stepSize = frequency * items.Length;
		if (spline.Loop || stepSize == 1) {
			stepSize = 1f / stepSize;
		}
		else {
			stepSize = 1f / (stepSize - 1);
		}
        Gradient gradient = null;
        if (colorGradient)
        {
            gradient = new Gradient();

            // Populate the color keys at the relative time 0 and 1 (0 and 100%)
            var colorKey = new GradientColorKey[]
            {
                new GradientColorKey(startColor, 0.0f),
                new GradientColorKey(secondColor, 0.1f),
                new GradientColorKey(endColor, 0.2f),
            };

            var alphaKey = new GradientAlphaKey[]
            {
                new GradientAlphaKey(1.0f, 0.0f),
            };

            gradient.SetKeys(colorKey, alphaKey);
        }
		for (int p = 0, f = 0; f < frequency; f++) {
			for (int i = 0; i < items.Length; i++, p++) {
				Transform item = Instantiate(items[i]) as Transform;
                var localScale = item.localScale;
				Vector3 position = spline.GetPoint(p * stepSize);
				item.transform.localPosition = position;
				if (lookForward) {
					item.transform.LookAt(position + spline.GetDirection(p * stepSize));
				}
				item.transform.parent = transform;
                item.localScale = localScale;
                Renderer renderer = item.GetComponent<Renderer>();
                if (gradient != null
                    && renderer != null
                    && renderer.material != null)
                {
                    renderer.material.SetColor("_Color", gradient.Evaluate((float)f / frequency));
                }
            }
		}
	}
}