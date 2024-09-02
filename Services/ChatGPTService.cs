using AlphaWorkout.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using OpenAI_API;
using OpenAI_API.Completions;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlphaWorkout.Services
{
    public class ChatGPTService
    {
        private readonly OpenAIAPI _api;
        private DateTime _lastRequestTime;

        public ChatGPTService(string apiKey)
        {
            _api = new OpenAIAPI(apiKey);
            _lastRequestTime = DateTime.MinValue;
        }

        public async Task<string> GenerateWorkoutPlan(string goals, string preferences, string injuries, string FitnessLevel)
        {

            string prompt = $"Generate **ONLY** a 3-day workout split for someone with the following details:\n" +
                            $"Goals: {goals}\n" +
                            $"Preferences: {preferences}\n" +
                            $"Injuries: {injuries}\n\n" +
                            $"Fitness Level: {FitnessLevel}\n\n" +
                            $"Provide a split with exercises, sets, and reps for exactly 3 days.\n" +
                            $"Use this format strictly: Day X: <focus of that day> \n Exercise Name | Number of Sets | Number of Reps per Set\n\n" +
                            $"Do not include rest days. Limit the response to 500-700 characters (tokens). Insert a line break between each day.";

            var completionRequest = new CompletionRequest
            {
                Prompt = prompt,
                MaxTokens = 700,
                Temperature = 0.5
            };

            try
            {
                TimeSpan timeSinceLastRequest = DateTime.Now - _lastRequestTime;
                if (timeSinceLastRequest.TotalSeconds < 20)
                {
                    return "Please wait a few seconds before trying again.";
                }

                var result = await _api.Completions.CreateCompletionAsync(completionRequest);
                _lastRequestTime = DateTime.Now;
                return result.Completions[0].Text.Trim();
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("TooManyRequests"))
            {
                return "Unable to generate a workout plan at the moment due to API request limits being exceeded. Please try again later.";
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}. Please try again later.";
            }
        }
    }
}
