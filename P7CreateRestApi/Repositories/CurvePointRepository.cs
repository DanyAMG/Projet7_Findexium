using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;


namespace P7CreateRestApi.Repositories
{
    public class CurvePointRepository : ICurvePointsRepository
    {
        private readonly LocalDbContext _context;

        public CurvePointRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurvePoint>> GetAllCurvePointAsync()
        {
            return await _context.CurvePoints.ToListAsync();
        }

        public async Task<CurvePoint> GetCurvePointByIdAsync(int id)
        {
            return await _context.CurvePoints.FindAsync(id);
        }

        public async Task<CurvePoint> CreateCurvePointAsync(CurvePoint curvePoint)
        {
            _context.CurvePoints.Add(curvePoint);
            await _context.SaveChangesAsync();
            return curvePoint;
        }

        public async Task<CurvePoint> UpdateCurvePointAsync(CurvePoint curvePoint)
        {
            _context.Entry(curvePoint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return curvePoint;
        }

        public async Task DeleteCurvePointAsync(int id)
        {
            var curvePoint = await _context.CurvePoints.FindAsync(id);
            if(curvePoint != null)
            {
                _context.CurvePoints.Remove(curvePoint);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CurvePointExistsAsync(int id)
        {
            return await _context.CurvePoints.AnyAsync(e => e.Id == id);
        }
    }
}
