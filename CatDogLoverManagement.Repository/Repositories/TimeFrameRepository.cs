using CatDogLoverManagement.Pages.Post;
using CatDogLoverManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDogLoverManagement.Repository.Repositories
{
    public class TimeFrameRepository : ITimeFrameRepository
    {  private readonly CatDogLoveManagementContext context=new();
        

        public async Task<TimeFrame> GetAsync(Guid id)
        {
            var time= await context.TimeFrames.Where(x=>x.ServiceId==id).FirstOrDefaultAsync();
            return time;
        }

        public async Task<TimeFrame> GetTimeFrameByIdAsync(Guid id)
        {
            var time = await context.TimeFrames.Where(x => x.Id == id).FirstOrDefaultAsync();
            return time;
        }

        public async Task<bool> UpdateAsync(TimeFrame timeFrame)
        {
            var existingTimeFrame = await context.TimeFrames.FindAsync(timeFrame.Id);

            if (existingTimeFrame != null)
            {
                existingTimeFrame.From=timeFrame.From;
                existingTimeFrame.To=timeFrame.To;
            }
            var result = await context.SaveChangesAsync();
            if (result > 0)
                return true;
            return false;
        }
    }
}
