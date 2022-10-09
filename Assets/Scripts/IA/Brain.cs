using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace IA
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(OtherNavBehavior))]
    public class Brain : MonoBehaviour
    {
        private Material _initMat;
        [SerializeField] private Material buggedMat;
        
        
        [SerializeField] private int moveAmount = 3;
        private OtherNavBehavior _nav;
        private Transform _tf;
        private MeshRenderer _rend;

        #region Decision makers

        private DecisionMaker _decision;

        public static NormalDecision NormalDecision { get; private set; }
        public static EnragedDecision EnragedDecision { get; private set; }
        
        public static GlitchedDecision GlitchedDecision { get; private set; }

        #endregion

        private void InitDecision()
        {
            var tf = transform;
            NormalDecision = new NormalDecision(_nav, this, moveAmount, tf);
            EnragedDecision = new EnragedDecision(_nav, this, moveAmount, tf);
            GlitchedDecision = new GlitchedDecision(_nav, this, moveAmount, tf);

            _decision = NormalDecision;
        }
        
        
        private int _playerX = -1, _playerY = -1;
        private int _minX,_maxX,_minY,_maxY;

        private void Start()
        {
            _tf = transform;
            _nav = GetComponent<OtherNavBehavior>();
            _rend = GetComponent<MeshRenderer>();
            EnableRendering(false);
            var playerPos = EnemyManager.Singleton.GetPlayerPosition();
            _playerX = (int) playerPos.x ; 
            _playerY = (int) playerPos.z;
            var half = MapMaskHandler.DrawnMapSize/2;    
            _minX = _playerX - half;
            _maxX = _playerX + half;
            _minY = _playerY-half;
            _maxY = _playerY+half;

            _initMat = _rend.material;
            InitDecision();
            
        }

        private void EnableRendering(bool b)
        {
            if (!b && !_rend.isVisible) return;
            if (b && _rend.isVisible) return;
            _rend.enabled = b;
            foreach (var childRenderer in transform.GetComponentsInChildren<MeshRenderer>())
            {
                childRenderer.enabled = b;
            }
        }
        

        /// <summary>
        /// Permet d'obtenir les cases déplaçables aux alentours.
        /// Cette fonction prend aussi les cases diagonales pour l'instant.
        /// </summary>
        /// <param name="pos">Position of the player</param>
        /// <returns>List of possible position on the map.</returns>
        public List<int[]> GetAvailableSurrounding(Vector3 pos)
        {
            var x = pos.x;
            var y = pos.z;

            var availableSurrounding = new List<int[]>();

            int posX, posY;
            for (var i = 0; i < 9; i++)
            {
                posX = (int) x + (i % 3) - 1;
                posY = (int) y + (i / 3) - 1;

                if (Generation.GenerationMap.IsAvailable(posX, posY))
                {
                    availableSurrounding.Add(new int[] {posX, posY});
                }
                
            }

            return availableSurrounding;
        }

        public void ShallBeDrawn()
        {
            if(_playerX == -1 || _playerY == -1)
                return;
            
            //We only hide the visual part, the animals still play, even out of bounds.
            var local = _tf.position;
            if (local.x < _minX || local.x > _maxX || local.z < _minY || local.z > _maxY)
            {
                EnableRendering(false);
            }
            else
            {
                EnableRendering(true);
            }
        }


        public void GlitchSwitch()
        {
            print(_decision.GetId());
            if (!_decision.GetId().Equals("Normal")) throw new Exception("Only applies to normal");

            if (UnityEngine.Random.Range(0f, 1f) < 0.7f) //70% chance to enrage rather than glitch
            {
                SwitchDecision(EnragedDecision);
            }
            else
            {
                SwitchDecision(GlitchedDecision);
            }
            
        }
        
        public void SwitchDecision(DecisionMaker decision)
        {
            Debug.Log($"Switching decision from {_decision} to {decision}");
            _decision = decision;
            if (decision is NormalDecision)
                _rend.material = _initMat;
            else if (decision is EnragedDecision)
                _rend.material = buggedMat;
            else if (decision is GlitchedDecision)
                _rend.material.color = Color.red;
        }
        public void Decide() => _decision.Decide();
        
        public virtual void Die()
        {
            Debug.Log("He's dead Jim.");
            _decision.Die();
            Destroy(gameObject);
        }
    }
    
    
}
