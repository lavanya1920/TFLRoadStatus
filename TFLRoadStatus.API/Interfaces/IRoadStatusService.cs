using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TFLRoadStatus.API.Models;

namespace TFLRoadStatus.API.Interfaces
{
    public interface IRoadStatusService
    {
        Task<RoadStatus> GetRoadStatus(string roadId);
    }
}
