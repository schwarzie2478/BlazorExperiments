using Example01.Models;
using Microsoft.AspNetCore.Components;
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
        private string _url = "";
        public string ApplicationsServiceUrl
        {
            get
            {
                if (_url == "")
                    _url = "https://localhost:5001";
                return _url;
            }
            set
            {
                _url = value;
            }
        }

        public IEnumerable<Application> ApplicationSearchResult { get; private set; }
        public Application ApplicationResult { get; private set; }
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

            SearchInProgress = true;
            NotifyStateChanged();

            ApplicationSearchResult = await http.GetJsonAsync<IEnumerable<Application>>($"{ApplicationsServiceUrl}/api/application");

            SearchInProgress = false;
            NotifyStateChanged();
        }
        public async Task GetApplication(int id)
        {
            ApplicationResult = await http.GetJsonAsync<Application>($"{ApplicationsServiceUrl}/api/application/{id}");
            NotifyStateChanged();
        }

        public async Task CreateApplicaton(Application newApplication)
        {

            await http.SendJsonAsync(HttpMethod.Post, $"{ApplicationsServiceUrl}/api/application", newApplication);
            NotifyStateChanged();
        }
        public async Task DeleteApplication(int id)
        {

            await http.SendJsonAsync(HttpMethod.Delete, $"{ApplicationsServiceUrl}/api/application/{id}", id);
            NotifyStateChanged();
        }

        public async Task UpdateApplication(Application application)
        {

            await http.SendJsonAsync(HttpMethod.Put, $"{ApplicationsServiceUrl}/api/application/{application.Id}", application); 
            NotifyStateChanged();

        }

        public async Task CreateApplicationRole(Application application, ApplicationRole role)
        {
            await http.SendJsonAsync(HttpMethod.Post, $"{ApplicationsServiceUrl}/api/application/{application.Id}/applicationrole", role);
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
