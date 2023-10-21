﻿using CatDogLoverManagement.Repository.Models;

namespace CatDogLoverManagement.Pages.Post
{
    public interface ITimeFrameRepository
    {
        Task<TimeFrame> GetAsync(Guid id);
        
        Task<bool> UpdateAsync(TimeFrame time);
    }
}