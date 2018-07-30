namespace SignalRTestDemo
{
    using Newtonsoft.Json;
    /// <summary>
    /// Add Figure ShapeModel
    /// </summary>
    public class ShapeModel
    {
        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        [JsonProperty("left")]
        public double Left { get; set; }
        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        [JsonProperty("top")]
        public double Top { get; set; }
        /// <summary>
        /// Gets or sets the last update by.
        /// </summary>
        /// <value>
        /// The last update by.
        /// </value>
        [JsonIgnore]
        public string LastUpdateBy { get; set; }
    }
}