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
        public Transform Bone_root;
        public Transform bone_pre;
        public Transform bone_mid;
        public Transform bone_pos;
        [Header("Rigs")]
        public Transform effector;
        public Transform codo;
        public Transform rig_root;
        [Header("Advanced")]
        public bool EnableRotationExtern = true;
        public Cambio CambioRoot = Cambio.None;

        public enum Cambio
        {
            None,
            Posicion,
            Rotacion
        }

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

            if (rig_root != null && Bone_root != null) {

                Transform R = Bone_root;
                Transform ER = rig_root;
                Vector3 rer = Formulas.GetDirPos(R, ER);
                Vector3 ra = Formulas.GetDirPos(R, A);
                Vector3 r_axis = Vector3.Cross(ra, rer).normalized;
                float r_angle = Vector3.SignedAngle(ra, rer, r_axis);

                R.Rotate(r_axis, r_angle, Space.World);

                if (CambioRoot != Cambio.None)
                {
                    if (CambioRoot == Cambio.Posicion)
                    {
                        ER.position = A.position;
                    }
                    else if (CambioRoot == Cambio.Rotacion)
                    {
                        ER.RotateAround(R.position, r_axis, r_angle);
                        ER.position = A.position;
                    }
                }
            }


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