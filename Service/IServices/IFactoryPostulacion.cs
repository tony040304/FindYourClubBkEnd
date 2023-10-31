using Model.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IFactoryPostulacion
    {
        string CrearPostulaciones(PostulacionDTO postu);
    }
}
