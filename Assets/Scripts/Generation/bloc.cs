using UnityEngine;

namespace Generation
{
    public class bloc : MonoBehaviour
    {

        int type;
        float hauteur;
        float x,z;
        public GameObject[] obj_type_floor;

    

        public void set_bloc(float x, float y,float z,int type){
            this.type = type;
            obj_type_floor[type].SetActive(true);
            Vector3 temp = new Vector3(x,y,z);
            gameObject.transform.position += temp;
        }


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public int get_type(){
            return type;
        }

        public void set_type(int type){
            this.type = type;
        }

        public void set_type(ResourceType type)
        {
            this.type = (int) type;
        }

        public void set_visibility(){
            obj_type_floor[type].SetActive(true);
        }

    
    }
}
