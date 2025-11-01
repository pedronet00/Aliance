using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Domain.Pagination;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Aliance.Infrastructure.Repositories;

public class IncomeRepository : IIncomeRepository
{
    private readonly AppDbContext _context;

    public IncomeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Income> DeleteIncome(int churchId, Income income)
    {
        _context.Income.Remove(income);

        return income;
    }

    public async Task<PagedResult<Income>> GetAllIncomes(int churchId, int pageNumber, int pageSize)
    {
        var incomes = _context.Income
            .Where(i => i.ChurchId == churchId)
            .AsNoTracking();

        var totalCount = await incomes.CountAsync();

        var items = await incomes
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Income>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<Income> GetIncomeByGuid(int churchId, Guid guid)
    {
        var income = await _context.Income.FirstOrDefaultAsync(i => i.Guid == guid && i.ChurchId == churchId);

        return income;
    }

    public async Task<IEnumerable<Income>> GetIncomesByCategory(int churchId, FinancialIncomingCategory category)
    {
        var incomes = await _context.Income
            .Where(i => i.ChurchId == churchId && i.Category == category)
            .AsNoTracking() 
            .ToListAsync();

        return incomes;
    }

    public async Task<Dictionary<string, decimal>> GetMonthlyTotals(int churchId, int year)
    {
        // Busca só os totais que existem
        var totals = await _context.Income
            .Where(i => i.ChurchId == churchId && i.Date.Year == year)
            .GroupBy(i => i.Date.Month)
            .Select(g => new
            {
                Month = g.Key,
                Total = g.Sum(x => x.Amount.Value)
            })
            .ToListAsync();

        // Cria um dicionário já com todos os meses zerados
        var result = Enumerable.Range(1, 12).ToDictionary(
            m => CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(m), // Janeiro, Fevereiro, ...
            m => 0m
        );

        // Substitui os meses que têm valores
        foreach (var t in totals)
        {
            var monthName = CultureInfo.GetCultureInfo("pt-BR").DateTimeFormat.GetMonthName(t.Month);
            result[monthName] = t.Total;
        }

        return result;
    }

    public async Task<decimal> GetTotalByDateRange(int churchId, DateTime start, DateTime end)
    {
        return await _context.Income
            .Where(i => i.ChurchId == churchId && i.Date >= start && i.Date <= end)
            .SumAsync(i => i.Amount.Value);
    }

    public async Task<Income> InsertIncome(Income income)
    {
        await _context.Income.AddAsync(income);

        return income;
    }

    public async Task<Income> UpdateIncome(int churchId, Income income)
    {
        _context.Income.Update(income);

        return income;
    }
}
