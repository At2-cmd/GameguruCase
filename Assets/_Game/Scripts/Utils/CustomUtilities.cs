using System;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public static class CustomUtilities
{
	public static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");

	public static Vector3 Add(Vector3 vectorA, Vector3 vectorB)
	{
		vectorA.x += vectorB.x;
		vectorA.y += vectorB.y;

		return vectorA;
	}

	public static Vector3 Substract(Vector3 vectorA, Vector3 vectorB)
	{
		vectorA.x -= vectorB.x;
		vectorA.y -= vectorB.y;
		return vectorA;
	}

	public static Vector3 Multiply(Vector3 vectorValue, float floatValue)
	{
		vectorValue.x *= floatValue;
		vectorValue.y *= floatValue;
		vectorValue.z *= floatValue;
		return vectorValue;
	}

	public static Vector3 Multiply(Vector3 vectorValue, float floatValueA, float floatValueB)
	{
		vectorValue.x *= floatValueA * floatValueB;
		vectorValue.y *= floatValueA * floatValueB;
		vectorValue.z *= floatValueA * floatValueB;
		return vectorValue;
	}

	/// <summary>
	/// Projects an input vector onto the tangent of a given plane (defined by its normal).
	/// </summary>
	public static Vector3 ProjectOnTangent(Vector3 inputVector, Vector3 planeNormal, Vector3 up)
	{
		Vector3 rotationAxis = Vector3.Cross(inputVector, up);
		Vector3 tangent = Vector3.Normalize(Vector3.Cross(planeNormal, rotationAxis));

		return Multiply(tangent, inputVector.magnitude);
	}

	/// <summary>
	/// Projects an input vector onto plane A and plane B orthonormal direction.
	/// </summary>
	public static Vector3 DeflectVector(Vector3 inputVector, Vector3 planeA, Vector3 planeB, bool maintainMagnitude = false)
	{
		Vector3 direction = Vector3.Cross(planeA, planeB);
		direction.Normalize();

		if (maintainMagnitude)
			return direction * inputVector.magnitude;
		else
			return Vector3.Project(inputVector, direction);
	}

	public static float GetTriangleValue(float center, float height, float width, float independentVariable, float minIndependentVariableLimit = Mathf.NegativeInfinity, float maxIndependentVariableLimit = Mathf.Infinity)
	{
		float minValue = center - width / 2f;
		float maxValue = center + width / 2f;

		if (independentVariable < minValue || independentVariable > maxValue)
		{
			return 0f;
		}
		else if (independentVariable < center)
		{
			return height * (independentVariable - minValue) / (center - minValue);
		}
		else
		{
			return -height * (independentVariable - center) / (maxValue - center) + height;
		}
	}

	/// <summary>
	/// Makes a value greater than or equal to zero (default value).
	/// </summary>
	public static void SetPositive<T>(ref T value) where T : System.IComparable<T>
	{
		SetMin<T>(ref value, default(T));
	}

	/// <summary>
	/// Makes a value less than or equal to zero (default value).
	/// </summary>
	public static void SetNegative<T>(ref T value) where T : System.IComparable<T>
	{
		SetMax<T>(ref value, default(T));
	}

	/// <summary>
	/// Makes a value greater than or equal to a minimum value.
	/// </summary>
	public static void SetMin<T>(ref T value, T minValue) where T : System.IComparable<T>
	{
		bool isLess = value.CompareTo(minValue) < 0;

		if (isLess)
			value = minValue;
	}

	/// <summary>
	/// Makes a value less than or equal to a maximum value.
	/// </summary>
	public static void SetMax<T>(ref T value, T maxValue) where T : System.IComparable<T>
	{
		bool isGreater = value.CompareTo(maxValue) > 0;

		if (isGreater)
			value = maxValue;
	}

	/// <summary>
	/// Limits a value range from a minimum value to a maximum value (similar to Mathf.Clamp).
	/// </summary>
	public static void SetRange<T>(ref T value, T minValue, T maxValue) where T : System.IComparable<T>
	{
		SetMin<T>(ref value, minValue);
		SetMax<T>(ref value, maxValue);
	}

	/// <summary>
	/// Returns true if the target value is between a and b ( both exclusive ). 
	/// To include the limits values set the "inclusive" parameter to true.
	/// </summary>
	public static bool IsBetween(float target, float a, float b, bool inclusive = false)
	{

		if (b > a)
			return (inclusive ? target >= a : target > a) && (inclusive ? target <= b : target < b);
		else
			return (inclusive ? target >= b : target > b) && (inclusive ? target <= a : target < a);
	}

	/// <summary>
	/// Returns true if the target value is between a and b ( both exclusive ). 
	/// To include the limits values set the "inclusive" parameter to true.
	/// </summary>
	public static bool IsBetween(int target, int a, int b, bool inclusive = false)
	{
		if (b > a)
			return (inclusive ? target >= a : target > a) && (inclusive ? target <= b : target < b);
		else
			return (inclusive ? target >= b : target > b) && (inclusive ? target <= a : target < a);
	}

	public static bool IsCloseTo(Vector3 input, Vector3 target, float tolerance)
	{
		return Vector3.Distance(input, target) <= tolerance;
	}

	public static bool IsCloseTo(float input, float target, float tolerance)
	{
		return Mathf.Abs(target - input) <= tolerance;
	}

	public static Vector3 TransformVectorUnscaled(this Transform transform, Vector3 vector)
	{
		return transform.rotation * vector;
	}

	public static Vector3 InverseTransformVectorUnscaled(this Transform transform, Vector3 vector)
	{
		return Quaternion.Inverse(transform.rotation) * vector;
	}

	public static Vector3 RotatePointAround(Vector3 point, Vector3 center, float angle, Vector3 axis)
	{
		Quaternion rotation = Quaternion.AngleAxis(angle, axis);
		Vector3 pointToCenter = center - point;
		Vector3 rotatedPointToCenter = rotation * pointToCenter;
		return center - rotatedPointToCenter;
	}

	public static T GetOrAddComponent<T>(this GameObject targetGameObject) where T : Component
	{
		var comp = targetGameObject.GetComponent<T>();
		if (comp == null)
		{
			comp = targetGameObject.AddComponent<T>();
		}
		return comp;
	}

	/// <summary>
	/// Gets a "target" component within a particular branch (inside the hierarchy). The branch is defined by the "branch root object", which is also defined by the chosen 
	/// "branch root component". The returned component must come from a child of the "branch root object".
	/// </summary>
	/// <param name="callerComponent"></param>
	/// <param name="includeInactive">Include inactive objects?</param>
	/// <typeparam name="T1">Branch root component type.</typeparam>
	/// <typeparam name="T2">Target component type.</typeparam>
	/// <returns>The target component.</returns>
	public static T2 GetComponentInBranch<T1, T2>(this Component callerComponent, bool includeInactive = true) where T1 : Component where T2 : Component
	{
		T1[] rootComponents = callerComponent.transform.root.GetComponentsInChildren<T1>(includeInactive);

		if (rootComponents.Length == 0)
		{
			Debug.LogWarning($"Root component: No objects found with { typeof(T1).Name } component");
			return null;
		}

		for (int i = 0; i < rootComponents.Length; i++)
		{
			T1 rootComponent = rootComponents[i];

			// Is the caller a child of this root?
			if (!callerComponent.transform.IsChildOf(rootComponent.transform) && !rootComponent.transform.IsChildOf(callerComponent.transform))
				continue;

			T2 targetComponent = rootComponent.GetComponentInChildren<T2>(includeInactive);

			if (targetComponent == null)
				continue;

			return targetComponent;

		}

		return null;

	}

	/// <summary>
	/// Gets a "target" component within a particular branch (inside the hierarchy). The branch is defined by the "branch root object", which is also defined by the chosen 
	/// "branch root component". The returned component must come from a child of the "branch root object".
	/// </summary>
	/// <param name="callerComponent"></param>
	/// <param name="includeInactive">Include inactive objects?</param>
	/// <typeparam name="T1">Target component type.</typeparam>	
	/// <returns>The target component.</returns>
	public static T1 GetComponentInBranch<T1>(this Component callerComponent, bool includeInactive = true) where T1 : Component
	{
		return callerComponent.GetComponentInBranch<T1, T1>(includeInactive);
	}

	public static string InBetweenString(this string targetString, string firstString, string lastString)
	{
		int start = targetString.IndexOf(firstString) + firstString.Length;
		int end = targetString.IndexOf(lastString);

		if (end - start < 0)
			return "";

		return targetString.Substring(start, end - start);
	}

	public static bool BelongsToLayerMask(int layer, int layerMask)
	{
		return (layerMask & (1 << layer)) > 0;
	}

	public static T2 GetOrRegisterValue<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key) where T1 : Component where T2 : Component
	{
		if (key == null)
			return null;

		T2 value;
		bool found = dictionary.TryGetValue(key, out value);

		if (!found)
		{
			value = key.GetComponent<T2>();

			if (value != null)
				dictionary.Add(key, value);
		}

		return value;
	}

	public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
	{
		float angle = Vector3.Angle(from, to);
		Vector3 cross = Vector3.Cross(from, to);
		cross.Normalize();

		float sign = cross == axis ? 1f : -1f;
		return sign * angle;
	}

	public static void DebugRay(Vector3 point, Vector3 direction = default(Vector3), float duration = 2f, Color color = default(Color))
	{
		Vector3 drawDirection = direction == default(Vector3) ? Vector3.up : direction;
		Color drawColor = color == default(Color) ? Color.blue : color;

		Debug.DrawRay(point, drawDirection, drawColor, duration);
	}

	public static void DrawArrowGizmo(Vector3 start, Vector3 end, Color color, float radius = 0.25f)
	{
		Gizmos.color = color;
		Gizmos.DrawLine(start, end);

		Gizmos.DrawRay(
			end,
			Quaternion.AngleAxis(45, Vector3.forward) * Vector3.Normalize(start - end) * radius
		);

		Gizmos.DrawRay(
			end,
			Quaternion.AngleAxis(-45, Vector3.forward) * Vector3.Normalize(start - end) * radius
		);
	}

	public static void DrawCross(Vector3 point, float radius, Color color)
	{
		Gizmos.color = color;

		Gizmos.DrawRay(
			point + Vector3.up * 0.5f * radius,
			Vector3.down * radius
		);

		Gizmos.DrawRay(
			point + Vector3.right * 0.5f * radius,
			Vector3.left * radius
		);
	}

	public static bool IsWholeNumber(float number)
	{
		return Mathf.Approximately(number, Mathf.Round(number));
	}

	public static int GetIndexLoop(int number, int total, int minimumIndex = 0)
	{
		if (total == 0 || number <= 0) return 0;
		var index = number % total;
		if (number >= total)
		{
			return Mathf.Clamp(index, minimumIndex, int.MaxValue);
		}
		return index;
	}

	public static float CalculateProfit(float minPrice, float minEarned, float targetPrice, float targetEarned)
	{
		var profit = (minEarned * targetPrice / (minPrice * targetEarned) - 1) * 100;
		return Mathf.Abs(profit);
	}

	public static float CalculateLoopValue(float lastListValue, int currentIndex, int lastIndex, float valueToAdd)
	{
		var lastValue = lastListValue;
		var residueIndex = currentIndex - lastIndex;
		for (var i = 0; i < residueIndex; i++)
		{
			lastValue += valueToAdd;
		}

		return lastValue;
	}

	public static List<int> GetRandomResultsWithProbability(int attempts, int percentage1, int percentage2)
	{
		var results = new List<int>();

		for (var i = 0; i < attempts; i++)
		{
			if (i < percentage1)
				results.Add(1);
			else if (i < percentage1 + percentage2)
				results.Add(2);
			else
				results.Add(0);
		}
		ShuffleList(results);
		return results;
	}

	public static void ShuffleList<T>(List<T> list)
	{
		var random = new System.Random();
		var n = list.Count;
		while (n > 1)
		{
			n--;
			var k = random.Next(n + 1);
			(list[k], list[n]) = (list[n], list[k]);
		}
	}

	public static int FindNumberIndex(List<float> floatList, float x)
	{
		var index = floatList.FindIndex(f => f > x);
		return index == -1 ? floatList.Count - 1 : index - 1;
	}

	public static Vector3 GetLookDirection(Vector3 targetPosition, Vector3 position)
	{
		return targetPosition - position;
	}

	public static Vector3 GetClosestPositionInList(List<Vector3> list, Vector3 objPosition)
	{
		var nearestPosition = list[0];
		var shortestDistance = Vector3.Distance(objPosition, nearestPosition);
		for (int i = 1; i < list.Count; i++)
		{
			var pos = list[i];
			var distance = Vector3.Distance(objPosition, pos);
			if (!(distance < shortestDistance)) continue;
			shortestDistance = distance;
			nearestPosition = pos;
		}
		return nearestPosition;
	}

	public static void ResetTransform(ref Transform transform)
	{
		transform.position = Vector3.zero;
		transform.localScale = Vector3.zero;
		transform.rotation = Quaternion.identity;
	}

	public static Vector3 SetX(ref Vector3 vector, float val)
	{
		return new Vector3(val, vector.y, vector.z);
	}

	public static Vector3 SetY(ref Vector3 vector, float val)
	{
		return new Vector3(vector.x, val, vector.z);
	}

	public static Vector3 SetZ(ref Vector3 vector, float val)
	{
		return new Vector3(vector.x, vector.y, val);
	}

	public static void SetParentAsOrigin(this Transform transform, Transform parent)
	{
		transform.SetParent(parent);
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;
		transform.localRotation = Quaternion.identity;
	}

	public static T GetRandomItem<T>(List<T> list)
	{
		return list[UnityEngine.Random.Range(0, list.Count - 1)];
	}

	public static void SetLayer(GameObject obj, int layer)
	{
		obj.layer = layer;
	}

	public static void SetTag(GameObject obj, string tag)
	{
		obj.tag = tag;
	}

	public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
	{
		return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
	}

	public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
	{
		return new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
	}

	public static void SetWorldToScreenPos(this RectTransform targetRect, Vector3 targetWorldPos, Camera cam, RectTransform canvasRect, bool isOverlayCamera = true)
	{
		var screenPos = cam.WorldToScreenPoint(targetWorldPos);
		if (isOverlayCamera) cam = null;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, cam, out var canvasPos);
		targetRect.localPosition = canvasPos;
	}

	public static Vector3 GetWorldToScreenPos(Vector3 targetWorldPos, RectTransform canvasRect, bool isOverlayCamera = true)
	{
		var cam = Camera.main;
		var screenPos = cam.WorldToScreenPoint(targetWorldPos);
		if (isOverlayCamera) cam = null;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, cam, out var canvasPos);
		return canvasPos;
	}

	public static void ChangeColor(this Renderer meshRenderer, Color color, int baseColorID, int materialIndex = 0)
	{
		var propertyBlock = new MaterialPropertyBlock();
		meshRenderer.GetPropertyBlock(propertyBlock, materialIndex);
		propertyBlock.SetColor(baseColorID, color);
		meshRenderer.SetPropertyBlock(propertyBlock, materialIndex);
	}

	public static void ChangeSizeOfUiElement(this RectTransform targetRect, Vector2 targetSize, bool isUpwards, float duration = 0f)
	{
		var yDirection = -1f;
		if (isUpwards) yDirection = 1f;

		var currentSize = targetRect.sizeDelta;
		Vector3 currentPos = targetRect.anchoredPosition;
		var heightDifference = (targetSize.y - currentSize.y) / 2 * yDirection;
		var newPos = currentPos + new Vector3(0, heightDifference, 0);

		if (duration <= 0f)
		{
			targetRect.sizeDelta = targetSize;
			targetRect.anchoredPosition = newPos;
		}
		else
		{
			targetRect.DOSizeDelta(targetSize, duration).SetEase(Ease.InOutQuad);
			targetRect.DOAnchorPos(newPos, duration).SetEase(Ease.InOutQuad);
		}
	}

	public static void ChangeChildIndex(this Transform targetTrans, int newChildIndex, int totalChildCount)
	{
		if (newChildIndex < 0 || newChildIndex > totalChildCount)
		{
			newChildIndex = totalChildCount;
		}
		targetTrans.SetSiblingIndex(newChildIndex);
	}

	public static void ChangeAlpha(this Graphic graphic, float alpha)
	{
		var color = graphic.color;
		color.a = alpha;
		graphic.color = color;
	}

	public static string GetFormattedCoinsText(float amount)
	{
		if (amount < 1000)
		{
			return amount.ToString("F1");
		}
		else if (amount < 100000)
		{
			var truncatedValue = Math.Floor(amount / 100.0) / 10.0;
			return truncatedValue.ToString("F1") + "K";
		}
		else if (amount < 1000000)
		{
			return (amount / 1000).ToString("F0") + "K";
		}
		else
		{
			var truncatedValue = Math.Floor(amount / 100000.0) / 10.0;
			return truncatedValue.ToString("F1") + "M";
		}
	}

	public static int GetRandomIndex(int min, int max, int exclusiveIndex)
	{
		var targetIndex = exclusiveIndex;
		var attempts = 0;

		while (targetIndex == exclusiveIndex)
		{
			targetIndex = UnityEngine.Random.Range(min, max);

			attempts++;
			if (attempts > 10) break;
		}

		return targetIndex;
	}
}