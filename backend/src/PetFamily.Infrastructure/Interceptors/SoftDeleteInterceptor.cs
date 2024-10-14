using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Interceptors;
public class SoftDeleteInterceptor : SaveChangesInterceptor //интерсептор перехватывает метод SavingChangesAsync  
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync
        (DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if(eventData.Context is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = eventData.Context.ChangeTracker
             .Entries<ISoftDeletable>()
             .Where(e => e.State == EntityState.Deleted); 

        foreach(var entry in entries) 
        {
            entry.State = EntityState.Modified; //Статус Deleted меняем на Modified
            if (entry.Entity is ISoftDeletable item)
            {
                item.Delete();
            }            //Меняем свойство Deleted на true
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
