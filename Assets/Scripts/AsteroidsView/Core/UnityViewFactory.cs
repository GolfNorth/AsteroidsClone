﻿using AsteroidsCore;
using UnityEngine;

namespace AsteroidsView
{
    public class UnityViewFactory : ViewFactory
    {
        private readonly Context _context;
        
        public UnityViewFactory(Context context)
        {
            _context = context;
        }


        public override IView<ShipModel> CreateShip(ShipModel model)
        {
            var gameObject = _context.ViewMode == ViewMode.Polygonal
                ? Object.Instantiate(_context.ShipDataSetter.polygonalPrefab)
                : Object.Instantiate(_context.ShipDataSetter.spritePrefab);

            gameObject.name = "Ship" + _context.ViewMode;

            var view = gameObject.AddComponent<ShipView>();
            view.ViewMode = _context.ViewMode;
            view.Context = _context;
            view.Model = model;

            return view;
        }

        public override IView<BulletModel> CreateBullet(BulletModel model)
        {
            var gameObject = _context.ViewMode == ViewMode.Polygonal
                ? Object.Instantiate(_context.BulletDataSetter.polygonalPrefab)
                : Object.Instantiate(_context.BulletDataSetter.spritePrefab);

            gameObject.name = "Bullet" + _context.ViewMode;

            var view = gameObject.AddComponent<BulletView>();
            view.ViewMode = _context.ViewMode;
            view.Context = _context;
            view.Model = model;

            return view;
        }

        public override IView<LaserModel> CreateLaser(LaserModel model)
        {
            var gameObject = _context.ViewMode == ViewMode.Polygonal
                ? Object.Instantiate(_context.LaserDataSetter.polygonalPrefab)
                : Object.Instantiate(_context.LaserDataSetter.spritePrefab);

            gameObject.name = "Laser" + _context.ViewMode;

            var view = gameObject.AddComponent<LaserView>();
            view.ViewMode = _context.ViewMode;
            view.Context = _context;
            view.Model = model;

            return view;
        }

        public override IView<AsteroidModel> CreateAsteroid(AsteroidModel model)
        {
            var gameObject = _context.ViewMode == ViewMode.Polygonal
                ? Object.Instantiate(_context.AsteroidDataSetter.polygonalPrefab)
                : Object.Instantiate(_context.AsteroidDataSetter.spritePrefab);

            gameObject.name = "Asteroid" + _context.ViewMode;

            var view = gameObject.AddComponent<AsteroidView>();
            view.ViewMode = _context.ViewMode;
            view.Context = _context;
            view.Model = model;

            return view;
        }

        public override IView<UfoModel> CreateUfo(UfoModel model)
        {
            var gameObject = _context.ViewMode == ViewMode.Polygonal
                ? Object.Instantiate(_context.UfoDataSetter.polygonalPrefab)
                : Object.Instantiate(_context.UfoDataSetter.spritePrefab);

            gameObject.name = "Ufo" + _context.ViewMode;

            var view = gameObject.AddComponent<UfoView>();
            view.ViewMode = _context.ViewMode;
            view.Context = _context;
            view.Model = model;

            return view;
        }
    }
}