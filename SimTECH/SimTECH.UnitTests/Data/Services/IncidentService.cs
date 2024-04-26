using SimTECH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTECH.UnitTests.Data.Services;

public class IncidentService
{
    public Task<List<Incident>> GetIncidents() => Task.FromResult(new List<Incident>());
}
