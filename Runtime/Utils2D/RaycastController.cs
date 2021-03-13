using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using Scream.UniMO.Editor;
#endif
namespace Scream.UniMO.Utils2D
{
    /// <summary>
    /// Class to shoot raycast around a box collider
    /// <para>return all raycast result</para>
    /// </summary>
    [System.Serializable]
    public class RayCastController
    {
#if UNITY_EDITOR
        [ReadOnly, SerializeField]
#endif
        List<HitResult> result = new List<HitResult>();
        BoxCollider2D col = null;
#if UNITY_EDITOR
        [ReadOnly, SerializeField]
#endif
        RayCastInfo info = null;
        RayCastPoint points = null;
        float horiSpace = 0f;
        float vertSpace = 0f;
        [SerializeField] LayerMask layers = -1;
        [SerializeField] Vector2 rayNums = new Vector2(3f, 3f);
        [SerializeField] [Range(-.5f, .5f)] float offset = -.015f;
        [SerializeField] float rayLength = .5f;

        /// <summary>
        /// if there is anything hits in up direction of this object
        /// </summary>
        public bool Up => info.up;

        /// <summary>
        /// if there is anything hits in down direction of this object
        /// </summary>
        public bool Down => info.down;

        /// <summary>
        /// if there is anything hits in right direction of this object
        /// </summary>
        public bool Right => info.right;

        /// <summary>
        /// if there is anything hits in left direction of this object
        /// </summary>
        public bool Left => info.left;

        /// <summary>
        /// if object collides with any objects
        /// </summary>
        /// <returns>result</returns>
        public bool IsCollide => (Up || Down || Right || Left);

        /// <summary>
        /// Contains all hit result from four direcion
        /// </summary>
        public List<HitResult> Result => result;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layers">layers should rays detect</param>
        /// <param name="rayNums">how many rays will cast in one direction every time</param>
        /// <param name="offset">offset from center point</param>
        /// <param name="rayLength">length of rays</param>
        /// <param name="collider2D">which collider to be the reference</param>
        public RayCastController(LayerMask layers, Vector2 rayNums, float offset, float rayLength, BoxCollider2D collider2D)
        {
            this.layers = layers;
            this.rayNums = rayNums;
            this.offset = offset;
            this.rayLength = rayLength;
            Init(collider2D);
        }

        /// <summary>
        /// MUST Call this method to init raycastcontroller if you set value in inspector
        /// </summary>
        /// <param name="collider2D">which collider to be the reference</param>
        public void Init(BoxCollider2D collider2D)
        {
            col = collider2D;
            points = new RayCastPoint();
            info = new RayCastInfo();
            result = new List<HitResult>();
            CalculateSpace();
        }

        /// <summary>
        /// Call this method to update information about hit result
        /// </summary>
        public void Tick()
        {
            UpdateRaycastPoint();
            UpdateInfo();
        }

        void UpdateRaycastPoint()
        {
            Bounds bounds = col.bounds;
            bounds.Expand(offset * 2);
            points.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            points.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            points.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            points.topRight = new Vector2(bounds.max.x, bounds.max.y);
        }

        void UpdateInfo()
        {
            List<HitResult> results = new List<HitResult>();
            info.Reset();
            // Update all horizontal ray and save result
            for (int i = 0; i < rayNums.x; i++)
            {
                #region UPDATE_RIGHT
                Vector2 originPoint = points.topRight;
                originPoint.y -= vertSpace * i;
                RaycastHit2D hit = Physics2D.Raycast(originPoint, Vector2.right, rayLength, layers);
                if (hit.collider)
                {
                    results.Add(new HitResult(hit, HitDirection.Right, new Vector2(1f, i / (rayNums.x - 1))));
                    info.right = true;
                }
#if UNITY_EDITOR
                Debug.DrawRay(originPoint, Vector2.right * rayLength, Color.red);
#endif
                #endregion
                #region UPDATE_LEFT
                originPoint = points.topLeft;
                originPoint.y -= vertSpace * i;
                hit = Physics2D.Raycast(originPoint, Vector2.left, rayLength, layers);
                if (hit.collider)
                {
                    results.Add(new HitResult(hit, HitDirection.Left, new Vector2(0f, i / (rayNums.x - 1))));
                    info.left = true;
                }
#if UNITY_EDITOR
                Debug.DrawRay(originPoint, Vector2.left * rayLength, Color.red);
#endif
                #endregion
            }
            //Update all vertical ray and save result
            for (int i = 0; i < rayNums.y; i++)
            {
                #region UPDATE_UP
                Vector2 originPoint = points.topLeft;
                originPoint.x += horiSpace * i;
                RaycastHit2D hit = Physics2D.Raycast(originPoint, Vector2.up, rayLength, layers);
                if (hit.collider)
                {
                    results.Add(new HitResult(hit, HitDirection.Up, new Vector2(i / (rayNums.y - 1), 0f)));
                    info.up = true;
                }
#if UNITY_EDITOR
                Debug.DrawRay(originPoint, Vector2.up * rayLength, Color.green);
#endif
                #endregion
                #region UPDATE_DOWN
                originPoint = points.bottomLeft;
                originPoint.x += horiSpace * i;
                hit = Physics2D.Raycast(originPoint, Vector2.down, rayLength, layers);
                if (hit.collider)
                {
                    results.Add(new HitResult(hit, HitDirection.Down, new Vector2(i / (rayNums.y - 1), 1f)));
                    info.down = true;
                }
#if UNITY_EDITOR
                Debug.DrawRay(originPoint, Vector2.down * rayLength, Color.green);
#endif
                #endregion
            }
            result.Clear();
            if (results.Count != 0)
                result.AddRange(results);
        }

        void CalculateSpace()
        {
            Bounds bounds = col.bounds;
            bounds.Expand(offset * 2);
            horiSpace = bounds.size.x / (rayNums.y - 1);
            vertSpace = bounds.size.y / (rayNums.x - 1);
        }
    }

    [System.Serializable]
    class RayCastInfo
    {
        public bool up, down, right, left = false;
        public void Reset() => up = down = right = left = false;
    }

    /// <summary>
    /// hit result about raycast
    /// </summary>
    [System.Serializable]
    public class HitResult
    {
        /// <summary>
        /// the hit result in raycasthit2d
        /// </summary>
        public RaycastHit2D Hit2D;

        /// <summary>
        /// which direction hits the object
        /// </summary>
        public HitDirection Direction = HitDirection.None;

        /// <summary>
        /// which ray hits the object
        /// </summary>
        public Vector2 DetailPos;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="hit2D">hit result in raycasthit2d</param>
        /// <param name="direction"></param>
        /// <param name="detailPos">which ray hits the object</param>
        public HitResult(RaycastHit2D hit2D, HitDirection direction, Vector2 detailPos)
        {
            Hit2D = hit2D;
            Direction = direction;
            DetailPos = detailPos;
        }

    }

    [System.Serializable]
    class RayCastPoint
    {
        public Vector2 topLeft, topRight = new Vector2();
        public Vector2 bottomLeft, bottomRight = new Vector2();
    }

    /// <summary>
    /// contains four direction and a none direction
    /// </summary>
    public enum HitDirection
    {
        None,
        Up,
        Down,
        Right,
        Left,
    }
}
