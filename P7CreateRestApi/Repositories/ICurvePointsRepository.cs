using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface ICurvePointsRepository
    {
        Task<IEnumerable<CurvePoint>> GetAllCurvePointAsync();
        Task<CurvePoint> GetCurvePointByIdAsync(int id);
        Task<CurvePoint> CreateCurvePointAsync(CurvePoint curvePoint);
        Task<CurvePoint> UpdateCurvePointAsync(CurvePoint curvePoint);
        Task DeleteCurvePointAsync(int id);
        Task<bool> CurvePointExistsAsync(int id);

    }
}
