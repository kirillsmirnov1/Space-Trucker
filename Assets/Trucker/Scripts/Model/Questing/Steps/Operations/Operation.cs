namespace Trucker.Model.Questing.Steps.Operations
{
    public abstract class Operation : Step // IMPR don't like naming, it should be something like Action or Step, but in between 
    {
        public override StepType Type => StepType.Operation;
    }
}