namespace SignalRTestDemo
{
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Threading;
    public class Broadcaster
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static readonly Lazy<Broadcaster>
            _instance = new Lazy<Broadcaster>(
                () => new Broadcaster());

        /// <summary>
        /// The broadcaster interval
        /// </summary>
        private readonly TimeSpan BroadcasterInterval =
            TimeSpan.FromMilliseconds(40);
        /// <summary>
        /// The hub context
        /// </summary>
        private readonly IHubContext _hubContext;
        /// <summary>
        /// The broadcast loop
        /// </summary>
        private Timer _broadcastLoop;
        /// <summary>
        /// The model
        /// </summary>
        private ShapeModel _model;
        /// <summary>
        /// The model update
        /// </summary>
        private bool _modelUpdate;

        public Broadcaster()
        {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<MoveShapeHub>();
            _model = new ShapeModel();
            _modelUpdate = false;

            _broadcastLoop = new Timer(
                BroadcastShape, null,
                BroadcasterInterval,
                BroadcasterInterval);
        }
        /// <summary>
        /// Broadcasts the shape.
        /// </summary>
        /// <param name="state">The state.</param>
        public void BroadcastShape(object state)
        {
            if (_modelUpdate)
            {
                _hubContext.Clients.AllExcept(_model.LastUpdateBy).updateShape(_model);
                _modelUpdate = false;
            }
        }
        /// <summary>
        /// Updates the shape.
        /// </summary>
        /// <param name="clientModel">The client model.</param>
        public void UpdateShape(ShapeModel clientModel)
        {
            _model = clientModel;
            _modelUpdate = true;
        }
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Broadcaster Instance => _instance.Value;
    }
}