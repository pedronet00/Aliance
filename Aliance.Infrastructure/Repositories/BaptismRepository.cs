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

public class BaptismRepository : IBaptismRepository
{

    private readonly AppDbContext _context;

    public BaptismRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Baptism> Add(Baptism baptism)
    {
        if(baptism is null)
            throw new ArgumentNullException(nameof(baptism), "Baptism cannot be null");
        
        await _context.Baptism.AddAsync(baptism);

        return baptism;
    }

    public async Task<bool> Delete(int id)
    {
        var baptism = await _context.Baptism.FindAsync(id);

        if (baptism is null)
            throw new KeyNotFoundException("Baptism not found");

        _context.Baptism.Remove(baptism);

        return true;

    }

    public async Task<IEnumerable<Baptism>> GetAllBaptisms()
    {
        var baptisms = await _context.Baptism
            .Include(b => b.Pastor)
            .Include(b => b.User)
            .ToListAsync();

        return baptisms;
    }

    public async Task<Baptism> GetById(int id)
    {
        var baptism = await _context.Baptism
            .Include(b => b.Pastor)
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (baptism is null)
            throw new KeyNotFoundException("Baptism not found");

        return baptism;
    }

    public void Update(Baptism baptism)
    {
        if (baptism is null)
            throw new ArgumentNullException(nameof(baptism), "Baptism cannot be null");

        _context.Baptism.Update(baptism);
    }
}
