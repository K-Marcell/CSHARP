using System.ComponentModel;
using System.Numerics;
using System.Net.NetworkInformation;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour {
    
    struct weaponData {
        public float w_MagSize;
        public float w_Damage;
        public float w_a_DropOff;
        public float w_a_Speed;
        public bool w_HasFull;
        public bool w_IsFull;
        public int w_FireRate;
        public weaponData(float magSize, float damage, float dropOff, float speed, int fireRate, bool hasFull){
            w_MagSize = magSize;
            w_Damage = damage;
            w_a_DropOff = dropOff;
            w_a_Speed = speed;
            w_HasFull = hasFull;
            w_IsFull = hasFull;
            w_FireRate = fireRate;
        }
    }
    [SerializeField]
    weaponData weapon = new weaponData(1f, 1f, 1f, 1f, 300, false);
    [SerializeField]
    float timeTillShoot = 0f;
    [SerializeField]
    bool canShoot = true;
    GameObject weaponBarrel;
    void Start() {
        weaponBarrel = transform.Find("barrel").gameObject;
    }

    void Update() {
        RaycastHit hit;
        if (timeTillShoot <= 0f)
            timeTillShoot = 0f;
        else
            timeTillShoot -= Time.deltaTime;
        if(Input.GetKeyDown("Mouse 1")){
            if(weapon.w_MagSize > 0)
                if(weapon.w_IsFull)
                    if(timeTillShoot == 0){
                        if(Physics.Raycast(weaponBarrel.transform.position, Vector3.forward(weaponBarrel.transform), weapon.w_a_DropOff, out hit))
                            hit.gameObject.getComponent<PlayerController>().damage(weapon.w_Damage);
                        timeTillShoot = 1f / weapon.w_FireRate;
                    }
                else
                    if(timeTillShoot == 0){
                        if(canShoot)
                            if(Physics.Raycast(weaponBarrel.transform.position, Vector3.forward(weaponBarrel.transform), weapon.w_a_DropOff, out hit)){
                                hit.gameObject.getComponent<PlayerController>().damage(weapon.w_Damage);
                                canShoot = !canShoot;
                            }
                    }
        }
        if(Input.GetKeyUp("Mouse 1"))
            canShoot = true;



    }

}