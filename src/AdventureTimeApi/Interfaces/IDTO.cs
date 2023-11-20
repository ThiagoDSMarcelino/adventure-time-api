namespace AdventureTimeApi.Interfaces;

public interface IDTO<T, U>
where T : IDTO<T, U>
where U : Model
{
    T Convert(U obj);
}