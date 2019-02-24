using Example01.Models;
using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Example_01.Services
{
    public class ApplicationState
    {
        public ApplicationState()
        {
            
        }
        public string ApplicationsServiceUrl { get; set; }

        public IEnumerable<Application> ApplicationSearchResult { get; private set; }
        public bool SearchInProgress { get; private set; }
        public bool SuppliedIdIsValid { get; private set; } = true;

        // Lets components receive change notifications
        // Could have whatever granularity you want (more events, hierarchy...)
        public event Action OnChange;

        // Receive 'http' instance from DI
        private readonly HttpClient http;
        public ApplicationState(HttpClient httpInstance)
        {
            http = httpInstance;
        }

        public async Task GetApplications()
        {
            ApplicationsServiceUrl = "https://localhost:5001";
            SearchInProgress = true;
            NotifyStateChanged();

            ApplicationSearchResult = await http.GetJsonAsync<IEnumerable<Application>>($"{ApplicationsServiceUrl}/api/application");

            SearchInProgress = false;
            NotifyStateChanged();
        }

        public async Task CreateApplicaton(Application newApplication)
        {
                        ApplicationsServiceUrl = "https://localhost:5001";
                        await http.SendJsonAsync(HttpMethod.Post, $"{ApplicationsServiceUrl}/api/application", newApplication);
                        NotifyStateChanged();
        }
        public async Task DeleteApplication(int id)
        {
            ApplicationsServiceUrl = "https://localhost:5001";
            await http.SendJsonAsync(HttpMethod.Delete, $"{ApplicationsServiceUrl}/api/application/{id}", id);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
