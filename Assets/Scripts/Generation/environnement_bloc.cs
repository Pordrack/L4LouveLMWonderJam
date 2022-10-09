using UnityEngine;

namespace Generation
{
    public class environnement_bloc : MonoBehaviour
    {
        public int type; // 0 water 1 //empty
        float hauteur;
        float x,z;
        public GameObject[] obj_type_floor;

    

        public void set_bloc(float x, float y,float z,int type){
            this.type = type;
            obj_type_floor[type].SetActive(true);
            Vector3 temp = new Vector3(x,y,z);
            gameObject.transform.position += temp;
        }

    

        public int get_type(){
            return type;
        }

        public void set_type(int type){
            this.type = type;
        }
        public void set_type(ResourceType type){
            this.type = (int) type;
            set_visibility();
        }
        
        
        

        public void set_visibility(){
            foreach(GameObject obj in obj_type_floor){
                obj.SetActive(false);
            }
            obj_type_floor[type].SetActive(true);
        }
    }
}
