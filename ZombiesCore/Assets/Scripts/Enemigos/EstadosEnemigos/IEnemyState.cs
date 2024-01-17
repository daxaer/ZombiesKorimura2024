using System.Threading.Tasks;

public interface IEnemyState
{
    Task<StateResult> HazAccion(object data);
}
