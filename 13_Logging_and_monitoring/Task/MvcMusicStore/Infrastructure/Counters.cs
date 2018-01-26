using PerformanceCounterHelper;

namespace MvcMusicStore.Infrastructure
{
  [PerformanceCounterCategory("MvcMusicStor", System.Diagnostics.PerformanceCounterCategoryType.MultiInstance, "MvcMusicStor")]
  public enum Counters
  {
    [PerformanceCounter("Go Home Count", "Go home", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
    GoToHome
      ,

    [PerformanceCounter("Success Login Count", "Success Login", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
    SuccessLogin

    //[PerformanceCounter("Go Login Count", "Go Login", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
    //GoToLogin
  }

  //[PerformanceCounterCategory("MvcMusicStor1", System.Diagnostics.PerformanceCounterCategoryType.MultiInstance, "MvcMusicStor1")]
  //public enum Counters1
  //{
  //  [PerformanceCounter("Success Login Count", "Success Login", System.Diagnostics.PerformanceCounterType.NumberOfItems32)]
  //  SuccessLogin
  //}
}