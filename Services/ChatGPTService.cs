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

        public async Task<string> GenerateWorkoutPlan(string goals, string preferences, string injuries, int preferredSplit, string FitnessLevel)
        {
            string splitDescription = preferredSplit switch
            {
                3 => "a 3-day workout split",
                4 => "a 4-day workout split",
                5 => "a 5-day workout split",
                _ => "a 3-day workout split"
            };

            string prompt = $"Generate {splitDescription} for someone with the following details:\n" +
                            $"Goals: {goals}\n" +
                            $"Preferences: {preferences}\n" +
                            $"Injuries: {injuries}\n\n" +
                            $"Fitness Level: {FitnessLevel}\n\n" +
                            $"provide the workout split with exercises, sets, and reps" +
                            $"Strictly follow this formatting with Day X: < focus of that day > and then exercise | number of sets | number of reps per set include an extra line break between each day title." +
                            $"Never generate more than 5-day splits. Do not include Rest days. Never over 700 characters, aim for 500 characters (tokens)";

            var completionRequest = new CompletionRequest
            {
                Prompt = prompt,
                MaxTokens = 700,
                Temperature = 0.7
            };

            try
            {
                TimeSpan timeSinceLastRequest = DateTime.Now - _lastRequestTime;
                if (timeSinceLastRequest.TotalSeconds < 20) // no rapid requests (adjust the time as needed)
                {
                    return "Please wait a few seconds before trying again.";
                }

                var result = await _api.Completions.CreateCompletionAsync(completionRequest);
                _lastRequestTime = DateTime.Now; // last request time
                return result.Completions[0].Text;
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("TooManyRequests"))
            {
                //handle quota exceeded error
                return "Unable to generate a workout plan at the moment due to API request limits being exceeded. Please try again later.";
            }
            catch (Exception ex)
            {
                // unexpected errors
                return $"An unexpected error occurred: {ex.Message}. Please try again later.";
            }
        }
    }
}
