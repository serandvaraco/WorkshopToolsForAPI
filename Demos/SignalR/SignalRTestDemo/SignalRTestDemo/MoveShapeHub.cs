
namespace SignalRTestDemo
{
    using Microsoft.AspNet.SignalR;

    public class MoveShapeHub : Hub
    {
        /// <summary>
        /// The broadcaster
        /// </summary>
        private Broadcaster _broadcaster;
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveShapeHub"/> class.
        /// </summary>
        public MoveShapeHub() : this(Broadcaster.Instance) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveShapeHub"/> class.
        /// </summary>
        /// <param name="broadcaster">The broadcaster.</param>
        public MoveShapeHub(Broadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <param name="clientModel">The client model.</param>
        public void UpdateModel(ShapeModel clientModel)
        {
            clientModel.LastUpdateBy = Context.ConnectionId;
            _broadcaster.UpdateShape(clientModel);
        }


    }
}