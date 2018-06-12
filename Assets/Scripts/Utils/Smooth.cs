using UnityEngine;

public static class Smooth {

    public static Quaternion SmoothDamp(Quaternion current, Quaternion target, ref Quaternion currentVelocity, float smoothTime)
    {
        float x = Mathf.SmoothDamp(current.x, target.x, ref currentVelocity.x, smoothTime);
        float y = Mathf.SmoothDamp(current.y, target.y, ref currentVelocity.y, smoothTime);
        float z = Mathf.SmoothDamp(current.z, target.z, ref currentVelocity.z, smoothTime);
        float w = Mathf.SmoothDamp(current.w, target.w, ref currentVelocity.w, smoothTime);

        return new Quaternion(x, y, z, w);
    }

    public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
    {
        float x = Mathf.SmoothDamp(current.x, target.x, ref currentVelocity.x, smoothTime);
        float y = Mathf.SmoothDamp(current.y, target.y, ref currentVelocity.y, smoothTime);
        float z = Mathf.SmoothDamp(current.z, target.z, ref currentVelocity.z, smoothTime);

        return new Vector3(x, y, z);
    }
}
