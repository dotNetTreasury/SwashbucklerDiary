﻿using Microsoft.AspNetCore.Components;
using NoDecentDiary.Components;
using NoDecentDiary.IServices;
using NoDecentDiary.Models;

namespace NoDecentDiary.Pages
{
    public partial class SearchPage : PageComponentBase
    {
        private List<DiaryModel> Diaries = new();

        [Inject]
        private IDiaryService? DiaryService { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public string? Search { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            await UpdateDiaries();
        }

        private async Task UpdateDiaries()
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                Diaries = await DiaryService!.QueryAsync(it =>
                    it.Title!.Contains(Search) ||
                    it.Content!.Contains(Search));
            }
            else
            {
                Diaries = new();
            }
        }

        private async Task TextChanged(string value)
        {
            Search = value;
            await UpdateDiaries();
            var url = Navigation!.GetUriWithQueryParameter("Search", value);
            Navigation!.NavigateTo(url);
        }
    }
}
