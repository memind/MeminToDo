using Consul;
using Microsoft.Extensions.Options;

namespace HealthCheck.Consul
{
    public class ConsulRegisterServices : IHostedService
    {
        private IConsulClient _client;
        private WorkoutConfiguration _workoutConfig;
        private SkillConfiguration _skillConfig;
        private EntertainmentConfiguration _entertainmentConfig;

        public ConsulRegisterServices(IOptions<WorkoutConfiguration> workoutConfiguration, IOptions<SkillConfiguration> skillConfiguration, IConsulClient client, IOptions<EntertainmentConfiguration> entertainmentConfiguration)
        {
            _client = client;
            _workoutConfig = workoutConfiguration.Value;
            _skillConfig = skillConfiguration.Value;
            _entertainmentConfig = entertainmentConfiguration.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var workoutUri = new Uri(_workoutConfig.Url);

            var workoutRegistration = new AgentServiceRegistration()
            {
                Address = workoutUri.Host,
                Name = _workoutConfig.ServiceName,
                ID = _workoutConfig.ServiceId,
                Port = workoutUri.Port,
                Tags = new[] { _workoutConfig.ServiceName }
            };

            var skillUri = new Uri(_skillConfig.Url);

            var skillRegistration = new AgentServiceRegistration()
            {
                Address = skillUri.Host,
                Name = _skillConfig.ServiceName,
                ID = _skillConfig.ServiceId,
                Port = skillUri.Port,
                Tags = new[] { _skillConfig.ServiceName }
            };

            var entertainmentUri = new Uri(_entertainmentConfig.Url);

            var entertainmentRegistration = new AgentServiceRegistration()
            {
                Address = entertainmentUri.Host,
                Name = _entertainmentConfig.ServiceName,
                ID = _entertainmentConfig.ServiceId,
                Port = entertainmentUri.Port,
                Tags = new[] { _entertainmentConfig.ServiceName }
            };

            await _client.Agent.ServiceDeregister(_workoutConfig.ServiceId, cancellationToken);
            await _client.Agent.ServiceDeregister(_skillConfig.ServiceId, cancellationToken);
            await _client.Agent.ServiceDeregister(_entertainmentConfig.ServiceId, cancellationToken);

            await _client.Agent.ServiceRegister(workoutRegistration, cancellationToken);
            await _client.Agent.ServiceRegister(skillRegistration, cancellationToken);
            await _client.Agent.ServiceRegister(entertainmentRegistration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.Agent.ServiceDeregister(_workoutConfig.ServiceId, cancellationToken);
            await _client.Agent.ServiceDeregister(_skillConfig.ServiceId, cancellationToken);
            await _client.Agent.ServiceDeregister(_entertainmentConfig.ServiceId, cancellationToken);
        }
    }
}
