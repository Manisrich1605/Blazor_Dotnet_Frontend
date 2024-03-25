using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyBlazorApp.Pages;
using ViewModels;
using static System.Net.WebRequestMethods;
namespace MyBlazorApp.Service
{
    public class BlazorService : IBlazorService
    {
        private readonly HttpClient _httpClient;
        //private readonly IDialogService _dialogService;
        private List<LeaveTypeVM> leavetypes;
        private readonly IDialogService _dialogService;
        public BlazorService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        public BlazorService(HttpClient httpClient, IDialogService dialogService)
        {
            _httpClient = httpClient;
            _dialogService = dialogService;
            leavetypes = new List<LeaveTypeVM>();
        }
        public async Task<List<LeaveTypeVM>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:44350/api/LeaveTypes");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<LeaveTypeVM>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
        }
        public async Task<int> Add(LeaveTypeVM leavetype)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44350/api/LeaveTypes", leavetype);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<LeaveTypeVM>();
            if (responseData != null && !string.IsNullOrEmpty(responseData.Name))
            {
                return responseData.Name.GetHashCode();
            }
            else
            {
                throw new InvalidOperationException("Unable to extract ID from the API response.");
            }
        }
        private List<LeaveTypeVM> _leaveTypes = new List<LeaveTypeVM>();

        public async Task Delete(int leavetypeId)
        {
            try
            {
                //http req
                var response = await _httpClient.DeleteAsync($"https://localhost:44350/api/LeaveTypes/{leavetypeId}");
                response.EnsureSuccessStatusCode();//ensure success by deleting product 
                var deletedLeavetype = leavetypes.FirstOrDefault(p => p.Id == leavetypeId);//uses LINQ to match id 
                if (deletedLeavetype != null)
                {
                    leavetypes.Remove(deletedLeavetype);//del from table
                }
            }
            catch (Exception ex)//if caught exception during delete
            {
                Console.WriteLine($"Error deleting leavetype: {ex.Message}");
            }
        }
        public async Task<LeaveTypeVM> GetLeavetypeById(int leavetypeId)
        {
            var leavetype = await _httpClient.GetFromJsonAsync<LeaveTypeVM>($"https://localhost:44350/api/LeaveTypes/{leavetypeId}");
            return leavetype;
        }
        public async Task Update(LeaveTypeVM updatedLeavetype)
        {
            try
            {
                int leavetypeId = updatedLeavetype.Id;
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44350/api/LeaveTypes/{leavetypeId}", updatedLeavetype);
                response.EnsureSuccessStatusCode();
                var existingLeavetype = leavetypes.FirstOrDefault(p => p.Id == updatedLeavetype.Id);
                if (existingLeavetype != null)
                {
                    existingLeavetype.Name = updatedLeavetype.Name;
                    existingLeavetype.DefaultDays = updatedLeavetype.DefaultDays;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating leavetype: {ex.Message}");
            }
        }
    }
}