using UnityEngine;

// Cast the value of three different inputs as a Vector3
public class StringToVector3 : MonoBehaviour
{
	public Vector3 GetPosition => _position;

	private Vector3 _position = Vector3.zero;

	public void SetX(string xInput) => _position.x = StringToFloat(xInput);
	public void SetY(string yInput) => _position.y = StringToFloat(yInput);
	public void SetZ(string zInput) => _position.z = StringToFloat(zInput);

	private float StringToFloat(string input)
	{
		float res;
		if (float.TryParse(input, out res))
		{
			return res;
		}

		return 0f;
	}
}
