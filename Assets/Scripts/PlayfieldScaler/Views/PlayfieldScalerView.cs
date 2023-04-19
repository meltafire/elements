using UnityEngine;

namespace Elements.PlayfieldScaler.Views
{
    public class PlayfieldScalerView : MonoBehaviour
    {
        public Vector3 LocalScale => transform.localScale;

        public void SetLocalScale(Vector3 scale)
        {
            transform.localScale = scale;
        }
    }
}
