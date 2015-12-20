// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;

// --------------------------------------------------
// 
// Script de Déplacements
// 
// --------------------------------------------------
public class ControlCharacterScript : MonoBehaviour
{
    [SerializeField]
    Camera _cameraPlayer = new Camera();

    [SerializeField]
    public bool _isActive = true;

    // Angle minimum et maximum de rotation sur l'axe Y (tourne sur Y pour changer la vue sur X)    // PAS NECESSAIRE
    // -360 -> 360, possibilite de faire un tour complet (ou plusieurs) sur l'axe Y
    //private float minimumX = -360.0f;
    //private float maximumX = 360.0f;

    // Angle minimum et maximum de rotation sur l'axe X (tourne sur X pour changer la vue sur Y)
    // -45 -> 45, angle de vue optimal pour le jeu
    private float minimumY = -60.0f;
    private float maximumY = 60.0f;

    // Angle Y initial
    private float rotationY = 0.0f;

    // Paramètres modifiables par le joueur
    public float sensitivity = 5.0f; // 5 par défaut

    [SerializeField]
    Rigidbody _rigibodyPlayer = new Rigidbody();

    // Parametres personnages
    private float speed      = 10.0f; // Vitesse de deplacement
    private float gravity    = -9.81f; // Gravité terrestre 9.81 m/s²

    void FixedUpdate()
    {
        if(_isActive)
        {
            Vector3 direction = new Vector3(0, 0, 0);

            // Dans InputManager, recupere les etats de l'axe "Horizontal" (touches 'q' ou 'd') et de l'axe "Horizontal" (touches 'z' ou 's')
            // En fonction des états de Z,Q,S,D, on créé une direction
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            direction = transform.TransformDirection(direction) * speed;

            // Calcul du vecteur de déplacement
            Vector3 actualVel = new Vector3(_rigibodyPlayer.velocity.x, 0, _rigibodyPlayer.velocity.z);
            Vector3 WantedVel = (direction - actualVel);
            _rigibodyPlayer.AddForce(WantedVel, ForceMode.VelocityChange);
            _rigibodyPlayer.AddForce(0, gravity, 0);

            // Si le joueur déplace la souris sur l'axe Horizontal
            if (Input.GetAxis("Mouse X") != 0)
            {
                // Rotation sur l'axe Y
                this.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            }

            // Si le joueur déplace la souris sur l'axe Vertical
            if (Input.GetAxis("Mouse Y") != 0)
            {
                rotationY -= Input.GetAxis("Mouse Y") * sensitivity;

                // Permet de contenir les valeurs entre -360 et 360
                if (rotationY < -360)
                    rotationY += 360;
                if (rotationY > 360)
                    rotationY -= 360;

                // Mathf.Clamp permet de "borné" la valeur entre de limite, ex: Mathf.Clamp(160,-50,42), la valeur de sortie devient 42. 
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                // Applique le nouvel angle. !!! Penser à ajouter l'angle sur l'axe Y, sinon la caméra sera bloquée !!!
                _cameraPlayer.transform.rotation = Quaternion.Euler(rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
