namespace ITI_MVC.Interfaces
{
    public interface ICRUD<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public T Create(T entity);
        public void Delete(int id);
        public void Update(T entity);
    }
}
