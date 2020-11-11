using Microsoft.Extensions.DependencyInjection;
using Q101.NetCoreTelegramBotHelper.Abstract;
using Q101.NetCoreTelegramBotHelper.Concrete;
using Q101.NetCoreTelegramBotHelper.Config.Abstract;

namespace Q101.NetCoreTelegramBotHelper.Extensions
{
    /// <summary>
    /// IServiceCollection extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register telegram bot helper services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddQ101TelegramBotHelper<T>(
            this IServiceCollection services) where T : class, ITelegramBotHelperConfig
        {

            services.AddTransient<ITelegramBotHelperConfig, T>();
            services.AddTransient<ITelegramBotHelper, TelegramBotHelper>();

            return services;
        }
    }
}
