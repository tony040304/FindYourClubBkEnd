﻿using Model.DTOS;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IJugadorServices
    {

        string CrearPostulaciones(PostulacionDTO postu);

    }
}
