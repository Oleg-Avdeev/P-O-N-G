namespace Pong.Game.PaddleControllers
{
    public enum PaddleType
    {
        AI,
        Player,
        Remote,
    }

    public interface IPaddleController
    {
        float GetPosition();
    }
}