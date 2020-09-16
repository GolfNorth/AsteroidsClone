namespace AsteroidsCore
{
    public abstract class ViewFactory
    {
        public virtual IView<TModel> CreateView<TModel>(TModel model) where TModel : Model
        {
            switch (typeof(TModel).Name)
            {
                case nameof(ShipModel):
                    return CreateShip(model as ShipModel) as IView<TModel>;
                case nameof(BulletModel):
                    return CreateBullet(model as BulletModel) as IView<TModel>;
                case nameof(LaserModel):
                    return CreateLaser(model as LaserModel) as IView<TModel>;
                case nameof(AsteroidModel):
                    return CreateAsteroid(model as AsteroidModel) as IView<TModel>;
                case nameof(UfoModel):
                    return CreateUfo(model as UfoModel) as IView<TModel>;
                default:
                    return null;
            }
        }

        public abstract IView<ShipModel> CreateShip(ShipModel model);

        public abstract IView<BulletModel> CreateBullet(BulletModel model);

        public abstract IView<LaserModel> CreateLaser(LaserModel model);

        public abstract IView<AsteroidModel> CreateAsteroid(AsteroidModel model);

        public abstract IView<UfoModel> CreateUfo(UfoModel model);
    }
}