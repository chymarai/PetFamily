﻿using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Models;
using System.Linq.Expressions;

namespace PetFamily.Core.Extensions;

public static class QueriesExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int Page,
        int PageSize,
        CancellationToken token = default)
    {
        var totalCount = await source.CountAsync(token);

        var items = await source
            .Skip((Page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(token);

        return new PagedList<T>
        {
            Items = items,
            TotalCount = totalCount,
            Page = Page,
            PageSize = PageSize
        };
    }

    public static IQueryable<T> WhereIf<T>(
           this IQueryable<T> source,
           bool condition,
           Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}