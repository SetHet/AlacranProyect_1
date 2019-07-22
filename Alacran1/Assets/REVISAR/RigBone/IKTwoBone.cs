using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RigBone
{

    //[ExecuteInEditMode]
    public class IKTwoBone : MonoBehaviour
    {

        #region Configuracion
        [Header("Bones")]
        public Transform bone_pre;
        public Transform bone_mid;
        public Transform bone_pos;
        [Header("Rigs")]
        public Transform effector;
        public Transform codo;
        [Header("Advanced")]
        public bool EnableRotationExtern = true;
        #endregion

        #region MonoBehaviour
        private void Update()
        {
            IKTwo();
        }
        #endregion

        #region Funciones
        private void IKTwo()
        {
            Transform A = bone_pre;
            Transform B = bone_mid;
            Transform C = bone_pos;
            Transform E = effector;
            Transform F = codo;

            Vector3 ab = Formulas.GetDirPos(A, B);
            Vector3 bc = Formulas.GetDirPos(B, C);
            Vector3 ac = Formulas.GetDirPos(A, C);
            Vector3 axis = -Vector3.Cross(ab, bc).normalized;

            if (effector != null)
            {
                Vector3 ae = Formulas.GetDirPos(A, E);
                Vector3 be = Formulas.GetDirPos(B, E);


                float abcAngle = Formulas.TriangleAngle(ae.magnitude, ab, bc);
                Formulas.InRange(ref abcAngle, 1, 179);
                float angle = Vector3.SignedAngle(-ab, bc, axis);
                float angle_dif = abcAngle - angle;
                B.Rotate(axis, angle_dif, Space.World);

                ac = Formulas.GetDirPos(A, C);
                Formulas.newDireccion(A, ac, ae);
            }

            if (codo != null)
            {
                Vector3 af = Formulas.GetDirPos(A, F);
                ac = Formulas.GetDirPos(A, C);
                ab = Formulas.GetDirPos(A, B);
                Vector3 med_ac = (A.transform.position + C.transform.position) / 2f;

                Vector3 c_axis = ac.normalized;
                Vector3 mac_f = F.transform.position - med_ac;
                Vector3 mac_b = B.transform.position - med_ac;
                mac_f = Vector3.ProjectOnPlane(mac_f, c_axis);
                mac_b = Vector3.ProjectOnPlane(mac_b, c_axis);

                float angle = Vector3.SignedAngle(mac_b, mac_f, c_axis);
                A.Rotate(c_axis, angle, Space.World);
            }

            if (EnableRotationExtern)
            {
                C.transform.rotation = E.transform.rotation;
            }
        }

        #endregion
    }
}