﻿using PetFamily.Volunteers.Domain.VolunteersValueObjects;

namespace PetFamily.Accounts.Domain;

public class AdminAccount
{
    public const string ADMIN = nameof(ADMIN);

    private AdminAccount()
    {
    }

    public AdminAccount(FullName fullName, User user)
    {
        Id = Guid.NewGuid();    
        FullName = fullName;
        User = user;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public FullName FullName { get; set; }
}
