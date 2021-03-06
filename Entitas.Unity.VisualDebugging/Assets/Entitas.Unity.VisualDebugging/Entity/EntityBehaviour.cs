﻿using UnityEngine;
using Entitas;

namespace Entitas.Unity.VisualDebugging {
    public class EntityBehaviour : MonoBehaviour {
        public Pool pool { get { return _pool; } }
        public Entity entity { get { return _entity; } }
        public bool[] unfoldedComponents { get { return _unfoldedComponents; } }

        Pool _pool;
        Entity _entity;
        bool[] _unfoldedComponents;

        public void Init(Pool pool, Entity entity) {
            _pool = pool;
            _entity = entity;
            _unfoldedComponents = new bool[_pool.totalComponents];
            _entity.OnComponentAdded += onComponentAdded;
            _entity.OnComponentRemoved += onComponentRemoved;
            updateName();

            UnfoldAllComponents();
        }

        public void UnfoldAllComponents() {
            for (int i = 0; i < _unfoldedComponents.Length; i++) {
                _unfoldedComponents[i] = true;
            }
        }

        public void FoldAllComponents() {
            for (int i = 0; i < _unfoldedComponents.Length; i++) {
                _unfoldedComponents[i] = false;
            }
        }

        public void DestroyBehaviour() {
            _entity.OnComponentAdded -= onComponentAdded;
            _entity.OnComponentRemoved -= onComponentRemoved;
            Destroy(gameObject);
        }

        void onComponentAdded(Entity e, int index, IComponent component) {
            updateName();
        }

        void onComponentRemoved(Entity e, int index, IComponent component) {
            if (_pool.HasEntity(e)) {
                updateName();
            } else {
                DestroyBehaviour();
            }
        }

        void updateName() {
            name = _entity.ToString();
        }
    }
}