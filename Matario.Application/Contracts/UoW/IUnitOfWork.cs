using System;
namespace Matario.Application.Contracts.UoW
{
	public interface IUnitOfWork
    {
        Task SaveChangesAsync();
	}
}

