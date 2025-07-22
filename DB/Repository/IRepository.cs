namespace NoteApp.DB.Repository;
public interface IRepository<T> : IDisposable 
        where T : class
{
    Task<List<T>> GetList(); // получение всех объектов
    Task<T?> Get(string name); // получение одной объекта по названию
    Task Create(T item); // создание объекта
    Task Delete(string name); // удаление одного объекта по названию
    Task Save();  // сохранение изменений
}