using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PackageRepository(DataContext context) : BaseRepository<PackageEntity>(context), IPackageRepository
{
}

