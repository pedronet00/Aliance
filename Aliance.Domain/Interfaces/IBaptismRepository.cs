using Aliance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Interfaces
{
    public interface IBaptismRepository
    {

        Task<IEnumerable<Baptism>> GetAllBaptisms();

        Task<Baptism> GetById(int id);

        Task<Baptism> Add(Baptism baptism);

        void Update(Baptism baptism);

        Task<bool> Delete(int id);

    }
}
