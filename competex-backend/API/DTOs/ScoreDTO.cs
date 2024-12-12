using competex_backend.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace competex_backend.API.DTOs
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(TimeScoreDTO), typeDiscriminator: "TimeScore")]
    [JsonDerivedType(typeof(SetScoreDTO), typeDiscriminator: "SetScore")]
    [JsonDerivedType(typeof(PointScoreDTO), typeDiscriminator: "PointScore")]
    [JsonDerivedType(typeof(TimeFaultScoreDTO), typeDiscriminator: "TimeFaultScore")]


    [SwaggerDiscriminator("$type")]
    [SwaggerSubType(typeof(TimeScoreDTO), DiscriminatorValue = "TimeScore")]
    [SwaggerSubType(typeof(SetScoreDTO), DiscriminatorValue = "SetScore")]
    [SwaggerSubType(typeof(PointScoreDTO), DiscriminatorValue = "PointScore")]
    [SwaggerSubType(typeof(TimeFaultScoreDTO), DiscriminatorValue = "TimeFaultScore")]

    public abstract class ScoreDTO : Identifiable
    {
        public MatchDTO? Match { get; set; }
        public Guid? MatchId { get; set; }
        public EkvipageDTO? Participant { get; set; }
        public Guid? ParticipantId { get; set; }
        //public ScoreType ScoreType { get; set; } // Do we really need this?
        public List<PenaltyDTO>? Penalties { get; set; } = new List<PenaltyDTO>();
        public List<Guid>? PenaltyIds { get; set; } = new List<Guid>();

        // Centralized handling of ScoreValue
        [JsonIgnore]
        public object ScoreValue
        {
            get => GetScoreValue();
            set => SetScoreValue(value);
        }

        // Abstract methods to be implemented in derived classes
        protected abstract object GetScoreValue();
        protected abstract void SetScoreValue(object value);
    }

    public class TimeScoreDTO : ScoreDTO
    {
        public TimeSpan Time { get; set; }

        protected override object GetScoreValue() => Time;

        protected override void SetScoreValue(object value)
        {
            if (value is string timeString)
            {
                Time = TimeSpan.Parse(timeString);
            }
            else if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.String)
            {
                Time = TimeSpan.Parse(jsonElement.GetString());
            }
            else if (value is TimeSpan timeSpan)
            {
                Time = timeSpan;
            }
            else
            {
                throw new InvalidCastException($"Unable to cast {value?.GetType().Name} to TimeSpan.");
            }
        }
    }

    public class SetScoreDTO : ScoreDTO
    {
        public int SetsWon { get; set; }

        protected override object GetScoreValue() => SetsWon;

        protected override void SetScoreValue(object value)
        {
            if (value is int intValue)
            {
                SetsWon = intValue;
            }
            else if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Number)
            {
                SetsWon = jsonElement.GetInt32();
            }
            else
            {
                throw new InvalidCastException($"Unable to cast {value?.GetType().Name} to int for SetsWon.");
            }
        }
    }

    public class PointScoreDTO : ScoreDTO
    {
        public int Points { get; set; }

        protected override object GetScoreValue() => Points;

        protected override void SetScoreValue(object value)
        {
            if (value is int intValue)
            {
                Points = intValue;
            }
            else if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Number)
            {
                Points = jsonElement.GetInt32();
            }
            else
            {
                throw new InvalidCastException($"Unable to cast {value?.GetType().Name} to int for Points.");
            }
        }
    }

    public class TimeFaultScoreDTO : ScoreDTO
    {
        public int Faults { get; set; }
        public TimeSpan Time { get; set; }

        protected override object GetScoreValue() => (Faults, Time);

        // SetScoreValue accepts a tuple or other supported input
        protected override void SetScoreValue(object value)
        {
            if (value is ValueTuple<int, TimeSpan> tuple)
            {
                Faults = tuple.Item1;
                Time = tuple.Item2;
            }
            else if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
            {
                // Example for deserialization from a JSON object
                if (jsonElement.TryGetProperty("Faults", out JsonElement faultsElement) && faultsElement.ValueKind == JsonValueKind.Number)
                {
                    Faults = faultsElement.GetInt32();
                }

                if (jsonElement.TryGetProperty("Time", out JsonElement timeElement) && timeElement.ValueKind == JsonValueKind.String)
                {
                    if (TimeSpan.TryParse(timeElement.GetString(), out var parsedTime))
                    {
                        Time = parsedTime;
                    }
                }
            }
            else
            {
                throw new InvalidCastException($"Unable to cast {value?.GetType().Name} to (int, DateTime) for ResultDTO.");
            }
        }
    }
}
