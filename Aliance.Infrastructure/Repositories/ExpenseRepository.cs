using Aliance.Domain.Entities;
using Aliance.Domain.Enums;
using Aliance.Domain.Interfaces;
using Aliance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Infrastructure.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;

    public ExpenseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Expense> DeleteExpense(int churchId, Expense expense)
    {
        _context.Expense.Remove(expense);

        return expense;
    }

    public async Task<IEnumerable<Expense>> GetAllExpenses(int churchId)
    {
        var Expenses = await _context.Expense
            .Where(i => i.ChurchId == churchId)
            .AsNoTracking()
            .ToListAsync();

        return Expenses;
    }

    public async Task<Expense> GetExpenseByGuid(int churchId, Guid guid)
    {
        var Expense = await _context.Expense.FirstOrDefaultAsync(i => i.Guid == guid && i.ChurchId == churchId);

        return Expense;
    }

    public async Task<IEnumerable<Expense>> GetExpensesByCategory(int churchId, FinancialExpenseCategory category)
    {
        var Expenses = await _context.Expense
            .Where(i => i.ChurchId == churchId && i.Category == category)
            .AsNoTracking()
            .ToListAsync();

        return Expenses;
    }

    public async Task<Dictionary<string, decimal>> GetMonthlyTotals(int churchId, int year)
    {
        // Busca só os totais que existem
        var totals = await _context.Expense
            .Where(i => i.ChurchId == churchId && i.Date.Year == year)
            .GroupBy(i => i.Date.Month)
            .Select(g => new
            {
                Month = g.Key,
                Total = g.Sum(x => x.Amount)
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
        return await _context.Expense
            .Where(i => i.ChurchId == churchId && i.Date >= start && i.Date <= end)
            .SumAsync(i => i.Amount);
    }

    public async Task<Expense> InsertExpense(Expense expense)
    {
        await _context.Expense.AddAsync(expense);

        return expense;
    }

    public async Task<Expense> UpdateExpense(int churchId, Expense expense)
    {
        _context.Expense.Update(expense);

        return expense;
    }
}
