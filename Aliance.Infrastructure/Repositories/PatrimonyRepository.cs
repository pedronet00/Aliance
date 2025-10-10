using Aliance.Domain.Entities;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories;

public class PatrimonyRepository : IPatrimonyRepository
{
    private readonly AppDbContext _context;

    public PatrimonyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Patrimony> DeletePatrimony(int churchId, int id)
    {
        var patrimony = await _context.Patrimony.FirstOrDefaultAsync(p => p.ChurchId == churchId && p.Id == id);

        _context.Patrimony.Remove(patrimony!);

        return patrimony;
    }

    public async Task<IEnumerable<Patrimony>> GetAllPatrimonies(int churchId)
    {
        var patrimonies = await _context.Patrimony.AsNoTracking().Where(p => p.ChurchId == churchId).ToListAsync();

        return patrimonies;
    }

    public async Task<Patrimony?> GetPatrimonyByGuid(int churchId, Guid guid)
    {
        var patrimony = await _context.Patrimony
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ChurchId == churchId && p.Guid == guid);

        return patrimony;
    }


    public async Task<Patrimony> InsertPatrimony(Patrimony patrimony)
    {
        await _context.Patrimony.AddAsync(patrimony);

        return patrimony;
    }

    public async Task<Patrimony> UpdatePatrimony(int churchId, Patrimony patrimony)
    {
        _context.Patrimony.Update(patrimony!);

        return patrimony;
    }

    public async Task<Patrimony> GetByGuidWithDocumentsAsync(int churchId, Guid guid)
    {
        var documents = await _context.Patrimony
            .Include(p => p.Documents) // carrega os documentos vinculados
            .FirstOrDefaultAsync(p => p.Guid == guid && p.ChurchId == churchId);

        return documents!;
    }

    public async Task<int> PatrimoniesCount(int churchId)
    {
        var totalPatrimonies = await _context.Patrimony
            .Where(p => p.ChurchId == churchId)
            .CountAsync();

        return totalPatrimonies;
    }
}
